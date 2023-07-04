using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBox : MonoBehaviour
{
    public GameObject killEffect;
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
        if ((other.tag == "EnemyAngryPig" || other.tag == "EnemyAngryRino") && MovementController.instance.getCanDoubleJump())
        {
            Destroy(other.transform.parent.gameObject);
            Instantiate(killEffect, other.transform.position, other.transform.rotation);
            MovementController.instance.killJump();
            AudioManager.instance.PlaySFX(6);
        }
        else if ((other.tag == "EnemyAngryPig" || other.tag == "EnemyAngryRino") && MovementController.instance.getCanDoubleJump() == false)
        {
            Destroy(other.transform.parent.gameObject);
            Instantiate(killEffect, other.transform.position, other.transform.rotation);
            MovementController.instance.resetDoubleJump();
            AudioManager.instance.PlaySFX(6);
        }

        if (other.tag == "ItemTrampoline")
        {
            
            MovementController.instance.trampolineJump();
        }
    }
}
