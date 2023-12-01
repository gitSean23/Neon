using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CardSelection : MonoBehaviour
{

    public GameObject cards; //Array of card GameObjects
    SaveSystem playerManager;
    





    void Start()
    {
        
    }

    public void SaveStun()
    {
        PlayerPrefs.SetString("stun", "true");
        PlayerPrefs.Save();
        cards.SetActive(false);
    }

    public void SaveBlackout()
    {
        PlayerPrefs.SetString("blackout", "true");
        PlayerPrefs.Save();
        cards.SetActive(false);
    }



public void SaveNanobytes()
    {
        PlayerPrefs.SetString("nanobytes", "true");
        PlayerPrefs.Save();
        cards.SetActive(false);
    }


public void SaveHologramDecoy()
    {
        PlayerPrefs.SetString("hologramdecoy", "true");
        PlayerPrefs.Save();
        cards.SetActive(false);
    }


public void SaveExtendedEnemyStun()
    {
        PlayerPrefs.SetString("saveextendedenemystun", "true");
        PlayerPrefs.Save();
        cards.SetActive(false);
    }


public void SaveTenacity()
    {
        PlayerPrefs.SetString("tenacity", "true");
        PlayerPrefs.Save();
        cards.SetActive(false);

    }


public void SaveKnockback()
    {
        PlayerPrefs.SetString("knockback", "true");
        PlayerPrefs.Save();
        cards.SetActive(false);
    }

public void SaveFlurryofPunches()
    {
        PlayerPrefs.SetString("flurryofpunches", "true");
        PlayerPrefs.Save();
        cards.SetActive(false);
    }

    public void SaveDash()
    {
        PlayerPrefs.SetString("dash", "true");
        PlayerPrefs.Save();
        cards.SetActive(false);
    }

    public void SaveDamageBoost()
    {
        PlayerPrefs.SetString("damageboost", "true");
        PlayerPrefs.Save();
        cards.SetActive(false);
    }




    
        

}
