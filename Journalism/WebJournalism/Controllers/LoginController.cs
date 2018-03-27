using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL;
using Model;
namespace WebJournalism.Controllers
{
    public class LoginController : Controller
    {
        //
        // GET: /Login/

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult UserLogin()
        {
            string validateCode = Session["code"] == null ? string.Empty : Session["code"].ToString();
            if (string.IsNullOrEmpty(validateCode))
            {
                return Content("no:请输入验证码!");
            }
            Session["code"] = null;
            string txtCode = Request["vCode"];
            if (!validateCode.Equals(txtCode, StringComparison.InvariantCultureIgnoreCase))
            {
                return Content("no:验证码错误!");
            }
            string userName = Request["LoginCode"];
            string userPwd = Request["LoginPwd"];
            UserInfoBLL UserInfoService = new UserInfoBLL();
            T_UserInfo userInfo = UserInfoService.GetUserInfo(userName, userPwd);
            if (userInfo != null)
            {                               
                Session["userInfo"] = userInfo;
                return Content("ok:登录成功");
            }
            else
            {
                return Content("no:登录失败!!");
            }
        }

        public ActionResult ShowValidateCode()
        {
            Common.ValidateCode validateCode = new Common.ValidateCode();
            string code = validateCode.CreateValidateCode(4);//获取验证码
            Session["code"] = code;
            byte[] buffer = validateCode.CreateValidateGraphic(code);
            return File(buffer, "image/jpeg");
        }
    }
}
