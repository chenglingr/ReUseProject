using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ReUse.Controllers
{
    public class WantController : Controller
    {
        private DAL.AccountContext db = new DAL.AccountContext();
        // GET: Want
        public ActionResult Index()
        {
            return View();
        }
       // [Authorize]
        public ActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Add(Models.Want model)
        {
            model.ClickNum = 0;
            model.CreatDate = DateTime.Now;
            model.State = 0;
            if (Session["UserID"] == null)
            {
                model.UserID = 1;
            }
            else
            {
                model.UserID = int.Parse(Session["UserID"].ToString());//获取作者id

            }
            if (ModelState.IsValid)
            {
                db.Wants.Add(model);//增加
                db.SaveChanges();//保y
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("Error", "添加失败，请重填");//
                return View();
            }
          
        }
            
        
    }
}