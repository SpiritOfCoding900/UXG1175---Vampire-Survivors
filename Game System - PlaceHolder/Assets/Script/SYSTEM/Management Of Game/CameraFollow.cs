using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;           // The player or object to follow
    public Vector3 offset;
    //public float smoothSpeed = 0.125f; // Smooth camera movement
    //public float minX = -20f;                 // Left boundary
    //public float maxX = 20f;                 // Right boundary
    //public float minY = -20f;                 // Up boundary
    //public float maxY = 20f;                 // Down boundary


    void Start()
    {

    }

    void Update()
    {
        
    }

    void LateUpdate()
    {
        if (target == null) return;
        
        transform.position = target.position + offset;

        //// Clamp target position within bounds
        //float targetX = Mathf.Clamp(target.position.x, minX, maxX);
        //float targetY = Mathf.Clamp(target.position.y, minY, maxY);

        //// Desired position includes both X and Y clamping
        //Vector3 desiredPosition = new Vector3(targetX, targetY, transform.position.z);

        //// Smooth camera movement
        //Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        //transform.position = smoothedPosition + offset;
    }
}