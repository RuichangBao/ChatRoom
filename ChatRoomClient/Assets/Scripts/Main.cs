

using Google.Protobuf;
using Net;
using Proto;
using UnityEngine;

public class Main : MonoBehaviour
{
    void Start()
    {
        NetClient.Instance.StartClient();
        //TestRequest request = new TestRequest
        //{
        //    Num1 = 1,
        //    Num2 = 2,
        //    Str = "132312"
        //};
        //Debug.LogError(request.ToString());
        //byte[] data = request.ToByteArray();
        //Debug.LogError(data.Length);

        //TestRequest test1Request = new TestRequest();
        //MessageExtensions.MergeFrom(test1Request, data);
        //Debug.LogError(test1Request.ToString());
        //MessageParser messageParser = TestRequest.Parser;
        //IMessage message = messageParser.ParseFrom(data);
        //Debug.LogError("AAA" + message.ToString());
        //MessageType messageType
        //MsgType.Test1
    }
}