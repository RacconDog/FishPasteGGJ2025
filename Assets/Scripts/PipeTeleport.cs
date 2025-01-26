using UnityEngine;

public class PipeTeleport : MonoBehaviour
{
    [Header("Teleport Settings")]
    [SerializeField] private GameObject entrance; // Reference to the entrance GameObject
    [SerializeField] private GameObject destination; // Reference to the destination GameObject

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the PlayerController entered the entrance trigger
        if (other.GetComponent<PlayerController>() != null)
        {
            // Teleport the player to the destination's position
            other.transform.position = destination.transform.position;
            Debug.Log("Player teleported to destination!");
        }
    }
}
