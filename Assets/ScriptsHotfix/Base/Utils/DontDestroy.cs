using UnityEngine;

namespace ScriptsHotfix
{
    public class DontDestroy: MonoBehaviour
    {
        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}