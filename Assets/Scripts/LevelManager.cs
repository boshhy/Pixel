using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    public float waitToRespawn;

    public int strawberriesCollected;

    private Vector3 spawnPointForCamera;

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

    public void RespawnPlayer()
    {
        StartCoroutine(RespawnCo());
    }

    private IEnumerator RespawnCo()
    {
        MovementController.instance.gameObject.SetActive(false);

        yield return new WaitForSeconds(waitToRespawn);

        MovementController.instance.gameObject.SetActive(true);


        MovementController.instance.transform.position = CheckPointController.instance.spawnPoint;

        spawnPointForCamera = CheckPointController.instance.spawnPoint;
        spawnPointForCamera.z = -10f;
        spawnPointForCamera.y = 1f;
        theCamera.GetComponent<CameraController>().FixGlitch(spawnPointForCamera.x, spawnPointForCamera.y, spawnPointForCamera.z);

        PlayerHealthController.instance.currentHealth = PlayerHealthController.instance.maxHealth;

        UIController.instance.UpdateHealthDisplay();
    }

}
