using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Used as camera view to follow player
public class CameraController : MonoBehaviour
{
    // used to target player
    public Transform target;

    // Used to clamp height and horizontal view
    public float minHeight, maxHeight;
    public float minHorizontal, maxHorizontal;

    // Used to offset camera from player
    private int offset = 1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Center the camera around the player, restricted to min and max, heights and positions
        transform.position = new Vector3(Mathf.Clamp(target.position.x, minHorizontal, maxHorizontal),
                                         Mathf.Clamp(target.position.y, minHeight, maxHeight) + offset, 
                                         transform.position.z);
    }

    // Used to fix glitch after player dies (updates the location to where player spawned)
    public void FixGlitch(float incomingX, float incomingY, float incomingZ)
    {
        transform.position = new Vector3(Mathf.Clamp(incomingX, minHorizontal, maxHorizontal),
                                         incomingY, 
                                         incomingZ);
    }
}
