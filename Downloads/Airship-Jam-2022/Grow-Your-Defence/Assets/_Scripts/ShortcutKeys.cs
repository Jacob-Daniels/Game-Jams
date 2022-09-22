using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ShortcutKeys : MonoBehaviour
{
    void Update()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }
        // Keyboard shortcut keys
        if (Input.GetKeyDown(KeyCode.Alpha1))   // Shop element shortcuts [Using numbers]
        {
            ShopManager.Instance.SelectedPlant(0);
        } else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ShopManager.Instance.SelectedPlant(1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            ShopManager.Instance.SelectedPlant(2);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            ShopManager.Instance.SelectedPlant(3);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            ShopManager.Instance.SelectedPlant(4);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            ShopManager.Instance.SelectedPlant(5);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            ShopManager.Instance.SelectedPlant(6);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            ShopManager.Instance.SelectedPlant(7);
        }
        else if (Input.GetKeyDown(KeyCode.H))    // 'Hoe' shortcut key
        {
            ToolManager.instance.SelectTool("Hoe");
        } else if (Input.GetKeyDown(KeyCode.S)) // 'Sickle' shortcut key
        {
            ToolManager.instance.SelectTool("Sickle");
        } else if (Input.GetKeyDown(KeyCode.G)) // 'Grass Seed' shortcut key
        {
            ToolManager.instance.SelectTool("Grass Seed");
        }
    }
}
