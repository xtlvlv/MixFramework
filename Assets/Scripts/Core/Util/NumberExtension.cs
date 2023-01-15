using System;

public static class NumberExtension
{
    public static string FormatCoin(this int self)
    {
        return FormatCoin((long)self);
    }

    public static string FormatCoin(this long self)
    {
        double size = self;
        string[] units = new string[] { "B", "K", "M", "G", "T", "P" };
        double mod = 1000;
        int i = 0;
        while (size >= mod)
        {
            size /= mod;
            i++;
        }
        return Math.Round(size, 2) + units[i];
    }
    
    public static string FormatByte(this int self)
    {
        return FormatByte((long)self);
    }
    
    public static string FormatByte(this long self)
    {
        double size = self;
        string[] units = new string[] { "B", "KB", "MB", "G", "T", "P" };
        double mod = 1000;
        int i = 0;
        while (size >= mod)
        {
            size /= mod;
            i++;
        }
        return Math.Round(size, 2) + units[i];
    }

    public static string FormatRMB(this int self)
    {
        return FormatRMB((long)self);
    }

    public static string FormatRMB(this long self)
    {
        double value = self;
        if (value < 1000d)
        {
            return value.ToString("f0");// + "$";
        }
        if (value < 10000d)
        {
            value /= 1000d;
            var str = value.ToString("0.##");
            return str + "千";
        }

        if (value < 100000000d)//1w-1e显示为0.n万
        {
            value /= 10000d;
            var str = value.ToString("0.##");
            return str + "万";
        }

        if (value < 1000000000000d)//1e-1m显示为0.n亿
        {
            value /= 100000000d;
            var str = value.ToString("0.##");
            return str + "亿";
        }

        if (value < 10000000000000000d)//1m-1g显示为0.n兆
        {
            value /= 1000000000000d;
            var str = value.ToString("0.##");
            return str + "兆";
        }

        if (value < 100000000000000000000d)//1g-1t显示为0.n京
        {
            value /= 10000000000000000d;
            var str = value.ToString("0.##");
            return str + "京";
        }

        {
            value /= 100000000000000000000d;
            var str = value.ToString("0.##");
            return str + "稊";
        }
    }
}