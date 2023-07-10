using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
//     public float trunkSpeed;
//     public float runningRange;
//     private Transform player;
//     private Rigidbody2D rBody;

//     private void Start() 
//     {
//         rBody = GetComponent<Rigidbody2D>();
//         player = GameObject.FindGameObjectWithTag("Player").transform;
//     }

//     private void Update() 
//     {
//         float distanceFromPlayer = Vector2.Distance(player.position, transform.position);
//         if (distanceFromPlayer < runningRange)
//         {
//             rBody.velocity = new Vector2(-trunkSpeed, rBody.velocity.y);
//             // transform.position = Vector2.MoveTowards(this.transform.position, player.position, speed * Time.deltaTime); 
//         }
//     }

//     void OnDrawGizmosSelected()
//     {
//         Gizmos.color = Color.green;
//         Gizmos.DrawWireSphere(transform.position, runningRange);
//     }
// }

    public int trunkHealth;
    public float trunkSpeed;
    public Transform pointA;
    public Transform pointB;

    public float maxMoveTime;
    public float maxWaitTime;

    private float currentMoveTime;
    private float currentWaitTime;

    public bool movingLeft;
    public float runningRange;
    public float firingRange;

    // public float originalTimeForFiringBullet;
    // private float timeForFiringBullet;
    public GameObject bullet;
    public Transform firePoint;
    public Transform firePointB;

    private Transform player;
    private Rigidbody2D rBody;
    private SpriteRenderer sRenderer;
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rBody = GetComponent<Rigidbody2D>();
        sRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        currentMoveTime = maxMoveTime;
        // timeForFiringBullet = originalTimeForFiringBullet;
    }

    // Update is called once per frame
    void Update()
    {
        float distanceFromPlayer = Vector2.Distance(player.position, transform.position);
        //Debug.Log("Trunk position: " + transform.position.x + " point A position: " + pointA.position.x + " point B position:" + pointB.position.x);
        if (distanceFromPlayer < runningRange)
        {
            // RUN
            //anim.SetBool("isJumping", false);
            anim.SetBool("isFiring", false);
            anim.SetBool("isRunning", true);

            if (movingLeft) 
            {
                //Debug.Log("should be running left");
                sRenderer.flipX = false;
                rBody.velocity = new Vector2(-trunkSpeed, rBody.velocity.y);
                if(transform.position.x < pointA.position.x)
                {
                    Debug.Log("movingLeft = false");
                    movingLeft = false;
                }
            }
            else
            {
                //Debug.Log("should be running right");
                sRenderer.flipX = true;
                rBody.velocity = new Vector2(trunkSpeed, rBody.velocity.y);
                if(transform.position.x > pointB.position.x)
                {
                    movingLeft = true;
                }
            }

        }
        else if (distanceFromPlayer < firingRange)
        {
            // Fire Bullet
            //anim.SetBool("isJumping", false);
            anim.SetBool("isRunning", false);
            anim.SetBool("isFiring", true);
            //timeForFiringBullet -= Time.deltaTime;
            // if (timeForFiringBullet <= 0.0f)
            // {
            //     anim.SetBool("isFiring", false);
            //     anim.SetBool("isJumping", true);
            //     timeForFiringBullet = originalTimeForFiringBullet;
            // }
            // else {
            //     anim.SetBool("isJumping", false);
            //     anim.SetBool("isFiring", true);
            // }

            if (player.position.x >= transform.position.x)
            {
                movingLeft = false;
                sRenderer.flipX = true;
            }
            else
            {
                movingLeft = true;
                sRenderer.flipX = false;
            }
            rBody.velocity = new Vector2(0.0f, rBody.velocity.y);
        }
        else
        {
            // Idle
            anim.SetBool("isRunning", false);
            anim.SetBool("isFiring", false);
            //anim.SetBool("isJumping", false);
            if (player.position.x >= transform.position.x)
            {
                movingLeft = false;
                sRenderer.flipX = true;
            }
            else
            {
                movingLeft = true;
                sRenderer.flipX = false;
            }
            rBody.velocity = new Vector2(0.0f, rBody.velocity.y);
        }
        
    }        

    private void OnDrawGizmos() {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, runningRange);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, firingRange);
    }

    private void fireBullet()
    {
        if (movingLeft)
        {
            Instantiate(bullet, firePoint.position, firePoint.rotation);
        }
        else 
        {
            var newBullet = Instantiate(bullet, firePointB.position, firePoint.rotation);
            newBullet.GetComponent<SpriteRenderer>().flipX = true;
            newBullet.GetComponent<bullet>().speed = -newBullet.GetComponent<bullet>().speed;
        }
        
    }
}





