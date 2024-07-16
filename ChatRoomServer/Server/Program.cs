using Server.Net;

namespace Server
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ServerNet.Instance.StartServer();
            Console.WriteLine("服务器启动成功!");
            while (true) { }
        }
    }
}