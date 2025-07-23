using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
///  Base script for all weapon controller
/// </summary>
public class WeaponController : SimpleSingleton<WeaponController>
{
    [Header("Weapon Stats")]
    public List<WeaponScriptableObject> weaponDataList = new List<WeaponScriptableObject>();
    private List<float> currentCooldowns = new List<float>();

    protected Player pm;

    
    protected virtual void Awake()
    {
        //// Initialize cooldown list
        //foreach (var weapon in weaponDataList)
        //    currentCooldowns.Add(weapon.CoolDownDuration);
    }
    protected virtual void Start()
    {
        pm = FindObjectOfType<Player>();

        // Initialize cooldown list
        foreach (var weapon in weaponDataList)               // This is
            currentCooldowns.Add(weapon.CoolDownDuration);   // The Problem
    }

    protected virtual void Update()
    {
        // Initialize cooldown list
        foreach (var weapon in weaponDataList)
            currentCooldowns.Add(weapon.CoolDownDuration);

        for (int i = 0; i < weaponDataList.Count; i++)
        {
            currentCooldowns[i] -= Time.deltaTime;

            if (currentCooldowns[i] <= 0f)
            {
                Attack(weaponDataList[i]);
                currentCooldowns[i] = weaponDataList[i].CoolDownDuration;
            }
        }
    }

    public bool AddWeapon(WeaponScriptableObject newWeapon)
    {
        if (weaponDataList.Contains(newWeapon))
        {
            Debug.LogWarning($"Weapon {newWeapon.name} is already in the list!");
            return false;
        }

        weaponDataList.Add(newWeapon);
        currentCooldowns.Add(0f);  // Use "Add(0f)" to allow immediate fire OR use "Add(newWeapon.CoolDownDuration)"
        return true;
    }

    protected virtual void Attack(WeaponScriptableObject weaponData)
    {
        GameObject weaponObj = Instantiate(weaponData.Prefab, transform.position, Quaternion.identity);
        weaponObj.transform.parent = transform;

        // Optional: apply knife direction if it's a knife
        var knife = weaponObj.GetComponent<KnifeBehaviour>();
        if (knife != null && pm != null)
        {
            knife.DirectionChecker(pm.lastMovedVector);
        }
    }
}
