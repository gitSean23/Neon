using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public SaveSystem saveSystem;
    public void NewGame()
    {
        SceneManager.LoadSceneAsync("Level1-1");
        saveSystem.SaveData("new");
    }

    public void LoadGame()
    {
        SceneManager.LoadSceneAsync("Level1-1");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
