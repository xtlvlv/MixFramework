using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

/// <summary>
/// Auto Generate Class!!!
/// </summary>
[System.Serializable]
public class EDItem_Skill : EDItemBase
{
	/// <summary>
	/// 技能名
	/// </summary>
	public string Name;

	/// <summary>
	/// 技能图标路径
	/// </summary>
	public string Icon;

	/// <summary>
	/// 描述
	/// </summary>
	public string Desc;

	/// <summary>
	/// 技能CD
	/// </summary>
	public float CD;

	/// <summary>
	/// 目标类型
	/// </summary>
	public int HitTargetType;


    public int ID { get { return id; } }

    public static EDItem_Skill GetById(int id)
    {
        return ExcelDataManager.Instance.GetExcelItem<EDTable_Skill, EDItem_Skill>(id);
    }
}

/// <summary>
/// Auto Generate Class!!!
/// </summary>
public class EDTable_Skill : EDTableBase<EDItem_Skill>
{
    public static EDTable_Skill Get()
    {
        return ExcelDataManager.Instance.GetExcelTable<EDTable_Skill, EDItem_Skill>();
    }

    public static EDItem_Skill GetById(int id)
    {
        return ExcelDataManager.Instance.GetExcelItem<EDTable_Skill, EDItem_Skill>(id);
    }

    public static Dictionary<int, EDItem_Skill> GetAllItem()
    {
        return ExcelDataManager.Instance.GetAllExcelItem<EDTable_Skill, EDItem_Skill>();
    }
}
