using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Item : MonoBehaviour
{
    [Header("Components")]
    public Player player;
    public Rigidbody2D rb;
    public ActivePowerUps text;
    public TextMeshProUGUI splashtext;
    public float Speed;
    public bool triggered = false;
    public int rng;

    [Header("Instantiate Text Objects")]
    public GameObject splashParent;
    public GameObject splashObject;

    [Header("What ability?")]
    public bool IncreaseROF;
    public bool IncreaseProjSpeed;
    public bool PlusHP;
    public bool MinusHP;
    public bool UPCannon;

    [Header("Increase Values")]
    public float IncreaseROFValue;
    public float IncreaseSizeValue;
    public float IncreaseProjSpeedValue;
    public int PlusHPValue;
    public int MinusHPValue;

    public void Awake()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        // RNG PICK A ITEM //

        rng = Random.Range(1, 6);

        switch (rng)
        {
            case 1:
                IncreaseROF = true;
                break;
            case 2:
                IncreaseProjSpeed = true;
                break;
            case 3:
                PlusHP = true;
                break;
            case 4:
                MinusHP = true;
                break;
            case 5:
                player.UpgradeCannon += 1;
                UPCannon = true;
                break;
        }
    }

    public void Start()
    {
        text = GameObject.Find("ActivePowerUpsTitle").GetComponent<ActivePowerUps>();
        splashParent = GameObject.Find("SplashParent");
    }

    public void Update()
    {
        rb.velocity = (-transform.up * Speed);

    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player") && triggered == false)
        {
            triggered = true;

            if (IncreaseROF)
            {
                // limit the fire rate
                if (other.GetComponent<Player>().BonusFireRate > 0.1)
                {
                    IncreaseROFValue = 0.02f;
                }
                else
                {
                    IncreaseROFValue = 0.01f;
                }
                if (other.GetComponent<Player>().BonusFireRate >= 0.05f)
                {
                    other.GetComponent<Player>().BonusFireRate -= IncreaseROFValue;
                }
                else
                {
                    other.GetComponent<Player>().BonusFireRate = 0.05f;
                }

                other.GetComponent<Player>().ChangeFireRate();
                StartCoroutine(SplashPowerupMsg("Increase Rate Of Fire!"));
                return;
            }

            if(IncreaseProjSpeed)
            {
                other.GetComponent<Player>().BulletSpeed += IncreaseProjSpeedValue;
                StartCoroutine(SplashPowerupMsg("Increased Projectile Speed!"));
                return;
            }

            if(PlusHP)
            {
                other.GetComponent<Player>().health += PlusHPValue;
                StartCoroutine(SplashPowerupMsg("Increase Health!"));
                return;
            }

            if (MinusHP)
            {
                // stop health decreasing at value
                if (other.GetComponent<Player>().health > MinusHPValue * 2)
                {
                    other.GetComponent<Player>().health -= MinusHPValue;
                }
                StartCoroutine(SplashPowerupMsg("Decrease Health!"));
                return;
            }
            
            // Display Cannon message on screen
            if (UPCannon)
            {
                StartCoroutine(SplashPowerupMsg("Upgrade Cannon!"));
            }

            // check cannon
            if (player.UpgradeCannon == 0) // Starter cannon
            {
                other.GetComponent<Player>().DoubleCannon = false;
                other.GetComponent<Player>().Starter = true;
                other.GetComponent<Player>().BigCannon = false;
                other.GetComponent<Player>().SingleCannon = false;
                StartCoroutine(ActivePowerupsMsg("Starter Cannon Active"));
                return;
            }

            if (player.UpgradeCannon == 1) // Single cannon
            {
                other.GetComponent<Player>().DoubleCannon = false;
                other.GetComponent<Player>().Starter = false;
                other.GetComponent<Player>().BigCannon = false;
                other.GetComponent<Player>().SingleCannon = true;
                StartCoroutine(ActivePowerupsMsg("Single Cannon Active"));
                return;
            }

            if (player.UpgradeCannon == 2) // Double cannon
            {
                other.GetComponent<Player>().DoubleCannon = true;
                other.GetComponent<Player>().Starter = false;
                other.GetComponent<Player>().BigCannon = false;
                other.GetComponent<Player>().SingleCannon = false;
                StartCoroutine(ActivePowerupsMsg("Double Cannon Active"));
                return;
            }

            if (player.UpgradeCannon == 3) // Big cannon
            {
                other.GetComponent<Player>().DoubleCannon = false;
                other.GetComponent<Player>().Starter = false;
                other.GetComponent<Player>().BigCannon = true;
                other.GetComponent<Player>().SingleCannon = false;
                StartCoroutine(ActivePowerupsMsg("Big Cannon Active"));
                return;
            }
            Debug.LogError("Item did nothing???");
        }
    }

    IEnumerator ActivePowerupsMsg(string msg)
    {
        while (true)
        {
            // Display power up in ui
            text.shipType(msg);
            Destroy(gameObject);
            yield break;
        }
    }

    IEnumerator SplashPowerupMsg(string msg)
    {
        while (true)
        {
            GameObject g = Instantiate(splashObject, transform.position, Quaternion.identity, splashParent.transform);
            TextMeshProUGUI gText = g.GetComponent<TextMeshProUGUI>();
            gText.text = msg;
            text.AddText(msg);
            Destroy(gameObject);
            yield break;
        }
    }
}
