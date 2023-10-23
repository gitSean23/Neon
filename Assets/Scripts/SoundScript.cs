using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundScript : MonoBehaviour
{

    [SerializeField] public AudioSource musicSrc;
    [SerializeField] public AudioSource sfxSource;
    [SerializeField] public AudioClip lightPunch;
    [SerializeField] public AudioClip mediumPunch;
    [SerializeField] public AudioClip lightWhoosh;
    [SerializeField] public AudioClip mediumWhoosh;
    [SerializeField] public AudioClip bgMusic;
    [SerializeField] public AudioClip blackout;
    [SerializeField] public AudioClip lightsOn;

    void Start()
    {
        musicSrc.clip = bgMusic;
    }

    public void playMusic()
    {
        musicSrc.Play();
        sfxSource.volume = 0.75f;
    }

    public void playSfx(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip);
    }

}
