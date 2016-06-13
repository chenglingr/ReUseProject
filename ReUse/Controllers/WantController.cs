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
        [Authorize]
        public ActionResult Add()
        {
            return View();
        }
        public ActionResult Add(Models.Want model)
        {
            return View();
        }
    }
}