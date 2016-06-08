using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace ReUse.DAL
{
    public class AccountContext:DbContext
    {
        public AccountContext():base("AccountContext") { }
        public DbSet<ReUse.Models.Account> Accounts { get; set; }
        public DbSet<ReUse.Models.Article> Articles { get; set; }
        public DbSet<ReUse.Models.Comment> Comments { get; set; }
        public DbSet<ReUse.Models.Goods> Goodss { get; set; }
        public DbSet<ReUse.Models.Tran> Trans { get; set; }
        public DbSet<ReUse.Models.User> Users { get; set; }
        public DbSet<ReUse.Models.Want> Wants { get; set; }
       
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}