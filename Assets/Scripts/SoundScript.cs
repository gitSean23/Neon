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

    void Start()
    {
        musicSrc.clip = bgMusic;
        musicSrc.Play();
        sfxSource.volume = 0.9f;
    }

    public void playSfx(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip);
    }

}
