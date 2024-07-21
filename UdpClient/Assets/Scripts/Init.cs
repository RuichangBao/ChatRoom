using Google.Protobuf;
using Net;
using Proto;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Init : MonoBehaviour
{
    private void Awake()
    {
         NetClient.Instance.StartClient();
    }
    void Start()
    {
        RequestLink request =new RequestLink();
        NetClient.Instance.SendMessage(MsgType.EnRequestLink, request);
        SceneManager.LoadScene("Main");
    }
}