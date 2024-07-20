using Google.Protobuf;
using Net;
using Proto;
using System;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class Main : MonoBehaviour
{
    public Button btn;
    void Start()
    {
        btn.onClick.AddListener(BtnCreateRoom);
        NetClient.Instance.Listen(MsgType.EnResponseCreateRoom, ResponseCreateRoom.Parser, _ResponseCreateRoom);
    }

    private void _ResponseCreateRoom(IMessage message)
    {
        ResponseCreateRoom response = message as ResponseCreateRoom;
        if (response == null)
        {
            return;
        }
        Debug.LogError($"创建房间，{response}");
    }

    public void BtnCreateRoom()
    {
        RequestCreateRoom request = new RequestCreateRoom();
        NetClient.Instance.SendMessage(MsgType.EnRequestCreateRoom, request);
    }
}