using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
                pauseMenu.SetActive(false);
                lightOutGame.SetActive(true);
            } else
            {
                pauseMenu.SetActive(true);
                lightOutGame.SetActive(false);
            }
            isPaused = !isPaused;
        }
    }
}
