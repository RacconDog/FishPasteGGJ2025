using UnityEngine;

public class LipsPulse : MonoBehaviour
{
    [Header("Pulse Settings")]
    [SerializeField] private float maxScale = 1.2f; // Maximum scale for the pulse
    [SerializeField] private float pulseSpeed = 5f; // Speed of the scaling up and down

    private Vector3 originalScale; // Stores the original scale of the lips
    private bool isPulsing = false;

    void Start()
    {
        // Save the original scale of the lips
        originalScale = transform.localScale;
    }

    void Update()
    {
        // Trigger the pulse effect when F is pressed
        if (Input.GetKeyDown(KeyCode.F) && !isPulsing)
        {
            StartCoroutine(PulseEffect());
        }
    }

    private System.Collections.IEnumerator PulseEffect()
    {
        isPulsing = true;

        // Scale up
        while (transform.localScale.x < originalScale.x * maxScale - 0.01f)
        {
            transform.localScale = Vector3.MoveTowards(transform.localScale, originalScale * maxScale, Time.deltaTime * pulseSpeed);
            yield return null;
        }

        // Scale down
        while (transform.localScale.x > originalScale.x + 0.01f)
        {
            transform.localScale = Vector3.MoveTowards(transform.localScale, originalScale, Time.deltaTime * pulseSpeed);
            yield return null;
        }

        // Ensure the scale is reset exactly to the original
        transform.localScale = originalScale;
        isPulsing = false;
    }
}
