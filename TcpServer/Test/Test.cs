using Google.Protobuf;
using Proto;

namespace Test
{
    internal class Test
    {
        static void Main(string[] args)
        {
            RequestTest requestTest = new RequestTest { Num1 = 1, Num2 = 2 };
            byte[] data = requestTest.ToByteArray();
            RequestTest requestTest1 =  RequestTest.Parser.ParseFrom(data);
            Console.WriteLine(requestTest1.ToString());
            Console.ReadKey();
        }
    }
}