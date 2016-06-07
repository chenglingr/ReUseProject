using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using System.Configuration;
using System.Web.Security;

namespace ReUse.Controllers
{
    public class AccountController : Controller
    {
        private DAL.AccountContext db = new DAL.AccountContext();
        // GET: Account
        //  [Authorize]
        public ActionResult GetAddAdminLink()
        {
            if (User.Identity.Name=="admin@qq.com")//如果是管理员账户
            {
                 return PartialView("_AddAdminLink");//导入局部视图
            }
            else
             {
                return new EmptyResult();
             }
       }
        public ActionResult Index3(string search)
        {
            List<Models.Account> Accounts;
            if (!string.IsNullOrEmpty(search))
            {
                Accounts = db.Accounts.Where(u => u.Email.Contains(search)).ToList();
            }
            else
            {
                Accounts = db.Accounts.ToList();
            }
            ViewModels.AccountListViewModel aLVM = new ViewModels.AccountListViewModel();
          
            aLVM.Accounts = Accounts;
            return View(aLVM);
        }
        [Authorize(Users = "admin@qq.com")]
        public ActionResult Index(int? page)
        {
            //用户列表
            var acc = db.Accounts;
            //第几页
            int pageNumber = page ?? 1;
            //每页显示多少条
            int pageSize = int.Parse(ConfigurationManager.AppSettings["pageSize"]);
            //根据邮箱排序
            var  acc1=  acc.OrderBy(x=>x.Address);
            //通过topagelist扩展方法进行分页
            IPagedList<ReUse.Models.Account> pageList = acc1.ToPagedList(pageNumber, pageSize);
            return View(pageList);
        }

        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(Models.Account account)
        {
            var acc = db.Accounts.Where(b => b.Email == account.Email & b.PassWord == account.PassWord);
            if (acc.Count() > 0) {
                // 
                ViewBag.LoginState = "成功";
                FormsAuthentication.SetAuthCookie(account.Email, false);//新增=acc
                Session["UserID"] = acc.First().ID;//新增
                return RedirectToAction("Detail");
             //   RedirectToAction(“ActionName”);  
             //   RedirectToAction(“ActionName”, "ControllerName");  
            }
            else
            { 
                ViewBag.LoginState = "失败";
                ModelState.AddModelError("CredentialError", "Invalid Username or Password");//新增
                return View();
            }
          
        }
       
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(Models.Account acc)
        {
            if (ModelState.IsValid)//执行服务端的验证
            { 
                db.Accounts.Add(acc);//增加数据
                db.SaveChanges();//保存
            }
            return RedirectToAction("Index");//跳转
          
        }
       
        public ActionResult Detail(int? id)
        {
            if(!id.HasValue){
                if (Session["UserID"] != null)
                {
                    id = int.Parse(Session["UserID"].ToString());
                }
            }
            Models.Account acc = db.Accounts.Find(id);
           
            return View(acc);
        }
        //修改用户
        public ActionResult Edit(int? id)
        {
            Models.Account acc = db.Accounts.Find(id);
            return View(acc);
        }

        [HttpPost]
        public ActionResult Edit(Models.Account acc)
        {
            //加上 using System.Data.Entity;
            db.Entry(acc).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        //删除用户
        public ActionResult Delete(int? id)
        {
            Models.Account acc = db.Accounts.Find(id);
            return View(acc);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Models.Account sysUser = db.Accounts.Find(id);
            db.Accounts.Remove(sysUser);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session["UserID"] = null;//新增
            return RedirectToAction("Login");
        }
    }
}