using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject lightOutGame;

    public GameObject pauseMenu;

    public static bool isPaused = false;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
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
