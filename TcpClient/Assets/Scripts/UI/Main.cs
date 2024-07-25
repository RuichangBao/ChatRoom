using Proto;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace UILogic
{
    public class Main : MonoBehaviour
    {
        public UIMain uiMain;
        public UIChat uiChat;
        void Start()
        {
            SysRoom.Instance.actionOpenRoom = OpenRoom;
            uiMain.gameObject.SetActive(true);
            uiChat.gameObject.SetActive(false);
        }
        private void OpenRoom()
        {
            uiMain.gameObject.SetActive(false);
            uiChat.gameObject.SetActive(true);
        }

    }
}