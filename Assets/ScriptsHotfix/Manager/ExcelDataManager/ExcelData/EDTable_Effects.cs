using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

/// <summary>
/// Auto Generate Class!!!
/// </summary>
[System.Serializable]
public class EDItem_Effects : EDItemBase
{
	/// <summary>
	/// 预制体路径
	/// </summary>
	public string PrefabPath;


    public int ID { get { return id; } }

    public static EDItem_Effects GetById(int id)
    {
        return ExcelDataManager.Instance.GetExcelItem<EDTable_Effects, EDItem_Effects>(id);
    }
}

/// <summary>
/// Auto Generate Class!!!
/// </summary>
public class EDTable_Effects : EDTableBase<EDItem_Effects>
{
    public static EDTable_Effects Get()
    {
        return ExcelDataManager.Instance.GetExcelTable<EDTable_Effects, EDItem_Effects>();
    }

    public static EDItem_Effects GetById(int id)
    {
        return ExcelDataManager.Instance.GetExcelItem<EDTable_Effects, EDItem_Effects>(id);
    }

    public static Dictionary<int, EDItem_Effects> GetAllItem()
    {
        return ExcelDataManager.Instance.GetAllExcelItem<EDTable_Effects, EDItem_Effects>();
    }
}
