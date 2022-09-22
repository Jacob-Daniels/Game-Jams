using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOver : MonoBehaviour
{
    [Header("Scripts, Objects & Components:")]
    public TextMeshProUGUI wavesCompletedText;

    public void RestartGame()
    {
        SceneManager.LoadScene(0);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void ClickSound()
    {
        AudioManager.instance.Play("Click");
    }

    void Update()
    {
        // Set the waves completed text
        wavesCompletedText.text = "Total waves completed: " + DontDestroyManager.instance.waveCount.ToString();
    }

}
