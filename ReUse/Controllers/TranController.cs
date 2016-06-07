using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
    }
}