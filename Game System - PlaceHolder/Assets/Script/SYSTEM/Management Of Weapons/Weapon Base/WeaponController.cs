using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
///  Base script for all weapon controller
/// </summary>
public class WeaponController : MonoBehaviour
{
    [Header("Weapon Stats")]
    public List<WeaponScriptableObject> weaponDataList = new List<WeaponScriptableObject>();
    private List<float> currentCooldowns = new List<float>();

    protected Player pm;

    protected virtual void Start()
    {
        pm = FindObjectOfType<Player>();

        // Initialize cooldown list
        foreach (var weapon in weaponDataList)
        {
            currentCooldowns.Add(weapon.CoolDownDuration);
        }
    }

    protected virtual void Update()
    {
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

        // GarlicBehaviour does not need Initialize, it's auto-handled by Start()
        // So no need to do anything special here
    }
}
