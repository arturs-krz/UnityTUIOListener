using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Text;
using UnityEngine;

public class InputListener : MonoBehaviour
{

    private Thread listenerThread;
    private UdpClient client;

    private IPEndPoint remoteEndpoint;

    // Start is called before the first frame update
    void Start()
    {
        remoteEndpoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 3333);
        client = new UdpClient(remoteEndpoint);
        //client.Client.ReceiveTimeout = 100;

        ThreadStart threadStarter = new ThreadStart(Listen);
        listenerThread = new Thread(threadStarter);
        listenerThread.IsBackground = true;
        listenerThread.Start();
    }

    private void Listen() {
        Debug.Log("UDP Listener started...");
        while (true) {
            try {
                byte[] receivedBytes = client.Receive(ref remoteEndpoint);
                Debug.Log(receivedBytes);

                string decoded = Encoding.UTF8.GetString(receivedBytes);
                Debug.Log(decoded);

            } catch (Exception error) {
                Debug.LogError(error.ToString());
            }
        }
    }

    void OnApplicationQuit() {
        listenerThread.Abort();
        client.Close();
    }

    // Update is called once per frame
    void Update()
    {
    }
}
