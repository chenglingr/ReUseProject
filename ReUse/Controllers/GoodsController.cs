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
       // [Authorize]
        public ActionResult Add()
        {
            ViewData["EnumStyle1"] = new SelectList(EnumExt.ToListItem<Style1>(), "value", "text");
            ViewData["EnumNews"] = new SelectList(EnumExt.ToListItem<News>(), "value", "text");
            ViewData["EnumChangeType"] = new SelectList(EnumExt.ToListItem<ChangeType>(), "value", "text");
            ViewData["EnumChangPriceType"] = new SelectList(EnumExt.ToListItem<ChangPriceType>(), "value", "text");

            return View();
        }
        public ActionResult Edit()
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