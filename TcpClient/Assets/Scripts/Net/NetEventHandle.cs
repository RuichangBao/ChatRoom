using Google.Protobuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
