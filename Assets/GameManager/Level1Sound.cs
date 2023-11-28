using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1Sound : MonoBehaviour
{

    [SerializeField] public AudioSource envSrc;
    [SerializeField] public AudioClip envSfx;
    [SerializeField] public AudioSource voiceSrc;
    [SerializeField] public AudioClip voiceLines;

    void Start()
    {
        voiceSrc.clip = voiceLines;
        voiceSrc.Play();

        envSrc.clip = envSfx;
        envSrc.Play();
    }


}
