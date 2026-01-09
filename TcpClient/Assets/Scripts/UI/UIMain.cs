using Google.Protobuf;
using Net;
using Proto;
using System;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

namespace UILogic
{
    public class UIMain : MonoBehaviour
    {
        public Button btnCreateRoom;
        public Button btnJoinRoom;
        public InputField InputFieldRoomId;
        public Text labPing;

        private float lastHardTimer;

        private Stopwatch headStopwatch = new Stopwatch();



        void Start()
        {
            btnCreateRoom.onClick.AddListener(BtnCreateRoom);
            btnJoinRoom.onClick.AddListener(BtnJoinRoom);
            NetClient.Instance.Listen(MsgType.EnResponseHeard, ResponseHeard.Parser, _ResponseHeard);
        }
        //创建房间
        public void BtnCreateRoom()
        {
            //SysRoom.Instance.RequestCreateRoom();
        }


        //加入房间
        public void BtnJoinRoom()
        {
            //if (string.IsNullOrEmpty(InputFieldRoomId.text))
            //    return;
            //if (!int.TryParse(InputFieldRoomId.text, out int roomId))
            //{
            //    return;
            //}
            //SysRoom.Instance.RequestJoinRoom(roomId);
        }


        private void Update()
        {
            Heard();
        }
        private void Heard()
        {
            if (Time.time - lastHardTimer > 10)
            {
                lastHardTimer = Time.time;
                headStopwatch.Reset();
                SysRoom.Instance.RequestHeat();
                UnityEngine.Debug.LogError(lastHardTimer);
            }
        }
        private void _ResponseHeard(IMessage message)
        {
            headStopwatch.Stop();
            long ping = headStopwatch.ElapsedMilliseconds;
            labPing.text = ping.ToString();
        }
    }
}