using Net;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ����Э���MonoBehaviour
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
