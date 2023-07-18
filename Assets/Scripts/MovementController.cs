using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    public static MovementController instance;

    public float movementSpeed = 6.0f;
    public float jumpForce = 16.0f;
    Vector2 movement = new Vector2();

    private bool isGrounded;
    private bool canDoubleJump;
    public Transform groundCheckPoint;
    public LayerMask whatIsGround;

    public Transform wallCheckPointRight;
    public Transform wallCheckPointLeft;
    public LayerMask whatIsWall;

    Animator animator;
    private SpriteRenderer spriteRenderer;

    private bool isWallSliding;
    private float wallSlidingSpeed = 2.0f;
    private bool isKnockedLeft = false;
    private bool isKnockedRight = false;


    private bool isWallJumping;
    private float wallJumpingDirection;
    public float wallJumpingTime = 0.2f;
    public float wallJumpingCounter;
    public float wallJumpingDuration = 0.2f;
    public Vector2 wallJumpingPower = new Vector2(8.0f, 16.0f);


    public float knockBackLength, knockBackForce;
    private float knockBackCounter;//

    string animationState = "AnimationState";
    Rigidbody2D rb2D;

    enum CharStates
    {
        run = 1,
        jump = 2,
        falling = 3,
        doubleJump = 4,

        idle = 5,
        wallJump = 6,
        hit = 7
    }

    private void Awake() 
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rb2D = GetComponent<Rigidbody2D>();    
        spriteRenderer  = GetComponent<SpriteRenderer>();
        CheckPointController.instance.SetSpawnPoint(transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        if (knockBackCounter <= 0)
        {
            isKnockedLeft = false;
            isKnockedRight = false;
            if (!isWallJumping)
            {
                MoveCharacter();
                wallSlide();
            }
        }
        else
        {
            knockBackCounter -= Time.deltaTime;
            if (isKnockedLeft)
            {
                rb2D.velocity = new Vector2(rb2D.velocity.x, rb2D.velocity.y);
            }
            else if (isKnockedRight)
            {
                rb2D.velocity = new Vector2(rb2D.velocity.x, rb2D.velocity.y);
            }
            else if (!spriteRenderer.flipX)
            {
                rb2D.velocity = new Vector2(-knockBackForce, rb2D.velocity.y);
            }
            else
            {
                rb2D.velocity = new Vector2(knockBackForce, rb2D.velocity.y);
            }
        }
        UpdateState();
        
    }

    void FixedUpdate()
    {
        // MoveCharacter();
    }

    void MoveCharacter()
    {
        rb2D.velocity = new Vector2(movementSpeed * Input.GetAxisRaw("Horizontal"), rb2D.velocity.y);

        isGrounded = Physics2D.OverlapCircle(groundCheckPoint.position, 0.2f, whatIsGround);

        if (isGrounded)
        {
            canDoubleJump = true;
        }
        // used for slashing forward (broken: hits edge and bounces up)
        // if (Input.GetButtonDown("x"))
        // {
        //     if (spriteRenderer.flipX == true)
        //     {
        //         rb2D.velocity = new Vector2(-jumpForce * 5, 0.0f);
        //     }
        //     else
        //     {
        //         rb2D.velocity = new Vector2(jumpForce * 5, rb2D.velocity.y);
        //     }
        // }
        if (Input.GetButtonDown("Jump"))
        {
            if (isWallSliding == true)
            {
                wallJump();
            }
            else if (isGrounded)
            {
                AudioManager.instance.PlaySFX(5);
                rb2D.velocity = new Vector2(rb2D.velocity.x, jumpForce);
            }
            else
            {
                if(canDoubleJump)
                {
                    AudioManager.instance.PlaySFX(5);
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
        if (knockBackCounter <= 0)
        {
            if (isWallJumping)
            {
                animator.SetInteger(animationState, (int)CharStates.jump);
            }
            else if (isWallSliding)
            {
                animator.SetInteger(animationState, (int)CharStates.wallJump);
            }
            else if (!canDoubleJump)
            {
                animator.SetInteger(animationState, (int)CharStates.doubleJump);
            }
            else if ((Mathf.Abs(movement.x) >= 0 && movement.y > 0.05 && isGrounded == false))
            {
                animator.SetInteger(animationState, (int)CharStates.jump);
            }
            else if (Mathf.Abs(movement.x) >= 0 && movement.y < -0.05  && isGrounded == false)
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

    public void KnockBack()
    {
        rb2D.velocity = new Vector2(0f, knockBackForce);
        knockBackCounter = knockBackLength;

        animator.SetInteger(animationState, (int)CharStates.hit);
    }

    public void KnockBackLeft()
    {
        Debug.Log("knock back to left-----------");
        rb2D.AddForce(new Vector2(-2000.0f, 800.0f));
        //rb2D.velocity = new Vector2(-100.0f, knockBackForce);
        knockBackCounter = knockBackLength+0.1f;
        isKnockedLeft = true;

        animator.SetInteger(animationState, (int)CharStates.hit);
    }

    public void KnockBackRight()
    {
        Debug.Log("knock back to right xxxxxxxxxxx: " + rb2D.velocity.x);
        rb2D.AddForce(new Vector2(2000.0f, 800.0f));
        //rb2D.velocity = new Vector2(500.0f, knockBackForce);
        knockBackCounter = knockBackLength+0.1f;
        isKnockedRight = true;

        animator.SetInteger(animationState, (int)CharStates.hit);
    }

    public void killJump()
    {
        rb2D.velocity = new Vector2(rb2D.velocity.x, jumpForce);
    }

    public bool getCanDoubleJump()
    {
        return canDoubleJump;
    }

    public void resetDoubleJump()
    {
        AudioManager.instance.PlaySFX(5);
        rb2D.velocity = new Vector2(rb2D.velocity.x, jumpForce);
        canDoubleJump = true;
    }
    
    public void trampolineJump()
    {
        AudioManager.instance.PlaySFX(5);
        rb2D.velocity = new Vector2(rb2D.velocity.x, jumpForce * 1.5f);
        canDoubleJump = true;
    }

    private bool isWalled()
    {
        return Physics2D.OverlapCircle(wallCheckPointLeft.position, 0.2f, whatIsWall) || Physics2D.OverlapCircle(wallCheckPointRight.position, 0.2f, whatIsWall);
    }

    private void wallSlide()
    {
        if(isWalled() && !isGrounded && rb2D.velocity.x != 0.0f)
        {
           // Debug.Log("is wall sliding");
            isWallSliding = true;
            rb2D.velocity = new Vector2(rb2D.velocity.x, Mathf.Clamp(rb2D.velocity.y, -wallSlidingSpeed, float.MaxValue));
        }
        else{
            isWallSliding = false;
        }
    }

    private void wallJump()
    {
        if (isWallSliding)
        {
            isWallJumping = false;
            if (spriteRenderer.flipX)
            {
                wallJumpingDirection = 1;
            }
            else
            {
                wallJumpingDirection = -1;
            }
            wallJumpingCounter = wallJumpingTime;

            CancelInvoke(nameof(stopWallJumping));
        }
        else
        {
            wallJumpingCounter -= Time.deltaTime;
        }

        if (Input.GetButtonDown("Jump") && wallJumpingCounter > 0.0f)
        {
            
            isWallJumping = true;
            //rb2D.velocity = new Vector2(rb2D.velocity.x, jumpForce);
            //rb2D.velocity = new Vector2(wallJumpingDirection * rb2D.velocity.x, jumpForce);
            AudioManager.instance.PlaySFX(11);
            rb2D.velocity = new Vector2(wallJumpingDirection * wallJumpingPower.x, wallJumpingPower.y);
            canDoubleJump = true;
            wallJumpingCounter = 0.0f;

            Invoke(nameof(stopWallJumping), wallJumpingDuration);
        }
        //Debug.Log("End of walljump()");
    }

    private void stopWallJumping()
    {
        isWallJumping = false;
    }
    


}
