using BaseFramework.Core;
using UnityEngine;

namespace BaseFramework
{
    public class SplashCtrl: SingleClass<SplashCtrl>
    {
        private SplashView _splashView;

        private string _path = "SplashUI/SplashUI";
        public SplashCtrl()
        {
        }

        public void ShowSplashView()
        {
            Log.Warning("Show");
            var uiGo = SimplePool.Spawn(Resources.Load<GameObject>(_path), GameObject.Find("UIRoot").transform);
            _splashView = uiGo.GetComponent<SplashView>();
        }
        public void RemoveSplashView()
        {
            if (_splashView!=null)
            {
                Object.Destroy(_splashView.gameObject);
            }
        }
    }
}