using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Abilities : MonoBehaviour
{
    private Animator anim;

    public SaveSystem saveSystem;

    // FINISHERS
    public float zoomedFOV = 0.5f;
    public float zoomSpeed = 1f;

    public Camera cam;

    private float originalFOV = 5.6f;
    private Vector3 originalPosition;
    private bool isZooming = false;

    public SoundScript soundManager;

    bool canFinisher;

    bool canFlurry;
    Coroutine finisherCoroutine;
    Coroutine flurryCoroutine;

    // Start is called before the first frame update
    void Start()
    {
        canFinisher = true;
        canFlurry = true;
        soundManager = GameObject.FindGameObjectWithTag("Sound").GetComponent<SoundScript>();
        anim = GetComponent<Animator>();
        originalFOV = cam.orthographicSize;
        originalPosition = cam.transform.position;


        if (PlayerPrefs.GetString("damageboost") == "true")
        {
            GetComponent<FreeFlowCombatScript>().dmg = 20f;
        }

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Debug.Log("Ability 1 Activated!");
            StartFinisher();
        }

        if (Input.GetKeyDown(KeyCode.Alpha2) && PlayerPrefs.GetString("flurryofpunches") == "true")
        {
            if (canFlurry)
            {
                canFlurry = false;
                anim.SetBool("flurry", true);
                flurryCoroutine = StartCoroutine(FlurryCoroutine());
            }
        }
    }

    // HUMAN Abilities

    public void StartFinisher()
    {
        if (canFinisher)
        {
            anim.SetBool("attack5", true);
            canFinisher = false;
            soundManager.playSfx(soundManager.finisherWhoosh);
            // Smoothly interpolate the camera's position and FOV
            cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, zoomedFOV, zoomSpeed);
            cam.transform.position = Vector3.Lerp(cam.transform.position, new Vector3(transform.position.x, transform.position.y, originalPosition.z), zoomSpeed);
            cam.orthographicSize = 4f;
            Time.timeScale = 0.5f;
            Time.fixedDeltaTime = 0.02f * Time.timeScale;
            finisherCoroutine = StartCoroutine(FinisherCoroutine());
        }
    }

    public void PlayFinisherSound()
    {
        soundManager.playSfx(soundManager.finisherPunch);
    }

    public void RestoreTime()
    {
        Time.timeScale = 1.0f;
        Time.fixedDeltaTime = 0.02f;
    }

    public void EndFinisher()
    {
        // Smoothly interpolate back to the original position and FOV
        cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, 5.6f, zoomSpeed);
        cam.transform.position = Vector3.Lerp(cam.transform.position, originalPosition, zoomSpeed);
        cam.orthographicSize = 5.6f;
    }
    public void EndHuman1()
    {
        anim.SetBool("attack5", false);
    }

    IEnumerator FinisherCoroutine()
    {
        yield return new WaitForSeconds(10f);
        canFinisher = true;
    }


    // Flurry
    IEnumerator FlurryCoroutine()
    {
        yield return new WaitForSeconds(10f);
        canFlurry = true;
    }

    public void EndFlurry()
    {
        anim.SetBool("flurry", false);
    }


}
