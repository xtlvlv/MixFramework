using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

/// <summary>
/// Auto Generate Class!!!
/// </summary>
[System.Serializable]
public class EDItem_ViewConfig : EDItemBase
{
	/// <summary>
	/// 预制体路径
	/// </summary>
	public string PrefabPath;

	/// <summary>
	/// 排序
	/// </summary>
	public int SortingOrder;


    public int ID { get { return id; } }

    public static EDItem_ViewConfig GetById(int id)
    {
        return ExcelDataManager.Instance.GetExcelItem<EDTable_ViewConfig, EDItem_ViewConfig>(id);
    }
}

/// <summary>
/// Auto Generate Class!!!
/// </summary>
public class EDTable_ViewConfig : EDTableBase<EDItem_ViewConfig>
{
    public static EDTable_ViewConfig Get()
    {
        return ExcelDataManager.Instance.GetExcelTable<EDTable_ViewConfig, EDItem_ViewConfig>();
    }

    public static EDItem_ViewConfig GetById(int id)
    {
        return ExcelDataManager.Instance.GetExcelItem<EDTable_ViewConfig, EDItem_ViewConfig>(id);
    }

    public static Dictionary<int, EDItem_ViewConfig> GetAllItem()
    {
        return ExcelDataManager.Instance.GetAllExcelItem<EDTable_ViewConfig, EDItem_ViewConfig>();
    }
}
