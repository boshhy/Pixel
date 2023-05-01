using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    public float movementSpeed = 6.0f;
    public float jumpForce = 16.0f;
    Vector2 movement = new Vector2();

    private bool isGrounded;
    private bool canDoubleJump;
    public Transform groundCheckPoint;
    public LayerMask whatIsGround;

    Animator animator;
    private SpriteRenderer spriteRenderer;

    string animationState = "AnimationState";
    Rigidbody2D rb2D;

    enum CharStates
    {
        run = 1,
        jump = 2,
        falling = 3,
        doubleJump = 4,

        idle = 5,
        wallJump = 6
    }

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rb2D = GetComponent<Rigidbody2D>();    
        spriteRenderer  = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateState();
    }

    void FixedUpdate()
    {
        MoveCharacter();
    }

    void MoveCharacter()
    {

        rb2D.velocity = new Vector2(movementSpeed * Input.GetAxis("Horizontal"), rb2D.velocity.y);

        isGrounded = Physics2D.OverlapCircle(groundCheckPoint.position, 0.2f, whatIsGround);

        if (isGrounded)
        {
            canDoubleJump = true;
        }
        
        if (Input.GetButtonDown("Jump"))
        {
            if(isGrounded)
            {
                rb2D.velocity = new Vector2(rb2D.velocity.x, jumpForce);
            }
            else
            {
                if(canDoubleJump)
                {
                    rb2D.velocity = new Vector2(rb2D.velocity.x, jumpForce);
                    canDoubleJump = false;
                }
            }
        }
        if(rb2D.velocity.x < 0)
        {
            spriteRenderer.flipX = true;
        }
        else if(rb2D.velocity.x > 0)
        {
            spriteRenderer.flipX = false;
        }

        movement.x = rb2D.velocity.x;
        movement.y = rb2D.velocity.y;

    }

    void UpdateState()
    {

        if(!canDoubleJump)
        {
            animator.SetInteger(animationState, (int)CharStates.doubleJump);
        }
        else if (Mathf.Abs(movement.x) >= 0 && movement.y > 0.05)
        {
            animator.SetInteger(animationState, (int)CharStates.jump);
        }
        else if (Mathf.Abs(movement.x) >= 0 && movement.y < -0.05)
        {
            animator.SetInteger(animationState, (int)CharStates.falling);
        }
        else if (Mathf.Abs(movement.x) > 0)
        {
            animator.SetInteger(animationState, (int)CharStates.run);
        }
        else
        {
            animator.SetInteger(animationState, (int)CharStates.idle);
        }
    }
}
