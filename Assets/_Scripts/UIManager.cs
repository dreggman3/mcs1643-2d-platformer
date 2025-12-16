using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI ScoreboardTMP;
    public GameObject[]LifeToken;
    public GameObject GameOverScreen;
    public GameObject LevelCompleteScreen;

    private void Start()
    {
        GameOverScreen.SetActive(false);
        LevelCompleteScreen.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager._gameOver)
        {
            GameOverScreen.SetActive(true);
            return;
        }
        ScoreboardTMP.text = GameManager.Score.ToString();
        for (int i = 0; i < LifeToken.Length; i++)
        {
            LifeToken[i].SetActive(false);
        }
            for (int i = 0; i < GameManager.Lives; i++)
        {
            LifeToken[i].SetActive(true);
        }
    }
}
