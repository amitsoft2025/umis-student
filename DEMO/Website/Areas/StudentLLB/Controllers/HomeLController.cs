using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataLayer;
using System.Data;
using System.IO;
using Website.Models;
using System.Text;
using System.Drawing;
using Website.Areas.StudentLLB.Models;
using Website.Areas.Student.Models;
using Newtonsoft.Json;
using System.Security.Cryptography;
using System.Collections.Specialized;
using System.Net;

namespace Website.Areas.StudentLLB.Controllers
{
    //[CookiesExpireFilterLLB]
    public class HomeLController : Controller
    {
        // GET: Student/HomePG
        //[VerifyUrlFilterAdminAttributeLLB]
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
                // for manual check , kon ses semeseter ka back niklana h , 30 for first sem
                if (obj1.CourseCategory == 29)
                {

                    subjectlist = exam.backFeesDetailSubjectlist_LLB(obj1.CourseCategory, 1141, 30, obj1.session, rec.collegeid, obj1.Id);
                    if (subjectlist.Count > 0)
                    {
                        ViewBag.isback1sem = 1;
                        ViewBag.courseyearidenc1 = EncriptDecript.Encrypt(30.ToString());
                    }
                    subjectlist = exam.backFeesDetailSubjectlist_LLB(obj1.CourseCategory, 1141, 32, obj1.session, rec.collegeid, obj1.Id); if (subjectlist.Count > 0)
                    {
                        ViewBag.isbackpart3sem = 1;
                        ViewBag.courseyearidenc3 = EncriptDecript.Encrypt(32.ToString());
                    }
                    subjectlist = exam.backFeesDetailSubjectlist_LLB(obj1.CourseCategory, 1141, 34, obj1.session, rec.collegeid, obj1.Id); if (subjectlist.Count > 0)
                    {
                        ViewBag.isbackpart5sem = 1;
                        ViewBag.courseyearidenc5 = EncriptDecript.Encrypt(34.ToString());
                    }

                    //Comment 2,4,6
                    //subjectlist = exam.backFeesDetailSubjectlist_LLB(obj1.CourseCategory, 1141, 31, obj1.session, rec.collegeid, obj1.Id);
                    //if (subjectlist.Count > 0)
                    //{
                    //    ViewBag.isback2sem = 1;
                    //    ViewBag.courseyearidenc2 = EncriptDecript.Encrypt(31.ToString());
                    //}
                    //subjectlist = exam.backFeesDetailSubjectlist_LLB(obj1.CourseCategory, 1141, 33, obj1.session, rec.collegeid, obj1.Id); if (subjectlist.Count > 0)
                    //{
                    //    ViewBag.isbackpart4sem = 1;
                    //    ViewBag.courseyearidenc4 = EncriptDecript.Encrypt(33.ToString());
                    //}
                    //subjectlist = exam.backFeesDetailSubjectlist_LLB(obj1.CourseCategory, 1141, 35, obj1.session, rec.collegeid, obj1.Id); if (subjectlist.Count > 0)
                    //{
                    //    ViewBag.isbackpart6sem = 1;
                    //    ViewBag.courseyearidenc6 = EncriptDecript.Encrypt(35.ToString());
                    //}



                    //subjectlist = exam.backFeesDetailSubjectlist_LLB(obj1.CourseCategory, 1141, 31, obj1.session, rec.collegeid, obj1.Id);
                    //if (subjectlist.Count > 0)
                    //{
                    //    ViewBag.isback = 1;
                    //}
                }
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
                var dateextend = ob.CheckStudentAddmisionExtendDate(sessionid, educationtype);
                ViewBag.addmissionExtenddate = dateextend.Status;
                ViewBag.addmissionExtenddateValue = dateextend.ExtendDate;
                var datestart = ob.CheckStudentAddmisionStartDate(sessionid, educationtype);
                ViewBag.addmissionStartdate = datestart.Status;
                ViewBag.addmissionStartdateValue = datestart.startdate;
                //ViewBag.IsApplied = ob.CheckStudentApplied(sessionid).Status;
                //ViewBag.IsVerify = ob.CheckStudentVerify(sessionid).Status;
                var dateextenddoc = ob.documnetCheckStudentAddmisionExtendDate(sessionid, educationtype);
                ViewBag.addmissionExtenddateValuedoc = dateextenddoc.ExtendDate;
                var datestartdoc = ob.documentCheckStudentAddmisionStartDate(sessionid, educationtype);
                ViewBag.addmissionStartdateValuedoc = datestartdoc.startdate;
                //ViewBag.IsApplied = ob.CheckStudentApplied(sessionid).Status;
                ViewBag.rejectreason = ob.CheckDocumentVerification(sessionid).rejectreason;
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
           
            //var resultexam = exam.Feesischeckexamfeesubmit(obj1.Id, obj1.session);
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
        [VerifyUrlFilterAdminAttributeLLB]
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
            //ViewBag.Qualification = stad.GetQualifiationMasterOldStudentPG();
            List<QualifiationMaster> objlist = new List<QualifiationMaster>();
            objlist = stad.getqualificationst(Convert.ToInt32(CommonSetting.Commonid.EducationtypeLLB)).ToList();
            //objlist = objlist.Where(x => x.ID != 12).ToList();
            ViewBag.Qualification = objlist;


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
        [VerifyUrlFilterAdminAttributeLLB]
        [HttpPost]
        public ActionResult BasicDetail(Login objlogin, HttpPostedFileBase sign, HttpPostedFileBase photo)
        {
            StudentLogin st = new StudentLogin();
            Commn_master com = new Commn_master();
            AcademicSession ac = new AcademicSession();
            StudentAdmissionQualification stad = new StudentAdmissionQualification();
            //ViewBag.Qualification = stad.GetQualifiationMasterOldStudentPG();
            List<QualifiationMaster> objlist = new List<QualifiationMaster>();
            objlist = stad.getqualificationst(Convert.ToInt32(CommonSetting.Commonid.EducationtypeLLB)).ToList();
           // objlist = objlist.Where(x => x.ID != 12).ToList();
            ViewBag.Qualification = objlist;
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
            //ViewBag.check_admissionopen = com.check_admission_open(ac.GetAcademiccurrentSession().ID, Convert.ToInt32(CommonSetting.Commonid.EducationtypeLLB));
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
            objlogin.previous_qua_id = obj.previous_qua_id;
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
                    if (objlogin.session > 39)
                    {
                        ClsLanguage.SetCookies(objlogin.FirstName, "NBUserName");
                    }
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
            //var res = obj.GetQualifiationByID(Convert.ToInt32(enID));
            //var fullPath = res.FileURl;
            //var result = obj.DeleteQualifiationByID_old(Convert.ToInt32(enID));
            //TempData["Msg"] = result.Msg;
            //return RedirectToAction("StudentQualification");
            var res = obj.GetQualifiationByID(Convert.ToInt32(enID));
            var fullPath = res.FileURl;
            if (Convert.ToInt32((ClsLanguage.GetCookies("NBSission"))) == 39)
            {
                var result = obj.DeleteQualifiationByID_old(Convert.ToInt32(enID));
                TempData["Msg"] = result.Msg;
                return RedirectToAction("StudentQualification");
            }
            else
            {
                var result = obj.DeleteQualifiationByID(Convert.ToInt32(enID));
                TempData["Msg"] = result.Msg;
                return RedirectToAction("StudentQualification");
            }
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
            if (id == 5)
            {
                list = obj.GetSubject(Convert.ToInt32(CommonSetting.Streamtype.LLBba), board);
            }
            if (id == 7)
            {
                list = obj.GetSubject(Convert.ToInt32(CommonSetting.Streamtype.LLBbsc), board);
            }
            if (id == 9)
            {
                list = obj.GetSubject(Convert.ToInt32(CommonSetting.Streamtype.LLBbcomm), board);
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
        //public JsonResult AddNewQualification(string id = "")
        //{
        //    StudentAdmissionQualification ob = new StudentAdmissionQualification();
        //    AcademicSession ac = new AcademicSession();
        //    if (Request.Form.Count > 0)
        //    {
        //        StudentAdmissionQualification doc = new StudentAdmissionQualification();
        //        doc.QualicationType = Convert.ToInt32(Request.Form["Qualification"] == "" ? "0" : Request.Form["Qualification"]);
        //        doc.Board_UniversityName = Request.Form["UniversityName"];
        //        doc.Percentage = Convert.ToDecimal(Request.Form["Percentage"] == "" ? "0" : Request.Form["Percentage"]);
        //        doc.ID = Convert.ToInt32(Request.Form["ID"] == "" ? "0" : Request.Form["ID"]);
        //        doc.PassingYear = Request.Form["PassingYear"];
        //        doc.RollNo = Request.Form["RollNo"];
        //        doc.hfile = Request.Form["hfile"];
        //        DataLayer.Login objl = new DataLayer.Login();
        //        StudentLogin objs = new StudentLogin();
        //        if (doc.Percentage == 0)
        //        {
        //            StudentAdmissionQualification doc1 = new StudentAdmissionQualification();
        //            doc1.Msg = "Please Fill Aggregate Percentage ,Or Fill Again form !!";
        //            return Json(doc1, JsonRequestBehavior.AllowGet);
        //        }
        //        doc.ApplicationNo = ClsLanguage.GetCookies("NBApplicationNo");
        //        doc.SID = Convert.ToInt32(ClsLanguage.GetCookies("NBStID"));
        //        doc.session = ac.GetAcademiccurrentSession().ID;
        //        var resultyear = doc.Checkpassingyear(doc.PassingYear, doc.SID, doc.ID.ToString());
        //        if (resultyear.Status)
        //        {
        //            StudentAdmissionQualification doc1 = new StudentAdmissionQualification();
        //            doc1.Msg = resultyear.Msg;
        //            return Json(doc1, JsonRequestBehavior.AllowGet);
        //        }
        //        StudentAdmissionQualification result = new StudentAdmissionQualification();
        //        string jsonstring = JsonConvert.SerializeObject(doc);
        //        if (Request.Files.Count > 0)
        //        {
        //            try
        //            {
        //                if (Request.Files.GetKey(0) == "file")
        //                {
        //                    HttpPostedFileBase fileUpload = Request.Files.Get(0);
        //                    if (Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
        //                    {
        //                        string[] testfiles = fileUpload.FileName.Split(new char[] { '\\' });
        //                    }
        //                    Stream st1 = fileUpload.InputStream;
        //                    string name = Path.GetFileName(fileUpload.FileName);
        //                    try
        //                    {
        //                        string myBucketName = "mungeruniversity"; //your s3 bucket name goes here  
        //                        string s3DirectoryName = "Student/Document";
        //                        string s3FileName = System.DateTime.Now.ToString("dd_MM_yyyy_HH:mm:ss") + "_StudentDocument_" + objl.FirstName + @name;
        //                        s3FileName = s3FileName.Replace(" ", "");
        //                        doc.FileURl = s3FileName;
        //                        bool a;
        //                        AmazonUploader myUploader = new AmazonUploader();
        //                        a = myUploader.sendMyFileToFolder(photo,Server.MapPath("~/App_Data/uploads"),s3FileName);
        //                    }
        //                    catch (Exception ex)
        //                    {
        //                        CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "Employee : document Image Upload", "DocumentType1" + jsonstring);
        //                    }
        //                }
        //                else
        //                {
        //                    StudentAdmissionQualification ob1 = new StudentAdmissionQualification();
        //                    ob1.Msg = "Error occurred. Error details: ";
        //                    return Json(ob1, JsonRequestBehavior.AllowGet);
        //                }
        //            }
        //            catch (Exception ex)
        //            {
        //                CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "Employee : document Image Upload", "DocumentType1" + jsonstring);
        //                StudentAdmissionQualification ob3 = new StudentAdmissionQualification();
        //                ob3.Msg = "Error occurred. Error details: " + ex.Message;
        //                return Json(ob3, JsonRequestBehavior.AllowGet);
        //            }
        //        }
        //        else
        //        {
        //            doc.FileURl = doc.hfile;
        //        }
        //        result = ob.SaveQualificationDetailsForOldStudentPG(doc);
        //        if (result.Status)
        //        {
        //            return Json(result, JsonRequestBehavior.AllowGet);
        //        }
        //        return Json(result, JsonRequestBehavior.AllowGet);
        //    }
        //    else
        //    {
        //        StudentAdmissionQualification logmsg = new StudentAdmissionQualification();
        //        logmsg.Msg = "Error occurred. Error details: ";
        //        return Json(logmsg, JsonRequestBehavior.AllowGet);
        //    }
        //}
        public JsonResult AddNewQualification(string id = "")
        {
            StudentAdmissionQualification ob = new StudentAdmissionQualification();
            if (Request.Form.Count > 0)
            {
                StudentAdmissionQualification doc = new StudentAdmissionQualification();
                StudentPreviousQualification obj = new StudentPreviousQualification();
                doc.QualicationType = Convert.ToInt32(Request.Form["Qualification"] == "" ? "0" : Request.Form["Qualification"]);
                doc.Board_UniversityName = Request.Form["UniversityName"];
                doc.Percentage = Convert.ToDecimal(Request.Form["Percentage"] == "" ? "0" : Request.Form["Percentage"]);
                doc.ID = Convert.ToInt32(Request.Form["ID"] == "" ? "0" : Request.Form["ID"]);
                doc.PassingYear = Request.Form["PassingYear"];
                doc.RollNo = Request.Form["RollNo"];
                doc.hfile = Request.Form["hfile"];

                doc.sublist = (Request.Form["SubjectID"] == "" ? "0" : Request.Form["SubjectID"]);
                doc.SubID = (Request.Form["Subid"] == "" ? "0" : Request.Form["Subid"]);
                doc.TotalMarks = (Request.Form["TotalMarks"] == "" ? "0" : Request.Form["TotalMarks"]);
                doc.MarksObtain = (Request.Form["MarksObtain"] == "" ? "0" : Request.Form["MarksObtain"]);
                doc.HonoursPercentage = Convert.ToDecimal(Request.Form["SubjectPercentage"] == "" ? "0" : Request.Form["SubjectPercentage"]);

                DataLayer.Login objl = new DataLayer.Login();
                StudentLogin objs = new StudentLogin();
                if (doc.Percentage == 0)
                {
                    StudentAdmissionQualification doc1 = new StudentAdmissionQualification();
                    doc1.Msg = "Please Fill Aggregate Percentage ,Or Fill Again form !!";
                    return Json(doc1, JsonRequestBehavior.AllowGet);
                }
                if (doc.QualicationType == 12)
                {
                    if (doc.Percentage < 45 )
                    {
                        StudentAdmissionQualification doc1 = new StudentAdmissionQualification();
                        doc1.Msg = "If You are selecting an other Degree then you have to sure that your Aggregate Percentage should be more than or equal 45 % !!";
                        return Json(doc1, JsonRequestBehavior.AllowGet);
                    }
                    doc.HonoursPercentage = doc.Percentage;
                }
                int ba = Convert.ToInt32(CommonSetting.commQualification.ArtUG);
                int bsc = Convert.ToInt32(CommonSetting.commQualification.ScienceUG);
                int bcomm = Convert.ToInt32(CommonSetting.commQualification.CommUG);
                if (doc.QualicationType == ba || doc.QualicationType == bsc || doc.QualicationType == bcomm)
                {
                    if (doc.HonoursPercentage == 0)
                    {
                        StudentAdmissionQualification doc1 = new StudentAdmissionQualification();
                        doc1.Msg = "Please Fill Honours  Percentage ,Or Fill Again form !!";
                        return Json(doc1, JsonRequestBehavior.AllowGet);
                    }
                    if (doc.sublist == "0" || doc.sublist == "")
                    {
                        StudentAdmissionQualification doc1 = new StudentAdmissionQualification();
                        doc1.Msg = "Please Fill Honours Subject ,Or Fill Again form !!";
                        return Json(doc1, JsonRequestBehavior.AllowGet);
                    }
                }
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
                if (doc.QualicationType == ba || doc.QualicationType == bsc || doc.QualicationType == bcomm)
                {
                    List<StudentPreviousQualification> mem = new List<StudentPreviousQualification>();
                    obj.Insertsingle(doc.SubID, result.ScopeIdentity, doc.sublist, doc.HonoursPercentage.ToString(), doc.TotalMarks, doc.MarksObtain);
                    Student_Admission_Choicesubject objchoice = new Student_Admission_Choicesubject();
                    if (doc.session == 40)
                    {
                        var resultdelete = objchoice.againfillform(doc.SID, doc.session);
                    }
                }
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
            if (5 == id || 7 == id || 9 == id )// allow for ba bsc and bcom
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
        [VerifyUrlFilterAdminAttributeLLB]
        //public ActionResult SelectSubject(int id = 0)
        //{
        //    StudentAdmissionQualification obj = new StudentAdmissionQualification();
        //    var ApplicationNo = ClsLanguage.GetCookies("NBApplicationNo");
        //    Login objl = new Login();
        //    StudentLogin objs = new StudentLogin();
        //    AcademicSession ac = new AcademicSession();
        //    objl = objs.BasicDetail(ApplicationNo);
        //    CollageList sub = new CollageList();
        //    BL_CollegeMaster obj111 = new BL_CollegeMaster();
        //    ViewBag.gender = objl.Gender;
        //    if (objl.Gender == 9)
        //    {
        //        sub = obj111.collagedetailviewlistdropallotedmalihacollege(objl.CourseCategory, ac.GetAcademiccurrentSession().ID);
        //    }
        //    else
        //    {
        //        sub = obj111.collagedetailviewlistdropalloted(objl.CourseCategory, ac.GetAcademiccurrentSession().ID);
        //    }
        //    var countcoll = sub.qlist.Count;
        //    if (countcoll < 2)
        //    {
        //        ViewBag.Collegecount = false;
        //    }
        //    else
        //    {
        //        ViewBag.Collegecount = true;
        //    }

        //    ViewBag.College = sub.qlist;
        //    ViewBag.sessionid = objl.session;
        //    ViewBag.isfeesubmit = objl.IsFeeSubmit;
        //    Commn_master com = new Commn_master();
        //    ViewBag.check_admissionopen = com.check_admission_open(ac.GetAcademiccurrentSession().ID, Convert.ToInt32(CommonSetting.Commonid.EducationtypeLLB));
        //    ViewBag.Course = com.getcommonMaster("Course", objl.EducationType);
        //    ViewBag.Coursecategoryid = objl.CourseCategory;
        //    List<StudentPreviousQualification> list = new List<StudentPreviousQualification>();
        //    for (int i = 0; i < 1; i++)
        //    {
        //        list.Add(new StudentPreviousQualification());
        //    }
        //    StudentPreviousQualification StudentPrevious = new StudentPreviousQualification();
        //    var tuple = new Tuple<StudentAdmissionQualification, List<StudentPreviousQualification>, Login>(obj, list,objl);
        //    BL_PrintApplication obrecrei = new BL_PrintApplication();
        //    Student_Admission_Choicesubject Choicesubject = new Student_Admission_Choicesubject();
        //    var Choicesubjectlist = Choicesubject.viewst_choicesubject(objl.Id);
        //    if (Choicesubjectlist.Count == 0)
        //    {

        //    }
        //    else
        //    {
        //        TempData["selectalready"] = "already";
        //        ViewBag.Choicesubject = Choicesubjectlist;
        //    }
        //    return View(tuple);
        //}
        public ActionResult SelectSubject(int id = 0)
        {
            StudentAdmissionQualification obj = new StudentAdmissionQualification();
            var ApplicationNo = ClsLanguage.GetCookies("NBApplicationNo");
            Login objl = new Login();
            StudentLogin objs = new StudentLogin();
            AcademicSession ac = new AcademicSession();
            objl = objs.BasicDetail(ApplicationNo);
            StudentPreviousQualification StudentPrevious = new StudentPreviousQualification();
            //  var tuple = new Tuple<StudentAdmissionQualification, Login>(obj, objl);
            BL_PrintApplication obrecrei = new BL_PrintApplication();
            Student_Admission_Choicesubject Choicesubject = new Student_Admission_Choicesubject();
            //var Choicesubjectlist = Choicesubject.viewst_choicesubject(objl.Id);
            //if (Choicesubjectlist.Count == 0)
            //{

            //}
            //else
            //{
            //    TempData["selectalready"] = "already";
            //    ViewBag.Choicesubject = Choicesubjectlist;
            //}
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
            ViewBag.check_admissionopen = com.check_admission_open(ac.GetAcademiccurrentSession().ID, Convert.ToInt32(CommonSetting.Commonid.EducationtypeLLB));

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
                    return RedirectToAction("PreviousyearQualificationO/" + id, "HomeL");
                }
                if (objl.prevoiusboardid == 3)
                {
                    return RedirectToAction("PreviousyearQualificationP/" + id, "HomeL");
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
                ViewBag.check_admissionopen = com.check_admission_open(ac.GetAcademiccurrentSession().ID, Convert.ToInt32(CommonSetting.Commonid.EducationtypeLLB));
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
                            return RedirectToAction("StudentQualification", "HomeL");
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
                            return RedirectToAction("StudentQualification", "HomeL");
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
                ViewBag.check_admissionopen = com.check_admission_open(ac.GetAcademiccurrentSession().ID, Convert.ToInt32(CommonSetting.Commonid.EducationtypeLLB));
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
                    return RedirectToAction("PreviousyearQualificationO/" + id, "HomeL");
                }
                //if (objl.prevoiusboardid == 3)
                //{
                //    return RedirectToAction("PreviousyearQualificationP/" + id, "Home");
                //}
                if (objl.prevoiusboardid == 1)
                {
                    return RedirectToAction("PreviousyearQualification/" + id, "HomeL");
                }
                if (objl.prevoiusboardid == 0)
                {
                    return RedirectToAction("PreviousyearQualification/" + id, "HomeL");
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
                            return RedirectToAction("StudentQualification", "HomeL");
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
                            return RedirectToAction("StudentQualification", "HomeL");
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
        [VerifyUrlFilterAdminAttributeLLB]
        //public ActionResult PreviousyearQualificationO(string id = "")
        //{
        //    try
        //    {
        //        ElegibilityCreteria obperr = new ElegibilityCreteria();
        //        var ApplicationNo = ClsLanguage.GetCookies("NBApplicationNo");
        //        List<ElegibilityCreteria> obert = obperr.getdetailofper1(ApplicationNo);
        //        BL_student_formcomplete bl = new BL_student_formcomplete();
        //        AcademicSession ac = new AcademicSession();
        //        var resultcheck = bl.sp_st_check_details(ClsLanguage.GetCookies("NBApplicationNo"), ac.GetAcademiccurrentSession().ID.ToString());
        //        Login objl = new Login();
        //        StudentLogin objs = new StudentLogin();
        //        objl = objs.BasicDetail(ApplicationNo);
        //        ViewBag.IsSubmit = objl.IsFeeSubmit;
        //        Commn_master com = new Commn_master();
        //        ViewBag.addmissionExtenddate = com.check_admission_open(ac.GetAcademiccurrentSession().ID, objl.EducationType);
        //        string enID = EncriptDecript.Decrypt(id);
        //        int eID = 0;
        //        StudentAdmissionQualification obj = new StudentAdmissionQualification();
        //        StudentAdmissionQualification objst = new StudentAdmissionQualification();
        //        BL_PrintApplication obrecrei = new BL_PrintApplication();
        //        StudentPreviousQualification objp = new StudentPreviousQualification();
        //        List<StudentPreviousQualification> result = objp.GetSubjectPercentageData(ApplicationNo);
        //        List<SelectListItem> ob = new List<SelectListItem>();
        //        for (int i = System.DateTime.Now.Year; i >= 1980; i--)
        //        {
        //            ob.Add(new SelectListItem { Text = i.ToString(), Value = i.ToString() });
        //        }
        //        ViewBag.year = ob;
        //        List<StudentPreviousQualification> list = new List<StudentPreviousQualification>();
        //        for (int i = 0; i < 5; i++)
        //        {
        //            list.Add(new StudentPreviousQualification());
        //        }
        //        if (id != "0" && id.Length > 0)
        //        {
        //            if (enID != "")
        //            {
        //                eID = Convert.ToInt32(enID);
        //            }
        //            if (eID == 0)
        //            {
        //                if (resultcheck.isqua_complete == true)
        //                {
        //                    return RedirectToAction("StudentQualification", "HomeL");
        //                }
        //                if (objl != null)
        //                {
        //                    Quali_bind(objl.EducationType, objl.previous_qua_id, "", objl.Id);
        //                }
        //            }
        //            else
        //            {
        //                if (objl != null)
        //                {
        //                    Quali_bind(objl.EducationType, eID, "edit", objl.Id);
        //                }
        //            }
        //            if (eID > 0)
        //            {
        //                objst = obj.GetQualifiationByID(eID);
        //                if (result.Count != 0)
        //                {
        //                    var tuple = new Tuple<StudentAdmissionQualification, List<StudentPreviousQualification>>(objst, result);
        //                    return View(tuple);
        //                }
        //                else
        //                {
        //                    var tuple = new Tuple<StudentAdmissionQualification, List<StudentPreviousQualification>>(objst, list);
        //                    return View(tuple);
        //                }
        //            }
        //            else
        //            {
        //                var tuple = new Tuple<StudentAdmissionQualification, List<StudentPreviousQualification>>(obj, list);
        //                return View(tuple);
        //            }
        //        }
        //        else
        //        {
        //            if (eID == 0)
        //            {
        //                if (resultcheck.isqua_complete == true)
        //                {
        //                    return RedirectToAction("StudentQualification", "HomeL");
        //                }
        //                if (objl != null)
        //                {
        //                    Quali_bind(objl.EducationType, objl.previous_qua_id, "", objl.Id);
        //                }
        //            }
        //            else
        //            {
        //                if (objl != null)
        //                {
        //                    Quali_bind(objl.EducationType, eID, "edit");
        //                }
        //            }
        //            var tuple = new Tuple<StudentAdmissionQualification, List<StudentPreviousQualification>>(obj, list);
        //            return View(tuple);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "PreviousyearQualification get action", ClsLanguage.GetCookies("NBApplicationNo"));
        //        return RedirectToAction("PreviousyearQualificationO/");
        //    }
        //}
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
                for (int i = System.DateTime.Now.Year; i >= 1975; i--)
                {
                    ob.Add(new SelectListItem { Text = i.ToString(), Value = i.ToString() });
                }
                ViewBag.year = ob;
                List<StudentPreviousQualification> list = new List<StudentPreviousQualification>();
                for (int i = 0; i < 1; i++)
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
                            return RedirectToAction("StudentQualification", "HomeL");
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
                            return RedirectToAction("StudentQualification", "HomeL");
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
                return RedirectToAction("PreviousyearQualificationO/");
            }
        }
        [VerifyUrlFilterAdminAttributeLLB]
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
            ViewBag.IsSubmit = objl.IsFeeSubmit;
            ViewBag.IsFeeSubmit_spot = objl.IsFeeSubmit_spot;
            if (objl != null)
            {
                Subject_bind(objl.previous_qua_id);
            }
            StudentPreviousQualification obj = new StudentPreviousQualification();
            List<StudentPreviousQualification> list = new List<StudentPreviousQualification>();
            List<StudentPreviousQualification> list1 = new List<StudentPreviousQualification>();
            list = obj.GetSubjectPercentageData(ID);
            ViewBag.check_admissionopen = com.check_admission_open(ac.GetAcademiccurrentSession().ID, Convert.ToInt32(CommonSetting.Commonid.EducationtypeLLB));
            ViewBag.sessionid = objl.session;
            BL_PrintApplication obrecrei = new BL_PrintApplication();

            var objrecritiny = obrecrei.CheckStudentAdmission(ac.GetAcademiccurrentSession().ID);
            ViewBag.Statusrecruitny = false;
            ViewBag.IsSubmit = objl.IsFeeSubmit;
            if (objrecritiny.Status == true)
            {
                ViewBag.Statusrecruitny = objrecritiny.Status;
            }
            if (list.Count != 0)
            {
                return View(list);
            }
            else
            {
                return View(list1);
            }
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
                    var obj = sbi.encriptData(Amount1, EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENBApplicationNo")), "studentLLB/homeB/PGSucess", "studentLLB/homeL/PGFailed");

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
                //if ((Convert.ToString(obj1.objPrintRecipt.status).ToLower() == "success".ToLower()))
                //{
                //    return RedirectToAction("ExamFeesSubmit", "ExamLLB");
                //}

                //Email.SendEmailForSt_RegistrationPaymentgateway(obj1.ObjApplication.Email, obj1.objPrintRecipt.status, obj1.ObjApplication.Name, obj1.objPrintRecipt.trxdate, obj1.objPrintRecipt.banktrxid, obj1.objPrintRecipt.TransactionId, obj1.objPrintRecipt.ApplicationNo, obj1.objfeesubmit.Fees, obj1.objPrintRecipt.PaymentType);
                return View(obj1);
               
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
        [VerifyUrlFilterAdminAttributeLLB]
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
        [VerifyUrlFilterAdminAttributeLLB]
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

            DocumentUpload obj1 = new DocumentUpload();
            DocumentUploadList sub1 = new DocumentUploadList();
            var subdoclist = obj.DocumentdetailList(1, 0100);
            if (lo.session > 39)
            {
                if (lo.CastCategory == 4)
                { }
                else
                {
                    if (subdoclist.qlist.Count < 2)
                    {
                        //fee.Status = false;
                        //TempData["msgfees"] = "Please upload your  CLC Or Caste Certificate !!!";
                        //return RedirectToAction("FeesSubmit");
                    }
                }
            }

            Commn_master com = new Commn_master();
            AcademicSession ad = new AcademicSession();
            // ViewBag.check_admissionopen = com.check_admission_open(ad.GetAcademiccurrentSession().ID, Convert.ToInt32(CommonSetting.Commonid.EducationtypeLLB));
            ViewBag.addmissionExtenddate = com.check_admission_open(ad.GetAcademiccurrentSession().ID, lo.EducationType);
            if (lo.EducationType == Convert.ToInt32(CommonSetting.Commonid.EducationtypeLLB))
            {
                var list10 = list.Where(m => m.QualicationType == Convert.ToInt32(CommonSetting.commQualification.Ten)).ToList();
                if (list10.Count == 0)
                {
                    fee.Status = false;
                    TempData["msgfees"] = "Please upload your Secondary Board qualification certificate !!!";
                    return RedirectToAction("FeesSubmit");
                }
                var list12 = list.Where(m => m.QualicationType == Convert.ToInt32(CommonSetting.commQualification.Art12) || m.QualicationType == Convert.ToInt32(CommonSetting.commQualification.Science12) || m.QualicationType == Convert.ToInt32(CommonSetting.commQualification.Comm12) || m.QualicationType == Convert.ToInt32(CommonSetting.commQualification.diploma)).ToList();
                if (list12.Count == 0)
                {
                    fee.Status = false;
                    TempData["msgfees"] = "Please upload your Intermediate qualification certificate !!!";
                    return RedirectToAction("FeesSubmit");
                }
                var listUG = list.Where(m => m.QualicationType == Convert.ToInt32(CommonSetting.commQualification.ArtUG) || m.QualicationType == Convert.ToInt32(CommonSetting.commQualification.ScienceUG) || m.QualicationType == Convert.ToInt32(CommonSetting.commQualification.CommUG) || m.QualicationType == Convert.ToInt32(CommonSetting.commQualification.others)).ToList();
                if (listUG.Count == 0)
                {
                    fee.Status = false;
                    TempData["msgfees"] = "Please upload your UG qualification certificate !!!";
                    return RedirectToAction("FeesSubmit");
                }
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
            //return RedirectToAction("Index");
            // ID card add in college protal

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

                CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "LLB Student Generate ID Card method", eID);
                return RedirectToAction("GenerateIDCard");
            }
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
        public JsonResult savest_choicesubject(string collegeidlist)
        {
            Student_Admission_Choicesubject obj = new Student_Admission_Choicesubject();
            var ApplicationNo = ClsLanguage.GetCookies("NBApplicationNo");
            Login objl = new Login();
            StudentLogin objs = new StudentLogin();

            try
            {
                objl = objs.BasicDetail(ApplicationNo);
                obj.SID = objl.Id;
                obj.hounors_subjectidlist = "1141,";
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
        public JsonResult College_bindDynamic(int coursecategoryid, string res = "", int isgender = 0)
        {
            BL_CollegeMaster obj = new BL_CollegeMaster();
            AcademicSession ac = new AcademicSession();
            if (isgender == 9)
            {
                //sub = obj111.collagedetailviewlistdropallotedmalihacollege(objl.CourseCategory, ac.GetAcademiccurrentSession().ID);
                var result = obj.collagedetailviewlistdropalloted_vocatinoalmahila(res, coursecategoryid, ac.GetAcademiccurrentSession().ID);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            else
            {
                //sub = obj111.collagedetailviewlistdropalloted(objl.CourseCategory, ac.GetAcademiccurrentSession().ID);
                var result = obj.collagedetailviewlistdropalloted_vocatinoal(res, coursecategoryid, ac.GetAcademiccurrentSession().ID);
                return Json(result, JsonRequestBehavior.AllowGet);
            }

        }
        public void Docu_bind()
        {
            DocumentUpload obj = new DocumentUpload();
            List<Commn_master> list = obj.GetCommMaster();
            List<SelectListItem> slist = new List<SelectListItem>();
            foreach (Commn_master dr in list)
            {
                slist.Add(new SelectListItem { Text = dr.Title, Value = dr.CommonId.ToString() });
            }
            ViewBag.Document = slist;
        }


        public ActionResult DocumentSave(string id = "")
        {
            if (Request.Form.Count > 0)
            {
                DocumentUpload ob = new DocumentUpload();
                ob.ApplicationNo = ClsLanguage.GetCookies("NBApplicationNo");
                ob.SID = Convert.ToInt32(ClsLanguage.GetCookies("NBStID"));
                ob.session = ClsLanguage.GetCookies("NBSission");
                ob.DocumentType = Convert.ToInt32(Request.Form["DocumentType"]);
                string jsonstring = JsonConvert.SerializeObject(ob);
                DataLayer.Login objl = new DataLayer.Login();
                StudentLogin objs = new StudentLogin();
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
                                ob.FileName = s3FileName;
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
                    ob.FileName = Request.Form["hfile"];
                    ob.EncriptedID = Request.Form["EncriptedID"];
                    ob.ID = Convert.ToInt32(EncriptDecript.Decrypt(ob.EncriptedID));
                }

                var result = ob.SaveDocument(ob);
                var str1 = result.Msg;
                return Json(str1, JsonRequestBehavior.AllowGet);
            }
            else
            {
                StudentAdmissionQualification logmsg = new StudentAdmissionQualification();
                logmsg.Msg = "Error occurred. Error details: ";
                return Json(logmsg, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult DocumentDelete(string id = "")
        {
            DocumentUpload obj = new DocumentUpload();
            string enID = "0";
            try
            {

                if (id != "0" && id.Length > 0)
                {
                    enID = EncriptDecript.Decrypt(id);
                }
                var res = obj.GetDocumentByID(Convert.ToInt32(enID));
                var fullPath = res.FileName;

                var result = obj.DeleteDocumentByID(Convert.ToInt32(enID));
                TempData["Msg"] = result.Msg;
            }
            catch (Exception ex)
            {
                CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "Student Document Delete", enID);
            }


            return RedirectToAction("DocumentUpload");
        }
        public ActionResult DocumentUpload(string id = "")
        {
            DocumentUpload obj = new DocumentUpload();
            var app = ClsLanguage.GetCookies("NBApplicationNo");
            Login objl = new Login();
            StudentLogin objs = new StudentLogin();
            Commn_master com = new Commn_master();
            AcademicSession ac = new AcademicSession();
            ViewBag.check_admissionopen = com.check_admission_open(ac.GetAcademiccurrentSession().ID, Convert.ToInt32(CommonSetting.Commonid.EducationtypeLLB));

            objl = objs.BasicDetail(app);
            ViewBag.IsSubmit = objl.IsFeeSubmit;
            ViewBag.sessionid = objl.session;
            Docu_bind();
            List<SelectListItem> ob = new List<SelectListItem>();
            if (id != "0" && id.Length > 0)
            {
                try
                {
                    string enID = EncriptDecript.Decrypt(id);
                    int eID = 0;
                    if (enID != "")
                    {
                        eID = Convert.ToInt32(enID);
                    }
                    if (eID > 0)
                    {
                        obj = obj.GetDocumentByID(eID);
                        ViewBag.que = obj.DocumentType.ToString();
                        return View(obj);
                    }
                    else
                    {
                        return View(obj);
                    }
                }
                catch (Exception ex)
                {
                    CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "Student Document upload get action", ClsLanguage.GetCookies("NBApplicationNo"));
                    return RedirectToAction("DocumentUpload/");
                }
            }
            return View(obj);



        }

        public ActionResult AdmissionFeeSubmit()
        {
            //return RedirectToAction("Index", "HomeL");

            //if (HttpContext.Request.Cookies["ENB123"] == null)
            //{
            //    return RedirectToAction("Index", "HomeL");
            //}

            BL_PrintApplication obrecrei = new BL_PrintApplication();
            AcademicSession ac = new AcademicSession();
            var objrecritiny = obrecrei.CheckStudentAdmission(ac.GetAcademiccurrentSession().ID);
            //if (objrecritiny.Status == true)
            //{
            //    return RedirectToAction("Index", "HomeL");
            //}
            AdmissionFeesSubmit stlogin = new AdmissionFeesSubmit();
            AdmissionFeesSubmit obj = new AdmissionFeesSubmit();
            List<AdmissionFeesSubmit> feestruckture = new List<AdmissionFeesSubmit>();
            string Application = EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENBApplicationNo"));

            doubleverificationgetstudent obj211 = new doubleverificationgetstudent();
            UserLogin objlogin = new UserLogin();
            var ObjDoubleVerification = objlogin.GetStudents_ForDoubleVerificationAdmissionAirpay(obj211, Application, "AirPay");
            if (ObjDoubleVerification.Count > 0)
            {
                DoubleVerification_CollegeFeesAdmissionAirpay();
            }

            ViewBag.IsSubmit = obj.IsFeeSubmit;
            Commn_master com = new Commn_master();
            //ViewBag.check_admissionopen = com.check_admission_open(ac.GetAcademiccurrentSession().ID, Convert.ToInt32(CommonSetting.Commonid.EducationtypeLLB));
            string Id = ClsLanguage.GetCookies("ENNBStID");
            BL_student_formcomplete bl = new BL_student_formcomplete();
            string ApplicationID = ClsLanguage.GetCookies("NBApplicationNo");
            StudentLogin tblST = new StudentLogin();
            BL_PrintApplication ob = new BL_PrintApplication();
            int sessionid = ac.GetAcademiccurrentSession().ID;
            var obj1 = tblST.BasicDetail(ApplicationID);
            var res1 = bl.CheckAdmission_details(sessionid);        
            ViewBag.IsAdmisApplied = res1.IsApplied;
          
            if (res1.IsApplied == false)
            {
                return RedirectToAction("Index", "HomeL");
            }
            if (res1.IsDocVerify == 0)
            {
                return RedirectToAction("Index", "HomeL");
            }
            if (res1.IsDocVerify == 2)
            {
                return RedirectToAction("Index", "HomeL");
            }
            if (obj1.IsFeeSubmit != 1)
            {
                return RedirectToAction("Index", "HomeL");
            }
            if (obj1.session == 39)
            {
                // if (obj1.StudentYear == 30 || obj1.StudentYear == 31 || obj1.StudentYear == 32 || obj1.StudentYear == 33 || obj1.StudentYear == 34 || obj1.StudentYear == 35)
                //if (obj1.StudentYear == 30 || obj1.StudentYear == 31 || obj1.StudentYear == 33 || obj1.StudentYear == 34 || obj1.StudentYear == 35)
                //{

                //     return RedirectToAction("Index", "HomeL");
                //}
                //else
                //{

                //}
            }
            if (obj1 != null)
            {
                int educationtype = obj1.EducationType;
                var dateextend = ob.CheckStudentAddmisionExtendDate(sessionid, educationtype);
                ViewBag.addmissionExtenddate = dateextend.Status;
                ViewBag.addmissionExtenddateValue = dateextend.ExtendDate;
                var datestart = ob.CheckStudentAddmisionStartDate(sessionid, educationtype);
                ViewBag.addmissionStartdate = datestart.Status;
                ViewBag.addmissionStartdateValue = datestart.startdate;

            }
            var objst = ob.CheckStudentAdmission(sessionid);
            if (objst.Status == true)
            {
                ViewBag.Status = objst.Status;
                ViewBag.Course = objst.CourseName;
                ViewBag.College = objst.CollegeName;
                ViewBag.Stream = objst.StreamName;
                
                ViewBag.CastCategory = objst.CastCategory;
            }
            else
            {
                ViewBag.Status = false;
                ViewBag.Course = "";
                ViewBag.College = "";
                ViewBag.CastCategory = "";
            }

            if (Id != "0" && Id.Length > 0)
            {
                string enID = EncriptDecript.Decrypt(Id);
                int eID = 0;
                if (enID != "")
                {
                    eID = Convert.ToInt32(enID);
                }
                if (eID > 0)
                {

                    if (obj1.StudentYear == 30 )// for Ist year
                    {

                        obj = stlogin.FeesDetails(eID);
                       
                        ViewBag.isfeesubmitt = res1.isfeesubmitt;
                        ViewBag.IsfeesubmitDate = res1.IsfeesubmitDate;
                    }
                    else
                    {
                        obj = stlogin.FeesDetailsotheryear(eID); // for other year
                        ViewBag.isfeesubmitt = res1.isfeesubmitt2;
                        ViewBag.IsfeesubmitDate = res1.IsfeesubmitDate2;

                    }
                    feestruckture = stlogin.FeesDetailsstructure(obj.Collegeid, obj1.CourseCategory, sessionid, obj1.CastCategory, obj.streamcategoryid, obj1.StudentYear, obj1.incomecertificate_iseligible,0);
                    var tuple = new Tuple<AdmissionFeesSubmit, List<AdmissionFeesSubmit>, Login>(obj, feestruckture, obj1);
                    return View(tuple);
                }
                else
                {
                    var tuple1 = new Tuple<AdmissionFeesSubmit, List<AdmissionFeesSubmit>, Login>(obj, feestruckture, obj1);
                    return View(tuple1);
                }
            }
            var tuple2 = new Tuple<AdmissionFeesSubmit, List<AdmissionFeesSubmit>,Login>(obj, feestruckture, obj1);
            return View(tuple2);
        }
        [HttpPost]
        public ActionResult AdmissionFeeSubmit(int id11 = 0,string applyadmissionform = "", string applyaffdavit = "")
        {
            //return RedirectToAction("Index", "HomeL");
            BL_PrintApplication obrecrei = new BL_PrintApplication();
            AcademicSession ac = new AcademicSession();
            var objrecritiny = obrecrei.CheckStudentAdmission(ac.GetAcademiccurrentSession().ID);
            //if (objrecritiny.Status == true)
            //{
            //    return RedirectToAction("Index", "HomeL");
            //}
            AdmissionFeesSubmit stlogin = new AdmissionFeesSubmit();
            AdmissionFeesSubmit obj = new AdmissionFeesSubmit();
            List<AdmissionFeesSubmit> feestruckture = new List<AdmissionFeesSubmit>();
            ViewBag.IsSubmit = obj.IsFeeSubmit;

            Commn_master com = new Commn_master();
            //ViewBag.check_admissionopen = com.check_admission_open(ac.GetAcademiccurrentSession().ID, Convert.ToInt32(CommonSetting.Commonid.EducationtypeLLB));
            //string Id = ClsLanguage.GetCookies("ENNBStID");
            BL_student_formcomplete bl = new BL_student_formcomplete();

            string ApplicationID = EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENBApplicationNo"));

            StudentLogin tblST = new StudentLogin();
            BL_PrintApplication ob = new BL_PrintApplication();
            int sessionid = ac.GetAcademiccurrentSession().ID;
            var obj1 = tblST.BasicDetail(ApplicationID);
            var res1 = bl.CheckAdmission_details(sessionid);
            if (applyaffdavit == "applyaffdavit")
            {

                stlogin.student_collegeform_affidaavitapply(obj1.Id, obj1.session, obj1.CourseCategory, obj.Collegeid, obj.streamcategoryid, obj.studentyear, 0, 0);
                return RedirectToAction("AdmissionFeeSubmit");
            }
            ViewBag.IsAdmisApplied = res1.IsApplied;
            ViewBag.IsAppliedDate = res1.IsAppliedDate;
            ViewBag.IsDocVerifyDate = res1.IsDocVerifyDate;
            if (obj1 == null)
            {
                return RedirectToAction("Index", "HomeL");
            }
            if (ViewBag.IsAdmisApplied == false)
            {
                return RedirectToAction("Index", "HomeL");
            }
            if (res1.IsDocVerify == 0)
            {
                return RedirectToAction("Index", "HomeL");
            }
            if (res1.IsDocVerify == 2)
            {
                return RedirectToAction("Index", "HomeL");
            }
            if (obj1.session == 39)
            {
             
            }
            //if (obj1.StudentYear == 33 || obj1.StudentYear == 31) // only for sem-2 and sem-4
            //{

            //}
            //else
            //{
            //    TempData["msgerror"] = "Date Closed";
            //    return RedirectToAction("Index", "HomeL");
            //}
            if (obj1 != null)
            {
                int educationtype = obj1.EducationType;
                //ViewBag.addmissionExtenddate = ob.CheckStudentAddmisionExtendDate(sessionid, educationtype);
                //ViewBag.addmissionStartdate = ob.CheckStudentAddmisionStartDate(sessionid, educationtype);
                var dateextend = ob.CheckStudentAddmisionExtendDate(sessionid, educationtype);
                ViewBag.addmissionExtenddate = dateextend.Status;
                ViewBag.addmissionExtenddateValue = dateextend.ExtendDate;
                var datestart = ob.CheckStudentAddmisionStartDate(sessionid, educationtype);
                ViewBag.addmissionStartdate = datestart.Status;
                ViewBag.addmissionStartdateValue = datestart.startdate;

            }
            var objst = ob.CheckStudentAdmission(sessionid);
            if (objst.Status == true)
            {
                ViewBag.Status = objst.Status;
                ViewBag.Course = objst.CourseName;
                ViewBag.College = objst.CollegeName;
                ViewBag.Stream = objst.StreamName;
            }
            else
            {
                ViewBag.Status = false;
                ViewBag.Course = "";
                ViewBag.College = "";
            }
            //if (obj1.StudentYear == 30)// for Ist year
            //{

            //    ViewBag.isfeesubmitt = res1.isfeesubmitt;
            //    ViewBag.IsfeesubmitDate = res1.IsfeesubmitDate;
            //}
            //else
            //{
            //    ViewBag.isfeesubmitt = res1.isfeesubmitt2;
            //    ViewBag.IsfeesubmitDate = res1.IsfeesubmitDate2;

            //}
            if (obj1.StudentYear == 30)// for Ist year
            {

                obj = stlogin.FeesDetails(obj1.Id);

                ViewBag.isfeesubmitt = res1.isfeesubmitt;
                ViewBag.IsfeesubmitDate = res1.IsfeesubmitDate;
                //stlogin.student_collegeform_apply(obj1.Id, obj1.session, obj1.CourseCategory, obj.Collegeid, obj.streamcategoryid, obj.studentyear, 0, 0);
                //if (obj.IsApplied == 0)
                //{
                //    return RedirectToAction("AdmissionFeeSubmit");
                //}
            }
            else
            {


                obj = stlogin.FeesDetailsotheryear(obj1.Id); // for other year
                ViewBag.isfeesubmitt = res1.isfeesubmitt2;
                ViewBag.IsfeesubmitDate = res1.IsfeesubmitDate2;
                //if (applyadmissionform == "applyadmissionform")
                //{
                //    stlogin.student_collegeform_apply(obj1.Id, obj1.session, obj1.CourseCategory, obj.Collegeid, obj.streamcategoryid, obj.studentyear, 0, 0);
                //    return RedirectToAction("AdmissionFeeSubmit");
                //}
                //if (obj.IsApplied == 0)
                //{
                //    return RedirectToAction("AdmissionFeeSubmit");
                //}
                if (obj.IsDocVerify == 1)
                {

                }
                else
                {
                    return RedirectToAction("AdmissionFeeSubmit");
                }

            }
          
           

            if (obj1 != null)
            {
                //if (obj1.session == 39)
                //{
                    if (ViewBag.isfeesubmitt != true)
                    {
                        if (ViewBag.addmissionExtenddate == true && ViewBag.addmissionStartdate == true)
                        {

                        return RedirectToAction("SelectGetwayAdmissionFee", "HomeL"); // for all remaining student
                        //return RedirectToAction("PGAdmissionGateway", "HomeL");
                        }
                    }
                //}
               
            }
            var tuple2 = new Tuple<AdmissionFeesSubmit, List<AdmissionFeesSubmit>, Login>(obj, feestruckture, obj1);
            return View(tuple2);
        }


        public ActionResult DoubleVerification_CollegeFeesAdmissionAirpay()
        {

            try
            {
                string ApplicationNO = ClsLanguage.GetCookies("NBApplicationNo");
                doubleverificationgetstudent obj211 = new doubleverificationgetstudent();
                SbiepayAdmission sb = new SbiepayAdmission();
                Recruitment re = new Recruitment();
                DataTable dt = new DataTable();
                UserLogin objlogin = new UserLogin();
                var obj = objlogin.GetStudents_ForDoubleVerificationAdmissionAirpay(obj211, ApplicationNO, "AirPay");
                int a = 0;
                string mid = "";
                string trxid = "";
                foreach (var item in obj)

                {

                    string cmid = CommonMethod.MIDcollegewiseAirPay().Where(x => x.collegeid == item.collegeid).FirstOrDefault().mid;
                    string cusername = CommonMethod.MIDcollegewiseAirPay().Where(x => x.collegeid == item.collegeid).FirstOrDefault().UserName;
                    string cpassword = CommonMethod.MIDcollegewiseAirPay().Where(x => x.collegeid == item.collegeid).FirstOrDefault().Password;
                    string ckey = CommonMethod.MIDcollegewiseAirPay().Where(x => x.collegeid == item.collegeid).FirstOrDefault().mkey;

                    string mercid = cmid;
                    string usernam = cusername;
                    string passwor = cpassword;
                    string secretKey = ckey;

                    var k = secretKey + "@" + usernam + ":|:" + passwor;
                    //mid = "1000858";
                    var pkey = sb.EncryptSHA256Managed(k);
                    trxid = item.clienttrxid;
                    // CommonMethod.WritetoNotepad(null, HttpContext.Request.Url.AbsolutePath, "DoubleVerificationURlRegistration " + "Breakpreetam heelo ");
                    string URI = "https://payments.airpay.co.in/order/verify.php";
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                    var request = (HttpWebRequest)WebRequest.Create(URI);
                    var postData = "mercid=" + mercid;
                    postData += "&merchant_txnId=" + trxid;
                    postData += "&privatekey=" + pkey;
                    // postData += "&airpayId=" + pkey;
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
                    SbiepayAdmission sbi = new SbiepayAdmission();

                    var result = sbi.doubleverificationAdmissionAirpay(responseString);

                    //return RedirectToAction("BackExamFeesSubmit", new { @courseyearidenc = courseyearidenc });

                    System.Threading.Thread.Sleep(10);
                }
            }
            catch (Exception ex)
            { }
            return View();
        }


        public ActionResult SelectGetwayAdmissionFee()
        {
            return View();
        }


        public ActionResult AdmissionGatewaySafex()
        {
            try
            {


                SbiepayAdmission sbi = new SbiepayAdmission();
                AdmissionFeesSubmit stlogin = new AdmissionFeesSubmit();
                AdmissionFeesSubmit result = new AdmissionFeesSubmit();
                List<AdmissionFeesSubmit> feestruckture = new List<AdmissionFeesSubmit>();
                decimal Amount1 = 1;
                string ApplicationID = EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENBApplicationNo"));
                StudentLogin tblST = new StudentLogin();
                var obj1 = tblST.BasicDetail(ApplicationID);
                string MerchantCustomerID1 = "1";
                if (obj1.StudentYear == 30)// for Ist sem
                {
                    result = stlogin.FeesDetails(Convert.ToInt32(EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENNBStID"))));
                    if (result.tbl_recruitment_IsApplied == 0)
                    {
                        return RedirectToAction("AdmissionFeeSubmit");
                    }
                    if (result.tbl_recruitment_IsDocVerify == 1)
                    {

                    }
                    else
                    {
                        return RedirectToAction("AdmissionFeeSubmit");
                    }
                }
                else
                {
                    // for other year
                    result = stlogin.FeesDetailsotheryear(Convert.ToInt32(EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENNBStID"))));
                    if (result.IsApplied == 0)
                    {
                        return RedirectToAction("AdmissionFeeSubmit");
                    }
                    if (result.IsDocVerify == 1)
                    {

                    }
                    else
                    {
                        return RedirectToAction("AdmissionFeeSubmit");
                    }
                }
                if (result.FeeStatus1 == "Paid")
                {
                    return RedirectToAction("AdmissionFeeSubmit");
                }
                feestruckture = stlogin.FeesDetailsstructure(result.Collegeid, result.coursecategoryid, result.sessionid, result.CastCategory, result.streamcategoryid, result.studentyear, result.incomecertificate_iseligible, 0);
                if (result != null)
                {
                    BL_PrintApplication ob = new BL_PrintApplication();
                    AcademicSession ac = new AcademicSession();
                    int sessionid = ac.GetAcademiccurrentSession().ID;
                    int educationtype = Convert.ToInt32(CommonSetting.Commonid.EducationtypeLLB);
                    var datestart = ob.CheckStudentAddmisionStartDate(sessionid, educationtype);

                    if (datestart.Status == false)
                    {
                        return RedirectToAction("AdmissionFeeSubmit");
                    }
                    var dateextend = ob.CheckStudentAddmisionExtendDate(sessionid, educationtype);
                    if (dateextend.Status == false)
                    {
                        return RedirectToAction("AdmissionFeeSubmit");
                    }
                    Amount1 = Convert.ToDecimal(feestruckture.Sum(x => x.amount));
                    if (Amount1 <= 0)
                    {
                        TempData["msgerror"] = "Amount Should be greater than 0 !!";
                        return RedirectToAction("AdmissionFeeSubmit", "HomeL");
                    }
                    if (sessionid == 39)
                    {

                    }

                    //return RedirectToAction("AdmissionFeeSubmit", "Home");
                    //if (ClsLanguage.GetCookies("NBApplicationNo") == "MU33761657")//Add For Testing Purpose
                    //{
                    //    Amount1 = 10;
                    //}

                    //LocalTest
                    string SucccessUrl = "http://localhost:33166/StudentLLB/HomeL/SafexPayUGSucessAdmission";
                    //Live
                    //string SucccessUrl = "https://portal.DemoUniversity.com/Student/home/SafexPayUGSucessAdmission";

                    //Amount1 = 2; //Add by jitendra 
                    var obj = sbi.encriptDataadmissionSafex(Amount1, EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENBApplicationNo")), SucccessUrl, SucccessUrl, result.mid, result.mkey, result.Collegeid, obj1.StudentYear);
                    Commn_master com = new Commn_master();
                    AcademicSession ad = new AcademicSession();
                    ViewBag.url = obj.Safex_POSTURL;
                    ViewBag.me_id = obj.Safex_me_id;
                    ViewBag.merchant_request = obj.Safex_merchant_request;
                    ViewBag.hash = obj.Safex_hash;
                }
            }
            catch (Exception ex)
            {
                CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "admission action PGGateway error", ClsLanguage.GetCookies("NBApplicationNo"));
                return RedirectToAction("AdmissionFeeSubmit");
                return View();
            }
            return View();
        }


        public ActionResult AdmissionGatewayAirPay()
        {
            try
            {

                SbiepayAdmission sbi = new SbiepayAdmission();
                AdmissionFeesSubmit stlogin = new AdmissionFeesSubmit();
                AdmissionFeesSubmit result = new AdmissionFeesSubmit();
                List<AdmissionFeesSubmit> feestruckture = new List<AdmissionFeesSubmit>();
                decimal Amount1 = 1;
                string ApplicationID = EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENBApplicationNo"));
                StudentLogin tblST = new StudentLogin();
                var obj1 = tblST.BasicDetail(ApplicationID);
                string MerchantCustomerID1 = "1";
                if (obj1.StudentYear == 30)// for Ist sem
                {
                    result = stlogin.FeesDetails(Convert.ToInt32(EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENNBStID"))));
                    if (result.tbl_recruitment_IsApplied == 0)
                    {
                        return RedirectToAction("AdmissionFeeSubmit");
                    }
                    if (result.tbl_recruitment_IsDocVerify == 1)
                    {

                    }
                    else
                    {
                        return RedirectToAction("AdmissionFeeSubmit");
                    }
                }
                else
                {
                    // for other year
                    result = stlogin.FeesDetailsotheryear(Convert.ToInt32(EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENNBStID"))));
                    if (result.IsApplied == 0)
                    {
                        return RedirectToAction("AdmissionFeeSubmit");
                    }
                    if (result.IsDocVerify == 1)
                    {

                    }
                    else
                    {
                        return RedirectToAction("AdmissionFeeSubmit");
                    }
                }
                if (result.FeeStatus1 == "Paid")
                {
                    return RedirectToAction("AdmissionFeeSubmit");
                }
                feestruckture = stlogin.FeesDetailsstructure(result.Collegeid, result.coursecategoryid, result.sessionid, result.CastCategory, result.streamcategoryid, result.studentyear, result.incomecertificate_iseligible, 0);
                if (result != null)
                {
                    BL_PrintApplication ob = new BL_PrintApplication();
                    AcademicSession ac = new AcademicSession();
                    int sessionid = ac.GetAcademiccurrentSession().ID;
                    int educationtype = Convert.ToInt32(CommonSetting.Commonid.EducationtypeLLB);
                    var datestart = ob.CheckStudentAddmisionStartDate(sessionid, educationtype);

                    if (datestart.Status == false)
                    {
                        return RedirectToAction("AdmissionFeeSubmit");
                    }
                    var dateextend = ob.CheckStudentAddmisionExtendDate(sessionid, educationtype);
                    if (dateextend.Status == false)
                    {
                        return RedirectToAction("AdmissionFeeSubmit");
                    }
                    Amount1 = Convert.ToDecimal(feestruckture.Sum(x => x.amount));
                    if (Amount1 <= 0)
                    {
                        TempData["msgerror"] = "Amount Should be greater than 0 !!";
                        return RedirectToAction("AdmissionFeeSubmit", "HomeL");
                    }
                    if (sessionid == 39)
                    {

                    }

                    //return RedirectToAction("AdmissionFeeSubmit", "Home");
                    //if (ClsLanguage.GetCookies("NBApplicationNo") == "MU99916090")//Add For Testing Purpose
                    //{
                    //    Amount1 = 10;
                    //}

                    //Amount1 = 2; //Add by jitendra 
                    var obj = sbi.encriptDataadmissionAirPay(Amount1, EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENBApplicationNo")), "student/HomeL/AirPayPGSucessAdmission", "Student/HomeL/Failedadmission", result.mid, result.mkey, result.Collegeid, obj1.StudentYear);
                    //ViewBag.requestparams = obj.requestparams;
                    //ViewBag.merchantId = obj.merchantId;
                    //ViewBag.EncryptbillingDetails = obj.EncryptbillingDetails;
                    //ViewBag.EncryptshippingDetais = obj.EncryptshippingDetais;
                    //ViewBag.url = obj.url;
                    ViewBag.orderid = obj.Oid;
                    ViewBag.buyerEmail = obj1.Email;
                    ViewBag.buyerPhone = obj1.MobileNo;
                    ViewBag.buyerFirstName = obj1.FirstName;
                    ViewBag.buyerLastName = obj1.LastName;
                    ViewBag.buyerPinCode = obj1.CA_PinCode;
                    ViewBag.amount = Amount1.ToString();
                    ViewBag.chmod = "";
                    ViewBag.checksum = obj.checksum;
                    ViewBag.privatekey = obj.privatekey;
                    ViewBag.mercid = obj.merchantId;
                    ViewBag.kittype = "inline";
                    ViewBag.currency = "360";
                    ViewBag.isocurrency = "INR";
                    ViewBag.url = "https://intschoolpay.nowpay.co.in/pay/index.php";
                    ViewBag.success_url = "http://localhost:33166/StudentLLB/HomeL/AirPayUGSucessAdmission";
                  // ViewBag.success_url = "https://portal.DemoUniversity.com/Student/home/AirPayUGSucessAdmission";
                    ViewBag.customvar = obj.customvar;
                }
            }
            catch (Exception ex)
            {
                CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "admission action PGGateway error", ClsLanguage.GetCookies("NBApplicationNo"));
                return RedirectToAction("AdmissionFeeSubmit");
                return View();
            }
            return View();
        }

        public ActionResult AirPayUGSucessAdmission()//New Add Airpay Response For Admission
        {

            string paramInfo = "";
            if (Request.Form.Count > 0)
            {
                try
                {
                    string error = "";
                    string TRANSACTIONSTATUS = Request.Params.Get("TRANSACTIONSTATUS").Trim();
                    string APTRANSACTIONID = Request.Params.Get("APTRANSACTIONID").Trim();
                    string MESSAGE = Request.Params.Get("MESSAGE").Trim();
                    string TRANSACTIONID = Request.Params.Get("TRANSACTIONID").Trim();
                    string AMOUNT = Request.Params.Get("AMOUNT").Trim();
                    var response = Request.Params;
                    string CUSTOMVAR = Request.Params.Get("CUSTOMVAR").Trim();
                    string ap_SecureHash = Request.Params.Get("ap_SecureHash").Trim();
                    string CHMOD = Request.Params.Get("CHMOD").Trim();

                    if (TRANSACTIONSTATUS == "" || APTRANSACTIONID == "" || TRANSACTIONID == "" || AMOUNT == "" || ap_SecureHash == "")
                    {
                        if (TRANSACTIONID == "") { error = "TRANSACTIONID"; }
                        if (APTRANSACTIONID == "") { error = "APTRANSACTIONID"; }
                        if (AMOUNT == "") { error = "AMOUNT"; }
                        if (TRANSACTIONSTATUS == "") { error = "TRANSACTIONSTATUS"; }
                        if (ap_SecureHash == "") { error = "ap_SecureHash"; }
                    }
                    DateTime now = DateTime.Now;
                    string MID = "";
                    string username = "";
                    //comparing Secure Hash with Hash sent by Airpay
                    string sTemp = TRANSACTIONID + ":" + APTRANSACTIONID + ":" + AMOUNT + ":" + TRANSACTIONSTATUS + ":" + MESSAGE + ":" + MID + ":" + username;
                    string strCRC = CRCCode(sTemp, ap_SecureHash);
                    string banktrxid = "";
                    string clienttrxid = "";
                    string amount1 = AMOUNT;
                    string feeamount = "0";
                    string gst = "0";
                    string commission = "0";
                    string paymode = CHMOD;
                    string banktxndate = now.ToString();
                    string Reason = CHMOD;
                    string apitxnid = APTRANSACTIONID;
                    string ApplicationNo = "";
                    string courseyearid = "";
                    string AdmissionType = "";
                    string Requestdata = sTemp;
                    string dRequestdata = strCRC;
                    string PGstatus = MESSAGE;
                    string Sid = "";
                    string Sessionid = "";

                    //Other_Details = Other_Details1 + ",MainYear," + courseyearid + ",Exampaper-" + year + "," + Order_Number;
                    var other_detail = CUSTOMVAR.Split(',');
                    ApplicationNo = other_detail[0];
                    AdmissionType = other_detail[1];
                    courseyearid = other_detail[2];
                    clienttrxid = other_detail[4];
                    Sid = other_detail[5];

                    Sessionid = other_detail[6];


                    //Other_Details = YearType + "," + 1 + ", Admission," + Order_Number + "," + StID + "," + Sission + ",";


                    if (error == "")
                    {
                        if (TRANSACTIONSTATUS == "200")
                        {
                            SbiepayAdmission sbi = new SbiepayAdmission();
                            var result = sbi.AirPaypgsucessdecrypt(Convert.ToInt32(Sid), banktrxid, clienttrxid, amount1, feeamount, gst, commission, paymode, banktxndate, Reason, apitxnid, ApplicationNo, courseyearid, AdmissionType, Requestdata, dRequestdata, PGstatus, Sessionid);
                            return RedirectToAction("ResponseAdmission");
                        }
                        else
                        {
                            SbiepayAdmission sbi = new SbiepayAdmission();
                            var result = sbi.AirPaypgsucessdecrypt(Convert.ToInt32(Sid), banktrxid, clienttrxid, amount1, feeamount, gst, commission, paymode, banktxndate, Reason, apitxnid, ApplicationNo, courseyearid, AdmissionType, Requestdata, dRequestdata, PGstatus, Sessionid);
                            return RedirectToAction("ResponseAdmission");
                        }
                    }
                    else
                    {
                        PGstatus = "ERROR";
                        SbiepayAdmission sbi = new SbiepayAdmission();
                        var result = sbi.AirPaypgsucessdecrypt(Convert.ToInt32(Sid), banktrxid, clienttrxid, amount1, feeamount, gst, commission, paymode, banktxndate, Reason, apitxnid, ApplicationNo, courseyearid, AdmissionType, Requestdata, dRequestdata, PGstatus, Sessionid);
                        return RedirectToAction("ResponseAdmission");
                    }

                }
                catch (Exception ex)
                {
                    CommonMethod.TraceLogWritetoNotepad("obj1.objExamFrom4", "Exam//AirPayPGSucessExam13", "obj1.objExamFrom", "Id");
                    CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, " Air Pay Payment PaymentGateway suucess url hit on Controller error for Exam Fees Submit", ClsLanguage.GetCookies("NBApplicationNo"));

                }
            }
            return RedirectToAction("AdmissionFeeSubmit");
            return View();
        }


        public string CRCCode(String ClearString, String key)
        {
            Crc32 crc32 = new Crc32();
            String hash = String.Empty;
            byte[] mybytes = Encoding.UTF8.GetBytes(ClearString);
            foreach (byte b in crc32.ComputeHash(mybytes)) hash += b.ToString("x2");
            UInt32 Output = UInt32.Parse(hash, System.Globalization.NumberStyles.HexNumber);
            UInt32 Output1 = UInt32.Parse(key);
            //  Response.Write(Output);
            //  Response.Write(Output1);
            if (Output1 == Output)
            {
                // Response.Write("Secure Hash match.");
                // return true.ToString();

            }
            else
            {
                // return true.ToString();
                // Environment.Exit(0);
            }

            return hash;

        }


        public ActionResult PGAdmissionGateway()
        {
            try
            {
                SbiepayAdmission sbi = new SbiepayAdmission();
                AdmissionFeesSubmit stlogin = new AdmissionFeesSubmit();
                AdmissionFeesSubmit result = new AdmissionFeesSubmit();
                List<AdmissionFeesSubmit> feestruckture = new List<AdmissionFeesSubmit>();
                decimal Amount1 = 1;
                string ApplicationID = EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENBApplicationNo"));
                StudentLogin tblST = new StudentLogin();
                var obj1 = tblST.BasicDetail(ApplicationID);
                string MerchantCustomerID1 = "1";
                if (obj1.StudentYear == 30)// for Ist sem
                {
                    result = stlogin.FeesDetails(Convert.ToInt32(EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENNBStID"))));
                    if (result.tbl_recruitment_IsApplied == 0)
                    {
                        return RedirectToAction("AdmissionFeeSubmit");
                    }
                    if (result.tbl_recruitment_IsDocVerify == 1)
                    {

                    }
                    else
                    {
                        return RedirectToAction("AdmissionFeeSubmit");
                    }
                }
                else
                {
                     // for other year
                    result = stlogin.FeesDetailsotheryear(Convert.ToInt32(EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENNBStID"))));
                    if (result.IsApplied == 0)
                    {
                        return RedirectToAction("AdmissionFeeSubmit");
                    }
                    if (result.IsDocVerify == 1)
                    {

                    }
                    else
                    {
                        return RedirectToAction("AdmissionFeeSubmit");
                    }
                }
                if (result.FeeStatus1 == "Paid")
                {
                    return RedirectToAction("AdmissionFeeSubmit");
                }
                feestruckture = stlogin.FeesDetailsstructure(result.Collegeid, result.coursecategoryid, result.sessionid, result.CastCategory, result.streamcategoryid, result.studentyear, result.incomecertificate_iseligible,0);
                if (result != null)
                {
                    BL_PrintApplication ob = new BL_PrintApplication();
                    AcademicSession ac = new AcademicSession();
                    int sessionid = ac.GetAcademiccurrentSession().ID;
                    int educationtype = Convert.ToInt32(CommonSetting.Commonid.EducationtypeLLB);
                    var datestart = ob.CheckStudentAddmisionStartDate(sessionid, educationtype);

                    if (datestart.Status == false)
                    {
                        return RedirectToAction("AdmissionFeeSubmit");
                    }
                    var dateextend = ob.CheckStudentAddmisionExtendDate(sessionid, educationtype);
                    if (dateextend.Status == false)
                    {
                        return RedirectToAction("AdmissionFeeSubmit");
                    }
                    Amount1 = Convert.ToDecimal(feestruckture.Sum(x => x.amount));
                    if (Amount1 <= 0)
                    {
                        TempData["msgerror"] = "Amount Should be greater than 0 !!";
                        return RedirectToAction("AdmissionFeeSubmit", "HomeL");
                    }
                    if (sessionid == 39)
                    {
                       
                    }
                    //Amount1 = 1;
                    var obj = sbi.encriptDataadmission(Amount1, EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENBApplicationNo")), "studentLLB/homeL/PGSucessadmission", "studentLLB/homeL/PGFailedadmission", result.mid, result.mkey, result.Collegeid, obj1.StudentYear);
                    ViewBag.requestparams = obj.requestparams;
                    ViewBag.merchantId = obj.merchantId;
                    ViewBag.EncryptbillingDetails = obj.EncryptbillingDetails;
                    ViewBag.EncryptshippingDetais = obj.EncryptshippingDetais;
                    ViewBag.url = obj.url;
                }
            }
            catch (Exception ex)
            {
                CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "admission action PGGateway error", ClsLanguage.GetCookies("NBApplicationNo"));
                return RedirectToAction("AdmissionFeeSubmit");
                return View();
            }
            return View();
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
        public ActionResult ResponseAdmission()
        {

            try
            {
                BL_PrintApplication PritApp = new BL_PrintApplication();
                var app = EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENBApplicationNo"));
                Login objl22 = new Login();
                StudentLogin objs = new StudentLogin();
                objl22 = objs.BasicDetail(app);
                AdmissionFeesSubmit obj = new AdmissionFeesSubmit();
                AdmissionFeesSubmit stlogin = new AdmissionFeesSubmit();

                List<AdmissionFeesSubmit> feestruckture = new List<AdmissionFeesSubmit>();
                if (objl22.StudentYear == 30)// for Ist year
                {
                    obj = stlogin.FeesDetails(Convert.ToInt32(EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENNBStID"))));
                }
                else
                {
                    // for other year
                    obj = stlogin.FeesDetailsotheryear(Convert.ToInt32(EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENNBStID"))));
                }

                
                feestruckture = stlogin.FeesDetailsstructure(obj.Collegeid, obj.coursecategoryid, obj.sessionid, obj.CastCategory, obj.streamcategoryid, obj.studentyear, obj.incomecertificate_iseligible,0);
                var tuple = new Tuple<AdmissionFeesSubmit, List<AdmissionFeesSubmit>>(obj, feestruckture);
                //var obj1 = PritApp.GetAppLicationDataAdmin(objl22.Id);
                //Email.SendEmailForSt_RegistrationPaymentgateway(obj1.ObjApplication.Email, obj1.objPrintRecipt.status, obj1.ObjApplication.Name, obj1.objPrintRecipt.trxdate, obj1.objPrintRecipt.banktrxid, obj1.objPrintRecipt.TransactionId, obj1.objPrintRecipt.ApplicationNo, obj1.objfeesubmit.Fees, obj1.objPrintRecipt.PaymentType);
                //  return View(obj1);
                return View(tuple);
            }
            catch (Exception ex)
            {
                CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, " admission Response payment gatewat get method on Controller error", ClsLanguage.GetCookies("NBApplicationNo"));
                BL_PrintAllRecord PritApp = new BL_PrintAllRecord();
                return View(PritApp);
            }
        }
        public ActionResult NewSubject(int id = 0)
        {
            //return RedirectToAction("SelectSubject", "Home");
            StudentAdmissionQualification obj = new StudentAdmissionQualification();
            var ApplicationNo = ClsLanguage.GetCookies("NBApplicationNo");
            Login objl = new Login();
            StudentLogin objs = new StudentLogin();
            AcademicSession ac = new AcademicSession();
            objl = objs.BasicDetail(ApplicationNo);
            if (objl.session == 39)
            {
                return RedirectToAction("SelectSubject", "HomePG");
            }
            ViewBag.IsFeeSubmit_spot = objl.IsFeeSubmit_spot;
            ViewBag.IsSubmit = objl.IsFeeSubmit;
            if (objl != null)
            {
                BL_StreamMaster stream = new BL_StreamMaster();
                var soptresult = stream.checkspotadmissionEntry(objl.Id);
                if (soptresult.status == true)
                {
                    ViewBag.SpotadStatus = soptresult.status;
                    ViewBag.hounors_subjectid = soptresult.hounors_subjectid;
                    ViewBag.collegeid = soptresult.collegeid;
                    ViewBag.hounors_subjectName = soptresult.hounors_subjectName;
                    ViewBag.CollegeName = soptresult.CollegeName;
                    var subjectid = soptresult.hounors_subjectid;
                    var collegeid = soptresult.collegeid;
                    //ViewBag.subsidiary1 = stream.getcollegesubjects(objl.Id, 11, collegeid, subjectid);
                    //ViewBag.subcomposition1 = stream.getcollegesubjects(objl.Id, 13, collegeid);
                    ViewBag.subsidiarySave = true;//stream.checkspotsubsidiarySave(objl.Id).status;
                }

                QualiSubject_bind(objl.previous_qua_id);
                Subject_bind(objl.previous_qua_id);
            }
            FeesSubmit log = new FeesSubmit();
            var listgeo = log.Showgeography().ToList();
            bool alreadyExist = listgeo.Contains(objl.Id);
            ViewBag.editrecoer = alreadyExist;

            Commn_master com = new Commn_master();
            ViewBag.check_admissionopen = false;// com.check_admission_open(ac.GetAcademiccurrentSession().ID, Convert.ToInt32(CommonSetting.Commonid.EducationtypeLLB));
            int educationtype = Convert.ToInt32(CommonSetting.Commonid.EducationtypeLLB);
            BL_PrintApplication ob123 = new BL_PrintApplication();
            var dateextend = ob123.CheckStudentAddmisionExtendDate(objl.session, educationtype);
          
            var datestart = ob123.CheckStudentAddmisionStartDate(objl.session, educationtype);
            ViewBag.addmissionStartdate = datestart.Status;

            ViewBag.addmissionExtenddate = dateextend.Status;
            if (datestart.Status == true && dateextend.Status == true)
            {
                ViewBag.check_admissionopen = true;

            }

            BL_CollegeMaster obj111 = new BL_CollegeMaster();
            CollageList sub = new CollageList();
            ViewBag.gender = objl.Gender;
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
            for (int i = 0; i < 8; i++)
            {
                list.Add(new StudentPreviousQualification());
            }
            StudentPreviousQualification StudentPrevious = new StudentPreviousQualification();
            var tuple = new Tuple<StudentAdmissionQualification, List<StudentPreviousQualification>, Login>(obj, list, objl);
            ViewBag.percentage = StudentPrevious.getqualify_percentage(objl.CourseCategory, objl.issame_stream);
            var countlist = StudentPrevious.GetSubjectPercentageData(objl.ApplicationNo);

            BL_PrintApplication obrecrei = new BL_PrintApplication();

            var objrecritiny = obrecrei.CheckStudentAdmission(ac.GetAcademiccurrentSession().ID);
            ViewBag.Statusrecruitny = false;
            if (objrecritiny.Status == true)
            {
                ViewBag.Statusrecruitny = objrecritiny.Status;
            }

            if (objl.previous_qua_id != 11)
            {
                //if (countlist.Count == 0)
                //{
                //    TempData["selectsubmsg"] = "Please First Upload your Qualification!!";
                //    return View(tuple);
                //}
            }

            if (objl.issame_stream == false)
            {
                var countlistcheck = StudentPrevious.GetSubjecisdiffrentstream_percentage(objl.Id.ToString(), objl.previous_qua_id);

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
            }
            if (objl.issame_stream == true)
            {

                var countlistcheck = StudentPrevious.GetSubjecissamestream_percentage(objl.Id.ToString(), objl.previous_qua_id, objl.CourseCategory);

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
            }

            Student_Admission_Choicesubject Choicesubject = new Student_Admission_Choicesubject();
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

        public JsonResult Changechoicesubject_spot()
        {
            // return Json(new { data = new Student_Admission_Choicesubject(), success = true });
            BL_StreamMaster obj = new BL_StreamMaster();
            //Student_Admission_Choicesubject obj = new Student_Admission_Choicesubject();
            var ApplicationNo = ClsLanguage.GetCookies("NBApplicationNo");
            Login objl = new Login();
            StudentLogin objs = new StudentLogin();
            BL_PrintApplication obrecrei = new BL_PrintApplication();
            AcademicSession ac = new AcademicSession();
            var objrecritiny = obrecrei.CheckStudentAdmission(ac.GetAcademiccurrentSession().ID);
            if (objrecritiny.Status == true)
            {
                return Json(new { data = new Student_Admission_Choicesubject(), success = true });
            }
            try
            {
                objl = objs.BasicDetail(ApplicationNo);
                var result = obj.Changesubjects_Choice("ChangeChoice", objl.Id, objl.session);
                return Json(new { data = result, success = true });

            }
            catch (Exception ex)
            {
                CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "Spot _Saving subject Choice ", ClsLanguage.GetCookies("NBApplicationNo") + "   hounors_subjectidlist: " + "" + "    collegeidlist:" + "" + "  Subsidiary1_subjectidlist:" + "" + "   Subsidiary2_subjectidlist:" + "" + "     Compulsory1_subjectidlist:" + "" + "   Compulsory2_subjectidlist:" + "");
                return Json(new { data = new Student_Admission_Choicesubject(), success = true });
            }


        }
        [HttpPost]

        public JsonResult savest_choicesubject_spot(int hounors_subjectid, int collegeid)
        {
            BL_StreamMaster obj = new BL_StreamMaster();
            //Student_Admission_Choicesubject obj = new Student_Admission_Choicesubject();
            var ApplicationNo = ClsLanguage.GetCookies("NBApplicationNo");
            Login objl = new Login();
            StudentLogin objs = new StudentLogin();
            BL_PrintApplication obrecrei = new BL_PrintApplication();
            AcademicSession ac = new AcademicSession();
            var objrecritiny = obrecrei.CheckStudentAdmission(ac.GetAcademiccurrentSession().ID);
            if (objrecritiny.Status == true)
            {
                Student_Admission_Choicesubject res = new Student_Admission_Choicesubject();
                res.Status = false;
                res.Msg = "You already got admission in assigned college!!!";
                return Json(new { data = res, success = true });
            }
            try
            {
                objl = objs.BasicDetail(ApplicationNo);
                var result = obj.getcollegesubjects_Savefor15minute("insert", objl.Id, objl.CastCategory, collegeid, hounors_subjectid, objl.session, objl.CourseCategory, 1);
                return Json(new { data = result, success = true });

            }
            catch (Exception ex)
            {
                CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "Spot _Saving subject Choice ", ClsLanguage.GetCookies("NBApplicationNo") + "   hounors_subjectidlist: " + "" + "    collegeidlist:" + "" + "  Subsidiary1_subjectidlist:" + "" + "   Subsidiary2_subjectidlist:" + "" + "     Compulsory1_subjectidlist:" + "" + "   Compulsory2_subjectidlist:" + "");
                return Json(new { data = new Student_Admission_Choicesubject(), success = true });
            }


        }

    }
}
