using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Used to control a moving platform
public class itemPlatform : MonoBehaviour
{
    // Keeps track of an up and down point that the platform
    // should not pass
    public Transform pointUp;
    public Transform pointDown;

    // Controls speed of platform
    public float platformSpeed;

    // Check to see if the platform is moving, and
    // what direction its moving
    public bool isMoving;
    public bool isMovingUp;
    
    // Used to move platform back to its starting point
    private float startingPoint;

    // Controls the velocity of the platform
    private Rigidbody2D rBody;

    // Used to change animation from off to on or vice versa
    private Animator anim;


    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rBody = GetComponent<Rigidbody2D>();
        startingPoint = transform.position.y;
        isMoving = false;
        isMovingUp = true;
    }

    // Update is called once per frame
    void Update()
    {
        // only move is moving is activated by player stading on it
        if (isMoving)
        {   
            // If platform should be moving up, change it velocity to refect that
            if (isMovingUp)
            {
                rBody.velocity = new Vector2(rBody.velocity.x, platformSpeed);

                // Once it reaches the 'pointUp' change its direction to go down
                if(transform.position.y > pointUp.position.y)
                {
                    isMovingUp = false;
                }
            }
            // If platform should be down up, change it velocity to refect that
            else
            {
                rBody.velocity = new Vector2(rBody.velocity.x, -platformSpeed);

                // Once it reaches the 'pointDown' change its direction to go up
                if(transform.position.y < pointDown.position.y)

                {
                    isMovingUp = true;
                }
            }
        }
        // Player is touching the platform
        else
        {
            // If platform not at starting position move it to starting position
            if (startingPoint != transform.position.y)
            {
                if (transform.position.y < startingPoint)
                {
                    rBody.velocity = new Vector2(rBody.velocity.x, platformSpeed);
                }
                else if (transform.position.y > startingPoint)
                {
                    rBody.velocity = new Vector2(rBody.velocity.x, -platformSpeed);
                }
            }
            // Platform is at starting positin and should stop moving (reset isMoving to default state)
            else
            {
                rBody.velocity = new Vector2(0.0f, 0.0f);
                isMovingUp = true;
            }
        }
    }

    // When player touches the platform change 'isMoving' to true
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            isMoving = true;
        }
    }

    // When player jumps off platform change 'isMoving' to false
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            isMoving = false;
        }
    }
}
