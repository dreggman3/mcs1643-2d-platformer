using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objective : MonoBehaviour
{
    public Sprite ObjectiveCompleteSprite;
    public GameObject LevelCompleteScreen;


    public AudioClip objectiveClip;
    private AudioSource audioSource;

    void Start()
    {
      
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
            spriteRenderer.sprite = ObjectiveCompleteSprite;
            LevelCompleteScreen.SetActive(true);

         
            if (objectiveClip != null)
            {
                audioSource.PlayOneShot(objectiveClip);
            }
        }
    }
}

