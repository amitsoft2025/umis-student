using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataLayer;
using System.Data;
using System.IO;
using System.Configuration;
using Website.Models;
using System.Threading;
using Newtonsoft.Json;
using Website.Areas.Student.Models;
using System.Security.Cryptography;
using System.Text;
using System.Net;
using System.Collections.Specialized;
using ThirdParty.Json.LitJson;
using System.Web.Script.Serialization;
using System.Net;
using System.Web.Script.Serialization;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using System.Drawing;
using System.Data.SqlClient;

namespace Website.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return RedirectToAction("Index", "Login", new { area = "Student" });
        }
        public ActionResult Error()
        {
            return View();
        }
        public ActionResult test()
        {
            //Email.SendEmailForSt_signup("preetamkmr3@gmail.com", "6454645645", "test", "test");
            return View();
        }
        [HttpPost]
        public ActionResult test(string val = "")
        {

            return View();
        }

        [HttpPost]
        public JsonResult UploadSaveFiles()
        {
            if (Request.Form.Count > 0)
            {
                Login stvalue = new Login();
                stvalue.FirstName = CommonSetting.RemoveSpecialChars(Request.Form["firstname"]);
                stvalue.MiddleName = CommonSetting.RemoveSpecialChars(Request.Form["middlename"]);
                stvalue.LastName = CommonSetting.RemoveSpecialChars(Request.Form["lastname"]);
                stvalue.Gender = Convert.ToInt32(Request.Form["Gender"]);
                stvalue.DOB = Request.Form["dob"];
                stvalue.CastCategory = Convert.ToInt32(Request.Form["Cast"]);
                stvalue.BloodGroup = Convert.ToInt32(Request.Form["Blood_Group"] == "" ? "0" : Request.Form["Blood_Group"]);
                stvalue.MobileNo = CommonSetting.RemoveSpecialChars(Request.Form["mobileno"]);
                stvalue.Email = CommonSetting.RemoveSpecialCharsemail(Request.Form["email"]);
                stvalue.CurrentAddress = CommonSetting.RemoveSpecialCharsaddress(Request.Form["Address"]);
                stvalue.CA_City = CommonSetting.RemoveSpecialCharsaddress(Request.Form["city"]);
                stvalue.CA_PinCode = CommonSetting.RemoveSpecialChars(Request.Form["pincode"]);
                stvalue.CA_Country = Convert.ToInt32(Request.Form["Country"]);
                stvalue.CA_State = Convert.ToInt32(Request.Form["State"]);
                stvalue.PA_Address = Request.Form["PAddress"];
                stvalue.PA_City = Request.Form["Pcity"];
                stvalue.PA_PinCode = Request.Form["PPinCode"];
                stvalue.PA_Country = Convert.ToInt32(Request.Form["PCountry"]);
                stvalue.PA_State = Convert.ToInt32(Request.Form["PState"]);
                stvalue.FatherName = CommonSetting.RemoveSpecialChars(Request.Form["fathername"]);
                stvalue.FatherMobile = CommonSetting.RemoveSpecialChars(Request.Form["fathermobile"]);
                stvalue.FatherEmail = CommonSetting.RemoveSpecialCharsemail(Request.Form["fatheremail"]);
                stvalue.FatherQualification = CommonSetting.RemoveSpecialChars(Request.Form["fatherqulification"]);
                stvalue.FatherOccupation = CommonSetting.RemoveSpecialChars(Request.Form["fatheroccupation"]);
                stvalue.MotherName = CommonSetting.RemoveSpecialChars(Request.Form["mothername"]);
                stvalue.MotherMobile = CommonSetting.RemoveSpecialChars(Request.Form["mothermobile"]);
                stvalue.MotherEmail = CommonSetting.RemoveSpecialCharsemail(Request.Form["motheremail"]);
                stvalue.MotherOccupation = CommonSetting.RemoveSpecialChars(Request.Form["motheroccupation"]);
                stvalue.MotherQualification = CommonSetting.RemoveSpecialChars(Request.Form["motherqulicafication"]);
                // stvalue.GargianName = Request.Form["Guardianname"];
                // stvalue.GargianRelation = Request.Form["Guardianrelation"];
                // stvalue.GargianContactNo = Request.Form["Guardianmobile"];
                stvalue.EducationType = Convert.ToInt32(Request.Form["educationtype1"] == "" ? "11" : Request.Form["educationtype1"]);
                stvalue.CourseCategory = Convert.ToInt32(Request.Form["coursetype1"] == "" ? "0" : Request.Form["coursetype1"]);
                if (stvalue.MobileNo == "")
                {
                    Login logmsg = new Login();
                    logmsg.Message = "Please Again Fill From  ,or please Enter MobileNo  ";
                    return Json(logmsg, JsonRequestBehavior.AllowGet);
                }
                if (stvalue.CourseCategory == 0)
                {
                    Login logmsg = new Login();
                    logmsg.Message = "Please Again Fill From  ,or please select Course  ";
                    return Json(logmsg, JsonRequestBehavior.AllowGet);
                }
                stvalue.prevoiusboardid = Convert.ToInt32(Request.Form["prevoiusboardid"] == "" ? "0" : Request.Form["prevoiusboardid"]);
                if (stvalue.prevoiusboardid == 0)
                {
                    Login logmsg = new Login();
                    logmsg.Message = "Please Again Fill From : ";
                    return Json(logmsg, JsonRequestBehavior.AllowGet);
                }
                stvalue.prevoiustreamid = Convert.ToInt32(Request.Form["prevoiustreamid"] == "" ? "0" : Request.Form["prevoiustreamid"]);
                if (stvalue.prevoiustreamid == 0)
                {
                    Login logmsg = new Login();
                    logmsg.Message = "Please Again Fill From : ";
                    return Json(logmsg, JsonRequestBehavior.AllowGet);
                }
                if (stvalue.PA_State == 0)
                {
                    Login logmsg = new Login();
                    logmsg.Message = "Please Again Fill From : Please select Permanent State";
                    return Json(logmsg, JsonRequestBehavior.AllowGet);
                }
                stvalue.CourseType = Convert.ToInt32(Request.Form["stream1"] == "" ? "0" : Request.Form["stream1"]);
                stvalue.AdmisitionCategory = Convert.ToInt32(Request.Form["administype1"] == "" ? "1" : Request.Form["administype1"]);
                //stvalue.title = Convert.ToInt32(Request.Form["title"] == "" ? "0" : Request.Form["title"]);
                stvalue.Ftitle = Convert.ToInt32(Request.Form["ftitle"] == "" ? "0" : Request.Form["ftitle"]);
                stvalue.session = Convert.ToInt32(Request.Form["cseesionid"] == "" ? "0" : Request.Form["cseesionid"]);
                stvalue.Nationality = Convert.ToInt32(Request.Form["Nationality"] == "" ? "0" : Request.Form["Nationality"]);
                stvalue.Religion = Convert.ToInt32(Request.Form["Religion"] == "" ? "0" : Request.Form["Religion"]);
                stvalue.MotherTongue = Convert.ToInt32(Request.Form["MotherTongue"] == "" ? "0" : Request.Form["MotherTongue"]);
                stvalue.ishandicapped = Convert.ToBoolean(Request.Form["ishandicapped"] == "" ? "0" : Request.Form["ishandicapped"]);
                stvalue.isex_service_man = Convert.ToBoolean(Request.Form["isex_service_man"] == "" ? "0" : Request.Form["isex_service_man"]);
                stvalue.is_ncc_candidate = Convert.ToBoolean(Request.Form["is_ncc_candidate"] == "" ? "0" : Request.Form["is_ncc_candidate"]);
                stvalue.aadharno = Convert.ToString(Request.Form["aadharno"] == "" ? "" : Request.Form["aadharno"]);
                //--- surendar work 01/04/2019
                stvalue.FirstNameInHindi = Request.Form["FirstNameInHindi"];
                stvalue.MiddleNameInHindi = Request.Form["MiddleNameInHindi"];
                stvalue.LastNameInHindi = Request.Form["LastNameInHindi"];
                stvalue.ReligonOther = Request.Form["ReligonOther"];
                stvalue.disabilityType = Request.Form["disabilityType"];
                stvalue.disabilityPercent = Request.Form["disabilityPercent"];

                stvalue.FatherNameInHindi = Request.Form["FatherNameInHindi"];
                stvalue.MotherNameInHindi = Request.Form["MotherNameInHindi"];

                stvalue.IsSports = Convert.ToBoolean(Request.Form["IsSports"] == "" ? "0" : Request.Form["IsSports"]);
                stvalue.IsStaff = Convert.ToBoolean(Request.Form["IsStaff"] == "" ? "0" : Request.Form["IsStaff"]);
                //stvalue.is_GEW = Convert.ToBoolean(Request.Form["is_GEW"] == "" ? "0" : Request.Form["is_GEW"]);
                stvalue.is_permanentaddress = Convert.ToBoolean(Request.Form["is_permanentaddress"] == "" ? "0" : Request.Form["is_permanentaddress"]);
                stvalue.Password = CommonMethod.RandomNumber(100000, 999999);
                stvalue.aadharno = stvalue.aadharno;
                MD5CryptoServiceProvider md5Hasher = new MD5CryptoServiceProvider();
                Byte[] hashedBytes;
                UTF8Encoding encoder = new UTF8Encoding();
                hashedBytes = md5Hasher.ComputeHash(encoder.GetBytes(stvalue.Password));
                stvalue.hashedBytes = hashedBytes;

                //--till here
                string jsonstring = JsonConvert.SerializeObject(stvalue);
                if (stvalue.Gender != 9)
                {
                    if (stvalue.CastCategory == 23)
                    {
                        Login logmsg = new Login();
                        logmsg.Message = "Please Again Fill From and select another caste !! ";
                        return Json(logmsg, JsonRequestBehavior.AllowGet);
                    }
                }

                //if (stvalue.is_GEW == true)
                //{
                //    if (stvalue.CastCategory != 4)
                //    {
                //        Login logmsg = new Login();
                //        logmsg.Message = "General Economical Weaker Option Only for General Category Student,Please Change ! ";
                //        return Json(logmsg, JsonRequestBehavior.AllowGet);
                //    }
                //}
                if (Request.Files.Count > 0)
                {
                    try
                    {
                        for (int i = 0; i < Request.Files.Count; i++)
                        {
                            if (Request.Files.GetKey(i) == "sign")
                            {
                                HttpPostedFileBase fileUpload = Request.Files.Get(i);
                                if (fileUpload != null && fileUpload.ContentLength > 0)
                                {
                                    if (Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                                    {
                                        string[] testfiles = fileUpload.FileName.Split(new char[] { '\\' });
                                    }

                                    string name = Path.GetFileName(fileUpload.FileName);

                                    try
                                    {
                                        string s3FileName = DateTime.Now.ToString("dd_MM_yyyy_HH_mm_ss") + "_StudentSign_" + stvalue.FirstName + "_" + name;
                                        s3FileName = s3FileName.Replace(" ", "");
                                        stvalue.stsign = s3FileName;

                                        // Save file to "sign" folder
                                        string signPath = Server.MapPath("~/uploads/Student/Photoandsign/");
                                        if (!Directory.Exists(signPath))
                                        {
                                            Directory.CreateDirectory(signPath);
                                        }

                                        string fullSignPath = Path.Combine(signPath, s3FileName);
                                        fileUpload.SaveAs(fullSignPath);
                                    }
                                    catch (Exception ex)
                                    {
                                        CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "Student Registration : Sign Image Upload", "Registration" + jsonstring);
                                    }
                                }
                            }

                            if (Request.Files.GetKey(i) == "photo")
                            {
                                HttpPostedFileBase fileUpload = Request.Files.Get(i);
                                if (fileUpload != null && fileUpload.ContentLength > 0)
                                {
                                    string fname = "";
                                    if (Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                                    {
                                        string[] testfiles = fileUpload.FileName.Split(new char[] { '\\' });
                                        fname = testfiles[testfiles.Length - 1];
                                    }

                                    string name = Path.GetFileName(fileUpload.FileName);

                                    try
                                    {
                                        string s3FileName = DateTime.Now.ToString("dd_MM_yyyy_HH_mm_ss") + "_StudentPhoto_" + stvalue.FirstName + "_" + name;
                                        s3FileName = s3FileName.Replace(" ", "");
                                        stvalue.stphoto = s3FileName;

                                        // Save file to "photo" folder
                                        string photoPath = Server.MapPath("~/uploads/Student/Photoandsign/");
                                        if (!Directory.Exists(photoPath))
                                        {
                                            Directory.CreateDirectory(photoPath);
                                        }

                                        string fullPhotoPath = Path.Combine(photoPath, s3FileName);
                                        fileUpload.SaveAs(fullPhotoPath);
                                    }
                                    catch (Exception ex)
                                    {
                                        CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "Student Registration : Photo Image Upload", "Registration" + jsonstring);
                                    }
                                }
                            }

                        }
                        StudentLogin st = new StudentLogin();
                        var result = st.Student_registration(stvalue);
                        //result.Password = stvalue.Password;
                        if (result.status == true)
                        {
                            Email.SendEmailForSt_signup(result.Email, result.Password, result.FirstName, result.ApplicationNo, result.MobileNo);
                        }
                        return Json(result, JsonRequestBehavior.AllowGet);

                    }
                    catch (Exception ex)
                    {
                        //CommonMethod.PrintLog(ex, HttpContext.Request.Url.AbsolutePath, "Student Registration", "");
                        CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "Student Registration : Save", "Registration  " + jsonstring);
                        Login logmsg = new Login();
                        logmsg.Message = "Error occurred. Error details: " + ex.Message;
                        return Json(logmsg, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    Login logmsg = new Login();
                    logmsg.Message = "Please Again Fill From : ";
                    return Json(logmsg, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                Login logmsg = new Login();
                logmsg.Message = "Please Again Fill From : ";
                return Json(logmsg, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult StudentApplicationForm()
        {
            // return RedirectToAction("Index", "Login", new { area = "Student" });
            UserLogin.ExpireAllCookies();
            Commn_master com = new Commn_master();
            ViewBag.Admissiontype = com.getcommonMaster("AdmissionType");
            ViewBag.Educationtype = com.getcommonMaster("selectbycommonid", Convert.ToInt32(CommonSetting.Commonid.Educationtype));
            ViewBag.Gender = com.getcommonMaster("Gender");
            ViewBag.CasteCategory = com.getcommonMaster("CasteCategory");
            ViewBag.bloodgroup = com.Getbloodgroup("Select");
            ViewBag.stitle = com.getcommonMaster("Title");
            ViewBag.ftitle = com.getcommonMaster("TitleM");
            ViewBag.Nationality = com.getcommonMaster("Nationality");
            ViewBag.Religion = com.getcommonMaster("Religion");
            ViewBag.MotherTongue = com.getcommonMaster("MotherTongue");
            StudentAdmissionQualification com1 = new StudentAdmissionQualification();
            ViewBag.previousqua = com1.getqualificationst(Convert.ToInt32(CommonSetting.Commonid.Educationtype), "getqualificationstdiploma");
            Country cont = new Country();
            ViewBag.country = cont.GetAllCountries();
            ViewBag.Pcountry = cont.GetAllCountries();
            ViewBag.boardtype = CommonMethod.Boradtype().Where(x => x.boardid == 1 || x.boardid == 2 || x.boardid == 3);
            State ob = new State();
            List<State> ds = ob.GetStateListByCountryId("80");
            List<SelectListItem> statelist = new List<SelectListItem>();
            foreach (State dr in ds)
            {
                statelist.Add(new SelectListItem { Text = dr.StateName, Value = dr.stateID.ToString() });
            }

            ViewBag.State = statelist;
            ViewBag.PState = statelist;
            AcademicSession ac = new AcademicSession();
            ViewBag.CurrentSession = ac.GetAcademiccurrentSession().Session;
            ViewBag.CurrentSessionid = ac.GetAcademiccurrentSession().ID;

            ViewBag.check_admissionopen = com.check_admission_open(ac.GetAcademiccurrentSession().ID, Convert.ToInt32(CommonSetting.Commonid.Educationtype));

            return View();
        }
        public JsonResult getprevioustream(int id, int prevoiusboradid = 0)
        {
            StudentAdmissionQualification com = new StudentAdmissionQualification();
            List<QualifiationMaster> obj = new List<QualifiationMaster>();
            string action = "getqualificationstdiploma";
            if (prevoiusboradid == 3)
            {
                action = "getqualificationstdiplomaonly";
            }
            obj = com.getqualificationst(id, action);
            return Json(new { data = obj, success = true });
        }
        public JsonResult getcousrequlification(int id, int quaid)
        {
            Commn_master com = new Commn_master();
            var obj = com.getcommonMaster("Courseandqualification", id, quaid);
            return Json(new { data = obj, success = true });
        }
        public JsonResult getcousre(int id)
        {
            Commn_master com = new Commn_master();
            var obj = com.getcommonMaster("Course", id);
            return Json(new { data = obj, success = true });
        }
        public JsonResult getstream(int id)
        {
            Commn_master com = new Commn_master();
            var obj = com.getcommonMaster("Stream", id);
            return Json(new { data = obj, success = true });
        }
        public JsonResult State_Bind(string id)
        {
            State ob = new State();
            List<State> ds = ob.GetStateListByCountryId(id);
            List<SelectListItem> statelist = new List<SelectListItem>();
            foreach (State dr in ds)
            {
                statelist.Add(new SelectListItem { Text = dr.StateName, Value = dr.stateID.ToString() });
            }
            return Json(statelist, JsonRequestBehavior.AllowGet);
        }
        public JsonResult studentsendotp(string MobileNo, string Coursetype, string sessionid)
        {
            Commn_master com = new Commn_master();
            var res = com.SendOTp(MobileNo, CommonMethod.RandomNumber(1000, 9999), Coursetype, sessionid);

            if (res != null)
            {
                if (res.status != false)
                {
                    SMSFUN.Send_OTPverifymobile(MobileNo, res.otp);
                    res.otp = null;
                }
            }

            return Json(res, JsonRequestBehavior.AllowGet);
        }
        public JsonResult studentverifyotp(string MobileNo, string otp)
        {
            Commn_master com = new Commn_master();
            var res = com.verifyOTp(MobileNo, otp);
            if (res != null)
            { res.otp = null; }
            return Json(res, JsonRequestBehavior.AllowGet);
        }
        public JsonResult googletranslate(string text)
        {
            try
            {
                if (text == "")
                {
                    return Json(new { data = "", success = true });
                }
                //return Json(new { success = true });
                // https://www.googleapis.com/language/translate/v2?key=AIzaSyDYK8tgnoZ5amt9rdvdlzJYppdqhmaeEHI&q=hello%20world&source=en&target=hi
                // string url = String.Format
                //("https://translate.googleapis.com/translate_a/single?client=gtx&sl={0}&tl={1}&dt=t&q={2}",
                // "en", "hi", Uri.EscapeUriString(text));

                string url = String.Format
               ("https://www.googleapis.com/language/translate/v2?key=AIzaSyDYK8tgnoZ5amt9rdvdlzJYppdqhmaeEHI&q={0}&source=en&target=hi",
                 Uri.EscapeUriString(text));

                //WebClient client = new WebClient();
                //string json = client.DownloadString(url);
                //JsonData jsonData = (new JavaScriptSerializer()).Deserialize<JsonData>(json);
                HttpClient httpClient = new HttpClient();
                //CommonMethod.WritetoNotepad(null, HttpContext.Request.Url.AbsolutePath, "Student google : url", "url  " + url);
                string result = httpClient.GetStringAsync(url).Result;
                //CommonMethod.WritetoNotepad(null, HttpContext.Request.Url.AbsolutePath, "Student google : result", "result  " + result);
                // Get all json data
                // result= "{ ""data"": {                    ""translations"": [{""translatedText"": ""प्रीतम कुमार""}]}}";
                result = result.Replace("\r", "").Replace("\n", "");
                result = result.Replace("\r\n", "").Replace("\n", "");
                result = result.Replace("  ", "").Replace("\n", "");

                var result11 = JsonConvert.DeserializeObject<dynamic>(result);
                var translation = result11.data.translations[0].translatedText.Value;
                translation = translation;
                return Json(new { data = translation, success = true });

                return Json(new { data = translation, success = true });
            }
            catch (Exception ex)
            {

                //  CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "Student google : text", "text  " + text);
            }
            return Json(new { success = false });
        }

        public JsonResult Bind_caste(int gender)
        {
            Commn_master com = new Commn_master();
            if (gender == Convert.ToInt32(CommonSetting.Commonid.Femalegender))
            {
                var aa = com.getcommonMaster("CasteCategory");
                return Json(aa, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var aa = com.getcommonMaster("CastwithoutWBC");
                return Json(aa, JsonRequestBehavior.AllowGet);
            }

        }
        public JsonResult Bind_gender(int title)
        {
            Commn_master com = new Commn_master();
            if (title == Convert.ToInt32(CommonSetting.Commonid.Mr))
            {
                var aa = com.getcommonMaster("malegender");
                return Json(aa, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var aa = com.getcommonMaster("femalegender");
                return Json(aa, JsonRequestBehavior.AllowGet);
            }

        }
        public JsonResult Bind_ftitle(int title)
        {
            Commn_master com = new Commn_master();
            if (title == Convert.ToInt32(CommonSetting.Commonid.Mr))
            {
                var aa = com.getcommonMaster("maleftile");
                return Json(aa, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var aa = com.getcommonMaster("femaleftitle");
                return Json(aa, JsonRequestBehavior.AllowGet);
            }

        }

        public ActionResult ForgotPassword(string id)
        {
            try
            {
                string AdminID = EncriptDecript.Decrypt(id);
                BL_ForgotPass obj = new BL_ForgotPass();
                obj.AdminID = Convert.ToInt32(AdminID);
                ViewBag.ID = Convert.ToInt32(AdminID);
                return View();
            }
            catch (Exception ex)
            {

                CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "ForgotPassword", id);
                return View();
            }
        }
        [HttpPost]
        public JsonResult ForgotPasswords(string Password, int id)
        {
            try
            {
                BL_ForgotPass forgot = new BL_ForgotPass();
                MD5CryptoServiceProvider md5Hasher = new MD5CryptoServiceProvider();
                Byte[] hashedBytes;
                UTF8Encoding encoder = new UTF8Encoding();
                hashedBytes = md5Hasher.ComputeHash(encoder.GetBytes(Password));


                var obj = forgot.ForgotPassword(id, hashedBytes, Password);
                return Json(new { data = obj, success = true });
            }
            catch (Exception ex)
            {
                CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "ForgotPassword paost Method", id.ToString());
                return Json(new { data = new BL_ForgotPass(), success = true });
            }
        }
        public ActionResult ResetPassword()
        {
            return View();
        }
        [HttpPost]
        public JsonResult ResetPass(string ApplicationNo, string DOB)
        {

            BL_ForgotPass obj = new BL_ForgotPass();
            try
            {
                if (DOB == null)
                {
                    DOB = "";
                }
                else
                {
                    DOB = DOB.Replace("-", "/");
                    DOB = DOB.Replace(" ", "/");
                    DOB = DOB.Replace(".", "/");
                    DOB = DateTime.ParseExact(DOB, "dd/MM/yyyy", null).ToString("MM/dd/yyyy");
                }
                MD5CryptoServiceProvider md5Hasher = new MD5CryptoServiceProvider();
                Byte[] hashedBytes;
                UTF8Encoding encoder = new UTF8Encoding();
                string Password = CommonMethod.RandomNumber(100000, 999999);
                hashedBytes = md5Hasher.ComputeHash(encoder.GetBytes(Password));

                var sendreset = obj.ForgotPassword_sms(0, hashedBytes, Password, ApplicationNo, DOB);
                int StudentID = sendreset.SID;
                string ID = EncriptDecript.Encrypt(StudentID.ToString());
                string MyName = sendreset.Name;
                string mobile = sendreset.mobileno;
                string url = ConfigurationManager.AppSettings["siteUrl"];
                //string PasswordResetLink = url + "Home/ForgotPassword?Id=" + ID;
                if (StudentID > 0)
                {
                    SMSFUN.sms_PasswordSend(3, mobile, MyName, ApplicationNo, Password, ConfigurationManager.AppSettings["ProjectName"]);
                    // Email.SendEmailForResetPassword(SEmail, MyName, PasswordResetLink);
                    obj.Msg = "Your Password Send to Registered Mobile Number !!";
                    obj.Status = true;
                    TempData["errorMsg"] = obj.Msg;
                }
                else
                {
                    obj.Msg = "Invalid Application ID or DOB not match !!";
                    //TempData["errorMsg"] = obj.Msg;
                }
            }
            catch (Exception ex)
            {

                CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "Reset Password" + " " + ApplicationNo + "  DOB:" + DOB);
            }
            return Json(new { data = obj, success = true });
        }
        public ActionResult PGPushResponse()
        {
            if (Request.Form.Count > 0)
            {
                try
                {
                    Sbiepay sbi = new Sbiepay();
                    var result = sbi.Pushresponsedecrypt();
                    //var result1 = sbi.Pushresponsedecrypt_spot();
                }
                catch (Exception ex)
                {
                    CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "Paymentgateway Pushresponse  url hit on Controller error");
                    return RedirectToAction("Index");
                }
            }
            return RedirectToAction("Index");
        }
        public ActionResult PGPushResponserdanddjcollege()
        {
            if (Request.Form.Count > 0)
            {
                try
                {
                    SbiepayAdmission sbi = new SbiepayAdmission();
                    string ckey = CommonMethod.MIDcollegewise().Where(x => x.collegeid == 1).FirstOrDefault().mkey;
                    var result = sbi.Pushresponseradmission(ckey);
                }
                catch (Exception ex)
                {
                    CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "admission Paymentgateway Pushresponse  url hit on Controller error");
                    // return RedirectToAction("Index");
                }
            }
            return RedirectToAction("Index");
        }
        public ActionResult PGPushResponsejrscollegejamalpur()
        {
            if (Request.Form.Count > 0)
            {
                try
                {
                    SbiepayAdmission sbi = new SbiepayAdmission();
                    string ckey = CommonMethod.MIDcollegewise().Where(x => x.collegeid == 2).FirstOrDefault().mkey;
                    var result = sbi.Pushresponseradmission(ckey);
                }
                catch (Exception ex)
                {
                    CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "admission Paymentgateway Pushresponse  url hit on Controller error");
                    return RedirectToAction("Index");
                }
            }
            return RedirectToAction("Index");
        }
        public ActionResult PGPushResponsekoshicollegekhagaria()
        {
            if (Request.Form.Count > 0)
            {
                try
                {
                    SbiepayAdmission sbi = new SbiepayAdmission();
                    string ckey = CommonMethod.MIDcollegewise().Where(x => x.collegeid == 3).FirstOrDefault().mkey;
                    var result = sbi.Pushresponseradmission(ckey);
                }
                catch (Exception ex)
                {
                    CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "admission Paymentgateway Pushresponse  url hit on Controller error");
                    return RedirectToAction("Index");
                }
            }
            return RedirectToAction("Index");
        }
        public ActionResult PGPushResponsekkmcollegejamui()
        {
            if (Request.Form.Count > 0)
            {
                try
                {
                    SbiepayAdmission sbi = new SbiepayAdmission();
                    string ckey = CommonMethod.MIDcollegewise().Where(x => x.collegeid == 4).FirstOrDefault().mkey;
                    var result = sbi.Pushresponseradmission(ckey);
                }
                catch (Exception ex)
                {
                    CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "admission Paymentgateway Pushresponse  url hit on Controller error");
                    return RedirectToAction("Index");
                }
            }
            return RedirectToAction("Index");
        }
        public ActionResult PGPushResponseskrcollegebarbigha()
        {
            if (Request.Form.Count > 0)
            {
                try
                {
                    SbiepayAdmission sbi = new SbiepayAdmission();
                    string ckey = CommonMethod.MIDcollegewise().Where(x => x.collegeid == 5).FirstOrDefault().mkey;
                    var result = sbi.Pushresponseradmission(ckey);
                }
                catch (Exception ex)
                {
                    CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "admission Paymentgateway Pushresponse  url hit on Controller error");
                    return RedirectToAction("Index");
                }
            }
            return RedirectToAction("Index");
        }
        public ActionResult PGPushResponserscollegetarapurmunger()
        {
            if (Request.Form.Count > 0)
            {
                try
                {
                    SbiepayAdmission sbi = new SbiepayAdmission();
                    string ckey = CommonMethod.MIDcollegewise().Where(x => x.collegeid == 6).FirstOrDefault().mkey;
                    var result = sbi.Pushresponseradmission(ckey);
                }
                catch (Exception ex)
                {
                    CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "admission Paymentgateway Pushresponse  url hit on Controller error");
                    return RedirectToAction("Index");
                }
            }
            return RedirectToAction("Index");
        }
        public ActionResult PGPushResponsebrmcollege()
        {
            if (Request.Form.Count > 0)
            {
                try
                {
                    SbiepayAdmission sbi = new SbiepayAdmission();
                    string ckey = CommonMethod.MIDcollegewise().Where(x => x.collegeid == 7).FirstOrDefault().mkey;
                    var result = sbi.Pushresponseradmission(ckey);
                }
                catch (Exception ex)
                {
                    CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "admission Paymentgateway Pushresponse  url hit on Controller error");
                    return RedirectToAction("Index");
                }
            }
            return RedirectToAction("Index");
        }
        public ActionResult PGPushResponsebnmcollege()
        {
            if (Request.Form.Count > 0)
            {
                try
                {
                    SbiepayAdmission sbi = new SbiepayAdmission();
                    string ckey = CommonMethod.MIDcollegewise().Where(x => x.collegeid == 10).FirstOrDefault().mkey;
                    var result = sbi.Pushresponseradmission(ckey);
                }
                catch (Exception ex)
                {
                    CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "admission Paymentgateway Pushresponse  url hit on Controller error");
                    return RedirectToAction("Index");
                }
            }
            return RedirectToAction("Index");
        }
        public ActionResult PGPushResponserdcollegesheikpura()
        {
            if (Request.Form.Count > 0)
            {
                try
                {
                    SbiepayAdmission sbi = new SbiepayAdmission();
                    string ckey = CommonMethod.MIDcollegewise().Where(x => x.collegeid == 11).FirstOrDefault().mkey;
                    var result = sbi.Pushresponseradmission(ckey);
                }
                catch (Exception ex)
                {
                    CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "admission Paymentgateway Pushresponse  url hit on Controller error");
                    return RedirectToAction("Index");
                }
            }
            return RedirectToAction("Index");
        }
        public ActionResult PGPushResponsejmscollegemunger()
        {
            if (Request.Form.Count > 0)
            {
                try
                {
                    SbiepayAdmission sbi = new SbiepayAdmission();
                    string ckey = CommonMethod.MIDcollegewise().Where(x => x.collegeid == 13).FirstOrDefault().mkey;
                    var result = sbi.Pushresponseradmission(ckey);
                }
                catch (Exception ex)
                {
                    CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "admission Paymentgateway Pushresponse  url hit on Controller error");
                    return RedirectToAction("Index");
                }
            }
            return RedirectToAction("Index");
        }
        public ActionResult PGPushResponsehscollege()
        {
            if (Request.Form.Count > 0)
            {
                try
                {
                    SbiepayAdmission sbi = new SbiepayAdmission();
                    string ckey = CommonMethod.MIDcollegewise().Where(x => x.collegeid == 14).FirstOrDefault().mkey;
                    var result = sbi.Pushresponseradmission(ckey);
                }
                catch (Exception ex)
                {
                    CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "admission Paymentgateway Pushresponse  url hit on Controller error");
                    return RedirectToAction("Index");
                }
            }
            return RedirectToAction("Index");
        }
        public ActionResult PGPushResponseksscollegelakhisarai()
        {
            if (Request.Form.Count > 0)
            {
                try
                {
                    SbiepayAdmission sbi = new SbiepayAdmission();
                    string ckey = CommonMethod.MIDcollegewise().Where(x => x.collegeid == 15).FirstOrDefault().mkey;
                    var result = sbi.Pushresponseradmission(ckey);
                }
                catch (Exception ex)
                {
                    CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "admission Paymentgateway Pushresponse  url hit on Controller error");
                    return RedirectToAction("Index");
                }
            }
            return RedirectToAction("Index");
        }
        public ActionResult PGPushResponsekdscollegegogri()
        {
            if (Request.Form.Count > 0)
            {
                try
                {
                    SbiepayAdmission sbi = new SbiepayAdmission();
                    string ckey = CommonMethod.MIDcollegewise().Where(x => x.collegeid == 16).FirstOrDefault().mkey;
                    var result = sbi.Pushresponseradmission(ckey);
                }
                catch (Exception ex)
                {
                    CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "admission Paymentgateway Pushresponse  url hit on Controller error");
                    return RedirectToAction("Index");
                }
            }
            return RedirectToAction("Index");
        }
        public ActionResult PGPushResponsedsmcollegejhajha()
        {
            if (Request.Form.Count > 0)
            {
                try
                {
                    SbiepayAdmission sbi = new SbiepayAdmission();
                    string ckey = CommonMethod.MIDcollegewise().Where(x => x.collegeid == 17).FirstOrDefault().mkey;
                    var result = sbi.Pushresponseradmission(ckey);
                }
                catch (Exception ex)
                {
                    CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "admission Paymentgateway Pushresponse  url hit on Controller error");
                    return RedirectToAction("Index");
                }
            }
            return RedirectToAction("Index");
        }
        public ActionResult PGPushResponsekmdcollegeparbatta()
        {
            if (Request.Form.Count > 0)
            {
                try
                {
                    SbiepayAdmission sbi = new SbiepayAdmission();
                    string ckey = CommonMethod.MIDcollegewise().Where(x => x.collegeid == 18).FirstOrDefault().mkey;
                    var result = sbi.Pushresponseradmission(ckey);
                }
                catch (Exception ex)
                {
                    CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "admission Paymentgateway Pushresponse  url hit on Controller error");
                    return RedirectToAction("Index");
                }
            }
            return RedirectToAction("Index");
        }
        public ActionResult PGPushResponsemahilacollegekhagaria()
        {
            if (Request.Form.Count > 0)
            {
                try
                {
                    SbiepayAdmission sbi = new SbiepayAdmission();
                    string ckey = CommonMethod.MIDcollegewise().Where(x => x.collegeid == 19).FirstOrDefault().mkey;
                    var result = sbi.Pushresponseradmission(ckey);
                }
                catch (Exception ex)
                {
                    CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "admission Paymentgateway Pushresponse  url hit on Controller error");
                    return RedirectToAction("Index");
                }
            }
            return RedirectToAction("Index");
        }
        public ActionResult PGPushResponsejamalpurcollegejamalpur()
        {
            if (Request.Form.Count > 0)
            {
                try
                {
                    SbiepayAdmission sbi = new SbiepayAdmission();
                    string ckey = CommonMethod.MIDcollegewise().Where(x => x.collegeid == 20).FirstOrDefault().mkey;
                    var result = sbi.Pushresponseradmission(ckey);
                }
                catch (Exception ex)
                {
                    CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "admission Paymentgateway Pushresponse  url hit on Controller error");
                    return RedirectToAction("Index");
                }
            }
            return RedirectToAction("Index");
        }
        public ActionResult PGPushResponsesbncollegegarhirampurmunger()
        {
            if (Request.Form.Count > 0)
            {
                try
                {
                    SbiepayAdmission sbi = new SbiepayAdmission();
                    string ckey = CommonMethod.MIDcollegewise().Where(x => x.collegeid == 21).FirstOrDefault().mkey;
                    var result = sbi.Pushresponseradmission(ckey);
                }
                catch (Exception ex)
                {
                    CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "admission Paymentgateway Pushresponse  url hit on Controller error");
                    return RedirectToAction("Index");
                }
            }
            return RedirectToAction("Index");
        }
        public ActionResult PGPushResponseinternationalcollegeghosaith()
        {
            if (Request.Form.Count > 0)
            {
                try
                {
                    SbiepayAdmission sbi = new SbiepayAdmission();
                    string ckey = CommonMethod.MIDcollegewise().Where(x => x.collegeid == 22).FirstOrDefault().mkey;
                    var result = sbi.Pushresponseradmission(ckey);
                }
                catch (Exception ex)
                {
                    CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "admission Paymentgateway Pushresponse  url hit on Controller error");
                    return RedirectToAction("Index");
                }
            }
            return RedirectToAction("Index");
        }
        public ActionResult PGPushResponserlalcollegelakhisarai()
        {
            if (Request.Form.Count > 0)
            {
                try
                {
                    SbiepayAdmission sbi = new SbiepayAdmission();
                    string ckey = CommonMethod.MIDcollegewise().Where(x => x.collegeid == 23).FirstOrDefault().mkey;
                    var result = sbi.Pushresponseradmission(ckey);
                }
                catch (Exception ex)
                {
                    CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "admission Paymentgateway Pushresponse  url hit on Controller error");
                    return RedirectToAction("Index");
                }
            }
            return RedirectToAction("Index");
        }
        public ActionResult PGPushResponsemahilacollegebarahiya()
        {
            if (Request.Form.Count > 0)
            {
                try
                {
                    SbiepayAdmission sbi = new SbiepayAdmission();
                    string ckey = CommonMethod.MIDcollegewise().Where(x => x.collegeid == 24).FirstOrDefault().mkey;
                    var result = sbi.Pushresponseradmission(ckey);
                }
                catch (Exception ex)
                {
                    CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "admission Paymentgateway Pushresponse  url hit on Controller error");
                    return RedirectToAction("Index");
                }
            }
            return RedirectToAction("Index");
        }
        public ActionResult PGPushResponsesanjaygandhimahilacollegesheikhpura()
        {
            if (Request.Form.Count > 0)
            {
                try
                {
                    SbiepayAdmission sbi = new SbiepayAdmission();
                    string ckey = CommonMethod.MIDcollegewise().Where(x => x.collegeid == 25).FirstOrDefault().mkey;
                    var result = sbi.Pushresponseradmission(ckey);
                }
                catch (Exception ex)
                {
                    CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "admission Paymentgateway Pushresponse  url hit on Controller error");
                    return RedirectToAction("Index");
                }
            }
            return RedirectToAction("Index");
        }
        public ActionResult PGPushResponsesscollegemehussheikhpura()
        {
            if (Request.Form.Count > 0)
            {
                try
                {
                    SbiepayAdmission sbi = new SbiepayAdmission();
                    string ckey = CommonMethod.MIDcollegewise().Where(x => x.collegeid == 26).FirstOrDefault().mkey;
                    var result = sbi.Pushresponseradmission(ckey);
                }
                catch (Exception ex)
                {
                    CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "admission Paymentgateway Pushresponse  url hit on Controller error");
                    return RedirectToAction("Index");
                }
            }
            return RedirectToAction("Index");
        }
        public ActionResult PGPushResponsecnbcollegehathiyamasheikhpura()
        {
            if (Request.Form.Count > 0)
            {
                try
                {
                    SbiepayAdmission sbi = new SbiepayAdmission();
                    string ckey = CommonMethod.MIDcollegewise().Where(x => x.collegeid == 27).FirstOrDefault().mkey;
                    var result = sbi.Pushresponseradmission(ckey);
                }
                catch (Exception ex)
                {
                    CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "admission Paymentgateway Pushresponse  url hit on Controller error");
                    return RedirectToAction("Index");
                }
            }
            return RedirectToAction("Index");
        }
        public ActionResult PGPushResponsemscollegealoulisoniharkhagaria()
        {
            if (Request.Form.Count > 0)
            {
                try
                {
                    SbiepayAdmission sbi = new SbiepayAdmission();
                    string ckey = CommonMethod.MIDcollegewise().Where(x => x.collegeid == 28).FirstOrDefault().mkey;
                    var result = sbi.Pushresponseradmission(ckey);
                }
                catch (Exception ex)
                {
                    CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "admission Paymentgateway Pushresponse  url hit on Controller error");
                    return RedirectToAction("Index");
                }
            }
            return RedirectToAction("Index");
        }
        public ActionResult PGPushResponsedhanrajsinghcollegesikandarajamui()
        {
            if (Request.Form.Count > 0)
            {
                try
                {
                    SbiepayAdmission sbi = new SbiepayAdmission();
                    string ckey = CommonMethod.MIDcollegewise().Where(x => x.collegeid == 29).FirstOrDefault().mkey;
                    var result = sbi.Pushresponseradmission(ckey);
                }
                catch (Exception ex)
                {
                    CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "admission Paymentgateway Pushresponse  url hit on Controller error");
                    return RedirectToAction("Index");
                }
            }
            return RedirectToAction("Index");
        }
        public ActionResult PGPushResponseskcollegelohandasikandarajamui()
        {
            if (Request.Form.Count > 0)
            {
                try
                {
                    SbiepayAdmission sbi = new SbiepayAdmission();
                    string ckey = CommonMethod.MIDcollegewise().Where(x => x.collegeid == 30).FirstOrDefault().mkey;
                    var result = sbi.Pushresponseradmission(ckey);
                }
                catch (Exception ex)
                {
                    CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "admission Paymentgateway Pushresponse  url hit on Controller error");
                    return RedirectToAction("Index");
                }
            }
            return RedirectToAction("Index");
        }
        public ActionResult PGPushResponsephalguniprasadyadavcollegechakaijamui()
        {
            if (Request.Form.Count > 0)
            {
                try
                {
                    SbiepayAdmission sbi = new SbiepayAdmission();
                    string ckey = CommonMethod.MIDcollegewise().Where(x => x.collegeid == 31).FirstOrDefault().mkey;
                    var result = sbi.Pushresponseradmission(ckey);
                }
                catch (Exception ex)
                {
                    CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "admission Paymentgateway Pushresponse  url hit on Controller error");
                    return RedirectToAction("Index");
                }
            }
            return RedirectToAction("Index");
        }
        public ActionResult PGPushResponseshyamaprasadsinghmahilacollegejamui()
        {
            if (Request.Form.Count > 0)
            {
                try
                {
                    SbiepayAdmission sbi = new SbiepayAdmission();
                    string ckey = CommonMethod.MIDcollegewise().Where(x => x.collegeid == 32).FirstOrDefault().mkey;
                    var result = sbi.Pushresponseradmission(ckey);
                }
                catch (Exception ex)
                {
                    CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "admission Paymentgateway Pushresponse  url hit on Controller error");
                    return RedirectToAction("Index");
                }
            }
            return RedirectToAction("Index");
        }
        public ActionResult PGPushResponseshardagirdharikesharicollege()
        {
            if (Request.Form.Count > 0)
            {
                try
                {
                    SbiepayAdmission sbi = new SbiepayAdmission();
                    string ckey = CommonMethod.MIDcollegewise().Where(x => x.collegeid == 33).FirstOrDefault().mkey;
                    var result = sbi.Pushresponseradmission(ckey);
                }
                catch (Exception ex)
                {
                    CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "admission Paymentgateway Pushresponse  url hit on Controller error");
                    return RedirectToAction("Index");
                }
            }
            return RedirectToAction("Index");
        }

        public ActionResult PGPushResponsesbishwanathsinghlegalstudies()
        {
            if (Request.Form.Count > 0)
            {
                try
                {
                    SbiepayAdmission sbi = new SbiepayAdmission();
                    string ckey = CommonMethod.MIDcollegewise().Where(x => x.collegeid == 34).FirstOrDefault().mkey;
                    var result = sbi.Pushresponseradmission(ckey);
                }
                catch (Exception ex)
                {
                    CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "admission Paymentgateway Pushresponse  url hit on Controller error");
                    return RedirectToAction("Index");
                }
            }
            return RedirectToAction("Index");
        }
        public ActionResult PGPushResponsesSaraswathiArjunEklavyaDegreeCollege()
        {
            if (Request.Form.Count > 0)
            {
                try
                {
                    SbiepayAdmission sbi = new SbiepayAdmission();
                    string ckey = CommonMethod.MIDcollegewise().Where(x => x.collegeid == 40).FirstOrDefault().mkey;
                    var result = sbi.Pushresponseradmission(ckey);
                }
                catch (Exception ex)
                {
                    CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "admission Paymentgateway Pushresponse  url hit on Controller error");
                    return RedirectToAction("Index");
                }
            }
            return RedirectToAction("Index");
        }
        public ActionResult PGPushResponseEnroll()
        {
            if (Request.Form.Count > 0)
            {
                try
                {
                    SbiPayEnrollment sbi = new SbiPayEnrollment();
                    var result = sbi.Pushresponsedecrypt(HttpContext.Request.Form["pushRespData"]);
                }
                catch (Exception ex)
                {
                    CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "Paymentgateway Enrollment Pushresponse  url hit on Controller error");
                    return RedirectToAction("Index");
                }
            }
            return RedirectToAction("Index");
        }
        public ActionResult PGPushResponseExam()
        {
            if (Request.Form.Count > 0)
            {
                try
                {
                    SbiPayExam sbi = new SbiPayExam();
                    var result = sbi.Pushresponsedecrypt(HttpContext.Request.Form["pushRespData"]);
                }
                catch (Exception ex)
                {
                    CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "Paymentgateway Exam Pushresponse  url hit on Controller error");
                    return RedirectToAction("Index");
                }
            }
            return RedirectToAction("Index");
        }
        public ActionResult DoubleVerificationURl()
        {
            //StringBuilder sbPostData = new StringBuilder();
            //sbPostData.AppendFormat("queryRequest={0}", "|1000733|23380762618491910380");
            //sbPostData.AppendFormat("&aggregatorId={0}", "SBIEPAY");
            //sbPostData.AppendFormat("&merchantId={0}", "1000733");


            try
            {
                //CommonMethod.WritetoNotepad(null, HttpContext.Request.Url.AbsolutePath, "DoubleVerificationURl " + "Breakpreetam heelo 1000756");
                //string URI = "https://www.sbiepay.com/payagg/orderStatusQuery/getOrderStatusQuery";
                //ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                //var request = (HttpWebRequest)WebRequest.Create(URI);
                //var postData = "queryRequest=|1000756|" + "23380762618491910380";
                //postData += "&aggregatorId=SBIEPAY";
                //postData += "&merchantId=1000756";
                //var data = Encoding.ASCII.GetBytes(postData);
                //request.Method = "POST";
                //request.ContentType = "application/x-www-form-urlencoded";
                //request.ContentLength = data.Length;
                //using (var stream = request.GetRequestStream())
                //{
                //    stream.Write(data, 0, data.Length);
                //}
                //var response = (HttpWebResponse)request.GetResponse();

                //var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
                //CommonMethod.WritetoNotepad(null, HttpContext.Request.Url.AbsolutePath, "DoubleVerificationURl " + "Break"+ responseString);
                ////Uri address = new Uri("https://test.sbiepay.com/payagg/orderStatusQuery/getOrderStatusQuery");

                ////ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
                ////ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3;

                //System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;

                ////using (WebClient webClient = new WebClient())
                ////{
                ////    var stream = webClient.OpenRead(address);
                ////    using (StreamReader sr = new StreamReader(stream))
                ////    {
                ////        var page = sr.ReadToEnd();

                ////        // return page;
                ////    }
                ////}
                ////CommonMethod.WritetoNotepad(null, HttpContext.Request.Url.AbsolutePath, "DoubleVerificationURl " + "Breakpreetam heelo 1000733");
                ////WebRequest webRequest = WebRequest.Create(@"https://www.sbiepay.com/payagg/orderStatusQuery/getOrderStatusQuery");
                ////webRequest.ContentType = "text/html";
                ////webRequest.Method = "POST";
                ////string body = "...";
                ////byte[] bytes = Encoding.ASCII.GetBytes(body);
                ////webRequest.ContentLength = bytes.Length;
                ////var os = webRequest.GetRequestStream();
                ////os.Write(bytes, 0, bytes.Length);
                ////os.Close();
                ////webRequest.Timeout = 0; //setting the timeout to 0 causes the request to fail
                ////WebResponse webResponse = webRequest.GetResponse(); //Exception thrown here ...
                ////CommonMethod.WritetoNotepad(null, HttpContext.Request.Url.AbsolutePath, "DoubleVerificationURl " + "Breakpreetam heelo "+ webResponse);
                ////return RedirectToAction("Index");
                ////using (var wb = new WebClient())
                ////{


                ////    var data = new NameValueCollection();
                ////    data["queryRequest"] = "|1000733|42067162616181939343";
                ////    data["aggregatorId"] = "SBIEPAY";
                ////    data["merchantId"] = "1000733";
                ////    CommonMethod.WritetoNotepad(null, HttpContext.Request.Url.AbsolutePath, "DoubleVerificationURl MID:--" + "1000733");
                ////    var response1 = wb.UploadValues("https://www.sbiepay.com/payagg/orderStatusQuery/getOrderStatusQuery", "POST", data);
                ////    CommonMethod.WritetoNotepad(null, HttpContext.Request.Url.AbsolutePath, "DoubleVerificationURl " + response1);

                ////}

                ////using (var wb = new WebClient())
                ////{
                ////    var data = new NameValueCollection();
                ////    data["queryRequest"] = "|1000003|2639506614091939495";
                ////    data["aggregatorId"] = "SBIEPAY";
                ////    data["merchantId"] = "1000003";
                ////    CommonMethod.WritetoNotepad(null, HttpContext.Request.Url.AbsolutePath, "DoubleVerificationURl MID:--" + "1000003");
                ////    var response1 = wb.UploadValues("https://www.sbiepay.com/payagg/orderStatusQuery/getOrderStatusQuery", "POST", data);
                ////    CommonMethod.WritetoNotepad(null, HttpContext.Request.Url.AbsolutePath, "DoubleVerificationURl " + response1);

                ////}
                //  return RedirectToAction("Index");

                //using (var wb = new WebClient())
                //{
                //    var data = new NameValueCollection();
                //    data["queryRequest"] = "|1000756|23380762618491910380";
                //    data["aggregatorId"] = "SBIEPAY";
                //    data["merchantId"] = "1000756";
                //    CommonMethod.WritetoNotepad(null, HttpContext.Request.Url.AbsolutePath, "DoubleVerificationURl MID:--" + "1000756");
                //    var response1 = wb.UploadValues("https://www.sbiepay.com/payagg/orderStatusQuery/getOrderStatusQuery", "POST", data);
                //    CommonMethod.WritetoNotepad(null, HttpContext.Request.Url.AbsolutePath, "DoubleVerificationURl " + response1);

                //}
                //Call Send SMS API
                //string sendSMSUri = "https://test.sbiepay.com/payagg/orderStatusQuery/getOrderStatusQuery";
                ////Create HTTPWebrequest
                //HttpWebRequest httpWReq = (HttpWebRequest)WebRequest.Create(sendSMSUri);
                ////Prepare and Add URL Encoded data
                //UTF8Encoding encoding = new UTF8Encoding();
                //byte[] data = encoding.GetBytes(sbPostData.ToString());
                ////Specify post method
                //httpWReq.Method = "POST";
                //httpWReq.ContentType = "application/x-www-form-urlencoded";
                //httpWReq.ContentLength = data.Length;
                //using (Stream stream = httpWReq.GetRequestStream())
                //{

                //    stream.Write(data, 0, data.Length);
                //}
                ////Get the response
                //HttpWebResponse response = (HttpWebResponse)httpWReq.GetResponse();
                //StreamReader reader = new StreamReader(response.GetResponseStream());
                //string responseString = reader.ReadToEnd();
                //CommonMethod.WritetoNotepad(null, HttpContext.Request.Url.AbsolutePath, "DoubleVerificationURl admission Paymentgateway Pushresponse  url hit on Controller error"+ responseString);

                ////Close the response
                //reader.Close();
                //response.Close();
            }
            catch (SystemException ex)
            {
                CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "DoubleVerificationURl admission Paymentgateway Pushresponse  url hit on Controller error");

            }
            return View();
        }
        [HttpPost]
        public ActionResult DoubleVerificationURl(int id = 0, string trxid = "", string password = "", string mid = "")
        {



            try
            {
                if (password != "Arora@321")
                {
                    TempData["responseString"] = "Wrong Password !!";
                    return View();
                }
                CommonMethod.WritetoNotepad(null, HttpContext.Request.Url.AbsolutePath, "DoubleVerificationURl " + "Breakpreetam heelo 1000756");
                string URI = "https://www.sbiepay.sbi/payagg/orderStatusQuery/getOrderStatusQuery";
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                var request = (HttpWebRequest)WebRequest.Create(URI);
                var postData = "queryRequest=|" + mid + "|" + trxid;
                postData += "&aggregatorId=SBIEPAY";
                postData += "&merchantId=" + mid + "";
                var data = Encoding.ASCII.GetBytes(postData);
                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";
                request.ContentLength = data.Length;
                using (var stream = request.GetRequestStream())
                {
                    stream.Write(data, 0, data.Length);
                }
                var response = (HttpWebResponse)request.GetResponse();

                //var responseString = "1000762|7092094895308|SUCCESS|IN|INR|MU40217991|78200463011141959420|1744|Transaction Paid Out|sbiepay|201918153234514|2019-06-30 16:48:41|DC|10007622019063000167|1000762|0.00^0.00||||||||||";// new StreamReader(response.GetResponseStream()).ReadToEnd();

                var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();

                TempData["responseString"] = responseString;

                SbiepayAdmission sbi = new SbiepayAdmission();
                var result = sbi.doubleverification(responseString);
                CommonMethod.WritetoNotepad(null, HttpContext.Request.Url.AbsolutePath, "DoubleVerificationURl " + "Break" + responseString);

                //Uri address = new Uri("https://test.sbiepay.com/payagg/orderStatusQuery/getOrderStatusQuery");

                //ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
                //ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3;

                //System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
                //StringBuilder sbPostData = new StringBuilder();
                //sbPostData.AppendFormat("queryRequest={0}", "|1000733|23380762618491910380");
                //sbPostData.AppendFormat("&aggregatorId={0}", "SBIEPAY");
                //sbPostData.AppendFormat("&merchantId={0}", "1000733");
                //using (WebClient webClient = new WebClient())
                //{
                //    var stream = webClient.OpenRead(address);
                //    using (StreamReader sr = new StreamReader(stream))
                //    {
                //        var page = sr.ReadToEnd();

                //        // return page;
                //    }
                //}
                //CommonMethod.WritetoNotepad(null, HttpContext.Request.Url.AbsolutePath, "DoubleVerificationURl " + "Breakpreetam heelo 1000733");
                //WebRequest webRequest = WebRequest.Create(@"https://www.sbiepay.com/payagg/orderStatusQuery/getOrderStatusQuery");
                //webRequest.ContentType = "text/html";
                //webRequest.Method = "POST";
                //string body = "...";
                //byte[] bytes = Encoding.ASCII.GetBytes(body);
                //webRequest.ContentLength = bytes.Length;
                //var os = webRequest.GetRequestStream();
                //os.Write(bytes, 0, bytes.Length);
                //os.Close();
                //webRequest.Timeout = 0; //setting the timeout to 0 causes the request to fail
                //WebResponse webResponse = webRequest.GetResponse(); //Exception thrown here ...
                //CommonMethod.WritetoNotepad(null, HttpContext.Request.Url.AbsolutePath, "DoubleVerificationURl " + "Breakpreetam heelo "+ webResponse);
                //return RedirectToAction("Index");
                //using (var wb = new WebClient())
                //{


                //    var data = new NameValueCollection();
                //    data["queryRequest"] = "|1000733|42067162616181939343";
                //    data["aggregatorId"] = "SBIEPAY";
                //    data["merchantId"] = "1000733";
                //    CommonMethod.WritetoNotepad(null, HttpContext.Request.Url.AbsolutePath, "DoubleVerificationURl MID:--" + "1000733");
                //    var response1 = wb.UploadValues("https://www.sbiepay.com/payagg/orderStatusQuery/getOrderStatusQuery", "POST", data);
                //    CommonMethod.WritetoNotepad(null, HttpContext.Request.Url.AbsolutePath, "DoubleVerificationURl " + response1);

                //}

                //using (var wb = new WebClient())
                //{
                //    var data = new NameValueCollection();
                //    data["queryRequest"] = "|1000003|2639506614091939495";
                //    data["aggregatorId"] = "SBIEPAY";
                //    data["merchantId"] = "1000003";
                //    CommonMethod.WritetoNotepad(null, HttpContext.Request.Url.AbsolutePath, "DoubleVerificationURl MID:--" + "1000003");
                //    var response1 = wb.UploadValues("https://www.sbiepay.com/payagg/orderStatusQuery/getOrderStatusQuery", "POST", data);
                //    CommonMethod.WritetoNotepad(null, HttpContext.Request.Url.AbsolutePath, "DoubleVerificationURl " + response1);

                //}
                // return RedirectToAction("Index");

                //using (var wb = new WebClient())
                //{
                //    var data = new NameValueCollection();
                //    data["queryRequest"] = "|1000756|23380762618491910380";
                //    data["aggregatorId"] = "SBIEPAY";
                //    data["merchantId"] = "1000756";
                //    CommonMethod.WritetoNotepad(null, HttpContext.Request.Url.AbsolutePath, "DoubleVerificationURl MID:--" + "1000756");
                //    var response1 = wb.UploadValues("https://www.sbiepay.com/payagg/orderStatusQuery/getOrderStatusQuery", "POST", data);
                //    CommonMethod.WritetoNotepad(null, HttpContext.Request.Url.AbsolutePath, "DoubleVerificationURl " + response1);

                //}
                //Call Send SMS API
                //string sendSMSUri = "https://test.sbiepay.com/payagg/orderStatusQuery/getOrderStatusQuery";
                ////Create HTTPWebrequest
                //HttpWebRequest httpWReq = (HttpWebRequest)WebRequest.Create(sendSMSUri);
                ////Prepare and Add URL Encoded data
                //UTF8Encoding encoding = new UTF8Encoding();
                //byte[] data = encoding.GetBytes(sbPostData.ToString());
                ////Specify post method
                //httpWReq.Method = "POST";
                //httpWReq.ContentType = "application/x-www-form-urlencoded";
                //httpWReq.ContentLength = data.Length;
                //using (Stream stream = httpWReq.GetRequestStream())
                //{

                //    stream.Write(data, 0, data.Length);
                //}
                ////Get the response
                //HttpWebResponse response = (HttpWebResponse)httpWReq.GetResponse();
                //StreamReader reader = new StreamReader(response.GetResponseStream());
                //string responseString = reader.ReadToEnd();
                //CommonMethod.WritetoNotepad(null, HttpContext.Request.Url.AbsolutePath, "DoubleVerificationURl admission Paymentgateway Pushresponse  url hit on Controller error"+ responseString);

                ////Close the response
                //reader.Close();
                //response.Close();
            }
            catch (SystemException ex)
            {
                CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "DoubleVerificationURl admission Paymentgateway Pushresponse  url hit on Controller error");
                return View();
            }
            return View();
        }

        [HttpPost]
        public ActionResult DoubleVerificationURlsee(int id = 0, string trxid = "", string password = "", string mid = "")
        {
            try
            {
                if (password != "Arora@321")
                {
                    TempData["responseString"] = "Wrong Password !!";
                    return View();
                }
                CommonMethod.WritetoNotepad(null, HttpContext.Request.Url.AbsolutePath, "DoubleVerificationURl " + "Breakpreetam heelo 1000756");
                string URI = "https://www.sbiepay.sbi/payagg/orderStatusQuery/getOrderStatusQuery";
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                var request = (HttpWebRequest)WebRequest.Create(URI);
                var postData = "queryRequest=|" + mid + "|" + trxid;
                postData += "&aggregatorId=SBIEPAY";
                postData += "&merchantId=" + mid + "";
                var data = Encoding.ASCII.GetBytes(postData);
                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";
                request.ContentLength = data.Length;
                using (var stream = request.GetRequestStream())
                {
                    stream.Write(data, 0, data.Length);
                }
                var response = (HttpWebResponse)request.GetResponse();

                //var responseString = "1000762|7092094895308|SUCCESS|IN|INR|MU40217991|78200463011141959420|1744|Transaction Paid Out|sbiepay|201918153234514|2019-06-30 16:48:41|DC|10007622019063000167|1000762|0.00^0.00||||||||||";// new StreamReader(response.GetResponseStream()).ReadToEnd();
                var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();

                TempData["responseString"] = responseString;
                return RedirectToAction("DoubleVerificationURl");
                //SbiepayAdmission sbi = new SbiepayAdmission();
                //var result = sbi.doubleverification(responseString);
                //CommonMethod.WritetoNotepad(null, HttpContext.Request.Url.AbsolutePath, "DoubleVerificationURl " + "Break" + responseString);

                //Uri address = new Uri("https://test.sbiepay.com/payagg/orderStatusQuery/getOrderStatusQuery");

                //ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
                //ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3;

                //System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
                //StringBuilder sbPostData = new StringBuilder();
                //sbPostData.AppendFormat("queryRequest={0}", "|1000733|23380762618491910380");
                //sbPostData.AppendFormat("&aggregatorId={0}", "SBIEPAY");
                //sbPostData.AppendFormat("&merchantId={0}", "1000733");
                //using (WebClient webClient = new WebClient())
                //{
                //    var stream = webClient.OpenRead(address);
                //    using (StreamReader sr = new StreamReader(stream))
                //    {
                //        var page = sr.ReadToEnd();

                //        // return page;
                //    }
                //}
                //CommonMethod.WritetoNotepad(null, HttpContext.Request.Url.AbsolutePath, "DoubleVerificationURl " + "Breakpreetam heelo 1000733");
                //WebRequest webRequest = WebRequest.Create(@"https://www.sbiepay.com/payagg/orderStatusQuery/getOrderStatusQuery");
                //webRequest.ContentType = "text/html";
                //webRequest.Method = "POST";
                //string body = "...";
                //byte[] bytes = Encoding.ASCII.GetBytes(body);
                //webRequest.ContentLength = bytes.Length;
                //var os = webRequest.GetRequestStream();
                //os.Write(bytes, 0, bytes.Length);
                //os.Close();
                //webRequest.Timeout = 0; //setting the timeout to 0 causes the request to fail
                //WebResponse webResponse = webRequest.GetResponse(); //Exception thrown here ...
                //CommonMethod.WritetoNotepad(null, HttpContext.Request.Url.AbsolutePath, "DoubleVerificationURl " + "Breakpreetam heelo "+ webResponse);
                //return RedirectToAction("Index");
                //using (var wb = new WebClient())
                //{


                //    var data = new NameValueCollection();
                //    data["queryRequest"] = "|1000733|42067162616181939343";
                //    data["aggregatorId"] = "SBIEPAY";
                //    data["merchantId"] = "1000733";
                //    CommonMethod.WritetoNotepad(null, HttpContext.Request.Url.AbsolutePath, "DoubleVerificationURl MID:--" + "1000733");
                //    var response1 = wb.UploadValues("https://www.sbiepay.com/payagg/orderStatusQuery/getOrderStatusQuery", "POST", data);
                //    CommonMethod.WritetoNotepad(null, HttpContext.Request.Url.AbsolutePath, "DoubleVerificationURl " + response1);

                //}

                //using (var wb = new WebClient())
                //{
                //    var data = new NameValueCollection();
                //    data["queryRequest"] = "|1000003|2639506614091939495";
                //    data["aggregatorId"] = "SBIEPAY";
                //    data["merchantId"] = "1000003";
                //    CommonMethod.WritetoNotepad(null, HttpContext.Request.Url.AbsolutePath, "DoubleVerificationURl MID:--" + "1000003");
                //    var response1 = wb.UploadValues("https://www.sbiepay.com/payagg/orderStatusQuery/getOrderStatusQuery", "POST", data);
                //    CommonMethod.WritetoNotepad(null, HttpContext.Request.Url.AbsolutePath, "DoubleVerificationURl " + response1);

                //}
                // return RedirectToAction("Index");

                //using (var wb = new WebClient())
                //{
                //    var data = new NameValueCollection();
                //    data["queryRequest"] = "|1000756|23380762618491910380";
                //    data["aggregatorId"] = "SBIEPAY";
                //    data["merchantId"] = "1000756";
                //    CommonMethod.WritetoNotepad(null, HttpContext.Request.Url.AbsolutePath, "DoubleVerificationURl MID:--" + "1000756");
                //    var response1 = wb.UploadValues("https://www.sbiepay.com/payagg/orderStatusQuery/getOrderStatusQuery", "POST", data);
                //    CommonMethod.WritetoNotepad(null, HttpContext.Request.Url.AbsolutePath, "DoubleVerificationURl " + response1);

                //}
                //Call Send SMS API
                //string sendSMSUri = "https://test.sbiepay.com/payagg/orderStatusQuery/getOrderStatusQuery";
                ////Create HTTPWebrequest
                //HttpWebRequest httpWReq = (HttpWebRequest)WebRequest.Create(sendSMSUri);
                ////Prepare and Add URL Encoded data
                //UTF8Encoding encoding = new UTF8Encoding();
                //byte[] data = encoding.GetBytes(sbPostData.ToString());
                ////Specify post method
                //httpWReq.Method = "POST";
                //httpWReq.ContentType = "application/x-www-form-urlencoded";
                //httpWReq.ContentLength = data.Length;
                //using (Stream stream = httpWReq.GetRequestStream())
                //{

                //    stream.Write(data, 0, data.Length);
                //}
                ////Get the response
                //HttpWebResponse response = (HttpWebResponse)httpWReq.GetResponse();
                //StreamReader reader = new StreamReader(response.GetResponseStream());
                //string responseString = reader.ReadToEnd();
                //CommonMethod.WritetoNotepad(null, HttpContext.Request.Url.AbsolutePath, "DoubleVerificationURl admission Paymentgateway Pushresponse  url hit on Controller error"+ responseString);

                ////Close the response
                //reader.Close();
                //response.Close();
            }
            catch (SystemException ex)
            {
                CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "DoubleVerificationURl admission Paymentgateway Pushresponse  url hit on Controller error");
                return RedirectToAction("DoubleVerificationURl");
            }
            return View();
        }
        //  Admin  Forgot Password  start

        //public ActionResult test()
        //{
        //    int i = 1;
        //    try
        //    {

        //        int a = 0, b = 10;
        //        int c = b / a;
        //    }
        //    catch (Exception ex)
        //    {

        //        CommonMethod.WritetoNotepad(ex,"test", "error", i.ToString());
        //        i++;
        //    }

        //    return View();
        //}


        public ActionResult DoubleVerificationURlRegistration()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DoubleVerificationURlRegistration(int id = 0, string trxid = "", string password = "", string mid = "")
        {
            try
            {
                if (password != "Arora@321")
                {
                    TempData["responseString"] = "Wrong Password !!";
                    return View();
                }
                Recruitment re = new Recruitment();
                DataTable dt = new DataTable();
                dt = re.Sendbulksms();
                int a = 0;
                //return View(); ;
                foreach (DataRow item in dt.Rows)
                {
                    mid = "1000694";
                    trxid = item["clienttrxid"].ToString();
                    CommonMethod.WritetoNotepad(null, HttpContext.Request.Url.AbsolutePath, "DoubleVerificationURlRegistration " + "Breakpreetam heelo ");
                    string URI = "https://www.sbiepay.sbi/payagg/orderStatusQuery/getOrderStatusQuery";
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                    var request = (HttpWebRequest)WebRequest.Create(URI);
                    var postData = "queryRequest=|" + mid + "|" + trxid;
                    postData += "&aggregatorId=SBIEPAY";
                    postData += "&merchantId=" + mid + "";
                    var data = Encoding.ASCII.GetBytes(postData);
                    request.Method = "POST";
                    request.ContentType = "application/x-www-form-urlencoded";
                    request.ContentLength = data.Length;
                    using (var stream = request.GetRequestStream())
                    {
                        stream.Write(data, 0, data.Length);
                    }
                    var response = (HttpWebResponse)request.GetResponse();

                    var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
                    TempData["responseString"] = responseString;
                    Sbiepay sbi = new Sbiepay();
                    // for save reord not repeat aganin
                    string connetionString = null;
                    SqlConnection sqlCnn;
                    SqlCommand sqlCmd;
                    SqlDataAdapter adapter = new SqlDataAdapter();
                    DataSet ds = new DataSet();
                    int i = a;
                    string sql = null;
                    connetionString = "Data Source=101.53.153.84;initial catalog=db_DemoUniversity1;user id=mungeruser;password=User%#$#lj45;MultipleActiveResultSets=True";
                    sqlCnn = new SqlConnection(connetionString);
                    SqlCommand cmd = new SqlCommand("insert into [tempsms2]  (mobileno,applicationno,transactionid,type) values(@mobileno,@applicationo,@transactionid,@type)", sqlCnn);
                    cmd.Parameters.AddWithValue("@mobileno", item["sid"].ToString());
                    cmd.Parameters.AddWithValue("@applicationo", item["clienttrxid"].ToString());
                    cmd.Parameters.AddWithValue("@transactionid", item["clienttrxid"].ToString());
                    cmd.Parameters.AddWithValue("@type", 12);
                    sqlCnn.Open();
                    cmd.ExecuteNonQuery();
                    sqlCnn.Close();
                    a++;
                    // end for save reord not repeat aganin
                    var result = sbi.doubleverificationregistration(responseString);
                    CommonMethod.WritetoNotepad(null, HttpContext.Request.Url.AbsolutePath, "DoubleVerificationURl " + "Break" + responseString);
                    //System.Threading.Thread.Sleep(10);
                }

            }
            catch (SystemException ex)
            {
                CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "DoubleVerificationURl admission Paymentgateway Pushresponse  url hit on Controller error");
                return View();
            }
            return View();
        }
        [HttpPost]
        public ActionResult DoubleVerificationURlseeRegistration(int id = 0, string trxid = "", string password = "", string mid = "")
        {
            try
            {
                if (password != "Arora@321")
                {
                    TempData["responseString"] = "Wrong Password !!";
                    return View();
                }
                mid = "1000694";
                CommonMethod.WritetoNotepad(null, HttpContext.Request.Url.AbsolutePath, "DoubleVerificationURlseeRegistration " + "Breakpreetam heelo ");
                string URI = "https://www.sbiepay.sbi/payagg/orderStatusQuery/getOrderStatusQuery";
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                var request = (HttpWebRequest)WebRequest.Create(URI);
                var postData = "queryRequest=|" + mid + "|" + trxid;
                postData += "&aggregatorId=SBIEPAY";
                postData += "&merchantId=" + mid + "";
                var data = Encoding.ASCII.GetBytes(postData);
                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";
                request.ContentLength = data.Length;
                using (var stream = request.GetRequestStream())
                {
                    stream.Write(data, 0, data.Length);
                }
                var response = (HttpWebResponse)request.GetResponse();

                //var responseString = "1000762|7092094895308|SUCCESS|IN|INR|MU40217991|78200463011141959420|1744|Transaction Paid Out|sbiepay|201918153234514|2019-06-30 16:48:41|DC|10007622019063000167|1000762|0.00^0.00||||||||||";// new StreamReader(response.GetResponseStream()).ReadToEnd();
                var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();

                TempData["responseString"] = responseString;
                return RedirectToAction("DoubleVerificationURlRegistration");

            }
            catch (SystemException ex)
            {
                CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "DoubleVerificationURl admission Paymentgateway Pushresponse  url hit on Controller error");
                return View();
            }
            return View();
        }
        [HttpPost]
        public ActionResult DoubleVerificationURlsingleRegistration(int id = 0, string trxid = "", string password = "", string mid = "")
        {



            try
            {
                if (password != "Arora@321")
                {
                    TempData["responseString"] = "Wrong Password !!";
                    return View();
                }
                mid = "1000694";
                CommonMethod.WritetoNotepad(null, HttpContext.Request.Url.AbsolutePath, "DoubleVerificationURlseeRegistration " + "Breakpreetam heelo ");
                string URI = "https://www.sbiepay.sbi/payagg/orderStatusQuery/getOrderStatusQuery";
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                var request = (HttpWebRequest)WebRequest.Create(URI);
                var postData = "queryRequest=|" + mid + "|" + trxid;
                postData += "&aggregatorId=SBIEPAY";
                postData += "&merchantId=" + mid + "";
                var data = Encoding.ASCII.GetBytes(postData);
                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";
                request.ContentLength = data.Length;
                using (var stream = request.GetRequestStream())
                {
                    stream.Write(data, 0, data.Length);
                }
                var response = (HttpWebResponse)request.GetResponse();

                //var responseString = "1000762|7092094895308|SUCCESS|IN|INR|MU40217991|78200463011141959420|1744|Transaction Paid Out|sbiepay|201918153234514|2019-06-30 16:48:41|DC|10007622019063000167|1000762|0.00^0.00||||||||||";// new StreamReader(response.GetResponseStream()).ReadToEnd();
                var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
                Sbiepay sbi = new Sbiepay();
                var result = sbi.doubleverificationregistration(responseString);

                TempData["responseString"] = responseString;
                return RedirectToAction("DoubleVerificationURlRegistration");

            }
            catch (SystemException ex)
            {
                CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "DoubleVerificationURl admission Paymentgateway Pushresponse  url hit on Controller error");
                return View();
            }
            return View();
        }

        public ActionResult DoubleVerificationURlEnroll()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DoubleVerificationURlEnroll(int id = 0, string trxid = "", string password = "", string mid = "")
        {
            try
            {
                if (password != "Arora@321")
                {
                    TempData["responseString"] = "Wrong Password !!";
                    return View();
                }
                Recruitment re = new Recruitment();
                DataTable dt = new DataTable();
                dt = re.Getpendingpaymentenrollment();
                int a = 0;
                //return View();
                SbiPayEnrollment sbi = new SbiPayEnrollment();
                foreach (DataRow item in dt.Rows)
                {
                    mid = sbi.MID;
                    trxid = item["clienttrxid"].ToString();
                    //CommonMethod.WritetoNotepad(null, HttpContext.Request.Url.AbsolutePath, "DoubleVerificationURlEnroll " + "Breakpreetam heelo ");
                    string URI = "https://www.sbiepay.sbi/payagg/orderStatusQuery/getOrderStatusQuery";
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                    var request = (HttpWebRequest)WebRequest.Create(URI);
                    var postData = "queryRequest=|" + mid + "|" + trxid;
                    postData += "&aggregatorId=SBIEPAY";
                    postData += "&merchantId=" + mid + "";
                    var data = Encoding.ASCII.GetBytes(postData);
                    request.Method = "POST";
                    request.ContentType = "application/x-www-form-urlencoded";
                    request.ContentLength = data.Length;
                    using (var stream = request.GetRequestStream())
                    {
                        stream.Write(data, 0, data.Length);
                    }
                    var response = (HttpWebResponse)request.GetResponse();

                    var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
                    TempData["responseString"] = responseString;

                    var result = sbi.doubleverificationregistration(responseString);
                    //CommonMethod.WritetoNotepad(null, HttpContext.Request.Url.AbsolutePath, "DoubleVerificationURlEnroll " + "Break" + responseString);
                    System.Threading.Thread.Sleep(800);
                }

            }
            catch (SystemException ex)
            {
                CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "DoubleVerificationURl admission Paymentgateway Pushresponse  url hit on Controller error");
                return View();
            }
            return View();
        }
        [HttpPost]
        public ActionResult DoubleVerificationURlEnrollSee(int id = 0, string trxid = "", string password = "", string mid = "")
        {



            try
            {
                if (password != "Arora@321")
                {
                    TempData["responseString"] = "Wrong Password !!";
                    return View();
                }

                SbiPayEnrollment sbi = new SbiPayEnrollment();
                mid = sbi.MID;
                CommonMethod.WritetoNotepad(null, HttpContext.Request.Url.AbsolutePath, "DoubleVerificationURlEnroll " + "Breakpreetam heelo ");
                string URI = "https://www.sbiepay.sbi/payagg/orderStatusQuery/getOrderStatusQuery";
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                var request = (HttpWebRequest)WebRequest.Create(URI);
                var postData = "queryRequest=|" + mid + "|" + trxid;
                postData += "&aggregatorId=SBIEPAY";
                postData += "&merchantId=" + mid + "";
                var data = Encoding.ASCII.GetBytes(postData);
                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";
                request.ContentLength = data.Length;
                using (var stream = request.GetRequestStream())
                {
                    stream.Write(data, 0, data.Length);
                }
                var response = (HttpWebResponse)request.GetResponse();

                //var responseString = "1000762|7092094895308|SUCCESS|IN|INR|MU40217991|78200463011141959420|1744|Transaction Paid Out|sbiepay|201918153234514|2019-06-30 16:48:41|DC|10007622019063000167|1000762|0.00^0.00||||||||||";// new StreamReader(response.GetResponseStream()).ReadToEnd();
                var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();

                TempData["responseString"] = responseString;
                return RedirectToAction("DoubleVerificationURlEnroll");

            }
            catch (SystemException ex)
            {
                CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "DoubleVerificationURlEnroll admission Paymentgateway Pushresponse  url hit on Controller error");
                return RedirectToAction("DoubleVerificationURlEnroll");
            }
            return RedirectToAction("DoubleVerificationURlEnroll");
        }
        public ActionResult DoubleVerificationURlExam()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DoubleVerificationURlExam(int id = 0, string trxid = "", string password = "", string mid = "")
        {
            try
            {
                if (password != "Arora@321")
                {
                    TempData["responseString"] = "Wrong Password !!";
                    return View();
                }
                Recruitment re = new Recruitment();
                DataTable dt = new DataTable();
                dt = re.GetpendingpaymentExam();
                int a = 0;
                //  return View();
                var responseString = "";
                SbiPayExam sbi = new SbiPayExam();
                foreach (DataRow item in dt.Rows)
                {
                    // mid = "";
                    mid = "1000858";
                    trxid = item["clienttrxid"].ToString();
                    CommonMethod.WritetoNotepad(null, HttpContext.Request.Url.AbsolutePath, "DoubleVerificationURlExam " + "Breakpreetam heelo ");
                    string URI = "https://www.sbiepay.sbi/payagg/orderStatusQuery/getOrderStatusQuery";
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                    var request = (HttpWebRequest)WebRequest.Create(URI);
                    var postData = "queryRequest=|" + mid + "|" + trxid;
                    postData += "&aggregatorId=SBIEPAY";
                    postData += "&merchantId=" + mid + "";
                    var data = Encoding.ASCII.GetBytes(postData);
                    request.Method = "POST";
                    request.ContentType = "application/x-www-form-urlencoded";
                    request.ContentLength = data.Length;
                    using (var stream = request.GetRequestStream())
                    {
                        stream.Write(data, 0, data.Length);
                    }
                    var response = (HttpWebResponse)request.GetResponse();

                    responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
                    //responseString += "<BR>";
                    //Response.Write("---------------------");
                    //Response.Write("---------------------");

                    var result = sbi.doubleverificationregistration(responseString);
                    //CommonMethod.WritetoNotepad(null, HttpContext.Request.Url.AbsolutePath, "DoubleVerificationURlExam " + "Break" + responseString);
                    System.Threading.Thread.Sleep(2000);
                }
                TempData["responseString"] = responseString;

            }
            catch (SystemException ex)
            {
                CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "DoubleVerificationURl Exam Paymentgateway Pushresponse  url hit on Controller error");
                return View();
            }
            return View();
        }

        public ActionResult DoubleVerificationURlExamByAppliRollno()
        {
            return View();
        }

        [HttpPost]
        public ActionResult DoubleVerificationURlExamByAppliRollno(int id = 0, string trxid = "", string password = "", string mid = "", string RollNo = "", string ApplicationNo = "")
        {
            try
            {
                if (password != "123")
                {
                    TempData["responseString"] = "Wrong Password !!";
                    return View();
                }
                if (RollNo == "" && ApplicationNo == "")
                {
                    TempData["responseString"] = "Enter Roll No or Application No";
                    return View();
                }
                Recruitment re = new Recruitment();
                DataTable dt = new DataTable();

                if (RollNo != "")
                {
                    dt = re.GetpendingpaymentExamByRollNo(RollNo);
                }
                else
                {
                    dt = re.GetpendingpaymentExamByApplicationNo(ApplicationNo);
                }

                int a = 0;
                //  return View();
                var responseString = "";
                SbiPayExam sbi = new SbiPayExam();
                foreach (DataRow item in dt.Rows)
                {
                    // mid = "";
                    mid = "1000858";
                    trxid = item["clienttrxid"].ToString();
                    CommonMethod.WritetoNotepad(null, HttpContext.Request.Url.AbsolutePath, "DoubleVerificationURlExam " + "Breakpreetam heelo ");
                    string URI = "https://www.sbiepay.sbi/payagg/orderStatusQuery/getOrderStatusQuery";
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                    var request = (HttpWebRequest)WebRequest.Create(URI);
                    var postData = "queryRequest=|" + mid + "|" + trxid;
                    postData += "&aggregatorId=SBIEPAY";
                    postData += "&merchantId=" + mid + "";
                    var data = Encoding.ASCII.GetBytes(postData);
                    request.Method = "POST";
                    request.ContentType = "application/x-www-form-urlencoded";
                    request.ContentLength = data.Length;
                    using (var stream = request.GetRequestStream())
                    {
                        stream.Write(data, 0, data.Length);
                    }
                    var response = (HttpWebResponse)request.GetResponse();

                    responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
                    //responseString += "<BR>";
                    //Response.Write("---------------------");
                    //Response.Write("---------------------");

                    var result = sbi.doubleverificationregistration(responseString);
                    //CommonMethod.WritetoNotepad(null, HttpContext.Request.Url.AbsolutePath, "DoubleVerificationURlExam " + "Break" + responseString);
                    System.Threading.Thread.Sleep(2000);
                }
                TempData["responseString"] = responseString;

            }
            catch (SystemException ex)
            {
                CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "DoubleVerificationURl Exam Paymentgateway Pushresponse  url hit on Controller error");
                return View();
            }
            return View();
        }

        //public ActionResult DoubleVerificationURlExamAirpay()
        //{
        //    return View();
        //}
        //[HttpPost]
        //public ActionResult DoubleVerificationURlExamAirPay(int id = 0, string trxid = "", string password = "", string mid = "")
        //{
        //    try
        //    {
        //        //if (password != "Arora@321")
        //        //{
        //        //    TempData["responseString"] = "Wrong Password !!";
        //        //    return View();
        //        //}
        //        Recruitment re = new Recruitment();
        //        DataTable dt = new DataTable();
        //        dt = re.GetpendingpaymentExam();
        //        int a = 0;
        //        //  return View();
        //        var responseString = "";
        //        SbiPayExam sbi = new SbiPayExam();
        //        foreach (DataRow item in dt.Rows)
        //        {
        //            // mid = "";
        //            mid = "1000858";
        //            trxid = item["clienttrxid"].ToString();
        //            CommonMethod.WritetoNotepad(null, HttpContext.Request.Url.AbsolutePath, "DoubleVerificationURlExam " + "Breakpreetam heelo ");
        //            string URI = "https://www.sbiepay.sbi/payagg/orderStatusQuery/getOrderStatusQuery";
        //            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
        //            var request = (HttpWebRequest)WebRequest.Create(URI);
        //            var postData = "queryRequest=|" + mid + "|" + trxid;
        //            postData += "&aggregatorId=SBIEPAY";
        //            postData += "&merchantId=" + mid + "";
        //            var data = Encoding.ASCII.GetBytes(postData);
        //            request.Method = "POST";
        //            request.ContentType = "application/x-www-form-urlencoded";
        //            request.ContentLength = data.Length;
        //            using (var stream = request.GetRequestStream())
        //            {
        //                stream.Write(data, 0, data.Length);
        //            }
        //            var response = (HttpWebResponse)request.GetResponse();

        //            responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
        //            //responseString += "<BR>";
        //            //Response.Write("---------------------");
        //            //Response.Write("---------------------");

        //            var result = sbi.doubleverificationregistration(responseString);
        //            //CommonMethod.WritetoNotepad(null, HttpContext.Request.Url.AbsolutePath, "DoubleVerificationURlExam " + "Break" + responseString);
        //            System.Threading.Thread.Sleep(2000);
        //        }
        //        TempData["responseString"] = responseString;

        //    }
        //    catch (SystemException ex)
        //    {
        //        CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "DoubleVerificationURl Exam Paymentgateway Pushresponse  url hit on Controller error");
        //        return View();
        //    }
        //    return View();
        //}

        public ActionResult DoubleVerificationURlExamAirpay()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DoubleVerificationURlExamAirPay(int id = 0, string trxid = "", string password = "", string mid = "")
        {
            try
            {
                if (password != "123")
                {
                    TempData["responseString"] = "Wrong Password !!";
                    return View();
                }
                Recruitment re = new Recruitment();
                DataTable dt = new DataTable();
                dt = re.GetpendingpaymentExamAirPay();
                int a = 0;
                //  return View();
                var responseString = "";
                SbiPayExam sbi = new SbiPayExam();
                foreach (DataRow item in dt.Rows)
                {
                    // mid = "";
                    string mercid = "256750";
                    string usernam = "3242993";
                    string passwor = "m9vKj5Nf";
                    string secretKey = "yg53qEswvaUvFEFj";
                    //mid = "1000858";
                    trxid = item["clienttrxid"].ToString();
                    var k = secretKey + "@" + usernam + ":|:" + passwor;
                    //var k = secretKey + usernam + ".~:~." + passwor;
                    // var sTemp = secretKey + "@" + usernam + "~:~" + passwor;
                    SbiPayExam sb = new SbiPayExam();
                    var pkey = sb.EncryptSHA256Managed(k);
                    //var pkey = sb.EncryptSHA256Managed(sTemp);
                    // var alldata = mercid + trxid + pkey;

                    string URI = "https://payments.airpay.co.in/order/verify.php";
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                    var request = (HttpWebRequest)WebRequest.Create(URI);
                    var postData = "mercid=" + mercid;
                    postData += "&merchant_txnId=" + trxid;
                    postData += "&privatekey=" + pkey;

                    var data = Encoding.ASCII.GetBytes(postData);
                    request.Method = "POST";
                    request.ContentType = "application/x-www-form-urlencoded";
                    request.ContentLength = data.Length;
                    using (var stream = request.GetRequestStream())
                    {
                        stream.Write(data, 0, data.Length);
                    }
                    var response = (HttpWebResponse)request.GetResponse();

                    responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
                    //responseString += "<BR>";
                    //Response.Write("---------------------");
                    //Response.Write("---------------------");

                    //XmlDocument doc = new XmlDocument();
                    //doc.LoadXml(responseString);
                    //responseString = JsonConvert.SerializeXmlNode(doc);

                    var result = sbi.doubleverificationregistrationAirPay(responseString);
                    //CommonMethod.WritetoNotepad(null, HttpContext.Request.Url.AbsolutePath, "DoubleVerificationURlExam " + "Break" + responseString);
                    System.Threading.Thread.Sleep(2000);
                }
                TempData["responseString"] = responseString;

            }
            catch (SystemException ex)
            {
                CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "DoubleVerificationURl Exam Paymentgateway Pushresponse  url hit on Controller error");
                return View();
            }
            return View();
        }

        [HttpPost]
        public ActionResult DoubleVerificationURlExamAirPayByAppliRollno(int id = 0, string trxid = "", string password = "", string mid = "", string RollNo = "", string ApplicationNo = "")
        {
            try
            {
                if (password != "123")
                {
                    TempData["responseString"] = "Wrong Password !!";
                    return View();
                }
                if (RollNo == "" && ApplicationNo == "")
                {
                    TempData["responseString"] = "Enter Roll No or Application No";
                    return RedirectToAction("DoubleVerificationURlExamAirpay");
                }

                Recruitment re = new Recruitment();
                DataTable dt = new DataTable();

                if (RollNo != "")
                {
                    dt = re.GetpendingpaymentExamAirPayByRollNo(RollNo);
                }
                else
                {
                    dt = re.GetpendingpaymentExamAirPayByApplicationNo(ApplicationNo);
                }

                int a = 0;
                //  return View();
                var responseString = "";
                SbiPayExam sbi = new SbiPayExam();
                foreach (DataRow item in dt.Rows)
                {
                    // mid = "";
                    string mercid = "256750";
                    string usernam = "3242993";
                    string passwor = "m9vKj5Nf";
                    string secretKey = "yg53qEswvaUvFEFj";
                    //mid = "1000858";
                    trxid = item["clienttrxid"].ToString();
                    var k = secretKey + "@" + usernam + ":|:" + passwor;
                    //var k = secretKey + usernam + ".~:~." + passwor;
                    // var sTemp = secretKey + "@" + usernam + "~:~" + passwor;
                    SbiPayExam sb = new SbiPayExam();
                    var pkey = sb.EncryptSHA256Managed(k);
                    //var pkey = sb.EncryptSHA256Managed(sTemp);
                    // var alldata = mercid + trxid + pkey;
                    CommonMethod.WritetoNotepad(null, HttpContext.Request.Url.AbsolutePath, "DoubleVerificationURlExamAirPay " + "Breakpreetam heelo ");
                    string URI = "https://payments.airpay.co.in/order/verify.php";
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                    var request = (HttpWebRequest)WebRequest.Create(URI);
                    var postData = "mercid=" + mercid;
                    postData += "&merchant_txnId=" + trxid;
                    postData += "&privatekey=" + pkey;

                    var data = Encoding.ASCII.GetBytes(postData);
                    request.Method = "POST";
                    request.ContentType = "application/x-www-form-urlencoded";
                    request.ContentLength = data.Length;
                    using (var stream = request.GetRequestStream())
                    {
                        stream.Write(data, 0, data.Length);
                    }
                    var response = (HttpWebResponse)request.GetResponse();

                    responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
                    //responseString += "<BR>";
                    //Response.Write("---------------------");
                    //Response.Write("---------------------");

                    //XmlDocument doc = new XmlDocument();
                    //doc.LoadXml(responseString);
                    //responseString = JsonConvert.SerializeXmlNode(doc);

                    var result = sbi.doubleverificationregistrationAirPay(responseString);
                    //CommonMethod.WritetoNotepad(null, HttpContext.Request.Url.AbsolutePath, "DoubleVerificationURlExam " + "Break" + responseString);
                    System.Threading.Thread.Sleep(2000);
                }
                TempData["responseString"] = responseString;

            }
            catch (SystemException ex)
            {
                CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "DoubleVerificationURl Exam Paymentgateway Pushresponse  url hit on Controller error");
                return RedirectToAction("DoubleVerificationURlExamAirpay");
            }
            return RedirectToAction("DoubleVerificationURlExamAirpay");
        }

        public ActionResult DoubleVerificationURlExamEasebuzz()
        {
            return View();
        }


        public ActionResult DoubleVerificationURlEnrolleent_Easebuzz()
        {
            return View();
        }

        [HttpPost]
        public ActionResult DoubleVerificationURlEnrolleent_Easebuzz(int id = 0, string trxid = "", string password = "", string mid = "")
        {
            try
            {
                if (password != "123")
                {
                    TempData["responseString"] = "Wrong Password !!";
                    return View();
                }
                Recruitment re = new Recruitment();
                DataTable dt = new DataTable();
                dt = re.GetpendingpaymentEnrollment_Easebuzz();
                int a = 0;
                //  return View();
                var responseString = "";
                SbiPayExam sbi = new SbiPayExam();
                foreach (DataRow item in dt.Rows)
                {

                    try
                    {
                        // mid = "";
                        mid = "22TBG2E6BD";
                        var salt = "6RK2GGSBWK";
                        string env = "prod";

                        trxid = item["clienttrxid"].ToString();

                        var emailclient = item["Email"].ToString();
                        var phoneclient = item["MobileNo"].ToString();
                        string amount = item["amount"].ToString();
                        amount = Convert.ToDecimal(amount).ToString("0.0");
                        var applicationno = item["applicationno"].ToString();


                        //var textstr = mid + "|" + trxid + "|" + f1 + "|" + emailclient + "|" + phoneclient + "|" + salt;
                        SbiPayExam ss = new SbiPayExam();
                        //var sha = ss.Easebuzz_Generatehash512("textstr");


                        System.Collections.Hashtable data = new System.Collections.Hashtable();
                        data.Add("key", mid);
                        data.Add("txnid", trxid);
                        data.Add("amount", amount);
                        data.Add("email", emailclient);
                        data.Add("phone", phoneclient);

                        // generate hash
                        string[] hashVarsSeq = "key|txnid|amount|email|phone".Split('|'); // spliting hash sequence from config
                        string hash_string = "";
                        foreach (string hash_var in hashVarsSeq)
                        {
                            hash_string = hash_string + (data.ContainsKey(hash_var) ? data[hash_var].ToString() : "");
                            hash_string = hash_string + '|';
                        }
                        hash_string += salt;// appending SALT
                                            //Console.WriteLine(hash_string);
                        var gen_hash = ss.Easebuzz_Generatehash512(hash_string);     //generating hash
                        data.Add("hash", gen_hash);

                        string url = "https://dashboard.easebuzz.in/transaction/v1/retrieve";
                        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                        var request = (HttpWebRequest)WebRequest.Create(url);

                        var postData = "txnid=" + trxid;
                        postData += "&amount=" + amount;
                        postData += "&email=" + emailclient;
                        postData += "&phone=" + phoneclient;
                        postData += "&key=" + mid;
                        postData += "&hash=" + gen_hash;

                        var Ndata = Encoding.ASCII.GetBytes(postData);

                        request.Method = "POST";
                        request.ContentType = "application/x-www-form-urlencoded";
                        request.ContentLength = Ndata.Length;

                        using (var stream = request.GetRequestStream())
                        {
                            stream.Write(Ndata, 0, Ndata.Length);
                        }

                        var response = (HttpWebResponse)request.GetResponse();

                        responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
                        //responseString += "<BR>";
                        //Response.Write("---------------------");
                        //Response.Write("---------------------");

                        var result = sbi.doubleverificationEnrollment_EaseBuzz(responseString);
                        //CommonMethod.WritetoNotepad(null, HttpContext.Request.Url.AbsolutePath, "DoubleVerificationURlExam " + "Break" + responseString);
                        System.Threading.Thread.Sleep(2000);
                    }
                    catch { }
                }
                TempData["responseString"] = responseString;

            }
            catch (SystemException ex)
            {
                CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "DoubleVerificationURl Exam Paymentgateway Pushresponse  url hit on Controller error");
                return View();
            }
            return View();
        }



        [HttpPost]
        public ActionResult DoubleVerificationURlExamEasebuzz(int id = 0, string trxid = "", string password = "", string mid = "")
        {
            try
            {
                if (password != "123")
                {
                    TempData["responseString"] = "Wrong Password !!";
                    return View();
                }
                Recruitment re = new Recruitment();
                DataTable dt = new DataTable();
                dt = re.GetpendingpaymentExamEasebuzz();
                int a = 0;
                //  return View();
                var responseString = "";
                SbiPayExam sbi = new SbiPayExam();
                foreach (DataRow item in dt.Rows)
                {
                    // mid = "";
                    mid = System.Configuration.ConfigurationSettings.AppSettings["key"];
                    var salt = System.Configuration.ConfigurationSettings.AppSettings["salt"];
                    string env = System.Configuration.ConfigurationSettings.AppSettings["env"];
                    trxid = item["clienttrxid"].ToString();

                    var emailclient = item["Email"].ToString();
                    var phoneclient = item["MobileNo"].ToString();
                    string amount = item["feeamount"].ToString();
                    amount = Convert.ToDecimal(amount).ToString("0.0");
                    var applicationno = item["applicationno"].ToString();
                    var courseyesrid = item["StudentYear"].ToString();
                    var isback = item["is_back"].ToString();

                    //var textstr = mid + "|" + trxid + "|" + f1 + "|" + emailclient + "|" + phoneclient + "|" + salt;
                    SbiPayExam ss = new SbiPayExam();
                    //var sha = ss.Easebuzz_Generatehash512("textstr");


                    System.Collections.Hashtable data = new System.Collections.Hashtable();
                    data.Add("key", mid);
                    data.Add("txnid", trxid);
                    data.Add("amount", amount);
                    data.Add("email", emailclient);
                    data.Add("phone", phoneclient);

                    // generate hash
                    string[] hashVarsSeq = "key|txnid|amount|email|phone".Split('|'); // spliting hash sequence from config
                    string hash_string = "";
                    foreach (string hash_var in hashVarsSeq)
                    {
                        hash_string = hash_string + (data.ContainsKey(hash_var) ? data[hash_var].ToString() : "");
                        hash_string = hash_string + '|';
                    }
                    hash_string += salt;// appending SALT
                                        //Console.WriteLine(hash_string);
                    var gen_hash = ss.Easebuzz_Generatehash512(hash_string);     //generating hash
                    data.Add("hash", gen_hash);

                    string url = "https://dashboard.easebuzz.in/transaction/v1/retrieve";
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                    var request = (HttpWebRequest)WebRequest.Create(url);

                    var postData = "txnid=" + trxid;
                    postData += "&amount=" + amount;
                    postData += "&email=" + emailclient;
                    postData += "&phone=" + phoneclient;
                    postData += "&key=" + mid;
                    postData += "&hash=" + gen_hash;

                    var Ndata = Encoding.ASCII.GetBytes(postData);

                    request.Method = "POST";
                    request.ContentType = "application/x-www-form-urlencoded";
                    request.ContentLength = Ndata.Length;

                    using (var stream = request.GetRequestStream())
                    {
                        stream.Write(Ndata, 0, Ndata.Length);
                    }

                    var response = (HttpWebResponse)request.GetResponse();

                    responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
                    //responseString += "<BR>";
                    //Response.Write("---------------------");
                    //Response.Write("---------------------");

                    var result = sbi.doubleverificationregistrationEaseBuzz(responseString);
                    //CommonMethod.WritetoNotepad(null, HttpContext.Request.Url.AbsolutePath, "DoubleVerificationURlExam " + "Break" + responseString);
                    System.Threading.Thread.Sleep(2000);
                }
                TempData["responseString"] = responseString;

            }
            catch (SystemException ex)
            {
                CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "DoubleVerificationURl Exam Paymentgateway Pushresponse  url hit on Controller error");
                return View();
            }
            return View();
        }

        [HttpPost]
        public ActionResult DoubleVerificationURlExamEasebuzzByAppliRollno(int id = 0, string trxid = "", string password = "", string mid = "", string RollNo = "", string ApplicationNo = "")
        {
            try
            {
                if (password != "123")
                {
                    TempData["responseString"] = "Wrong Password !!";
                    //return View();
                    return RedirectToAction("DoubleVerificationURlExamEasebuzz");
                }
                if (RollNo == "" && ApplicationNo == "")
                {
                    TempData["responseString"] = "Enter Roll No or Application No";
                    return RedirectToAction("DoubleVerificationURlExamEasebuzz");
                }

                Recruitment re = new Recruitment();
                DataTable dt = new DataTable();
                if (RollNo != "")
                {
                    dt = re.GetpendingpaymentExamEasebuzzByRollNo(RollNo);
                }
                else
                {
                    dt = re.GetpendingpaymentExamEasebuzzByApplicationNo(ApplicationNo);
                }

                int a = 0;
                //  return View();
                var responseString = "";
                SbiPayExam sbi = new SbiPayExam();
                foreach (DataRow item in dt.Rows)
                {
                    // mid = "";
                    mid = System.Configuration.ConfigurationSettings.AppSettings["key"];
                    var salt = System.Configuration.ConfigurationSettings.AppSettings["salt"];
                    string env = System.Configuration.ConfigurationSettings.AppSettings["env"];
                    trxid = item["clienttrxid"].ToString();

                    var emailclient = item["Email"].ToString();
                    var phoneclient = item["MobileNo"].ToString();
                    string amount = item["feeamount"].ToString();
                    amount = Convert.ToDecimal(amount).ToString("0.0");
                    var applicationno = item["applicationno"].ToString();
                    var courseyesrid = item["StudentYear"].ToString();
                    var isback = item["is_back"].ToString();

                    //var textstr = mid + "|" + trxid + "|" + f1 + "|" + emailclient + "|" + phoneclient + "|" + salt;
                    SbiPayExam ss = new SbiPayExam();
                    //var sha = ss.Easebuzz_Generatehash512("textstr");


                    System.Collections.Hashtable data = new System.Collections.Hashtable();
                    data.Add("key", mid);
                    data.Add("txnid", trxid);
                    data.Add("amount", amount);
                    data.Add("email", emailclient);
                    data.Add("phone", phoneclient);

                    // generate hash
                    string[] hashVarsSeq = "key|txnid|amount|email|phone".Split('|'); // spliting hash sequence from config
                    string hash_string = "";
                    foreach (string hash_var in hashVarsSeq)
                    {
                        hash_string = hash_string + (data.ContainsKey(hash_var) ? data[hash_var].ToString() : "");
                        hash_string = hash_string + '|';
                    }
                    hash_string += salt;// appending SALT
                                        //Console.WriteLine(hash_string);
                    var gen_hash = ss.Easebuzz_Generatehash512(hash_string);     //generating hash
                    data.Add("hash", gen_hash);

                    string url = "https://dashboard.easebuzz.in/transaction/v1/retrieve";
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                    var request = (HttpWebRequest)WebRequest.Create(url);

                    var postData = "txnid=" + trxid;
                    postData += "&amount=" + amount;
                    postData += "&email=" + emailclient;
                    postData += "&phone=" + phoneclient;
                    postData += "&key=" + mid;
                    postData += "&hash=" + gen_hash;

                    var Ndata = Encoding.ASCII.GetBytes(postData);

                    request.Method = "POST";
                    request.ContentType = "application/x-www-form-urlencoded";
                    request.ContentLength = Ndata.Length;

                    using (var stream = request.GetRequestStream())
                    {
                        stream.Write(Ndata, 0, Ndata.Length);
                    }

                    var response = (HttpWebResponse)request.GetResponse();

                    responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
                    //responseString += "<BR>";
                    //Response.Write("---------------------");
                    //Response.Write("---------------------");

                    var result = sbi.doubleverificationregistrationEaseBuzz(responseString);
                    //CommonMethod.WritetoNotepad(null, HttpContext.Request.Url.AbsolutePath, "DoubleVerificationURlExam " + "Break" + responseString);
                    System.Threading.Thread.Sleep(2000);
                }
                TempData["responseString"] = responseString;

            }
            catch (SystemException ex)
            {
                CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "DoubleVerificationURl Exam Paymentgateway Pushresponse  url hit on Controller error");
                return RedirectToAction("DoubleVerificationURlExamEasebuzz");
            }
            return RedirectToAction("DoubleVerificationURlExamEasebuzz");
        }

        public ActionResult DoubleVerificationURlExamSafex()
        {
            return View();
        }
        [HttpPost]

        public ActionResult DoubleVerificationURlExamSafex(int id = 0, string trxid = "", string password = "", string mid = "")
        {
            try
            {
                if (password != "123")
                {
                    TempData["responseString"] = "Wrong Password !!";
                    return View();
                }
                Recruitment re = new Recruitment();
                DataTable dt = new DataTable();
                dt = re.Sp_DoubleVerification_Admission_Safex();
                int a = 0;
                //  return View();
                var responseString = "";
                SbiPayExam sbi = new SbiPayExam();
                foreach (DataRow item in dt.Rows)
                {
                    //mid = "";

                    mid = System.Configuration.ConfigurationSettings.AppSettings["merchant_id"];
                    trxid = item["clienttrxid"].ToString();
                    var agref = item["apitrxid"].ToString();
                    var applicationno = item["applicationno"].ToString();
                    var courseyesrid = item["StudentYear"].ToString();
                    var isback = item["is_back"].ToString();
                    MyCryptoClass cls = new MyCryptoClass();
                    var enagref = cls.encrypt(agref);
                    var enctrxid = cls.encrypt(trxid);

                    CommonMethod.WritetoNotepad(null, HttpContext.Request.Url.AbsolutePath, "DoubleVerificationURlExam " + "Breakpreetam heelo ");

                    safexdoubleverify model = new safexdoubleverify();
                    model.ag_id = System.Configuration.ConfigurationSettings.AppSettings["ag_id"];
                    model.me_id = mid;
                    model.ag_ref = enagref;
                    model.order_no = enctrxid;


                    // var dat = JsonConvert.SerializeObject(model);

                    string URI = "https://www.avantgardepayments.com/agcore/api/query/post";
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                    var request = (HttpWebRequest)WebRequest.Create(URI);
                    var dat = JsonConvert.SerializeObject(model);
                    byte[] data = Encoding.ASCII.GetBytes(dat);
                    request.Method = "POST";
                    request.ContentType = "application/json";
                    request.ContentLength = data.Length;
                    using (var stream = request.GetRequestStream())
                    {
                        stream.Write(data, 0, data.Length);
                    }
                    var response = (HttpWebResponse)request.GetResponse();

                    responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();

                    //responseString += "<BR>";
                    //Response.Write("---------------------");
                    //Response.Write("---------------------");

                    //MyCryptoClass aes = new MyCryptoClass();
                    //string enc_txn_response = (!String.IsNullOrEmpty(responseString)) ? responseString : string.Empty;
                    //string txn_response = aes.decrypt(enc_txn_response);

                    var result = sbi.doubleverificationregistrationSafex(responseString);
                    CommonMethod.WritetoNotepad(null, HttpContext.Request.Url.AbsolutePath, "DoubleVerificationURlExamSafex " + "Break" + responseString);
                    System.Threading.Thread.Sleep(2000);
                }
                TempData["responseString"] = responseString;

            }
            catch (SystemException ex)
            {
                CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "DoubleVerificationURl Exam Paymentgateway Pushresponse  url hit on Controller error");
                return View();
            }
            return View();
        }


        public ActionResult DoubleVerificationAdmission_Safex()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DoubleVerificationAdmission_Safex(int id = 0, string trxid = "", string password = "", string mid = "")

        {
            try
            {
                if (password != "123")
                {
                    TempData["responseString"] = "Wrong Password !!";
                    return View();
                }
                Recruitment re = new Recruitment();
                DataTable dt = new DataTable();
                dt = re.Sp_DoubleVerification_Admission_Safex();
                int a = 0;
                //  return View();
                var responseString = "";
                SbiPayExam sbi = new SbiPayExam();
                foreach (DataRow item in dt.Rows)
                {
                    try
                    {
                        string cmid = CommonMethod.MIDcollegewiseSafex().Where(x => x.collegeid == Convert.ToInt32(item["collegeid"])).FirstOrDefault().mid;
                        string ckey = CommonMethod.MIDcollegewiseSafex().Where(x => x.collegeid == Convert.ToInt32(item["collegeid"])).FirstOrDefault().mkey;

                        //mid = "";
                        Areas.Student.Models.SbiepayAdmission.MyCryptoClass aes = new Areas.Student.Models.SbiepayAdmission.MyCryptoClass();
                        mid = cmid;
                        trxid = item["clienttrxid"].ToString();
                        var agref = item["apitrxid"].ToString();
                        //var applicationno = item["applicationno"].ToString();
                        //var courseyesrid = item["StudentYear"].ToString();                 

                        var enagref = aes.encrypt(agref, ckey);
                        var enctrxid = aes.encrypt(trxid, ckey);


                        safexdoubleverify model = new safexdoubleverify();
                        model.ag_id = "Paygate";
                        model.me_id = cmid;
                        model.ag_ref = enagref;
                        model.order_no = enctrxid;
                        // var dat = JsonConvert.SerializeObject(model);

                        string URI = "https://www.avantgardepayments.com/agcore/api/query/post";
                        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                        var request = (HttpWebRequest)WebRequest.Create(URI);
                        var dat = JsonConvert.SerializeObject(model);
                        byte[] data = Encoding.ASCII.GetBytes(dat);
                        request.Method = "POST";
                        request.ContentType = "application/json";
                        request.ContentLength = data.Length;
                        using (var stream = request.GetRequestStream())
                        {
                            stream.Write(data, 0, data.Length);
                        }
                        var response = (HttpWebResponse)request.GetResponse();

                        responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();

                        //responseString += "<BR>";
                        //Response.Write("---------------------");
                        //Response.Write("---------------------");

                        //MyCryptoClass aes = new MyCryptoClass();
                        //string enc_txn_response = (!String.IsNullOrEmpty(responseString)) ? responseString : string.Empty;
                        //string txn_response = aes.decrypt(enc_txn_response);

                        var result = SafexPayUGSucessAdmission(responseString, item["collegeid"].ToString());
                    }
                    catch
                    {

                    }

                    System.Threading.Thread.Sleep(2000);
                }
                TempData["responseString"] = responseString;

            }
            catch (SystemException ex)
            {
                CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "DoubleVerificationURl Exam Paymentgateway Pushresponse  url hit on Controller error");
                return View();
            }
            return View();
        }




        public ActionResult DoubleVerificationAdmission_EaseBuzz()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DoubleVerificationAdmission_EaseBuzz(string Amount = "", string application = "")
        {
            try
            {
                //if (password != "123")
                //{
                //    TempData["responseString"] = "Wrong Password !!";
                //    return View();
                //}
                Recruitment re = new Recruitment();
                DataTable dt = new DataTable();

                dt = re.Sp_DoubleVerification_Admission_Comman("", "EaseBuzzbyApplicationno", application);

                int a = 0;
                //  return View();
                var responseString = "";
                SbiPayExam sbi = new SbiPayExam();
                foreach (DataRow item in dt.Rows)
                {
                    // mid = "80265",mkey = "IKZ0BZ2TK7" ,Salt = "2ONXNJ4YEA"
                    //string cmid = CommonMethod.MIDcollegewiseSafex().Where(x => x.collegeid == Convert.ToInt32(item["collegeid"])).FirstOrDefault().mid;
                    //string ckey = CommonMethod.MIDcollegewiseSafex().Where(x => x.collegeid == Convert.ToInt32(item["collegeid"])).FirstOrDefault().mkey;


                    // mid = "";
                    string mid = "IKZ0BZ2TK7";
                    var salt = "2ONXNJ4YEA";
                    string env = "prod";

                    string trxid = item["clienttrxid"].ToString();

                    var emailclient = item["Email"].ToString();
                    var phoneclient = item["MobileNo"].ToString();
                    string amount = Amount;
                    amount = Convert.ToDecimal(amount).ToString("0.0");
                    var applicationno = item["applicationno"].ToString();


                    //var textstr = mid + "|" + trxid + "|" + f1 + "|" + emailclient + "|" + phoneclient + "|" + salt;
                    SbiPayExam ss = new SbiPayExam();
                    //var sha = ss.Easebuzz_Generatehash512("textstr");


                    System.Collections.Hashtable data = new System.Collections.Hashtable();
                    data.Add("key", mid);
                    data.Add("txnid", trxid);
                    data.Add("amount", amount);
                    data.Add("email", emailclient);
                    data.Add("phone", phoneclient);

                    // generate hash
                    string[] hashVarsSeq = "key|txnid|amount|email|phone".Split('|'); // spliting hash sequence from config
                    string hash_string = "";
                    foreach (string hash_var in hashVarsSeq)
                    {
                        hash_string = hash_string + (data.ContainsKey(hash_var) ? data[hash_var].ToString() : "");
                        hash_string = hash_string + '|';
                    }
                    hash_string += salt;// appending SALT
                                        //Console.WriteLine(hash_string);
                    var gen_hash = ss.Easebuzz_Generatehash512(hash_string);     //generating hash
                    data.Add("hash", gen_hash);

                    string url = "https://dashboard.easebuzz.in/transaction/v1/retrieve";
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                    var request = (HttpWebRequest)WebRequest.Create(url);

                    var postData = "txnid=" + trxid;
                    postData += "&amount=" + amount;
                    postData += "&email=" + emailclient;
                    postData += "&phone=" + phoneclient;
                    postData += "&key=" + mid;
                    postData += "&hash=" + gen_hash;

                    var Ndata = Encoding.ASCII.GetBytes(postData);

                    request.Method = "POST";
                    request.ContentType = "application/x-www-form-urlencoded";
                    request.ContentLength = Ndata.Length;

                    using (var stream = request.GetRequestStream())
                    {
                        stream.Write(Ndata, 0, Ndata.Length);
                    }

                    var response = (HttpWebResponse)request.GetResponse();

                    responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
                    //responseString += "<BR>";
                    //Response.Write("---------------------");
                    //Response.Write("---------------------");

                    var result = EaseBuzzPayUGSucessAdmission(responseString, "");
                    //CommonMethod.WritetoNotepad(null, HttpContext.Request.Url.AbsolutePath, "DoubleVerificationURlExam " + "Break" + responseString);
                    System.Threading.Thread.Sleep(2000);
                }
                TempData["responseString"] = responseString;

            }
            catch (SystemException ex)
            {
                CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "DoubleVerificationURl Exam Paymentgateway Pushresponse  url hit on Controller error");
                return View();
            }
            return View();
        }



        public ActionResult DoubleVerificationAdmission_comman()
        {
            return View();
        }
        [HttpPost]
        //public ActionResult DoubleVerificationAdmission_Airpay( string CollegeId )
        //{
        //    try
        //    {
        //        //if (password != "123")
        //        //{
        //        //    TempData["responseString"] = "Wrong Password !!";
        //        //    return View();
        //        //}
        //        doubleverificationgetstudent obj211 = new doubleverificationgetstudent();
        //        SbiepayAdmission sb = new SbiepayAdmission();
        //        Recruitment re = new Recruitment();
        //        DataTable dt = new DataTable();
        //        UserLogin objlogin = new UserLogin();

        //        dt = re.Sp_DoubleVerification_Admission_Comman("", "AirPay", "");


        //        int a = 0;
        //        //  return View();
        //        var responseString = "";
        //        SbiPayExam sbi = new SbiPayExam();
        //        foreach (DataRow item in dt.Rows)
        //        {
        //            try
        //            {
        //                string cmid = CommonMethod.MIDcollegewiseAirPay().Where(x => x.collegeid == Convert.ToInt32(item["collegeid"])).FirstOrDefault().mid;
        //                string cusername = CommonMethod.MIDcollegewiseAirPay().Where(x => x.collegeid == Convert.ToInt32(item["collegeid"])).FirstOrDefault().UserName;
        //                string cpassword = CommonMethod.MIDcollegewiseAirPay().Where(x => x.collegeid == Convert.ToInt32(item["collegeid"])).FirstOrDefault().Password;
        //                string ckey = CommonMethod.MIDcollegewiseAirPay().Where(x => x.collegeid == Convert.ToInt32(item["collegeid"])).FirstOrDefault().mkey;

        //                string mercid = cmid;
        //                string usernam = cusername;
        //                string passwor = cpassword;
        //                string secretKey = ckey;

        //                var k = secretKey + "@" + usernam + ":|:" + passwor;
        //                //mid = "1000858";
        //                var pkey = sb.EncryptSHA256Managed(k);
        //                string trxid = item["clienttrxid"].ToString();
        //                // CommonMethod.WritetoNotepad(null, HttpContext.Request.Url.AbsolutePath, "DoubleVerificationURlRegistration " + "Breakpreetam heelo ");
        //                string URI = "https://payments.airpay.co.in/order/verify.php";
        //                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
        //                var request = (HttpWebRequest)WebRequest.Create(URI);
        //                var postData = "mercid=" + mercid;
        //                postData += "&merchant_txnId=" + trxid;
        //                postData += "&privatekey=" + pkey;
        //                // postData += "&airpayId=" + pkey;
        //                var data = Encoding.ASCII.GetBytes(postData);
        //                request.Method = "POST";
        //                request.ContentType = "application/x-www-form-urlencoded";
        //                request.ContentLength = data.Length;
        //                using (var stream = request.GetRequestStream())
        //                {
        //                    stream.Write(data, 0, data.Length);
        //                }

        //                var response = (HttpWebResponse)request.GetResponse();
        //                var responseString1 = new StreamReader(response.GetResponseStream()).ReadToEnd();


        //                var result = sb.EaseBuzzPayUGSucessAdmission(responseString1);
        //         }
        //            catch
        //            {

        //            }

        //            System.Threading.Thread.Sleep(2000);
        //        }
        //        TempData["responseString"] = responseString;

        //    }
        //    catch (SystemException ex)
        //    {
        //        CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "DoubleVerificationURl Exam Paymentgateway Pushresponse  url hit on Controller error");
        //        return View();
        //    }
        //    return View();
        //}


        //public ActionResult DoubleVerificationAdmission_Airpay()
        //{

        //    try
        //    {
        //        string ApplicationNO = ClsLanguage.GetCookies("NBApplicationNo");
        //        doubleverificationgetstudent obj211 = new doubleverificationgetstudent();
        //        SbiepayAdmission sb = new SbiepayAdmission();
        //        Recruitment re = new Recruitment();
        //        DataTable dt = new DataTable();
        //        UserLogin objlogin = new UserLogin();
        //        var obj = objlogin.Sp_DoubleVerification_Admission_Comman(obj211, ApplicationNO, "AirPay");
        //        int a = 0;
        //        string mid = "";
        //        string trxid = "";
        //        foreach (var item in obj)
        //        {

        //            string cmid = CommonMethod.MIDcollegewiseAirPay().Where(x => x.collegeid == item.collegeid).FirstOrDefault().mid;
        //            string cusername = CommonMethod.MIDcollegewiseAirPay().Where(x => x.collegeid == item.collegeid).FirstOrDefault().UserName;
        //            string cpassword = CommonMethod.MIDcollegewiseAirPay().Where(x => x.collegeid == item.collegeid).FirstOrDefault().Password;
        //            string ckey = CommonMethod.MIDcollegewiseAirPay().Where(x => x.collegeid == item.collegeid).FirstOrDefault().mkey;

        //            string mercid = cmid;
        //            string usernam = cusername;
        //            string passwor = cpassword;
        //            string secretKey = ckey;

        //            var k = secretKey + "@" + usernam + ":|:" + passwor;
        //            //mid = "1000858";
        //            var pkey = sb.EncryptSHA256Managed(k);
        //            trxid = item.clienttrxid;
        //            // CommonMethod.WritetoNotepad(null, HttpContext.Request.Url.AbsolutePath, "DoubleVerificationURlRegistration " + "Breakpreetam heelo ");
        //            string URI = "https://payments.airpay.co.in/order/verify.php";
        //            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
        //            var request = (HttpWebRequest)WebRequest.Create(URI);
        //            var postData = "mercid=" + mercid;
        //            postData += "&merchant_txnId=" + trxid;
        //            postData += "&privatekey=" + pkey;
        //            // postData += "&airpayId=" + pkey;
        //            var data = Encoding.ASCII.GetBytes(postData);
        //            request.Method = "POST";
        //            request.ContentType = "application/x-www-form-urlencoded";
        //            request.ContentLength = data.Length;
        //            using (var stream = request.GetRequestStream())
        //            {
        //                stream.Write(data, 0, data.Length);
        //            }

        //            var response = (HttpWebResponse)request.GetResponse();
        //            var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
        //            SbiepayAdmission sbi = new SbiepayAdmission();

        //            var result = sbi.doubleverificationAdmissionAirpay(responseString);

        //            //return RedirectToAction("BackExamFeesSubmit", new { @courseyearidenc = courseyearidenc });

        //            System.Threading.Thread.Sleep(10);
        //        }
        //    }
        //    catch (Exception ex)
        //    { }
        //    return View();
        //}



        public string SafexPayUGSucessAdmission(string data, string CollegeId)
        {

            var result = "";
            int ColId = Convert.ToInt32(CollegeId);
            //Add new
            Areas.Student.Models.SbiepayAdmission.MyCryptoClass aes = new Areas.Student.Models.SbiepayAdmission.MyCryptoClass();
            string privateKey = CommonMethod.MIDcollegewiseSafex().Where(x => x.collegeid == ColId).FirstOrDefault().mkey;
            string enc_txn_response = (!String.IsNullOrEmpty(data)) ? data : string.Empty;
            string txn_response = aes.decrypt(enc_txn_response, privateKey);

            SafexResponse dataobj = JsonConvert.DeserializeObject<SafexResponse>(txn_response);

            if (dataobj.txn_response != null && dataobj.other_details != null && dataobj.pg_details != null && dataobj.fraud_details != null)
            {


                string status = dataobj.txn_response.status;
                string pg_ref = dataobj.txn_response.pg_ref;
                string order_no = dataobj.txn_response.order_no;
                string amount = dataobj.txn_response.amount;
                string txn_date = dataobj.txn_response.txn_date;
                string txn_time = dataobj.txn_response.txn_time;
                string ag_ref = dataobj.txn_response.ag_ref;
                string res_message = dataobj.txn_response.res_message;

                string banktrxid = pg_ref;
                string clienttrxid = order_no;
                string amount1 = amount;
                string feeamount = amount;
                string gst = "0";
                string commission = "0";
                string banktxndate = txn_date + " " + txn_time;
                string Reason = res_message;
                string apitxnid = ag_ref;
                string ApplicationNo = dataobj.other_details.udf_1;
                string Requestdata = "";
                string dRequestdata = "";
                string PGstatus = dataobj.txn_response.status;
                string Sessionid = "";
                string examtype = dataobj.other_details.udf_2;

                string courseyearid = dataobj.other_details.udf_3;
                string AdmissionType = "";
                string paymode = "";

                string Sid = dataobj.other_details.udf_4;

                if (status == "Successful")
                {
                    SbiepayAdmission sbi = new SbiepayAdmission();
                    PGstatus = "Success";
                    status = "Success";
                    //var result = sbi.SafexPaypgsucessdecrypt(Convert.ToInt32(Sid), banktrxid, clienttrxid, amount1, feeamount, gst, commission, paymode, banktxndate, Reason, apitxnid, ApplicationNo, courseyearid, examType, Requestdata, dRequestdata, PGstatus);                 
                    result = sbi.AirPaypgsucessdecrypt(Convert.ToInt32(Sid), banktrxid, clienttrxid, amount1, feeamount, gst, commission, paymode, banktxndate, Reason, apitxnid, ApplicationNo, courseyearid, AdmissionType, Requestdata, dRequestdata, PGstatus, Sessionid); ;

                }


            }
            return result;
        }


        public string EaseBuzzPayUGSucessAdmission(string data, string CollegeId)
        {


            var result = "";
            string Salt = "2ONXNJ4YEA";
            Easebuzzresponse dataobj = JsonConvert.DeserializeObject<Easebuzzresponse>(data);

            SbiepayAdmission admission = new SbiepayAdmission();
            //return RedirectToAction("Index", "HomePG");
            string paramInfo = "";
            string[] merc_hash_vars_seq;
            string merc_hash_string = string.Empty;
            string merc_hash = string.Empty;
            string order_id = string.Empty;
            string hash_seq = "key|txnid|amount|productinfo|firstname|email|udf1|udf2|udf3|udf4|udf5|udf6|udf7|udf8|udf9|udf10";
            merc_hash_vars_seq = hash_seq.Split('|');
            Array.Reverse(merc_hash_vars_seq);
            merc_hash_string = Salt + "|" + dataobj.msg.status;
            foreach (string merc_hash_var in merc_hash_vars_seq)
            {
                merc_hash_string += "|";
                merc_hash_string = merc_hash_string + (Request.Form[merc_hash_var] != null ? Request.Form[merc_hash_var] : "");
            }

            merc_hash = admission.Easebuzz_Generatehash512(merc_hash_string).ToLower();

            if (merc_hash != dataobj.msg.hash)
            {

            }
            else
            {
                order_id = dataobj.msg.txnid;

                //Response.Write("value matched");
                if (dataobj.msg.status == "success")
                {
                    var respon = Request.Form;
                }
                else
                {

                }
                //Hash value did not matched
            }

            if (Request.Form.Count > 0)
            {
                try
                {
                    string TRANSACTIONSTATUS = dataobj.msg.status;
                    string MESSAGE = dataobj.msg.status;
                    string TRANSACTIONID = dataobj.msg.bank_ref_num;
                    string AMOUNT = dataobj.msg.amount;
                    string ap_SecureHash = dataobj.msg.easepayid;
                    string CHMOD = "Web";
                    DateTime now = DateTime.Now;
                    string MID = "";
                    string username = "";
                    string banktrxid = dataobj.msg.bank_ref_num;
                    string clienttrxid = "";
                    string amount1 = AMOUNT;
                    string feeamount = "0";
                    string gst = "0";
                    string error = dataobj.msg.error;
                    string commission = dataobj.msg.deduction_percentage;
                    string paymode = dataobj.msg.card_type;
                    string banktxndate = dataobj.msg.addedon;
                    string Reason = error;
                    string apitxnid = dataobj.msg.easepayid;
                    string ApplicationNo = "";
                    string courseyearid = "";
                    string AdmissionType = "";
                    string Requestdata = merc_hash_string;
                    string dRequestdata = merc_hash;
                    string PGstatus = MESSAGE;
                    string Sid = "0";
                    string Sessionid = "";

                    ApplicationNo = dataobj.msg.udf1;
                    clienttrxid = dataobj.msg.udf2;
                    AdmissionType = dataobj.msg.udf3;

                    //Sid = Request.Form["udf5"];
                    if (TRANSACTIONSTATUS.ToLower() == "success")
                    {
                        SbiepayAdmission sbi = new SbiepayAdmission();
                        result = sbi.AirPaypgsucessdecrypt(Convert.ToInt32(Sid), banktrxid, clienttrxid, amount1, feeamount, gst, commission, paymode, banktxndate, Reason, apitxnid, ApplicationNo, courseyearid, AdmissionType, Requestdata, dRequestdata, PGstatus, Sessionid);

                    }
                    else
                    {
                        SbiepayAdmission sbi = new SbiepayAdmission();
                        result = sbi.AirPaypgsucessdecrypt(Convert.ToInt32(Sid), banktrxid, clienttrxid, amount1, feeamount, gst, commission, paymode, banktxndate, Reason, apitxnid, ApplicationNo, courseyearid, AdmissionType, Requestdata, dRequestdata, PGstatus, Sessionid);
                        //var result = sbi.AirPaypgsucessdecrypt(Convert.ToInt32(Sid), banktrxid, clienttrxid, amount1, feeamount, gst, commission, paymode, banktxndate, Reason, apitxnid, ApplicationNo, courseyearid, examType, Requestdata, dRequestdata, TRANSACTIONSTATUS);

                    }
                }
                catch (Exception ex)
                {
                    CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, " Air Pay Payment PaymentGateway suucess url hit on Controller error for Exam Fees Submit", ClsLanguage.GetCookies("NBApplicationNo"));
                }
            }
            return result;
        }




        [HttpPost]
        public ActionResult DoubleVerificationURlExamSee(int id = 0, string trxid = "", string password = "", string mid = "")
        {



            try
            {
                if (password != "Arora@321")
                {
                    TempData["responseString"] = "Wrong Password !!";
                    return View();
                }

                SbiPayExam sbi = new SbiPayExam();
                mid = sbi.MID;
                CommonMethod.WritetoNotepad(null, HttpContext.Request.Url.AbsolutePath, "DoubleVerificationURlEnroll " + "Breakpreetam heelo ");
                string URI = "https://www.sbiepay.sbi/payagg/orderStatusQuery/getOrderStatusQuery";
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                var request = (HttpWebRequest)WebRequest.Create(URI);
                var postData = "queryRequest=|" + mid + "|" + trxid;
                postData += "&aggregatorId=SBIEPAY";
                postData += "&merchantId=" + mid + "";
                var data = Encoding.ASCII.GetBytes(postData);
                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";
                request.ContentLength = data.Length;
                using (var stream = request.GetRequestStream())
                {
                    stream.Write(data, 0, data.Length);
                }
                var response = (HttpWebResponse)request.GetResponse();

                //var responseString = "1000762|7092094895308|SUCCESS|IN|INR|MU40217991|78200463011141959420|1744|Transaction Paid Out|sbiepay|201918153234514|2019-06-30 16:48:41|DC|10007622019063000167|1000762|0.00^0.00||||||||||";// new StreamReader(response.GetResponseStream()).ReadToEnd();
                var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();

                TempData["responseString"] = responseString;
                return RedirectToAction("DoubleVerificationURlExam");

            }
            catch (SystemException ex)
            {
                CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "DoubleVerificationURlEnroll admission Paymentgateway Pushresponse  url hit on Controller error");
                return RedirectToAction("DoubleVerificationURlExam");
            }
            return RedirectToAction("DoubleVerificationURlExam");
        }
        public ActionResult DoubleVerification_CollegeFees()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DoubleVerification_CollegeFees(string Collegeid)
        {

            try
            {
                doubleverificationgetstudent obj211 = new doubleverificationgetstudent();
                UserLogin objlogin = new UserLogin();
                obj211.collegeid = !string.IsNullOrEmpty(Collegeid) ? Convert.ToInt32(Collegeid) : 0;
                var obj = objlogin.GetStudents_ForDoubleVerification(obj211);
                foreach (var item in obj)
                {
                    string mid = item.Mid;
                    string trxid = item.clienttrxid;
                    string sid = item.sid.ToString();
                    string applicationno = item.applicationno;



                    //if (password != "Arora@321")
                    //{
                    //    TempData["responseString"] = "Wrong Password !!";
                    //    return View();
                    //}

                    // CommonMethod.WritetoNotepad(null, HttpContext.Request.Url.AbsolutePath, "DoubleVerificationURl " + "Breakpreetam heelo 1000756");
                    string URI = "https://www.sbiepay.sbi/payagg/orderStatusQuery/getOrderStatusQuery";
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                    var request = (HttpWebRequest)WebRequest.Create(URI);
                    var postData = "queryRequest=|" + mid + "|" + trxid;
                    postData += "&aggregatorId=SBIEPAY";
                    postData += "&merchantId=" + mid + "";
                    var data = Encoding.ASCII.GetBytes(postData);
                    request.Method = "POST";
                    request.ContentType = "application/x-www-form-urlencoded";
                    request.ContentLength = data.Length;
                    using (var stream = request.GetRequestStream())
                    {
                        stream.Write(data, 0, data.Length);
                    }
                    var response = (HttpWebResponse)request.GetResponse();

                    //var responseString = "1000762|7092094895308|SUCCESS|IN|INR|MU40217991|78200463011141959420|1744|Transaction Paid Out|sbiepay|201918153234514|2019-06-30 16:48:41|DC|10007622019063000167|1000762|0.00^0.00||||||||||";// new StreamReader(response.GetResponseStream()).ReadToEnd();

                    var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
                    if (!string.IsNullOrEmpty(responseString))
                    {
                        string[] result11 = responseString.Split('|');
                        if (!string.IsNullOrEmpty(result11[2].ToLower()))
                        {
                            obj211.applicationno = item.applicationno;
                            obj211.sid = item.sid;
                            obj211.clienttrxid = item.clienttrxid;
                            obj211.paystatus = result11[2].ToLower();
                            int Returnvalue = objlogin.addtempdata(obj211);

                        }
                    }

                    TempData["responseString"] = responseString;

                    SbiepayAdmission sbi = new SbiepayAdmission();
                    var result = sbi.doubleverification(responseString);
                    System.Threading.Thread.Sleep(10);
                }
            }
            catch (Exception ex)
            { }
            return View();
        }

        public ActionResult coinbase()
        {

            string str = "{'name': 'The Sovereign Individual','description': 'Mastering the Transition to the Information Age','local_price': {'amount': '100.00','currency': 'USD'},'pricing_type': 'fixed_price','metadata': {'customer_id': 'id_1005','customer_name': 'Satoshi Nakamoto'},'redirect_url': 'https://charge/completed/page','cancel_url': 'https://charge/canceled/page'}";
            JObject json = JObject.Parse(str);
            return View();
        }
        [HttpPost]
        public ActionResult coinbase(string name, string vardescription, string price)
        {
            String jsonresponse = "";

            try
            {
                string Url = "https://api.commerce.coinbase.com/charges";

                //ServicePointManager.Expect100Continue = true;
                //ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                HttpWebRequest httpWReq = (HttpWebRequest)WebRequest.Create(Url);

                Encoding encoding = new UTF8Encoding();
                // string postData = "{\"data\":{\"number\":\"" + arrMobile + "\",\"text\":\"" + sms + "\"},\"port\":\"" + arrPort + "\"}}";  
                string postData = "{\"name\":\"" + name + "\",\"description\":\"" + vardescription + "\",\"pricing_type\":\"" + price + "\"}";

                byte[] data = encoding.GetBytes(postData);

                //httpWReq.ProtocolVersion = HttpVersion.Version11;
                httpWReq.Method = "POST";
                httpWReq.ContentType = "application/json"; //charset=UTF-8";  
                                                           //httpWReq.Headers.Add("X-Amzn-Type-Version",  
                                                           // "com.amazon.device.messaging.ADMMessage@1.0");  
                                                           //httpWReq.Headers.Add("X-Amzn-Accept-Type",  
                                                           // "com.amazon.device.messaging.ADMSendResult@1.0");  

                //string _auth = string.Format("{0}:{1}", userName, passWord);
                //string _enc = Convert.ToBase64String(Encoding.ASCII.GetBytes(_auth));
                //string _cred = string.Format("{0} {1}", "Basic", _enc);
                //httpWReq.Headers.Add(HttpRequestHeader.Authorization,  
                // "Bearer " + accessToken);  

                httpWReq.Headers["X-CC-Api-Key"] = "90d50ce8-4bd0-4024-afc3-7edf75a415f8";
                httpWReq.Headers["X-CC-Version"] = "2018-03-22";
                httpWReq.ContentType = "application/json";
                httpWReq.ContentLength = data.Length;


                Stream stream = httpWReq.GetRequestStream();
                stream.Write(data, 0, data.Length);
                stream.Close();

                HttpWebResponse response = (HttpWebResponse)httpWReq.GetResponse();
                string s = response.ToString();
                StreamReader reader = new StreamReader(response.GetResponseStream());

                String temp = null;
                while ((temp = reader.ReadLine()) != null)
                {
                    jsonresponse += temp;
                }
                Console.WriteLine(jsonresponse);

                var bsObj = JsonConvert.DeserializeObject<Root>(jsonresponse);
                // var result11 = JsonConvert.DeserializeObject<dynamic>(jsonresponse);
                string hosted_url = bsObj.data.hosted_url;
                string chargecode = bsObj.data.code;
                string bitcoinaddress = bsObj.data.addresses.bitcoin;





            }
            catch (WebException e)
            {


            }
            return View();
        }
        public ActionResult DoubleVerificationURlRegistration_spot()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DoubleVerificationURlRegistration_spot(int id = 0, string trxid = "", string password = "", string mid = "")
        {
            try
            {
                if (password != "Arora@321")
                {
                    TempData["responseString"] = "Wrong Password !!";
                    return View();
                }
                Recruitment re = new Recruitment();
                DataTable dt = new DataTable();
                dt = re.Sendbulksms_spot();
                int a = 0;
                //return View(); ;
                foreach (DataRow item in dt.Rows)
                {
                    mid = "1000694";
                    trxid = item["clienttrxid"].ToString();
                    CommonMethod.WritetoNotepad(null, HttpContext.Request.Url.AbsolutePath, "DoubleVerificationURlRegistration " + "Breakpreetam heelo ");
                    string URI = "https://www.sbiepay.sbi/payagg/orderStatusQuery/getOrderStatusQuery";
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                    var request = (HttpWebRequest)WebRequest.Create(URI);
                    var postData = "queryRequest=|" + mid + "|" + trxid;
                    postData += "&aggregatorId=SBIEPAY";
                    postData += "&merchantId=" + mid + "";
                    var data = Encoding.ASCII.GetBytes(postData);
                    request.Method = "POST";
                    request.ContentType = "application/x-www-form-urlencoded";
                    request.ContentLength = data.Length;
                    using (var stream = request.GetRequestStream())
                    {
                        stream.Write(data, 0, data.Length);
                    }
                    var response = (HttpWebResponse)request.GetResponse();

                    var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
                    TempData["responseString"] = responseString;
                    Sbiepay sbi = new Sbiepay();
                    // for save reord not repeat aganin
                    string connetionString = null;
                    SqlConnection sqlCnn;
                    SqlCommand sqlCmd;
                    SqlDataAdapter adapter = new SqlDataAdapter();
                    DataSet ds = new DataSet();
                    int i = 0;
                    string sql = null;
                    connetionString = "Data Source=101.53.153.84;initial catalog=db_DemoUniversity1;user id=mungeruser;password=User%#$#lj45;MultipleActiveResultSets=True";
                    sqlCnn = new SqlConnection(connetionString);
                    SqlCommand cmd = new SqlCommand("insert into [tempsms2]  (mobileno,applicationno,transactionid,type) values(@mobileno,@applicationo,@transactionid,@type)", sqlCnn);
                    cmd.Parameters.AddWithValue("@mobileno", item["sid"].ToString());
                    cmd.Parameters.AddWithValue("@applicationo", item["clienttrxid"].ToString());
                    cmd.Parameters.AddWithValue("@transactionid", item["clienttrxid"].ToString());
                    cmd.Parameters.AddWithValue("@type", 12);
                    sqlCnn.Open();
                    cmd.ExecuteNonQuery();
                    sqlCnn.Close();
                    // end for save reord not repeat aganin
                    var result = sbi.doubleverificationregistration_spot(responseString);
                    CommonMethod.WritetoNotepad(null, HttpContext.Request.Url.AbsolutePath, "DoubleVerificationURl " + "Break" + responseString);
                    System.Threading.Thread.Sleep(2000);
                }

            }
            catch (SystemException ex)
            {
                CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "DoubleVerificationURl admission Paymentgateway Pushresponse  url hit on Controller error");
                return View();
            }
            return View();
        }
        [HttpPost]
        public ActionResult DoubleVerificationURlseeRegistration_spot(int id = 0, string trxid = "", string password = "", string mid = "")
        {



            try
            {
                if (password != "Arora@321")
                {
                    TempData["responseString"] = "Wrong Password !!";
                    return View();
                }
                mid = "1000694";
                CommonMethod.WritetoNotepad(null, HttpContext.Request.Url.AbsolutePath, "DoubleVerificationURlseeRegistration " + "Breakpreetam heelo ");
                string URI = "https://www.sbiepay.sbi/payagg/orderStatusQuery/getOrderStatusQuery";
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                var request = (HttpWebRequest)WebRequest.Create(URI);
                var postData = "queryRequest=|" + mid + "|" + trxid;
                postData += "&aggregatorId=SBIEPAY";
                postData += "&merchantId=" + mid + "";
                var data = Encoding.ASCII.GetBytes(postData);
                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";
                request.ContentLength = data.Length;
                using (var stream = request.GetRequestStream())
                {
                    stream.Write(data, 0, data.Length);
                }
                var response = (HttpWebResponse)request.GetResponse();

                //var responseString = "1000762|7092094895308|SUCCESS|IN|INR|MU40217991|78200463011141959420|1744|Transaction Paid Out|sbiepay|201918153234514|2019-06-30 16:48:41|DC|10007622019063000167|1000762|0.00^0.00||||||||||";// new StreamReader(response.GetResponseStream()).ReadToEnd();
                var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();

                TempData["responseString"] = responseString;
                return RedirectToAction("DoubleVerificationURlRegistration");

            }
            catch (SystemException ex)
            {
                CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "DoubleVerificationURl admission Paymentgateway Pushresponse  url hit on Controller error");
                return View();
            }
            return View();
        }
        [HttpPost]
        public ActionResult DoubleVerificationURlsingleRegistration_spot(int id = 0, string trxid = "", string password = "", string mid = "")
        {



            try
            {
                if (password != "Arora@321")
                {
                    TempData["responseString"] = "Wrong Password !!";
                    return View();
                }
                mid = "1000694";
                CommonMethod.WritetoNotepad(null, HttpContext.Request.Url.AbsolutePath, "DoubleVerificationURlseeRegistration " + "Breakpreetam heelo ");
                string URI = "https://www.sbiepay.sbi/payagg/orderStatusQuery/getOrderStatusQuery";
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                var request = (HttpWebRequest)WebRequest.Create(URI);
                var postData = "queryRequest=|" + mid + "|" + trxid;
                postData += "&aggregatorId=SBIEPAY";
                postData += "&merchantId=" + mid + "";
                var data = Encoding.ASCII.GetBytes(postData);
                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";
                request.ContentLength = data.Length;
                using (var stream = request.GetRequestStream())
                {
                    stream.Write(data, 0, data.Length);
                }
                var response = (HttpWebResponse)request.GetResponse();

                //var responseString = "1000762|7092094895308|SUCCESS|IN|INR|MU40217991|78200463011141959420|1744|Transaction Paid Out|sbiepay|201918153234514|2019-06-30 16:48:41|DC|10007622019063000167|1000762|0.00^0.00||||||||||";// new StreamReader(response.GetResponseStream()).ReadToEnd();
                var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
                Sbiepay sbi = new Sbiepay();
                var result = sbi.doubleverificationregistration_spot(responseString);

                TempData["responseString"] = responseString;
                return RedirectToAction("DoubleVerificationURlRegistration");

            }
            catch (SystemException ex)
            {
                CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "DoubleVerificationURl admission Paymentgateway Pushresponse  url hit on Controller error");
                return View();
            }
            return View();
        }



    }
    public class Addresses
    {
        public string bitcoincash { get; set; }
        public string litecoin { get; set; }
        public string bitcoin { get; set; }
        public string ethereum { get; set; }
        public string usdc { get; set; }
        public string dai { get; set; }
    }

    public class Metadata
    {
    }

    public class Timeline
    {
        public string status { get; set; }
        public DateTime time { get; set; }
    }

    public class Data
    {
        public Addresses addresses { get; set; }
        public string code { get; set; }
        public DateTime created_at { get; set; }
        public string description { get; set; }
        public DateTime expires_at { get; set; }
        public string hosted_url { get; set; }
        public string id { get; set; }
        public Metadata metadata { get; set; }
        public string name { get; set; }
        public List<object> payments { get; set; }
        public string pricing_type { get; set; }
        public string resource { get; set; }
        public string support_email { get; set; }
        public List<Timeline> timeline { get; set; }
    }

    public class Root
    {
        public Data data { get; set; }
    }


    public class translate
    {
        public Data data;
    }
    //public class Data
    //{
    //    public translations1 translations;
    //}
    public class translations1
    {
        public List<translatedText1> translatedText;
    }
    public class translatedText1
    {
        public String translatedText;
    }
}