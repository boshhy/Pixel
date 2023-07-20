using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Used to kill the player if they fall out of world
public class KillPlayer : MonoBehaviour
{
    // Used to instantiate an exlopsion effect
    public GameObject explosionEffect;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Used to detect if player has entered kill plane
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            // Get the player position and adjust it 1.5 blocks higher
            Vector3 playerPosition = other.transform.position;
            playerPosition.y += 1.5f;

            // Instantiate an explosion effect to adjusted player position
            Instantiate(explosionEffect, playerPosition, other.transform.rotation);

            // Play explosion SFX and respawn player
            AudioManager.instance.PlaySFX(14);
            LevelManager.instance.RespawnPlayer();
        }
    }
}
