using Cysharp.Text;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utility
{
    public static DateTime TimeStampToDateTime(long timeStamp)
    {
        var dto = DateTimeOffset.FromUnixTimeSeconds(timeStamp);
        return dto.LocalDateTime;
    }
    public static string FormatDateTime(DateTime dt)
    {
        TimeSpan span = (DateTime.Now - dt).Duration();//表示取timespan绝对值
        if (span.TotalDays > 60)
        {
            return dt.ToString("yyyy-MM-dd");
        }
        else if (span.TotalDays > 30)
        {
            return "1个月前";
        }
        else if (span.TotalDays > 14)
        {
            return "2周前";
        }
        else if (span.TotalDays > 7)
        {
            return "1周前";
        }
        else if (span.TotalDays > 1)
        {
            return ZString.Format("{0}天前", (int)Math.Floor(span.TotalDays));
        }
        else if (span.TotalHours > 1)
        {
            return ZString.Format("{0}小时前", (int)Math.Floor(span.TotalHours));
        }
        else if (span.TotalMinutes > 1)
        {
            return ZString.Format("{0}分钟前", (int)Math.Floor(span.TotalMinutes));
        }
        else if (span.TotalSeconds >= 1)
        {
            return ZString.Format("{0}秒前", (int)Math.Floor(span.TotalSeconds));
        }
        else
        {
            return "1秒前";
        }
    }
}
