using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

public class Settings : MonoBehaviour
{
    public AudioMixer MASTERaudiomixer; // window -> audio -> audiomixer -> click on 'Master' -> right click attenuation in inspector -> expose -> go to exposed parameters in audiomixer view -> rename "volume"
    public TMP_Dropdown resdropdown;
    Resolution[] resoultions;

    private void Start() // dropdown
    {
        resoultions = Screen.resolutions;

        resdropdown.ClearOptions();

        List<string> options = new List<string>();

        int currentresindex = 0;

        for (int i = 0; i < resoultions.Length; i++)
        {
            string option = resoultions[i].width + " x " + resoultions[i].height;
            options.Add(option);

            if (resoultions[i].width == Screen.currentResolution.width && resoultions[i].height == Screen.currentResolution.height)
            {
                currentresindex = i;
            }
        }

        resdropdown.AddOptions(options);
        resdropdown.value = currentresindex;
        resdropdown.RefreshShownValue();
    }

    public void SetResolution(int resolutionindex) //dropdown
    {
        Resolution resolution = resoultions[resolutionindex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void SetMasterVolume(float volume) //slider
    {
        MASTERaudiomixer.SetFloat("Master", volume);
    }

    public void SetSFXVolume(float volume) //slider
    {
        MASTERaudiomixer.SetFloat("SFX", volume);
    }

    public void SetBGMVolume(float volume) //slider
    {
        MASTERaudiomixer.SetFloat("BGM", volume);
    }

    public void SetQuality(int qualityindex) //dropdown
    {
        QualitySettings.SetQualityLevel(qualityindex);
    }

    public void SetFullscreen(bool isFullscreen) //toggle
    {
        Screen.fullScreen = isFullscreen;
    }
}
