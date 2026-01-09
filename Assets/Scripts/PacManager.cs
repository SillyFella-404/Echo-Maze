using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PacManager : MonoBehaviour
{
    public PlayerSpawn playerSpawn;
    private Player player;

    public UIManager UIManager;

    public int totalCoins;
    public int coinsCollected;

    public int maxLives;
    public int currentLives;

    public EnemySpawn[] enemySpawns;
    private Enemy[] enemies;
    public Boolean frozen;

    public int levelToUnlock;
    public String thisLevel;
    public String nextLevel;

    [SerializeField] float respawnDelay = 2f;
    bool isResetting = false;

    // Start is called before the first frame update
    void Start()
    {
        player = playerSpawn.getChild();

        enemies = new Enemy[enemySpawns.Length];

        for (int i = 0; i < enemySpawns.Length; i++) {
            enemies[i] = enemySpawns[i].getChild();
        }

        player.setState(new NormalState());

        updateCoinCounter();
        updateLivesCounter();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void win() {
        UnityEngine.Debug.Log("Game Won");
        SaveState.unlockLevel(levelToUnlock);
        SceneManager.LoadScene(nextLevel);
    }

    public void lose() {
        UnityEngine.Debug.Log("Game Lost");
        SceneManager.LoadScene(thisLevel);
    }

    public void reset() {
        playerSpawn.resetState();

        for (int i = 0; i < enemySpawns.Length; i++) {
            enemySpawns[i].resetState();
        }
    }

    public void stopEnemies() {
        for (int i = 0; i < enemies.Length; i++) {
            enemies[i].gameObject.GetComponent<EnemyMovement>().StopAndSnapToGrid();
        }
    }

    public void placeOutlines() {
        this.gameObject.GetComponent<GridOutlineInstantiator>().GenerateOutlines(this.gameObject.GetComponent<GridLayerScanner>().ScanGrid());
    }

    public void addCoin() {
        coinsCollected++;

        updateCoinCounter();
    }

    public void takeLife() {
        if (isResetting)
            return;

        currentLives--;

        updateLivesCounter();

        UIManager.playLoseLife();

        StartCoroutine(RespawnRoutine());
    }

    IEnumerator RespawnRoutine()
    {
        isResetting = true;

        // 1. Freeze gameplay
        Time.timeScale = 0f;

        // 2. Wait in REAL time so animation & UI still finish
        yield return new WaitForSecondsRealtime(respawnDelay);

        // 3. Reset player + enemies
        playerSpawn.resetState();
        for (int i = 0; i < enemySpawns.Length; i++) {
            enemySpawns[i].resetState();
        }

        // 4. Resume gameplay
        Time.timeScale = 1f;

        isResetting = false;
    }

    public void updateCoinCounter()
    {
        UIManager.updateCoinCounter(coinsCollected, totalCoins);

        if (totalCoins <= coinsCollected)
        {
            win();
        }
    }

    public void updateLivesCounter()
    {
        UIManager.updateLivesCounter(currentLives, maxLives);

        if (currentLives <= 0)
        {
            lose();
        }
    }
}
