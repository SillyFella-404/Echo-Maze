using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelectButton : MonoBehaviour
{
    public int levelNumber;
    public string level;

    // Start is called before the first frame update
    void Start()
    {
        if (!SaveState.levelsUnlocked[levelNumber])
        {
            this.gameObject.SetActive(false);
        }
    }

    public void press() {
        SceneManager.LoadScene(level);
    }
}
