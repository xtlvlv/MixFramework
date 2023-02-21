using System;
using System.Collections;
using System.Threading;
using BaseFramework;
using BaseFramework.Core;
using UnityEngine;
using UnityEngine.Advertisements;

namespace ScriptsHotfix
{
    // 启动入口，挂在初始prefab上
    public class GameApp: SingleObject<GameApp>
    {
        // public static string GameId = "3260921";
        // public static string PlacementId = "Must001";
        
        public static string GameId = "5174164";
        // public static string GameId = "DemoAd";
        public static string PlacementId = "Interstitial_Android";


        private void Awake()
        {
            Log.Info("Game start");
            
            Application.runInBackground = true;
            Application.targetFrameRate = 60;
            Screen.sleepTimeout = SleepTimeout.NeverSleep;
            
            // 其他线程回掉到主线程
            SynchronizationContext.SetSynchronizationContext(MainThreadSynchronizationContext.Instance);
            
            GameSingleton.AddSingleton<SingleExample>();
            GameSingleton.AddSingleton<ExcelDataManager>();
            GameSingleton.AddSingleton<NetManager>();
            GameSingleton.AddSingleton<UIManager>();
            GameSingleton.AddSingleton<AudioManager>();
            GameSingleton.AddSingleton<EventManager>();
            GameSingleton.AddSingleton<InputManager>();
            GameSingleton.AddSingleton<StorageManager>();
            GameSingleton.AddSingleton<BulletTimeManager>();
            GameSingleton.AddSingleton<LevelManager>();
            
            

            DontDestroyOnLoad(this);
        }

        private void Start()
        {
            // Advertisement.Initialize(GameId, true);
            
            LevelManager.Instance.LoadLevel(1);
        }

        private void Update()
        {
            GameSingleton.Update();
            MainThreadSynchronizationContext.Instance.Update();
        }

        private void LateUpdate()
        {
            GameSingleton.LateUpdate();
        }
        
        public void GameStartCoroutine(IEnumerator routine)
        {
            StartCoroutine(routine);
        }
    }
}