using DG.Tweening;
using System;
using UnityEngine;

public class CoinAnimation : MonoBehaviour
{
    public static CoinAnimation Instance;
    [SerializeField] private GameObject pileOfCoins;
    [SerializeField] private Vector2[] initialPos;
    [SerializeField] private Quaternion[] initialRotation;
    [SerializeField] private int coinsAmount;
    [SerializeField] private float xPos = 300f, yPos = 980f;

    private void Awake()
    {
        if (Instance == null) 
        {
            Instance = this;
        }
        StoreInitialTransforms();
    }

    private void StoreInitialTransforms()
    {
        int childCount = pileOfCoins.transform.childCount;
        initialPos = new Vector2[childCount];
        initialRotation = new Quaternion[childCount];

        for (int i = 0; i < childCount; i++)
        {
            initialPos[i] = pileOfCoins.transform.GetChild(i).GetComponent<RectTransform>().anchoredPosition;
            initialRotation[i] = pileOfCoins.transform.GetChild(i).GetComponent<RectTransform>().rotation;
        }
    }

    public void CountCoins()
    {
        pileOfCoins.SetActive(true);
        var delay = 0f;

        for (int i = 0; i < pileOfCoins.transform.childCount; i++)
        {
            SFXManager.Instance.PlaySound(SoundType.Coin, transform);
            pileOfCoins.transform.GetChild(i).DOScale(1f, 0.3f)
                .SetDelay(delay)
                .SetEase(Ease.OutBack)
                .SetUpdate(true);

            pileOfCoins.transform.GetChild(i).GetComponent<RectTransform>().DOAnchorPos(new Vector2(xPos, yPos), 0.8f)
                .SetDelay(delay + 0.5f)
                .SetEase(Ease.InBack)
                .SetUpdate(true);

            pileOfCoins.transform.GetChild(i).DORotate(Vector3.zero, 0.5f)
                .SetDelay(delay + 0.5f)
                .SetEase(Ease.Flash)
                .SetUpdate(true);

            pileOfCoins.transform.GetChild(i).DOScale(0f, 0.3f)
                .SetDelay(delay + 1.5f)
                .SetEase(Ease.OutBack)
                .SetUpdate(true);

            
            delay += 0.1f;
        }

    // You can call ResetCoins after a delay or use another method to trigger it if needed
    ResetCoins();
}


    public void ResetCoins()
    {
        try 
        {
            for (int i = 0; i < pileOfCoins.transform.childCount; i++)
            {
                pileOfCoins.transform.GetChild(i).GetComponent<RectTransform>().anchoredPosition = initialPos[i];
                pileOfCoins.transform.GetChild(i).GetComponent<RectTransform>().rotation = initialRotation[i];
            }
        } 
        catch (Exception e) 
        {
            Debug.Log(""+e);
        }
        // pileOfCoins.SetActive(false);
    }
}
