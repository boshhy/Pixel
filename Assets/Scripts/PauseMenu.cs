using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Used to control the pause menu
public class PauseMenu : MonoBehaviour
{
    // Get a reference to the pause menu object    
    public GameObject pauseMenu;

    // Public Static bool so other scripts can reference and make sure game is not paused
    public static bool isPaused;

    // Used to transition from scene to scene
    public GameObject LevelTransition;

    // Start is called before the first frame update
    void Start()
    {
        // Pause menu shoul not be active when loading a scene
        pauseMenu.SetActive(false);   
    }

    // Update is called once per frame
    void Update()
    {
        // Check to see if player has pressed the 'escape' key
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // If game is paused, then resume the game
            if (isPaused)
            {
                ResumeGame();
            }
            // Else game is playing so pause the game
            else
            {
                PauseGame();
            }
        }
    }

    // Used to pause the game
    public void PauseGame()
    {
        // Play pause SFX
        AudioManager.instance.PlaySFX(17);

        // Activate the pause menu
        pauseMenu.SetActive(true);

        // Set timescale to zero so nothing gets updated
        Time.timeScale = 0f;

        // Game is now paused
        isPaused = true;
    }

    // Used to resume the game
    public void ResumeGame()
    {
        // Play resume SFX
        AudioManager.instance.PlaySFX(17);

        // Deactivate the pause menu
        pauseMenu.SetActive(false);

        // Set timescale back to 1 so we update normally
        Time.timeScale = 1f;

        // Game is now unpaused 
        isPaused = false;
    }

    // Used to go back to 'Main Menu' (start of game)
    public void BackToMenu()
    {
        // Set timescale back to 1 so we update normally 
        Time.timeScale = 1f;

        // Game is now unpaused
        isPaused = false;

        // Call level transitioner to take player back to 'Main Menu'
        LevelTransition.GetComponent<LevelLoader>().LoadGameAgain();
    }

    // Used to quit the game
    public void Quit()
    {
        Application.Quit();
    }
}
