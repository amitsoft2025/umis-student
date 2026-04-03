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
    public class BL_PrintRecipt
    {
        public int Id { get; set; }
        public string ApplicationNo { get; set; }
        public string Session { get; set; }
        public string Title { get; set; }
        public string Fees { get; set; }
        public string SubjecCategory { get; set; }
        public string CastCategory { get; set; }
        public string FeeStatus { get; set; }
        public string PaymentHolderName { get; set; }
        public string Expires { get; set; }
        public string NuthNum { get; set; }
        public string PaymentType { get; set; }
        public string CardNumber { get; set; }
        public string TransactionId { get; set; }
        public string banktrxid { get; set; }
        public string status { get; set; }
        public string trxdate { get; set; }
        public string CollegeName { get; set; }

        public string streamCategoryName { get; set; }
        public BL_PrintRecipt GetPaymentRecipt() 
        {            
            int id = Convert.ToInt32(ClsLanguage.GetCookies("NBStID"));
            string SessionID = ClsLanguage.GetCookies("NBSission");
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {            
                var Obj = conn.Query<BL_PrintRecipt>("[sp_StudentRegistration]", new { @Action = "PrintRecipt", @Id = id, @session = SessionID }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                if (Obj == null)
                {
                    BL_PrintRecipt bl = new BL_PrintRecipt();
                    Obj = bl; 
                }
                return Obj;


            }
        }
        public BL_PrintRecipt GetPaymentRecipt_spot()
        {
            int id = Convert.ToInt32(ClsLanguage.GetCookies("NBStID"));
            string SessionID = ClsLanguage.GetCookies("NBSission");
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var Obj = conn.Query<BL_PrintRecipt>("[sp_StudentRegistration]", new { @Action = "PrintRecipt_spot", @Id = id, @session = SessionID }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                if (Obj == null)
                {
                    BL_PrintRecipt bl = new BL_PrintRecipt();
                    Obj = bl;
                }
                return Obj;


            }
        }
        public BL_PrintRecipt GetPaymentReciptPush(int sid, int sessionid)
        {
            int id = Convert.ToInt32(sid);
            string SessionID = sessionid.ToString();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var Obj = conn.Query<BL_PrintRecipt>("[sp_StudentRegistration]", new { @Action = "PrintRecipt", @Id = id, @session = SessionID }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                if (Obj == null)
                {
                    BL_PrintRecipt bl = new BL_PrintRecipt();
                    Obj = bl;
                }
                return Obj;


            }
        }
        public BL_PrintRecipt GetPaymentReciptExamFeeprac()
        {
            int id = (ClsLanguage.GetCookies("NBStID") != null ? Convert.ToInt32(ClsLanguage.GetCookies("NBStID")) : 0);
            int SessionID = (ClsLanguage.GetCookies("NBSission") != null ? Convert.ToInt32(ClsLanguage.GetCookies("NBSission")) : 0);
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var Obj = conn.Query<BL_PrintRecipt>("[sp_student_ExamFromprac]", new { @Action = "PrintRecipt", @SID = id, @session = SessionID }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                if (Obj == null)
                {
                    BL_PrintRecipt bl = new BL_PrintRecipt();
                    Obj = bl;
                }
                return Obj;


            }
        }

        public BL_PrintRecipt GetPaymentReciptExamFee()
        {
            int id = (ClsLanguage.GetCookies("NBStID") != null ? Convert.ToInt32(ClsLanguage.GetCookies("NBStID")) : 0);
            int SessionID = (ClsLanguage.GetCookies("NBSission") != null ? Convert.ToInt32(ClsLanguage.GetCookies("NBSission")) : 0);
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var Obj = conn.Query<BL_PrintRecipt>("[sp_student_ExamFrom]", new { @Action = "PrintRecipt", @SID = id, @session = SessionID }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                if (Obj == null)
                {
                    BL_PrintRecipt bl = new BL_PrintRecipt();
                    Obj = bl;
                }
                return Obj;


            }
        }
        public BL_PrintRecipt backGetPaymentReciptExamFee(int courseyearid)
        {
            int id = (ClsLanguage.GetCookies("NBStID") != null ? Convert.ToInt32(ClsLanguage.GetCookies("NBStID")) : 0);
            int SessionID = (ClsLanguage.GetCookies("NBSission") != null ? Convert.ToInt32(ClsLanguage.GetCookies("NBSission")) : 0);
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var Obj = conn.Query<BL_PrintRecipt>("[sp_student_ExamFrom_back]", new { @Action = "PrintRecipt", @SID = id, @session = SessionID, @currentyear=System.DateTime.Now.Year, @courseyearid= courseyearid }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                if (Obj == null)
                {
                    BL_PrintRecipt bl = new BL_PrintRecipt();
                    Obj = bl;
                }
                return Obj;


            }
        }


        public BL_PrintRecipt GetPaymentReciptScrutinyFeeprac()
        {
            int id = (ClsLanguage.GetCookies("NBStID") != null ? Convert.ToInt32(ClsLanguage.GetCookies("NBStID")) : 0);
            int SessionID = (ClsLanguage.GetCookies("NBSission") != null ? Convert.ToInt32(ClsLanguage.GetCookies("NBSission")) : 0);
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var Obj = conn.Query<BL_PrintRecipt>("[sp_student_ExamFromprac]", new { @Action = "PrintRecipt", @SID = id, @session = SessionID }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                if (Obj == null)
                {
                    BL_PrintRecipt bl = new BL_PrintRecipt();
                    Obj = bl;
                }
                return Obj;


            }
        }

        public BL_PrintRecipt GetPaymentReciptScrutinyFee()
        {
            int id = (ClsLanguage.GetCookies("NBStID") != null ? Convert.ToInt32(ClsLanguage.GetCookies("NBStID")) : 0);
            int SessionID = (ClsLanguage.GetCookies("NBSission") != null ? Convert.ToInt32(ClsLanguage.GetCookies("NBSission")) : 0);
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var Obj = conn.Query<BL_PrintRecipt>("[sp_student_ScrutinyFrom]", new { @Action = "PrintRecipt", @SID = id, @session = SessionID }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                if (Obj == null)
                {
                    BL_PrintRecipt bl = new BL_PrintRecipt();
                    Obj = bl;
                }
                return Obj;


            }
        }


        public BL_PrintRecipt backGetPaymentReciptScrutinyFee(int courseyearid)
        {
            int id = (ClsLanguage.GetCookies("NBStID") != null ? Convert.ToInt32(ClsLanguage.GetCookies("NBStID")) : 0);
            int SessionID = (ClsLanguage.GetCookies("NBSission") != null ? Convert.ToInt32(ClsLanguage.GetCookies("NBSission")) : 0);
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var Obj = conn.Query<BL_PrintRecipt>("[sp_student_ScrutinyFrom_back]", new { @Action = "PrintRecipt", @SID = id, @session = SessionID, @currentyear = System.DateTime.Now.Year, @courseyearid = courseyearid }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                if (Obj == null)
                {
                    BL_PrintRecipt bl = new BL_PrintRecipt();
                    Obj = bl;
                }
                return Obj;


            }
        }
        public BL_PrintRecipt GetPaymentReciptEnrollmentFee()
        {
            int id = (ClsLanguage.GetCookies("NBStID") != null ? Convert.ToInt32(ClsLanguage.GetCookies("NBStID")) : 0);
            int SessionID = (ClsLanguage.GetCookies("NBSission") != null ? Convert.ToInt32(ClsLanguage.GetCookies("NBSission")) : 0);
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var Obj = conn.Query<BL_PrintRecipt>("[sp_EnrollmentRequest]", new { @Action = "PrintRecipt", @SID = id, @session = SessionID }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                if (Obj == null)
                {
                    BL_PrintRecipt bl = new BL_PrintRecipt();
                    Obj = bl;
                }
                return Obj;


            }
        }
    }
}
