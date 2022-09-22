using UnityEngine.Audio;
using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

    public static AudioManager instance;

    public void Awake()
    {
        // prevent sound from restarting on new scene
        if (instance == null)
        {
            instance = this;
        } else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
        

        foreach (Sound s in sounds)
        {
            // add audio to the gameobject and set variables to clip
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    void Start()
    {
        // play menu music
        Play("bgMusic");
    }

    public void Play(string name)
    {
        // find name of clip in array
        Sound s = Array.Find(sounds, sound => sound.name == name);
        // return if audio clip is not found
        if (s == null)
        {
            Debug.LogWarning("No such audio: " + name);
            return;
        }
        // play sound
        s.source.Play();
    }

    public void updateVolume(float effectsValue, float musicValue)
    {
        foreach (Sound s in sounds)
        {
            if (s.name == "bgMusic")
            {
                // change volume of background music
                s.source.volume = musicValue;
            } else
            {
                // change volume of all other sounds
                s.source.volume = effectsValue;
            }
            if (s.name == "RubbleHit")
            {
                s.source.volume = 0.01f;
            }
        }
    }
}
