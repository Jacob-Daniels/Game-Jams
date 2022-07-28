using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class Sound
{
    // class to deal with the variables when audio clip is created
    [Header("Audio Clip:")]
    public string name;

    public AudioClip clip;

    [Range(0f, 1f)]
    public float volume = 1;
    [Range (0.1f, 3f)]
    public float pitch = 1;

    public bool loop;

    [HideInInspector]
    public AudioSource source;

}
