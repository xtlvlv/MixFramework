using System.Collections.Generic;
using BaseFramework.Core;
using UnityEngine;
using UnityEngine.EventSystems;

namespace ScriptsHotfix
{
    public class LevelManager: Singleton<LevelManager>,ISingletonAwake,ISingletonUpdate
    {
        private GameObject _curLevelObg;
        public void Awake()
        {
        }


        public void Update()
        {
            
        }

        public void LoadLevel(int a_level)
        {
            _curLevelObg = SimplePool.Spawn(AssetMgr.Load<GameObject>("Assets/ResHotfix/MainBundle/Level/Level1.prefab"), GameObject.Find("Level").transform);
        }
      
    }
}