using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointController : MonoBehaviour
{
    public static CheckPointController instance;

    private checkPoint[] checkpoints;

    public Vector3 spawnPoint;

    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        checkpoints = FindObjectsOfType<checkPoint>();

        spawnPoint = MovementController.instance.transform.position;
        Debug.Log("spawnpoint is: " + spawnPoint);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DeactivateCheckPoints()
    {
        for (int i = 0; i < checkpoints.Length; i++)
        {
            checkpoints[i].ResetCheckPoint();
        }
    }

    public void SetSpawnPoint(Vector3 newSpawnPoint)
    {
        spawnPoint = newSpawnPoint;
    }

    public bool isActive(Vector3 spawnToCheck)
    {
        return spawnPoint == spawnToCheck;
    }
}
