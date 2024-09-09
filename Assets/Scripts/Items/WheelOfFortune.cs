using UnityEngine;
using EasyUI.PickerWheelUI;
using UnityEngine.UI;
using TMPro;
using System;

public class WheelOfFortune : MonoBehaviour
{
    [SerializeField] private Button spinButton;
    [SerializeField] private TMP_Text spinButtonText;
    [SerializeField] private TMP_Text rewardAmountText;

    [SerializeField] private PickerWheel pickerWheel;

    [SerializeField] private GameObject[] objectsToHide;
    [SerializeField] private String targetLabel;

    public int rewardAmount { get; private set; }

    private void Start()
    {
        spinButton.onClick.AddListener(() => OnSpin());
        foreach (var obj in objectsToHide)
        {
            obj.SetActive(false);
        }
    }

    private void OnSpin()
    {
        AdManager.Instance.ShowRewardedAd((isRewarded) => 
        {
            if(isRewarded)
            {
                spinButtonText.text = "Spinning";
                pickerWheel.SetTargetLabel(targetLabel);
                pickerWheel.Spin();
                pickerWheel.OnSpinEnd(wheelPiece => { UpdateRewardUI(wheelPiece.Amount); });
                isRewarded = false;
            }
        });
    }
    
    private void UpdateRewardUI(int amount)
    {
        rewardAmountText.text = amount.ToString();
        CoinAnimation.Instance.CountCoins();
        spinButtonText.text = "Spin";
        Coins.CounterUpdateUIandCoin(amount, true);
    }
}
