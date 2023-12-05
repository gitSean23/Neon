using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelComplete : MonoBehaviour
{
    public string overworldSceneName = "MainMenu"; // Name of the overworld scene
    public GameObject cards;

    public int currentLevel;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Collider Trigger is nominal :)");

        if (other.gameObject.CompareTag("Player")) // Make sure the colliding object is the player
        {
            if (AreAllEnemiesDefeated())
            {
                //cards = GameObject.FindGameObjectWithTag("CardSelection");
                if (cards == null)
                {
                    SceneManager.LoadScene(overworldSceneName); // Load the overworld scene
                }

                else
                {
                    cards.SetActive(true);
                }

                PlayerPrefs.SetInt("CurrentLevel", currentLevel);

            }

            else
            {
                Debug.Log("Can't send you because you need to DEFEAT MORE ENEMIES!");
            }
        }
    }

    private bool AreAllEnemiesDefeated()
    {
        GameObject[] enemyManagers = GameObject.FindGameObjectsWithTag("EnemyManager"); // Find all objects tagged with EnemyManage

        foreach (var managerObj in enemyManagers)
        {
            EnemyStateMachine manager = managerObj.GetComponent<EnemyStateMachine>();
            if (manager != null && manager.GetEnemyCount() > 0) // Assuming GetEnemyCount() is a method that returns the number of enemies left
            {
                Debug.Log("Not all enemies defeated yet!");
                return false; // If any enemy manager still has enemies left, return false
            }

        }

        Debug.Log("Sending player to MAIN MENU!");
        return true; // All enemies are defeated
    }
}

