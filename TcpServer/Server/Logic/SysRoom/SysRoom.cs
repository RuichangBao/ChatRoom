using Google.Protobuf;
using Proto;
using Server.Net;

namespace Server.Logic.SysRoom
{
    public class SysRoom : Singleton<SysRoom>
    {
        public int roomIndex = 0;
        private Dictionary<int, RoomClass> roomDatas = new Dictionary<int, RoomClass>();
        public override void Init()
        {
            base.Init();
            NetServer.Instance.Listen(MsgType.EnRequestCreateRoom, RequestCreateRoom.Parser, _RequestCreateRoom);
            NetServer.Instance.Listen(MsgType.EnRequestJoinRoom, RequestJoinRoom.Parser, _RequestJoinRoom);
            NetServer.Instance.Listen(MsgType.EnRequestLeaveRoom, RequestLeaveRoom.Parser, _RequestLeaveRoom);
        }

        private RoomClass CreateRoom(NetSession netSession)
        {
            roomIndex++;
            RoomClass roomData = new RoomClass(roomIndex);
            roomData.AddSession(netSession);
            return roomData;
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
            RoomClass roomClass = CreateRoom(netSession);
            RoomData roomData = new RoomData();
            roomData.RoomId = roomClass.roomId;
            roomData.UserData.Add(roomClass.GetUsers());
            ResponseCreateRoom response = new ResponseCreateRoom();
            response.RoomData = roomData;
            netSession.SendMessage(MsgType.EnResponseCreateRoom, response);
        }
        public void _RequestJoinRoom(NetSession netSession, IMessage message)
        {
            RequestJoinRoom request = message as RequestJoinRoom;
            if (request == null)
            {
                Console.WriteLine("加入房间错误：");
                return;
            }
            if (!roomDatas.ContainsKey(request.RoomId))
            {
                ResponseError responseError = new ResponseError
                {
                    ErrorCode = ErrorCode.ErrRoomNone
                };
                netSession.SendMessage(MsgType.EnResponseError, responseError);
                return;
            }
            RoomClass roomData = roomDatas[request.RoomId];
            roomData.AddSession(netSession);
            roomData.BroadcastUserJoin(netSession);

            ResponseJoinRoom response = new ResponseJoinRoom();
            netSession.SendMessage(MsgType.EnResponseJoinRoom, response);
        }

        public void _RequestLeaveRoom(NetSession netSession, IMessage message)
        {
            RequestLeaveRoom request = message as RequestLeaveRoom; 
            if (request == null)
            {
                Console.WriteLine("离开房间错误：");
                return;
            }
            RoomClass roomData = roomDatas[request.RoomId];
            roomData.RemoveSession(netSession);
            roomData.BroadcastUserLeave(netSession);
        }

        #endregion
    }
}
