using BaseFramework;
using BaseFramework.Core;
using UnityEngine;

namespace ScriptsHotfix
{
    public class GameWorld: SingleObject<GameWorld>
    {
        [SerializeField] public PlayerController Player;

        private Vector3 _originPos;
        private void Awake()
        {
            MainCtrl.Instance.ShowMainView();
            if (Player==null)
            {
                Log.Error("No Player");
                Player = transform.Find("Player").gameObject.GetComponent<PlayerController>();
            }
            CameraFollow.Instance.SetTarget(Player.gameObject.transform);
            _originPos = Player.gameObject.transform.position;
        }

        private void Start()
        {
            AudioManager.Instance.PlayMusic("MainBundle/Audio/music");
        }

        private void OnDestroy()
        {
            // MainCtrl.Instance.RemoveMainView();
        }

        public void Regain()
        {
            BulletTimeManager.Instance.Unpause();
            AudioManager.Instance.PlayMusic("MainBundle/Audio/music");
            AdRegainCtrl.Instance.RemoveAdView();
            Player.transform.position = new Vector3(0, Player.transform.position.y+5,
                Player.transform.position.z);
        }
        
        public void Reset()
        {
            BulletTimeManager.Instance.Unpause();
            AudioManager.Instance.PlayMusic("MainBundle/Audio/music");
            CameraFollow.Instance.SetTarget(Player.transform);
            AdRegainCtrl.Instance.RemoveAdView();
            MainCtrl.Instance.RemoveMainView();
            Player.transform.position = _originPos;
            MainCtrl.Instance.ResetData();
            MainCtrl.Instance.ShowMainView();
        }
    }
}