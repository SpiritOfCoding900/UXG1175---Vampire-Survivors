using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeBehaviour : ProjectileWeaponBehaviour
{
    KnifeController kc;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        base.Start();
        kc = FindObjectOfType<KnifeController>();
    }

    // Update is called once per frame
    protected override void Update()
    {
        //base.Update();
        transform.position += direction * kc.speed * Time.deltaTime;
    }
}
