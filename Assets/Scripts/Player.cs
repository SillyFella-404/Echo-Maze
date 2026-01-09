using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.Diagnostics;
using UnityEngine;

public class Player : MonoBehaviour
{
    private PlayerState state;

    public Transform spawnPoint;

    public PacManager PacManager;


    public void resetState()
    {
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        transform.position = spawnPoint.position;
        transform.rotation = spawnPoint.rotation;
        isMoving = false;
        this.state = new NormalState();
    }

    public void touchEnemy(GameObject enemy) {
        UnityEngine.Debug.Log("called touchEnemy with state " + state);
        state.touch(this.gameObject, enemy);
    }

    public PlayerState getState() {
        return state;
    }

    public void setState(PlayerState newState) {
        state = newState;
    }

    public float moveSpeed = 5f;             // units per second
    public LayerMask obstacleLayers;

    private Rigidbody rb;
    private Vector3 targetPosition;
    private Vector3 queuedDirection = Vector3.zero;
    private bool isMoving = false;
    private float sphereRadius;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotation;

        SphereCollider sc = GetComponent<SphereCollider>();
        sphereRadius = sc.radius * transform.localScale.x; // didn't scale the player but who knows

        targetPosition = transform.position;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Coin"))
        {
            Destroy(other.gameObject);
            PacManager.addCoin();
        }
    }

    public void touchOrb(float duration) {
        state = new InvulnerableState();
        state.setDuration(duration);
    }

    void Update()
    {
        Vector3 inputDir = Vector3.zero;

        // Capture input, vertical prioritized over horizontal (prevents diagonals)
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) inputDir = Vector3.forward;
        else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) inputDir = Vector3.back;
        else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) inputDir = Vector3.left;
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) inputDir = Vector3.right;

        // Queue input if currently moving
        if (isMoving)
        {
            queuedDirection = inputDir;
        }
        else
        {
            TryMove(inputDir);
        }

        state.update(this.gameObject);
    }

    void FixedUpdate()
    {
        if (isMoving)
        {
            Vector3 moveDir = (targetPosition - transform.position).normalized;
            rb.velocity = moveDir * moveSpeed;

            // snap to grid
            if (Vector3.Distance(transform.position, targetPosition) < 0.01f)
            {
                transform.position = targetPosition;
                rb.velocity = Vector3.zero;
                isMoving = false;

                // Only move to the next square if input is still held
                if (queuedDirection != Vector3.zero && InputHeld(queuedDirection))
                {
                    Vector3 dir = queuedDirection;
                    queuedDirection = Vector3.zero;
                    TryMove(dir);
                }
                else
                {
                    queuedDirection = Vector3.zero; // cancel queued move if key not held
                }
            }
        }
    }

    void TryMove(Vector3 dir)
    {
        if (dir == Vector3.zero) return;

        // Check for obstacles
        if (!Physics.SphereCast(transform.position, sphereRadius, dir, out RaycastHit hit, 1f, obstacleLayers))
        {
            targetPosition = transform.position + dir.normalized;
            isMoving = true;
            transform.rotation = Quaternion.LookRotation(dir);
        }
    }

    // Check if the key corresponding to the direction is still being held
    bool InputHeld(Vector3 dir)
    {
        if (dir == Vector3.forward) return Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow);
        if (dir == Vector3.back) return Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow);
        if (dir == Vector3.left) return Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow);
        if (dir == Vector3.right) return Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow);
        return false;
    }
}
