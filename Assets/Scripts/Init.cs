using System.Collections.Generic;
using System.Threading.Tasks;
using BaseFramework;
using BaseFramework.Core;
using UnityEngine;
using ET;
using BM;
using UnityEditor;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;


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
    
    void OnDestroy()
    {
        // updateBundleDataInfo?.CancelUpdate();
    }

    /// <summary>
    /// 下载资源
    /// </summary>
    // private async ETTask DownLoad(GameObject downLoadUI)
    // {
    //     downLoadUI.SetActive(false);
    //     Dictionary<string, bool> updatePackageBundle = new Dictionary<string, bool>()
    //     {
    //         // {AssetComponentConfig.DefaultBundlePackageName, false},
    //         {"Level1", false},
    //         {"Level2", false},
    //         //{"OriginFile", false},
    //     };
    //     updateBundleDataInfo = await AssetComponent.CheckAllBundlePackageUpdate(updatePackageBundle);
    //     if (!updateBundleDataInfo.NeedUpdate)
    //     {
    //         GameObject.Destroy(downLoadUI);
    //         InitializePackage().Coroutine();
    //         return;
    //     }
    //     downLoadUI.SetActive(true);
    //     Debug.Log("需要更新, 大小: " + updateBundleDataInfo.NeedUpdateSize);
    //     Slider progressSlider = downLoadUI.transform.Find("ProgressSlider").GetComponent<Slider>();
    //     Text progressText = downLoadUI.transform.Find("ProgressValue/Text").GetComponent<Text>();
    //     Text speedText = downLoadUI.transform.Find("SpeedValue/Text").GetComponent<Text>();
    //     Button cancelDownLoad = downLoadUI.transform.Find("Cancel").GetComponent<Button>();
    //     Button reDownLoad = downLoadUI.transform.Find("ReDown").GetComponent<Button>();
    //     updateBundleDataInfo.DownLoadFinishCallback += () =>
    //     {
    //         GameObject.Destroy(downLoadUI);
    //         InitializePackage().Coroutine();
    //     };
    //     updateBundleDataInfo.ProgressCallback += p =>
    //     {
    //         progressSlider.value = p / 100.0f;
    //         progressText.text = p.ToString("#0.00") + "%";
    //     };
    //     updateBundleDataInfo.DownLoadSpeedCallback += s =>
    //     {
    //         speedText.text = (s / 1024.0f).ToString("#0.00") + " kb/s";
    //     };
    //     updateBundleDataInfo.ErrorCancelCallback += () =>
    //     {
    //         Debug.LogWarning("下载取消");
    //     };
    //     cancelDownLoad.onClick.RemoveAllListeners();
    //     cancelDownLoad.onClick.AddListener(updateBundleDataInfo.CancelUpdate);
    //     reDownLoad.onClick.RemoveAllListeners();
    //     reDownLoad.onClick.AddListener(() =>
    //     {
    //         DownLoad(downLoadUI).Coroutine();
    //     });
    //     AssetComponent.DownLoadUpdate(updateBundleDataInfo).Coroutine();
    // }

}
