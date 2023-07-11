using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hurtBoss : MonoBehaviour
{
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

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.tag == "Player")
        {
                Debug.Log("should hurt trunk");
                anim.SetBool("isRunning", false);
                anim.SetBool("isFiring", false);
                anim.SetBool("isJumping", false);
                anim.SetBool("isHurt", true);
                GetComponentInParent<BossController>().hurtCounter = GetComponentInParent<BossController>().hurtLength;
                //hurtCounter = hurtLength;
                MovementController.instance.killJump();
                
        }    
    }
}
