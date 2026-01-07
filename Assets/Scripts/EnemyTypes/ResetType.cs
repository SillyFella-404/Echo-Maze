using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetType : EnemyType
{
    void EnemyType.hurt(GameObject enemy)
    {
        enemy.GetComponent<Enemy>().waitToRespawn(2);
        //enemy.GetComponent<Enemy>().resetState();
    }

}
