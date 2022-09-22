using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyManager : MonoBehaviour
{
    public static DontDestroyManager instance;
    public int waveCount;


    private void Awake()
    {
        // Create a single instance of the script to be called to
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        // Prevent more than 1 instance existing
        /*
        GameObject[] objects = GameObject.FindGameObjectsWithTag("DontDestroy");
        if (objects.Length > 1)
        {
            Destroy(this.gameObject);
        }
        */
        // Dont destroy on next scene
        DontDestroyOnLoad(gameObject);
    }
}
