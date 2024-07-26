using Google.Protobuf;
using Proto;
using System.Net.Sockets;

namespace Server.Net
{
    /// <summary>
    /// 接入一个客户端，创建一个NetSession
    /// </summary>
    public class NetSession
    {
        private NetPackage netPackage;
        private Socket socket;
        public int userId;
        public string name;
        private NetworkStream networkStream;
        public NetSession(Socket socket, int userId)
        {
            Console.WriteLine("客户端链接成功");
            this.socket = socket;
            this.userId = userId;
            this.name = userId.ToString();
            netPackage = new NetPackage();
            networkStream = new NetworkStream(socket);
            socket.BeginReceive(netPackage.headBuffer, 0, NetPackage.HeadLength, SocketFlags.None, AsyncReceiveHead, netPackage);
        }

        public void SendMessage(MsgType msgType, IMessage message)
        {
            byte[] sendBuffer = GetSendData(msgType, message);
            SendMessage(sendBuffer);
        }
        /// <summary>
        /// 临时序列化接口
        /// </summary>
        public static byte[] GetSendData(MsgType msgType, IMessage message)
        {
            byte[] messageData = message.ToByteArray();
            //数据包长度
            int bodyLength = messageData.Length + NetPackage.MsgTypeLength;
            int length = bodyLength + NetPackage.HeadLength;//最终发送协议包长度
            byte[] sendBuffer = new byte[length];
            //长度
            sendBuffer[0] = (byte)bodyLength;
            sendBuffer[1] = (byte)(bodyLength >> 8);
            //MsgType 协议号
            ushort cmd = (ushort)msgType;
            sendBuffer[2] = (byte)cmd;
            sendBuffer[3] = (byte)(cmd >> 8);
            messageData.CopyTo(sendBuffer, NetPackage.HeadLength + NetPackage.MsgTypeLength);
            return sendBuffer;
        }
        public void SendMessage(byte[] data)
        {
            networkStream.BeginWrite(data, 0, data.Length, SendCallBack, networkStream);
        }

        private void SendCallBack(IAsyncResult ar)
        {
        }

        private void AsyncReceiveHead(IAsyncResult ar)
        {
            try
            {
                int length = socket.EndReceive(ar);//本次接收的字节数
                if (length <= 0)
                {
                    Console.WriteLine("已经下线");
                    CloseSession();
                    return;
                }
                netPackage.headIndex += length;
                if (netPackage.headIndex < NetPackage.HeadLength)
                {
                    socket.BeginReceive(netPackage.headBuffer, netPackage.headIndex, NetPackage.HeadLength - netPackage.headIndex, SocketFlags.None, AsyncReceiveHead, socket);
                }
                else
                {
                    netPackage.InitBodyBuff();
                    socket.BeginReceive(netPackage.bodyBuffer, 0, netPackage.bodyLength, SocketFlags.None, AsyncReceiveBody, socket);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("客户端非正常下线：" + ex.ToString());
                CloseSession();
            }
        }

        private void AsyncReceiveBody(IAsyncResult ar)
        {
            try
            {
                int length = socket.EndReceive(ar);//本次接收的字节数
                if (length <= 0)
                {
                    Console.WriteLine("已经下线");
                    CloseSession();
                    return;
                }
                netPackage.bodyIndex += length;
                if (netPackage.bodyIndex < netPackage.bodyLength)
                {
                    socket.BeginReceive(netPackage.bodyBuffer, netPackage.bodyIndex, netPackage.bodyLength - netPackage.bodyIndex, SocketFlags.None, AsyncReceiveBody, socket);
                }
                else
                {
                    AnalyseMessage();
                    socket.BeginReceive(netPackage.headBuffer, 0, NetPackage.HeadLength, SocketFlags.None, AsyncReceiveHead, socket);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("客户端非正常下线：" + ex.ToString());
                CloseSession();
            }
        }
        //解析协议
        private void AnalyseMessage()
        {
            ushort msgType = netPackage.GetMsgType();
            if (!NetServer.Instance.messageEventHandle.TryGetValue(msgType, out NetEventHandle netEventHandle))
            {
                Console.WriteLine("协议未注册：" + msgType);
            }
            else
            {
                IMessage message = netPackage.GetMessage(netEventHandle.parser);
                Console.WriteLine($"收到客户端协议，msgType：{msgType},data:{message}");
                netEventHandle.callBack(this, message);
            }
            netPackage.Reset();
        }
        private void CloseSession()
        {

        }
    }
}
