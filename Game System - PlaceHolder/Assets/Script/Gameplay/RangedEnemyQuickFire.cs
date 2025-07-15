using UnityEngine;
using System.Collections.Generic;

public class RangedEnemyQuickFire : EnemyController
{
    private Transform playerTransform;
    private float fireCooldownTimer;

    public GameObject bulletPrefab;
    public Transform pointOFire;

    //Randomized Patrol settings around player
    public float patrolRadius = 100f;
    public int patrolPoints = 3;
    public float patrolPointTreshold = 0.5f;

    private List<Vector2> generatedPatrolPoints; //List to store generated patrol points;
    private int currentPatrolPoint = 0;
    private Vector3 initialSpawnPosition; //where enemies start patrolling from.


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    void Awake()
    {
        GameObject playerGO = GameObject.FindGameObjectWithTag("Player");
        playerTransform = playerGO.transform;

        if (pointOFire == null)
        {
            pointOFire = transform;
        }

        fireCooldownTimer = 0f;
        initialSpawnPosition = transform.position;
        generatedPatrolPoints = new List<Vector2>();
    }

    void Start()
    {
        fireCooldownTimer = 1f / baseFireRate;
        GeneratePatrol();
    }
    // Update is called once per frame
    void Update()
    {
        PatrolMovement();

        //Enemy will shoot forward as it goes, not looking at the player. 
        fireCooldownTimer -= Time.deltaTime;
        if (fireCooldownTimer <= 0)
        {
            ShootBullet();
            if (baseFireRate > 0)
            {
                fireCooldownTimer = 1f / baseFireRate;
            }
            else
            {
                fireCooldownTimer = 3.0f; //default anchor if firerate is 0
            }
        }
    }

    private void GeneratePatrol()
    {
        generatedPatrolPoints.Clear();

        for (int i = 0; i < patrolPoints; i++)
        {
            // Generate a random spawn position within a radius around the player. 
            Vector2 randomOffset = Random.insideUnitCircle * patrolRadius;
            Vector2 randomPoint = new Vector2(initialSpawnPosition.x + randomOffset.x, initialSpawnPosition.z + randomOffset.y);

            generatedPatrolPoints.Add(randomPoint);
        }
    }

    private void PatrolMovement()
    {
        Vector2 targetPoint = generatedPatrolPoints[currentPatrolPoint];
        Vector2 currentPosition = new Vector2(transform.position.x, transform.position.y);
        Vector2 directionToTarget = (targetPoint - currentPosition).normalized;

        //Moving
        transform.position = new Vector3((transform.position.x + directionToTarget.x * (baseMovementSpeed / 100f) * Time.deltaTime), (transform.position.y + directionToTarget.y * (baseMovementSpeed / 100f) * Time.deltaTime), (transform.position.z));

        //Change Patrol Point 
        if (Vector2.Distance(currentPosition, targetPoint) < patrolPointTreshold)
        {
            currentPatrolPoint = (currentPatrolPoint +1 ) % generatedPatrolPoints.Count;
        }
    }
    
    private void ShootBullet()
    {
        GameObject newBullet = Instantiate (bulletPrefab, pointOFire.position, transform.rotation);
        Bullet bullet = newBullet.GetComponent<Bullet>();
        Vector2 bulletDirection = transform.right;
        bullet.InitializeBullet(baseDamageAmount, baseBulletSpeed, baseBulletSize, bulletDirection);
    }
}
