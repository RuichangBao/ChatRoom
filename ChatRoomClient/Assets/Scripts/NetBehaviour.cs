using Net;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 处理协议的MonoBehaviour
/// </summary>
public class NetBehaviour : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    void Update()
    {
        NetClient.Instance.Update();
    }
}
