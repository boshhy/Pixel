using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Used to deal damage to player with rino object
public class DamagePlayerWithRino : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Used to see if player and rino have collided
    void OnTriggerEnter2D(Collider2D other)
    {   
        // If rino hits player change rino direction and deal damage to player
        if (other.tag == "Player")
        {
            GetComponent<RinoController>().movingLeft = !GetComponent<RinoController>().movingLeft;
            PlayerHealthController.instance.DealDamage();
        }
    }
}
