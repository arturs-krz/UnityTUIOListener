using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using UnityEngine;

// Open Sound Control for .NET
// Copyright (c) 2006, Yoshinori Kawasaki 
using OSC.NET;

public class InputListener : MonoBehaviour
{

    private Thread listenerThread;
    private UdpClient client;

    private IPEndPoint remoteEndpoint;

    public Dictionary<int, FingerInput> surfaceFingers = new Dictionary<int, FingerInput>();

    public Dictionary<int, ObjectInput> surfaceObjects = new Dictionary<int, ObjectInput>();

    // Start is called before the first frame update
    void Start()
    {
        // We will be listening to the Surface on localhost
        remoteEndpoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 3333);
        client = new UdpClient(remoteEndpoint);

        // Run the listener on a separate thread...
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
                
                if (receivedBytes.Length > 0) {
                    OSCBundle packet = (OSCBundle)OSCPacket.Unpack(receivedBytes);
                    
                    foreach (OSCMessage msg in packet.Values) {
                        if (msg.Address.Equals("/tuio/2Dobj")) {
                            ProcessObjectMessage(msg);
                        } else if (msg.Address.Equals("/tuio/2Dcur")) {
                            ProcessCursorMessage(msg);
                        }
                        // there's also /tuio/2Dblb
                        // but we don't really need it
                    } 
                }

            } catch (Exception error) {
                Debug.LogError(error.ToString());
            }
        }
    }

    private void ProcessCursorMessage(OSCMessage msg) {
        string msgType = msg.Values[0].ToString(); //   source / alive / set / fseq
        
        switch (msgType) {
            case "alive":
                Debug.Log("Alive");
                for (int i = 1; i < msg.Values.Count; i++) {
                    Debug.Log(msg.Values[i].ToString());
                }
                break;
            case "set":
                Debug.Log("Set");
                int id = (int)msg.Values[0];
                if (surfaceFingers.ContainsKey(id)) {

                } else {
                    
                }

                for (int i = 1; i < msg.Values.Count; i++) {
                    Debug.Log(msg.Values[i].ToString());
                }
                break;
        }
    } 

    private void ProcessObjectMessage(OSCMessage msg) {

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
