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

    private void LoadLevelData()
    {
        level = LevelDataContainer.Instance.level;
    }    

    public void NextLevel()
    {
        gameDataManager.Level ++;
        gameDataManager.LevelReached ++;
    }
}
