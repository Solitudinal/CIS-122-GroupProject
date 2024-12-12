// Written by Philip Jacobson
// 11/8/2024

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioSource musicPlayer;
    [SerializeField] AudioSource sfxPlayer;

    public static AudioManager Instance { get; private set; }

    // Instantiate the audio manager in the awake function
    private void Awake()
    {
        Instance = this; 
    }

    // Function that plays audio clip that is passed as a parameter
    public void PlayMusic(AudioClip clip, bool loop = true)
    {
        if (clip == null) return; // If there is no music, don't attempt to play anything

        musicPlayer.clip = clip;
        musicPlayer.loop = loop;
        musicPlayer.Play();
    }
}
