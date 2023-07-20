using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Used to keep track of boss health
public class BossHealthController : MonoBehaviour
{
    // Used to reference this one and only instance
    public static BossHealthController instance;

    // Used to call level transition
    public GameObject LevelTransition;
    
    // Used to call a kill effect upon some object destruction
    public GameObject killEffect;
    
    // Used to keep track of boss health and invincibility 
    public int currentHealth, maxHealth;
    public float invincibleLength;
    public float invincibleCounter;

    // Create one instance of this game object
    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        // If player is invinvible deduct time
        if(invincibleCounter > 0)
        {
            invincibleCounter -= Time.deltaTime;
        }
    }

    public void DealDamage()
    {
        // Deal damage to boss only if not invincible
        if(invincibleCounter <= 0)
        {
            // Play SFX (either hurt, or defected sound effect)
            if (currentHealth >= 2)
            {
                AudioManager.instance.PlaySFX(4);
            }
            else 
            {
                AudioManager.instance.PlaySFX(16);
            }

            // Deduct one from current healt
            currentHealth--;

            // If boss has no more health, destroy it and load credits
            if (currentHealth <= 0)
            {
                Instantiate(killEffect, transform.position, transform.rotation);
                Destroy(gameObject);
                LevelTransition.GetComponent<LevelLoader>().LoadNextLevel();
            }
            else
            {
                // Make boss invincible for a bit so no duplicate hits occur
                invincibleCounter = invincibleLength;
            }
        }
    }
}
