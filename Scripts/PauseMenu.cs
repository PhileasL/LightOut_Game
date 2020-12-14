using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject lightOutGame, pauseMenu, congratText, failText, pauseText;

    public static bool isPaused = false;

    public static bool finished = false;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !finished)
        {
            if (isPaused)
            {
                Resume();
            } else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        lightOutGame.SetActive(true);
        isPaused = false;
    }

    public void Pause()
    {
        congratText.SetActive(false);
        failText.SetActive(false);
        pauseText.SetActive(true);

        pauseMenu.SetActive(true);
        lightOutGame.SetActive(false);

        isPaused = true;
    }

    public void QuitGame()
    {
        Debug.Log("quit pressed");
        Application.Quit();
    }

    public void GoToMenu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
}
