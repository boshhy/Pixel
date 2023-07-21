using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Used to control the pig
public class PigController : MonoBehaviour
{
    // Used to keep track of pig health
    public int pigHealth;

    // Used to adjust pig speed
    public float pigSpeed;

    // Used to keep the pig between two points
    public Transform pointA;
    public Transform pointB;

    // Max time that the pig will move or wait
    public float maxMoveTime;
    public float maxWaitTime;
    
    // Used to keep track of how long the pig has been moving or waiting
    private float currentMoveTime;
    private float currentWaitTime;

    // Used to detect if pig is facing left or right
    public bool movingLeft;

    // Used to reference pigs Rigidbody2D, SpriteRenderer, and Animator    
    private Rigidbody2D rBody;
    private SpriteRenderer sRenderer;
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        rBody = GetComponent<Rigidbody2D>();
        sRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        currentMoveTime = maxMoveTime;
    }

    // Update is called once per frame
    void Update()
    {
        // If pig currentMoveTime is greater than zero, then move pig
        if (currentMoveTime > 0)
        {
            // Adjust moveTime
            currentMoveTime = currentMoveTime - Time.deltaTime;

            // If pig is moving left
            if (movingLeft)
            {
                // Flip sprite renderer to face left
                sRenderer.flipX = false;

                // move the pig left by pigSpeed
                rBody.velocity = new Vector2(-pigSpeed, rBody.velocity.y);

                // If pig reaches the pointA, change his direction
                if(transform.position.x < pointA.position.x)
                {
                    movingLeft = false;
                }
            }
            // Pig is moving right
            else
            {
                // Flip sprite renderer to face right
                sRenderer.flipX = true;

                // move the pig right by pigSpeed
                rBody.velocity = new Vector2(pigSpeed, rBody.velocity.y);

                // If pig reaches the pointB, change his direction
                if(transform.position.x > pointB.position.x)
                {
                    movingLeft = true;
                }
            }

            // If move time has been used up then change pig to wait
            if (currentMoveTime <= 0)
            {
                // Randomly get a wait time between half of maxWaitTime and maxWaitTime
                currentWaitTime = Random.Range(maxWaitTime * 0.5f, maxWaitTime);

                // change animation back to idle
                anim.SetBool("isMoving", false);
            }
        }
        // If pig currentWaitTime is greater than zero, then keep pig idle
        else if (currentWaitTime > 0)
        {
            // Adjust Wait time
            currentWaitTime = currentWaitTime - Time.deltaTime;

            // Do not move the pig left or right
            rBody.velocity = new Vector2(0.0f, rBody.velocity.y);

            // If wait time has been used up then change pig to move
            if (currentWaitTime <= 0)
            {
                // Randomly get a move time between half of maxMoveTime and maxMoveTime
                currentMoveTime = Random.Range(maxMoveTime * 0.5f, maxMoveTime);
                anim.SetBool("isMoving", true);
            }
        }
    }
}
