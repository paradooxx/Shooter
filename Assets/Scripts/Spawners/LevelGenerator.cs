using UnityEngine;
using UnityEngine.AI;

public class LevelGenerator : MonoBehaviour
{
    private GameDataManager gameDataManager;
    [SerializeField] private GameObject enemySpawner;
    [SerializeField] private GameObject playerMuxSpawner;

    private int level;

    private void Start()
    {
        gameDataManager = GameDataManager.Instance;
        LoadLevelData();
    }

    private void OnEnable()
    {
        
    }

    private void LoadLevelData()
    {
        level = LevelDataContainer.Instance.level;
    }    

    public void NextLevel()
    {
        // level++;
        // gameDataManager.Level = level;
        gameDataManager.Level ++;
        gameDataManager.LevelReached ++;
    }
}
