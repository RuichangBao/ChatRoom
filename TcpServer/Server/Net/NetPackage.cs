using Google.Protobuf;

namespace Server.Net
{
    public class NetPackage
    {
        public const ushort MsgTypeLength = 2;//协议号长度
        public const ushort HeadLength = 2;
        public byte[] headBuffer = null;
        public int headIndex;

        public ushort bodyLength = 0;
        public byte[] bodyBuffer = null;
        public int bodyIndex;

        public NetPackage()
        {
            headBuffer = new byte[HeadLength];
        }
        public void InitBodyBuff()
        {
            Console.WriteLine();
            //bodyLength = BitConverter.ToUInt16(headBuffer, 0);
            bodyLength = (ushort)(headBuffer[0] | (headBuffer[1] << 8));
            bodyBuffer = new byte[bodyLength];
        }
        public ushort GetMsgType()
        {
            //C#小端，高位在右
            ushort msgType = (ushort)(bodyBuffer[0] | (bodyBuffer[1] << 8));
            return msgType;
        }
        public IMessage GetMessage(MessageParser parser)
        {
            IMessage message = parser.ParseFrom(bodyBuffer, MsgTypeLength, bodyLength - MsgTypeLength);
            return message;
        }
        public void Reset()
        {
            //必要清除
            headIndex = 0;
            bodyIndex = 0;
        }
    }
}
