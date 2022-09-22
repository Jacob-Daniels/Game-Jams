using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
    public GameManager gm;
    [Header("Animation")]
    public new Animator animaiton;

    [Header("Player Death")]
    public Player player;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
        gm = FindObjectOfType<GameManager>();
        animaiton = GetComponent<Animator>();
    }


    public void playerKilled()
    {
        FindObjectOfType<AudioManager>().Play("PlayerDeath");
        animaiton.SetBool("IsHit", true);
        player.deathMovement();
        StartCoroutine(waitRestart());
    }

    IEnumerator waitRestart()
    {
        yield return new WaitForSeconds(2);
        gm.LoseGame("Hit Enemy!");
    }
}
