using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    [Header("Scripts, Objects & Components:")]
    public static BuildManager instance;
    public GameObject shootPlantPrefab;
    public GameObject plantToBuild;

    void Awake()
    {
        // Set a single instance of the class which is referenced to throughout the scene.
        if (instance != null)
        {
            Debug.Log("More than one BuildManager class in the scene");
        }
        instance = this;
    }

    public GameObject SelectPlantToBuild()
    {
        return plantToBuild;
    }

    public void SetSelectedPlant(GameObject plant)
    {
        plantToBuild = plant;
    }
    
    public void DeselectPlant()
    {
        // Reset plantToBuild
        plantToBuild = null;
        SelectObject.instance.DisableUIObject();
    }
}
