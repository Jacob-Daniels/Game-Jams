using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [Header("Scripts, Objects & Components")]
    private Transform targetPoint;

    [Header("Movement:")]
    private int waypointIndex = 0;

    [Header("Attributes:")]
    public float weight;
    public float speed = 10f;
    public int damage = 10;

    void Start()
    {
        targetPoint = Waypoints.waypoints[0];
    }

    void Update()
    {
        Vector3 direction = targetPoint.transform.position - transform.position;
        transform.Translate(direction.normalized * speed * Time.deltaTime, Space.World);

        // Check to change waypoint
        if (Vector3.Distance(transform.position, targetPoint.transform.position) <= 0.1)
        {
            GetNextWaypoint();
        }
    }

    void GetNextWaypoint()
    {
        // Check if enemy is at the last waypoint
        if (waypointIndex >= Waypoints.waypoints.Length - 1)
        {
            PlayerManager.instance.RemoveHealth(damage);
            Destroy(gameObject);
            return;
        }

        // Next waypoint
        waypointIndex++;
        targetPoint = Waypoints.waypoints[waypointIndex];
    }
}
