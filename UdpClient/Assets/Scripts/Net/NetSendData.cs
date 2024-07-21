using System;

namespace Net
{
    /// <summary>已经发送的消息缓存，用来补发数据</summary>
    public struct NetSendData
    {
        public long timer;//发送时间(ms)
        public byte[] data;
        public NetSendData(byte[]data)
        {
            this.timer = (long)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalMilliseconds;
            this.data = data;
        }
    }
}
