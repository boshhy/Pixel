using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyController : MonoBehaviour
{
    public GameObject theClosedDoor;
    public GameObject collectEffect;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            theClosedDoor.GetComponent<DoorController>().PlayerHasKey = true;
            Destroy(gameObject);
            Instantiate(collectEffect, gameObject.transform.position, gameObject.transform.rotation);
        }
    }
}
