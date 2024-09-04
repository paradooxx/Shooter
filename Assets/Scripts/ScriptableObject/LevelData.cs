using UnityEngine;

[CreateAssetMenu(fileName = "Level", menuName = "Level/LevelData", order = 1)]
public class LevelData : ScriptableObject
{   
    [Header("Level")]
    public int level;

    [Space]
    [Header("Enemies")]
    public int[] enemyCount;
    public bool isBossLevel;
    public float initialEnemySpawnTime;
    public float enemySpawnDelay;
    public float bossSpawnDelay;

    [Space]
    [Header("PlayerMultipliers")]
    public int numOfMultiplierObjects;
    public float initialMultiplierStartTime,
                 minTimeBetnObjs,
                 maxTimeBetnObjs;
    
    [Space]
    [Header("ObstacleSpawners")]
    public int numOfObstacles;
    public float spawnDelay,
                 initialObstacleStartTime;

    [Space]
    [Header("RewardGates")]
    public int numOfGates;
    public float initialGateStartTime,
                 minTimeBetnGates,
                 maxTimeBetnGates;
}
