using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
namespace BLL
{
   public class NewInfoBLL
    {
       DAL.NewInfoDAL NewInfoDal = new DAL.NewInfoDAL();
     /// <summary>
     /// 获取分页数据
     /// </summary>
     /// <param name="pageIndex">当前页码值</param>
     /// <param name="pageSize">每页显示的记录数</param>
     /// <returns></returns>
       public List<T_News> GetPageList(int pageIndex,int pageSize)
       {
           int start = (pageIndex - 1) * pageSize + 1;
           int end = pageIndex * pageSize;
           List<T_News> list = NewInfoDal.GetPageList(start, end);
           return list;
       }
       /// <summary>
       /// 获取总的页数
       /// </summary>
       /// <param name="pageSize"></param>
       /// <returns></returns>
       public int GetPageCount(int pageSize)
       {
           int recordCount = NewInfoDal.GetRecordCount();
          int pageCount=Convert.ToInt32(Math.Ceiling((double)recordCount / pageSize));
          return pageCount;
       }
       public T_News GetModel(int id)
       {
           return NewInfoDal.GetModel(id);
       }
       public bool DeleteInfo(int id)
       {
           return NewInfoDal.DeleteInfo(id) > 0;
       }
    }
}
