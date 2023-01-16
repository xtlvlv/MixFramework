using BaseFramework.Core;
using UnityEngine;

namespace ScriptsHotfix
{
    public class LoginCtrl : SingleClass<LoginCtrl>
    {
        private LoginView _loginView;

        public LoginCtrl()
        {
            // UIManager.Instance.AddConfig(ViewId.LoginUI, "Assets/ResHotfix/MainBundle/UI/LoginUI/LoginUI.prefab");
        }
        
        public void ShowLoginView()
        {
            _loginView = UIManager.Instance.OpenUI(ViewId.LoginUI) as LoginView;
        }
        public void RemoveLoginView()
        {
            UIManager.Instance.CloseUI(ViewId.LoginUI);
        }

    }
}
