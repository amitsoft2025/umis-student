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

using Website.Areas.StudentV.Models;

namespace Website.Areas.StudentV.Controllers
{
    [CookiesExpireFilterVoc]
    public class HomevController : Controller
    {
        // GET: StudentV/Homev
        [VerifyUrlFilterVoc]
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
            var res1 = bl.CheckAdmission_details(sessionid);
            ViewBag.IsDocVerify = res1.IsDocVerify;
            ViewBag.IsAdmisApplied = res1.IsApplied;
            ViewBag.IsAppliedDate = res1.IsAppliedDate;
            ViewBag.IsDocVerifyDate = res1.IsDocVerifyDate;
            ViewBag.isfeesubmitt = res1.isfeesubmitt;
            ViewBag.IsfeesubmitDate = res1.IsfeesubmitDate;
            ViewBag.castecategory = obj1.CastCategory;
            ViewBag.incomecertificate_iseligible = obj1.incomecertificate_iseligible;
            ViewBag.incomecertificate = obj1.incomecertificate;
            ViewBag.sessionid = obj1.session;
            if (obj1 != null)
            {
                int educationtype = obj1.EducationType;
                var dateextend = ob.CheckStudentAddmisionExtendDate(sessionid, educationtype);
                ViewBag.addmissionExtenddate = dateextend.Status;
                ViewBag.addmissionExtenddateValue = dateextend.ExtendDate;
                var datestart = ob.CheckStudentAddmisionStartDate(sessionid, educationtype);
                ViewBag.addmissionStartdate = datestart.Status;
                ViewBag.addmissionStartdateValue = datestart.startdate;
                ViewBag.IsApplied = ob.CheckStudentApplied(sessionid).Status;
                //ViewBag.IsVerify = ob.CheckStudentVerify(sessionid).Status;
                var dateextenddoc = ob.documnetCheckStudentAddmisionExtendDate(sessionid, educationtype);
                ViewBag.addmissionExtenddateValuedoc = dateextenddoc.ExtendDate;
                var datestartdoc = ob.documentCheckStudentAddmisionStartDate(sessionid, educationtype);
                ViewBag.addmissionStartdateValuedoc = datestartdoc.startdate;
                ViewBag.rejectreason = ob.CheckDocumentVerification(sessionid).rejectreason;
            }
            var obj = ob.CheckStudentAdmission(sessionid);
            if (obj.Status == true)
            {

                ViewBag.Status = obj.Status;
                ViewBag.Course = obj.CourseName;
                ViewBag.College = obj.CollegeName;
                ViewBag.Stream = obj.StreamName;
            }
            else
            {
                ViewBag.Status = false;
                ViewBag.Course = "";
                ViewBag.College = "";
            }

            //if (ViewBag.sessionid==43)
            //{ 
            //ViewBag.BasicInforMationDiv = false;
            //ViewBag.SpotAdmissionDiv = true;           
            //ViewBag.meritListdiv = true;
            //ViewBag.ApplicationFormApplyDiv = true;
            //ViewBag.EnrollMentFeeDiv = true;
            //ViewBag.ExamFeeDiv = true;
            //}

            Recruitment rec = new Recruitment();
            RecruitmentList reclist = new RecruitmentList();
            ExamForm exam = new ExamForm();
            List<EaxmFeesSubmit> subjectlist = new List<EaxmFeesSubmit>();
            ViewBag.isback = 0;
            reclist = rec.view1customfeesubmittedstudentdetailList(obj1.Id);
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
                ViewBag.CourseCategory = obj1.CourseCategory;
                // add manaul courseyearid 11
                if (obj1.CourseCategory == 26)//26 bca  --27  bba  --30 Bio tech
                {

                    //subjectlist = exam.backFeesDetailSubjectlist_Vocational(obj1.CourseCategory, 1124, 10, obj1.session ,rec.collegeid, obj1.Id);
                    //if (subjectlist.Count > 0)
                    //{
                    //    ViewBag.isback = 1;
                    //    ViewBag.courseyearidenc1 = EncriptDecript.Encrypt(10.ToString());
                    //}
                    subjectlist = exam.backFeesDetailSubjectlist_Vocational(obj1.CourseCategory, 1124, 11, obj1.session, rec.collegeid, obj1.Id); if (subjectlist.Count > 0)
                    {
                        ViewBag.isbackpart2 = 1;
                        ViewBag.courseyearidenc2 = EncriptDecript.Encrypt(11.ToString());
                    }
                    //subjectlist = exam.backFeesDetailSubjectlist_Vocational(obj1.CourseCategory, 1124, 12, obj1.session, rec.collegeid, obj1.Id); if (subjectlist.Count > 0)
                    //{
                    //    ViewBag.isbackpart3 = 1;
                    //    ViewBag.courseyearidenc3 = EncriptDecript.Encrypt(12.ToString());
                    //}
                    subjectlist = exam.backFeesDetailSubjectlist_Vocational(obj1.CourseCategory, 1124, 13, obj1.session, rec.collegeid, obj1.Id); if (subjectlist.Count > 0)
                    {
                        ViewBag.isbackpart4 = 1;
                        ViewBag.courseyearidenc4 = EncriptDecript.Encrypt(13.ToString());
                    }
                    //subjectlist = exam.backFeesDetailSubjectlist_Vocational(obj1.CourseCategory, 1124, 14, obj1.session, rec.collegeid, obj1.Id); if (subjectlist.Count > 0)
                    //{
                    //    ViewBag.isbackpart5 = 1;
                    //    ViewBag.courseyearidenc5 = EncriptDecript.Encrypt(14.ToString());
                    //}
                    subjectlist = exam.backFeesDetailSubjectlist_Vocational(obj1.CourseCategory, 1124, 15, obj1.session, rec.collegeid, obj1.Id); if (subjectlist.Count > 0)
                    {
                        ViewBag.isbackpart6 = 1;
                        ViewBag.courseyearidenc6 = EncriptDecript.Encrypt(15.ToString());
                    }


                }
                if (obj1.CourseCategory == 27)//26 bca  --27  bba  --30 Bio tech
                {

                    subjectlist = exam.backFeesDetailSubjectlist_Vocational(obj1.CourseCategory, 1124, 18, obj1.session, rec.collegeid, obj1.Id);
                    if (subjectlist.Count > 0)
                    {
                        ViewBag.isback = 1;
                        ViewBag.courseyearidenc1 = EncriptDecript.Encrypt(18.ToString());
                    }
                    //subjectlist = exam.backFeesDetailSubjectlist_Vocational(obj1.CourseCategory, 1124, 19, obj1.session, rec.collegeid, obj1.Id); if (subjectlist.Count > 0)
                    //{
                    //    ViewBag.isbackpart2 = 1;
                    //    ViewBag.courseyearidenc2 = EncriptDecript.Encrypt(19.ToString());
                    //}
                    //subjectlist = exam.backFeesDetailSubjectlist_Vocational(obj1.CourseCategory, 1124, 20, obj1.session, rec.collegeid, obj1.Id); if (subjectlist.Count > 0)
                    //{
                    //    ViewBag.isbackpart3 = 1;
                    //    ViewBag.courseyearidenc3 = EncriptDecript.Encrypt(20.ToString());
                    //}
                  

                }
                if (obj1.CourseCategory == 30)//26 bca  --27  bba  --30 Bio tech
                {

                    subjectlist = exam.backFeesDetailSubjectlist_Vocational(obj1.CourseCategory, 1125, 42, obj1.session, rec.collegeid, obj1.Id);
                    if (subjectlist.Count > 0)
                    {
                        ViewBag.isback = 1;
                        ViewBag.courseyearidenc1 = EncriptDecript.Encrypt(42.ToString());
                    }
                    //subjectlist = exam.backFeesDetailSubjectlist_Vocational(obj1.CourseCategory, 1125, 43, obj1.session, rec.collegeid, obj1.Id); if (subjectlist.Count > 0)
                    //{
                    //    ViewBag.isbackpart2 = 1;
                    //    ViewBag.courseyearidenc2 = EncriptDecript.Encrypt(43.ToString());
                    //}
                    //subjectlist = exam.backFeesDetailSubjectlist_Vocational(obj1.CourseCategory, 1125, 44, obj1.session, rec.collegeid, obj1.Id); if (subjectlist.Count > 0)
                    //{
                    //    ViewBag.isbackpart3 = 1;
                    //    ViewBag.courseyearidenc3 = EncriptDecript.Encrypt(44.ToString());
                    //}               
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
                    Amount1 = Convert.ToDecimal(result.Fees);
                    var obj = sbi.encriptData(Amount1, EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENBApplicationNo")), "studentV/homev/PGSucess", "studentV/homev/PGFailed");
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
                feestruckture = stlogin.FeesDetailsstructure(result.Collegeid, result.coursecategoryid, result.sessionid, result.CastCategory, result.streamcategoryid, 0,0,0);
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
                    Amount1 = Convert.ToDecimal(feestruckture.Sum(x => x.amount));
                    if (Amount1 <= 0)
                    {
                        TempData["msgerror"] = "Amount Should be greater than 0 !!";
                        return RedirectToAction("AdmissionFeeSubmit", "Homev");
                    }
                    // Amount1 = 1;
                    var obj = sbi.encriptDataadmission(Amount1, EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENBApplicationNo")), "studentV/homev/PGSucessadmission", "studentV/homev/PGFailedadmission", result.mid, result.mkey, result.Collegeid, obj1.StudentYear);
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

                obj = stlogin.FeesDetails(Convert.ToInt32(EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENNBStID"))));
                feestruckture = stlogin.FeesDetailsstructure(obj.Collegeid, obj.coursecategoryid, obj.sessionid, obj.CastCategory, obj.streamcategoryid,0,0,0);
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

        [VerifyUrlFilterVoc]
        public ActionResult BasicDetail()
        {

            Commn_master com = new Commn_master();
            ViewBag.Admissiontype = com.getcommonMaster("AdmissionType");
            ViewBag.Educationtype = com.getcommonMaster("EducationType");
           // ViewBag.Gender = com.getcommonMaster("Gender");
            //ViewBag.CasteCategory = com.getcommonMaster("CasteCategory");
            ViewBag.bloodgroup = com.Getbloodgroup("Select");
            Country cont = new Country();
            ViewBag.country = cont.GetAllCountries();
            ViewBag.Pcountry = cont.GetAllCountries();
            ViewBag.stitle = com.getcommonMaster("Title");
            //ViewBag.ftitle = com.getcommonMaster("TitleM");
            ViewBag.Nationality = com.getcommonMaster("Nationality");
            ViewBag.Religion = com.getcommonMaster("Religion");
            string ApplicationID = ClsLanguage.GetCookies("NBApplicationNo");
            StudentLogin tblST = new StudentLogin();
            var obj = tblST.BasicDetail(ApplicationID);
            ViewBag.boardtype = CommonMethod.Boradtype().Where(x => x.boardid == 1 || x.boardid == 2 || x.boardid == 3);
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
            AcademicSession ac = new AcademicSession();
            ViewBag.check_admissionopen = com.check_admission_open(ac.GetAcademiccurrentSession().ID, Convert.ToInt32(CommonSetting.Commonid.EducationtypeVoc));
            BL_PrintApplication ob = new BL_PrintApplication();
            var objrecritiny = ob.CheckStudentAdmission(ac.GetAcademiccurrentSession().ID);
            ViewBag.Statusrecruitny = false;
            StudentAdmissionQualification stad = new StudentAdmissionQualification();
            ViewBag.Qualification = stad.GetQualifiationMasterOldStudent();
            if (objrecritiny.Status == true)
            {
                ViewBag.Statusrecruitny = objrecritiny.Status;
            }
            return View(obj);

        }
        [HttpPost]
        public ActionResult BasicDetail(Login objlogin, HttpPostedFileBase sign, HttpPostedFileBase photo)
        {

            StudentLogin st = new StudentLogin();
            AcademicSession ac = new AcademicSession();
            Commn_master com = new Commn_master();
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
            ViewBag.boardtype = CommonMethod.Boradtype().Where(x => x.boardid == 1 || x.boardid == 2 || x.boardid == 3);
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
           // objlogin.previous_qua_id = obj.previous_qua_id;
            objlogin.session = obj.session;
            ViewBag.eduid = obj.EducationType;
            StudentAdmissionQualification stad = new StudentAdmissionQualification();
            ViewBag.Qualification = stad.GetQualifiationMasterOldStudent();
            ViewBag.Statusrecruitny = false;
            BL_PrintApplication ob = new BL_PrintApplication();
            ViewBag.check_admissionopen = com.check_admission_open(ac.GetAcademiccurrentSession().ID, Convert.ToInt32(CommonSetting.Commonid.EducationtypeVoc));

            var objrecritiny = ob.CheckStudentAdmission(ac.GetAcademiccurrentSession().ID);
            if (objrecritiny.Status == true)
            {
                ViewBag.Statusrecruitny = objrecritiny.Status;
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
                        // CommonMethod.PrintLog(ex);
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
                        // CommonMethod.PrintLog(ex);
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
                CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "Student Qualification Upload method", obj.ApplicationNo + " " + jsonstr);

            }
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


            //var result = obj.DeleteQualifiationByID(Convert.ToInt32(enID));
            //if (res.QualicationType == 2 || res.QualicationType == 3 || res.QualicationType == 4)
            //{
            //    var result = obj.DeleteQualifiationByID(Convert.ToInt32(enID));
            //    TempData["Msg"] = result.Msg;
            //}
            //else
            //{
                var result = obj.DeleteQualifiationByIDwithoutchoice(Convert.ToInt32(enID));
                TempData["Msg"] = result.Msg;
            //}

      
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
                if (ob.EncriptedID != "0" && ob.EncriptedID.Length > 0)
                {
                    if (ob.FileName != null)
                    {
                        var PicName = ob.FileName;

                        MemoryStream file = this.changeBase64ToImage(ob);
                        string[] str = ob.FileName.Split('.');
                        string ext = str.LastOrDefault();
                        string name = System.DateTime.Now.ToString("dd_MM_yyyy_HH_mm_ss") + "_Document_" + ob.ApplicationNo + "." + ext;
                        string myBucketName = "mungeruniversity"; //your s3 bucket name goes here  
                        string s3DirectoryName = "Student/Document";
                        string s3FileName = @name;
                        bool a;
                        ob.FileName = name;
                        AmazonUploader myUploader = new AmazonUploader();
                        a = myUploader.sendMyFileToS3(file, myBucketName, s3DirectoryName, s3FileName);
                        ob.ID = Convert.ToInt32(EncriptDecript.Decrypt(ob.EncriptedID));
                        var result = obj.SaveDocument(ob);
                        var str1 = result.Msg;
                        return Json(str1, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        ob.FileName = ob.hfile;
                        ob.ID = Convert.ToInt32(EncriptDecript.Decrypt(ob.EncriptedID));
                        var result = obj.SaveDocument(ob);
                        var str1 = result.Msg;
                        return Json(str1, JsonRequestBehavior.AllowGet);

                    }
                }
                else
                {
                    MemoryStream file = this.changeBase64ToImage(ob);

                    string[] str = ob.FileName.Split('.');
                    string ext = str.LastOrDefault();
                    string name = System.DateTime.Now.ToString("dd_MM_yyyy_HH_mm_ss") + "_Document_" + ob.ApplicationNo + "." + ext;
                    string myBucketName = "mungeruniversity"; //your s3 bucket name goes here  
                    string s3DirectoryName = "Student/Document";
                    string s3FileName = @name;
                    bool a;
                    ob.FileName = name;
                    AmazonUploader myUploader = new AmazonUploader();
                    a = myUploader.sendMyFileToS3(file, myBucketName, s3DirectoryName, s3FileName);
                    //ob.ID = Convert.ToInt32(EncriptDecript.Decrypt(ob.EncriptedID));
                    var result = obj.SaveDocument(ob);
                    var str1 = result.Msg;
                    return Json(str1, JsonRequestBehavior.AllowGet);
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
        [VerifyUrlFilterVoc]
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
            ViewBag.sessionid = objl22.session;
            var obj1 = PritApp.GetAppLicationDataAdmin(objl22.Id);
            ViewBag.check_admissionopen = com.check_admission_open(ad.GetAcademiccurrentSession().ID, Convert.ToInt32(CommonSetting.Commonid.EducationtypeVoc));
            BL_PrintApplication ob = new BL_PrintApplication();
            var objrecritiny = ob.CheckStudentAdmission(ad.GetAcademiccurrentSession().ID);
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
            ViewBag.sessionid = lo.session;
            DocumentUpload obj = new DocumentUpload();
            DocumentUploadList subdoc = new DocumentUploadList();
            subdoc = obj.DocumentdetailList(1, 10);
            if (lo.CastCategory == 0)
            {
                fee.Status = false;
                TempData["msgfees"] = "Please upload basic detials!!!";
                return RedirectToAction("FeesSubmit");

            }
            
            if (lo.EducationType == Convert.ToInt32(CommonSetting.Commonid.EducationtypeVoc))
            {
                var list10 = list.Where(m => m.QualicationType == Convert.ToInt32(CommonSetting.commQualification.Ten)).ToList();
                if (list10.Count == 0)
                {
                    fee.Status = false;
                    TempData["msgfees"] = "Please upload your Secondary Board qualification certificate !!!";
                    return RedirectToAction("FeesSubmit");
                }
            }
            if (lo.EducationType == Convert.ToInt32(CommonSetting.Commonid.EducationtypeVoc))
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
                Student_Admission_Choicesubject Choicesubject = new Student_Admission_Choicesubject();
                var Choicesubjectlist = Choicesubject.viewst_choicesubject(lo.Id);
                if (Choicesubjectlist.Count == 0)
                {
                    fee.Status = false;
                    TempData["msgfees"] = "Please first select choices for subject and college !!!";
                    return RedirectToAction("FeesSubmit");
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
                if (objl22.IsFeeSubmit == 0)
                {
                    stlogin.Status = false;

                    //TempData["msgfees"] = "Please Try Tomorrow with new paymentgetway ..Today Full Fill All Form Detail";
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
        [HttpPost, ValidateInput(false)]
        public ActionResult PrintApplicationCallForm()
        {
            string InpuData = HttpContext.Request.Form["data"];       //Div Content will be fetched from form data.
                                                                      //If You find this error in above line 
                                                                      //Error: A potentially dangerous Request.Form value was detected from the client (data="<html><head><title><...").
                                                                      //then add this in your Web.config file.
                                                                      // <httpRuntime requestValidationMode="2.0" /> inside   <system.web>  </system.web>
            string PageType = HttpContext.Request.Form["PageType"];  //here we will recieve Page Type sent from front end.
            string dataname = HttpContext.Request.Form["dataname"];  // here we declare for filename for download
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
        //[HttpPost]
        //public JsonResult AddNewQualification()
        //{
        //    StudentAdmissionQualification obj = new StudentAdmissionQualification();
        //    try
        //    {
        //        if (Request.Form.Count > 0)
        //        {
        //            StudentAdmissionQualification ob = new StudentAdmissionQualification();
        //            ob.ID = Convert.ToInt32(Request.Form["ID"] == "" ? "0" : Request.Form["ID"]);
        //            ob.QualicationType = Convert.ToInt32(Request.Form["QualicationType"] == "" ? "0" : Request.Form["QualicationType"]);
        //            ob.PassingYear = CommonSetting.RemoveSpecialChars((Request.Form["PassingYear"]));
        //            ob.Percentage = Convert.ToDecimal(Request.Form["Percentage"] == "" ? "0" : Request.Form["Percentage"]);
        //            if (ob.Percentage == 0)
        //            {
        //                return Json("Please Fill Aggregate Percentage ,Or Fill Again form !!", JsonRequestBehavior.AllowGet);
        //            }
        //            ob.Board_UniversityName = CommonSetting.RemoveSpecialChars((Request.Form["Board_UniversityName"]));
        //            ob.RollNo = CommonSetting.RemoveSpecialChars((Request.Form["RollNo"]));
        //            ob.hfile = ((Request.Form["hfile"]));
        //            ob.ApplicationNo = ClsLanguage.GetCookies("NBApplicationNo");
        //            ob.SID = Convert.ToInt32(ClsLanguage.GetCookies("NBStID"));
        //            ob.session = Convert.ToInt32(ClsLanguage.GetCookies("NBSission"));
        //            string jsonstring = JsonConvert.SerializeObject(ob);
        //            try
        //            {
        //                if (Request.Files.Count > 0)
        //                {
        //                    {
        //                        for (int i = 0; i < Request.Files.Count; i++)
        //                        {
        //                            if (Request.Files.GetKey(i) == "file")
        //                            {
        //                                HttpPostedFileBase fileUpload = Request.Files.Get(i);
        //                                if (Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
        //                                {
        //                                    string[] testfiles = fileUpload.FileName.Split(new char[] { '\\' });
        //                                }
        //                                Stream st1 = fileUpload.InputStream;
        //                                string name = Path.GetExtension(fileUpload.FileName);
        //                                try
        //                                {
        //                                    string myBucketName = "mungeruniversity"; //your s3 bucket name goes here  
        //                                    string s3DirectoryName = "Student/Document";
        //                                    string s3FileName = System.DateTime.Now.ToString("dd_MM_yyyy_HH:mm:ss") + "_Qualification_" + ob.ApplicationNo + name;
        //                                    s3FileName = s3FileName.Replace(" ", "");
        //                                    ob.FileURl = s3FileName;
        //                                    bool a;
        //                                    AmazonUploader myUploader = new AmazonUploader();
        //                                    a = myUploader.sendMyFileToFolder(photo,Server.MapPath("~/App_Data/uploads"),s3FileName);
        //                                }
        //                                catch (Exception ex)
        //                                {
        //                                    CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "Student Qualicafication document upload  image or file upload error:", "Add Qualification");

        //                                }
        //                            }
        //                        }
        //                    }
        //                }

        //                if (ob.ID > 0)
        //                {
        //                    if (ob.FileURl != null)
        //                    {
        //                        //var PicName = ob.FileURl;
        //                        //MemoryStream file = this.Base64ToImage(ob);
        //                        //string[] str = ob.FileURl.Split('.');
        //                        //string ext = str.LastOrDefault();
        //                        //string name = System.DateTime.Now.ToString("dd_MM_yyyy_HH:mm:ss") + "_Qualification_" + ob.ApplicationNo + "." + ext;
        //                        //string myBucketName = "mungeruniversity"; //your s3 bucket name goes here  
        //                        //string s3DirectoryName = "Student/Document";
        //                        //string s3FileName = @name;
        //                        //bool a;
        //                        //ob.FileURl = name;
        //                        //AmazonUploader myUploader = new AmazonUploader();
        //                        //a = myUploader.sendMyFileToS3(file, myBucketName, s3DirectoryName, s3FileName);
        //                        //  var result = obj.SaveQualificationDetails(ob);
        //                        if (ob.session == 39)
        //                        {
        //                            var result = ob.SaveQualificationDetailsForOldStudent(ob);

        //                            return Json(result, JsonRequestBehavior.AllowGet);
        //                        }
        //                        else
        //                        {
        //                            var result = ob.SaveQualificationDetails(ob);
        //                            return Json(result, JsonRequestBehavior.AllowGet);
        //                        }

        //                    }
        //                    else
        //                    {
        //                        ob.FileURl = ob.hfile;

        //                        if (ob.session == 39)
        //                        {
        //                            var result = ob.SaveQualificationDetailsForOldStudent(ob);
        //                            return Json(result, JsonRequestBehavior.AllowGet);
        //                        }
        //                        else
        //                        {    var result = obj.SaveQualificationDetails(ob);
        //                            return Json(result, JsonRequestBehavior.AllowGet);
        //                        }


        //                    }
        //                }
        //                else
        //                {

        //                    //MemoryStream file = this.Base64ToImage(ob);
        //                    //string[] str = ob.FileURl.Split('.');
        //                    //string ext = str.LastOrDefault();
        //                    //string name = System.DateTime.Now.ToString("dd_MM_yyyy_HH:mm:ss") + "_Qualification_" + ob.ApplicationNo + "." + ext;
        //                    //string myBucketName = "mungeruniversity"; //your s3 bucket name goes here  
        //                    //string s3DirectoryName = "Student/Document";
        //                    //string s3FileName = @name;
        //                    //bool a;
        //                    //ob.FileURl = name;
        //                    //AmazonUploader myUploader = new AmazonUploader();
        //                    //a = myUploader.sendMyFileToS3(file, myBucketName, s3DirectoryName, s3FileName);
        //                    //var result = obj.SaveQualificationDetails(ob);
        //                    if (ob.session == 39)
        //                    {
        //                        var result = ob.SaveQualificationDetailsForOldStudent(ob);
        //                        if (result.Status)
        //                        {
        //                            return Json(result, JsonRequestBehavior.AllowGet);
        //                        }
        //                    }
        //                    else
        //                    {
        //                        var result = ob.SaveQualificationDetails(ob);
        //                        if (result.Status)
        //                        {
        //                            return Json(result, JsonRequestBehavior.AllowGet);
        //                        }
        //                    }
        //                    //return Json(result, JsonRequestBehavior.AllowGet);
        //                }
        //            }
        //            catch (Exception ex)
        //            {
        //                CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "Student AddNewQualification Method", ob.ApplicationNo + " " + jsonstring);
        //                return Json("error", JsonRequestBehavior.AllowGet);
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "Student AddNewQualification Method", ClsLanguage.GetCookies("NBApplicationNo"));
        //        return Json("error", JsonRequestBehavior.AllowGet);
        //    }
        //    return Json("error", JsonRequestBehavior.AllowGet);

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
                doc.paperTotalMarks = Request.Form["paperTotalMarks"];
                doc.paperMarksObtain = Request.Form["paperMarksObtain"];
                doc.sublist = Request.Form["sublist"];
                doc.subper = Request.Form["subper"];
                doc.TotalMarks = Request.Form["TotalMarks"];
                doc.MarksObtain = Request.Form["MarksObtain"];
                doc.SubID = Request.Form["SubID"];
                doc.boardtype = Convert.ToInt32((Request.Form["boardtype"]));
                doc.hfile = Request.Form["hfile"];
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
                if (doc.paperTotalMarks == "" || doc.paperTotalMarks == "0")
                {
                    StudentAdmissionQualification doc1 = new StudentAdmissionQualification();
                    doc1.Msg = "Please Fill Total Paper Marks  ,Or Fill Again form !!";
                    return Json(doc1, JsonRequestBehavior.AllowGet);
                }
                if (doc.paperMarksObtain == "" || doc.paperMarksObtain == "0")
                {
                    StudentAdmissionQualification doc1 = new StudentAdmissionQualification();
                    doc1.Msg = "Please Fill Aggregate Total Obtain Marks  ,Or Fill Again form !!";
                    return Json(doc1, JsonRequestBehavior.AllowGet);
                }
                if (Convert.ToDecimal(doc.paperMarksObtain) > Convert.ToDecimal(doc.paperTotalMarks))
                {
                    StudentAdmissionQualification doc1 = new StudentAdmissionQualification();
                    doc1.Msg = "Paper Obtain Marks  should be less Paper Total Marks  ,Or Fill Again form !!";
                    return Json(doc1, JsonRequestBehavior.AllowGet);
                }
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
                    if (objl.prevoiusboardid == 2)
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
                    if (objl.prevoiusboardid == 1 || objl.prevoiusboardid == 0)
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

                decimal final = aggper * 100 / totalm;
                doc.Percentage = final;
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
                var insertby = ClsLanguage.GetCookies("ENNBStID");
                if (insertby != "")
                {
                    doc.InsertedBy = Convert.ToInt32(EncriptDecript.Decrypt(insertby));
                }
                //  return Json(null, JsonRequestBehavior.AllowGet);
                //result = ob.SaveQualificationDetails(doc);
                result = ob.SaveQualificationDetailsForOldStudent(doc);
                List<StudentPreviousQualification> mem = new List<StudentPreviousQualification>();
                if (doc.QualicationType != 1)
                {
                    if (objl.prevoiusboardid == 1 || objl.prevoiusboardid == 0 || objl.prevoiusboardid == 2)
                    {
                        for (int i = 0; i < sublistarr.Length; i++)
                        {
                            obj.Insertsingle(SubIDarr[i], result.ScopeIdentity, sublistarr[i], subperarr[i], TotalMarksarr[i], MarksObtainarr[i]);
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
        public JsonResult savest_choicesubject(string collegeidlist, string hounors_subjectidlist)
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
                CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "Saving subject Choice ", ClsLanguage.GetCookies("NBApplicationNo") + "   hounors_subjectidlist:  "+ hounors_subjectidlist + " + collegeidlist + " + collegeidlist + "  +  +  + Subsidiary1_subjectidlist + + Subsidiary2_subjectidlist + + Compulsory1_subjectidlist + :");
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


        [VerifyUrlFilterVoc]
        public ActionResult SelectSubject(int id = 0)
        {
            StudentAdmissionQualification obj = new StudentAdmissionQualification();
            var ApplicationNo = ClsLanguage.GetCookies("NBApplicationNo");
            Login objl = new Login();
            StudentLogin objs = new StudentLogin();
            AcademicSession ac = new AcademicSession();
            objl = objs.BasicDetail(ApplicationNo);
            if (objl != null)
            {
                QualiSubject_bind(objl.previous_qua_id);
                Subject_bind(objl.previous_qua_id);
            }
            Commn_master com = new Commn_master();
            BL_CollegeMaster obj111 = new BL_CollegeMaster();
            CollageList sub = new CollageList();
            //  sub = obj111.collagedetailviewlistdropalloted(objl.CourseCategory, ac.GetAcademiccurrentSession().ID);
            ViewBag.gender = objl.Gender;
            if (objl.CourseCategory == Convert.ToInt32(CommonSetting.coursecategory.bca))
            {
                ViewBag.coursestreamid = Convert.ToInt32(CommonSetting.Coursestreamcategoryid.BCAstreamid);
            }
            else if (objl.CourseCategory == Convert.ToInt32(CommonSetting.coursecategory.bba)) 
            {
                ViewBag.coursestreamid = Convert.ToInt32(CommonSetting.Coursestreamcategoryid.BBAstreamid);
            }
            else if (objl.CourseCategory == Convert.ToInt32(CommonSetting.coursecategory.BioTech)) 
            {
                ViewBag.coursestreamid = Convert.ToInt32(CommonSetting.Coursestreamcategoryid.Biotechstreamid);
            }


            if (objl.Gender == 9)
            {
                sub = obj111.collagedetailviewlistdropallotedmalihacollege(objl.CourseCategory, ac.GetAcademiccurrentSession().ID);
            }
            else
            {
                sub = obj111.collagedetailviewlistdropalloted(objl.CourseCategory, ac.GetAcademiccurrentSession().ID);
            }
            var countcoll = sub.qlist.Count;
            if (countcoll < 2)
            {
                ViewBag.Collegecount = false;
            }
            else
            {
                ViewBag.Collegecount = true;
            }

            ViewBag.College = sub.qlist;
            ViewBag.sessionid = objl.session;
            ViewBag.check_admissionopen = com.check_admission_open(ac.GetAcademiccurrentSession().ID, Convert.ToInt32(CommonSetting.Commonid.EducationtypeVoc));

            ViewBag.Course = com.getcommonMaster("Course", objl.EducationType);
            ViewBag.Coursecategoryid = objl.CourseCategory;
            List<SelectListItem> ob = new List<SelectListItem>();
            for (int i = 1995; i <= System.DateTime.Now.Year; i++)
            {
                ob.Add(new SelectListItem { Text = i.ToString(), Value = i.ToString() });
            }
            ob.OrderByDescending(x => x.Value);
            ViewBag.year = ob;
            List<StudentPreviousQualification> list = new List<StudentPreviousQualification>();
            for (int i = 0; i < 1; i++)
            {
                list.Add(new StudentPreviousQualification());
            }
            BL_PrintApplication ob534 = new BL_PrintApplication();
            var objrecritiny = ob534.CheckStudentAdmission(ac.GetAcademiccurrentSession().ID);
            ViewBag.Statusrecruitny = false;
            if (objrecritiny.Status == true)
            {
                ViewBag.Statusrecruitny = objrecritiny.Status;
            }
            StudentPreviousQualification StudentPrevious = new StudentPreviousQualification();
            var tuple = new Tuple<StudentAdmissionQualification, List<StudentPreviousQualification>, Login>(obj, list, objl);
            ViewBag.percentage = StudentPrevious.getqualify_percentage(objl.CourseCategory, objl.issame_stream);
            var countlist = StudentPrevious.GetSubjectPercentageData(objl.ApplicationNo);

            if (objl.previous_qua_id != 11)
            {
                if (objl.session != 39)
                {
                    if (countlist.Count == 0)
                    {
                        TempData["selectsubmsg"] = "Please First Upload your Qualification!!";
                        return View(tuple);
                    }
                }
            }
            //if (objl.issame_stream == false)
            //{
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
            //}
            //if (objl.issame_stream == true)
            //{
            //    var countlistcheck = StudentPrevious.GetSubjecissamestream_percentage(objl.Id.ToString(), objl.previous_qua_id);

            //    if (countlistcheck == null)
            //    {
            //        TempData["selectsubmsg"] = "First Upload For Previous year Qualification !!!";
            //    }
            //    else
            //    {
            //        if (countlistcheck.Status == false)
            //        {
            //            TempData["selectsubmsg"] = countlistcheck.Msg;
            //        }
            //    }
            //}

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
            if (objl.CourseCategory == Convert.ToInt32(CommonSetting.coursecategory.bca))
            {
                var ischeck_bcain_math = StudentPrevious.ischeck_bcain_math(objl.Id.ToString());
                if (ischeck_bcain_math.Status == true)
                {


                    TempData["selectsubmsg"] = ischeck_bcain_math.Msg;
                    return View(tuple);
                }
            }
            if (objl.CourseCategory == Convert.ToInt32(CommonSetting.coursecategory.BioTech))
            {
                var ischeck_biotechin_biology = StudentPrevious.ischeck_biotechin_biology(objl.Id.ToString());
                if (ischeck_biotechin_biology.Status == true)
                {
                    TempData["selectsubmsg"] = ischeck_biotechin_biology.Msg;
                    return View(tuple);
                }
            }
            return View(tuple);
        }
        public JsonResult getcollegelist(int Coursecategoryid)
        {
            BL_CollegeMaster obj111 = new BL_CollegeMaster();
            CollageList sub = new CollageList();
            AcademicSession ac = new AcademicSession();
            sub = obj111.collagedetailviewlistdropalloted(Coursecategoryid, ac.GetAcademiccurrentSession().ID);
            return Json(new { data = sub.qlist, success = true });
        }
        public JsonResult getcollegesubjects(int id)
        {
            StudentAdmissionQualification obj = new StudentAdmissionQualification();
            var ApplicationNo = ClsLanguage.GetCookies("NBApplicationNo");
            Login objl = new Login();
            StudentLogin objs = new StudentLogin();
            objl = objs.BasicDetail(ApplicationNo);

            BL_StreamMaster objStream = new BL_StreamMaster();
            var result = objStream.getcollegesubjects(objl.Id, 10, id);
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

            if (lo.EducationType == Convert.ToInt32(CommonSetting.Commonid.EducationtypeVoc))
            {
                var list10 = list.Where(m => m.QualicationType == Convert.ToInt32(CommonSetting.commQualification.Ten)).ToList();
                if (list10.Count == 0)
                {
                    fee.Status = false;
                    fee.Message = "please upload your Secondary Board qualification certificate";
                    return Json(fee, JsonRequestBehavior.AllowGet);
                }
            }
            if (lo.EducationType == Convert.ToInt32(CommonSetting.Commonid.EducationtypeVoc))
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
                if (objl.session == 39)
                { return RedirectToAction("PreviousyearQualificationO/" + id, "Homev"); }
                ViewBag.boardtype = CommonMethod.BoradtypePrevious().Where(x => x.boardid == 1 || x.boardid == 2);

                if (objl.prevoiusboardid == 2)
                {
                    return RedirectToAction("PreviousyearQualificationO/" + id, "Homev");
                }
                if (objl.prevoiusboardid == 3)
                {
                    return RedirectToAction("PreviousyearQualificationP/" + id, "Homev");
                }

                ViewBag.IsSubmit = objl.IsFeeSubmit;
                ViewBag.boardname = CommonMethod.Boradtype().ToList().Where(x => x.boardid == 1).FirstOrDefault().boardname;
                string enID = EncriptDecript.Decrypt(id);
                int eID = 0;
                StudentAdmissionQualification obj = new StudentAdmissionQualification();
                StudentAdmissionQualification objst = new StudentAdmissionQualification();
                Commn_master com = new Commn_master();
                ViewBag.check_admissionopen = com.check_admission_open(ac.GetAcademiccurrentSession().ID, Convert.ToInt32(CommonSetting.Commonid.EducationtypeVoc));

                StudentPreviousQualification objp = new StudentPreviousQualification();
                List<StudentPreviousQualification> result = objp.GetSubjectPercentageData(ApplicationNo);


                List<SelectListItem> ob = new List<SelectListItem>();
                for (int i = System.DateTime.Now.Year; i >= 1995; i--)
                {
                    ob.Add(new SelectListItem { Text = i.ToString(), Value = i.ToString() });
                }
                // ob.OrderByDescending(x => x.Value);
                ViewBag.year = ob;
                List<StudentPreviousQualification> list = new List<StudentPreviousQualification>();
                for (int i = 0; i < 7; i++)
                {
                    list.Add(new StudentPreviousQualification());

                }
                BL_PrintApplication ob534 = new BL_PrintApplication();
                var objrecritiny = ob534.CheckStudentAdmission(ac.GetAcademiccurrentSession().ID);
                ViewBag.Statusrecruitny = false;
                if (objrecritiny.Status == true)
                {
                    ViewBag.Statusrecruitny = objrecritiny.Status;
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
                            return RedirectToAction("StudentQualification", "Homev");
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
                            return RedirectToAction("StudentQualification", "Homev");
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
                string enID = EncriptDecript.Decrypt(id);
                int eID = 0;
                StudentAdmissionQualification obj = new StudentAdmissionQualification();
                StudentAdmissionQualification objst = new StudentAdmissionQualification();
                Commn_master com = new Commn_master();
                ViewBag.check_admissionopen = com.check_admission_open(ac.GetAcademiccurrentSession().ID, Convert.ToInt32(CommonSetting.Commonid.EducationtypeVoc));
                ViewBag.boardtype = CommonMethod.BoradtypePrevious().Where(x => x.boardid == 1 || x.boardid == 2);

                StudentPreviousQualification objp = new StudentPreviousQualification();
                List<StudentPreviousQualification> result = objp.GetSubjectPercentageData(ApplicationNo);
                if (objl.session == 39)
                { return RedirectToAction("PreviousyearQualificationO/" + id, "Homev");
                }
                if (objl.prevoiusboardid == 2)
                {
                    return RedirectToAction("PreviousyearQualificationO/" + id, "Homev");
                }
                //if (objl.prevoiusboardid == 3)
                //{
                //    return RedirectToAction("PreviousyearQualificationP/" + id, "Homev");
                //}
                if (objl.prevoiusboardid == 1)
                {
                    return RedirectToAction("PreviousyearQualification/" + id, "Homev");
                }
                if (objl.prevoiusboardid == 0)
                {
                    return RedirectToAction("PreviousyearQualification/" + id, "Homev");
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
                BL_PrintApplication ob534 = new BL_PrintApplication();
                var objrecritiny = ob534.CheckStudentAdmission(ac.GetAcademiccurrentSession().ID);
                ViewBag.Statusrecruitny = false;
                if (objrecritiny.Status == true)
                {
                    ViewBag.Statusrecruitny = objrecritiny.Status;
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
                            return RedirectToAction("StudentQualification", "Homev");
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
                            return RedirectToAction("StudentQualification", "Homev");
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
        [VerifyUrlFilterVoc]
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
      
              
                ViewBag.sessionid = objl.session;
                string enID = EncriptDecript.Decrypt(id);
                int eID = 0;
                StudentAdmissionQualification obj = new StudentAdmissionQualification();
                StudentAdmissionQualification objst = new StudentAdmissionQualification();

                StudentPreviousQualification objp = new StudentPreviousQualification();
                List<StudentPreviousQualification> result = objp.GetSubjectPercentageData(ApplicationNo);
             
                Commn_master com = new Commn_master();
                ViewBag.check_admissionopen = com.check_admission_open(ac.GetAcademiccurrentSession().ID, Convert.ToInt32(CommonSetting.Commonid.EducationtypeVoc));
                ViewBag.boardtype = CommonMethod.BoradtypePrevious().Where(x => x.boardid == 1 || x.boardid == 2);

                if (objl.CastCategory == 0)
                {
                    return RedirectToAction("BasicDetail/" + id, "Homev");
                }
                if (objl.session != 39)
                {

                    if (objl.prevoiusboardid == 1)
                    {
                        return RedirectToAction("PreviousyearQualification/" + id, "Homev");
                    }
                    if (objl.prevoiusboardid == 0)
                    {
                        return RedirectToAction("PreviousyearQualification/" + id, "Homev");
                    }
                    if (objl.prevoiusboardid == 3)
                    {
                        return RedirectToAction("PreviousyearQualificationP/" + id, "Homev");
                    }
                }

                List<SelectListItem> ob = new List<SelectListItem>();
                for (int i = System.DateTime.Now.Year; i >= 1995; i--)
                {
                    ob.Add(new SelectListItem { Text = i.ToString(), Value = i.ToString() });
                }
                // ob.OrderByDescending(x => x.Value);
                ViewBag.year = ob;
                List<StudentPreviousQualification> list = new List<StudentPreviousQualification>();
                for (int i = 0; i < 5; i++)
                {
                    list.Add(new StudentPreviousQualification());

                }
                BL_PrintApplication ob534 = new BL_PrintApplication();
                var objrecritiny = ob534.CheckStudentAdmission(ac.GetAcademiccurrentSession().ID);
                ViewBag.Statusrecruitny = false;
                if (objrecritiny.Status == true)
                {
                    ViewBag.Statusrecruitny = objrecritiny.Status;
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
                            return RedirectToAction("StudentQualification", "Homev");
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
                            return RedirectToAction("StudentQualification", "Homev");
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


        public ActionResult DocumentUpload(string id = "")
        {
            DocumentUpload obj = new DocumentUpload();
            var app = ClsLanguage.GetCookies("NBApplicationNo");
            Login objl = new Login();
            StudentLogin objs = new StudentLogin();
            objl = objs.BasicDetail(app);
            ViewBag.IsSubmit = objl.IsFeeSubmit;
            Docu_bind();
            AcademicSession ac = new AcademicSession();
            Commn_master com = new Commn_master();
            ViewBag.check_admissionopen = com.check_admission_open(ac.GetAcademiccurrentSession().ID, Convert.ToInt32(CommonSetting.Commonid.EducationtypeVoc));

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
        [VerifyUrlFilterVoc]
        public ActionResult StudentQualification(int id = 0)
        {


            var ID = ClsLanguage.GetCookies("NBApplicationNo");
            Login objl = new Login();
            StudentLogin objs = new StudentLogin();
            objl = objs.BasicDetail(ID);
            ViewBag.IsSubmit = objl.IsFeeSubmit;
            ViewBag.sessionid = objl.session;
            if (objl != null)
            {
                Subject_bind(objl.previous_qua_id);
            }
            StudentPreviousQualification obj = new StudentPreviousQualification();
            List<StudentPreviousQualification> list = new List<StudentPreviousQualification>();
            List<StudentPreviousQualification> list1 = new List<StudentPreviousQualification>();
            Commn_master com = new Commn_master();
            AcademicSession ac = new AcademicSession();
            ViewBag.check_admissionopen = com.check_admission_open(ac.GetAcademiccurrentSession().ID, Convert.ToInt32(CommonSetting.Commonid.EducationtypeVoc));

            BL_PrintApplication ob534 = new BL_PrintApplication();
            var objrecritiny = ob534.CheckStudentAdmission(ac.GetAcademiccurrentSession().ID);
            ViewBag.Statusrecruitny = false;
            if (objrecritiny.Status == true)
            {
                ViewBag.Statusrecruitny = objrecritiny.Status;
            }
            list = obj.GetSubjectPercentageData(ID);
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
        [VerifyUrlFilterVoc]
        public ActionResult AdmissionFeeSubmit()
        {
            return RedirectToAction("Index", "Homev");
            AdmissionFeesSubmit stlogin = new AdmissionFeesSubmit();
            AdmissionFeesSubmit obj = new AdmissionFeesSubmit();
            List<AdmissionFeesSubmit> feestruckture = new List<AdmissionFeesSubmit>();
            ViewBag.IsSubmit = obj.IsFeeSubmit;
            AcademicSession ac = new AcademicSession();
            Commn_master com = new Commn_master();
            ViewBag.check_admissionopen = com.check_admission_open(ac.GetAcademiccurrentSession().ID, Convert.ToInt32(CommonSetting.Commonid.EducationtypeVoc));
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
            ViewBag.IsAdmisApplied = res1.IsApplied;
            if (res1.IsApplied == false)
            {
                return RedirectToAction("Index", "Homev");
            }
            if (res1.IsDocVerify == 0)
            {
                return RedirectToAction("Index", "Homev");
            }
            if (res1.IsDocVerify == 2)
            {
                return RedirectToAction("Index", "Homev");
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
            }
            else
            {
                ViewBag.Status = false;
                ViewBag.Course = "";
                ViewBag.College = "";
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
                    feestruckture = stlogin.FeesDetailsstructure(obj.Collegeid, obj1.CourseCategory, sessionid, obj1.CastCategory, obj.streamcategoryid,0,0,0);
                    var tuple = new Tuple<AdmissionFeesSubmit, List<AdmissionFeesSubmit>>(obj, feestruckture);
                    return View(tuple);
                }
                else
                {
                    var tuple1 = new Tuple<AdmissionFeesSubmit, List<AdmissionFeesSubmit>>(obj, feestruckture);
                    return View(tuple1);
                }


            }

            var tuple2 = new Tuple<AdmissionFeesSubmit, List<AdmissionFeesSubmit>>(obj, feestruckture);
            return View(tuple2);
        }
        [HttpPost]
        public ActionResult AdmissionFeeSubmit(int id11 = 0)
        {
            return RedirectToAction("Index", "Home");
            AdmissionFeesSubmit stlogin = new AdmissionFeesSubmit();
            AdmissionFeesSubmit obj = new AdmissionFeesSubmit();
            List<AdmissionFeesSubmit> feestruckture = new List<AdmissionFeesSubmit>();
            ViewBag.IsSubmit = obj.IsFeeSubmit;
            AcademicSession ac = new AcademicSession();
            Commn_master com = new Commn_master();
            ViewBag.check_admissionopen = com.check_admission_open(ac.GetAcademiccurrentSession().ID, Convert.ToInt32(CommonSetting.Commonid.EducationtypeVoc));
            //string Id = ClsLanguage.GetCookies("ENNBStID");
            BL_student_formcomplete bl = new BL_student_formcomplete();
            string ApplicationID = EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENBApplicationNo"));
            StudentLogin tblST = new StudentLogin();
            BL_PrintApplication ob = new BL_PrintApplication();
            int sessionid = ac.GetAcademiccurrentSession().ID;
            var obj1 = tblST.BasicDetail(ApplicationID);
            var res1 = bl.CheckAdmission_details(sessionid);
            ViewBag.IsAdmisApplied = res1.IsApplied;
            ViewBag.IsAppliedDate = res1.IsAppliedDate;
            ViewBag.IsDocVerifyDate = res1.IsDocVerifyDate;
            ViewBag.isfeesubmitt = res1.isfeesubmitt;
            ViewBag.IsfeesubmitDate = res1.IsfeesubmitDate;
            if (obj1 == null)
            {
                return RedirectToAction("Index", "Homev");
            }
            if (ViewBag.IsAdmisApplied == false)
            {
                return RedirectToAction("Index", "Homev");
            }
            if (res1.IsDocVerify == 0)
            {
                return RedirectToAction("Index", "Homev");
            }
            if (res1.IsDocVerify == 2)
            {
                return RedirectToAction("Index", "Homev");
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
            }
            else
            {
                ViewBag.Status = false;
                ViewBag.Course = "";
                ViewBag.College = "";
            }
            if (obj1 != null)
            {
                if (ViewBag.isfeesubmitt != true)
                {
                    if (ViewBag.addmissionExtenddate == true && ViewBag.addmissionStartdate == true)
                    {
                        //  var result = stlogin.AdmissionFeessub(obj1.Id);
                        // feestruckture = stlogin.FeesDetailsstructure(obj.Collegeid, obj1.CourseCategory, sessionid);
                        return RedirectToAction("PGAdmissionGateway", "Homev");
                    }
                }
            }
            var tuple2 = new Tuple<AdmissionFeesSubmit, List<AdmissionFeesSubmit>>(obj, feestruckture);
            return View(tuple2);
        }
        public ActionResult test()
        {
            int i = 1;
            try
            {

                int a = 0, b = 10;
                // int c = b / a;

                ViewBag.test = DateTime.Now.ToString("MdHHmmyyssfff") + " " + DateTime.Now.ToString("MdHHmmyyssfff") + "<br>  " + DateTime.Now.ToString("MdMdHHmmyyMyy") + "  " + DateTime.Now.ToString("MdMdHHmmyyMyy");

            }
            catch (Exception ex)
            {

                CommonMethod.WritetoNotepad(ex, "test", "error", i.ToString());
                i++;
            }

            return View();
        }
        public PartialViewResult _AdmissionCollegeFee()
        {
            AdmissionFeesSubmit stlogin = new AdmissionFeesSubmit();
            AdmissionFeesSubmit obj = new AdmissionFeesSubmit();
            List<AdmissionFeesSubmit> feestruckture = new List<AdmissionFeesSubmit>();
            ViewBag.IsSubmit = obj.IsFeeSubmit;
            AcademicSession ac = new AcademicSession();
            Commn_master com = new Commn_master();
            ViewBag.check_admissionopen = com.check_admission_open(ac.GetAcademiccurrentSession().ID, Convert.ToInt32(CommonSetting.Commonid.EducationtypeVoc));
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
            ViewBag.IsAdmisApplied = res1.IsApplied;
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
            string enID = EncriptDecript.Decrypt(Id);
            int eID = 0;
            eID = Convert.ToInt32(enID);
            obj = stlogin.FeesDetails(eID);
            if (obj != null)
            {
                feestruckture = stlogin.FeesDetailsstructure(obj.Collegeid, obj1.CourseCategory, sessionid, obj1.CastCategory, obj.streamcategoryid, 0, 0,0);
            }
            var tuple = new Tuple<AdmissionFeesSubmit, List<AdmissionFeesSubmit>>(obj, feestruckture);
            return PartialView("_AdmissionCollegeFee", tuple);
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

                CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "UG Student Generate ID Card method", eID);
                return RedirectToAction("GenerateIDCard");
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
            ViewBag.check_admissionopen = false; //com.check_admission_open(ac.GetAcademiccurrentSession().ID, Convert.ToInt32(CommonSetting.Commonid.EducationtypeVoc));
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
            ViewBag.check_admissionopen = true;
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

    }
}