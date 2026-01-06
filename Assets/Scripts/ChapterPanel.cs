using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChapterPanel : MonoBehaviour
{
    public int chapterNumber;

    // Start is called before the first frame update
    void Start()
    {
        if (!SaveState.chaptersUnlocked[chapterNumber]) {
            this.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
