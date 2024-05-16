using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAudio : MonoBehaviour
{
    AudioSource aud;
    public AudioClip doorOpenClip;
    public AudioClip doorCloseClip;

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
