using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    // Properties of the enemy 
    // Controls: Enemy hp, dmg and attack speed. 
    
    public int baseHP = 5;
    public int baseDmg = 1;
    public float baseAttackSpeed = 1f;

    // Set values that can be retrieved and altered by other scripts (Levelling system). 
   //public int Damage = currentDmg;
   //public int AttackSpeed = currentAttackSpeed;

    public GameObject bulletPrefabModel;
    public float bulletAttackSpeed = 1f;
    public float bulletTimer = 2f;

}
