using Google.Protobuf;
using Net;
using Proto;
using UnityEngine;
using UnityEngine.UI;

public class Main : MonoBehaviour
{
    public Button btn;
    void Start()
    {
        btn.onClick.AddListener(BtnCreateRoom);
        NetClient.Instance.Listen(MsgType.EnResponseCreateRoom, ResponseCreateRoom.Parser, _ResponseCreateRoom);
    }

    private void _ResponseCreateRoom(IMessage message)
    {
        ResponseCreateRoom response = message as ResponseCreateRoom;
        if (response == null)
        {
            return;
        }
        Debug.LogError($"创建房间，{response}");
    }

    public void BtnCreateRoom()
    {
        RequestTest request = new RequestTest();
        for (int i = 0; i < 100; i++)
        {
            request.Num1 = request.Num2 = i;
            request.Str = $"{i}{i}{i}";
            byte[] data = request.ToByteArray();
            Debug.LogError(i + ":" + data.Length);
            NetClient.Instance.SendMessage(data);
        }

    }
}