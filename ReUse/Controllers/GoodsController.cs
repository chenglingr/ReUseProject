using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ReUse.Controllers
{
    public class GoodsController : Controller
    {
        private DAL.AccountContext db = new DAL.AccountContext();
        // GET: Goods
        #region 增加
        [Authorize]
        public ActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Add(Models.Goods model)
        {
            model.CreatDate = DateTime.Now;
            model.ClickNum = 0;
            model.State =0;
            model.UserID = 1;
            return View();
        }
        #endregion
        public ActionResult Index()
        {
            return View();
        }
    }
}