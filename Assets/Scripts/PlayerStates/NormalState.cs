using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalState : PlayerState
{
    void PlayerState.touch(GameObject player, GameObject enemy)
    {
        //player.UI.GetComponent<UIManager>().
        player.SetActive(false);
        enemy.GetComponent<EnemyMovement>().StopAndSnapToGrid();
        player.GetComponent<Player>().PacManager.takeLife();
    }
}
 