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
using Website.Areas.StudentPG.Models;
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
namespace Website.Areas.UGCBCS.Controllers
{
    [RouteArea("MUStudentPortalDashBoard")]  // area define kar diya
    [RoutePrefix("")]
    public class HomeUGCBCSController : Controller
    {
        // GET: Student/HomeUGCBCS
        //[VerifyUrlFilterAdminAttributePG]
        [Route("dashboard")]
        public ActionResult Index()
        {
            var url = Request.Url.AbsolutePath.ToLower();
            if (Request.Cookies["ENBUGCBCSApplicationNo"] == null)
            {
                return Redirect("/custom-login");
            }
            // ❌ old URL block (direct 404)
            if (url.Contains("/ugcbcs/homeugcbcs"))
            {
                return HttpNotFound();
            }


            BL_student_formcomplete bl = new BL_student_formcomplete();
            AcademicSession ac = new AcademicSession();
            int sessionid = ac.GetAcademiccurrentSession().ID;
            var result = bl.CheckPasswordBasicDetail(ClsLanguage.GetCookies("NBApplicationNo"), sessionid.ToString());


            if (result.isfeesubmitt == false)
            {
                return Redirect("change-password");
            }
            else if (result.is_basic_complete == false)
            {
                return Redirect("profile");
            }


            //BL_PrintApplication ob = new BL_PrintApplication();
            //string ApplicationID = ClsLanguage.GetCookies("NBApplicationNo");
            //StudentLogin tblST = new StudentLogin();
            //var obj1 = tblST.BasicDetail(ApplicationID);
            //Recruitment rec = new Recruitment();
            //RecruitmentList reclist = new RecruitmentList();
            //reclist = rec.view1customfeesubmittedstudentdetailList(obj1.Id);
            //ViewBag.sessionid = obj1.session;
            //rec = reclist.qlist.ToList().FirstOrDefault();
            //var res1 = bl.CheckAdmission_details(sessionid);
            //ViewBag.IsDocVerify = res1.IsDocVerify;
            //ViewBag.IsAdmisApplied = res1.IsApplied;
            //ViewBag.IsAppliedDate = res1.IsAppliedDate;
            //ViewBag.IsDocVerifyDate = res1.IsDocVerifyDate;
            //ViewBag.isfeesubmitt = res1.isfeesubmitt;
            //ViewBag.IsfeesubmitDate = res1.IsfeesubmitDate;
            //ViewBag.isfeesubmitt2 = res1.isfeesubmitt2;
            //ViewBag.IsfeesubmitDate2 = res1.IsfeesubmitDate2;
            //ViewBag.castecategory = obj1.CastCategory;
            //ViewBag.incomecertificate_iseligible = obj1.incomecertificate_iseligible;
            //ViewBag.incomecertificate = obj1.incomecertificate;
            //ViewBag.incomeRejectReaseon = obj1.incomeRejectReaseon;
            //List<EaxmFeesSubmit> subjectlist = new List<EaxmFeesSubmit>();
            //ExamForm exam = new ExamForm();
            //if (rec != null)
            //{
            //    ViewBag.Stream = rec.StreamCategoryName;

            //    ViewBag.enrollnmentno = rec.enrollmentno;
            //    ViewBag.rollno = rec.RollNo;
            //    ViewBag.Sessionname = rec.Session;
            //    ViewBag.studentname = rec.StudentName;
            //    ViewBag.fathername = rec.FatherName;
            //    ViewBag.mothername = rec.mothername;
            //    ViewBag.DOb = rec.DOB;
            //    ViewBag.Gender = rec.Gender;
            //    ViewBag.category = rec.StudentCasteCategoryName;
            //    ViewBag.Nationlity = rec.Nationlity;
            //    ViewBag.stphoto = rec.stphoto;
            //    ViewBag.stsign = rec.stsign;
            //    ViewBag.Course = rec.coursecategotyName;
            //    ViewBag.ftitle = obj1.Ftitle;
            //    if (obj1.CourseCategory == 7)// ma
            //    {
            //        // for manual check , kon ses semeseter ka back niklana h , 16 for sesmerter-1
            //        subjectlist = exam.backFeesDetailSubjectlist_pg(obj1.CourseCategory, rec.StreamCategoryID, 16, obj1.session, rec.collegeid, obj1.Id);
            //        if (subjectlist.Count > 0)
            //        {
            //            ViewBag.isback = 1;
            //            ViewBag.courseyearidenc1 = EncriptDecript.Encrypt(16.ToString());
            //        }
            //        subjectlist = exam.backFeesDetailSubjectlist_pg(obj1.CourseCategory, 1135, 17, obj1.session, rec.collegeid, obj1.Id); if (subjectlist.Count > 0)
            //        {
            //            ViewBag.isbackpart2 = 1;
            //            ViewBag.courseyearidenc2 = EncriptDecript.Encrypt(17.ToString());
            //        }
            //        subjectlist = exam.backFeesDetailSubjectlist_pg(obj1.CourseCategory, rec.StreamCategoryID, 40, obj1.session, rec.collegeid, obj1.Id); if (subjectlist.Count > 0)
            //        {
            //            ViewBag.isbackpart3 = 1;
            //            ViewBag.courseyearidenc3 = EncriptDecript.Encrypt(40.ToString());
            //        }
            //        subjectlist = exam.backFeesDetailSubjectlist_pg(obj1.CourseCategory, rec.StreamCategoryID, 41, obj1.session, rec.collegeid, obj1.Id); if (subjectlist.Count > 0)
            //        {
            //            ViewBag.isbackpart4 = 1;
            //            ViewBag.courseyearidenc4 = EncriptDecript.Encrypt(41.ToString());
            //        }
            //    }
            //    if (obj1.CourseCategory == 11)//msc
            //    {
            //        // for manual check , kon ses semeseter ka back niklana h , 24 for sesmerter-1
            //        subjectlist = exam.backFeesDetailSubjectlist_pg(obj1.CourseCategory, rec.StreamCategoryID, 24, obj1.session, rec.collegeid, obj1.Id);
            //        if (subjectlist.Count > 0)
            //        {
            //            ViewBag.isback = 1;
            //            ViewBag.courseyearidenc1 = EncriptDecript.Encrypt(24.ToString());
            //        }
            //        subjectlist = exam.backFeesDetailSubjectlist_pg(obj1.CourseCategory, rec.StreamCategoryID, 25, obj1.session, rec.collegeid, obj1.Id); if (subjectlist.Count > 0)
            //        {
            //            ViewBag.isbackpart2 = 1;
            //            ViewBag.courseyearidenc2 = EncriptDecript.Encrypt(25.ToString());
            //        }
            //        subjectlist = exam.backFeesDetailSubjectlist_pg(obj1.CourseCategory, rec.StreamCategoryID, 36, obj1.session, rec.collegeid, obj1.Id); if (subjectlist.Count > 0)
            //        {
            //            ViewBag.isbackpart3 = 1;
            //            ViewBag.courseyearidenc3 = EncriptDecript.Encrypt(36.ToString());
            //        }
            //        subjectlist = exam.backFeesDetailSubjectlist_pg(obj1.CourseCategory, rec.StreamCategoryID, 37, obj1.session, rec.collegeid, obj1.Id); if (subjectlist.Count > 0)
            //        {
            //            ViewBag.isbackpart4 = 1;
            //            ViewBag.courseyearidenc4 = EncriptDecript.Encrypt(37.ToString());
            //        }
            //    }
            //    if (obj1.CourseCategory == 9)//mcom
            //    {
            //        // for manual check , kon ses semeseter ka back niklana h , 26 for sesmerter-1
            //        subjectlist = exam.backFeesDetailSubjectlist_pg(obj1.CourseCategory, rec.StreamCategoryID, 26, obj1.session, rec.collegeid, obj1.Id);
            //        if (subjectlist.Count > 0)
            //        {
            //            ViewBag.isback = 1;
            //            ViewBag.courseyearidenc1 = EncriptDecript.Encrypt(26.ToString());
            //        }
            //        subjectlist = exam.backFeesDetailSubjectlist_pg(obj1.CourseCategory, rec.StreamCategoryID, 27, obj1.session, rec.collegeid, obj1.Id); if (subjectlist.Count > 0)
            //        {
            //            ViewBag.isbackpart2 = 1;
            //            ViewBag.courseyearidenc2 = EncriptDecript.Encrypt(27.ToString());
            //        }
            //        subjectlist = exam.backFeesDetailSubjectlist_pg(obj1.CourseCategory, rec.StreamCategoryID, 38, obj1.session, rec.collegeid, obj1.Id); if (subjectlist.Count > 0)
            //        {
            //            ViewBag.isbackpart3 = 1;
            //            ViewBag.courseyearidenc3 = EncriptDecript.Encrypt(38.ToString());
            //        }
            //        subjectlist = exam.backFeesDetailSubjectlist_pg(obj1.CourseCategory, rec.StreamCategoryID, 39, obj1.session, rec.collegeid, obj1.Id); if (subjectlist.Count > 0)
            //        {
            //            ViewBag.isbackpart4 = 1;
            //            ViewBag.courseyearidenc4 = EncriptDecript.Encrypt(39.ToString());
            //        }
            //    }


            //}
            //else
            //{
            //    ViewBag.Stream = "";
            //    ViewBag.Course = "";
            //    ViewBag.enrollnmentno = "";
            //    ViewBag.rollno = "";
            //    ViewBag.Sessionname = "";
            //    ViewBag.studentname = "";
            //    ViewBag.fathername = "";
            //    ViewBag.mothername = "";
            //    ViewBag.DOb = "";
            //    ViewBag.Gender = "";
            //    ViewBag.category = "";
            //    ViewBag.Nationlity = "";
            //    ViewBag.stphoto = "";
            //    ViewBag.stsign = "";
            //    ViewBag.ftitle = obj1.Ftitle;
            //}
            //if (obj1 != null)
            //{
            //    int educationtype = obj1.EducationType;
            //    var dateextend = ob.CheckStudentAddmisionExtendDate(sessionid, educationtype);
            //    ViewBag.addmissionExtenddate = dateextend.Status;
            //    ViewBag.addmissionExtenddateValue = dateextend.ExtendDate;
            //    var datestart = ob.CheckStudentAddmisionStartDate(sessionid, educationtype);
            //    ViewBag.addmissionStartdate = datestart.Status;
            //    ViewBag.addmissionStartdateValue = datestart.startdate;
            //    //ViewBag.IsApplied = ob.CheckStudentApplied(sessionid).Status;
            //    //ViewBag.IsVerify = ob.CheckStudentVerify(sessionid).Status;
            //    var dateextenddoc = ob.documnetCheckStudentAddmisionExtendDate(sessionid, educationtype);
            //    ViewBag.addmissionExtenddateValuedoc = dateextenddoc.ExtendDate;
            //    var datestartdoc = ob.documentCheckStudentAddmisionStartDate(sessionid, educationtype);
            //    ViewBag.addmissionStartdateValuedoc = datestartdoc.startdate;
            //    //ViewBag.IsApplied = ob.CheckStudentApplied(sessionid).Status;
            //    ViewBag.rejectreason = ob.CheckDocumentVerification(sessionid).rejectreason;
            //}
            //var obj = ob.CheckStudentAdmission(sessionid);
            //ViewBag.sessionid = sessionid;
            //ViewBag.studentyear = obj1.StudentYear;
            //if (obj.Status == true)
            //{
            //    ViewBag.Status = obj.Status;
            //    ViewBag.Course = obj.CourseName;
            //    ViewBag.College = obj.CollegeName;
            //    ViewBag.Stream = obj.StreamName;
            //    ClsLanguage.SetCookies(EncriptDecript.Encrypt(obj.streamcategoryid.ToString()), "ENNBstreamcategoryid");
            //}
            //else
            //{
            //    ViewBag.Status = false;
            //    ViewBag.Course = "";
            //    ViewBag.College = "";
            //    ViewBag.Stream = "";
            //}

            return View("~/Areas/UGCBCS/Views/HomeUGCBCS/Index.cshtml", result);

        }
        [HttpPost, ValidateInput(false)]
        public ActionResult EnrollSlipdownload()
        {

            return Redirect("/custom-login");

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
        //[VerifyUrlFilterAdminAttributeUG]
        [Route("profile")]
        public ActionResult BasicDetail()
        {
            try
            {



                BL_student_formcomplete bl = new BL_student_formcomplete();
                AcademicSession ac = new AcademicSession();
                int sessionid = ac.GetAcademiccurrentSession().ID;
                var result = bl.CheckPasswordBasicDetail(ClsLanguage.GetCookies("NBApplicationNo"), sessionid.ToString());


                if (result.isfeesubmitt == false)
                {
                    return Redirect("change-password");
                }


                Commn_master com = new Commn_master();


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
                objlist = stad.getqualificationst(Convert.ToInt32(CommonSetting.Commonid.EducationtypeUGCBCS)).ToList();
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
                ViewBag.addmissionExtenddate = com.check_admission_open(obj.EducationType, obj.EducationType);


                return View("~/Areas/UGCBCS/Views/HomeUGCBCS/BasicDetail.cshtml", obj);
            }
            catch
            {
                return Redirect("~/custom-login");
            }


        }
        //[VerifyUrlFilterAdminAttributeUGCBCS]
        [HttpPost]
        [Route("update-profile")]
        public ActionResult BasicDetail(Login objlogin, HttpPostedFileBase sign, HttpPostedFileBase photo)
        {
            StudentLogin st = new StudentLogin();
            Commn_master com = new Commn_master();
            AcademicSession ac = new AcademicSession();
            StudentAdmissionQualification stad = new StudentAdmissionQualification();

            // ViewBag.Qualification = stad.GetQualifiationMasterOldStudentPG();
            List<QualifiationMaster> objlist = new List<QualifiationMaster>();
            objlist = stad.getqualificationst(Convert.ToInt32(CommonSetting.Commonid.EducationtypeUGCBCS)).ToList();
            objlist = objlist.Where(x => x.ID != 12).ToList();
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

            string ApplicationID = ClsLanguage.GetCookies("NBApplicationNo");
            StudentLogin tblST = new StudentLogin();
            var obj = tblST.BasicDetail(ApplicationID);
            ViewBag.addmissionExtenddate = com.check_admission_open(obj.session, objlogin.EducationType);
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
            //objlogin.previous_qua_id = obj.previous_qua_id;
            ViewBag.eduid = obj.EducationType;

            //ViewBag.check_admissionopen = com.check_admission_open(ac.GetAcademiccurrentSession().ID, Convert.ToInt32(CommonSetting.Commonid.Educationtype));
            BL_PrintApplication ob = new BL_PrintApplication();
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
                //if (photo != null)
                //{
                //    Stream st1 = photo.InputStream;
                //    string name = Path.GetFileName(photo.FileName);
                //    try
                //    {
                //        string myBucketName = "mungeruniversity"; //your s3 bucket name goes here  
                //        string s3DirectoryName = "Student/Photoandsign";
                //        string s3FileName = System.DateTime.Now.ToString("dd_MM_yyyy_HH_mm_ss") + "_StudentPhoto_" + objlogin.FirstName + @name;
                //        s3FileName = s3FileName.Replace(" ", "");
                //        objlogin.stphoto = s3FileName;
                //        bool a;
                //        AmazonUploader myUploader = new AmazonUploader();
                //        a = myUploader.sendMyFileToFolder(photo,Server.MapPath("~/App_Data/uploads"),s3FileName);
                //    }
                //    catch (Exception ex)
                //    {
                //        CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "Basic Detail post Method", ClsLanguage.GetCookies("NBApplicationNo") + "   " + jsonstring);
                //    }
                //}
                //if (sign != null)
                //{
                //    Stream st1 = sign.InputStream;
                //    string name = Path.GetFileName(sign.FileName);
                //    try
                //    {
                //        string myBucketName = "mungeruniversity"; //your s3 bucket name goes here  
                //        string s3DirectoryName = "Student/Photoandsign";
                //        string s3FileName = System.DateTime.Now.ToString("dd_MM_yyyy_HH_mm_ss") + "_StudentSign_" + objlogin.FirstName + @name;
                //        s3FileName = s3FileName.Replace(" ", "");
                //        objlogin.stsign = s3FileName;
                //        bool a;
                //        AmazonUploader myUploader = new AmazonUploader();
                //        a = myUploader.sendMyFileToFolder(photo,Server.MapPath("~/App_Data/uploads"),s3FileName);
                //    }
                //    catch (Exception ex)
                //    {
                //        CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "Basic Detail post Method", ClsLanguage.GetCookies("NBApplicationNo") + "   " + jsonstring);
                //    }
                //}




                if (!string.IsNullOrEmpty(ApplicationID))
                {
                    // Base upload folder (public folder)
                    string baseUploadPath = Server.MapPath("~/Content/uploads/Student");

                    // Student ka folder (ApplicationID ke naam se)
                    string studentFolder = Path.Combine(baseUploadPath, ApplicationID);

                    if (!Directory.Exists(studentFolder))
                        Directory.CreateDirectory(studentFolder);

                    // --- PHOTO ---
                    if (photo != null)
                    {
                        try
                        {
                            string extension = Path.GetExtension(photo.FileName); // .jpg, .png etc
                            string fileName = "PHOTO_" + Guid.NewGuid().ToString("N") + extension; // unique name

                            string fullPath = Path.Combine(studentFolder, fileName);
                            photo.SaveAs(fullPath); // save in student's folder

                            objlogin.stphoto = $"{ApplicationID}/{fileName}"; // database me path relative to uploads
                        }
                        catch (Exception ex)
                        {
                            CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath,
                                "Basic Detail post Method", ApplicationID + "   " + jsonstring);
                        }
                    }

                    // --- SIGNATURE ---
                    if (sign != null)
                    {
                        try
                        {
                            string extension = Path.GetExtension(sign.FileName);
                            string fileName = "SIGN_" + Guid.NewGuid().ToString("N") + extension;

                            string fullPath = Path.Combine(studentFolder, fileName);
                            sign.SaveAs(fullPath);

                            objlogin.stsign = $"{ApplicationID}/{fileName}"; // DB me relative path
                        }
                        catch (Exception ex)
                        {
                            CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath,
                                "Basic Detail post Method", ApplicationID + "   " + jsonstring);
                        }
                    }
                }

                objlogin.session = ac.GetAcademiccurrentSession().ID;
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
            //if (id == 5)
            //{
            //    list = obj.GetSubject(Convert.ToInt32(CommonSetting.Streamtype.Comm12), board);
            //}
            if (id == 5)
            {
                list = obj.GetSubject(Convert.ToInt32(CommonSetting.Streamtype.ba), board);
            }
            if (id == 7)
            {
                list = obj.GetSubject(Convert.ToInt32(CommonSetting.Streamtype.bsc), board);
            }
            if (id == 9)
            {
                list = obj.GetSubject(Convert.ToInt32(CommonSetting.Streamtype.bcomm), board);
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
                                a = myUploader.sendMyFileToFolder(fileUpload, Server.MapPath("~/App_Data/uploads"), s3FileName);
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
        [VerifyUrlFilterAdminAttributePG]
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
            ViewBag.check_admissionopen = com.check_admission_open(ac.GetAcademiccurrentSession().ID, Convert.ToInt32(CommonSetting.Commonid.EducationtypePG));

            BL_CollegeMaster obj111 = new BL_CollegeMaster();
            CollageList sub = new CollageList();
            ViewBag.gender = objl.Gender;
            ViewBag.sessionid = objl.session;
            if (objl.Gender == 9)
            {
                sub = obj111.collagedetailviewlistdropallotedmalihacollege(objl.CourseCategory, objl.session);
            }
            else
            {
                sub = obj111.collagedetailviewlistdropalloted(objl.CourseCategory, objl.session);
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
        public JsonResult savest_choicesubject(string hounors_subjectidlist, string collegeidlist, string Subsidiary1_subjectidlist, string Subsidiary2_subjectidlist, string Compulsory1_subjectidlist, string Compulsory2_subjectidlist)
        {
            Student_Admission_Choicesubject obj = new Student_Admission_Choicesubject();
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
                obj.SID = objl.Id;
                obj.hounors_subjectidlist = hounors_subjectidlist;
                obj.collegeidlist = collegeidlist;
                obj.Subsidiary1_subjectidlist = CommonSetting.Removenumber(collegeidlist); ;
                obj.Subsidiary2_subjectidlist = CommonSetting.Removenumber(collegeidlist);
                obj.Compulsory1_subjectidlist = CommonSetting.Removenumber(collegeidlist);
                obj.Compulsory2_subjectidlist = CommonSetting.Removenumber(collegeidlist);
                var result = obj.savest_choicesubject(obj);
                return Json(new { data = result, success = true });

            }
            catch (Exception ex)
            {
                CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "Saving subject Choice ", ClsLanguage.GetCookies("NBApplicationNo") + "   hounors_subjectidlist: " + hounors_subjectidlist + "    collegeidlist:" + collegeidlist + "  Subsidiary1_subjectidlist:" + Subsidiary1_subjectidlist + "   Subsidiary2_subjectidlist:" + Subsidiary2_subjectidlist + "     Compulsory1_subjectidlist:" + Compulsory1_subjectidlist + "   Compulsory2_subjectidlist:" + Compulsory2_subjectidlist);
                return Json(new { data = new Student_Admission_Choicesubject(), success = true });
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

            result = objStream.getcollegesubjects(objl.Id, 23, id, 0, 0, 0, objl.CourseCategory);

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
                    return RedirectToAction("PreviousyearQualificationO/" + id, "Home");
                }
                if (objl.prevoiusboardid == 3)
                {
                    return RedirectToAction("PreviousyearQualificationP/" + id, "Home");
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
                ViewBag.check_admissionopen = com.check_admission_open(ac.GetAcademiccurrentSession().ID, Convert.ToInt32(CommonSetting.Commonid.Educationtype));
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
                            return RedirectToAction("StudentQualification", "Home");
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
                            return RedirectToAction("StudentQualification", "Home");
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
                ViewBag.check_admissionopen = com.check_admission_open(ac.GetAcademiccurrentSession().ID, Convert.ToInt32(CommonSetting.Commonid.Educationtype));
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
                    return RedirectToAction("PreviousyearQualificationO/" + id, "Home");
                }
                //if (objl.prevoiusboardid == 3)
                //{
                //    return RedirectToAction("PreviousyearQualificationP/" + id, "Home");
                //}
                if (objl.prevoiusboardid == 1)
                {
                    return RedirectToAction("PreviousyearQualification/" + id, "Home");
                }
                if (objl.prevoiusboardid == 0)
                {
                    return RedirectToAction("PreviousyearQualification/" + id, "Home");
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
                            return RedirectToAction("StudentQualification", "Home");
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
                            return RedirectToAction("StudentQualification", "Home");
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
        [VerifyUrlFilterAdminAttributePG]
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
                            return RedirectToAction("StudentQualification", "HomeUGCBCS");
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
                            return RedirectToAction("StudentQualification", "HomeUGCBCS");
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
        [VerifyUrlFilterAdminAttributePG]
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
            ViewBag.check_admissionopen = com.check_admission_open(ac.GetAcademiccurrentSession().ID, Convert.ToInt32(CommonSetting.Commonid.EducationtypePG));
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

        [Route("changePassword")]
        public ActionResult Changepassword()
        {
            var url = Request.Url.AbsolutePath.ToLower();
            if (Request.Cookies["ENBUGCBCSApplicationNo"] == null)
            {
                return Redirect("/custom-login");
            }
            // ❌ old URL block (direct 404)
            if (url.Contains("/ugcbcs/changePassword"))
            {
                return HttpNotFound();
            }
            return View("~/Areas/UGCBCS/Views/HomeUGCBCS/changePassword.cshtml");
        }

        public string DoubleVerificationAirpay_300loop()
        {
            try
            {
                string ApplicationNO = ClsLanguage.GetCookies("NBApplicationNo");
                doubleverificationgetstudent obj211 = new doubleverificationgetstudent();
                Sbiepay sb = new Sbiepay();
                Recruitment re = new Recruitment();
                DataTable dt = new DataTable();
                UserLogin objlogin = new UserLogin();
                dt = null;
                var obj = objlogin.GetStudents_ForDoubleVerification300Airpay(obj211, ApplicationNO);
                int a = 0;
                string mid = "";
                string trxid = "";
                foreach (var item in obj)
                {


                    string mercid = "256538";
                    string usernam = "2609081";
                    string passwor = "EJ3Qhr7E";
                    string secretKey = "5CKDVhJGqpZ5b2gz";

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
                    Sbiepay sbi = new Sbiepay();

                    var result = sbi.doubleverificationregistrationAirpay(responseString);
                    System.Threading.Thread.Sleep(10);
                }

            }
            catch (SystemException ex)
            {
                //CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "DoubleVerificationURl admission Paymentgateway Pushresponse  url hit on Controller error");
                return "";
            }
            return "";
        }
        public ActionResult DoubleVerificationURlExamEasebuzz(int id = 0, string trxid = "", string password = "", string mid = "")
        {
            try
            {

                string ApplicationNO = ClsLanguage.GetCookies("NBApplicationNo");
                doubleverificationgetstudent obj211 = new doubleverificationgetstudent();
                Sbiepay sb = new Sbiepay();
                Recruitment re = new Recruitment();
                DataTable dt = new DataTable();
                UserLogin objlogin = new UserLogin();
                dt = null;
                var obj = objlogin.RegistrationVerification300EaseBuss(obj211, ApplicationNO);
                int a = 0;
                //  return View();
                var responseString = "";
                Sbiepay sbi = new Sbiepay();
                foreach (var item in obj)
                {
                    // mid = "";
                    var key = "I2DPETI0FE";
                    var salt = "F6LU2RUJ1C";
                    string env = "prod";
                    trxid = item.clienttrxid;

                    var emailclient = item.Email;
                    var phoneclient = item.MobileNo;
                    string amount = item.Amount;
                    amount = Convert.ToDecimal(amount).ToString("0.0");
                    var applicationno = item.applicationno;

                    //var textstr = mid + "|" + trxid + "|" + f1 + "|" + emailclient + "|" + phoneclient + "|" + salt;
                    Sbiepay ss = new Sbiepay();
                    //var sha = ss.Easebuzz_Generatehash512("textstr");

                    System.Collections.Hashtable data = new System.Collections.Hashtable();
                    data.Add("key", key);
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
                    postData += "&key=" + key;
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

        [Route("update-Password")]
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

        public ActionResult SelectPaymentGetway()
        {
            return Redirect("/admission/payment-gateway");
        }

        [Route("admission/payment-gateway", Name = "PaymentGateway")]
        public ActionResult SelectGetwayAdmissionFee()
        {
            return View();
        }




        public ActionResult PGGateway_EaseBuzz()
        {
            try
            {
                Sbiepay sbi = new Sbiepay();
                StudentLogin stu = new StudentLogin();
                FeesSubmit stlogin = new FeesSubmit();
                string ApplicationID = ClsLanguage.GetCookies("NBApplicationNo");
                Login lo = stu.BasicDetail(ApplicationID);
                decimal Amount1 = 1;
                string MerchantCustomerID1 = "1";
                var result = stlogin.FeessubEncryt(Convert.ToInt32(EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENNBStID"))));
                if (result != null)
                {
                    Amount1 = Convert.ToDecimal(result.Fees);
                    var obj = sbi.encriptDataEaseBuzz(Amount1, EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENBApplicationNo")), "studentPG/HomeUGCBCS/PGSucess", "studentPG/HomeUGCBCS/PGFailed");
                    ViewBag.txnid = obj.Etxnid;
                    ViewBag.Key = obj.EKey;
                    ViewBag.amount = Amount1.ToString();
                    ViewBag.firstname = lo.FirstName + " " + lo.LastName;
                    ViewBag.email = lo.Email;
                    ViewBag.phone = lo.MobileNo;
                    ViewBag.productinfo = obj.Eproductinfo;
                    ViewBag.surl = obj.Esurl;
                    ViewBag.furl = obj.Efurl;
                    ViewBag.udf1 = obj.Eudf1;
                    ViewBag.udf2 = obj.Eudf2;
                    ViewBag.udf3 = obj.Eudf3;
                    ViewBag.udf4 = obj.Eudf4;
                    ViewBag.udf5 = obj.Eudf5;
                    ViewBag.strForm = obj.EstrForm;
                    //ViewBag.strForm = obj.Esaltvalue;
                    ViewBag.hash_string = obj.Ehash_string;
                    ViewBag.easebuzz_action_url = obj.Eeasebuzz_action_url;
                }
            }
            catch (Exception ex)
            {
                CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "action PGGateway error", ClsLanguage.GetCookies("NBApplicationNo"));

                return View();
            }
            return View();
        }

        public ActionResult PGGateway_Airpay()
        {
            try
            {
                StudentLogin stu = new StudentLogin();
                string ApplicationID = ClsLanguage.GetCookies("NBApplicationNo");
                Login lo = stu.BasicDetail(ApplicationID);

                Sbiepay sbi = new Sbiepay();
                FeesSubmit stlogin = new FeesSubmit();
                decimal Amount1 = 1;
                string MerchantCustomerID1 = "1";
                var result = stlogin.FeessubEncryt(Convert.ToInt32(EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENNBStID"))));
                if (result != null)
                {
                    Amount1 = Convert.ToDecimal(result.Fees);
                    var obj = sbi.encriptDataAirpay(Amount1, EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENBApplicationNo")), "studentPG/HomeUGCBCS/PGSucess", "studentPG/HomeUGCBCS/PGFailed");

                    Commn_master com = new Commn_master();
                    AcademicSession ad = new AcademicSession();
                    ViewBag.orderid = obj.Oid;
                    ViewBag.buyerEmail = lo.Email;
                    ViewBag.buyerPhone = lo.MobileNo;
                    ViewBag.buyerFirstName = lo.FirstName;
                    ViewBag.buyerLastName = lo.LastName;
                    ViewBag.buyerPinCode = lo.CA_PinCode;
                    ViewBag.amount = Amount1.ToString();
                    ViewBag.chmod = "";
                    ViewBag.checksum = obj.checksum;
                    ViewBag.privatekey = obj.privatekey;
                    ViewBag.mercid = obj.merchantId;
                    ViewBag.kittype = "inline";
                    ViewBag.currency = "360";
                    ViewBag.isocurrency = "INR";
                    ViewBag.url = "https://intschoolpay.nowpay.co.in/pay/index.php";
                    ViewBag.success_url = obj.A_Success_url;
                    ViewBag.customvar = obj.customvar;
                }
            }
            catch (Exception ex)
            {
                CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "action PGGateway error", ClsLanguage.GetCookies("NBApplicationNo"));

                return View();
            }
            return View();
        }

        public ActionResult PGGateway_SafexPay()
        {
            try
            {
                StudentLogin stu = new StudentLogin();
                string ApplicationID = ClsLanguage.GetCookies("NBApplicationNo");
                Login lo = stu.BasicDetail(ApplicationID);

                Sbiepay sbi = new Sbiepay();
                FeesSubmit stlogin = new FeesSubmit();
                decimal Amount1 = 1;
                string MerchantCustomerID1 = "1";
                var result = stlogin.FeessubEncryt(Convert.ToInt32(EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENNBStID"))));
                if (result != null)
                {
                    Amount1 = Convert.ToDecimal(result.Fees);
                    var obj = sbi.encriptDataSafex(Amount1, EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENBApplicationNo")), "studentPG/HomeUGCBCS/PGSucess", "studentPG/HomeUGCBCS/PGFailed");

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
                CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "action PGGateway error", ClsLanguage.GetCookies("NBApplicationNo"));

                return View();
            }
            return View();
        }




        public ActionResult ResponseAirpay()
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

        public ActionResult ResponseSafexPay()
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
        public ActionResult PGSucess_AirPay()
        {

            string paramInfo = "";


            if (Request.Form.Count > 0)
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
                string examType = "";
                string Requestdata = sTemp;
                string dRequestdata = strCRC;
                string PGstatus = MESSAGE;
                string Sid = "";
                string Sessionid = "";
                var other_detail = CUSTOMVAR.Split(',');
                ApplicationNo = other_detail[0];
                examType = other_detail[1];
                courseyearid = other_detail[2];
                clienttrxid = other_detail[4];
                Sid = other_detail[5];
                Sessionid = other_detail[6];

                if (error == "")
                {
                    if (TRANSACTIONSTATUS == "200")
                    {
                        Sbiepay sbi = new Sbiepay();
                        var result = sbi.pgsucessdecryptAirpay(Convert.ToInt32(Sid), banktrxid, clienttrxid, amount1, feeamount, gst, commission, paymode, banktxndate, Reason, apitxnid, ApplicationNo, courseyearid, examType, Requestdata, dRequestdata, PGstatus);
                        return RedirectToAction("ResponseAirpay");
                    }
                    else
                    {
                        Sbiepay sbi = new Sbiepay();
                        var result = sbi.pgsucessdecryptAirpay(Convert.ToInt32(Sid), banktrxid, clienttrxid, amount1, feeamount, gst, commission, paymode, banktxndate, Reason, apitxnid, ApplicationNo, courseyearid, examType, Requestdata, dRequestdata, PGstatus);
                        return RedirectToAction("ResponseAirpay");
                    }
                }
                else
                {
                    PGstatus = "ERROR";
                    Sbiepay sbi = new Sbiepay();
                    var result = sbi.pgsucessdecryptAirpay(Convert.ToInt32(Sid), banktrxid, clienttrxid, amount1, feeamount, gst, commission, paymode, banktxndate, Reason, apitxnid, ApplicationNo, courseyearid, examType, Requestdata, dRequestdata, PGstatus);
                    return RedirectToAction("ResponseAirpay");
                }
            }
            return RedirectToAction("FeesSubmit");
            return View();
        }

        public ActionResult PGSucess_EaseBuzz()
        {
            //return RedirectToAction("Index", "HomeUGCBCS");
            string paramInfo = "";
            string[] merc_hash_vars_seq;
            string merc_hash_string = string.Empty;
            string merc_hash = string.Empty;
            string order_id = string.Empty;
            string hash_seq = "key|txnid|amount|productinfo|firstname|email|udf1|udf2|udf3|udf4|udf5|udf6|udf7|udf8|udf9|udf10";
            merc_hash_vars_seq = hash_seq.Split('|');
            Array.Reverse(merc_hash_vars_seq);
            merc_hash_string = System.Configuration.ConfigurationSettings.AppSettings["salt"] + "|" + Request.Form["status"];
            foreach (string merc_hash_var in merc_hash_vars_seq)
            {
                merc_hash_string += "|";
                merc_hash_string = merc_hash_string + (Request.Form[merc_hash_var] != null ? Request.Form[merc_hash_var] : "");
            }

            merc_hash = Easebuzz_Generatehash512(merc_hash_string).ToLower();

            if (merc_hash != Request.Form["hash"])
            {

            }
            else
            {
                order_id = Request.Form["txnid"];

                //Response.Write("value matched");
                if (Request.Form["status"] == "success")
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
                    string TRANSACTIONSTATUS = Request.Form["status"];
                    string MESSAGE = Request.Form["status"];
                    string TRANSACTIONID = Request.Form["bank_ref_num"];
                    string AMOUNT = Request.Form["amount"];
                    string ap_SecureHash = Request.Form["easepayid"];
                    string CHMOD = "Web";
                    DateTime now = DateTime.Now;
                    string MID = "";
                    string username = "";
                    string banktrxid = Request.Form["bank_ref_num"];
                    string clienttrxid = "";
                    string amount1 = AMOUNT;
                    string feeamount = "0";
                    string gst = "0";
                    string error = Request.Form["error"];
                    string commission = Request.Form["deduction_percentage"];
                    string paymode = Request.Form["card_type"];
                    string banktxndate = Request.Form["addedon"];
                    string Reason = error;
                    string apitxnid = Request.Form["easepayid"];
                    string ApplicationNo = "";
                    string courseyearid = "";
                    string examType = "";
                    string Requestdata = merc_hash_string;
                    string dRequestdata = merc_hash;
                    string PGstatus = MESSAGE;
                    string Sid = "0";
                    string Sessionid = "";
                    ApplicationNo = Request.Form["udf1"];
                    examType = Request.Form["udf2"];
                    courseyearid = Request.Form["udf3"];
                    clienttrxid = Request.Form["udf4"];
                    //Sid = Request.Form["udf5"];
                    if (TRANSACTIONSTATUS.ToLower() == "success")
                    {
                        Sbiepay sbi = new Sbiepay();
                        var result = sbi.pgsucessdecryptEaseBuzz(Convert.ToInt32(Sid), banktrxid, clienttrxid, amount1, feeamount, gst, commission, paymode, banktxndate, Reason, apitxnid, ApplicationNo, courseyearid, examType, Requestdata, dRequestdata, TRANSACTIONSTATUS);
                        return RedirectToAction("ResponseEaseBuzz");
                    }
                    else
                    {
                        Sbiepay sbi = new Sbiepay();
                        var result = sbi.pgsucessdecryptEaseBuzz(Convert.ToInt32(Sid), banktrxid, clienttrxid, amount1, feeamount, gst, commission, paymode, banktxndate, Reason, apitxnid, ApplicationNo, courseyearid, examType, Requestdata, dRequestdata, TRANSACTIONSTATUS);
                        return RedirectToAction("ResponseEaseBuzz");
                    }
                }
                catch (Exception ex)
                {
                    CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, " Air Pay Payment PaymentGateway suucess url hit on Controller error for Exam Fees Submit", ClsLanguage.GetCookies("NBApplicationNo"));
                }
            }
            return RedirectToAction("FeesSubmit");
            return View();
        }

        public ActionResult SafexPayPGSucessExam()
        {
            string responseapi = Request.Params.ToString();
            //try
            //{
            //CommonMethod.TraceLogWritetoNotepad("obj1.objExamFrom4", "Exam//SafexPayPGSucessExam1", "obj1.objExamFrom", "Id");
            if (Request.Params.Count > 0)
            {
                //CommonMethod.TraceLogWritetoNotepad("obj1.objExamFrom4", "Exam//SafexPayPGSucessExam2", "obj1.objExamFrom", "Id");
                string enc_txn_response = (!String.IsNullOrEmpty(Request.Params["txn_response"])) ? Request.Params["txn_response"] : string.Empty;
                string enc_pg_details = (!String.IsNullOrEmpty(Request.Params["pg_details"])) ? Request.Params["pg_details"] : string.Empty;
                string enc_fraud_details = (!String.IsNullOrEmpty(Request.Params["fraud_details"])) ? Request.Params["fraud_details"] : string.Empty;
                string enc_other_details = (!String.IsNullOrEmpty(Request.Params["other_details"])) ? Request.Params["other_details"] : string.Empty;
                MyCryptoClass aes = new MyCryptoClass();
                string txn_response = aes.decrypt(enc_txn_response);
                string decodedUrl = HttpUtility.UrlDecode(txn_response);
                //string[] txn_hash = txn_response.Split('|');
                string[] txn_hash = decodedUrl.Split('~');
                string txn_res_hash = txn_hash[1];
                string txn_res_actual = txn_hash[0] + "" + txn_hash[2];
                //string txn_response = aes.decrypt(enc_txn_response);
                string[] txn_response_arr = txn_res_actual.Split('|');
                string Hash = txn_response_arr[10] + "~" + txn_response_arr[1] + "~" + txn_response_arr[2] + "~" + txn_response_arr[3] + "~" + txn_response_arr[4] + "~" + txn_response_arr[5] + "~" + txn_response_arr[8];
                //string Hash = txn_response_arr[10] + "|" + txn_response_arr[1] + "|" + txn_response_arr[2] + "|" + txn_response_arr[3] + "|" + txn_response_arr[4] + "|" + txn_response_arr[5] + "|" + txn_response_arr[8];
                string hashing = aes.ComputeSha256Hash(Hash);
                string encHash = aes.encrypt(hashing);
                string genuine = "genuine";
                string fake = "fake";
                string protocol = "";
                if (txn_res_hash == encHash)
                {
                    protocol = genuine;
                }
                else
                {
                    protocol = fake;
                }
                string ag_id = (!String.IsNullOrEmpty(txn_response_arr[0])) ? txn_response_arr[0] : string.Empty;
                string me_id = (!String.IsNullOrEmpty(txn_response_arr[1])) ? txn_response_arr[1] : string.Empty;
                string order_no = (!String.IsNullOrEmpty(txn_response_arr[2])) ? txn_response_arr[2] : string.Empty;
                string amount = (!String.IsNullOrEmpty(txn_response_arr[3])) ? txn_response_arr[3] : string.Empty;
                string country = (!String.IsNullOrEmpty(txn_response_arr[4])) ? txn_response_arr[4] : string.Empty;
                string currency = (!String.IsNullOrEmpty(txn_response_arr[5])) ? txn_response_arr[5] : string.Empty;
                string txn_date = (!String.IsNullOrEmpty(txn_response_arr[6])) ? txn_response_arr[6] : string.Empty;
                string txn_time = (!String.IsNullOrEmpty(txn_response_arr[7])) ? txn_response_arr[7] : string.Empty;
                string ag_ref = (!String.IsNullOrEmpty(txn_response_arr[8])) ? txn_response_arr[8] : string.Empty;
                string pg_ref = (!String.IsNullOrEmpty(txn_response_arr[9])) ? txn_response_arr[9] : string.Empty;
                string status = (!String.IsNullOrEmpty(txn_response_arr[10])) ? txn_response_arr[10] : string.Empty;
                //string txn_type = (!String.IsNullOrEmpty(txn_response_arr[11])) ? txn_response_arr[11] : string.Empty;
                string res_code = (!String.IsNullOrEmpty(txn_response_arr[11])) ? txn_response_arr[11] : string.Empty;
                string res_message = (!String.IsNullOrEmpty(txn_response_arr[12])) ? txn_response_arr[12] : string.Empty;
                string pg_details = aes.decrypt(enc_pg_details);
                string[] pg_details_arr = pg_details.Split('|');
                string pg_id = (!String.IsNullOrEmpty(pg_details_arr[0])) ? pg_details_arr[0] : string.Empty;
                string pg_name = (!String.IsNullOrEmpty(pg_details_arr[1])) ? pg_details_arr[1] : string.Empty;
                string paymode = (!String.IsNullOrEmpty(pg_details_arr[2])) ? pg_details_arr[2] : string.Empty;
                string emi_months = (!String.IsNullOrEmpty(pg_details_arr[3])) ? pg_details_arr[3] : string.Empty;
                string fraud_details = aes.decrypt(enc_fraud_details);
                string[] fraud_details_arr = fraud_details.Split('|');
                string fraud_action = (!String.IsNullOrEmpty(fraud_details_arr[0])) ? fraud_details_arr[0] : string.Empty;
                string fraud_message = (!String.IsNullOrEmpty(fraud_details_arr[1])) ? fraud_details_arr[1] : string.Empty;
                string score = (!String.IsNullOrEmpty(fraud_details_arr[2])) ? fraud_details_arr[2] : string.Empty;
                string other_details = aes.decrypt(enc_other_details);
                string[] other_details_arr = other_details.Split('|');
                string udf_1 = (!String.IsNullOrEmpty(other_details_arr[0])) ? other_details_arr[0] : string.Empty;
                string udf_2 = (!String.IsNullOrEmpty(other_details_arr[1])) ? other_details_arr[1] : string.Empty;
                string udf_3 = (!String.IsNullOrEmpty(other_details_arr[2])) ? other_details_arr[2] : string.Empty;
                string udf_4 = (!String.IsNullOrEmpty(other_details_arr[3])) ? other_details_arr[3] : string.Empty;
                string udf_5 = (!String.IsNullOrEmpty(other_details_arr[4])) ? other_details_arr[4] : string.Empty;
                string MID = "";
                string username = "";
                //comparing Secure Hash with Hash sent by Airpay
                string banktrxid = pg_ref;
                string clienttrxid = order_no;
                string amount1 = amount;
                string feeamount = amount;
                string gst = "0";
                string commission = "0";
                string banktxndate = txn_date + " " + txn_time;
                string Reason = res_message;
                string apitxnid = ag_ref;
                string ApplicationNo = udf_1;
                string courseyearid = udf_3;
                string examType = udf_2;
                string Requestdata = enc_txn_response;
                string dRequestdata = "";
                string PGstatus = status;
                string Sid = udf_4;
                string Sessionid = "";
                if (status == "Successful")
                {
                    Sbiepay sbi = new Sbiepay();
                    PGstatus = "Success";
                    status = "Success";
                    //var result = sbi.SafexPaypgsucessdecrypt(Convert.ToInt32(Sid), banktrxid, clienttrxid, amount1, feeamount, gst, commission, paymode, banktxndate, Reason, apitxnid, ApplicationNo, courseyearid, examType, Requestdata, dRequestdata, PGstatus);                 
                    var result = sbi.pgsucessdecryptAirpay(Convert.ToInt32(Sid), banktrxid, clienttrxid, amount1, feeamount, gst, commission, paymode, banktxndate, Reason, apitxnid, ApplicationNo, courseyearid, examType, Requestdata, dRequestdata, PGstatus);
                    return RedirectToAction("ResponseSafexPay");
                }
                else
                {
                    SbiPayExam sbi = new SbiPayExam();
                    var result = sbi.SafexPaypgsucessdecrypt(Convert.ToInt32(Sid), banktrxid, clienttrxid, amount1, feeamount, gst, commission, paymode, banktxndate, Reason, apitxnid, ApplicationNo, courseyearid, examType, Requestdata, dRequestdata, PGstatus);
                    return RedirectToAction("ResponseSafexPay");
                }
            }

            return View();
        }



        public string Easebuzz_Generatehash512(string text)
        {

            byte[] message = Encoding.UTF8.GetBytes(text);

            UnicodeEncoding UE = new UnicodeEncoding();
            byte[] hashValue;
            SHA512Managed hashString = new SHA512Managed();
            string hex = "";
            hashValue = hashString.ComputeHash(message);
            foreach (byte x in hashValue)
            {
                hex += String.Format("{0:x2}", x);
            }
            return hex;

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
                    var obj = sbi.encriptData(Amount1, EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENBApplicationNo")), "studentPG/HomeUGCBCS/PGSucess", "studentPG/HomeUGCBCS/PGFailed");

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
        [VerifyUrlFilterAdminAttributePG]
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
        [VerifyUrlFilterAdminAttributePG]
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
            // ViewBag.check_admissionopen = com.check_admission_open(ad.GetAcademiccurrentSession().ID, Convert.ToInt32(CommonSetting.Commonid.Educationtype));
            ViewBag.addmissionExtenddate = com.check_admission_open(ad.GetAcademiccurrentSession().ID, lo.EducationType);
            if (lo.EducationType == Convert.ToInt32(CommonSetting.Commonid.EducationtypePG))
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
                    //TempData["msgfees"] = "FeesTemperary Hold !!!"; //Hold By JC
                    //return RedirectToAction("FeesSubmit");
                    return RedirectToAction("SelectPaymentGetway");
                    //return RedirectToAction("PGGateway"); 
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
                CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "PG Student Generate ID Card method", eID);
                return RedirectToAction("GenerateIDCard");
            }
        }


        #region College Admission Fee work
        //[Route("admission")]
        //public ActionResult AdmissionFeeSubmit()
        //{

        //    BL_student_formcomplete bl = new BL_student_formcomplete();
        //    AcademicSession ac = new AcademicSession();
        //    int sessionid = ac.GetAcademiccurrentSession().ID;
        //    var result = bl.CheckPasswordBasicDetail(ClsLanguage.GetCookies("NBApplicationNo"), sessionid.ToString());
        //    if (result.isfeesubmitt == false)
        //    {
        //        return Redirect("change-password");
        //    }
        //    else if (result.is_basic_complete == false)
        //    {
        //        return Redirect("profile");
        //    }
        //    string viewPath = "~/Areas/UGCBCS/Views/HomeUGCBCS/AdmissionFeeSubmit.cshtml";

        //    BL_PrintApplication obrecrei = new BL_PrintApplication();

        //    var objrecritiny = obrecrei.CheckStudentAdmission(ac.GetAcademiccurrentSession().ID);
        //    //if (objrecritiny.Status == true)
        //    //{
        //    //    return RedirectToAction("Index", "HomeL");
        //    //}
        //    AdmissionFeesSubmit stlogin = new AdmissionFeesSubmit();
        //    AdmissionFeesSubmit obj = new AdmissionFeesSubmit();
        //    List<AdmissionFeesSubmit> feestruckture = new List<AdmissionFeesSubmit>();
        //    ViewBag.IsSubmit = obj.IsFeeSubmit;

        //    StudentAdmissionQualification stad = new StudentAdmissionQualification();
        //    List<SubjectList> subjectList1 = new List<SubjectList>()
        //        {
        //            new SubjectList { Id = 1, SubjectName = "Test" },
        //            new SubjectList { Id = 2, SubjectName = "Test1" }
        //        };

        //    ViewBag.Subject1 = subjectList1;


        //    int courseId = 33;
        //    int semesterId = 56;

        //    var data = ac.GetOtherSubjects(courseId, semesterId);

        //    // Group by Heading (IMPORTANT)
        //    var grouped = data.GroupBy(x => x.HeadingName).ToList();

        //    ViewBag.SubjectGroups = grouped;


        //    Commn_master com = new Commn_master();
        //    // ViewBag.check_admissionopen = com.check_admission_open(ac.GetAcademiccurrentSession().ID, Convert.ToInt32(CommonSetting.Commonid.EducationtypePG));
        //    string Id = ClsLanguage.GetCookies("ENNBStID");

        //    string ApplicationID = ClsLanguage.GetCookies("NBApplicationNo");
        //    StudentLogin tblST = new StudentLogin();
        //    BL_PrintApplication ob = new BL_PrintApplication();
        //    sessionid = ac.GetAcademiccurrentSession().ID;
        //    var obj1 = tblST.BasicDetail(ApplicationID);
        //    var res1 = bl.CheckAdmission_details(sessionid);


        //    ViewBag.IsAdmisApplied = res1.IsApplied;

        //    //if (res1.IsApplied == false)
        //    //{
        //    //    return RedirectToAction("Index", "HomeUGCBCS");
        //    //}
        //    //if (res1.IsDocVerify == 0)
        //    //{
        //    //    return RedirectToAction("Index", "HomeUGCBCS");
        //    //}
        //    //if (res1.IsDocVerify == 2)
        //    //{
        //    //    return RedirectToAction("Index", "HomeUGCBCS");
        //    //}
        //    //if (obj1.IsFeeSubmit != 1)
        //    //{
        //    //    return RedirectToAction("Index", "HomeUGCBCS");
        //    //}

        //    if (obj1 != null)
        //    {
        //        int educationtype = obj1.EducationType;
        //        var dateextend = ob.CheckStudentAddmisionExtendDate(sessionid, educationtype);
        //        ViewBag.addmissionExtenddate = dateextend.Status;
        //        ViewBag.addmissionExtenddateValue = dateextend.ExtendDate;
        //        var datestart = ob.CheckStudentAddmisionStartDate(sessionid, educationtype);
        //        ViewBag.addmissionStartdate = datestart.Status;
        //        ViewBag.addmissionStartdateValue = datestart.startdate;

        //    }
        //    var objst = ob.CheckStudentAdmission(sessionid);
        //    if (objst.Status == true)
        //    {
        //        ViewBag.Status = objst.Status;
        //        ViewBag.Course = objst.CourseName;
        //        ViewBag.College = objst.CollegeName;
        //        ViewBag.Stream = objst.StreamName;
        //        ViewBag.CastCategory = objst.CastCategory;
        //    }
        //    else
        //    {
        //        ViewBag.Status = false;
        //        ViewBag.Course = "";
        //        ViewBag.College = "";
        //        ViewBag.CastCategory = "";
        //    }

        //    if (Id != "0" && Id.Length > 0)
        //    {
        //        string enID = EncriptDecript.Decrypt(Id);
        //        int eID = 0;
        //        if (enID != "")
        //        {
        //            eID = Convert.ToInt32(enID);
        //        }
        //        if (eID > 0)
        //        {

        //            if (obj1.StudentYear == Convert.ToInt32(CommonSetting.CourseYearID.ma1sem) || obj1.StudentYear == Convert.ToInt32(CommonSetting.CourseYearID.mcom1sem) || obj1.StudentYear == Convert.ToInt32(CommonSetting.CourseYearID.msc1sem)) // for Ist year// for Ist year
        //            {
        //                obj = stlogin.FeesDetails(eID);
        //                ViewBag.isfeesubmitt = res1.isfeesubmitt;
        //                ViewBag.IsfeesubmitDate = res1.IsfeesubmitDate;
        //            }

        //            else
        //            {
        //                obj = stlogin.FeesDetailsotheryear(eID); // for other year
        //                ViewBag.isfeesubmitt = res1.isfeesubmitt2;
        //                ViewBag.IsfeesubmitDate = res1.IsfeesubmitDate2;
        //            }
        //            feestruckture = stlogin.FeesDetailsstructure(obj.Collegeid, obj1.CourseCategory, sessionid, obj1.CastCategory, obj.streamcategoryid, obj1.StudentYear, obj1.incomecertificate_iseligible, obj1.Gender);
        //            //var tuple = new Tuple<AdmissionFeesSubmit, List<AdmissionFeesSubmit>, Login>(obj, feestruckture, obj1);
        //            var tuple = new Tuple<AdmissionFeesSubmit, List<AdmissionFeesSubmit>, Login, List<OtherSubjectModel>>(obj, feestruckture, obj1, data);

        //            return View(viewPath, tuple);
        //        }
        //        else
        //        {
        //            //var tuple1 = new Tuple<AdmissionFeesSubmit, List<AdmissionFeesSubmit>, Login>(obj, feestruckture, obj1);
        //            //var tuple1 = new Tuple<AdmissionFeesSubmit, List<AdmissionFeesSubmit>, Login>(obj, feestruckture, obj1);

        //            var tuple1 = new Tuple<AdmissionFeesSubmit, List<AdmissionFeesSubmit>, Login, List<OtherSubjectModel>>(obj, feestruckture, obj1, data);


        //            return View(viewPath, tuple1);
        //        }
        //    }
        //    //var tuple2 = new Tuple<AdmissionFeesSubmit, List<AdmissionFeesSubmit>, Login>(obj, feestruckture, obj1);

        //    //var tuple1 = new Tuple<AdmissionFeesSubmit, List<AdmissionFeesSubmit>, Login>(obj, feestruckture, obj1);
        //    var tuple2 = new Tuple<AdmissionFeesSubmit, List<AdmissionFeesSubmit>, Login, List<OtherSubjectModel>>(obj, feestruckture, obj1, data);
        //    return View(viewPath, tuple2);



        //}

        [Route("admission")]
        public ActionResult AdmissionFeeSubmit()
        {


            BL_student_formcomplete bl = new BL_student_formcomplete();
            AcademicSession ac = new AcademicSession();
            int sessionid = ac.GetAcademiccurrentSession().ID;
            var result = bl.CheckPasswordBasicDetail(ClsLanguage.GetCookies("NBApplicationNo"), sessionid.ToString());
            if (result.isfeesubmitt == false)
            {
                return Redirect("change-password");
            }



            else if (result.is_basic_complete == false)
            {
                return Redirect("profile");
            }

            if (result.IsCurrent >0)
            {
                ViewBag.IsCurrent = result.IsCurrent;
            }

            string viewPath = "~/Areas/UGCBCS/Views/HomeUGCBCS/AdmissionFeeSubmit.cshtml";
            BL_PrintApplication obrecrei = new BL_PrintApplication();

            var objrecritiny = obrecrei.CheckStudentAdmission(ac.GetAcademiccurrentSession().ID);

            AdmissionFeesSubmit stlogin = new AdmissionFeesSubmit();
            AdmissionFeesSubmit obj = new AdmissionFeesSubmit();
            List<AdmissionFeesSubmit> feestruckture = new List<AdmissionFeesSubmit>();
            ViewBag.IsSubmit = obj.IsFeeSubmit;

            Commn_master com = new Commn_master();
            // ViewBag.check_admissionopen = com.check_admission_open(ac.GetAcademiccurrentSession().ID, Convert.ToInt32(CommonSetting.Commonid.EducationtypePG));
            string Id = ClsLanguage.GetCookies("ENNBStID");

            string ApplicationID = ClsLanguage.GetCookies("NBApplicationNo");
            StudentLogin tblST = new StudentLogin();
            BL_PrintApplication ob = new BL_PrintApplication();
            sessionid = ac.GetAcademiccurrentSession().ID;
            var obj1 = tblST.BasicDetail(ApplicationID);
            var res1 = bl.CheckAdmission_details(sessionid);

            var data = bl.GetOtherSubjects(obj1.CourseCategory, obj1.StudentYear);

            // Group by Heading (IMPORTANT)
            var grouped = data.GroupBy(x => x.HeadingName).ToList();

            ViewBag.SubjectGroups = grouped;

            var savedSubjects = bl.GetSavedSubjects(obj1.Id, obj1.StudentYear);
            ViewBag.SavedSubjects = savedSubjects;


            ViewBag.IsAdmisApplied = res1.IsApplied;

            //if (res1.IsApplied == false)
            //{
            //    return RedirectToAction("Index", "HomeUGCBCS");
            //}
            //if (res1.IsDocVerify == 0)
            //{
            //    return RedirectToAction("Index", "HomeUGCBCS");
            //}
            //if (res1.IsDocVerify == 2)
            //{
            //    return RedirectToAction("Index", "HomeUGCBCS");
            //}
            //if (obj1.IsFeeSubmit != 1)
            //{
            //    return RedirectToAction("Index", "HomeUGCBCS");
            //}

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

                    if (obj1.StudentYear == Convert.ToInt32(CommonSetting.CourseYearID.ma1sem) || obj1.StudentYear == Convert.ToInt32(CommonSetting.CourseYearID.mcom1sem) || obj1.StudentYear == Convert.ToInt32(CommonSetting.CourseYearID.msc1sem)) // for Ist year// for Ist year
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
                    feestruckture = stlogin.FeesDetailsstructure(obj.Collegeid, obj1.CourseCategory, sessionid, obj1.CastCategory, obj.streamcategoryid, obj1.StudentYear, obj1.incomecertificate_iseligible, obj1.Gender);
                    var tuple =  new Tuple<AdmissionFeesSubmit, List<AdmissionFeesSubmit>, Login, List<OtherSubjectModel>>(obj, feestruckture, obj1, data);


                    return View(viewPath, tuple);
                }
                else
                {
                    var tuple1 = new Tuple<AdmissionFeesSubmit, List<AdmissionFeesSubmit>, Login, List<OtherSubjectModel>>(obj, feestruckture, obj1, data);
                    return View(viewPath, tuple1);
                }
            }
            var tuple2 = new Tuple<AdmissionFeesSubmit, List<AdmissionFeesSubmit>, Login, List<OtherSubjectModel>>(obj, feestruckture, obj1, data);
            return View(viewPath, tuple2);
        }

        [HttpPost]
        public ActionResult AdmissionFeeSubmit(int id11 = 0, string applyadmissionform = "", string applyaffdavit = "", string paywith0Rs = "", List<StudentSubjectVM> SubjectList = null)
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
            //  ViewBag.check_admissionopen = com.check_admission_open(ac.GetAcademiccurrentSession().ID, Convert.ToInt32(CommonSetting.Commonid.EducationtypePG));
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
                TempData["Msg"] = "✅ Affidavit applied successfully!";
                return RedirectToAction("AdmissionFeeSubmit");
            }
            if (obj1 != null && Request["saveSubject"] == "1")
            {
                if (SubjectList != null && SubjectList.Count > 0)
                {
                     ApplicationID = EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENBApplicationNo"));
                    foreach (var item in SubjectList)
                    {
                        if (item.SubjectID > 0)
                        {
                            bl.SaveStudentSubjectChoice(
                                obj1.Id,
                                obj1.session,
                                item.SubjectID,
                                item.SubjectTypeID,
                                obj1.StudentYear
                            );
                        }
                    }
                    TempData["Msg"] = "✅ Subjects saved successfully!";
                }
                else
                {
                    TempData["msgerror"] = "❌ Please select at least one subject!";
                }


                return RedirectToAction("AdmissionFeeSubmit");
            }
            ViewBag.IsAdmisApplied = res1.IsApplied;
            ViewBag.IsAppliedDate = res1.IsAppliedDate;
            ViewBag.IsDocVerifyDate = res1.IsDocVerifyDate;

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
            if (obj1.StudentYear == Convert.ToInt32(CommonSetting.CourseYearID.ma1sem) || obj1.StudentYear == Convert.ToInt32(CommonSetting.CourseYearID.mcom1sem) || obj1.StudentYear == Convert.ToInt32(CommonSetting.CourseYearID.msc1sem)) // for Ist year
            {

                ViewBag.isfeesubmitt = res1.isfeesubmitt;
                ViewBag.IsfeesubmitDate = res1.IsfeesubmitDate;
            }
            else
            {
                ViewBag.isfeesubmitt = res1.isfeesubmitt2;
                ViewBag.IsfeesubmitDate = res1.IsfeesubmitDate2;

            }
            if (obj1.StudentYear == Convert.ToInt32(CommonSetting.CourseYearID.ma1sem) || obj1.StudentYear == Convert.ToInt32(CommonSetting.CourseYearID.mcom1sem) || obj1.StudentYear == Convert.ToInt32(CommonSetting.CourseYearID.msc1sem)) // for Ist year// for Ist year
            {

                obj = stlogin.FeesDetails(obj1.Id);

                ViewBag.isfeesubmitt = res1.isfeesubmitt;
                ViewBag.IsfeesubmitDate = res1.IsfeesubmitDate;
                stlogin.student_collegeform_apply(obj1.Id, obj1.session, obj1.CourseCategory, obj.Collegeid, obj.streamcategoryid, obj.studentyear, 0, 0);
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
                if (applyadmissionform == "applyadmissionform")
                {
                    stlogin.student_collegeform_apply(obj1.Id, obj1.session, obj1.CourseCategory, obj.Collegeid, obj.streamcategoryid, obj.studentyear, 0, 0);
                    TempData["Msg"] = "✅ Admission applied successfully!";
                    return RedirectToAction("AdmissionFeeSubmit");
                }
                if (obj.IsApplied == 0)
                {
                    return RedirectToAction("AdmissionFeeSubmit");
                }
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
                //}

                if (ViewBag.isfeesubmitt != true)
                {
                    if (ViewBag.addmissionExtenddate == true && ViewBag.addmissionStartdate == true)
                    {


                        //return RedirectToAction("PGAdmissionGateway", "HomeUGCBCS"); // for all remaining student

                        //Call comment By JC session 2021 fee compalsary for all student
                      
                            if (obj1.CastCategory == 5 || obj1.CastCategory == 6) // sc and st
                            {
                                if (paywith0Rs == "paywith0Rs")
                                {
                                    SbiepayAdmission sbi = new SbiepayAdmission();
                                    var obj15454 = sbi.Feeeadmission(EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENBApplicationNo")), 0);
                                    return RedirectToAction("AdmissionFeeSubmit");
                                }
                            }
                            else
                            {
                                if (obj1.Gender == 9) // all girls
                                {
                                    if (paywith0Rs == "paywith0Rs")
                                    {
                                        SbiepayAdmission sbi = new SbiepayAdmission();
                                        var obj15454 = sbi.Feeeadmission(EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENBApplicationNo")), 0);
                                        return RedirectToAction("AdmissionFeeSubmit");
                                    }
                                }
                                else
                                {
                                    return RedirectToAction("PGAdmissionGatewayAirPay", "HomeUGCBCS");
                                    //  return RedirectToAction("PGAdmissionGateway", "HomeUGCBCS"); // for all remaining student
                                }
                            }
                      
                       
                    }
                }


            }
            var tuple2 = new Tuple<AdmissionFeesSubmit, List<AdmissionFeesSubmit>, Login>(obj, feestruckture, obj1);
            return View(tuple2);
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
                if (obj1.StudentYear == Convert.ToInt32(CommonSetting.CourseYearID.ma1sem) || obj1.StudentYear == Convert.ToInt32(CommonSetting.CourseYearID.mcom1sem) || obj1.StudentYear == Convert.ToInt32(CommonSetting.CourseYearID.msc1sem)) // for Ist year// for Ist year
                {
                    result = stlogin.FeesDetails(Convert.ToInt32(EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENNBStID"))));
                    if (result.tbl_recruitment_IsApplied == 0)
                    {
                        return RedirectToAction("AdmissionFeeSubmit");
                    }
                    if (result.tbl_recruitment_IsDocVerify == 1)//comment By JC Tempary document verify Hold for testing Purpose
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
                feestruckture = stlogin.FeesDetailsstructure(result.Collegeid, result.coursecategoryid, result.sessionid, result.CastCategory, result.streamcategoryid, result.studentyear, result.incomecertificate_iseligible, obj1.Gender);
                if (result != null)
                {
                    BL_PrintApplication ob = new BL_PrintApplication();
                    AcademicSession ac = new AcademicSession();
                    int sessionid = ac.GetAcademiccurrentSession().ID;
                    int educationtype = Convert.ToInt32(CommonSetting.Commonid.EducationtypePG);
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
                        return RedirectToAction("AdmissionFeeSubmit", "HomeUGCBCS");
                    }
                    if (sessionid == 39)
                    {

                    }
                    //Amount1 = 1;
                    var obj = sbi.encriptDataadmission(Amount1, EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENBApplicationNo")), "studentPG/HomeUGCBCS/PGSucessadmission", "studentPG/HomeUGCBCS/PGFailedadmission", result.mid, result.mkey, result.Collegeid, obj1.StudentYear);
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

        public ActionResult PGAdmissionGatewayAirPay()
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
                if (obj1.StudentYear == Convert.ToInt32(CommonSetting.CourseYearID.ma1sem) || obj1.StudentYear == Convert.ToInt32(CommonSetting.CourseYearID.mcom1sem) || obj1.StudentYear == Convert.ToInt32(CommonSetting.CourseYearID.msc1sem)) // for Ist year// for Ist year
                {
                    result = stlogin.FeesDetails(Convert.ToInt32(EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENNBStID"))));
                    if (result.tbl_recruitment_IsApplied == 0)
                    {
                        return RedirectToAction("AdmissionFeeSubmit");
                    }
                    if (result.tbl_recruitment_IsDocVerify == 1)//comment By JC Tempary document verify Hold for testing Purpose
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



                feestruckture = stlogin.FeesDetailsstructure(result.Collegeid, result.coursecategoryid, result.sessionid, result.CastCategory, result.streamcategoryid, result.studentyear, result.incomecertificate_iseligible, obj1.Gender);
                if (result != null)
                {
                    BL_PrintApplication ob = new BL_PrintApplication();
                    AcademicSession ac = new AcademicSession();
                    int sessionid = ac.GetAcademiccurrentSession().ID;
                    int educationtype = Convert.ToInt32(CommonSetting.Commonid.EducationtypeUGCBCS);
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
                        return RedirectToAction("AdmissionFeeSubmit", "HomeUGCBCS");
                    }

                    if (ClsLanguage.GetCookies("NBApplicationNo") == "MU30554260" || ClsLanguage.GetCookies("NBApplicationNo") == "MU85189613" || ClsLanguage.GetCookies("NBApplicationNo") == "MU51278446")//Add For Testing Purpose
                    {
                        Amount1 = 2;
                    }

                    //Amount1 = 2; //Add by jitendra 
                    var obj = sbi.encriptDataadmissionAirPay(Amount1, EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENBApplicationNo")), "studentPG/HomeUGCBCS/AirPayPGSucessAdmission", "studentPG/HomeUGCBCS/PGFailedadmission", result.mid, result.mkey, result.Collegeid, obj1.StudentYear);
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
                    ViewBag.success_url = "https://portal.DemoUniversity.com/StudentPG/HomeUGCBCS/AirPayPGSucessAdmission";
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

        public ActionResult AirPayPGSucessAdmission()//New Add Airpay Response For Admission
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
        public ActionResult PGSucessadmission()
        {

            string paramInfo = "";
            if (Request.Form.Count > 0)
            {
                try
                {
                    AdmissionFeesSubmit fee = new AdmissionFeesSubmit();
                    var midget = fee.getmidviasid(Convert.ToInt32(EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENNBStID"))));

                    SbiepayAdmission sbi = new SbiepayAdmission();
                    var result = sbi.pgsucessdecryptadmission(Convert.ToInt32(EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENNBStID"))), midget.mkey);
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



        public ActionResult PGAdmissionGatewaySafex()
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
                if (obj1.StudentYear == Convert.ToInt32(CommonSetting.CourseYearID.ma1sem) || obj1.StudentYear == Convert.ToInt32(CommonSetting.CourseYearID.mcom1sem) || obj1.StudentYear == Convert.ToInt32(CommonSetting.CourseYearID.msc1sem)) // for Ist year// for Ist year
                {
                    result = stlogin.FeesDetails(Convert.ToInt32(EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENNBStID"))));
                    if (result.tbl_recruitment_IsApplied == 0)
                    {
                        return RedirectToAction("AdmissionFeeSubmit");
                    }
                    //if (result.tbl_recruitment_IsDocVerify == 1)//comment By JC Tempary document verify Hold for testing Purpose
                    //{

                    //}
                    //else
                    //{
                    //    return RedirectToAction("AdmissionFeeSubmit");
                    //}
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
                feestruckture = stlogin.FeesDetailsstructure(result.Collegeid, result.coursecategoryid, result.sessionid, result.CastCategory, result.streamcategoryid, result.studentyear, result.incomecertificate_iseligible, obj1.Gender);
                if (result != null)
                {
                    BL_PrintApplication ob = new BL_PrintApplication();
                    AcademicSession ac = new AcademicSession();
                    int sessionid = ac.GetAcademiccurrentSession().ID;
                    int educationtype = Convert.ToInt32(CommonSetting.Commonid.EducationtypePG);
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
                        return RedirectToAction("AdmissionFeeSubmit", "HomeUGCBCS");
                    }
                    if (sessionid == 39)
                    {

                    }
                    //Amount1 = 1;
                    var obj = sbi.encriptDataadmission(Amount1, EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENBApplicationNo")), "studentPG/HomeUGCBCS/PGSucessadmission", "studentPG/HomeUGCBCS/PGFailedadmission", result.mid, result.mkey, result.Collegeid, obj1.StudentYear);

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
        public ActionResult PGAdmissionGatewayEaseBuzz()
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
                if (obj1.StudentYear == Convert.ToInt32(CommonSetting.CourseYearID.ma1sem) || obj1.StudentYear == Convert.ToInt32(CommonSetting.CourseYearID.mcom1sem) || obj1.StudentYear == Convert.ToInt32(CommonSetting.CourseYearID.msc1sem)) // for Ist year// for Ist year
                {
                    result = stlogin.FeesDetails(Convert.ToInt32(EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENNBStID"))));
                    if (result.tbl_recruitment_IsApplied == 0)
                    {
                        return RedirectToAction("AdmissionFeeSubmit");
                    }
                    if (result.tbl_recruitment_IsDocVerify == 1)//comment By JC Tempary document verify Hold for testing Purpose
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
                feestruckture = stlogin.FeesDetailsstructure(result.Collegeid, result.coursecategoryid, result.sessionid, result.CastCategory, result.streamcategoryid, result.studentyear, result.incomecertificate_iseligible, obj1.Gender);
                if (result != null)
                {
                    BL_PrintApplication ob = new BL_PrintApplication();
                    AcademicSession ac = new AcademicSession();
                    int sessionid = ac.GetAcademiccurrentSession().ID;
                    int educationtype = Convert.ToInt32(CommonSetting.Commonid.EducationtypePG);
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
                        return RedirectToAction("AdmissionFeeSubmit", "HomeUGCBCS");
                    }
                    if (sessionid == 39)
                    {

                    }
                    //Amount1 = 1;
                    var obj = sbi.encriptDataEaseBuzz(Amount1, EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENBApplicationNo")), "studentPG/HomeUGCBCS/PGSucessadmission", "studentPG/HomeUGCBCS/PGFailedadmission", result.mid, result.mkey, result.Collegeid, obj1.StudentYear);
                    ViewBag.txnid = obj.Etxnid;
                    ViewBag.Key = obj.EKey;
                    ViewBag.amount = Amount1.ToString();
                    ViewBag.firstname = obj1.FirstName + " " + obj1.LastName;
                    ViewBag.email = obj1.Email;
                    ViewBag.phone = obj1.MobileNo;
                    ViewBag.productinfo = obj.Eproductinfo;
                    ViewBag.surl = obj.Esurl;
                    ViewBag.furl = obj.Efurl;
                    ViewBag.udf1 = obj.Eudf1;
                    ViewBag.udf2 = obj.Eudf2;
                    ViewBag.udf3 = obj.Eudf3;
                    ViewBag.udf4 = obj.Eudf4;
                    ViewBag.udf5 = obj.Eudf5;
                    ViewBag.strForm = obj.EstrForm;
                    //ViewBag.strForm = obj.Esaltvalue;
                    ViewBag.hash_string = obj.Ehash_string;
                    ViewBag.easebuzz_action_url = obj.Eeasebuzz_action_url;
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

        public ActionResult PGSucessadmissionEaseBuzz()
        {
            //return RedirectToAction("Index", "HomeUGCBCS");
            string paramInfo = "";
            string[] merc_hash_vars_seq;
            string merc_hash_string = string.Empty;
            string merc_hash = string.Empty;
            string order_id = string.Empty;
            string hash_seq = "key|txnid|amount|productinfo|firstname|email|udf1|udf2|udf3|udf4|udf5|udf6|udf7|udf8|udf9|udf10";
            merc_hash_vars_seq = hash_seq.Split('|');
            Array.Reverse(merc_hash_vars_seq);
            merc_hash_string = System.Configuration.ConfigurationSettings.AppSettings["salt"] + "|" + Request.Form["status"];
            foreach (string merc_hash_var in merc_hash_vars_seq)
            {
                merc_hash_string += "|";
                merc_hash_string = merc_hash_string + (Request.Form[merc_hash_var] != null ? Request.Form[merc_hash_var] : "");
            }

            merc_hash = Easebuzz_Generatehash512(merc_hash_string).ToLower();

            if (merc_hash != Request.Form["hash"])
            {

            }
            else
            {
                order_id = Request.Form["txnid"];

                //Response.Write("value matched");
                if (Request.Form["status"] == "success")
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
                    string TRANSACTIONSTATUS = Request.Form["status"];
                    string MESSAGE = Request.Form["status"];
                    string TRANSACTIONID = Request.Form["bank_ref_num"];
                    string AMOUNT = Request.Form["amount"];
                    string ap_SecureHash = Request.Form["easepayid"];
                    string CHMOD = "Web";
                    DateTime now = DateTime.Now;
                    string MID = "";
                    string username = "";
                    string banktrxid = Request.Form["bank_ref_num"];
                    string clienttrxid = "";
                    string amount1 = AMOUNT;
                    string feeamount = "0";
                    string gst = "0";
                    string error = Request.Form["error"];
                    string commission = Request.Form["deduction_percentage"];
                    string paymode = Request.Form["card_type"];
                    string banktxndate = Request.Form["addedon"];
                    string Reason = error;
                    string apitxnid = Request.Form["easepayid"];
                    string ApplicationNo = "";
                    string courseyearid = "";
                    string examType = "";
                    string Requestdata = merc_hash_string;
                    string dRequestdata = merc_hash;
                    string PGstatus = MESSAGE;
                    string Sid = "0";
                    string Sessionid = "";
                    string AdmissionType = "";
                    ApplicationNo = Request.Form["udf1"];
                    examType = Request.Form["udf2"];
                    AdmissionType = Request.Form["udf3"];
                    Sid = Request.Form["udf4"];
                    Sessionid = Request.Form["udf5"];
                    //Sid = Request.Form["udf5"];
                    //if (TRANSACTIONSTATUS.ToLower() == "success")
                    //{
                    //    SbiepayAdmission sbi = new SbiepayAdmission();
                    //    var result = sbi.AirPaypgsucessdecrypt(Convert.ToInt32(Sid), banktrxid, clienttrxid, amount1, feeamount, gst, commission, paymode, banktxndate, Reason, apitxnid, ApplicationNo, courseyearid, AdmissionType, Requestdata, dRequestdata, PGstatus, Sessionid);

                    //    var result = sbi.AirPaypgsucessdecrypt(Convert.ToInt32(Sid), banktrxid, clienttrxid, amount1, feeamount, gst, commission, paymode, banktxndate, Reason, apitxnid, ApplicationNo, courseyearid, examType, Requestdata, dRequestdata, TRANSACTIONSTATUS);
                    //    return RedirectToAction("ResponseEaseBuzz");
                    //}
                    //else
                    //{
                    SbiepayAdmission sbi = new SbiepayAdmission();
                    var result = sbi.AirPaypgsucessdecrypt(Convert.ToInt32(Sid), banktrxid, clienttrxid, amount1, feeamount, gst, commission, paymode, banktxndate, Reason, apitxnid, ApplicationNo, courseyearid, AdmissionType, Requestdata, dRequestdata, PGstatus, Sessionid);
                    return RedirectToAction("ResponseAdmission");
                    //}
                }
                catch (Exception ex)
                {
                    CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, " Air Pay Payment PaymentGateway suucess url hit on Controller error for Exam Fees Submit", ClsLanguage.GetCookies("NBApplicationNo"));
                }
            }
            return RedirectToAction("FeesSubmit");
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
                    SbiepayAdmission sbi = new SbiepayAdmission();
                    var result = sbi.pgfaileddecryptadmission(Convert.ToInt32(EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENNBStID"))), midget.mkey);
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
                if (objl22.StudentYear == Convert.ToInt32(CommonSetting.CourseYearID.ma1sem) || objl22.StudentYear == Convert.ToInt32(CommonSetting.CourseYearID.mcom1sem) || objl22.StudentYear == Convert.ToInt32(CommonSetting.CourseYearID.msc1sem)) // for Ist year
                {
                    obj = stlogin.FeesDetails(Convert.ToInt32(EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENNBStID"))));
                }
                else
                {
                    // for other year
                    obj = stlogin.FeesDetailsotheryear(Convert.ToInt32(EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENNBStID"))));
                }


                feestruckture = stlogin.FeesDetailsstructure(obj.Collegeid, obj.coursecategoryid, obj.sessionid, obj.CastCategory, obj.streamcategoryid, obj.studentyear, obj.incomecertificate_iseligible, objl22.Gender);
                var tuple = new Tuple<AdmissionFeesSubmit, List<AdmissionFeesSubmit>>(obj, feestruckture);
                var obj1 = PritApp.GetAppLicationDataAdmin(objl22.Id);
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
        #endregion
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
                return RedirectToAction("SelectSubject", "HomeUGCBCS");
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
                    ViewBag.subsidiary1 = stream.getcollegesubjects(objl.Id, 11, collegeid, subjectid);
                    ViewBag.subcomposition1 = stream.getcollegesubjects(objl.Id, 13, collegeid);
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
            ViewBag.check_admissionopen = false;// com.check_admission_open(ac.GetAcademiccurrentSession().ID, Convert.ToInt32(CommonSetting.Commonid.EducationtypePG));
            BL_PrintApplication ob123 = new BL_PrintApplication();
            int educationtype = 12;
            var dateextend = ob123.CheckStudentAddmisionExtendDate(objl.session, educationtype);
            ViewBag.addmissionExtenddate = dateextend.Status;
            ViewBag.addmissionExtenddateValue = dateextend.ExtendDate;
            var datestart = ob123.CheckStudentAddmisionStartDate(objl.session, educationtype);
            ViewBag.addmissionStartdate = datestart.Status;
            ViewBag.addmissionStartdateValue = datestart.startdate;
            if (dateextend.Status == true && datestart.Status == true)
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
                if (countlist.Count == 0)
                {
                    TempData["selectsubmsg"] = "Please First Upload your Qualification!!";
                    return View(tuple);
                }
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

        public JsonResult savest_Susidiarychoicesubject_spot(string Subsidiary1 = "", string Susidiary2 = "", string Compulsory1 = "", string Compulsory2 = "")
        {
            return Json(new { data = new Student_Admission_Choicesubject(), success = true });
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
                int subsidiary1 = (Subsidiary1 != "" ? Convert.ToInt32(Subsidiary1) : 0);
                int susidiary2 = (Susidiary2 != "" ? Convert.ToInt32(Susidiary2) : 0);
                int compulsory1 = (Compulsory1 != "" ? Convert.ToInt32(Compulsory1) : 0);
                int compulsory2 = (Compulsory2 != "" ? Convert.ToInt32(Compulsory2) : 0);
                objl = objs.BasicDetail(ApplicationNo);
                var result = obj.subsidiarysubjects_Save("SaveSubsubsidiary", objl.Id, subsidiary1, susidiary2, compulsory1, compulsory2, objl.session);
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