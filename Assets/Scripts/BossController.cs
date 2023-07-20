using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Used to control the movements of the 'Trunk' boss

public class BossController : MonoBehaviour
{
    // Used to keep track of health
    public int trunkHealth;
    
    // Used to keep track of speed
    public float trunkSpeed;

    // Used to make character walk from ponit A to point B
    public Transform pointA;
    public Transform pointB;

    // Used to detect if character is jumping from ground or air
    public Transform groundTouchCheck;
    public LayerMask whatIsGround;
    public bool isGrounded;
    public float testJumpPower;

    // Used to keep track of move and wait times
    public float maxMoveTime;
    public float maxWaitTime;
    private float currentMoveTime;
    private float currentWaitTime;

    // Used to detect if character is moving left
    public bool movingLeft;

    // Used to see if player is within running or firing range
    public float runningRange;
    public float firingRange;

    // Used to keep track of player invincibility length
    public float hurtCounter;
    public float hurtLength;

    // Used to fire bullet at certain intervals
    // and where the bullet should instanstiate
    public float originalTimeForFiringBullet;
    public float timeForFiringBullet;
    public GameObject bullet;
    public Transform firePoint;
    public Transform firePointB;

    // Used to keep track of player
    private Transform player;

    // Used to keep track of character animation and velocity
    private Rigidbody2D rBody;
    private SpriteRenderer sRenderer;
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rBody = GetComponent<Rigidbody2D>();
        sRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        currentMoveTime = maxMoveTime;
        timeForFiringBullet = originalTimeForFiringBullet;
    }

    // Update is called once per frame
    void Update()
    {
        // Get the distance of character from player 
        float distanceFromPlayer = Vector2.Distance(player.position, transform.position);
        // Check to see if character is on the ground
        isGrounded = Physics2D.OverlapCircle(groundTouchCheck.position, 0.2f, whatIsGround);
        
        // If character is not hurt (invincibility when hurt)
        if (hurtCounter <= 0)
        {
            // Only if character is on the ground then update
            if (isGrounded)
            {
                // If player is withing running range, make character run
                if (distanceFromPlayer < runningRange)
                {
                    // Change animation to [run]
                    anim.SetBool("isHurt", false);
                    anim.SetBool("isJumping", false);
                    anim.SetBool("isFiring", false);
                    anim.SetBool("isRunning", true);

                    // Change velocity to run left
                    if (movingLeft) 
                    {
                        sRenderer.flipX = false;
                        rBody.velocity = new Vector2(-trunkSpeed, rBody.velocity.y);

                        // If player hits point A, reverse run
                        if(transform.position.x < pointA.position.x)
                        {
                            movingLeft = false;
                        }
                    }
                    else
                    {
                        // Change velocity to run right
                        sRenderer.flipX = true;
                        rBody.velocity = new Vector2(trunkSpeed, rBody.velocity.y);

                        // If player hits point B, reverse run
                        if(transform.position.x > pointB.position.x)
                        {
                            movingLeft = true;
                        }
                    }

                }
                // Else if player is within firing distance, make character fire a bullet
                else if (distanceFromPlayer < firingRange)
                {
                    // Change animation to Fire Bullet won ground
                    if (rBody.velocity.y == 0.0f){
                        anim.SetBool("isHurt", false);
                        anim.SetBool("isJumping", false);
                        anim.SetBool("isRunning", false);
                        anim.SetBool("isFiring", true);
                    }

                    // Used to firing bullet intervals
                    timeForFiringBullet -= Time.deltaTime;

                    // Fire a bullet while jumping
                    if (timeForFiringBullet <= 0.0f)
                    {
                        anim.SetBool("isHurt", false);
                        anim.SetBool("isRunning", false);
                        anim.SetBool("isFiring", false);
                        anim.SetBool("isJumping", true);
                        rBody.AddForce(new Vector2(rBody.velocity.x, testJumpPower));
                        timeForFiringBullet = originalTimeForFiringBullet;
                    }
                    // Make character face the player and stop moving
                    else
                    {
                        if (player.position.x >= transform.position.x)
                        {
                            movingLeft = false;
                            sRenderer.flipX = true;
                        }
                        else
                        {
                            movingLeft = true;
                            sRenderer.flipX = false;
                        }
                        rBody.velocity = new Vector2(0.0f, rBody.velocity.y);
                    }
                }
                else
                {
                    // Change animation to Idle
                    anim.SetBool("isHurt", false);
                    anim.SetBool("isRunning", false);
                    anim.SetBool("isFiring", false);
                    anim.SetBool("isJumping", false);

                    // Make character face the player
                    if (player.position.x >= transform.position.x)
                    {
                        movingLeft = false;
                        sRenderer.flipX = true;
                    }
                    else
                    {
                        movingLeft = true;
                        sRenderer.flipX = false;
                    }          
                    rBody.velocity = new Vector2(0.0f, rBody.velocity.y);

                }
            }
        }
        else
        {
            // Take time from hurt counter
            hurtCounter -= Time.deltaTime;
        }
    }        

    // Used to draw running and firing range to Unity Program
    private void OnDrawGizmos() 
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, runningRange);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, firingRange);
    }


    // Character bullet firing is called from the animator
    private void fireBullet()
    {   
        // If facing left create a bullet going left (default)
        if (movingLeft)
        {
            Instantiate(bullet, firePoint.position, firePoint.rotation);
        }
        // Else flip the bullet to face right and change the velocity to reflect
        else 
        {
            var newBullet = Instantiate(bullet, firePointB.position, firePoint.rotation);
            newBullet.GetComponent<SpriteRenderer>().flipX = true;
            newBullet.GetComponent<bullet>().speed = -newBullet.GetComponent<bullet>().speed;
        }
        
    }
}
