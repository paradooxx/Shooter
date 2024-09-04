using System;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkinUnlockedAchievement : MonoBehaviour
{
    [SerializeField] private TMP_Text skinCountText;
    [SerializeField] private Button skinAchievementButton;
    [SerializeField] TMP_Text buttonText;

    public static bool isSkinUnlocked = false;
    private int unlockedSkinCount;
    private int rewardAmount = 1000;
    private AchievementManager achievementManager;

    private event Action notificationEvent;

    private void Awake()
    {
        achievementManager = GetComponent<AchievementManager>();
        skinAchievementButton.onClick.AddListener(() => GiveReward(rewardAmount));
    }

    private void OnEnable() => notificationEvent += achievementManager.NotificationUpdate;

    private void OnDisable() => notificationEvent += achievementManager.NotificationUpdate;

    private void Start()
    {
        SkinUpdateUI();
    }

    public void CheckSkinUnlock()
    {
        if(GameDataManager.Instance.IsPlayerUnlocked.All(unlocked => unlocked))
        {
            isSkinUnlocked = true;
            notificationEvent?.Invoke();
            skinAchievementButton.image.sprite = achievementManager.greenButton;
        }
        SkinUpdateUI();
    }

    private void SkinUpdateUI()
    {
        unlockedSkinCount = 0;
        for(int i = 0; i < GameDataManager.Instance.IsPlayerUnlocked.Length ; i++)
        {
            if(GameDataManager.Instance.IsPlayerUnlocked[i])
            {
                unlockedSkinCount ++;
            }
        }
        skinCountText.text = unlockedSkinCount.ToString() + "/" + GameDataManager.Instance.IsPlayerUnlocked.Length.ToString();
        OnAllSkinUnlocked();
    }

    private void OnAllSkinUnlocked()
    {
        if(GameDataManager.Instance.IsSkinAchievementButtonPressed)
        {
            OnRewardTaken();
            return;
        }

        if(unlockedSkinCount == GameDataManager.Instance.IsPlayerUnlocked.Length)
        {
            buttonText.text = "Completed";
            skinAchievementButton.image.sprite = achievementManager.greenButton;
        }
    }

    private void OnRewardTaken()
    {
        buttonText.text = "Collected";
        skinAchievementButton.image.sprite = achievementManager.brownButton;
    }

    private void GiveReward(int amount)
    {
        if(isSkinUnlocked)
        {
            Coins.CounterUpdateUIandCoin(amount, true);
            CoinAnimation.Instance.CountCoins();
            skinAchievementButton.image.sprite = achievementManager.redButton;
            isSkinUnlocked = false;
            GameDataManager.Instance.IsSkinAchievementButtonPressed = true;
            notificationEvent?.Invoke();
        }
        OnAllSkinUnlocked();
    }
}
