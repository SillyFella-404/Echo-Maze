using System;
using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SaveState
{
    public static Boolean[] chaptersUnlocked = new Boolean[5];
    public static Boolean[] levelsUnlocked = new Boolean[10];
    public static EnemyType[] enemyTypes = { new DieType(), new ResetType() };

    public static float enemyRespawnTime = 2;


    public static Boolean startedUnlocks = false;

    public static void unlockAll() {
        for (int i = 0; i < chaptersUnlocked.Length; i++) {
            chaptersUnlocked[i] = true;
        }

        for (int i = 0; i < levelsUnlocked.Length; i++) {
            levelsUnlocked[i] = true;
        }

    }

    public static void startingUnlocks() {
        chaptersUnlocked[1] = true;
        levelsUnlocked[1] = true;
        startedUnlocks = true;
    }

    public static EnemyType getEnemyType(int id) {
        return enemyTypes[id];
    }

    public static void unlockLevel(int number) {
        levelsUnlocked[number] = true;
    }
}
