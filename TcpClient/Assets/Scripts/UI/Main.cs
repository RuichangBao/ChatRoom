using Proto;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace UILogin
{
    public class Main : MonoBehaviour
    {
        public UIMain uiMain;
        public UIChat uiChat;
        void Start()
        {
            uiMain.actionChat = JoinRoom;
        } 
        private void JoinRoom()
        {
            uiMain.gameObject.SetActive(false);
            uiChat.gameObject.SetActive(true);
        }
    }
}