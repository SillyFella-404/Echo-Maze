using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawn : MonoBehaviour
{
    private Player child;

    public Player getChild()
    {
        return child;
    }

    void Awake() {
        child = this.gameObject.GetComponentInChildren<Player>();
    }

    public void resetState()
    {
        child.gameObject.SetActive(true);
        child.resetState();
    }
}
