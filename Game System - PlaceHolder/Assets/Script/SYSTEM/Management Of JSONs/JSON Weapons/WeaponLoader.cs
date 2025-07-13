using UnityEngine;
using System.Collections.Generic;



[System.Serializable]
public class Weapon
{
    public string weaponName;
    public int damage;
    public float range;
    public float coolDownDuration;
    public string description;
}

[System.Serializable]
public class WeaponList
{
    public List<Weapon> weapons;
}

public class WeaponLoader : MonoBehaviour
{
    public WeaponList myWeaponList = new WeaponList();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        TextAsset jsonFile = Resources.Load<TextAsset>("weapons");
        if (jsonFile != null)
        {
            myWeaponList = JsonUtility.FromJson<WeaponList>(jsonFile.text);
        }
        else
        {
            Debug.LogError("Could not find player.json in Resources folder.");
        }
    }
}
