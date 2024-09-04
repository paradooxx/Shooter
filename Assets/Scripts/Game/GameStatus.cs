using UnityEngine;
using UnityEngine.Events;

public class GameStatus : MonoBehaviour
{
    public UnityEvent onGameWin;
    public UnityEvent onGameLoose;

    public static GameStatus Instance;

    [SerializeField] private LevelGenerator levelGenerator;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    public void GameWin()
    {
        if(EnemySpawner.totalEnemies == 0)
        {
            GameStateManager.Instance.SetState(GameState.Win);
            levelGenerator.NextLevel();    
        }
    }

    public void GameLoose()
    {
        GameStateManager.Instance.SetState(GameState.Loose);
    }
}
