
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestListener : MonoBehaviour
{
    // Array to set the bool for pucks detected
    public bool[] puckList = { false, false, false, false, false, false, };

    public float[] puckSpin = { 0, 0, 0, 0, 0, 0, };
    public float[] puckXPos = { 0, 0, 0, 0, 0, 0, };
    public float[] puckYPos = { 0, 0, 0, 0, 0, 0, };

    // Set the constraints based on the unity editor transform values
    public int fromRx = -9;
    public int fromRy = -4;
    public int toRx = 9;
    public int toRy = 6;
    public int angleOutputMin = 0;
    public float angleOutputMax = (float)6.3;
    int objectID;

    float xPos, yPos, oSpin = 0;


    int resetCounter;

    public GameObject gamecube;

    // Start is called before the first frame update
    void Start()
    {
        SurfaceInputs.Instance.OnTouch += OnTouchReceive;
    }

    void OnTouchReceive(Dictionary<int, FingerInput> surfaceFingers, Dictionary<int, ObjectInput> surfaceObjects)
    {
        Debug.ClearDeveloperConsole();
        if (surfaceFingers.Count > 0)
        {
            //Debug.Log(surfaceFingers.Count + " fingers:");
            foreach (KeyValuePair<int, FingerInput> entry in surfaceFingers)
            {
                //Debug.Log(entry.Key + " @ " + entry.Value.position.x + ";" + entry.Value.position.y);
            }
        }

        if (surfaceObjects.Count > 0)
        {
            //Debug.Log(surfaceObjects.Count + " objects:");
            foreach (KeyValuePair<int, ObjectInput> entry in surfaceObjects)
            {
                //Debug.Log(entry.Key + ", tag: " + entry.Value.tagValue + " @ " + entry.Value.position.x + ";" + entry.Value.position.y);

                // Mapping the rotation and position into readable formats for transform manipulations
                oSpin = -Map(0, 360, angleOutputMin, angleOutputMax, entry.Value.orientation);
                xPos = Map(fromRx, toRx, 0, 1, entry.Value.position.x);
                yPos = Map(toRy, fromRy, 0, 1, entry.Value.position.y);

                // Object ID starts from 1 but array values start from 0
                objectID = entry.Value.tagValue - 1;

                // Setting values into respective arrays
                puckList[objectID] = true;
                puckSpin[objectID] = oSpin;
                puckXPos[objectID] = xPos;
                puckYPos[objectID] = yPos;

            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        // TESTS
        if (Input.GetKeyDown(KeyCode.A))
        {
            puckList[0] = true;
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            puckList[1] = true;
        }
    }

    // Map fucntion for normalising the data from the tuio interface
    public float Map(float from, float to, float from2, float to2, float value)
    {
        if (value <= from2)
        {
            return from;
        }
        else if (value >= to2)
        {
            return to;
        }
        else
        {
            return (to - from) * ((value - from2) / (to2 - from2)) + from;
        }
    }
}
