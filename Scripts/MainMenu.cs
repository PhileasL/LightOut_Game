using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Scripts.Rules;
using Scripts;

public class MainMenu : MonoBehaviour
{
    int size = 6;
    int neighbour = 2;

    public void PlayGame()
    {
        PlayerPrefs.SetInt("size", size);
        PlayerPrefs.SetInt("neighbour", neighbour);
        PlayerPrefs.Save();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        Debug.Log("quit pressed");
        Application.Quit();
    }

    public void SetSize(float slider)
    {
        int tmpSize = (int)( 5 + slider * 5 );
        if (tmpSize != size)
        {
            size = tmpSize;
            Debug.Log(tmpSize);
        }
    }

    public void SetNeighbour(float slider)
    {
        int tmpNeighbour = (int)(slider * 4);
        if (tmpNeighbour != neighbour)
        {
            neighbour = tmpNeighbour;
            Debug.Log(tmpNeighbour);
        }
    }
}
