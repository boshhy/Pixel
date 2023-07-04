using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PigController : MonoBehaviour
{
    public int pigHealth;
    public float pigSpeed;
    public Transform pointA;
    public Transform pointB;

    public float maxMoveTime;
    public float maxWaitTime;

    private float currentMoveTime;
    private float currentWaitTime;

    public bool movingLeft;

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
        if (currentMoveTime > 0)
        {
            currentMoveTime = currentMoveTime - Time.deltaTime;
            if (movingLeft)
            {
                sRenderer.flipX = false;
                rBody.velocity = new Vector2(-pigSpeed, rBody.velocity.y);
                if(transform.position.x < pointA.position.x)
                {
                    movingLeft = false;
                }
            }
            else
            {
                sRenderer.flipX = true;
                rBody.velocity = new Vector2(pigSpeed, rBody.velocity.y);
                if(transform.position.x > pointB.position.x)
                {
                    movingLeft = true;
                }
            }
            if (currentMoveTime <= 0)
            {
                currentWaitTime = Random.Range(maxWaitTime * 0.5f, maxWaitTime);
                anim.SetBool("isMoving", false);
            }
        }
        else if (currentWaitTime > 0)
        {
            currentWaitTime = currentWaitTime - Time.deltaTime;
            rBody.velocity = new Vector2(0.0f, rBody.velocity.y);

            if (currentWaitTime <= 0)
            {
                currentMoveTime = Random.Range(maxMoveTime * 0.5f, maxMoveTime);
                anim.SetBool("isMoving", true);
            }


        }
        
    }
}
