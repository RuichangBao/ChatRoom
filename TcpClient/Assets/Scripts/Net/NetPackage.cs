using Google.Protobuf;
using System;
using UnityEngine;

namespace Net
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

        public NetPackage()
        {
            headBuffer = new byte[HeadLength];
        }
        public void InitBodyBuff()
        {
            bodyIndex = 0;
            bodyLength = (ushort)(headBuffer[0] | (headBuffer[1] << 8));
            bodyBuffer = new byte[bodyLength];
        }
        public ushort GetMsgType()
        {
            byte key = bodyBuffer[0];
            NetSerializeUtil.EncryptData(bodyBuffer, key, 1);
            //C#小端，高位在右
            ushort msgType = (ushort)(bodyBuffer[1] | (bodyBuffer[2] << 8));
            return msgType;
        }
        public IMessage GetMessage(MessageParser parser)
        {
            byte[] data = new byte[bodyLength - KeyLength - HeadLength];
            Array.Copy(bodyBuffer, KeyLength + HeadLength, data, 0, bodyLength - KeyLength - HeadLength);
            IMessage message = parser.ParseFrom(data);
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
