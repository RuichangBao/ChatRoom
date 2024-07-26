using Google.Protobuf;
using Proto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Net
{
    public class NetSerializeUtil
    {
        public static byte[] Serialize(MsgType msgType, IMessage message)
        {
            byte[] messageData = message.ToByteArray();
            //数据包长度
            int bodyLength = messageData.Length + NetPackage.MsgTypeLength;
            int length = bodyLength + NetPackage.HeadLength;//最终发送协议包长度
            byte[] datas = new byte[length];
            //长度
            datas[0] = (byte)bodyLength;
            datas[1] = (byte)(bodyLength >> 8);
            //MsgType 协议号
            ushort cmd = (ushort)msgType;
            datas[2] = (byte)cmd;
            datas[3] = (byte)(cmd >> 8);
            messageData.CopyTo(datas, NetPackage.HeadLength + NetPackage.MsgTypeLength);
            return datas;
        }
    }
}
