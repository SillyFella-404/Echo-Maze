using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orb : MonoBehaviour
{
    public float duration;

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) {
            UnityEngine.Debug.Log("Not touched player");
            return;
        } 

        PlayerTrigger trigger = other.GetComponent<PlayerTrigger>();
        if (trigger != null)
        {
            //UnityEngine.Debug.Log("Called touchOrb");
            trigger.player.touchOrb(duration);
            this.gameObject.SetActive(false);
        }
        else {
            //UnityEngine.Debug.Log("Trigger null");
        }
    }


}
