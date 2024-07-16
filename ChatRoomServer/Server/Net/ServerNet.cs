﻿using System.Net;
using System.Net.Sockets;

namespace Server.Net
{
    public class ServerNet : Singleton<ServerNet>
    {
        private List<NetSession> listNetSession = new List<NetSession>();
        private string ip = "127.0.0.1";
        private int port = 1994;
        private Socket socket;
        private long userId = 0;
        public void StartServer()
        {
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPAddress iPAddress = IPAddress.Parse(ip);
            IPEndPoint endPoint = new IPEndPoint(iPAddress, port);
            try
            {
                socket.Bind(endPoint);
                socket.Listen(100);
                socket.BeginAccept(AcceptCallBack, socket);
                Console.WriteLine("服务器启动成功");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

        }
        ///<summary>开始接收客户端链接</summary>
        private void AcceptCallBack(IAsyncResult ar)
        {
            Socket clientSocket = socket.EndAccept(ar);
            userId++;
            NetSession netSession = new NetSession(clientSocket, userId);
            socket.BeginAccept(AcceptCallBack, socket);
        }
    }
}
