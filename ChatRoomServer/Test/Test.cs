using Proto;
using Server;
using Server.Net;
using System.ComponentModel;

namespace Test
{
    internal class Test
    {
        static void Main(string[] args)
        {
            TestRequest request = new TestRequest
            {
                Num1 = 1,
                Num2 = 2,
                Str = "dsa",
            };

            byte[] datas = BitConverter.GetBytes(100);
            Console.WriteLine(BitConverter.ToInt32(datas));
            Console.WriteLine("Hello, World!");
        }
    }
}