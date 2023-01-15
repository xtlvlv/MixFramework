using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using BaseFramework.Core;
using BM;
using ET;
using HybridCLR;
using UnityEngine;
using Object = UnityEngine.Object;

namespace BaseFramework
{
    public class HotUpdateCtrl: SingleClass<HotUpdateCtrl>, IDisposable
    {
        private DownloadView _downloadView;

        private UpdateBundleDataInfo _updateBundleDataInfo;

        public static List<string> AOTMetaAssemblyNames { get; } = new List<string>()
        {
            "mscorlib.dll",
            "System.dll",
            "System.Core.dll",
        };
        
        public HotUpdateCtrl()
        {
        }
        
        public DownloadView ShowDownloadView()
        {
            var uiGo = Object.Instantiate(Resources.Load<GameObject>("DownloadUI"));
            _downloadView = uiGo.GetComponent<DownloadView>();
            
            return _downloadView;
        }
        public void RemoveDownloadView()
        {
            if (_downloadView!=null)
            {
                Object.Destroy(_downloadView.gameObject);
            }
        }

        public async void CheckUpdate()
        {
            //重新配置热更路径(开发方便用, 打包移动端需要注释注释)
            AssetComponentConfig.HotfixPath = Application.dataPath + "/../DownloadBundles/";
            AssetComponentConfig.DefaultBundlePackageName = "MainBundle";
            // AssetComponentConfig.BundleServerUrl = "file://"+"/../BuildBundles/";
            
            bool needUpdate = await IsNeedUpdateBundle();
            if (!needUpdate)
            {
                Log.Info("no need to update");
                await InitializePackage();
                LoadHotfix();
            }
            else
            {
                string tips = string.Format("需要更新{0}, 是否更新？", _updateBundleDataInfo.NeedUpdateSize.FormatByte() );
                MessageBox.Show("提示", tips,() =>
                {
                    this.ShowDownloadView();
                    UpdateBundle(); // 开始更新
                }, () =>
                {
                    Application.Quit();
                },"更新","退出");
            }
        }
        
        async Task<bool> IsNeedUpdateBundle()
        {
            Dictionary<string, bool> updatePackageBundle = new Dictionary<string, bool>()
            {
                {AssetComponentConfig.DefaultBundlePackageName, false},
                {"Level2", false},
                {"DLL", false}
                //{"OriginFile", false},
            };
            _updateBundleDataInfo = await AssetComponent.CheckAllBundlePackageUpdate(updatePackageBundle);
            return _updateBundleDataInfo.NeedUpdate;
        }
        
        void UpdateBundle()
        {
            // 打开下载界面，设置回调
            _updateBundleDataInfo.DownLoadFinishCallback += () =>
            {
                UpdateFinish();
                Log.Info("UpdateFinishAfter");
            };
            _updateBundleDataInfo.ProgressCallback += p =>
            {
                _downloadView.UpdateProgress(p);
            };
            _updateBundleDataInfo.DownLoadSpeedCallback += s =>
            {
                _downloadView.UpdateSpeed(s);
            };
            _updateBundleDataInfo.ErrorCancelCallback += () =>
            {
                Log.Warning("下载取消");
            };
            
            AssetComponent.DownLoadUpdate(_updateBundleDataInfo).Coroutine();
        }

        async void UpdateFinish()
        {
            Log.Info("UpdateFinish");
            await InitializePackage();
            MessageBox.Show("提示", "更新完成，点击进入游戏",() =>
            {
                RemoveDownloadView();
                LoadHotfix();
            }, () =>
            {
                Log.Info("quit");
            },"进入游戏","退出游戏");
        }
        
        private async ETTask InitializePackage()
        {
            await AssetComponent.Initialize(AssetComponentConfig.DefaultBundlePackageName);
            await AssetComponent.Initialize("Level2");
            await AssetComponent.Initialize("DLL");

            // await InitUI();
        }

        void LoadHotfix()
        {
            LoadMetadataForAOTAssemblies();
            
            byte[] assBytes;
            var hotfixDll = AssetMgr.Load<TextAsset>("Assets/ResHotfix/DLL/Hotfix.dll.bytes", "DLL");
            assBytes = (hotfixDll).bytes;
            var ass = Assembly.Load(assBytes);
			
            // Type entryType = ass.GetType("HotEntry");
            // MethodInfo method = entryType.GetMethod("Entry");
            // Action mainFunc = (Action)Delegate.CreateDelegate(typeof(Action), method);
            // mainFunc();
            
            SplashCtrl.Instance.RemoveSplashView();
            
            var prefab = AssetMgr.Load<GameObject>("Assets/ResHotfix/MainBundle/HotEntry.prefab");
            Object.Instantiate(prefab);
        }
        
        /// <summary>
        /// 为aot assembly加载原始metadata， 这个代码放aot或者热更新都行。
        /// 一旦加载后，如果AOT泛型函数对应native实现不存在，则自动替换为解释模式执行
        /// </summary>
        private static void LoadMetadataForAOTAssemblies()
        {
            /// 注意，补充元数据是给AOT dll补充元数据，而不是给热更新dll补充元数据。
            /// 热更新dll不缺元数据，不需要补充，如果调用LoadMetadataForAOTAssembly会返回错误
            HomologousImageMode mode = HomologousImageMode.SuperSet;
            foreach (var aotDllName in AOTMetaAssemblyNames)
            {
                var dllAsset = AssetMgr.Load<TextAsset>("Assets/ResHotfix/DLL/"+aotDllName+".bytes", "DLL");
                // 加载assembly对应的dll，会自动为它hook。一旦aot泛型函数的native函数不存在，用解释器版本代码
                LoadImageErrorCode err = RuntimeApi.LoadMetadataForAOTAssembly(dllAsset.bytes, mode);
                Debug.Log($"LoadMetadataForAOTAssembly:{aotDllName}. mode:{mode} ret:{err}");
            }
        }

        public void Dispose()
        {
            _updateBundleDataInfo?.CancelUpdate();
        }
    }
}