using BaseFramework;
using UnityEngine;
using ET;
using BM;


public class Init : MonoBehaviour
{
    private void Awake()
    {
        System.AppDomain.CurrentDomain.UnhandledException += (sender, e) =>
        {
            AssetLogHelper.LogError(e.ExceptionObject.ToString());
        };
        ETTask.ExceptionHandler += AssetLogHelper.LogError;
    }

    void Start()
    {
        DontDestroyOnLoad(gameObject);
        SplashCtrl.Instance.ShowSplashView();
        HotUpdateCtrl.Instance.CheckUpdate();
    }
    
    void Update()
    {
        AssetComponent.Update();
    }
    
    void OnLowMemory()
    {
        AssetComponent.ForceUnLoadAll();
    }

}
