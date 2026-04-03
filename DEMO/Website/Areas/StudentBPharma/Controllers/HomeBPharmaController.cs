using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataLayer;
using System.Data;
using System.IO;
using System.Drawing;
using Website.Areas.Student.Models;
using Website.Areas.StudentBPharma.Models;
using Website.Models;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.IO;
using System.Web.UI;
using System.Text;
using NReco.PdfGenerator;
using com.toml.dp.util;
using Website.Areas.Student.Models;
using Newtonsoft.Json;
using System.Security.Cryptography;
using System.Collections.Specialized;
namespace Website.Areas.StudentBPharma.Controllers
{
    [CookiesExpireFilterBPharma]
    public class HomeBPharmaController : Controller
    {
        // GET: Student/HomePG
        [VerifyUrlFilterAdminAttributeBPharma]
        public ActionResult Index()
        {
            BL_student_formcomplete bl = new BL_student_formcomplete();
            AcademicSession ac = new AcademicSession();
            int sessionid = ac.GetAcademiccurrentSession().ID;
            var result = bl.sp_st_check_details(ClsLanguage.GetCookies("NBApplicationNo"), sessionid.ToString());
            BL_PrintApplication ob = new BL_PrintApplication();
            string ApplicationID = ClsLanguage.GetCookies("NBApplicationNo");
            StudentLogin tblST = new StudentLogin();
            var obj1 = tblST.BasicDetail(ApplicationID);
            Recruitment rec = new Recruitment();
            RecruitmentList reclist = new RecruitmentList();
            reclist = rec.view1customfeesubmittedstudentdetailList(obj1.Id);
            rec = reclist.qlist.ToList().FirstOrDefault();
            ViewBag.sessionid = obj1.session;
            var res1 = bl.CheckAdmission_details(sessionid);
            ViewBag.IsDocVerify = res1.IsDocVerify;
            ViewBag.IsAdmisApplied = res1.IsApplied;
            ViewBag.IsAppliedDate = res1.IsAppliedDate;
            ViewBag.IsDocVerifyDate = res1.IsDocVerifyDate;
            ViewBag.isfeesubmitt = res1.isfeesubmitt;
            ViewBag.IsfeesubmitDate = res1.IsfeesubmitDate;
            ViewBag.isfeesubmitt2 = res1.isfeesubmitt2;
            ViewBag.IsfeesubmitDate2 = res1.IsfeesubmitDate2;
            ViewBag.castecategory = obj1.CastCategory;
            ViewBag.incomecertificate_iseligible = obj1.incomecertificate_iseligible;
            ViewBag.incomecertificate = obj1.incomecertificate;
            ViewBag.incomeRejectReaseon = obj1.incomeRejectReaseon;
            ViewBag.isback = 0;
            List<EaxmFeesSubmit> subjectlist = new List<EaxmFeesSubmit>();
            ExamForm exam = new ExamForm();
            if (rec != null)
            {
                ViewBag.Stream = rec.StreamCategoryName;
                ViewBag.enrollnmentno = rec.enrollmentno;
                ViewBag.rollno = rec.RollNo;
                ViewBag.Sessionname = rec.Session;
                ViewBag.studentname = rec.StudentName;
                ViewBag.fathername = rec.FatherName;
                ViewBag.mothername = rec.mothername;
                ViewBag.DOb = rec.DOB;
                ViewBag.Gender = rec.Gender;
                ViewBag.category = rec.StudentCasteCategoryName;
                ViewBag.Nationlity = rec.Nationlity;
                ViewBag.stphoto = rec.stphoto;
                ViewBag.stsign = rec.stsign;
                ViewBag.Course = rec.coursecategotyName;
                ViewBag.ftitle = obj1.Ftitle;
               
                //if (obj1.CourseCategory == 28)
                //{
                //    if (obj1.session != 39)
                //    {
                //        // for manual check , kon ses semeseter ka back niklana h , 29 for second part
                //        subjectlist = exam.backFeesDetailSubjectlist_BPharma(obj1.CourseCategory, 1140, 29, obj1.session, rec.collegeid, obj1.Id);
                //        if (subjectlist.Count > 0)
                //        {
                //            ViewBag.isbackpart2 = 1;
                //            ViewBag.courseyearidenc = EncriptDecript.Encrypt(29.ToString());
                //        }
                //        // for manual check , kon ses semeseter ka back niklana h , 28 for first part
                //        subjectlist = exam.backFeesDetailSubjectlist_BPharma(obj1.CourseCategory, 1140, 28, obj1.session, rec.collegeid, obj1.Id);
                //        if (subjectlist.Count > 0)
                //        {
                //            ViewBag.isback = 1;
                //            ViewBag.courseyearidenc = EncriptDecript.Encrypt(28.ToString());
                //        }
                //    }
                //    else
                //    {
                //        // for manual check , kon ses semeseter ka back niklana h , 28 for first part
                //        subjectlist = exam.backFeesDetailSubjectlist_BPharma(obj1.CourseCategory, 1140, 28, obj1.session, rec.collegeid, obj1.Id);
                //        if (subjectlist.Count > 0)
                //        {
                //            ViewBag.isback = 1;
                //            ViewBag.courseyearidenc = EncriptDecript.Encrypt(28.ToString());
                //        }
                //    }
                //}

            }
            else
            {
                ViewBag.Stream = "";
                ViewBag.Course = "";
                ViewBag.enrollnmentno = "";
                ViewBag.rollno = "";
                ViewBag.Sessionname = "";
                ViewBag.studentname = "";
                ViewBag.fathername = "";
                ViewBag.mothername = "";
                ViewBag.DOb = "";
                ViewBag.Gender = "";
                ViewBag.category = "";
                ViewBag.Nationlity = "";
                ViewBag.stphoto = "";
                ViewBag.stsign = "";
                ViewBag.ftitle = obj1.Ftitle;
            }
            if (obj1 != null)
            {
                int educationtype = obj1.EducationType;
                //var dateextend = ob.CheckStudentAddmisionExtendDate(sessionid, educationtype);
                //ViewBag.addmissionExtenddate = dateextend.Status;
                //ViewBag.addmissionExtenddateValue = dateextend.ExtendDate;
                //var datestart = ob.CheckStudentAddmisionStartDate(sessionid, educationtype);
                //ViewBag.addmissionStartdate = datestart.Status;
                //ViewBag.addmissionStartdateValue = datestart.startdate;
                ////ViewBag.IsApplied = ob.CheckStudentApplied(sessionid).Status;
                ////ViewBag.IsVerify = ob.CheckStudentVerify(sessionid).Status;
                //var dateextenddoc = ob.documnetCheckStudentAddmisionExtendDate(sessionid, educationtype);
                //ViewBag.addmissionExtenddateValuedoc = dateextenddoc.ExtendDate;
                //var datestartdoc = ob.documentCheckStudentAddmisionStartDate(sessionid, educationtype);
                //ViewBag.addmissionStartdateValuedoc = datestartdoc.startdate;
                ////ViewBag.IsApplied = ob.CheckStudentApplied(sessionid).Status;
                //ViewBag.rejectreason = ob.CheckDocumentVerification(sessionid).rejectreason;
            }
            var obj = ob.CheckStudentAdmission(sessionid);
            ViewBag.sessionid = sessionid;
            ViewBag.studentyear = obj1.StudentYear;
            if (obj.Status == true)
            {
                ViewBag.Status = obj.Status;
                ViewBag.Course = obj.CourseName;
                ViewBag.College = obj.CollegeName;
            }
            else
            {
                ViewBag.Status = false;
                ViewBag.Course = "";
                ViewBag.College = "";
            }

            //var resultexam = exam.Feesischeckexamfeesubmit(obj1.Id, obj1.session);// check main exam fees submit or not 
            //if (resultexam == null)
            //{ ViewBag.Examfeesubmit = 0; }
            //else
            //{ ViewBag.Examfeesubmit = resultexam.IsExamfeesubmit; }
            return View(result);
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult EnrollSlipdownload()
        {
            string InpuData = HttpContext.Request.Form["data1"];       //Div Content will be fetched from form data.
                                                                       //If You find this error in above line 
                                                                       //Error: A potentially dangerous Request.Form value was detected from the client (data="<html><head><title><...").
                                                                       //then add this in your Web.config file.
                                                                       // <httpRuntime requestValidationMode="2.0" /> inside   <system.web>  </system.web>
            string PageType = HttpContext.Request.Form["PageType1"];  //here we will recieve Page Type sent from front end.
            string dataname = HttpContext.Request.Form["dataname1"];  // here we declare for filename for download
            if (string.IsNullOrEmpty(InpuData))
                InpuData = "Some Error occured.Content not found.Please try again.";

            string appPath = HttpContext.Request.PhysicalApplicationPath;

            var htmlContent = InpuData.Replace("AppPath", appPath);
            var pdfDoc = new NReco.PdfGenerator.HtmlToPdfConverter(); //created an object of HtmlToPdfConverter class.

            if (string.IsNullOrEmpty(PageType))
                pdfDoc.Orientation = NReco.PdfGenerator.PageOrientation.Default;  //setting orientation.
            else
            {
                if (PageType == "Landscape")
                    pdfDoc.Orientation = NReco.PdfGenerator.PageOrientation.Landscape;
                else
                    pdfDoc.Orientation = NReco.PdfGenerator.PageOrientation.Portrait;
            }

            pdfDoc.Size = NReco.PdfGenerator.PageSize.A4;   //8.27 in × 11.02 in //Page Size
            NReco.PdfGenerator.PageMargins pageMargins = new NReco.PdfGenerator.PageMargins();     //Margins in mm
            pageMargins.Bottom = 05;
            pageMargins.Left = 05;
            pageMargins.Right = 05;
            pageMargins.Top = 05;
            pdfDoc.Margins = pageMargins;                      //margins added to PDF.

            //Why I am adding this to page footer?? So we can get paging in footer section of each PDF page.how its working?? A Javascript code is written inside the DLL which is handling  Div's class i.e. page and topage.
            var pdfBytes = pdfDoc.GeneratePdf(htmlContent);
            HttpContext.Response.ContentType = "application/pdf";
            HttpContext.Response.AddHeader("content-disposition", "attachment;filename=" + dataname.Replace(" ", "") + ".pdf");//Use inline in place of attachment If You wish to open PDF on Browser.
            HttpContext.Response.Cache.SetCacheability(HttpCacheability.NoCache);
            HttpContext.Response.BinaryWrite(pdfBytes);
            HttpContext.Response.End();
            return View();

        }
        [VerifyUrlFilterAdminAttributeBPharma]
        public ActionResult BasicDetail()
        {
            Commn_master com = new Commn_master();
            AcademicSession ac = new AcademicSession();
            ViewBag.Admissiontype = com.getcommonMaster("AdmissionType");
            ViewBag.Educationtype = com.getcommonMaster("EducationType");
            ViewBag.Gender = com.getcommonMaster("Gender");
            ViewBag.CasteCategory = com.getcommonMaster("CasteCategory");
            ViewBag.bloodgroup = com.Getbloodgroup("Select");
            Country cont = new Country();
            ViewBag.country = cont.GetAllCountries();
            ViewBag.Pcountry = cont.GetAllCountries();
            ViewBag.stitle = com.getcommonMaster("Title");
            ViewBag.ftitle = com.getcommonMaster("TitleM");
            ViewBag.Nationality = com.getcommonMaster("Nationality");
            ViewBag.Religion = com.getcommonMaster("Religion");
            string ApplicationID = ClsLanguage.GetCookies("NBApplicationNo");
            StudentLogin tblST = new StudentLogin();
            StudentAdmissionQualification stad = new StudentAdmissionQualification();
            ViewBag.Qualification = stad.GetQualifiationMasterOldStudentBpharma();
            var obj = tblST.BasicDetail(ApplicationID);
            ViewBag.MotherTongue = com.getcommonMaster("MotherTongue");
            // Commn_master com = new Commn_master();
            ViewBag.Course = com.getcommonMaster("Course", obj.EducationType);
            ViewBag.Stream = com.getcommonMaster("Stream", obj.CourseCategory);
            State st = new State();
            ViewBag.CAState = st.GetStateListByCountryId(obj.CA_Country.ToString());
            ViewBag.PAState = st.GetStateListByCountryId(obj.PA_Country.ToString());
            // obj.AdmisitionCategory=
            ViewBag.bloodgroupid = obj.BloodGroup;
            ViewBag.castid = obj.CastCategory;
            ViewBag.genderid = obj.Gender;
            ViewBag.eduid = obj.EducationType;
            ViewBag.titileid = obj.title;
            ViewBag.ftitileid = obj.Ftitle;
            ViewBag.Nationalityid = obj.Nationality;
            ViewBag.Religionid = obj.Religion;
            ViewBag.MotherTongueid = obj.MotherTongue;
            ViewBag.addmissionExtenddate = com.check_admission_open(ac.GetAcademiccurrentSession().ID, obj.EducationType);

            return View(obj);
        }
        [VerifyUrlFilterAdminAttributeBPharma]
        [HttpPost]
        public ActionResult BasicDetail(Login objlogin, HttpPostedFileBase sign, HttpPostedFileBase photo)
        {
            StudentLogin st = new StudentLogin();
            Commn_master com = new Commn_master();
            AcademicSession ac = new AcademicSession();
            StudentAdmissionQualification stad = new StudentAdmissionQualification();
            ViewBag.Qualification = stad.GetQualifiationMasterOldStudentPG();
            ViewBag.Admissiontype = com.getcommonMaster("AdmissionType");
            ViewBag.Educationtype = com.getcommonMaster("EducationType");
            ViewBag.Gender = com.getcommonMaster("Gender");
            ViewBag.CasteCategory = com.getcommonMaster("CasteCategory");
            ViewBag.bloodgroup = com.Getbloodgroup("Select");
            ViewBag.stitle = com.getcommonMaster("Title");
            ViewBag.ftitle = com.getcommonMaster("TitleM");
            Country cont = new Country();
            ViewBag.country = cont.GetAllCountries();
            ViewBag.Pcountry = cont.GetAllCountries();
            ViewBag.Course = com.getcommonMaster("Course", objlogin.EducationType);
            ViewBag.Stream = com.getcommonMaster("Stream", objlogin.CourseCategory);
            ViewBag.Nationality = com.getcommonMaster("Nationality");
            ViewBag.Religion = com.getcommonMaster("Religion");
            ViewBag.MotherTongue = com.getcommonMaster("MotherTongue");
            State st11 = new State();
            ViewBag.CAState = st11.GetStateListByCountryId(objlogin.CA_Country.ToString());
            ViewBag.PAState = st11.GetStateListByCountryId(objlogin.PA_Country.ToString());
            ViewBag.bloodgroupid = objlogin.BloodGroup;
            ViewBag.castid = objlogin.CastCategory;
            ViewBag.genderid = objlogin.Gender;
            ViewBag.eduid = objlogin.EducationType;
            ViewBag.titileid = objlogin.title;
            ViewBag.ftitileid = objlogin.Ftitle;
            ViewBag.Nationalityid = objlogin.Nationality;
            ViewBag.Religionid = objlogin.Religion;
            ViewBag.MotherTongueid = objlogin.MotherTongue;
            ViewBag.addmissionExtenddate = com.check_admission_open(ac.GetAcademiccurrentSession().ID, objlogin.EducationType);
            BL_PrintApplication ob = new BL_PrintApplication();
            objlogin.session = ac.GetAcademiccurrentSession().ID;
            string ApplicationID = ClsLanguage.GetCookies("NBApplicationNo");
            StudentLogin tblST = new StudentLogin();
            var obj = tblST.BasicDetail(ApplicationID);
            if (obj.Gender == Convert.ToInt32(CommonSetting.Commonid.Femalegender))
            {
                ViewBag.CasteCategory = com.getcommonMaster("CasteCategory");
            }
            else
            {
                ViewBag.CasteCategory = com.getcommonMaster("CastwithoutWBC");
            }
            objlogin.EducationType = obj.EducationType;
            objlogin.AdmisitionCategory = obj.AdmisitionCategory;
            objlogin.EducationType = obj.EducationType;
            objlogin.CourseCategory = obj.CourseCategory;
            objlogin.session = obj.session;
            ViewBag.eduid = obj.EducationType;
            if (objlogin.is_GEW == true)
            {
                if (objlogin.CastCategory != 4)
                {
                    TempData["StMessage"] = "General Economical Weaker Only for General Category Student,Please Change !!";
                    return RedirectToAction("BasicDetail");
                }
            }
            string jsonstring = JsonConvert.SerializeObject(objlogin);
            if (objlogin.Gender != 9)
            {
                if (objlogin.CastCategory == 23)
                {
                    TempData["StMessage"] = "Please Select Another Caste !!";
                    return RedirectToAction("BasicDetail");
                }
            }
            try
            {
                if (photo != null)
                {
                    Stream st1 = photo.InputStream;
                    string name = Path.GetFileName(photo.FileName);
                    try
                    {
                        string myBucketName = "mungeruniversity"; //your s3 bucket name goes here  
                        string s3DirectoryName = "Student/Photoandsign";
                        string s3FileName = System.DateTime.Now.ToString("dd_MM_yyyy_HH_mm_ss") + "_StudentPhoto_" + objlogin.FirstName + @name;
                        s3FileName = s3FileName.Replace(" ", "");
                        objlogin.stphoto = s3FileName;
                        bool a;
                        AmazonUploader myUploader = new AmazonUploader();
                        a = myUploader.sendMyFileToFolder(photo,Server.MapPath("~/App_Data/uploads"),s3FileName);
                    }
                    catch (Exception ex)
                    {
                        CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "Basic Detail post Method", ClsLanguage.GetCookies("NBApplicationNo") + "   " + jsonstring);
                    }
                }
                if (sign != null)
                {
                    Stream st1 = sign.InputStream;
                    string name = Path.GetFileName(sign.FileName);
                    try
                    {
                        string myBucketName = "mungeruniversity"; //your s3 bucket name goes here  
                        string s3DirectoryName = "Student/Photoandsign";
                        string s3FileName = System.DateTime.Now.ToString("dd_MM_yyyy_HH_mm_ss") + "_StudentSign_" + objlogin.FirstName + @name;
                        s3FileName = s3FileName.Replace(" ", "");
                        objlogin.stsign = s3FileName;
                        bool a;
                        AmazonUploader myUploader = new AmazonUploader();
                        a = myUploader.sendMyFileToFolder(photo,Server.MapPath("~/App_Data/uploads"),s3FileName);
                    }
                    catch (Exception ex)
                    {
                        CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "Basic Detail post Method", ClsLanguage.GetCookies("NBApplicationNo") + "   " + jsonstring);
                    }
                }
                var result = st.Student_registrationUpdateOldStudentPG(objlogin);
                if (result.status == true)
                {
                    TempData["StMessage"] = result.Message + result.ApplicationNo;
                    return RedirectToAction("BasicDetail");
                }
                else { TempData["StMessage"] = result.Message; }
            }
            catch (Exception ex)
            {
                CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "Basic Detail post Method", ClsLanguage.GetCookies("NBApplicationNo") + "   " + jsonstring);
            }
            return View(objlogin);
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
        public ActionResult StudentDelete(string id = "")
        {
            StudentAdmissionQualification obj = new StudentAdmissionQualification();
            string enID = "0";
            if (id != "0" && id.Length > 0)
            {
                enID = EncriptDecript.Decrypt(id);
            }
            var res = obj.GetQualifiationByID(Convert.ToInt32(enID));
            var fullPath = res.FileURl;
            var result = obj.DeleteQualifiationByID_old(Convert.ToInt32(enID));
            TempData["Msg"] = result.Msg;
            return RedirectToAction("StudentQualification");
        }
        public void Quali_bind(int edu = 0, int pre = 0, string edit = "", int sid = 0)
        {
            StudentAdmissionQualification obj = new StudentAdmissionQualification();
            List<QualifiationMaster> list = obj.GetQualifiationMaster(edu, pre, edit, sid);
            List<SelectListItem> slist = new List<SelectListItem>();
            foreach (QualifiationMaster dr in list)
            {
                slist.Add(new SelectListItem { Text = dr.QualificationType, Value = dr.ID.ToString() });
            }
            ViewBag.Qualification = slist;
        }
        public void QualiSubject_bind(int edu = 0)
        {
            StudentAdmissionQualification obj = new StudentAdmissionQualification();
            List<QualifiationMaster> list = obj.GetQualifiationMasterBYyear(edu);
            List<SelectListItem> slist = new List<SelectListItem>();
            foreach (QualifiationMaster dr in list)
            {
                slist.Add(new SelectListItem { Text = dr.QualificationType, Value = dr.ID.ToString() });
            }
            ViewBag.Qualification = slist;
        }
        public void Subject_bind(int id = 0)
        {
            StudentPreviousQualification obj = new StudentPreviousQualification();
            List<SubjectMaster> list = new List<SubjectMaster>();
            var ApplicationNo = ClsLanguage.GetCookies("NBApplicationNo");
            Login objl = new Login();
            StudentLogin objs = new StudentLogin();
            objl = objs.BasicDetail(ApplicationNo);
            string board = "";
            if (objl.prevoiusboardid == 2)
            {
                board = "2";
            }
            else
            {
                board = "1";
            }
            if (id == 2)
            {
                list = obj.GetSubject(Convert.ToInt32(CommonSetting.Streamtype.Art12), board);
            }
            if (id == 3)
            {
                list = obj.GetSubject(Convert.ToInt32(CommonSetting.Streamtype.Science12), board);
            }
            if (id == 4)
            {
                list = obj.GetSubject(Convert.ToInt32(CommonSetting.Streamtype.Comm12), board);
            }
            List<SelectListItem> slist = new List<SelectListItem>();

            foreach (SubjectMaster dr in list)
            {
                slist.Add(new SelectListItem { Text = dr.SubjectName, Value = dr.ID.ToString() });
            }
            ViewBag.Subject = slist;
        }
        public JsonResult Subject_bindDanamic(int id = 0, string res = "")
        {
            StudentPreviousQualification obj = new StudentPreviousQualification();
            List<SubjectMaster> list = new List<SubjectMaster>();
            var ApplicationNo = ClsLanguage.GetCookies("NBApplicationNo");
            Login objl = new Login();
            StudentLogin objs = new StudentLogin();
            objl = objs.BasicDetail(ApplicationNo);
            string board = "";
            if (objl.prevoiusboardid == 2)
            {
                board = "2";
            }
            else
            {
                board = "1";
            }

            if (id == 2)
            {
                list = obj.GetSubjectBYID(1, res, board);
            }
            if (id == 3)
            {
                list = obj.GetSubjectBYID(2, res, board);
            }
            if (id == 4)
            {
                list = obj.GetSubjectBYID(3, res, board);
            }
            List<SelectListItem> slist = new List<SelectListItem>();
            foreach (SubjectMaster dr in list)
            {
                slist.Add(new SelectListItem { Text = dr.SubjectName, Value = dr.ID.ToString() });
            }
            ViewBag.Subject = slist;
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        public JsonResult AddNewQualification(string id = "")
        {
            StudentAdmissionQualification ob = new StudentAdmissionQualification();
            if (Request.Form.Count > 0)
            {
                StudentAdmissionQualification doc = new StudentAdmissionQualification();
                doc.QualicationType = Convert.ToInt32(Request.Form["Qualification"] == "" ? "0" : Request.Form["Qualification"]);
                doc.Board_UniversityName = Request.Form["UniversityName"];
                doc.Percentage = Convert.ToDecimal(Request.Form["Percentage"] == "" ? "0" : Request.Form["Percentage"]);
                doc.ID = Convert.ToInt32(Request.Form["ID"] == "" ? "0" : Request.Form["ID"]);
                doc.PassingYear = Request.Form["PassingYear"];
                doc.RollNo = Request.Form["RollNo"];
                doc.hfile = Request.Form["hfile"];
                DataLayer.Login objl = new DataLayer.Login();
                StudentLogin objs = new StudentLogin();
                if (doc.Percentage == 0)
                {
                    StudentAdmissionQualification doc1 = new StudentAdmissionQualification();
                    doc1.Msg = "Please Fill Aggregate Percentage ,Or Fill Again form !!";
                    return Json(doc1, JsonRequestBehavior.AllowGet);
                }
                if (doc.Percentage > 100)
                {
                    StudentAdmissionQualification doc1 = new StudentAdmissionQualification();
                    doc1.Msg = "Please Fill Invalid Percentage ,Or Fill Again form !!";
                    return Json(doc1, JsonRequestBehavior.AllowGet);
                }
                doc.HonoursPercentage = doc.Percentage;
                doc.ApplicationNo = ClsLanguage.GetCookies("NBApplicationNo");
                doc.SID = Convert.ToInt32(ClsLanguage.GetCookies("NBStID"));
                doc.session = Convert.ToInt32((ClsLanguage.GetCookies("NBSission"))); ;
                var resultyear = doc.Checkpassingyear(doc.PassingYear, doc.SID, doc.ID.ToString());
                if (resultyear.Status)
                {
                    StudentAdmissionQualification doc1 = new StudentAdmissionQualification();
                    doc1.Msg = resultyear.Msg;
                    return Json(doc1, JsonRequestBehavior.AllowGet);
                }
                StudentAdmissionQualification result = new StudentAdmissionQualification();
                string jsonstring = JsonConvert.SerializeObject(doc);
                if (Request.Files.Count > 0)
                {
                    try
                    {
                        if (Request.Files.GetKey(0) == "file")
                        {
                            HttpPostedFileBase fileUpload = Request.Files.Get(0);
                            if (Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                            {
                                string[] testfiles = fileUpload.FileName.Split(new char[] { '\\' });
                            }
                            Stream st1 = fileUpload.InputStream;
                            string name = Path.GetFileName(fileUpload.FileName);
                            try
                            {
                                string myBucketName = "mungeruniversity"; //your s3 bucket name goes here  
                                string s3DirectoryName = "Student/Document";
                                string s3FileName = System.DateTime.Now.ToString("dd_MM_yyyy_HH_mm_ss") + "_StudentDocument_" + objl.FirstName + @name;
                                s3FileName = s3FileName.Replace(" ", "");
                                doc.FileURl = s3FileName;
                                bool a;
                                AmazonUploader myUploader = new AmazonUploader();
                                a = myUploader.sendMyFileToFolder(fileUpload, Server.MapPath("~/App_Data/uploads"),s3FileName);
                            }
                            catch (Exception ex)
                            {
                                CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "Employee : document Image Upload", "DocumentType1" + jsonstring);
                            }
                        }
                        else
                        {
                            StudentAdmissionQualification ob1 = new StudentAdmissionQualification();
                            ob1.Msg = "Error occurred. Error details: ";
                            return Json(ob1, JsonRequestBehavior.AllowGet);
                        }
                    }
                    catch (Exception ex)
                    {
                        CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "Employee : document Image Upload", "DocumentType1" + jsonstring);
                        StudentAdmissionQualification ob3 = new StudentAdmissionQualification();
                        ob3.Msg = "Error occurred. Error details: " + ex.Message;
                        return Json(ob3, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    doc.FileURl = doc.hfile;
                }
                result = ob.SaveQualificationDetailsForOldStudentPG(doc);
                if (result.Status)
                {
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            else
            {
                StudentAdmissionQualification logmsg = new StudentAdmissionQualification();
                logmsg.Msg = "Error occurred. Error details: ";
                return Json(logmsg, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult SubjectDetails(List<StudentPreviousQualification> mem)
        {
            StudentPreviousQualification obj = new StudentPreviousQualification();
            bool result = false;
            try
            {
                if (mem.Count >= 6) // 6*6 or 7*7 ka matrix
                {
                    result = obj.Insert(mem);
                }
                else
                {
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "Adding subject details for Intermidate", ClsLanguage.GetCookies("NBApplicationNo"));
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult SubjectDetailsO(List<StudentPreviousQualification> mem)
        {
            StudentPreviousQualification obj = new StudentPreviousQualification();
            bool result = false;
            try
            {
                if (mem.Count == 5)// 5*5 ka matrix
                {
                    result = obj.Insert(mem);
                }
                else
                {
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "Adding subject details for Intermidate", ClsLanguage.GetCookies("NBApplicationNo"));
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult SubjectTable(int id = 0)
        {
            var ApplicationNo = ClsLanguage.GetCookies("NBApplicationNo");
            Login objl = new Login();
            StudentLogin objs = new StudentLogin();
            objl = objs.BasicDetail(ApplicationNo);
            if (objl.previous_qua_id == id)
            {
                var result = true;
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var result = false;
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }
        [VerifyUrlFilterAdminAttributeBPharma]
        public ActionResult SelectSubject(int id = 0)
        {
            StudentAdmissionQualification obj = new StudentAdmissionQualification();
            var ApplicationNo = ClsLanguage.GetCookies("NBApplicationNo");
            Login objl = new Login();
            StudentLogin objs = new StudentLogin();
            AcademicSession ac = new AcademicSession();
            objl = objs.BasicDetail(ApplicationNo);
            StudentPreviousQualification StudentPrevious = new StudentPreviousQualification();
            
            BL_PrintApplication obrecrei = new BL_PrintApplication();
            Student_Admission_Choicesubject Choicesubject = new Student_Admission_Choicesubject();
         
            if (objl != null)
            {
                QualiSubject_bind(objl.previous_qua_id);
                Subject_bind(objl.previous_qua_id);
            }
            FeesSubmit log = new FeesSubmit();
            var listgeo = log.Showgeography().ToList();
            bool alreadyExist = listgeo.Contains(objl.Id);
            ViewBag.editrecoer = alreadyExist;

            Commn_master com = new Commn_master();
            ViewBag.check_admissionopen = com.check_admission_open(ac.GetAcademiccurrentSession().ID, Convert.ToInt32(CommonSetting.Commonid.EducationtypeBPharma));

            BL_CollegeMaster obj111 = new BL_CollegeMaster();
            CollageList sub = new CollageList();
            ViewBag.gender = objl.Gender;
            ViewBag.sessionid = objl.session;
            if (objl.Gender == 9)
            {
                sub = obj111.collagedetailviewlistdropallotedmalihacollege(objl.CourseCategory, ac.GetAcademiccurrentSession().ID);
            }
            else
            {
                sub = obj111.collagedetailviewlistdropalloted(objl.CourseCategory, ac.GetAcademiccurrentSession().ID);
            }
            ViewBag.College = sub.qlist;
            ViewBag.Course = com.getcommonMaster("Course", objl.EducationType);
            ViewBag.Coursecategoryid = objl.CourseCategory;
            List<SelectListItem> ob = new List<SelectListItem>();
            for (int i = 1995; i <= 2019; i++)
            {
                ob.Add(new SelectListItem { Text = i.ToString(), Value = i.ToString() });
            }
            ViewBag.year = ob;
            List<StudentPreviousQualification> list = new List<StudentPreviousQualification>();
            for (int i = 0; i < 1; i++)
            {
                list.Add(new StudentPreviousQualification());
            }
            var tuple = new Tuple<StudentAdmissionQualification, List<StudentPreviousQualification>, Login>(obj, list, objl);
            ViewBag.percentage = StudentPrevious.getqualify_percentage(objl.CourseCategory, objl.issame_stream);
            var countlist = StudentPrevious.GetSubjectPercentageData(objl.ApplicationNo);
            var objrecritiny = obrecrei.CheckStudentAdmission(ac.GetAcademiccurrentSession().ID);
            ViewBag.Statusrecruitny = false;
            if (objrecritiny.Status == true)
            {
                ViewBag.Statusrecruitny = objrecritiny.Status;
            }

            if (objl.previous_qua_id != 11)
            {
                if (countlist.Count == 0)
                {
                    // TempData["selectsubmsg"] = "Please First Upload your Qualification!!";
                    // return View(tuple);
                }
            }

            //if (objl.issame_stream == false)
            //{
            if (objl.session != 39)
            {
                var countlistcheck = StudentPrevious.PG_honours_percentage_check(objl.Id.ToString(), objl.previous_qua_id);

                if (countlistcheck == null)
                {
                    TempData["selectsubmsg"] = "First Upload For Previous year Qualification !!!";
                }
                else
                {
                    if (countlistcheck.Status == false)
                    {
                        TempData["selectsubmsg"] = countlistcheck.Msg;
                    }
                }

                //}
                //if (objl.issame_stream == true)
                //{

                var countlistcheck1 = StudentPrevious.GetSubjecissamestream_percentage(objl.Id.ToString(), objl.previous_qua_id, objl.CourseCategory);

                if (countlistcheck1 == null)
                {
                    TempData["selectsubmsg"] = "First Upload For Previous year Qualification !!!";
                }
                else
                {
                    if (countlistcheck1.Status == false)
                    {
                        TempData["selectsubmsg"] = countlistcheck1.Msg;
                    }
                }
            }
            //}

            var Choicesubjectlist = Choicesubject.viewst_choicesubject(objl.Id);
            if (Choicesubjectlist.Count == 0)
            {

            }
            else
            {
                TempData["selectalready"] = "already";
                ViewBag.Choicesubject = Choicesubjectlist;
            }
            var ischeck_failedorpass = StudentPrevious.ischeck_failedorpass(objl.Id.ToString());
            if (ischeck_failedorpass.Status == true)
            {
                TempData["selectsubmsg"] = ischeck_failedorpass.Msg;
                return View(tuple);
            }
            return View(tuple);
        }
        [HttpPost]
        public JsonResult againfillform()
        {
            Student_Admission_Choicesubject obj = new Student_Admission_Choicesubject();
            var ApplicationNo = ClsLanguage.GetCookies("NBApplicationNo");
            Login objl = new Login();
            StudentLogin objs = new StudentLogin();
            try
            {
                objl = objs.BasicDetail(ApplicationNo);
                var result = obj.againfillform(objl.Id, objl.session);

                return Json(new { data = result, success = true });
            }
            catch (Exception ex)
            {
                CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "Saving subject Choice ", ClsLanguage.GetCookies("NBApplicationNo"));
                return Json(new { data = obj, success = true });
            }

        }
        [HttpPost]
        public JsonResult savest_choicesubject(string collegeidlist,string hounors_subjectidlist)
        {
            Student_Admission_Choicesubject obj = new Student_Admission_Choicesubject();
            var ApplicationNo = ClsLanguage.GetCookies("NBApplicationNo");
            Login objl = new Login();
            StudentLogin objs = new StudentLogin();

            try
            {
                objl = objs.BasicDetail(ApplicationNo);
                obj.SID = objl.Id;
                obj.hounors_subjectidlist = hounors_subjectidlist;
                obj.collegeidlist = collegeidlist;
                obj.Subsidiary1_subjectidlist = CommonSetting.Removenumber(collegeidlist);
                obj.Subsidiary2_subjectidlist = CommonSetting.Removenumber(collegeidlist);
                obj.Compulsory1_subjectidlist = CommonSetting.Removenumber(collegeidlist);
                obj.Compulsory2_subjectidlist = CommonSetting.Removenumber(collegeidlist);
                var result = obj.savest_choicesubject(obj);
                return Json(new { data = result, success = true });
            }
            catch (Exception ex)
            {
                CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "Saving subject Choice ", ClsLanguage.GetCookies("NBApplicationNo") + "   hounors_subjectidlist: " + " + " + collegeidlist + " + collegeidlist +  + Subsidiary1_subjectidlist + + Subsidiary2_subjectidlist + + Compulsory1_subjectidlist + :");
                return Json(new { data = new Student_Admission_Choicesubject(), success = true });
            }


        }
        public JsonResult getcollegelist(int Coursecategoryid, int isgender)
        {
            BL_CollegeMaster obj111 = new BL_CollegeMaster();
            CollageList sub = new CollageList();
            AcademicSession ac = new AcademicSession();
            if (isgender == 9)
            {
                sub = obj111.collagedetailviewlistdropallotedmalihacollege(Coursecategoryid, ac.GetAcademiccurrentSession().ID);
            }
            else
            {
                sub = obj111.collagedetailviewlistdropalloted(Coursecategoryid, ac.GetAcademiccurrentSession().ID);
            }
            return Json(new { data = sub.qlist, success = true });
        }
        public JsonResult getcollegesubjects(int id)
        {
            StudentAdmissionQualification obj = new StudentAdmissionQualification();
            var ApplicationNo = ClsLanguage.GetCookies("NBApplicationNo");
            Login objl = new Login();
            StudentLogin objs = new StudentLogin();
            objl = objs.BasicDetail(ApplicationNo);
            var result = new List<BL_StreamMaster>();
            BL_StreamMaster objStream = new BL_StreamMaster();
            if (objl.CourseCategory == 3)
            { result = objStream.getcollegesubjects(objl.Id, 20, id, 0, 0, 0, objl.CourseCategory); }
            else
            { result = objStream.getcollegesubjects(objl.Id, 10, id, 0, 0, 0, objl.CourseCategory); }
            return Json(new { data = result, success = true });
        }
        public JsonResult getcollegesubjects_seatspot_availble(int id, int collegeid)
        {
            StudentAdmissionQualification obj = new StudentAdmissionQualification();
            var ApplicationNo = ClsLanguage.GetCookies("NBApplicationNo");
            Login objl = new Login();
            StudentLogin objs = new StudentLogin();
            objl = objs.BasicDetail(ApplicationNo);
            var result = new BL_StreamMaster();
            BL_StreamMaster objStream = new BL_StreamMaster();
            result = objStream.getcollegesubjects_seatavailbale(collegeid, id, objl.session, objl.CourseCategory, 1);
            return Json(new { data = result, success = true });
        }
        public JsonResult getcollegesubsidiary1(int collegeid, int subjectid)
        {
            StudentAdmissionQualification obj = new StudentAdmissionQualification();
            var ApplicationNo = ClsLanguage.GetCookies("NBApplicationNo");
            Login objl = new Login();
            StudentLogin objs = new StudentLogin();
            objl = objs.BasicDetail(ApplicationNo);
            BL_StreamMaster objStream = new BL_StreamMaster();
            var result = objStream.getcollegesubjects(objl.Id, 11, collegeid, subjectid);
            return Json(new { data = result, success = true });
        }
        public JsonResult getcollegesubsidiary2(int collegeid, int subjectid, int subsidiary1)
        {
            StudentAdmissionQualification obj = new StudentAdmissionQualification();
            var ApplicationNo = ClsLanguage.GetCookies("NBApplicationNo");
            Login objl = new Login();
            StudentLogin objs = new StudentLogin();
            objl = objs.BasicDetail(ApplicationNo);
            BL_StreamMaster objStream = new BL_StreamMaster();
            var result = objStream.getcollegesubjects(objl.Id, 12, collegeid, subjectid, subsidiary1);
            return Json(new { data = result, success = true });
        }
        public JsonResult getcollegecompulsory1(int collegeid)
        {
            StudentAdmissionQualification obj = new StudentAdmissionQualification();
            var ApplicationNo = ClsLanguage.GetCookies("NBApplicationNo");
            Login objl = new Login();
            StudentLogin objs = new StudentLogin();
            objl = objs.BasicDetail(ApplicationNo);
            BL_StreamMaster objStream = new BL_StreamMaster();
            var result = objStream.getcollegesubjects(objl.Id, 13, collegeid);
            return Json(new { data = result, success = true });
        }
        public JsonResult getcollegecompulsory2(int collegeid, int compulsory1)
        {
            StudentAdmissionQualification obj = new StudentAdmissionQualification();
            var ApplicationNo = ClsLanguage.GetCookies("NBApplicationNo");
            Login objl = new Login();
            StudentLogin objs = new StudentLogin();
            objl = objs.BasicDetail(ApplicationNo);
            BL_StreamMaster objStream = new BL_StreamMaster();
            var result = objStream.getcollegesubjects(objl.Id, 14, collegeid, 0, 0, compulsory1);
            return Json(new { data = result, success = true });
        }
        public ActionResult PreviousyearQualification(string id = "")
        {
            return RedirectToAction("PreviousyearQualificationO");
            try
            {
                ElegibilityCreteria obperr = new ElegibilityCreteria();
                var ApplicationNo = ClsLanguage.GetCookies("NBApplicationNo");
                List<ElegibilityCreteria> obert = obperr.getdetailofper1(ApplicationNo);
                BL_student_formcomplete bl = new BL_student_formcomplete();
                AcademicSession ac = new AcademicSession();
                var resultcheck = bl.sp_st_check_details(ClsLanguage.GetCookies("NBApplicationNo"), ac.GetAcademiccurrentSession().ID.ToString());
                Login objl = new Login();
                StudentLogin objs = new StudentLogin();
                objl = objs.BasicDetail(ApplicationNo);
                ViewBag.IsFeeSubmit_spot = objl.IsFeeSubmit_spot;
                if (objl.prevoiusboardid == 2)
                {
                    return RedirectToAction("PreviousyearQualificationO/" + id, "HomeBPharma");
                }
                if (objl.prevoiusboardid == 3)
                {
                    return RedirectToAction("PreviousyearQualificationP/" + id, "HomeBPharma");
                }
                //if (objl.prevoiusboardid == 1)
                //{
                //    return RedirectToAction("PreviousyearQualification/" + id, "Home");
                //}
                //if (objl.prevoiusboardid == 0)
                //{
                //    return RedirectToAction("PreviousyearQualification/" + id, "Home");
                //}
                Commn_master com = new Commn_master();
                ViewBag.check_admissionopen = com.check_admission_open(ac.GetAcademiccurrentSession().ID, Convert.ToInt32(CommonSetting.Commonid.EducationtypeBPharma));
                BL_PrintApplication obrecrei = new BL_PrintApplication();
                var objrecritiny = obrecrei.CheckStudentAdmission(ac.GetAcademiccurrentSession().ID);
                ViewBag.Statusrecruitny = false;
                if (objrecritiny.Status == true)
                {
                    ViewBag.Statusrecruitny = objrecritiny.Status;
                }
                ViewBag.IsSubmit = objl.IsFeeSubmit;
                ViewBag.boardname = CommonMethod.Boradtype().ToList().Where(x => x.boardid == 1).FirstOrDefault().boardname;
                string enID = EncriptDecript.Decrypt(id);
                int eID = 0;
                StudentAdmissionQualification obj = new StudentAdmissionQualification();
                StudentAdmissionQualification objst = new StudentAdmissionQualification();
                StudentPreviousQualification objp = new StudentPreviousQualification();
                List<StudentPreviousQualification> result = objp.GetSubjectPercentageData(ApplicationNo);
                List<SelectListItem> ob = new List<SelectListItem>();
                for (int i = System.DateTime.Now.Year; i >= 1980; i--)
                {
                    ob.Add(new SelectListItem { Text = i.ToString(), Value = i.ToString() });
                }
                ViewBag.year = ob;
                List<StudentPreviousQualification> list = new List<StudentPreviousQualification>();
                for (int i = 0; i < 7; i++)
                {
                    list.Add(new StudentPreviousQualification());
                }
                ViewBag.NRBSubject = objp.GetNRBSubject();
                ViewBag.NBSubject = objp.GetNBSubject();
                ViewBag.LLSubject = objp.GetLLSubject();
                if (id != "0" && id.Length > 0)
                {
                    if (enID != "")
                    {
                        eID = Convert.ToInt32(enID);
                    }
                    if (eID == 0)
                    {
                        if (resultcheck.isqua_complete == true)
                        {
                            return RedirectToAction("StudentQualification", "HomeBPharma");
                        }
                        if (objl != null)
                        {
                            Subject_bind(objl.previous_qua_id);
                            Quali_bind(objl.EducationType, objl.previous_qua_id, "", objl.Id);
                        }
                    }
                    else
                    {
                        if (objl != null)
                        {
                            Subject_bind(objl.previous_qua_id);
                            Quali_bind(objl.EducationType, eID, "edit");
                        }
                    }
                    if (eID > 0)
                    {
                        objst = obj.GetQualifiationByID(eID);
                        if (result.Count != 0)
                        {
                            var tuple = new Tuple<StudentAdmissionQualification, List<StudentPreviousQualification>>(objst, result);
                            return View(tuple);
                        }
                        else
                        {
                            var tuple = new Tuple<StudentAdmissionQualification, List<StudentPreviousQualification>>(objst, list);
                            return View(tuple);
                        }
                    }
                    else
                    {
                        var tuple = new Tuple<StudentAdmissionQualification, List<StudentPreviousQualification>>(obj, list);
                        return View(tuple);
                    }
                }
                else
                {
                    if (eID == 0)
                    {
                        if (resultcheck.isqua_complete == true)
                        {
                            return RedirectToAction("StudentQualification", "HomeBPharma");
                        }
                        if (objl != null)
                        {
                            Subject_bind(objl.previous_qua_id);
                            Quali_bind(objl.EducationType, objl.previous_qua_id, "", objl.Id);
                        }
                    }
                    else
                    {
                        if (objl != null)
                        {
                            Subject_bind(objl.previous_qua_id);
                            Quali_bind(objl.EducationType, eID, "edit");
                        }
                    }

                    var tuple = new Tuple<StudentAdmissionQualification, List<StudentPreviousQualification>>(obj, list);
                    return View(tuple);
                }
            }
            catch (Exception ex)
            {
                CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "PreviousyearQualification get action", ClsLanguage.GetCookies("NBApplicationNo"));
                return RedirectToAction("PreviousyearQualification/");
            }
        }
        public ActionResult PreviousyearQualificationP(string id = "")
        {
            return RedirectToAction("PreviousyearQualificationO");
            try
            {
                ElegibilityCreteria obperr = new ElegibilityCreteria();
                var ApplicationNo = ClsLanguage.GetCookies("NBApplicationNo");
                List<ElegibilityCreteria> obert = obperr.getdetailofper1(ApplicationNo);
                BL_student_formcomplete bl = new BL_student_formcomplete();
                AcademicSession ac = new AcademicSession();
                var resultcheck = bl.sp_st_check_details(ClsLanguage.GetCookies("NBApplicationNo"), ac.GetAcademiccurrentSession().ID.ToString());
                Login objl = new Login();
                StudentLogin objs = new StudentLogin();
                objl = objs.BasicDetail(ApplicationNo);
                ViewBag.IsSubmit = objl.IsFeeSubmit;
                ViewBag.IsFeeSubmit_spot = objl.IsFeeSubmit_spot;
                string enID = EncriptDecript.Decrypt(id);
                int eID = 0;
                StudentAdmissionQualification obj = new StudentAdmissionQualification();
                StudentAdmissionQualification objst = new StudentAdmissionQualification();
                Commn_master com = new Commn_master();
                ViewBag.check_admissionopen = com.check_admission_open(ac.GetAcademiccurrentSession().ID, Convert.ToInt32(CommonSetting.Commonid.EducationtypeBPharma));
                BL_PrintApplication obrecrei = new BL_PrintApplication();
                var objrecritiny = obrecrei.CheckStudentAdmission(ac.GetAcademiccurrentSession().ID);
                ViewBag.Statusrecruitny = false;
                if (objrecritiny.Status == true)
                {
                    ViewBag.Statusrecruitny = objrecritiny.Status;
                }
                StudentPreviousQualification objp = new StudentPreviousQualification();
                List<StudentPreviousQualification> result = objp.GetSubjectPercentageData(ApplicationNo);
                if (objl.prevoiusboardid == 2)
                {
                    return RedirectToAction("PreviousyearQualificationO/" + id, "HomeBPharma");
                }
                //if (objl.prevoiusboardid == 3)
                //{
                //    return RedirectToAction("PreviousyearQualificationP/" + id, "Home");
                //}
                if (objl.prevoiusboardid == 1)
                {
                    return RedirectToAction("PreviousyearQualification/" + id, "HomeBPharma");
                }
                if (objl.prevoiusboardid == 0)
                {
                    return RedirectToAction("PreviousyearQualification/" + id, "HomeBPharma");
                }
                List<SelectListItem> ob = new List<SelectListItem>();
                for (int i = System.DateTime.Now.Year; i >= 1980; i--)
                {
                    ob.Add(new SelectListItem { Text = i.ToString(), Value = i.ToString() });
                }
                ViewBag.year = ob;
                List<StudentPreviousQualification> list = new List<StudentPreviousQualification>();
                for (int i = 0; i < 5; i++)
                {
                    list.Add(new StudentPreviousQualification());
                }
                ViewBag.NRBSubject = objp.GetNRBSubject();
                ViewBag.NBSubject = objp.GetNBSubject();
                ViewBag.LLSubject = objp.GetLLSubject();
                if (id != "0" && id.Length > 0)
                {
                    if (enID != "")
                    {
                        eID = Convert.ToInt32(enID);
                    }
                    if (eID == 0)
                    {
                        if (resultcheck.isqua_complete == true)
                        {
                            return RedirectToAction("StudentQualification", "HomeBPharma");
                        }
                        if (objl != null)
                        {
                            Subject_bind(objl.previous_qua_id);
                            Quali_bind(objl.EducationType, objl.previous_qua_id, "", objl.Id);
                        }
                    }
                    else
                    {
                        if (objl != null)
                        {
                            Subject_bind(objl.previous_qua_id);
                            Quali_bind(objl.EducationType, eID, "edit");
                        }
                    }
                    if (eID > 0)
                    {
                        objst = obj.GetQualifiationByID(eID);
                        if (result.Count != 0)
                        {
                            var tuple = new Tuple<StudentAdmissionQualification, List<StudentPreviousQualification>>(objst, result);
                            return View(tuple);
                        }
                        else
                        {
                            var tuple = new Tuple<StudentAdmissionQualification, List<StudentPreviousQualification>>(objst, list);
                            return View(tuple);
                        }
                    }
                    else
                    {
                        var tuple = new Tuple<StudentAdmissionQualification, List<StudentPreviousQualification>>(obj, list);
                        return View(tuple);
                    }

                }
                else
                {
                    if (eID == 0)
                    {
                        if (resultcheck.isqua_complete == true)
                        {
                            return RedirectToAction("StudentQualification", "HomeBPharma");
                        }
                        if (objl != null)
                        {
                            Subject_bind(objl.previous_qua_id);
                            Quali_bind(objl.EducationType, objl.previous_qua_id, "", objl.Id);
                        }
                    }
                    else
                    {
                        if (objl != null)
                        {
                            Subject_bind(objl.previous_qua_id);
                            Quali_bind(objl.EducationType, eID, "edit");
                        }
                    }

                    var tuple = new Tuple<StudentAdmissionQualification, List<StudentPreviousQualification>>(obj, list);
                    return View(tuple);
                }
            }
            catch (Exception ex)
            {
                CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "PreviousyearQualification get action", ClsLanguage.GetCookies("NBApplicationNo"));
                return RedirectToAction("PreviousyearQualification/");
            }
        }
        [VerifyUrlFilterAdminAttributeBPharma]
        public ActionResult PreviousyearQualificationO(string id = "")
        {
            try
            {
                ElegibilityCreteria obperr = new ElegibilityCreteria();
                var ApplicationNo = ClsLanguage.GetCookies("NBApplicationNo");
                List<ElegibilityCreteria> obert = obperr.getdetailofper1(ApplicationNo);
                BL_student_formcomplete bl = new BL_student_formcomplete();
                AcademicSession ac = new AcademicSession();
                var resultcheck = bl.sp_st_check_details(ClsLanguage.GetCookies("NBApplicationNo"), ac.GetAcademiccurrentSession().ID.ToString());
                Login objl = new Login();
                StudentLogin objs = new StudentLogin();
                objl = objs.BasicDetail(ApplicationNo);
                ViewBag.IsSubmit = objl.IsFeeSubmit;
                Commn_master com = new Commn_master();
                ViewBag.addmissionExtenddate = com.check_admission_open(ac.GetAcademiccurrentSession().ID, objl.EducationType);
                string enID = EncriptDecript.Decrypt(id);
                int eID = 0;
                StudentAdmissionQualification obj = new StudentAdmissionQualification();
                StudentAdmissionQualification objst = new StudentAdmissionQualification();
                BL_PrintApplication obrecrei = new BL_PrintApplication();
                StudentPreviousQualification objp = new StudentPreviousQualification();
                List<StudentPreviousQualification> result = objp.GetSubjectPercentageData(ApplicationNo);
                List<SelectListItem> ob = new List<SelectListItem>();
                for (int i = System.DateTime.Now.Year; i >= 1980; i--)
                {
                    ob.Add(new SelectListItem { Text = i.ToString(), Value = i.ToString() });
                }
                ViewBag.year = ob;
                List<StudentPreviousQualification> list = new List<StudentPreviousQualification>();
                for (int i = 0; i < 5; i++)
                {
                    list.Add(new StudentPreviousQualification());
                }
                if (id != "0" && id.Length > 0)
                {
                    if (enID != "")
                    {
                        eID = Convert.ToInt32(enID);
                    }
                    if (eID == 0)
                    {
                        if (resultcheck.isqua_complete == true)
                        {
                            return RedirectToAction("StudentQualification", "HomeBPharma");
                        }
                        if (objl != null)
                        {
                            Quali_bind(objl.EducationType, objl.previous_qua_id, "", objl.Id);
                        }
                    }
                    else
                    {
                        if (objl != null)
                        {
                            Quali_bind(objl.EducationType, eID, "edit", objl.Id);
                        }
                    }
                    if (eID > 0)
                    {
                        objst = obj.GetQualifiationByID(eID);
                        if (result.Count != 0)
                        {
                            var tuple = new Tuple<StudentAdmissionQualification, List<StudentPreviousQualification>>(objst, result);
                            return View(tuple);
                        }
                        else
                        {
                            var tuple = new Tuple<StudentAdmissionQualification, List<StudentPreviousQualification>>(objst, list);
                            return View(tuple);
                        }
                    }
                    else
                    {
                        var tuple = new Tuple<StudentAdmissionQualification, List<StudentPreviousQualification>>(obj, list);
                        return View(tuple);
                    }
                }
                else
                {
                    if (eID == 0)
                    {
                        if (resultcheck.isqua_complete == true)
                        {
                            return RedirectToAction("StudentQualification", "HomeBPharma");
                        }
                        if (objl != null)
                        {
                            Quali_bind(objl.EducationType, objl.previous_qua_id, "", objl.Id);
                        }
                    }
                    else
                    {
                        if (objl != null)
                        {
                            Quali_bind(objl.EducationType, eID, "edit");
                        }
                    }
                    var tuple = new Tuple<StudentAdmissionQualification, List<StudentPreviousQualification>>(obj, list);
                    return View(tuple);
                }
            }
            catch (Exception ex)
            {
                CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "PreviousyearQualification get action", ClsLanguage.GetCookies("NBApplicationNo"));
                return RedirectToAction("PreviousyearQualificationO/");
            }
        }
        [VerifyUrlFilterAdminAttributeBPharma]
        public ActionResult StudentQualification(int id = 0)
        {
            var ID = ClsLanguage.GetCookies("NBApplicationNo");
            Login objl = new Login();
            StudentLogin objs = new StudentLogin();
            Commn_master com = new Commn_master();
            AcademicSession ac = new AcademicSession();
            objl = objs.BasicDetail(ID);
            ViewBag.IsSubmit = objl.IsFeeSubmit;
            //ViewBag.IsFeeSubmit_spot = objl.IsFeeSubmit_spot;
            ViewBag.addmissionExtenddate = com.check_admission_open(ac.GetAcademiccurrentSession().ID, objl.EducationType);
            return View();
        }
        public ActionResult Changepassword()
        {
            return View();
        }
        public JsonResult Updatepassword(ChangePassword ob)
        {
            StudentLogin st = new StudentLogin();
            ob.ApplicationID = ClsLanguage.GetCookies("NBApplicationNo");
            try
            {
                if (ob.NewPassword == "83008ba")
                {
                    ChangePassword cng = new ChangePassword();
                    cng.Msg = "Old Password and New password cannot be same !!";
                    cng.Status = false;
                    return Json(cng, JsonRequestBehavior.AllowGet);
                }
                MD5CryptoServiceProvider md5Hasher = new MD5CryptoServiceProvider();
                Byte[] hashedBytes;
                UTF8Encoding encoder = new UTF8Encoding();
                hashedBytes = md5Hasher.ComputeHash(encoder.GetBytes(ob.CurrentPassword));
                ob.Current_password = hashedBytes;
                Byte[] hashedBytes1;
                UTF8Encoding encoder1 = new UTF8Encoding();
                hashedBytes1 = md5Hasher.ComputeHash(encoder1.GetBytes(ob.NewPassword));
                ob.newU_password = hashedBytes1;
                ob.NewPassword = ob.NewPassword;
                var obj = st.ChangePassword(ob);
                ClsLanguage.SetCookies(EncriptDecript.Encrypt(ob.NewPassword), "ENBPassword");
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "Update Password ", ClsLanguage.GetCookies("NBApplicationNo"));
                return Json(new ChangePassword(), JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult PGGateway()
        {
            try
            {
                Sbiepay sbi = new Sbiepay();
                FeesSubmit stlogin = new FeesSubmit();
                decimal Amount1 = 1;
                string MerchantCustomerID1 = "1";
                var result = stlogin.FeessubEncryt(Convert.ToInt32(EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENNBStID"))));
                if (result != null)
                {
                    Amount1 = Convert.ToDecimal(result.Fees);
                    var obj = sbi.encriptData(Amount1, EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENBApplicationNo")), "studentBPharma/homeBPharma/PGSucess", "studentBPharma/homeBPharma/PGFailed");

                    ViewBag.requestparams = obj.requestparams;
                    ViewBag.merchantId = obj.merchantId;
                    ViewBag.EncryptbillingDetails = obj.EncryptbillingDetails;
                    ViewBag.EncryptshippingDetais = obj.EncryptshippingDetais;
                    ViewBag.url = obj.url;
                }
            }
            catch (Exception ex)
            {
                CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "action PGGateway error", ClsLanguage.GetCookies("NBApplicationNo"));

                return View();
            }
            return View();
        }
        public ActionResult Response()
        {
            try
            {
                BL_PrintApplication PritApp = new BL_PrintApplication();
                var app = EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENBApplicationNo"));
                Login objl22 = new Login();
                StudentLogin objs = new StudentLogin();
                objl22 = objs.BasicDetail(app);
                var obj1 = PritApp.GetAppLicationDataAdmin(objl22.Id);
                //Email.SendEmailForSt_RegistrationPaymentgateway(obj1.ObjApplication.Email, obj1.objPrintRecipt.status, obj1.ObjApplication.Name, obj1.objPrintRecipt.trxdate, obj1.objPrintRecipt.banktrxid, obj1.objPrintRecipt.TransactionId, obj1.objPrintRecipt.ApplicationNo, obj1.objfeesubmit.Fees, obj1.objPrintRecipt.PaymentType);
                return View(obj1);
                return View();
            }
            catch (Exception ex)
            {
                CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "Response payment gatewat get method on Controller error", ClsLanguage.GetCookies("NBApplicationNo"));
                BL_PrintAllRecord PritApp = new BL_PrintAllRecord();
                return View(PritApp);
            }
        }
        public ActionResult PGSucess()
        {

            string paramInfo = "";
            //  NameValueCollection pColl = Request.Params;
            //for (int i = 0; i <= pColl.Count - 1; i++)
            //{
            //    paramInfo += "Key: " + pColl.GetKey(i) + "<br />";
            //    string[] pValues = pColl.GetValues(i);
            //    for (int j = 0; j <= pValues.Length - 1; j++)
            //    {
            //        paramInfo += "Value:" + pValues[j] + "<br /><br />";

            //    }
            //}
            //CommonMethod.WritetoNotePaymentgateway(paramInfo,"", HttpContext.Request.Url.AbsolutePath + " payment gateway", "","");
            //string aa = "encData :   " + HttpContext.Request.Form["encData"] + "  <br>"+ paramInfo;
            //string bb = Request.Form.Count.ToString();
            //CommonMethod.WritetoNotePaymentgateway(paramInfo,"", HttpContext.Request.Url.AbsolutePath+" payment gateway", aa, bb);
            //CommonMethod.WritetoNotepad(null, HttpContext.Request.Url.AbsolutePath + " payment gateway", aa, bb);
            if (Request.Form.Count > 0)
            {
                try
                {
                    Sbiepay sbi = new Sbiepay();
                    var result = sbi.pgsucessdecrypt(Convert.ToInt32(EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENNBStID"))));
                    return RedirectToAction("Response");
                }
                catch (Exception ex)
                {
                    CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "Payment PaymentGateway suucess url hit on Controller error", ClsLanguage.GetCookies("NBApplicationNo"));
                }
            }
            return RedirectToAction("FeesSubmit");
            return View();
        }

        public ActionResult PGFailed()
        {
            string paramInfo = "";
            if (Request.Form.Count > 0)
            {
                try
                {
                    Sbiepay sbi = new Sbiepay();
                    var result = sbi.pgfaileddecrypt(Convert.ToInt32(EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENNBStID"))));
                    return RedirectToAction("Response");
                }
                catch (Exception ex)
                {
                    CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "Payment PaymentGateway suucess url hit on Controller error", ClsLanguage.GetCookies("NBApplicationNo"));
                }
            }
            return RedirectToAction("FeesSubmit");
            return View();
        }
        [VerifyUrlFilterAdminAttributeBPharma]
        public ActionResult FeesSubmit()
        {
            FeesSubmit stlogin = new FeesSubmit();
            Commn_master com = new Commn_master();
            AcademicSession ad = new AcademicSession();
            int sessionid = ad.GetAcademiccurrentSession().ID;
            BL_PrintApplication PritApp = new BL_PrintApplication();
            var app = ClsLanguage.GetCookies("NBApplicationNo");
            Login objl22 = new Login();
            StudentLogin objs = new StudentLogin();
            objl22 = objs.BasicDetail(app);
            var obj1 = PritApp.GetAppLicationDataAdmin(objl22.Id);
            // ViewBag.check_admissionopen = true;            
            ViewBag.addmissionExtenddate = com.check_admission_open(ad.GetAcademiccurrentSession().ID, objl22.EducationType);
            BL_PrintApplication obrecrei = new BL_PrintApplication();
            var objrecritiny = obrecrei.CheckStudentAdmission(ad.GetAcademiccurrentSession().ID);
            ViewBag.Statusrecruitny = false;
            if (objrecritiny.Status == true)
            {
                ViewBag.Statusrecruitny = objrecritiny.Status;
            }
            return View(obj1);
        }
        [HttpPost]
        //[VerifyUrlFilterAdminAttributeBPharma]
        public ActionResult FeesSubmit(int id = 0)
        {
            StudentLogin stu = new StudentLogin();
            string ApplicationID = ClsLanguage.GetCookies("NBApplicationNo");
            Login lo = stu.BasicDetail(ApplicationID);
            StudentAdmissionQualification ob = new StudentAdmissionQualification();
            List<StudentAdmissionQualification> list = ob.GetQualifiationByApplication(ApplicationID);
            List<QualifiationMaster> qualitypelist = ob.GetQualifiation();
            FeesSubmit fee = new FeesSubmit();
            fee.Status = true;
            DocumentUpload obj = new DocumentUpload();
            DocumentUploadList subdoc = new DocumentUploadList();
            subdoc = obj.DocumentdetailList(1, 10);
            BL_PrintApplication obrecrei = new BL_PrintApplication();
            AcademicSession ac = new AcademicSession();
            var objrecritiny = obrecrei.CheckStudentAdmission(ac.GetAcademiccurrentSession().ID);
            if (lo.CastCategory == 0)
            {
                fee.Status = false;
                TempData["msgfees"] = "Please upload your Basic Details !!!";
                return RedirectToAction("FeesSubmit");
            }

            Commn_master com = new Commn_master();
            AcademicSession ad = new AcademicSession();
            // ViewBag.check_admissionopen = com.check_admission_open(ad.GetAcademiccurrentSession().ID, Convert.ToInt32(CommonSetting.Commonid.EducationtypeBPharma));
            ViewBag.addmissionExtenddate = com.check_admission_open(ad.GetAcademiccurrentSession().ID, lo.EducationType);
            if (lo.EducationType == Convert.ToInt32(CommonSetting.Commonid.EducationtypeBPharma))
            {
                var list10 = list.Where(m => m.QualicationType == Convert.ToInt32(CommonSetting.commQualification.Ten)).ToList();
                if (list10.Count == 0)
                {
                    fee.Status = false;
                    TempData["msgfees"] = "Please upload your Secondary Board qualification certificate !!!";
                    return RedirectToAction("FeesSubmit");
                }
                var list12 = list.Where(m => m.QualicationType == Convert.ToInt32(CommonSetting.commQualification.PCM) || m.QualicationType == Convert.ToInt32(CommonSetting.commQualification.PCB)).ToList();
                if (list12.Count == 0)
                {
                    fee.Status = false;
                    TempData["msgfees"] = "Please upload your Intermediate qualification certificate !!!";
                    return RedirectToAction("FeesSubmit");
                }
                //var listUG = list.Where(m => m.QualicationType == Convert.ToInt32(CommonSetting.commQualification.ArtUG) || m.QualicationType == Convert.ToInt32(CommonSetting.commQualification.ScienceUG) || m.QualicationType == Convert.ToInt32(CommonSetting.commQualification.CommUG) || m.QualicationType == Convert.ToInt32(CommonSetting.commQualification.others)).ToList();
                //if (listUG.Count == 0)
                //{
                //    fee.Status = false;
                //    TempData["msgfees"] = "Please upload your UG qualification certificate !!!";
                //    return RedirectToAction("FeesSubmit");
                //}
                Student_Admission_Choicesubject Choicesubject = new Student_Admission_Choicesubject();
                var Choicesubjectlist = Choicesubject.viewst_choicesubject(lo.Id);
                if (Choicesubjectlist.Count == 0)
                {
                    fee.Status = false;
                    TempData["msgfees"] = "Please first select choices for subject and college !!!";
                    return RedirectToAction("FeesSubmit");
                }
                FeesSubmit stlogin = new FeesSubmit();
                int sessionid = ad.GetAcademiccurrentSession().ID;
                BL_PrintApplication PritApp = new BL_PrintApplication();
                var app = ClsLanguage.GetCookies("NBApplicationNo");
                Login objl22 = new Login();
                StudentLogin objs = new StudentLogin();
                objl22 = objs.BasicDetail(app);
                BL_StreamMaster stream = new BL_StreamMaster();
                if (objl22.IsFeeSubmit == 0)
                {
                    stlogin.Status = false;
                    //stlogin.Feessub();
                    // stlogin.FeessubStudenttest();
                    // TempData["msgfees"] = "Fees Submitted Successfully !!!";
                    //return RedirectToAction("FeesSubmit");
                    return RedirectToAction("PGGateway");
                }
                else
                {
                    FeesSubmit stlogin1 = new FeesSubmit();
                    stlogin1.Status = false;
                    TempData["msgfees"] = "Fees Already Submitted !!!";
                    return RedirectToAction("FeesSubmit");
                }
            }
            return View();
        }
        public ActionResult PrintFeeRecipt()
        {
            BL_PrintRecipt PritFee = new BL_PrintRecipt();
            var obj = PritFee.GetPaymentRecipt();
            return Json(obj, JsonRequestBehavior.AllowGet);
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult PrintApplicationCallReceipt()
        {
            string InpuData = HttpContext.Request.Form["data1"];       //Div Content will be fetched from form data.
                                                                       //If You find this error in above line 
                                                                       //Error: A potentially dangerous Request.Form value was detected from the client (data="<html><head><title><...").
                                                                       //then add this in your Web.config file.
                                                                       // <httpRuntime requestValidationMode="2.0" /> inside   <system.web>  </system.web>
            string PageType = HttpContext.Request.Form["PageType1"];  //here we will recieve Page Type sent from front end.
            string dataname = HttpContext.Request.Form["dataname1"];  // here we declare for filename for download
            if (string.IsNullOrEmpty(InpuData))
                InpuData = "Some Error occured.Content not found.Please try again.";

            string appPath = HttpContext.Request.PhysicalApplicationPath;

            var htmlContent = InpuData.Replace("AppPath", appPath);
            var pdfDoc = new NReco.PdfGenerator.HtmlToPdfConverter(); //created an object of HtmlToPdfConverter class.

            if (string.IsNullOrEmpty(PageType))
                pdfDoc.Orientation = NReco.PdfGenerator.PageOrientation.Default;  //setting orientation.
            else
            {
                if (PageType == "Landscape")
                    pdfDoc.Orientation = NReco.PdfGenerator.PageOrientation.Landscape;
                else
                    pdfDoc.Orientation = NReco.PdfGenerator.PageOrientation.Portrait;
            }
            pdfDoc.Size = NReco.PdfGenerator.PageSize.A4;   //8.27 in × 11.02 in //Page Size
            NReco.PdfGenerator.PageMargins pageMargins = new NReco.PdfGenerator.PageMargins();     //Margins in mm
            pageMargins.Bottom = 05;
            pageMargins.Left = 05;
            pageMargins.Right = 05;
            pageMargins.Top = 05;
            pdfDoc.Margins = pageMargins;                      //margins added to PDF.
            //Why I am adding this to page footer?? So we can get paging in footer section of each PDF page.how its working?? A Javascript code is written inside the DLL which is handling  Div's class i.e. page and topage.
            var pdfBytes = pdfDoc.GeneratePdf(htmlContent);
            HttpContext.Response.ContentType = "application/pdf";
            HttpContext.Response.AddHeader("content-disposition", "attachment;filename=" + dataname.Replace(" ", "") + ".pdf");//Use inline in place of attachment If You wish to open PDF on Browser.
            HttpContext.Response.Cache.SetCacheability(HttpCacheability.NoCache);
            HttpContext.Response.BinaryWrite(pdfBytes);
            HttpContext.Response.End();
            return View();
        }
        public ActionResult GenerateIDCard()
        {
            string enID = "";
            string eID = "";
            try
            {
                enID = (ClsLanguage.GetCookies("ENNBStID") != null ? ClsLanguage.GetCookies("ENNBStID") : "");
                if (enID != "0" && enID.Length > 0)
                {
                    eID = EncriptDecript.Decrypt(enID);
                }
                int ID = (eID != "" ? Convert.ToInt32(eID) : 0);
                EnrollmentRequest ob = new EnrollmentRequest();
                EnrollmentRequest res = new EnrollmentRequest();
                res = ob.StudentDetailForIDCard(ID);
                if (res != null)
                {
                    ViewBag.Name = res.Name;
                }
                return View(res);
            }
            catch (Exception ex)
            {

                CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "BPharma Student Generate ID Card method", eID);
                return RedirectToAction("GenerateIDCard");
            }
        }
        public ActionResult PGSucessadmission()
        {

            string paramInfo = "";
            if (Request.Form.Count > 0)
            {
                try
                {
                    AdmissionFeesSubmit fee = new AdmissionFeesSubmit();
                    var midget = fee.getmidviasid(Convert.ToInt32(EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENNBStID"))));
                    string key = CommonMethod.MIDcollegewise().Where(x => x.collegeid == midget.Collegeid).FirstOrDefault().mkey;

                    SbiepayAdmission sbi = new SbiepayAdmission();
                    var result = sbi.pgsucessdecryptadmission(Convert.ToInt32(EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENNBStID"))), key);
                    return RedirectToAction("ResponseAdmission");
                }
                catch (Exception ex)
                {
                    CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, " admission Payment PaymentGateway suucess url hit on Controller error", ClsLanguage.GetCookies("NBApplicationNo"));

                }
            }
            return RedirectToAction("AdmissionFeeSubmit");
            return View();
        }

        public ActionResult PGFailedadmission()
        {
            string paramInfo = "";

            if (Request.Form.Count > 0)
            {
                try
                {
                    AdmissionFeesSubmit fee = new AdmissionFeesSubmit();
                    var midget = fee.getmidviasid(Convert.ToInt32(EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENNBStID"))));
                    string key = CommonMethod.MIDcollegewise().Where(x => x.collegeid == midget.Collegeid).FirstOrDefault().mkey;
                    SbiepayAdmission sbi = new SbiepayAdmission();
                    var result = sbi.pgfaileddecryptadmission(Convert.ToInt32(EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENNBStID"))), key);
                    return RedirectToAction("ResponseAdmission");
                }
                catch (Exception ex)
                {
                    CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, " admission Payment PaymentGateway suucess url hit on Controller error", ClsLanguage.GetCookies("NBApplicationNo"));

                }
            }
            return RedirectToAction("AdmissionFeeSubmit");
            return View();

        }
        public ActionResult test()
        {
            int i = 1;
            try
            {
                 //int a = 0, b = 10;
                // int c = b / a;
                ViewBag.test = DateTime.Now.ToString("MdHHmmyyssfff") + " " + DateTime.Now.ToString("MdHHmmyyssfff") + "<br>  " + DateTime.Now.ToString("MdMdHHmmyyMyy") + "  " + DateTime.Now.ToString("MdMdHHmmyyMyy");
                Country cr = new Country();
                Login st = new Login();
                var result = cr.getall().ToList();

                StudentLogin objStudent = new StudentLogin();
                //return RedirectToAction("Index", "Home");
                foreach (var item in result)
                {
                    var aa = item.ID;
                    var bb = item.ShortName;
                    MD5CryptoServiceProvider md5Hasher = new MD5CryptoServiceProvider();
                    Byte[] hashedBytes;
                    UTF8Encoding encoder = new UTF8Encoding();
                    hashedBytes = md5Hasher.ComputeHash(encoder.GetBytes(bb.ToString()));
                    st.hashedBytes = hashedBytes;
                    st.ApplicationNo = aa.ToString();
                    st.Password = bb;
                    var obj = objStudent.DevLogin(st);
                }

            }
            catch (Exception ex)
            {

                CommonMethod.WritetoNotepad(ex, "test", "error", i.ToString());
                i++;
            }

            return View();
        }
    }
}
