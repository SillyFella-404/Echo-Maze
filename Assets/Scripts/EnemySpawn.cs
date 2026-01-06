using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    Enemy child;

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
}
