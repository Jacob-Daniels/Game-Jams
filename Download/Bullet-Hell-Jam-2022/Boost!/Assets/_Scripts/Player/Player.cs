using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using MilkShake;

public class Player : MonoBehaviour
{
    [Header("Components")]
    public GameObject SpawnArea;
    public Rigidbody2D rb;
    public int health;
    public float Points;
    public Shaker shake;
    public MilkShake.ShakePreset preset;
    public GameObject LosePanel;

    public GameObject ShootPosLeft;
    public GameObject ShootPosRight;

    [Header("Pause Menu")]
    public GameObject PauseMenu;

    [Header("Animation")]
    public Animator anim;
    public AnimationClip SingleCannonClip;
    public AnimationClip DoubleCannonClip;
    public AnimationClip BigCannonClip;

    [Header("Movement")]
    public float PlayerSpeed;
    public float HorizontalAxis;
    public float VerticalAxis;

    [Header("Abiltiies")]
    public GameObject Bullet;
    public float BulletSpeed;
    public KeyCode ShootKey;
    public float GrowthSpeed;

    [Header("Ship Type")]
    public bool Starter = true;
    public bool SingleCannon = false;
    public bool DoubleCannon = false;
    public bool BigCannon = false;
    public int UpgradeCannon;

    [Header("Gun")]
    public float FireRate;
    public float BonusFireRate;
    public bool CanShoot = true;

    public void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        ChangeFireRate();
    }

    public void Update()
    {
        // MOVEMENT //

        HorizontalAxis = Input.GetAxis("Horizontal");
        VerticalAxis = Input.GetAxis("Vertical");

        movePlayer();
        //rb.velocity = new Vector2(HorizontalAxis, VerticalAxis) * PlayerSpeed;

        // SPAWN AREA //
        SpawnArea.transform.position = new Vector3(0, transform.position.y + 400);

        // ROTATION //

        if (LosePanel.activeInHierarchy == false)
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = 0;

            Vector3 objectPos = UnityEngine.Camera.main.WorldToScreenPoint(transform.position);
            mousePos.x = mousePos.x - objectPos.x;
            mousePos.y = mousePos.y - objectPos.y;

            float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90));
        }

        // SHOOT //
        if (Input.GetKey(ShootKey) && CanShoot)
        {
            StartCoroutine(RateOfFire());

            if (SingleCannon)
            {
                // disable other animations
                anim.SetBool("DoubleCannon", false);
                anim.SetBool("BigCannon", false);
                // new animation
                StartCoroutine(ShootAnim(SingleCannonClip, "SingleCannon"));
            }

            if(DoubleCannon)
            {
                // disable other animations
                anim.SetBool("SingleCannon", false);
                anim.SetBool("BigCannon", false);
                // enable new animation
                StartCoroutine(ShootAnim(DoubleCannonClip, "DoubleCannon"));
            }

            if(BigCannon)
            {
                // disable other animations
                anim.SetBool("SingleCannon", false);
                anim.SetBool("DoubleCannon", false);
                // enable new animation
                StartCoroutine(ShootAnim(BigCannonClip, "BigCannon"));
            }

            if(Starter)
            {
                anim.SetBool("SingleCannon", false);
                anim.SetBool("DoubleCannon", false);
                anim.SetBool("BigCannon", false);
            }
        }

        // CHANGE SIZE //

        // SETUP FOR SIZE OF PLAYER MATTERING EXAMPLE LOOK AT DECREASESIZE & INCREASE SIZE FUNCTIONS + CHANGEFIRERATE FUNCTION - LEX

        if (Input.GetKey(KeyCode.E))
        {
            IncreaseSize(0);
        }

        if (Input.GetKey(KeyCode.Q))
        {
            DecreaseSize(0);
        }

        if(SceneManager.GetActiveScene().name == "MainGame")
        {
            if(Input.GetKeyDown(KeyCode.Escape))
            {
                if (PauseMenu.activeInHierarchy)
                {
                    Time.timeScale = 1;
                    PauseMenu.SetActive(false);
                }
                else
                {
                    Time.timeScale = 0;
                    PauseMenu.SetActive(true);
                }
            }
        }
    }

    public void CheckVisibility()
    {
        if (GetComponent<SpriteRenderer>().isVisible == false)
        {
            LosePanel.SetActive(true);
            Time.timeScale = 0;
        }
    }

    public void movePlayer()
    {
        rb.velocity = new Vector2(HorizontalAxis, VerticalAxis) * PlayerSpeed;
    }

    // INCREASE/DECREASE SIZE

    public void IncreaseSize(float value)
    {
        if (value == 0)
        {
            if (transform.localScale.x < 100 && transform.localScale.y < 100)
            {
                transform.localScale += new Vector3(1,1,1) * Time.deltaTime * GrowthSpeed;
            }
        }
        else
        {
            if (transform.localScale.x > 100 && transform.localScale.y > 100 && transform.localScale.z > 100)
            {
                transform.localScale *= value;
            }
        }
        ChangeFireRate();
    }

    public void DecreaseSize(float value)
    {
        if (value == 0)
        {
            if (transform.localScale.x > 40 && transform.localScale.y > 40)
            {
                transform.localScale -= new Vector3(1, 1, 1) * Time.deltaTime * GrowthSpeed;
            }
        }
        else
        {
            if (transform.localScale.x > 40 && transform.localScale.y > 40 && transform.localScale.z > 40)
            {
                transform.localScale /= value;
            }
        }
        ChangeFireRate();
    }

    public void ChangeFireRate()
    {
        // SETUP FOR SIZE OF PLAYER MATTERING EXAMPLE

        float scale1 = Mathf.Max(transform.localScale.x, transform.localScale.y);

        float finalscale = Mathf.Max(scale1, transform.localScale.z);

        float ROF = finalscale / 80 - FireRate + BonusFireRate;

        var result = (Mathf.Round(ROF * 100)) / 100.0;

        FireRate = (float)result;
    }

    // SHOOT //

    IEnumerator RateOfFire()
    {
        CanShoot = false;
        Shoot();
        yield return new WaitForSeconds(FireRate);
        CanShoot = true;
    }

    public void Shoot()
    {
        if (Starter)
        {
            GameObject g = Instantiate(Bullet, transform.position, transform.rotation);
            g.transform.localScale = new Vector2(transform.localScale.x / 5, transform.localScale.y / 5);
            g.transform.parent = g.GetComponent<Bullet>().BulletParent.transform;
            g.GetComponent<Bullet>().isPlayerFired = true;
            g.GetComponent<Bullet>().Speed = 999 + BulletSpeed;
        }


        if (SingleCannon)
        {
            // Left cannon bullet
            GameObject g = Instantiate(Bullet, transform.position, transform.rotation);
            g.transform.localScale = new Vector2(transform.localScale.x / 4, transform.localScale.y / 4);
            g.transform.position = new Vector2(transform.position.x - transform.localScale.x / 3, transform.position.y + transform.localScale.y / 3);
            g.transform.parent = g.GetComponent<Bullet>().BulletParent.transform;
            g.GetComponent<Bullet>().isPlayerFired = true;
            g.GetComponent<Bullet>().Speed = 700 + BulletSpeed;
        }

        if(DoubleCannon)
        {
            // Left cannon bullet
            GameObject g = Instantiate(Bullet, transform.position, transform.rotation);
            g.transform.localScale = new Vector2(transform.localScale.x / 4, transform.localScale.y / 4);
            g.transform.position = new Vector2(ShootPosLeft.transform.position.x - transform.localScale.x / 3, ShootPosLeft.transform.position.y + transform.localScale.y / 3);
            g.transform.parent = g.GetComponent<Bullet>().BulletParent.transform;
            g.GetComponent<Bullet>().isPlayerFired = true;
            g.GetComponent<Bullet>().Speed = 700 + BulletSpeed;
            // Right cannon bullet
            GameObject g2 = Instantiate(Bullet, transform.position, transform.rotation);
            g2.transform.localScale = new Vector2(transform.localScale.x / 4, transform.localScale.y / 4);
            g2.transform.position = ShootPosRight.transform.position;
            g2.transform.parent = g2.GetComponent<Bullet>().BulletParent.transform;
            g2.GetComponent<Bullet>().isPlayerFired = true;
            g2.GetComponent<Bullet>().Speed = 700 + BulletSpeed;
        }

        if (BigCannon)
        {
            GameObject g = Instantiate(Bullet, transform.position, transform.rotation);
            g.transform.localScale = new Vector2(transform.localScale.x/3, transform.localScale.y/3);
            g.transform.position = new Vector2(transform.position.x, transform.position.y + 10);
            g.transform.parent = g.GetComponent<Bullet>().BulletParent.transform;
            g.GetComponent<Bullet>().isPlayerFired = true;
            g.GetComponent<Bullet>().Speed = 700 + BulletSpeed;
        }
    }

    public IEnumerator ShootAnim(AnimationClip animclip, string animname)
    {
        anim.SetBool(animname, true);
        if (Input.GetKey(ShootKey))
        {

        }
        else
        {
            yield return new WaitForSeconds(animclip.length);
            anim.SetBool(animname, false);
        }
    }

    // PLAYER HIT //

    public void Hit(int damage)
    {
        health -= damage;
        shake.Shake(preset);
        if (health <= 0)
        {
            health = 0;
            LosePanel.SetActive(true);
            Time.timeScale = 0;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("DistancedEnemy") || collision.CompareTag("ChaserEnemy") || collision.CompareTag("SpinEnemy") || collision.CompareTag("Boss"))
        {
            Hit(1);
            return;
        }
    }
}
