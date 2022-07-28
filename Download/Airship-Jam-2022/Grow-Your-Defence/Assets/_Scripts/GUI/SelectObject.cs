using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SelectObject : MonoBehaviour
{
    public static SelectObject instance;
    [Header("UI Selection Objects:")]
    public GameObject[] backgrounds;
    public GameObject[] UIObjects;
    public GameObject currentSelection;

    [Header("Buy Plant:")]
    public TextMeshProUGUI plantNameText;
    public TextMeshProUGUI plantCostText;
    public TextMeshProUGUI plantInfoText;
    public TextMeshProUGUI plantRangeText;
    public TextMeshProUGUI plantDamageText;

    [Header("Upgrade Plant:")]
    public TextMeshProUGUI upgradePlantText;
    public TextMeshProUGUI growthPercentageText;

    [Header("Invalid Placement:")]
    public TextMeshProUGUI invalidNameText;
    public TextMeshProUGUI invalidMessageText;

    [Header("Tool Tab:")]
    public TextMeshProUGUI toolNameText;
    public TextMeshProUGUI toolInformationText;

    void Awake()
    {
        if (instance != null)
        {
            Debug.Log("More than one SelectPlant class in scene");
        }
        instance = this;
        DisableUIObject();
    }
    void Update()
    {
        // Update percentage when selected
        if (UIObjects[1].activeSelf && currentSelection != null)
        {
            growthPercentageText.text = "Growth Percentage: " + currentSelection.GetComponent<Plant>().growthPercentage.ToString() + "%";
        }
    }

    public void SelectToolUI(string toolName, string toolInfo)
    {
        DisableUIObject();
        EnableUIObject(UIObjects[3]);

        // Set UI Elements
        toolNameText.text = toolName + ":";
        toolInformationText.text = toolInfo;
    }

    public void SelectInvalidUI(string errorText)   // Invalid UI Message
    {
        DisableUIObject();
        EnableUIObject(UIObjects[2]);

        // Set UI elements
        invalidNameText.text = "Error:";
        invalidMessageText.text = errorText;
    }

    public void SelectUpgradeUI(GameObject plant)   // Upgrade plant info
    {
        // Reset UI Elements
        DisableUIObject();
        EnableUIObject(UIObjects[1]);
        
        Plant selectedPlant = plant.GetComponent<Plant>();
        // Set UI elements
        upgradePlantText.text = selectedPlant.name + ":";
        if (selectedPlant.fruitPlant)
        {
            currentSelection = plant;
            growthPercentageText.text = "Growth Percentage: "+ selectedPlant.GetComponent<Plant>().growthPercentage.ToString() + "%";
        } else
        {
            growthPercentageText.text = selectedPlant.plantInformation;
        }
    }

    public void SelectBuyUI(GameObject plant)   // Buy plant info
    {
        // Reset UI elements
        DisableUIObject();
        EnableUIObject(UIObjects[0]);
        // Set plant variables to UI elements
        Plant selectedPlant = plant.GetComponent<Plant>();
        if (selectedPlant.fruitPlant)
        {
            plantRangeText.text = "Range: none";
            plantDamageText.text = "Damage: none";
        } else if (selectedPlant.gnomeTurret)
        {
            plantRangeText.text = "Range: " + selectedPlant.range.ToString();
            plantDamageText.text = "Damage: " + selectedPlant.damage.ToString();
        }

        // Set UI elements
        plantNameText.text = selectedPlant.name + ":";
        plantCostText.text = "Cost: £" + selectedPlant.cost.ToString();
        plantInfoText.text = selectedPlant.plantInformation;
        
    }

    // Check active tab/UI element
    void EnableUIObject(GameObject enableObj)
    {
        foreach (GameObject tab in UIObjects)
        {
            if (tab != enableObj)
            {
                tab.SetActive(false);
            } else
            {
                tab.SetActive(true);
                foreach (GameObject background in backgrounds)
                {
                    background.SetActive(true);
                }
            }
        }
    }
    public void DisableUIObject()
    {
        currentSelection = null;
        foreach (GameObject tab in UIObjects)
        {
            tab.SetActive(false);
            foreach (GameObject background in backgrounds)
            {
                background.SetActive(false);
            }
        }
    }
}
