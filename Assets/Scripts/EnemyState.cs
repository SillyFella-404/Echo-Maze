using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface EnemyState
{
    public void update(GameObject enemy) {}
    public void see(GameObject enemy, float attentionSpan) {}
}
