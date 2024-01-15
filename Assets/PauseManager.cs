using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    public GameObject pauseMenu;
    public void Resume()
    {
        Time.timeScale = 1f;
        pauseMenu.SetActive(false);
    }

    public void Controls()
    {
        Debug.Log("Controls menu");
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

}
