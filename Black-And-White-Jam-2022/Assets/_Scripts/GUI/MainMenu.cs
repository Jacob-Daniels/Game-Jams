using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public void startGame()
    {
        buttonSound();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void mainMenu()
    {
        buttonSound();
        SceneManager.LoadScene(0);
    }

    public void exitGame()
    {
        buttonSound();
        Application.Quit();
    }

    public void buttonSound()
    {
        // grab the audio clip from the manager
        FindObjectOfType<AudioManager>().Play("ButtonClick");
    }
}
