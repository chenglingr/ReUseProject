using System.Web;
using System.Web.Mvc;

namespace ReUse
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
          //  filters.Add(new Filter.IsPostFromThisSiteAttribute()); //添加过滤器
        }
    }
}
