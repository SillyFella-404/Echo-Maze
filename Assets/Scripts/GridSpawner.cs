using UnityEngine;

public class GridSpawner : MonoBehaviour
{
    public int length = 10;
    public int width = 10;
    public GameObject prefab;

    public LayerMask blockingLayers; // Layers that count as "not empty"

    void Start()
    {
        ScanAndSpawn();
    }

    void ScanAndSpawn()
    {
        for (int x = 0; x < length; x++)
        {
            for (int z = 0; z < width; z++)
            {
                Vector3 position = new Vector3(x, 0f, z);

                // Check if space is empty
                bool occupied = Physics.CheckBox(
                    position,
                    Vector3.one * 0.4f,
                    Quaternion.identity,
                    blockingLayers
                );

                if (!occupied)
                {
                    Instantiate(prefab, position, Quaternion.identity, transform);
                }
            }
        }
    }
}
