using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour
{
   // create variables that store base and modified values from JSON file.
    
    public string enemyName;
    public int baseHealth; // Store the base health from JSON
    public int currentHealth; // This will be the modified health
    public int baseDamageAmount; // Store base damage
    public int damageAmount; // This will be the modified damage
    public float baseMovementSpeed; // Store base speed
    public float movementSpeed; // This will be the modified speed
    public float baseFireRate;
    public float fireRate;
    public float baseWeaponSwingSpeed;
    public float weaponSwingSpeed;
    public float baseBulletSize;
    public float bulletSize;
    public float baseBulletSpeed;
    public float bulletSpeed;

    public string currentEnemyPattern;
    private Rigidbody2D rb;
    private Transform playerTransform; //reference player position

    public Transform[] patrolPoints;
    private int currentPatrolPointIndex = 0;
    public float patrolWaypointCheck = 0.1f; // the distance needed for the enemy to reach the waypoint to consider it reached. 

    public GameObject bulletPrefab;
    public Transform pointOfFire;
    private bool shootCooldown = true; // shooting cooldown
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
    }

    public void Initialize(JSONReader.EnemyClass enemyData, int gameLevel) //create variable enemyData and brings in data fron JSON reader.
    {
        enemyName = enemyData.name;
        currentEnemyPattern = enemyData.enemyPattern;

        // Store base stats from JSON
        baseHealth = enemyData.health;
        baseDamageAmount = enemyData.damageDealt;
        baseMovementSpeed = enemyData.movementSpeed / 100f; // Scale if needed
        baseFireRate = enemyData.firerate;
        baseWeaponSwingSpeed = enemyData.weaponSwingSpeed;
        baseBulletSize = enemyData.bulletSize;
        baseBulletSpeed = enemyData.bulletSpeed;

        // Apply level scaling to derive final stats
        ApplyLevelScaling(gameLevel);

        Debug.Log($"Initialized {enemyName} for Level {gameLevel}: Health={currentHealth}, Damage={damageAmount}, Speed={movementSpeed}, Pattern={currentEnemyPattern}");

        StartEnemyBehavior();
    }

    private void ApplyLevelScaling(int gameLevel) //each level makes enemies tougher to fight
    {
        float healthMultiplier = 1f + (gameLevel - 1) * 0.10f; // 10% health increase per level 
        float damageMultiplier = 1f + (gameLevel - 1) * 0.15f; //15% increase per level
        float speedMultiplier = 1f + (gameLevel - 1) * 0.10f; //10% increase per level 
        float bulletSpeedMultiplier = 1f + (gameLevel - 1) * 0.05f; //5% increase per level

        //no bullet size changes

        currentHealth = (int)(baseHealth * healthMultiplier);
        damageAmount = (int)(baseDamageAmount * damageMultiplier);
        movementSpeed = baseMovementSpeed * speedMultiplier;
        bulletSpeed = (int)(baseBulletSpeed * bulletSpeedMultiplier);

        //Ensure enemies do not have too low or high stats
        currentHealth = Mathf.Max(1, currentHealth);
        damageAmount = Mathf.Max(1, damageAmount);
        movementSpeed = Mathf.Max(0.1f, movementSpeed);
        fireRate = Mathf.Max(0.1f, fireRate);
        weaponSwingSpeed = Mathf.Max(0.1f, weaponSwingSpeed);
        bulletSpeed = Mathf.Max(1f, bulletSpeed);


        currentHealth = Mathf.Min(5, currentHealth);
        damageAmount = Mathf.Min(2, damageAmount);
        movementSpeed = Mathf.Min(1f, movementSpeed);
        fireRate = Mathf.Min(1f, fireRate);
        weaponSwingSpeed = Mathf.Min(0.5f, weaponSwingSpeed);
        bulletSpeed = Mathf.Min(1.5f, bulletSpeed);
    }

    private void StartEnemyBehavior()
    {
        // Stop any previous behavior coroutines first
        StopAllCoroutines();

        switch (currentEnemyPattern)
        {
            case "Chase":
                StartCoroutine(Chase());
                break;
            case "Patrol":
                if (patrolPoints == null || patrolPoints.Length == 0)
                {
                    Debug.LogWarning( "no patrol points assigned!");
                    return; // Don't start patrol if no points
                }
                StartCoroutine(Patrol());
                break;
            case "Look":
                StartCoroutine(Look());
                break;
            default:
                Debug.LogWarning($"Look Coroutine broken");
                break;
        }
    }

    private void SpriteRotation (Vector2 direction)
    {
        if (direction.magnitude > 0.01f)
        {
            Vector2 defaultSprite = Vector2.up; // by default the sprite points up.
            Quaternion targetRotation = Quaternion.FromToRotation(defaultSprite, direction);
            transform.rotation = targetRotation;
        }
    }
    
    private IEnumerator Chase()
    {
        if (playerTransform == null)
        {
            yield break; //exit if there is no player object. 
        }

        while (true) //chase indefinitely 
        {
            Vector2 playerDirection = (playerTransform.position - transform.position).normalized;
            rb.linearVelocity = (playerDirection * movementSpeed);


            // flip sprite based on left/right/up/down movement)
           SpriteRotation(playerDirection);

            yield return null;
        }

    }

    private IEnumerator Patrol()
    {
        if (playerTransform == null)
        {
            yield break; //exit if there is no player object. 
        }


        while (true) //patrol indefintely 
        {
            if (patrolPoints.Length == 0)
            {
                rb.linearVelocity = Vector2.zero;
                yield break;
            }

            Transform targetWaypoint = patrolPoints[currentPatrolPointIndex];
            Vector2 waypointDirection = (targetWaypoint.position - transform.position).normalized; // direction to the current waypoint
            rb.linearVelocity = waypointDirection * movementSpeed; //moving towards waypoint

            if (Vector2.Distance (transform.position, targetWaypoint.position)  < patrolWaypointCheck) //check if the waypoint has been reached 
            {
                currentPatrolPointIndex = (currentPatrolPointIndex + 1) % patrolPoints.Length; //move towards the next waypoint 
            }

            yield return null;
        }
    }

    private IEnumerator Look()
    {
        if (playerTransform == null)
        {
            yield break; //exit if there is no player object. 
        }

        while (true) //look indefintely 
        {
            Vector2 playerDirection = (playerTransform.position - transform.position).normalized;
            SpriteRotation(playerDirection);

            yield return null;
        }
    }

    private IEnumerator ShootRoutine()
    {
        if (fireRate <= 0) //if the enemy cannot fire, just ignore this coroutine.
        {
            yield break;
        }

        yield return new WaitForSeconds(Random.Range(1f, 1f / fireRate)); //randomizes bullet fire start time so that all enemies dont fire at the same time

        while (true)
        {
            if (shootCooldown == true) //if can shoot
            {
                Shoot();
                shootCooldown = false;
                yield return new WaitForSeconds(1f / fireRate);
                shootCooldown = true;
            }
            yield return null; //next frame
        }
    }

    private void Shoot()
    {
        // Instantiate the bullet
        GameObject bullet = Instantiate(bulletPrefab, pointOfFire.position, pointOfFire.rotation);

        // Get the bullet's Rigidbody2D and assign to bulletrb
        Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();

        
        // Pass in bullet stats into bullet.cs
        Bullet bulletScript = bulletRb.GetComponent<Bullet>();
        bulletScript.InitializeBullet(damageAmount, bulletSpeed, bulletSize);

        // Calculate bullet direction
        Vector2 shootDirection = (playerTransform.position - pointOfFire.position).normalized;

        // Apply velocity to bullet
        bulletRb.linearVelocity = shootDirection * bulletSpeed;

        // Apply bullet size to bullet
        bullet.transform.localScale = Vector3.one * bulletSize;
    }





    public void TakeDamage (int playerDamage)
    {
        currentHealth -= playerDamage;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        StopAllCoroutines();
        Destroy(gameObject);
    }
}
