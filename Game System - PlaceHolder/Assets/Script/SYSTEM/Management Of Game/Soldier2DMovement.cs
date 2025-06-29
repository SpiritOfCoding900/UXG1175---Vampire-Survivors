using UnityEngine;

public class Soldier2DMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Rigidbody2D rb;
    public Vector2 moveDir;

    // public bool isInGroundZone = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        moveDir = new Vector2(moveX, moveY).normalized;
    }

    void FixedUpdate()
    {
        rb.linearVelocity = moveDir * moveSpeed;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Entered: " + other.name);
        if (other.CompareTag("GroundTag"))
        {
            // isInGroundZone = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("GroundTag"))
        {
            // isInGroundZone = false;
        }
    }
}
