using Google.Protobuf;
using Proto;
using Server.Net;
using System.Net;

namespace Server.Logic.SysRoom
{
    public class SysRoom : Singleton<SysRoom>
    {
        public int roomIndex = 0;
        public override void Init()
        {
            base.Init();
            NetServer.Instance.Listen(MsgType.EnRequestCreateRoom, RequestCreateRoom.Parser, _RequestCreateRoom);
            NetServer.Instance.Listen(MsgType.EnRequestJoinRoom, RequestJoinRoom.Parser, JoinRoom);
        }

        private int CreateRoom()
        {
            roomIndex++;
            return roomIndex;
        }

        #region 协议
        public void _RequestCreateRoom(NetSession netSession, IMessage message)
        {
            RequestCreateRoom request = message as RequestCreateRoom;
            if (request == null)
            {
                Console.WriteLine("创建房间错误：");
                return;
            }
            int roomId = CreateRoom();

            ResponseCreateRoom response = new ResponseCreateRoom
            {
                RoomId = roomId,
            };
            netSession.SendMessage(MsgType.EnResponseCreateRoom, response);
        }
        public void JoinRoom(NetSession netSession, IMessage message)
        {

        }
        #endregion
    }
}
