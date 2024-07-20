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
        NetClient.Instance.StartClient();
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