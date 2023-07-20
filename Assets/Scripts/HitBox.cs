using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Used as a hitbox on the bottom of players feet, for jumping on enemies
public class HitBox : MonoBehaviour
{
    // Used to instantiate a kill effect
    public GameObject killEffect;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Used to check collision between player hitbox and pig or rino
    void OnTriggerEnter2D(Collider2D other)
    {
        // If hitbox hits pig or rino and the player can double jump, destroy pig or rino
        if ((other.tag == "EnemyAngryPig" || other.tag == "EnemyAngryRino") && MovementController.instance.getCanDoubleJump())
        {

            // Destroy pig or rino and instantiate a kill effect
            Destroy(other.transform.parent.gameObject);
            Instantiate(killEffect, other.transform.position, other.transform.rotation);

            // Call 'killJump' which makes player bounce of enemies, and play jump SFX
            MovementController.instance.killJump();
            AudioManager.instance.PlaySFX(6);
        }
        // If player can not double jump
        else if ((other.tag == "EnemyAngryPig" || other.tag == "EnemyAngryRino") && MovementController.instance.getCanDoubleJump() == false)
        {
            // Destroy pig or rino and instantiate a kill effect
            Destroy(other.transform.parent.gameObject);
            Instantiate(killEffect, other.transform.position, other.transform.rotation);

            // Call 'resetDoubleJump' which resets the double jump so player can jump again
            // after bouncing off an enemy, and play jump SFX
            MovementController.instance.resetDoubleJump();
            AudioManager.instance.PlaySFX(6);
        }

        // If player hitbox touches a trampoline
        if (other.tag == "ItemTrampoline")
        {
            // Call 'trampolineJump' which makes player jump higher and reset dounle jump
            MovementController.instance.trampolineJump();
        }
    }
}
