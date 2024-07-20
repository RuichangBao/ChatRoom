using Google.Protobuf;
using Net;
using Proto;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Init : MonoBehaviour
{
    void Start()
    {
        NetClient.Instance.Listen(MsgType.EnResponseLinkSucces, ResponseLinkSucces.Parser, _ResponseLinkSucces);
        int port = 1994;
        string ip = "127.0.0.1";
        NetClient.Instance.StartClient(ip, port);
    }

    private void _ResponseLinkSucces(IMessage message)
    {
        ResponseLinkSucces response = message as ResponseLinkSucces;
        if (response == null)
        {
            Debug.LogError("消息转化错误");
            return;
        }
        Debug.LogError(message.ToString());
        SceneManager.LoadScene("Main");
    }
}