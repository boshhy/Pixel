using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Used to collect the key
public class KeyController : MonoBehaviour
{
    // Used to reference closed door
    public GameObject theClosedDoor;

    // Used to create a pick up effect when collected
    public GameObject collectEffect;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Used to see if plaayer has collided with the key
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            // Play pick up SFX and change closed bool 'playerHaskey' bool to true;
            AudioManager.instance.PlaySFX(10);
            theClosedDoor.GetComponent<DoorController>().PlayerHasKey = true;

            // Destroy the key object and intantiate a pick up effect
            Destroy(gameObject);
            Instantiate(collectEffect, gameObject.transform.position, gameObject.transform.rotation);
        }
    }
}
