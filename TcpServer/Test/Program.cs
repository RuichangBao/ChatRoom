using Server;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Proto;
using System.Threading.Channels;
using System.Reflection;

namespace Test
{
    internal class Program
    {
        static void Main(string[] args)
        {
            RequestTest requestTest = new RequestTest
            {
                Num1 = 100,
                Num2 = 200
            };
            Console.WriteLine(RequestTest.Parser);
            Test(requestTest);
        }

        static void Test(IMessage message)
        {
            Console.WriteLine(message.ToString());
            Type type = message.GetType();
            PropertyInfo info = type.GetProperty("Parser");
            Console.WriteLine(info.DeclaringType.Name);
        }
    }
}