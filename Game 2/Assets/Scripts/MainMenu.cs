using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Starts the next scene in the build order
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    // Quits the game
    public void QuitGame()
    {
        Debug.Log("You Quit!");
        Application.Quit();
    }

    // Restarts the current scene
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // Returns to the Main Menu (assumes Main Menu is scene index 0)
    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}