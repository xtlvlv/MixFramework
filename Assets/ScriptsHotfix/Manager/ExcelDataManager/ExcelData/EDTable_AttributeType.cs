using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

/// <summary>
/// Auto Generate Class!!!
/// </summary>
[System.Serializable]
public class EDItem_AttributeType : EDItemBase
{
	/// <summary>
	/// 属性名
	/// </summary>
	public string PropertyName;

	/// <summary>
	/// 最大值属性
	/// </summary>
	public int MaxValueProperty;

	/// <summary>
	/// 最小值
	/// </summary>
	public float MinValue;

	/// <summary>
	/// 最大值
	/// </summary>
	public float MaxValue;

	/// <summary>
	/// 格式类型
	/// </summary>
	public bool FormatType;


    public int ID { get { return id; } }

    public static EDItem_AttributeType GetById(int id)
    {
        return ExcelDataManager.Instance.GetExcelItem<EDTable_AttributeType, EDItem_AttributeType>(id);
    }
}

/// <summary>
/// Auto Generate Class!!!
/// </summary>
public class EDTable_AttributeType : EDTableBase<EDItem_AttributeType>
{
    public static EDTable_AttributeType Get()
    {
        return ExcelDataManager.Instance.GetExcelTable<EDTable_AttributeType, EDItem_AttributeType>();
    }

    public static EDItem_AttributeType GetById(int id)
    {
        return ExcelDataManager.Instance.GetExcelItem<EDTable_AttributeType, EDItem_AttributeType>(id);
    }

    public static Dictionary<int, EDItem_AttributeType> GetAllItem()
    {
        return ExcelDataManager.Instance.GetAllExcelItem<EDTable_AttributeType, EDItem_AttributeType>();
    }
}
