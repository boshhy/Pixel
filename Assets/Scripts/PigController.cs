using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PigController : MonoBehaviour
{
    public float pigSpeed;
    public Transform pointA;
    public Transform pointB;

    public bool movingLeft;

    private Rigidbody2D rBody;
    private SpriteRenderer sRenderer;

    // Start is called before the first frame update
    void Start()
    {
        rBody = GetComponent<Rigidbody2D>();
        sRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(movingLeft)
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
        
    }
}
