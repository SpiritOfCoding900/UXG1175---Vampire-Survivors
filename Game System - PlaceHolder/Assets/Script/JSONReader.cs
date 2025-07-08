using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class JSONReader : MonoBehaviour
{

    public TextAsset Enemy;
    [System.Serializable]
    public class EnemyClass
    {
        public string name;
        public int health;
        public int damageDealt;
        public int movementSpeed;
        public int firerate;
        public int weaponSwingSpeed;
        public string enemyPattern;
    }

    [System.Serializable]
    public class EnemyClassList
    {
        public EnemyClass[] enemyClasses;
    }

    public EnemyClassList enemyClassList = new EnemyClassList();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        enemyClassList = JsonUtility.FromJson<EnemyClassList>(Enemy.text);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
