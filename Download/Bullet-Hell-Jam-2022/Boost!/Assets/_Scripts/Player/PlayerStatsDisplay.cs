using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerStatsDisplay : MonoBehaviour
{
    [Header("Player")]
    public Player player;

    [Header("Display Text")]
    public TextMeshProUGUI Lives;
    public TextMeshProUGUI Points;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Lives.text = "Health: " + player.health;
        Points.text = "Points: " + player.Points;
    }
}
