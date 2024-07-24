using Google.Protobuf;
using Proto;
using Server.Net;

namespace Server.Logic.SysRoom
{
    public class SysRoom : Singleton<SysRoom>
    {
        public int roomIndex = 0;
        private Dictionary<int, RoomData> roomDatas = new Dictionary<int, RoomData>();
        public override void Init()
        {
            base.Init();
            NetServer.Instance.Listen(MsgType.EnRequestCreateRoom, RequestCreateRoom.Parser, _RequestCreateRoom);
            NetServer.Instance.Listen(MsgType.EnRequestJoinRoom, RequestJoinRoom.Parser, JoinRoom);
        }

        private int CreateRoom(NetSession netSession)
        {
            roomIndex++;
            RoomData roomData = new RoomData(roomIndex);
            roomData.AddSession(netSession);
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
            int roomId = CreateRoom(netSession);

            ResponseCreateRoom response = new ResponseCreateRoom
            {
                RoomId = roomId,
            };
            netSession.SendMessage(MsgType.EnResponseCreateRoom, response);
        }
        public void JoinRoom(NetSession netSession, IMessage message)
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
            RoomData roomData = roomDatas[request.RoomId];
            roomData.AddSession(netSession);
            roomData.BroadcastUserJoin(netSession);

            ResponseJoinRoom response = new ResponseJoinRoom();
            netSession.SendMessage(MsgType.EnResponseJoinRoom, response);
        }

        #endregion
    }
}
