using System.Collections.Generic;
using UnityEngine;

public static class GridPathfinding
{
    public static List<Vector2Int> FindPath(Vector2Int start, Vector2Int target)
    {
        HashSet<Vector2Int> closedSet = new HashSet<Vector2Int>();
        List<Vector2Int> openList = new List<Vector2Int>();
        Dictionary<Vector2Int, Vector2Int> cameFrom = new Dictionary<Vector2Int, Vector2Int>();
        Dictionary<Vector2Int, int> gScore = new Dictionary<Vector2Int, int>();

        openList.Add(start);
        gScore[start] = 0;

        while (openList.Count > 0)
        {
            Vector2Int current = openList[0];
            foreach (var node in openList)
            {
                if (gScore.ContainsKey(node) && gScore[node] < gScore[current])
                    current = node;
            }

            if (current == target)
                return ReconstructPath(cameFrom, current);

            openList.Remove(current);
            closedSet.Add(current);

            foreach (Vector2Int neighbor in GetNeighbors(current))
            {
                if (closedSet.Contains(neighbor))
                    continue;

                // Check if neighbor is blocked by a wall object
                if (IsWallAt(neighbor))
                {
                    closedSet.Add(neighbor);
                    continue;
                }

                int tentativeGScore = gScore[current] + 1;
                if (!gScore.ContainsKey(neighbor) || tentativeGScore < gScore[neighbor])
                {
                    cameFrom[neighbor] = current;
                    gScore[neighbor] = tentativeGScore;
                    if (!openList.Contains(neighbor))
                        openList.Add(neighbor);
                }
            }
        }

        // No path found
        return null;
    }

    private static bool IsWallAt(Vector2Int gridPos)
    {
        // Convert grid tile to world position
        Vector3 worldPos = new Vector3(gridPos.x, 0f, gridPos.y);

        // Check if any object with tag "Wall" is at this position
        Collider[] hits = Physics.OverlapBox(worldPos, Vector3.one * 0.45f); // slightly smaller than 1 unit
        foreach (var hit in hits)
        {
            if (hit.CompareTag("Wall"))
                return true;
        }

        return false;
    }

    private static List<Vector2Int> GetNeighbors(Vector2Int node)
    {
        return new List<Vector2Int>
        {
            node + Vector2Int.up,
            node + Vector2Int.down,
            node + Vector2Int.left,
            node + Vector2Int.right
        };
    }

    private static List<Vector2Int> ReconstructPath(Dictionary<Vector2Int, Vector2Int> cameFrom, Vector2Int current)
    {
        List<Vector2Int> path = new List<Vector2Int> { current };
        while (cameFrom.ContainsKey(current))
        {
            current = cameFrom[current];
            path.Insert(0, current);
        }
        return path;
    }
}
