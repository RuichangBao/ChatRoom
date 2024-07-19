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
            Debug.Log("�첽���ӵ�������" + socket.Connected);
            if (!socket.Connected)
            {
                Debug.LogError("���ӷ�����ʧ�ܣ�����������");
                return;
            }
            Debug.LogError("���ӳɹ�");
            networkStream = new NetworkStream(socket);
            netPackage = new NetPackage();
            socket.BeginReceive(netPackage.headBuffer, netPackage.headIndex, NetPackage.HeadLength, SocketFlags.None, AsyncReceiveHead, socket);
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
            byte[] bodyLengthData = BitConverter.GetBytes(bodyLength);
            byte[] msgTypeData = BitConverter.GetBytes((int)msgType);
            int length = bodyLength + NetPackage.HeadLength;//���շ���Э�������
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