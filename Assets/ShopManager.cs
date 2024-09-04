using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    [SerializeField] private Button watchAdToGetCoinButton;
    [SerializeField] private Button unlockAllSkinPlusCoinsButton;
    [SerializeField] private Button noAdsPlusCoinsButton;

    private void Start()
    {
        watchAdToGetCoinButton.onClick.AddListener(() => WatchAdGetCoin());

        unlockAllSkinPlusCoinsButton.onClick.AddListener(() => 
        {
            UnlockAllSkinPlus5000Coins();
            unlockAllSkinPlusCoinsButton.interactable = false;
            unlockAllSkinPlusCoinsButton.GetComponentInChildren<TMP_Text>().text = "Bought";
        });

        noAdsPlusCoinsButton.onClick.AddListener(() =>
        {
            NoAdsPlus1000Coins();
            noAdsPlusCoinsButton.interactable = false;  
            noAdsPlusCoinsButton.GetComponentInChildren<TMP_Text>().text = "Bought";
        });
    }

    private void WatchAdGetCoin()
    {
        AdManager.Instance.ShowRewardedAd((isRewarded) => 
        {
            if (isRewarded)
            {
                Coins.CounterUpdateUIandCoin(25, true);
                CoinAnimation.Instance.CountCoins();
                isRewarded = false;
            }
        });
    }

    private void NoAdsPurchase()
    {
        /*
        after purchase is implemented
        implement,
        GameDataManager.Instance.IsNoAdsSubPurchased = true;
        GameDataManager.Instance.SaveGameData();
         */
        GameDataManager.Instance.IsNoAdsSubPurchased = true;
    }

    private void UnlockAllSkinPlus5000Coins()
    {
        for (int i = 0; i < GameDataManager.Instance.IsPlayerUnlocked.Length; i++)
        {
            GameDataManager.Instance.IsPlayerUnlocked[i] = true;
        }
        GameDataManager.Instance.SaveGameData();
    }

    private void NoAdsPlus1000Coins()
    {
        Coins.CounterUpdateUIandCoin(1000, true);
        CoinAnimation.Instance.CountCoins();
        NoAdsPurchase();
    }
}