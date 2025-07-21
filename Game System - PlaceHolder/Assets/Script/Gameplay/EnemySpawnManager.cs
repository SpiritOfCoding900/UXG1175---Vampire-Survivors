using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemySpawnManager : SimpleSingleton<EnemySpawnManager>
{
    public GameObject meleeEnemyPrefab;
    public GameObject rangedEnemyQuickFirePrefab;
    public GameObject rangedEnemySlowFirePrefab;

    public float waveDowntime = 2f;
    public float enemySpawnDelay = 0.1f;
    public float initalMaxEnemies = 5f;
    public float perWaveMaxEnemyIncrement = 5f;
    public int maxWaves = 5;

    public float viewPointSpawn = 2f;
    public float spawnZ = 0f;

    public int currentWave = 0;
    private int enemiesRemaining = 0;
    private int maxEnemiesForWave;

    private List<EnemyController> activeEnemies = new List<EnemyController>();
    private bool waveInProgress = false;
    private bool gameFinished = false;

    private Camera mainCamera;

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
        mainCamera = Camera.main;
        StartCoroutine(StartAfterDelay(waveDowntime));
    }

    private void HandleEnemyVanquished()
    {
        if (gameFinished)
        {
            return;
        }
        enemiesRemaining--;
        for (int i = activeEnemies.Count -1; i >= 0; i--)
        {
            if (activeEnemies[i] == null)
            {
                activeEnemies.RemoveAt(i);
            }
        }

        if (enemiesRemaining <= 0 && waveInProgress)
        {
            waveInProgress = false;
            if (currentWave >= maxWaves)
            {
                gameFinished = true;
            }
            else
            {
                StartCoroutine(StartAfterDelay(waveDowntime));
            }
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
        if (gameFinished)
        {
            return;
        }
        currentWave++;
        waveInProgress = true;

        maxEnemiesForWave = (int)(initalMaxEnemies + (currentWave - 1) * perWaveMaxEnemyIncrement);
        maxEnemiesForWave = (int)(Mathf.Max(10, maxEnemiesForWave));

        int noOfEnemiesToSpawn = Random.Range (1, maxEnemiesForWave +1);
        enemiesRemaining = noOfEnemiesToSpawn;

        StartCoroutine(SpawnWaveEnemies(noOfEnemiesToSpawn));
    }    
    
    private IEnumerator SpawnWaveEnemies (int count)
    {
        for (int i = 0; i< count; i++)
        {
            if (gameFinished)
            {
                yield break;
            }
            SpawnRandom();
            yield return new WaitForSeconds(enemySpawnDelay);
        }
    }

    private void SpawnRandom()
    {
        GameObject targetEnemyPrefab = GetRandomEnemy();
        
        float randomViewPointSpawnX = Random.Range (viewPointSpawn, 1f - viewPointSpawn);
        float randomViewPointSpawnY = Random.Range(viewPointSpawn, 1f - viewPointSpawn);
        Vector3 spawnPosition = mainCamera.ViewportToWorldPoint(new Vector3 ( randomViewPointSpawnX,  randomViewPointSpawnY, spawnZ));
        spawnPosition.z = spawnZ;

        GameObject newEnemyGO = Instantiate(targetEnemyPrefab, spawnPosition, Quaternion.identity);
        EnemyController enemyController = newEnemyGO.GetComponent<EnemyController>();

        if (enemyController != null )
        {
            JSONReader.EnemyClass dataForEnemy = JSONReader.Instance.GetEnemyDataByName(targetEnemyPrefab.name);
            if (dataForEnemy != null ) 
            {
                enemyController.Initialize(dataForEnemy, currentWave);
                activeEnemies.Add(enemyController);
            }
        }
    }

    private GameObject GetRandomEnemy()
    {
        List<GameObject> allEnemies = new List<GameObject>();
        allEnemies.Add(meleeEnemyPrefab);
        allEnemies.Add(rangedEnemyQuickFirePrefab);
        allEnemies.Add(rangedEnemySlowFirePrefab);

        int randomIndex = Random.Range (0, allEnemies.Count);
        return allEnemies[randomIndex];
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
