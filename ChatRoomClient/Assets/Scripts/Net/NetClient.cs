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
        private const int maxBufferLength = 2048;
        private byte[] receiveBuffer = new byte[maxBufferLength];

        public void StartClient()
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
            socket.BeginReceive(receiveBuffer, 0, maxBufferLength, SocketFlags.None, AsyncReceive, socket);
        }


        private void AsyncReceive(IAsyncResult ar)
        {
            try
            {
                int length = socket.EndReceive(ar);//���ν��յ��ֽ���
                if (length <= 0)
                {
                    Console.WriteLine("�Ѿ�����");
                    return;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("�ͻ��˷��������ߣ�" + ex.ToString());
            }
        }

    }
}