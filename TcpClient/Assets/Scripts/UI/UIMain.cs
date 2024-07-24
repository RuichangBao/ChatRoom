using Google.Protobuf;
using Net;
using Proto;
using System;
using System.Threading;
using UnityEditor.PackageManager.Requests;
using UnityEngine;
using UnityEngine.UI;

namespace UILogin
{
    public class UIMain : MonoBehaviour
    {
        public Action actionChat;
        public Button btnCreateRoom;
        public Button btnJoinRoom;
        public InputField InputFieldRoomId;
        void Start()
        {
            NetClient.Instance.Listen(MsgType.EnResponseCreateRoom, ResponseCreateRoom.Parser, _ResponseCreateRoom);
            NetClient.Instance.Listen(MsgType.EnResponseJoinRoom, ResponseJoinRoom.Parser, _ResponseJoinRoom);
            btnCreateRoom.onClick.AddListener(BtnCreateRoom);
            btnJoinRoom.onClick.AddListener(BtnJoinRoom);
        }
        //创建房间
        public void BtnCreateRoom()
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
            actionChat?.Invoke();
        }

        //加入房间
        public void BtnJoinRoom()
        {
            if (string.IsNullOrEmpty(InputFieldRoomId.text))
                return;
            if (!int.TryParse(InputFieldRoomId.text, out int roomId))
            {
                return;
            }
            RequestJoinRoom request = new RequestJoinRoom
            {
                RoomId = roomId
            };
            NetClient.Instance.SendMessage(MsgType.EnRequestCreateRoom, request);
        }
        public void _ResponseJoinRoom(IMessage message)
        {
            ResponseJoinRoom response = message as ResponseJoinRoom; 
            if (response == null)
            {
                return;
            }
            actionChat?.Invoke();
        }
    }
}