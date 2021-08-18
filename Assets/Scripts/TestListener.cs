
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestListener : MonoBehaviour
{
    public int fromRx = -9;
    public int fromRy = -4;
    public int toRx = 9;
    public int toRy = 6;

    float xPos, yPos = 0;

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
            Debug.Log(surfaceFingers.Count + " fingers:");
            foreach (KeyValuePair<int, FingerInput> entry in surfaceFingers)
            {
                Debug.Log(entry.Key + " @ " + entry.Value.position.x + ";" + entry.Value.position.y);
            }
        }

        if (surfaceObjects.Count > 0)
        {
            Debug.Log(surfaceObjects.Count + " objects:");
            foreach (KeyValuePair<int, ObjectInput> entry in surfaceObjects)
            {
                Debug.Log(entry.Key + ", tag: " + entry.Value.tagValue + " @ " + entry.Value.position.x + ";" + entry.Value.position.y);

                xPos = Map(-9, 9, 0, 1, entry.Value.position.x);
                yPos = Map(6, -4, 0, 1, entry.Value.position.y);

                print("X " + xPos);
                print("Y " + yPos);

                if (entry.Value.tagValue == 2)
                {
                    transform.position = new Vector3(xPos, yPos, 0);
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        //print("Y " + Map(0, 10, 0, 1, 1));
    }

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
