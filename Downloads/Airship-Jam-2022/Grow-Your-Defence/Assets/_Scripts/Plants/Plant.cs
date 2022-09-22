using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : MonoBehaviour
{
    [Header("Scripts, Objects & Components:")]
    private Transform target;
    public Transform rotateHead;
    public GameObject bulletPrefab;
    public GameObject bulletParent;
    public Transform bulletSpawnPoint;

    [Header("Plant Type:")]
    public bool fruitPlant;
    public bool gnomeTurret;

    [Header("Plant Attributes:")]
    public Material[] fruitColours;
    public int cost = 20;
    public string plantInformation;

    [Header("Growth Attributes:")]
    public int harvestReward = 50;
    public float maxHarvest = 1.5f;
    public bool canHarvest;
    public float harvestRate;
    public float timer = 0.04f;
    public int growthPercentage;

    [Header("Turret Attributes:")]
    public string enemyTag = "Enemy";
    public float damage;
    public float rotationSpeed = 10f;
    public float range = 15f;
    public float fireRate = 1f;
    private float fireCountdown = 0f;

    void Awake()
    {
        // Random plant colour
        int randomColour = (int)Random.Range(0, fruitColours.Length);
        // Loop through all the children to find the fruits and change the material
        foreach (Transform plantChild in gameObject.transform)
        {
            if (plantChild.name == "plant")
            {
                foreach (Transform fruitsChild in plantChild.transform)
                {
                    if (fruitsChild.name == "Fruits")
                    {
                        foreach (Transform fruit in fruitsChild.transform)
                        {
                            fruit.GetComponent<MeshRenderer>().material = fruitColours[randomColour];
                        }
                    }
                }
            }
        }
    }

    void Start()
    {
        // Call function on timer
        InvokeRepeating("CheckTarget", 0f, 0.5f);

        rotateHead = gameObject.transform.Find("Head");
        bulletParent = GameObject.Find("BulletParent");
    }

    void Update()
    {
        // Check the plant type (e.g. passive plant / turret
        if (fruitPlant)
        {
            growPlant();
        } else if (gnomeTurret)
        {
            turret();
        }
    }

    void growPlant()
    {
        // Check can harvest
        if (harvestRate < maxHarvest)
        {
            harvestRate += timer * Time.deltaTime;
            // Slowly increase size of the plant
            growthPercentage = (int)((harvestRate / maxHarvest) * 100);
            float localSize = harvestRate/2;
            if (localSize > 1f)
            {
                localSize = 1f;
            }
            transform.localScale = new Vector3(localSize, localSize, localSize);
        } else
        {
            canHarvest = true;
        }
    }

    // If object is a turret then locate enemy and shoot
    void turret()
    {
        // Return if no enemy in range
        if (target == null)
        {
            return;
        }

        // Rotate object towards enemy target
        Vector3 direction = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        Vector3 rotation = Quaternion.Lerp(rotateHead.rotation, lookRotation, Time.deltaTime * rotationSpeed).eulerAngles;
        rotateHead.rotation = Quaternion.Euler(0f, rotation.y, 0f);

        // Shoot enemy
        if (fireCountdown <= 0f)
        {
            Shoot();
            fireCountdown = 1f / fireRate;
        }
        fireCountdown -= Time.deltaTime;
    }

    void Shoot()
    {
        // Spawn bullet
        GameObject bulletObj = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
        bulletObj.transform.parent = bulletParent.transform;

        // Set bullet target and damage
        Bullet bullet = bulletObj.GetComponent<Bullet>();
        bullet.damage = damage;
        if (bullet != null)
        {
            bullet.SetTarget(target);
        }
    }

    void CheckTarget()
    {
        // Find enemies with tag and set initial values
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        // Check distance to each enemy in the array
        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            // Get the smallest distance between all enemies
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        // Check if enemy is within the objects range
        if (nearestEnemy != null && shortestDistance <= range)
        {
            target = nearestEnemy.transform;
        } else
        {
            target = null;
        }
    }

    private void OnDrawGizmosSelected() // Display range when object is selected
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
