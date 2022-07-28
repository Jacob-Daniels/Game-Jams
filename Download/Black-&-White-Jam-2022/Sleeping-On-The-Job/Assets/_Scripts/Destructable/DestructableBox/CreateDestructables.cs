using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CreateDestructables : MonoBehaviour
{
    [Header("Spawn Destructables")]
    public GameObject destPrefab;
    public Tilemap tilemap;
    public List<Vector3> spawnLocations;
    public int destLimit;
    public int destCounter;

    void Start()
    {
        spawnLocations = new List<Vector3>();

        // Get the boundary of the tilemap
        for (int i = tilemap.cellBounds.xMin; i < tilemap.cellBounds.xMax; i++)
        {
            for (int j = tilemap.cellBounds.yMin; j < tilemap.cellBounds.yMax; j++)
            {
                // checking whether the tilemap has a tile in the location to instantiate an object
                Vector3Int selectTile = new Vector3Int(i, j, (int)tilemap.transform.position.y);
                // getting the center of the tile in world space coords
                Vector3 useTile = tilemap.GetCellCenterWorld(selectTile);
                if (tilemap.HasTile(selectTile))
                {
                    // add tile location to list
                    spawnLocations.Add(useTile);
                }
            }
        }
        // Set limit
        destLimit = spawnLocations.Count;
    }

    void Update()
    {
        // spawn destructable object
        if (destCounter < destLimit)
        {
            spawnDestructable();
        }
    }

    public void spawnDestructable()
    {
        for (int i = 0; i < spawnLocations.Count; i++)
        {
            destCounter++;
            // Grab each vector
            Vector3 spawnPoint = new Vector3(spawnLocations[i].x, spawnLocations[i].y, 0f);
            // Create object on vector
            Instantiate(destPrefab, new Vector3(spawnPoint.x, spawnPoint.y, 0f), Quaternion.identity);
        }
    }
}
