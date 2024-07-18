using Google.Protobuf;
using Proto;
using Server;
using Server.Net;
using System.ComponentModel;
using System.Diagnostics;

namespace Test
{
    internal class Test
    {
        static void Main(string[] args)
        {
            TestRequest request = new TestRequest
            {
                Num1 = 10000,
                //Num2 = 2,
            };

            byte[] datas = MessageExtensions.ToByteArray(request);
            Console.WriteLine(datas.Length);
            Console.WriteLine("Hello, World!");
        }
    }
}