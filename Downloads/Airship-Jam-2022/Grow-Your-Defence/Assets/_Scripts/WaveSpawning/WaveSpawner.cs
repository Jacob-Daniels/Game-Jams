using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WaveSpawner : MonoBehaviour
{
    [Header("Scripts, Objects & Components")]
    public TextMeshProUGUI waveCountdownText;
    public TextMeshProUGUI waveCounterText;
    public Transform spawnPoint;
    public EnemySpawn[] enemyTypes;
    public GameObject enemyParent;

    [Header("Spawning Variables:")]
    private float countdown = 8f;
    public float waveTimer = 5f;
    public int waveWeight = 0;
    public int waveIndex = 0;

    void Start()
    {
        waveCountdownText = GameObject.Find("WaveCountdown").GetComponent<TextMeshProUGUI>();
        waveCounterText = GameObject.Find("WaveCounter").GetComponent<TextMeshProUGUI>();
        spawnPoint = GameObject.Find("StartPoint").transform;
        enemyParent = GameObject.Find("EnemyParent");
    }

    void Update()
    {
        // Check to spawn next wave
        if (countdown <= 0)
        {
            // Increase wave
            StartCoroutine(SpawnWave());
            countdown = waveTimer;
        }
        // Change wave countdown & counter text
        waveCountdownText.text = "Next Wave: " + Mathf.Round(countdown).ToString();
        if (waveIndex != 0)
        {
            waveCounterText.text = "Wave: " + waveIndex.ToString();
        } else
        {
            waveCounterText.text = "Wave: ";
        }
        

        if (waveWeight <= 0)
        {
            countdown -= Time.deltaTime;
        }
        // Update the game over wave count
        DontDestroyManager.instance.waveCount = waveIndex;
    }

    IEnumerator SpawnWave()
    {
        waveIndex++;
        // Calc the wave weight to spawn different types of enemies
        waveWeight = (int)Mathf.Pow(waveIndex, 2);

        // Cycle through the weight until limit is reached
        for (int i = waveWeight; i > 0;)
        {
            i = SpawnEnemy(i);
            yield return new WaitForSeconds(0.5f);
        }
    }

    int SpawnEnemy(int i)
    {
        EnemySpawn spawnEnemy = null;
        // Random Wave type
        // Spawn a random enemy
        for (int j = Random.Range(0, enemyTypes.Length); j >= 0;)
        {
            if (enemyTypes[j].weight <= i)  // Check if random enemy can spawn
            {
                spawnEnemy = enemyTypes[j];
                break;
            } else
            {
                // Reduce enemy type if unable to spawn
                j -= Random.Range(1, j+1);
            }
        }
        // Prevent error
        if (spawnEnemy == null)
        {
            return i;
        }
        // Set the wave values according to enemy spawned
        i -= spawnEnemy.weight;
        waveWeight -= spawnEnemy.weight;

        // spawn the strongest enemy
        GameObject newEnemy = Instantiate(spawnEnemy.enemyPrefab, spawnPoint.position, spawnPoint.rotation);
        newEnemy.transform.parent = enemyParent.transform;
        return i;
        
    }

}
