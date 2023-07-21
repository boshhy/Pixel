using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Used to control the rino character
public class RinoController : MonoBehaviour
{
    // controls the speed of the rino
    public float rinoSpeed;

    // Used to check if player is withing a certain range of rino
    public Transform player;
    public float agroRange;

    // Used to keep rino walk state between pointA and pointB
    public Transform pointA;
    public Transform pointB;

    // Used to keep rino attack state between PointAAttack and PointBAttack
    public Transform pointAAttack;
    public Transform pointBAttack;

    // Used as rino's max move time and max wait time
    public float maxMoveTime;
    public float maxWaitTime;

    // Used to keep track of rinos current move and current wait time
    private float currentMoveTime;
    private float currentWaitTime;

    // Used to check if rino is moving left or right
    public bool movingLeft;

    // Reference to rinos Rigidbody2D, SpriteRenderer, and Animator
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
        // Check to see how far rino is from player
        float distToPlayer = Vector2.Distance(transform.position , player.position);

        // If the distance from player is less than the aggro range then attack player
        if (distToPlayer <= agroRange)
        {
            // change animaton to attack and change to attack state
            anim.SetBool("isAttacking", true);
            attackPlayer();
        }
        else
        {   
            // Change animation to idle and change to idle/walk state
            anim.SetBool("isAttacking", false);
            idleAndWalk();
        }
    }

    // Used to attack the player
    public void attackPlayer()
    {   
        // If moving left  attack player to the left
        if (movingLeft)
        {   
            // Change direction to left
            sRenderer.flipX = false;

            // Make rino move to the left by four times rinospeed 
            rBody.velocity = new Vector2(-rinoSpeed * 4, rBody.velocity.y);

            // If rino has reached pointAAttack change movement to the right
            if(transform.position.x < pointAAttack.position.x)
            {
                movingLeft = false;
            }
        }
        else
        {
            // Change direction to right
            sRenderer.flipX = true;

            // Make rino move to the right by four times rinospeed 
            rBody.velocity = new Vector2(rinoSpeed * 4, rBody.velocity.y);

            // If rino has reached pointBAttack change movement to the left
            if(transform.position.x > pointBAttack.position.x)
            {
                movingLeft = true;
            }
        }
    }

    // Used to make rino idle and walk when not aggravated
    void idleAndWalk()
    {
        // If move time has not ended keep moving rino
        if (currentMoveTime > 0)
        {
            // Adjust move time
            currentMoveTime = currentMoveTime - Time.deltaTime;

            // If rino is moving left
            if (movingLeft)
            {
                // Face rino to the left
                sRenderer.flipX = false;

                // Make rino move to the left by rinospeed 
                rBody.velocity = new Vector2(-rinoSpeed, rBody.velocity.y);

                // If rino has reached pointA change movement to the right
                if(transform.position.x < pointA.position.x)
                {
                    movingLeft = false;
                }
            }
            // Rino is moving right
            else
            {
                // Face rino to the right
                sRenderer.flipX = true;

                // Make rino move to the right by rinospeed 
                rBody.velocity = new Vector2(rinoSpeed, rBody.velocity.y);

                // If rino has reached pointB change movement to the left
                if(transform.position.x > pointB.position.x)
                {
                    movingLeft = true;
                }
            }

            // If move time has ended change to wait(idle) time
            if (currentMoveTime <= 0)
            {
                // Pick random wait time between half maxWaitTime and MaxWaitTime
                currentWaitTime = Random.Range(maxWaitTime * 0.5f, maxWaitTime);

                // Change animation to idle
                anim.SetBool("isRunning", false);
            }
        }
        // If wait(idle) time has not ended keep moving rino
        else if (currentWaitTime > 0)
        {
            // Adjust wait(idle) time
            currentWaitTime = currentWaitTime - Time.deltaTime;

            // Keep the rino from moving left or right
            rBody.velocity = new Vector2(0.0f, rBody.velocity.y);

            // If wait(idle) time has ended change to move time
            if (currentWaitTime <= 0)
            {
                // Pick random move time between half maxMoveTime and MaxMoveTime
                currentMoveTime = Random.Range(maxMoveTime * 0.5f, maxMoveTime);

                // Change animation to running
                anim.SetBool("isRunning", true);
            }
        }
    }

    // Used to draw the distance from rino to player
    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, agroRange);
    }
}
