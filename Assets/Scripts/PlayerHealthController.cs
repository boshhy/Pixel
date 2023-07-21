using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Used to keep track of player health
public class PlayerHealthController : MonoBehaviour
{
    // Used to reference this one and only instance 
    public static PlayerHealthController instance;

    // Used to keep track of curent health and max health for the player
    public int currentHealth, maxHealth;

    // Used to make player invincible for a certain length
    public float invincibleLength;
    private float invincibleCounter;

    // Reference the SpriteRenderer of the player
    private SpriteRenderer spriteRenderer;

    // Reference a kill effect
    public GameObject killEffect;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        // If player is Invincible
        if(invincibleCounter > 0)
        {
            // Adjust invincibiliy time
            invincibleCounter -= Time.deltaTime;

            // If we are no longer invincible then change players color back to normal
            if(invincibleCounter <= 0)
            {
                spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 1.0f);
            }
        }
    }

    // Used to deal damage to the player
    public void DealDamage()
    {
        // If player is not invincible then deal damage
        if(invincibleCounter <= 0)
        {
            // Play hurt SFX and take away health from player
            AudioManager.instance.PlaySFX(3);
            currentHealth--;

            // If player health is zero or less kill player
            if (currentHealth <= 0)
            {
                // Change to zero so UI can reference how many hearts to draw
                currentHealth = 0;
                
                // Instantiate a kill effect and respawn player
                Instantiate(killEffect, transform.position, transform.rotation);
                LevelManager.instance.RespawnPlayer();
            }
            // Else player need to get hurt
            else
            {
                // Make player invincible for some length of time
                invincibleCounter = invincibleLength;

                // Change opacity so player looks hurt
                spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 0.4f);

                // Knock back the player
                MovementController.instance.KnockBack();
            }
            
            // Update UI controller to display hearts
            UIController.instance.UpdateHealthDisplay();
        }
    }

    // Used to heal Player
    public void Heal()
    {
        // Add one to player health
        currentHealth++;

        // If player reached max health then current health should be max health
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    
        // Update UI controller to display hearts
        UIController.instance.UpdateHealthDisplay();
    }

    // Used to deal damage to player by boss's left side
    public void DealDamageLeft()
    {
        // knock the player back left if not invincible
        if(invincibleCounter <= 0)
        {
            // Play hurt SFX and take away health from player
            AudioManager.instance.PlaySFX(3);
            currentHealth--;

            // If player health is zero or less kill player
            if (currentHealth <= 0)
            {
                // Change to zero so UI can reference how many hearts to draw
                currentHealth = 0;
                
                // Instantiate a kill effect and respawn player
                Instantiate(killEffect, transform.position, transform.rotation);
                LevelManager.instance.RespawnPlayer();
            }
            else
            {
                // Make player invincible for some length of time
                invincibleCounter = invincibleLength;

                // Change opacity so player looks hurt
                spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 0.4f);
                
                // Knock back the player to the left from boss hit
                MovementController.instance.KnockBackLeft();
            }
            
            // Update UI controller to display hearts
            UIController.instance.UpdateHealthDisplay();
        }
    }
    
    // Used to deal damage to player by boss's right side
    public void DealDamageRight()
    {
        // knock the player back right if not invincible
        if(invincibleCounter <= 0)
        {
            // Play hurt SFX and take away health from player
            AudioManager.instance.PlaySFX(3);
            currentHealth--;

             // If player health is zero or less kill player
            if (currentHealth <= 0)
            {
                // Change to zero so UI can reference how many hearts to draw
                currentHealth = 0;
                
                // Instantiate a kill effect and respawn player
                Instantiate(killEffect, transform.position, transform.rotation);
                LevelManager.instance.RespawnPlayer();
            }
            else
            {
                // Make player invincible for some length of time
                invincibleCounter = invincibleLength;

                // Change opacity so player looks hurt
                spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 0.4f);

                // Knock back the player to the right from boss hit
                MovementController.instance.KnockBackRight();
            }
            
            // Update UI controller to display hearts
            UIController.instance.UpdateHealthDisplay();
        }
    }
}
