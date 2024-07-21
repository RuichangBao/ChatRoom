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
        public int userId;
        public NetSession()
        {
            Console.WriteLine("客户端链接成功");
            this.userId = userId;
            netPackage = new NetPackage();
        }

        public void SendMessage(MsgType msgType, IMessage message)
        {
            byte[] messageData = message.ToByteArray();
            int bodyLength = messageData.Length + NetPackage.MsgTypeLength;
            byte[] bodyLengthData = BitConverter.GetBytes(bodyLength);
            byte[] msgTypeData = BitConverter.GetBytes((int)msgType);
            int length = bodyLength + NetPackage.HeadLength;//最终发送协议包长度
            byte[] sendBuffer = new byte[length];
            bodyLengthData.CopyTo(sendBuffer, 0);
            msgTypeData.CopyTo(sendBuffer, NetPackage.HeadLength);
            messageData.CopyTo(sendBuffer, NetPackage.HeadLength + NetPackage.MsgTypeLength);
            SendMessage(sendBuffer);
        }
        private void SendMessage(byte[] data)
        {
        }

        private void SendCallBack(IAsyncResult ar)
        {
        }

        private void AsyncReceiveHead(IAsyncResult ar)
        {
            
        }

        private void AsyncReceiveBody(IAsyncResult ar)
        {
        }
        //解析协议
        private void AnalyseMessage()
        {
            int msgType = netPackage.GetMsgType();
            if (!NetServer.Instance.messageEventHandle.TryGetValue(msgType, out NetEventHandle netEventHandle))
            {
                Console.WriteLine("协议未注册：" + msgType);
            }
            else
            {
                IMessage message = netPackage.GetMessage(netEventHandle.parser);
                Console.WriteLine("收到客户端协议：" + message);
                netEventHandle.callBack(this, message);
            }
            netPackage.Reset();
        }
        private void CloseSession()
        {

        }
    }
}
