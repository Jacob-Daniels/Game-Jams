using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{

    [Header("Enemy Attributes:")]
    public float health = 100f;
    public int killReward = 1;

    public void RemoveHealth(float damage)  // Remove health and check to destroy object
    {
        // Reduce health
        health -= damage;

        // Check if enemy is killed
        if (health <= 0f)
        {
            InventoryManager.instance.IncreaseCurrency(killReward);
            Destroy(gameObject);
        }
    }

}
