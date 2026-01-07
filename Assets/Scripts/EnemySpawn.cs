using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    float waitTime = 0;
    Enemy child;
    Boolean waitingToRespawn;

    public Enemy getChild() {
        return child;
    }

    void Awake() {
        child = this.gameObject.GetComponentInChildren<Enemy>();
    }

    public void resetState() {
        child.gameObject.SetActive(true);
        child.resetState();
    }

    public void waitToRespawn(float time) {
        child.gameObject.SetActive(false);
        waitTime = time;
        waitingToRespawn = true;
    }

    void Update() {
        waitTime -= Time.deltaTime;

        if (waitTime <= 0 && waitingToRespawn) {
            waitingToRespawn = false;

            resetState();
        }
    }
}
