using TMPro;
using UnityEngine;

public class EnemyBar : MonoBehaviour
{
    [SerializeField] private EnemySpawner enemySpawner;
    [SerializeField] private GameObject enemyBarLife;
    [SerializeField] private TMP_Text enemyCount;
    [SerializeField] private TMP_Text enemyText;

    private int initialEnemyCount;
    private float enemyPercent;
    
    private void Awake()
    {
        
    }

    private void Start()
    {
        initialEnemyCount = EnemySpawner.totalEnemies;
        enemyCount.text = (initialEnemyCount - EnemySpawner.totalEnemies).ToString();
        enemyText.text = "Defeat " + initialEnemyCount + " Enemies";
    }

    private void Update()
    {
        enemyCount.text = (initialEnemyCount - EnemySpawner.totalEnemies).ToString();
        enemyPercent = (initialEnemyCount - (float)EnemySpawner.totalEnemies) / initialEnemyCount;
        enemyBarLife.transform.localScale = new Vector3(enemyPercent, 1, 1);
    }
}
