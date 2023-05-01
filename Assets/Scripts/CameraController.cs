using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    public float minHeight, maxHeight;
    public float minHorizontal, maxHorizontal;
    private int offset = 1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // transform.position = new Vector3(target.position.x, target.position.y, transform.position.z);

        // float clampedY = Mathf.Clamp(transform.position.y, minHeight, maxHeight);

        // transform.position = new Vector3(transform.position.x, clampedY, transform.position.z);

        transform.position = new Vector3(Mathf.Clamp(target.position.x, minHorizontal, maxHorizontal),
                                         Mathf.Clamp(target.position.y, minHeight, maxHeight) + offset, 
                                         transform.position.z);
    }
}
