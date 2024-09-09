using System.Collections;
using UnityEngine;

public class AdManager : MonoBehaviour
{
    public static AdManager Instance;
    private System.Action<bool> onAdComplete;

    [SerializeField] private GameObject adNotAvailable;

    void Awake()
    {
        if (Instance != null && Instance != this) 
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        //DontDestroyOnLoad(gameObject);

        IronSource.Agent.init("1f68b8fa5");
        IronSource.Agent.validateIntegration();

        LoadBannerAd();
        LoadInterstrialAd();
        adNotAvailable.SetActive(false);
    }

    void OnEnable()
    {
        IronSourceEvents.onSdkInitializationCompletedEvent += SdkInitializationCompletedEvent;
        //Add AdInfo Banner Events
        IronSourceBannerEvents.onAdLoadedEvent += BannerOnAdLoadedEvent;
        IronSourceBannerEvents.onAdLoadFailedEvent += BannerOnAdLoadFailedEvent;
        IronSourceBannerEvents.onAdClickedEvent += BannerOnAdClickedEvent;
        IronSourceBannerEvents.onAdScreenPresentedEvent += BannerOnAdScreenPresentedEvent;
        IronSourceBannerEvents.onAdScreenDismissedEvent += BannerOnAdScreenDismissedEvent;
        IronSourceBannerEvents.onAdLeftApplicationEvent += BannerOnAdLeftApplicationEvent;

        //Add AdInfo Interstitial Events
        IronSourceInterstitialEvents.onAdReadyEvent += InterstitialOnAdReadyEvent;
        IronSourceInterstitialEvents.onAdLoadFailedEvent += InterstitialOnAdLoadFailed;
        IronSourceInterstitialEvents.onAdOpenedEvent += InterstitialOnAdOpenedEvent;
        IronSourceInterstitialEvents.onAdClickedEvent += InterstitialOnAdClickedEvent;
        IronSourceInterstitialEvents.onAdShowSucceededEvent += InterstitialOnAdShowSucceededEvent;
        IronSourceInterstitialEvents.onAdShowFailedEvent += InterstitialOnAdShowFailedEvent;
        IronSourceInterstitialEvents.onAdClosedEvent += InterstitialOnAdClosedEvent;

        //Add AdInfo Rewarded Video Events
        IronSourceRewardedVideoEvents.onAdOpenedEvent += RewardedVideoOnAdOpenedEvent;
        IronSourceRewardedVideoEvents.onAdClosedEvent += RewardedVideoOnAdClosedEvent;
        IronSourceRewardedVideoEvents.onAdAvailableEvent += RewardedVideoOnAdAvailable;
        IronSourceRewardedVideoEvents.onAdUnavailableEvent += RewardedVideoOnAdUnavailable;
        IronSourceRewardedVideoEvents.onAdShowFailedEvent += RewardedVideoOnAdShowFailedEvent;
        IronSourceRewardedVideoEvents.onAdRewardedEvent += RewardedVideoOnAdRewardedEvent;
        IronSourceRewardedVideoEvents.onAdClickedEvent += RewardedVideoOnAdClickedEvent;
    }

    void OnApplicationPause(bool isPaused)
    {
        IronSource.Agent.onApplicationPause(isPaused);
    }

    private void SdkInitializationCompletedEvent() {}

    #region Banner

    public void LoadBannerAd() 
    {
        IronSource.Agent.loadBanner(IronSourceBannerSize.SMART, IronSourceBannerPosition.BOTTOM);
    }

    public void HideBannerAd() 
    {
        IronSource.Agent.hideBanner();
    }

    public void ShowBannerAd() 
    {
        IronSource.Agent.displayBanner(); 
    }

    public void DestroyBanneAd() 
    {
        IronSource.Agent.destroyBanner();
    }

    void BannerOnAdLoadedEvent(IronSourceAdInfo adInfo) {}

    void BannerOnAdLoadFailedEvent(IronSourceError ironSourceError) {}

    void BannerOnAdClickedEvent(IronSourceAdInfo adInfo) {}

    void BannerOnAdScreenPresentedEvent(IronSourceAdInfo adInfo) {}

    void BannerOnAdScreenDismissedEvent(IronSourceAdInfo adInfo) {}
 
    void BannerOnAdLeftApplicationEvent(IronSourceAdInfo adInfo) {}
    #endregion

    #region Interstitial

    public void LoadInterstrialAd() 
    {
        IronSource.Agent.loadInterstitial();
    }

    public void ShowInterstitialAd() 
    {
        //if (IronSource.Agent.isInterstitialReady()) 
        //{
            IronSource.Agent.showInterstitial();
        //}
    }

    private IEnumerator ShowAdNotAvailable()
    {
        adNotAvailable.SetActive(true);
        yield return new WaitForSecondsRealtime(0.6f);
        adNotAvailable.SetActive(false);
    }

    void InterstitialOnAdReadyEvent(IronSourceAdInfo adInfo) {}

    void InterstitialOnAdLoadFailed(IronSourceError ironSourceError) {}

    void InterstitialOnAdOpenedEvent(IronSourceAdInfo adInfo) {}

    void InterstitialOnAdClickedEvent(IronSourceAdInfo adInfo) {}

    void InterstitialOnAdShowFailedEvent(IronSourceError ironSourceError, IronSourceAdInfo adInfo) {}

    void InterstitialOnAdClosedEvent(IronSourceAdInfo adInfo) {}

    void InterstitialOnAdShowSucceededEvent(IronSourceAdInfo adInfo) {}
    #endregion

    #region Reward

    public void ShowRewardedAd(System.Action<bool> onAdCompleteCallBack) 
    {
        onAdComplete = onAdCompleteCallBack; 
        if (IronSource.Agent.isRewardedVideoAvailable()) 
        {
            IronSource.Agent.showRewardedVideo();
        }
        else
        {
            StartCoroutine(ShowAdNotAvailable());
        }
    }

    void RewardedVideoOnAdAvailable(IronSourceAdInfo adInfo) {}
 
    void RewardedVideoOnAdUnavailable()
    {
        onAdComplete?.Invoke(false);
    }

    void RewardedVideoOnAdOpenedEvent(IronSourceAdInfo adInfo) {}

    void RewardedVideoOnAdClosedEvent(IronSourceAdInfo adInfo)
    {
        onAdComplete?.Invoke(false);
    }

    void RewardedVideoOnAdRewardedEvent(IronSourcePlacement placement, IronSourceAdInfo adInfo)
    {
        
    }

    void RewardedVideoOnAdShowFailedEvent(IronSourceError error, IronSourceAdInfo adInfo)
    {
        StartCoroutine(ShowAdNotAvailable());
    }

    void RewardedVideoOnAdClickedEvent(IronSourcePlacement placement, IronSourceAdInfo adInfo) {}
    #endregion
}