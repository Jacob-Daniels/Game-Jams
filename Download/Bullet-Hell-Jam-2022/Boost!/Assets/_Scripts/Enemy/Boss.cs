using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    [Header("Movement")]
    public float Speed;
    public float MinDistance;
    public float MaxDistance;
    public float CurrentDistance;

    [Header("Components")]
    public float timer;
    public float health;

    [Header("Attack Speed")]
    public float rotationSpeed = 20f;
    public float angleSpeed = 0.1f;
    public float RateOfFire;
    public float BulletSpeed;

    [Header("Bullet Components")]
    public SpiralBullet bullet;
    private float OGRateOfFire;
    public float angle;

    [Header("Player")]
    public Player player;

    [Header("Enemy Spawn System")]
    public EnemySpawnSystem ess;

    [Header("Attack Area")]
    public GameObject AttackArea;
    Vector3 bossPos;

    public void Start()
    {
        AttackArea = GameObject.Find("AttackArea");
        OGRateOfFire = RateOfFire;

        GameObject g = GameObject.FindGameObjectWithTag("Player");
        player = g.GetComponent<Player>();

        InvokeRepeating("Fire", 0, RateOfFire);
    }

    public void Fire()
    {
        // rotate enemy
        transform.Rotate(0f, 0f, rotationSpeed, Space.Self);

        // instantiate bullet
        float bulDirX = transform.position.x + Mathf.Sin((angle * Mathf.PI) / 180f);
        float bulDirY = transform.position.y + Mathf.Sin((angle * Mathf.PI) / 180f);

        Vector3 bulMoveVector = new Vector3(bulDirX, bulDirY, 0f);
        Vector2 bulDir = (bulMoveVector - transform.position).normalized;

        GameObject g = Instantiate(bullet.gameObject, transform.position, transform.rotation);
        g.transform.parent = g.GetComponent<SpiralBullet>().BulletParent.transform;
        g.GetComponent<SpiralBullet>().isPlayerFired = false;
        g.GetComponent<SpiralBullet>().MoveSpeed = BulletSpeed;
        g.GetComponent<SpiralBullet>().SetMoveDirection(bulDir);

        angle += angleSpeed;
    }

    // Update is called once per frame
    public void Update()
    {
        // check whether to move the boss
        moveBoss();

        timer += Time.deltaTime;
        int timerInt = (int)timer;
        rotationSpeed += 0.1f * Time.deltaTime;
        // select shooting pattern depending on timer
        switch (timerInt)
        {
            case 0:
                phase1();
                break;
            case 10:
                phase2();
                break;
            case 20:
                phase3();
                break;
            case 30:
                phase4();
                break;
            case 40:
                phase5();
                break;
        }
        if (timer > 50)
        {
            timer = 0;
        }
    }

    public void moveBoss()
    {
        // move enemy to screen
        var attackBoundss = AttackArea.GetComponent<BoxCollider2D>().bounds;
        if (!attackBoundss.Contains(transform.position))
        {
            // get random position in attack box
            var posX = Random.Range(attackBoundss.min.x, attackBoundss.max.x);
            var posY = Random.Range(attackBoundss.min.y, attackBoundss.max.y);
            bossPos = new Vector3(posX, posY, transform.position.z);
        }
        // move boss to the attack box
        if (transform.position != bossPos)
        {
            transform.position = Vector2.MoveTowards(transform.position, bossPos, Speed * Time.deltaTime);
        }
    }


    public void phase1()    // Attack pattern 1
    {
        if (timer <= 1)
        {
            rotationSpeed = 45f;
        }
        BulletSpeed = 200f;
        //rotationSpeed += 0.1f * Time.deltaTime;
        RateOfFire = 0.1f;
    }

    public void phase2()    // Attack pattern 2
    {
        rotationSpeed = 10f;
        RateOfFire = 0.5f;
    }

    public void phase3()    // Attack pattern 3
    {
        rotationSpeed = 35f;
        RateOfFire = 0.2f;
    }

    public void phase4()    // Attack pattern 4
    {
        BulletSpeed = 50f;
    }

    public void phase5()    // Attack pattern 5
    {
        rotationSpeed = 5f;

        rotationSpeed += 0.1f * Time.deltaTime;
        RateOfFire = 0.01f;
    }

    public void Hit(float damage)
    {
        health -= damage;
        StartCoroutine(TintSprite(GetComponent<SpriteRenderer>()));
    }

    public IEnumerator TintSprite(SpriteRenderer sprite)
    {
        while (true)
        {
            Color c = sprite.color;
            sprite.color = Color.red;
            yield return new WaitForSeconds(.5f);
            sprite.color = c;
            yield break;
        }
    }
}
