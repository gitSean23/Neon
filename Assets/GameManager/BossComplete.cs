using UnityEngine;
using UnityEngine.SceneManagement;

public class BossComplete : MonoBehaviour
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
        GameObject boss = GameObject.FindGameObjectWithTag("Boss"); // Find all objects tagged with EnemyManage

        if (boss != null)
        {
            Debug.Log("You haven't defeated the BOSS yet!");
            return false;
        }


        Debug.Log("Sending player to MAIN MENU!");
        return true; // All enemies are defeated
    }
}

