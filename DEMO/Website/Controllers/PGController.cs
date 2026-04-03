using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc;
using DataLayer;
using System.Data;
using System.IO;
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

namespace Website.Controllers
{
    public class PGController : Controller
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
                AcademicSession ac = new AcademicSession();
                Login stvalue = new Login();
                //stvalue.FirstName = CommonSetting.RemoveSpecialChars(Request.Form["firstname"]);
                //stvalue.MiddleName = CommonSetting.RemoveSpecialChars(Request.Form["middlename"]);
                //stvalue.LastName = CommonSetting.RemoveSpecialChars(Request.Form["lastname"]);
                //stvalue.Gender = Convert.ToInt32(Request.Form["Gender"]);
                stvalue.DOB = Request.Form["dob"];
                //stvalue.CastCategory = Convert.ToInt32(Request.Form["Cast"]);
                //stvalue.BloodGroup = Convert.ToInt32(Request.Form["Blood_Group"] == "" ? "0" : Request.Form["Blood_Group"]);
                stvalue.MobileNo = CommonSetting.RemoveSpecialChars(Request.Form["mobileno"]);
                stvalue.Email = CommonSetting.RemoveSpecialCharsemail(Request.Form["email"]);
                //stvalue.CurrentAddress = CommonSetting.RemoveSpecialCharsaddress(Request.Form["Address"]);
                //stvalue.CA_City = CommonSetting.RemoveSpecialCharsaddress(Request.Form["city"]);
                //stvalue.CA_PinCode = CommonSetting.RemoveSpecialChars(Request.Form["pincode"]);
                stvalue.CA_Country = 80;
                //stvalue.CA_State = Convert.ToInt32(Request.Form["State"]);
                //stvalue.PA_Address = Request.Form["PAddress"];
                //stvalue.PA_City = Request.Form["Pcity"];
                //stvalue.PA_PinCode = Request.Form["PPinCode"];
                stvalue.PA_Country = 80;
                //stvalue.PA_State = Convert.ToInt32(Request.Form["PState"]);
                //stvalue.FatherName = CommonSetting.RemoveSpecialChars(Request.Form["fathername"]);
                //stvalue.FatherMobile = CommonSetting.RemoveSpecialChars(Request.Form["fathermobile"]);
                //stvalue.FatherEmail = CommonSetting.RemoveSpecialCharsemail(Request.Form["fatheremail"]);
                //stvalue.FatherQualification = CommonSetting.RemoveSpecialChars(Request.Form["fatherqulification"]);
                //stvalue.FatherOccupation = CommonSetting.RemoveSpecialChars(Request.Form["fatheroccupation"]);
                //stvalue.MotherName = CommonSetting.RemoveSpecialChars(Request.Form["mothername"]);
                //stvalue.MotherMobile = CommonSetting.RemoveSpecialChars(Request.Form["mothermobile"]);
                //stvalue.MotherEmail = CommonSetting.RemoveSpecialCharsemail(Request.Form["motheremail"]);
                //stvalue.MotherOccupation = CommonSetting.RemoveSpecialChars(Request.Form["motheroccupation"]);
                //stvalue.MotherQualification = CommonSetting.RemoveSpecialChars(Request.Form["motherqulicafication"]);
                // stvalue.GargianName = Request.Form["Guardianname"];
                // stvalue.GargianRelation = Request.Form["Guardianrelation"];
                // stvalue.GargianContactNo = Request.Form["Guardianmobile"];
                stvalue.EducationType = Convert.ToInt32(Request.Form["educationtype1"] == "" ? "12" : Request.Form["educationtype1"]);
                stvalue.CourseCategory = Convert.ToInt32(Request.Form["coursetype1"] == "" ? "0" : Request.Form["coursetype1"]);
                if (stvalue.CourseCategory == 0)
                {
                    Login logmsg = new Login();
                    logmsg.Message = "Please Again Fill From  ,or please select Course  ";
                    return Json(logmsg, JsonRequestBehavior.AllowGet);
                }
                //stvalue.prevoiusboardid = Convert.ToInt32(Request.Form["prevoiusboardid"] == "" ? "0" : Request.Form["prevoiusboardid"]);
                //if (stvalue.prevoiusboardid == 0)
                //{
                //    Login logmsg = new Login();
                //    logmsg.Message = "Please Again Fill From : ";
                //    return Json(logmsg, JsonRequestBehavior.AllowGet);
                //}
                stvalue.prevoiustreamid = Convert.ToInt32(Request.Form["prevoiustreamid"] == "" ? "0" : Request.Form["prevoiustreamid"]);
                if (stvalue.prevoiustreamid == 0)
                {
                    Login logmsg = new Login();
                    logmsg.Message = "Please Again Fill From : ";
                    return Json(logmsg, JsonRequestBehavior.AllowGet);
                }
                //stvalue.CourseType = Convert.ToInt32(Request.Form["stream1"] == "" ? "0" : Request.Form["stream1"]);
                stvalue.AdmisitionCategory = Convert.ToInt32(Request.Form["administype1"] == "" ? "1" : Request.Form["administype1"]);
                //stvalue.title = Convert.ToInt32(Request.Form["title"] == "" ? "0" : Request.Form["title"]);
                //stvalue.Ftitle = Convert.ToInt32(Request.Form["ftitle"] == "" ? "0" : Request.Form["ftitle"]);
                stvalue.session = ac.GetAcademiccurrentSessioneducationtype(12).ID;
                //stvalue.Nationality = Convert.ToInt32(Request.Form["Nationality"] == "" ? "0" : Request.Form["Nationality"]);
                //stvalue.Religion = Convert.ToInt32(Request.Form["Religion"] == "" ? "0" : Request.Form["Religion"]);
                //stvalue.MotherTongue = Convert.ToInt32(Request.Form["MotherTongue"] == "" ? "0" : Request.Form["MotherTongue"]);
                //stvalue.ishandicapped = Convert.ToBoolean(Request.Form["ishandicapped"] == "" ? "0" : Request.Form["ishandicapped"]);
                //stvalue.isex_service_man = Convert.ToBoolean(Request.Form["isex_service_man"] == "" ? "0" : Request.Form["isex_service_man"]);
                //stvalue.is_ncc_candidate = Convert.ToBoolean(Request.Form["is_ncc_candidate"] == "" ? "0" : Request.Form["is_ncc_candidate"]);
                //stvalue.aadharno = Convert.ToString(Request.Form["aadharno"] == "" ? "" : Request.Form["aadharno"]);
                ////--- surendar work 01/04/2019
                //stvalue.FirstNameInHindi = Request.Form["FirstNameInHindi"];
                //stvalue.MiddleNameInHindi = Request.Form["MiddleNameInHindi"];
                //stvalue.LastNameInHindi = Request.Form["LastNameInHindi"];

                //stvalue.ReligonOther = Request.Form["ReligonOther"];

                //stvalue.FatherNameInHindi = Request.Form["FatherNameInHindi"];
                //stvalue.MotherNameInHindi = Request.Form["MotherNameInHindi"];

                //stvalue.IsSports = Convert.ToBoolean(Request.Form["IsSports"] == "" ? "0" : Request.Form["IsSports"]);
                //stvalue.IsStaff = Convert.ToBoolean(Request.Form["IsStaff"] == "" ? "0" : Request.Form["IsStaff"]);
                //stvalue.is_GEW = Convert.ToBoolean(Request.Form["is_GEW"] == "" ? "0" : Request.Form["is_GEW"]);
                //stvalue.is_permanentaddress = Convert.ToBoolean(Request.Form["is_permanentaddress"] == "" ? "0" : Request.Form["is_permanentaddress"]);
                stvalue.Password = CommonMethod.RandomNumber(100000, 999999);
                stvalue.aadharno = stvalue.Password;
                MD5CryptoServiceProvider md5Hasher = new MD5CryptoServiceProvider();
                Byte[] hashedBytes;
                UTF8Encoding encoder = new UTF8Encoding();
                hashedBytes = md5Hasher.ComputeHash(encoder.GetBytes(stvalue.Password));
                stvalue.hashedBytes = hashedBytes;

                //--till here
                string jsonstring = JsonConvert.SerializeObject(stvalue);
                //if (stvalue.Gender != 9)
                //{
                //    if (stvalue.CastCategory == 23)
                //    {
                //        Login logmsg = new Login();
                //        logmsg.Message = "Please Again Fill From and select another caste !! ";
                //        return Json(logmsg, JsonRequestBehavior.AllowGet);
                //    }
                //}

                //if (stvalue.is_GEW == true)
                //{
                //    if (stvalue.CastCategory != 4)
                //    {
                //        Login logmsg = new Login();
                //        logmsg.Message = "General Economical Weaker Option Only for General Category Student,Please Change ! ";
                //        return Json(logmsg, JsonRequestBehavior.AllowGet);
                //    }
                //}
                //if (Request.Files.Count > 0)
                //{
                try
                {
                    //for (int i = 0; i < Request.Files.Count; i++)
                    //{
                    //    if (Request.Files.GetKey(i) == "sign")
                    //    {
                    //        HttpPostedFileBase fileUpload = Request.Files.Get(i);
                    //        if (Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                    //        {
                    //            string[] testfiles = fileUpload.FileName.Split(new char[] { '\\' });
                    //        }
                    //        Stream st1 = fileUpload.InputStream;
                    //        string name = Path.GetFileName(fileUpload.FileName);
                    //        try
                    //        {
                    //            string myBucketName = "mungeruniversity"; //your s3 bucket name goes here  
                    //            string s3DirectoryName = "Student/Photoandsign";
                    //            string s3FileName = System.DateTime.Now.ToString("dd_MM_yyyy_HH:mm:ss") + "_StudentSign_" + stvalue.FirstName + @name;
                    //            s3FileName = s3FileName.Replace(" ", "");
                    //            stvalue.stsign = s3FileName;
                    //            bool a;
                    //            AmazonUploader myUploader = new AmazonUploader();
                    //            a = myUploader.sendMyFileToFolder(photo,Server.MapPath("~/App_Data/uploads"),s3FileName);
                    //        }
                    //        catch (Exception ex)
                    //        {
                    //            CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "Student Registration : Sign Image Upload", "Registration" + jsonstring);
                    //        }
                    //    }
                    //    if (Request.Files.GetKey(i) == "photo")
                    //    {
                    //        HttpPostedFileBase fileUpload = Request.Files.Get(i);
                    //        string fname;
                    //        if (Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                    //        {
                    //            string[] testfiles = fileUpload.FileName.Split(new char[] { '\\' });
                    //            fname = testfiles[testfiles.Length - 1];
                    //        }
                    //        Stream st1 = fileUpload.InputStream;
                    //        string name = Path.GetFileName(fileUpload.FileName);
                    //        try
                    //        {
                    //            string myBucketName = "mungeruniversity"; //your s3 bucket name goes here  
                    //            string s3DirectoryName = "Student/Photoandsign";
                    //            string s3FileName = System.DateTime.Now.ToString("dd_MM_yyyy_HH:mm:ss") + "_StudentPhoto_" + stvalue.FirstName + @name;
                    //            s3FileName = s3FileName.Replace(" ", "");
                    //            stvalue.stphoto = s3FileName;
                    //            bool a;
                    //            AmazonUploader myUploader = new AmazonUploader();
                    //            a = myUploader.sendMyFileToFolder(photo,Server.MapPath("~/App_Data/uploads"),s3FileName);
                    //        }
                    //        catch (Exception ex)
                    //        {
                    //            //CommonMethod.PrintLog(ex, HttpContext.Request.Url.AbsolutePath, "Student Registration : Photo Image Upload", "");
                    //            CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "Student Registration : Photo Image Upload", "Registration" + jsonstring);

                    //        }
                    //    }
                    //}
                    StudentLogin st = new StudentLogin();
                    var result = st.Student_registration(stvalue);
                    result.Password = stvalue.Password;
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
                //}
                //else
                //{
                //    Login logmsg = new Login();
                //    logmsg.Message = "Please Again Fill From : ";
                //    return Json(logmsg, JsonRequestBehavior.AllowGet);
                //}
            }
            else
            {
                Login logmsg = new Login();
                logmsg.Message = "Please Again Fill From : ";
                return Json(logmsg, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult PGStudentApplicationForm()
        {
            UserLogin.ExpireAllCookies();
            //return RedirectToAction("Index", "Login", new { area = "Student" });
            Commn_master com = new Commn_master();
            ViewBag.Admissiontype = com.getcommonMaster("AdmissionType");
            ViewBag.Educationtype = com.getcommonMaster("selectbycommonid", Convert.ToInt32(CommonSetting.Commonid.EducationtypePG));
            ViewBag.Gender = com.getcommonMaster("Gender");
            ViewBag.CasteCategory = com.getcommonMaster("CasteCategory");
            ViewBag.bloodgroup = com.Getbloodgroup("Select");
            ViewBag.stitle = com.getcommonMaster("Title");
            ViewBag.ftitle = com.getcommonMaster("TitleM");
            ViewBag.Nationality = com.getcommonMaster("Nationality");
            ViewBag.Religion = com.getcommonMaster("Religion");
            ViewBag.MotherTongue = com.getcommonMaster("MotherTongue");
            StudentAdmissionQualification com1 = new StudentAdmissionQualification();
            List<QualifiationMaster> objlist = new List<QualifiationMaster>();
            objlist = com1.getqualificationst(Convert.ToInt32(CommonSetting.Commonid.EducationtypePG)).ToList();
            objlist = objlist.Where(x => x.ID != 12).ToList();
            ViewBag.previousqua = objlist;
            Country cont = new Country();
            ViewBag.country = cont.GetAllCountries();
            ViewBag.Pcountry = cont.GetAllCountries();
            ViewBag.boardtype = CommonMethod.Boradtype().Where(x => x.boardid == 4 || x.boardid == 5);
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
            ViewBag.CurrentSessionid = ac.GetAcademiccurrentSessioneducationtype(12).ID;
            ViewBag.CurrentSession = ac.GetAcademiccurrentSessioneducationtype(12).Session;// ac.GetAcademiccurrentSessionname(ac.GetAcademiccurrentSession().ID.ToString(), Convert.ToInt32(CommonSetting.coursecategory.Ma)).sessionname;


            ViewBag.check_admissionopen = com.check_admission_open(ac.GetAcademiccurrentSessioneducationtype(12).ID, Convert.ToInt32(CommonSetting.Commonid.EducationtypePG));

            return View();
        }
        public JsonResult getprevioustream(int id)
        {
            StudentAdmissionQualification com = new StudentAdmissionQualification();
            var obj = com.getqualificationst(id);
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

        //public ActionResult ForgotPassword(string id)
        //{
        //    try
        //    {
        //        string AdminID = EncriptDecript.Decrypt(id);
        //        BL_ForgotPass obj = new BL_ForgotPass();
        //        obj.AdminID = Convert.ToInt32(AdminID);
        //        ViewBag.ID = Convert.ToInt32(AdminID);
        //        return View();
        //    }
        //    catch (Exception ex)
        //    {

        //        CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "ForgotPassword", id);
        //        return View();
        //    }
        //}
        //[HttpPost]
        //public JsonResult ForgotPasswords(string Password, int id)
        //{
        //    try
        //    {
        //        BL_ForgotPass forgot = new BL_ForgotPass();
        //        MD5CryptoServiceProvider md5Hasher = new MD5CryptoServiceProvider();
        //        Byte[] hashedBytes;
        //        UTF8Encoding encoder = new UTF8Encoding();
        //        hashedBytes = md5Hasher.ComputeHash(encoder.GetBytes(Password));


        //        var obj = forgot.ForgotPassword(id, hashedBytes, Password);
        //        return Json(new { data = obj, success = true });
        //    }
        //    catch (Exception ex)
        //    {
        //        CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "ForgotPassword paost Method", id.ToString());
        //        return Json(new { data = new BL_ForgotPass(), success = true });
        //    }
        //}
        //public ActionResult ResetPassword()
        //{
        //    return View();
        //}
        //[HttpPost]
        //public JsonResult ResetPass(string ApplicationNo)
        //{

        //    BL_ForgotPass obj = new BL_ForgotPass();
        //    try
        //    {
        //        var sendreset = obj.ResetPass(ApplicationNo);
        //        int StudentID = sendreset.SID;
        //        string ID = EncriptDecript.Encrypt(StudentID.ToString());
        //        string MyName = sendreset.Name;
        //        string SEmail = sendreset.Email;
        //        string url = ConfigurationManager.AppSettings["siteUrl"];
        //        string PasswordResetLink = url + "Home/ForgotPassword?Id=" + ID;
        //        if (StudentID > 0)
        //        {
        //            Email.SendEmailForResetPassword(SEmail, MyName, PasswordResetLink);
        //            obj.Msg = "Reset Password Link sent to your registered Email ID..!!";
        //            TempData["errorMsg"] = obj.Msg;
        //        }
        //        else
        //        {
        //            obj.Msg = "Invalid Application ID..!!";
        //            //TempData["errorMsg"] = obj.Msg;
        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //        CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "Reset Password", ApplicationNo);
        //    }
        //    return Json(new { data = obj, success = true });
        //}
        //public ActionResult PGPushResponse()
        //{
        //    if (Request.Form.Count > 0)
        //    {
        //        try
        //        {
        //            Sbiepay sbi = new Sbiepay();
        //            var result = sbi.Pushresponsedecrypt();
        //        }
        //        catch (Exception ex)
        //        {
        //            CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "Paymentgateway Pushresponse  url hit on Controller error");
        //            return RedirectToAction("Index");
        //        }
        //    }
        //    return RedirectToAction("Index");
        //}
        //public ActionResult PGPushResponserdanddjcollege()
        //{
        //    if (Request.Form.Count > 0)
        //    {
        //        try
        //        {
        //            SbiepayAdmission sbi = new SbiepayAdmission();
        //            string ckey = CommonMethod.MIDcollegewise().Where(x => x.collegeid == 1).FirstOrDefault().mkey;
        //            var result = sbi.Pushresponseradmission(ckey);
        //        }
        //        catch (Exception ex)
        //        {
        //            CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "admission Paymentgateway Pushresponse  url hit on Controller error");
        //            // return RedirectToAction("Index");
        //        }
        //    }
        //    return RedirectToAction("Index");
        //}
        //public ActionResult PGPushResponsejrscollegejamalpur()
        //{
        //    if (Request.Form.Count > 0)
        //    {
        //        try
        //        {
        //            SbiepayAdmission sbi = new SbiepayAdmission();
        //            string ckey = CommonMethod.MIDcollegewise().Where(x => x.collegeid == 2).FirstOrDefault().mkey;
        //            var result = sbi.Pushresponseradmission(ckey);
        //        }
        //        catch (Exception ex)
        //        {
        //            CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "admission Paymentgateway Pushresponse  url hit on Controller error");
        //            return RedirectToAction("Index");
        //        }
        //    }
        //    return RedirectToAction("Index");
        //}
        //public ActionResult PGPushResponsekoshicollegekhagaria()
        //{
        //    if (Request.Form.Count > 0)
        //    {
        //        try
        //        {
        //            SbiepayAdmission sbi = new SbiepayAdmission();
        //            string ckey = CommonMethod.MIDcollegewise().Where(x => x.collegeid == 3).FirstOrDefault().mkey;
        //            var result = sbi.Pushresponseradmission(ckey);
        //        }
        //        catch (Exception ex)
        //        {
        //            CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "admission Paymentgateway Pushresponse  url hit on Controller error");
        //            return RedirectToAction("Index");
        //        }
        //    }
        //    return RedirectToAction("Index");
        //}
        //public ActionResult PGPushResponsekkmcollegejamui()
        //{
        //    if (Request.Form.Count > 0)
        //    {
        //        try
        //        {
        //            SbiepayAdmission sbi = new SbiepayAdmission();
        //            string ckey = CommonMethod.MIDcollegewise().Where(x => x.collegeid == 4).FirstOrDefault().mkey;
        //            var result = sbi.Pushresponseradmission(ckey);
        //        }
        //        catch (Exception ex)
        //        {
        //            CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "admission Paymentgateway Pushresponse  url hit on Controller error");
        //            return RedirectToAction("Index");
        //        }
        //    }
        //    return RedirectToAction("Index");
        //}
        //public ActionResult PGPushResponseskrcollegebarbigha()
        //{
        //    if (Request.Form.Count > 0)
        //    {
        //        try
        //        {
        //            SbiepayAdmission sbi = new SbiepayAdmission();
        //            string ckey = CommonMethod.MIDcollegewise().Where(x => x.collegeid == 5).FirstOrDefault().mkey;
        //            var result = sbi.Pushresponseradmission(ckey);
        //        }
        //        catch (Exception ex)
        //        {
        //            CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "admission Paymentgateway Pushresponse  url hit on Controller error");
        //            return RedirectToAction("Index");
        //        }
        //    }
        //    return RedirectToAction("Index");
        //}
        //public ActionResult PGPushResponserscollegetarapurmunger()
        //{
        //    if (Request.Form.Count > 0)
        //    {
        //        try
        //        {
        //            SbiepayAdmission sbi = new SbiepayAdmission();
        //            string ckey = CommonMethod.MIDcollegewise().Where(x => x.collegeid == 6).FirstOrDefault().mkey;
        //            var result = sbi.Pushresponseradmission(ckey);
        //        }
        //        catch (Exception ex)
        //        {
        //            CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "admission Paymentgateway Pushresponse  url hit on Controller error");
        //            return RedirectToAction("Index");
        //        }
        //    }
        //    return RedirectToAction("Index");
        //}
        //public ActionResult PGPushResponsebrmcollege()
        //{
        //    if (Request.Form.Count > 0)
        //    {
        //        try
        //        {
        //            SbiepayAdmission sbi = new SbiepayAdmission();
        //            string ckey = CommonMethod.MIDcollegewise().Where(x => x.collegeid == 7).FirstOrDefault().mkey;
        //            var result = sbi.Pushresponseradmission(ckey);
        //        }
        //        catch (Exception ex)
        //        {
        //            CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "admission Paymentgateway Pushresponse  url hit on Controller error");
        //            return RedirectToAction("Index");
        //        }
        //    }
        //    return RedirectToAction("Index");
        //}
        //public ActionResult PGPushResponsebnmcollege()
        //{
        //    if (Request.Form.Count > 0)
        //    {
        //        try
        //        {
        //            SbiepayAdmission sbi = new SbiepayAdmission();
        //            string ckey = CommonMethod.MIDcollegewise().Where(x => x.collegeid == 10).FirstOrDefault().mkey;
        //            var result = sbi.Pushresponseradmission(ckey);
        //        }
        //        catch (Exception ex)
        //        {
        //            CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "admission Paymentgateway Pushresponse  url hit on Controller error");
        //            return RedirectToAction("Index");
        //        }
        //    }
        //    return RedirectToAction("Index");
        //}
        //public ActionResult PGPushResponserdcollegesheikpura()
        //{
        //    if (Request.Form.Count > 0)
        //    {
        //        try
        //        {
        //            SbiepayAdmission sbi = new SbiepayAdmission();
        //            string ckey = CommonMethod.MIDcollegewise().Where(x => x.collegeid == 11).FirstOrDefault().mkey;
        //            var result = sbi.Pushresponseradmission(ckey);
        //        }
        //        catch (Exception ex)
        //        {
        //            CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "admission Paymentgateway Pushresponse  url hit on Controller error");
        //            return RedirectToAction("Index");
        //        }
        //    }
        //    return RedirectToAction("Index");
        //}
        //public ActionResult PGPushResponsejmscollegemunger()
        //{
        //    if (Request.Form.Count > 0)
        //    {
        //        try
        //        {
        //            SbiepayAdmission sbi = new SbiepayAdmission();
        //            string ckey = CommonMethod.MIDcollegewise().Where(x => x.collegeid == 13).FirstOrDefault().mkey;
        //            var result = sbi.Pushresponseradmission(ckey);
        //        }
        //        catch (Exception ex)
        //        {
        //            CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "admission Paymentgateway Pushresponse  url hit on Controller error");
        //            return RedirectToAction("Index");
        //        }
        //    }
        //    return RedirectToAction("Index");
        //}
        //public ActionResult PGPushResponsehscollege()
        //{
        //    if (Request.Form.Count > 0)
        //    {
        //        try
        //        {
        //            SbiepayAdmission sbi = new SbiepayAdmission();
        //            string ckey = CommonMethod.MIDcollegewise().Where(x => x.collegeid == 14).FirstOrDefault().mkey;
        //            var result = sbi.Pushresponseradmission(ckey);
        //        }
        //        catch (Exception ex)
        //        {
        //            CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "admission Paymentgateway Pushresponse  url hit on Controller error");
        //            return RedirectToAction("Index");
        //        }
        //    }
        //    return RedirectToAction("Index");
        //}
        //public ActionResult PGPushResponseksscollegelakhisarai()
        //{
        //    if (Request.Form.Count > 0)
        //    {
        //        try
        //        {
        //            SbiepayAdmission sbi = new SbiepayAdmission();
        //            string ckey = CommonMethod.MIDcollegewise().Where(x => x.collegeid == 15).FirstOrDefault().mkey;
        //            var result = sbi.Pushresponseradmission(ckey);
        //        }
        //        catch (Exception ex)
        //        {
        //            CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "admission Paymentgateway Pushresponse  url hit on Controller error");
        //            return RedirectToAction("Index");
        //        }
        //    }
        //    return RedirectToAction("Index");
        //}
        //public ActionResult PGPushResponsekdscollegegogri()
        //{
        //    if (Request.Form.Count > 0)
        //    {
        //        try
        //        {
        //            SbiepayAdmission sbi = new SbiepayAdmission();
        //            string ckey = CommonMethod.MIDcollegewise().Where(x => x.collegeid == 16).FirstOrDefault().mkey;
        //            var result = sbi.Pushresponseradmission(ckey);
        //        }
        //        catch (Exception ex)
        //        {
        //            CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "admission Paymentgateway Pushresponse  url hit on Controller error");
        //            return RedirectToAction("Index");
        //        }
        //    }
        //    return RedirectToAction("Index");
        //}
        //public ActionResult PGPushResponsedsmcollegejhajha()
        //{
        //    if (Request.Form.Count > 0)
        //    {
        //        try
        //        {
        //            SbiepayAdmission sbi = new SbiepayAdmission();
        //            string ckey = CommonMethod.MIDcollegewise().Where(x => x.collegeid == 17).FirstOrDefault().mkey;
        //            var result = sbi.Pushresponseradmission(ckey);
        //        }
        //        catch (Exception ex)
        //        {
        //            CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "admission Paymentgateway Pushresponse  url hit on Controller error");
        //            return RedirectToAction("Index");
        //        }
        //    }
        //    return RedirectToAction("Index");
        //}
        //public ActionResult PGPushResponsekmdcollegeparbatta()
        //{
        //    if (Request.Form.Count > 0)
        //    {
        //        try
        //        {
        //            SbiepayAdmission sbi = new SbiepayAdmission();
        //            string ckey = CommonMethod.MIDcollegewise().Where(x => x.collegeid == 18).FirstOrDefault().mkey;
        //            var result = sbi.Pushresponseradmission(ckey);
        //        }
        //        catch (Exception ex)
        //        {
        //            CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "admission Paymentgateway Pushresponse  url hit on Controller error");
        //            return RedirectToAction("Index");
        //        }
        //    }
        //    return RedirectToAction("Index");
        //}
        //public ActionResult PGPushResponsemahilacollegekhagaria()
        //{
        //    if (Request.Form.Count > 0)
        //    {
        //        try
        //        {
        //            SbiepayAdmission sbi = new SbiepayAdmission();
        //            string ckey = CommonMethod.MIDcollegewise().Where(x => x.collegeid == 19).FirstOrDefault().mkey;
        //            var result = sbi.Pushresponseradmission(ckey);
        //        }
        //        catch (Exception ex)
        //        {
        //            CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "admission Paymentgateway Pushresponse  url hit on Controller error");
        //            return RedirectToAction("Index");
        //        }
        //    }
        //    return RedirectToAction("Index");
        //}
        //public ActionResult PGPushResponsejamalpurcollegejamalpur()
        //{
        //    if (Request.Form.Count > 0)
        //    {
        //        try
        //        {
        //            SbiepayAdmission sbi = new SbiepayAdmission();
        //            string ckey = CommonMethod.MIDcollegewise().Where(x => x.collegeid == 20).FirstOrDefault().mkey;
        //            var result = sbi.Pushresponseradmission(ckey);
        //        }
        //        catch (Exception ex)
        //        {
        //            CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "admission Paymentgateway Pushresponse  url hit on Controller error");
        //            return RedirectToAction("Index");
        //        }
        //    }
        //    return RedirectToAction("Index");
        //}
        //public ActionResult PGPushResponsesbncollegegarhirampurmunger()
        //{
        //    if (Request.Form.Count > 0)
        //    {
        //        try
        //        {
        //            SbiepayAdmission sbi = new SbiepayAdmission();
        //            string ckey = CommonMethod.MIDcollegewise().Where(x => x.collegeid == 21).FirstOrDefault().mkey;
        //            var result = sbi.Pushresponseradmission(ckey);
        //        }
        //        catch (Exception ex)
        //        {
        //            CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "admission Paymentgateway Pushresponse  url hit on Controller error");
        //            return RedirectToAction("Index");
        //        }
        //    }
        //    return RedirectToAction("Index");
        //}
        //public ActionResult PGPushResponseinternationalcollegeghosaith()
        //{
        //    if (Request.Form.Count > 0)
        //    {
        //        try
        //        {
        //            SbiepayAdmission sbi = new SbiepayAdmission();
        //            string ckey = CommonMethod.MIDcollegewise().Where(x => x.collegeid == 22).FirstOrDefault().mkey;
        //            var result = sbi.Pushresponseradmission(ckey);
        //        }
        //        catch (Exception ex)
        //        {
        //            CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "admission Paymentgateway Pushresponse  url hit on Controller error");
        //            return RedirectToAction("Index");
        //        }
        //    }
        //    return RedirectToAction("Index");
        //}
        //public ActionResult PGPushResponserlalcollegelakhisarai()
        //{
        //    if (Request.Form.Count > 0)
        //    {
        //        try
        //        {
        //            SbiepayAdmission sbi = new SbiepayAdmission();
        //            string ckey = CommonMethod.MIDcollegewise().Where(x => x.collegeid == 23).FirstOrDefault().mkey;
        //            var result = sbi.Pushresponseradmission(ckey);
        //        }
        //        catch (Exception ex)
        //        {
        //            CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "admission Paymentgateway Pushresponse  url hit on Controller error");
        //            return RedirectToAction("Index");
        //        }
        //    }
        //    return RedirectToAction("Index");
        //}
        //public ActionResult PGPushResponsemahilacollegebarahiya()
        //{
        //    if (Request.Form.Count > 0)
        //    {
        //        try
        //        {
        //            SbiepayAdmission sbi = new SbiepayAdmission();
        //            string ckey = CommonMethod.MIDcollegewise().Where(x => x.collegeid == 24).FirstOrDefault().mkey;
        //            var result = sbi.Pushresponseradmission(ckey);
        //        }
        //        catch (Exception ex)
        //        {
        //            CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "admission Paymentgateway Pushresponse  url hit on Controller error");
        //            return RedirectToAction("Index");
        //        }
        //    }
        //    return RedirectToAction("Index");
        //}
        //public ActionResult PGPushResponsesanjaygandhimahilacollegesheikhpura()
        //{
        //    if (Request.Form.Count > 0)
        //    {
        //        try
        //        {
        //            SbiepayAdmission sbi = new SbiepayAdmission();
        //            string ckey = CommonMethod.MIDcollegewise().Where(x => x.collegeid == 25).FirstOrDefault().mkey;
        //            var result = sbi.Pushresponseradmission(ckey);
        //        }
        //        catch (Exception ex)
        //        {
        //            CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "admission Paymentgateway Pushresponse  url hit on Controller error");
        //            return RedirectToAction("Index");
        //        }
        //    }
        //    return RedirectToAction("Index");
        //}
        //public ActionResult PGPushResponsesscollegemehussheikhpura()
        //{
        //    if (Request.Form.Count > 0)
        //    {
        //        try
        //        {
        //            SbiepayAdmission sbi = new SbiepayAdmission();
        //            string ckey = CommonMethod.MIDcollegewise().Where(x => x.collegeid == 26).FirstOrDefault().mkey;
        //            var result = sbi.Pushresponseradmission(ckey);
        //        }
        //        catch (Exception ex)
        //        {
        //            CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "admission Paymentgateway Pushresponse  url hit on Controller error");
        //            return RedirectToAction("Index");
        //        }
        //    }
        //    return RedirectToAction("Index");
        //}
        //public ActionResult PGPushResponsecnbcollegehathiyamasheikhpura()
        //{
        //    if (Request.Form.Count > 0)
        //    {
        //        try
        //        {
        //            SbiepayAdmission sbi = new SbiepayAdmission();
        //            string ckey = CommonMethod.MIDcollegewise().Where(x => x.collegeid == 27).FirstOrDefault().mkey;
        //            var result = sbi.Pushresponseradmission(ckey);
        //        }
        //        catch (Exception ex)
        //        {
        //            CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "admission Paymentgateway Pushresponse  url hit on Controller error");
        //            return RedirectToAction("Index");
        //        }
        //    }
        //    return RedirectToAction("Index");
        //}
        //public ActionResult PGPushResponsemscollegealoulisoniharkhagaria()
        //{
        //    if (Request.Form.Count > 0)
        //    {
        //        try
        //        {
        //            SbiepayAdmission sbi = new SbiepayAdmission();
        //            string ckey = CommonMethod.MIDcollegewise().Where(x => x.collegeid == 28).FirstOrDefault().mkey;
        //            var result = sbi.Pushresponseradmission(ckey);
        //        }
        //        catch (Exception ex)
        //        {
        //            CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "admission Paymentgateway Pushresponse  url hit on Controller error");
        //            return RedirectToAction("Index");
        //        }
        //    }
        //    return RedirectToAction("Index");
        //}
        //public ActionResult PGPushResponsedhanrajsinghcollegesikandarajamui()
        //{
        //    if (Request.Form.Count > 0)
        //    {
        //        try
        //        {
        //            SbiepayAdmission sbi = new SbiepayAdmission();
        //            string ckey = CommonMethod.MIDcollegewise().Where(x => x.collegeid == 29).FirstOrDefault().mkey;
        //            var result = sbi.Pushresponseradmission(ckey);
        //        }
        //        catch (Exception ex)
        //        {
        //            CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "admission Paymentgateway Pushresponse  url hit on Controller error");
        //            return RedirectToAction("Index");
        //        }
        //    }
        //    return RedirectToAction("Index");
        //}
        //public ActionResult PGPushResponseskcollegelohandasikandarajamui()
        //{
        //    if (Request.Form.Count > 0)
        //    {
        //        try
        //        {
        //            SbiepayAdmission sbi = new SbiepayAdmission();
        //            string ckey = CommonMethod.MIDcollegewise().Where(x => x.collegeid == 30).FirstOrDefault().mkey;
        //            var result = sbi.Pushresponseradmission(ckey);
        //        }
        //        catch (Exception ex)
        //        {
        //            CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "admission Paymentgateway Pushresponse  url hit on Controller error");
        //            return RedirectToAction("Index");
        //        }
        //    }
        //    return RedirectToAction("Index");
        //}
        //public ActionResult PGPushResponsephalguniprasadyadavcollegechakaijamui()
        //{
        //    if (Request.Form.Count > 0)
        //    {
        //        try
        //        {
        //            SbiepayAdmission sbi = new SbiepayAdmission();
        //            string ckey = CommonMethod.MIDcollegewise().Where(x => x.collegeid == 31).FirstOrDefault().mkey;
        //            var result = sbi.Pushresponseradmission(ckey);
        //        }
        //        catch (Exception ex)
        //        {
        //            CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "admission Paymentgateway Pushresponse  url hit on Controller error");
        //            return RedirectToAction("Index");
        //        }
        //    }
        //    return RedirectToAction("Index");
        //}
        //public ActionResult PGPushResponseshyamaprasadsinghmahilacollegejamui()
        //{
        //    if (Request.Form.Count > 0)
        //    {
        //        try
        //        {
        //            SbiepayAdmission sbi = new SbiepayAdmission();
        //            string ckey = CommonMethod.MIDcollegewise().Where(x => x.collegeid == 32).FirstOrDefault().mkey;
        //            var result = sbi.Pushresponseradmission(ckey);
        //        }
        //        catch (Exception ex)
        //        {
        //            CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "admission Paymentgateway Pushresponse  url hit on Controller error");
        //            return RedirectToAction("Index");
        //        }
        //    }
        //    return RedirectToAction("Index");
        //}
        //public ActionResult PGPushResponseshardagirdharikesharicollege()
        //{
        //    if (Request.Form.Count > 0)
        //    {
        //        try
        //        {
        //            SbiepayAdmission sbi = new SbiepayAdmission();
        //            string ckey = CommonMethod.MIDcollegewise().Where(x => x.collegeid == 33).FirstOrDefault().mkey;
        //            var result = sbi.Pushresponseradmission(ckey);
        //        }
        //        catch (Exception ex)
        //        {
        //            CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "admission Paymentgateway Pushresponse  url hit on Controller error");
        //            return RedirectToAction("Index");
        //        }
        //    }
        //    return RedirectToAction("Index");
        //}

        //public ActionResult DoubleVerificationURl()
        //{
        //    //StringBuilder sbPostData = new StringBuilder();
        //    //sbPostData.AppendFormat("queryRequest={0}", "|1000733|23380762618491910380");
        //    //sbPostData.AppendFormat("&aggregatorId={0}", "SBIEPAY");
        //    //sbPostData.AppendFormat("&merchantId={0}", "1000733");


        //    try
        //    {
        //        //CommonMethod.WritetoNotepad(null, HttpContext.Request.Url.AbsolutePath, "DoubleVerificationURl " + "Breakpreetam heelo 1000756");
        //        //string URI = "https://www.sbiepay.com/payagg/orderStatusQuery/getOrderStatusQuery";
        //        //ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
        //        //var request = (HttpWebRequest)WebRequest.Create(URI);
        //        //var postData = "queryRequest=|1000756|" + "23380762618491910380";
        //        //postData += "&aggregatorId=SBIEPAY";
        //        //postData += "&merchantId=1000756";
        //        //var data = Encoding.ASCII.GetBytes(postData);
        //        //request.Method = "POST";
        //        //request.ContentType = "application/x-www-form-urlencoded";
        //        //request.ContentLength = data.Length;
        //        //using (var stream = request.GetRequestStream())
        //        //{
        //        //    stream.Write(data, 0, data.Length);
        //        //}
        //        //var response = (HttpWebResponse)request.GetResponse();

        //        //var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
        //        //CommonMethod.WritetoNotepad(null, HttpContext.Request.Url.AbsolutePath, "DoubleVerificationURl " + "Break"+ responseString);
        //        ////Uri address = new Uri("https://test.sbiepay.com/payagg/orderStatusQuery/getOrderStatusQuery");

        //        ////ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
        //        ////ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3;

        //        //System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;

        //        ////using (WebClient webClient = new WebClient())
        //        ////{
        //        ////    var stream = webClient.OpenRead(address);
        //        ////    using (StreamReader sr = new StreamReader(stream))
        //        ////    {
        //        ////        var page = sr.ReadToEnd();

        //        ////        // return page;
        //        ////    }
        //        ////}
        //        ////CommonMethod.WritetoNotepad(null, HttpContext.Request.Url.AbsolutePath, "DoubleVerificationURl " + "Breakpreetam heelo 1000733");
        //        ////WebRequest webRequest = WebRequest.Create(@"https://www.sbiepay.com/payagg/orderStatusQuery/getOrderStatusQuery");
        //        ////webRequest.ContentType = "text/html";
        //        ////webRequest.Method = "POST";
        //        ////string body = "...";
        //        ////byte[] bytes = Encoding.ASCII.GetBytes(body);
        //        ////webRequest.ContentLength = bytes.Length;
        //        ////var os = webRequest.GetRequestStream();
        //        ////os.Write(bytes, 0, bytes.Length);
        //        ////os.Close();
        //        ////webRequest.Timeout = 0; //setting the timeout to 0 causes the request to fail
        //        ////WebResponse webResponse = webRequest.GetResponse(); //Exception thrown here ...
        //        ////CommonMethod.WritetoNotepad(null, HttpContext.Request.Url.AbsolutePath, "DoubleVerificationURl " + "Breakpreetam heelo "+ webResponse);
        //        ////return RedirectToAction("Index");
        //        ////using (var wb = new WebClient())
        //        ////{


        //        ////    var data = new NameValueCollection();
        //        ////    data["queryRequest"] = "|1000733|42067162616181939343";
        //        ////    data["aggregatorId"] = "SBIEPAY";
        //        ////    data["merchantId"] = "1000733";
        //        ////    CommonMethod.WritetoNotepad(null, HttpContext.Request.Url.AbsolutePath, "DoubleVerificationURl MID:--" + "1000733");
        //        ////    var response1 = wb.UploadValues("https://www.sbiepay.com/payagg/orderStatusQuery/getOrderStatusQuery", "POST", data);
        //        ////    CommonMethod.WritetoNotepad(null, HttpContext.Request.Url.AbsolutePath, "DoubleVerificationURl " + response1);

        //        ////}

        //        ////using (var wb = new WebClient())
        //        ////{
        //        ////    var data = new NameValueCollection();
        //        ////    data["queryRequest"] = "|1000003|2639506614091939495";
        //        ////    data["aggregatorId"] = "SBIEPAY";
        //        ////    data["merchantId"] = "1000003";
        //        ////    CommonMethod.WritetoNotepad(null, HttpContext.Request.Url.AbsolutePath, "DoubleVerificationURl MID:--" + "1000003");
        //        ////    var response1 = wb.UploadValues("https://www.sbiepay.com/payagg/orderStatusQuery/getOrderStatusQuery", "POST", data);
        //        ////    CommonMethod.WritetoNotepad(null, HttpContext.Request.Url.AbsolutePath, "DoubleVerificationURl " + response1);

        //        ////}
        //        //  return RedirectToAction("Index");

        //        //using (var wb = new WebClient())
        //        //{
        //        //    var data = new NameValueCollection();
        //        //    data["queryRequest"] = "|1000756|23380762618491910380";
        //        //    data["aggregatorId"] = "SBIEPAY";
        //        //    data["merchantId"] = "1000756";
        //        //    CommonMethod.WritetoNotepad(null, HttpContext.Request.Url.AbsolutePath, "DoubleVerificationURl MID:--" + "1000756");
        //        //    var response1 = wb.UploadValues("https://www.sbiepay.com/payagg/orderStatusQuery/getOrderStatusQuery", "POST", data);
        //        //    CommonMethod.WritetoNotepad(null, HttpContext.Request.Url.AbsolutePath, "DoubleVerificationURl " + response1);

        //        //}
        //        //Call Send SMS API
        //        //string sendSMSUri = "https://test.sbiepay.com/payagg/orderStatusQuery/getOrderStatusQuery";
        //        ////Create HTTPWebrequest
        //        //HttpWebRequest httpWReq = (HttpWebRequest)WebRequest.Create(sendSMSUri);
        //        ////Prepare and Add URL Encoded data
        //        //UTF8Encoding encoding = new UTF8Encoding();
        //        //byte[] data = encoding.GetBytes(sbPostData.ToString());
        //        ////Specify post method
        //        //httpWReq.Method = "POST";
        //        //httpWReq.ContentType = "application/x-www-form-urlencoded";
        //        //httpWReq.ContentLength = data.Length;
        //        //using (Stream stream = httpWReq.GetRequestStream())
        //        //{

        //        //    stream.Write(data, 0, data.Length);
        //        //}
        //        ////Get the response
        //        //HttpWebResponse response = (HttpWebResponse)httpWReq.GetResponse();
        //        //StreamReader reader = new StreamReader(response.GetResponseStream());
        //        //string responseString = reader.ReadToEnd();
        //        //CommonMethod.WritetoNotepad(null, HttpContext.Request.Url.AbsolutePath, "DoubleVerificationURl admission Paymentgateway Pushresponse  url hit on Controller error"+ responseString);

        //        ////Close the response
        //        //reader.Close();
        //        //response.Close();
        //    }
        //    catch (SystemException ex)
        //    {
        //        CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "DoubleVerificationURl admission Paymentgateway Pushresponse  url hit on Controller error");

        //    }
        //    return View();
        //}
        //[HttpPost]
        //public ActionResult DoubleVerificationURl(int id = 0, string trxid = "", string password = "", string mid = "")
        //{



        //    try
        //    {
        //        if (password != "Arora@321")
        //        {
        //            TempData["responseString"] = "Wrong Password !!";
        //            return View();
        //        }
        //        CommonMethod.WritetoNotepad(null, HttpContext.Request.Url.AbsolutePath, "DoubleVerificationURl " + "Breakpreetam heelo 1000756");
        //        string URI = "https://www.sbiepay.com/payagg/orderStatusQuery/getOrderStatusQuery";
        //        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
        //        var request = (HttpWebRequest)WebRequest.Create(URI);
        //        var postData = "queryRequest=|" + mid + "|" + trxid;
        //        postData += "&aggregatorId=SBIEPAY";
        //        postData += "&merchantId=" + mid + "";
        //        var data = Encoding.ASCII.GetBytes(postData);
        //        request.Method = "POST";
        //        request.ContentType = "application/x-www-form-urlencoded";
        //        request.ContentLength = data.Length;
        //        using (var stream = request.GetRequestStream())
        //        {
        //            stream.Write(data, 0, data.Length);
        //        }
        //        var response = (HttpWebResponse)request.GetResponse();

        //        //var responseString = "1000762|7092094895308|SUCCESS|IN|INR|MU40217991|78200463011141959420|1744|Transaction Paid Out|sbiepay|201918153234514|2019-06-30 16:48:41|DC|10007622019063000167|1000762|0.00^0.00||||||||||";// new StreamReader(response.GetResponseStream()).ReadToEnd();
        //        var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();

        //        TempData["responseString"] = responseString;

        //        SbiepayAdmission sbi = new SbiepayAdmission();
        //        var result = sbi.doubleverification(responseString);
        //        CommonMethod.WritetoNotepad(null, HttpContext.Request.Url.AbsolutePath, "DoubleVerificationURl " + "Break" + responseString);

        //        //Uri address = new Uri("https://test.sbiepay.com/payagg/orderStatusQuery/getOrderStatusQuery");

        //        //ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
        //        //ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3;

        //        //System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
        //        //StringBuilder sbPostData = new StringBuilder();
        //        //sbPostData.AppendFormat("queryRequest={0}", "|1000733|23380762618491910380");
        //        //sbPostData.AppendFormat("&aggregatorId={0}", "SBIEPAY");
        //        //sbPostData.AppendFormat("&merchantId={0}", "1000733");
        //        //using (WebClient webClient = new WebClient())
        //        //{
        //        //    var stream = webClient.OpenRead(address);
        //        //    using (StreamReader sr = new StreamReader(stream))
        //        //    {
        //        //        var page = sr.ReadToEnd();

        //        //        // return page;
        //        //    }
        //        //}
        //        //CommonMethod.WritetoNotepad(null, HttpContext.Request.Url.AbsolutePath, "DoubleVerificationURl " + "Breakpreetam heelo 1000733");
        //        //WebRequest webRequest = WebRequest.Create(@"https://www.sbiepay.com/payagg/orderStatusQuery/getOrderStatusQuery");
        //        //webRequest.ContentType = "text/html";
        //        //webRequest.Method = "POST";
        //        //string body = "...";
        //        //byte[] bytes = Encoding.ASCII.GetBytes(body);
        //        //webRequest.ContentLength = bytes.Length;
        //        //var os = webRequest.GetRequestStream();
        //        //os.Write(bytes, 0, bytes.Length);
        //        //os.Close();
        //        //webRequest.Timeout = 0; //setting the timeout to 0 causes the request to fail
        //        //WebResponse webResponse = webRequest.GetResponse(); //Exception thrown here ...
        //        //CommonMethod.WritetoNotepad(null, HttpContext.Request.Url.AbsolutePath, "DoubleVerificationURl " + "Breakpreetam heelo "+ webResponse);
        //        //return RedirectToAction("Index");
        //        //using (var wb = new WebClient())
        //        //{


        //        //    var data = new NameValueCollection();
        //        //    data["queryRequest"] = "|1000733|42067162616181939343";
        //        //    data["aggregatorId"] = "SBIEPAY";
        //        //    data["merchantId"] = "1000733";
        //        //    CommonMethod.WritetoNotepad(null, HttpContext.Request.Url.AbsolutePath, "DoubleVerificationURl MID:--" + "1000733");
        //        //    var response1 = wb.UploadValues("https://www.sbiepay.com/payagg/orderStatusQuery/getOrderStatusQuery", "POST", data);
        //        //    CommonMethod.WritetoNotepad(null, HttpContext.Request.Url.AbsolutePath, "DoubleVerificationURl " + response1);

        //        //}

        //        //using (var wb = new WebClient())
        //        //{
        //        //    var data = new NameValueCollection();
        //        //    data["queryRequest"] = "|1000003|2639506614091939495";
        //        //    data["aggregatorId"] = "SBIEPAY";
        //        //    data["merchantId"] = "1000003";
        //        //    CommonMethod.WritetoNotepad(null, HttpContext.Request.Url.AbsolutePath, "DoubleVerificationURl MID:--" + "1000003");
        //        //    var response1 = wb.UploadValues("https://www.sbiepay.com/payagg/orderStatusQuery/getOrderStatusQuery", "POST", data);
        //        //    CommonMethod.WritetoNotepad(null, HttpContext.Request.Url.AbsolutePath, "DoubleVerificationURl " + response1);

        //        //}
        //        // return RedirectToAction("Index");

        //        //using (var wb = new WebClient())
        //        //{
        //        //    var data = new NameValueCollection();
        //        //    data["queryRequest"] = "|1000756|23380762618491910380";
        //        //    data["aggregatorId"] = "SBIEPAY";
        //        //    data["merchantId"] = "1000756";
        //        //    CommonMethod.WritetoNotepad(null, HttpContext.Request.Url.AbsolutePath, "DoubleVerificationURl MID:--" + "1000756");
        //        //    var response1 = wb.UploadValues("https://www.sbiepay.com/payagg/orderStatusQuery/getOrderStatusQuery", "POST", data);
        //        //    CommonMethod.WritetoNotepad(null, HttpContext.Request.Url.AbsolutePath, "DoubleVerificationURl " + response1);

        //        //}
        //        //Call Send SMS API
        //        //string sendSMSUri = "https://test.sbiepay.com/payagg/orderStatusQuery/getOrderStatusQuery";
        //        ////Create HTTPWebrequest
        //        //HttpWebRequest httpWReq = (HttpWebRequest)WebRequest.Create(sendSMSUri);
        //        ////Prepare and Add URL Encoded data
        //        //UTF8Encoding encoding = new UTF8Encoding();
        //        //byte[] data = encoding.GetBytes(sbPostData.ToString());
        //        ////Specify post method
        //        //httpWReq.Method = "POST";
        //        //httpWReq.ContentType = "application/x-www-form-urlencoded";
        //        //httpWReq.ContentLength = data.Length;
        //        //using (Stream stream = httpWReq.GetRequestStream())
        //        //{

        //        //    stream.Write(data, 0, data.Length);
        //        //}
        //        ////Get the response
        //        //HttpWebResponse response = (HttpWebResponse)httpWReq.GetResponse();
        //        //StreamReader reader = new StreamReader(response.GetResponseStream());
        //        //string responseString = reader.ReadToEnd();
        //        //CommonMethod.WritetoNotepad(null, HttpContext.Request.Url.AbsolutePath, "DoubleVerificationURl admission Paymentgateway Pushresponse  url hit on Controller error"+ responseString);

        //        ////Close the response
        //        //reader.Close();
        //        //response.Close();
        //    }
        //    catch (SystemException ex)
        //    {
        //        CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "DoubleVerificationURl admission Paymentgateway Pushresponse  url hit on Controller error");
        //        return View();
        //    }
        //    return View();
        //}

        //[HttpPost]
        //public ActionResult DoubleVerificationURlsee(int id = 0, string trxid = "", string password = "", string mid = "")
        //{



        //    try
        //    {
        //        if (password != "Arora@321")
        //        {
        //            TempData["responseString"] = "Wrong Password !!";
        //            return View();
        //        }
        //        CommonMethod.WritetoNotepad(null, HttpContext.Request.Url.AbsolutePath, "DoubleVerificationURl " + "Breakpreetam heelo 1000756");
        //        string URI = "https://www.sbiepay.com/payagg/orderStatusQuery/getOrderStatusQuery";
        //        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
        //        var request = (HttpWebRequest)WebRequest.Create(URI);
        //        var postData = "queryRequest=|" + mid + "|" + trxid;
        //        postData += "&aggregatorId=SBIEPAY";
        //        postData += "&merchantId=" + mid + "";
        //        var data = Encoding.ASCII.GetBytes(postData);
        //        request.Method = "POST";
        //        request.ContentType = "application/x-www-form-urlencoded";
        //        request.ContentLength = data.Length;
        //        using (var stream = request.GetRequestStream())
        //        {
        //            stream.Write(data, 0, data.Length);
        //        }
        //        var response = (HttpWebResponse)request.GetResponse();

        //        //var responseString = "1000762|7092094895308|SUCCESS|IN|INR|MU40217991|78200463011141959420|1744|Transaction Paid Out|sbiepay|201918153234514|2019-06-30 16:48:41|DC|10007622019063000167|1000762|0.00^0.00||||||||||";// new StreamReader(response.GetResponseStream()).ReadToEnd();
        //        var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();

        //        TempData["responseString"] = responseString;
        //        return RedirectToAction("DoubleVerificationURl");
        //        //SbiepayAdmission sbi = new SbiepayAdmission();
        //        //var result = sbi.doubleverification(responseString);
        //        //CommonMethod.WritetoNotepad(null, HttpContext.Request.Url.AbsolutePath, "DoubleVerificationURl " + "Break" + responseString);

        //        //Uri address = new Uri("https://test.sbiepay.com/payagg/orderStatusQuery/getOrderStatusQuery");

        //        //ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
        //        //ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3;

        //        //System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
        //        //StringBuilder sbPostData = new StringBuilder();
        //        //sbPostData.AppendFormat("queryRequest={0}", "|1000733|23380762618491910380");
        //        //sbPostData.AppendFormat("&aggregatorId={0}", "SBIEPAY");
        //        //sbPostData.AppendFormat("&merchantId={0}", "1000733");
        //        //using (WebClient webClient = new WebClient())
        //        //{
        //        //    var stream = webClient.OpenRead(address);
        //        //    using (StreamReader sr = new StreamReader(stream))
        //        //    {
        //        //        var page = sr.ReadToEnd();

        //        //        // return page;
        //        //    }
        //        //}
        //        //CommonMethod.WritetoNotepad(null, HttpContext.Request.Url.AbsolutePath, "DoubleVerificationURl " + "Breakpreetam heelo 1000733");
        //        //WebRequest webRequest = WebRequest.Create(@"https://www.sbiepay.com/payagg/orderStatusQuery/getOrderStatusQuery");
        //        //webRequest.ContentType = "text/html";
        //        //webRequest.Method = "POST";
        //        //string body = "...";
        //        //byte[] bytes = Encoding.ASCII.GetBytes(body);
        //        //webRequest.ContentLength = bytes.Length;
        //        //var os = webRequest.GetRequestStream();
        //        //os.Write(bytes, 0, bytes.Length);
        //        //os.Close();
        //        //webRequest.Timeout = 0; //setting the timeout to 0 causes the request to fail
        //        //WebResponse webResponse = webRequest.GetResponse(); //Exception thrown here ...
        //        //CommonMethod.WritetoNotepad(null, HttpContext.Request.Url.AbsolutePath, "DoubleVerificationURl " + "Breakpreetam heelo "+ webResponse);
        //        //return RedirectToAction("Index");
        //        //using (var wb = new WebClient())
        //        //{


        //        //    var data = new NameValueCollection();
        //        //    data["queryRequest"] = "|1000733|42067162616181939343";
        //        //    data["aggregatorId"] = "SBIEPAY";
        //        //    data["merchantId"] = "1000733";
        //        //    CommonMethod.WritetoNotepad(null, HttpContext.Request.Url.AbsolutePath, "DoubleVerificationURl MID:--" + "1000733");
        //        //    var response1 = wb.UploadValues("https://www.sbiepay.com/payagg/orderStatusQuery/getOrderStatusQuery", "POST", data);
        //        //    CommonMethod.WritetoNotepad(null, HttpContext.Request.Url.AbsolutePath, "DoubleVerificationURl " + response1);

        //        //}

        //        //using (var wb = new WebClient())
        //        //{
        //        //    var data = new NameValueCollection();
        //        //    data["queryRequest"] = "|1000003|2639506614091939495";
        //        //    data["aggregatorId"] = "SBIEPAY";
        //        //    data["merchantId"] = "1000003";
        //        //    CommonMethod.WritetoNotepad(null, HttpContext.Request.Url.AbsolutePath, "DoubleVerificationURl MID:--" + "1000003");
        //        //    var response1 = wb.UploadValues("https://www.sbiepay.com/payagg/orderStatusQuery/getOrderStatusQuery", "POST", data);
        //        //    CommonMethod.WritetoNotepad(null, HttpContext.Request.Url.AbsolutePath, "DoubleVerificationURl " + response1);

        //        //}
        //        // return RedirectToAction("Index");

        //        //using (var wb = new WebClient())
        //        //{
        //        //    var data = new NameValueCollection();
        //        //    data["queryRequest"] = "|1000756|23380762618491910380";
        //        //    data["aggregatorId"] = "SBIEPAY";
        //        //    data["merchantId"] = "1000756";
        //        //    CommonMethod.WritetoNotepad(null, HttpContext.Request.Url.AbsolutePath, "DoubleVerificationURl MID:--" + "1000756");
        //        //    var response1 = wb.UploadValues("https://www.sbiepay.com/payagg/orderStatusQuery/getOrderStatusQuery", "POST", data);
        //        //    CommonMethod.WritetoNotepad(null, HttpContext.Request.Url.AbsolutePath, "DoubleVerificationURl " + response1);

        //        //}
        //        //Call Send SMS API
        //        //string sendSMSUri = "https://test.sbiepay.com/payagg/orderStatusQuery/getOrderStatusQuery";
        //        ////Create HTTPWebrequest
        //        //HttpWebRequest httpWReq = (HttpWebRequest)WebRequest.Create(sendSMSUri);
        //        ////Prepare and Add URL Encoded data
        //        //UTF8Encoding encoding = new UTF8Encoding();
        //        //byte[] data = encoding.GetBytes(sbPostData.ToString());
        //        ////Specify post method
        //        //httpWReq.Method = "POST";
        //        //httpWReq.ContentType = "application/x-www-form-urlencoded";
        //        //httpWReq.ContentLength = data.Length;
        //        //using (Stream stream = httpWReq.GetRequestStream())
        //        //{

        //        //    stream.Write(data, 0, data.Length);
        //        //}
        //        ////Get the response
        //        //HttpWebResponse response = (HttpWebResponse)httpWReq.GetResponse();
        //        //StreamReader reader = new StreamReader(response.GetResponseStream());
        //        //string responseString = reader.ReadToEnd();
        //        //CommonMethod.WritetoNotepad(null, HttpContext.Request.Url.AbsolutePath, "DoubleVerificationURl admission Paymentgateway Pushresponse  url hit on Controller error"+ responseString);

        //        ////Close the response
        //        //reader.Close();
        //        //response.Close();
        //    }
        //    catch (SystemException ex)
        //    {
        //        CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "DoubleVerificationURl admission Paymentgateway Pushresponse  url hit on Controller error");
        //        return View();
        //    }
        //    return View();
        //}
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

    }
}