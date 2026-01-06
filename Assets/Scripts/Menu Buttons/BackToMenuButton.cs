using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackToMenuButton : MonoBehaviour
{
    public void press()
    {
        SceneManager.LoadScene("Main Menu");
    }
}
