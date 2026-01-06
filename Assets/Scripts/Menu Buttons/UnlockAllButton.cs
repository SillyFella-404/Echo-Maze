using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockAllButton : MonoBehaviour
{
    public void unlockAll() {
        SaveState.unlockAll();
    }
}
