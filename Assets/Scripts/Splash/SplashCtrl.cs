using BaseFramework.Core;
using UnityEngine;

namespace BaseFramework
{
    public class SplashCtrl: SingleClass<SplashCtrl>
    {
        private SplashView _splashView;

        public SplashCtrl()
        {
        }

        public void ShowSplashView()
        {
            var uiGo = Object.Instantiate(Resources.Load<GameObject>("SplashUI"));
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