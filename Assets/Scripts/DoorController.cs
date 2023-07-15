using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class DoorController : MonoBehaviour
{
    public bool PlayerHasKey = false;
    public bool isDoorOpen = false;
    public bool playerInDoorway = false;
    public Sprite openDoor;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("up") && playerInDoorway && isDoorOpen)
        {
            Debug.Log("Player should enter door now.");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            playerInDoorway = true;
            if (PlayerHasKey)
            {
                this.GetComponent<SpriteRenderer>().sprite = openDoor;
                isDoorOpen = true;
            }
            

        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            playerInDoorway = false;
        }
    }

}
