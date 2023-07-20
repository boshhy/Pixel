using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Used to set spawnpoint and activate or deactivate checkpoints
public class CheckPointController : MonoBehaviour
{
    // Used to reference this one and only instance 
    public static CheckPointController instance;

    // This array will hold the checkpoints in the coresponding level
    private checkPoint[] checkpoints;

    // Used as spawn point for when the player dies
    public Vector3 spawnPoint;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        // Get all the checkpoints in the level
        checkpoints = FindObjectsOfType<checkPoint>();

        // Set first spawnpoint to where the player starts the level
        spawnPoint = MovementController.instance.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Used to deactivate all theck points (change animation to off)
    public void DeactivateCheckPoints()
    {
        for (int i = 0; i < checkpoints.Length; i++)
        {   
            checkpoints[i].ResetCheckPoint();
        }
    }

    // Set the spawnpoint to the new checkpoint position
    public void SetSpawnPoint(Vector3 newSpawnPoint)
    {
        spawnPoint = newSpawnPoint;
    }

    // Used to see if current spawn point is the same as the one touched
    public bool isActive(Vector3 spawnToCheck)
    {
        return spawnPoint == spawnToCheck;
    }
}
