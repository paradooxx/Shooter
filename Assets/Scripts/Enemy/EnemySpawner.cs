using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private LayerMask collisionMask;
    [SerializeField] private Transform[] spawnPoints;

    [SerializeField] [Tooltip("Add new Enemy Prefabs here")] 
    private EnemyPrefab[] enemyPrefabs;
    [SerializeField] private BossPrefab[] bossPrefabs;
    [SerializeField] private GameObject bossComingMessage;

    public static int totalEnemies;
    private bool isBossLevel;
    
    [Tooltip("Lower value corresponds to higher difficulty")]
    private float initialEnemySpawnTime,
                  bossSpawnDelay,
                  enemySpawnDelay;

    public UnityEvent onCoroutineStopped;

    private void OnEnable()
    {
        GameStateManager.OnStateChange += OnGameStateChanged;
    }

    private void OnDisable() => GameStateManager.OnStateChange -= OnGameStateChanged;

    private void OnGameStateChanged(GameState newState)
    {
        if (newState == GameState.Game)
        {
            StartSpawning(); 
        }
    }

    private void Start()
    {
        for (int i = 0; i < enemyPrefabs.Length; i++)
        {
            if (i < LevelDataContainer.Instance.enemyCount.Length)
            {
                enemyPrefabs[i].SetCount(LevelDataContainer.Instance.enemyCount[i]);
            }
            else
            {
                enemyPrefabs[i].SetCount(0);
            }
        }
        isBossLevel = LevelDataContainer.Instance.isBossLevel;
        initialEnemySpawnTime = LevelDataContainer.Instance.initialEnemySpawnTime;
        enemySpawnDelay = LevelDataContainer.Instance.enemySpawnDelay;
        bossSpawnDelay = LevelDataContainer.Instance.bossSpawnDelay;

        if(isBossLevel)
        {
            totalEnemies = CalculateTotalEnemyCount() + 1;
        }
        else
        {
            totalEnemies = CalculateTotalEnemyCount();
        }
    }
    
    public int CalculateTotalEnemyCount()
    {
        int totalCount = 0;
        foreach (var enemyPrefab in enemyPrefabs)
        {
            totalCount += enemyPrefab.GetCount();
        }
        return totalCount;
    }

    public int CalculateTotalBossCount()
    {
        int totalCount = 0;
        foreach(var boss in bossPrefabs)
        {
            totalCount += boss.GetCount();
        }
        return totalCount;
    }

    public void SpawnEnemy(EnemyPrefab enemyPrefab)
    {
        if (enemyPrefab.count > 0)
        {
            int spawnIndex = Random.Range(0, spawnPoints.Length);
            Instantiate(enemyPrefab.prefab, spawnPoints[spawnIndex].position, Quaternion.identity);
            enemyPrefab.count--;
        }
        else
        {
           return;
        }
    }

    public IEnumerator SpawnBoss(BossPrefab bossPrefab)
    {
        yield return new WaitForSeconds(bossSpawnDelay);
        bossComingMessage?.SetActive(true);
        SFXManager.Instance.PlaySound(SoundType.Boss, transform);
        int spawnIndex = Random.Range(0, spawnPoints.Length);
        Instantiate(bossPrefab.prefab, spawnPoints[spawnIndex].position, Quaternion.identity);   
    }

    public void SpawnBoss()
    {
        
        StartCoroutine(SpawnBoss(bossPrefabs[Random.Range(0, bossPrefabs.Length)]));
    }

    public void StartSpawning()
    {
        StartCoroutine(SpawnAllEnemiesCoroutine());
    }

    private IEnumerator SpawnAllEnemiesCoroutine()
    {
        List<EnemyPrefab> availableEnemies = new List<EnemyPrefab>(enemyPrefabs);
        yield return new WaitForSeconds(initialEnemySpawnTime);

        while (availableEnemies.Count > 0)
        {
            int randomIndex = Random.Range(0, availableEnemies.Count);
            EnemyPrefab enemyPrefab = availableEnemies[randomIndex];

            if (enemyPrefab.GetCount() > 0)
            {
                SpawnEnemy(enemyPrefab);
                yield return new WaitForSeconds(enemySpawnDelay);
            }

            if (enemyPrefab.GetCount() <= 0)
            {
                availableEnemies.RemoveAt(randomIndex);
            }
        }
        if(isBossLevel)
        {
            onCoroutineStopped?.Invoke();
        }
    }
}
