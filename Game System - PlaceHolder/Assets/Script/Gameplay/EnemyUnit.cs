using UnityEngine;
using System.Collections;
using Unity.VisualScripting;

public class EnemyUnit : MonoBehaviour
{
    // Enemy unit. 
    // Required properties:
    // Minimum HP, Maximum HP, Minimum Damage, Maximum Damage, Minimum attack speed, maximum attack speed (must be able to be increased as level progresses.)
    // Current hp, current dmg, current attack speed. 

    // Required functions
    // Shooting (shooting frequency tied to attack speed), Damage taking, Despawning. 

    [SerializeField] private EnemyStats enemyStats;
    [SerializeField] private Transform pointOfFire;
    private bool shooting = true;

    private int currentHP;
    private int currentDmg;
    private float currentAttackSpeed;

    private void Awake()
    {
        currentHP = enemyStats.baseHP;
        currentDmg = enemyStats.baseDmg;
        currentAttackSpeed = enemyStats.baseAttackSpeed;

        StartCoroutine(ShootRoutine());
    }

    private void Shoot()
    {
        GameObject newBullet = Instantiate(enemyStats.bulletPrefabModel, pointOfFire.position, pointOfFire.rotation);
        EnemyBulletBehaviour bulletScript = newBullet.GetComponent <EnemyBulletBehaviour>();
        bulletScript.Initialize(enemyStats, 10f);
    }
    
    
    
    private IEnumerator ShootRoutine()
    {
        while (true)
        {
            if (shooting)
            {
                Shoot();
                shooting = false;
                float delay = 1f / currentAttackSpeed;
                yield return new WaitForSeconds(delay);
                shooting = true;
            }
            yield return null; 
        }
    }


    
    //TODO: OnTriggerEnter for enemy unit when player bullet impacts the enemy, run TakeDamage routine. 
    
    public void TakeDamage (int playerDamage)
    {
        // TODO: create player bullet script and do playerDamage
        currentHP -= playerDamage;
        if (currentHP <= 0)
        {
            Die();
        }
    }
    private void Die()
    {
        Destroy (gameObject);
    }    
}
