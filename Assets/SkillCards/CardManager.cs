using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class CardManager : MonoBehaviour
{
    public GameObject cards; //Array of card GameObjects

    private string level1Cutscene = "Level1-Cutscene";

    private string level3Cutscene = "Level3-Cutscene";
    private string level4 = "Level4";
    private string level6 = "Level 6";
    SaveSystem playerManager;

    void Start()
    {
        cards.SetActive(false);
    }

    // LEVEL 1 Part 2 SKILLS

    public void SaveBlackout()
    {
        Debug.Log("black saved!");
        PlayerPrefs.SetString("blackout", "true");
        PlayerPrefs.Save();
        cards.SetActive(false);
        SceneManager.LoadScene(level1Cutscene);
    }

    public void SaveDamageBoost()
    {
        Debug.Log("damage boost saved!");
        PlayerPrefs.SetString("damageboost", "true");
        PlayerPrefs.Save();
        cards.SetActive(false);
        SceneManager.LoadScene(level1Cutscene);
    }

    // LEVEL 3 Skills

    public void SaveStun()
    {
        Debug.Log("Stun saved!");
        PlayerPrefs.SetString("stun", "true");
        PlayerPrefs.Save();
        cards.SetActive(false);
        SceneManager.LoadScene(level1Cutscene);
    }


    public void SaveNanobytes()
    {
        Debug.Log("nanobytes saved!");
        PlayerPrefs.SetString("nanobytes", "true");
        PlayerPrefs.Save();
        cards.SetActive(false);
        SceneManager.LoadScene(level3Cutscene);
    }


    public void SaveHologramDecoy()
    {
        Debug.Log("holo decoy saved!");
        PlayerPrefs.SetString("hologramdecoy", "true");
        PlayerPrefs.Save();
        cards.SetActive(false);
        SceneManager.LoadScene(level1Cutscene);
    }


    public void SaveExtendedEnemyStun()
    {
        Debug.Log("ext stun saved!");
        PlayerPrefs.SetString("saveextendedenemystun", "true");
        PlayerPrefs.Save();
        cards.SetActive(false);
        SceneManager.LoadScene(level1Cutscene);
    }


    public void SaveTenacity()
    {
        Debug.Log("tenacity saved!");
        PlayerPrefs.SetString("tenacity", "true");
        PlayerPrefs.Save();
        cards.SetActive(false);
        SceneManager.LoadScene(level1Cutscene);

    }


    public void SaveKnockback()
    {
        Debug.Log("knockback saved!");
        PlayerPrefs.SetString("knockback", "true");
        PlayerPrefs.Save();
        cards.SetActive(false);
        SceneManager.LoadScene(level1Cutscene);
    }

    public void SaveFlurryofPunches()
    {
        Debug.Log("flurry saved!");
        PlayerPrefs.SetString("flurryofpunches", "true");
        PlayerPrefs.Save();
        cards.SetActive(false);
        SceneManager.LoadScene(level3Cutscene);
    }

    public void SaveDash()
    {
        Debug.Log("dash saved!");
        PlayerPrefs.SetString("dash", "true");
        PlayerPrefs.Save();
        cards.SetActive(false);
        SceneManager.LoadScene(level1Cutscene);
    }

}
