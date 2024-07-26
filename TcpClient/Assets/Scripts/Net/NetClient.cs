using Google.Protobuf;
using Proto;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using UnityEngine;

namespace Net
{
    public class NetClient : Singleton<NetClient>
    {
        private Socket socket;

        private NetPackage netPackage;
        private Dictionary<ushort, NetEventHandle> messageEventHandle = new Dictionary<ushort, NetEventHandle>();
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
            ushort msgType = netPackage.GetMsgType();
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
            messageEventHandle[(ushort)msgType] = netEventHandle;
        }

        public void SendMessage(MsgType msgType, IMessage message)
        {
            byte[] messageData = message.ToByteArray();
            int bodyLength = messageData.Length + NetPackage.MsgTypeLength;
            int length = bodyLength + NetPackage.HeadLength;//���շ���Э�������
            byte[] datas = new byte[length];
            //����
            datas[0] = (byte)bodyLength;
            datas[1] = (byte)(bodyLength >> 8);
            //MsgType Э���
            ushort cmd = (ushort)msgType;
            datas[2] = (byte)cmd;
            datas[3] = (byte)(cmd >> 8);
            messageData.CopyTo(datas, NetPackage.HeadLength + NetPackage.MsgTypeLength);
            SendMessage(datas);
            {
                Debug.LogError(datas[0]);
                Debug.LogError(datas[1]);
                byte[]AAA = BitConverter.GetBytes((ushort)bodyLength);
                Debug.LogError(AAA[0]);
                Debug.LogError(AAA[1]);
                Debug.LogError(datas[2]);
                Debug.LogError(datas[3]);
            }
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