using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using UnityEngine;

namespace Net
{
    public class NetClient : Singleton<NetClient>
    {
        private Socket socket;
        private int port = 1994;
        private string ip = "127.0.0.1";
        private NetPackage netPackage;
        private const int maxBufferLength = 2048;
        private byte[] receiveBuffer = new byte[maxBufferLength];

        public void StartClient()
        {
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPAddress iPAddress = IPAddress.Parse(ip);
            IPEndPoint iPEndPoint = new IPEndPoint(iPAddress, port);
            socket.BeginConnect(iPEndPoint, ConnectCallBack, socket);
            Debug.LogError("���ӳɹ�");
        }

        private void ConnectCallBack(IAsyncResult ar)
        {
            Debug.Log("�첽���ӵ�������" + socket.Connected);
            if (!socket.Connected)
            {
                Debug.LogError("���ӷ�����ʧ�ܣ�����������");
                return;
            }
            socket.BeginReceive(receiveBuffer, 0, maxBufferLength, SocketFlags.None, AsyncReceive, socket);
        }


        private void AsyncReceive(IAsyncResult ar)
        {
            try
            {
                NetPackage netPackage = ar.AsyncState as NetPackage;
                int length = socket.EndReceive(ar);//���ν��յ��ֽ���
                if (length <= 0)
                {
                    Console.WriteLine("�Ѿ�����");
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
                Console.WriteLine("�ͻ��˷��������ߣ�" + ex.ToString());
            }
        }

    }
}