using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainGeneration : MonoBehaviour
{
    [Header("Scripts, Objects & Components")]
    public Terrain terrain;

    [Header("Variables")]
    public int depth = 10;
    public int width = 256;
    public int height = 256;

    [Header("Edit Terrain:")]
    public float scale = 8f;
    public float offsetX = 0f;
    public float offsetY = 0f;

    private void Start()
    {
        // Random offset for terrain
        //offsetX = Random.Range(0f, 9999f);
        //offsetY = Random.Range(0f, 9999f);

        terrain = GetComponent<Terrain>();
    }

    private void Update()
    {
        // Set the new terrain data
        Terrain terrain = GetComponent<Terrain>();
        terrain.terrainData = GenerateTerrain(terrain.terrainData);
    }

    TerrainData GenerateTerrain(TerrainData terrainData)
    {
        // Generate new terrain data
        terrainData.heightmapResolution = width + 1;

        terrainData.size = new Vector3(width, depth, height);
        terrainData.SetHeights(0, 0, GenerateHeights());

        return terrainData;
    }

    float[,] GenerateHeights()
    {
        // Generate the height of each point on the terrain
        float[,] heights = new float[width, height];
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                heights[x, y] = CalculateHeight(x, y);
            }
        }
        return heights;
    }

    float CalculateHeight(int x, int y)
    {
        // Convert coords into values for perlin noise function
        float xCoord = (float)x / width * scale + offsetX;
        float yCoord = (float)y / height * scale + offsetY;

        return Mathf.PerlinNoise(xCoord, yCoord);
    }
}