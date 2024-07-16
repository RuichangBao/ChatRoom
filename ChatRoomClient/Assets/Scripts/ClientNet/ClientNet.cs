using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using UnityEngine;

public class ClientNet : Singleton<ClientNet>
{
    private Socket socket;
    private int port = 1994;
    private string ip = "127.0.0.1";
    public void StartClient()
    {
        socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        IPAddress iPAddress = IPAddress.Parse(ip);
        IPEndPoint iPEndPoint = new IPEndPoint(iPAddress, port);
        socket.Connect(iPEndPoint);
        UnityEngine.Debug.LogError("Á´½Ó³É¹¦");
    }
}