using UnityEngine;

public class Bubbleshot : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float initialSpeed = 10f;      // Initial speed of the bubble
    [SerializeField] private float acceleration = 0f;      // Optional acceleration (set to 0 for constant speed)
    [SerializeField] private float deceleration = 5f;      // Rate at which the bubble slows down
    [SerializeField] private float lifetime = 5f;          // Lifetime of the bubble before it's destroyed

    [Header("Effects")]
    [SerializeField] private GameObject bubblePopEffect;    // Animated sprite prefab for the bubble pop effect

    private Rigidbody2D rb;                                // Rigidbody2D component for movement
    private float currentSpeed;                            // Current speed of the bubble

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentSpeed = initialSpeed;

        // Destroy the bubble after its lifetime
        Destroy(gameObject, lifetime);

        // Ignore collision with the player object
        IgnoreFishCollision();
    }

    private void IgnoreFishCollision()
    {
        Collider2D otherCollider = GameObject.FindFirstObjectByType<PlayerController>().GetComponent<Collider2D>(); // Fish collider
        Collider2D bubbleCollider = GetComponent<Collider2D>(); // Bubble collider
        Physics2D.IgnoreCollision(otherCollider, bubbleCollider); // Ignore collision between the bubble and its parent's collider
    }

    private void FixedUpdate()
    {
        // Decelerate the bubble
        if (currentSpeed > 0)
        {
            currentSpeed = Mathf.MoveTowards(currentSpeed, 0, deceleration * Time.fixedDeltaTime);
        }

        // Apply movement in the bubble's current direction
        rb.linearVelocity = transform.up * currentSpeed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Destroy the bubble and instantiate the bubble pop effect
        DestroyBubble();
    }

    private void DestroyBubble()
    {
        // Destroy the bubble GameObject
        Destroy(gameObject);

        // Instantiate the bubble pop effect if assigned
        if (bubblePopEffect != null)
        {
            Instantiate(bubblePopEffect, transform.position, Quaternion.identity);
        }
        else
        {
            Debug.LogWarning("Bubble pop effect not assigned in the Inspector!");
        }
    }
}
