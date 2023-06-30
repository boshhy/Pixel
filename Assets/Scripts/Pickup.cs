using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public bool isStrawberry;
    public bool isHealth;

    private bool isCollected;

    public GameObject DestroyEffect;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && !isCollected)
        {
            if (isStrawberry)
            {
                AudioManager.instance.PlaySFX(7);
                LevelManager.instance.strawberriesCollected++;

                isCollected = true;
                Destroy(gameObject);

                Instantiate(DestroyEffect, transform.position, transform.rotation);
            }
        }

        if (isHealth)
        {
            if(PlayerHealthController.instance.currentHealth < PlayerHealthController.instance.maxHealth)
            {
                AudioManager.instance.PlaySFX(7);
                PlayerHealthController.instance.Heal();

                isCollected = true;
                Destroy(gameObject);

                Instantiate(DestroyEffect, transform.position, transform.rotation);
            }
        }
    }
}
