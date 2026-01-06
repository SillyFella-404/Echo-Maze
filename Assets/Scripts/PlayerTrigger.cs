using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class PlayerTrigger : MonoBehaviour
{
    public GameObject parent;
    public Player player;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            //UnityEngine.Debug.Log("Player hit by " + other.gameObject.name);
            player.touchEnemy(other.gameObject);

        }
    }

    void Start() {
        parent = transform.parent.gameObject;
        player = parent.GetComponent<Player>();
    }

}
