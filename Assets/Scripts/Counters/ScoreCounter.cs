using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class ScoreCounter : MonoBehaviour
{
    [SerializeField] private TMP_Text counterText;
    //[SerializeField] private StepPitch stepPitch;
   
    private bool isPlaying;
    private GameDataManager gameDataManager;

    public UnityEvent OnCountUpdated;
    public UnityEvent OnAnimationFinished;

    private void OnEnable()
    {
        gameDataManager = GameDataManager.Instance;
    }

    private void UpdateCounter(int count)
    {
        counterText.text = count.ToString();
    }

    public void PlayMainCounter(int count)
    {
        if (isPlaying) return;
        StartCoroutine(AnimationCoroutine(Coins.coins, count));
    }

    public void PlayNormalCounter(int amount = 100)
    {
        if (isPlaying) return;
        Coins.CounterUpdateUIandCoin(Rewards.rewardAmount, true);
        StartCoroutine(AnimationCoroutine(int.Parse(counterText.text), Rewards.rewardAmount));
    }

    public void PlayZeroCounter(int amount)
    {
        if (isPlaying) return;
        Coins.CounterUpdateUIandCoin(amount, true);
        StartCoroutine(AnimationCoroutine(0, amount));
    }  

    private IEnumerator AnimationCoroutine(int originalCount, int additionalCount)
    {
        float waitTime = 0.025f;
        isPlaying = true;

        int soundPlayLimit = 5; 
        int totalSteps = Mathf.Abs(additionalCount); 
        int soundPlayInterval = Mathf.Max(1, totalSteps / soundPlayLimit);
        int soundPlayCount = 0;

        float adjustedWaitTime = waitTime * Time.timeScale;

        if (additionalCount > 0)  // Increasing count
        {
            for (int i = originalCount; i < originalCount + additionalCount; i++)
            {
                UpdateCounter(i + 1);
                if (soundPlayCount < soundPlayLimit && (i - originalCount) % soundPlayInterval == 0)
                {
                    SFXManager.Instance.PlaySound(SoundType.Coin, transform);
                    soundPlayCount++;
                }

                if ((i + 1) % 5 == 0) adjustedWaitTime *= 0.25f;

                OnCountUpdated?.Invoke();
                yield return new WaitForSeconds(adjustedWaitTime);
            }
        }
        else if (additionalCount < 0)  // Decreasing count
        {
            for (int i = originalCount; i > originalCount + additionalCount; i--)
            {
                UpdateCounter(i - 1);
                if (soundPlayCount < soundPlayLimit && (originalCount - i) % soundPlayInterval == 0)
                {
                    SFXManager.Instance.PlaySound(SoundType.Coin, transform);
                    soundPlayCount++;
                }

                if ((i - 1) % 5 == 0) adjustedWaitTime *= 0.25f;

                OnCountUpdated?.Invoke();
                yield return new WaitForSeconds(adjustedWaitTime); 
            }
        }

        // if any sounds are left to play, play them at the end
        while (soundPlayCount < soundPlayLimit)
        {
            SFXManager.Instance.PlaySound(SoundType.Coin, transform);
            soundPlayCount++;
            yield return new WaitForSeconds(adjustedWaitTime); 
        }

        yield return new WaitForSecondsRealtime(0.2f);
        OnAnimationFinished?.Invoke();
        isPlaying = false;
    }
}
