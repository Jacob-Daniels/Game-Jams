using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinEnemy : MonoBehaviour
{
    [Header("Movement")]
    public float Speed;

    [Header("Bullet Components")]
    public Bullet bullet;
    public float BulletSpeed;
    public float RateOfFire;
    private float OGRateOfFire;

    [Header("Player")]
    public Player player;

    [Header("Components")]
    public bool hasBeenInView = false;
    public float health;
    public bool CanShoot = true;
    public float RotatePlus;

    [Header("Enemy Spawn System")]
    public EnemySpawnSystem ess;

    public void Start()
    {
        OGRateOfFire = RateOfFire;

        GameObject g = GameObject.FindGameObjectWithTag("Player");
        player = g.GetComponent<Player>();

        GameObject e = GameObject.FindGameObjectWithTag("EnemySpawnSystem");
        ess = e.GetComponent<EnemySpawnSystem>();
    }

    // Update is called once per frame
    public void Update()
    {
        float zrot = transform.rotation.z + RotatePlus;
        //transform.Rotate(new Vector3(transform.position.x, transform.position.y, zrot) * Time.deltaTime);
        transform.Rotate(0f, 0f, zrot);

        if (hasBeenInView == false)
        {
            float step = Speed * Time.deltaTime;
            transform.position = Vector2.MoveTowards(gameObject.transform.position, player.transform.position, step);
        }

        if (CanShoot)
        {
            StartCoroutine(RateOfFireCourotine());
        }

        Vector3 viewPos = UnityEngine.Camera.main.WorldToViewportPoint(transform.position);

        if (viewPos.x >= 0 && viewPos.x <= 1 && viewPos.y >= 0 && viewPos.y <= 1 && viewPos.z > 0)
        {
            hasBeenInView = true;
            //Debug.Log("in view");
        }
        else if (hasBeenInView == true)
        {
            //Debug.Log("out of view");
           // StartCoroutine(ess.RemoveObj(ess.ChaserEnemy, gameObject));
        }
    }

    public void Hit(float damage)
    {
        health -= damage;
        StartCoroutine(TintSprite(GetComponent<SpriteRenderer>()));
    }

    // SHOOT //

    IEnumerator RateOfFireCourotine()
    {
        CanShoot = false;
        Shoot();
        yield return new WaitForSeconds(RateOfFire);
        CanShoot = true;
    }

    public void Shoot()
    {
        GameObject g = Instantiate(bullet.gameObject, transform.position, transform.rotation);
        g.transform.parent = g.GetComponent<Bullet>().BulletParent.transform;
        g.GetComponent<Bullet>().isPlayerFired = false;
        g.GetComponent<Bullet>().Speed = BulletSpeed;
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
