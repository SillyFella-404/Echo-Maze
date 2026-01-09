using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixedWanderingState : EnemyState
{
    [Tooltip("Set the positions the enemy will move to in order.")]
    public Vector3[] waypoints;

    private int currentIndex = 0;
    private Vector3 destination;
    private SphereCollider sphereCollider;
    private Rigidbody rb;
    private bool moving = false;

    void EnemyState.setWaypoints(Vector3[] waypoints) {
        this.waypoints = waypoints;
    }

    void EnemyState.update(GameObject enemy)
    {
        if (waypoints == null || waypoints.Length == 0)
        {
            Debug.LogWarning("No waypoints assigned for FixedWanderingState.");
            return;
        }

        if (sphereCollider == null)
            sphereCollider = enemy.GetComponent<SphereCollider>();
        if (rb == null)
            rb = enemy.GetComponent<Rigidbody>();

        if (rb == null)
        {
            Debug.LogError("Enemy must have a Rigidbody for physics-safe movement!");
            return;
        }

        // Start moving to the next waypoint if not already moving
        if (!moving)
        {
            destination = SnapToGrid(waypoints[currentIndex]);
            moving = true;
        }

        float speed = enemy.GetComponent<Enemy>().getWanderSpeed();
        Vector3 nextPos = Vector3.MoveTowards(rb.position, destination, speed * Time.fixedDeltaTime);
        rb.MovePosition(nextPos);

        // Check if reached destination
        if (Vector3.Distance(rb.position, destination) < 0.01f)
        {
            rb.position = destination;
            moving = false;

            // Advance to next waypoint and loop if necessary
            currentIndex = (currentIndex + 1) % waypoints.Length;
        }
    }

    void EnemyState.see(GameObject enemy, float attentionSpan)
    {
        enemy.GetComponent<Enemy>().setState(new ChasingState());
        enemy.GetComponent<Enemy>().getState().see(enemy, attentionSpan);
    }

    private Vector3 SnapToGrid(Vector3 pos)
    {
        return new Vector3(
            Mathf.Round(pos.x),
            pos.y,
            Mathf.Round(pos.z)
        );
    }
}
