using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace ScriptsHotfix
{
    public static class TransformExtension
    {
        public static bool CheckDistance(this Transform src, Transform tar, float dis)
        {
            if (src == null || tar == null)
            {
                return false;
            }
            return VectorExtension.GetHorizontalDistance(src.position, tar.position) < dis;
        }
        
        public static float GetDistance(this Transform src, Transform tar)
        {
            if (src == null || tar == null)
            {
                return 0;
            }
            Vector3 localPos = src.InverseTransformPoint(tar.position); // 将tar 位置转换为src 的局部坐标，从而计算长度
            return localPos.magnitude;
        }
        
        public static void GetAllComponents<T>(this Transform trans, List<T> pList) where T : Component
        {
            if (trans == null) return;
            for (int i = 0; i < trans.childCount; i++)
            {
                T t = trans.GetChild(i).GetComponent<T>();
                if (t != null)
                {
                    pList.Add(t);
                }
            }
        }
    }

}