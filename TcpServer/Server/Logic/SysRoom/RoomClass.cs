using Proto;
using Server.Net;

namespace Server.Logic.SysRoom
{
    public class RoomClass
    {
        public int roomId;
        public List<NetSession> listNetSessions = new List<NetSession>();
        public RoomClass(int roomId)
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

        public List<UserData> GetUsers()
        {
            List<UserData> listUserDatas = new List<UserData>();
            for (int i = 0,length = listNetSessions.Count; i < length; i++)
            {
                NetSession netSession = listNetSessions[i];
                listUserDatas.Add(new UserData { UserId = netSession.userId, Name = netSession.name });
            }
            return listUserDatas;
        }

        public bool SessionInRoom(NetSession netSession)
        {
            for (int i = 0, length = listNetSessions.Count; i < length; i++)
            {
                if (netSession == listNetSessions[i])
                    return true;
            }
            return false;
        }

        public int GetUserNum()
        {
            return listNetSessions.Count;
        }

        public void BroadcastUserJoin(NetSession netSession)
        {
            ResponseOtherJoin response = new ResponseOtherJoin
            {
                UserData = new UserData { UserId = netSession.userId, Name = netSession.name }
            };
            byte[] data = NetSerializeUtil.Serialize(MsgType.EnResponseOtherJoin, response);
            for (int i = 0, length = listNetSessions.Count; i < length; i++)
            {
                if (listNetSessions[i] == netSession)
                    continue;
                listNetSessions[i].SendMessage(data);
            }
        }
        public void BroadcastUserLeave(int userId)
        {
            ResponseOtherLeave response = new ResponseOtherLeave
            {
                UserId = userId,
            };
            byte[] data = NetSerializeUtil.Serialize(MsgType.EnResponseOtherLeave, response);
            for (int i = 0, length = listNetSessions.Count; i < length; i++)
            {
                if (listNetSessions[i].userId == userId)
                    continue;
                listNetSessions[i].SendMessage(data);
            }
        }
        public void BroadcastUserChat(int userId, string msg)
        {
            ResponseOtherSend response = new ResponseOtherSend
            {
                UserId = userId,
                Msg = msg,
            };
            byte[] data = NetSerializeUtil.Serialize(MsgType.EnResponseOtherSend, response);
            for (int i = 0, length = listNetSessions.Count; i < length; i++)
            {
                if (listNetSessions[i].userId == userId)
                    continue;
                listNetSessions[i].SendMessage(data);
            }
        }
    }
}
