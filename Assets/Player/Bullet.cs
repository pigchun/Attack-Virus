using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Range(1, 10)]
    [SerializeField] private float speed = 10f;

    [Range(1, 10)]
    [SerializeField] private float lifeTime = 3f;

    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Destroy(gameObject, lifeTime); // Destroy the bullet after its lifetime expires
    }

    private void FixedUpdate()
    {
        rb.velocity = transform.up * speed; // Move the bullet in its forward direction
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the bullet collides with the wall
        if (collision.CompareTag("wall"))
        {
            Destroy(gameObject); // Destroy the bullet
        }
    }
}
