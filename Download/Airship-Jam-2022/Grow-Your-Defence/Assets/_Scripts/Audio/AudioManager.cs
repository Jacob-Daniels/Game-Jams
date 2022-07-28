using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using System;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
    public static AudioManager instance;

    void Awake()
    {
        // Only allow one instance of audio manager to exist
        if (instance == null)
        {
            instance = this;
        } else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);

        // Create audio clips from sounds array
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }
    void Start()
    {
        // Play background music
        Play("bgMusic");
    }

    public void Play(string name)   // Play audio source
    {
        // Find the sound in the sounds array with the same name
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.Log("No audio file called: " + name);
            return;
        }
        s.source.Play();
    }

    public void UpdateVolume(float effectsVolume, float musicVolume)
    {
        foreach (Sound s in sounds)
        {
            if (s.name == "bgMusic")
            {
                s.source.volume = musicVolume;
            } else
            {
                s.source.volume = effectsVolume;
            }
        }
    }
}
