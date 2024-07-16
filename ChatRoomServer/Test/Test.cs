using Proto;

namespace Test
{
    internal class Test
    {
        static void Main(string[] args)
        {
            TestRequest request = new TestRequest
            {
                MsgType = MsgType.EnTestRequest,
                Num1 = 1,
                Num2 = 2,
                Str = "dsa",
            };
            Console.WriteLine("Hello, World!");
        }
    }
}