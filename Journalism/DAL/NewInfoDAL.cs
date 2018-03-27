using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using System.Data;
using System.Data.SqlClient;
namespace DAL
{
    public class NewInfoDAL
    {
        public List<T_News> GetPageList(int start, int end)
        {
            string sql = "select * from (select row_number() over(order by id) as num,* from T_News) as t where t.num>=@start and t.num<=@end";
            SqlParameter[] pars = { 
                                 new SqlParameter("@start",SqlDbType.Int),
                                 new SqlParameter("@end",SqlDbType.Int)
                                 };
            pars[0].Value = start;
            pars[1].Value = end;
            DataTable da = SqlHelper.GetTable(sql, CommandType.Text, pars);
            List<T_News> list = null;
            if (da.Rows.Count > 0)
            {
                list = new List<T_News>();
                T_News newInfo = null;
                foreach (DataRow row in da.Rows)
                {
                    newInfo = new T_News();
                    LoadEntity(row, newInfo);
                    list.Add(newInfo);
                }
            }
            return list;
        }
        private void LoadEntity(DataRow row, T_News newInfo)
        {
            newInfo.id = Convert.ToInt32(row["ID"]);
            newInfo.Author = row["Author"] != DBNull.Value ? row["Author"].ToString() : string.Empty;
            newInfo.Title = row["Title"] != DBNull.Value ? row["Title"].ToString() : string.Empty;
            newInfo.Msg = row["Msg"] != DBNull.Value ? row["Msg"].ToString() : string.Empty;
            newInfo.ImagePath = row["ImagePath"] != DBNull.Value ? row["ImagePath"].ToString() : string.Empty;
            newInfo.SubDateTime = Convert.ToDateTime(row["SubDateTime"]);
        }

        /// <summary>
        /// 获取总的记录数
        /// </summary>
        /// <returns></returns>
        public int GetRecordCount()
        {
            string sql = "select count(*) from T_News";
            return Convert.ToInt32(SqlHelper.ExecuteScalare(sql, CommandType.Text));
        }
        /// <summary>
        /// 获取一条记录
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public T_News GetModel(int id)
        {
            string sql = "select * from T_News where id=@id";
            SqlParameter[] pars = { 
                                 new SqlParameter("@id",SqlDbType.Int)
                                 };
            pars[0].Value = id;
            DataTable da = SqlHelper.GetTable(sql, CommandType.Text, pars);
            T_News newInfo = null;
            if (da.Rows.Count > 0)
            {
                newInfo = new T_News();
                LoadEntity(da.Rows[0], newInfo);
            }
            return newInfo;
        }
        public int DeleteInfo(int id)
        {
            string Strsql = "delete from  T_News where id=@id";
            return SqlHelper.ExecuteNonquery(Strsql, CommandType.Text, new SqlParameter("@id", id));
        }
    }
}

