using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int sightRange;
    [SerializeField] private Boolean lineOfSight;
    [SerializeField] private float wanderSpeed;
    [SerializeField] private float chaseSpeed;
    [SerializeField] private float attentionSpan;
    [SerializeField] private EnemyState state;
    [SerializeField] private float wanderDistance;
    public Transform spawnPoint;
    public EnemyType type;
    public int typeID;
    public Boolean fixedWander;
    public Vector3[] waypoints;

    public void resetState()
    {
        transform.position = spawnPoint.position;
        transform.rotation = spawnPoint.rotation;
        state = new WanderingState();
    }

    public void hurt() {
        type.hurt(this.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        if (fixedWander) {
            state = new FixedWanderingState();
            state.setWaypoints(waypoints);
        }
        state = new WanderingState();
        type = SaveState.getEnemyType(typeID);

    }

    // Update is called once per frame
    void Update()
    {

        if (SeesPlayer(sightRange, lineOfSight)) {

            state.see(this.gameObject, attentionSpan);
        }

        state.update(this.gameObject);
    }

    public void setState(EnemyState newState) {
        state = newState;
    }

    public EnemyState getState() {
        return state;
    }

    public float getWanderSpeed() {
        return wanderSpeed;
    }

    public float getChaseSpeed() {
        return chaseSpeed;
    }

    public bool SeesPlayer(float sightRange, bool lineOfSight)
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, sightRange);

        foreach (Collider hit in hits)
        {
            if (!hit.CompareTag("Player"))
                continue;

            if (!lineOfSight)
                return true;

            Vector3 dir = (hit.transform.position - transform.position).normalized;
            float dist = Vector3.Distance(transform.position, hit.transform.position);

            if (Physics.Raycast(transform.position, dir, out RaycastHit rayHit, dist))
            {
                if (rayHit.collider.CompareTag("Wall"))
                    return false;
            }

            return true;
        }

        return false;
    }

    public void waitToRespawn(float time) {
        spawnPoint.gameObject.GetComponent<EnemySpawn>().waitToRespawn(time);
    }


}
