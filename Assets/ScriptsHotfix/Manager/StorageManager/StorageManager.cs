using System;
using System.IO;
using System.Text.RegularExpressions;
using BaseFramework.Core;
using UnityEngine;

namespace ScriptsHotfix
{
    public class StorageManager: Singleton<StorageManager>,ISingletonAwake
    {
        private string _filePath = Application.streamingAssetsPath + "/";

        public void Awake()
        {
            string path = string.Empty;
            switch (Application.platform)
            {
                case RuntimePlatform.WindowsPlayer:
                    path = Application.streamingAssetsPath;
                    break;
                case RuntimePlatform.WindowsEditor:
                    path = Application.streamingAssetsPath;
                    break;
                case RuntimePlatform.Android:
                    path = Application.persistentDataPath;
                    break;
                case RuntimePlatform.IPhonePlayer:
                    path = Application.persistentDataPath;
                    break;
                default:
                    path = Application.streamingAssetsPath;
                    break;
            }

            _filePath = path + "/";
        }
        
        public void SaveJson2Prefs(string key, string value)
        {
            PlayerPrefs.SetString(key, value);
        }

        public string LoadJsonFromPrefs(string key, string defaultValue = "")
        {
            return PlayerPrefs.GetString(key, defaultValue);
        }
        
        public void SaveJson(string filename, string data)
        {
            using (StreamWriter sw  = new StreamWriter(_filePath+filename, false))
            {
                Regex reg = new Regex(@"(?i)\\[uU]([0-9a-f]{4})");
                var ss = reg.Replace(data, delegate(Match m) { return ((char)Convert.ToInt32(m.Groups[1].Value, 16)).ToString(); });
                sw.WriteLine(ss);
            }
        }

        public string LoadJson(string filename, string defaultValue = "")
        {
            try
            {
                StreamReader sr = new StreamReader(_filePath + filename);
                defaultValue = sr.ReadToEnd();
                sr.Dispose();
            }
            catch (Exception e)
            {
                Log.Info("sr is null. {0}", e);
            }

            return defaultValue;
        }
    }
}