using UnityEngine;

public class EnemyStats : MonoBehaviour
{

    [SerializeField] private int minHp = 1;
    [SerializeField] private int maxHp = 5;
    [SerializeField] private int currentHP;

    [SerializeField] private int baseDmg = 1;
    [SerializeField] private int maxDmg = 5;
    [SerializeField] private int currentDmg;

    [SerializeField] private float baseAttackSpeed = 1;
    [SerializeField] private float maxAttackSpeed = 10;
    [SerializeField] private float currentAttackSpeed;

    void Awake()
    {
        currentHP = 3;
        currentDmg = baseDmg;
        currentAttackSpeed = baseAttackSpeed;

        Debug.Log(string.Format("Game Start. Base Enemy HP = {0}" , currentHP));
        Debug.Log(string.Format("Game Start. Base Enemy DMG = {0}", currentDmg));
        Debug.Log(string.Format("Game Start. Base Enemy Attack Speed = {0}", currentAttackSpeed));

    }

    // Set values that can be retrieved and altered by other scripts (Levelling system). 
   public int Damage = currentDmg;
   public int AttackSpeed = currentAttackSpeed;

    private void Die()
    {
        Destroy(gameObject);
    }
}
