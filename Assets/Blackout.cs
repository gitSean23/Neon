using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Blackout : MonoBehaviour
{
    public GameObject floor1Lights; // Reference to the "Floor 1" GameObject
    public GameObject floor2Lights; // Reference to the "Floor 2" GameObject

    public GameObject floor3Lights; // Reference to the "Floor 1" GameObject
    public GameObject floor4Lights; // Reference to the "Floor 2" GameObject

    public GameObject floor5Lights; // Reference to the "Floor 1" GameObject
    public GameObject floor6Lights; // Reference to the "Floor 2" GameObject

    public SoundScript sound;

    void Start()
    {
        sound = GameObject.FindGameObjectWithTag("Sound").GetComponent<SoundScript>();
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            TurnOffLights();
        }

        if (Input.GetKeyDown(KeyCode.N))
        {
            TurnOnLights();
        }
    }
    public void TurnOffLights()
    {
        sound.playSfx(sound.blackout);
        ToggleLights(floor1Lights, false); // Turn off lights on Floor 1
        ToggleLights(floor2Lights, false); // Turn off lights on Floor 2
        ToggleLights(floor3Lights, false); // Turn off lights on Floor 1
        ToggleLights(floor4Lights, false); // Turn off lights on Floor 2
        ToggleLights(floor5Lights, false); // Turn off lights on Floor 1
        ToggleLights(floor6Lights, false); // Turn off lights on Floor 2
    }

    public void TurnOnLights()
    {
        sound.playSfx(sound.lightsOn);
        ToggleLights(floor1Lights, true); // Turn on lights on Floor 1
        ToggleLights(floor2Lights, true); // Turn on lights on Floor 2
        ToggleLights(floor3Lights, true); // Turn on lights on Floor 1
        ToggleLights(floor4Lights, true); // Turn on lights on Floor 2
        ToggleLights(floor5Lights, true); // Turn on lights on Floor 1
        ToggleLights(floor6Lights, true); // Turn on lights on Floor 2
    }

    private void ToggleLights(GameObject floorLights, bool enableLights)
    {
        if (floorLights != null)
        {
            foreach (Transform child in floorLights.transform)
            {
                UnityEngine.Rendering.Universal.Light2D light2D = child.GetComponent<UnityEngine.Rendering.Universal.Light2D>();
                if (light2D != null)
                {
                    light2D.enabled = enableLights;
                }
            }
        }
    }
}
