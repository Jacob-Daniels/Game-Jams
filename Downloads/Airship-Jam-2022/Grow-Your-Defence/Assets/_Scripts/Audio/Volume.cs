using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Volume : MonoBehaviour
{
    [Header("Volume Controls:")]
    public float effectsVolume = 0.5f;
    public float musicVolume = 0.5f;

    public static Volume instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        } else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        AudioManager.instance.UpdateVolume(effectsVolume, musicVolume);
    }

    // Use sliders to set volume
    public void EffectsSlider(float value)
    {
        effectsVolume = value;
    }
    public void MusicSlider(float value)
    {
        musicVolume = value;
    }
}
