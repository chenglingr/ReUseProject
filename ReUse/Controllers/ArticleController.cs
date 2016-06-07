using PagedList;//分页
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Data.Entity;

namespace ReUse.Controllers
{
    public class ArticleController : Controller
    {
        private DAL.AccountContext db = new DAL.AccountContext();
        // GET: Article
        public ActionResult Index()
        {
            ViewModels.IndexViewModel ivm = new ViewModels.IndexViewModel();
            ivm.Accounts = db.Accounts.OrderByDescending(a => a.ID).Skip(0).Take(6).ToList();
            //0表示从第0个起 取前10个数据 按照点击量倒序排的序
            ivm.Articles= db.Articles.OrderByDescending(a => a.clickCount).Skip(0).Take(10).ToList();

            return View(ivm);
        }
        public ActionResult Detail(int?id)
        {

            Models.Article art = db.Articles.Find(id);
            if (art != null) {
                art.clickCount = art.clickCount + 1;//修改点击率
                art.lastClickTime = DateTime.Now;//修改点击时间
                db.Entry(art).State = EntityState.Modified;
                db.SaveChanges();//保存修改
            }
            return View(art);
        }
        public ActionResult IndexList(int? page,int? id)
        {
            //用户列表
            ViewBag.Uid = id;
            var art= db.Articles.Include(a=>a.Account) ; 
            if (id.HasValue) {//id不为空，则根据用户id筛选
                //必须引用using System.Data.Entity;才能用Include
                art =db.Articles.Where(b=>b.AccountID==id).Include(a => a.Account);
            }
            //第几页
            int pageNumber = page ?? 1;
            //每页显示多少条
            int pageSize = int.Parse(ConfigurationManager.AppSettings["pageSize"]);
            //根据创建时间 降序排序
            var art1 = art.OrderByDescending(x => x.createTime);
            //通过topagelist扩展方法进行分页
            IPagedList<ReUse.Models.Article> pageList = art1.ToPagedList(pageNumber, pageSize);
            return View(pageList);
            
        }
        public ActionResult MyIndexList(int? page)
        {
            int id=0;
            if (Session["UserID"] != null)//判断是否登录
                {
                    id = int.Parse(Session["UserID"].ToString());
                }
            var art = db.Articles.Where(b=>b.AccountID==id);
            //第几页
            int pageNumber = page ?? 1;
            //每页显示多少条
            int pageSize = int.Parse(ConfigurationManager.AppSettings["pageSize"]);
            //根据创建时间 降序排序
            var art1 = art.OrderByDescending(x => x.createTime);
            //通过topagelist扩展方法进行分页
            IPagedList<ReUse.Models.Article> pageList = art1.ToPagedList(pageNumber, pageSize);
            return View(pageList);
       }
        [Authorize(Users = "admin@qq.com")]
        public ActionResult ArticleList(int? page,int?id)
        {
            var art = db.Articles.Include(a => a.Account);

            if (id.HasValue)
            {//id不为空，则根据用户id筛选
                //必须引用using System.Data.Entity;才能用Include
                art = db.Articles.Where(b => b.AccountID == id).Include(a => a.Account);
            }
            //第几页
            int pageNumber = page ?? 1;
            //每页显示多少条
            int pageSize = int.Parse(ConfigurationManager.AppSettings["pageSize"]);
            //根据创建时间 降序排序
            var art1 = art.OrderByDescending(x => x.createTime);
            //通过topagelist扩展方法进行分页
            IPagedList<ReUse.Models.Article> pageList = art1.ToPagedList(pageNumber, pageSize);
            return View(pageList);
           
        }
        // [Authorize(Users = "admin@qq.com")] 限定用户
        [Authorize]
        public ActionResult Add()
        {
            return View();
        }
        [HttpPost]
        [ValidateInput(false)] //取消输入验证 --因为有内容有标签-得加这句
        public ActionResult Add(Models.Article art)
        {
            //topic、content 已从前边view里获取了，其他要赋值。
            art.createTime = DateTime.Now;
            art.lastClickTime = DateTime.Now;
            art.clickCount = 0;
            if (Session["UserID"] == null)
            {
                FormsAuthentication.SignOut();//清除假登陆状态
                return RedirectToAction("Login","Account");
            }
            else
            { 
                art.AccountID = int.Parse(Session["UserID"].ToString());//获取作者id
                db.Articles.Add(art);//增加
                db.SaveChanges();//保y
                return RedirectToAction("MyIndexList"); 
            }
        }
        /*
               um.ready(function () {
                    var con = '@Html.Raw(Model.content)';
                    um.setContent(con);
                });
        */

        //修改用户
        public ActionResult Edit(int? id)
        {
            Models.Article art = db.Articles.Find(id);
            return View(art);
        }

        [HttpPost]
        [ValidateInput(false)] //取消输入验证 --因为有内容有标签-得加这句
        public ActionResult Edit(Models.Article art)
        {
            //加上 using System.Data.Entity;
            if (ModelState.IsValid)
            { 
                db.Entry(art).State = EntityState.Modified;
                db.SaveChanges();
            }
           
            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult Delete(int? id)
        {
            Models.Article art = db.Articles.Find(id);
            db.Articles.Remove(art);
            db.SaveChanges();
            return RedirectToAction("MyIndexList");
        }
    }
}