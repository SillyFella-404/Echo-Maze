using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderingState : EnemyState
{
    private Vector3 destination;
    private SphereCollider sphereCollider;
    private Rigidbody rb;
    private bool moving = false;

    // Hardcoded obstacle layers by name
    private LayerMask obstacleMask = LayerMask.GetMask("Obstacle", "Wall");

    private static readonly Vector3[] Directions = new Vector3[]
    {
        Vector3.right,
        Vector3.left,
        Vector3.forward,
        Vector3.back
    };

    void EnemyState.update(GameObject enemy)
    {
        if (sphereCollider == null)
            sphereCollider = enemy.GetComponent<SphereCollider>();
        if (rb == null)
            rb = enemy.GetComponent<Rigidbody>();

        if (rb == null)
        {
            Debug.LogError("Enemy must have a Rigidbody for physics-safe movement!");
            return;
        }

        if (!moving || destination == Vector3.zero)
        {
            destination = PickNewDestination(enemy);
            moving = true;
        }

        float speed = enemy.GetComponent<Enemy>().getWanderSpeed();
        Vector3 nextPos = Vector3.MoveTowards(rb.position, destination, speed * Time.fixedDeltaTime);

        rb.MovePosition(nextPos);

        if (Vector3.Distance(rb.position, destination) < 0.01f)
        {
            rb.position = destination;
            moving = false;
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

    private Vector3 PickNewDestination(GameObject enemy)
    {
        Vector3 current = SnapToGrid(enemy.transform.position);
        Vector3[] shuffledDirections = ShuffleDirections(Directions);

        foreach (Vector3 dir in shuffledDirections)
        {
            Vector3 potential = current + dir;
            if (CanMoveTo(potential))
                return potential;
        }

        return current;
    }

    private bool CanMoveTo(Vector3 pos)
    {
        float radius = sphereCollider.radius;

        // Check for obstacles at the destination
        if (Physics.CheckSphere(pos, radius, obstacleMask))
            return false;

        // Check for floor beneath the destination
        Ray ray = new Ray(pos + Vector3.up * 0.5f, Vector3.down); // start slightly above
        if (!Physics.Raycast(ray, 1.0f)) // cast down 1 unit
            return false;

        return true;
    }


    private Vector3[] ShuffleDirections(Vector3[] original)
    {
        Vector3[] shuffled = (Vector3[])original.Clone();
        for (int i = 0; i < shuffled.Length; i++)
        {
            int r = Random.Range(i, shuffled.Length);
            Vector3 temp = shuffled[i];
            shuffled[i] = shuffled[r];
            shuffled[r] = temp;
        }
        return shuffled;
    }
}
