using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemySpawnSystem : MonoBehaviour
{
    [Header("Types Of Enemies")]
    public DistancedEnemy distancedenemy;
    public ChaserEnemy chaserenemy;
    public SpinEnemy spinenemy;
    public Boss bossenemy;
    public WallsScript walls;

    [Header("Item")]
    public Item item;

    [Header("Enemy Parents")]
    public GameObject ChaserParent;
    public GameObject DistanceParent;
    public GameObject SpinParent;
    public GameObject BossParent;
    public GameObject ItemParent;
    public GameObject WallParent;

    [Header("Player")]
    public Player player;

    [Header("Game Stats")]
    public float PlayTime; // dynamic difficulty float
    public float TimeUntilNewSpawn;
    private float OGTimeUntilNewSpawn;
    private UnityEngine.Camera cam; // get bounds of what the player can see
    public bool hit15 = false;
    public bool hit30 = false;
    public bool hit60 = false;
    public bool hit90 = false;
    public bool hit120 = false;

    [Header("Limits")]
    public List<GameObject> SpinEnemy;
    public float SpinEnemyLimit;
    public List<GameObject> DistanceEnemy;
    public float DistanceEnemyLimit;
    public List<GameObject> ChaserEnemy;
    public float ChaserEnemyLimit;
    public List<GameObject> BossEnemy;
    public float BossEnemyLimit;
    public List<GameObject> WallEnemy;
    public float WallLimit;

    [Header("Spawn Chance")]
    public float DistanceSpawnChance;
    public float ChaserSpawnChance;
    public float SpinSpawnChance;
    public float BossSpawnChance;
    public float ItemSpawnChance;
    public float WallSpawnChance;

    [Header("Spawn Area")]
    public GameObject SpawnArea;

    void Start()
    {
        cam = UnityEngine.Camera.main;
        OGTimeUntilNewSpawn = TimeUntilNewSpawn;
    }

    void Update()
    {
        if(SceneManager.GetActiveScene().name == "Tutorial")
        {
            return;
        }
        PlayTime += Time.deltaTime;

        if (TimeUntilNewSpawn <= 0)
        {
            Spawn();
            TimeUntilNewSpawn = OGTimeUntilNewSpawn;
        }
        else
        {
            TimeUntilNewSpawn -= Time.deltaTime;
        }

        int playtimeint = (int)PlayTime;

        switch (playtimeint)
        {
            case 15:
                if (hit15 == true)
                {

                }
                else
                {
                    hit15 = true;
                    ChaserEnemyLimit++;
                }
                break;
            case 30:
                if (hit30 == true)
                {

                }
                else
                {
                    hit30 = true;
                    TimeUntilNewSpawn -= 0.1f;
                }
                break;
            case 60:
                if(hit60)
                {

                }
                else
                {
                    hit60 = true;
                    SpinEnemyLimit++;
                }
                break;
            case 90:
                if (hit90)
                {

                }
                else
                {
                    BossEnemyLimit++;
                    hit90 = true;
                }

                break;
            case 120:
                if (hit120)
                {

                }
                else
                {
                    DistanceEnemyLimit += 3;
                    DistanceSpawnChance += 5;
                    hit120 = true;
                }
                break;
            case 121:
                hit15 = false;
                hit30 = false;
                hit60 = false;
                hit90 = false;
                hit120 = false;
                PlayTime = 0;
                break;
        }
    }

    public void Spawn()
    {
        // Spawn in enemies

        int spawnChance = Random.Range(0, 100);

        if (SpinEnemy.Count < SpinEnemyLimit && spawnChance <= SpinSpawnChance)
        {
            spawnInArea(spinenemy.gameObject);
        }
        if (DistanceEnemy.Count < DistanceEnemyLimit && spawnChance <= DistanceSpawnChance)
        {
            spawnInArea(distancedenemy.gameObject);
        }
        if (ChaserEnemy.Count < ChaserEnemyLimit && spawnChance <= ChaserSpawnChance)
        {
            spawnInArea(chaserenemy.gameObject);
        }
        if (BossEnemy.Count < BossEnemyLimit && spawnChance <= BossSpawnChance)
        {
            spawnInArea(bossenemy.gameObject);
        }

        // ITEM //

        if(spawnChance <= ItemSpawnChance)
        {
            spawnInArea(item.gameObject);
        }

        // WALL //

        if (WallEnemy.Count < WallLimit && spawnChance <= WallSpawnChance)
        {
            spawnInArea(walls.gameObject);
        }

    }

    public void spawnInArea(GameObject objectName)
    {
        var bounds = SpawnArea.GetComponent<BoxCollider2D>().bounds;
        var px = Random.Range(bounds.min.x, bounds.max.x);
        var py = Random.Range(bounds.min.y, bounds.max.y);

        Vector2 pos = new Vector3(px, py);

        switch(objectName.name)
        {
            case "SpinEnemy":
                GameObject s = Instantiate(objectName, pos, transform.rotation, SpinParent.transform);
                SpinEnemy.Add(s);
                break;

            case "DistancedEnemy":
                GameObject d = Instantiate(objectName, pos, transform.rotation, DistanceParent.transform);
                DistanceEnemy.Add(d);
                break;

            case "ChaserEnemy":
                GameObject c = Instantiate(objectName, pos, transform.rotation, ChaserParent.transform);
                ChaserEnemy.Add(c);
                break;

            case "Boss":
                GameObject b = Instantiate(objectName, pos, transform.rotation, BossParent.transform);
                BossEnemy.Add(b);
                break;

                // WALL //

            case "Wall":
                GameObject q = Instantiate(objectName, pos, transform.rotation, WallParent.transform);
                WallEnemy.Add(q);
                break;

                // ITEM //

            case "Item":
                Instantiate(objectName, pos, transform.rotation, ItemParent.transform);
                break;

        }
    }

    public IEnumerator RemoveObj(List<GameObject> g, GameObject ind)
    {
        switch(ind.name)
        {
            case "SpinEnemy(Clone)":
                player.Points += 100;
                break;

            case "DistancedEnemy(Clone)":
                player.Points += 200;
                break;

            case "ChaserEnemy(Clone)":
                player.Points += 25;
                break;

            case "Boss(Clone)":
                player.Points += 500;
                break;
        }

        g.Remove(ind);
        yield return new WaitForFixedUpdate();
        Destroy(ind);
        g.RemoveAll(s => s == null);
    }
}
