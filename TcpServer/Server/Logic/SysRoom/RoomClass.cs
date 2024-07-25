using Proto;
using Server.Net;

namespace Server.Logic.SysRoom
{
    public class RoomClass
    {
        public int roomId;
        public List<NetSession> listNetSessions = new List<NetSession>();
        public List<UserData> listUserDatas;
        public RoomClass(int roomId)
        {
            this.roomId = roomId;
            listUserDatas = new List<UserData>();
        }
        public void AddSession(NetSession netSession)
        {
            if (listNetSessions.Contains(netSession))
                return;
            listNetSessions.Add(netSession);
            listUserDatas.Add(new UserData { UserId = netSession.userId, Name = netSession.name });
        }
        public void RemoveSession(NetSession netSession)
        {
            if (!listNetSessions.Contains(netSession))
                return;
            listNetSessions.Remove(netSession);
            UserData userData = listUserDatas.Find((userData) => { return userData.UserId == netSession.userId; });
            if (userData == null)
                return;
            listUserDatas.Remove(userData);
        }

        public List<UserData> GetUsers(NetSession netSession)
        {
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

        public void BroadcastUserJoin(NetSession netSession)
        {
            ResponseOtherJoin response = new ResponseOtherJoin
            {
                UserData = new UserData { UserId = netSession.userId, Name = netSession.name }
            };
            byte[] data = NetSession.GetSendData(MsgType.EnResponseOtherJoin, response);
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
            byte[] data = NetSession.GetSendData(MsgType.EnResponseOtherLeave, response);
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
            byte[] data = NetSession.GetSendData(MsgType.EnResponseOtherSend, response);
            for (int i = 0, length = listNetSessions.Count; i < length; i++)
            {
                if (listNetSessions[i].userId == userId)
                    continue;
                listNetSessions[i].SendMessage(data);
            }
        }
    }
}
