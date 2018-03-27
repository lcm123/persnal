using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
namespace BLL
{
   public class UserInfoBLL
    {
       UserInfoDAL UserInfoDal = new UserInfoDAL();
       public Model.T_UserInfo GetUserInfo(string userName, string userPwd)
        {
            return UserInfoDal.GetUserInfo(userName, userPwd);
        }
    }
}
