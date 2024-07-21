using Google.Protobuf;
using Proto;
using System.Net;
using System.Net.Sockets;


namespace Server.Net
{
    public class NetServer : Singleton<NetServer>
    {
        private Dictionary<EndPoint, NetSession> listNetSession = new Dictionary<EndPoint, NetSession>();
        private string ip = "127.0.0.1";
        private int port = 1994;
        private Socket socket;
        private int userId = 0;
        public Dictionary<int, NetEventHandle> messageEventHandle = new Dictionary<int, NetEventHandle>();



        private byte[] buffer = new byte[1024];

        #region 开启服务器，接收客户端链接
        public void StartServer()
        {
            //Udp是以报文的形式传输的 所以 SocketType = Dgram
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            IPAddress iPAddress = IPAddress.Parse(ip);
            IPEndPoint endPoint = new IPEndPoint(iPAddress, port);
            socket.Bind(endPoint);
            EndPoint clientEndPoint = new IPEndPoint(0, 0);
            socket.BeginReceiveFrom(buffer, 0, 1024, SocketFlags.None, ref clientEndPoint, Receive, socket); ;
        }
        #endregion

        private void Receive(IAsyncResult ar)
        {
            EndPoint clientEndPoint = new IPEndPoint(0, 0);
            int size = socket.EndReceiveFrom(ar, ref clientEndPoint);
            byte[] data = new byte[size];
            Array.Copy(buffer, 0, data, 0, size);
            NetSession netSession = GetNetSession(clientEndPoint);
            socket.BeginReceiveFrom(buffer, 0, 1024, SocketFlags.None, ref clientEndPoint, Receive, socket);
        }


        internal void Listen(MsgType msgType, MessageParser parser, Action<NetSession, IMessage> callback)
        {
            NetEventHandle netEventHandle = new NetEventHandle(parser, callback);
            messageEventHandle[(int)msgType] = netEventHandle;
        }

        private NetSession GetNetSession(EndPoint clientEndPoint)
        {
            if (!listNetSession.ContainsKey(clientEndPoint))
            {
                NetSession netSession = new NetSession();
                listNetSession[clientEndPoint] = netSession;
            }
            return listNetSession[clientEndPoint];
        }

    }
}
