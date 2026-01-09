using Google.Protobuf;
using Proto;
using System;
using System.Threading;

namespace Net
{
    public struct NetMsg
    {
        public IMessage message;
        public Action<IMessage> callBack;
        public NetMsg( IMessage message, Action<IMessage> callBack)
        {
            this.message = message;
            this.callBack = callBack;
        }
    }
    public struct ReqMsg
    {
        public MsgType msgType;
        public IMessage message;
        public ReqMsg(MsgType msgType, IMessage message)
        {
            this.msgType = msgType;
            this.message = message;
        }
    }
}
