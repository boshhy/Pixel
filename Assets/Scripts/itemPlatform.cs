using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemPlatform : MonoBehaviour
{
    public Transform pointUp;
    public Transform pointDown;

    public float platformSpeed;
    public bool isMoving;
    public float maxMoveTime;
    public float maxWaitTime;

    private float currentMoveTime;
    private float currentWaitTime;
    
    private float startingPoint;

    public bool isMovingUp;

    private Rigidbody2D rBody;
    private SpriteRenderer sRenderer;
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        sRenderer = GetComponent<SpriteRenderer>();
        rBody = GetComponent<Rigidbody2D>();
        startingPoint = transform.position.y;
        currentMoveTime = maxMoveTime;
        isMoving = false;
        isMovingUp = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (isMoving)
        {
            if (isMovingUp)
            {
                rBody.velocity = new Vector2(rBody.velocity.x, platformSpeed);
                if(transform.position.y > pointUp.position.y)
                {
                    isMovingUp = false;
                }
            }
            else
            {
                rBody.velocity = new Vector2(rBody.velocity.x, -platformSpeed);
                if(transform.position.y < pointDown.position.y)

                {
                    isMovingUp = true;
                }

            }
        }
        else
        {
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
            else
            {
                rBody.velocity = new Vector2(0.0f, 0.0f);
                isMovingUp = true;
            }
        }
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            isMoving = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            isMoving = false;
        }
    }
}
