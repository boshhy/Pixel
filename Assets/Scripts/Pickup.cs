using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Used to pick up objects
public class Pickup : MonoBehaviour
{
    // Used to determine what the player is picking up
    public bool isStrawberry;
    public bool isHealth;

    // Checks to see if object was collected
    private bool isCollected;

    // Reference to a destroy effect
    public GameObject DestroyEffect;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Used to check if player is touching object
    private void OnTriggerEnter2D(Collider2D other)
    {
        // If player touches object and object is not collected
        // isCollected is used so we dont 'collect' objects twice
        if (other.tag == "Player" && !isCollected)
        {
            // If player is picking up a stawberry
            if (isStrawberry)
            {
                // Play collecting SFX and add one to 'stawberriesCollected'
                AudioManager.instance.PlaySFX(7);
                LevelManager.instance.strawberriesCollected++;

                // Change bool to reflect being collected
                isCollected = true;

                // Destroy the strawberry
                Destroy(gameObject);

                // Instantiate a Destroy effect in the location of the strawberry
                Instantiate(DestroyEffect, transform.position, transform.rotation);
            }

            // If player is picking up a heart
            if (isHealth)
            {
                // Only get collected if players current health is less than players max health
                if(PlayerHealthController.instance.currentHealth < PlayerHealthController.instance.maxHealth)
                {
                    // Play collecting SFX and heal the player
                    AudioManager.instance.PlaySFX(7);
                    PlayerHealthController.instance.Heal();

                    // Change bool to reflect being collected
                    isCollected = true;

                    // Destroy the heart
                    Destroy(gameObject);

                    // Instantiate a Destroy effect in the location of the heart
                    Instantiate(DestroyEffect, transform.position, transform.rotation);
                }
            }
        }
    }
}
