using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

/// <summary>
/// Auto Generate Class!!!
/// </summary>
[System.Serializable]
public class EDItem_MonsterSpot : EDItemBase
{
	/// <summary>
	/// 据点名
	/// </summary>
	public string SpotName;

	/// <summary>
	/// 怪物id
	/// </summary>
	public int MonsterId;

	/// <summary>
	/// 游荡范围
	/// </summary>
	public float WanderRange;

	/// <summary>
	/// 警戒范围
	/// </summary>
	public float WarningRange;


    public int ID { get { return id; } }

    public static EDItem_MonsterSpot GetById(int id)
    {
        return ExcelDataManager.Instance.GetExcelItem<EDTable_MonsterSpot, EDItem_MonsterSpot>(id);
    }
}

/// <summary>
/// Auto Generate Class!!!
/// </summary>
public class EDTable_MonsterSpot : EDTableBase<EDItem_MonsterSpot>
{
    public static EDTable_MonsterSpot Get()
    {
        return ExcelDataManager.Instance.GetExcelTable<EDTable_MonsterSpot, EDItem_MonsterSpot>();
    }

    public static EDItem_MonsterSpot GetById(int id)
    {
        return ExcelDataManager.Instance.GetExcelItem<EDTable_MonsterSpot, EDItem_MonsterSpot>(id);
    }

    public static Dictionary<int, EDItem_MonsterSpot> GetAllItem()
    {
        return ExcelDataManager.Instance.GetAllExcelItem<EDTable_MonsterSpot, EDItem_MonsterSpot>();
    }
}
