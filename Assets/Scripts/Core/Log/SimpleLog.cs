using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public enum LogLevel
{
    None = 0,
    Exception = 1,
    Error = 2,
    Warning = 3,
    Normal = 4,
    Max = 5,
}

public static class SimpleLog
{

    public static void LogInfo(this object selfMsg)
    {
        I(selfMsg);
    }

    public static void LogWarning(this object selfMsg)
    {
        W(selfMsg);
    }

    public static void LogError(this object selfMsg)
    {
        E(selfMsg);
    }

    public static void LogException(this Exception selfExp)
    {
        E(selfExp);
    }

    private static LogLevel mLogLevel = LogLevel.Normal;

    public static LogLevel Level
    {
        get { return mLogLevel; }
        set { mLogLevel = value; }
    }

    public static void I(object msg, params object[] args)
    {
        if (mLogLevel < LogLevel.Normal)
        {
            return;
        }

        if (args == null || args.Length == 0)
        {
            Debug.Log(msg);
        }
        else
        {
            Debug.LogFormat(msg.ToString(), args);
        }
    }

    public static void E(Exception e)
    {
        if (mLogLevel < LogLevel.Exception)
        {
            return;
        }
        Debug.LogException(e);
    }

    public static void E(object msg, params object[] args)
    {
        if (mLogLevel < LogLevel.Error)
        {
            return;
        }

        if (args == null || args.Length == 0)
        {
            Debug.LogError(msg);
        }
        else
        {
            Debug.LogError(string.Format(msg.ToString(), args));
        }

    }

    public static void W(object msg)
    {
        if (mLogLevel < LogLevel.Warning)
        {
            return;
        }

        Debug.LogWarning(msg);
    }

    public static void W(string msg, params object[] args)
    {
        if (mLogLevel < LogLevel.Warning)
        {
            return;
        }

        Debug.LogWarning(string.Format(msg, args));
    }
    
    private static string logDir;
    private static string logFileName;
    private static string fullPath;
 
    public static void InitLogger()
    {
        logDir = Application.streamingAssetsPath + "/Logs/";
        string timeStr = DateTime.Now.ToString("MM-dd-HH-mm-ss");
        logFileName = timeStr+".log";
        fullPath = logDir + logFileName;
        if (File.Exists(fullPath)) File.Delete(fullPath);
        Debug.Log(fullPath.Replace(logFileName, ""));
        if (Directory.Exists(fullPath.Replace(logFileName, "")))
        {
            FileStream fs = File.Create(fullPath);
            fs.Close();
            Application.logMessageReceived += logCallBack;
            Debug.Log("日志输出到文件");
        }
        else
        {
            Debug.LogError("directory is not exist");
        }
    }
 
 
    private static void logCallBack(string condition, string stackTrace, LogType type)
    {
        string logStr = string.Empty;
        string timeStr = DateTime.Now.ToString("yyyyMMdd HH:mm:ss ");
        timeStr = "[" + timeStr + (GetTimeStampMilliSecond()%1000).ToString() + "]";
        
        switch (type)
        {
            case LogType.Log:
            {
                logStr = string.Format("{0}：{1}\n" , timeStr , condition);
            }
                break;
            case LogType.Assert:
            case LogType.Warning:
            case LogType.Exception:
            case LogType.Error:
            {
                if (string.IsNullOrEmpty(stackTrace))
                {
                    // 发布到对应平台后，调用堆栈获取不到。使用 Environment.StackTrace 获取调用堆栈
                    logStr = string.Format("{0}：{1} {2}\n{3}" , timeStr, type , condition , Environment.StackTrace);
                }
                else
                {
                    logStr = string.Format("{0}：{1} {2}\n{3}" , timeStr, type , condition , stackTrace);
                }
            }
                break;
        }
        
        if (File.Exists(fullPath))
        {
            using (StreamWriter sw = File.AppendText(fullPath))
            {
                sw.WriteLine(logStr);
            }
        }
    }
    
    /// <summary>
    /// 获取时间戳-单位毫秒
    /// </summary>
    /// <returns></returns>
    public static long GetTimeStampMilliSecond()
    {
    
        TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
        try
        {
            return Convert.ToInt64(ts.TotalMilliseconds);
        }
        catch (Exception ex)
        {
            Debug.LogError("获得毫秒时间戳异常"+ex.ToString());
            return 0;
        }
    }

}
