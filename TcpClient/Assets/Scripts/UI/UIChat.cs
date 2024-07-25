using Net;
using Proto;

using UnityEngine;
using UnityEngine.UI;

namespace UILogin
{
    public class UIChat : MonoBehaviour
    {
        public InputField inputField;
        public Button btnSend, btnClose;


        void Start()
        {
            btnSend.onClick.AddListener(BtnSend);
            btnClose.onClick.AddListener(BtnClose);
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

    }
}