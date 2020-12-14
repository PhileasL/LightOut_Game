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
    int difficulty = 1;
    Rules preview;

    public void PlayGame()
    {
        PlayerPrefs.SetInt("size", size);
        PlayerPrefs.SetInt("neighbour", neighbour);
        PlayerPrefs.SetInt("difficulty", difficulty);
        PlayerPrefs.Save();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Start()
    {
        UpdatePreview();
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
            UpdatePreview();
        }
    }


    public void SetDifficulty(int dropDown)
    {
        difficulty = dropDown + 1;
    }

    public void SetNeighbour(float slider)
    {
        int tmpNeighbour = (int)(slider * 4);
        if (tmpNeighbour != neighbour)
        {
            neighbour = tmpNeighbour;
            UpdatePreview();
        }
    }

    private void UpdatePreview()
    {
        Destroy(GameObject.Find(UnityParams.gameName));
        new GameObject(UnityParams.gameName);
        StartCoroutine(SpawnBoard());
    }

    IEnumerator SpawnBoard()
    {
        yield return new WaitForSeconds(0.01f);
        preview = new Rules(size, neighbour);
        GameObject.Find("Camera").transform.position = new Vector3((float)(size) / 2, 10, (float)(size) / 2);
    }
}
