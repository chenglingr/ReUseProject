using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ReUse.Filter
{
    public class AntiSqlInjectAttribute : FilterAttribute, IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext filterContext)
        {

        }
        //给action前面加上 [AntiSqlInject]
        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var actionParameters = filterContext.ActionDescriptor.GetParameters();
            foreach (var p in actionParameters)
            {
                if (p.ParameterType == typeof(string))
                {
                    if (filterContext.ActionParameters[p.ParameterName] != null)
                    {
                        filterContext.ActionParameters[p.ParameterName] = StringHelper.FilterSql(filterContext.ActionParameters[p.ParameterName].ToString());
                    }
                }
            }
        }
    }
}