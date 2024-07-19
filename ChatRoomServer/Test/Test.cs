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
            int num = 1;
            byte[] data = BitConverter.GetBytes(num);
            for (int i = 0; i < data.Length; i++)
            {
                Console.WriteLine(data[i]);
            }
            int aaa = BitConverter.ToInt32(data);
            Console.WriteLine(  aaa);
            Console.ReadKey();
        }
    }
}