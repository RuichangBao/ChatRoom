using Google.Protobuf;
using Pool;
using System.Net.Sockets;

namespace Server.Net
{
    public class NetPackage
    {
        public const ushort MsgTypeLength = 2;  //协议号长度
        public const byte KeyLength = 1;        //密钥长度
        public const ushort HeadLength = 2;
        public const ushort AllHeadLength = 5;
        public byte[] headBuffer = null;
        public int headIndex;

        public ushort bodyLength = 0;
        public byte[] bodyBuffer = null;
        public int bodyIndex;

        public void InitBodyBuff()
        {
            Console.WriteLine();
            bodyLength = BitConverter.ToUInt16(headBuffer, 0);
            bodyLength = (ushort)(headBuffer[0] | (headBuffer[1] << 8));
            bodyBuffer = new byte[bodyLength];
        }
        public ushort GetMsgType()
        {
            byte key = bodyBuffer[0];
            //NetSerializeUtil.EncryptData(bodyBuffer, key, 1);
            //C#小端，高位在右
            ushort msgType = (ushort)(bodyBuffer[1] | (bodyBuffer[2] << 8));
            return msgType;
        }

        public IMessage GetMessage(MessageParser parser)
        {
            IMessage message = parser.ParseFrom(bodyBuffer, MsgTypeLength + KeyLength, bodyLength - MsgTypeLength - KeyLength);
            return message;
        }
        public void Reset()
        {
            //必要清除
            headIndex = 0;
            bodyIndex = 0;
        }
        #region 对象池 
        public static NetPackage GetFetch()
        {
            NetPackage netSession = PoolManager.GetFetch<NetPackage>();
            netSession.headBuffer = new byte[HeadLength];
            return netSession;
        }
        public void Recycle()
        {
            this.headBuffer = null;
            this.headIndex = 0;
            this.bodyLength = 0;
            this.bodyBuffer = null;
            this.bodyIndex = 0;
            PoolManager.Recycle(this);
        }
        #endregion
    }
}
