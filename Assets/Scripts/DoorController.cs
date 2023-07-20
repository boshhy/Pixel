using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Used to control the door
public class DoorController : MonoBehaviour
{
    // Used to transition from level to level
    public GameObject LevelTransition;

    // Keeps track if player has the key 
    public bool PlayerHasKey = false;

    // keeps track if the door has been opned
    public bool isDoorOpen = false;

    // Keeps track to see if player is in the doorway
    public bool playerInDoorway = false;

    // Used to change from closed door to an open door sprite
    public Sprite openDoor;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // If player presses up and is in the doorway and the door is open then enter the door
        if (Input.GetButtonDown("up") && playerInDoorway && isDoorOpen)
        {
            // Play door enter SFX and load the next level
            AudioManager.instance.PlaySFX(9);
            LevelTransition.GetComponent<LevelLoader>().LoadNextLevel();
        }
    }

    // Used to check if player has entered the doorway
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            // If player is in doorway change bool to true
            playerInDoorway = true;

            // If player has the kepp open the door
            if (PlayerHasKey)
            {
                this.GetComponent<SpriteRenderer>().sprite = openDoor;
                isDoorOpen = true;
            }
            

        }
    }

    // When player leaves the doorway change the bool to false
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            playerInDoorway = false;
        }
    }

}
