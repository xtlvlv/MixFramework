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
            AudioManager.Instance.PlayMusic("Assets/ResHotfix/MainBundle/Audio/music.wav");
        }

        private void OnDestroy()
        {
            // MainCtrl.Instance.RemoveMainView();
        }

        public void Regain()
        {
            BulletTimeManager.Instance.Unpause();
            AudioManager.Instance.PlayMusic("Assets/ResHotfix/MainBundle/Audio/music.wav");
            AdRegainCtrl.Instance.RemoveAdView();
            Player.transform.position = new Vector3(0, Player.transform.position.y+5,
                Player.transform.position.z);
        }
        
        public void Reset()
        {
            BulletTimeManager.Instance.Unpause();
            AudioManager.Instance.PlayMusic("Assets/ResHotfix/MainBundle/Audio/music.wav");
            CameraFollow.Instance.SetTarget(Player.transform);
            AdRegainCtrl.Instance.RemoveAdView();
            MainCtrl.Instance.RemoveMainView();
            Player.transform.position = _originPos;
            MainCtrl.Instance.ResetData();
            MainCtrl.Instance.ShowMainView();
        }
    }
}