using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    private SpawnInfo randomSpawnInfo;
    private Transform randomSpawnPoint;
    private float spawnDelay;
    private int obstacleCount;
    private float initialObstacleStartTime;
    
    private void OnEnable() => GameStateManager.OnStateChange += OnGameStateChanged;

    private void OnDisable() => GameStateManager.OnStateChange -= OnGameStateChanged;

    private void OnGameStateChanged(GameState newState)
    {
        if (newState == GameState.Game)
        {
            StartCoroutine(SpawnObstacleTimely());
        }
    }

    private void Start()
    {
        obstacleCount = LevelDataContainer.Instance.numOfObstacles;
        spawnDelay = LevelDataContainer.Instance.spawnDelay;
        initialObstacleStartTime = LevelDataContainer.Instance.initialObstacleStartTime;
    }

    [System.Serializable]
    public class SpawnInfo
    {
        public GameObject gameObject;
        public List<Transform> spawnPoints;
        public Vector3 position;
    }

    public List<SpawnInfo> spawnInfos;

    private void SpawnObstacle(GameObject prefab, Transform spawnPoint, Vector3 position)
    {
        Vector3 combinedPosition = spawnPoint.position + position;
        
        GameObject instantiatedObject = Instantiate(prefab, combinedPosition, prefab.transform.rotation);

        instantiatedObject.transform.position = combinedPosition;
        instantiatedObject.transform.rotation = prefab.transform.rotation;

    }

    private SpawnInfo GetRandomSpawnInfo()
    {
        int randomIndex = Random.Range(0, spawnInfos.Count);
        return spawnInfos[randomIndex];
    }

    private Transform GetRandomSpawnPoint(List<Transform> spawnPoints)
    {
        int randomIndex = Random.Range(0, spawnPoints.Count);
        return spawnPoints[randomIndex];
    }
    
    private IEnumerator SpawnObstacleTimely()
    {
        yield return new WaitForSeconds(initialObstacleStartTime);
        for(int i = 0 ; i < obstacleCount ; i++)
        {
            randomSpawnInfo = GetRandomSpawnInfo();
            randomSpawnPoint = GetRandomSpawnPoint(randomSpawnInfo.spawnPoints);
            yield return new WaitForSeconds(spawnDelay);
            SpawnObstacle(randomSpawnInfo.gameObject, randomSpawnPoint, randomSpawnInfo.position);
        }
    }
}
