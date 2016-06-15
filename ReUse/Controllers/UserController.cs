using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace ReUse.Controllers
{
    public class UserController : Controller
    {
        private DAL.AccountContext db = new DAL.AccountContext();
        public ActionResult Index()
        {
            return View();
        }
        //修改用户
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (!id.HasValue)
            {
                if (Session["UserID"] != null)
                {
                    id = int.Parse(Session["UserID"].ToString());
                }
            }
            Models.User acc = db.Users.Find(id);
            return View(acc);
        }

        [HttpPost]
        [Filter.AntiSqlInject]
        public ActionResult Edit(Models.User acc)
        {
            //加上 using System.Data.Entity;
            db.Entry(acc).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("MyIndexList", "Goods");
        }
        #region 登录
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
       // [Filter.AntiSqlInject]
        public ActionResult Login(string UserName, string UserPwd)
        {
            var acc = db.Users.Where(b => b.UserName == UserName & b.UserPwd == UserPwd);
            if (acc.Count() > 0)
            {
                FormsAuthentication.SetAuthCookie(UserName, false);//新增=acc
                Session["UserID"] = acc.First().ID;//新增
                return RedirectToAction("MyIndexList", "Goods");
            }
            else
            {
                ModelState.AddModelError("Error", "登陆失败，请重输用户名、密码");//新增
                return View();
            }
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session["UserID"] = null;//新增
            ViewBag.LoginState = "已退出登录";
            return RedirectToAction("Login");
        }
        #endregion
        #region 注册
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [Filter.AntiSqlInject]
        public ActionResult Register(Models.User acc)
        {
            string name = acc.UserName;
            int n=  db.Users.Where(b => b.UserName == name).Count();
            if (n >= 1)
            {
                ModelState.AddModelError("Error", "该用户名已被占用，请重新选择用户名");//
                return View();
            }

            acc.Score = 10;
            acc.State = 1;
            if (ModelState.IsValid)//执行服务端的验证
            {
                db.Users.Add(acc);//增加数据
                db.SaveChanges();//保存 
                ViewBag.LoginState = "注册成功,请登录";
                return RedirectToAction("Login");//跳转
            }
            else
            {
                ModelState.AddModelError("Error", "注册失败，请重填资料");//
                return View();
            }
         }
        #endregion
        
        public ActionResult GetAddAdminLink()
        {
            if (User.Identity.Name == "reuseadmin")//如果是管理员账户
            {
                return PartialView("_AddAdminLink");//导入局部视图
            }
            else
            {
                return new EmptyResult();
            }
        }
    }
}