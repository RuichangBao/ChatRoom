using Google.Protobuf;
using Proto;


namespace Server.Net
{
    public class NetSerializeUtil
    {
        public static Random random = new Random();
        public static byte[] Serialize(MsgType msgType, IMessage message)
        {
            byte[] messageData = message.ToByteArray();
            //数据包长度
            int bodyLength = messageData.Length + NetPackage.MsgTypeLength + NetPackage.KeyLength;
            int length = bodyLength + NetPackage.HeadLength;//最终发送协议包长度
            byte[] datas = new byte[length];
            //长度
            datas[0] = (byte)bodyLength;
            datas[1] = (byte)(bodyLength >> 8);
            //密钥
            datas[2] = RandomKey();
            //MsgType 协议号
            ushort cmd = (ushort)msgType;
            datas[3] = (byte)cmd;
            datas[4] = (byte)(cmd >> 8);
            messageData.CopyTo(datas, NetPackage.AllHeadLength);
            //Console.WriteLine($"加密前：{datas.ToString(2)}");
            //EncryptData(datas, datas[2], 3);
            //Console.WriteLine($"加密后：{datas.ToString(2)}");
            return datas;
        }
        ///<summary>协议加密解密</summary>
        public static void EncryptData(byte[] data, byte key, int startIndex)
        {
            for (int i = startIndex, length = data.Length; i < length; i++)
            {
                data[i] ^= key;
                key = GetNextRandomKey(key);
            }
        }
        public static byte RandomKey()
        {
            byte key = (byte)random.Next(int.MaxValue);
            return key;
        }
        public static byte GetNextRandomKey(byte key)
        {
            int nextKey = key * 37817 % 66817 % 253;
            return (byte)nextKey;
        }
    }
}
