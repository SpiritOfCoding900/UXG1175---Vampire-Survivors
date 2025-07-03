using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///  Base script for all Melee Weapon
/// </summary>
public class MeleeWeaponBehaviour : MonoBehaviour
{
    protected Vector3 direction;
    public float destroyAfterSeconds;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected virtual void Start()
    {
        Destroy(gameObject, destroyAfterSeconds);
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        
    }
}
