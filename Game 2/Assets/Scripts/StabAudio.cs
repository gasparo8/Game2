using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StabAudio : MonoBehaviour
{
    AudioSource aud;
    public AudioClip stab1;
    public AudioClip stab2;
    public AudioClip stab3;

    void Start()
    {
        aud = GetComponent<AudioSource>();
    }

    public void PlayAudioClip(AudioClip clip)
    {
        aud.clip = clip;
        aud.Play();
    }
}
