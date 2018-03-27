using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class T_UserInfo
    {
        public int id { get; set; }
        public string UserName { get; set; }
        public string UserPwd { get; set; }
        public string UserMail { get; set; }
        public DateTime RegTime { get; set; }
    }
}
