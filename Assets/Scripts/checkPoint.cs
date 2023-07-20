using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Used to activate the checkpoint touched
public class checkPoint : MonoBehaviour
{
    // Controls the animation of actived checkpoint
    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    // Used to deactivate checkpoints and activate the one touched by player
    private void OnTriggerEnter2D(Collider2D other)
    {
        // If player touched checkpoint and checkpoint not already active 
        // then active the checkpoint touched
        if(other.tag == "Player" && CheckPointController.instance.isActive(transform.position) == false)
        {
            // Deactivate all checkpoints
            CheckPointController.instance.DeactivateCheckPoints();

            // Play SFX when touching checkpoint
            AudioManager.instance.PlaySFX(1);

            // Set the animation of touched checkpoint
            animator.SetBool("checkPointOn", true);

            // Update the spawnpoint to this checkpoint position
            CheckPointController.instance.SetSpawnPoint(transform.position);
        }
    }

    // Used to reset checkpoint animation
    public void ResetCheckPoint()
    {
        animator.SetBool("checkPointOn", false);
    }
}
