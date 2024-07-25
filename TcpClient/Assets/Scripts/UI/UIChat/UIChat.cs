

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UILogic
{
    public class UIChat : MonoBehaviour
    {
        public InputField inputField;
        public Button btnSend, btnClose;
        public Text labRoomId;
        public ChatClass chatClass;
        private List<ChatClass> listChat;

        void Start()
        {
            SysRoom.Instance.actionChat = Chat;
            btnSend.onClick.AddListener(BtnSend);
            btnClose.onClick.AddListener(BtnClose);
            labRoomId.text = SysRoom.Instance.roomId.ToString();
            chatClass.gameObject.SetActive(false);
            listChat = new List<ChatClass>();
        }

        private void BtnSend()
        {
            if (string.IsNullOrEmpty(inputField.text))
                return;
            SysRoom.Instance.RequestSend(inputField.text);
        }

        private void BtnClose()
        {
            SysRoom.Instance.RequestLeaveRoom();
        }
        private void Chat(string name, string msg, bool isSelf)
        {
            GameObject gameObject = Instantiate(this.chatClass.gameObject, this.chatClass.transform.parent);
            ChatClass chatClass = gameObject.GetComponent<ChatClass>();
            chatClass.Alignment(isSelf ? TextAnchor.UpperRight : TextAnchor.UpperLeft);
            chatClass.SetLab($"{name}:{msg}");
            chatClass.gameObject.SetActive(true);
            listChat.Add(chatClass);
        }
        private void OnDestroy()
        {

        }
    }
}