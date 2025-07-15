using UnityEngine;
using UnityEngine.UIElements;

public class MeleeEnemy : EnemyController
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private Transform playerTransform;
    private float attackCooldownTimer;
    private float attackRange = 1.5f;
    public float timeBetweenAttacks;
    void Awake()
    {
        GameObject GO = GameObject.FindGameObjectWithTag("Player");
        playerTransform = GO.transform;
        attackCooldownTimer = 0f;
    }

    public void Start()
    {
        if (baseWeaponSwingSpeed <= 0f)
        {
            timeBetweenAttacks = 1f/ baseWeaponSwingSpeed;
        }
        else
        {
            timeBetweenAttacks = 1f; //default swing speed if there are no attacks. 
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 lookDirection = (playerTransform.position - transform.position); // Result is already Vector2 if positions are 2D or implicitly cast

        
        float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.Euler(0, 0, angle);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f); // 10f is rotation speed, adjust as needed
        float distanceToPlayer = Vector2.Distance(transform.position, playerTransform.position);


        //Attack
        attackCooldownTimer -= Time.deltaTime;
        if (distanceToPlayer <= attackRange)
        {
            if (attackCooldownTimer <= 0f)
            {
                PerformMeleeAttack();
                attackCooldownTimer = timeBetweenAttacks;
            }
            return;
        }

        //Chase
        Vector2 moveDirection = (new Vector2(playerTransform.position.x, playerTransform.position.y) - new Vector2(transform.position.x, transform.position.y)).normalized;
        transform.position = new Vector3((transform.position.x + moveDirection.x * (baseMovementSpeed / 100f) * Time.deltaTime), (transform.position.y + moveDirection.y * (baseMovementSpeed / 100f) * Time.deltaTime), (transform.position.z));
 
    }

    private void PerformMeleeAttack()
    {
        Player.Instance.TakeDamage(baseDamageAmount);
    }
       
}
