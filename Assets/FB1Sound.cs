using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FB1Sound : MonoBehaviour
{
    public AudioSource fb1Source;
    public AudioClip fb1Clip;

    void PlayAttack()
    {
        fb1Source.PlayOneShot(fb1Clip);
    }
}
