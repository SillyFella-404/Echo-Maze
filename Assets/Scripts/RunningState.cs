using System.Collections.Generic;
using UnityEngine;

public class RunningState : EnemyState
{
    private float runTime;

    private Vector3 currentMoveTarget;
    private bool hasMoveTarget;

    private Vector2Int lastTargetTile;

    // Directions for checking neighbors (up, down, left, right)
    private static readonly Vector2Int[] directions = new Vector2Int[]
    {
        new Vector2Int(1, 0),
        new Vector2Int(-1, 0),
        new Vector2Int(0, 1),
        new Vector2Int(0, -1)
    };

    public RunningState(float duration)
    {
        runTime = duration;
    }

    void EnemyState.update(GameObject enemy)
    {
        runTime -= Time.deltaTime;
        if (runTime <= 0f)
        {
            enemy.GetComponent<Enemy>().setState(new WanderingState());
            return;
        }

        EnemyMovement movement = enemy.GetComponent<EnemyMovement>();
        Transform player = GameObject.FindGameObjectWithTag("Player").transform;

        Vector2Int enemyTile = WorldToGrid(enemy.transform.position);
        Vector2Int playerTile = WorldToGrid(player.position);

        // If already moving toward a tile, check arrival
        if (hasMoveTarget)
        {
            if (Vector3.Distance(enemy.transform.position, currentMoveTarget) <= 0.05f)
            {
                hasMoveTarget = false;
            }
            else
            {
                // still moving
                return;
            }
        }

        // Choose next tile to move to
        Vector2Int bestTile = enemyTile;
        float maxDistance = Vector2Int.Distance(enemyTile, playerTile);

        foreach (var dir in directions)
        {
            Vector2Int candidate = enemyTile + dir;

            // Check if candidate is walkable
            if (!IsTileWalkable(candidate))
                continue;

            // Compute distance to player
            float dist = Vector2Int.Distance(candidate, playerTile);

            if (dist > maxDistance)
            {
                maxDistance = dist;
                bestTile = candidate;
            }
        }

        // Move if a better tile was found
        if (bestTile != enemyTile)
        {
            currentMoveTarget = GridToWorld(bestTile);
            movement.MoveTo(currentMoveTarget);
            hasMoveTarget = true;
        }
    }

    void EnemyState.see(GameObject enemy, float attentionSpan)
    {
        // Reset run timer if desired
        runTime = Mathf.Max(runTime, attentionSpan);
    }

    // -----------------------------
    // Helpers

    private Vector2Int WorldToGrid(Vector3 pos)
    {
        return new Vector2Int(
            Mathf.RoundToInt(pos.x),
            Mathf.RoundToInt(pos.z)
        );
    }

    private Vector3 GridToWorld(Vector2Int gridPos)
    {
        return new Vector3(gridPos.x, 0f, gridPos.y);
    }

    // Determines if a tile is walkable (not a wall)
    private bool IsTileWalkable(Vector2Int tile)
    {
        // This is a simple placeholder — adapt to your game's layer/collision system
        Collider[] hits = Physics.OverlapBox(
            new Vector3(tile.x, 0f, tile.y),
            Vector3.one * 0.45f, // half extents slightly smaller than 1
            Quaternion.identity,
            LayerMask.GetMask("Obstacle") // obstacles layer
        );

        return hits.Length == 0;
    }
}
