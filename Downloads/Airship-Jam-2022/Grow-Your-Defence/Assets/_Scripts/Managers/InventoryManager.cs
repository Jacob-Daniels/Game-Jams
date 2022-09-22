using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InventoryManager : MonoBehaviour
{
    [Header("Scripts, Objects & Components:")]
    public static InventoryManager instance;
    public TextMeshProUGUI currencyText;

    [Header("Currency:")]
    public int currency = 35;

    void Awake()
    {
        // Only allow a single instance of this class to occur
        if (instance != null)
        {
            Debug.Log("More than one InventoryManager in the scene");
        }
        instance = this;
    }

    void Start()
    {
        UpdateUIText();
    }

    public void IncreaseCurrency(int increase)
    {
        currency += increase;
        UpdateUIText();
    }

    public void DecreaseCurrency(int decrease)
    {
        currency -= decrease;
        UpdateUIText();
    }

    void UpdateUIText() // Update UI Text
    {
        currencyText.text = "£" + currency.ToString();
    }

    public bool CheckPurchase(int value)    // Check player can purchase object in game
    {
        if (currency - value >= 0)
        {
            DecreaseCurrency(value);
            return true;
        } else
        {
            return false;
        }
    }
}
