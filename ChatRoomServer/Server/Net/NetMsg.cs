using Google.Protobuf;
using Proto;

namespace Server.Net
{
    public struct NetMsg
    {
        public MsgType msgType;
        public NetMsg(MsgType msgType, IMessage message)
        {
            this.msgType = msgType;
        }
    }
}
