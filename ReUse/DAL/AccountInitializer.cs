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
            var accounts = new List<Models.Account>
            {
                new Models.Account {Email="test@qq.com",PassWord="888888",Address="中山" },
                new Models.Account {Email="admin@qq.com",PassWord="888888",Address="广州" }
            };
            accounts.ForEach(s => context.Accounts.Add(s));
            context.SaveChanges();
           
        }
    }
}