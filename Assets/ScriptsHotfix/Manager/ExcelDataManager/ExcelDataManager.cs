using System;
using System.Collections;
using System.Collections.Generic;
using BaseFramework.Core;
using UnityEngine;

public class ExcelDataManager : Singleton<ExcelDataManager>
{
    
    public const string ASSET_OUTPUT_PATH = "Assets/ResHotfix/MainBundle/ExcelData/";
    
    public Dictionary<Type, object> dic = new Dictionary<Type, object>();

    public ExcelDataManager()
    {
        //JsonMapper.RegisterExporter((float val, JsonWriter jw) =>
        //{
        //    jw.Write(double.Parse(val.ToString()));
        //});
        //JsonMapper.RegisterImporter((double val) =>
        //{
        //    return (float)val;
        //});
    }

    /// <summary>
    /// 获取一张表
    /// </summary>
    /// <typeparam name="K">Table</typeparam>
    /// <typeparam name="V">Item</typeparam>
    /// <returns></returns>
    public K GetExcelTable<K, V>() where K : EDTableBase<V> where V : EDItemBase
    {
        Type type = typeof(K);
        if (dic.ContainsKey(type) && dic[type] is K)
            return dic[type] as K;

        var jsonName = typeof(V).Name.Replace("EDItem_", "") + ".json";
        var json = AssetMgr.Load<TextAsset>(ASSET_OUTPUT_PATH + jsonName);
        if (json == null) return null;

        var asset = LitJson.JsonMapper.ToObject<K>(json.text);
        if (asset != null) dic.Add(type, asset);

        return asset;
    }

    /// <summary>
    /// 获取一个数据
    /// </summary>
    /// <typeparam name="K">Table</typeparam>
    /// <typeparam name="V">Item</typeparam>
    /// <param name="id"></param>
    /// <returns></returns>
    public V GetExcelItem<K, V>(int id) where K : EDTableBase<V> where V : EDItemBase
    {
        var excelData = GetExcelTable<K, V>();

        if (excelData == null) return null;

        return excelData.GetExcelItem(id);
    }

    public Dictionary<int, V> GetAllExcelItem<K, V>() where K : EDTableBase<V> where V : EDItemBase
    {
        var excelData = GetExcelTable<K, V>();

        if (excelData == null) return null;

        return excelData.DicDatas;
    }
}
