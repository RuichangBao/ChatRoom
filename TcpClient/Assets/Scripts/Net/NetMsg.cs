using Google.Protobuf;
using System;

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
}
