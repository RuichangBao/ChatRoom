using Google.Protobuf;
using Proto;
using System.Net;
using System.Net.Sockets;

namespace Server.Net
{
    public class NetServer : Singleton<NetServer>
    {
        private Dictionary<int, NetSession> listNetSession = new Dictionary<int, NetSession>();
        private string ip = "127.0.0.1";
        private int port = 1994;
        private Socket socket;
        private int userId = 0;
        public Dictionary<int, NetEventHandle> messageEventHandle = new Dictionary<int, NetEventHandle>();
        
        
        #region 开启服务器，接收客户端链接
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
            listNetSession.Add(userId, netSession);
            socket.BeginAccept(AcceptCallBack, socket);//监听下个接入的客户端
            LinkSuccesResponse(netSession);//向客户端发送链接成功协议
        }
        #endregion


        internal void Listen(MsgType msgType, MessageParser parser, Action<NetSession,IMessage> callback)
        {
            NetEventHandle netEventHandle = new NetEventHandle(parser, callback);
            messageEventHandle[(int)msgType] = netEventHandle;
        }

        private void LinkSuccesResponse(NetSession netSession)
        {
            ResponseLinkSucces response = new ResponseLinkSucces
            {
                UserId = netSession.userId
            };
            netSession.SendMessage(MsgType.EnResponseLinkSucces, response);
        }

    }
}
