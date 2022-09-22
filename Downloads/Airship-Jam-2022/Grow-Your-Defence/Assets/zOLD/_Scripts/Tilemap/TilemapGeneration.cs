using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapGeneration : MonoBehaviour
{
    [Header("Scripts, Objects & Components")]
    public GameObject stonePrefab, dirtPrefab, grassPrefab;
    public GameObject TileParent;

    [Header("Variables")]
    public int width = 300;
    public int height = 300;

    [Header("Generation Variables")]
    public float scale = 6f;
    private float depth;
    public float offsetX = 0f;
    public float offsetY = 0f;

    void Start()
    {
        // Generate offset
        offsetX = Random.Range(0, 9999f);
        offsetY = Random.Range(0, 9999f);
        
        // create map
        GenerateHeight();
    }

    void GenerateHeight()
    {
        // Generate the height of each point
        for (int x = 0; x < width; x+= 5)
        {
            for (int y = 0; y < height; y+= 5)
            {
                float perlinNoise = CalculateHeight(x, y);
                // set the object type depending on height
                if (perlinNoise < 0.3)
                {
                    SpawnTile(dirtPrefab, x, y);
                } else if (perlinNoise < 0.5)
                {
                    SpawnTile(grassPrefab, x, y);
                } else if (perlinNoise >= 0.5)
                {
                    SpawnTile(stonePrefab, x, y);
                }
            }
        }
    }

    float CalculateHeight(int x, int y)
    {
        // Pass x, y values into perlin noise function
        float xCoord = (float)x / width * scale + offsetX;
        float yCoord = (float)y / height * scale + offsetY;

        return Mathf.PerlinNoise(xCoord, yCoord);
    }

    void SpawnTile(GameObject prefab, int width, int height)
    {
        // Create objects
        depth = 1;
        GameObject newObj = Instantiate(prefab, new Vector3(width, depth, height), Quaternion.identity);
        newObj.transform.parent = TileParent.transform;
    }
}
