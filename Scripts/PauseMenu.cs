using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject lightOutGame, pauseMenu, congratText, failText, 
        pauseText, retryButton, resumeButton;

    public static bool isPaused = false;

    public static bool finished = false;

    public static bool failed = false;

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
        if (finished)
        {
            Finished();
        }
        if (failed)
        {
            Failed();
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

        retryButton.SetActive(true);
        resumeButton.SetActive(true);

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

    public void Finished()
    {
        congratText.SetActive(true);
        failText.SetActive(false);
        pauseText.SetActive(false);

        retryButton.SetActive(false);
        resumeButton.SetActive(false);

        pauseMenu.SetActive(true);
        lightOutGame.SetActive(false);
    }

    public void Failed()
    {
        congratText.SetActive(false);
        failText.SetActive(true);
        pauseText.SetActive(false);

        retryButton.SetActive(true);
        resumeButton.SetActive(false);

        pauseMenu.SetActive(true);
        lightOutGame.SetActive(false);
    }
}
