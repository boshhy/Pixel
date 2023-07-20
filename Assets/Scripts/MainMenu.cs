using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Used for the menu at the beginning of the game
public class MainMenu : MonoBehaviour
{
    // Used to reference the level loader
    public GameObject LevelTransition;

    // When player clicks on 'play' button load the next scene
    public void PlayGame()
    {
        LevelTransition.GetComponent<LevelLoader>().LoadNextLevel();
    }

    // Used to quit the game
    public void Quit()
    {
        Application.Quit();
    }

    // Used to Restart the game from end credits scene
    public void PlayAgain()
    {
        LevelTransition.GetComponent<LevelLoader>().LoadGameAgain();
    }

    // Used to Restart the game from pause menu
    public void BackToMenu()
    {
        LevelTransition.GetComponent<LevelLoader>().BackToMenu();
    }
}
