using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyWave : MonoBehaviour
{
    public List<Enemy> enemies = new List<Enemy>();
    public int currWave;
    public int waveValue;
    public List<GameObject> enemiesToSpawn = new List <GameObject>();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GenerateWave();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void GenerateWave()
    {
        waveValue = currWave * 10;
        GenerateEnemies();
    }

    public void GenerateEnemies()
    {
        /*
        List <GameObject> generatedEnemies = new List <GameObject>();
        while (waveValue > 0)
        {
            int randEnemyId = Random.Range(0, enemies.Count);
            int randEnemyCost = enemies[randEnemyId].cost;

            if (waveValue - randEnemyCost >= 0)
            {
                generatedEnemies.Add(enemies[randEnemyId].enemyPrefab);
                waveValue -= randEnemyCost;
            }

            else if (waveValue <= 0 )
            {
                break;
            }
        }

        enemiesToSpawn.Clear();
        enemiesToSpawn = generatedEnemies;
        //*/
    }
}
