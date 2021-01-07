using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Scripts.Rules;
using Scripts;

namespace Scripts.MainMenu
{
    /// <summary>
    /// MainMenu class is attached to the first canvas of the menu scene
    /// it is the supervisor of the whole main menu
    /// </summary>
    public class MainMenu : MonoBehaviour
    {
        /// <summary>
        /// size represents the size of the board choosen by the player
        /// </summary>
        private int size = Rules.UnityParams.defaultSize;

        /// <summary>
        /// neighbour represents the number of neighbour affected by the clic of the player
        /// </summary>
        private int neighbour = Rules.UnityParams.defaultNeighbour;

        /// <summary>
        /// difficulty represents the number of action to complete the game
        /// </summary>
        private int difficulty = Rules.UnityParams.defaultDifficulty;

        /// <summary>
        /// preview is an instance of Rules for the preview
        /// </summary>
        Rules.Rules preview;

        /// <summary>
        /// PlayGame function is called when player presses Start on the menu
        /// sets the size, neighbour, difficulty then load the game scene
        /// </summary>
        public void PlayGame()
        {
            PlayerPrefs.SetInt("size", size);
            PlayerPrefs.SetInt("neighbour", neighbour);
            PlayerPrefs.SetInt("difficulty", difficulty);
            PlayerPrefs.Save();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

        /// <summary>
        /// Start is called before the first frame update
        /// </summary>
        public void Start()
        {
            UpdatePreview();
        }

        /// <summary>
        /// QuitGame is called when the user presses Quit on the menu
        /// </summary>
        public void QuitGame()
        {
            Application.Quit();
        }

        /// <summary>
        /// SetSize is called when the size slider value is changed in the option menu
        /// </summary>
        /// <param name="slider"> slider float </param>
        public void SetSize(float slider)
        {
            int tmpSize = (int)(5 + slider * 5);
            if (tmpSize != size)
            {
                size = tmpSize;
                UpdatePreview();
            }
        }

        /// <summary>
        /// SetDifficulty is called when the difficullty is changed in the dropDown in the option menu
        /// </summary>
        /// <param name="dropDown"> int dropDown </param>
        public void SetDifficulty(int dropDown)
        {
            difficulty = dropDown + 1;
        }

        /// <summary>
        /// SetNieghbour is called when the neighbour slider value is changed in the option menu
        /// </summary>
        /// <param name="slider"> slider float </param>
        public void SetNeighbour(float slider)
        {
            int tmpNeighbour = (int)(slider * 4);
            if (tmpNeighbour != neighbour)
            {
                neighbour = tmpNeighbour;
                UpdatePreview();
            }
        }

        /// <summary>
        /// updatePeview sets the preview with the options selected by the player
        /// </summary>
        private void UpdatePreview()
        {
            Destroy(GameObject.Find(UnityParams.gameName));
            new GameObject(UnityParams.gameName);
            StartCoroutine(SpawnBoard());
        }

        /// <summary>
        /// SpawnBoard is a coroutine waiting 10ms after the destruction of the previous preview
        /// then it spawns the new preview with the new parameters
        /// </summary>
        /// <returns>None</returns>
        IEnumerator SpawnBoard()
        {
            yield return new WaitForSeconds(0.01f);
            preview = new Rules.Rules(size, neighbour);
            GameObject.Find("Camera").transform.position = new Vector3((float)(size) / 2, 10, (float)(size) / 2);
        }
    }
}