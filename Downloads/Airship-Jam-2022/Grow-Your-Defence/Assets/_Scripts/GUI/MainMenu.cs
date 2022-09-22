using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartGame()
    {
        // Load the next scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void ButtonClick()
    {
        AudioManager.instance.Play("Click");
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void SetEffectsVol(float effects)
    {
        Volume.instance.effectsVolume = effects;
    }
    public void SetMusicVol(float music)
    {
        Volume.instance.musicVolume = music;
    }
}
