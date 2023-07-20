using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Manages player respawn when loadding a scene or when player dies
public class LevelManager : MonoBehaviour
{
    // Used to reference this one and only instance 
    public static LevelManager instance;

    // Used to make player respawn after some seconds
    public float waitToRespawn;

    // Used to see how many strawberries the player has collected
    public int strawberriesCollected;

    // Used to set the spawnpoint of camera after player death
    private Vector3 spawnPointForCamera;

    // Used to reference the camera
    public Camera theCamera;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Used to call the Coroutine for player restart
    public void RespawnPlayer()
    {
        StartCoroutine(RespawnCo());
    }

    // Used to respawn player
    private IEnumerator RespawnCo()
    {
        // Deactivate the player object
        MovementController.instance.gameObject.SetActive(false);

        // Wait a certain amount of seconds
        yield return new WaitForSeconds(waitToRespawn);

        // Reactivate the player
        MovementController.instance.gameObject.SetActive(true);

        // Change the player's positin to the spawnpoint
        MovementController.instance.transform.position = CheckPointController.instance.spawnPoint;

        // Used to set the reset the camera back on the player but adjust the cameras
        // z and y values back to what they should be;
        spawnPointForCamera = CheckPointController.instance.spawnPoint;
        spawnPointForCamera.z = -10f;
        spawnPointForCamera.y = 1f;

        // Sometimes camera should be position offset of the player, this makes sure we can a smooth adjustment
        theCamera.GetComponent<CameraController>().FixGlitch(spawnPointForCamera.x, spawnPointForCamera.y, spawnPointForCamera.z);

        // Set the players current health back to max health
        PlayerHealthController.instance.currentHealth = PlayerHealthController.instance.maxHealth;

        // Update the UI display so heart show up correctly
        UIController.instance.UpdateHealthDisplay();
    }
}
