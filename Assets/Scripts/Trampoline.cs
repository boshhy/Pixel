using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Used to control the trampoline
public class Trampoline : MonoBehaviour
{   
    // Get a reference to the animator
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Used to see if player has touched the trampoline
    void OnTriggerEnter2D(Collider2D other)
    {   
        // If player collides with trampoline, trampoline launch animatino is enabled
        if (other.tag == "Player")
        {
            anim.SetBool("isJumping", false);
            anim.SetBool("isJumping", true);
        }
    }

    // Used to change trampoline animatio to idle
    public void setIdle()
    {
        anim.SetBool("isJumping", false);
    }
}
