using Dapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace DataLayer
{
    public class BL_ForgotPass
    {

        [StringLength(20, ErrorMessage = "Password must be at least {5} characters long.", MinimumLength = 5)]
        [Required(ErrorMessage = "New Password is required")]
        public string NewPassword { get; set; }
        [Required(ErrorMessage = "Confirm Password is required")]
        [Compare("NewPassword")]
        public string ConfirmPassword { get; set; }
        public string ApplicationID { get; set; }
        public string Email { get; set; }
        public string Msg { get; set; }
        public bool Status { get; set; }
        public int UserType { get; set; }
        public int AdminID { get; set; }
        public int SID { get; set; }
      
        public string Name { get; set; }
        //college start
        public string CollegeName { get; set; }
        public int CollegeID { get; set; }
         public byte[] UNewPassword { get; set; }
        public string mobileno { get; set; }
        //college end

        public BL_ForgotPass ForgotPassword(int ID, byte[] NewPassword,string NewPasswordplain)
        {
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var ob = conn.Query<BL_ForgotPass>("sp_ForgotPasswordStudent", new { @SID = ID, @Role = "ResetStudentPass", @NewPassword= NewPassword, @NewPasswordplain= NewPasswordplain }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return ob;
            }
        }

        public BL_ForgotPass ResetPass(string ApplicationNo)
        {
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var ob = conn.Query<BL_ForgotPass>("sp_ForgotPasswordStudent", new { @ApplicationNo = ApplicationNo, @Role = "SendStudent" }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return ob;
            }
        }
        public BL_ForgotPass ForgotPassword_sms(int ID, byte[] NewPassword, string NewPasswordplain, string applicationno,string dob)
        {
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var ob = conn.Query<BL_ForgotPass>("sp_ForgotPasswordStudent", new { @SID = ID, @Role = "ResetStudentPassdirectupdate", @NewPassword = NewPassword, @NewPasswordplain = NewPasswordplain, @ApplicationNo= applicationno, @dob= dob }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return ob;
            }
        }

        public BL_ForgotPass ResetPassClg(string EmailID)
        {
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var ob = conn.Query<BL_ForgotPass>("sp_ForgotPassword", new { @Email = EmailID, @Role = "SendCollege" }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return ob;
            }
        }

    }
}
