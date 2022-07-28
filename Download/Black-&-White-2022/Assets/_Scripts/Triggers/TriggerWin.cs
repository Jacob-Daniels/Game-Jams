using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerWin : MonoBehaviour
{
    public GameManager gm;
    public bool hit = false;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player") && hit == false)
        {
            hit = true;
            gm.WonGame();
        }
    }
}
