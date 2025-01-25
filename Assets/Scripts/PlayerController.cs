using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float acceleration = 5f;
    public float deceleration = 5f;
    public float turnSpeed = 200f;
    public float maxSpeed = 10f;

    private Rigidbody2D rb;
    private float currentSpeed = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Get input
        float vertical = Input.GetAxis("Vertical"); // W/S
        float horizontal = Input.GetAxis("Horizontal"); // A/D

        // Handle acceleration and deceleration
        if (vertical > 0)
        {
            currentSpeed = Mathf.MoveTowards(currentSpeed, maxSpeed, acceleration * Time.deltaTime);
        }
        else if (vertical < 0)
        {
            currentSpeed = Mathf.MoveTowards(currentSpeed, 0, deceleration * Time.deltaTime);
        }

        // Handle turning
        if (horizontal != 0)
        {
            transform.Rotate(Vector3.forward, -horizontal * turnSpeed * Time.deltaTime);
        }
    }

    void FixedUpdate()
    {
        // Apply movement
        Vector2 direction = transform.up; // Forward direction of the player
        rb.linearVelocity = direction * currentSpeed;
    }
}
