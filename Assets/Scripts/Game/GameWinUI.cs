using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GameWinUI : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject[] gameObjects;
    [SerializeField] private bool isGameWinPanel;
    [SerializeField] private TMP_Text coinText;
    [SerializeField] private Button doubleButton;
    public UnityEvent OnVictoryTextOver;
    
    private void Awake()
    {
        foreach (var gameObject in gameObjects)
        {
            gameObject.SetActive(false);
        }
    }

    void Start()
    {
        animator.updateMode = AnimatorUpdateMode.UnscaledTime;
        StartCoroutine(StartVictoryAnimation());
        if(isGameWinPanel)       
        {
            SFXManager.Instance.PlaySound(SoundType.GameWin, transform);
            coinText.text = 100.ToString();
        }
        else
        {
            SFXManager.Instance.PlaySound(SoundType.GameLoose, transform);
        }
        doubleButton?.onClick.AddListener(() => DoubleGameFinishReward());
    }

    private IEnumerator StartVictoryAnimation()
    {
        animator.Play("ScaleOnly");
        
        yield return null;

        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        float animationLength = stateInfo.length;

        yield return new WaitForSecondsRealtime(animationLength);
        OnVictoryTextOver?.Invoke();
        if(isGameWinPanel)
        {
            Coins.CounterUpdateUIandCoin(100, true);
            yield return new WaitForSecondsRealtime(0.5f);
            CoinAnimation.Instance.CountCoins();
        }
    }

    private void DoubleGameFinishReward()
    {
        AdManager.Instance.ShowRewardedAd((isRewarded) => 
        {
            if(isRewarded)
            {
                Coins.CounterUpdateUIandCoin(100, true);
                CoinAnimation.Instance.CountCoins();
                doubleButton.gameObject.SetActive(false);
                SFXManager.Instance.PlaySound(SoundType.Button, transform);
                isRewarded = false;
            }
        });
    }    
}
