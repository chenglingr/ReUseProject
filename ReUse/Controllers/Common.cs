using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;

public enum Style1
{
    图书,
    手机,
    数码,
    电脑,
    食品,
    服饰,
    鞋靴,
    玩具,
    电器,
    家具,
    化妆品,
    母婴用品,
    其他类别
}
public enum News
{
    全新,
    九成新,
    八成新,
    七成新,
    六成新,
    五成新,
    四成新,
    三成新
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
    西区,
    南区,
    石岐区,
    火炬区,
    五桂山,
    其他地区
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