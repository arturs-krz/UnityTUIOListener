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

    // Start is called before the first frame update
    void Start()
    {
        client = new UdpClient("127.0.0.1", 3333);
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
                IPEndPoint remoteEndpoint = new IPEndPoint(IPAddress.Any, 3333);
                byte[] receivedBytes = client.Receive(ref remoteEndpoint);

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
