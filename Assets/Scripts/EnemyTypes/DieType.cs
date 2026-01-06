using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieType : EnemyType
{
    void EnemyType.hurt(GameObject enemy) {
        enemy.SetActive(false);
    }

}
