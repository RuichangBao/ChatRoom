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
        private int sendMsgId = 0;//�������������Ϣ�ı��
        private Queue<NetSendData> netSendDatas = new Queue<NetSendData>();
        private const int SENDDATATIMER = 5000;//�Ѿ�������Ϣά��ʱ��
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
                int length = socket.EndReceive(ar);//���ν��յ��ֽ���
                if (length <= 0)
                {
                    Debug.Log("�Ѿ�����");
                    return;
                }
                netPackage.headIndex += length;
                if (netPackage.headIndex < NetPackage.HeadLength)//��Ϣͷδ�������
                    socket.BeginReceive(netPackage.headBuffer, netPackage.headIndex, NetPackage.HeadLength - netPackage.headIndex, SocketFlags.None, AsyncReceiveHead, socket);
                else
                {
                    netPackage.InitBodyBuff();
                    socket.BeginReceive(netPackage.bodyBuffer, netPackage.bodyIndex, netPackage.bodyLength, SocketFlags.None, AsyncReceiveBody, socket);
                }
            }
            catch (Exception ex)
            {
                Debug.Log("�ͻ��˷��������ߣ�" + ex.ToString());
            }
        }
        private void AsyncReceiveBody(IAsyncResult ar)
        {
            int length = socket.EndReceive(ar);//���ν��յ��ֽ���
            if (length <= 0)
            {
                Debug.Log("�Ѿ�����");
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

        //����Э��
        private void AnalyseMessage()
        {
            int msgType = netPackage.GetMsgType();
            if (!messageEventHandle.TryGetValue(msgType, out NetEventHandle netEventHandle))
            {
                Debug.LogError("Э��δע�᣺" + msgType);
            }
            else
            {
                IMessage message = netPackage.GetMessage(netEventHandle.parser);
                NetMsg netMsg = new NetMsg(message, netEventHandle.callBack);
                Debug.Log($"��������Ϣ,MsgType:{(MsgType)msgType}    buffer{message}");
                recevieMessage.Enqueue(netMsg);//���յ�����Ϣ������Ϣ����
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
            int length = bodyLength + NetPackage.HeadLength;//���շ���Э�������
            byte[] sendBuffer = new byte[length];
            msgTypeData.CopyTo(sendBuffer, NetPackage.HeadLength);
            messageData.CopyTo(sendBuffer, NetPackage.HeadLength + NetPackage.MsgTypeLength);
            SendMessage(sendBuffer);
        }
        public void SendMessage(byte[] data)
        {
            //����Ϣ�ż��뵽��Ϣ��ȥ
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

        ///<summary>ά���Ѿ����͵���Ϣ</summary>
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