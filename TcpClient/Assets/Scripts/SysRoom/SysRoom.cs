using Google.Protobuf;
using Net;
using Proto;
using System;
using System.Collections.Generic;
using UnityEditor.PackageManager.Requests;
using UnityEngine;


public class SysRoom : Singleton<SysRoom>
{
    public int roomId;
    public List<UserData> listUserDatas;

    public Action actionOpenRoom;//临时用时间处理，后期可以接入广播系统
    public Action<string, string, bool> actionChat;

    public override void Init()
    {
        base.Init();

        NetClient.Instance.Listen(MsgType.EnResponseCreateRoom, ResponseCreateRoom.Parser, _ResponseCreateRoom);
        NetClient.Instance.Listen(MsgType.EnResponseJoinRoom, ResponseJoinRoom.Parser, _ResponseJoinRoom);
        NetClient.Instance.Listen(MsgType.EnResponseLeaveRoom, ResponseLeaveRoom.Parser, _ResponseLeaveRoom);
        NetClient.Instance.Listen(MsgType.EnResponseSend, ResponseSend.Parser, _ResponseSend);
        NetClient.Instance.Listen(MsgType.EnResponseOtherSend, ResponseOtherSend.Parser, _ResponseOtherSend);
        NetClient.Instance.Listen(MsgType.EnResponseOtherJoin, ResponseOtherJoin.Parser, _ResponseOtherJoin);
        NetClient.Instance.Listen(MsgType.EnResponseOtherLeave, ResponseOtherLeave.Parser, _ResponseOtherLeave);
        NetClient.Instance.Listen(MsgType.EnResponseError, ResponseError.Parser, _ResponseError);

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
    private string GetNameById(int userId)
    {
        UserData userData = listUserDatas.Find((userData) => { return userData.UserId == userId; });
        if (userData == null)
        {
            Debug.LogError($"查找名字错误userId:{userId}");
            return "";
        }
        return userData.Name;
    }
    public void Clear()
    {
        roomId = 0;
        listUserDatas.Clear();
    }


    #region 协议
    //创建房间
    public void RequestCreateRoom()
    {
        RequestCreateRoom request = new RequestCreateRoom();
        NetClient.Instance.SendMessage(MsgType.EnRequestCreateRoom, request);
    }
    private void _ResponseCreateRoom(IMessage message)
    {
        ResponseCreateRoom response = message as ResponseCreateRoom;
        if (response == null)
        {
            return;
        }
        SysRoom.Instance.CreateRoomData(response.RoomData);
        actionOpenRoom?.Invoke();
    }
    //加入房间
    public void RequestJoinRoom(int roomId)
    {
        RequestJoinRoom request = new RequestJoinRoom
        {
            RoomId = roomId
        };
        NetClient.Instance.SendMessage(MsgType.EnRequestJoinRoom, request);
    }
    private void _ResponseJoinRoom(IMessage message)
    {
        ResponseJoinRoom response = message as ResponseJoinRoom;
        if (response == null)
            return;
        SysRoom.Instance.CreateRoomData(response.RoomData);
        actionOpenRoom?.Invoke();
    }
    public void RequestSend(string str)
    {
        RequestSend request = new RequestSend
        {
            Msg = str,
        };
        NetClient.Instance.SendMessage(MsgType.EnRequestSend, request);
    }

    public void RequestHeat()
    {
        RequestHeard request = new RequestHeard();
        NetClient.Instance.SendMessage(MsgType.EnRequestHeard, request);
    }

    private void _ResponseSend(IMessage message)
    {
        ResponseSend response = message as ResponseSend;
        if (response == null)
            return;
        //name 可以自己获取
        actionChat?.Invoke("我自己", response.Msg, true);
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
        string name = this.GetNameById(response.UserId);
        actionChat?.Invoke(name, response.Msg, false);
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
    private void _ResponseError(IMessage message)
    {
        ResponseError response = message as ResponseError;
        if (response == null) return;
        Debug.LogError($"服务器返回错误协议，错误码{response.ErrorCode}:{(int)response.ErrorCode}");
    }

    private void _ResponseHeat(IMessage message)
    {

    }
    #endregion


    public void RequestTest(int index)
    {
        for (int i = 0; i < 10; i++)
        {
            RequestTest request = new RequestTest();
            request.Num1 = index;
            request.Num2 = i;
            request.Str = i.ToString();
            NetClient.Instance.SendMessage(MsgType.EnRequestTest, request);
        }

    }
}