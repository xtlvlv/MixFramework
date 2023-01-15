using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace ScriptsHotfix
{

    public static class StringExtension
    {
        static char[] _seParator = new char[3] { '(', ',', ')' };

        public static int ToInt(this string value)
        {
            int v = 0;
            if (int.TryParse(value, out v) == false)
            {
                Debug.LogError(string.Format("value={0} can not convert to int", value));
            }

            return v;
        }

        public static Int16 ToInt16(this string value)
        {
            Int16 v = 0;
            if (Int16.TryParse(value, out v) == false)
            {
                Debug.LogError(string.Format("value={0} can not convert to int16", value));
            }

            return v;
        }

        public static Int32 ToInt32(this string value)
        {
            int v = 0;
            if (int.TryParse(value, out v) == false)
            {
                Debug.LogError(string.Format("value={0} can not convert to int", value));
            }

            return v;
        }

        public static Int64 ToInt64(this string value)
        {
            Int64 v = 0;
            if (Int64.TryParse(value, out v) == false)
            {
                Debug.LogError(string.Format("value={0} can not convert to int64", value));
            }

            return v;
        }

        public static UInt16 ToUInt16(this string value)
        {
            UInt16 v = 0;
            if (UInt16.TryParse(value, out v) == false)
            {
                Debug.LogError(string.Format("value={0} can not convert to uint16", value));
            }

            return v;
        }

        public static UInt32 ToUInt32(this string value)
        {
            UInt32 v = 0;
            if (UInt32.TryParse(value, out v) == false)
            {
                Debug.LogError(string.Format("value={0} can not convert to uint32", value));
            }

            return v;
        }

        public static UInt64 ToUInt64(this string value)
        {
            UInt64 v = 0;
            if (UInt64.TryParse(value, out v) == false)
            {
                Debug.LogError(string.Format("value={0} can not convert to uint64", value));
            }

            return v;
        }

        public static bool ToBool(this string value)
        {
            string s = value.ToLower();
            return s.Equals("true");
        }

        public static float ToFloat(this string value)
        {
            float v = 0;
            if (float.TryParse(value, out v) == false)
            {
                Debug.LogFormat("value:{0} can not convert to float", value);
            }

            return v;
        }

        public static Vector2 ToVector2(this string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return Vector2.zero;
            }

            string[] array = value.Split(_seParator);
            return new Vector2(array[1].ToFloat(), array[2].ToFloat());
        }

        public static Vector3 ToVector3(this string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return Vector3.zero;
            }

            string[] array = value.Split(_seParator);
            return new Vector3(array[1].ToFloat(), array[2].ToFloat(), array[3].ToFloat());
        }

        public static Vector3Int ToVector3Int(this string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return Vector3Int.zero;
            }

            string[] array = value.Split('~');
            return new Vector3Int(array[0].ToInt(), array[1].ToInt(), array[2].ToInt());
        }

        public static Vector4 ToVector4(this string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return Vector3.zero;
            }

            string[] array = value.Split(_seParator);
            return new Vector4(array[1].ToFloat(), array[2].ToFloat(), array[3].ToFloat(), array[4].ToFloat());
        }

        public static bool IsNumber(this string s)
        {
            if (s.Length == 0)
            {
                return false;
            }

            for (int i = 0; i < s.Length; i++)
            {
                if ((s[i] < '0' || s[i] > '9') && (s[i] != '.' || i == s.Length - 1 || s.IndexOf('.', i + 1) != -1) &&
                    (s[i] != '-' || i != 0))
                {
                    return false;
                }
            }

            return true;
        }

        public static KeyValuePair<int, float> ToKvIntFloat(this string s)
        {
            if (string.IsNullOrEmpty(s))
            {
                return new KeyValuePair<int, float>(0, 0);
            }

            string[] array = s.Split('~');
            return new KeyValuePair<int, float>(array[0].ToInt(), array[1].ToFloat());
        }
    }

}
