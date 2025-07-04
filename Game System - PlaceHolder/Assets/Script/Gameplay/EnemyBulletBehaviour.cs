using UnityEngine;

public class EnemyBulletBehaviour : MonoBehaviour
{
    private EnemyStats enemyStats;
    private Rigidbody rb;

    private void Awake()
    {
       rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = transform.forward * enemyStats.bulletAttackSpeed;
    }

    public void Initialize (int dmg, float spd, float timer)
    {
        dmg = enemyStats.baseDmg;
        spd = enemyStats.bulletAttackSpeed;
        timer = enemyStats.bulletTimer;

        Destroy(enemyStats.bulletPrefabModel, timer);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // TODO: Create and call player script
         
            Destroy (enemyStats.bulletPrefabModel);
        }

        else
        {
            Destroy(enemyStats.bulletPrefabModel);
        }
    }
}
