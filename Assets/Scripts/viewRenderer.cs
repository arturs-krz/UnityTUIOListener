using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class viewRenderer : MonoBehaviour
{
    public int puckID;
    public MeshRenderer rend;
    public TestListener testListener;

    public Component[] childRenderer;

    void Start()
    {
        // Getting the meshrenderer of all objects and setting them to false
        childRenderer = gameObject.GetComponentsInChildren<MeshRenderer>();
        foreach (MeshRenderer r in childRenderer)
            r.enabled = false;

        rend = GetComponent<MeshRenderer>();
        rend.enabled = false;
    }

    // Toggle the Object's visibility each second.
    void Update()
    {
        //print(viewManager.puckList[puckID]);
        // Find out whether current second is odd or even
        //bool oddeven = Mathf.FloorToInt(Time.time) % 2 == 0;

        // Enable renderer accordingly for parent and children
        rend.enabled = testListener.puckList[puckID];

        foreach (Renderer r in childRenderer)
            r.enabled = testListener.puckList[puckID];

        // Position and Rotation mimicing the puck
        //transform.position = new Vector3(testListener.puckXPos[puckID], testListener.puckYPos[puckID], 0);
        //transform.rotation = Quaternion.Euler(0,0, testListener.puckSpin[puckID]);
    }
}
