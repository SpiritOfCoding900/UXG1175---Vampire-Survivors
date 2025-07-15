using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemySpawnManager : SimpleSingleton<EnemySpawnManager>
{
    public GameObject meleeEnemyPrefab;
    public GameObject rangedEnemyQuickFirePrefab;
    public GameObject rangedEnemySlowFirePrefab;

    public float waveDowntime = 5f;
    public float enemySpawnDelay = 0.5f;
    public float initalMaxEnemies = 10f;
    public float perWaveMaxEnemyIncrement = 5f;
    public int maxWaves = 5;

    public Vector2 spawnAreaMin = new Vector2(-30, -30);
    public Vector2 spawnAreaMax = new Vector2(30, 30);
    public float spawnZ = 0f;

    public int currentWave = 0;
    private int enemiesRemaining = 0;
    private int maxEnemiesForWave;

    private List<EnemyController> activeEnemies = new List<EnemyController>();
    private bool waveInProgress = false;

    public delegate void EnemyVanquished();
    public static event EnemyVanquished OnEnemyVanquished;

    void OnEnable()
    {
        OnEnemyVanquished += HandleEnemyVanquished;
    }

    void OnDisable()
    {
        OnEnemyVanquished += HandleEnemyVanquished;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(StartAfterDelay(waveDowntime));
    }

    private void HandleEnemyVanquished()
    {
        enemiesRemaining--;
        for (int i = activeEnemies.Count  - 1; i >= 0; i--)
        {
            if (activeEnemies[i] == null)
            {
                activeEnemies.RemoveAt(i);
            }
        }

        if (enemiesRemaining <= 0 && waveInProgress)
        {
            waveInProgress = false;
            StartCoroutine(StartAfterDelay(waveDowntime));
        }
    }

    public int GetEnemiesRemaining()
    {
        return enemiesRemaining;
    }

    private IEnumerator StartAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        StartNextWave();
    }

    private void StartNextWave()
    {
        currentWave++;
        waveInProgress = true;
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
