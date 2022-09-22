using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("Scripts, Objects & Components:")]
    private Transform target;

    [Header("Variables")]
    public float speed = 40f;
    public float damage = 50f;

    public void SetTarget(Transform enemy)  // Pass in selected enemy
    {
        target = enemy;
    }

    void Update()
    {
        // Check if target exists
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        // Rotation of the bullet
        Vector3 direction = target.position - transform.position;

        // Check if bullet is in range of hitting enemy
        float distanceToHit = speed * Time.deltaTime;
        if (direction.magnitude <= distanceToHit)
        {
            HitTarget();
            return;
        }

        // Move bullet
        transform.Translate(direction.normalized * distanceToHit, Space.World);
    }

    void HitTarget()
    {
        // Remove health
        target.GetComponent<EnemyHealth>().RemoveHealth(damage);
        // Destroy bullet
        Destroy(gameObject);
    }
}
