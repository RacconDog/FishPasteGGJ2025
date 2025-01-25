using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float acceleration = 5f;
    public float deceleration = 5f;
    public float baseTurnSpeed = 200f; // Base turn speed
    public float maxSpeed = 10f;
    public float turnRadiusModifier = 0.5f; // Modifier for wider turning radius (0 = no adjustment, higher = wider turns)
    public float boostMultiplier = 1.5f; // Multiplier for boosting acceleration
    public float rotationalImpulseScale = 100f; // Scale for rotational impulse
    public float doubleTapTime = 0.3f;  // Maximum time between double-taps

    private Rigidbody2D rb;
    private float currentSpeed = 0f;
    private float currentAcceleration;

    // Double-tap detection
    private float lastLeftPressTime = -1f;
    private float lastRightPressTime = -1f;

    // Bubble references
    public GameObject bubbleshotPrefab; // Assign this in the Inspector
    public Transform spawnPoint;        // Position where the bubble spawns

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentAcceleration = acceleration; // Initialize with default acceleration
    }

    void Update()
    {
        // Get input
        float vertical = Input.GetAxis("Vertical"); // W/S
        float horizontal = Input.GetAxis("Horizontal"); // A/D
        bool isBoosting = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);

        // Handle boosting
        if (isBoosting)
        {
            currentAcceleration = acceleration * boostMultiplier;
        }
        else
        {
            currentAcceleration = acceleration;
        }

        // Handle acceleration and deceleration
        if (vertical > 0)
        {
            currentSpeed = Mathf.MoveTowards(currentSpeed, maxSpeed, currentAcceleration * Time.deltaTime);
        }
        else if (vertical < 0)
        {
            currentSpeed = Mathf.MoveTowards(currentSpeed, 0, deceleration * Time.deltaTime);
        }
        else
        {
            currentSpeed = Mathf.MoveTowards(currentSpeed, 0, deceleration * Time.deltaTime);
        }

        // Handle turning
        if (horizontal != 0)
        {
            // Adjust turn speed based on current speed and turn radius modifier
            float adjustedTurnSpeed = baseTurnSpeed * (1 - (currentSpeed / maxSpeed) * turnRadiusModifier);
            transform.Rotate(Vector3.forward, -horizontal * adjustedTurnSpeed * Time.deltaTime);
        }

        // Handle double-tap left for rotational impulse
        if (Input.GetKeyDown(KeyCode.A))
        {
            if (Time.time - lastLeftPressTime <= doubleTapTime)
            {
                ApplyRotationalImpulse(-rotationalImpulseScale); // Quick rotational impulse to the left
            }
            lastLeftPressTime = Time.time;
        }
        // Handle double-tap right for rotational impulse
        else if (Input.GetKeyDown(KeyCode.D))
        {
            if (Time.time - lastRightPressTime <= doubleTapTime)
            {
                ApplyRotationalImpulse(rotationalImpulseScale); // Quick rotational impulse to the right
            }
            lastRightPressTime = Time.time;
        }

        // Shooting the bubble
        if (Input.GetKeyDown(KeyCode.F))
        {
            Instantiate(bubbleshotPrefab, spawnPoint.position, transform.rotation);
        }
    }

    void FixedUpdate()
    {
        // Apply movement
        Vector2 direction = transform.up; // Forward direction of the player
        rb.linearVelocity = direction * currentSpeed;
    }

    private void ApplyRotationalImpulse(float impulse)
    {
        // Apply a rotational impulse directly to the Rigidbody2D's angular velocity
        rb.angularVelocity += impulse;
    }
}
