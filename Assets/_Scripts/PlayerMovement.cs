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


    public AudioClip jumpClip;
    public AudioClip hitEnemyClip;     // sound when hitting Enemy2/Enemy3
    public AudioClip gotHitClip;       // sound when enemy hits player

    private AudioSource audioSource;       // main AudioSource (jump, hit enemy)
    private AudioSource gotHitAudioSource; // second AudioSource (getting hit)

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = gravity;

        // Initialize AudioSources
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        gotHitAudioSource = gameObject.AddComponent<AudioSource>();
    }

    void Update()
    {
        if (GameManager._gameOver) return;

        Vector3 vel = rb.velocity;
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            vel.x -= deltaSpeed * Time.deltaTime;
            if (vel.x < -1 * speed)
            {
                vel.x = 1 * speed;
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

            // Play jump sound
            if (jumpClip != null)
            {
                audioSource.PlayOneShot(jumpClip);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Enemy"))
        {
            GameManager.SubtractLife();

            // Play "got hit" sound
            if (gotHitClip != null)
            {
                gotHitAudioSource.PlayOneShot(gotHitClip);
            }

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

        // Play hit sound when touching Enemy2 or Enemy3
        if (collision.transform.CompareTag("Enemy") || collision.transform.CompareTag("Enemy"))
        {
            if (hitEnemyClip != null)
            {
                audioSource.PlayOneShot(hitEnemyClip);
            }
        }
    }

    private bool IsGrounded()
    {
        return groundCollider.IsTouchingLayers(LayerMask.GetMask("Ground"));
    }
}
