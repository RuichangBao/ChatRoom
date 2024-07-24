using Proto;
using Server.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Logic.SysRoom
{
    public class RoomData
    {
        public int roomId;
        public List<NetSession> listNetSessions = new List<NetSession>();
        public RoomData(int roomId)
        {
            this.roomId = roomId;
        }
        public void AddSession(NetSession netSession)
        {
            if (listNetSessions.Contains(netSession))
                return;
            listNetSessions.Add(netSession);
        }
        public void RemoveSession(NetSession netSession)
        {
            if (!listNetSessions.Contains(netSession))
                return;
            listNetSessions.Remove(netSession);
        }

        public void BroadcastUserJoin(NetSession netSession)
        {
            ResponseOtherJoin response = new ResponseOtherJoin
            {
                UserId = netSession.userId,
            };
            byte[] data = NetSession.GetSendData(MsgType.EnResponseOtherJoin, response);
            for (int i = 0, length = listNetSessions.Count; i < length; i++)
            {
                if (listNetSessions[i] == netSession)
                    continue;
                listNetSessions[i].SendMessage(data);
            }
        }

    }
}
