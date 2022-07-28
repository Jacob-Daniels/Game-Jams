using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameVolume : MonoBehaviour
{
    public float effectsValue = 0.2f;
    public float musicValue = 0.1f;


    void Update()
    {
        // update the volume when changed
        FindObjectOfType<AudioManager>().updateVolume(effectsValue, musicValue);
    }

    public void effectsSlider(float val)
    {
        // get the value of the slider
        effectsValue = val;
    }

    public void musicSlider(float val)
    {
        // get the value of the slider
        musicValue = val;
    }
}
