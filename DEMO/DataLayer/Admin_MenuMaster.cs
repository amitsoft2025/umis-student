using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
   public class Admin_MenuMaster
    {
        public int MenuID { get; set; }
        public string MenuName { get; set; }
        public int MenuLevel { get; set; }
        public int ParentID { get; set; }       
        public string MenuLink { get; set; }
        public string MenuStr { get; set; }
        public bool IsActive { get; set; }       
        public int orderposition { get; set; }
        public string controllername { get; set; }
        public string viewnamename { get; set; }
        public string class1 { get; set; }


        public IList<Admin_MenuMaster> getAdminMenuList(string action)
        {
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var obj = conn.Query<Admin_MenuMaster>("sp_AdminMenu", new { @Action=action }, commandType: CommandType.StoredProcedure).ToList();
                return obj;
            }
        }


    }
}
