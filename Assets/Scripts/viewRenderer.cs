using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class viewRenderer : MonoBehaviour
{
    public int puckID;
    public MeshRenderer rend;
    public TestListener testListener;

    void Start()
    {
        
        rend = GetComponent<MeshRenderer>();
        rend.enabled = false;
    }

    // Toggle the Object's visibility each second.
    void Update()
    {
        //print(viewManager.puckList[puckID]);
        // Find out whether current second is odd or even
        //bool oddeven = Mathf.FloorToInt(Time.time) % 2 == 0;

        // Enable renderer accordingly
        rend.enabled = testListener.puckList[puckID];

        transform.position = new Vector3(testListener.puckXPos[puckID], testListener.puckYPos[puckID], 0);
        transform.rotation = Quaternion.Euler(0,0, testListener.puckSpin[puckID]);
    }
}
