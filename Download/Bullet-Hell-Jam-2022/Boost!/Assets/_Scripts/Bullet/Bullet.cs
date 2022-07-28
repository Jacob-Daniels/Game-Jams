using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Player player;
    public Rigidbody2D rb;
    public float Speed;
    public GameObject BulletParent;
    public EnemySpawnSystem ess;

    public bool isPlayerFired = false;

    [Header("Sprite")]
    public SpriteRenderer spriteRenderer;
    public Sprite playerBullet;
    public Sprite enemyBullet;

    public void Awake()
    {
        BulletParent = GameObject.FindGameObjectWithTag("BulletParent");
        GameObject e = GameObject.FindGameObjectWithTag("EnemySpawnSystem");
        ess = e.GetComponent<EnemySpawnSystem>();
        GameObject g = GameObject.FindGameObjectWithTag("Player");
        player = g.GetComponent<Player>();
    }

    public void Update()
    {
        // check bullet direction depenging if player or enemy fired
        if (isPlayerFired)
        {
            // Direction & Speed
            rb.velocity = (transform.up * Speed);
            // Change sprite
            spriteRenderer.sprite = playerBullet;
        }
        else
        {
            // Direction & Speed
            rb.velocity = (-transform.up * Speed);
            // Change sprite
            spriteRenderer.sprite = enemyBullet;
        }

        if (GetComponent<Renderer>().isVisible == false)
        {
            Destroy(gameObject);
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        /*
        if(collision.CompareTag("Bullet"))
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
        */

        // ENEMY FIRED //

        if (collision.CompareTag("Player") && isPlayerFired == false)
        {
            collision.GetComponent<Player>().Hit(1);
            Destroy(gameObject);
            return;
        }

        // PLAYER FIRED //

        if (collision.CompareTag("DistancedEnemy") && isPlayerFired == true)
        {
            collision.GetComponent<DistancedEnemy>().Hit(player.FireRate + 1);
            if (collision.GetComponent<DistancedEnemy>().health <= 0)
            {
                StartCoroutine(ess.RemoveObj(ess.DistanceEnemy, collision.gameObject));
            }
            return;
        }

        if (collision.CompareTag("ChaserEnemy") && isPlayerFired == true)
        {
            collision.GetComponent<ChaserEnemy>().Hit(1);
            if (collision.GetComponent<ChaserEnemy>().health <= 0)
            {
                StartCoroutine(ess.RemoveObj(ess.ChaserEnemy, collision.gameObject));
            }
            return;
        }

        if (collision.CompareTag("SpinEnemy") && isPlayerFired == true)
        {
            collision.GetComponent<SpinEnemy>().Hit(player.FireRate + 1);
            if (collision.GetComponent<SpinEnemy>().health <= 0)
            {
                StartCoroutine(ess.RemoveObj(ess.SpinEnemy, collision.gameObject));
            }
            return;
        }

        if (collision.CompareTag("Boss") && isPlayerFired == true)
        {
            collision.GetComponent<Boss>().Hit(player.FireRate + 1);
            if (collision.GetComponent<Boss>().health <= 0)
            {
                StartCoroutine(ess.RemoveObj(ess.BossEnemy, collision.gameObject));
            }
            return;
        }
    }
}
