using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuLevelSelect : MonoBehaviour
{
    public void press() {
        SceneManager.LoadScene("Level Select");
    }
}
