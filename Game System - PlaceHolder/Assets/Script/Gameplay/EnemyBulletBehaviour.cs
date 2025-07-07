using UnityEngine;

public class EnemyBulletBehaviour : MonoBehaviour
{
    private EnemyStats enemyStats;
    private Rigidbody2D rb;

    private void Awake()
    {
       rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            Debug.LogWarning("Bullet script requires a Rigidbody component on the GameObject to function correctly.");
        }
    }
    public void Initialize (EnemyStats stats, float timer)
    {
        this.enemyStats = stats;
        timer = enemyStats.bulletTimer;
        Destroy(gameObject, timer);
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = transform.up * enemyStats.bulletAttackSpeed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // TODO: Create and call player script
         
            Destroy (gameObject);
        }

        else
        {
            Destroy(gameObject);
        }
    }
}
