using System.Collections.Generic;
using UnityEngine;

public class ChasingState : EnemyState
{
    private float chaseTime;

    private List<Vector2Int> currentPath;
    private int pathIndex;

    private Vector3 currentMoveTarget;
    private bool hasMoveTarget;

    void EnemyState.update(GameObject enemy)
    {
        // Countdown attention
        chaseTime -= Time.deltaTime;
        if (chaseTime <= 0f)
        {
            enemy.GetComponent<Enemy>().setState(new WanderingState());
            return;
        }

        EnemyMovement movement = enemy.GetComponent<EnemyMovement>();
        Transform player = GameObject.FindGameObjectWithTag("Player").transform;

        Vector2Int enemyTile = WorldToGrid(enemy.transform.position);
        Vector2Int playerTile = WorldToGrid(player.position);

        // Recalculate path if needed
        if (currentPath == null || pathIndex >= currentPath.Count || playerTile != lastTargetTile)
        {
            currentPath = GridPathfinding.FindPath(enemyTile, playerTile);
            pathIndex = 1;
            lastTargetTile = playerTile;
            hasMoveTarget = false;

            if (currentPath == null || currentPath.Count < 2)
                return; // no valid path yet
        }

        // Move along path
        if (!movement.IsMoving() && pathIndex < currentPath.Count && !hasMoveTarget)
        {
            currentMoveTarget = GridToWorld(currentPath[pathIndex]);
            movement.MoveTo(currentMoveTarget);
            hasMoveTarget = true;
        }

        // Advance only after arrival
        if (hasMoveTarget &&
            Vector3.Distance(enemy.transform.position, currentMoveTarget) <= 0.05f)
        {
            pathIndex++;
            hasMoveTarget = false;
        }

        // Gizmos
        EnemyPathGizmos gizmo = enemy.GetComponent<EnemyPathGizmos>();
        if (gizmo != null)
        {
            gizmo.path = currentPath;
            gizmo.pathIndex = pathIndex;
        }
    }

    void EnemyState.see(GameObject enemy, float attentionSpan)
    {
        // Simply reset chase timer; pathfinding continues regardless of LOS
        chaseTime = attentionSpan;
    }

    // -----------------------------
    // Helpers

    private Vector2Int lastTargetTile; // used only to prevent unnecessary path recalculation

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
}
