using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]
public class Sound
{
    [Header("Audio Clip Attributes:")]
    [HideInInspector]
    public AudioSource source;

    public string name;

    public AudioClip clip;

    [Range(0f,1f)]
    public float volume = 0.5f;
    [Range(0.1f, 3f)]
    public float pitch = 0.5f;

    public bool loop;
}
