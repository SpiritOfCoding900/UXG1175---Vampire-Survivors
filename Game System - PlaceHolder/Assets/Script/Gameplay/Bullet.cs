using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float bulletTimer = 2f;
    private int bulletDamage;
    private float bulletSpeed;
    
    private Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    
    //initialize bullet properties
    public void InitializeBullet (int damageAmount, float speed, float size, Vector2 direction)
    {
        bulletDamage = damageAmount;
        bulletSpeed = speed;
        transform.localScale = Vector3.one * size;
        
        rb.linearVelocity = direction.normalized * bulletSpeed;
    }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Destroy(gameObject, bulletTimer); //destroy all bullets currently on screen after the timer is reached.  
    }

    void OnTriggerEnter2D(Collider2D other) // create a function for bullet behaviour
    {
        if (other.CompareTag ("Player"))
        {
            Player.Instance.TakeDamage(bulletDamage);
        }
        Destroy (gameObject); // Destroy after colliding with player.
    } 
}
