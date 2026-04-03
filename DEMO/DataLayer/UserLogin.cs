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
    public class UserLogin
    {
        #region Properties
        [EmailAddress(ErrorMessage = "Invalid LoginId")]
        [Required(ErrorMessage = "LoginId is required")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }

        public int UserType { get; set; }
        public string UserName { get; set; }
        public string ContactNo { get; set; }
        public bool IsActive { get; set; }
        public long ID { get; set; }

        [Required(ErrorMessage = "LoginId is required")]
        public string LoginID { get; set; }
        public string ProfilePic { get; set; }
        public bool rememberMe { get; set; }
        public int UserID { get; set; }
        public string msg { get; set; }
        public bool status { get; set; }
        public string menustr { get; set; }
        public string Mobile { get; set; }
        public string confpassword { get; set; }
        public int Permission { get; set; }
        public string apitrxid { get; set; }
        #endregion
        public UserLogin Login(UserLogin objLogin)
        {
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var tbl = conn.Query<UserLogin>("[USP_AdminLogin]", new { LoginID = objLogin.Email, Password = objLogin.Password }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                if (tbl.UserID > 0)
                {
                    objLogin.status = true;
                    objLogin.UserID = tbl.UserID;
                    objLogin.msg = tbl.msg;
                    objLogin.IsActive = tbl.IsActive;
                    objLogin.UserType = tbl.UserType;
                    objLogin.menustr = tbl.menustr;
                    objLogin.LoginID = tbl.LoginID;
                    objLogin.UserName = tbl.UserName;
                    objLogin.ProfilePic = tbl.ProfilePic;
                }
                else
                {
                    objLogin.msg = tbl.msg;
                    objLogin.status = false;
                }

                return objLogin;
            }
        }
        public UserLogin userdata(int id = 0)
        {
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var tbl = conn.Query<UserLogin>("[sp_AdminMenu]", new
                {
                    @Action = "UserById",
                    @ID = id
                }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return tbl;
            }
        }


        public static bool logout(string id = "0")
        {
            ExpireAllCookies();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                //var obj = conn.Query<UserLogin>("update tbl_userMaster set isLoggedin=0 where ID=@ID ", new { ID = id }, commandType: CommandType.Text).FirstOrDefault();
                var obj = conn.Query<UserLogin>("[sp_Common_QueryMethod]", new
                {
                    @Action = "Logout",
                    @ID = id
                }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return true;
            }

        }
        public static void ExpireAllCookies()
        {
            if (HttpContext.Current != null)
            {
                int cookieCount = HttpContext.Current.Request.Cookies.Count;
                for (var i = 0; i < cookieCount; i++)
                {
                    var cookie = HttpContext.Current.Request.Cookies[i];
                    if (cookie != null)
                    {
                        if (cookie.Name != "rcpc")
                        {
                            var expiredCookie = new HttpCookie(cookie.Name)
                            {

                                Expires = DateTime.Now.AddDays(-1),
                                Domain = cookie.Domain

                            };
                            HttpContext.Current.Response.Cookies.Add(expiredCookie); // overwrite it
                        }

                    }
                }

                // clear cookies server side
                HttpContext.Current.Request.Cookies.Clear();
            }
        }
        public ChangePasswordAdmin ChangePassword(ChangePasswordAdmin obj)
        {
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var ob = conn.Query<ChangePasswordAdmin>("sp_ChangeCollegePassword", new { @CollegeCode = obj.ID, CurrentPassword = obj.CurrentPassword, NewPassword = obj.NewPassword, Status = "ChangePasswordAdmin" }, commandType: CommandType.StoredProcedure).FirstOrDefault();

                return ob;
            }
        }
        //GetUserRole
        public List<UserRole> GetUserRole()
        {
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var ob = conn.Query<UserRole>("sp_UserMaster", new { @Action = "Userrole" }, commandType: CommandType.StoredProcedure).ToList();

                return ob;
            }
        }
        public UserLogin Addnewusertype(UserLogin objLogin)
        {
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var obj = conn.Query<UserLogin>("[sp_UserMaster]", new
                {
                    @Action = "Insert",
                    @ID = objLogin.ID,
                    @UserType = objLogin.UserType,
                    @UserName = objLogin.UserName,
                    @Mobile = objLogin.Mobile,
                    @ContactNo = objLogin.ContactNo,
                    @Email = Email,
                    @Password = objLogin.Password,
                    @Permission = objLogin.Permission,
                    @MenuStr = ",",


                }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return obj;




            }
        }
        public UserLogin Checkuserbyname(string id = "")
        {
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var tbl = conn.Query<UserLogin>("[sp_UserMaster]", new
                {
                    @Action = "UserByName",
                    @UserName = id
                }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return tbl;
            }
        }
        public List<doubleverificationgetstudent> GetStudents_ForDoubleVerification(doubleverificationgetstudent objLogin)
        {
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var obj = conn.Query<doubleverificationgetstudent>("[USP_doubleverificationforcollegefee]", new
                {
                    @Action = "Insert",
                    @collegeid = objLogin.collegeid
                }, commandType: CommandType.StoredProcedure).ToList();
                return obj;
            }
        }
        public List<doubleverificationgetstudent> GetStudents_ForDoubleVerification300Airpay(doubleverificationgetstudent objLogin, string ApplicationNo)
        {
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var obj = conn.Query<doubleverificationgetstudent>("[USP_doubleverificationforRegistration]", new
                {
                    @Action = "Airpay",
                    @applicationno = ApplicationNo
                }, commandType: CommandType.StoredProcedure).ToList();
                return obj;
            }
        }

        public List<doubleverificationgetstudent> GetStudents_ForDoubleVerificationAdmissionAirpay(doubleverificationgetstudent objLogin, string ApplicationNo,string Getway)
        {
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var obj = conn.Query<doubleverificationgetstudent>("[USP_doubleverificationforcollegefee]", new
                {
                    @Action = "GetStudentDetail",
                    @GetWayType= Getway,
                    @applicationno = ApplicationNo
                }, commandType: CommandType.StoredProcedure).ToList();
                return obj;
            }
        }

        public List<doubleverificationgetstudent> DoubleVerificationAdmission_Comman(doubleverificationgetstudent objLogin, string ApplicationNo, string Getway, string Action)
        {
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var obj = conn.Query<doubleverificationgetstudent>("[Sp_DoubleVerification_Admission_Comman]", new
                {
                    @Action = Action,
                    @GetWayType = Getway,
                    @applicationno = ApplicationNo
                }, commandType: CommandType.StoredProcedure).ToList();
                return obj;
            }
        }

        public List<doubleverificationgetstudent> RegistrationVerification300EaseBuss(doubleverificationgetstudent objLogin, string ApplicationNo)
        {
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var obj = conn.Query<doubleverificationgetstudent>("[USP_doubleverificationforRegistration]", new
                {
                    @Action = "EaseBuzz",
                    @applicationno = ApplicationNo
                }, commandType: CommandType.StoredProcedure).ToList();
                return obj;
            }
        }

        public List<doubleverificationgetstudent> GetStudents_ForDoubleVerification300(doubleverificationgetstudent objLogin)
        {
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var obj = conn.Query<doubleverificationgetstudent>("[USP_doubleverificationforcollegefee]", new
                {
                    @Action = "get300",
                    @collegeid = objLogin.collegeid
                }, commandType: CommandType.StoredProcedure).ToList();
                return obj;
            }
        }
        public List<doubleverificationgetstudent> GetStudents_ForDoubleVerificationgetenroll(doubleverificationgetstudent objLogin)
        {
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var obj = conn.Query<doubleverificationgetstudent>("[USP_doubleverificationforcollegefee]", new
                {
                    @Action = "getenroll",
                    @collegeid = objLogin.collegeid
                }, commandType: CommandType.StoredProcedure).ToList();
                return obj;
            }
        }
        public int addtempdata(doubleverificationgetstudent objLogin)
        {
            int Returnvalue = 0;
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                Returnvalue = conn.Execute("[USP_doubleverificationforcollegefee]", new
                {
                    @Action = "inserttemp",
                    @applicationno = objLogin.applicationno,
                    @transactionid = objLogin.clienttrxid,
                    @sid = objLogin.sid,
                    @paystatus = objLogin.paystatus
                }, commandType: CommandType.StoredProcedure);
                return Returnvalue;
            }
        }
        public int addtempdata4(doubleverificationgetstudent objLogin)
        {
            int Returnvalue = 0;
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                Returnvalue = conn.Execute("[USP_doubleverificationforcollegefee]", new
                {
                    @Action = "inserttemp4",
                    @applicationno = objLogin.applicationno,
                    @transactionid = objLogin.clienttrxid,
                    @sid = objLogin.sid,
                    @paystatus = objLogin.paystatus
                }, commandType: CommandType.StoredProcedure);
                return Returnvalue;
            }
        }
        public int addtempdata5(doubleverificationgetstudent objLogin)
        {
            int Returnvalue = 0;
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                Returnvalue = conn.Execute("[USP_doubleverificationforcollegefee]", new
                {
                    @Action = "inserttemp5",
                    @applicationno = objLogin.applicationno,
                    @transactionid = objLogin.clienttrxid,
                    @sid = objLogin.sid,
                    @paystatus = objLogin.paystatus
                }, commandType: CommandType.StoredProcedure);
                return Returnvalue;
            }
        }

    }
    public class ChangePasswordAdmin
    {
        [Required(ErrorMessage = "Current Password is required")]
        public string CurrentPassword { get; set; }
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
        public int ID { get; set; }


    }
    public class UserRole
    {
        public int URoleID { get; set; }
        public string URole { get; set; }
    }

    public class doubleverificationgetstudent
    {
        public int collegeid { get; set; }
        public string Mid { get; set; }
        public int sid { get; set; }
        public string clienttrxid { get; set; }
        public string applicationno { get; set; }
        public int type { get; set; }
        public string paystatus { get; set; }
        public string Email { get; set; }
        public string MobileNo { get; set; }
        public string Amount { get; set; }
        public string firstname { get; set; }


    }


}
