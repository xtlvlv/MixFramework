using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

/// <summary>
/// Auto Generate Class!!!
/// </summary>
[System.Serializable]
public class EDItem_LevelAttribute : EDItemBase
{
	/// <summary>
	/// 等级
	/// </summary>
	public string Level;

	/// <summary>
	/// 生命
	/// </summary>
	public int Hp;

	/// <summary>
	/// 攻击
	/// </summary>
	public float Atk;

	/// <summary>
	/// 防御
	/// </summary>
	public float Def;


    public int ID { get { return id; } }

    public static EDItem_LevelAttribute GetById(int id)
    {
        return ExcelDataManager.Instance.GetExcelItem<EDTable_LevelAttribute, EDItem_LevelAttribute>(id);
    }
}

/// <summary>
/// Auto Generate Class!!!
/// </summary>
public class EDTable_LevelAttribute : EDTableBase<EDItem_LevelAttribute>
{
    public static EDTable_LevelAttribute Get()
    {
        return ExcelDataManager.Instance.GetExcelTable<EDTable_LevelAttribute, EDItem_LevelAttribute>();
    }

    public static EDItem_LevelAttribute GetById(int id)
    {
        return ExcelDataManager.Instance.GetExcelItem<EDTable_LevelAttribute, EDItem_LevelAttribute>(id);
    }

    public static Dictionary<int, EDItem_LevelAttribute> GetAllItem()
    {
        return ExcelDataManager.Instance.GetAllExcelItem<EDTable_LevelAttribute, EDItem_LevelAttribute>();
    }
}
