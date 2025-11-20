using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoverTrigger : MonoBehaviour
{
public bool Triggered = false;
    public Mover TriggeredObject;

    private void OnTriggerEnter2D(Collider2D collision)
    {
      if (collision.transform.CompareTag("Player"))
        {
            TriggeredObject.Moving = true;
        }
    }
}
