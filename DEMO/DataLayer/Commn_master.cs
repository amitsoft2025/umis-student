using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;
namespace DataLayer
{
   public  class Commn_master
    {    
        public int CommonId { get; set; }
        public int CommonCode { get; set; }
        public string Title { get; set; }
        public string Titlecode { get; set; } 
        public string CreateDate { get; set; }
        public string Type { get; set; }
        public string FormStatus { get; set; }
        public int CourseCategoryID { get; set; }
        public int EducationTypeID { get; set; }
        public string CourseCategory { get; set; }
        public int StreamCategoryID { get; set; }
        public string streamCategory { get; set; }
        public int bloodgrouid { get; set; }
        public string BloodGroup { get; set; }
        public decimal commonvalue { get; set; }
        public bool isopendate { get; set; }
        public bool isclosedate { get; set; }
        public string opendate { get; set; }
        public string closedate { get; set; }
        public bool status { get; set; }
        public string msg { get; set; }
        public string otp { get; set; }

        public List<Commn_master> getcommonMaster(string type,  int Id= 0,int quaid =0 )
        {
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var obj = conn.Query<Commn_master>("[sp_Common_master]", new { @type = type, @Id= Id , @quaid = quaid },commandTimeout:500000, commandType: CommandType.StoredProcedure).ToList();
                return obj;
            }
        }

        public List<Commn_master> Getbloodgroup(string Action, string BloodGroup="", int Id = 0)
        {
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var obj = conn.Query<Commn_master>("[sp_BloodGroup]", new { @Id = Id, @BloodGroup = BloodGroup, @Action = Action }, commandType: CommandType.StoredProcedure).ToList();
                return obj;
            }
        }

        public bool check_admission_open(int seessionid = 0, int educationtype = 0)
        {
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var obj = conn.Query<bool>("[sp_check_admissionopen]", new { @seessionid = seessionid, @educationtype = educationtype }, commandType: CommandType.StoredProcedure).FirstOrDefault();
              return obj;
            // return true;
            }
        }

        public bool check_admission_close(int seessionid = 0, int educationtype = 0)
        {
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var obj = conn.Query<bool>("[sp_check_admissionopen]", new { @seessionid = seessionid, @educationtype = educationtype }, commandType: CommandType.StoredProcedure).FirstOrDefault();
               return obj;
                //return true;
            }
        }

        public bool check_ExamFeeSubmit_open(int seessionid = 0, int educationtype = 0)
        {
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var obj = conn.Query<bool>("[sp_check_ExamFeeopen]", new { @Action = "ExamFeestartDate", @seessionid = seessionid, @educationtype = educationtype }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return obj;
               // return true;
            }
        }

        public bool check_ExamFeeSubmit_close(int seessionid = 0, int educationtype = 0)
        {
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var obj = conn.Query<bool>("[sp_check_ExamFeeopen]", new { @Action = "ExamFee", @seessionid = seessionid, @educationtype = educationtype }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return obj;
                // return true;
            }
        }

        public Commn_master check_ExamFeeSubmit_check(int seessionid = 0, int educationtype = 0)
        {
            Commn_master obj = new Commn_master();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                obj = conn.Query<Commn_master>("[sp_check_ExamFeeopen]", new { @Action = "ExamFeecheck", @seessionid = seessionid, @educationtype = educationtype }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return obj;              
            }
        }

        public Commn_master check_ExamFeeApply( int Sid  = 0 , string  Action ="" ,int courseyearid=0)
        {
            Commn_master obj = new Commn_master();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                obj = conn.Query<Commn_master>("[sp_check_ExamFormApply]", new { @Action = Action, @sid = Sid, @courseyearid = courseyearid }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return obj;
                //return true;
            }
        }

        public Commn_master check_ScrutinyFeeSubmit_check(int seessionid = 0, int educationtype = 0)
        {
            Commn_master obj = new Commn_master();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                obj = conn.Query<Commn_master>("[sp_check_ExamFeeopen]", new { @Action = "ScrutinyFeecheck", @seessionid = seessionid, @educationtype = educationtype }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return obj;
                //return true;
            }
        }

        public Commn_master check_ExamFeeSubmit_checkprac(int seessionid = 0, int educationtype = 0)
        {
            Commn_master obj = new Commn_master();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                obj = conn.Query<Commn_master>("[sp_check_ExamFeeopen_practical]", new { @Action = "pracExamFeecheck", @seessionid = seessionid, @educationtype = educationtype }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return obj;
                //return true;
            }
        }

        public Commn_master check_BAckExamFeeSubmit_check(int seessionid = 0, int educationtype = 0)
        {
            Commn_master obj = new Commn_master();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                obj = conn.Query<Commn_master>("[sp_check_ExamFeeopen]", new  { @Action = "BAckExamFeecheck", @seessionid = seessionid, @educationtype = educationtype },  commandType: CommandType.StoredProcedure).FirstOrDefault();
                return obj;             
                //return true;
            }
        }


        public Commn_master check_BAckScrutinyFeeSubmit_check(int seessionid = 0, int educationtype = 0)
        {
            Commn_master obj = new Commn_master();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                obj = conn.Query<Commn_master>("[sp_check_ExamFeeopen]", new { @Action = "BAcScrutinyFeecheck", @seessionid = seessionid, @educationtype = educationtype }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                // obj.isopendate = true;
                //  obj.isclosedate = true;
                return obj;

                //return true;
            }
        }

        public Commn_master check_EnrollmentSubmit_check(int seessionid = 0, int educationtype = 0)
        {
            Commn_master obj = new Commn_master();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                //obj.isopendate = true;
                //obj.isclosedate = true;
                obj = conn.Query<Commn_master>("[sp_check_ExamFeeopen]", new { @Action = "EnrollmentFeecheck", @seessionid = seessionid, @educationtype = educationtype }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return obj;
                //return true;
            }
        }
        public bool check_EnrollmentFeeSubmit_open(int seessionid = 0, int educationtype = 0)
        {
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var obj = conn.Query<bool>("[sp_check_ExamFeeopen]", new { @Action = "EnrollmentFeestartDate", @seessionid = seessionid, @educationtype = educationtype }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return obj;
                // return true;
            }
        }
        public bool check_EnrollmentFeeSubmit_Close(int seessionid = 0, int educationtype = 0)
        {
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var obj = conn.Query<bool>("[sp_check_ExamFeeopen]", new { @Action = "EnrollmentFee", @seessionid = seessionid, @educationtype = educationtype }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return obj;
                // return true;
            }
        }

        public bool Backcheck_ExamFeeSubmit_open(int seessionid = 0, int educationtype = 0)
        {
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var obj = conn.Query<bool>("[sp_check_ExamFeeopen]", new { @Action = "BackExamFeestartDate", @seessionid = seessionid, @educationtype = educationtype }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return obj;
                // return true;
            }
        }

        public bool Backcheck_ScrutinyFeeSubmit_open(int seessionid = 0, int educationtype = 0)
        {
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var obj = conn.Query<bool>("[sp_check_ExamFeeopen]", new { @Action = "ScrutinyFeestartDate", @seessionid = seessionid, @educationtype = educationtype }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return obj;
                // return true;
            }
        }


        public bool Maincheck_ScrutinyFeeSubmit_open(int seessionid = 0, int educationtype = 0)
        {
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var obj = conn.Query<bool>("[sp_check_ExamFeeopen]", new { @Action = "ScrutinyFeestartDate", @seessionid = seessionid, @educationtype = educationtype }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return obj;
                // return true;
            }
        }


        public bool Backcheck_ExamFeeSubmit_status(int seessionid = 0, int educationtype = 0,   int courseyearid=0 , int sid  = 0)
        {
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var obj = conn.Query<bool>("[sp_check_ExamFeeFeesStatus]", new { @Action = "Status", @seessionid = seessionid, @educationtype = educationtype, @courseyearid= courseyearid, @sid= sid }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return obj;
                // return true;
            }
        }
        public bool Backcheck_ScrutinyFeeSubmit_status(int seessionid = 0, int educationtype = 0, int courseyearid = 0, int sid = 0)
        {
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var obj = conn.Query<bool>("[sp_check_ScrutinyFeeFeesStatus]", new { @Action = "Status", @seessionid = seessionid, @educationtype = educationtype, @courseyearid = courseyearid, @sid = sid }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return obj;
                // return true;
            }
        }

        public bool Backcheck_ExamFeeSubmit_close(int seessionid = 0, int educationtype = 0)
        {
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
               var obj = conn.Query<bool>("[sp_check_ExamFeeopen]", new { @Action = "BackExamFeeenddate", @seessionid = seessionid, @educationtype = educationtype }, commandType: CommandType.StoredProcedure).FirstOrDefault();
               return obj;
               // return true;
            }
        }

        public bool Backcheck_ScrutinyFeeSubmit_close(int seessionid = 0, int educationtype = 0)
        {
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var obj = conn.Query<bool>("[sp_check_ExamFeeopen]", new { @Action = "ScrutinyFeeenddate", @seessionid = seessionid, @educationtype = educationtype }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return obj;
                // return true;
            }
        }

        public bool Maincheck_ScrutinyFeeSubmit_close(int seessionid = 0, int educationtype = 0)
        {
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var obj = conn.Query<bool>("[sp_check_ExamFeeopen]", new { @Action = "ScrutinyFeeenddate", @seessionid = seessionid, @educationtype = educationtype }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return obj;
                // return true;
            }
        }
        public Commn_master SendOTp(string mobile = "", string otp = "", string CourseCategory = "", string session = "")
        {
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var obj = conn.Query<Commn_master>("[sp_student_verifyotp]", new { @Action = "sendotp", @mobileno = mobile, @otp = otp, @CourseCategory= CourseCategory, @session= session }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return obj;
                // return true;
            }
        }
        public Commn_master verifyOTp(string mobile = "", string otp = "")
        {
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var obj = conn.Query<Commn_master>("[sp_student_verifyotp]", new { @Action = "verifyotp", @mobileno = mobile, @otp = otp }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return obj;
                // return true;
            }
        }


    }
}
