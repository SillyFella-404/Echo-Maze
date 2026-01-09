using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalState : PlayerState
{
    void PlayerState.touch(GameObject player, GameObject enemy)
    {
        //player.UI.GetComponent<UIManager>().
        player.SetActive(false);
        player.GetComponent<Player>().PacManager.stopEnemies();
        player.GetComponent<Player>().PacManager.takeLife();
    }
}
 