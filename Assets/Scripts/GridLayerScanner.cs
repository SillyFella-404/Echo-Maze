using UnityEngine;

public class GridLayerScanner : MonoBehaviour
{
    public int width = 10;
    public int height = 10;
    public LayerMask detectionLayer;
    public float checkRadius = 0.45f;

    // Bottom-left corner of the grid in world space
    public Vector3 origin = Vector3.zero;

    public int[,] ScanGrid()
    {
        int[,] grid = new int[width, height];

        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < height; z++)
            {
                Vector3 checkPos = origin + new Vector3(x, 0f, z);

                bool hit = Physics.CheckSphere(
                    checkPos,
                    checkRadius,
                    detectionLayer,
                    QueryTriggerInteraction.Ignore
                );

                grid[x, z] = hit ? 1 : 0;
            }
        }

        return grid;
    }

    // Optional: visualize grid checks in editor
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;

        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < height; z++)
            {
                Vector3 pos = origin + new Vector3(x + 0.5f, 0f, z + 0.5f);
                Gizmos.DrawWireSphere(pos, checkRadius);
            }
        }
    }
}
