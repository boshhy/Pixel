using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillPlayer : MonoBehaviour
{
    public GameObject explosionEffect;
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
        if (other.tag == "Player")
        {
            Vector3 playerPosition = other.transform.position;
            playerPosition.y += 1.5f;
            Instantiate(explosionEffect, playerPosition, other.transform.rotation);
            LevelManager.instance.RespawnPlayer();
        }
    }
}
