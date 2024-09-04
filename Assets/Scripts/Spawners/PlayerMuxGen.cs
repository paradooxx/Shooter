using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMuxGen : MonoBehaviour
{
    [SerializeField] private GameObject[] playerMuxObjects;
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private PlayerStats[] playerStats;

    private int numberOfObjectsToGenerate = 10;
    private float minTimeBetweenSpawns;
    private float maxTimeBetweenSpawns;
    private float initialMultiplierStartTime; 

    private void OnEnable() => GameStateManager.OnStateChange += OnGameStateChanged;

    private void OnDisable() => GameStateManager.OnStateChange -= OnGameStateChanged;

    private void Start()
    {
        numberOfObjectsToGenerate = LevelDataContainer.Instance.numOfMultiplierObjects;
        minTimeBetweenSpawns = LevelDataContainer.Instance.minTimeBetnObjs;
        maxTimeBetweenSpawns = LevelDataContainer.Instance.maxTimeBetnObjs;
        initialMultiplierStartTime = LevelDataContainer.Instance.initialMultiplierStartTime;
    }

    private void OnGameStateChanged(GameState newState)
    {
        if (newState == GameState.Game)
        {
            StartCoroutine(GeneratePlayerMuxCoroutine());
        }
    }

    private IEnumerator GeneratePlayerMuxCoroutine()
    {
        yield return new WaitForSeconds(initialMultiplierStartTime);
        for (int i = 0; i < numberOfObjectsToGenerate; i++)
        {
            GeneratePlayerMux();

            float waitTime = Random.Range(minTimeBetweenSpawns, maxTimeBetweenSpawns);
            yield return new WaitForSeconds(waitTime);
        }
    }

    private void GeneratePlayerMux()
    {
        List<int> unlockedIndices = new List<int>();

        for (int i = 0; i < playerStats.Length; i++)
        {
            if (playerStats[i].isUnlocked)
            {
                unlockedIndices.Add(i);
            }
        }

        if (unlockedIndices.Count == 0)
        {
            Debug.LogWarning("No unlocked objects to generate.");
            return;
        }

        int randomIndex = unlockedIndices[Random.Range(0, unlockedIndices.Count)];
        GameObject selectedObject = playerMuxObjects[randomIndex];

        Vector3 spawnPosition = Vector3.zero;
        if (spawnPoints != null && spawnPoints.Length > 0)
        {
            int randomSpawnIndex = Random.Range(0, spawnPoints.Length);
            spawnPosition = spawnPoints[randomSpawnIndex].position;
        }
        Instantiate(selectedObject, spawnPosition + new Vector3(0, 1, 0), Quaternion.Euler(new Vector3(0, 0, 90)));
    }
}
