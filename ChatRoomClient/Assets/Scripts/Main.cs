

using Google.Protobuf;
using UnityEngine;

public class Main : MonoBehaviour
{
    void Start()
    {
        //ClientNet.Instance.StartClient();
        Test1Request request = new Test1Request
        {
            Num1 = 1,
            Num2 = 2,
            Str = "132312"
        };
        Debug.LogError(request.ToString());
        byte[] data = request.ToByteArray();
        Debug.LogError(data.Length);

        Test1Request test1Request = new Test1Request();
        MessageExtensions.MergeFrom(test1Request, data);
        Debug.LogError(test1Request.ToString());
        MessageParser messageParser = Test1Request.Parser;
        IMessage message = messageParser.ParseFrom(data);
        Debug.LogError("AAA" + message.ToString());
        MessageType messageType
        //MsgType.Test1
    }

    void Update()
    {

    }
}
