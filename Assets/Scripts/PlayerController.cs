using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float acceleration = 5f; // Rate the fish speeds up
    [SerializeField] private float deceleration = 10f; // Rate the fish slows down
    [SerializeField] private float baseTurnSpeed = 200f; // Base turn speed
    [SerializeField] private float maxSpeed = 10f; // Top speed of the fish
    [SerializeField] private float turnRadiusModifier = 0.5f; // Modifier for wider turning radius (0 = no adjustment, higher = wider turns)

    [Header("Boost Settings")]
    [SerializeField] private float boostMultiplier = 10f; // Multiplier for boosting acceleration
    [SerializeField] private float boostDuration = 1f; // Time fish can boost for (from 1 to 0)
    [SerializeField] private float boostRechargeRate = 0.1f; // How quickly the boost recharges per second
    [SerializeField] private float boostDrainRate = 0.5f; // How quickly the boost drains per second
    private bool coolingDown = false; // Determines if the boost needs to recharge to 1 after a full depletion

    [Header("Turning Settings")]
    [SerializeField] private float turnForceImpulseMultiplier = 1f; // Multiplier for torque impulses
    [SerializeField] private float rotationalDeceleration = 5f; // Rate at which angular velocity slows down

    [Header("Bubble Settings")]
    [SerializeField] private GameObject bubbleshotPrefab; // Assign this in the Inspector
    [SerializeField] private Transform spawnPoint; // Position where the bubble spawns

    private Rigidbody2D rb;
    private float currentSpeed = 0f;

    // Double-tap detection
    private float lastLeftPressTime = -1f;
    private float lastRightPressTime = -1f;
    private const float doubleTapTime = 0.3f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Get input
        float vertical = Input.GetAxis("Vertical"); // W/S
        float horizontal = Input.GetAxis("Horizontal"); // A/D
        bool isBoosting = Input.GetKey(KeyCode.Space);

        // Boost logic
        if (isBoosting && boostDuration > 0f && !coolingDown)
        {
            float currentAcceleration = acceleration * boostMultiplier;
            boostDuration -= boostDrainRate * Time.deltaTime; // Decrease boost duration
            currentSpeed = Mathf.MoveTowards(currentSpeed, maxSpeed, currentAcceleration * Time.deltaTime);
        }
        else
        {
            // Recharge boost when not boosting
            boostDuration = Mathf.Clamp(boostDuration + boostRechargeRate * Time.deltaTime, 0f, 1f);
        }

        // Prevent boosting if boostDuration isn't fully recharged
        if (isBoosting && boostDuration <= 0f)
        {
            Debug.Log("Boost unavailable, recharging...");
            coolingDown = true;
        }

        if (!isBoosting && boostDuration == 1f)
        {
            coolingDown = false;
        }

        // Handle acceleration and deceleration
        if (vertical > 0 && !isBoosting)
        {
            currentSpeed = Mathf.MoveTowards(currentSpeed, maxSpeed, acceleration * Time.deltaTime);
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
            float adjustedTurnSpeed = baseTurnSpeed * (1 - (currentSpeed / maxSpeed) * turnRadiusModifier);
            transform.Rotate(Vector3.forward, -horizontal * adjustedTurnSpeed * Time.deltaTime);
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            if (Time.time - lastLeftPressTime <= doubleTapTime)
            {
                rb.AddTorque(turnForceImpulseMultiplier, ForceMode2D.Impulse); // Apply torque for left turn
            }
            lastLeftPressTime = Time.time;
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            if (Time.time - lastRightPressTime <= doubleTapTime)
            {
                rb.AddTorque(-turnForceImpulseMultiplier, ForceMode2D.Impulse); // Apply torque for right turn
            }
            lastRightPressTime = Time.time;
        }

        // Shooting the bubble
        if (Input.GetKeyDown(KeyCode.F))
        {
            Instantiate(bubbleshotPrefab, spawnPoint.position, spawnPoint.rotation);

        }
    }

    void FixedUpdate()
    {
        // Apply movement
        Vector2 direction = transform.up; // Forward direction of the player
        rb.linearVelocity = direction * currentSpeed;

        // Apply rotational deceleration
        if (Mathf.Abs(rb.angularVelocity) > 0.1f) // Only decelerate if there's significant angular velocity
        {
            float deceleration = rotationalDeceleration * Time.fixedDeltaTime * Mathf.Sign(rb.angularVelocity);
            rb.angularVelocity = Mathf.MoveTowards(rb.angularVelocity, 0f, Mathf.Abs(deceleration));
        }
    }
}
