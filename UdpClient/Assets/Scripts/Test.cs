using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class Test : MonoBehaviour
{
    Dictionary<IPEndPoint, int> dic = new Dictionary<IPEndPoint, int>();
    void Start()
    {
        IPEndPoint iPEndPoint1 = new IPEndPoint(0, 0);
        dic.Add(iPEndPoint1, 1);
        IPEndPoint iPEndPoint2 = new IPEndPoint(0, 0);
        Debug.LogError(dic.ContainsKey(iPEndPoint2));
    }

    // Update is called once per frame
    void Update()
    {

    }
}
