using Google.Protobuf;
using Proto;
using System;
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
        public long userId;
        public NetSession(Socket socket, long userId)
        {
            Console.WriteLine("客户端链接成功");
            this.socket = socket;
            this.userId = userId;
            netPackage = new NetPackage();
            socket.BeginReceive(netPackage.headBuffer, 0, NetPackage.headLength, SocketFlags.None, AsyncReceiveHead, netPackage);
        }

        public void SendMessage(MsgType msgType, IMessage message)
        {
            byte[] msgTypeData = BitConverter.GetBytes((int)msgType);
            byte[] msgData = SerializerUtil.Serializer(message);
            int msgLength = msgData.Length;
            byte[]lengthData = BitConverter.GetBytes(msgLength);
            NetMsg netMsg = new NetMsg(msgType, message);
        }
        public void SendMessage(byte[] datas)
        {

        }
        private void AsyncReceiveHead(IAsyncResult ar)
        {
            try
            {
                NetPackage netPackage = ar.AsyncState as NetPackage;
                int length = socket.EndReceive(ar);//本次接收的字节数
                if (length <= 0)
                {
                    Console.WriteLine("已经下线");
                    CloseSession();
                    return;
                }
                netPackage.headIndex += length;
                if (netPackage.headIndex < NetPackage.headLength)
                {
                    socket.BeginReceive(netPackage.headBuffer, netPackage.headIndex, NetPackage.headLength - netPackage.headIndex, SocketFlags.None, AsyncReceiveHead, netPackage);
                }
                else
                {
                    netPackage.InitBodyBuff();
                    socket.BeginReceive(netPackage.bodyBuffer, 0, netPackage.bodyLength, SocketFlags.None, AsyncReceiveBody, netPackage);
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
                NetPackage netPackage = ar.AsyncState as NetPackage;
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
                    socket.BeginReceive(netPackage.bodyBuffer, netPackage.bodyIndex, netPackage.bodyLength - netPackage.bodyIndex, SocketFlags.None, AsyncReceiveBody, netPackage);
                }
                else
                {
                    //IMessage netMsg = SerializerUtil.DeSerializer<IMessage>(netPackage.bodyBuffer);
                    //OnReciveMsg(netMsg);
                    netPackage.Reset();
                    socket.BeginReceive(netPackage.headBuffer, 0, NetPackage.headLength, SocketFlags.None, AsyncReceiveHead, netPackage);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("客户端非正常下线：" + ex.ToString());
                CloseSession();
            }
        }


        private void OnReciveMsg(IMessage netMsg)
        {

        }
        private void CloseSession()
        {

        }
    }
}
