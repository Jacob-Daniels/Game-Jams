using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructable : MonoBehaviour
{
    [Header("Manager")]
    public GameManager gm;

    [Header("Dialogue")]
    public float Health;
    public ParticleSystem ps;
    public bool LoseIfDestroyed;

    [Header("Check Hit Status")]
    public DestructableConstraints destConstraints;
    public Rigidbody2D rb;
    public float maxHealth;

    private void Start()
    {
        gm = FindObjectOfType<GameManager>();
        
        destConstraints = GetComponent<DestructableConstraints>();
        rb = GetComponent<Rigidbody2D>();
        maxHealth = Health;
    }

    public void Burn()
    {
        if (ps == null)
        {
            ps = GetComponent<ParticleSystem>();
        }
        ps.Play();

        if (Health > 0)
        {
            //Health -= Time.deltaTime;
            Health -= 0.02f;
            destConstraints.checkHit();
        }
        else if(Health <= 0)
        {
            if (LoseIfDestroyed == false)
            {
                Destroy();
            }
            else
            {
                Destroy();
            }
        }
    }
    public void Destroy()
    {
        Destroy(gameObject);
    }
}
