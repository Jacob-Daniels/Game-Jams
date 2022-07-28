using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGM : MonoBehaviour
{
    public static BGM instance = null;
    public new AudioSource audio;
    public AudioClip MainMenu;
    public AudioClip Game;

    void Start()
    {
        audio = GetComponent<AudioSource>();
        audio.clip = MainMenu;
        audio.Play();
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            return;
        }
        if (instance == this) return;
        Destroy(gameObject);
    }

    public void OnLevelWasLoaded(int level)
    {
        if(level == 0)
        {
            audio.clip = MainMenu;
            audio.Play();
        }
        if(level == 1)
        {
            audio.clip = Game;
            audio.Play();
        }
    }
}
