using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
namespace ReUse.Controllers
{
    public class TranController : Controller
    {
        private DAL.AccountContext db = new DAL.AccountContext();
        // GET: Tran
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult List(int?id)
        {
            var trans =db.Trans.Where(a=>a.GoodsID==id).OrderByDescending(b=>b.CreateDate).Include(b => b.User);
            List<ViewModels.ShowTran> tranList = new List<ViewModels.ShowTran>();
            foreach (Models.Tran model in trans)
            {
                tranList.Add(new ViewModels.ShowTran { name = model.User.UserName, qq =model.User.QQ, email = model.User.Email, weixin = model.User.WeiXin, tel = model.User.Tel, content= model.Content, createdate=model.CreateDate });
            }
         
           JsonResult json = new JsonResult
            {
                Data = tranList
            };
            return Json(json);
        }
        [HttpPost]
        [Filter.AntiSqlInject]
        public ActionResult Add(int? Goodsid ,string content)
        {
            Models.Tran model = new Models.Tran();
            model.Content = content;
            model.CreateDate = DateTime.Now;
            model.GoodsID = Goodsid;
            model.Star = 5;
            model.State = 1;
            model.UserID = 1;//匿名
            if (Session["UserID"] != null)
            {
                model.UserID = int.Parse(Session["UserID"].ToString());
            }
            try
            {
                db.Trans.Add(model);//增加数据
                db.SaveChanges();//保存
            }
            catch (Exception aa)
            {
                string sss = aa.Message;
            }
            return Content("ok");
        }
    }
}