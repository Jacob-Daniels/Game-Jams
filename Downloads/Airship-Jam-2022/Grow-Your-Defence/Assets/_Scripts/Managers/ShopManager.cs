using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    [Header("Scripts, Objects & Components:")]
    public static ShopManager Instance;
    public PlayerManager playerManager;
    public InventoryManager inventoryManager;
    public GameObject[] plantPrefabs;


    private void Awake()
    {
        if (Instance != null)
        {
            Debug.Log("More than one ShopManager in the scene");
        }
        Instance = this;
    }

    void Start()
    {
       playerManager = GameObject.Find("GameManager").GetComponent<PlayerManager>();
       inventoryManager = GameObject.Find("GameManager").GetComponent<InventoryManager>();
    }

    public void SelectedPlant(int plantNumber)
    {
        BuildManager.instance.SetSelectedPlant(plantPrefabs[plantNumber]);
        // Activate UI for selected plant
        SelectObject.instance.SelectBuyUI(plantPrefabs[plantNumber]);
    }
}
