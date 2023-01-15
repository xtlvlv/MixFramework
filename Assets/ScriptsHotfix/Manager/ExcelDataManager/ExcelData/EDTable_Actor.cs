using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

/// <summary>
/// Auto Generate Class!!!
/// </summary>
[System.Serializable]
public class EDItem_Actor : EDItemBase
{
	/// <summary>
	/// 名字
	/// </summary>
	public string Name;

	/// <summary>
	/// 预制体路径
	/// </summary>
	public string PrefabPath;

	/// <summary>
	/// 标签
	/// </summary>
	public string Tag;

	/// <summary>
	/// 类型
	/// </summary>
	public int Type;

	/// <summary>
	/// 特性
	/// </summary>
	public bool CanBeKill;

	/// <summary>
	/// 移动速度
	/// </summary>
	public float Speed;

	/// <summary>
	/// 血量倍率
	/// </summary>
	public float HpRate;

	/// <summary>
	/// 攻击倍率
	/// </summary>
	public float AtkRate;

	/// <summary>
	/// 防御倍率
	/// </summary>
	public float DefRate;

	/// <summary>
	/// 死亡掉落经验
	/// </summary>
	public int DeadExp;

	/// <summary>
	/// 警戒范围
	/// </summary>
	public float WarningRange;

	/// <summary>
	/// 攻击范围
	/// </summary>
	public float AtkRange;


    public int ID { get { return id; } }

    public static EDItem_Actor GetById(int id)
    {
        return ExcelDataManager.Instance.GetExcelItem<EDTable_Actor, EDItem_Actor>(id);
    }
}

/// <summary>
/// Auto Generate Class!!!
/// </summary>
public class EDTable_Actor : EDTableBase<EDItem_Actor>
{
    public static EDTable_Actor Get()
    {
        return ExcelDataManager.Instance.GetExcelTable<EDTable_Actor, EDItem_Actor>();
    }

    public static EDItem_Actor GetById(int id)
    {
        return ExcelDataManager.Instance.GetExcelItem<EDTable_Actor, EDItem_Actor>(id);
    }

    public static Dictionary<int, EDItem_Actor> GetAllItem()
    {
        return ExcelDataManager.Instance.GetAllExcelItem<EDTable_Actor, EDItem_Actor>();
    }
}
