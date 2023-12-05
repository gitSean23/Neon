using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public SaveSystem saveSystem;
    public void NewGame()
    {
        SceneManager.LoadSceneAsync("Level1-Intro");
        saveSystem.SaveData("new");
    }

    public void LoadGame()
    {
        if (PlayerPrefs.GetInt("CurrentLevel") == 1)
        {
            SceneManager.LoadSceneAsync("Level1-Intro");
        }

        if (PlayerPrefs.GetInt("CurrentLevel") == 2)
        {
            SceneManager.LoadSceneAsync("Level1-Cutscene");
        }
        if (PlayerPrefs.GetInt("CurrentLevel") == 3)
        {
            SceneManager.LoadSceneAsync("Level3-Cutscene");
        }
        if (PlayerPrefs.GetInt("CurrentLevel") == 4)
        {
            SceneManager.LoadSceneAsync("Level4-Cutscene");
        }

    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
