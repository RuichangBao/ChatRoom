using Google.Protobuf.Reflection;

namespace Google.Protobuf
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IMessage imessage = new TestRequest();
            
            byte[]data = imessage.ToByteArray();
            CodedOutputStream output = new CodedOutputStream(data);
            imessage.WriteTo(output);
            output.WriteRawMessage(imessage);
            CodedOutputStream.ComputeStringSize("");
            CodedOutputStream.ComputeUInt32Size(100);
        }
    }
    public class TestRequest : IMessage
    {
        public MessageDescriptor Descriptor => throw new NotImplementedException();

        public int CalculateSize()
        {
            throw new NotImplementedException();
        }

        public void MergeFrom(CodedInputStream input)
        {
            throw new NotImplementedException();
        }

        public void WriteTo(CodedOutputStream output)
        {
            throw new NotImplementedException();
        }
    }
}