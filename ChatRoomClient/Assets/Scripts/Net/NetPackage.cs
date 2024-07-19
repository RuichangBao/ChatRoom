using Google.Protobuf;
using System;
using UnityEngine;

namespace Net
{
    public class NetPackage
    {
        public const int MsgTypeLength = 4;//协议号长度
        public const int HeadLength = 4;
        public byte[] headBuffer = null;
        public int headIndex;

        public int bodyLength = 0;
        public byte[] bodyBuffer = null;
        public int bodyIndex;
        
        public NetPackage()
        {
            headBuffer = new byte[HeadLength];
        }
        public void InitBodyBuff()
        {
            bodyIndex = 0;
            bodyLength = BitConverter.ToInt32(headBuffer, 0);
            bodyBuffer = new byte[bodyLength];
        }
        public int GetMsgType()
        {
           int msgType = BitConverter.ToInt32(bodyBuffer, 0);
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
