using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{
    public static PlayerHealthController instance;

    public int currentHealth, maxHealth;

    public float invincibleLength;
    private float invincibleCounter;
    private SpriteRenderer spriteRenderer;

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
        if(invincibleCounter > 0)
        {
            invincibleCounter -= Time.deltaTime;
            if(invincibleCounter <= 0)
            {
                spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 1.0f);
            }
        }
    }

    public void DealDamage()
    {
        if(invincibleCounter <= 0)
        {
            AudioManager.instance.PlaySFX(3);
            currentHealth--;

            if (currentHealth <= 0)
            {
                currentHealth = 0;
                //gameObject.SetActive(false);
                Instantiate(killEffect, transform.position, transform.rotation);
                LevelManager.instance.RespawnPlayer();
            }
            else
            {
                invincibleCounter = invincibleLength;
                spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 0.4f);

                MovementController.instance.KnockBack();
            }
            
            UIController.instance.UpdateHealthDisplay();
        }
    }

    public void Heal()
    {
        currentHealth++;

        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }

        UIController.instance.UpdateHealthDisplay();

    }
    public void DealDamageLeft()
    {
        // knock the player back left
        if(invincibleCounter <= 0)
        {
            AudioManager.instance.PlaySFX(3);
            currentHealth--;

            if (currentHealth <= 0)
            {
                currentHealth = 0;
                //gameObject.SetActive(false);
                Instantiate(killEffect, transform.position, transform.rotation);
                LevelManager.instance.RespawnPlayer();
            }
            else
            {
                invincibleCounter = invincibleLength;
                spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 0.4f);

                MovementController.instance.KnockBackLeft();
            }
            
            UIController.instance.UpdateHealthDisplay();
        }
    }
    
    public void DealDamageRight()
    {
        // knock the player back right
                if(invincibleCounter <= 0)
        {
            AudioManager.instance.PlaySFX(3);
            currentHealth--;

            if (currentHealth <= 0)
            {
                currentHealth = 0;
                //gameObject.SetActive(false);
                Instantiate(killEffect, transform.position, transform.rotation);
                LevelManager.instance.RespawnPlayer();
            }
            else
            {
                invincibleCounter = invincibleLength;
                spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 0.4f);

                MovementController.instance.KnockBackRight();
            }
            
            UIController.instance.UpdateHealthDisplay();
        }
    }
}
