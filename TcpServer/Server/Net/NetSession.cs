﻿using Google.Protobuf;
using Pool;
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
        private Action<int> closeSession;

        public void SendMessage(MsgType msgType, IMessage message)
        {
            byte[] sendBuffer = NetSerializeUtil.Serialize(msgType, message);
            SendMessage(sendBuffer);
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
            if (socket != null)
            {
                socket.Close();
                socket.Dispose();
            }
            closeSession?.Invoke(userId);
        }

        #region 对象池
        public static NetSession GetFetch(Socket socket, int userId,Action<int> closeSession)
        {
            NetSession netSession = PoolManager.GetFetch<NetSession>();
            netSession.socket = socket;
            netSession.userId = userId;
            netSession.name = userId.ToString();
            netSession.netPackage = NetPackage.GetFetch();
            netSession.networkStream = new NetworkStream(socket);
            netSession.socket.BeginReceive(netSession.netPackage.headBuffer, 0, NetPackage.HeadLength, SocketFlags.None, netSession.AsyncReceiveHead, netSession.netPackage);
            netSession.closeSession = closeSession;
            return netSession;
        }
        public void Recycle()
        {
            this.netPackage.Recycle();
            this.netPackage = null;
            this.socket = null;
            this.userId = 0;
            this.name = null;
            this.networkStream = null;
            PoolManager.Recycle(this);
        }
        #endregion
    }
}
