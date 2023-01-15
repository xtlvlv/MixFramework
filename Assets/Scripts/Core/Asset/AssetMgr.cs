
using System;
using BM;
using ET;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;


namespace BaseFramework.Core
{
    // TODO: 可选择的资源缓存
    public class AssetMgr: SingleClass<AssetMgr>
    {
        #region 同步加载
        public static Object Load(string path)
        {
            return Load(path, null, null);
        }

        public static Object Load(string path, string package)
        {
            return Load(path, package, null);
        }
        
        private static Object Load(string path, string package, Type type)
        {
            var ret = AssetComponent.Load(out _, path, package);
            return ret;
        }
        
        public static T Load<T>(string path)
            where T : Object
        {
            return Load<T>(path, null, null);
        }

        public static T Load<T>(string path, string package)
            where T : Object
        {
            return Load<T>(path, package, null);
        }
        
        private static T Load<T>(string path, string package, Type type)
            where T : Object
        {
            var ret = AssetComponent.Load<T>(out _, path, package);
            return ret;
        }
        #endregion

        #region 异步加载
        public static async ETTask<Object> LoadAsync(string path)
        {
            return await LoadAsync(path, null, null);
        }

        public static async ETTask<Object> LoadAsync(string path, string package)
        {
            return await LoadAsync(path, package, null);
        }

        private static async ETTask<Object> LoadAsync(string path, string package, Type type)
        {
            var ret = await AssetComponent.LoadAsync(out _, path, package);
            return ret;
        }

        public static async ETTask<T> LoadAsync<T>(string path)
            where T : Object
        {
            return await LoadAsync<T>(path, null, null);
        }

        public static async ETTask<T> LoadAsync<T>(string path, string package)
            where T : Object
        {
            return await LoadAsync<T>(path, package, null);
        }
        
        private static async ETTask<T> LoadAsync<T>(string path, string package = null, Type type = null)
            where T : Object
        {
            var ret = await AssetComponent.LoadAsync<T>(out _, path, package);
            return ret;
        }
        #endregion

        #region 加载场景

        public static void LoadScene(string path, bool additive = false, string package = null)
        {
            AssetComponent.LoadScene(path, package);
            if (additive)
                SceneManager.LoadScene(path, LoadSceneMode.Additive);
            else
                SceneManager.LoadScene(path);
            RemoveUnusedAssets();
        }

        #endregion

        #region 卸载资源
        public static void Unload(string path, string package = null)
        {
            package = string.IsNullOrEmpty(package) ? AssetComponentConfig.DefaultBundlePackageName : package;
            AssetComponent.UnLoadByPath(path, package);
        }
        
        public static void RemoveUnusedAssets()
        {
            AssetComponent.ForceUnLoadAll();
        }
        #endregion 


    }
}