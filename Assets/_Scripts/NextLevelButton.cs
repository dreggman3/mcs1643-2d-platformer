using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevelButton : MonoBehaviour
{
    public string Nextlevel = "Level_0";

    public void OnClick()
    {
        if (SceneManager.GetActiveScene().name != Nextlevel)
        {
            Debug.Log("Loading next level: " + Nextlevel);
            SceneManager.LoadScene(Nextlevel);
        }
        else
        {
            Debug.Log("Scene " + Nextlevel + " is already active. Skipping rreload.");
        }
        
    }
}
