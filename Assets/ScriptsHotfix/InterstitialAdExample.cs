using System;
using BaseFramework.Core;
using ScriptsHotfix;
using UnityEngine;
using UnityEngine.Advertisements;
 
public class InterstitialAdExample : SingleObject<InterstitialAdExample>,IUnityAdsInitializationListener, IUnityAdsLoadListener, IUnityAdsShowListener
{
    string         _adUnitId = "Interstitial_Android";
    private string _gameId = "5174164";

    private void Awake()
    {
        InitializeAds();
    }

    private void Start()
    {
        LoadAd();
    }

    public void InitializeAds()
    {
        Advertisement.Initialize(_gameId, false, this);
    }
    
    // 将内容加载到广告单元中：
    public void LoadAd()
    {
        Debug.Log("Loading Ad: " + _adUnitId);
        Advertisement.Load(_adUnitId, this);
    }
 
    // 展示广告单元中加载的内容：
    public void ShowAd()
    {
        Debug.Log("Showing Ad: " + _adUnitId);
        Advertisement.Show(_adUnitId, this);
    }
    
    public void OnInitializationComplete()
    {
        Debug.Log("Unity Ads initialization complete.");
    }
 
    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        Debug.Log($"Unity Ads Initialization Failed: {error.ToString()} - {message}");
    }
 
    // 实现 Load Listener 和 Show Listener 接口方法： 
    public void OnUnityAdsAdLoaded(string adUnitId)
    {
        //（可选）如果广告单元成功加载内容，执行代码。
    }
 
    public void OnUnityAdsFailedToLoad(string adUnitId, UnityAdsLoadError error, string message)
    {
        Debug.Log($"Error loading Ad Unit: {adUnitId} - {error.ToString()} - {message}");
        //（可选）如果广告单元加载失败，执行代码（例如再次尝试）。
    }
 
    public void OnUnityAdsShowFailure(string adUnitId, UnityAdsShowError error, string message)
    {
        Debug.Log($"Error showing Ad Unit {adUnitId}: {error.ToString()} - {message}");
        //（可选）如果广告单元展示失败，执行代码（例如加载另一个广告）。
    }

    public void OnUnityAdsShowStart(string adUnitId)
    {
        Debug.Log($"OnUnityAdsShowStart {adUnitId}");
    }

    public void OnUnityAdsShowClick(string adUnitId)
    {
        Debug.Log($"OnUnityAdsShowClick {adUnitId}");
    }

    public void OnUnityAdsShowComplete(string adUnitId, UnityAdsShowCompletionState showCompletionState)
    {
        Debug.Log($"OnUnityAdsShowComplete {adUnitId}");
        // EventManager.Instance.Fire<LevelDeadEvent>(new LevelDeadEvent(false));
        // GameWorld.Instance.Regain();
    }
}