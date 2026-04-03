using Dapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace DataLayer
{
    public class Country
    {
        public Int32 ID { get; set; }
        [Required(ErrorMessage = "Country is required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Country  Code is required")]
        public string ShortName { get; set; }
        public bool Status { get; set; }
        public bool IsActive { get; set; }
        public string Msg { get; set; }

        public int PhoneCode { get; set; }
        public DateTime CreateDate { get; set; }
       
        public List<Country> GetAllCountries(int id = 0)
        {
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                //var obj = conn.Query<Country>("Select * from tbl_Country   where id=80").ToList();
                var obj = conn.Query<Country>("[sp_Common_QueryMethod]", new
                {
                    @Action = "Country",
                }, commandType: CommandType.StoredProcedure).ToList();
                return obj;
            }

        }
        public List<Country> getall(int id = 0)
        {
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var obj = conn.Query<Country>("select  id,password as ShortName from tbl_studentregistration  where session=41 and CourseCategory=28 --and id in(select  sid from tbl_recruitment  where coursecategoryid=28 and sessionid=40 and collegeid in(38))").ToList();
                return obj;
            }

        }



    }
    public class countries
    {
        public List<Country> countrylist { get; set; }
        public string totalCount { get; set; }

    }
}
