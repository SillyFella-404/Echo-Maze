using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface PlayerState
{
    public void touch(GameObject player, GameObject enemy) {
        //player.getComponent<Player>(); then call touch method
        //enemy.getComponent<Enemy>(); then call hurt method
    }

    public void update(GameObject player) {}

    public void setDuration(float duration) { }
}
