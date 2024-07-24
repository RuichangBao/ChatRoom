using Net;
using Proto;

using UnityEngine;
using UnityEngine.UI;

namespace UILogin
{
    public class UIChat : MonoBehaviour
    {
        public InputField inputField;
        public Button btnSend;

        void Start()
        {
            btnSend.onClick.AddListener(BtnSend);
        }

        private void BtnSend()
        {
            if (string.IsNullOrEmpty(inputField.text))
                return;
            RequestSend request = new RequestSend
            {
                Msg = inputField.text,
            };
            NetClient.Instance.SendMessage(MsgType.EnRequestSend, request);
        }
    }
}