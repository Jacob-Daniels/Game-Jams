using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public void LoseGame(string popupmessage)
    {
        Debug.Log("Lost Game, Reason: " + popupmessage);
        // reload the current scene if hit
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void nextLevel()
    {
        // load to next scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void WonGame()
    {
        Debug.Log("Won Game!");
    }
}
