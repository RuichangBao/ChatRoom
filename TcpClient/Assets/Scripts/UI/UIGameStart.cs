using Google.Protobuf;
using Net;
using Proto;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
namespace UILogic
{
    public class UIGameStart : MonoBehaviour
    {
        //�������������ļ�
        private int port = 1994;
        private string ip = "127.0.0.1";
        public Button btnGameStart;

        void Start()
        {
            NetClient.Instance.Listen(MsgType.EnResponseLink, ResponseLink.Parser, _ResponseLinkSucces);
            btnGameStart.onClick.AddListener(BtnGameStart);
        }

        public void BtnGameStart()
        {
            NetClient.Instance.StartClient(ip, port);
        }

        private void _ResponseLinkSucces(IMessage message)
        {
            ResponseLink response = message as ResponseLink;
            if (response == null)
            {
                Debug.LogError("��Ϣת������");
                return;
            }
            Debug.LogError(message.ToString());
            SceneManager.LoadScene("Main");
        }
    }
}