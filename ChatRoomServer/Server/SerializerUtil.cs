using Google.Protobuf;

namespace Server
{
    public class SerializerUtil
    {

        public static byte[] Serializer(IMessage message)
        {
            byte[] datas = MessageExtensions.ToByteArray(message);
            return datas;
        }
        internal static IMessage DeSerializer(byte[] buffer)
        {
            return null;
        }
    }
}
