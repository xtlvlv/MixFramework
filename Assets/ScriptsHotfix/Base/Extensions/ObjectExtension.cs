using System;
using System.Collections.Generic;
using System.Reflection;

namespace ScriptsHotfix
{
    public static class ObjectTools
    {
        
           // 深拷贝，用到了Linq
    //     public static int Copy(object destination, object source)
    //     {
    //         if (destination == null || source == null)
    //         {
    //             return 0;
    //         }
    //         return ObjectTools.Copy(destination, source, source.GetType());
    //     }
    //
    //     public static int Copy(object destination, object source, Type type)
    //     {
    //         return ObjectTools.Copy(destination, source, type, null);
    //     }
    //
    //     public static int Copy(object destination, object source, Type type, IEnumerable<string> excludeName)
    //     {
    //         if (destination == null || source == null)
    //         {
    //             return 0;
    //         }
    //         if (excludeName == null)
    //         {
    //             excludeName = new List<string>();
    //         }
    //         int num = 0;
    //         Type type2 = destination.GetType();
    //         FieldInfo[] fields = type.GetFields();
    //         for (int i = 0; i < fields.Length; i++)
    //         {
    //             FieldInfo fieldInfo = fields[i];
    //             if (!excludeName.Contains(fieldInfo.Name))
    //             {
    //                 try
    //                 {
    //                     FieldInfo field = type2.GetField(fieldInfo.Name);
    //                     if (field != null && field.FieldType == fieldInfo.FieldType)
    //                     {
    //                         field.SetValue(destination, fieldInfo.GetValue(source));
    //                         num++;
    //                     }
    //                 }
    //                 catch
    //                 {
    //                 }
    //             }
    //         }
    //         PropertyInfo[] properties = type.GetProperties();
    //         for (int j = 0; j < properties.Length; j++)
    //         {
    //             PropertyInfo propertyInfo = properties[j];
    //             if (!excludeName.Contains(propertyInfo.Name))
    //             {
    //                 try
    //                 {
    //                     PropertyInfo property = type2.GetProperty(propertyInfo.Name);
    //                     if (property != null && property.PropertyType == propertyInfo.PropertyType && property.CanWrite && propertyInfo.CanRead)
    //                     {
    //                         property.SetValue(destination, propertyInfo.GetValue(source, null), null);
    //                         num++;
    //                     }
    //                 }
    //                 catch
    //                 {
    //                 }
    //             }
    //         }
    //         return num;
    //     }
    }
}