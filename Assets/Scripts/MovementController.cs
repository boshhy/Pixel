using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    public float movementSpeed = 3.0f;
    Vector2 movement = new Vector2();

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
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y =  Input.GetAxisRaw("Vertical");

        movement.Normalize();

        rb2D.velocity = movement * movementSpeed;
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
