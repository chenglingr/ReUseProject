using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ReUse.Filter
{
    public class IsPostFromThisSiteAttribute : AuthorizeAttribute
    {
        //在action里添加本过滤器
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (filterContext.HttpContext != null)
            {
                if (filterContext.HttpContext.Request.UrlReferrer == null)
                {
                    throw new System.Web.HttpException("无效请求");
                }

                // 判断返回的url是否是自己所在站点 发布时要修改
                if (filterContext.HttpContext.Request.UrlReferrer.Host != "localhost")
                {
                    throw new System.Web.HttpException("不是从本站提交的请求");
                }
            }
        }
    }
}