using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContinueButton : MonoBehaviour
{
    public GameObject PauseMenu;
    public void OnClick()
    {
        PauseMenu.SetActive(false);
        Time.timeScale = 1;
    }
}
