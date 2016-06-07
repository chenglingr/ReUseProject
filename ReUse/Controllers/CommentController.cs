using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ReUse.Controllers
{
    public class CommentController : Controller
    {
        private DAL.AccountContext db = new DAL.AccountContext();
        // GET: Comment
        public ActionResult Index()
        {
            return View();
        }
    }
}