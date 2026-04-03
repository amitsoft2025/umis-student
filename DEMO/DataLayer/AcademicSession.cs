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
  public  class AcademicSession
    {
        public int ID { get; set; }
        public string Session { get; set; }
        public int IsActive { get; set; }
        public int IsDelete { get; set; }
        public string year { get; set; }
        public bool IsCurrent { get; set; }
        public int nID { get; set; }
        public bool status { get; set; }
        public string Msg { get; set; }
        public string sessionname { get; set; }
        public string yearshort { get; set; }
        public AcademicSession GetAcademiccurrentSession()
        {
            AcademicSession obj = new AcademicSession();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                // obj = conn.Query<AcademicSession>("sp_AcademicSession", new { Action = "View"}, commandType: CommandType.StoredProcedure).FirstOrDefault();
                //Get cuurent session id and his details
                // beacuse this procedure every master type in call                 
                obj.ID = 46;
                obj.IsActive = 1;
                obj.year = "2025";
                obj.IsCurrent = true;
                obj.yearshort = "25";
                obj.Session = "2025";
                string  StID = (ClsLanguage.GetCookies("ENBPGApplicationNo"));
                string NBSission = (ClsLanguage.GetCookies("NBSission"));
                if (NBSission == "ok" || NBSission=="0" || NBSission=="")
                {
                }
                else
                {
                    obj.ID = Convert.ToInt32(NBSission);
                }
               
                return obj;
            }

        }
        public AcademicSession GetAcademiccurrentSessioneducationtype(int educationid)
        {
            AcademicSession obj = new AcademicSession();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                // obj = conn.Query<AcademicSession>("sp_AcademicSession", new { Action = "View"}, commandType: CommandType.StoredProcedure).FirstOrDefault();
                //Get cuurent session id and his details
                // beacuse this procedure every master type in call
               
                 if(educationid == 12)
                {
                    obj.ID = 46;
                    obj.IsActive = 1;
                    obj.year = "2025";
                    obj.IsCurrent = true;
                    obj.yearshort = "25";
                    obj.Session = "2025";
                    string StID = (ClsLanguage.GetCookies("ENBPGApplicationNo"));
                    string NBSission = (ClsLanguage.GetCookies("NBSission"));
                    if (NBSission == "ok" || NBSission == "0" || NBSission == "")
                    {
                    }
                    else
                    {
                        obj.ID = Convert.ToInt32(NBSission);
                    }

                }
                else 
                {
                    obj.ID = 46;
                    obj.IsActive = 1;
                    obj.year = "2025";
                    obj.IsCurrent = true;
                    obj.yearshort = "25";
                    obj.Session = "2025";
                    //obj.sessionname = "2021";
                    string StID = (ClsLanguage.GetCookies("ENBPGApplicationNo"));
                    string NBSission = (ClsLanguage.GetCookies("NBSission"));
                    if (NBSission == "ok" || NBSission == "0" || NBSission == "")
                    {
                    }
                    else
                    {
                        obj.ID = Convert.ToInt32(NBSission);
                    }
                }

                return obj;
            }

        }
        public AcademicSession getcutdate()
        {
            AcademicSession obj = new AcademicSession();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                obj = conn.Query<AcademicSession>("[sp_AcademicSession_date]", new { Action = "View" }, commandTimeout:500000,commandType: CommandType.StoredProcedure).FirstOrDefault();
             
                return obj;
            }

        }
        public AcademicSession GetAcademiccurrentSessionname(string sessionid,int coursecategotyid)
        {
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var obj = conn.Query<AcademicSession>("sp_AcademicSession", new { Action = "Currentsessionname" , @Session = sessionid , @ID = coursecategotyid }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return obj;
            }

        }
        public AcademicSession AddDetail(AcademicSession obj)
        {
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var obje = conn.Query<AcademicSession>("sp_AcademicSession", new { Action = "add" ,@ID=obj.nID,
                    @Session=obj.Session,
                    @year=obj.year

                }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return obje;
            }

        }
               
        //public AcademicSession GetSession()
        //{
        //    using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
        //    {
        //        var obj = conn.Query<AcademicSession>("[sp_GetSession]", new { Action = "View" }, commandType: CommandType.StoredProcedure).FirstOrDefault();
        //        return obj;
        //    }

        //}
        public List<AcademicSession> GetSession()
        {
            List<AcademicSession> obj = new List<AcademicSession>();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                 obj = conn.Query<AcademicSession>("[sp_GetSession]", new { Action = "View" }, commandType: CommandType.StoredProcedure).ToList();
               
            }
            return obj;
        }
    }
}
