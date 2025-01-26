using Unity.Mathematics;
using UnityEngine;

public class Bubbleshot : MonoBehaviour
{
    public float initialSpeed = 10f;      // Initial speed of the bubble
    public float acceleration = 0f;      // Optional acceleration (set to 0 for constant speed)
    public float deceleration = 5f;      // Rate at which the bubble slows down
    public float lifetime = 5f;          // Lifetime of the bubble before it's destroyed
    float curLifeTime = 0;

    private Rigidbody2D rb;              // Rigidbody2D component for movement
    private float currentSpeed;          // Current speed of the bubble

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentSpeed = initialSpeed;
    }

    void Update()
    {
        curLifeTime += Time.deltaTime;
        if (curLifeTime > lifetime)
        {
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

            for (int i = 0; i < enemies.Length; i++)
            {
                enemies[i].GetComponent<EnemyController>().mostRecentBubble = transform.position;
                Destroy(gameObject);
            }
        }
    }

    void FixedUpdate()
    {
        // Decelerate the bubble
        if (currentSpeed > 0)
        {
            currentSpeed = Mathf.MoveTowards(currentSpeed, 0, deceleration * Time.fixedDeltaTime);
        }

        // Apply movement in the bubble's current direction
        rb.linearVelocity = transform.up * currentSpeed;
    }
}
