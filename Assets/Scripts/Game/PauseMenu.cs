using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Scripts.Game
{
    /// <summary>
    /// PauseMenu class is the supervisor of the pause menu screen
    /// </summary>
    public class PauseMenu : MonoBehaviour
    {
        /// <summary>
        /// All thoses GameObject are binded to their corresponding object in the editor hierarchy
        /// </summary>
        public GameObject lightOutGame, pauseMenu, congratText, failText,
            pauseText, retryButton, resumeButton;

        /// <summary>
        /// isPaused bool is an image of the pause of the game
        /// </summary>
        public static bool isPaused = false;

        /// <summary>
        /// finished bool is an image of the completion of the game
        /// </summary>
        public static bool finished = false;

        /// <summary>
        /// failed bool is an image of the fail of the game
        /// </summary>
        public static bool failed = false;

        /// <summary>
        /// Update is called once per frame
        /// supervisor of the end, fail, pause screen
        /// </summary>
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape) && !finished)
            {
                if (isPaused)
                {
                    Resume();
                }
                else
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

        /// <summary>
        /// Resume is called when he resume button is pressed in the pause menu
        /// </summary>
        public void Resume()
        {
            pauseMenu.SetActive(false);
            lightOutGame.SetActive(true);

            isPaused = false;
        }

        /// <summary>
        /// Pause is called when the pause button is pressed in the game scene
        /// </summary>
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

        /// <summary>
        /// QuitGame is called when the quit button is pressed in the pause menu
        /// </summary>
        public void QuitGame()
        {
            Application.Quit();
        }

        /// <summary>
        /// GoToMenu is called when the menu button is pressed in the pause menu
        /// </summary>
        public void GoToMenu()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        }

        /// <summary>
        /// Finished function shows the completion screen
        /// </summary>
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

        /// <summary>
        /// Failed function shows the failed screen
        /// </summary>
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
}
