using Google.Protobuf;
using Proto;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using UnityEngine;
using UnityEngine.UIElements;

namespace Net
{
    public class NetClient : Singleton<NetClient>
    {
        private Socket socket;

        private NetPackage netPackage;
        private Dictionary<int, NetEventHandle> messageEventHandle = new Dictionary<int, NetEventHandle>();
        private Queue<NetMsg> recevieMessage = new Queue<NetMsg>();
        private NetworkStream networkStream;

        public void StartClient(string ip, int port)
        {
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPAddress iPAddress = IPAddress.Parse(ip);
            IPEndPoint iPEndPoint = new IPEndPoint(iPAddress, port);
            socket.BeginConnect(iPEndPoint, ConnectCallBack, socket);
        }

        private void ConnectCallBack(IAsyncResult ar)
        {
            Debug.Log("异步链接到服务器" + socket.Connected);
            if (!socket.Connected)
            {
                Debug.LogError("链接服务器失败，请重新链接");
                return;
            }
            Debug.LogError("链接成功");
            networkStream = new NetworkStream(socket);
            netPackage = new NetPackage();
            socket.BeginReceive(netPackage.headBuffer, netPackage.headIndex, NetPackage.HeadLength, SocketFlags.None, AsyncReceiveHead, socket);
        }


        private void AsyncReceiveHead(IAsyncResult ar)
        {
            try
            {
                int length = socket.EndReceive(ar);//本次接收的字节数
                if (length <= 0)
                {
                    Debug.Log("已经下线");
                    return;
                }
                netPackage.headIndex += length;
                if (netPackage.headIndex < NetPackage.HeadLength)//消息头未接收完成
                    socket.BeginReceive(netPackage.headBuffer, netPackage.headIndex, NetPackage.HeadLength - netPackage.headIndex, SocketFlags.None, AsyncReceiveHead, socket);
                else
                {
                    netPackage.InitBodyBuff();
                    socket.BeginReceive(netPackage.bodyBuffer, netPackage.bodyIndex, netPackage.bodyLength, SocketFlags.None, AsyncReceiveBody, socket);
                }
            }
            catch (Exception ex)
            {
                Debug.Log("客户端非正常下线：" + ex.ToString());
            }
        }
        private void AsyncReceiveBody(IAsyncResult ar)
        {
            int length = socket.EndReceive(ar);//本次接收的字节数
            if (length <= 0)
            {
                Debug.Log("已经下线");
                return;
            }
            netPackage.bodyIndex += length;
            if (netPackage.bodyIndex < netPackage.bodyLength)
                socket.BeginReceive(netPackage.bodyBuffer, netPackage.bodyIndex, netPackage.bodyLength, SocketFlags.None, AsyncReceiveBody, socket);
            else
            {
                AnalyseMessage();
                socket.BeginReceive(netPackage.headBuffer, netPackage.headIndex, NetPackage.HeadLength, SocketFlags.None, AsyncReceiveHead, socket);
            }
        }

        //解析协议
        private void AnalyseMessage()
        {
            int msgType = netPackage.GetMsgType();
            if (!messageEventHandle.TryGetValue(msgType, out NetEventHandle netEventHandle))
            {
                Debug.LogError("协议未注册：" + msgType);
            }
            else
            {
                IMessage message = netPackage.GetMessage(netEventHandle.parser);
                NetMsg netMsg = new NetMsg(message, netEventHandle.callBack);
                Debug.Log($"服务器消息,MsgType:{(MsgType)msgType}    buffer{message}");
                recevieMessage.Enqueue(netMsg);//将收到的消息放入消息队列
            }
            netPackage.Reset();
        }

        public void Listen(MsgType msgType, MessageParser parser, Action<IMessage> callBack)
        {
            NetEventHandle netEventHandle = new NetEventHandle(parser, callBack);
            messageEventHandle[(int)msgType] = netEventHandle;
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
            networkStream.BeginWrite(data, 0, data.Length, SendCallBack, networkStream);
        }
        private void SendCallBack(IAsyncResult ar)
        {
        }

        public void Update()
        {
            while (recevieMessage.Count > 0)
            {
                NetMsg netMsg = recevieMessage.Dequeue();
                netMsg.callBack?.Invoke(netMsg.message);
            }
        }
    }
}