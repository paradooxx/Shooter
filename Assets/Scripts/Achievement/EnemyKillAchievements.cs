using System;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.UI;

public class EnemyKillAchievements : MonoBehaviour
{
    [Header("Enemy")]
    [SerializeField] private TMP_Text enemyCountText;     // 0/0 form
    [SerializeField] private TMP_Text descriptionText;
    [SerializeField] private TMP_Text coinDisplayText;
    [SerializeField] private Button enemyAchievementButton;
    [SerializeField] private AchievementData[] enemyAchievementList;
    public int enemyKilledCount;
    public static bool isEnemyAchievementComplete = false;
    private AchievementManager achievementManager;

    private event Action notificationEvent;

    private void Awake()
    {
        
        achievementManager = GetComponent<AchievementManager>();
    }

    private void OnEnable() => notificationEvent += achievementManager.NotificationUpdate;

    private void OnDisable() => notificationEvent += achievementManager.NotificationUpdate;

    private void Start()
    {
        enemyAchievementButton.onClick.AddListener(() => GiveReward(enemyAchievementList[GameDataManager.Instance.EnemyAchievementIndex].rewardAmount));

        if(isEnemyAchievementComplete)
        {
            enemyAchievementButton.image.sprite = achievementManager.greenButton;
            notificationEvent?.Invoke();
        }
        descriptionText.text = "Kill " + enemyAchievementList[GameDataManager.Instance.EnemyAchievementIndex].achievementCount + " enemies";
        AchivementCompleteCheck();
        EnemyKillUpdateUI();
    }

    private void EnemyKillUpdateUI()
    {
        enemyCountText.text = GameDataManager.Instance.EnemyKilledCount.ToString() + "/" + enemyAchievementList[GameDataManager.Instance.EnemyAchievementIndex].achievementCount.ToString();
        coinDisplayText.text = enemyAchievementList[GameDataManager.Instance.EnemyAchievementIndex].rewardAmount.ToString();
    }

    private void AchivementCompleteCheck()
    {
        if(IsAchievementComplete(GameDataManager.Instance.EnemyAchievementIndex))
        {
            isEnemyAchievementComplete = true;
        }

        if(isEnemyAchievementComplete)
        {
            enemyAchievementButton.image.sprite = achievementManager.greenButton;
        }

    }
    private bool IsAchievementComplete(int index)
    {
        if(GameDataManager.Instance.EnemyKilledCount >= enemyAchievementList[index].achievementCount)
            return true;
        else
            return false;
    }

    private void GiveReward(int amount)
    {
        if(isEnemyAchievementComplete)
        {
            Coins.CounterUpdateUIandCoin(amount, true);
            CoinAnimation.Instance.CountCoins();
            enemyAchievementButton.image.sprite = achievementManager.redButton;
            notificationEvent?.Invoke();
            isEnemyAchievementComplete = false;
            GameDataManager.Instance.EnemyAchievementIndex ++;
            EnemyKillUpdateUI();
            AchivementCompleteCheck();
            descriptionText.text = "Kill " + enemyAchievementList[GameDataManager.Instance.EnemyAchievementIndex].achievementCount + " enemies";
            notificationEvent?.Invoke();
        }
    }
}
