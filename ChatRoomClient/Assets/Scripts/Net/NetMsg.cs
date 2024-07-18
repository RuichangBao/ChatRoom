using Google.Protobuf;
using Proto;
using System;

namespace Net
{
    public struct NetMsg
    {
        public MsgType msgType;
        public byte[] data;
        private const int lengthCount = 4;
        private const int lengthMsgType = 4;
        public NetMsg(MsgType msgType, IMessage message)
        {
            this.msgType = msgType;
            byte[] messageData = message.ToByteArray();
            int messageLength = messageData.Length + lengthCount;//消息体长度，包括message长度和message长度的长度
            byte[] lengthData = BitConverter.GetBytes(messageLength);
            byte[] msgTypeData = BitConverter.GetBytes((int)msgType);
            data = new byte[lengthMsgType + lengthCount + messageData.Length];
            msgTypeData.CopyTo(data, 0);
            lengthData.CopyTo(data, lengthCount);
            messageData.CopyTo(data, lengthCount + lengthMsgType);

            Console.WriteLine("构造完成");
        }
    }
}
