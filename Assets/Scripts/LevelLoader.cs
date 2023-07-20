using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Used to transition from scene to scene
public class LevelLoader : MonoBehaviour
{
    // Used to animate the transition
    public Animator transition;

    // Used to adjust transition time from scene to scene
    public float transitionTime = 1f;

    // Used to call Coroutine to go to next scene with transition time
    public void LoadNextLevel()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }

    // Used to restart game from the beginning
    public void LoadGameAgain()
    {
        StartCoroutine(LoadLevel(0));
    }

    // Used to restart game from the beginning
    public void BackToMenu()
    {
        StartCoroutine(LoadLevel(0));
    }

    // Used to wait 'transitionTime' and then load the next scene
    IEnumerator LoadLevel(int levelIndex)
    {
        // Change animation to start transition
        transition.SetTrigger("Start");
        
        // Wait 'transitionTime' seconds
        yield return new WaitForSeconds(transitionTime);

        // Load the scene that was passed in
        SceneManager.LoadScene(levelIndex);
    }
}
