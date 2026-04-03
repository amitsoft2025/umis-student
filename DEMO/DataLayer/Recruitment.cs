using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
namespace DataLayer
{
   public  class Recruitment
    {
        public int id { get; set; }
        public int sid { get; set; }
        public decimal percenatge { get; set; }
        public int coursecategoryid { get; set; }

        public int CasteCategory { get; set; }
        public int sessionid { get; set; }
        public int collegeid { get; set; }
        public int StreamCategoryID { get; set; }
        public DateTime adddate { get; set; }
        public int choicetable_id { get; set; }
        public int till_reaming_seat { get; set; }
        public bool ishandicapped { get; set; }
        public int counsellingno { get; set; }
        public int flag { get; set; }
        public string StudentName { get; set; }
        public string coursecategotyName { get; set; }
        public string CasteCategoryName { get; set; }
        public string StreamCategoryName { get; set; }
        public string CollegeName { get; set; }
        public string Session { get; set; }
        public string ApplicationNo { get; set; }
        public string StudentCasteCategoryName { get; set; }
        public bool status { get; set; }
        public string Msg { get; set; }
        public int carryseat_casteid { get; set; }
        public string seatType { get; set; }
        public string waitingno { get; set; }
        public string Mobileno { get; set; }
        public string ReservationType { get; set; }
        public string educationtype1 { get; set; }
        public string EncriptedID { get; set; }
        public bool IsApplied { get; set; }
        public int IsDocVerify { get; set; }// 0 pending,1 accecpt or verify,2 reject
        public string BloodGroup { get; set; }
        public string CurrentAddress { get; set; }
        public string PA_Address { get; set; }
        public string Gender { get; set; }
        public string Nationality { get; set; }
        public string Religion { get; set; }
        public string rejectreason { get; set; }
        public string rejectdate { get; set; }
        public string IsAppliedDate { get; set; }
        public string IsDocVerifyDate { get; set; }
        public bool IsAdmissionFee { get; set; }
        public string IsfeesubmitDate { get; set; }
        public string FatherName { get; set; }
        public string IsDocVerifyDatenew { get; set; }
        public string Faculty { get; set; }
        public int RollNoId { get; set; }
        //public int Sid { get; set; }
        public string RollNo { get; set; }
        public string CurrentYear { get; set; }
        public string Semster { get; set; }
        public bool IsActive { get; set; }
        public string CounsellingNo1 { get; set; }
        public string carryseat { get; set; }
        public string Fees { get; set; }
        public string Subsidiary1_subject { get; set; }
        public string Subsidiary2_subject { get; set; }
        public string Compulsory1_subject { get; set; }
        public string Compulsory2_subject { get; set; }
        public string enrollmentno { get; set; }
        public string mothername { get; set; }
        public string DOB { get; set; }
        public string Nationlity { get; set; }
        public string stphoto { get; set; }
        public string stsign { get; set; }
        public string clienttrxid { get; set; }
        public object WebConfiguration { get; private set; }

        public  DataTable Sendbulksms( string collegeID = "")
        {

           
            string connetionString = null;
            SqlConnection sqlCnn;
            SqlCommand sqlCmd;
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataSet ds = new DataSet();
            int i = 0;
            string sql = null;
            connetionString="Data Source=101.53.153.84;initial catalog=db_MungerUniversity1;user id=mungeruser;password=User%#$#lj45;MultipleActiveResultSets=True";
            sqlCnn = new SqlConnection(connetionString);
                sqlCnn.Open();
                sqlCmd = new SqlCommand("Sp_Recruitmentsendbulksmsgeo", sqlCnn);
                sqlCmd.Parameters.Add(new SqlParameter("@Action", "statusnull"));
                sqlCmd.CommandType = CommandType.StoredProcedure;
                adapter.SelectCommand = sqlCmd;  
                adapter.Fill(ds);
                adapter.Dispose();
                sqlCmd.Dispose();
                sqlCnn.Close();
                return ds.Tables[0];
           
        }
        public DataTable Sendbulksms_spot(string collegeID = "")
        {


            string connetionString = null;
            SqlConnection sqlCnn;
            SqlCommand sqlCmd;
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataSet ds = new DataSet();
            int i = 0;
            string sql = null;
            connetionString = "Data Source=101.53.153.84;initial catalog=db_MungerUniversity1;user id=mungeruser;password=User%#$#lj45;MultipleActiveResultSets=True";
            sqlCnn = new SqlConnection(connetionString);
            sqlCnn.Open();
            sqlCmd = new SqlCommand("Sp_Recruitmentsendbulksmsgeo", sqlCnn);
            sqlCmd.Parameters.Add(new SqlParameter("@Action", "statusnull_spot"));
            sqlCmd.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand = sqlCmd;
            adapter.Fill(ds);
            adapter.Dispose();
            sqlCmd.Dispose();
            sqlCnn.Close();
            return ds.Tables[0];

        }

        public DataTable Getpendingpaymentenrollment(string collegeID = "")
        {


            string connetionString = null;
            SqlConnection sqlCnn;
            SqlCommand sqlCmd;
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataSet ds = new DataSet();
            int i = 0;
            string sql = null;
           // connetionString = "Data Source=101.53.153.84;initial catalog=db_MungerUniversity1;user id=mungeruser;password=User%#$#lj45;MultipleActiveResultSets=True";
            connetionString = CommonSetting.constr;

            sqlCnn = new SqlConnection(connetionString);
            sqlCnn.Open();
            sqlCmd = new SqlCommand("Sp_Recruitmentsendbulksmsgeo", sqlCnn);
            sqlCmd.Parameters.Add(new SqlParameter("@Action", "statusnullenroll"));
            sqlCmd.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand = sqlCmd;
            adapter.Fill(ds);
            adapter.Dispose();
            sqlCmd.Dispose();
            sqlCnn.Close();
            return ds.Tables[0];

        }
        public DataTable GetpendingpaymentExam(string collegeID = "")
        {


            string connetionString = null;
            SqlConnection sqlCnn;
            SqlCommand sqlCmd;
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataSet ds = new DataSet();
            int i = 0;
            string sql = null;
            connetionString = CommonSetting.constr;
            sqlCnn = new SqlConnection(connetionString);
            sqlCnn.Open();
            sqlCmd = new SqlCommand("Sp_Recruitmentsendbulksmsgeo", sqlCnn);
            sqlCmd.Parameters.Add(new SqlParameter("@Action", "statusnullexam"));
            sqlCmd.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand = sqlCmd;
            adapter.Fill(ds);
            adapter.Dispose();
            sqlCmd.Dispose();
            sqlCnn.Close();
            return ds.Tables[0];

        }

        public DataTable GetpendingpaymentExamByRollNo(string RollNo = "")
        {


            string connetionString = null;
            SqlConnection sqlCnn;
            SqlCommand sqlCmd;
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataSet ds = new DataSet();
            int i = 0;
            string sql = null;
            connetionString = CommonSetting.constr;
            sqlCnn = new SqlConnection(connetionString);
            sqlCnn.Open();
            sqlCmd = new SqlCommand("Sp_Recruitmentsendbulksmsgeo", sqlCnn);
            sqlCmd.Parameters.Add(new SqlParameter("@Action", "statusnullexamByRollno"));
            sqlCmd.Parameters.Add(new SqlParameter("@RollNo", RollNo));
            sqlCmd.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand = sqlCmd;
            adapter.Fill(ds);
            adapter.Dispose();
            sqlCmd.Dispose();
            sqlCnn.Close();
            return ds.Tables[0];

        }

        public DataTable GetpendingpaymentExamByApplicationNo(string ApplicationNo = "")
        {


            string connetionString = null;
            SqlConnection sqlCnn;
            SqlCommand sqlCmd;
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataSet ds = new DataSet();
            int i = 0;
            string sql = null;
            connetionString = CommonSetting.constr;
            sqlCnn = new SqlConnection(connetionString);
            sqlCnn.Open();
            sqlCmd = new SqlCommand("Sp_Recruitmentsendbulksmsgeo", sqlCnn);
            sqlCmd.Parameters.Add(new SqlParameter("@Action", "statusnullexamByApplicationNo"));
            sqlCmd.Parameters.Add(new SqlParameter("@Applicationno", ApplicationNo));
            sqlCmd.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand = sqlCmd;
            adapter.Fill(ds);
            adapter.Dispose();
            sqlCmd.Dispose();
            sqlCnn.Close();
            return ds.Tables[0];

        }

        public RecruitmentList view1customfeesubmittedstudentdetailList(int sid = 0)
        {

            RecruitmentList list = new RecruitmentList();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
               // CommonMethod.WritetoNotepadtest("Page proc start view1customfeesubmittedstudentdetailList Exection", "start time " + System.DateTime.Now.ToString("yyyy.MM.dd HH:mm:ss:ffff"));

                var obj = conn.QueryMultiple("[sp_StudentRegistration]", new { Action = "FeeSubmitstudentReport_viewone", @Id = sid }, commandTimeout: 152000, commandType: CommandType.StoredProcedure);
                list.qlist = obj.Read<Recruitment>().ToList();
               // CommonMethod.WritetoNotepadtest("Page proc start view1customfeesubmittedstudentdetailList Exection", "end time " + System.DateTime.Now.ToString("yyyy.MM.dd HH:mm:ss:ffff"));

                // list.totalCount = obj.Read<string>().FirstOrDefault();
            }
            return list;
        }

        public DataTable GetpendingpaymentExamsafex(string collegeID = "")
        {


            string connetionString = null;
            SqlConnection sqlCnn;
            SqlCommand sqlCmd;
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataSet ds = new DataSet();
            int i = 0;
            string sql = null;
            connetionString = CommonSetting.constr;
            sqlCnn = new SqlConnection(connetionString);
            sqlCnn.Open();
            sqlCmd = new SqlCommand("Sp_RecruitmentsendbulksmsgeoSafex", sqlCnn);
            sqlCmd.Parameters.Add(new SqlParameter("@Action", "statusnullexam"));
            sqlCmd.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand = sqlCmd;
            adapter.Fill(ds);
            adapter.Dispose();
            sqlCmd.Dispose();
            sqlCnn.Close();
            return ds.Tables[0];

        }


        public DataTable Sp_DoubleVerification_Admission_Safex(string collegeID = "")
        {
            string connetionString = null;
            SqlConnection sqlCnn;
            SqlCommand sqlCmd;
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataSet ds = new DataSet();
            int i = 0;
            string sql = null;
            connetionString = CommonSetting.constr;
            sqlCnn = new SqlConnection(connetionString);
            sqlCnn.Open();
            sqlCmd = new SqlCommand("Sp_DoubleVerification_Admission_Safex", sqlCnn);
            sqlCmd.Parameters.Add(new SqlParameter("@Action", "AdmissionPaymentPendingLsit"));
            sqlCmd.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand = sqlCmd;
            adapter.Fill(ds);
            adapter.Dispose();
            sqlCmd.Dispose();
            sqlCnn.Close();
            return ds.Tables[0];
        }


        public DataTable Sp_DoubleVerification_Admission_Comman(string collegeID = "" , string Action="", string Applicationno="")
        {
            string connetionString = null;
            SqlConnection sqlCnn;
            SqlCommand sqlCmd;
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataSet ds = new DataSet();
            int i = 0;
            string sql = null;
            connetionString = CommonSetting.constr;
            sqlCnn = new SqlConnection(connetionString);
            sqlCnn.Open();
            sqlCmd = new SqlCommand("Sp_DoubleVerification_Admission_Comman", sqlCnn);
            sqlCmd.Parameters.Add(new SqlParameter("@Action", Action));
            sqlCmd.Parameters.Add(new SqlParameter("@Applicationno", Applicationno));
            sqlCmd.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand = sqlCmd;
            adapter.Fill(ds);
            adapter.Dispose();
            sqlCmd.Dispose();
            sqlCnn.Close();
            return ds.Tables[0];
        }

        public DataTable GetpendingpaymentExamsafexByApplicationNo(string ApplicationNo = "")
        {


            string connetionString = null;
            SqlConnection sqlCnn;
            SqlCommand sqlCmd;
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataSet ds = new DataSet();
            int i = 0;
            string sql = null;
            connetionString = CommonSetting.constr;
            sqlCnn = new SqlConnection(connetionString);
            sqlCnn.Open();
            sqlCmd = new SqlCommand("Sp_RecruitmentsendbulksmsgeoSafex", sqlCnn);
            sqlCmd.Parameters.Add(new SqlParameter("@Action", "statusnullexamByApplicationNo"));
            sqlCmd.Parameters.Add(new SqlParameter("@Applicationno", ApplicationNo));
            sqlCmd.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand = sqlCmd;
            adapter.Fill(ds);
            adapter.Dispose();
            sqlCmd.Dispose();
            sqlCnn.Close();
            return ds.Tables[0];

        }

        public DataTable GetpendingpaymentExamsafexByRollNo(string RollNo = "")
        {


            string connetionString = null;
            SqlConnection sqlCnn;
            SqlCommand sqlCmd;
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataSet ds = new DataSet();
            int i = 0;
            string sql = null;
            connetionString = CommonSetting.constr;
            sqlCnn = new SqlConnection(connetionString);
            sqlCnn.Open();
            sqlCmd = new SqlCommand("Sp_RecruitmentsendbulksmsgeoSafex", sqlCnn);
            sqlCmd.Parameters.Add(new SqlParameter("@Action", "statusnullexamByRollno"));
            sqlCmd.Parameters.Add(new SqlParameter("@RollNo", RollNo));
            sqlCmd.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand = sqlCmd;
            adapter.Fill(ds);
            adapter.Dispose();
            sqlCmd.Dispose();
            sqlCnn.Close();
            return ds.Tables[0];

        }

        public DataTable GetpendingpaymentExamAirPay(string collegeID = "")
        {


            string connetionString = null;
            SqlConnection sqlCnn;
            SqlCommand sqlCmd;
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataSet ds = new DataSet();
            int i = 0;
            string sql = null;
            connetionString = CommonSetting.constr;
            sqlCnn = new SqlConnection(connetionString);
            sqlCnn.Open();
            sqlCmd = new SqlCommand("Sp_RecruitmentsendbulksmsgeoAirPay", sqlCnn);
            sqlCmd.Parameters.Add(new SqlParameter("@Action", "statusnullexam"));
            sqlCmd.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand = sqlCmd;
            adapter.Fill(ds);
            adapter.Dispose();
            sqlCmd.Dispose();
            sqlCnn.Close();
            return ds.Tables[0];

        }

        public DataTable GetpendingpaymentExamAirPayByApplicationNo(string ApplicationNo = "")
        {


            string connetionString = null;
            SqlConnection sqlCnn;
            SqlCommand sqlCmd;
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataSet ds = new DataSet();
            int i = 0;
            string sql = null;
            connetionString = CommonSetting.constr;
            sqlCnn = new SqlConnection(connetionString);
            sqlCnn.Open();
            sqlCmd = new SqlCommand("Sp_RecruitmentsendbulksmsgeoAirPay", sqlCnn);
            sqlCmd.Parameters.Add(new SqlParameter("@Action", "statusnullexamByApplicationNo"));
            sqlCmd.Parameters.Add(new SqlParameter("@Applicationno", ApplicationNo));
            sqlCmd.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand = sqlCmd;
            adapter.Fill(ds);
            adapter.Dispose();
            sqlCmd.Dispose();
            sqlCnn.Close();
            return ds.Tables[0];

        }

        public DataTable GetpendingpaymentExamAirPayByRollNo(string RollNo = "")
        {


            string connetionString = null;
            SqlConnection sqlCnn;
            SqlCommand sqlCmd;
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataSet ds = new DataSet();
            int i = 0;
            string sql = null;
            connetionString = CommonSetting.constr;
            sqlCnn = new SqlConnection(connetionString);
            sqlCnn.Open();
            sqlCmd = new SqlCommand("Sp_RecruitmentsendbulksmsgeoAirPay", sqlCnn);
            sqlCmd.Parameters.Add(new SqlParameter("@Action", "statusnullexamByRollno"));
            sqlCmd.Parameters.Add(new SqlParameter("@RollNo", RollNo));
            sqlCmd.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand = sqlCmd;
            adapter.Fill(ds);
            adapter.Dispose();
            sqlCmd.Dispose();
            sqlCnn.Close();
            return ds.Tables[0];

        }

        public DataTable GetpendingpaymentExamEasebuzz(string collegeID = "")
        {


            string connetionString = null;
            SqlConnection sqlCnn;
            SqlCommand sqlCmd;
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataSet ds = new DataSet();
            int i = 0;
            string sql = null;
            connetionString = CommonSetting.constr;
            sqlCnn = new SqlConnection(connetionString);
            sqlCnn.Open();
            sqlCmd = new SqlCommand("Sp_RecruitmentsendbulksmsgeoEasebuzz", sqlCnn);
            sqlCmd.Parameters.Add(new SqlParameter("@Action", "statusnullexam"));
            sqlCmd.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand = sqlCmd;
            adapter.Fill(ds);
            adapter.Dispose();
            sqlCmd.Dispose();
            sqlCnn.Close();
            return ds.Tables[0];

        }

        public DataTable GetpendingpaymentEnrollment_Easebuzz(string collegeID = "")
        {


            string connetionString = null;
            SqlConnection sqlCnn;
            SqlCommand sqlCmd;
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataSet ds = new DataSet();
            int i = 0;
            string sql = null;
            connetionString = CommonSetting.constr;
            sqlCnn = new SqlConnection(connetionString);
            sqlCnn.Open();
            sqlCmd = new SqlCommand("Sp_doubleverification_enrollment", sqlCnn);
            sqlCmd.Parameters.Add(new SqlParameter("@Action", "EaseBuzz"));
            sqlCmd.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand = sqlCmd;
            adapter.Fill(ds);
            adapter.Dispose();
            sqlCmd.Dispose();
            sqlCnn.Close();
            return ds.Tables[0];

        }

        public DataTable GetpendingpaymentExamEasebuzzByApplicationNo(string ApplicationNo = "")
        {


            string connetionString = null;
            SqlConnection sqlCnn;
            SqlCommand sqlCmd;
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataSet ds = new DataSet();
            int i = 0;
            string sql = null;
            connetionString = CommonSetting.constr;
            sqlCnn = new SqlConnection(connetionString);
            sqlCnn.Open();
            sqlCmd = new SqlCommand("Sp_RecruitmentsendbulksmsgeoEasebuzz", sqlCnn);
            sqlCmd.Parameters.Add(new SqlParameter("@Action", "statusnullexamByApplicationNo"));
            sqlCmd.Parameters.Add(new SqlParameter("@Applicationno", ApplicationNo));
            sqlCmd.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand = sqlCmd;
            adapter.Fill(ds);
            adapter.Dispose();
            sqlCmd.Dispose();
            sqlCnn.Close();
            return ds.Tables[0];

        }

        public DataTable GetpendingpaymentExamEasebuzzByRollNo(string RollNo = "")
        {


            string connetionString = null;
            SqlConnection sqlCnn;
            SqlCommand sqlCmd;
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataSet ds = new DataSet();
            int i = 0;
            string sql = null;
            connetionString = CommonSetting.constr;
            sqlCnn = new SqlConnection(connetionString);
            sqlCnn.Open();
            sqlCmd = new SqlCommand("Sp_RecruitmentsendbulksmsgeoEasebuzz", sqlCnn);
            sqlCmd.Parameters.Add(new SqlParameter("@Action", "statusnullexamByRollno"));
            sqlCmd.Parameters.Add(new SqlParameter("@RollNo", RollNo));
            sqlCmd.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand = sqlCmd;
            adapter.Fill(ds);
            adapter.Dispose();
            sqlCmd.Dispose();
            sqlCnn.Close();
            return ds.Tables[0];

        }

    }
    public class RecruitmentList
    {
        public List<Recruitment> qlist { get; set; }
        public string totalCount { get; set; }
    }
    public class tbl_recruitment_cutoff
    {
        public int ID { get; set; }
        public int CounsellingNo { get; set; }
        public string adddate { get; set; }
        public string coursecategoryid { get; set; }

        public string sessionid { get; set; }
        public string caste { get; set; }
        public string totalCount { get; set; }
        public bool status { get; set; }
        public string Msg { get; set; }
      
    }
    public class safexdoubleverify
    {
        public string ag_id { get; set; }
        public string me_id { get; set; }
        public string ag_ref { get; set; }
        public string order_no { get; set; }

    }
}



