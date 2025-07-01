using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    // Set player stats for HP, Damage, attack speed

    [SerializeField] private int minhp = 1;
    [SerializeField] private int maxhp = 100;
    [SerializeField] private int CurrentHP;
    [SerializeField] private int BaseDmg = 1;
    [SerializeField] private int MaxDmg = 10;
    [SerializeField] private int CurrentDmg;
    [SerializeField] private float BaseAttackSpeed = 1;
    [SerializeField] private float MaxAttackSpeed = 20;
    [SerializeField] private float CurrentAttackSpeed;


    void Awake()
    {
        CurrentHP = 3;
        CurrentDmg = BaseDmg;
        CurrentAttackSpeed = BaseAttackSpeed;

        Debug.Log(string.Format("Game Start. Base HP = {0}", CurrentHP));
        Debug.Log(string.Format("Game Start. Base DMG = {0}", CurrentDmg));
        Debug.Log(string.Format("Game Start. Base Attack Speed = {0}", CurrentAttackSpeed));
    }
}

