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
    public class BPharmaController : Controller
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
                AcademicSession ac = new AcademicSession();
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
                stvalue.EducationType = Convert.ToInt32(Request.Form["educationtype1"] == "" ? "40" : Request.Form["educationtype1"]);
                // stvalue.CourseCategory = Convert.ToInt32(Request.Form["coursetype1"] == "" ? "0" : Request.Form["coursetype1"]);
                stvalue.CourseCategory = Convert.ToInt32(CommonSetting.coursecategory.BPharma);
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
                stvalue.session = ac.GetAcademiccurrentSession().ID;
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
              
                try
                {
                    
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
                
            }
            else
            {
                Login logmsg = new Login();
                logmsg.Message = "Please Again Fill From : ";
                return Json(logmsg, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult BpharmaStudentApplicationForm()
        {
            //return RedirectToAction("Index", "Login", new { area = "Student" });
            UserLogin.ExpireAllCookies();
            Commn_master com = new Commn_master();
            ViewBag.Admissiontype = com.getcommonMaster("AdmissionType");
            ViewBag.Educationtype = com.getcommonMaster("selectbycommonid", Convert.ToInt32(CommonSetting.Commonid.EducationtypeBPharma));
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
            objlist = com1.getqualificationst(Convert.ToInt32(CommonSetting.Commonid.EducationtypeBPharma)).ToList();

            // objlist = objlist.Where(x => x.ID != 12).ToList();

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
            ViewBag.CurrentSessionid = ac.GetAcademiccurrentSession().ID;
            ViewBag.CurrentSession = ac.GetAcademiccurrentSessionname(ac.GetAcademiccurrentSession().ID.ToString(), Convert.ToInt32(CommonSetting.coursecategory.BPharma)).sessionname;


            ViewBag.check_admissionopen = com.check_admission_open(ac.GetAcademiccurrentSession().ID, Convert.ToInt32(CommonSetting.Commonid.EducationtypeBPharma));

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


    }
}