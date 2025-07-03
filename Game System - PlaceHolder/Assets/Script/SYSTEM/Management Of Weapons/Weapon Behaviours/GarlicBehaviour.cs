using UnityEngine;

public class GarlicBehaviour : MeleeWeaponBehaviour
{
    GarlicController gc;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        base.Start();
        gc = FindObjectOfType<GarlicController>();
    }

    // Update is called once per frame
    protected override void Update()
    {
        //base.Update();
        //transform.position += direction * kc.speed * Time.deltaTime;
    }
}
