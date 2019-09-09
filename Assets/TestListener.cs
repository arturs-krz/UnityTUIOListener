using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestListener : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        SurfaceInputs.Instance.OnTouch += OnTouchReceive;
    }

    void OnTouchReceive(Dictionary<int, FingerInput> surfaceFingers, Dictionary<int, ObjectInput> surfaceObjects) {
        Debug.ClearDeveloperConsole();
        if (surfaceFingers.Count > 0) { 
            Debug.Log(surfaceFingers.Count + " fingers:");
            foreach (KeyValuePair<int, FingerInput> entry in surfaceFingers) {
                Debug.Log(entry.Key + " @ " + entry.Value.position.x + ";" + entry.Value.position.y);
            }
        }

        if (surfaceObjects.Count > 0) {
            Debug.Log(surfaceObjects.Count + " objects:");
            foreach (KeyValuePair<int, ObjectInput> entry in surfaceObjects) {
                Debug.Log(entry.Key + ", tag: " + entry.Value.tagValue + " @ " + entry.Value.position.x + ";" + entry.Value.position.y);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
