using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Used to hurt the enemy boss
public class hurtBoss : MonoBehaviour
{
    // Used to reference the boss's animator
    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponentInParent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Used to see if boss's hurtbox has been hit
    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.tag == "Player")
        {
                // Change boss's animation to hurt
                anim.SetBool("isRunning", false);
                anim.SetBool("isFiring", false);
                anim.SetBool("isJumping", false);
                anim.SetBool("isHurt", true);

                // Make them invincible for a certain length
                GetComponentInParent<BossController>().hurtCounter = GetComponentInParent<BossController>().hurtLength;
                
                // make player bounce off boss and deal damage to boss
                MovementController.instance.killJump();
                BossHealthController.instance.DealDamage();
        }    
    }
}
