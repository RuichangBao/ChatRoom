using Google.Protobuf;
using Proto;
using Server.Net;

namespace Server.Logic.SysRoom
{
    public class SysRoom : Singleton<SysRoom>
    {
        public int roomIndex = 0;
        private Dictionary<int, RoomClass> roomClasses = new Dictionary<int, RoomClass>();
        public override void Init()
        {
            base.Init();
            NetServer.Instance.Listen(MsgType.EnRequestCreateRoom, RequestCreateRoom.Parser, _RequestCreateRoom);
            NetServer.Instance.Listen(MsgType.EnRequestJoinRoom, RequestJoinRoom.Parser, _RequestJoinRoom);
            NetServer.Instance.Listen(MsgType.EnRequestLeaveRoom, RequestLeaveRoom.Parser, _RequestLeaveRoom);
            NetServer.Instance.Listen(MsgType.EnRequestSend, RequestSend.Parser, _RequestSend);
            NetServer.Instance.Listen(MsgType.EnRequestTest, RequestTest.Parser, _RequestTest);
        }

        private RoomClass CreateRoom(NetSession netSession)
        {
            roomIndex++;
            RoomClass roomClass = new RoomClass(roomIndex);
            roomClasses.Add(roomIndex, roomClass);
            roomClass.AddSession(netSession);
            return roomClass;
        }

        private RoomClass GetRoomClassBuySession(NetSession netSession) 
        {
            foreach (RoomClass roomClass in roomClasses.Values)
            {
                if (roomClass.SessionInRoom(netSession))
                    return roomClass;
            }
            return null;
        }

        #region 协议
        private void _RequestCreateRoom(NetSession netSession, IMessage message)
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
        private void _RequestJoinRoom(NetSession netSession, IMessage message)
        {
            RequestJoinRoom request = message as RequestJoinRoom;
            if (request == null)
            {
                Console.WriteLine("加入房间错误：");
                return;
            }
            if (!roomClasses.ContainsKey(request.RoomId))
            {
                ResponseError responseError = new ResponseError
                {
                    ErrorCode = ErrorCode.ErrRoomNone
                };
                netSession.SendMessage(MsgType.EnResponseError, responseError);
                return;
            }
            RoomClass roomClass = roomClasses[request.RoomId];
            roomClass.AddSession(netSession);
            roomClass.BroadcastUserJoin(netSession);

            ResponseJoinRoom response = new ResponseJoinRoom();
            RoomData roomData = new RoomData();
            roomData.RoomId = roomClass.roomId;
            roomData.UserData.Add(roomClass.GetUsers());
            response.RoomData = roomData;
            netSession.SendMessage(MsgType.EnResponseJoinRoom, response);
        }

        private void _RequestLeaveRoom(NetSession netSession, IMessage message)
        {
            RequestLeaveRoom request = message as RequestLeaveRoom;
            if (request == null)
            {
                Console.WriteLine("离开房间错误：");
                return;
            }
            RoomClass roomClass = roomClasses[request.RoomId];
            roomClass.RemoveSession(netSession);
            if (roomClass.GetUserNum() <= 0)//房间内没人了
            {
                roomClasses.Remove(roomClass.roomId);//关闭房间
            }
            ResponseLeaveRoom response = new ResponseLeaveRoom();
            netSession.SendMessage(MsgType.EnResponseLeaveRoom, response);
            roomClass.BroadcastUserLeave(netSession.userId);
        }
        private void _RequestTest(NetSession netSession, IMessage message)
        {
            
        }
        private void _RequestSend(NetSession netSession, IMessage message)
        {
            RequestSend request = message as RequestSend;
            if (request == null)
                return;

            RoomClass roomClass = GetRoomClassBuySession(netSession);
            if (roomClass == null)
            {
                ResponseError(netSession, ErrorCode.ErrNoInRoom);
                return;
            }

            ResponseSend response = new ResponseSend();
            response.Msg = request.Msg;
            netSession.SendMessage(MsgType.EnResponseSend,response);


            roomClass.BroadcastUserChat(netSession.userId, request.Msg);
        }

        private void ResponseError(NetSession netSession, ErrorCode errorCode)
        {
            ResponseError response = new ResponseError();
            response.ErrorCode = errorCode;
            netSession.SendMessage(MsgType.EnResponseError, response);
        }

        #endregion
    }
}
