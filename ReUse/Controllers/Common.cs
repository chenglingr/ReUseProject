﻿using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;

public enum Style1
{
    图书,
    电器,
    家具
}
public enum News
{
    全新,
    九成新,
    八成新,
    七成新
}
public enum ChangeType
{
    现金交易,
    积分交换,
    实物交换    
}
public enum Area
{
    东区,
    石岐区,
    火炬区
}


public class EnumExt
{
    static public List<ListItem> ToListItem<T>()
    {
        List<ListItem> li = new List<ListItem>();
        foreach (int s in Enum.GetValues(typeof(T)))
        {
            li.Add(new ListItem
            {
                Value = s.ToString(),
                Text = Enum.GetName(typeof(T), s)
            }
            );
        }
        return li;
    }
}
public class UploadFileResult
{
    public string FileName { get; set; }
    public string FilePath { get; set; }
}