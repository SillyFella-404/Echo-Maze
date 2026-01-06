using UnityEngine;

public class SmartWallBuilder : MonoBehaviour
{
    [Header("Prefabs")]
    public GameObject straightPrefab;
    public GameObject cornerPrefab;
    public GameObject tPiecePrefab;
    public GameObject xPiecePrefab;

    [Header("Grid Settings")]
    public float cellSize = 1f;

    public int rows = 10;
    public int cols = 10;
    [Range(0f, 1f)]
    public float wallProbability = 0.3f; // chance a cell is 1 (wall)

    // The generated array
    public int[,] grid;

    private int[,] GenerateRandomGrid(int rows, int cols)
    {
        int[,] newGrid = new int[rows, cols];

        for (int y = 0; y < rows; y++)
        {
            for (int x = 0; x < cols; x++)
            {
                // Randomly assign 0 or 1 based on wallProbability
                newGrid[y, x] = (Random.value < wallProbability) ? 1 : 0;
            }
        }

        return newGrid;
    }

    void Start() {
        grid = GenerateRandomGrid(rows, cols);
        GenerateGrid(grid);
    }

    // Call this to generate the level
    public void GenerateGrid(int[,] grid)
    {
        int rows = grid.GetLength(0);
        int cols = grid.GetLength(1);

        for (int y = 0; y < rows; y++)
        {
            for (int x = 0; x < cols; x++)
            {
                if (grid[y, x] == 1)
                {
                    PlaceWall(grid, x, y);
                }
            }
        }
    }

    private void PlaceWall(int[,] grid, int x, int y)
    {
        // Determine neighbors
        bool up = IsWall(grid, x, y + 1);
        bool down = IsWall(grid, x, y - 1);
        bool left = IsWall(grid, x - 1, y);
        bool right = IsWall(grid, x + 1, y);

        GameObject prefab = null;
        float rotation = 0f;

        int neighborCount = (up ? 1 : 0) + (down ? 1 : 0) + (left ? 1 : 0) + (right ? 1 : 0);

        switch (neighborCount)
        {
            case 4: // All sides
                prefab = xPiecePrefab;
                rotation = 0f;
                break;

            case 3: // T-piece
                prefab = tPiecePrefab;
                if (!up) rotation = 180f;
                else if (!right) rotation = 270f;
                else if (!down) rotation = 0f;
                else if (!left) rotation = 90f;
                break;

            case 2:
                // Straight if opposite sides
                if ((up && down) || (left && right))
                {
                    prefab = straightPrefab;
                    rotation = (left && right) ? 90f : 0f;
                }
                else // Corner if adjacent sides
                {
                    prefab = cornerPrefab;
                    if (up && right) rotation = 0f;
                    else if (right && down) rotation = 90f;
                    else if (down && left) rotation = 180f;
                    else if (left && up) rotation = 270f;
                }
                break;

            case 1: // Dead end
                prefab = straightPrefab;
                if (up) rotation = 0f;
                else if (down) rotation = 180f;
                else if (right) rotation = 90f;
                else if (left) rotation = 270f;
                break;

            case 0: // Isolated block
                prefab = straightPrefab;
                rotation = 0f;
                break;
        }

        Vector3 position = new Vector3(x * cellSize, 0f, y * cellSize);
        Instantiate(prefab, position, Quaternion.Euler(0f, rotation, 0f), transform);
    }

    private bool IsWall(int[,] grid, int x, int y)
    {
        if (x < 0 || y < 0 || x >= grid.GetLength(1) || y >= grid.GetLength(0))
            return false;
        return grid[y, x] == 1;
    }
}