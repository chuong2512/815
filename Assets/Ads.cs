using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ads : MonoBehaviour
{
    private static Ads playerInstance;
    public static Ads Instance;
    public GameObject InternetAlert;
    void Awake() {
      
        DontDestroyOnLoad(this);

        if (playerInstance == null)
        {
            playerInstance = this;
        }
        else
        {
            DestroyObject(gameObject);
        }


        Instance = this;
        Advertisements.Instance.Initialize();
    }

    void Update()
    {
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            Time.timeScale = 0;
            Debug.Log("Error. Check internet connection!");
            InternetAlert.SetActive(true);
            //No internet
        }
        else
        {
            Time.timeScale = 1;
            InternetAlert.SetActive(false);
         
            //Connected
        }
    }
    // Start is called before the first frame update
    void Start()
    {
     
        //MaxSdkCallbacks.OnSdkInitializedEvent += (MaxSdkBase.SdkConfiguration sdkConfiguration) => {
        //    // AppLovin SDK is initialized, start loading ads
        //    InitializeInterstitialAds();
        //    InitializeRewardedAds();
        //};

        //MaxSdk.SetSdkKey("GXiTAfTXDckzTwf7IiR88u34rXRRDB4rsc2-apBn0hzuCncXagQKu3CO8II3EDuQiPgFPR1IvCDBSWM4oCUn-b");
        //// MaxSdk.SetUserId("USER_ID");
        //MaxSdk.InitializeSdk();
        //MaxSdk.SetHasUserConsent(true);

    }
    void VibrateTrigger()
    {
        Handheld.Vibrate();
    }
    //public void ShowInterstitialExec()
    //{
    //    if (MaxSdk.IsInterstitialReady(adUnitId))
    //    {
    //        MaxSdk.ShowInterstitial(adUnitId);
    //    }
    //    else
    //    {
    //        LoadInterstitial();
    //        Invoke("ShowAgainInterstitialAd", 0.5f);
    //    }
    //}

    //void ShowAgainInterstitialAd()
    //{
    //    ShowInterstitialExec();
    //}
    //public void ShowRewardedExec()
    //{
    //    if (MaxSdk.IsRewardedAdReady(RewardedadUnitId))
    //    {
    //        MaxSdk.ShowRewardedAd(RewardedadUnitId);
    //    }
    //    else
    //    {
    //        LoadRewardedAd();
    //        Invoke("ShowAgainRewardedAd", 0.5f);
    //    }
    //}
    //void ShowAgainRewardedAd()
    //{
    //    ShowRewardedExec();
    //}

    //string adUnitId = "57c3ec10df1cd4f4";
    //int retryAttempt;

    //public void InitializeInterstitialAds()
    //{
    //    // Attach callback
    //    MaxSdkCallbacks.Interstitial.OnAdLoadedEvent += OnInterstitialLoadedEvent;
    //    MaxSdkCallbacks.Interstitial.OnAdLoadFailedEvent += OnInterstitialLoadFailedEvent;
    //    MaxSdkCallbacks.Interstitial.OnAdDisplayedEvent += OnInterstitialDisplayedEvent;
    //    MaxSdkCallbacks.Interstitial.OnAdClickedEvent += OnInterstitialClickedEvent;
    //    MaxSdkCallbacks.Interstitial.OnAdHiddenEvent += OnInterstitialHiddenEvent;
    //    MaxSdkCallbacks.Interstitial.OnAdDisplayFailedEvent += OnInterstitialAdFailedToDisplayEvent;

    //    // Load the first interstitial
    //    LoadInterstitial();
    //}

    //private void LoadInterstitial()
    //{
    //    MaxSdk.LoadInterstitial(adUnitId);
    //}

    //private void OnInterstitialLoadedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
    //{
    //    // Interstitial ad is ready for you to show. MaxSdk.IsInterstitialReady(adUnitId) now returns 'true'

    //    // Reset retry attempt
    //    retryAttempt = 0;
    //}

    //private void OnInterstitialLoadFailedEvent(string adUnitId, MaxSdkBase.ErrorInfo errorInfo)
    //{
    //    // Interstitial ad failed to load 
    //    // AppLovin recommends that you retry with exponentially higher delays, up to a maximum delay (in this case 64 seconds)

    //    retryAttempt++;
    //    double retryDelay = Mathf.Pow(2, Mathf.Min(6, retryAttempt));

    //    Invoke("LoadInterstitial", (float)retryDelay);
    //}

    //private void OnInterstitialDisplayedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
    //{
    //    Debug.Log("Ads Showed !");
    //}

    //private void OnInterstitialAdFailedToDisplayEvent(string adUnitId, MaxSdkBase.ErrorInfo errorInfo, MaxSdkBase.AdInfo adInfo)
    //{
    //    // Interstitial ad failed to display. AppLovin recommends that you load the next ad.
    //    LoadInterstitial();
    //}

    //private void OnInterstitialClickedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo) { }

    //private void OnInterstitialHiddenEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
    //{
    //    // Interstitial ad is hidden. Pre-load the next ad.
    //    LoadInterstitial();
    //}



    //string RewardedadUnitId = "fcf81fab081b26cb";
    //int RewardedretryAttempt;

    //public void InitializeRewardedAds()
    //{
    //    // Attach callback
    //    MaxSdkCallbacks.Rewarded.OnAdLoadedEvent += OnRewardedAdLoadedEvent;
    //    MaxSdkCallbacks.Rewarded.OnAdLoadFailedEvent += OnRewardedAdLoadFailedEvent;
    //    MaxSdkCallbacks.Rewarded.OnAdDisplayedEvent += OnRewardedAdDisplayedEvent;
    //    MaxSdkCallbacks.Rewarded.OnAdClickedEvent += OnRewardedAdClickedEvent;
    //    MaxSdkCallbacks.Rewarded.OnAdRevenuePaidEvent += OnRewardedAdRevenuePaidEvent;
    //    MaxSdkCallbacks.Rewarded.OnAdHiddenEvent += OnRewardedAdHiddenEvent;
    //    MaxSdkCallbacks.Rewarded.OnAdDisplayFailedEvent += OnRewardedAdFailedToDisplayEvent;
    //    MaxSdkCallbacks.Rewarded.OnAdReceivedRewardEvent += OnRewardedAdReceivedRewardEvent;

    //    // Load the first rewarded ad
    //    LoadRewardedAd();
    //}

    //private void LoadRewardedAd()
    //{
    //    MaxSdk.LoadRewardedAd(RewardedadUnitId);
    //}

    //private void OnRewardedAdLoadedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
    //{
    //    // Rewarded ad is ready for you to show. MaxSdk.IsRewardedAdReady(adUnitId) now returns 'true'.

    //    // Reset retry attempt
    //    RewardedretryAttempt = 0;
    //}

    //private void OnRewardedAdLoadFailedEvent(string adUnitId, MaxSdkBase.ErrorInfo errorInfo)
    //{
    //    // Rewarded ad failed to load 
    //    // AppLovin recommends that you retry with exponentially higher delays, up to a maximum delay (in this case 64 seconds).

    //    RewardedretryAttempt++;
    //    double retryDelay = Mathf.Pow(2, Mathf.Min(6, RewardedretryAttempt));

    //    Invoke("LoadRewardedAd", (float)retryDelay);
    //}

    //private void OnRewardedAdDisplayedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo) { }

    //private void OnRewardedAdFailedToDisplayEvent(string adUnitId, MaxSdkBase.ErrorInfo errorInfo, MaxSdkBase.AdInfo adInfo)
    //{
    //    // Rewarded ad failed to display. AppLovin recommends that you load the next ad.
    //    LoadRewardedAd();
    //}

    //private void OnRewardedAdClickedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo) { }

    //private void OnRewardedAdHiddenEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
    //{
    //    // Rewarded ad is hidden. Pre-load the next ad
    //    LoadRewardedAd();
    //}

    //private void OnRewardedAdReceivedRewardEvent(string adUnitId, MaxSdk.Reward reward, MaxSdkBase.AdInfo adInfo)
    //{

    //    // The rewarded ad displayed and the user should receive the reward.
    //}

    //private void OnRewardedAdRevenuePaidEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
    //{
    //    // Ad revenue paid. Use this callback to track user revenue.
    //}

    public void ShowInterstitial()
    {
        Advertisements.Instance.ShowInterstitial();
    }

   
    public void ShowRewardedVideo()
    {
        Advertisements.Instance.ShowRewardedVideo(CompleteMethod);
    }

    private void CompleteMethod(bool completed)
    {
        if (completed)
        {
            
        }

       
    }
}
