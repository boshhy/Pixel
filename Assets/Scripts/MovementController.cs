using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Used to control player movement and animations
public class MovementController : MonoBehaviour
{
    // Used to reference this one and only instance 
    public static MovementController instance;

    // Controls the movement speed and jump force
    public float movementSpeed = 6.0f;
    public float jumpForce = 16.0f;
    Vector2 movement = new Vector2();

    // Checks to see if player is grounded
    private bool isGrounded;
    public Transform groundCheckPoint;
    public LayerMask whatIsGround;

    // Check to see if player can double jump
    private bool canDoubleJump;

    // Check to see if the player is touching a wall
    public Transform wallCheckPointRight;
    public Transform wallCheckPointLeft;
    public LayerMask whatIsWall;

    // Checks to see if player is sliding on the wall
    private bool isWallSliding;

    // Checks to see if player is jumping from a wall 
    private bool isWallJumping;

    // Used to control deactivation of player control
    public float wallJumpingTime = 0.2f;
    public float wallJumpingCounter;
    public float wallJumpingDuration = 0.2f;

    // Controler wall jump force
    public Vector2 wallJumpingPower = new Vector2(8.0f, 16.0f);

    // Controls what direction to apply jump power
    private float wallJumpingDirection;

    // controls wall slide speed
    private float wallSlidingSpeed = 2.0f;

    // Reference to the players animator and sprite renderer
    Animator animator;
    private SpriteRenderer spriteRenderer;

    // Check to see if the player is being knocked left or right by a boss enemy
    private bool isKnockedLeft = false;
    private bool isKnockedRight = false;

    // Used to deactivate player controls for knock back length
    public float knockBackLength;
    private float knockBackCounter;

    // Used to apply a knockback force to player
    public float knockBackForce;

    // Used to store a string
    string animationState = "AnimationState";

    // Reference to players rigid body (2D)
    Rigidbody2D rb2D;

    // Creates a constant reference to variables and associated animation number
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
        // Only update player if menu is unpaused
        if (!PauseMenu.isPaused)
        {   
            // If we are not being knocked back change bools
            if (knockBackCounter <= 0)
            {
                isKnockedLeft = false;
                isKnockedRight = false;

                // If we are not wall jumping, update character movement and wall slide
                if (!isWallJumping)
                {
                    MoveCharacter();
                    wallSlide();
                }
            }
            // We are being knocked back and should not have control of player
            else
            {
                // update 'knockBackCounter' time left
                knockBackCounter -= Time.deltaTime;

                // If player is knocked left by boss, 
                // keep the velocity applied to left while knockback counter is not zero or less
                if (isKnockedLeft)
                {
                    rb2D.velocity = new Vector2(rb2D.velocity.x, rb2D.velocity.y);
                }
                // If player is knocked right by boss, 
                // keep the velocity applied to right while knockback counter is not zero or less
                else if (isKnockedRight)
                {
                    rb2D.velocity = new Vector2(rb2D.velocity.x, rb2D.velocity.y);
                }
                // Change velocity of player accoding to direction the player is facing
                else if (!spriteRenderer.flipX)
                {
                    rb2D.velocity = new Vector2(-knockBackForce, rb2D.velocity.y);
                }
                else
                {
                    rb2D.velocity = new Vector2(knockBackForce, rb2D.velocity.y);
                }
            }

            // Update the animation of the player
            UpdateState();
        }
    }

    // Update character movement
    void MoveCharacter()
    {
        // Change player velocity according to horizontal input
        rb2D.velocity = new Vector2(movementSpeed * Input.GetAxisRaw("Horizontal"), rb2D.velocity.y);

        // Check to see if the player is grounded
        isGrounded = Physics2D.OverlapCircle(groundCheckPoint.position, 0.2f, whatIsGround);

        // If the player is grounded, the player can double jump
        if (isGrounded)
        {
            canDoubleJump = true;
        }

        // if the jump button was pressed make the player jump
        if (Input.GetButtonDown("Jump"))
        {
            // If player is wall sliding the apply a wall jump
            if (isWallSliding == true)
            {
                wallJump();
            }
            // If player grounded play a jump SFX and apply a normal jump
            else if (isGrounded)
            {
                AudioManager.instance.PlaySFX(5);
                rb2D.velocity = new Vector2(rb2D.velocity.x, jumpForce);
            }
            // Else if we are not grounded see if player can double jump
            else
            {
                // If player can double jump, apply a double jump
                if(canDoubleJump)
                {
                    AudioManager.instance.PlaySFX(5);
                    rb2D.velocity = new Vector2(rb2D.velocity.x, jumpForce);

                    // Change 'canDoubleJump' so player can't jump indefinitely 
                    canDoubleJump = false;
                }
            }
        }

        // If player is moving left or right change sprite accordingly 
        if(rb2D.velocity.x < 0)
        {
            spriteRenderer.flipX = true;
        }
        else if(rb2D.velocity.x > 0)
        {
            spriteRenderer.flipX = false;
        }

        // Used by animator to decide what state player should be in        
        movement.x = rb2D.velocity.x;
        movement.y = rb2D.velocity.y;

    }

    // Used to update the animation state of the player
    void UpdateState()
    {
        // Update state if player is not being knockedback
        if (knockBackCounter <= 0)
        {   
            // If player is wall jumping change animation to jump state
            if (isWallJumping)
            {
                animator.SetInteger(animationState, (int)CharStates.jump);
            }
            // If player is wall sliding change animation to wall slide
            else if (isWallSliding)
            {
                animator.SetInteger(animationState, (int)CharStates.wallJump);
            }
            // If player is not longer double jump, means player is currently double jumping
            else if (!canDoubleJump)
            {
                animator.SetInteger(animationState, (int)CharStates.doubleJump);
            }
            // If player is not grounded and player's y velocity is positive, make animatiom jump state
            else if ((Mathf.Abs(movement.x) >= 0 && movement.y > 0.05 && isGrounded == false))
            {
                animator.SetInteger(animationState, (int)CharStates.jump);
            }
            // If player is not grounded and player's y velocity is negative, make animatiom falling state 
            else if (Mathf.Abs(movement.x) >= 0 && movement.y < -0.05  && isGrounded == false)
            {
                animator.SetInteger(animationState, (int)CharStates.falling);
            }
            // If player is moving left or right, change animation to walk 
            else if (Mathf.Abs(movement.x) > 0)
            {
                animator.SetInteger(animationState, (int)CharStates.run);
            }
            // Change animation to idle
            else
            {
                animator.SetInteger(animationState, (int)CharStates.idle);
            }
        }
    }

    // Used to knock back player when hurt
    public void KnockBack()
    {
        rb2D.velocity = new Vector2(0f, knockBackForce);
        knockBackCounter = knockBackLength;
        
        // Change animaton to hit state
        animator.SetInteger(animationState, (int)CharStates.hit);
    }

    // Used to knockback player to the left when hit by boss
    public void KnockBackLeft()
    {
        // Apply knockback force to player to the left
        rb2D.AddForce(new Vector2(-2000.0f, 800.0f));
        knockBackCounter = knockBackLength+0.1f;
        isKnockedLeft = true;

        // Change animation to hit state
        animator.SetInteger(animationState, (int)CharStates.hit);
    }

    public void KnockBackRight()
    {
        // Apply knockback force to player to the right
        rb2D.AddForce(new Vector2(2000.0f, 800.0f));
        knockBackCounter = knockBackLength+0.1f;
        isKnockedRight = true;

        // Change animatino to hit state
        animator.SetInteger(animationState, (int)CharStates.hit);
    }

    // Used to make player bounce when an enemy is killed
    public void killJump()
    {
        rb2D.velocity = new Vector2(rb2D.velocity.x, jumpForce);
    }

    // Returns a bool to see if player can double jump
    public bool getCanDoubleJump()
    {
        return canDoubleJump;
    }

    // Used to reset the double jump of player, used when player hits enemy
    public void resetDoubleJump()
    {
        AudioManager.instance.PlaySFX(5);
        rb2D.velocity = new Vector2(rb2D.velocity.x, jumpForce);
        canDoubleJump = true;
    }
    
    // Used to reset the double jump of player, used when jumping off a trampoline
    // jumpforce increased by 1.5
    public void trampolineJump()
    {
        AudioManager.instance.PlaySFX(5);
        rb2D.velocity = new Vector2(rb2D.velocity.x, jumpForce * 1.5f);
        canDoubleJump = true;
    }

    // Returns a bool to answer if player is touching a wall
    private bool isWalled()
    {
        return Physics2D.OverlapCircle(wallCheckPointLeft.position, 0.2f, whatIsWall) || Physics2D.OverlapCircle(wallCheckPointRight.position, 0.2f, whatIsWall);
    }

    // Used to detect if player is wall sliding
    private void wallSlide()
    {
        if(isWalled() && !isGrounded && rb2D.velocity.x != 0.0f)
        {
            // Change bool to true if player is wall sliding
            isWallSliding = true;
            
            // Make player slide on wall according to 'wallslidingSpeed'
            rb2D.velocity = new Vector2(rb2D.velocity.x, Mathf.Clamp(rb2D.velocity.y, -wallSlidingSpeed, float.MaxValue));
        }
        // Else Player is not wall sliding
        else
        {
            isWallSliding = false;
        }
    }

    // Used to make player jump from a wall
    private void wallJump()
    {
        // If player is wall sliding then reset wall jump timers
        if (isWallSliding)
        {
            // Player is no longer jumping (in the air)
            isWallJumping = false;

            // Change wall jumping direction depending on sprite direction
            if (spriteRenderer.flipX)
            {
                wallJumpingDirection = 1;
            }
            else
            {
                wallJumpingDirection = -1;
            }

            // Reset wallJumpingCounter
            wallJumpingCounter = wallJumpingTime;

            // Cancel wall jumping
            CancelInvoke(nameof(stopWallJumping));
        }
        else
        // Player is jumping, so deduct rfom wall jumping counter
        {
            wallJumpingCounter -= Time.deltaTime;
        }

        // If player hit the jump button and Player can wall jump, then wall jump
        if (Input.GetButtonDown("Jump") && wallJumpingCounter > 0.0f)
        {
            // Change bool to true and play associated SFX
            isWallJumping = true;
            AudioManager.instance.PlaySFX(11);

            // Change player velocity to reflect jumping direction
            rb2D.velocity = new Vector2(wallJumpingDirection * wallJumpingPower.x, wallJumpingPower.y);

            // Player can double jump
            canDoubleJump = true;

            // Change counter so player can't spam wall jump
            wallJumpingCounter = 0.0f;

            // Change 'isWallJumping' to false after wall jumping duration finishes
            Invoke(nameof(stopWallJumping), wallJumpingDuration);
        }

    }

    // Changes player wall jump to false so player can have control of movement back
    private void stopWallJumping()
    {
        isWallJumping = false;
    }
}
