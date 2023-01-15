using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

/// <summary>
/// Auto Generate Class!!!
/// </summary>
[System.Serializable]
public class EDItem_LevelUpExp : EDItemBase
{
	/// <summary>
	/// 等级
	/// </summary>
	public string NeedExp;


    public int ID { get { return id; } }

    public static EDItem_LevelUpExp GetById(int id)
    {
        return ExcelDataManager.Instance.GetExcelItem<EDTable_LevelUpExp, EDItem_LevelUpExp>(id);
    }
}

/// <summary>
/// Auto Generate Class!!!
/// </summary>
public class EDTable_LevelUpExp : EDTableBase<EDItem_LevelUpExp>
{
    public static EDTable_LevelUpExp Get()
    {
        return ExcelDataManager.Instance.GetExcelTable<EDTable_LevelUpExp, EDItem_LevelUpExp>();
    }

    public static EDItem_LevelUpExp GetById(int id)
    {
        return ExcelDataManager.Instance.GetExcelItem<EDTable_LevelUpExp, EDItem_LevelUpExp>(id);
    }

    public static Dictionary<int, EDItem_LevelUpExp> GetAllItem()
    {
        return ExcelDataManager.Instance.GetAllExcelItem<EDTable_LevelUpExp, EDItem_LevelUpExp>();
    }
}
