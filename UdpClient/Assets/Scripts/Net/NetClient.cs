using Google.Protobuf;
using Proto;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEngine;
using UnityEngine.UIElements;

namespace Net
{
    public class NetClient : Singleton<NetClient>
    {
        private Socket socket;
        private EndPoint serverEndPoint;
        private NetPackage netPackage;
        private Dictionary<int, NetEventHandle> messageEventHandle = new Dictionary<int, NetEventHandle>();
        private Queue<NetMsg> recevieMessage = new Queue<NetMsg>();
        private int sendMsgId = 0;//向服务器发送消息的编号
        private Queue<NetSendData> netSendDatas = new Queue<NetSendData>();
        private const int SENDDATATIMER = 5000;//已经发送消息维护时间
        public void StartClient()
        {
            int port = 1995;
            string ip = "127.0.0.1";
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            IPAddress iPAddress = IPAddress.Parse(ip);
            IPEndPoint iPEndPoint = new IPEndPoint(iPAddress, port);
            socket.Bind(iPEndPoint);
            serverEndPoint = new IPEndPoint(iPAddress, 1994);
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
            byte[] msgTypeData = BitConverter.GetBytes((int)msgType);
            int length = bodyLength + NetPackage.HeadLength;//最终发送协议包长度
            byte[] sendBuffer = new byte[length];
            msgTypeData.CopyTo(sendBuffer, NetPackage.HeadLength);
            messageData.CopyTo(sendBuffer, NetPackage.HeadLength + NetPackage.MsgTypeLength);
            SendMessage(sendBuffer);
        }
        public void SendMessage(byte[] data)
        {
            //把消息号加入到消息中去
            sendMsgId++;
            byte[] sendMsgIdData = BitConverter.GetBytes(sendMsgId);
            sendMsgIdData.CopyTo(data, 0);
            netSendDatas.Enqueue(new NetSendData(data));
            socket.BeginSendTo(data, 0, data.Length, SocketFlags.None, serverEndPoint, SendCallBack, socket);
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
            ClearSendDatas();
        }

        ///<summary>维护已经发送的消息</summary>
        private void ClearSendDatas()
        {
            long timer = (long)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalMilliseconds;
            while (netSendDatas.Count > 0)
            {
                NetSendData netSendData = netSendDatas.Peek();
                long passTimer = timer - netSendData.timer;
                if (passTimer <= SENDDATATIMER)
                    return;
                netSendDatas.Dequeue();
                netSendData = netSendDatas.Peek();
            }
        }

    }
}