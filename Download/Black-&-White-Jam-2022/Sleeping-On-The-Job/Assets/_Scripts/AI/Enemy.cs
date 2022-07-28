using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameManager gm;
    public PlayerDeath pDeath;

    private void Start()
    {
        pDeath = FindObjectOfType<PlayerDeath>();
        gm = FindObjectOfType<GameManager>();
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            pDeath.playerKilled();
        }
    }
}
