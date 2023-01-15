using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace ScriptsHotfix
{
    public static class VectorExtension
    {

        public static float GetHorizontalDistance(this Vector3 a, Vector3 b)
        {
            Vector3 v1 = a;
            v1.y = 0;
            Vector3 v2 = b;
            v2.y = 0;
            return Vector3.Distance(v1, v2);
        }
        
        public static string Vector3ToString(this Vector3 pos)
        {
            return $"({pos.x:0.00},{pos.y:0.00},{pos.z:0.00})";
        }
        
        public static string Vector2ToString(this Vector2 pos)
        {
            return $"({pos.x:0.00},{pos.y:0.00})";
        }
        
        public static float Distance(this Vector3 s, Vector3 target)
        {
            return Vector3.Distance(s, target);
        }
    }

}