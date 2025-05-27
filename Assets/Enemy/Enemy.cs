using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using playerNameSpace;

public class Enemy : MonoBehaviour
{
    public Transform target;
    public float speed = 3f;
    public float chaseDistance = 5f;
    public int maxHealth = 100;
    private int currentHealth;
    private bool isDead = false;

    public Animator animator;
    public Color flashColor = Color.red;
    public float flashDuration = 0.1f;
    public int flashCount = 3;

    private SpriteRenderer spriteRenderer;
    private Color originalColor;

    private Rigidbody2D rb;

    public GameObject healthPickupPrefab;
    public float dropChance = 0.3f;

    public float fireRatedropChance = 0.2f;

    public GameObject fireRatePickupPrefab;

    [Header("Sound Effects")]
    [SerializeField] private AudioClip hitSound; // Sound when hit by a bullet
    [SerializeField] private AudioClip deathSound; // Sound when the enemy dies
    private AudioSource audioSource;

    private GameManager gameManager; // Reference to the GameManager

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
        currentHealth = maxHealth;

        rb.constraints = RigidbodyConstraints2D.FreezeRotation;

        audioSource = GetComponent<AudioSource>(); // Reference to the AudioSource

        // Get reference to the GameManager
        gameManager = FindObjectOfType<GameManager>();

        if (GameObject.FindGameObjectWithTag("Player"))
        {
            target = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }

    private void Update()
    {
        if (!isDead)
        {
            UpdateAnimation();
        }
    }

    private void FixedUpdate()
    {
        if (!isDead && target != null)
        {
            float distanceToPlayer = Vector2.Distance(transform.position, target.position);

            if (distanceToPlayer <= chaseDistance)
            {
                Vector2 direction = (target.position - transform.position).normalized;
                rb.velocity = direction * speed;
            }
            else
            {
                rb.velocity = Vector2.zero;
            }
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
    }

    private void UpdateAnimation()
    {
        if (rb.velocity.sqrMagnitude > 0)
        {
            animator.SetBool("isRunning", true);
            spriteRenderer.flipX = rb.velocity.x > 0;
        }
        else
        {
            animator.SetBool("isRunning", false);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !isDead)
        {
            PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();

            if (playerHealth != null)
            {
                playerHealth.TakeDamage(20);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet") && !isDead)
        {
            PlaySound(hitSound); // Play hit sound
            TakeDamage(50); // Bullet hits the enemy
            Destroy(other.gameObject); // Destroy the bullet
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        StartCoroutine(FlashEffect());

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private IEnumerator FlashEffect()
    {
        for (int i = 0; i < flashCount; i++)
        {
            spriteRenderer.color = flashColor;
            yield return new WaitForSeconds(flashDuration);
            spriteRenderer.color = originalColor;
            yield return new WaitForSeconds(flashDuration);
        }
    }

    private void Die()
    {
        isDead = true;
        animator.SetTrigger("IsDead");
        rb.velocity = Vector2.zero;

        PlaySound(deathSound); // Play death sound

        // Notify the GameManager
        if (gameManager != null)
        {
            gameManager.OnVirusKilled();
        }

        if (Random.value < dropChance)
        {
            Instantiate(healthPickupPrefab, transform.position, Quaternion.identity);
        }

        if (Random.value < fireRatedropChance)
        {
            Instantiate(fireRatePickupPrefab, transform.position, Quaternion.identity);
        }

        StartCoroutine(DestroyAfterDelay());
    }

    private IEnumerator DestroyAfterDelay()
    {
        yield return new WaitForSeconds(0.3f);
        Destroy(gameObject);
        Player.scoreVal += 10;
    }

    private void PlaySound(AudioClip clip)
    {
        if (clip != null && audioSource != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }
}
