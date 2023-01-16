using BaseFramework.Core;
using UnityEngine;
using UnityEngine.UI;

namespace ScriptsHotfix
{
    public class LoginView : ViewBase
    {
    
        [SerializeField] private Button _loginBtn;

        private void Reset()
        {
            BaseReset();
            _loginBtn = transform.Find("LoginBtn").GetComponent<Button>();
        }

        public override void OnLoad()
        {
            base.OnLoad();
            _loginBtn.onClick.AddListener(() =>
            {
                Log.Info("Click Login");
            });
        }
    }
}