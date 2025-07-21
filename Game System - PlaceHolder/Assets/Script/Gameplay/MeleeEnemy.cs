using UnityEngine;
using UnityEngine.UIElements;

public class MeleeEnemy : EnemyController
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private Transform playerTransform;
    private float attackCooldownTimer;
    private float attackRange = 2f;
    public float timeBetweenAttacks;
    private Rigidbody2D rb;

    public GameObject attackRangeIndicator;
    public float indicatorDisplay = 0.2f;
    void Awake()
    {
        GameObject GO = GameObject.FindGameObjectWithTag("Player");
        playerTransform = GO.transform;
        attackCooldownTimer = 0f;
        rb = GetComponent<Rigidbody2D>();
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
    void FixedUpdate()
    {
        Vector2 lookDirection = (playerTransform.position - transform.position);
        Debug.Log(playerTransform.position);


        float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.Euler(0, 0, angle);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f); // 10f is rotation speed.
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
            rb.linearVelocity = Vector2.zero;
            return;
        }

        //Chase
        rb.linearVelocity = lookDirection * baseMovementSpeed;
 
    }

    private void PerformMeleeAttack()
    {
        Debug.Log("PerformMelee running");
        Player.Instance.TakeDamage(baseDamageAmount);
        VisualizeAttackRange();
    }

    private void VisualizeAttackRange()
    {
        GameObject indicator = Instantiate (attackRangeIndicator, transform.position, Quaternion.identity);
        indicator.transform.SetParent(this.transform);
        indicator.transform.localScale = Vector3.one * (attackRange * 2);
        Destroy (indicator, indicatorDisplay);
    }
       
}
