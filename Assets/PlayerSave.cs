using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSave : MonoBehaviour
{

    public SaveSystem saveSystem;
    // Start is called before the first frame update
    void Start()
    {
        saveSystem.LoadData();
    }

    // Update is called once per frame
    void Update()
    {
        // when player completes the level, save their data
        if (Input.GetKeyDown(KeyCode.P))
        {
            Debug.Log("Nano 1 ability unlocked and saved!");
            saveSystem.SaveData("nano1");
            saveSystem.LoadData();

        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("MAKING NEW GAME!");
            saveSystem.SaveData("new");
            saveSystem.LoadData();
        }
    }
}
