using UnityEngine;

public class GridOutlineInstantiator : MonoBehaviour
{
    public GameObject outlinePrefab;
    public Vector3 origin = Vector3.zero;

    public void GenerateOutlines(int[,] grid)
    {
        int width = grid.GetLength(0);
        int height = grid.GetLength(1);

        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < height; z++)
            {
                if (grid[x, z] != 1)
                    continue;

                Vector3 cellCenter = origin + new Vector3(x + 0.5f, 0f, z + 0.5f);

                // North (+Z)
                if (IsEmpty(grid, x, z + 1))
                    SpawnOutline(cellCenter + new Vector3(0f, 0f, 0.5f), Quaternion.Euler(90f, 0f, 0f));

                // South (-Z)
                if (IsEmpty(grid, x, z - 1))
                    SpawnOutline(cellCenter + new Vector3(0f, 0f, -0.5f), Quaternion.Euler(-90f, 0f, 0f));

                // East (+X)
                if (IsEmpty(grid, x + 1, z))
                    SpawnOutline(cellCenter + new Vector3(0.5f, 0f, 0f), Quaternion.Euler(0f, 0f, -90f));

                // West (-X)
                if (IsEmpty(grid, x - 1, z))
                    SpawnOutline(cellCenter + new Vector3(-0.5f, 0f, 0f), Quaternion.Euler(0f, 0f, 90f));
            }
        }
    }

    private bool IsEmpty(int[,] grid, int x, int z)
    {
        if (x < 0 || z < 0 || x >= grid.GetLength(0) || z >= grid.GetLength(1))
            return true;

        return grid[x, z] == 0;
    }

    private void SpawnOutline(Vector3 position, Quaternion rotation)
    {
        Instantiate(outlinePrefab, position, rotation, transform);
    }
}
