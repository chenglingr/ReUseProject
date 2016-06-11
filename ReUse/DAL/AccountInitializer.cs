using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
namespace ReUse.DAL
{
    public class AccountInitializer:DropCreateDatabaseIfModelChanges<AccountContext>
    {
        protected override void Seed(AccountContext context)
        {
            
            var accounts = new List<Models.User>
            {
                new Models.User { UserName="匿名", UserPwd="123456", RealName="匿名", QQ="",WeiXin="", Tel="",Email="", Score=0,State=1},
                new Models.User { UserName="admin", UserPwd="123456", RealName="管理员", QQ="7675645",WeiXin="admin", Tel="1333888797",Email="admin@qq.com", Score=10,State=1},
                new Models.User { UserName="test", UserPwd="123456", RealName="测试用户", QQ="123456",WeiXin="testweixin", Tel="13855553249",Email="test@qq.com", Score=10,State=1},
                new Models.User { UserName="test1", UserPwd="123456", RealName="测试用户1", QQ="654321",WeiXin="testweixin1", Tel="15955553249",Email="test1@qq.com", Score=10,State=1},
               
            };
            accounts.ForEach(s => context.Users.Add(s));
            context.SaveChanges();
           
        }
    }
}