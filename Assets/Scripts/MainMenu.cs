using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public GameObject LevelTransition;

    public void PlayGame()
    {
        LevelTransition.GetComponent<LevelLoader>().LoadNextLevel();
    }

    public void Options()
    {

    }

    public void Quit()
    {
        Application.Quit();
    }

    public void PlayAgain()
    {
        LevelTransition.GetComponent<LevelLoader>().LoadGameAgain();
    }

    public void BackToMenu()
    {
        LevelTransition.GetComponent<LevelLoader>().BackToMenu();
    }
}
