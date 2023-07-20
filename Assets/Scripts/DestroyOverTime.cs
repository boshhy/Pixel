using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Used to Destroy an instantiated effect
public class DestroyOverTime : MonoBehaviour
{
    // the lifespan of the object
    public float lifeSpan;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {   
        // Destroy the object after lifespan time
        Destroy(gameObject,lifeSpan);
    }
}
