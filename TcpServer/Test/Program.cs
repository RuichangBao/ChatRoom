using Server;


namespace Test
{
    internal class Program
    {
        static void Main(string[] args)
        {
            byte[] data = { 1, 2, 3, 4 };
            byte[] expected = new byte[10];
            data.CopyTo(expected, 3);
            Console.WriteLine(expected.ToString(10));
            Queue<byte> queue = new Queue<byte>();  
            queue.Dequeue
        }
    }
}