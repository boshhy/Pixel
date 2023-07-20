using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Used to control a bullet that has been instantiated
public class bullet : MonoBehaviour
{
    // Controls the speed of the bullet
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Move the bullet
        transform.position += new Vector3(-speed * transform.localScale.x * Time.deltaTime, 0.0f, 0.0f);    
    }

    // Used to hurt player
    void OnTriggerEnter2D(Collider2D other)
    {
        // If bullet hits player, deal damage to player
        if (other.tag == "Player") 
        {
            PlayerHealthController.instance.DealDamage();
        }
        
        // Destroy bullet when it comes in contact with an object
        Destroy(gameObject);
    }

}
