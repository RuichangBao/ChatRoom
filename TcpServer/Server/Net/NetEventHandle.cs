using Google.Protobuf;

namespace Server.Net
{
    public struct NetEventHandle
    {
        public MessageParser parser;
        public Action<NetSession, IMessage> callBack;
        public NetEventHandle(MessageParser parser, Action<NetSession, IMessage> callBack)
        {
            this.parser = parser;
            this.callBack = callBack;
        }
    }
}
