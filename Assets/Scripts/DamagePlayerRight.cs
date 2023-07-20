using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Used to deal player damage by a boss object from right side
public class DamagePlayerRight : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    // When player enters objects left side deal damage and knockback player to the right
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            PlayerHealthController.instance.DealDamageRight();
        }
    }

    // If player still on left side of object keep doing damage and knockback to the right
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            PlayerHealthController.instance.DealDamageRight();
        }
    }
}
