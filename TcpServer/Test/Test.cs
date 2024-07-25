using Google.Protobuf;
using Proto;
using Pool;
using System.Runtime.InteropServices;

namespace Test
{

    internal class Test
    {
        public static void Main()
        {
            //Queue<TestClass> queue = new Queue<TestClass>();
            //TestClass testClass1 = new TestClass();
            //TestClass testClass2 = new TestClass();
            //TestClass testClass3 = new TestClass();

            //AAA(testClass1);
            //AAA(testClass2);
            //AAA(testClass3);
            //queue.Enqueue(testClass1);
            //queue.Enqueue(testClass2);
            //queue.Enqueue(testClass3);
            //AAA(testClass1);
            //AAA(testClass2);
            //AAA(testClass3);
            string str = "";
            for (int i = 0; i < 10; i++)
            {
                str+= i.ToString();
                Console.ReadKey();
            }
        }
        static void AAA(object obj)
        {
           
        }
    }
    public class TestClass
    {
        public int Id { get; set; }
        public static TestClass GetFetch()
        {
            return PoolManager.GetFetch<TestClass>();
        }
        ~TestClass()
        {
            Console.WriteLine("TestClass析构函数");
        }
        public void Recycle()
        {
            PoolManager.Recycle(this);
        }
    }

    public class TestClass2
    {
        public int num = 122222222;
    }
}