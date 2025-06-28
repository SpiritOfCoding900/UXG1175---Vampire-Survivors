using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;           // The player or object to follow
    public float smoothSpeed = 0.125f; // Smooth camera movement
    public float minX;                 // Left boundary
    public float maxX;                 // Right boundary


    void Start()
    {

    }

    void Update()
    {
        
    }

    void LateUpdate()
    {
        if (target == null) return;

        // Only follow X axis
        float targetX = Mathf.Clamp(target.position.x, minX, maxX);
        Vector3 desiredPosition = new Vector3(targetX, transform.position.y, transform.position.z);
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
    }
}