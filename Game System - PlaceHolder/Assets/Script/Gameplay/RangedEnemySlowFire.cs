using UnityEngine;

public class RangedEnemySlowFire : EnemyController
{
    private Transform playerTransform;
    private float fireCooldown;

    public GameObject bulletPrefab;
    public Transform firePoint;
    public float rotationSpeed = 5f;

    void Awake()
    {
        GameObject playerGO = GameObject.FindGameObjectWithTag("Player");
        playerTransform = playerGO.transform;
        if (firePoint == null)
        {
            firePoint = transform;
        }

        fireCooldown = 0f;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        fireCooldown = 1f / baseFireRate;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 lookDirection = (playerTransform.position - transform.position);
        float angle = Mathf.Atan2 (lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.Euler(0, 0, angle);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);

        fireCooldown -= Time.deltaTime;
        if (fireCooldown <= 0)
        {
            ShootBullet();
            fireCooldown = 1f/ baseFireRate;
        }
    }

    private void ShootBullet()
    {
        GameObject newBulletGO = Instantiate (bulletPrefab, firePoint.position, transform.rotation);
        Bullet bullet = newBulletGO.GetComponent<Bullet>();
        Vector2 bulletDirection = transform.right;
        bullet.InitializeBullet(baseDamageAmount, baseBulletSpeed, baseBulletSize, bulletDirection);
    }
}
