using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveSystem : MonoBehaviour // Add static class for JSON
{
    //private static string savePath => $"{Application.persistentDataPath}/playerdata.json";
    // WHEN TO SAVE DATA:
    // - When player unlocks an ability
    // - when player completes a level

    // public static void SavePlayerData(PlayerData data)
    // {
    //     string json = JsonUtility.ToJson(data);
    //     File.WriteAllText(savePath, json);
    // }

    // public static PlayerData LoadPlayerData()
    // {
    //     if (File.Exists(savePath))
    //     {
    //         string json = File.ReadAllText(savePath);
    //         return JsonUtility.FromJson<PlayerData>(json);
    //     }

    //     return new PlayerData();
    // }
    public void SaveData(string data)
    {

        if (data == "new")
        {
            // Current Level
            PlayerPrefs.SetInt("CurrentLevel", 1);

            // Abilities
            PlayerPrefs.SetString("blackout", "false");
            PlayerPrefs.SetString("damageboost", "false");
            PlayerPrefs.SetString("stun", "false");
            PlayerPrefs.SetString("nanobytes", "false");
            PlayerPrefs.SetString("hologramdecoy", "false");
            PlayerPrefs.SetString("saveextendedenemystun", "false");
            PlayerPrefs.SetString("tenacity", "false");
            PlayerPrefs.SetString("knockback", "false");
            PlayerPrefs.SetString("flurryofpunches", "false");
            PlayerPrefs.SetString("dash", "false");

            // Load game button will be enabled
            PlayerPrefs.SetString("load", "true");
        }

        PlayerPrefs.SetString(data, "true");
        PlayerPrefs.Save();

    }

    public void LoadData()
    {
        // Gathers all the levels
        PlayerPrefs.GetInt("CurrentLevel");

        // Gathers all player abilities
        PlayerPrefs.GetString("human1");
        PlayerPrefs.GetString("human2");
        PlayerPrefs.GetString("ai1");
        PlayerPrefs.GetString("ai2");
        PlayerPrefs.GetString("ai3");
        PlayerPrefs.GetString("ai4");
        PlayerPrefs.GetString("nano1");
        PlayerPrefs.GetString("nano2");
        PlayerPrefs.GetString("nano3");
    }

}
