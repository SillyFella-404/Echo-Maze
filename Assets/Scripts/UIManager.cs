using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{

    public TMP_Text coinCounter;
    public TMP_Text lifeCounter;
    public GameObject PacManager;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }




    public void updateCoinCounter(int coinsCollected, int totalCoins) {
        coinCounter.text = coinsCollected + "/" + totalCoins;
    }

    public void updateLivesCounter(int currentLives, int maxLives) {
        lifeCounter.text = currentLives + "/" + maxLives;

    }

    public void playLoseLife() {
        UnityEngine.Debug.Log("Player lost life D:");
    }

}
