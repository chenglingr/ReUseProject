using PagedList;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace ReUse.Controllers
{
    public class GoodsController : Controller
    {
        private DAL.AccountContext db = new DAL.AccountContext();
        // GET: Goods
        public ActionResult Index()
        {
            ViewModels.GoodsIndexViewModel model = new ViewModels.GoodsIndexViewModel();
            model.HotGoodss = db.Goodss.Where(b=>b.State==0).OrderByDescending(a => a.ClickNum).Skip(0).Take(4).ToList();
            model.NewGoodss = db.Goodss.Where(b => b.State == 0).OrderByDescending(a => a.CreatDate).Skip(0).Take(4).ToList();
            return View(model);
        }
        [HttpPost]
        public ActionResult EditState(int? id,int? state)
        {
            Models.Goods art = db.Goodss.Find(id);
            int state1 = state ?? 0;
            art.State = state1;
            db.Entry(art).State = EntityState.Modified;
            db.SaveChanges();
            return Content("ok");
        }
        [Authorize]
        public ActionResult MyIndexList(int? page)
        {
            int id = 0;
            if (Session["UserID"] != null)//判断是否登录
            {
                id = int.Parse(Session["UserID"].ToString());
                var art = db.Goodss.Where(b => b.UserID == id);
                //第几页
                int pageNumber = page ?? 1;
                //每页显示多少条
                int pageSize = int.Parse(ConfigurationManager.AppSettings["pageSizeUser"]);
                //根据创建时间 降序排序
                var art1 = art.OrderByDescending(x => x.CreatDate);
                //通过topagelist扩展方法进行分页
                IPagedList<ReUse.Models.Goods> pageList = art1.ToPagedList(pageNumber, pageSize);
                return View(pageList);
            }
            else
            {
                FormsAuthentication.SignOut();//清除假登陆状态
                return RedirectToAction("Login","User");
            }
        }
        [HttpPost]
        public ActionResult Delete(int? id)
        {
            Models.Goods  art = db.Goodss.Find(id);
            db.Goodss.Remove(art);
            db.SaveChanges();
            return RedirectToAction("MyIndexList");
        }
        public ActionResult Detail(int? id)
        {
            ViewData["EnumStyle1"] = new SelectList(EnumExt.ToListItem<Style1>(), "text", "text");
            Models.Goods art = db.Goodss.Find(id);
            if (art != null)
            {
                art.ClickNum = art.ClickNum + 1;//修改点击率
                db.Entry(art).State = EntityState.Modified;
                db.SaveChanges();//保存修改
            }
            return View(art);
        }
        #region 增加
        [Authorize]
        public ActionResult Add()
        {
            setContent();
            return View();
        }
        /// <summary>
        /// 设置基础数据 类型、新旧程度 交易类型 地区
        /// </summary>
        public void setContent()
        {
            ViewData["EnumStyle1"] = new SelectList(EnumExt.ToListItem<Style1>(), "text", "text");
            ViewData["EnumNews"] = new SelectList(EnumExt.ToListItem<News>(), "text", "text");
            ViewData["EnumChangeType"] = new SelectList(EnumExt.ToListItem<ChangeType>(), "text", "text");
            ViewData["EnumArea"] = new SelectList(EnumExt.ToListItem<Area>(), "text", "text");
         
        }
        [HttpPost]
        [ValidateInput(false)]
        [Authorize]
        [Filter.AntiSqlInject]
        public ActionResult Add(Models.Goods model)
        {
            model.CreatDate = DateTime.Now;
            model.ClickNum = 0;
            model.State =0;

            if (Session["UserID"] == null)
            {
                FormsAuthentication.SignOut();//清除假登陆状态
                return RedirectToAction("Login", "User");
            }
            else
            {
                model.UserID= int.Parse(Session["UserID"].ToString());//获取作者id
                if (ModelState.IsValid)//执行服务端的验证
                {
                    db.Goodss.Add(model);//增加
                    db.SaveChanges();//保y
                    return RedirectToAction("MyIndexList");
                }
                else
                {
                    ModelState.AddModelError("Error", "添加失败，请重填");//
                    setContent();
                    return View();
                }
            }

        }
        [HttpPost]
        public ActionResult UploadFile()
        {
            List<UploadFileResult> results = new List<UploadFileResult>();
            foreach (string file in Request.Files)
            {
                HttpPostedFileBase hpf = Request.Files[file] as HttpPostedFileBase;
                if (hpf.ContentLength == 0 || hpf == null)
                {
                    continue;
                }
                var fileName = DateTime.Now.ToString("yyyyMMddhhmmss") +
                hpf.FileName.Substring(hpf.FileName.LastIndexOf('.'));
                string pathForSaving = Server.MapPath("~/Upload");
                if (this.CreateFolderIfNeeded(pathForSaving))
                {
                    hpf.SaveAs(Path.Combine(pathForSaving, fileName));
                    results.Add(new UploadFileResult()
                    {
                        FilePath = Url.Content(String.Format("~/Upload/{0}", fileName)),
                        FileName = fileName                       
                    });
                }
            }

            return Json(new
            {
                name = results[0].FileName,
                path = results[0].FilePath,
             
            });

        }
        //根据文件名称删除文件
        [HttpPost]
        public ActionResult DeleteFileByName(string name)
        {
            string pathForSaving = Server.MapPath("~/Upload");
            System.IO.File.Delete(Path.Combine(pathForSaving, name));
            return Json(new
            {
                msg = true
            });
        }
        /// <summary>
        /// 检查是否要创建上传文件夹，如果没有就创建
        /// </summary>
        /// <param name="path">路径</param>
        /// <returns></returns>
        private bool CreateFolderIfNeeded(string path)
        {
            bool result = true;
            if (!Directory.Exists(path))
            {
                try
                {
                    Directory.CreateDirectory(path);
                }
                catch (Exception)
                {
                    //TODO：处理异常
                    result = false;
                }
            }
            return result;
        }
        #endregion

        /// <summary>
        /// 二手物品列表
        /// </summary>
        /// <param name="page"></param>
        /// <param name="id">用户ID</param>
        /// <param name="search">物品名</param>
        /// <param name="style">物品类别</param>
        /// <returns></returns>
        public ActionResult IndexList(int? page, int? id,string search,string style)
        {
            //用户列表
            ViewBag.search = search;
            ViewBag.style = style;
            ViewBag.id = id;
            var art = from m in db.Goodss where m.State==0 select m;
            if (id.HasValue)
            {  
              art = art.Where(b=>b.UserID==id);
            }
            // books = books.Where(s => s.Name.Contains(searchString));
            if (!String.IsNullOrEmpty(search) )
            {
                art = art.Where(s => s.GoodsName.Contains(search));
           }
            if (!string.IsNullOrEmpty(style))
            {
               art=  art.Where(x => x.Style1 ==style);
            }
            ViewData["EnumStyle1"] = new SelectList(EnumExt.ToListItem<Style1>(), "text", "text");
            //第几页
            int pageNumber = page ?? 1;
            //每页显示多少条
            int pageSize = int.Parse(ConfigurationManager.AppSettings["pageSize"]);
            //根据创建时间 降序排序
            var art1 = art.OrderByDescending(x => x.CreatDate);
            //通过topagelist扩展方法进行分页
            IPagedList<ReUse.Models.Goods> pageList = art1.ToPagedList(pageNumber, pageSize);
            return View(pageList);

        }
    }
}