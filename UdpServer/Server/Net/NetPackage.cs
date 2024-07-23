using Google.Protobuf;

namespace Server.Net
{
    public class NetPackage
    {
        public const short MsgTypeLength = 2;//协议号长度
        public const int HeadLength = 2;
        public byte[] headBuffer = null;
        public int headIndex;

        public short bodyLength = 0;
        public byte[] bodyBuffer = null;
        public int bodyIndex;

        public NetPackage()
        {
            headBuffer = new byte[HeadLength];
        }
        public void InitBodyBuff()
        {
            Console.WriteLine();
            bodyLength = BitConverter.ToInt16(headBuffer, 0);
            bodyBuffer = new byte[bodyLength];
        }
        public int GetMsgType()
        {
            int msgType = BitConverter.ToInt16(bodyBuffer, 0);
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
