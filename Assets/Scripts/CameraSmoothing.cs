using UnityEngine;

public class CameraSmoothing : MonoBehaviour
{
    private Vector3 offset = new Vector3(0f, 0f, 0f);
    [SerializeField] private float smoothTime = 0.25f;
    private Vector3 velocity = Vector3.zero;

    [SerializeField] private Transform target;

    private void Update()
    {
        Vector3 targetPosition = target.position + offset;
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
    }
}
