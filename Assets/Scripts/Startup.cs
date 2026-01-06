using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Startup : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (!SaveState.startedUnlocks) {
            SaveState.startingUnlocks();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
