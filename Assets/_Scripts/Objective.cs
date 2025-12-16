using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objective : MonoBehaviour
{
    public Sprite ObjectiveCompleteSprite;
    public GameObject LevelCompleteScreen;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>(); 
        spriteRenderer.sprite = ObjectiveCompleteSprite;
        LevelCompleteScreen.SetActive(true);
    }
}
