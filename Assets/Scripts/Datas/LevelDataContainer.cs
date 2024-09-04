using UnityEngine;

public class LevelDataContainer : MonoBehaviour
{
    [SerializeField] private LevelData[] levelData;

    public int level { get; private set; } 
    
    //Enemues
    public int[] enemyCount { get; private set; }
    public bool isBossLevel { get; private set; }
    public float initialEnemySpawnTime { get; private set; }
    public float enemySpawnDelay { get; private set; }
    public float bossSpawnDelay { get; private set; }

    //PlayerMultipliers
    public int numOfMultiplierObjects { get; private set; }
    public float initialMultiplierStartTime { get; private set; }
    public float minTimeBetnObjs { get; private set; }    
    public float maxTimeBetnObjs { get; private set; }

    //ObstacleSpawners
    public int numOfObstacles { get; private set; }
    public float spawnDelay { get; private set; }
    public float initialObstacleStartTime { get; private set; }

    //RewardGates
    public int numOfGates { get; private set; }
    public float initialGateStartTime { get; private set; }
    public float minTimeBetnGates { get; private set; }
    public float maxTimeBetnGates { get; private set; }

    public static LevelDataContainer Instance;
    public int index;  

    private void Awake()
    {
        index = GameDataManager.Instance.Level;
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }

        // initializing the enemyCount array based on the level data
        if (levelData != null && levelData.Length > 0)
        {
            enemyCount = new int[levelData[index - 1].enemyCount.Length];
            SetLevelData(index - 1);
        }
        else
        {
            Debug.LogError("Level data is not properly set or is empty.");
            return;
        }
        Debug.Log("LEVEL: " + level);
    }

    private void SetLevelData(int index)
    {
        if(index < 0 || index >= levelData.Length)
        {
            Debug.LogError("Invalid level index.");
            return;
        }

        level = levelData[index].level;
        for (int i = 0; i < levelData[index].enemyCount.Length; i++)
        {
            enemyCount[i] = levelData[index].enemyCount[i];
        }

        isBossLevel = levelData[index].isBossLevel;
        initialEnemySpawnTime = levelData[index].initialEnemySpawnTime;
        enemySpawnDelay = levelData[index].enemySpawnDelay;
        bossSpawnDelay = levelData[index].bossSpawnDelay;

        numOfMultiplierObjects = levelData[index].numOfMultiplierObjects;
        minTimeBetnObjs = levelData[index].minTimeBetnObjs;
        maxTimeBetnObjs = levelData[index].maxTimeBetnObjs;
        initialMultiplierStartTime = levelData[index].initialMultiplierStartTime;

        numOfObstacles = levelData[index].numOfObstacles;
        spawnDelay = levelData[index].spawnDelay;
        initialObstacleStartTime = levelData[index].initialObstacleStartTime;

        numOfGates = levelData[index].numOfGates;
        initialGateStartTime = levelData[index].initialGateStartTime;
        minTimeBetnGates = levelData[index].minTimeBetnGates;
        maxTimeBetnGates = levelData[index].maxTimeBetnGates;
    }
}
