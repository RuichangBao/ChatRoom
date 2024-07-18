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
            Debug.Log("异步链接到服务器" + socket.Connected);
            if (!socket.Connected)
            {
                Debug.LogError("链接服务器失败，请重新链接");
                return;
            }
            Debug.LogError("链接成功");
            socket.BeginReceive(receiveBuffer, 0, maxBufferLength, SocketFlags.None, AsyncReceive, socket);
        }


        private void AsyncReceive(IAsyncResult ar)
        {
            try
            {
                int length = socket.EndReceive(ar);//本次接收的字节数
                if (length <= 0)
                {
                    Console.WriteLine("已经下线");
                    return;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("客户端非正常下线：" + ex.ToString());
            }
        }

    }
}