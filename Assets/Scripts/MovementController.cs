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

    string animationState = "AnimationState";
    Rigidbody2D rb2D;

    enum CharStates
    {
        walkEast = 1,
        walkSouth = 2,
        walkWest = 3,
        walkNorth = 4,

        idleEast = 5,
        idleWest = 6
    }

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rb2D = GetComponent<Rigidbody2D>();    
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
        movement.x = rb2D.velocity.x;
        movement.y = rb2D.velocity.y;
        // movement.x = Input.GetAxisRaw("Horizontal");
        // movement.y = rb2D.velocity.y;
        // movement.Normalize();
        // rb2D.velocity = movement * movementSpeed;

        // if(Input.GetButtonDown("Jump"))
        // {
        //     movement.x = rb2D.velocity.x;
        //     movement.Normalize();
        //     movement.y = jumpForce;
        //     rb2D.velocity = movement * movementSpeed;
        // }

    }

    void UpdateState()
    {

        if (movement.x > 0)
        {
            this.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));
            animator.SetInteger(animationState, (int)CharStates.walkEast);
        }
        else if (movement.x < 0)
        {
            this.transform.rotation = Quaternion.Euler(new Vector3(0f, 180f, 0f));
            animator.SetInteger(animationState, (int)CharStates.walkEast);
        }
        else
        {
            animator.SetInteger(animationState, (int)CharStates.idleEast);
        }
    }

}
