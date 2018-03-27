using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebJournalism.Controllers
{
    public class AdminNewListController : Controller
    {
        //
        // GET: /AdminNewList/
        BLL.NewInfoBLL NewInfoService = new BLL.NewInfoBLL();
        public ActionResult Index()
        {
            int pageIndex = Request["pageIndex"] != null ? int.Parse(Request["pageIndex"]) : 1;
            int pageSize =10;
            int pageCount = NewInfoService.GetPageCount(pageSize);
            pageIndex = pageIndex < 1 ? 1 : pageIndex;
            pageIndex = pageIndex > pageCount ? pageCount : pageIndex;
            List<Model.T_News> list = NewInfoService.GetPageList(pageIndex, pageSize);//获取分页的数据
            ViewData["list"] = list;
            ViewData["pageIndex"] = pageIndex;
            ViewData["pageCount"] = pageCount;
            return View();
        }
        public ActionResult GetNewInfoModel()
        {
            int id = int.Parse(Request["id"]);
            Model.T_News newInfo = NewInfoService.GetModel(id);//获取详细信息.
            return Json(newInfo, JsonRequestBehavior.AllowGet);
        }
        //删除
        public ActionResult DeleteNewInfo()
        {
            int id = int.Parse(Request["id"]);
            if (NewInfoService.DeleteInfo(id))
            {
                return Content("OK");
            }
            else
            {
                return Content("NO");
            }
        }
        public ActionResult ShowInfo()
        {
            return View();
        }
        public ActionResult FileUpload() 
        {
           HttpPostedFileBase PostFile= Request.Files["fileUp"];
           if (PostFile == null)
           {
               return Content("on:请选择上传文件");
           }
           else
           {
               string fileName = Path.GetFileName(PostFile.FileName);//获取文件名
               string fileExt = Path.GetExtension(fileName);//获取文件扩展名
               if (fileExt == ".jpg")
               {
                   string dir = "/ImagePath/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + DateTime.Now.Day + "/";
                   Directory.CreateDirectory(Path.GetDirectoryName(Request.MapPath(dir)));//创建文件夹.
                   string newfileName = Guid.NewGuid().ToString();//新的文件名称.
                   string fullDir = dir + newfileName + fileExt;//完整的路径.
                   PostFile.SaveAs(Request.MapPath(fullDir));//保存文件.
                   return Content("ok:" + fullDir);
               }
               else
               {
                   return Content("no:文件格式错误！！");
               }
           }
        }
    }
}
