using System.Collections.Generic;
using UnityEngine;

public class EnemyPathGizmos : MonoBehaviour
{
    public List<Vector2Int> path;
    public int pathIndex;

    void OnDrawGizmos()
    {
        if (path == null || path.Count == 0) return;

        // Draw cyan lines for path
        Gizmos.color = Color.cyan;
        for (int i = 0; i < path.Count - 1; i++)
        {
            Vector3 a = new Vector3(path[i].x, 0f, path[i].y);
            Vector3 b = new Vector3(path[i + 1].x, 0f, path[i + 1].y);
            Gizmos.DrawLine(a, b);
        }

        // Draw red cube at current target tile
        if (pathIndex < path.Count)
        {
            Gizmos.color = Color.red;
            Vector3 targetPos = new Vector3(path[pathIndex].x, 0f, path[pathIndex].y);
            Gizmos.DrawWireCube(targetPos, Vector3.one * 0.9f);
        }
    }
}
