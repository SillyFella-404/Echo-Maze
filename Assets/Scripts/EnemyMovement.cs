using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float moveSpeed = 4f;

    private Vector3 targetPosition;
    private bool moving = false;

    private const float SNAP_DISTANCE = 0.05f; // slightly larger for safety

    void Update()
    {
        if (!moving) return;

        Vector3 newPos = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
        transform.position = newPos;

        if (Vector3.Distance(transform.position, targetPosition) <= SNAP_DISTANCE)
        {
            transform.position = targetPosition;
            moving = false;
        }
    }

    /// <summary>
    /// Move towards target tile.
    /// Returns true once the tile is reached or we are already there.
    /// </summary>
    public bool MoveTo(Vector3 target)
    {
        // If already at or extremely close to target
        if (Vector3.Distance(transform.position, target) <= SNAP_DISTANCE)
        {
            transform.position = target;
            moving = false; // crucial fix
            return true;
        }

        // Start moving if not already moving
        if (!moving)
        {
            targetPosition = target;
            moving = true;
            return false; // not yet finished
        }

        // Still moving
        return !moving;
    }

    public bool IsMoving()
    {
        return moving;
    }

    public void StopAndSnapToGrid()
    {
        moving = false;

        Vector3 pos = transform.position;
        transform.position = new Vector3(
            Mathf.Round(pos.x),
            pos.y,
            Mathf.Round(pos.z)
        );

        this.gameObject.GetComponent<Enemy>().setState(new FrozenState());
    }
}
