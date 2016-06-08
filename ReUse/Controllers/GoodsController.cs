using System;
using System.Collections.Generic;
using System.IO;
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
            ViewData["EnumArea"] = new SelectList(EnumExt.ToListItem<Area>(), "value", "text");
            return View();
        }
        public ActionResult Add1() {
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
        public ActionResult Index()
        {
            return View();
        }
    }
}