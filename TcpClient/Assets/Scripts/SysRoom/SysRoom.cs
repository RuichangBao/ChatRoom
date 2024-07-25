using Google.Protobuf;
using Net;
using Proto;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class SysRoom : Singleton<SysRoom>
{
    public int roomId;
    public List<UserData> listUserDatas;

    public override void Init()
    {
        base.Init();
        NetClient.Instance.Listen(MsgType.EnResponseSend, ResponseSend.Parser, _ResponseSend);
        NetClient.Instance.Listen(MsgType.EnResponseLeaveRoom, ResponseLeaveRoom.Parser, _ResponseLeaveRoom);
        NetClient.Instance.Listen(MsgType.EnResponseOtherSend, ResponseOtherSend.Parser, _ResponseOtherSend);
        NetClient.Instance.Listen(MsgType.EnResponseOtherJoin, ResponseOtherJoin.Parser, _ResponseOtherJoin);
        NetClient.Instance.Listen(MsgType.EnResponseOtherLeave, ResponseOtherLeave.Parser, _ResponseOtherLeave);
    }

    public void CreateRoomData(RoomData roomData)
    {
        roomId = roomData.RoomId;
        listUserDatas = new List<UserData>();
        for (int i = 0, length = roomData.UserData.Count; i < length; i++)
        {
            UserData userData = roomData.UserData[i];
            listUserDatas.Add(userData);
        }
    }

    public void AddUser(UserData userData)
    {
        listUserDatas.Add(userData);
    }
    public void RemoveUser(int userId)
    {
        UserData userData = listUserDatas.Find((userData) => { return userData.UserId == userId; });
        if (userData == null)
            return;
        listUserDatas.Remove(userData);
    }
    public void Clear()
    {
        roomId = 0;
        listUserDatas.Clear();
    }


    #region 协议
    public void RequestSend(string str)
    {
        RequestSend request = new RequestSend
        {
            Msg = str,
        };
        NetClient.Instance.SendMessage(MsgType.EnRequestSend, request);
    }
    private void _ResponseSend(IMessage message)
    {
        ResponseSend response = message as ResponseSend;
        if (response == null)
            return;
    }
    public void RequestLeaveRoom()
    {
        RequestLeaveRoom request = new RequestLeaveRoom
        {
            RoomId = SysRoom.Instance.roomId
        };
        NetClient.Instance.SendMessage(MsgType.EnRequestLeaveRoom, request);
    }
    private void _ResponseLeaveRoom(IMessage message)
    {
        ResponseLeaveRoom response = message as ResponseLeaveRoom;
        if (response == null)
            return;
        Debug.LogError("离开房间成功");
    }
    private void _ResponseOtherSend(IMessage message)
    {
        ResponseOtherSend response = message as ResponseOtherSend;
        if (response == null) return;
        Debug.LogError("其他玩家发送消息" + response.Msg);
    }
    private void _ResponseOtherJoin(IMessage message)
    {
        ResponseOtherJoin response = message as ResponseOtherJoin;
        if (response == null)
            return;
        this.AddUser(response.UserData);
    }
    private void _ResponseOtherLeave(IMessage message)
    {
        ResponseOtherLeave response = message as ResponseOtherLeave;
        if (response == null)
            return;
        this.RemoveUser(response.UserId);
    }
    #endregion
}