
using UnityEngine;

namespace ScriptsHotfix
{
    public static class GameObjectExtensinon
    {
        public static void LocalReset(this Transform self)
        {
            self.localPosition = Vector3.zero;
            self.localRotation = Quaternion.identity;
            self.localScale = Vector3.one;
        }
        
        public static T GetOrAdd<T>(this GameObject go) where T : UnityEngine.Component
        {
            if (go == null) return null;
            T t = go.GetComponent<T>();
            if (t == null)
            {
                t = go.AddComponent<T>();
            }
            return t;
        }
    }
}