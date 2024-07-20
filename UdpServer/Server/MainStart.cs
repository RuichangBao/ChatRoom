using Server.Logic;
using Server.Logic.SysRoom;
using Server.Net;

namespace Server
{
    internal class MainStart
    {
        static void Main(string[] args)
        {
            NetServer.Instance.StartServer();
            _ = SysRoom.Instance;
            Console.WriteLine("服务器启动成功!");
            while (true) { }
        }
    }
}