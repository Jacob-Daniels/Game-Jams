using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerManager : MonoBehaviour
{
    [Header("Scripts, Objects & Components:")]
    public static PlayerManager instance;
    public TextMeshProUGUI healthText;

    [Header("Health Variables:")]
    public int health = 100;

    [Header("Total Score:")]
    public int waveCount;

    void Awake()
    {
        // Only allow a single instance of this class to occur
        if (instance != null)
        {
            Debug.Log("More than one PlayerManager in the scene");
        }
        instance = this;
    }

    private void Start()
    {
        UpdateUIText();
    }

    public void RemoveHealth(int damage)    // Reduce health and display on UI
    {
        // Change Health stats
        health -= damage;
        if (health <= 0)
        {
            // Play Audio
            AudioManager.instance.Play("GameOver");
            // Load next scene when all lives are lost
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        } else
        {
            // Play Audio
            AudioManager.instance.Play("LifeLost");
        }
        UpdateUIText();
    }

    void UpdateUIText() // Update UI Text
    {
        healthText.text = health.ToString();
    }

}
