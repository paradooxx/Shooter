using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelProgressionAchievements : MonoBehaviour
{
    [Header("Enemy")]
    [SerializeField] private TMP_Text levelReachedText;
    [SerializeField] private TMP_Text descriptionText;
    [SerializeField] private TMP_Text coinDisplayText;
    [SerializeField] private Button levelReachedButton;
    [SerializeField] private AchievementData[] levelReachedList;
    public int levelReachedCount;
    public static bool isLevelReached = false;
    private AchievementManager achievementManager;

    private event Action notificationEvent;

    private void Awake()
    {
        LevelReachedUpdateUI();
        achievementManager = GetComponent<AchievementManager>();
        AchievementCompleteCheck();
    }

    private void OnEnable() => notificationEvent += achievementManager.NotificationUpdate;

    private void OnDisable() => notificationEvent += achievementManager.NotificationUpdate;

    private void Start()
    {
        levelReachedButton.onClick.AddListener(() => GiveReward(levelReachedList[GameDataManager.Instance.LevelIndex].rewardAmount));

        if(isLevelReached)
        {
            levelReachedButton.image.sprite = achievementManager.greenButton;
            notificationEvent?.Invoke();
        }
        descriptionText.text = "Complete " + levelReachedList[GameDataManager.Instance.LevelIndex].achievementCount + " levels";
    }

    private void LevelReachedUpdateUI()
    {
        levelReachedText.text = GameDataManager.Instance.LevelReached.ToString() + "/" + levelReachedList[GameDataManager.Instance.LevelIndex].achievementCount.ToString();
        coinDisplayText.text = levelReachedList[GameDataManager.Instance.LevelIndex].rewardAmount.ToString();
    }

    private void AchievementCompleteCheck()
    {
        if(GameDataManager.Instance.IsLevelChallengeFinished == true)
        {
            levelReachedButton.image.sprite = achievementManager.brownButton;
            levelReachedButton.GetComponentInChildren<TMP_Text>().text = "Completed";
            return;
        }

        if(IsAchievementComplete(GameDataManager.Instance.LevelIndex))
        {
            isLevelReached = true;
        }

        if(isLevelReached)
        {
            levelReachedButton.image.sprite = achievementManager.greenButton;
        }
    }

    private bool IsAchievementComplete(int index)
    {
        if(GameDataManager.Instance.LevelReached >= levelReachedList[index].achievementCount)
            return true;
        else
            return false;
    }

    private void GiveReward(int amount)
    {
        if(isLevelReached)
        {
            Coins.CounterUpdateUIandCoin(amount, true);
            CoinAnimation.Instance.CountCoins();
            levelReachedButton.image.sprite = achievementManager.redButton;
            isLevelReached = false;
            if(GameDataManager.Instance.LevelIndex < levelReachedList.Length - 1)
            {
                GameDataManager.Instance.LevelIndex ++;
            }
            else
            {
                GameDataManager.Instance.IsLevelChallengeFinished = true;
            }
            LevelReachedUpdateUI();
            AchievementCompleteCheck();
            descriptionText.text = "Complete " + levelReachedList[GameDataManager.Instance.LevelIndex].achievementCount + " levels";
            notificationEvent?.Invoke();
        }
    }
}