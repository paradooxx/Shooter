using System.Dynamic;
using TMPro;
using UnityEngine;

public class Coins : MonoBehaviour
{
    public static int coins = 50000;

    [SerializeField] private TMP_Text coinText;

    private static Coins instance;
    private ScoreCounter scoreCounter;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
        UpdateCoinText();
    }

    private void OnEnable()
    {
        scoreCounter = GetComponent<ScoreCounter>();
    }

    private void UpdateCoinText()
    {
        if (coinText != null)
        {
            coinText.text = coins.ToString();
        }
        else
        {
            return;
        }
    }

    // to use animation when changing coin count
    //for smaller change values
    public static void CounterUpdateUIandCoin(int amount, bool isAmountPositive)
    {
        if(isAmountPositive) 
        {
            instance.scoreCounter.PlayMainCounter(amount);
            coins += amount;
        }
        else if(!isAmountPositive)
        {
            instance.scoreCounter.PlayMainCounter(-amount);
            coins -= amount;
        }
    }

    //to change coin count without animation
    //for larger change values
    public static void UpdateUIandCoin(int amount, bool isAmountPositive)
    {   
        // instance.Invoke("PlayCoinAnimation", 0.2f);
        if(isAmountPositive) coins += amount;
        else if(!isAmountPositive) coins -= amount;

        if (instance != null)
        {
            instance.UpdateCoinText();
        }
        else
        {
            return;
        }
    }
}
