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
using DataLayer;
using System.IO;


namespace DataLayer
{
    public class StudentLogin
    {
        public Login Login(Login objLogin)
        {
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {


                CommonMethod cmn = new CommonMethod();
                string ip = CommonMethod.GetIPAddress();
                objLogin = conn.Query<Login>("ProcST_StudentLogin", new { @ApplicationNo = objLogin.ApplicationNo, @Password = objLogin.Password, @IPAddress = ip, @U_password = objLogin.hashedBytes }, commandTimeout: 120000, commandType: CommandType.StoredProcedure).FirstOrDefault();
                if (objLogin.Id <= 0)
                {
                    objLogin.status = false;

                }
                return objLogin;
            }
        }
        public Login DevLogin(Login objLogin)
        {
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                CommonMethod cmn = new CommonMethod();
                string ip = CommonMethod.GetIPAddress();
                objLogin = conn.Query<Login>("ProcST_StudentLogin_developer", new { @ApplicationNo = objLogin.ApplicationNo, @Password = objLogin.Password, @IPAddress = ip, @U_password = objLogin.hashedBytes }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                if (objLogin.Id <= 0)
                {
                    objLogin.status = false;

                }
                return objLogin;
            }
        }

        public Login alldetailcheeck(string ApplicationNo)
        {
            Login obj = new Login();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                CommonMethod cmn = new CommonMethod();
                //CommonMethod.WritetoNotepadtest("Page proc start BasicDetail Exection", "start time " + System.DateTime.Now.ToString("yyyy.MM.dd HH:mm:ss:ffff")); commandTimeout: 152000 120000, 
                obj = conn.Query<Login>("[sp_StudentChecpassinFirstAndSecond]", new { @Action = "GetByID", @ApplicID = ApplicationNo }, commandTimeout: 152000, commandType: CommandType.StoredProcedure).FirstOrDefault();
                //CommonMethod.WritetoNotepadtest("Page proc start BasicDetail Exection", "end time " + System.DateTime.Now.ToString("yyyy.MM.dd HH:mm:ss:ffff"));
                return obj;
            }
        }

        public Login BasicDetail(string ApplicationNo)
        {
            Login obj = new Login();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                CommonMethod cmn = new CommonMethod();
                //CommonMethod.WritetoNotepadtest("Page proc start BasicDetail Exection", "start time " + System.DateTime.Now.ToString("yyyy.MM.dd HH:mm:ss:ffff")); commandTimeout: 152000 120000, 
                obj = conn.Query<Login>("[sp_StudentRegistration]", new { @Action = "GetByID", @ApplicID = ApplicationNo }, commandTimeout: 152000, commandType: CommandType.StoredProcedure).FirstOrDefault();
                //CommonMethod.WritetoNotepadtest("Page proc start BasicDetail Exection", "end time " + System.DateTime.Now.ToString("yyyy.MM.dd HH:mm:ss:ffff"));
                return obj;
            }
        }
        public Login BasicDetailpg(string ApplicationNo)
        {
            Login obj = new Login();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                CommonMethod cmn = new CommonMethod();

                obj = conn.Query<Login>("[sp_StudentRegistration_pg]", new { @Action = "GetByID", @ApplicID = ApplicationNo }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return obj;
            }
        }

        public ChangePassword ChangePassword(ChangePassword obj)
        {
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var ob = conn.Query<ChangePassword>("sp_ChangeStudentPassword", new { ApplicationID = obj.ApplicationID, CurrentPassword = obj.Current_password, NewPassword = obj.newU_password, Status = "ChangePassword", @NewPasswordplain = obj.NewPassword }, commandType: CommandType.StoredProcedure).FirstOrDefault();

                return ob;
            }
        }
        public static bool logout(string id = "0")
        {
            ExpireAllCookies();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var obj = conn.Query<Login>("sp_ChangeStudentPassword", new { ApplicationID = id }, commandType: CommandType.StoredProcedure).FirstOrDefault();
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
        public Login Student_registration(Login objLogin)
        {
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                string ip = CommonMethod.GetIPAddress();
                if (objLogin.DOB == null)
                {
                    objLogin.DOB = "";
                }
                else
                {
                    objLogin.DOB = objLogin.DOB.Replace("-", "/");
                    objLogin.DOB = objLogin.DOB.Replace(".", "/");
                    objLogin.DOB = objLogin.DOB.Replace(" ", "/");
                    objLogin.DOB = DateTime.ParseExact(objLogin.DOB, "dd/MM/yyyy", null).ToString("MM/dd/yyyy");
                }
                var obj = conn.Query<Login>("[sp_StudentRegistration]", new
                {
                    @Id = 0,
                    @FirstName = objLogin.FirstName,
                    @MiddleName = objLogin.MiddleName,
                    @LastName = objLogin.LastName,
                   
                    @Gender = objLogin.Gender,
                    @DOB = objLogin.DOB,
                    @CastCategory = objLogin.CastCategory,
                    @BloodGroup = objLogin.BloodGroup,
                    @MobileNo = objLogin.MobileNo,
                    @Email = objLogin.Email,
                    @CurrentAddress = objLogin.CurrentAddress,
                    @CA_PinCode = objLogin.CA_PinCode,
                    @CA_Country = objLogin.CA_Country,
                    @CA_State = objLogin.CA_State,
                    @CA_City = objLogin.CA_City,
                    @PA_Address = objLogin.PA_Address,
                    @PA_PinCode = objLogin.PA_PinCode,
                    @PA_Country = objLogin.PA_Country,
                    @PA_State = objLogin.PA_State,
                    @PA_City = objLogin.PA_City,
                    @FatherName = objLogin.FatherName,
                    @FatherQualification = objLogin.FatherQualification,
                    @FatherOccupation = objLogin.FatherOccupation,
                    @FatherMobile = objLogin.FatherMobile,
                    @FatherEmail = objLogin.FatherEmail,
                    @MotherName = objLogin.MotherName,
                    @MotherQualification = objLogin.MotherQualification,
                    @MotherOccupation = objLogin.MotherOccupation,
                    @MotherEmail = objLogin.MotherEmail,
                    @MotherMobile = objLogin.MotherMobile,
                    @session = objLogin.session,
                    @AdmisitionCategory = objLogin.AdmisitionCategory,
                    @CourseType = objLogin.CourseType,
                    @EducationType = objLogin.EducationType,
                    @CourseCategory = objLogin.CourseCategory,
                    @IsQualifying = 0,
                    @IsLogin = 0,
                    @Action = "Insert",
                    @ApplicID = 0,
                    @stphoto = objLogin.stphoto,
                    @stsign = objLogin.stsign,
                    @title = objLogin.title,
                    @ftile = objLogin.Ftitle,
                    @Nationality = objLogin.Nationality,
                    @Religion = objLogin.Religion,
                    @MotherTongue = objLogin.MotherTongue,
                    @ishandicapped = objLogin.ishandicapped,
                    @isex_service_man = objLogin.isex_service_man,
                    @is_ncc_candidate = objLogin.is_ncc_candidate,
                    @disabilityType = objLogin.disabilityType,
                    @disabilityPercent = objLogin.disabilityPercent,
                    @aadharno = objLogin.aadharno,
                    @previous_qua_id = objLogin.prevoiustreamid,
                    @FirstNameInHindi = objLogin.FirstNameInHindi,
                    @MiddleNameInHindi = objLogin.MiddleNameInHindi,
                    @LastNameInHindi = objLogin.LastNameInHindi,
                    @ReligonOther = objLogin.ReligonOther,
                    @FatherNameInHindi = objLogin.FatherNameInHindi,// N"'+ objLogin.FatherNameInHindi+',
                    @MotherNameInHindi = objLogin.MotherNameInHindi,
                    @IsSports = objLogin.IsSports,
                    @IsStaff = objLogin.IsStaff,
                    @U_password = objLogin.hashedBytes,
                    @prevoiusboardid = objLogin.prevoiusboardid,
                    @is_GEW = objLogin.is_GEW,
                    @is_permanentaddress = objLogin.is_permanentaddress

                }, commandType: CommandType.StoredProcedure).FirstOrDefault();

                return obj;


            }
        }
        public Login Student_registrationPG(Login objLogin)
        {
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                string ip = CommonMethod.GetIPAddress();
                if (objLogin.DOB == null)
                {
                    objLogin.DOB = "";
                }
                else
                {
                    objLogin.DOB = objLogin.DOB.Replace("-", "/");
                    objLogin.DOB = objLogin.DOB.Replace("-", "/");
                    objLogin.DOB = objLogin.DOB.Replace("-", "/");
                    objLogin.DOB = objLogin.DOB.Replace(".", "/");
                    objLogin.DOB = objLogin.DOB.Replace(" ", "/");
                    objLogin.DOB = DateTime.ParseExact(objLogin.DOB, "dd/MM/yyyy", null).ToString("MM/dd/yyyy");
                }
                var obj = conn.Query<Login>("[sp_StudentRegistration_pg]", new
                {
                    @Id = 0,
                    @FirstName = objLogin.FirstName,
                    @MiddleName = objLogin.MiddleName,
                    @LastName = objLogin.LastName,
                    @Gender = objLogin.Gender,
                    @DOB = objLogin.DOB,
                    @CastCategory = objLogin.CastCategory,
                    @BloodGroup = objLogin.BloodGroup,
                    @MobileNo = objLogin.MobileNo,
                    @Email = objLogin.Email,
                    @CurrentAddress = objLogin.CurrentAddress,
                    @CA_PinCode = objLogin.CA_PinCode,
                    @CA_Country = objLogin.CA_Country,
                    @CA_State = objLogin.CA_State,
                    @CA_City = objLogin.CA_City,
                    @PA_Address = objLogin.PA_Address,
                    @PA_PinCode = objLogin.PA_PinCode,
                    @PA_Country = objLogin.PA_Country,
                    @PA_State = objLogin.PA_State,
                    @PA_City = objLogin.PA_City,
                    @FatherName = objLogin.FatherName,
                    @FatherQualification = objLogin.FatherQualification,
                    @FatherOccupation = objLogin.FatherOccupation,
                    @FatherMobile = objLogin.FatherMobile,
                    @FatherEmail = objLogin.FatherEmail,
                    @MotherName = objLogin.MotherName,
                    @MotherQualification = objLogin.MotherQualification,
                    @MotherOccupation = objLogin.MotherOccupation,
                    @MotherEmail = objLogin.MotherEmail,
                    @MotherMobile = objLogin.MotherMobile,
                    @session = objLogin.session,
                    @AdmisitionCategory = objLogin.AdmisitionCategory,
                    @CourseType = objLogin.CourseType,
                    @EducationType = objLogin.EducationType,
                    @CourseCategory = objLogin.CourseCategory,
                    @IsQualifying = 0,
                    @IsLogin = 0,
                    @Action = "Insert",
                    @ApplicID = 0,
                    @stphoto = objLogin.stphoto,
                    @stsign = objLogin.stsign,
                    @title = objLogin.title,
                    @ftile = objLogin.Ftitle,
                    @Nationality = objLogin.Nationality,
                    @Religion = objLogin.Religion,
                    @MotherTongue = objLogin.MotherTongue,
                    @ishandicapped = objLogin.ishandicapped,
                    @isex_service_man = objLogin.isex_service_man,
                    @is_ncc_candidate = objLogin.is_ncc_candidate,
                    @aadharno = objLogin.aadharno,
                    @previous_qua_id = objLogin.prevoiustreamid,
                    @FirstNameInHindi = objLogin.FirstNameInHindi,
                    @MiddleNameInHindi = objLogin.MiddleNameInHindi,
                    @LastNameInHindi = objLogin.LastNameInHindi,
                    @ReligonOther = objLogin.ReligonOther,
                    @FatherNameInHindi = objLogin.FatherNameInHindi,// N"'+ objLogin.FatherNameInHindi+',
                    @MotherNameInHindi = objLogin.MotherNameInHindi,
                    @IsSports = objLogin.IsSports,
                    @IsStaff = objLogin.IsStaff,
                    @U_password = objLogin.hashedBytes,
                    @prevoiusboardid = objLogin.prevoiusboardid,
                    @is_GEW = objLogin.is_GEW,
                    @is_permanentaddress = objLogin.is_permanentaddress,
                    @Password = objLogin.Password

                }, commandType: CommandType.StoredProcedure).FirstOrDefault();

                return obj;
            }
        }

        public Login Student_registrationUpdate(Login objLogin)
        {
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                int StID = Convert.ToInt32(ClsLanguage.GetCookies("NBStID"));
                if (StID == 0)
                {
                    objLogin.status = false;
                    objLogin.Message = "Network Error !!";
                    return objLogin;
                }
                string ip = CommonMethod.GetIPAddress();
                if (objLogin.DOB == null)
                {
                    objLogin.DOB = "";
                }
                else
                {
                    objLogin.DOB = objLogin.DOB.Replace("-", "/");
                    objLogin.DOB = objLogin.DOB.Replace("-", "/");
                    objLogin.DOB = objLogin.DOB.Replace(".", "/");
                    objLogin.DOB = objLogin.DOB.Replace(" ", "/");
                    objLogin.DOB = DateTime.ParseExact(objLogin.DOB, "dd/MM/yyyy", null).ToString("MM/dd/yyyy");
                }
                var obj = conn.Query<Login>("[sp_StudentRegistration]", new
                {
                    @Id = StID,
                    @FirstName = CommonSetting.RemoveSpecialChars(objLogin.FirstName),
                    @MiddleName = CommonSetting.RemoveSpecialChars(objLogin.MiddleName),
                    @LastName = CommonSetting.RemoveSpecialChars(objLogin.LastName),
                    @Gender = objLogin.Gender,
                    @DOB = objLogin.DOB,
                    @CastCategory = objLogin.CastCategory,
                    @BloodGroup = objLogin.BloodGroup,
                    @MobileNo = CommonSetting.RemoveSpecialChars(objLogin.MobileNo),
                    @Email = CommonSetting.RemoveSpecialCharsemail(objLogin.Email),
                    @CurrentAddress = CommonSetting.RemoveSpecialCharsaddress(objLogin.CurrentAddress),
                    @CA_PinCode = CommonSetting.RemoveSpecialChars(objLogin.CA_PinCode),
                    @CA_Country = objLogin.CA_Country,
                    @CA_State = objLogin.CA_State,
                    @CA_City = CommonSetting.RemoveSpecialCharsaddress(objLogin.CA_City),
                    @PA_Address = CommonSetting.RemoveSpecialCharsaddress(objLogin.PA_Address),
                    @PA_PinCode = CommonSetting.RemoveSpecialChars(objLogin.PA_PinCode),
                    @PA_Country = objLogin.PA_Country,
                    @PA_State = objLogin.PA_State,
                    @PA_City = CommonSetting.RemoveSpecialCharsaddress(objLogin.PA_City),
                    @FatherName = CommonSetting.RemoveSpecialChars(objLogin.FatherName),
                    @FatherQualification = CommonSetting.RemoveSpecialChars(objLogin.FatherQualification),
                    @FatherOccupation = objLogin.FatherOccupation,
                    @FatherMobile = CommonSetting.RemoveSpecialChars(objLogin.FatherMobile),
                    @FatherEmail = CommonSetting.RemoveSpecialCharsemail(objLogin.FatherEmail),
                    @MotherName = CommonSetting.RemoveSpecialChars(objLogin.MotherName),
                    @MotherQualification = CommonSetting.RemoveSpecialChars(objLogin.MotherQualification),
                    @MotherOccupation = CommonSetting.RemoveSpecialChars(objLogin.MotherOccupation),
                    @MotherEmail = CommonSetting.RemoveSpecialCharsemail(objLogin.MotherEmail),
                    @MotherMobile = CommonSetting.RemoveSpecialChars(objLogin.MotherMobile),
                    @session = objLogin.session,
                    @AdmisitionCategory = objLogin.AdmisitionCategory,
                    @CourseType = objLogin.CourseType,
                    @EducationType = objLogin.EducationType,
                    @CourseCategory = objLogin.CourseCategory,
                    @IsQualifying = 0,
                    @IsLogin = 0,
                    @Action = "Update",
                    @ApplicID = 0,
                    @stphoto = objLogin.stphoto,
                    @stsign = objLogin.stsign,
                    @title = objLogin.title,
                    @ftile = objLogin.Ftitle,
                    @Nationality = objLogin.Nationality,
                    @Religion = objLogin.Religion,
                    @MotherTongue = objLogin.MotherTongue,
                    @ishandicapped = objLogin.ishandicapped,
                    @isex_service_man = objLogin.isex_service_man,
                    @is_ncc_candidate = objLogin.is_ncc_candidate,
                    @aadharno = CommonSetting.RemoveSpecialChars(objLogin.aadharno),
                    @disabilityType = CommonSetting.RemoveSpecialChars(objLogin.disabilityType),
                    @disabilityPercent = CommonSetting.RemoveSpecialChars(objLogin.disabilityPercent),
                    @FirstNameInHindi = CommonSetting.RemoveSpecialChars(objLogin.FirstNameInHindi),
                    @MiddleNameInHindi = objLogin.MiddleNameInHindi,
                    @LastNameInHindi = objLogin.LastNameInHindi,
                    @ReligonOther = CommonSetting.RemoveSpecialChars(objLogin.ReligonOther),
                    @FatherNameInHindi = objLogin.FatherNameInHindi,
                    @MotherNameInHindi = objLogin.MotherNameInHindi,
                    @IsSports = objLogin.IsSports,
                    @IsStaff = objLogin.IsStaff,
                    @is_GEW = objLogin.is_GEW,
                    @is_permanentaddress = objLogin.is_permanentaddress,
                    @previous_qua_id = objLogin.previous_qua_id,
                    @prevoiusboardid = objLogin.prevoiusboardid,

                }, commandType: CommandType.StoredProcedure).FirstOrDefault();

                return obj;


            }
        }
        public Login Student_registrationUpdate_pg(Login objLogin)
        {
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                int StID = Convert.ToInt32(ClsLanguage.GetCookies("NBStID"));
                if (StID == 0)
                {
                    objLogin.status = false;
                    objLogin.Message = "Network Error !!";
                    return objLogin;
                }
                string ip = CommonMethod.GetIPAddress();
                if (objLogin.DOB == null)
                {
                    objLogin.DOB = "";
                }
                else
                {
                    objLogin.DOB = objLogin.DOB.Replace("-", "/");
                    objLogin.DOB = objLogin.DOB.Replace("-", "/");
                    objLogin.DOB = objLogin.DOB.Replace(".", "/");
                    objLogin.DOB = objLogin.DOB.Replace(" ", "/");
                    objLogin.DOB = objLogin.DOB.Replace("-", "/");
                    objLogin.DOB = DateTime.ParseExact(objLogin.DOB, "dd/MM/yyyy", null).ToString("MM/dd/yyyy");
                }
                var obj = conn.Query<Login>("[sp_StudentRegistration_pg]", new
                {
                    @Id = StID,
                    @FirstName = CommonSetting.RemoveSpecialChars(objLogin.FirstName),
                    @MiddleName = CommonSetting.RemoveSpecialChars(objLogin.MiddleName),
                    @LastName = CommonSetting.RemoveSpecialChars(objLogin.LastName),
                    @Gender = objLogin.Gender,
                    @DOB = objLogin.DOB,
                    @CastCategory = objLogin.CastCategory,
                    @BloodGroup = objLogin.BloodGroup,
                    @MobileNo = CommonSetting.RemoveSpecialChars(objLogin.MobileNo),
                    @Email = CommonSetting.RemoveSpecialCharsemail(objLogin.Email),
                    @CurrentAddress = CommonSetting.RemoveSpecialCharsaddress(objLogin.CurrentAddress),
                    @CA_PinCode = CommonSetting.RemoveSpecialChars(objLogin.CA_PinCode),
                    @CA_Country = objLogin.CA_Country,
                    @CA_State = objLogin.CA_State,
                    @CA_City = CommonSetting.RemoveSpecialCharsaddress(objLogin.CA_City),
                    @PA_Address = CommonSetting.RemoveSpecialCharsaddress(objLogin.PA_Address),
                    @PA_PinCode = CommonSetting.RemoveSpecialChars(objLogin.PA_PinCode),
                    @PA_Country = objLogin.PA_Country,
                    @PA_State = objLogin.PA_State,
                    @PA_City = CommonSetting.RemoveSpecialCharsaddress(objLogin.PA_City),
                    @FatherName = CommonSetting.RemoveSpecialChars(objLogin.FatherName),
                    @FatherQualification = CommonSetting.RemoveSpecialChars(objLogin.FatherQualification),
                    @FatherOccupation = objLogin.FatherOccupation,
                    @FatherMobile = CommonSetting.RemoveSpecialChars(objLogin.FatherMobile),
                    @FatherEmail = CommonSetting.RemoveSpecialCharsemail(objLogin.FatherEmail),
                    @MotherName = CommonSetting.RemoveSpecialChars(objLogin.MotherName),
                    @MotherQualification = CommonSetting.RemoveSpecialChars(objLogin.MotherQualification),
                    @MotherOccupation = CommonSetting.RemoveSpecialChars(objLogin.MotherOccupation),
                    @MotherEmail = CommonSetting.RemoveSpecialCharsemail(objLogin.MotherEmail),
                    @MotherMobile = CommonSetting.RemoveSpecialChars(objLogin.MotherMobile),
                    @session = 40,
                    @AdmisitionCategory = objLogin.AdmisitionCategory,
                    @CourseType = objLogin.CourseType,
                    @EducationType = objLogin.EducationType,
                    @CourseCategory = objLogin.CourseCategory,
                    @IsQualifying = 0,
                    @IsLogin = 0,
                    @Action = "Update",
                    @ApplicID = 0,
                    @stphoto = objLogin.stphoto,
                    @stsign = objLogin.stsign,
                    @title = objLogin.title,
                    @ftile = objLogin.Ftitle,
                    @Nationality = objLogin.Nationality,
                    @Religion = objLogin.Religion,
                    @MotherTongue = objLogin.MotherTongue,
                    @ishandicapped = objLogin.ishandicapped,
                    @isex_service_man = objLogin.isex_service_man,
                    @is_ncc_candidate = objLogin.is_ncc_candidate,
                    @aadharno = CommonSetting.RemoveSpecialChars(objLogin.aadharno),
                    @FirstNameInHindi = CommonSetting.RemoveSpecialChars(objLogin.FirstNameInHindi),
                    @MiddleNameInHindi = objLogin.MiddleNameInHindi,
                    @LastNameInHindi = objLogin.LastNameInHindi,
                    @ReligonOther = CommonSetting.RemoveSpecialChars(objLogin.ReligonOther),
                    @FatherNameInHindi = objLogin.FatherNameInHindi,
                    @MotherNameInHindi = objLogin.MotherNameInHindi,
                    @IsSports = objLogin.IsSports,
                    @IsStaff = objLogin.IsStaff,
                    @is_GEW = objLogin.is_GEW,
                    @is_permanentaddress = objLogin.is_permanentaddress
                }, commandType: CommandType.StoredProcedure).FirstOrDefault();

                return obj;


            }
        }
        public Login Student_registrationUpdateOldStudentPG(Login objLogin)
        {
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                int StID = Convert.ToInt32(ClsLanguage.GetCookies("NBStID"));
                if (StID == 0)
                {
                    objLogin.status = false;
                    objLogin.Message = "Network Error !!";
                    return objLogin;
                }
                string ip = CommonMethod.GetIPAddress();
                if (objLogin.DOB == null)
                {
                    objLogin.DOB = "";
                }
                else
                {
                    objLogin.DOB = objLogin.DOB.Replace("-", "/");
                    objLogin.DOB = objLogin.DOB.Replace("-", "/");
                    objLogin.DOB = objLogin.DOB.Replace("-", "/");
                    objLogin.DOB = objLogin.DOB.Replace(".", "/");
                    objLogin.DOB = objLogin.DOB.Replace(" ", "/");
                    objLogin.DOB = DateTime.ParseExact(objLogin.DOB, "dd/MM/yyyy", null).ToString("MM/dd/yyyy");
                }
                var obj = conn.Query<Login>("[sp_AddStudentDataPG]", new
                {
                    @Id = StID,
                    @FirstName = CommonSetting.RemoveSpecialChars(objLogin.FirstName),
                    @MiddleName = CommonSetting.RemoveSpecialChars(objLogin.MiddleName),
                    @LastName = CommonSetting.RemoveSpecialChars(objLogin.LastName),
                    @Gender = objLogin.Gender,
                    @DOB = objLogin.DOB,
                    @CastCategory = objLogin.CastCategory,
                    @BloodGroup = objLogin.BloodGroup,
                    @MobileNo = CommonSetting.RemoveSpecialChars(objLogin.MobileNo),
                    @Email = CommonSetting.RemoveSpecialCharsemail(objLogin.Email),
                    @CurrentAddress = CommonSetting.RemoveSpecialCharsaddress(objLogin.CurrentAddress),
                    @CA_PinCode = CommonSetting.RemoveSpecialChars(objLogin.CA_PinCode),
                    @CA_Country = objLogin.CA_Country,
                    @CA_State = objLogin.CA_State,
                    @CA_City = CommonSetting.RemoveSpecialCharsaddress(objLogin.CA_City),
                    @PA_Address = CommonSetting.RemoveSpecialCharsaddress(objLogin.PA_Address),
                    @PA_PinCode = CommonSetting.RemoveSpecialChars(objLogin.PA_PinCode),
                    @PA_Country = objLogin.PA_Country,
                    @PA_State = objLogin.PA_State,
                    @PA_City = CommonSetting.RemoveSpecialCharsaddress(objLogin.PA_City),
                    @FatherName = CommonSetting.RemoveSpecialChars(objLogin.FatherName),
                    @FatherQualification = CommonSetting.RemoveSpecialChars(objLogin.FatherQualification),
                    @FatherOccupation = objLogin.FatherOccupation,
                    @FatherMobile = CommonSetting.RemoveSpecialChars(objLogin.FatherMobile),
                    @FatherEmail = CommonSetting.RemoveSpecialCharsemail(objLogin.FatherEmail),
                    @MotherName = CommonSetting.RemoveSpecialChars(objLogin.MotherName),
                    @MotherQualification = CommonSetting.RemoveSpecialChars(objLogin.MotherQualification),
                    @MotherOccupation = CommonSetting.RemoveSpecialChars(objLogin.MotherOccupation),
                    @MotherEmail = CommonSetting.RemoveSpecialCharsemail(objLogin.MotherEmail),
                    @MotherMobile = CommonSetting.RemoveSpecialChars(objLogin.MotherMobile),
                    @session = objLogin.session,
                    @AdmisitionCategory = objLogin.AdmisitionCategory,
                    @CourseType = objLogin.CourseType,
                    @EducationType = objLogin.EducationType,
                    @CourseCategory = objLogin.CourseCategory,
                    @IsQualifying = 0,
                    @IsLogin = 0,
                    @Action = "UpdateData",
                    @ApplicationNo = 0,
                    @stphoto = objLogin.stphoto,
                    @stsign = objLogin.stsign,
                    @title = objLogin.title,
                    @ftile = objLogin.Ftitle,
                    @Nationality = objLogin.Nationality,
                    @Religion = objLogin.Religion,
                    @MotherTongue = objLogin.MotherTongue,
                    @ishandicapped = objLogin.ishandicapped,
                    @isex_service_man = objLogin.isex_service_man,
                    @is_ncc_candidate = objLogin.is_ncc_candidate,
                    @aadharno = CommonSetting.RemoveSpecialChars(objLogin.aadharno),
                    @FirstNameInHindi = CommonSetting.RemoveSpecialChars(objLogin.FirstNameInHindi),
                    @MiddleNameInHindi = objLogin.MiddleNameInHindi,
                    @LastNameInHindi = objLogin.LastNameInHindi,
                    @ReligonOther = CommonSetting.RemoveSpecialChars(objLogin.ReligonOther),
                    @FatherNameInHindi = objLogin.FatherNameInHindi,
                    @MotherNameInHindi = objLogin.MotherNameInHindi,
                    @IsSports = objLogin.IsSports,
                    @IsStaff = objLogin.IsStaff,
                    @is_GEW = objLogin.is_GEW,
                    @previous_qua_id = objLogin.previous_qua_id,
                    @is_permanentaddress = objLogin.is_permanentaddress,
                    @RegistrationNo = objLogin.RegistrationNo
                }, commandType: CommandType.StoredProcedure).FirstOrDefault();

                return obj;


            }
        }
        public Login sp_studentregistration_changecourse(Login objLogin)
        {
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var obj = conn.Query<Login>("[sp_studentregistration_changecourse]", new
                {
                    @Id = objLogin.Id,
                    @session = objLogin.session,
                    @AdmisitionCategory = objLogin.AdmisitionCategory,
                    @EducationType = objLogin.EducationType,
                    @CourseCategory = objLogin.CourseCategory,
                    @Action = "Update",
                    @previous_qua_id = objLogin.previous_qua_id,
                    @prevoiusboardid = objLogin.prevoiusboardid
                }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return obj;
            }

        }
    }


    public class Login
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "LoginId is ApplicationNo")]
        public string ApplicationNo { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
       
        public bool islogout { get; set; }
        public string FirstNameInHindi { get; set; }
        public string MiddleNameInHindi { get; set; }
        public string LastNameInHindi { get; set; }
        public string captcha { get; set; }

        public int Gender { get; set; }
        public string DOB { get; set; }
        public int CastCategory { get; set; }
        public int BloodGroup { get; set; }

        public string MobileNo { get; set; }
        public string Email { get; set; }
        public string CurrentAddress { get; set; }
        public string CA_PinCode { get; set; }
        public int CA_Country { get; set; }
        public int CA_State { get; set; }
        public string CA_City { get; set; }
        public string PA_Address { get; set; }
        public string PA_PinCode { get; set; }
        public int PA_Country { get; set; }
        public int PA_State { get; set; }
        public string PA_City { get; set; }
        public string FatherName { get; set; }

        public string FatherNameInHindi { get; set; }
        public string FatherQualification { get; set; }
        public string FatherOccupation { get; set; }
        public string FatherMobile { get; set; }
        public string FatherEmail { get; set; }
        public string MotherName { get; set; }
        public string MotherNameInHindi { get; set; }

        public string MotherQualification { get; set; }
        public string MotherOccupation { get; set; }
        public string MotherEmail { get; set; }
        public string MotherMobile { get; set; }
        public string GargianName { get; set; }
        public string GargianContactNo { get; set; }
        public string GargianRelation { get; set; }
        public int session { get; set; }
        public int AdmisitionCategory { get; set; }
        public int CourseType { get; set; }
        public int EducationType { get; set; }
        public int CourseCategory { get; set; }
        public int IsLogin { get; set; }
        public int IsFeeSubmit { get; set; }
        public int IsFeeSubmit_spot { get; set; }
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
        public bool status { get; set; }
        public string Message { get; set; }
        public bool rememberMe { get; set; }
        public string stphoto { get; set; }
        public string stsign { get; set; }
        public int Ftitle { get; set; }
        public int title { get; set; }

        public int Nationality { get; set; }
        public int Religion { get; set; }
        public int MotherTongue { get; set; }
        public bool is_ncc_candidate { get; set; }
        public bool ishandicapped { get; set; }
        public bool isex_service_man { get; set; }
        public string aadharno { get; set; }
        public int prevoiustreamid { get; set; }
        public int previous_qua_id { get; set; }
        public bool issame_stream { get; set; }


        public string ReligonOther { get; set; }
        public bool IsSports { get; set; }
        public bool IsStaff { get; set; }
        public byte[] hashedBytes { get; set; }
        public int prevoiusboardid { get; set; }
        public int loginRedriect { get; set; }
        public bool is_GEW { get; set; }
        public bool is_permanentaddress { get; set; }
        public string RegistrationNo { get; set; }
        public int StudentYear { get; set; }
        public string courseyear { get; set; }
        public int incomecertificate_iseligible { get; set; }
        public string incomecertificate { get; set; }
        public bool IsExamFee { get; set; }
        public string incomeRejectReaseon { get; set; }
        public int Ismannualadmission { get; set; }
        public string rollno { get; set; }
        public int CollegeID { get; set; }
        public int CheckFailOrPass { get; set; }
        public string disabilityType { get; set; }
        public string disabilityPercent { get; set; }
    }


    public class ChangePassword
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
        public int CollegeID { get; set; }
        public byte[] Current_password { get; set; }
        public byte[] newU_password { get; set; }
    }

    public class FeesSubmit
    {
        public int Id { get; set; }
        public string ApplicationNo { get; set; }
        public string Title { get; set; }
        public string Session { get; set; }
        public string Message { get; set; }
        public string CastCategort { get; set; }
        public string Fees { get; set; }
        public string Course { get; set; }
        public string IsFeeSubmit { get; set; }
        public int IsFeeSubmit_spot { get; set; }
        public string FeeStatus { get; set; }
        public bool Status { get; set; }
        public string Requestdata { get; set; }
        public string dRequestdata { get; set; }
        public string requesttime { get; set; }
        public string PGstatus { get; set; }
        public string banktrxid { get; set; }
        public string clienttrxid { get; set; }
        public string amount { get; set; }
        public string feeamount { get; set; }
        public string gst { get; set; }
        public string commission { get; set; }
        public string paymode { get; set; }
        public string banktxndate { get; set; }
        public string Reason { get; set; }
        public string disabilityType { get; set; }
        public string disabilityPercent { get; set; }

        public string apitxnid { get; set; }
        public string SessionName { get; set; }
        public bool IsExamFee { get; set; }
        public bool IsRegistrationFee { get; set; }
        public string Other_Details { get; set; }
        public string GetWayType { get; set; }
        public int sessionid { get; set; }
        public string MID { get; set; }
        
        public int migrationcertificate_iseligible { get; set; }
        public FeesSubmit Feessub()
        {
            FeesSubmit ObjFees = new FeesSubmit();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                int StID = Convert.ToInt32(ClsLanguage.GetCookies("NBStID"));
                if (StID == 0)
                {
                    ObjFees.Status = false;
                    ObjFees.Message = "Network Error !!";
                    return ObjFees;
                }
                int Sission = Convert.ToInt32(ClsLanguage.GetCookies("NBSission"));
                CommonMethod cmn = new CommonMethod();
                string ip = CommonMethod.GetIPAddress();
                ObjFees = conn.Query<FeesSubmit>("[sp_StudentRegistration]", new { @Action = "StudentFeesDetail", @Id = StID.ToString(), @session = Sission }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                if (ObjFees == null)
                {
                    FeesSubmit ObjFees11 = new FeesSubmit();
                    ObjFees11.Status = false;
                    return ObjFees11;
                }
                if (ObjFees.Id <= 0)
                {
                    ObjFees.Status = false;

                }
                return ObjFees;
            }
        }
        public FeesSubmit Feessub_spot()
        {
            FeesSubmit ObjFees = new FeesSubmit();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                int StID = Convert.ToInt32(ClsLanguage.GetCookies("NBStID"));
                if (StID == 0)
                {
                    ObjFees.Status = false;
                    ObjFees.Message = "Network Error !!";
                    return ObjFees;
                }
                int Sission = Convert.ToInt32(ClsLanguage.GetCookies("NBSission"));
                CommonMethod cmn = new CommonMethod();
                string ip = CommonMethod.GetIPAddress();
                ObjFees = conn.Query<FeesSubmit>("[sp_StudentRegistration]", new { @Action = "StudentFeesDetail_spot", @Id = StID.ToString(), @session = Sission }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                if (ObjFees == null)
                {
                    FeesSubmit ObjFees11 = new FeesSubmit();
                    ObjFees11.Status = false;
                    return ObjFees11;
                }
                if (ObjFees.Id <= 0)
                {
                    ObjFees.Status = false;

                }
                return ObjFees;
            }
        }

        public List<int> Showgeography()
        {
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var obj = conn.Query<int>("select sid as id From tbl_temp_geography").ToList();
                return obj;
            }
        }
        public FeesSubmit FeessubEncryt(int sid)
        {
            FeesSubmit ObjFees = new FeesSubmit();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                int StID = sid;
                if (StID == 0)
                {
                    ObjFees.Status = false;
                    ObjFees.Message = "Network Error !!";
                    return ObjFees;
                }
                int Sission = Convert.ToInt32(ClsLanguage.GetCookies("NBSission"));
                CommonMethod cmn = new CommonMethod();
                string ip = CommonMethod.GetIPAddress();
                ObjFees = conn.Query<FeesSubmit>("[sp_StudentRegistration]", new { @Action = "StudentFeesDetail", @Id = StID.ToString(), @session = Sission }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                if (ObjFees.Id <= 0)
                {
                    ObjFees.Status = false;
                }
                return ObjFees;
            }
        }
        public FeesSubmit FeessubEncrytPush(int sid, int sessionid)
        {
            FeesSubmit ObjFees = new FeesSubmit();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                int StID = sid;
                if (StID == 0)
                {
                    ObjFees.Status = false;
                    ObjFees.Message = "Network Error !!";
                    return ObjFees;
                }
                int Sission = sessionid;
                CommonMethod cmn = new CommonMethod();
                string ip = CommonMethod.GetIPAddress();
                ObjFees = conn.Query<FeesSubmit>("[sp_StudentRegistration]", new { @Action = "StudentFeesDetail", @Id = StID.ToString(), @session = Sission }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                if (ObjFees.Id <= 0)
                {
                    ObjFees.Status = false;
                }
                return ObjFees;
            }
        }
        public FeesSubmit FeessubStudent(FeesSubmit obj123)
        {
            FeesSubmit ObjFees = new FeesSubmit();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                //int Sission = Convert.ToInt32(ClsLanguage.GetCookies("NBSission"));
                CommonMethod cmn = new CommonMethod();
                //ObjFees = conn.Query<FeesSubmit>("[sp_student_registrationfee_spot]", new
                ObjFees = conn.Query<FeesSubmit>("[sp_student_registrationfee]", new
                {
                    @applicationno = obj123.ApplicationNo,
                    @Requestdata = obj123.Requestdata,
                    @dRequestdata = obj123.dRequestdata,
                    @status = obj123.PGstatus,
                    @banktrxid = obj123.banktrxid,
                    @clienttrxid = obj123.clienttrxid,
                    @amount = obj123.amount,
                    @feeamount = (obj123.feeamount == "" ? "0" : obj123.feeamount),
                    @gst = (obj123.gst == "" ? "0" : obj123.gst),
                    @commission = (obj123.commission == "" ? "0" : obj123.commission),
                    @paymode = obj123.paymode,
                    @Reason = obj123.Reason,
                    @banktxndate = obj123.banktxndate,
                    @apitxnid = obj123.apitxnid,
                    @responsevia = "browser"

                }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                if (ObjFees.Id <= 0)
                {
                    ObjFees.Status = false;

                }
                return ObjFees;
            }
        }
        public FeesSubmit FeessubStudent_spot(FeesSubmit obj123)
        {
            FeesSubmit ObjFees = new FeesSubmit();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                int Sission = Convert.ToInt32(ClsLanguage.GetCookies("NBSission"));
                CommonMethod cmn = new CommonMethod();
                ObjFees = conn.Query<FeesSubmit>("[sp_student_registrationfee_spot]", new

                {
                    @applicationno = obj123.ApplicationNo,
                    @Requestdata = obj123.Requestdata,
                    @dRequestdata = obj123.dRequestdata,
                    @status = obj123.PGstatus,
                    @banktrxid = obj123.banktrxid,
                    @clienttrxid = obj123.clienttrxid,
                    @amount = obj123.amount,
                    @feeamount = (obj123.feeamount == "" ? "0" : obj123.feeamount),
                    @gst = (obj123.gst == "" ? "0" : obj123.gst),
                    @commission = (obj123.commission == "" ? "0" : obj123.commission),
                    @paymode = obj123.paymode,
                    @Reason = obj123.Reason,
                    @banktxndate = obj123.banktxndate,
                    @apitxnid = obj123.apitxnid,
                    @responsevia = "browser"

                }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                if (ObjFees.Id <= 0)
                {
                    ObjFees.Status = false;

                }
                return ObjFees;
            }
        }
        public FeesSubmit FeessubEncryt_spot(int sid)
        {
            FeesSubmit ObjFees = new FeesSubmit();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                int StID = sid;
                if (StID == 0)
                {
                    ObjFees.Status = false;
                    ObjFees.Message = "Network Error !!";
                    return ObjFees;
                }
                int Sission = Convert.ToInt32(ClsLanguage.GetCookies("NBSission"));
                CommonMethod cmn = new CommonMethod();
                string ip = CommonMethod.GetIPAddress();
                ObjFees = conn.Query<FeesSubmit>("[sp_StudentRegistration]", new { @Action = "StudentFeesDetail_spot", @Id = StID.ToString(), @session = Sission }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                if (ObjFees.Id <= 0)
                {
                    ObjFees.Status = false;
                }
                return ObjFees;
            }
        }


        public FeesSubmit FeessubStudentbefore(FeesSubmit obj123)
        {
            FeesSubmit ObjFees = new FeesSubmit();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                int Sission = Convert.ToInt32(ClsLanguage.GetCookies("NBSission"));
                CommonMethod cmn = new CommonMethod();
                ///  ObjFees = conn.Query<FeesSubmit>("[sp_student_registrationfee_before_spot]", new
                ObjFees = conn.Query<FeesSubmit>("[sp_student_registrationfee_before]", new
                {
                    @applicationno = obj123.ApplicationNo,
                    @Requestdata = obj123.Requestdata,
                    @dRequestdata = obj123.dRequestdata,
                    @status = obj123.PGstatus,
                    @banktrxid = obj123.banktrxid,
                    @clienttrxid = obj123.clienttrxid,
                    @amount = obj123.amount,
                    @feeamount = (obj123.feeamount == "" ? "0" : obj123.feeamount),
                    @gst = (obj123.gst == "" ? "0" : obj123.gst),
                    @commission = (obj123.commission == "" ? "0" : obj123.commission),
                    @paymode = obj123.paymode,
                    @Reason = obj123.Reason,
                    @banktxndate = obj123.banktxndate,
                    @apitxnid = obj123.apitxnid,
                    @responsevia = "browser",
                    @GetWayType = obj123.GetWayType

                }, commandType: CommandType.StoredProcedure).FirstOrDefault();

                ObjFees.Status = ObjFees.Status;


                return ObjFees;
            }
        }
        public FeesSubmit FeessubStudentbefore_spot(FeesSubmit obj123)
        {
            FeesSubmit ObjFees = new FeesSubmit();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                int Sission = Convert.ToInt32(ClsLanguage.GetCookies("NBSission"));
                CommonMethod cmn = new CommonMethod();
                ObjFees = conn.Query<FeesSubmit>("[sp_student_registrationfee_before_spot]", new

                {
                    @applicationno = obj123.ApplicationNo,
                    @Requestdata = obj123.Requestdata,
                    @dRequestdata = obj123.dRequestdata,
                    @status = obj123.PGstatus,
                    @banktrxid = obj123.banktrxid,
                    @clienttrxid = obj123.clienttrxid,
                    @amount = obj123.amount,
                    @feeamount = (obj123.feeamount == "" ? "0" : obj123.feeamount),
                    @gst = (obj123.gst == "" ? "0" : obj123.gst),
                    @commission = (obj123.commission == "" ? "0" : obj123.commission),
                    @paymode = obj123.paymode,
                    @Reason = obj123.Reason,
                    @banktxndate = obj123.banktxndate,
                    @apitxnid = obj123.apitxnid,
                    @responsevia = "browser"

                }, commandType: CommandType.StoredProcedure).FirstOrDefault();

                ObjFees.Status = ObjFees.Status;


                return ObjFees;
            }
        }
        public FeesSubmit FeessubStudentPushresponse(FeesSubmit obj123)
        {
            FeesSubmit ObjFees = new FeesSubmit();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                // ObjFees = conn.Query<FeesSubmit>("[sp_student_registrationfee_spot]", new
                ObjFees = conn.Query<FeesSubmit>("[sp_student_registrationfee]", new
                {
                    @applicationno = obj123.ApplicationNo,
                    @Requestdata = obj123.Requestdata,
                    @dRequestdata = obj123.dRequestdata,
                    @status = obj123.PGstatus,
                    @banktrxid = obj123.banktrxid,
                    @clienttrxid = obj123.clienttrxid,
                    @amount = obj123.amount,
                    @feeamount = (obj123.feeamount == "" ? "0" : obj123.feeamount),
                    @gst = (obj123.gst == "" ? "0" : obj123.gst),
                    @commission = (obj123.commission == "" ? "0" : obj123.commission),
                    @paymode = obj123.paymode,
                    @Reason = obj123.Reason,
                    @banktxndate = obj123.banktxndate,
                    @apitxnid = obj123.apitxnid,
                    @responsevia = "Pushresponse"

                }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                if (ObjFees.Id <= 0)
                {
                    ObjFees.Status = false;

                }
                return ObjFees;
            }
        }
        public FeesSubmit FeessubStudentPushresponse_spot(FeesSubmit obj123)
        {
            FeesSubmit ObjFees = new FeesSubmit();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                // ObjFees = conn.Query<FeesSubmit>("[sp_student_registrationfee_spot]", new
                ObjFees = conn.Query<FeesSubmit>("[sp_student_registrationfee_spot]", new
                {
                    @applicationno = obj123.ApplicationNo,
                    @Requestdata = obj123.Requestdata,
                    @dRequestdata = obj123.dRequestdata,
                    @status = obj123.PGstatus,
                    @banktrxid = obj123.banktrxid,
                    @clienttrxid = obj123.clienttrxid,
                    @amount = obj123.amount,
                    @feeamount = (obj123.feeamount == "" ? "0" : obj123.feeamount),
                    @gst = (obj123.gst == "" ? "0" : obj123.gst),
                    @commission = (obj123.commission == "" ? "0" : obj123.commission),
                    @paymode = obj123.paymode,
                    @Reason = obj123.Reason,
                    @banktxndate = obj123.banktxndate,
                    @apitxnid = obj123.apitxnid,
                    @responsevia = "Pushresponse"

                }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                if (ObjFees.Id <= 0)
                {
                    ObjFees.Status = false;

                }
                return ObjFees;
            }
        }

        public FeesSubmit FeessubStudent_doubleverification(FeesSubmit obj123)
        {
            FeesSubmit ObjFees = new FeesSubmit();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                // ObjFees = conn.Query<FeesSubmit>("[sp_student_registrationfee_spot]", new
                ObjFees = conn.Query<FeesSubmit>("[sp_student_registrationfee]", new
                {
                    @applicationno = obj123.ApplicationNo,
                    @Requestdata = obj123.Requestdata,
                    @dRequestdata = obj123.dRequestdata,
                    @status = obj123.PGstatus,
                    @banktrxid = obj123.banktrxid,
                    @clienttrxid = obj123.clienttrxid,
                    @amount = obj123.amount,
                    @feeamount = (obj123.feeamount == "" ? "0" : obj123.feeamount),
                    @gst = (obj123.gst == "" ? "0" : obj123.gst),
                    @commission = (obj123.commission == "" ? "0" : obj123.commission),
                    @paymode = obj123.paymode,
                    @Reason = obj123.Reason,
                    @banktxndate = obj123.banktxndate,
                    @apitxnid = obj123.apitxnid,
                    @responsevia = "DoubleVerification"

                }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                if (ObjFees.Id <= 0)
                {
                    ObjFees.Status = false;

                }
                return ObjFees;
            }
        }
        public FeesSubmit FeessubStudent_doubleverification_spot(FeesSubmit obj123)
        {
            FeesSubmit ObjFees = new FeesSubmit();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                // ObjFees = conn.Query<FeesSubmit>("[sp_student_registrationfee_spot]", new
                ObjFees = conn.Query<FeesSubmit>("[sp_student_registrationfee_spot]", new
                {
                    @applicationno = obj123.ApplicationNo,
                    @Requestdata = obj123.Requestdata,
                    @dRequestdata = obj123.dRequestdata,
                    @status = obj123.PGstatus,
                    @banktrxid = obj123.banktrxid,
                    @clienttrxid = obj123.clienttrxid,
                    @amount = obj123.amount,
                    @feeamount = (obj123.feeamount == "" ? "0" : obj123.feeamount),
                    @gst = (obj123.gst == "" ? "0" : obj123.gst),
                    @commission = (obj123.commission == "" ? "0" : obj123.commission),
                    @paymode = obj123.paymode,
                    @Reason = obj123.Reason,
                    @banktxndate = obj123.banktxndate,
                    @apitxnid = obj123.apitxnid,
                    @responsevia = "DoubleVerification"

                }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                if (ObjFees.Id <= 0)
                {
                    ObjFees.Status = false;

                }
                return ObjFees;
            }
        }

        public FeesSubmit FeessubStudenttest()
        {
            FeesSubmit ObjFees = new FeesSubmit();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                int StID = Convert.ToInt32(ClsLanguage.GetCookies("NBStID"));
                string ApplicationNo = ClsLanguage.GetCookies("NBApplicationNo");
                if (StID == 0)
                {
                    ObjFees.Status = false;
                    ObjFees.Message = "Network Error !!";
                    return ObjFees;
                }
                int Sission = Convert.ToInt32(ClsLanguage.GetCookies("NBSission"));
                CommonMethod cmn = new CommonMethod();
                ObjFees = conn.Query<FeesSubmit>("[sp_StudentRegistration]", new { @Action = "FeesSubmit", @ApplicID = ApplicationNo.ToString(), @Id = StID }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                if (ObjFees.Id <= 0)
                {
                    ObjFees.Status = false;

                }
                return ObjFees;
            }
        }
        public FeesSubmit FeessubEncrytEnrollment(int sid)
        {
            FeesSubmit ObjFees = new FeesSubmit();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                int StID = sid;
                if (StID == 0)
                {
                    ObjFees.Status = false;
                    ObjFees.Message = "Network Error !!";
                    return ObjFees;
                }
                int Sission = Convert.ToInt32(ClsLanguage.GetCookies("NBSission"));
                CommonMethod cmn = new CommonMethod();
                string ip = CommonMethod.GetIPAddress();
                ObjFees = conn.Query<FeesSubmit>("[sp_EnrollmentRequest]", new { @Action = "StudentFeesDetailpaymentgatway", @SID = StID.ToString(), @session = Sission }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                if (ObjFees == null)
                {
                    ObjFees = new FeesSubmit();
                    ObjFees.Status = false;
                }
                else
                {
                    if (ObjFees.Id <= 0)
                    {
                        ObjFees.Status = false;
                    }
                }

                return ObjFees;
            }
        }
        public FeesSubmit FeessubStudenttestForExam()
        {
            FeesSubmit ObjFees = new FeesSubmit();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                int StID = (ClsLanguage.GetCookies("NBStID") != null ? Convert.ToInt32(ClsLanguage.GetCookies("NBStID")) : 0);
                int Sission = (ClsLanguage.GetCookies("NBSission") != null ? Convert.ToInt32(ClsLanguage.GetCookies("NBSission")) : 0);
                if (StID == 0)
                {
                    ObjFees.Status = false;
                    ObjFees.Message = "Network Error !!";
                    return ObjFees;
                }

                CommonMethod cmn = new CommonMethod();
                ObjFees = conn.Query<FeesSubmit>("[sp_student_ExamFrom]", new { @Action = "FeesSubmit", @SID = StID }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                if (ObjFees.Id <= 0)
                {
                    ObjFees.Status = false;

                }
                return ObjFees;
            }
        }

        public FeesSubmit EnrollmentFeesDetail()
        {
            FeesSubmit ObjFees = new FeesSubmit();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {

                int StID = (ClsLanguage.GetCookies("NBStID") != null ? Convert.ToInt32(ClsLanguage.GetCookies("NBStID")) : 0);
                int Sission = (ClsLanguage.GetCookies("NBSission") != null ? Convert.ToInt32(ClsLanguage.GetCookies("NBSission")) : 0);

                if (StID == 0)
                {
                    ObjFees.Status = false;
                    ObjFees.Message = "Network Error !!";
                    return ObjFees;
                }
                CommonMethod cmn = new CommonMethod();
                string ip = CommonMethod.GetIPAddress();
                ObjFees = conn.Query<FeesSubmit>("[sp_EnrollmentRequest]", new { @Action = "StudentFeesDetail", @Sid = StID, @session = Sission }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                if (ObjFees.Id <= 0)
                {
                    ObjFees.Status = false;

                }
                return ObjFees;
            }
        }
        public FeesSubmit EnrollmentFeesDetailpaymentgatewate()
        {
            FeesSubmit ObjFees = new FeesSubmit();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {

                int StID = (ClsLanguage.GetCookies("NBStID") != null ? Convert.ToInt32(ClsLanguage.GetCookies("NBStID")) : 0);
                int Sission = (ClsLanguage.GetCookies("NBSission") != null ? Convert.ToInt32(ClsLanguage.GetCookies("NBSission")) : 0);

                if (StID == 0)
                {
                    ObjFees.Status = false;
                    ObjFees.Message = "Network Error !!";
                    return ObjFees;
                }
                CommonMethod cmn = new CommonMethod();
                string ip = CommonMethod.GetIPAddress();
                ObjFees = conn.Query<FeesSubmit>("[sp_EnrollmentRequest]", new { @Action = "StudentFeesDetailpaymentgatway", @Sid = StID, @session = Sission }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                if (ObjFees.Id <= 0)
                {
                    ObjFees.Status = false;

                }
                return ObjFees;
            }
        }
        public FeesSubmit FeessubEnrollment(FeesSubmit obj123)
        {
            FeesSubmit ObjFees = new FeesSubmit();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                //int Sission = Convert.ToInt32(ClsLanguage.GetCookies("NBSission"));
                CommonMethod cmn = new CommonMethod();
                ObjFees = conn.Query<FeesSubmit>("[sp_student_Enrollmentfee]", new
                {
                    @applicationno = obj123.ApplicationNo,
                    @Requestdata = obj123.Requestdata,
                    @dRequestdata = obj123.dRequestdata,
                    @status = obj123.PGstatus,
                    @banktrxid = obj123.banktrxid,
                    @clienttrxid = obj123.clienttrxid,
                    @amount = obj123.amount,
                    @feeamount = (obj123.feeamount == "" ? "0" : obj123.feeamount),
                    @gst = (obj123.gst == "" ? "0" : obj123.gst),
                    @commission = (obj123.commission == "" ? "0" : obj123.commission),
                    @paymode = obj123.paymode,
                    @Reason = obj123.Reason,
                    @banktxndate = obj123.banktxndate,
                    @apitxnid = obj123.apitxnid,
                    @responsevia = "browser"

                }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                if (ObjFees.Id <= 0)
                {
                    ObjFees.Status = false;

                }
                return ObjFees;
            }
        }

        public FeesSubmit FeessubEnrollmentbefore(FeesSubmit obj123)
        {
            FeesSubmit ObjFees = new FeesSubmit();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                int Sission = Convert.ToInt32(ClsLanguage.GetCookies("NBSission"));
                CommonMethod cmn = new CommonMethod();
                ObjFees = conn.Query<FeesSubmit>("[sp_student_Enrollmentfee_before]", new
                {
                    @applicationno = obj123.ApplicationNo,
                    @Requestdata = obj123.Requestdata,
                    @dRequestdata = obj123.dRequestdata,
                    @status = obj123.PGstatus,
                    @banktrxid = obj123.banktrxid,
                    @clienttrxid = obj123.clienttrxid,
                    @amount = obj123.amount,
                    @feeamount = (obj123.feeamount == "" ? "0" : obj123.feeamount),
                    @gst = (obj123.gst == "" ? "0" : obj123.gst),
                    @commission = (obj123.commission == "" ? "0" : obj123.commission),
                    @paymode = obj123.paymode,
                    @Reason = obj123.Reason,
                    @banktxndate = obj123.banktxndate,
                    @apitxnid = obj123.apitxnid,
                    @responsevia = "browser",
                    @Other_Details = obj123.Other_Details,
                    @GetWayType= obj123.GetWayType,
                    @MID = obj123.MID,
                }, commandType: CommandType.StoredProcedure).FirstOrDefault();

                ObjFees.Status = ObjFees.Status;


                return ObjFees;
            }
        }

        public FeesSubmit FeessubEnrollmentPushresponse(FeesSubmit obj123)
        {
            FeesSubmit ObjFees = new FeesSubmit();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                ObjFees = conn.Query<FeesSubmit>("[sp_student_Enrollmentfee]", new
                {
                    @applicationno = obj123.ApplicationNo,
                    @Requestdata = obj123.Requestdata,
                    @dRequestdata = obj123.dRequestdata,
                    @status = obj123.PGstatus,
                    @banktrxid = obj123.banktrxid,
                    @clienttrxid = obj123.clienttrxid,
                    @amount = obj123.amount,
                    @feeamount = (obj123.feeamount == "" ? "0" : obj123.feeamount),
                    @gst = (obj123.gst == "" ? "0" : obj123.gst),
                    @commission = (obj123.commission == "" ? "0" : obj123.commission),
                    @paymode = obj123.paymode,
                    @Reason = obj123.Reason,
                    @banktxndate = obj123.banktxndate,
                    @apitxnid = obj123.apitxnid,
                    @responsevia = "Pushresponse"

                }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                if (ObjFees.Id <= 0)
                {
                    ObjFees.Status = false;

                }
                return ObjFees;
            }
        }

        public FeesSubmit FeessubStudentPushresponseDoubleverificationEnroll(FeesSubmit obj123)
        {
            FeesSubmit ObjFees = new FeesSubmit();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                ObjFees = conn.Query<FeesSubmit>("[sp_student_Enrollmentfee]", new
                {
                    @applicationno = obj123.ApplicationNo,
                    @Requestdata = obj123.Requestdata,
                    @dRequestdata = obj123.dRequestdata,
                    @status = obj123.PGstatus,
                    @banktrxid = obj123.banktrxid,
                    @clienttrxid = obj123.clienttrxid,
                    @amount = obj123.amount,
                    @feeamount = (obj123.feeamount == "" ? "0" : obj123.feeamount),
                    @gst = (obj123.gst == "" ? "0" : obj123.gst),
                    @commission = (obj123.commission == "" ? "0" : obj123.commission),
                    @paymode = obj123.paymode,
                    @Reason = obj123.Reason,
                    @banktxndate = obj123.banktxndate,
                    @apitxnid = obj123.apitxnid,
                    @responsevia = "DoubleVerification"

                }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                if (ObjFees.Id <= 0)
                {
                    ObjFees.Status = false;

                }
                return ObjFees;
            }
        }


    }

    public class ElegibilityCreteria
    {
        public int ID { get; set; }
        public int EducationType { get; set; }
        public int CourseCategoryID { get; set; }
        public int QualificationTypeID { get; set; }
        public decimal Percentage { get; set; }
        public DateTime createdate { get; set; }
        public string IPAddress { get; set; }
        public int insertBy { get; set; }
        public bool Status { get; set; }
        public string Msg { get; set; }

        public ElegibilityCreteria getdetail(string app = "", int qual = 0)
        {

            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                CommonMethod cmn = new CommonMethod();

                var obj = conn.Query<ElegibilityCreteria>("[sp_StudentRegistration]", new { @Action = "checkpercentage", @ApplicID = app, @qual = qual }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return obj;
            }
        }
        public ElegibilityCreteria getdetailofper(string app = "")
        {

            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                CommonMethod cmn = new CommonMethod();

                var obj = conn.Query<ElegibilityCreteria>("[sp_StudentRegistration]", new { @Action = "percentage", @ApplicID = app }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return obj;
            }
        }
        public List<ElegibilityCreteria> getdetailofper1(string app = "")
        {

            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                CommonMethod cmn = new CommonMethod();

                var obj = conn.Query<ElegibilityCreteria>("[sp_StudentRegistration]", new { @Action = "percentage", @ApplicID = app }, commandType: CommandType.StoredProcedure).ToList();
                return obj;
            }
        }





    }
    public class OtherSubjectModel
    {
        public int SubjectID { get; set; }
        public string SubjectName { get; set; }
        public string HeadingName { get; set; }
        public string SubjectTypeID { get; set; }
    }

    public class StudentSubjectVM
    {
        public int SubjectID { get; set; }
        public int SubjectTypeID { get; set; }
    }

    public class AdmissionFeesSubmit
    {
        public int Id { get; set; }
        public string ApplicationNo { get; set; }
        public string Title { get; set; }

        public int is_affidavitapply { get; set; }
        public int is_affiliated { get; set; }
        public string Session { get; set; }
        public string Message { get; set; }
        public string CastCategort { get; set; }
        public string Fees { get; set; }
        public string Course { get; set; }
        public string IsFeeSubmit { get; set; }
        public string FeeStatus { get; set; }
        public string FeeStatus1 { get; set; }
        public bool Status { get; set; }
        public string CollegeName { get; set; }
        public string Stream { get; set; }
        public string PaymentType { get; set; }
        public string PaymentHolderName { get; set; }
        public string CardNumber { get; set; }
        public string Expires { get; set; }
        public string NuthNum { get; set; }
        public string mid { get; set; }
        public string mkey { get; set; }
        public string headname { get; set; }
        public decimal amount { get; set; }
        public int Collegeid { get; set; }
        public int coursecategoryid { get; set; }
        public int sessionid { get; set; }


        public string Requestdata { get; set; }
        public string dRequestdata { get; set; }
        public string requesttime { get; set; }
        public string PGstatus { get; set; }
        public string banktrxid { get; set; }
        public string clienttrxid { get; set; }
        public string feeamount { get; set; }
        public string gst { get; set; }
        public string commission { get; set; }
        public string paymode { get; set; }
        public string banktxndate { get; set; }
        public string Reason { get; set; }

        public string apitxnid { get; set; }

        public decimal surcharge { get; set; }
        public string email { get; set; }
        public string mobileno { get; set; }
        public string name { get; set; }
        public int CastCategory { get; set; }
        public int streamcategoryid { get; set; }
        public string yearname { get; set; }
        public bool IsAdmissionFee2 { get; set; }
        public string FeeStatus2 { get; set; }
        public int studentyear { get; set; }
        public int incomecertificate_iseligible { get; set; }
        public int IsApplied { get; set; }

        public int IsDocVerify { get; set; }
        public int tbl_recruitment_IsApplied { get; set; }

        public int IsSubjectChoice { get; set; }
        public int tbl_recruitment_IsSubjectChoice { get; set; }

        public int tbl_recruitment_IsDocVerify { get; set; }

        public string rejectreason { get; set; }
        public string rejectdate { get; set; }

        public string GetwayType { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }


        public string now { get; set; }
        //Airpay
        public string allParamValue1 { get; set; }
        public string sTemp { get; set; }
        public string str256Key { get; set; }
        public string allParamValue12 { get; set; }
        public string checksum1 { get; set; }
        public string checksum { get; set; }
        public string privatekey { get; set; }
        public string customvar { get; set; }
        public string Oid { get; set; }
        public string A_Success_url { get; set; }
        public string A_Mid { get; set; }
        public string allParamValue { get; set; }
        public string GetType { get; set; }

        public AdmissionFeesSubmit FeesDetails(int StID = 0)
        {
            AdmissionFeesSubmit ObjFees = new AdmissionFeesSubmit();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                // int StID = Convert.ToInt32(ClsLanguage.GetCookies("NBStID"));
                if (StID == 0)
                {
                    ObjFees.Status = false;
                    ObjFees.Message = "Network Error !!";
                    return ObjFees;
                }
                //int Sission = Convert.ToInt32(ClsLanguage.GetCookies("NBSission"));
                int session = new AcademicSession().GetAcademiccurrentSession().ID;
                CommonMethod cmn = new CommonMethod();
                string ip = CommonMethod.GetIPAddress();
                ObjFees = conn.Query<AdmissionFeesSubmit>("[sp_AdmissionFee]", new { @Action = "AdmissionFeesDetail", @SID = StID, @SessionID = session }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                if (ObjFees != null)
                {
                    if (ObjFees.Id <= 0)
                    {
                        ObjFees.Status = false;

                    }
                }
                return ObjFees;
            }
        }
        public AdmissionFeesSubmit FeesDetailsotheryear(int StID = 0)
        {
            AdmissionFeesSubmit ObjFees = new AdmissionFeesSubmit();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                // int StID = Convert.ToInt32(ClsLanguage.GetCookies("NBStID"));
                if (StID == 0)
                {
                    ObjFees.Status = false;
                    ObjFees.Message = "Network Error !!";
                    return ObjFees;
                }
                //int Sission = Convert.ToInt32(ClsLanguage.GetCookies("NBSission"));
                int session = new AcademicSession().GetAcademiccurrentSession().ID;
                CommonMethod cmn = new CommonMethod();
                string ip = CommonMethod.GetIPAddress();
                ObjFees = conn.Query<AdmissionFeesSubmit>("[sp_AdmissionFee_otheryear]", new { @Action = "AdmissionFeesDetail", @SID = StID, @SessionID = session }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                if (ObjFees != null)
                {
                    if (ObjFees.Id <= 0)
                    {
                        ObjFees.Status = false;

                    }
                }
                return ObjFees;
            }
        }
        //public AdmissionFeesSubmit FeesDetailsapplicationno(string applicationno = "")
        //{
        //    AdmissionFeesSubmit ObjFees = new AdmissionFeesSubmit();
        //    using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
        //    { 
        //        ObjFees = conn.Query<AdmissionFeesSubmit>("[sp_AdmissionFee]", new { @Action = "AdmissionFeesDetailbyapplicationno", @Applicationno = applicationno}, commandType: CommandType.StoredProcedure).FirstOrDefault();
        //        if (ObjFees.Id <= 0)
        //        {
        //            ObjFees.Status = false;

        //        }
        //        return ObjFees;
        //    }
        //}
        public AdmissionFeesSubmit getmidviasid(int StID = 0)
        {
            AdmissionFeesSubmit ObjFees = new AdmissionFeesSubmit();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                // int StID = Convert.ToInt32(ClsLanguage.GetCookies("NBStID"));
                if (StID == 0)
                {
                    ObjFees.Status = false;
                    ObjFees.Message = "Network Error !!";
                    return ObjFees;
                }
                //int Sission = Convert.ToInt32(ClsLanguage.GetCookies("NBSission"));
                int session = new AcademicSession().GetAcademiccurrentSession().ID;
                CommonMethod cmn = new CommonMethod();
                string ip = CommonMethod.GetIPAddress();
                ObjFees = conn.Query<AdmissionFeesSubmit>("[sp_AdmissionFee]", new { @Action = "getmidviasid", @SID = StID }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                if (ObjFees.Id <= 0)
                {
                    ObjFees.Status = false;

                }
                return ObjFees;
            }
        }
        public List<AdmissionFeesSubmit> FeesDetailsstructure(int collegeid, int courseCategoryid, int sessionid, int castecategory, int streamCategoryid, int courseyearid, int isconsession, int gender)
        {
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                CommonMethod cmn = new CommonMethod();
                string ip = CommonMethod.GetIPAddress();
                // var ObjFees = conn.Query<AdmissionFeesSubmit>("[sp_AdmissionFee]", new { @Action = "AdmissionFeesstructure", @collegeid = collegeid, @SessionID = sessionid, @courseCategoryid = courseCategoryid , @castecategory = 4, @streamCategoryid= streamCategoryid }, commandType: CommandType.StoredProcedure).ToList();
                var ObjFees = conn.Query<AdmissionFeesSubmit>("[sp_AdmissionFee]", new { @Action = "AdmissionFeesstructure", @collegeid = collegeid, @SessionID = sessionid, @courseCategoryid = courseCategoryid, @castecategory = castecategory, @streamCategoryid = streamCategoryid, @courseyearid = courseyearid, @isconsession = isconsession, @gender = gender }, commandType: CommandType.StoredProcedure).ToList();

                return ObjFees;
            }
        }
        public AdmissionFeesSubmit AdmissionFeessub(int StID = 0)
        {
            AdmissionFeesSubmit ObjFees = new AdmissionFeesSubmit();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                if (StID == 0)
                {
                    ObjFees.Status = false;
                    ObjFees.Message = "Network Error !!";
                    return ObjFees;
                }

                int session = new AcademicSession().GetAcademiccurrentSession().ID;
                var AddmissionCategoryid = Convert.ToInt32(CommonSetting.Commonid.RegularAdmissionType);
                CommonMethod cmn = new CommonMethod();
                ObjFees = conn.Query<AdmissionFeesSubmit>("[sp_AdmissionFee]", new { @Action = "AdmissionFeesSubmit", @SID = StID, @SessionID = session, @AddmissionCategoryid = AddmissionCategoryid }, commandType: CommandType.StoredProcedure).FirstOrDefault();

                return ObjFees;
            }
        }
        public AdmissionFeesSubmit FeessubStudentbeforeadmission(AdmissionFeesSubmit obj123)
        {
            AdmissionFeesSubmit ObjFees = new AdmissionFeesSubmit();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                int Sission = Convert.ToInt32(ClsLanguage.GetCookies("NBSission"));
                ObjFees = conn.Query<AdmissionFeesSubmit>("[sp_student_registrationfee_beforeadmission]", new
                {
                    @applicationno = obj123.ApplicationNo,
                    @Requestdata = obj123.Requestdata,
                    @dRequestdata = obj123.dRequestdata,
                    @status = obj123.PGstatus,
                    @banktrxid = obj123.banktrxid,
                    @clienttrxid = obj123.clienttrxid,
                    @amount = obj123.amount,
                    @feeamount = (obj123.feeamount == "" ? "0" : obj123.feeamount),
                    @gst = (obj123.gst == "" ? "0" : obj123.gst),
                    @commission = (obj123.commission == "" ? "0" : obj123.commission),
                    @paymode = obj123.paymode,
                    @Reason = obj123.Reason,
                    @banktxndate = obj123.banktxndate,
                    @apitxnid = obj123.apitxnid,
                    @responsevia = "browser",
                    @collegeid = obj123.Collegeid,
                    @Mid = obj123.mid,
                    @GetwayType = obj123.GetwayType

                }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                ObjFees.Status = ObjFees.Status;
                return ObjFees;
            }
        }
        public AdmissionFeesSubmit FeessubStudentbeforeadmissionFree(AdmissionFeesSubmit obj123)
        {
            AdmissionFeesSubmit ObjFees = new AdmissionFeesSubmit();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                int Sission = Convert.ToInt32(ClsLanguage.GetCookies("NBSission"));
                ObjFees = conn.Query<AdmissionFeesSubmit>("[sp_student_admissionfee_freeadmission]", new
                {
                    @applicationno = obj123.ApplicationNo,

                    @status = obj123.PGstatus,

                    @amount = obj123.amount,


                }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                ObjFees.Status = ObjFees.Status;
                return ObjFees;
            }
        }

        public AdmissionFeesSubmit GetDoubleVerificationStudent(AdmissionFeesSubmit obj123)
        {
            AdmissionFeesSubmit ObjFees = new AdmissionFeesSubmit();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                int Sission = Convert.ToInt32(ClsLanguage.GetCookies("NBSission"));
                ObjFees = conn.Query<AdmissionFeesSubmit>("[USP_doubleverificationforAdmission]", new
                {
                    @applicationno = obj123.ApplicationNo,
                    @GetWayType = obj123.GetwayType,
                    @CourseYear = obj123.coursecategoryid,
                }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                ObjFees.Status = ObjFees.Status;
                return ObjFees;
            }
        }
        public AdmissionFeesSubmit FeessubStudentadmission(AdmissionFeesSubmit obj123)
        {
            AdmissionFeesSubmit ObjFees = new AdmissionFeesSubmit();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                int Sission = Convert.ToInt32(ClsLanguage.GetCookies("NBSission"));
                CommonMethod cmn = new CommonMethod();
                ObjFees = conn.Query<AdmissionFeesSubmit>("[sp_student_admissionfee]", new
                {
                    @applicationno = obj123.ApplicationNo,
                    @Requestdata = obj123.Requestdata,
                    @dRequestdata = obj123.dRequestdata,
                    @status = obj123.PGstatus,
                    @banktrxid = obj123.banktrxid,
                    @clienttrxid = obj123.clienttrxid,
                    @amount = obj123.amount,
                    @feeamount = (obj123.feeamount == "" ? "0" : obj123.feeamount),
                    @gst = (obj123.gst == "" ? "0" : obj123.gst),
                    @commission = (obj123.commission == "" ? "0" : obj123.commission),
                    @paymode = obj123.paymode,
                    @Reason = obj123.Reason,
                    @banktxndate = obj123.banktxndate,
                    @apitxnid = obj123.apitxnid,
                    @responsevia = "browser",
                    @collegeid = obj123.Collegeid

                }, commandTimeout: 120545, commandType: CommandType.StoredProcedure).FirstOrDefault();
                if (ObjFees.Id <= 0)
                {
                    ObjFees.Status = false;
                }
                return ObjFees;
            }
        }

        public AdmissionFeesSubmit FeessubStudentadmissionOtherGetway(AdmissionFeesSubmit obj123)
        {
            AdmissionFeesSubmit ObjFees = new AdmissionFeesSubmit();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                int Sission = sessionid; // Convert.ToInt32(ClsLanguage.GetCookies("NBSission"));
                CommonMethod cmn = new CommonMethod();
                ObjFees = conn.Query<AdmissionFeesSubmit>("[sp_student_admissionfee]", new
                {
                    @applicationno = obj123.ApplicationNo,
                    @Requestdata = obj123.Requestdata,
                    @dRequestdata = obj123.dRequestdata,
                    @status = obj123.PGstatus,
                    @banktrxid = obj123.banktrxid,
                    @clienttrxid = obj123.clienttrxid,
                    @amount = obj123.amount,
                    @feeamount = (obj123.feeamount == "" ? "0" : obj123.feeamount),
                    @gst = (obj123.gst == "" ? "0" : obj123.gst),
                    @commission = (obj123.commission == "" ? "0" : obj123.commission),
                    @paymode = obj123.paymode,
                    @Reason = obj123.Reason,
                    @banktxndate = obj123.banktxndate,
                    @apitxnid = obj123.apitxnid,
                    @responsevia = "browser",
                    @collegeid = obj123.Collegeid

                }, commandTimeout: 120545, commandType: CommandType.StoredProcedure).FirstOrDefault();
                if (ObjFees.Id <= 0)
                {
                    ObjFees.Status = false;
                }
                return ObjFees;
            }
        }
        public AdmissionFeesSubmit FeessubStudentadmission_otheryear(AdmissionFeesSubmit obj123)
        {
            AdmissionFeesSubmit ObjFees = new AdmissionFeesSubmit();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                int Sission = Convert.ToInt32(ClsLanguage.GetCookies("NBSission"));
                CommonMethod cmn = new CommonMethod();
                ObjFees = conn.Query<AdmissionFeesSubmit>("[sp_student_admissionfee_otheryear]", new
                {
                    @applicationno = obj123.ApplicationNo,
                    @Requestdata = obj123.Requestdata,
                    @dRequestdata = obj123.dRequestdata,
                    @status = obj123.PGstatus,
                    @banktrxid = obj123.banktrxid,
                    @clienttrxid = obj123.clienttrxid,
                    @amount = obj123.amount,
                    @feeamount = (obj123.feeamount == "" ? "0" : obj123.feeamount),
                    @gst = (obj123.gst == "" ? "0" : obj123.gst),
                    @commission = (obj123.commission == "" ? "0" : obj123.commission),
                    @paymode = obj123.paymode,
                    @Reason = obj123.Reason,
                    @banktxndate = obj123.banktxndate,
                    @apitxnid = obj123.apitxnid,
                    @responsevia = "browser",
                    @collegeid = obj123.Collegeid

                }, commandTimeout: 120545, commandType: CommandType.StoredProcedure).FirstOrDefault();
                if (ObjFees.Id <= 0)
                {
                    ObjFees.Status = false;
                }
                return ObjFees;
            }
        }

        public AdmissionFeesSubmit FeessubStudentadmission_otheryear_OtherGetway(AdmissionFeesSubmit obj123)
        {
            AdmissionFeesSubmit ObjFees = new AdmissionFeesSubmit();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                int Sission = sessionid;// Convert.ToInt32(ClsLanguage.GetCookies("NBSission"));
                CommonMethod cmn = new CommonMethod();
                ObjFees = conn.Query<AdmissionFeesSubmit>("[sp_student_admissionfee_otheryear]", new
                {
                    @applicationno = obj123.ApplicationNo,
                    @Requestdata = obj123.Requestdata,
                    @dRequestdata = obj123.dRequestdata,
                    @status = obj123.PGstatus,
                    @banktrxid = obj123.banktrxid,
                    @clienttrxid = obj123.clienttrxid,
                    @amount = obj123.amount,
                    @feeamount = (obj123.feeamount == "" ? "0" : obj123.feeamount),
                    @gst = (obj123.gst == "" ? "0" : obj123.gst),
                    @commission = (obj123.commission == "" ? "0" : obj123.commission),
                    @paymode = obj123.paymode,
                    @Reason = obj123.Reason,
                    @banktxndate = obj123.banktxndate,
                    @apitxnid = obj123.apitxnid,
                    @responsevia = "browser",
                    @collegeid = obj123.Collegeid

                }, commandTimeout: 120545, commandType: CommandType.StoredProcedure).FirstOrDefault();
                if (ObjFees.Id <= 0)
                {
                    ObjFees.Status = false;
                }
                return ObjFees;
            }
        }
        public AdmissionFeesSubmit FeessubStudentadmissionPushresponse(AdmissionFeesSubmit obj123)
        {
            AdmissionFeesSubmit ObjFees = new AdmissionFeesSubmit();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                ObjFees = conn.Query<AdmissionFeesSubmit>("[sp_student_admissionfee]", new
                {
                    @applicationno = obj123.ApplicationNo,
                    @Requestdata = obj123.Requestdata,
                    @dRequestdata = obj123.dRequestdata,
                    @status = obj123.PGstatus,
                    @banktrxid = obj123.banktrxid,
                    @clienttrxid = obj123.clienttrxid,
                    @amount = obj123.amount,
                    @feeamount = (obj123.feeamount == "" ? "0" : obj123.feeamount),
                    @gst = (obj123.gst == "" ? "0" : obj123.gst),
                    @commission = (obj123.commission == "" ? "0" : obj123.commission),
                    @paymode = obj123.paymode,
                    @Reason = obj123.Reason,
                    @banktxndate = obj123.banktxndate,
                    @apitxnid = obj123.apitxnid,
                    @responsevia = "Pushresponse"

                }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                if (ObjFees.Id <= 0)
                {
                    ObjFees.Status = false;

                }
                return ObjFees;
            }
        }
        public AdmissionFeesSubmit FeessubStudentadmissionPushresponse_otheryear(AdmissionFeesSubmit obj123)
        {
            AdmissionFeesSubmit ObjFees = new AdmissionFeesSubmit();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                ObjFees = conn.Query<AdmissionFeesSubmit>("[sp_student_admissionfee_otheryear]", new
                {
                    @applicationno = obj123.ApplicationNo,
                    @Requestdata = obj123.Requestdata,
                    @dRequestdata = obj123.dRequestdata,
                    @status = obj123.PGstatus,
                    @banktrxid = obj123.banktrxid,
                    @clienttrxid = obj123.clienttrxid,
                    @amount = obj123.amount,
                    @feeamount = (obj123.feeamount == "" ? "0" : obj123.feeamount),
                    @gst = (obj123.gst == "" ? "0" : obj123.gst),
                    @commission = (obj123.commission == "" ? "0" : obj123.commission),
                    @paymode = obj123.paymode,
                    @Reason = obj123.Reason,
                    @banktxndate = obj123.banktxndate,
                    @apitxnid = obj123.apitxnid,
                    @responsevia = "Pushresponse"

                }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                if (ObjFees.Id <= 0)
                {
                    ObjFees.Status = false;

                }
                return ObjFees;
            }
        }
        public AdmissionFeesSubmit FeessubStudentadmissionDoubleverification(AdmissionFeesSubmit obj123)
        {
            AdmissionFeesSubmit ObjFees = new AdmissionFeesSubmit();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {

                ObjFees = conn.Query<AdmissionFeesSubmit>("[sp_student_admissionfee]", new
                {
                    @applicationno = obj123.ApplicationNo,
                    @Requestdata = obj123.Requestdata,
                    @dRequestdata = obj123.dRequestdata,
                    @status = obj123.PGstatus,
                    @banktrxid = obj123.banktrxid,
                    @clienttrxid = obj123.clienttrxid,
                    @amount = obj123.amount,
                    @feeamount = (obj123.feeamount == "" ? "0" : obj123.feeamount),
                    @gst = (obj123.gst == "" ? "0" : obj123.gst),
                    @commission = (obj123.commission == "" ? "0" : obj123.commission),
                    @paymode = obj123.paymode,
                    @Reason = obj123.Reason,
                    @banktxndate = obj123.banktxndate,
                    @apitxnid = obj123.apitxnid,
                    @responsevia = "DoubleVerificationURl"

                }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                if (ObjFees.Id <= 0)
                {
                    ObjFees.Status = false;

                }
                return ObjFees;
            }
        }
        public AdmissionFeesSubmit FeessubStudentadmissionDoubleverification_otheryear(AdmissionFeesSubmit obj123)
        {
            AdmissionFeesSubmit ObjFees = new AdmissionFeesSubmit();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {

                ObjFees = conn.Query<AdmissionFeesSubmit>("[sp_student_admissionfee_otheryear]", new
                {
                    @applicationno = obj123.ApplicationNo,
                    @Requestdata = obj123.Requestdata,
                    @dRequestdata = obj123.dRequestdata,
                    @status = obj123.PGstatus,
                    @banktrxid = obj123.banktrxid,
                    @clienttrxid = obj123.clienttrxid,
                    @amount = obj123.amount,
                    @feeamount = (obj123.feeamount == "" ? "0" : obj123.feeamount),
                    @gst = (obj123.gst == "" ? "0" : obj123.gst),
                    @commission = (obj123.commission == "" ? "0" : obj123.commission),
                    @paymode = obj123.paymode,
                    @Reason = obj123.Reason,
                    @banktxndate = obj123.banktxndate,
                    @apitxnid = obj123.apitxnid,
                    @responsevia = "DoubleVerificationURl"

                }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                if (ObjFees.Id <= 0)
                {
                    ObjFees.Status = false;

                }
                return ObjFees;
            }
        }
        public AdmissionFeesSubmit student_collegeform_apply(int sid, int Session, int coursecategoryid, int collegeid, int StreamCategoryID, int courseyearid, int isappearedearlierfail, int electivesubjectid, string type = "", string fileupload = "")
        {
            AdmissionFeesSubmit Obj = new AdmissionFeesSubmit();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {

                CommonMethod cmn = new CommonMethod();
                string ip = CommonMethod.GetIPAddress();
                Obj = conn.Query<AdmissionFeesSubmit>("[sp_student_collegeform_apply]", new
                {
                    @Action = "isapply",
                    @SID = sid,
                    @sessionid = Session,
                    @coursecategoryid = @coursecategoryid,
                    @collegeid = collegeid,
                    @StreamCategoryID = StreamCategoryID,
                    @courseyearid = courseyearid,
                    @isappearedearlierfail = isappearedearlierfail,
                    @electivesubjectid = electivesubjectid
                    ,
                    @type = type,
                    @fileupload = fileupload
                }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return Obj;
            }
        }
        public AdmissionFeesSubmit student_collegeform_affidaavitapply(int sid, int Session, int coursecategoryid, int collegeid, int StreamCategoryID, int courseyearid, int isappearedearlierfail, int electivesubjectid, string type = "", string fileupload = "")
        {
            AdmissionFeesSubmit Obj = new AdmissionFeesSubmit();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {

                CommonMethod cmn = new CommonMethod();
                string ip = CommonMethod.GetIPAddress();
                Obj = conn.Query<AdmissionFeesSubmit>("[sp_student_collegeform_affidavitapply]", new
                {
                    @Action = "isapply",
                    @SID = sid,
                    @sessionid = Session,
                    @coursecategoryid = @coursecategoryid,
                    @collegeid = collegeid,
                    @StreamCategoryID = StreamCategoryID,
                    @courseyearid = courseyearid,
                    //@isappearedearlierfail = isappearedearlierfail,
                   //@electivesubjectid = electivesubjectid
                    //,
                    //@type = type,
                    //@fileupload = fileupload
                }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return Obj;
            }
        }

    }
}

