using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvulnerableState : PlayerState
{
    private float timeLeft = 1;

    void PlayerState.touch(GameObject player, GameObject enemy) {
        UnityEngine.Debug.Log("Mans lowk invincible typeshi");
        enemy.GetComponent<Enemy>().hurt();
    }

    void PlayerState.update(GameObject player) {
        timeLeft -= Time.deltaTime;

        if (timeLeft <= 0) {
            player.GetComponent<Player>().setState(new NormalState());
        }
    }

    public void setDuration(float duration) {
        timeLeft = duration;
    }
}
