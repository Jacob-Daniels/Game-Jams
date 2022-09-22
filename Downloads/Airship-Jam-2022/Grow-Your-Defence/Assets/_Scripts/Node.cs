using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class Node : MonoBehaviour
{
    [Header("Scripts, Objects & Components:")]
    public GameObject plantParent;
    private GameObject hasPlant;
    private Material mat;

    [Header("Node Attributes:")]
    public int nodeType = 0;
    public Material soilMat;
    private Color grassColour;

    [Header("Variables:")]
    public Vector3 positionOffset;
    public Color hoverColour;

    void Start()
    {
        plantParent = GameObject.Find("PlantParent");
        mat = GetComponent<Renderer>().material;
        grassColour = mat.color;
    }

    void OnMouseDown()
    {
        if (CheckUIMenu())
        {
            return;
        }
        // Building a new plant on a tile (Purchasing from the UI store)
        if (BuildManager.instance.SelectPlantToBuild()) // Check a plant is selected in build manager
        {
            ToolManager.instance.DeselectTool();
            if (BuildManager.instance.plantToBuild.GetComponent<Plant>().gnomeTurret) // Plant type == 'GnomeTurret'
            {
                if (nodeType == 0)  // Node type == 'Grass'
                {
                    if (!hasPlant)
                    {
                        CheckTowerPlacement(false);
                        return;
                    }
                }
                else
                {
                    BuildManager.instance.DeselectPlant();
                    SelectObject.instance.SelectInvalidUI("Gnome must be placed on grass!");
                }
            }
            else
            {
                // Plant type == 'FruitPlant'
                if (nodeType == 1)  // Node type == 'Grass'
                {
                    if (!hasPlant)
                    {
                        CheckTowerPlacement(true);
                        return;
                    }
                }
                else
                {
                    BuildManager.instance.DeselectPlant();
                    SelectObject.instance.SelectInvalidUI("Hoe the ground first!");
                }
            }
        }

        // Using a tool on a node/plant or selecting a plant
        int currentTool = ToolManager.instance.toolSelected;
        if (hasPlant == null)
        {
            // Check if the player has a tool selected
            switch (currentTool)
            {
                case 1: // Hoe Tool
                    if (nodeType != 1)  // Prevent same node being hoe'd more than once
                    {
                        if (InventoryManager.instance.CheckPurchase(5)) // check can purchase
                        {
                            // Play Audio
                            AudioManager.instance.Play("HoeTool");
                            // Change node type to soil
                            nodeType = 1;
                            mat.color = soilMat.color;
                            ToolManager.instance.DeselectTool();
                            SelectObject.instance.DisableUIObject();
                        }
                        else
                        {
                            BuildManager.instance.DeselectPlant();
                            SelectObject.instance.SelectInvalidUI("Not enough money!");
                        }
                    }
                    break;
                case 3: // Grass Seed
                    if (nodeType != 0)  // Prevent node having multiple grass seeds on it
                    {
                        if (InventoryManager.instance.CheckPurchase(5)) // Check player can purchase
                        {
                            // Play Audio
                            AudioManager.instance.Play("GrassSeed");
                            // Change node to grass
                            nodeType = 0;
                            mat.color = grassColour;
                            ToolManager.instance.DeselectTool();
                            SelectObject.instance.DisableUIObject();
                        } else
                        {
                            BuildManager.instance.DeselectPlant();
                            SelectObject.instance.SelectInvalidUI("Not enough money!");
                        }
                    }
                    break;
            }
        } else
        {
            // Check if tool is selected
            if (currentTool != 0)
            {
                switch (currentTool)
                {
                    case 2: // Sickle Tool
                        if (hasPlant.GetComponent<Plant>().canHarvest)  // Harvest plant
                        {
                            // Play Audio
                            AudioManager.instance.Play("SickleTool");
                            // Destroy plant and add gold/reward
                            InventoryManager.instance.IncreaseCurrency(hasPlant.GetComponent<Plant>().harvestReward);
                            Destroy(hasPlant);
                            // reset values
                            ToolManager.instance.DeselectTool();
                            SelectObject.instance.DisableUIObject();
                        } else
                        {
                            ToolManager.instance.DeselectTool();
                            SelectObject.instance.DisableUIObject();
                        }
                        break;
                    case 4: // Sell Tool
                        int sellValue = (int)(hasPlant.GetComponent<Plant>().cost / 5);
                        InventoryManager.instance.IncreaseCurrency(sellValue);
                        Destroy(hasPlant);
                        ToolManager.instance.DeselectTool();
                        SelectObject.instance.DisableUIObject();
                        break;
                }
            }
            else
            {
                // Check if plant/gnome on node can be selected
                ToolManager.instance.DeselectTool();
                // Check whether plant can be selected
                if (BuildManager.instance.SelectPlantToBuild() != null) // Not enough space on tile
                {
                    BuildManager.instance.DeselectPlant();
                    SelectObject.instance.SelectInvalidUI("Not enough space!");
                    return;
                }
                else if (BuildManager.instance.SelectPlantToBuild() == null)  // Select plant
                {
                    // play audio
                    if (hasPlant.GetComponent<Plant>().fruitPlant)
                    {
                        AudioManager.instance.Play("SelectPlant");
                    } else
                    {
                        AudioManager.instance.Play("SelectGnome");
                    }
                    // select plant UI
                    SelectObject.instance.SelectUpgradeUI(hasPlant);
                    return;
                }
            }
        }
    }

    public void CheckTowerPlacement(bool isPlant)
    {
        // Get the cost of the plant selected
        int plantCost = BuildManager.instance.plantToBuild.GetComponent<Plant>().cost;
        // Check if player can afford to place plant
        if (InventoryManager.instance.CheckPurchase(plantCost))
        {
            // Play placement Audio
            if (isPlant)
            {
                AudioManager.instance.Play("BuyShop");
            } else
            {
                AudioManager.instance.Play("BuyShop");
            }
            // Spawn in new plant on selected node
            GameObject plantToBuild = BuildManager.instance.SelectPlantToBuild();
            hasPlant = Instantiate(plantToBuild, transform.position + positionOffset, transform.rotation);
            hasPlant.name = plantToBuild.name;
            hasPlant.transform.parent = plantParent.transform;
            BuildManager.instance.DeselectPlant();
        } else
        {
            BuildManager.instance.DeselectPlant();
            SelectObject.instance.SelectInvalidUI("Not enough money!");
        }
    }

    // Change colour when mouse hovers on object
    void OnMouseEnter()
    {
        if (CheckUIMenu())
        {
            return;
        }
        mat.color = hoverColour;
    }

    void OnMouseExit()
    {
        if (nodeType == 0)
        {
            mat.color = grassColour;
        } else
        {
            mat.color = soilMat.color;
        }
    }

    bool CheckUIMenu()
    {
        // Check if the mouse is on a UI element (e.g. The pause menu)
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return true;
        } else
        {
            return false;
        }
    }
}
