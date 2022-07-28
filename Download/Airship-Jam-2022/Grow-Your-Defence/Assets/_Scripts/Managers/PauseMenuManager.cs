using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuManager : MonoBehaviour
{
    [Header("Scripts, Objects & Components:")]
    public GameObject pauseMenu;

    void Update()
    {
        // Check the escape key is pressed to pause/unpause the game
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pauseMenu.activeSelf != true)
            {
                // Pause the game
                pauseMenu.SetActive(true);
                PauseGame();
            }
        }
    }
    public void ExitGame()
    {
        Application.Quit();
    }

    public void CloseButton()
    {
        pauseMenu.SetActive(false);
        ResumeGame();
    }

    void PauseGame()
    {
        Time.timeScale = 0;
    }
    void ResumeGame()
    {
        Time.timeScale = 1;
    }

    // Volume
    public void SetEffectsVol(float effects)
    {
        Volume.instance.effectsVolume = effects;
    }
    public void SetMusicVol(float music)
    {
        Volume.instance.musicVolume = music;
    }
    public void ButtonClick()
    {
        AudioManager.instance.Play("Click");
    }
}
