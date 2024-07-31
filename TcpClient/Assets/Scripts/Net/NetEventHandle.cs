using Google.Protobuf;
using System;

namespace Net
{
    public struct NetEventHandle
    {
        public MessageParser parser;
        public Action<IMessage> callBack;
        public NetEventHandle(MessageParser parser, Action<IMessage> callBack)
        {
            this.parser = parser;
            this.callBack = callBack;
        }
    }
}
