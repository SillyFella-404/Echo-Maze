using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetType : EnemyType
{
    void EnemyType.hurt(GameObject enemy)
    {
        enemy.GetComponent<Enemy>().resetState();
    }

}
