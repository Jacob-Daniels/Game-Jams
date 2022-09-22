using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ToolManager : MonoBehaviour
{
    [Header("Scripts, Objects & Components:")]
    public static ToolManager instance;
    public TextMeshProUGUI hoeText;

    [Header("Tool Selected:")]
    public int toolSelected = 0;

    [Header("Tool Information:")]
    public string hoeInfo;
    public string sickleInfo;
    public string grassSeedInfo;
    public string sellInfo;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.Log("More than one ToolSelected class in the scene");
        }
        instance = this;
    }

    public void SelectTool(string toolName)
    {
        // Check the tool type
        switch (toolName)
        {
            case "Hoe":
                toolSelected = 1;
                BuildManager.instance.DeselectPlant();
                SelectObject.instance.SelectToolUI(toolName, hoeInfo);
                break;
            case "Sickle":
                toolSelected = 2;
                BuildManager.instance.DeselectPlant();
                SelectObject.instance.SelectToolUI(toolName, sickleInfo);
                break;
            case "Grass Seed":
                toolSelected = 3;
                BuildManager.instance.DeselectPlant();
                SelectObject.instance.SelectToolUI(toolName, grassSeedInfo);
                break;
            case "Sell":
                toolSelected = 4;
                BuildManager.instance.DeselectPlant();
                SelectObject.instance.SelectToolUI(toolName, sellInfo);
                break;
        }
    }

    public void DeselectTool()
    {
        toolSelected = 0;
    }

}
