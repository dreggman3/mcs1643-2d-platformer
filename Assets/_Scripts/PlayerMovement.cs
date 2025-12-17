using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 4.0f;
    public float deltaSpeed = 3.0f;
    public float jumpForce = 12.0f;
    public float enemyBumpForce = 3.0f;
    public BoxCollider2D groundCollider;

    private Rigidbody2D rb;
    private const float gravity = 2.0f;

    // Improvements to consider:
    // - Double jump
    // - Easing into movement (accelerating more slowly)

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = gravity;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager._gameOver) return;

        Vector3 vel = rb.velocity;
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            vel.x -= deltaSpeed * Time.deltaTime;
            if (vel.x <  -1 * speed)
            {
                vel.x = -1 * speed;
            }
        }

        else if (Input.GetKey(KeyCode.RightArrow))
        {
            vel.x += deltaSpeed * Time.deltaTime;
            if (vel.x < 1 * speed)
            {
                vel.x = speed;
            }
        }
        else
        {
            vel.x = 0;
        }
            rb.velocity = vel;

        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode2D.Impulse);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Enemy"))
        {
            GameManager.SubtractLife();

            Vector2 myCenter = transform.position;
            Vector2 contactPoint = collision.GetContact(0).point;
            myCenter.y = contactPoint.y;
            Vector3 forceVector = myCenter - contactPoint;
            forceVector.y += 1;
            rb.AddForce(forceVector * enemyBumpForce, ForceMode2D.Impulse);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Enemy"))
        {
            GameManager.Score += 100; 
            Debug.Log($"Killed enemy! Score is now {GameManager.Score}");
          Destroy(collision.gameObject);
        }

    }


    private bool IsGrounded()
    {
         return groundCollider.IsTouchingLayers(LayerMask.GetMask("Ground"));
    }
}
