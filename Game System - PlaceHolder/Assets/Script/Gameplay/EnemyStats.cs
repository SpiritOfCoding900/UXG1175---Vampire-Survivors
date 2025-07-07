using UnityEngine;

public class EnemyStats : ScriptableObject
{
    // Properties of the enemy 
    // Controls: Enemy hp, dmg and attack speed. 

    public int baseHP = 5;
    public int baseDmg = 1;
    public float baseAttackSpeed = 2f;

    // Properties of the bullet that the enemy will shoot. 
    // Controls: Bullet spawn and despawn, bullet travel speed. 

    public GameObject bulletPrefabModel;
    public float bulletAttackSpeed = 2f;
    public float bulletTimer = 2f;
}
