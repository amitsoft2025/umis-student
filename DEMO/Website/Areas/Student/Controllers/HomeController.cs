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
using Website.Models;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html.simpleparser;
using System.Configuration;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web.UI;
using NReco.PdfGenerator;
using com.toml.dp.util;
using Newtonsoft.Json;
using System.Security.Cryptography;
using System.Collections.Specialized;
using javax.print;
using java.rmi.dgc;

namespace Website.Areas.Student.Controllers
{
    //[CookiesExpireFilterAdminAttribute]
    public class HomeController : Controller
    {
        // GET: Student/Home

        // [VerifyUrlFilterAdminAttribute]

        public ActionResult Index1()
        {

            DateTime dt = DateTime.Now;
            DateTime baseDate = new DateTime();
            baseDate = System.DateTime.Now;
            DateTime dt1 = System.DateTime.Now.Date;
            ////CommonMethod.WritetoNotepadtest("Page load Start", "start time "+System.DateTime.Now.ToString("yyyy.MM.dd HH:mm:ss:ffff"));
            BL_student_formcomplete bl = new BL_student_formcomplete();
            AcademicSession ac = new AcademicSession();
            int sessionid = ac.GetAcademiccurrentSession().ID;
            var result = bl.sp_st_check_details(ClsLanguage.GetCookies("NBApplicationNo"), sessionid.ToString());
            // CommonMethod.WritetoNotepadtest("Page proc end sp_st_check_details Exection", "end time " + System.DateTime.Now.ToString());
            BL_PrintApplication ob = new BL_PrintApplication();
            //CommonMethod.WritetoNotepadtest("Page proc start BasicDetail Exection", "start time " + System.DateTime.Now.ToString());
            string ApplicationID = ClsLanguage.GetCookies("NBApplicationNo");

            StudentLogin tblST = new StudentLogin();
            var obj1 = tblST.BasicDetail(ApplicationID);

            ViewBag.studentyear = obj1.StudentYear;

            Recruitment rec = new Recruitment();
            RecruitmentList reclist = new RecruitmentList();
            List<EaxmFeesSubmit> subjectlist = new List<EaxmFeesSubmit>();
            ExamForm exam = new ExamForm();
            ViewBag.isback = 0;



            if (obj1.CourseCategory == 1)
            {
                subjectlist = exam.backFeesDetailSubjectlist_UG(obj1.CourseCategory, 0, 1, obj1.session, obj1.CollegeID, obj1.Id);
                if (subjectlist.Count > 0)
                {
                    ViewBag.isback = 1;
                    ViewBag.courseyearidenc1 = EncriptDecript.Encrypt(1.ToString());
                }
                subjectlist = exam.backFeesDetailSubjectlist_UG(obj1.CourseCategory, 0, 2, obj1.session, rec.collegeid, obj1.Id); if (subjectlist.Count > 0)
                {
                    ViewBag.isbackpart2 = 1;
                    ViewBag.courseyearidenc2 = EncriptDecript.Encrypt(2.ToString());
                }

            }

            if (obj1.CourseCategory == 2)
            {
                subjectlist = exam.backFeesDetailSubjectlist_UG(obj1.CourseCategory, 0, 4, obj1.session, rec.collegeid, obj1.Id);
                if (subjectlist.Count > 0)
                {
                    ViewBag.isback = 1;
                    ViewBag.courseyearidenc1 = EncriptDecript.Encrypt(4.ToString());
                }
                subjectlist = exam.backFeesDetailSubjectlist_UG(obj1.CourseCategory, 0, 5, obj1.session, rec.collegeid, obj1.Id); if (subjectlist.Count > 0)
                {
                    ViewBag.isbackpart2 = 1;
                    ViewBag.courseyearidenc2 = EncriptDecript.Encrypt(5.ToString());
                }

            }

            if (obj1.CourseCategory == 3)
            {
                subjectlist = exam.backFeesDetailSubjectlist_UG(obj1.CourseCategory, 0, 7, obj1.session, rec.collegeid, obj1.Id);
                if (subjectlist.Count > 0)
                {
                    ViewBag.isback = 1;
                    ViewBag.courseyearidenc1 = EncriptDecript.Encrypt(7.ToString());
                }
                subjectlist = exam.backFeesDetailSubjectlist_UG(obj1.CourseCategory, 0, 8, obj1.session, rec.collegeid, obj1.Id); if (subjectlist.Count > 0)
                {
                    ViewBag.isbackpart2 = 1;
                    ViewBag.courseyearidenc2 = EncriptDecript.Encrypt(8.ToString());
                }

            }



            return View(result);
        }


        public ActionResult Index()

        {

            //return RedirectToAction("Index1", "Home");

            DateTime dt = DateTime.Now;
            DateTime baseDate = new DateTime();
            baseDate = System.DateTime.Now;

            DateTime dt1 = System.DateTime.Now.Date;
            ////CommonMethod.WritetoNotepadtest("Page load Start", "start time "+System.DateTime.Now.ToString("yyyy.MM.dd HH:mm:ss:ffff"));
            BL_student_formcomplete bl = new BL_student_formcomplete();
            AcademicSession ac = new AcademicSession();
            int sessionid = ac.GetAcademiccurrentSession().ID;

            // for spot admission //ac.getcutdate().status;
            // CommonMethod.WritetoNotepadtest("Page proc start sp_st_check_details Exection", "start time " + System.DateTime.Now.ToString());
            var result = bl.sp_st_check_details(ClsLanguage.GetCookies("NBApplicationNo"), sessionid.ToString());
            // CommonMethod.WritetoNotepadtest("Page proc end sp_st_check_details Exection", "end time " + System.DateTime.Now.ToString());
            BL_PrintApplication ob = new BL_PrintApplication();
            //CommonMethod.WritetoNotepadtest("Page proc start BasicDetail Exection", "start time " + System.DateTime.Now.ToString());
            string ApplicationID = ClsLanguage.GetCookies("NBApplicationNo");
            ViewBag.statusdate = true;

            //ViewBag.statusdate = true;

            //if (ApplicationID == "Mu52144703" || ApplicationID == "MU86787018"|| ApplicationID == "MU74773359"|| ApplicationID == "MU45508561"||ApplicationID == "MU13563519"||ApplicationID == "MU17959121"||ApplicationID == "MU26766272"||ApplicationID == "MU25680998"||ApplicationID == "MU70029209"||ApplicationID == "MU69140394"||ApplicationID == "MU76033877"|| ApplicationID == "MU47083433")//Spot Admisiion Allow
            //{
            //    ViewBag.statusdate = true;
            //}


            StudentLogin tblST = new StudentLogin();
            var obj1 = tblST.BasicDetail(ApplicationID);
            var objfailpass = tblST.alldetailcheeck(ApplicationID);
            // CommonMethod.WritetoNotepadtest("Page proc end BasicDetail Exection", "start time " + System.DateTime.Now.ToString());
            //CommonMethod.WritetoNotepadtest("Page proc start CheckAdmission_details Exection", "start time " + System.DateTime.Now.ToString());
            var res1 = bl.CheckAdmission_details(sessionid);
            // CommonMethod.WritetoNotepadtest("Page proc end CheckAdmission_details Exection", "end time " + System.DateTime.Now.ToString());

            ViewBag.FailPAss = objfailpass.CheckFailOrPass;
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

            // CommonMethod.WritetoNotepadtest("Page proc start CheckStudent_detailsInReject Exection", "start time " + System.DateTime.Now.ToString());
            var resureject = bl.CheckStudent_detailsInReject(sessionid);

            // CommonMethod.WritetoNotepadtest("Page proc end CheckStudent_detailsInReject Exection", "end time " + System.DateTime.Now.ToString());
            if (resureject != null)
            {
                ViewBag.IsDocVerifyReject = resureject.IsDocVerify;
                ViewBag.IsDocVerifyDateReject = resureject.IsDocVerifyDate;
            }
            else
            {
                ViewBag.IsDocVerifyReject = 0;
                ViewBag.IsDocVerifyDateReject = 0;
            }
            if (obj1 != null)
            {
                if (obj1.session == 43)//first time admission hota h tb lgana h
                {
                    // when college admission open
                    ViewBag.addmissionExtenddate = false;
                    ViewBag.addmissionExtenddateValue = "";
                    ViewBag.addmissionStartdate = false;
                    ViewBag.addmissionStartdateValue = "";
                    ViewBag.addmissionExtenddateValuedoc = "";
                    ViewBag.addmissionStartdateValuedoc = "";
                    int educationtype = obj1.EducationType;
                    var dateextend = ob.CheckStudentAddmisionExtendDate(sessionid, educationtype);
                    ViewBag.addmissionExtenddate = dateextend.Status;
                    ViewBag.addmissionExtenddateValue = dateextend.ExtendDate;
                    var datestart = ob.CheckStudentAddmisionStartDate(sessionid, educationtype);
                    ViewBag.addmissionStartdate = datestart.Status;
                    ViewBag.addmissionStartdateValue = datestart.startdate;
                    var dateextenddoc = ob.documnetCheckStudentAddmisionExtendDate(sessionid, educationtype);
                    ViewBag.addmissionExtenddateValuedoc = dateextenddoc.ExtendDate;
                    var datestartdoc = ob.documentCheckStudentAddmisionStartDate(sessionid, educationtype);
                    ViewBag.addmissionStartdateValuedoc = datestartdoc.startdate;
                    ViewBag.rejectreason = ob.CheckDocumentVerification(sessionid).rejectreason;
                }
                else
                {
                    ViewBag.addmissionExtenddate = false;
                    ViewBag.addmissionExtenddateValue = "";
                    ViewBag.addmissionStartdate = false;
                    ViewBag.addmissionStartdateValue = "";
                    ViewBag.addmissionExtenddateValuedoc = "";
                    ViewBag.addmissionStartdateValuedoc = "";
                    int educationtype = obj1.EducationType;
                    var dateextend = ob.CheckStudentAddmisionExtendDate(sessionid, educationtype);
                    ViewBag.addmissionExtenddate = dateextend.Status;
                    ViewBag.addmissionExtenddateValue = dateextend.ExtendDate;
                    var datestart = ob.CheckStudentAddmisionStartDate(sessionid, educationtype);
                    ViewBag.addmissionStartdate = datestart.Status;
                    ViewBag.addmissionStartdateValue = datestart.startdate;
                    var dateextenddoc = ob.documnetCheckStudentAddmisionExtendDate(sessionid, educationtype);
                    ViewBag.addmissionExtenddateValuedoc = dateextenddoc.ExtendDate;
                    var datestartdoc = ob.documentCheckStudentAddmisionStartDate(sessionid, educationtype);
                    ViewBag.addmissionStartdateValuedoc = datestartdoc.startdate;
                    ViewBag.rejectreason = ob.CheckDocumentVerification(sessionid).rejectreason;

                }
            }


            //  CommonMethod.WritetoNotepadtest("Page proc start CheckStudentAdmission Exection", "start time " + System.DateTime.Now.ToString());

            var obj = ob.CheckStudentAdmission(sessionid);

            // CommonMethod.WritetoNotepadtest("Page proc end CheckStudentAdmission Exection", "end time " + System.DateTime.Now.ToString());
            ViewBag.sessionid = sessionid;
            ViewBag.studentyear = obj1.StudentYear;


            if (obj.Status == true)
            {
                ViewBag.Status = obj.Status;
                ViewBag.Course = obj.CourseName;
                ViewBag.College = obj.CollegeName;
                ViewBag.Stream = obj.StreamName;
                ViewBag.Subsidiary1_subject = obj.Subsidiary1_subject;
                ViewBag.Subsidiary2_subject = obj.Subsidiary2_subject;
                ViewBag.Compulsory1_subject = obj.Compulsory1_subject;
                ViewBag.Compulsory2_subject = (obj.Compulsory2_subject == null ? "" : obj.Compulsory2_subject);
                ClsLanguage.SetCookies(EncriptDecript.Encrypt(obj.streamcategoryid.ToString()), "ENNBstreamcategoryid");
            }
            else
            {
                ViewBag.Status = false;
                ViewBag.Course = "";
                ViewBag.College = "";
            }
            Recruitment rec = new Recruitment();
            RecruitmentList reclist = new RecruitmentList();
            List<EaxmFeesSubmit> subjectlist = new List<EaxmFeesSubmit>();
            ExamForm exam = new ExamForm();
            ViewBag.isback = 0;

            //CommonMethod.WritetoNotepadtest("Page proc start view1customfeesubmittedstudentdetailList Exection", "start time " + System.DateTime.Now.ToString());
            reclist = rec.view1customfeesubmittedstudentdetailList(obj1.Id);


            // CommonMethod.WritetoNotepadtest("Page proc end view1customfeesubmittedstudentdetailList Exection", "end time " + System.DateTime.Now.ToString());
            rec = reclist.qlist.ToList().FirstOrDefault();
            if (rec != null)
            {
                ViewBag.enrollnmentno = rec.enrollmentno;
                ViewBag.rollno = rec.RollNo;
                ViewBag.Sessionname = rec.Session;
                ViewBag.studentname = rec.StudentName;
                ViewBag.ftitle = obj1.Ftitle;
                ViewBag.fathername = rec.FatherName;
                ViewBag.mothername = rec.mothername;
                ViewBag.DOb = rec.DOB;
                ViewBag.Gender = rec.Gender;
                ViewBag.category = rec.StudentCasteCategoryName;
                ViewBag.Nationlity = rec.Nationlity;
                ViewBag.stphoto = rec.stphoto;
                ViewBag.stsign = rec.stsign;
                // find Back Log Subject
                if (obj1.session == 43)
                { }
                else
                {
                    if (obj1.CourseCategory == 1)
                    {
                        subjectlist = exam.backFeesDetailSubjectlist_UG(obj1.CourseCategory, 0, 1, obj1.session, rec.collegeid, obj1.Id);
                        if (subjectlist.Count > 0)
                        {
                            ViewBag.isback = 1;
                            ViewBag.courseyearidenc1 = EncriptDecript.Encrypt(1.ToString());
                        }
                        subjectlist = exam.backFeesDetailSubjectlist_UG(obj1.CourseCategory, 0, 2, obj1.session, rec.collegeid, obj1.Id); if (subjectlist.Count > 0)
                        {
                            ViewBag.isbackpart2 = 1;
                            ViewBag.courseyearidenc2 = EncriptDecript.Encrypt(2.ToString());
                        }
                        //subjectlist = exam.backFeesDetailSubjectlist_UG(obj1.CourseCategory, 0, 3, obj1.session, rec.collegeid, obj1.Id); if (subjectlist.Count > 0)
                        //{
                        //    ViewBag.isbackpart3 = 1;
                        //    ViewBag.courseyearidenc3 = EncriptDecript.Encrypt(3.ToString());
                        //}
                    }

                    if (obj1.CourseCategory == 2)
                    {
                        subjectlist = exam.backFeesDetailSubjectlist_UG(obj1.CourseCategory, 0, 4, obj1.session, rec.collegeid, obj1.Id);
                        if (subjectlist.Count > 0)
                        {
                            ViewBag.isback = 1;
                            ViewBag.courseyearidenc1 = EncriptDecript.Encrypt(4.ToString());
                        }
                        subjectlist = exam.backFeesDetailSubjectlist_UG(obj1.CourseCategory, 0, 5, obj1.session, rec.collegeid, obj1.Id); if (subjectlist.Count > 0)
                        {
                            ViewBag.isbackpart2 = 1;
                            ViewBag.courseyearidenc2 = EncriptDecript.Encrypt(5.ToString());
                        }
                        //subjectlist = exam.backFeesDetailSubjectlist_UG(obj1.CourseCategory, 0, 6, obj1.session, rec.collegeid, obj1.Id); if (subjectlist.Count > 0)
                        //{
                        //    ViewBag.isbackpart3 = 1;
                        //    ViewBag.courseyearidenc3 = EncriptDecript.Encrypt(6.ToString());
                        //}
                    }

                    if (obj1.CourseCategory == 3)
                    {
                        subjectlist = exam.backFeesDetailSubjectlist_UG(obj1.CourseCategory, 0, 7, obj1.session, rec.collegeid, obj1.Id);
                        if (subjectlist.Count > 0)
                        {
                            ViewBag.isback = 1;
                            ViewBag.courseyearidenc1 = EncriptDecript.Encrypt(7.ToString());
                        }
                        subjectlist = exam.backFeesDetailSubjectlist_UG(obj1.CourseCategory, 0, 8, obj1.session, rec.collegeid, obj1.Id); if (subjectlist.Count > 0)
                        {
                            ViewBag.isbackpart2 = 1;
                            ViewBag.courseyearidenc2 = EncriptDecript.Encrypt(8.ToString());
                        }
                        //subjectlist = exam.backFeesDetailSubjectlist_UG(obj1.CourseCategory, 0, 9, obj1.session, rec.collegeid, obj1.Id); if (subjectlist.Count > 0)
                        //{
                        //    ViewBag.isbackpart3 = 1;
                        //    ViewBag.courseyearidenc3 = EncriptDecript.Encrypt(9.ToString());
                        //}
                    }


                }


                DataTable dtAdmit = new DataTable();
                dtAdmit = exam.DummyAdmitCardFormStatus_UG(0, obj1.Id, "AllAdmitCard");
                int a = 0;
                //  return View();
                var responseString = "";
                SbiPayExam sbi = new SbiPayExam();

                foreach (DataRow item in dtAdmit.Rows)

                {
                    if (item["IsExamfeesubmit"].ToString() == "1")
                    {
                        if (item["courseyearid"].ToString() == "1" || item["courseyearid"].ToString() == "4" || item["courseyearid"].ToString() == "7")
                        {
                            ViewBag.courseyearid1 = item["courseyearid"].ToString();
                            if (item["IsBack"].ToString() == "1")
                            {
                                ViewBag.courseyearidenc = "1";
                                ViewBag.isbackAdmitCard1 = "1";
                            }
                            else
                            {
                                ViewBag.isMainAdmitCard1 = "1";
                            }

                        }
                        if (item["courseyearid"].ToString() == "2" || item["courseyearid"].ToString() == "5" || item["courseyearid"].ToString() == "8")
                        {
                            ViewBag.courseyearid2 = item["courseyearid"].ToString();
                            if (item["IsBack"].ToString() == "1")
                            {
                                ViewBag.isbackAdmitCard2 = "1";
                            }
                            else
                            {
                                ViewBag.isMainAdmitCard2 = "1";
                            }
                        }

                        //if (item["coursecategoryid"].ToString() == "3" || item["coursecategoryid"].ToString() == "6" || item["coursecategoryid"].ToString() == "9")
                        //{
                        //ViewBag.courseyearid3 = item["coursecategoryid"].ToString();
                        //    if (item["IsBack"].ToString() == "1")
                        //    {
                        //        ViewBag.isbackAdmitCard3 = "1";
                        //    }
                        //    else
                        //    {
                        //        ViewBag.isMainAdmitCard3 = "1";
                        //    }
                        //}
                    }
                }


            }
            else
            {

                ViewBag.enrollnmentno = "";
                ViewBag.rollno = "";
                ViewBag.Sessionname = "";
                ViewBag.studentname = "";
                ViewBag.ftitle = obj1.Ftitle;
                ViewBag.fathername = "";
                ViewBag.mothername = "";
                ViewBag.DOb = "";
                ViewBag.Gender = "";
                ViewBag.category = "";
                ViewBag.Nationlity = "";
                ViewBag.stphoto = "";
                ViewBag.stsign = "";
            }
            var resultexam = exam.Feesischeckexamfeesubmit(obj1.Id, obj1.session);
            if (resultexam == null)
            {
                ViewBag.Examfeesubmit = 0;
            }
            else
            {
                ViewBag.Examfeesubmit = resultexam.IsExamfeesubmit;
            }
            // DateTime dt2 = DateTime.Now;
            //getting Milliseconds only from the currenttime
            //int ms2 = dt.Millisecond;

            // CommonMethod.WritetoNotepadtest("Page load end  ", "end time Millisecond" + ms2.ToString());
            // int final = ms - ms2;
            //TimeSpan diff = DateTime.Now - baseDate;
            //CommonMethod.WritetoNotepadtest("Page load end  ", "end time final Millisecond:----------" + diff.TotalMilliseconds.ToString() + "               Time end;-" + System.DateTime.Now.ToString("yyyy.MM.dd HH:mm:ss:ffff"));


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


        public JsonResult ADDIncomecertificate(string id = "")
        {
            StudentAdmissionQualification doc = new StudentAdmissionQualification();
            doc.ID = Convert.ToInt32(Request.Form["ID"] == "" ? "0" : Request.Form["ID"]);
            DataLayer.Login objl = new DataLayer.Login();
            StudentLogin objs = new StudentLogin();
            doc.ApplicationNo = ClsLanguage.GetCookies("NBApplicationNo");
            doc.SID = Convert.ToInt32(ClsLanguage.GetCookies("NBStID"));
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
                            string s3FileName = System.DateTime.Now.ToString("dd_MM_yyyy_HH_mm_ss") + "_Incomecertificate_" + objl.FirstName + @name;

                            s3FileName = s3FileName.Replace(" ", "");
                            doc.FileURl = s3FileName;
                            bool a;
                            //AmazonUploader myUploader = new AmazonUploader();
                            //a = myUploader.sendMyFileToFolder(photo,Server.MapPath("~/App_Data/uploads"),s3FileName);
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
                    if (doc.FileURl == "")
                    {
                        StudentAdmissionQualification ob1 = new StudentAdmissionQualification();
                        ob1.Msg = "Error Please select File: ";
                        return Json(ob1, JsonRequestBehavior.AllowGet);

                    }
                    var result = doc.Addincomedocument(doc.SID, doc.FileURl);
                    if (result.Status == true)
                    {
                        result.Msg = "Document Upload Successfully,Please reach college and verify document !!";
                    }
                    else
                    {
                        result.Msg = "Failed to upload document !!";
                    }
                    return Json(result, JsonRequestBehavior.AllowGet);
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
                StudentAdmissionQualification logmsg1 = new StudentAdmissionQualification();
                logmsg1.Msg = "Error occurred. Error details: ";
                return Json(logmsg1, JsonRequestBehavior.AllowGet);
            }
            StudentAdmissionQualification logmsg = new StudentAdmissionQualification();
            logmsg.Msg = "Error occurred. Error details: ";
            return Json(logmsg, JsonRequestBehavior.AllowGet);
        }

        public ActionResult PG()
        {
            try
            {
                Sbiepay sbi = new Sbiepay();
                FeesSubmit stlogin = new FeesSubmit();
                decimal Amount1 = 1;
                string MerchantCustomerID1 = "1";

                ViewBag.requestparams = "";
                ViewBag.merchantId = "";
                ViewBag.EncryptbillingDetails = "";
                ViewBag.EncryptshippingDetais = "";
                ViewBag.url = "";

            }
            catch (Exception ex)
            {
                CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "action PGGateway error", ClsLanguage.GetCookies("NBApplicationNo"));

                return View();
            }
            return View();
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
                    Commn_master com = new Commn_master();
                    AcademicSession ad = new AcademicSession();
                    //if (result.Id == 53770 || result.Id == 56145 || result.Id == 56544 || result.Id == 60408 || result.Id == 53350 || result.Id == 53367 || result.Id == 58239 || result.Id == 60550 || result.Id==56043 || result.Id == 64387 || result.Id == 64386 || result.Id == 55598 || result.Id == 55835 || result.Id == 59996 || result.Id== 56008 ||result.Id== 58002)
                    //{
                    // ViewBag.check_admissionopen = true;
                    //}
                    //else
                    //{
                    //    ViewBag.check_admissionopen = com.check_admission_open(ad.GetAcademiccurrentSession().ID, Convert.ToInt32(CommonSetting.Commonid.Educationtype));

                    //}
                    ViewBag.check_admissionopen = com.check_admission_open(ad.GetAcademiccurrentSession().ID, Convert.ToInt32(CommonSetting.Commonid.Educationtype));
                    if (ViewBag.check_admissionopen == false)
                    {
                        return RedirectToAction("FeesSubmit");
                    }
                    Amount1 = Convert.ToDecimal(result.Fees);
                    var obj = sbi.encriptData(Amount1, EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENBApplicationNo")), "student/home/PGSucess", "student/home/PGFailed");

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

        public ActionResult SelectPaymentGetway()
        {
            return View();
        }
        public ActionResult PGGateway_AirPay()
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
                    Commn_master com = new Commn_master();
                    AcademicSession ad = new AcademicSession();
                    //if (result.Id == 53770 || result.Id == 56145 || result.Id == 56544 || result.Id == 60408 || result.Id == 53350 || result.Id == 53367 || result.Id == 58239 || result.Id == 60550 || result.Id==56043 || result.Id == 64387 || result.Id == 64386 || result.Id == 55598 || result.Id == 55835 || result.Id == 59996 || result.Id== 56008 ||result.Id== 58002)
                    //{
                    // ViewBag.check_admissionopen = true;
                    //}
                    //else
                    //{
                    //    ViewBag.check_admissionopen = com.check_admission_open(ad.GetAcademiccurrentSession().ID, Convert.ToInt32(CommonSetting.Commonid.Educationtype));

                    //}
                    ViewBag.check_admissionopen = com.check_admission_open(ad.GetAcademiccurrentSession().ID, Convert.ToInt32(CommonSetting.Commonid.Educationtype));
                    if (ViewBag.check_admissionopen == false)
                    {
                        return RedirectToAction("FeesSubmit");
                    }
                    Amount1 = Convert.ToDecimal(result.Fees);
                    var obj = sbi.encriptDataAirpay(Amount1, EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENBApplicationNo")), ConfigurationManager.AppSettings["AirPaySuccesUrl"], ConfigurationManager.AppSettings["AirPayFailURL"]);

                    //ViewBag.requestparams = obj.requestparams;
                    //ViewBag.merchantId = obj.merchantId;
                    //ViewBag.EncryptbillingDetails = obj.EncryptbillingDetails;
                    //ViewBag.EncryptshippingDetais = obj.EncryptshippingDetais;
                    //ViewBag.url = obj.url;

                    ViewBag.orderid = obj.Oid;
                    ViewBag.buyerEmail = lo.Email;
                    ViewBag.buyerPhone = lo.MobileNo;
                    ViewBag.buyerFirstName = lo.FirstName;
                    ViewBag.buyerLastName = lo.LastName;
                    //ViewBag.buyerAddress = lo.CurrentAddress;
                    //ViewBag.ViewBag.buyerCity = lo.CA_City.ToString();
                    //ViewBag.buyerState = lo.CA_State.ToString();
                    //ViewBag.buyerCountry = lo.CA_Country.ToString();
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
                string ApplicationID = ClsLanguage.GetCookies("NBApplicationNo");


                StudentLogin stu = new StudentLogin();
                Login lo = stu.BasicDetail(ApplicationID);
                Sbiepay sbi = new Sbiepay();
                FeesSubmit stlogin = new FeesSubmit();
                decimal Amount1 = 1;
                string MerchantCustomerID1 = "1";
                var result = stlogin.FeessubEncryt(Convert.ToInt32(EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENNBStID"))));
                if (result != null)
                {
                    Commn_master com = new Commn_master();
                    AcademicSession ad = new AcademicSession();
                    ViewBag.check_admissionopen = com.check_admission_open(ad.GetAcademiccurrentSession().ID, Convert.ToInt32(CommonSetting.Commonid.Educationtype));
                    if (ViewBag.check_admissionopen == false)
                    {
                        return RedirectToAction("FeesSubmit");
                    }
                    Amount1 = Convert.ToDecimal(result.Fees);
                    //if(ApplicationID== "MU74182702")
                    //{
                    //    Amount1 = 5;
                    //}
                    var obj = sbi.encriptDataSafex(Amount1, EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENBApplicationNo")), "student/home/PGSucess", "student/home/PGFailed");

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
        public ActionResult PGGateway_EaseBuzz()
        {
            try
            {
                //AirPayApplication hsd =new AirPayApplication()
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
                    Commn_master com = new Commn_master();
                    AcademicSession ad = new AcademicSession();
                    ViewBag.check_admissionopen = com.check_admission_open(ad.GetAcademiccurrentSession().ID, Convert.ToInt32(CommonSetting.Commonid.Educationtype));
                    if (ViewBag.check_admissionopen == false)
                    {
                        return RedirectToAction("FeesSubmit");
                    }
                    Amount1 = Convert.ToDecimal(result.Fees);
                    //Amount1 = 1;
                    var obj = sbi.encriptDataEaseBuzz(Amount1, EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENBApplicationNo")), "student/home/PGSucess", "student/home/PGFailed");
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




        public ActionResult SafexPayPGSucess()
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
                //MyCryptoClass aes = new MyCryptoClass();
                Sbiepay aes = new Sbiepay();
                string txn_response = aes.decrypt(enc_txn_response);
                string decodedUrl = HttpUtility.UrlDecode(txn_response);
                string[] txn_hash = decodedUrl.Split('|');
                //string[] txn_hash = decodedUrl.Split('~');
                string txn_res_hash = txn_hash[1];
                string txn_res_actual = txn_hash[0] + "" + txn_hash[2];
                //string txn_response = aes.decrypt(enc_txn_response);
                string[] txn_response_arr = txn_hash;//txn_res_actual.Split('|');
                //string Hash = txn_response_arr[10] + "~" + txn_response_arr[1] + "~" + txn_response_arr[2] + "~" + txn_response_arr[3] + "~" + txn_response_arr[4] + "~" + txn_response_arr[5] + "~" + txn_response_arr[8];
                string Hash = txn_response_arr[10] + "|" + txn_response_arr[1] + "|" + txn_response_arr[2] + "|" + txn_response_arr[3] + "|" + txn_response_arr[4] + "|" + txn_response_arr[5] + "|" + txn_response_arr[8];
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
                    var result = sbi.pgsucessdecryptSafexPay(Convert.ToInt32(Sid), banktrxid, clienttrxid, amount1, feeamount, gst, commission, paymode, banktxndate, Reason, apitxnid, ApplicationNo, courseyearid, examType, Requestdata, dRequestdata, PGstatus);
                    return RedirectToAction("ResponseAirpay");
                }
                else
                {


                    PGstatus = "Fail";
                    Sbiepay sbi = new Sbiepay();
                    var result = sbi.pgsucessdecryptSafexPay(Convert.ToInt32(Sid), banktrxid, clienttrxid, amount1, feeamount, gst, commission, paymode, banktxndate, Reason, apitxnid, ApplicationNo, courseyearid, examType, Requestdata, dRequestdata, PGstatus);
                    return RedirectToAction("ResponseAirpay");
                }
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
        public ActionResult ResponseEaseBuzz()
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
            //return RedirectToAction("Index", "HomePG");
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


        [VerifyUrlFilterAdminAttribute]
        public ActionResult BasicDetail()
        {
            Commn_master com = new Commn_master();
            AcademicSession ac = new AcademicSession();
            ViewBag.Admissiontype = com.getcommonMaster("AdmissionType");
            var Educationtyperes = com.getcommonMaster("EducationType");
            ViewBag.Educationtype1 = Educationtyperes.Where(m => m.CommonId == 11);
            ViewBag.Educationtype = Educationtyperes;

            ViewBag.bloodgroup = com.Getbloodgroup("Select");
            Country cont = new Country();
            ViewBag.country = cont.GetAllCountries();
            ViewBag.Pcountry = cont.GetAllCountries();
            ViewBag.stitle = com.getcommonMaster("Title");

            ViewBag.Nationality = com.getcommonMaster("Nationality");
            ViewBag.Religion = com.getcommonMaster("Religion");
            string ApplicationID = ClsLanguage.GetCookies("NBApplicationNo");
            ViewBag.boardtype = CommonMethod.Boradtype().Where(x => x.boardid == 1 || x.boardid == 2 || x.boardid == 3);
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
            if (obj.title == Convert.ToInt32(CommonSetting.Commonid.Mr))
            {
                ViewBag.ftitle = com.getcommonMaster("maleftile");
                ViewBag.Gender = com.getcommonMaster("malegender");
            }
            else
            {
                ViewBag.ftitle = com.getcommonMaster("femaleftitle");
                ViewBag.Gender = com.getcommonMaster("femalegender");
            }
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
            //if (obj.Id == 53770 || obj.Id == 56145 || obj.Id == 56544 || obj.Id == 60408 || obj.Id == 53350 || obj.Id == 53367 || obj.Id == 58239 || obj.Id == 60550 || obj.Id == 56043 || obj.Id == 64387 || obj.Id == 64386 || obj.Id == 55598 || obj.Id == 55835 || obj.Id == 59996 || obj.Id == 56008 || obj.Id == 58002)
            //{
            //    ViewBag.check_admissionopen = true;
            //}
            //else
            //{
            ViewBag.check_admissionopen = true;//com.check_admission_open(ac.GetAcademiccurrentSession().ID, Convert.ToInt32(CommonSetting.Commonid.Educationtype));

            //}
            ViewBag.check_admissionclose = com.check_admission_close(ac.GetAcademiccurrentSession().ID, Convert.ToInt32(CommonSetting.Commonid.Educationtype));
            BL_PrintApplication ob = new BL_PrintApplication();
            var objrecritiny = ob.CheckStudentAdmission(ac.GetAcademiccurrentSession().ID);

            StudentAdmissionQualification stad = new StudentAdmissionQualification();
            ViewBag.Qualification = stad.GetQualifiationMasterOldStudent();
            // ViewBag.Qualification = stad.GetQualifiationMasterOldStudent();
            ViewBag.previousqua = stad.getqualificationst(Convert.ToInt32(CommonSetting.Commonid.Educationtype), "getqualificationstdiploma");
            ViewBag.Statusrecruitny = false;
            if (objrecritiny.Status == true)
            {
                if (obj.session == 42)
                {
                    if (objrecritiny.IsDocVerify == 2)
                    {

                    }
                    else
                    {
                        ViewBag.Statusrecruitny = objrecritiny.Status;
                    }
                }
            }
            return View(obj);

        }
        [HttpPost]
        public ActionResult BasicDetail(Login objlogin, HttpPostedFileBase sign, HttpPostedFileBase photo)
        {

            StudentLogin st = new StudentLogin();
            Commn_master com = new Commn_master();
            AcademicSession ac = new AcademicSession();
            ViewBag.Admissiontype = com.getcommonMaster("AdmissionType");
            var Educationtyperes = com.getcommonMaster("EducationType");
            ViewBag.Educationtype1 = Educationtyperes.Where(m => m.CommonId == 11);
            ViewBag.Educationtype = Educationtyperes;
            ViewBag.Gender = com.getcommonMaster("Gender");
            ViewBag.boardtype = CommonMethod.Boradtype().Where(x => x.boardid == 1 || x.boardid == 2 || x.boardid == 3);
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
            
            //ViewBag.titileid = objlogin.title;
            ViewBag.ftitileid = objlogin.Ftitle;
            ViewBag.Nationalityid = objlogin.Nationality;
            ViewBag.Religionid = objlogin.Religion;
            ViewBag.MotherTongueid = objlogin.MotherTongue;
            string ApplicationID = ClsLanguage.GetCookies("NBApplicationNo");
            StudentLogin tblST = new StudentLogin();
            var obj = tblST.BasicDetail(ApplicationID);
            try
            {
                if (obj.Gender == Convert.ToInt32(CommonSetting.Commonid.Femalegender))
                {
                    ViewBag.CasteCategory = com.getcommonMaster("CasteCategory");
                }
                else
                {
                    ViewBag.CasteCategory = com.getcommonMaster("CastwithoutWBC");
                }
            }
            catch 
            { 
            
            }
            objlogin.EducationType = obj.EducationType;
            objlogin.AdmisitionCategory = obj.AdmisitionCategory;
            objlogin.EducationType = obj.EducationType;
            objlogin.CourseCategory = obj.CourseCategory;
            objlogin.session = obj.session;
            objlogin.disabilityType = objlogin.disabilityType;
            objlogin.disabilityPercent = objlogin.disabilityPercent;

            // objlogin.previous_qua_id = obj.previous_qua_id;
            ViewBag.eduid = obj.EducationType;
            //ViewBag.check_admissionopen = com.check_admission_open(ac.GetAcademiccurrentSession().ID, Convert.ToInt32(CommonSetting.Commonid.Educationtype));
            //if (objlogin.Id == 53770 || objlogin.Id == 56145 || objlogin.Id == 56544 || objlogin.Id == 60408 || objlogin.Id == 53350 || objlogin.Id == 53367 || objlogin.Id == 58239 || objlogin.Id == 60550 || objlogin.Id == 56043 || objlogin.Id == 64387 || objlogin.Id == 64386 || objlogin.Id == 55598 || objlogin.Id == 55835 || objlogin.Id == 59996 || objlogin.Id == 56008 || objlogin.Id == 58002)
            //{
            //    ViewBag.check_admissionopen = true;
            //}
            //else
            //{
            ViewBag.check_admissionopen = com.check_admission_open(ac.GetAcademiccurrentSession().ID, Convert.ToInt32(CommonSetting.Commonid.Educationtype));

            //}
            BL_PrintApplication ob = new BL_PrintApplication();
            var objrecritiny = ob.CheckStudentAdmission(ac.GetAcademiccurrentSession().ID);

            StudentAdmissionQualification stad = new StudentAdmissionQualification();
            ViewBag.Qualification = stad.GetQualifiationMasterOldStudent();
            ViewBag.previousqua = stad.getqualificationst(Convert.ToInt32(CommonSetting.Commonid.Educationtype), "getqualificationstdiploma");
            //ViewBag.Qualification = stad.GetQualifiationMaster(obj.EducationType, obj.previous_qua_id, "0", obj.Id);
            ViewBag.Statusrecruitny = false;
            //if (objrecritiny.Status == true)
            //{
            //    ViewBag.Statusrecruitny = objrecritiny.Status;
            //}
            if (objrecritiny.Status == true)
            {
                if (obj.session == 42)
                {
                    if (objrecritiny.IsDocVerify == 2)
                    {

                    }
                    else
                    {
                        ViewBag.Statusrecruitny = objrecritiny.Status;
                    }
                }
            }
            //if (objlogin.is_GEW == true)
            //{
            //    if (objlogin.CastCategory != 4)
            //    {
            //        TempData["StMessage"] = "General Economical Weaker Only for General Category Student,Please Change !!";
            //        return View(objlogin);
            //    }
            //}
            if (objlogin.PA_State == 0)
            {
                TempData["StMessage"] = "Please Select Permanent  State !!";
                return View(objlogin);
            }

            string jsonstring = JsonConvert.SerializeObject(objlogin);
            if (objlogin.Gender != 9)
            {
                if (objlogin.CastCategory == 23)
                {
                    TempData["StMessage"] = "Please Select Another Caste ,Becuase This is a Only for Female!!";
                    return View(objlogin);
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
                        string directoryPath = Server.MapPath("~/uploads/Student/Photoandsign/");
                        string fileName = System.DateTime.Now.ToString("dd_MM_yyyy_HH_mm_ss") + "_StudentPhoto_" + objlogin.FirstName + Path.GetExtension(name);
                        fileName = fileName.Replace(" ", "");

                        if (!Directory.Exists(directoryPath))
                        {
                            Directory.CreateDirectory(directoryPath);
                        }
                        string localPath = Path.Combine(directoryPath, fileName);
                        using (FileStream fileStream = new FileStream(localPath, FileMode.Create))
                        {
                            st1.CopyTo(fileStream);
                        }
                        // ✅ Only save image name in DB
                        objlogin.stphoto = fileName;
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
                        string directoryPath = Server.MapPath("~/uploads/Student/Photoandsign/"); // Local directory

                        // ✨ Create a unique file name with extension
                        string fileExtension = Path.GetExtension(name);
                        string fileName = DateTime.Now.ToString("dd_MM_yyyy_HH_mm_ss") + "_StudentSignature_" + objlogin.FirstName + fileExtension;
                        fileName = fileName.Replace(" ", "");

                        // Create directory if not exists
                        if (!Directory.Exists(directoryPath))
                        {
                            Directory.CreateDirectory(directoryPath);
                        }

                        // Save to local path
                        string localPath = Path.Combine(directoryPath, fileName);
                        using (FileStream fileStream = new FileStream(localPath, FileMode.Create))
                        {
                            st1.CopyTo(fileStream);
                        }

                        // ✅ Save only file name in DB
                        objlogin.stsign = fileName;
                    }
                    catch (Exception ex)
                    {
                        CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "Basic Detail post Method", ClsLanguage.GetCookies("NBApplicationNo") + "   " + jsonstring);
                    }
                }
                var result = st.Student_registrationUpdate(objlogin);
                if (result.status == true)
                {
                    TempData["StMessage"] = result.Message;
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
        [HttpPost]
        public JsonResult ChangeCourse_prevoiusquali(int CourseCategory, int previous_qua_id, int prevoiusboardid)
        {
            var ApplicationNo = ClsLanguage.GetCookies("NBApplicationNo");
            Login objl = new Login();
            StudentLogin objs = new StudentLogin();
            BL_PrintApplication obrecrei = new BL_PrintApplication();
            AcademicSession ac = new AcademicSession();
            var objrecritiny = obrecrei.CheckStudentAdmission(ac.GetAcademiccurrentSession().ID);
            if (objrecritiny.Status == true)
            {
                return Json(new { data = new Login(), success = true });
            }
            try
            {
                if (prevoiusboardid == 0)
                {
                    Login neww = new Login();
                    neww.status = false;
                    neww.Message = "Please Select Previous Board ";
                    return Json(new { data = neww, success = true });
                }
                if (previous_qua_id == 0)
                {
                    Login neww = new Login();
                    neww.status = false;
                    neww.Message = "Please Select Previous Stream  ";
                    return Json(new { data = neww, success = true });
                }
                if (CourseCategory == 0)
                {
                    Login neww = new Login();
                    neww.status = false;
                    neww.Message = "Please Select Course  ";
                    return Json(new { data = neww, success = true });
                }
                Login objl1text = new Login();
                objl1text = objs.BasicDetail(ApplicationNo);
                objl.Id = objl1text.Id;
                objl.session = objl1text.session;

                objl.AdmisitionCategory = Convert.ToInt32(CommonSetting.Commonid.RegularAdmissionType);
                objl.EducationType = Convert.ToInt32(CommonSetting.Commonid.Educationtype);
                objl.CourseCategory = CourseCategory;
                objl.previous_qua_id = previous_qua_id;
                objl.prevoiusboardid = prevoiusboardid;
                var result = objs.sp_studentregistration_changecourse(objl);
                return Json(new { data = result, success = true });
            }
            catch (Exception ex)
            {
                CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "ChangeCourse_prevoiusquali ", ClsLanguage.GetCookies("NBApplicationNo"));
                return Json(new { data = objl, success = true });
            }

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
        public MemoryStream Base64ToImage(StudentAdmissionQualification obj)
        {
            try
            {


                byte[] buffer = Convert.FromBase64String(obj.file.Split(',')[1]);
                MemoryStream memoryStream = new MemoryStream(buffer);
                memoryStream.Write(buffer, 0, 0);
                return memoryStream;

            }
            catch (Exception ex)
            {
                CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "StudentQualification file Upload method ", obj.ApplicationNo);
                return null;
            }
        }

        private void SaveByteArrayAsImage(string fullOutputPath, string base64String)
        {
            byte[] bytes = Convert.FromBase64String(base64String.Split(',')[1]);

            System.Drawing.Image image;
            using (MemoryStream ms = new MemoryStream(bytes))
            {
                image = System.Drawing.Image.FromStream(ms);
            }

            image.Save(fullOutputPath, System.Drawing.Imaging.ImageFormat.Png);
        }
        [HttpPost]
        public JsonResult QualificationSave(StudentAdmissionQualification ob)
        {
            string jsonstr = JsonConvert.SerializeObject(ob);
            StudentAdmissionQualification obj = new StudentAdmissionQualification();
            try
            {
                ob.ApplicationNo = ClsLanguage.GetCookies("NBApplicationNo");
                ob.SID = Convert.ToInt32(ClsLanguage.GetCookies("NBStID"));
                ob.session = Convert.ToInt32(ClsLanguage.GetCookies("NBSission"));

                if (ob.ID > 0)
                {
                    if (ob.FileURl != null)
                    {
                        var PicName = ob.FileURl;
                        MemoryStream file = this.Base64ToImage(ob);
                        string[] str = ob.FileURl.Split('.');
                        string ext = str.LastOrDefault();
                        string name = System.DateTime.Now.ToString("dd_MM_yyyy_HH_mm_ss") + "_Qualification_" + ob.ApplicationNo + "." + ext;
                        string directoryPath = Server.MapPath("~/uploads/Student/Document/");
                        if (!Directory.Exists(directoryPath))
                        {
                            Directory.CreateDirectory(directoryPath);
                        }
                        string localPath = Path.Combine(directoryPath, name);
                        using (FileStream fileStream = new FileStream(localPath, FileMode.Create))
                        {
                            file.CopyTo(fileStream);
                        }

                        try
                        {
                            int index = localPath.IndexOf("uploads");
                            if (index != -1)
                            {
                                string relativePath = localPath.Substring(index);
                                ob.FileURl = relativePath;
                            }
                        }
                        catch
                        {
                            ob.FileURl = localPath;
                        }
                        var result = obj.SaveQualificationDetails(ob);
                        var str1 = result.Msg;
                        return Json(str1, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        ob.FileURl = ob.hfile;
                        var result = obj.SaveQualificationDetails(ob);
                        var str1 = result.Msg;
                        return Json(str1, JsonRequestBehavior.AllowGet);
                        //return Json(result, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {

                    MemoryStream file = this.Base64ToImage(ob);
                    string[] str = ob.FileURl.Split('.');
                    string ext = str.LastOrDefault();
                    string name = System.DateTime.Now.ToString("dd_MM_yyyy_HH_mm_ss") + "_Qualification_" + ob.ApplicationNo + "." + ext;
                    string myBucketName = "mungeruniversity"; //your s3 bucket name goes here  
                    string s3DirectoryName = "Student/Document";
                    string s3FileName = @name;
                    bool a;
                    ob.FileURl = name;
                    AmazonUploader myUploader = new AmazonUploader();
                    a = myUploader.sendMyFileToS3(file, myBucketName, s3DirectoryName, s3FileName);
                    var result = obj.SaveQualificationDetails(ob);
                    var str1 = result.Msg;
                    return Json(str1, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                // CommonMethod.PrintLog(ex,HttpContext.Request.Url.AbsolutePath, "QualificationSave in previous qualification ", ClsLanguage.GetCookies("NBApplicationNo"));
                CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "Student Qualification Upload method", obj.ApplicationNo + " " + jsonstr);

            }
            // SaveByteArrayAsImage(Server.MapPath("~/Student/QualificationDocument/") + ob.FileURl , ob.file);
            return Json("error", JsonRequestBehavior.AllowGet);
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
            //string filePath = Server.MapPath("~/Student/QualificationDocument/" + fullPath);
            //if (System.IO.File.Exists(filePath))
            //{
            //    System.IO.File.Delete(filePath);
            //}
            if (res.QualicationType == 2 || res.QualicationType == 3 || res.QualicationType == 4)
            {
                if (res.session == 39)
                {
                    var result = obj.DeleteQualifiationByID_old(Convert.ToInt32(enID));
                    TempData["Msg"] = result.Msg;
                }
                else
                {
                    var result = obj.DeleteQualifiationByID(Convert.ToInt32(enID));
                    TempData["Msg"] = result.Msg;
                }

            }
            else
            {
                var result = obj.DeleteQualifiationByIDwithoutchoice(Convert.ToInt32(enID));
                TempData["Msg"] = result.Msg;
            }
            //var result = obj.DeleteQualifiationByID_old(Convert.ToInt32(enID));
            return RedirectToAction("StudentQualification");

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

        public void Docu_bindapplicationdoc()
        {
            DocumentUpload obj = new DocumentUpload();
            List<Commn_master> list = obj.Getapplicationdocument(Convert.ToInt32(ClsLanguage.GetCookies("NBStID")));
            List<SelectListItem> slist = new List<SelectListItem>();
            foreach (Commn_master dr in list)
            {
                slist.Add(new SelectListItem { Text = dr.Title, Value = dr.CommonId.ToString() });
            }
            ViewBag.Document = slist;
        }


        public MemoryStream changeBase64ToImage(DocumentUpload obj)
         {
            try
            {
                byte[] buffer = Convert.FromBase64String(obj.file.Split(',')[1]);
                MemoryStream memoryStream = new MemoryStream(buffer);
                memoryStream.Write(buffer, 0, 0);
                return memoryStream;

            }
            catch (Exception ex)
            {
                CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "Student Document file  Upload method", obj.ApplicationNo);
                return null;
            }
        }
        public ActionResult DocumentSave(DocumentUpload ob)
        {
            string jsonstring = JsonConvert.SerializeObject(ob);
            DocumentUpload obj = new DocumentUpload();
            try
            {
                ob.ApplicationNo = ClsLanguage.GetCookies("NBApplicationNo");
                ob.SID = Convert.ToInt32(ClsLanguage.GetCookies("NBStID"));
                ob.session = ClsLanguage.GetCookies("NBSission");
                jsonstring = JsonConvert.SerializeObject(ob);

                string fileSavePath = Server.MapPath("~/Student/Documents/");
                if (!Directory.Exists(fileSavePath))
                {
                    Directory.CreateDirectory(fileSavePath);
                }

                if (ob.EncriptedID != "0" && ob.EncriptedID.Length > 0)
                {
                    if (ob.FileName != null)
                    {
                        var file = this.changeBase64ToImage(ob);
                        string ext = Path.GetExtension(ob.FileName);
                        string name = DateTime.Now.ToString("dd_MM_yyyy_HH_mm_ss") + "_Document_" + ob.ApplicationNo + ext;
                        string fullPath = Path.Combine(fileSavePath, name);

                        using (var fileStream = new FileStream(fullPath, FileMode.Create, FileAccess.Write))
                        {
                            file.WriteTo(fileStream);
                        }

                        ob.FileName = name;
                        ob.ID = Convert.ToInt32(EncriptDecript.Decrypt(ob.EncriptedID));
                        var result = obj.SaveDocument(ob);
                        return Json(result.Msg, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        ob.FileName = ob.hfile;
                        ob.ID = Convert.ToInt32(EncriptDecript.Decrypt(ob.EncriptedID));
                        var result = obj.SaveDocument(ob);
                        return Json(result.Msg, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    var file = this.changeBase64ToImage(ob);
                    string ext = Path.GetExtension(ob.FileName);
                    string name = DateTime.Now.ToString("dd_MM_yyyy_HH_mm_ss") + "_Document_" + ob.ApplicationNo + ext;
                    string fullPath = Path.Combine(fileSavePath, name);

                    using (var fileStream = new FileStream(fullPath, FileMode.Create, FileAccess.Write))
                    {
                        file.WriteTo(fileStream);
                    }

                    ob.FileName = name;
                    var result = obj.SaveDocument(ob);
                    return Json(result.Msg, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "Student Document Upload", obj.ApplicationNo + "  " + jsonstring);
            }

            return View();
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
        [VerifyUrlFilterAdminAttribute]
        public ActionResult FeesSubmit()
        {

            //DoubleVerificationAirpay_300loop

            doubleverificationgetstudent obj211 = new doubleverificationgetstudent();
            UserLogin objlogin = new UserLogin();
            string ApplicationNO = ClsLanguage.GetCookies("NBApplicationNo").ToString();
            var obj = objlogin.GetStudents_ForDoubleVerification300Airpay(obj211, ApplicationNO);

            

            if (obj.Count > 0)
            {
                DoubleVerificationAirpay_300loop();
            }
            obj = objlogin.RegistrationVerification300EaseBuss(obj211, ApplicationNO);

            if (obj.Count > 0)
            {
                DoubleVerificationURlExamEasebuzz();
            }
            FeesSubmit stlogin = new FeesSubmit();
            Commn_master com = new Commn_master();
            AcademicSession ad = new AcademicSession();
            int sessionid = ad.GetAcademiccurrentSession().ID;
            BL_PrintApplication PritApp = new BL_PrintApplication();
            var app = ClsLanguage.GetCookies("NBApplicationNo");
            Login objl22 = new Login();
            StudentLogin objs = new StudentLogin();
            objl22 = objs.BasicDetail(app);
            // spot admission
            //if (objl22.IsFeeSubmit_spot == 1)
            //{
            //    return RedirectToAction("FeesSubmit_spot");
            //}

            var obj1 = PritApp.GetAppLicationDataAdmin(objl22.Id);

            //if (objl22.Id == 53770 || objl22.Id == 56145 || objl22.Id == 56544 || objl22.Id == 60408 || objl22.Id == 53350 || objl22.Id == 53367 || objl22.Id == 58239 || objl22.Id == 60550 || objl22.Id == 56043 || objl22.Id == 64387 || objl22.Id == 64386 || objl22.Id == 55598 || objl22.Id == 55835 || objl22.Id == 59996 || objl22.Id == 56008 || objl22.Id == 58002)
            //{
            //    ViewBag.check_admissionopen = true;
            //}
            //else
            //{
            ViewBag.check_admissionopen = com.check_admission_open(ad.GetAcademiccurrentSession().ID, Convert.ToInt32(CommonSetting.Commonid.Educationtype));

            //}
            //ViewBag.check_admissionopen = com.check_admission_open(ad.GetAcademiccurrentSession().ID, Convert.ToInt32(CommonSetting.Commonid.Educationtype));
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
            //if (subdoc.qlist.Count == 0)
            //{
            //    fee.Status = false;
            //    TempData["msgfees"] = "Please upload your Document  !!!";
            //    return RedirectToAction("FeesSubmit");
            //}
            BL_PrintApplication obrecrei = new BL_PrintApplication();
            AcademicSession ac = new AcademicSession();
            var objrecritiny = obrecrei.CheckStudentAdmission(ac.GetAcademiccurrentSession().ID);
            if (objrecritiny.Status == true)
            {
                if (lo.session != 39)
                {
                    return RedirectToAction("FeesSubmit", "Home");
                }
            }
            Commn_master com = new Commn_master();
            AcademicSession ad = new AcademicSession();
            //if (lo.Id == 53770 || lo.Id == 56145 || lo.Id == 56544 || lo.Id == 60408 || lo.Id == 53350 || lo.Id == 53367 || lo.Id == 58239 || lo.Id == 60550 || lo.Id == 56043 || lo.Id == 64387 || lo.Id == 64386 || lo.Id == 55598 || lo.Id == 55835 || lo.Id == 59996 || lo.Id == 56008 || lo.Id == 58002)
            //{
            //    ViewBag.check_admissionopen = true;
            //}
            //else
            //{
            ViewBag.check_admissionopen = com.check_admission_open(ac.GetAcademiccurrentSession().ID, Convert.ToInt32(CommonSetting.Commonid.Educationtype));

            //}
            //ViewBag.check_admissionopen = com.check_admission_open(ad.GetAcademiccurrentSession().ID, Convert.ToInt32(CommonSetting.Commonid.Educationtype));
            if (lo.CastCategory == 0)
            {
                fee.Status = false;
                TempData["msgfees"] = "Please upload your Basic Details !!!";
                return RedirectToAction("FeesSubmit");
            }
            if (lo.EducationType == Convert.ToInt32(CommonSetting.Commonid.Educationtype))
            {
                var list10 = list.Where(m => m.QualicationType == Convert.ToInt32(CommonSetting.commQualification.Ten)).ToList();
                if (list10.Count == 0)
                {
                    fee.Status = false;
                    TempData["msgfees"] = "Please upload your Secondary Board qualification certificate !!!";
                    return RedirectToAction("FeesSubmit");
                }
            }
            if (lo.EducationType == Convert.ToInt32(CommonSetting.Commonid.Educationtype))
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
                if (list12.FirstOrDefault().Percentage >= 100)
                {
                    fee.Status = false;
                    TempData["msgfees"] = "Please correct your  Intermediate qualification Aggregate Percentage ,  you have filled  100 %  !!!";
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
                if (lo.session != 39)
                {

                    if (Choicesubjectlist.Count < 1)
                    {
                        fee.Status = false;
                        TempData["msgfees"] = "Atleast Three choices save from subject and college !!!";
                        return RedirectToAction("FeesSubmit");
                    }
                }
                FeesSubmit stlogin = new FeesSubmit();
                // Commn_master com = new Commn_master();
                // AcademicSession ad = new AcademicSession();
                int sessionid = ad.GetAcademiccurrentSession().ID;
                BL_PrintApplication PritApp = new BL_PrintApplication();
                var app = ClsLanguage.GetCookies("NBApplicationNo");
                Login objl22 = new Login();
                StudentLogin objs = new StudentLogin();
                objl22 = objs.BasicDetail(app);
                BL_StreamMaster stream = new BL_StreamMaster();
                // if (objl22.IsFeeSubmit== 0)
                // for spot admission
                //ViewBag.subsidiarySave = stream.checkspotsubsidiarySave(objl22.Id).status;
                //if (ViewBag.subsidiarySave == false)
                //{
                //   // FeesSubmit stlogin1 = new FeesSubmit();
                //   // stlogin1.Status = false;
                //    TempData["msgfees"] = "PLease Fill Subsidiary Subjects!!!";
                //    return RedirectToAction("FeesSubmit");
                //}
                //var soptresult = stream.checkspotadmissionEntry(objl22.Id);
                //if (soptresult.status == false)
                //{
                //    TempData["msgfees"] = "Due to overtime ,please Select another college and subjects from Spot admission !!!";
                //    return RedirectToAction("FeesSubmit");
                //}

                if (objl22.IsFeeSubmit == 0)
                {
                    stlogin.Status = false;
                    //stlogin.Feessub();
                    // stlogin.FeessubStudenttest();
                    // TempData["msgfees"] = "Fees Submitted Successfully !!!";
                    //return RedirectToAction("FeesSubmit");
                    //if (ClsLanguage.GetCookies("NBApplicationNo") == "MU12201233")
                    //{ 
                    return RedirectToAction("SelectPaymentGetway");
                    //}
                    //sTempData["msgfees"] = "Please Try Tomorrow with new paymentgetway..";
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

        public JsonResult Checkfee(string qual = "")
        {
            ElegibilityCreteria ob = new ElegibilityCreteria();
            string ApplicationID = ClsLanguage.GetCookies("NBApplicationNo");

            if (qual != "")
            {
                var qualification = Convert.ToInt32(qual);
                var obj = ob.getdetail(ApplicationID, qualification);
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
            else
            {
                ob.Msg = "Please select qualification Value";
                return Json(ob, JsonRequestBehavior.AllowGet);
            }

            //return View(obj);

        }

        [HttpPost]
        public ActionResult PrintApplicationCall()
        {
            BL_PrintApplication PritApp = new BL_PrintApplication();
            var obj = PritApp.GetAppLicationData();
            //return View(obj);
            return Json(obj, JsonRequestBehavior.AllowGet);
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
        [HttpPost]
        public ActionResult PrintAllReport()
        {
            return View();
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


        //public JsonResult AddNewQualification(StudentAdmissionQualification ob)
        //{
        //    StudentAdmissionQualification obj = new StudentAdmissionQualification();
        //    if (ob.Percentage == 0)
        //    {
        //        return Json("Please Fill Aggregate Percentage ,Or Fill Again form !!", JsonRequestBehavior.AllowGet);
        //    }
        //    //if (ob.file == null)
        //    //{
        //    //    StudentAdmissionQualification obfileer = new StudentAdmissionQualification();
        //    //    obfileer.Status = false;
        //    //    obfileer.Msg = "Please Refresh your Browsers !!";
        //    //    return Json(obfileer, JsonRequestBehavior.AllowGet);
        //    //}
        //    BL_PrintApplication obrecrei= new BL_PrintApplication();
        //    AcademicSession ac = new AcademicSession();
        //    var objrecritiny = obrecrei.CheckStudentAdmission(ac.GetAcademiccurrentSession().ID);
        //    if (objrecritiny.Status == true)
        //    {
        //        return Json("you have already take a admission !!", JsonRequestBehavior.AllowGet);
        //    }

        //    try
        //    {
        //        ob.ApplicationNo = ClsLanguage.GetCookies("NBApplicationNo");
        //        ob.SID = Convert.ToInt32(ClsLanguage.GetCookies("NBStID"));
        //        ob.session = Convert.ToInt32(ClsLanguage.GetCookies("NBSission"));

        //        if (ob.ID > 0)
        //        {
        //            if (ob.FileURl != null)
        //            {
        //                var PicName = ob.FileURl;

        //                MemoryStream file = this.Base64ToImage(ob);
        //                string[] str = ob.FileURl.Split('.');
        //                string ext = str.LastOrDefault();
        //                string name = System.DateTime.Now.ToString("dd_MM_yyyy_HH:mm:ss") + "_Qualification_" + ob.ApplicationNo + "." + ext;
        //                string myBucketName = "mungeruniversity"; //your s3 bucket name goes here  
        //                string s3DirectoryName = "Student/Document";
        //                string s3FileName = @name;
        //                bool a;
        //                ob.FileURl = name;
        //                AmazonUploader myUploader = new AmazonUploader();
        //                a = myUploader.sendMyFileToS3(file, myBucketName, s3DirectoryName, s3FileName);
        //                //ob.ID = Convert.ToInt32(EncriptDecript.Decrypt(ob.EncriptedID));
        //                var result = obj.SaveQualificationDetails(ob);
        //                //var str1 = result.Msg;
        //                return Json(result, JsonRequestBehavior.AllowGet);

        //            }
        //            else
        //            {
        //                ob.FileURl = ob.hfile;
        //                //ob.ID = Convert.ToInt32(EncriptDecript.Decrypt(ob.EncriptedID));
        //                var result = obj.SaveQualificationDetails(ob);
        //                //var str1 = result.Msg;
        //                return Json(result, JsonRequestBehavior.AllowGet);

        //            }
        //        }


        //        else
        //        {

        //            MemoryStream file = this.Base64ToImage(ob);
        //            string[] str = ob.FileURl.Split('.');
        //            string ext = str.LastOrDefault();
        //            string name = System.DateTime.Now.ToString("dd_MM_yyyy_HH:mm:ss") + "_Qualification_" + ob.ApplicationNo + "." + ext;
        //            string myBucketName = "mungeruniversity"; //your s3 bucket name goes here  
        //            string s3DirectoryName = "Student/Document";
        //            string s3FileName = @name;
        //            bool a;
        //            ob.FileURl = name;
        //            AmazonUploader myUploader = new AmazonUploader();
        //            a = myUploader.sendMyFileToS3(file, myBucketName, s3DirectoryName, s3FileName);

        //            var result = obj.SaveQualificationDetails(ob);
        //            //var str1 = result.Msg;
        //            return Json(result, JsonRequestBehavior.AllowGet);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "Student AddNewQualification Method", ob.ApplicationNo);
        //    }


        //    return Json("error", JsonRequestBehavior.AllowGet);
        //}

        //public JsonResult AddNewQualification(string id = "")
        //{
        //    StudentAdmissionQualification ob = new StudentAdmissionQualification();
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
        //        doc.session = 39;
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


        //        result = ob.SaveQualificationDetailsForOldStudent(doc);
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
        public JsonResult AddNewQualification(string id = "", string qualiID = "")
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
                doc.paperTotalMarks = Request.Form["paperTotalMarks"] == "" ? "0" : Request.Form["paperTotalMarks"];
                doc.paperMarksObtain = Request.Form["paperMarksObtain"];
                doc.sublist = Request.Form["sublist"];
                doc.subper = Request.Form["subper"];
                doc.TotalMarks = Request.Form["TotalMarks"];
                doc.MarksObtain = Request.Form["MarksObtain"];
              

                doc.SubID = Request.Form["SubID"];
                doc.boardtype = Convert.ToInt32((Request.Form["boardtype"]));
                doc.hfile = Request.Form["hfile"];
                doc.marksType = Request.Form["marksType"];
                DataLayer.Login objl = new DataLayer.Login();
                StudentLogin objs = new StudentLogin();
                var sublistarr = doc.sublist.Split(',');
                var subperarr = doc.subper.Split(',');
                var TotalMarksarr = doc.TotalMarks.Split(',');
                var MarksObtainarr = doc.MarksObtain.Split(',');
                var SubIDarr = doc.SubID.Split(',');

                if (doc.Percentage == 0)
                {
                    StudentAdmissionQualification doc1 = new StudentAdmissionQualification();
                    doc1.Msg = "Please Fill Aggregate Percentage ,Or Fill Again form !!";
                    return Json(doc1, JsonRequestBehavior.AllowGet);
                }
                //if (doc.paperTotalMarks == "" || doc.paperTotalMarks == "0")
                //{
                //    StudentAdmissionQualification doc1 = new StudentAdmissionQualification();
                //    doc1.Msg = "Please Fill Total Paper Marks  ,Or Fill Again form !!";
                //    return Json(doc1, JsonRequestBehavior.AllowGet);
                //}
                if (doc.paperMarksObtain == "" || doc.paperMarksObtain == "0")
                {
                    StudentAdmissionQualification doc1 = new StudentAdmissionQualification();
                    doc1.Msg = "Please Fill Aggregate Total Obtain Marks  ,Or Fill Again form !!";
                    return Json(doc1, JsonRequestBehavior.AllowGet);
                }
                //if (Convert.ToDecimal(doc.paperMarksObtain) > Convert.ToDecimal(doc.paperTotalMarks))
                //{
                //    StudentAdmissionQualification doc1 = new StudentAdmissionQualification();
                //    doc1.Msg = "Paper Obtain Marks  should be less Paper Total Marks  ,Or Fill Again form !!";
                //    return Json(doc1, JsonRequestBehavior.AllowGet);
                //}
                var EnID = (ClsLanguage.GetCookies("NBApplicationNo"));
                objl = objs.BasicDetail((EnID));
                var resultyear = doc.Checkpassingyear(doc.PassingYear, objl.Id, doc.ID.ToString());
                if (resultyear.Status)
                {
                    StudentAdmissionQualification doc1 = new StudentAdmissionQualification();
                    doc1.Msg = resultyear.Msg;
                    return Json(doc1, JsonRequestBehavior.AllowGet);
                }

                if (doc.QualicationType != 1)
                {


                    if (doc.QualicationType == 2)
                    {
                        if (sublistarr.Length < 5)
                        {
                            StudentAdmissionQualification doc1 = new StudentAdmissionQualification();
                            doc1.Msg = "Please Fill minimum five Subject ,Or Fill Again form !!";
                            return Json(doc1, JsonRequestBehavior.AllowGet);
                        }
                        if (subperarr.Length < 5)
                        {
                            StudentAdmissionQualification doc1 = new StudentAdmissionQualification();
                            doc1.Msg = "Please Fill minimum five Subject ,Or Fill Again form !!";
                            return Json(doc1, JsonRequestBehavior.AllowGet);
                        }
                        if (TotalMarksarr.Length < 5)
                        {
                            StudentAdmissionQualification doc1 = new StudentAdmissionQualification();
                            doc1.Msg = "Please Fill minimum five Subject ,Or Fill Again form !!";
                            return Json(doc1, JsonRequestBehavior.AllowGet);
                        }
                        if (MarksObtainarr.Length < 5)
                        {
                            StudentAdmissionQualification doc1 = new StudentAdmissionQualification();
                            doc1.Msg = "Please Fill minimum five Subject ,Or Fill Again form !!";
                            return Json(doc1, JsonRequestBehavior.AllowGet);
                        }
                        if (SubIDarr.Length < 5)
                        {
                            StudentAdmissionQualification doc1 = new StudentAdmissionQualification();
                            doc1.Msg = "Please Fill minimum five Subject ,Or Fill Again form !!";
                            return Json(doc1, JsonRequestBehavior.AllowGet);
                        }
                    }
                    if (doc.QualicationType == 1 || doc.QualicationType == 0)
                    {
                        if (sublistarr.Length < 6)
                        {
                            StudentAdmissionQualification doc1 = new StudentAdmissionQualification();
                            doc1.Msg = "Please Fill minimum Six Subject ,Or Fill Again form !!";
                            return Json(doc1, JsonRequestBehavior.AllowGet);
                        }
                        if (subperarr.Length < 6)
                        {
                            StudentAdmissionQualification doc1 = new StudentAdmissionQualification();
                            doc1.Msg = "Please Fill minimum Six Subject ,Or Fill Again form !!";
                            return Json(doc1, JsonRequestBehavior.AllowGet);
                        }
                        if (TotalMarksarr.Length < 6)
                        {
                            StudentAdmissionQualification doc1 = new StudentAdmissionQualification();
                            doc1.Msg = "Please Fill minimum Six Subject ,Or Fill Again form !!";
                            return Json(doc1, JsonRequestBehavior.AllowGet);
                        }
                        if (MarksObtainarr.Length < 6)
                        {
                            StudentAdmissionQualification doc1 = new StudentAdmissionQualification();
                            doc1.Msg = "Please Fill minimum Six Subject ,Or Fill Again form !!";
                            return Json(doc1, JsonRequestBehavior.AllowGet);
                        }
                        if (SubIDarr.Length < 6)
                        {
                            StudentAdmissionQualification doc1 = new StudentAdmissionQualification();
                            doc1.Msg = "Please Fill minimum Six Subject ,Or Fill Again form !!";
                            return Json(doc1, JsonRequestBehavior.AllowGet);
                        }

                    }

                }
                decimal aggper = Convert.ToDecimal(doc.paperMarksObtain);
                decimal totalm = Convert.ToDecimal(doc.paperTotalMarks);
                // int count = 0;
                int SubjectExtraID = 0;
                if (sublistarr.Length > 6)
                {
                    SubjectExtraID = 1;
                }
                var Bper = 0.0;// 12th  subject of biology
                var Mper = 0.0;// 12th  subject of Math 
                for (var k = 0; k < sublistarr.Length; k++)
                {
                    if (sublistarr[k] == "12")// 12 is Master table of 12 table id in stream science 12	MATHEMATICS
                    {
                        Mper = Convert.ToInt32(MarksObtainarr[k]);
                    }
                    if (sublistarr[k] == "33")// 33 is Master table of 12 table id in stream science 33	BIOLOGY
                    {
                        Bper = Convert.ToInt32(MarksObtainarr[k]);
                    }
                }
                var Cper = 0;
                if (Bper > Mper)
                {
                    Cper = 12;
                }
                else
                {
                    Cper = 33;
                }
                //if (doc.QualicationType == Convert.ToInt32(CommonSetting.commQualification.Science12))
                //{

                //    if (SubjectExtraID != null)
                //    {
                //        if (SubjectExtraID != 0)
                //        {

                //            for (var i = 0; i < subperarr.Length; i++)
                //            {
                //                if (Convert.ToDecimal(subperarr[i]) != Cper)
                //                {
                //                    if (MarksObtainarr[i] != "")
                //                    {
                //                        //count++;
                //                        aggper = aggper + Convert.ToDecimal(MarksObtainarr[i]);
                //                        totalm = totalm + Convert.ToDecimal(TotalMarksarr[i]);
                //                    }
                //                }
                //            }
                //        }
                //        else
                //        {

                //            for (var i = 0; i < 6; i++)
                //            {
                //                if (MarksObtainarr[i] != "")
                //                {
                //                    // count++;
                //                    aggper = aggper + Convert.ToDecimal(MarksObtainarr[i]);
                //                    totalm = totalm + Convert.ToDecimal(TotalMarksarr[i]);
                //                }
                //            }
                //        }
                //    }
                //    else
                //    {

                //        for (var i = 0; i < 6; i++)
                //        {
                //            if (MarksObtainarr[i] != "")
                //            {
                //                //count++;
                //                aggper = aggper + Convert.ToDecimal(MarksObtainarr[i]);
                //                totalm = totalm + Convert.ToDecimal(TotalMarksarr[i]);
                //            }
                //        }
                //    }





                //}
                //else
                //{
                //    for (var i = 0; i < 6; i++)
                //    {
                //        if (MarksObtainarr[i] != "")
                //        {
                //            // count++;
                //            aggper = aggper + Convert.ToDecimal(MarksObtainarr[i]);
                //            totalm = totalm + Convert.ToDecimal(TotalMarksarr[i]);
                //        }
                //    }
                //}

                //decimal final = aggper * 100 / totalm;
                //doc.Percentage = final;
                doc.SID = objl.Id;
                doc.ApplicationNo = objl.ApplicationNo;
                doc.session = objl.session;
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
                                string directoryPath = Server.MapPath("~/uploads/Student/StudentDocument");
                                string fileName = System.DateTime.Now.ToString("dd_MM_yyyy_HH_mm_ss") + "_StudentDocument_" + objl.FirstName + @name;
                                fileName = fileName.Replace(" ", "");
                                if (!Directory.Exists(directoryPath))
                                {
                                    Directory.CreateDirectory(directoryPath);
                                }
                                string localPath = Path.Combine(directoryPath, fileName);
                                fileUpload.SaveAs(localPath);
                                try {
                                    int index = localPath.IndexOf("uploads");
                                    if (index != -1)
                                    {
                                        string relativePath = localPath.Substring(index);
                                        doc.FileURl = relativePath;
                                    }
                                }
                                catch
                                {
                                    doc.FileURl = localPath;
                                }                                                            
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
                var insertby = ClsLanguage.GetCookies("ENNBStID");
                if (insertby != "")
                {
                    doc.InsertedBy = Convert.ToInt32(EncriptDecript.Decrypt(insertby));
                }
                //  return Json(null, JsonRequestBehavior.AllowGet);
                if (objl.session == 39)
                {
                    result = ob.SaveQualificationDetailsForOldStudent(doc);
                }
                else
                {
                    result = ob.SaveQualificationDetails(doc);
                }
                List<StudentPreviousQualification> mem = new List<StudentPreviousQualification>();
                if (doc.QualicationType != 1)
                {
                    if (objl.prevoiusboardid == 1 || objl.prevoiusboardid == 0 || objl.prevoiusboardid == 2)
                    {
                        CommonMethod.Addnewqulification(null, HttpContext.Request.Url.AbsolutePath, "Add new qualification Add", "Add/Edit Value :-" + doc.ID + " :(sublist)sublistarr.Length :-" + sublistarr.Length + ":" + jsonstring);
                        try
                        {
                            for (int i = 0; i < sublistarr.Length; i++)
                            {
                                obj.Insertsingle(SubIDarr[i], result.ScopeIdentity, sublistarr[i], subperarr[i], TotalMarksarr[i], MarksObtainarr[i]);
                            }
                        }
                        catch (Exception ex)
                        {
                            CommonMethod.Addnewqulification(null, "error aa rahi hai");
                            //CommonMethod.Addnewqulification(ex, HttpContext.Request.Url.AbsolutePath, "Add new qualification Add", "Add/Edit Value :-" + doc.ID + " :sublistarr.Length :-" + sublistarr.Length + ":" + jsonstring);

                        }
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
        //CheckSubjectDetail
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

        //-- pritam work 01/04/2019
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

        [HttpPost]

        public JsonResult savest_Susidiarychoicesubject_spot(string Subsidiary1 = "", string Susidiary2 = "", string Compulsory1 = "", string Compulsory2 = "")
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

        public JsonResult Changechoicesubject_spot()
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
        public JsonResult savest_choicesubject(string hounors_subjectidlist, string collegeidlist, string Subsidiary1_subjectidlist="", string Subsidiary2_subjectidlist="", string Compulsory1_subjectidlist="", string Compulsory2_subjectidlist="")
        {
            Student_Admission_Choicesubject obj = new Student_Admission_Choicesubject();
            var ApplicationNo = ClsLanguage.GetCookies("NBApplicationNo");
            Login objl = new Login();
            StudentLogin objs = new StudentLogin();
            BL_PrintApplication obrecrei = new BL_PrintApplication();
            AcademicSession ac = new AcademicSession();
            objl = objs.BasicDetail(ApplicationNo);
            var objrecritiny = obrecrei.CheckStudentAdmission(ac.GetAcademiccurrentSession().ID);

            ViewBag.Statusrecruitny = false;
            if (objrecritiny.Status == true)
            {
                if (objl.session == 41)
                {
                    if (objrecritiny.IsDocVerify == 2)
                    {

                    }
                    else
                    {
                        ViewBag.Statusrecruitny = objrecritiny.Status;
                    }
                }
            }
            if (ViewBag.Statusrecruitny == true)
            {
                return Json(new { data = new Student_Admission_Choicesubject(), success = true });
            }
            try
            {

                var sublistarr = hounors_subjectidlist.Split(',');
                if (sublistarr.Length - 1 < 1)
                {
                    Student_Admission_Choicesubject doc1 = new Student_Admission_Choicesubject();
                    doc1.Msg = "Please Fill minimum Three Choice ,Or Fill Again form !!";
                    doc1.Status = false;
                    return Json(new { data = doc1, success = false });
                }


                obj.SID = objl.Id;
                obj.hounors_subjectidlist = hounors_subjectidlist;
                obj.collegeidlist = collegeidlist;
                obj.Subsidiary1_subjectidlist = Subsidiary1_subjectidlist;
                obj.Subsidiary2_subjectidlist = Subsidiary2_subjectidlist;
                obj.Compulsory1_subjectidlist = Compulsory1_subjectidlist;
                obj.Compulsory2_subjectidlist = Compulsory2_subjectidlist;
                CommonMethod.Addnewqulification(null, HttpContext.Request.Url.AbsolutePath, "Saving subject Choice ", ClsLanguage.GetCookies("NBApplicationNo") + "   hounors_subjectidlist: " + hounors_subjectidlist + "    collegeidlist:" + collegeidlist + "  Subsidiary1_subjectidlist:" + Subsidiary1_subjectidlist + "   Subsidiary2_subjectidlist:" + Subsidiary2_subjectidlist + "     Compulsory1_subjectidlist:" + Compulsory1_subjectidlist + "   Compulsory2_subjectidlist:" + Compulsory2_subjectidlist);
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
            BL_PrintApplication obrecrei = new BL_PrintApplication();
            AcademicSession ac = new AcademicSession();
            objl = objs.BasicDetail(ApplicationNo);
            var objrecritiny = obrecrei.CheckStudentAdmission(ac.GetAcademiccurrentSession().ID);
            ViewBag.Statusrecruitny = false;
            if (objrecritiny.Status == true)
            {
                if (objl.session == 41)
                {
                    if (objrecritiny.IsDocVerify == 2)
                    {

                    }
                    else
                    {
                        ViewBag.Statusrecruitny = objrecritiny.Status;
                    }
                }
            }
            if (ViewBag.Statusrecruitny == true)
            {
                return Json(new { data = new Student_Admission_Choicesubject(), success = true });
            }
            try
            {

                var result = obj.againfillform(objl.Id, objl.session);

                return Json(new { data = result, success = true });
            }
            catch (Exception ex)
            {
                CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "Saving subject Choice ", ClsLanguage.GetCookies("NBApplicationNo"));
                return Json(new { data = obj, success = true });
            }

        }

        public ActionResult NewSubject(int id = 0)
        {
            // return RedirectToAction("SelectSubject", "Home");
            StudentLogin objs = new StudentLogin();
            AcademicSession ac = new AcademicSession();
            StudentAdmissionQualification obj = new StudentAdmissionQualification();
            var ApplicationNo = ClsLanguage.GetCookies("NBApplicationNo");
            var statusdate = ac.getcutdate().status;
            if (statusdate == false)
            {
                return RedirectToAction("Index", "Home");
            }
            Login objl = new Login();

            objl = objs.BasicDetail(ApplicationNo);

            ViewBag.IsFeeSubmit_spot = objl.IsFeeSubmit_spot;
            ViewBag.IsFeeSubmit = objl.IsFeeSubmit;
            ViewBag.Ismannualadmission = objl.Ismannualadmission;

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
                    // ViewBag.subsidiary1 = stream.getcollegesubjects(objl.Id, 11, collegeid, subjectid); //old
                    if (objl.CourseCategory == 3)
                    {
                        ViewBag.subsidiary1 = stream.getcollegesubjects(objl.Id, 25, collegeid, subjectid);//bcom
                    }
                    else if (objl.CourseCategory == 2)
                    {
                        ViewBag.subsidiary1 = stream.getcollegesubjects(objl.Id, 26, collegeid, subjectid);//bsc
                    }
                    else
                    {
                        ViewBag.subsidiary1 = stream.getcollegesubjects(objl.Id, 28, collegeid, subjectid);//ba
                    }


                    ViewBag.subcomposition1 = stream.getcollegesubjects(objl.Id, 13, collegeid);
                    ViewBag.subsidiarySave = stream.checkspotsubsidiarySave(objl.Id).status;
                }

                // QualiSubject_bind(objl.previous_qua_id);
                // Subject_bind(objl.previous_qua_id);
            }
            //FeesSubmit log = new FeesSubmit();
            //var listgeo = log.Showgeography().ToList();
            //bool alreadyExist = listgeo.Contains(objl.Id);
            //ViewBag.editrecoer = alreadyExist;
            StudentAdmissionQualification ob = new StudentAdmissionQualification();
            List<StudentPreviousQualification> list = new List<StudentPreviousQualification>();
            List<StudentAdmissionQualification> list11 = ob.GetQualifiationByApplication(ApplicationNo);
            var tuple = new Tuple<StudentAdmissionQualification, List<StudentPreviousQualification>, Login>(obj, list, objl);

            if (objl.EducationType == Convert.ToInt32(CommonSetting.Commonid.Educationtype))
            {
                if (objl.CastCategory == 0)
                {

                    TempData["selectsubmsg"] = "Please upload your Basic Details !!!";
                    // return RedirectToAction("newSubject");
                    return View(tuple);
                }

                if (objl.IsFeeSubmit == 0)
                {

                    TempData["selectsubmsg"] = " Regsitration Fees not Submit !!!";
                    // return RedirectToAction("newSubject");
                    return View(tuple);
                }
                var list10 = list11.Where(m => m.QualicationType == Convert.ToInt32(CommonSetting.commQualification.Ten)).ToList();
                if (list10.Count == 0)
                {

                    TempData["selectsubmsg"] = "Please upload your Secondary Board qualification certificate !!!";
                    //  return RedirectToAction("newSubject");
                    return View(tuple);
                }
                var list12 = list11.Where(m => m.QualicationType == Convert.ToInt32(CommonSetting.commQualification.Art12) || m.QualicationType == Convert.ToInt32(CommonSetting.commQualification.Science12) || m.QualicationType == Convert.ToInt32(CommonSetting.commQualification.Comm12) || m.QualicationType == Convert.ToInt32(CommonSetting.commQualification.diploma)).ToList();
                if (list12.Count == 0)
                {
                    TempData["selectsubmsg"] = "Please upload your Intermediate qualification certificate !!!";
                    // return RedirectToAction("newSubject");
                    return View(tuple);
                }
                if (list12.FirstOrDefault().Percentage >= 100)
                {
                    TempData["selectsubmsg"] = "Please correct your  Intermediate qualification Aggregate Percentage ,  you have filled  100 %  !!!";
                    // return RedirectToAction("newSubject");
                    return View(tuple);
                }
            }
            Commn_master com = new Commn_master();
            ViewBag.check_admissionopen = "";// com.check_admission_open(ac.GetAcademiccurrentSession().ID, Convert.ToInt32(CommonSetting.Commonid.Educationtype));
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
            // List<SelectListItem> ob = new List<SelectListItem>();
            //for (int i = 1995; i <= 2019; i++)
            //{
            //    ob.Add(new SelectListItem { Text = i.ToString(), Value = i.ToString() });
            //}
            //ViewBag.year = ob;

            //for (int i = 0; i < 8; i++)
            //{
            //    list.Add(new StudentPreviousQualification());
            //}
            StudentPreviousQualification StudentPrevious = new StudentPreviousQualification();

            //ViewBag.percentage = StudentPrevious.getqualify_percentage(objl.CourseCategory, objl.issame_stream);
            var countlist = StudentPrevious.GetSubjectPercentageData(objl.ApplicationNo);
            BL_PrintApplication obrecrei = new BL_PrintApplication();
            var objrecritiny = obrecrei.CheckStudentAdmission(ac.GetAcademiccurrentSession().ID);
            //ViewBag.Statusrecruitny = false;
            //if (objrecritiny.Status == true)
            //{
            //    ViewBag.Statusrecruitny = objrecritiny.Status;
            //}
            ViewBag.Statusrecruitny = false;
            if (objrecritiny.Status == true)
            {
                if (objl.session == 42)
                {
                    if (objrecritiny.IsDocVerify == 2)
                    {

                    }
                    else
                    {
                        ViewBag.Statusrecruitny = objrecritiny.Status;
                    }
                }
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
        [VerifyUrlFilterAdminAttribute]
        public ActionResult SelectSubject(int id = 0)
        {

            StudentAdmissionQualification obj = new StudentAdmissionQualification();
            var ApplicationNo = ClsLanguage.GetCookies("NBApplicationNo");
            Login objl = new Login();
            StudentLogin objs = new StudentLogin();
            AcademicSession ac = new AcademicSession();
            objl = objs.BasicDetail(ApplicationNo);
            //for Spot Admission
            //if (objl.session == 41)
            //    {
            //        return RedirectToAction("NewSubject");
            //    }
            // For Slide Up Admission
            //if (objl.session == 41)
            //{
            //    return RedirectToAction("SlideUp");
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
            ViewBag.IsSubmit = objl.IsFeeSubmit;
            ViewBag.IsFeeSubmit_spot = objl.IsFeeSubmit_spot;
            Commn_master com = new Commn_master();
            ViewBag.check_admissionopen = com.check_admission_open(ac.GetAcademiccurrentSession().ID, Convert.ToInt32(CommonSetting.Commonid.Educationtype));

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
            StudentPreviousQualification StudentPrevious = new StudentPreviousQualification();
            var tuple = new Tuple<StudentAdmissionQualification, List<StudentPreviousQualification>, Login>(obj, list, objl);
            ViewBag.percentage = StudentPrevious.getqualify_percentage(objl.CourseCategory, objl.issame_stream);
            var countlist = StudentPrevious.GetSubjectPercentageData(objl.ApplicationNo);

            BL_PrintApplication obrecrei = new BL_PrintApplication();

            var objrecritiny = obrecrei.CheckStudentAdmission(ac.GetAcademiccurrentSession().ID);
            ViewBag.Statusrecruitny = false;
            //if (objrecritiny.Status == true)
            //{
            //    ViewBag.Statusrecruitny = objrecritiny.Status;
            //}
            if (objrecritiny.Status == true)
            {
                if (objl.session == 42)
                {
                    if (objrecritiny.IsDocVerify == 2)
                    {

                    }
                    else
                    {
                        ViewBag.Statusrecruitny = objrecritiny.Status;
                    }
                }
            }

            if (objl.previous_qua_id != 11)
            {
                if (countlist.Count == 0)
                {
                    // TempData["selectsubmsg"] = "Please First Upload your Qualification!!";
                    // return View(tuple);
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
            {
                result = objStream.getcollegesubjects(objl.Id, 20, id, 0, 0, 0, objl.CourseCategory);//bcom
            }
            else if (objl.CourseCategory == 2)
            {
                result = objStream.getcollegesubjects(objl.Id, 24, id, 0, 0, 0, objl.CourseCategory);//bsc
            }
            else
            {
                result = objStream.getcollegesubjects(objl.Id, 10, id, 0, 0, 0, objl.CourseCategory);//ba
            }

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
            var result = new List<BL_StreamMaster>();
            if (objl.CourseCategory == 3)
            {
                result = objStream.getcollegesubjects(objl.Id, 25, collegeid, subjectid);//bcom
            }
            else if (objl.CourseCategory == 2)
            {
                result = objStream.getcollegesubjects(objl.Id, 26, collegeid, subjectid);//bsc
            }
            else
            {
                result = objStream.getcollegesubjects(objl.Id, 28, collegeid, subjectid);//ba
            }

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
            var result = new List<BL_StreamMaster>();
            if (objl.CourseCategory == 3)
            {
                result = objStream.getcollegesubjects(objl.Id, 12, collegeid, subjectid, subsidiary1);//bcom
            }
            else if (objl.CourseCategory == 2)
            {
                result = objStream.getcollegesubjects(objl.Id, 27, collegeid, subjectid, subsidiary1);//bsc
            }
            else
            {
                result = objStream.getcollegesubjects(objl.Id, 29, collegeid, subjectid, subsidiary1);//ba
            }


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

        public JsonResult feeStatus()
        {
            StudentLogin stu = new StudentLogin();
            string ApplicationID = ClsLanguage.GetCookies("NBApplicationNo");
            Login lo = stu.BasicDetail(ApplicationID);

            StudentAdmissionQualification ob = new StudentAdmissionQualification();
            List<StudentAdmissionQualification> list = ob.GetQualifiationByApplication(ApplicationID);
            // return View(obj);
            List<QualifiationMaster> qualitypelist = ob.GetQualifiation();
            FeesSubmit fee = new FeesSubmit();
            fee.Status = true;

            DocumentUpload obj = new DocumentUpload();
            DocumentUploadList subdoc = new DocumentUploadList();
            subdoc = obj.DocumentdetailList(1, 10);
            if (subdoc.qlist.Count == 0)
            {
                fee.Status = false;
                fee.Message = "please upload your Document !!";
                return Json(fee, JsonRequestBehavior.AllowGet);
            }

            if (lo.EducationType == Convert.ToInt32(CommonSetting.Commonid.Educationtype))
            {
                var list10 = list.Where(m => m.QualicationType == Convert.ToInt32(CommonSetting.commQualification.Ten)).ToList();
                if (list10.Count == 0)
                {
                    fee.Status = false;
                    fee.Message = "please upload your Secondary Board qualification certificate";
                    return Json(fee, JsonRequestBehavior.AllowGet);
                }
            }
            if (lo.EducationType == Convert.ToInt32(CommonSetting.Commonid.Educationtype))
            {
                var list10 = list.Where(m => m.QualicationType == Convert.ToInt32(CommonSetting.commQualification.Ten)).ToList();
                if (list10.Count == 0)
                {
                    fee.Status = false;
                    fee.Message = "please upload your Secondary Board qualification certificate";
                    return Json(fee, JsonRequestBehavior.AllowGet);
                }
                var list12 = list.Where(m => m.QualicationType == Convert.ToInt32(CommonSetting.commQualification.Art12) || m.QualicationType == Convert.ToInt32(CommonSetting.commQualification.Science12) || m.QualicationType == Convert.ToInt32(CommonSetting.commQualification.Comm12) || m.QualicationType == Convert.ToInt32(CommonSetting.commQualification.diploma)).ToList();
                if (list12.Count == 0)
                {
                    fee.Status = false;
                    fee.Message = "please upload your Intermediate qualification certificate";
                    return Json(fee, JsonRequestBehavior.AllowGet);
                }



            }
            //if (lo.EducationType == 12)
            //{

            //    for (int i = 0; i < list.Count; i++)
            //    {
            //        for (int j = 0; j < qualitypelist.Count; j++)
            //        {
            //            if (list[i].QualicationType == 1)
            //            {
            //                fee.Status = true;
            //                //continue;
            //            }
            //            else
            //            {
            //                fee.Status = false;
            //                continue;
            //            }
            //            if (list[i].QualicationType == 2)
            //            {
            //                fee.Status = true;
            //                //continue;
            //            }
            //            else
            //            {
            //                fee.Status = false;
            //                continue;
            //            }
            //            if (list[i].QualicationType == 3)
            //            {
            //                fee.Status = true;
            //                //continue;
            //            }
            //            else
            //            {
            //                fee.Status = false;
            //                continue;
            //            }
            //            if (list[i].QualicationType == 4)
            //            {
            //                fee.Status = true;
            //                //continue;
            //            }
            //            else
            //            {
            //                fee.Status = false;
            //                continue;
            //            }
            //            if (list[i].QualicationType == 5)
            //            {
            //                fee.Status = true;
            //                // continue;
            //            }
            //            if (list[i].QualicationType == 6)
            //            {
            //                fee.Status = true;
            //                //continue;
            //            }
            //            else
            //            {
            //                fee.Status = false;
            //                continue;
            //            }

            //        }
            //    }
            //    if (list.Count < 3)
            //    {
            //        fee.Status = false;
            //    }
            //    if (fee.Status == false)
            //    {

            //        fee.Message = "please upload your Intermediate , Secondary Board and Bachelor's degree qualification certificate";
            //        return Json(fee, JsonRequestBehavior.AllowGet);
            //    }
            //}

            Student_Admission_Choicesubject Choicesubject = new Student_Admission_Choicesubject();
            var Choicesubjectlist = Choicesubject.viewst_choicesubject(lo.Id);
            if (Choicesubjectlist.Count == 0)
            {
                fee.Status = false;
                fee.Message = "Please first select choices for subject and college !!";
                return Json(fee, JsonRequestBehavior.AllowGet);
            }
            return Json(fee, JsonRequestBehavior.AllowGet);
        }
        //-- till here 

        //-- bharti work 02/04
        public ActionResult PreviousyearQualification(string id = "")
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
                ViewBag.boardtype = CommonMethod.BoradtypePrevious().Where(x => x.boardid == 1 || x.boardid == 2);
                if (objl.session == 39)
                {
                    return RedirectToAction("PreviousyearQualificationO");
                }
                else
                {

                }


                ViewBag.IsFeeSubmit_spot = objl.IsFeeSubmit_spot;
                //if (objl.previous_qua_id == 11)
                //{
                //    objl.prevoiusboardid = 3;
                //}
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
                //if (objl.Id == 53770 || objl.Id == 56145 || objl.Id == 56544 || objl.Id == 60408 || objl.Id == 53350 || objl.Id == 53367 || objl.Id == 58239 || objl.Id == 60550 || objl.Id == 56043 || objl.Id == 64387 || objl.Id == 64386 || objl.Id == 55598 || objl.Id == 55835 || objl.Id == 59996 || objl.Id == 56008 || objl.Id == 58002)
                //{
                //    ViewBag.check_admissionopen = true;
                //}
                //else
                //{
                //    ViewBag.check_admissionopen = com.check_admission_open(ac.GetAcademiccurrentSession().ID, Convert.ToInt32(CommonSetting.Commonid.Educationtype));

                //}
                ViewBag.check_admissionopen = com.check_admission_open(ac.GetAcademiccurrentSession().ID, Convert.ToInt32(CommonSetting.Commonid.Educationtype));

                BL_PrintApplication obrecrei = new BL_PrintApplication();

                var objrecritiny = obrecrei.CheckStudentAdmission(ac.GetAcademiccurrentSession().ID);
                ViewBag.Statusrecruitny = false;
                //if (objrecritiny.Status == true)
                //{
                //    ViewBag.Statusrecruitny = objrecritiny.Status;
                //}
                if (objrecritiny.Status == true)
                {
                    if (objl.session == 42)
                    {
                        if (objrecritiny.IsDocVerify == 2)
                        {

                        }
                        else
                        {
                            ViewBag.Statusrecruitny = objrecritiny.Status;
                        }
                    }
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
                            //ViewBag.Subject = "";
                            Subject_bind(objl.previous_qua_id);
                            Quali_bind(objl.EducationType, objl.previous_qua_id, "", objl.Id);
                        }
                    }
                    else
                    {
                        if (objl != null)
                        {
                            // ViewBag.Subject = "";
                            Subject_bind(objl.previous_qua_id);
                            Quali_bind(objl.EducationType, eID, "edit");
                        }
                    }
                    if (eID > 0)
                    {
                        objst = obj.GetQualifiationByID(eID);
                        objst.paperTotalMarks = objst.totalpapermasks;
                        objst.paperMarksObtain = objst.totaobtaindmasks;
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
                            //ViewBag.Subject = "";
                            Subject_bind(objl.previous_qua_id);
                            Quali_bind(objl.EducationType, objl.previous_qua_id, "", objl.Id);
                        }
                    }
                    else
                    {
                        if (objl != null)
                        {
                            //ViewBag.Subject = "";
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
                //return View();
                return RedirectToAction("PreviousyearQualification/");
            }

        }
        public PartialViewResult _AdmissionFee()
        {
            BL_PrintApplication PritApp = new BL_PrintApplication();
            var app = EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENBApplicationNo"));
            Login objl22 = new Login();
            StudentLogin objs = new StudentLogin();
            objl22 = objs.BasicDetail(app);
            var obj1 = PritApp.GetAppLicationDataAdmin(objl22.Id);
            return PartialView("_AdmissionFee", obj1);
            //  return PartialView("_AdmissionFee");
        }
        public ActionResult PreviousyearQualificationP(string id = "")
        {
            try
            {
                // return RedirectToAction("PreviousyearQualificationO");
                ElegibilityCreteria obperr = new ElegibilityCreteria();
                var ApplicationNo = ClsLanguage.GetCookies("NBApplicationNo");
                List<ElegibilityCreteria> obert = obperr.getdetailofper1(ApplicationNo);
                BL_student_formcomplete bl = new BL_student_formcomplete();
                AcademicSession ac = new AcademicSession();
                var resultcheck = bl.sp_st_check_details(ClsLanguage.GetCookies("NBApplicationNo"), ac.GetAcademiccurrentSession().ID.ToString());
                Login objl = new Login();
                ViewBag.boardtype = CommonMethod.BoradtypePrevious().Where(x => x.boardid == 1 || x.boardid == 2);
                StudentLogin objs = new StudentLogin();
                objl = objs.BasicDetail(ApplicationNo);
                if (objl.session == 39)
                { return RedirectToAction("PreviousyearQualificationO"); }
                else
                { }
                ViewBag.IsSubmit = objl.IsFeeSubmit;
                ViewBag.IsFeeSubmit_spot = objl.IsFeeSubmit_spot;
                string enID = EncriptDecript.Decrypt(id);
                int eID = 0;
                StudentAdmissionQualification obj = new StudentAdmissionQualification();
                StudentAdmissionQualification objst = new StudentAdmissionQualification();
                Commn_master com = new Commn_master();
                //if (objl.Id == 53770 || objl.Id == 56145 || objl.Id == 56544 || objl.Id == 60408 || objl.Id == 53350 || objl.Id == 53367 || objl.Id == 58239 || objl.Id == 60550 || objl.Id == 60550 || objl.Id == 56043 || objl.Id == 64387 || objl.Id == 64386 || objl.Id == 55598 || objl.Id == 55835 || objl.Id == 59996 || objl.Id == 56008 || objl.Id == 58002)
                //{
                //    ViewBag.check_admissionopen = true;
                //}
                //else
                //{
                //    ViewBag.check_admissionopen = com.check_admission_open(ac.GetAcademiccurrentSession().ID, Convert.ToInt32(CommonSetting.Commonid.Educationtype));

                //}
                ViewBag.check_admissionopen = com.check_admission_open(ac.GetAcademiccurrentSession().ID, Convert.ToInt32(CommonSetting.Commonid.Educationtype));

                BL_PrintApplication obrecrei = new BL_PrintApplication();

                var objrecritiny = obrecrei.CheckStudentAdmission(ac.GetAcademiccurrentSession().ID);
                ViewBag.Statusrecruitny = false;
                //if (objrecritiny.Status == true)
                //{
                //    ViewBag.Statusrecruitny = objrecritiny.Status;
                //}
                if (objrecritiny.Status == true)
                {
                    if (objl.session == 42)
                    {
                        if (objrecritiny.IsDocVerify == 2)
                        {

                        }
                        else
                        {
                            ViewBag.Statusrecruitny = objrecritiny.Status;
                        }
                    }
                }
                StudentPreviousQualification objp = new StudentPreviousQualification();
                List<StudentPreviousQualification> result = objp.GetSubjectPercentageData(ApplicationNo);
                //if (objl.previous_qua_id == 11)
                //{
                //    objl.prevoiusboardid = 3;
                //}
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
                            //ViewBag.Subject = "";
                            Subject_bind(objl.previous_qua_id);
                            Quali_bind(objl.EducationType, objl.previous_qua_id, "", objl.Id);
                        }
                    }
                    else
                    {
                        if (objl != null)
                        {
                            // ViewBag.Subject = "";
                            Subject_bind(objl.previous_qua_id);
                            Quali_bind(objl.EducationType, eID, "edit");
                        }
                    }
                    if (eID > 0)
                    {
                        objst = obj.GetQualifiationByID(eID);
                        objst.paperTotalMarks = objst.totalpapermasks;
                        objst.paperMarksObtain = objst.totaobtaindmasks;
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
                            //ViewBag.Subject = "";
                            Subject_bind(objl.previous_qua_id);
                            Quali_bind(objl.EducationType, objl.previous_qua_id, "", objl.Id);
                        }
                    }
                    else
                    {
                        if (objl != null)
                        {
                            //ViewBag.Subject = "";
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
                //return View();
                return RedirectToAction("PreviousyearQualification/");
            }

        }
        [VerifyUrlFilterAdminAttribute]
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
                ViewBag.boardtype = CommonMethod.BoradtypePrevious().Where(x => x.boardid == 1 || x.boardid == 2);
                StudentLogin objs = new StudentLogin();
                objl = objs.BasicDetail(ApplicationNo);
                ViewBag.IsSubmit = objl.IsFeeSubmit;
                ViewBag.IsFeeSubmit_spot = objl.IsFeeSubmit_spot;
                string enID = EncriptDecript.Decrypt(id);
                int eID = 0;
                StudentAdmissionQualification obj = new StudentAdmissionQualification();
                StudentAdmissionQualification objst = new StudentAdmissionQualification();
                Commn_master com = new Commn_master();
                //if (objl.Id == 53770 || objl.Id == 56145 || objl.Id == 56544 || objl.Id == 60408 || objl.Id == 53350 || objl.Id == 53367 || objl.Id == 58239 || objl.Id == 60550 || objl.Id == 60550 || objl.Id == 56043 || objl.Id == 64387 || objl.Id == 64386 || objl.Id == 55598 || objl.Id == 55835 || objl.Id == 59996 || objl.Id == 56008 || objl.Id == 58002)
                //{
                //    ViewBag.check_admissionopen = true;
                //}
                //else
                //{
                //ViewBag.check_admissionopen = com.check_admission_open(ac.GetAcademiccurrentSession().ID, Convert.ToInt32(CommonSetting.Commonid.Educationtype));

                //}
                StudentAdmissionQualification stad = new StudentAdmissionQualification();
                ViewBag.Qualification = stad.GetQualifiationMasterOldStudent();
                ViewBag.check_admissionopen = com.check_admission_open(ac.GetAcademiccurrentSession().ID, Convert.ToInt32(CommonSetting.Commonid.Educationtype));
                ViewBag.IsSubmit = objl.IsFeeSubmit;
                ViewBag.sessionid = objl.session;
                BL_PrintApplication obrecrei = new BL_PrintApplication();

                var objrecritiny = obrecrei.CheckStudentAdmission(ac.GetAcademiccurrentSession().ID);
                ViewBag.Statusrecruitny = false;
                if (objrecritiny.Status == true)
                {
                    ViewBag.Statusrecruitny = objrecritiny.Status;
                }
                if (objrecritiny.Status == true)
                {
                    if (objl.session == 43)
                    {
                        if (objrecritiny.IsDocVerify == 2)
                        {

                        }
                        else
                        {
                            ViewBag.Statusrecruitny = objrecritiny.Status;
                        }
                    }
                }
                StudentPreviousQualification objp = new StudentPreviousQualification();
                List<StudentPreviousQualification> result = objp.GetSubjectPercentageData(ApplicationNo);
                //if (objl.previous_qua_id == 11)
                //{
                //    objl.prevoiusboardid = 3;
                //}
                if (objl.session == 39)
                {

                }
                else
                {
                    if (objl.prevoiusboardid != 2)
                    {
                        return RedirectToAction("PreviousyearQualification/" + id, "Home");
                    }
                    if (objl.prevoiusboardid == 3)
                    {
                        return RedirectToAction("PreviousyearQualificationP/" + id, "Home");
                    }
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
                            //ViewBag.Subject = "";
                            Subject_bind(objl.previous_qua_id);
                            Quali_bind(objl.EducationType, objl.previous_qua_id, "", objl.Id);
                        }
                    }
                    else
                    {
                        if (objl != null)
                        {
                            // ViewBag.Subject = "";
                            Subject_bind(objl.previous_qua_id);
                            Quali_bind(objl.EducationType, eID, "edit");
                        }
                    }
                    if (eID > 0)
                    {
                        objst = obj.GetQualifiationByID(eID);
                        objst.paperTotalMarks = objst.totalpapermasks;
                        objst.paperMarksObtain = objst.totaobtaindmasks;
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
                            //ViewBag.Subject = "";
                            Subject_bind(objl.previous_qua_id);
                            Quali_bind(objl.EducationType, objl.previous_qua_id, "", objl.Id);
                        }
                    }
                    else
                    {
                        if (objl != null)
                        {
                            //ViewBag.Subject = "";
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
                //return View();
                return RedirectToAction("PreviousyearQualification/");
            }

        }
        //public ActionResult PreviousyearQualification(string id = "")
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
        //        string enID = EncriptDecript.Decrypt(id);
        //        int eID = 0;
        //        StudentAdmissionQualification obj = new StudentAdmissionQualification();
        //        StudentAdmissionQualification objst = new StudentAdmissionQualification();

        //        StudentPreviousQualification objp = new StudentPreviousQualification();
        //        List<StudentPreviousQualification> result = objp.GetSubjectPercentageData(ApplicationNo);


        //        List<SelectListItem> ob = new List<SelectListItem>();
        //        for (int i = 1995; i <= 2019; i++)
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
        //                    return RedirectToAction("StudentQualification", "Home");
        //                }
        //                if (objl != null)
        //                {
        //                    //ViewBag.Subject = "";
        //                    Subject_bind(objl.previous_qua_id);
        //                    Quali_bind(objl.EducationType, objl.previous_qua_id, "", objl.Id);
        //                }
        //            }
        //            else
        //            {
        //                if (objl != null)
        //                {
        //                    // ViewBag.Subject = "";
        //                    Subject_bind(objl.previous_qua_id);
        //                    Quali_bind(objl.EducationType, eID, "edit");
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
        //                    return RedirectToAction("StudentQualification", "Home");
        //                }
        //                if (objl != null)
        //                {
        //                    //ViewBag.Subject = "";
        //                    Subject_bind(objl.previous_qua_id);
        //                    Quali_bind(objl.EducationType, objl.previous_qua_id, "", objl.Id);
        //                }
        //            }
        //            else
        //            {
        //                if (objl != null)
        //                {
        //                    //ViewBag.Subject = "";
        //                    Subject_bind(objl.previous_qua_id);
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
        //        //return View();
        //        return RedirectToAction("PreviousyearQualification/");
        //    }

        //}

        public ActionResult DocumentUpload(string id = "")
        {
            DocumentUpload obj = new DocumentUpload();
            var app = ClsLanguage.GetCookies("NBApplicationNo");
            Login objl = new Login();
            StudentLogin objs = new StudentLogin();
            Commn_master com = new Commn_master();
            AcademicSession ac = new AcademicSession();
            ViewBag.check_admissionopen = com.check_admission_open(ac.GetAcademiccurrentSession().ID, Convert.ToInt32(CommonSetting.Commonid.Educationtype));
            objl = objs.BasicDetail(app);
            ViewBag.IsSubmit = objl.IsFeeSubmit;
            Docu_bindapplicationdoc();
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
        [VerifyUrlFilterAdminAttribute]
        public ActionResult StudentQualification(int id = 0)
        {

            //NBStID
            //var ID = ClsLanguage.GetCookies("NBStID");
            var ID = ClsLanguage.GetCookies("NBApplicationNo");
            Login objl = new Login();
            StudentLogin objs = new StudentLogin();
            objl = objs.BasicDetail(ID);
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
            Commn_master com = new Commn_master();

            AcademicSession ac = new AcademicSession();
            //if (objl.Id == 53770 || objl.Id == 56145 || objl.Id == 56544 || objl.Id == 60408 || objl.Id == 53350 || objl.Id == 53367 || objl.Id == 58239 || objl.Id == 60550 || objl.Id == 60550 || objl.Id == 56043 || objl.Id == 64387 || objl.Id == 64386 || objl.Id == 55598 || objl.Id == 55835 || objl.Id == 59996 || objl.Id == 56008 || objl.Id == 58002)
            //{
            //    ViewBag.check_admissionopen = true;
            //}
            //else
            //{
            //    ViewBag.check_admissionopen = com.check_admission_open(ac.GetAcademiccurrentSession().ID, Convert.ToInt32(CommonSetting.Commonid.Educationtype));

            //}
            ViewBag.check_admissionopen = com.check_admission_open(ac.GetAcademiccurrentSession().ID, Convert.ToInt32(CommonSetting.Commonid.Educationtype));
            ViewBag.sessionid = objl.session;
            BL_PrintApplication obrecrei = new BL_PrintApplication();

            var objrecritiny = obrecrei.CheckStudentAdmission(ac.GetAcademiccurrentSession().ID);
            ViewBag.Statusrecruitny = false;
            ViewBag.IsSubmit = objl.IsFeeSubmit;
            //if (objrecritiny.Status == true)
            //{
            //    ViewBag.Statusrecruitny = objrecritiny.Status;
            //}
            if (objrecritiny.Status == true)
            {
                if (objl.session == 42)
                {
                    if (objrecritiny.IsDocVerify == 2)
                    {

                    }
                    else
                    {
                        ViewBag.Statusrecruitny = objrecritiny.Status;
                    }
                }
            }
            if (list.Count != 0)
            {
                return View(list);
            }
            else
            {
                return View(list1);
            }

        }
        //-- till here
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
        public JsonResult CheckstudentforAdmission()
        {
            BL_PrintApplication ob = new BL_PrintApplication();
            var result = ob.CheckStudentAdmission();
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        //public ActionResult ApplyAdmission()
        //{
        //    string ApplicationID = ClsLanguage.GetCookies("NBApplicationNo");
        //    StudentLogin tblST = new StudentLogin();
        //    BL_PrintApplication ob = new BL_PrintApplication();
        //    AcademicSession ad = new AcademicSession();
        //    int sessionid = ad.GetAcademiccurrentSession().ID;
        //    StudentLogin STu = new StudentLogin();
        //    BL_student_formcomplete bl = new BL_student_formcomplete();
        //    var res1 = bl.CheckAdmission_details(sessionid);
        //    ViewBag.IsDocVerify = res1.IsDocVerify;
        //    ViewBag.IsAdmisApplied = res1.IsApplied;
        //    ViewBag.IsAdmissionfee = res1.IsAdmissionfee;
        //    ViewBag.IsAppliedDate = res1.IsAppliedDate;
        //    ViewBag.IsDocVerifyDate = res1.IsDocVerifyDate;
        //    var obj1 = tblST.BasicDetail(ApplicationID);
        //    if (obj1 != null)
        //    {
        //        int educationtype = obj1.EducationType;
        //        ViewBag.addmissionExtenddate = ob.CheckStudentAddmisionExtendDate(sessionid, educationtype).Status;
        //        ViewBag.addmissionExtenddateValue = ob.CheckStudentAddmisionExtendDate(sessionid, educationtype).ExtendDate;
        //        ViewBag.addmissionStartdate = ob.CheckStudentAddmisionStartDate(sessionid, educationtype).Status;
        //        ViewBag.addmissionStartdateValue = ob.CheckStudentAddmisionStartDate(sessionid, educationtype).startdate;
        //        ViewBag.IsApplied = ob.CheckStudentApplied(sessionid).Status;
        //        ViewBag.rejectreason = ob.CheckDocumentVerification(sessionid).rejectreason;
        //    }
        //    var objstu = ob.CheckStudentAdmission(sessionid);
        //    if (objstu.Status == true)
        //    {
        //        ViewBag.Status = objstu.Status;
        //        ViewBag.Course = objstu.CourseName;
        //        ViewBag.College = objstu.CollegeName;
        //        ViewBag.Stream = objstu.StreamName;
        //    }
        //    else
        //    {
        //        ViewBag.Status = false;
        //        ViewBag.Course = "";
        //        ViewBag.College = "";
        //    }
        //    BL_PrintAllRecord result = ob.AdmissionDetail(sessionid);
        //    return View(result);
        //}
        //public JsonResult ApplyForRegisteration(string Id = "")
        //{
        //    BL_PrintApplication ob = new BL_PrintApplication();
        //    //if(Id!="")
        //    //{
        //    var result = ob.UpdateAdmission(/*Convert.ToInt32(Id)*/);
        //    return Json(result, JsonRequestBehavior.AllowGet);
        //    //}
        //    //else
        //    //{
        //    //    var obj = new BL_PrintApplication();
        //    //    obj.Status = false;
        //    //    obj.Msg = "Something Wrong Happen!!!";
        //    //    return Json(obj, JsonRequestBehavior.AllowGet);
        //    //}
        //}

        public ActionResult SelectGetwayAdmissionFee()
        {
            return View();
        }
        public ActionResult AdmissionFeeSubmit()
        {
            //return RedirectToAction("Index", "Home");

            //if (HttpContext.Request.Cookies["ENB123"] == null)
            //{
            //    return RedirectToAction("Index", "Home");
            //}


            string Application = EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENBApplicationNo"));
            doubleverificationgetstudent obj211 = new doubleverificationgetstudent();
            UserLogin objlogin = new UserLogin();
            var ObjDoubleVerification = objlogin.GetStudents_ForDoubleVerificationAdmissionAirpay(obj211, Application, "AirPay");
            if (ObjDoubleVerification.Count > 0)
            {
                DoubleVerification_CollegeFeesAdmissionAirpay();
            }



            BL_PrintApplication obrecrei = new BL_PrintApplication();
            AcademicSession ac = new AcademicSession();
            var objrecritiny = obrecrei.CheckStudentAdmission(ac.GetAcademiccurrentSession().ID);
            //if (objrecritiny.Status == true)
            //{
            //    return RedirectToAction("Index", "Home");
            //}
            AdmissionFeesSubmit stlogin = new AdmissionFeesSubmit();
            AdmissionFeesSubmit obj = new AdmissionFeesSubmit();
            List<AdmissionFeesSubmit> feestruckture = new List<AdmissionFeesSubmit>();
            ViewBag.IsSubmit = obj.IsFeeSubmit;

            Commn_master com = new Commn_master();
            // ViewBag.check_admissionopen = com.check_admission_open(ac.GetAcademiccurrentSession().ID, Convert.ToInt32(CommonSetting.Commonid.Educationtype));
            string Id = ClsLanguage.GetCookies("ENNBStID");
            BL_student_formcomplete bl = new BL_student_formcomplete();
            string ApplicationID = ClsLanguage.GetCookies("NBApplicationNo");
            StudentLogin tblST = new StudentLogin();
            BL_PrintApplication ob = new BL_PrintApplication();
            int sessionid = ac.GetAcademiccurrentSession().ID;
            var obj1 = tblST.BasicDetail(ApplicationID);
            var res1 = bl.CheckAdmission_details(sessionid);
            ViewBag.isfeesubmitt = res1.isfeesubmitt;
            ViewBag.IsfeesubmitDate = res1.IsfeesubmitDate;
            ViewBag.isfeesubmitt2 = res1.isfeesubmitt2;
            ViewBag.IsfeesubmitDate2 = res1.IsfeesubmitDate2;
            ViewBag.IsAdmisApplied = res1.IsApplied;
            ViewBag.StudentYear = obj.studentyear.ToString();
            ViewBag.incomecertificate_iseligible = obj1.incomecertificate_iseligible;
            ViewBag.incomecertificate = obj1.incomecertificate;

            //if (obj1.StudentYear.ToString() == "2" || obj1.StudentYear.ToString() == "5" || obj1.StudentYear.ToString() == "8")
            //{ 


            stlogin.student_collegeform_apply(obj1.Id, obj1.session, obj1.CourseCategory, obj.Collegeid, obj.streamcategoryid, obj.studentyear, 0, 0);


            if (res1.IsApplied == false)
            {
                return RedirectToAction("Index", "Home");
            }
            if (res1.IsDocVerify == 0)
            {
                return RedirectToAction("Index", "Home");
            }
            if (res1.IsDocVerify == 2)
            {
                return RedirectToAction("Index", "Home");
            }
            if (obj1.IsFeeSubmit != 1)
            {
                return RedirectToAction("Index", "Home");
            }
            if (obj1.session == 39)
            {
                // return RedirectToAction("Index", "Home");
                if (obj1.StudentYear == 3 || obj1.StudentYear == 6 || obj1.StudentYear == 9)
                {
                    ////if (obj1.incomecertificate != "")
                    ////{
                    ////    if (obj1.incomecertificate_iseligible == 0)
                    ////    {
                    ////        return RedirectToAction("Index", "Home");
                    ////    }
                    ////}
                    //if(obj1.incomecertificate!="")
                    //{
                    //    if (obj1.incomecertificate_iseligible==0)
                    //    {
                    //        return RedirectToAction("Index", "Home");
                    //    }
                    //}
                }
                //else
                //{
                //    return RedirectToAction("Index", "Home");
                //}
            }

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
                    obj = stlogin.FeesDetails(eID);
                    //if (obj.Collegeid == 11)
                    //{
                    //    return RedirectToAction("Index", "Home");
                    //}


                    stlogin.student_collegeform_apply(obj1.Id, obj1.session, obj1.CourseCategory, obj.Collegeid, obj.streamcategoryid, obj.studentyear, 0, 0);

                    feestruckture = stlogin.FeesDetailsstructure(obj.Collegeid, obj1.CourseCategory, sessionid, obj1.CastCategory, obj.streamcategoryid, obj1.StudentYear, obj1.incomecertificate_iseligible, obj1.Gender);
                    var tuple = new Tuple<AdmissionFeesSubmit, List<AdmissionFeesSubmit>, Login>(obj, feestruckture, obj1);
                    return View(tuple);
                }
                else
                {

                    stlogin.student_collegeform_apply(obj1.Id, obj1.session, obj1.CourseCategory, obj.Collegeid, obj.streamcategoryid, obj.studentyear, 0, 0);

                    var tuple1 = new Tuple<AdmissionFeesSubmit, List<AdmissionFeesSubmit>, Login>(obj, feestruckture, obj1);
                    return View(tuple1);
                }
            }

            //}

            //else
            //{
            //    TempData["msgerror"] = "Admission Allow ony Part -2 !!";
            //    return RedirectToAction("Index", "Home");


            //}



            var tuple2 = new Tuple<AdmissionFeesSubmit, List<AdmissionFeesSubmit>, Login>(obj, feestruckture, obj1);
            return View(tuple2);
        }
        [HttpPost]
        public ActionResult AdmissionFeeSubmit(int id11 = 0, string applyaffdavit = "", string paywith0Rs = "")
        {
            //return RedirectToAction("Index", "Home");
            BL_PrintApplication obrecrei = new BL_PrintApplication();
            AcademicSession ac = new AcademicSession();

            // Double verification
            string Application = EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENBApplicationNo"));
            doubleverificationgetstudent obj211 = new doubleverificationgetstudent();
            UserLogin objlogin = new UserLogin();
            var ObjDoubleVerification = objlogin.GetStudents_ForDoubleVerificationAdmissionAirpay(obj211, Application, "AirPay");
            if (ObjDoubleVerification.Count > 0)
            {
                DoubleVerification_CollegeFeesAdmissionAirpay();
            }


            var objrecritiny = obrecrei.CheckStudentAdmission(ac.GetAcademiccurrentSession().ID);
            //if (objrecritiny.Status == true)
            //{
            //    return RedirectToAction("Index", "Home");
            //}
            AdmissionFeesSubmit stlogin = new AdmissionFeesSubmit();
            AdmissionFeesSubmit obj = new AdmissionFeesSubmit();
            List<AdmissionFeesSubmit> feestruckture = new List<AdmissionFeesSubmit>();
            ViewBag.IsSubmit = obj.IsFeeSubmit;

            Commn_master com = new Commn_master();
            // ViewBag.check_admissionopen = com.check_admission_open(ac.GetAcademiccurrentSession().ID, Convert.ToInt32(CommonSetting.Commonid.Educationtype));
            //string Id = ClsLanguage.GetCookies("ENNBStID");
            BL_student_formcomplete bl = new BL_student_formcomplete();
            string ApplicationID = EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENBApplicationNo"));
            StudentLogin tblST = new StudentLogin();
            BL_PrintApplication ob = new BL_PrintApplication();
            int sessionid = ac.GetAcademiccurrentSession().ID;
            var obj1 = tblST.BasicDetail(ApplicationID);
            if (applyaffdavit == "applyaffdavit")
            {

                stlogin.student_collegeform_affidaavitapply(obj1.Id, obj1.session, obj1.CourseCategory, obj.Collegeid, obj.streamcategoryid, obj.studentyear, 0, 0);
                return RedirectToAction("AdmissionFeeSubmit");
            }
            var res1 = bl.CheckAdmission_details(sessionid);

            ViewBag.IsAdmisApplied = res1.IsApplied;
            ViewBag.IsAppliedDate = res1.IsAppliedDate;
            ViewBag.IsDocVerifyDate = res1.IsDocVerifyDate;
            ViewBag.isfeesubmitt = res1.isfeesubmitt;
            ViewBag.IsfeesubmitDate = res1.IsfeesubmitDate;
            ViewBag.isfeesubmitt2 = res1.isfeesubmitt2;
            ViewBag.IsfeesubmitDate2 = res1.IsfeesubmitDate2;
            ViewBag.incomecertificate_iseligible = obj1.incomecertificate_iseligible;
            ViewBag.incomecertificate = obj1.incomecertificate;


            if (obj1 == null)
            {
                return RedirectToAction("Index", "Home");
            }
            if (ViewBag.IsAdmisApplied == false)
            {
                return RedirectToAction("Index", "Home");
            }
            if (res1.IsDocVerify == 0)
            {
                return RedirectToAction("Index", "Home");
            }
            if (res1.IsDocVerify == 2)
            {
                return RedirectToAction("Index", "Home");
            }


            if (obj1.session == 39)
            {
                if (obj1.StudentYear == 3 || obj1.StudentYear == 6 || obj1.StudentYear == 9)
                {

                }
                //else
                //{
                //    return RedirectToAction("Index", "Home");
                //}
            }
            if (obj1.session == 40 || obj1.session == 41)
            {
                //return RedirectToAction("Index", "Home");
            }
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
                ViewBag.CastCategory = objst.CastCategory;

            }
            else
            {
                ViewBag.Status = false;
                ViewBag.Course = "";
                ViewBag.College = "";
                ViewBag.CastCategory = "";
            }

            //if ((objst.CollegeName == "DHANRAJ SINGH COLLEGE, SIKANDARA, JAMUI" && objst.CourseCategory == "2") || objst.CollegeName == "S.S.COLLEGE, MEHUS, SHEIKHPURA" && objst.CourseCategory == "2")
            //{
            //    TempData["msgerror"] = objst.CollegeName + " " + "Admission Hold !!";
            //    return RedirectToAction("AdmissionFeeSubmit", "Home");
            //}

            if (obj1 != null)
            {
                //if (obj1.session <41)
                //{
                if (ViewBag.isfeesubmitt == true)
                {
                    if (ViewBag.addmissionExtenddate == true && ViewBag.addmissionStartdate == true)
                    {
                        //  var result = stlogin.AdmissionFeessub(obj1.Id);
                        // feestruckture = stlogin.FeesDetailsstructure(obj.Collegeid, obj1.CourseCategory, sessionid);
                        obj = stlogin.FeesDetails(obj1.Id);
                        //if (obj.Collegeid == 29)
                        //{
                        //    if (obj.streamcategoryid == 1026 || obj.streamcategoryid == 1048 || obj.streamcategoryid == 1049 || obj.streamcategoryid == 1051 || obj.streamcategoryid == 11053)
                        //    {
                        //        TempData["msgerror"] = "Admission Hold !!";
                        //        return RedirectToAction("AdmissionFeeSubmit", "Home");
                        //    }
                        //}
                        //if (obj.Collegeid == 26)
                        //{
                        //    if (obj.streamcategoryid == 1013 || obj.streamcategoryid == 1010 || obj.streamcategoryid == 1014 || obj.streamcategoryid == 1045 || obj.streamcategoryid == 1021 || obj.streamcategoryid == 1036 || obj.streamcategoryid == 1054)
                        //    {
                        //        TempData["msgerror"] = "Admission Hold !!";
                        //        return RedirectToAction("AdmissionFeeSubmit", "Home");
                        //    }
                        //}

                        //if (obj.is_affiliated == 1)
                        //{
                        //    TempData["msgerror"] = "Payment is offline of affiliated college ,please go to college and pay your fees and take admission  !!";
                        //    return RedirectToAction("AdmissionFeeSubmit", "Home");
                        //}
                        stlogin.student_collegeform_apply(obj1.Id, obj1.session, obj1.CourseCategory, obj.Collegeid, obj.streamcategoryid, obj.studentyear, 0, 0);
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

                                return RedirectToAction("SelectGetwayAdmissionFee", "Home"); // for all remaining student
                            }
                        }
                        //return RedirectToAction("PGAdmissionGateway", "Home");
                    }
                }

                if (obj1.session > 41)
                {
                    if (ViewBag.addmissionExtenddate == true && ViewBag.addmissionStartdate == true)
                    {
                        //  var result = stlogin.AdmissionFeessub(obj1.Id);
                        // feestruckture = stlogin.FeesDetailsstructure(obj.Collegeid, obj1.CourseCategory, sessionid);
                        obj = stlogin.FeesDetails(obj1.Id);
                        //if (obj.Collegeid == 29)
                        //{
                        //    if (obj.streamcategoryid == 1026 || obj.streamcategoryid == 1048 || obj.streamcategoryid == 1049 || obj.streamcategoryid == 1051 || obj.streamcategoryid == 11053)
                        //    {
                        //        TempData["msgerror"] = "Admission Hold !!";
                        //        return RedirectToAction("AdmissionFeeSubmit", "Home");
                        //    }
                        //}
                        //if (obj.Collegeid == 26)
                        //{
                        //    if (obj.streamcategoryid == 1013 || obj.streamcategoryid == 1010 || obj.streamcategoryid == 1014 || obj.streamcategoryid == 1045 || obj.streamcategoryid == 1021 || obj.streamcategoryid == 1036 || obj.streamcategoryid == 1054)
                        //    {
                        //        TempData["msgerror"] = "Admission Hold !!";
                        //        return RedirectToAction("AdmissionFeeSubmit", "Home");
                        //    }
                        //}

                        //if (obj.is_affiliated == 1)
                        //{
                        //    TempData["msgerror"] = "Payment is offline of affiliated college ,please go to college and pay your fees and take admission  !!";
                        //    return RedirectToAction("AdmissionFeeSubmit", "Home");
                        //}
                        stlogin.student_collegeform_apply(obj1.Id, obj1.session, obj1.CourseCategory, obj.Collegeid, obj.streamcategoryid, obj.studentyear, 0, 0);
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

                                return RedirectToAction("SelectGetwayAdmissionFee", "Home"); // for all remaining student
                            }
                        }
                        //return RedirectToAction("PGAdmissionGateway", "Home");
                    }
                }
                //}
                //else
                //{
                //    if (ViewBag.isfeesubmitt != true)
                //    {
                //        if (ViewBag.addmissionExtenddate == true && ViewBag.addmissionStartdate == true)
                //        {
                //            //  var result = stlogin.AdmissionFeessub(obj1.Id);
                //            // feestruckture = stlogin.FeesDetailsstructure(obj.Collegeid, obj1.CourseCategory, sessionid);
                //            obj = stlogin.FeesDetails(obj1.Id);

                //            if (obj.is_affidavitapply == 0)
                //            {
                //                return RedirectToAction("AdmissionFeeSubmit", "Home");
                //            }
                //            //if (obj.is_affiliated == 1)
                //            //{
                //            //    TempData["msgerror"] = "Payment is offline of affiliated college ,please go to college and pay your fees and take admission  !!";
                //            //    return RedirectToAction("AdmissionFeeSubmit", "Home");
                //            //}
                //            stlogin.student_collegeform_apply(obj1.Id, obj1.session, obj1.CourseCategory, obj.Collegeid, obj.streamcategoryid, obj.studentyear, 0, 0);
                //            if (obj1.CastCategory == 5 || obj1.CastCategory == 6) // sc and st
                //            {
                //                if (paywith0Rs == "paywith0Rs")
                //                {
                //                    SbiepayAdmission sbi = new SbiepayAdmission();
                //                    var obj15454 = sbi.Feeeadmission(EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENBApplicationNo")), 0);
                //                    return RedirectToAction("AdmissionFeeSubmit");
                //                }
                //            }
                //            else
                //            {
                //                if (obj1.Gender == 9) // all girls
                //                {
                //                    if (paywith0Rs == "paywith0Rs")
                //                    {
                //                        SbiepayAdmission sbi = new SbiepayAdmission();
                //                        var obj15454 = sbi.Feeeadmission(EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENBApplicationNo")), 0);
                //                        return RedirectToAction("AdmissionFeeSubmit");
                //                    }
                //                }
                //                else
                //                {
                //                    return RedirectToAction("PGAdmissionGateway", "Home"); // for all remaining student
                //                }
                //            }
                //         //   return RedirectToAction("PGAdmissionGateway", "Home");
                //        }
                //    }
                //}
            }
            var tuple2 = new Tuple<AdmissionFeesSubmit, List<AdmissionFeesSubmit>, Login>(obj, feestruckture, obj1);
            return View(tuple2);
        }

        public ActionResult UGAdmissionGatewayAirPay()
        {
            try
            {
                SbiepayAdmission sbi = new SbiepayAdmission();
                AdmissionFeesSubmit stlogin = new AdmissionFeesSubmit();
                List<AdmissionFeesSubmit> feestruckture = new List<AdmissionFeesSubmit>();
                decimal Amount1 = 1;
                string MerchantCustomerID1 = "1";
                string ApplicationID = EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENBApplicationNo"));
                StudentLogin tblST = new StudentLogin();
                var obj1 = tblST.BasicDetail(ApplicationID);
                var result = stlogin.FeesDetails(Convert.ToInt32(EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENNBStID"))));
                feestruckture = stlogin.FeesDetailsstructure(result.Collegeid, result.coursecategoryid, result.sessionid, result.CastCategory, result.streamcategoryid, result.studentyear, result.incomecertificate_iseligible, 0);
                if (result != null)
                {
                    BL_PrintApplication ob = new BL_PrintApplication();
                    AcademicSession ac = new AcademicSession();
                    int sessionid = ac.GetAcademiccurrentSession().ID;
                    int educationtype = Convert.ToInt32(CommonSetting.Commonid.Educationtype);
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
                    if (result.sessionid == 43)
                    {
                        if (result.is_affidavitapply == 0)
                        {
                            return RedirectToAction("AdmissionFeeSubmit");

                        }
                        //if (result.is_affiliated == 1)
                        //{
                        //    TempData["msgerror"] = "Payment is offline of affiliated college ,please go to college and pay your fees and take admission  !!";
                        //    return RedirectToAction("AdmissionFeeSubmit", "Home");
                        //}
                        //if (result.is_affiliated == 1)
                        //{
                        //    TempData["msgerror"] = "Payment is offline of affiliated college ,please go to college and pay your fees and take admission  !!";
                        //    return RedirectToAction("AdmissionFeeSubmit", "Home");
                        //}
                        if (result.tbl_recruitment_IsDocVerify == 0)
                        {
                            return RedirectToAction("AdmissionFeeSubmit");
                        }
                    }
                    if (result.IsDocVerify == 0 && 1 == 2)
                    {
                        return RedirectToAction("AdmissionFeeSubmit");

                    }

                    //if (result.Collegeid == 29)
                    //{
                    //    if (result.streamcategoryid == 1026 || result.streamcategoryid == 1048 || result.streamcategoryid == 1049 || result.streamcategoryid == 1051 || result.streamcategoryid == 11053)
                    //    {
                    //        TempData["msgerror"] = "Admission Hold !!";
                    //        return RedirectToAction("AdmissionFeeSubmit", "Home");
                    //    }
                    //}
                    //if (result.Collegeid == 26)
                    //{
                    //    if (result.streamcategoryid == 1013 || result.streamcategoryid == 1010 || result.streamcategoryid == 1014 || result.streamcategoryid == 1045 || result.streamcategoryid == 1021 || result.streamcategoryid == 1036 || result.streamcategoryid == 1054)
                    //    {
                    //        TempData["msgerror"] = "Admission Hold !!";
                    //        return RedirectToAction("AdmissionFeeSubmit", "Home");
                    //    }
                    //}
                    Amount1 = Convert.ToDecimal(feestruckture.Sum(x => x.amount));

                    if (Amount1 <= 0)
                    {
                        TempData["msgerror"] = "Amount Should be greater than 0 !!";
                        return RedirectToAction("AdmissionFeeSubmit", "Home");
                    }

                    //return RedirectToAction("AdmissionFeeSubmit", "Home");
                    //if (ClsLanguage.GetCookies("NBApplicationNo") == "MU99916090")//Add For Testing Purpose
                    //{
                    //    Amount1 = 10;
                    //}

                    //Amount1 = 2; //Add by jitendra 
                    var obj = sbi.encriptDataadmissionAirPay(Amount1, EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENBApplicationNo")), "student/home/AirPayPGSucessAdmission", "student/home/Failedadmission", result.mid, result.mkey, result.Collegeid, obj1.StudentYear);
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
                    //ViewBag.success_url = "http://localhost:33166/Student/home/AirPayUGSucessAdmission";
                    ViewBag.success_url = "https://portal.DemoUniversity.com/Student/home/AirPayUGSucessAdmission";
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

        public ActionResult UGAdmissionGatewaySafex()
        {
            try
            {


                SbiepayAdmission sbi = new SbiepayAdmission();

                SafexPayAdmission safex = new SafexPayAdmission();

                AdmissionFeesSubmit stlogin = new AdmissionFeesSubmit();
                List<AdmissionFeesSubmit> feestruckture = new List<AdmissionFeesSubmit>();
                decimal Amount1 = 1;
                string MerchantCustomerID1 = "1";
                string ApplicationID = EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENBApplicationNo"));
                StudentLogin tblST = new StudentLogin();
                var obj1 = tblST.BasicDetail(ApplicationID);
                var result = stlogin.FeesDetails(Convert.ToInt32(EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENNBStID"))));
                feestruckture = stlogin.FeesDetailsstructure(result.Collegeid, result.coursecategoryid, result.sessionid, result.CastCategory, result.streamcategoryid, result.studentyear, result.incomecertificate_iseligible, 0);
                if (result != null)
                {
                    BL_PrintApplication ob = new BL_PrintApplication();
                    AcademicSession ac = new AcademicSession();
                    int sessionid = ac.GetAcademiccurrentSession().ID;
                    int educationtype = Convert.ToInt32(CommonSetting.Commonid.Educationtype);
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
                    if (result.sessionid == 43)
                    {
                        if (result.is_affidavitapply == 0)
                        {
                            return RedirectToAction("AdmissionFeeSubmit");

                        }
                        //if (result.is_affiliated == 1)
                        //{
                        //    TempData["msgerror"] = "Payment is offline of affiliated college ,please go to college and pay your fees and take admission  !!";
                        //    return RedirectToAction("AdmissionFeeSubmit", "Home");
                        //}
                        //if (result.is_affiliated == 1)
                        //{
                        //    TempData["msgerror"] = "Payment is offline of affiliated college ,please go to college and pay your fees and take admission  !!";
                        //    return RedirectToAction("AdmissionFeeSubmit", "Home");
                        //}
                        if (result.tbl_recruitment_IsDocVerify == 0)
                        {
                            return RedirectToAction("AdmissionFeeSubmit");
                        }
                    }
                    if (result.IsDocVerify == 0 && 1 == 2)
                    {
                        return RedirectToAction("AdmissionFeeSubmit");

                    }

                    //if (result.Collegeid == 29)
                    //{
                    //    if (result.streamcategoryid == 1026 || result.streamcategoryid == 1048 || result.streamcategoryid == 1049 || result.streamcategoryid == 1051 || result.streamcategoryid == 11053)
                    //    {
                    //        TempData["msgerror"] = "Admission Hold !!";
                    //        return RedirectToAction("AdmissionFeeSubmit", "Home");
                    //    }
                    //}
                    //if (result.Collegeid == 26)
                    //{
                    //    if (result.streamcategoryid == 1013 || result.streamcategoryid == 1010 || result.streamcategoryid == 1014 || result.streamcategoryid == 1045 || result.streamcategoryid == 1021 || result.streamcategoryid == 1036 || result.streamcategoryid == 1054)
                    //    {
                    //        TempData["msgerror"] = "Admission Hold !!";
                    //        return RedirectToAction("AdmissionFeeSubmit", "Home");
                    //    }
                    //}
                    Amount1 = Convert.ToDecimal(feestruckture.Sum(x => x.amount));

                    if (Amount1 <= 0)
                    {
                        TempData["msgerror"] = "Amount Should be greater than 0 !!";
                        return RedirectToAction("AdmissionFeeSubmit", "Home");
                    }

                    //return RedirectToAction("AdmissionFeeSubmit", "Home");
                    //if (ClsLanguage.GetCookies("NBApplicationNo") == "MU33761657")//Add For Testing Purpose
                    //{
                    //    Amount1 = 10;
                    //}

                    //LocalTest
                    //string SucccessUrl = "http://localhost:33166/Student/home/SafexPayUGSucessAdmission";
                    //Live
                    string SucccessUrl = "https://portal.DemoUniversity.com/Student/home/SafexPayUGSucessAdmission";

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

        public ActionResult UGAdmissionGatewayEaseBuzz()
        {
            try
            {
                SbiepayAdmission sbi = new SbiepayAdmission();
                AdmissionFeesSubmit stlogin = new AdmissionFeesSubmit();
                List<AdmissionFeesSubmit> feestruckture = new List<AdmissionFeesSubmit>();
                decimal Amount1 = 1;
                string MerchantCustomerID1 = "1";
                string ApplicationID = EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENBApplicationNo"));
                StudentLogin tblST = new StudentLogin();
                var obj1 = tblST.BasicDetail(ApplicationID);
                var result = stlogin.FeesDetails(Convert.ToInt32(EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENNBStID"))));
                feestruckture = stlogin.FeesDetailsstructure(result.Collegeid, result.coursecategoryid, result.sessionid, result.CastCategory, result.streamcategoryid, result.studentyear, result.incomecertificate_iseligible, 0);
                if (result != null)
                {
                    BL_PrintApplication ob = new BL_PrintApplication();
                    AcademicSession ac = new AcademicSession();
                    int sessionid = ac.GetAcademiccurrentSession().ID;
                    int educationtype = Convert.ToInt32(CommonSetting.Commonid.Educationtype);
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
                    if (result.sessionid == 43)
                    {
                        if (result.is_affidavitapply == 0)
                        {
                            return RedirectToAction("AdmissionFeeSubmit");

                        }
                        //if (result.is_affiliated == 1)
                        //{
                        //    TempData["msgerror"] = "Payment is offline of affiliated college ,please go to college and pay your fees and take admission  !!";
                        //    return RedirectToAction("AdmissionFeeSubmit", "Home");
                        //}
                        //if (result.is_affiliated == 1)
                        //{
                        //    TempData["msgerror"] = "Payment is offline of affiliated college ,please go to college and pay your fees and take admission  !!";
                        //    return RedirectToAction("AdmissionFeeSubmit", "Home");
                        //}
                        if (result.tbl_recruitment_IsDocVerify == 0)
                        {
                            return RedirectToAction("AdmissionFeeSubmit");
                        }
                    }
                    if (result.IsDocVerify == 0 && 1 == 2)
                    {
                        return RedirectToAction("AdmissionFeeSubmit");

                    }

                    //if (result.Collegeid == 29)
                    //{
                    //    if (result.streamcategoryid == 1026 || result.streamcategoryid == 1048 || result.streamcategoryid == 1049 || result.streamcategoryid == 1051 || result.streamcategoryid == 11053)
                    //    {
                    //        TempData["msgerror"] = "Admission Hold !!";
                    //        return RedirectToAction("AdmissionFeeSubmit", "Home");
                    //    }
                    //}
                    //if (result.Collegeid == 26)
                    //{
                    //    if (result.streamcategoryid == 1013 || result.streamcategoryid == 1010 || result.streamcategoryid == 1014 || result.streamcategoryid == 1045 || result.streamcategoryid == 1021 || result.streamcategoryid == 1036 || result.streamcategoryid == 1054)
                    //    {
                    //        TempData["msgerror"] = "Admission Hold !!";
                    //        return RedirectToAction("AdmissionFeeSubmit", "Home");
                    //    }
                    //}
                    Amount1 = Convert.ToDecimal(feestruckture.Sum(x => x.amount));

                    if (Amount1 <= 0)
                    {
                        TempData["msgerror"] = "Amount Should be greater than 0 !!";
                        return RedirectToAction("AdmissionFeeSubmit", "Home");
                    }

                    //return RedirectToAction("AdmissionFeeSubmit", "Home");
                    //if (ClsLanguage.GetCookies("NBApplicationNo") == "MU99916090")//Add For Testing Purpose
                    //{
                    //    Amount1 = 10;
                    //}
                    // string SucccessUrl = "http://localhost:33166/Student/Home/EaseBuzzPayUGSucessAdmission?CollegeId=" + result.Collegeid;

                    string SucccessUrl = "https://portal.DemoUniversity.com/Student/home/EaseBuzzPayUGSucessAdmission?CollegeId=" + result.Collegeid;

                    //Amount1 = 10; //Add by jitendra 
                    var obj = sbi.encriptDataadmissionEaseBuzz(Amount1, EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENBApplicationNo")), SucccessUrl, SucccessUrl, result.mid, result.mkey, result.Collegeid, obj1.StudentYear);
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

        //http://localhost:33166/Student/Home/EaseBuzzPayUGSucessAdmission?CollegeId=40
        public ActionResult SafexPayUGSucessAdmission(string CollegeId)
        {


            int ColId = Convert.ToInt32(CollegeId);

            string responseapi = Request.Params.ToString();
            //try
            //{
            //CommonMethod.TraceLogWritetoNotepad("obj1.objExamFrom4", "Exam//SafexPayPGSucessExam1", "obj1.objExamFrom", "Id");
            if (Request.Params.Count > 0)
            {
                string privateKey = "";

                privateKey = CommonMethod.MIDcollegewiseSafex().Where(x => x.collegeid == ColId).FirstOrDefault().mkey;


                //CommonMethod.TraceLogWritetoNotepad("obj1.objExamFrom4", "Exam//SafexPayPGSucessExam2", "obj1.objExamFrom", "Id");
                string enc_txn_response = (!String.IsNullOrEmpty(Request.Params["txn_response"])) ? Request.Params["txn_response"] : string.Empty;
                string enc_pg_details = (!String.IsNullOrEmpty(Request.Params["pg_details"])) ? Request.Params["pg_details"] : string.Empty;
                string enc_fraud_details = (!String.IsNullOrEmpty(Request.Params["fraud_details"])) ? Request.Params["fraud_details"] : string.Empty;
                string enc_other_details = (!String.IsNullOrEmpty(Request.Params["other_details"])) ? Request.Params["other_details"] : string.Empty;
                //MyCryptoClass aes = new MyCryptoClass();



                //Add new
                Areas.Student.Models.SbiepayAdmission.MyCryptoClass aes = new Areas.Student.Models.SbiepayAdmission.MyCryptoClass();

                string txn_response = aes.decrypt(enc_txn_response, privateKey);

                char splitChar = '|';
                if (txn_response.Contains("~"))
                {
                    splitChar = '~';
                }

                string str1 = txn_response.Split(splitChar)[1];

                string str2 = txn_response.Split(splitChar)[0];

                string str3 = txn_response.Split(splitChar)[2];
                string txn_hash = HttpUtility.UrlDecode(str1);
                string txn_res_hash = str1;// txn_hash[1];
                //string txn_res_actual = txn_hash[0] + "" + txn_hash[2];
                string txn_res_actual = HttpUtility.UrlDecode(str2) + "" + HttpUtility.UrlDecode(str3);

                string[] txn_response_arr = txn_response.Split('|');

                string Hash = txn_response_arr[10] + "~" + txn_response_arr[1] + "~" + txn_response_arr[2] + "~" + txn_response_arr[3] + "~" + txn_response_arr[4] + "~" + txn_response_arr[5] + "~" + txn_response_arr[8];

                string hashing = aes.ComputeSha256Hash(Hash);

                string encHash = aes.encrypt(hashing, privateKey);

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
                //End New




                //string txn_response = aes.decrypt(enc_txn_response,privateKey);
                //string decodedUrl = HttpUtility.UrlDecode(txn_response);
                //char splitChar='|';
                //if (decodedUrl.Contains("~"))
                //{
                //    splitChar = '|';
                //}

                //string[] txn_hash = decodedUrl.Split(splitChar);
                //string txn_res_hash = txn_hash[1];
                //string txn_res_actual = txn_hash[0] + "" + txn_hash[2];
                ////string txn_response = aes.decrypt(enc_txn_response,);
                //string[] txn_response_arr = txn_res_actual.Split('|');
                //string Hash = txn_response_arr[10] + "~" + txn_response_arr[1] + "~" + txn_response_arr[2] + "~" + txn_response_arr[3] + "~" + txn_response_arr[4] + "~" + txn_response_arr[5] + "~" + txn_response_arr[8];
                ////string Hash = txn_response_arr[10] + "|" + txn_response_arr[1] + "|" + txn_response_arr[2] + "|" + txn_response_arr[3] + "|" + txn_response_arr[4] + "|" + txn_response_arr[5] + "|" + txn_response_arr[8];
                //string hashing = aes.ComputeSha256Hash(Hash);
                //string encHash = aes.encrypt(hashing, privateKey);
                //string genuine = "genuine";
                //string fake = "fake";
                //string protocol = "";
                //if (txn_res_hash == encHash)
                //{
                //    protocol = genuine;
                //}
                //else
                //{
                //    protocol = fake;
                //}
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
                string pg_details = aes.decrypt(enc_pg_details, privateKey);
                string[] pg_details_arr = pg_details.Split('|');
                string pg_id = (!String.IsNullOrEmpty(pg_details_arr[0])) ? pg_details_arr[0] : string.Empty;
                string pg_name = (!String.IsNullOrEmpty(pg_details_arr[1])) ? pg_details_arr[1] : string.Empty;
                string paymode = (!String.IsNullOrEmpty(pg_details_arr[2])) ? pg_details_arr[2] : string.Empty;
                string emi_months = (!String.IsNullOrEmpty(pg_details_arr[3])) ? pg_details_arr[3] : string.Empty;
                string fraud_details = aes.decrypt(enc_fraud_details, privateKey);
                string[] fraud_details_arr = fraud_details.Split('|');
                string fraud_action = (!String.IsNullOrEmpty(fraud_details_arr[0])) ? fraud_details_arr[0] : string.Empty;
                string fraud_message = (!String.IsNullOrEmpty(fraud_details_arr[1])) ? fraud_details_arr[1] : string.Empty;
                string score = (!String.IsNullOrEmpty(fraud_details_arr[2])) ? fraud_details_arr[2] : string.Empty;
                string other_details = aes.decrypt(enc_other_details, privateKey);
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
                string AdmissionType = udf_2;
                string Requestdata = enc_txn_response;
                string dRequestdata = "";
                string PGstatus = status;
                string Sid = udf_4;
                string Sessionid = udf_5;
                if (status == "Successful")
                {
                    SbiepayAdmission sbi = new SbiepayAdmission();
                    PGstatus = "Success";
                    status = "Success";
                    //var result = sbi.SafexPaypgsucessdecrypt(Convert.ToInt32(Sid), banktrxid, clienttrxid, amount1, feeamount, gst, commission, paymode, banktxndate, Reason, apitxnid, ApplicationNo, courseyearid, examType, Requestdata, dRequestdata, PGstatus);                 
                    var result = sbi.AirPaypgsucessdecrypt(Convert.ToInt32(Sid), banktrxid, clienttrxid, amount1, feeamount, gst, commission, paymode, banktxndate, Reason, apitxnid, ApplicationNo, courseyearid, AdmissionType, Requestdata, dRequestdata, PGstatus, Sessionid); ;
                    return RedirectToAction("ResponseAdmission");
                }
                else
                {

                    SbiepayAdmission sbi = new SbiepayAdmission();
                    var result = sbi.AirPaypgsucessdecrypt(Convert.ToInt32(Sid), banktrxid, clienttrxid, amount1, feeamount, gst, commission, paymode, banktxndate, Reason, apitxnid, ApplicationNo, courseyearid, AdmissionType, Requestdata, dRequestdata, PGstatus, Sessionid);
                    return RedirectToAction("ResponseAdmission");
                }
            }

            return View();
        }


        public ActionResult EaseBuzzPayUGSucessAdmission(string CollegeId)
        {

            string Salt = CommonMethod.MIDcollegewiseEaseBuzz().Where(x => x.collegeid == Convert.ToInt32(CollegeId)).FirstOrDefault().Salt;

            //return RedirectToAction("Index", "HomePG");
            string paramInfo = "";
            string[] merc_hash_vars_seq;
            string merc_hash_string = string.Empty;
            string merc_hash = string.Empty;
            string order_id = string.Empty;
            string hash_seq = "key|txnid|amount|productinfo|firstname|email|udf1|udf2|udf3|udf4|udf5|udf6|udf7|udf8|udf9|udf10";
            merc_hash_vars_seq = hash_seq.Split('|');
            Array.Reverse(merc_hash_vars_seq);
            merc_hash_string = Salt + "|" + Request.Form["status"];
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
                    string AdmissionType = "";
                    string Requestdata = merc_hash_string;
                    string dRequestdata = merc_hash;
                    string PGstatus = MESSAGE;
                    string Sid = "0";
                    string Sessionid = "";

                    ApplicationNo = Request.Form["udf1"];
                    clienttrxid = Request.Form["udf2"];
                    AdmissionType = Request.Form["udf3"];

                    //Sid = Request.Form["udf5"];
                    if (TRANSACTIONSTATUS.ToLower() == "success")
                    {
                        SbiepayAdmission sbi = new SbiepayAdmission();
                        var result = sbi.AirPaypgsucessdecrypt(Convert.ToInt32(Sid), banktrxid, clienttrxid, amount1, feeamount, gst, commission, paymode, banktxndate, Reason, apitxnid, ApplicationNo, courseyearid, AdmissionType, Requestdata, dRequestdata, PGstatus, Sessionid);
                        return RedirectToAction("ResponseAdmission");
                    }
                    else
                    {
                        SbiepayAdmission sbi = new SbiepayAdmission();
                        var result = sbi.AirPaypgsucessdecrypt(Convert.ToInt32(Sid), banktrxid, clienttrxid, amount1, feeamount, gst, commission, paymode, banktxndate, Reason, apitxnid, ApplicationNo, courseyearid, AdmissionType, Requestdata, dRequestdata, PGstatus, Sessionid);
                        //var result = sbi.AirPaypgsucessdecrypt(Convert.ToInt32(Sid), banktrxid, clienttrxid, amount1, feeamount, gst, commission, paymode, banktxndate, Reason, apitxnid, ApplicationNo, courseyearid, examType, Requestdata, dRequestdata, TRANSACTIONSTATUS);
                        return RedirectToAction("ResponseAdmission");
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

        public ActionResult PGAdmissionGateway()
        {
            try
            {
                SbiepayAdmission sbi = new SbiepayAdmission();
                AdmissionFeesSubmit stlogin = new AdmissionFeesSubmit();
                List<AdmissionFeesSubmit> feestruckture = new List<AdmissionFeesSubmit>();
                decimal Amount1 = 1;
                string MerchantCustomerID1 = "1";
                string ApplicationID = EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENBApplicationNo"));
                StudentLogin tblST = new StudentLogin();
                var obj1 = tblST.BasicDetail(ApplicationID);
                var result = stlogin.FeesDetails(Convert.ToInt32(EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENNBStID"))));
                feestruckture = stlogin.FeesDetailsstructure(result.Collegeid, result.coursecategoryid, result.sessionid, result.CastCategory, result.streamcategoryid, result.studentyear, result.incomecertificate_iseligible, 0);
                if (result != null)
                {
                    BL_PrintApplication ob = new BL_PrintApplication();
                    AcademicSession ac = new AcademicSession();
                    int sessionid = ac.GetAcademiccurrentSession().ID;
                    int educationtype = Convert.ToInt32(CommonSetting.Commonid.Educationtype);
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
                    if (result.sessionid == 42)
                    {
                        if (result.is_affidavitapply == 0)
                        {
                            return RedirectToAction("AdmissionFeeSubmit");

                        }
                        //if (result.is_affiliated == 1)
                        //{
                        //    TempData["msgerror"] = "Payment is offline of affiliated college ,please go to college and pay your fees and take admission  !!";
                        //    return RedirectToAction("AdmissionFeeSubmit", "Home");
                        //}
                        //if (result.is_affiliated == 1)
                        //{
                        //    TempData["msgerror"] = "Payment is offline of affiliated college ,please go to college and pay your fees and take admission  !!";
                        //    return RedirectToAction("AdmissionFeeSubmit", "Home");
                        //}
                        if (result.tbl_recruitment_IsDocVerify == 0)
                        {
                            return RedirectToAction("AdmissionFeeSubmit");
                        }
                    }
                    if (result.IsDocVerify == 0 && 1 == 2)
                    {
                        return RedirectToAction("AdmissionFeeSubmit");

                    }

                    //if (result.Collegeid == 29)
                    //{
                    //    if (result.streamcategoryid == 1026 || result.streamcategoryid == 1048 || result.streamcategoryid == 1049 || result.streamcategoryid == 1051 || result.streamcategoryid == 11053)
                    //    {
                    //        TempData["msgerror"] = "Admission Hold !!";
                    //        return RedirectToAction("AdmissionFeeSubmit", "Home");
                    //    }
                    //}
                    //if (result.Collegeid == 26)
                    //{
                    //    if (result.streamcategoryid == 1013 || result.streamcategoryid == 1010 || result.streamcategoryid == 1014 || result.streamcategoryid == 1045 || result.streamcategoryid == 1021 || result.streamcategoryid == 1036 || result.streamcategoryid == 1054)
                    //    {
                    //        TempData["msgerror"] = "Admission Hold !!";
                    //        return RedirectToAction("AdmissionFeeSubmit", "Home");
                    //    }
                    //}
                    Amount1 = Convert.ToDecimal(feestruckture.Sum(x => x.amount));

                    if (Amount1 <= 0)
                    {
                        TempData["msgerror"] = "Amount Should be greater than 0 !!";
                        return RedirectToAction("AdmissionFeeSubmit", "Home");
                    }
                    if (sessionid == 39)
                    {
                        //if (result.studentyear == 3 || result.studentyear == 6 || result.studentyear == 9)
                        //{

                        //}
                        //else
                        //{
                        //    return RedirectToAction("Index", "Home");
                        //}
                    }
                    //Amount1 = 1;
                    var obj = sbi.encriptDataadmission(Amount1, EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENBApplicationNo")), "student/home/PGSucessadmission", "student/home/PGFailedadmission", result.mid, result.mkey, result.Collegeid, obj1.StudentYear);
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

                obj = stlogin.FeesDetails(Convert.ToInt32(EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENNBStID"))));
                feestruckture = stlogin.FeesDetailsstructure(obj.Collegeid, obj.coursecategoryid, obj.sessionid, obj.CastCategory, obj.streamcategoryid, obj.studentyear, obj.incomecertificate_iseligible, 0);
                var tuple = new Tuple<AdmissionFeesSubmit, List<AdmissionFeesSubmit>, Login>(obj, feestruckture, objl22);
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
        public ActionResult test()
        {
            int i = 1;
            try
            {
                return RedirectToAction("Index", "Home");
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
        // [HttpPost]
        // [ValidateInput(false)]
        //public FileResult Export(string GridHtml)
        //{
        //    using (MemoryStream stream = new System.IO.MemoryStream())
        //    {
        //        StringReader sr = new StringReader(GridHtml);
        //        Document pdfDoc = new Document(System.PageSize.A4, 10f, 10f, 100f, 0f);
        //        PdfWriter writer = PdfWriter.GetInstance(pdfDoc, stream);
        //        pdfDoc.Open();
        //        // XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
        //        pdfDoc.Close();
        //        return File(stream.ToArray(), "application/pdf", "Report.pdf");
        //    }
        //}
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

                CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "UG Student Generate ID Card method", eID);
                return RedirectToAction("GenerateIDCard");
            }
        }
        [VerifyUrlFilterAdminAttribute]
        public ActionResult FeesSubmit_spot()
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
            var obj1 = PritApp.GetAppLicationDataAdmin_spot(objl22.Id);
            ViewBag.check_admissionopen = com.check_admission_open(ad.GetAcademiccurrentSession().ID, Convert.ToInt32(CommonSetting.Commonid.Educationtype));
            BL_PrintApplication obrecrei = new BL_PrintApplication();
            var objrecritiny = obrecrei.CheckStudentAdmission(ad.GetAcademiccurrentSession().ID);
            ViewBag.Statusrecruitny = false;
            ViewBag.IsFeeSubmit_spot = objl22.IsFeeSubmit_spot;
            if (objrecritiny.Status == true)
            {
                ViewBag.Statusrecruitny = objrecritiny.Status;
            }
            return View(obj1);
        }
        [HttpPost]

        public ActionResult FeesSubmit_spot(int id = 0)
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
            ViewBag.IsFeeSubmit_spot = lo.IsFeeSubmit_spot;
            BL_PrintApplication obrecrei = new BL_PrintApplication();
            AcademicSession ac = new AcademicSession();
            var objrecritiny = obrecrei.CheckStudentAdmission(ac.GetAcademiccurrentSession().ID);
            if (objrecritiny.Status == true)
            {
                if (lo.session != 39)
                {
                    return RedirectToAction("FeesSubmit_spot", "Home");
                }
            }
            Commn_master com = new Commn_master();
            AcademicSession ad = new AcademicSession();
            ViewBag.check_admissionopen = com.check_admission_open(ac.GetAcademiccurrentSession().ID, Convert.ToInt32(CommonSetting.Commonid.Educationtype));
            if (lo.CastCategory == 0)
            {
                fee.Status = false;
                TempData["msgfees"] = "Please upload your Basic Details !!!";
                return RedirectToAction("FeesSubmit_spot");
            }
            if (lo.EducationType == Convert.ToInt32(CommonSetting.Commonid.Educationtype))
            {
                var list10 = list.Where(m => m.QualicationType == Convert.ToInt32(CommonSetting.commQualification.Ten)).ToList();
                if (list10.Count == 0)
                {
                    fee.Status = false;
                    TempData["msgfees"] = "Please upload your Secondary Board qualification certificate !!!";
                    return RedirectToAction("FeesSubmit_spot");
                }
            }
            if (lo.EducationType == Convert.ToInt32(CommonSetting.Commonid.Educationtype))
            {
                var list10 = list.Where(m => m.QualicationType == Convert.ToInt32(CommonSetting.commQualification.Ten)).ToList();
                if (list10.Count == 0)
                {
                    fee.Status = false;
                    TempData["msgfees"] = "Please upload your Secondary Board qualification certificate !!!";
                    return RedirectToAction("FeesSubmit_spot");
                }
                var list12 = list.Where(m => m.QualicationType == Convert.ToInt32(CommonSetting.commQualification.Art12) || m.QualicationType == Convert.ToInt32(CommonSetting.commQualification.Science12) || m.QualicationType == Convert.ToInt32(CommonSetting.commQualification.Comm12) || m.QualicationType == Convert.ToInt32(CommonSetting.commQualification.diploma)).ToList();
                if (list12.Count == 0)
                {
                    fee.Status = false;
                    TempData["msgfees"] = "Please upload your Intermediate qualification certificate !!!";
                    return RedirectToAction("FeesSubmit_spot");
                }
                if (list12.FirstOrDefault().Percentage >= 100)
                {
                    fee.Status = false;
                    TempData["msgfees"] = "Please correct your  Intermediate qualification Aggregate Percentage ,  you have filled  100 %  !!!";
                    return RedirectToAction("FeesSubmit_spot");
                }
                Student_Admission_Choicesubject Choicesubject = new Student_Admission_Choicesubject();
                var Choicesubjectlist = Choicesubject.viewst_choicesubject_spot(lo.Id);
                if (Choicesubjectlist.Count == 0)
                {
                    fee.Status = false;
                    TempData["msgfees"] = "Please first select choices for subject and college !!!";
                    return RedirectToAction("FeesSubmit_spot");
                }
                if (Choicesubjectlist.Count < 1)
                {
                    fee.Status = false;
                    TempData["msgfees"] = "Atleast One choices save from subject and college !!!";
                    return RedirectToAction("FeesSubmit_spot");
                }
                FeesSubmit stlogin = new FeesSubmit();
                int sessionid = ad.GetAcademiccurrentSession().ID;
                BL_PrintApplication PritApp = new BL_PrintApplication();
                var app = ClsLanguage.GetCookies("NBApplicationNo");
                //Login objl22 = new Login();
                StudentLogin objs = new StudentLogin();
                //objl22 = objs.BasicDetail(app);
                BL_StreamMaster stream = new BL_StreamMaster();
                if (lo.IsFeeSubmit_spot == 0)
                {
                    stlogin.Status = false;
                    return RedirectToAction("PGGateway_spot");
                }
                else
                {
                    FeesSubmit stlogin1 = new FeesSubmit();
                    stlogin1.Status = false;
                    TempData["msgfees"] = "Fees Already Submitted !!!";
                    return RedirectToAction("FeesSubmit_spot");
                }
            }
            return View();
        }

        public ActionResult PGGateway_spot()
        {
            try
            {
                Sbiepay sbi = new Sbiepay();
                FeesSubmit stlogin = new FeesSubmit();
                decimal Amount1 = 1;
                string MerchantCustomerID1 = "1";
                var result = stlogin.FeessubEncryt_spot(Convert.ToInt32(EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENNBStID"))));
                if (result != null)
                {
                    Commn_master com = new Commn_master();
                    AcademicSession ad = new AcademicSession();
                    ViewBag.check_admissionopen = com.check_admission_open(ad.GetAcademiccurrentSession().ID, Convert.ToInt32(CommonSetting.Commonid.Educationtype));
                    if (ViewBag.check_admissionopen == false)
                    {
                        return RedirectToAction("FeesSubmit_spot");
                    }
                    Amount1 = Convert.ToDecimal(result.Fees);
                    var obj = sbi.encriptData_spot(Amount1, EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENBApplicationNo")), "student/home/PGSucess_spot", "student/home/PGFailed_spot");

                    ViewBag.requestparams = obj.requestparams;
                    ViewBag.merchantId = obj.merchantId;
                    ViewBag.EncryptbillingDetails = obj.EncryptbillingDetails;
                    ViewBag.EncryptshippingDetais = obj.EncryptshippingDetais;
                    ViewBag.url = obj.url;
                }
            }
            catch (Exception ex)
            {
                CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "action PGGateway spot error", ClsLanguage.GetCookies("NBApplicationNo"));

                return View();
            }
            return View();
        }
        public ActionResult Response_spot()
        {

            try
            {
                BL_PrintApplication PritApp = new BL_PrintApplication();
                var app = EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENBApplicationNo"));
                Login objl22 = new Login();
                StudentLogin objs = new StudentLogin();
                objl22 = objs.BasicDetail(app);
                var obj1 = PritApp.GetAppLicationDataAdmin_spot(objl22.Id);
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
        public ActionResult PGSucess_spot()
        {
            string paramInfo = "";
            if (Request.Form.Count > 0)
            {
                try
                {
                    Sbiepay sbi = new Sbiepay();
                    var result = sbi.pgsucessdecrypt_spot(Convert.ToInt32(EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENNBStID"))));
                    return RedirectToAction("Response_spot");
                }
                catch (Exception ex)
                {
                    CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "Payment PaymentGateway suucess url hit on Controller error", ClsLanguage.GetCookies("NBApplicationNo"));

                }
            }
            return RedirectToAction("FeesSubmit_spot");
            return View();
        }

        public ActionResult PGFailed_spot()
        {
            string paramInfo = "";
            if (Request.Form.Count > 0)
            {
                try
                {
                    Sbiepay sbi = new Sbiepay();
                    var result = sbi.pgfaileddecrypt_spot(Convert.ToInt32(EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENNBStID"))));
                    return RedirectToAction("Response_spot");
                }
                catch (Exception ex)
                {
                    CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "Payment PaymentGateway suucess url hit on Controller error", ClsLanguage.GetCookies("NBApplicationNo"));

                }
            }
            return RedirectToAction("FeesSubmit_spot");
            return View();

        }
        [VerifyUrlFilterAdminAttribute]
        public ActionResult SlideUp(int id = 0)
        {
            // Slide Up Admission Cloesd
            return RedirectToAction("Index");

            StudentAdmissionQualification obj = new StudentAdmissionQualification();
            var ApplicationNo = ClsLanguage.GetCookies("NBApplicationNo");
            Login objl = new Login();
            StudentLogin objs = new StudentLogin();
            AcademicSession ac = new AcademicSession();
            objl = objs.BasicDetail(ApplicationNo);
            if (objl.session != 42)
            {
                return RedirectToAction("selectsubject");
            }
            if (objl != null)
            {
                //QualiSubject_bind(objl.previous_qua_id);
                //Subject_bind(objl.previous_qua_id);
            }
            FeesSubmit log = new FeesSubmit();
            ViewBag.IsSubmit = objl.IsFeeSubmit;
            Commn_master com = new Commn_master();
            BL_PrintApplication ob111 = new BL_PrintApplication();
            var datestart = ob111.CheckStudentAddmisionExtendDate(objl.session, objl.EducationType);
            var dateextend = ob111.CheckStudentAddmisionStartDate(objl.session, objl.EducationType);
            if (dateextend.Status == true && datestart.Status == true)
            {
                ViewBag.check_admissionopen = true;  // com.check_admission_open(ac.GetAcademiccurrentSession().ID, Convert.ToInt32(CommonSetting.Commonid.Educationtype));
            }
            // ViewBag.check_admissionopen = true;



            BL_CollegeMaster obj111 = new BL_CollegeMaster();
            CollageList sub = new CollageList();
            ViewBag.gender = objl.Gender;
            ViewBag.sessionid = objl.session;
            ViewBag.College = sub.qlist;
            ViewBag.Coursecategoryid = objl.CourseCategory;
            List<SelectListItem> ob = new List<SelectListItem>();
            ViewBag.year = ob;
            List<StudentPreviousQualification> list = new List<StudentPreviousQualification>();
            StudentPreviousQualification StudentPrevious = new StudentPreviousQualification();
            var tuple = new Tuple<StudentAdmissionQualification, List<StudentPreviousQualification>, Login>(obj, list, objl);
            BL_PrintApplication_rec obrecrei = new BL_PrintApplication_rec();

            var objrecritiny = obrecrei.CheckStudentAdmission(ac.GetAcademiccurrentSession().ID);
            ViewBag.Statusrecruitny = false;
            ViewBag.CastCategory = 0;
            ViewBag.coursecategoryid = 0;
            ViewBag.choicetable_id = 0;
            ViewBag.streamcategoryid = 0;
            ViewBag.collegeid = 0;
            ViewBag.Percentage = 0;
            ViewBag.collegeid = objrecritiny.collegeid;
            ViewBag.streamcategoryid = objrecritiny.streamcategoryid;
            ViewBag.choicetable_id = objrecritiny.choicetable_id;
            ViewBag.coursecategoryid = objrecritiny.coursecategoryid;
            ViewBag.CastCategory = objrecritiny.CastCategory;
            ViewBag.Percentage = objrecritiny.percenatge;
            if (objrecritiny.Status == true)
            {
                ViewBag.Statusrecruitny = objrecritiny.Status;

            }
            Student_Admission_Choicesubject Choicesubject = new Student_Admission_Choicesubject();
            var Choicesubjectlist = Choicesubject.viewst_choicesubject_1(objl.Id);
            if (Choicesubjectlist.Count == 0)
            {

            }
            else
            {
                TempData["selectalready"] = "already";
                ViewBag.Choicesubject = Choicesubjectlist;
            }
            return View(tuple);
        }
        [HttpPost]

        public JsonResult savest_seatavailable_slideup(int hounors_subjectid, int collegeid)
        {
            Student_Admission_Choicesubject res111 = new Student_Admission_Choicesubject();
            //res111.Status = false;
            //res111.Msg = "Slide Up admission Closed";
            //return Json(new { data = res111, success = true });


            BL_StreamMaster obj = new BL_StreamMaster();
            var ApplicationNo = ClsLanguage.GetCookies("NBApplicationNo");
            Login objl = new Login();
            StudentLogin objs = new StudentLogin();
            BL_PrintApplication_rec obrecrei = new BL_PrintApplication_rec();
            AcademicSession ac = new AcademicSession();
            var objrecritiny = obrecrei.CheckStudentAdmission(ac.GetAcademiccurrentSession().ID);
            if (objrecritiny.IsmanualAdmission == 1)
            {
                Student_Admission_Choicesubject res = new Student_Admission_Choicesubject();
                res.Status = false;
                res.Msg = "You already got admission by Slide Up college!!!";
                return Json(new { data = res, success = true });
            }
            try
            {
                objl = objs.BasicDetail(ApplicationNo);
                var result = obj.getcollegesubjects_seatavailbale_slideup(collegeid, hounors_subjectid, objl.session, objl.CourseCategory, objl.CastCategory, objl.ishandicapped, 1);
                return Json(new { data = result, success = true });
            }
            catch (Exception ex)
            {
                CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "Slideup seat available subject Choice ", ClsLanguage.GetCookies("NBApplicationNo") + "   hounors_subjectidlist: " + "" + "    collegeidlist:" + "" + "  Subsidiary1_subjectidlist:" + "" + "   Subsidiary2_subjectidlist:" + "" + "     Compulsory1_subjectidlist:" + "" + "   Compulsory2_subjectidlist:" + "");
                return Json(new { data = new Student_Admission_Choicesubject(), success = true });
            }
        }
        [HttpPost]

        public JsonResult savest_slideup_admission(int hounors_subjectid, int collegeid, decimal percenatge, int choicetable_id)
        {
            Student_Admission_Choicesubject res111 = new Student_Admission_Choicesubject();
            res111.Status = false;
            res111.Msg = "Slide Up admission Closed";
            return Json(new { data = res111, success = true });

            BL_StreamMaster obj = new BL_StreamMaster();
            var ApplicationNo = ClsLanguage.GetCookies("NBApplicationNo");
            Login objl = new Login();
            StudentLogin objs = new StudentLogin();
            BL_PrintApplication_rec obrecrei = new BL_PrintApplication_rec();
            AcademicSession ac = new AcademicSession();
            var objrecritiny = obrecrei.CheckStudentAdmission(ac.GetAcademiccurrentSession().ID);
            if (objrecritiny.IsmanualAdmission == 1)
            {
                Student_Admission_Choicesubject res = new Student_Admission_Choicesubject();
                res.Status = false;
                res.Msg = "You already got admission by Slide Up college!!!";
                return Json(new { data = res, success = true });
            }
            try
            {
                objl = objs.BasicDetail(ApplicationNo);
                BL_PrintApplication ob = new BL_PrintApplication();
                var datestart = ob.CheckStudentAddmisionExtendDate(objl.session, objl.EducationType);
                if (datestart.Status == false)
                {
                    Student_Admission_Choicesubject res = new Student_Admission_Choicesubject();
                    res.Status = false;
                    res.Msg = "Date Closed!!!";
                    return Json(new { data = res, success = true });
                }
                var dateextend = ob.CheckStudentAddmisionStartDate(objl.session, objl.EducationType);
                if (dateextend.Status == false)
                {
                    Student_Admission_Choicesubject res = new Student_Admission_Choicesubject();
                    res.Status = false;
                    res.Msg = "Date Closed!!!";
                    return Json(new { data = res, success = true });
                }



                var result = obj.getcollegesubjects_Saveslideup("insert", objl.Id, objl.CastCategory, collegeid, hounors_subjectid, objl.session, objl.CourseCategory, 1, percenatge, choicetable_id, objl.ishandicapped, objl.StudentYear);
                return Json(new { data = result, success = true });

            }
            catch (Exception ex)
            {
                CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "Slide admisison _Saving subject Choice ", ClsLanguage.GetCookies("NBApplicationNo") + "   hounors_subjectidlist: " + hounors_subjectid.ToString() + "    collegeidlist:" + "" + "  choicetable_id:" + choicetable_id.ToString() + "   Subsidiary2_subjectidlist:" + "" + "     Compulsory1_subjectidlist:" + "" + "   Compulsory2_subjectidlist:" + "");
                return Json(new { data = new Student_Admission_Choicesubject(), success = true });
            }


        }
        public class testincreent
        {
            static int a = 0;
            public static int add()
            {
                return a++;
            }
        }
    }

}