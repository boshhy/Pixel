using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealthController : MonoBehaviour
{
    public static BossHealthController instance;
    public GameObject LevelTransition;
    public int currentHealth, maxHealth;
    public float invincibleLength;
    public float invincibleCounter;

    public GameObject killEffect;

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
        if(invincibleCounter > 0)
        {
            invincibleCounter -= Time.deltaTime;
        }
    }

    public void DealDamage()
    {
        //Debug.Log("Deal damage to boss");
        if(invincibleCounter <= 0)
        {
            if (currentHealth >= 2)
            {
                AudioManager.instance.PlaySFX(4);
            }
            else 
            {
                AudioManager.instance.PlaySFX(16);
            }
            currentHealth--;

            if (currentHealth <= 0)
            {
                currentHealth = 0;
                Instantiate(killEffect, transform.position, transform.rotation);
                Destroy(gameObject);
                // END GAME
                LevelTransition.GetComponent<LevelLoader>().LoadNextLevel();
            }
            else
            {
                invincibleCounter = invincibleLength;
            }
        }
    }
}
