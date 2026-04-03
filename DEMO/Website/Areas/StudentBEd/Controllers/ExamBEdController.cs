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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Website.Areas.StudentBEd.Models;
using Website.Areas.Student.Models;
using Website.Models;
using Newtonsoft.Json;
using System.IO;
using System.Text;

namespace Website.Areas.StudentBEd.Controllers
{
    [CookiesExpireFilterBEd]
    public class ExamBEdController : Controller
    {
        // GET: Student/Exam
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ExamFeesSubmit()
        {
           // return RedirectToAction("Index", "HomeB");
            ExamForm ob = new ExamForm();
            AcademicSession ad = new AcademicSession();
            BL_PrintApplication PritApp = new BL_PrintApplication();
            var obj1 = ob.GetAppLicationDataForExamFee(Convert.ToInt32(EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENNBStID"))));
            Commn_master com = new Commn_master();
           
            Commn_master datecom = new Commn_master();
            datecom = com.check_ExamFeeSubmit_check(ad.GetAcademiccurrentSession().ID, Convert.ToInt32(CommonSetting.Commonid.EducationtypeBEd));
            ViewBag.check_ExamFeeSubmit_open = datecom.isopendate;
            ViewBag.check_ExamFeeSubmit_Close = datecom.isclosedate;
            ViewBag.check_admissionopen = ViewBag.check_ExamFeeSubmit_open;
            //if (ViewBag.check_ExamFeeSubmit_open == true && ViewBag.check_ExamFeeSubmit_Close == true)
            //{

            //}
            //else
            //{
            //    //return RedirectToAction("Index", "HomeB");
            //}
            if (obj1.objExamFrom == null)
            {
                return RedirectToAction("Index", "HomeB");
            }
            if (obj1.objExamFrom.isfeesubmitregistration == 0)
            {
                return RedirectToAction("Index", "HomeB");
            }
            //if (obj1.objExamFrom.courseyearid != 29)
            //{
            //    return RedirectToAction("Index", "HomeB");
            //}
            if (obj1.Examobjfeesubmit.sessionid != 40)
            {
                // return RedirectToAction("Index", "HomeB");
            }
            StudentAdmissionQualification obj = new StudentAdmissionQualification();
            QualifiationMasterList sub = new QualifiationMasterList();
           // sub = obj.QualificationdetailList(1, 100000);
            ViewBag.previousqualification = sub;
            List<EaxmFeesSubmit> feestruckture = new List<EaxmFeesSubmit>();
            List<EaxmFeesSubmit> subjectlist = new List<EaxmFeesSubmit>();
            List<EaxmFeesSubmit> Electivesubjectlist = new List<EaxmFeesSubmit>();
            List<EaxmFeesSubmit> Electivesubjectlist_c7b = new List<EaxmFeesSubmit>();
            List<EaxmFeesSubmit> Electivesubjectlist_c7a = new List<EaxmFeesSubmit>();
            feestruckture = ob.FeesDetailsstructure(obj1.objExamFrom.collegeid, obj1.objExamFrom.coursecategoryid, obj1.objExamFrom.sessionid, obj1.objExamFrom.castecategoryid, obj1.objExamFrom.StreamCategoryID, obj1.objExamFrom.courseyearid, 0);
            subjectlist = ob.FeesDetailSubjectlist(obj1.objExamFrom.coursecategoryid, obj1.objExamFrom.StreamCategoryID, obj1.objExamFrom.courseyearid);
            Electivesubjectlist= ob.ElectiveFeesDetailSubjectlist_bed_C11(obj1.objExamFrom.coursecategoryid, obj1.objExamFrom.StreamCategoryID, obj1.objExamFrom.courseyearid, Convert.ToInt32(CommonMethod.SubSubjectType.Electivesubject));
            Electivesubjectlist_c7b = ob.ElectiveFeesDetailSubjectlist_bed_c7b(obj1.objExamFrom.coursecategoryid, obj1.objExamFrom.StreamCategoryID, obj1.objExamFrom.courseyearid, Convert.ToInt32(CommonMethod.SubSubjectType.Electivesubject));
            Electivesubjectlist_c7a = ob.ElectiveFeesDetailSubjectlist_bed_c7a(obj1.objExamFrom.coursecategoryid, obj1.objExamFrom.StreamCategoryID, obj1.objExamFrom.courseyearid, Convert.ToInt32(CommonMethod.SubSubjectType.Electivesubject));
            ViewBag.Electivesubjectlist = Electivesubjectlist;
            ViewBag.Electivesubjectlist_c7b = Electivesubjectlist_c7b;
            ViewBag.Electivesubjectlist_c7a = Electivesubjectlist_c7a;

            var tuple = new Tuple<PrintExamForm, QualifiationMasterList, List<EaxmFeesSubmit>, List<EaxmFeesSubmit>>(obj1, sub, feestruckture, subjectlist);
            return View(tuple);
        }

        [HttpPost]
        public ActionResult ExamFeesSubmit(int id = 0, string applyadmissionform = "", string isappeared = "", string BackandEdit = "", string checkboxid = "", string Substreamcategoryid = "0", string Substreamcategoryid2 = "0")
        {
            StudentLogin stu = new StudentLogin();
            string ApplicationID = ClsLanguage.GetCookies("NBApplicationNo");
            Login lo = stu.BasicDetail(ApplicationID);
            ExamForm objexamfrom = new ExamForm();
            Login result = new Login();
            ExamForm ob = new ExamForm();
            AcademicSession ad = new AcademicSession();
            BL_PrintApplication PritApp = new BL_PrintApplication();
            var obj1 = ob.GetAppLicationDataForExamFee(Convert.ToInt32(EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENNBStID"))));

            Commn_master com = new Commn_master();
            Commn_master datecom = new Commn_master();
            datecom = com.check_ExamFeeSubmit_check(ad.GetAcademiccurrentSession().ID, Convert.ToInt32(CommonSetting.Commonid.EducationtypeBEd));
            ViewBag.check_ExamFeeSubmit_open = datecom.isopendate;
            ViewBag.check_ExamFeeSubmit_Close = datecom.isclosedate;
            ViewBag.check_admissionopen = ViewBag.check_ExamFeeSubmit_open;
            if (ViewBag.check_ExamFeeSubmit_open == true && ViewBag.check_ExamFeeSubmit_Close == true)
            {

            }
            else
            {
                //return RedirectToAction("Index", "HomeB");
                TempData["msgfees"] = "Date Closed!!!";
                return RedirectToAction("ExamFeesSubmit");
            }
            if (obj1.objExamFrom == null)
            {
                //return RedirectToAction("Index", "HomeB");
                TempData["msgfees"] = "Registration no does not esist!!!";
                return RedirectToAction("ExamFeesSubmit");
            }
            if (obj1.objExamFrom.sessionid == 39)
            {
                //if (obj1.objExamFrom.courseyearid == 29)
                //{
                //    TempData["msgfees"] = "Date Closed !!";
                //    return RedirectToAction("ExamFeesSubmit");

                //}
            }
          
            if (obj1.objExamFrom.RollNo == null || obj1.objExamFrom.RollNo == "")
            {
                TempData["msgfees"] = "Roll no does not esist!!!";
                return RedirectToAction("ExamFeesSubmit");
               
            }
            if (obj1.Examobjfeesubmit.sessionid != 40)
            {
                // return RedirectToAction("Index", "HomeB");
            }
            if (applyadmissionform == "applyadmissionform")
            {
                //if (isappeared == "")
                //{
                //    return RedirectToAction("ExamFeesSubmit");
                //}
                if (obj1.objExamFrom.courseyearid == 29)// only for second year student 
                {
                    if (Substreamcategoryid == "")
                    {
                        TempData["msgfees"] = "Please Select Optional Subject!!!";
                        return RedirectToAction("ExamFeesSubmit");
                    }
                    if (Substreamcategoryid2 == "")
                    {
                        TempData["msgfees"] = "Please C-7 (b) Subject!!!";
                        return RedirectToAction("ExamFeesSubmit");
                    }
                }
                if (obj1.objExamFrom.courseyearid == 28)// only for first year student 
                {
                    if (Substreamcategoryid == "")
                    {
                        TempData["msgfees"] = "Please Select C-7 (a) Subject!!!";
                        return RedirectToAction("ExamFeesSubmit");
                    }
                    
                }
                if (checkboxid == "")
                {
                    return RedirectToAction("ExamFeesSubmit");
                }
                objexamfrom.student_examform_apply(obj1.objExamFrom.sid, obj1.objExamFrom.sessionid, obj1.objExamFrom.coursecategoryid, obj1.objExamFrom.collegeid, obj1.objExamFrom.StreamCategoryID, obj1.objExamFrom.courseyearid, 0,Convert.ToInt32(Substreamcategoryid),"Bed","", Convert.ToInt32(Substreamcategoryid2));
                return RedirectToAction("ExamFeesSubmit");
            }
            if (obj1.objExamFrom.IsDocVerify == 1)
            {

            }
            else
            {
                return RedirectToAction("ExamFeesSubmit");
            }
            if (obj1.objExamFrom.isfeesubmitregistration == 0)
            {
                //  return RedirectToAction("Index", "HomeB");
                TempData["msgfees"] = "Application no does not paid!!!";
                return RedirectToAction("ExamFeesSubmit");
            }
            if (obj1.objExamFrom.RegistrationNo == null || obj1.objExamFrom.RegistrationNo == "")
            {
                TempData["msgfees"] = "Registration no does not esist!!!";
                return RedirectToAction("ExamFeesSubmit");
            }
            StudentAdmissionQualification obj = new StudentAdmissionQualification();
            QualifiationMasterList sub = new QualifiationMasterList();
            //sub = obj.QualificationdetailList(1, 100000);
            //ViewBag.previousqualification = sub;
            List<EaxmFeesSubmit> feestruckture = new List<EaxmFeesSubmit>();
            List<EaxmFeesSubmit> subjectlist = new List<EaxmFeesSubmit>();
            List<EaxmFeesSubmit> Electivesubjectlist = new List<EaxmFeesSubmit>();
            List<EaxmFeesSubmit> Electivesubjectlist_c7b = new List<EaxmFeesSubmit>();
            feestruckture = ob.FeesDetailsstructure(obj1.objExamFrom.collegeid, obj1.objExamFrom.coursecategoryid, obj1.objExamFrom.sessionid, obj1.objExamFrom.castecategoryid, obj1.objExamFrom.StreamCategoryID, obj1.objExamFrom.courseyearid, 0);
            subjectlist = ob.FeesDetailSubjectlist(obj1.objExamFrom.coursecategoryid, obj1.objExamFrom.StreamCategoryID, obj1.objExamFrom.courseyearid);
            Electivesubjectlist = ob.ElectiveFeesDetailSubjectlist_bed_C11(obj1.objExamFrom.coursecategoryid, obj1.objExamFrom.StreamCategoryID, obj1.objExamFrom.courseyearid, Convert.ToInt32(CommonMethod.SubSubjectType.Electivesubject));
            Electivesubjectlist_c7b = ob.ElectiveFeesDetailSubjectlist_bed_c7b(obj1.objExamFrom.coursecategoryid, obj1.objExamFrom.StreamCategoryID, obj1.objExamFrom.courseyearid, Convert.ToInt32(CommonMethod.SubSubjectType.Electivesubject));

            ViewBag.Electivesubjectlist = Electivesubjectlist;
            ViewBag.Electivesubjectlist_c7b = Electivesubjectlist_c7b;

            if (obj1.objExamFrom.IsExamfeesubmit == 0)
            {
                // TempData["msgfees"] = "Please wait till exam fee open date !!!";
                //return RedirectToAction("ExamFeesSubmit");
                return RedirectToAction("PGGatewayExam");
            }
            else
            {
                FeesSubmit stlogin1 = new FeesSubmit();
                stlogin1.Status = false;
                TempData["msgfees"] = "Exam Fees Already Submitted !!!";
                return RedirectToAction("ExamFeesSubmit");
            }
            var tuple = new Tuple<PrintExamForm, QualifiationMasterList, List<EaxmFeesSubmit>, List<EaxmFeesSubmit>>(obj1, sub, feestruckture, subjectlist);
            return View(tuple);
        }
    
        
        
        public ActionResult PGGatewayExam()
        {
            //  return RedirectToAction("Index", "HomeB");
            try
            {
                SbiPayExam sbi = new SbiPayExam();
                ExamForm ob = new ExamForm();
                AcademicSession ad = new AcademicSession();
                Commn_master com = new Commn_master();
                List<EaxmFeesSubmit> feestruckture = new List<EaxmFeesSubmit>();
                decimal Amount1 = 1;
                decimal latefee = 0;
                decimal amount_without_latefee = 0;
                string MerchantCustomerID1 = "1";
                var obj1 = ob.GetAppLicationDataForExamFee(Convert.ToInt32(EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENNBStID"))));
                feestruckture = ob.FeesDetailsstructure(obj1.objExamFrom.collegeid, obj1.objExamFrom.coursecategoryid, obj1.objExamFrom.sessionid, obj1.objExamFrom.castecategoryid, obj1.objExamFrom.StreamCategoryID, obj1.objExamFrom.courseyearid, 0);
                if (obj1.objExamFrom != null)
                {
                    AcademicSession ac = new AcademicSession();
                    int sessionid = ac.GetAcademiccurrentSession().ID;
                    int educationtype = Convert.ToInt32(CommonSetting.Commonid.EducationtypeBEd);
                    // var datestart = com.check_ExamFeeSubmit_open(ad.GetAcademiccurrentSession().ID, Convert.ToInt32(CommonSetting.Commonid.EducationtypeBEd));

                    // var dateextend = com.check_ExamFeeSubmit_close(ad.GetAcademiccurrentSession().ID, Convert.ToInt32(CommonSetting.Commonid.EducationtypeBEd));
                    Commn_master datecom = new Commn_master();
                    datecom = com.check_ExamFeeSubmit_check(ad.GetAcademiccurrentSession().ID, Convert.ToInt32(CommonSetting.Commonid.EducationtypeBEd));
                    if (datecom.isopendate == false)
                    {
                        return RedirectToAction("ExamFeesSubmit");
                    }
                    if (datecom.isclosedate == false)
                    {
                        return RedirectToAction("ExamFeesSubmit");
                    }
                    if (obj1.objExamFrom.IsDocVerify == 1)
                    {

                    }
                    else
                    {
                        return RedirectToAction("ExamFeesSubmit");
                    }
                    if (obj1.objExamFrom.IsExamfeesubmit == 1) {
                        return RedirectToAction("ExamFeesSubmit");
                    }
                    Amount1 = Convert.ToDecimal(feestruckture.Sum(x => x.amount));
                    latefee = Convert.ToDecimal(feestruckture.Sum(x => x.late_fee));
                    amount_without_latefee = Amount1;
                    Amount1 = Amount1 + latefee;
                    if (Amount1 <= 0)
                    {
                        TempData["msgerror"] = "Amount Should be greater than 0 !!";
                        return RedirectToAction("ExamFeesSubmit", "ExamBEd");
                    }
                    //Amount1 = 1;
                    var obj = sbi.encriptDataExam(Amount1, EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENBApplicationNo")), "studentBEd/ExamBEd/PGSucessExam", "studentBEd/ExamBEd/PGFailedExam", obj1.objExamFrom.YearName, latefee, amount_without_latefee, obj1.objExamFrom.courseyearid);
                    ViewBag.requestparams = obj.requestparams;
                    ViewBag.merchantId = obj.merchantId;
                    ViewBag.EncryptbillingDetails = obj.EncryptbillingDetails;
                    ViewBag.EncryptshippingDetais = obj.EncryptshippingDetais;
                    ViewBag.url = obj.url;
                }
            }
            catch (Exception ex)
            {
                CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "action PGGateway error for Eaxm Fee", ClsLanguage.GetCookies("NBApplicationNo"));

                return View();
            }
            return View();
        }
        public ActionResult ResponseExam()
        {
            // return RedirectToAction("Index", "HomeB");

            try
            {
                ExamForm ob = new ExamForm();
                AcademicSession ad = new AcademicSession();
                // List<EaxmFeesSubmit> subjectlist = new List<EaxmFeesSubmit>();

                var obj1 = ob.GetAppLicationDataForExamFee(Convert.ToInt32(EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENNBStID"))));
                return View(obj1);

            }
            catch (Exception ex)
            {
                CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "Response payment gateway get method for exam Fee on Controller error", ClsLanguage.GetCookies("NBApplicationNo"));
                PrintExamForm PritApp = new PrintExamForm();
                return View(PritApp);
            }
        }
        public ActionResult PGSucessExam()
        {
            //return RedirectToAction("Index", "HomeB");
            string paramInfo = "";


            if (Request.Form.Count > 0)
            {
                try
                {
                    SbiPayExam sbi = new SbiPayExam();
                    var result = sbi.pgsucessdecrypt(Convert.ToInt32(EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENNBStID"))));
                    return RedirectToAction("ResponseExam");
                }
                catch (Exception ex)
                {
                    CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "Payment PaymentGateway suucess url hit on Controller error for Exam Fees Submit", ClsLanguage.GetCookies("NBApplicationNo"));

                }
            }
            return RedirectToAction("ExamFeesSubmit");
            return View();
        }

        public ActionResult PGFailedExam()
        {
            //  return RedirectToAction("Index", "HomeB");
            string paramInfo = "";

            if (Request.Form.Count > 0)
            {
                try
                {
                    SbiPayExam sbi = new SbiPayExam();
                    var result = sbi.pgfaileddecrypt(Convert.ToInt32(EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENNBStID"))));
                    return RedirectToAction("ResponseExam");
                }
                catch (Exception ex)
                {
                    CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "Payment PaymentGateway suucess url hit on Controller error for Exam Fees Submit", ClsLanguage.GetCookies("NBApplicationNo"));

                }
            }
            return RedirectToAction("ExamFeesSubmit");
            return View();

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
        public ActionResult PrintApplication()
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
        public ActionResult ExaminationAdmitCard()
        {
            //  return RedirectToAction("Index", "HomeB");
            ExamForm ob = new ExamForm();
            Commn_master com = new Commn_master();
            AcademicSession ad = new AcademicSession();
            var result = ob.StudentDetailForAdmitCard();
            return View(result);
        }
        public ActionResult MigrationCertificate()
        {
            return RedirectToAction("Index", "HomeB");
            ExamForm ob = new ExamForm();
            Commn_master com = new Commn_master();
            AcademicSession ad = new AcademicSession();
            var result = ob.StudentDetailForAdmitCard();
            return View(result);
        }
        public ActionResult EnrollmentFeesSubmit()
        {
            //return RedirectToAction("Index", "HomeB");
            AcademicSession ad = new AcademicSession();
            ExamForm ob = new ExamForm();
            Commn_master com = new Commn_master();
            Commn_master datecom = new Commn_master();
            datecom = com.check_EnrollmentSubmit_check(ad.GetAcademiccurrentSession().ID, Convert.ToInt32(CommonSetting.Commonid.EducationtypeBEd));
            ViewBag.check_EnrollmentFeeSubmit_open = datecom.isopendate;
            ViewBag.check_EnrollmentFeeSubmit_Close = datecom.isclosedate;
            ViewBag.enrollStartdateValue = datecom.opendate;
            ViewBag.ViewBagenrollExtenddateValue = datecom.closedate;
            //if (ViewBag.check_EnrollmentFeeSubmit_open == true && ViewBag.check_EnrollmentFeeSubmit_Close == true)
            //{


            //}
            //else
            //{
            //    return RedirectToAction("Index", "HomeB");
            //}
            var obj1 = ob.GetAppLicationDataForEnrollmentFee();

            if (obj1.objExamFrom == null)
            {

                return RedirectToAction("Index", "HomeB");
            }
            if (obj1.objExamFrom.IsAdmissionFee == false)
            {

                return RedirectToAction("Index", "HomeB");
            }
            //if (obj1.objfeesubmit.sessionid != 41)
            //{

            //    return RedirectToAction("Index", "HomeB");
            //}
            ViewBag.boardtype = CommonMethod.BoradtypePrevious().Where(x => x.boardid == 1 || x.boardid == 2);
            StudentAdmissionQualification obj = new StudentAdmissionQualification();
            QualifiationMasterList sub = new QualifiationMasterList();
            //sub = obj.QualificationdetailList(1, 1000);
            //List<StudentAdmissionQualification> asa = new List<StudentAdmissionQualification>();
            //asa = sub.qlist.Where(x => x.QualicationType != 1).ToList();
            //var boardtype = asa.FirstOrDefault().boardtype;

            return View(obj1);
        }
        [HttpPost]
        public ActionResult EnrollmentFeesSubmit(int id = 0, string saveandnext = "", int boardtype = 0, string BackandEdit = "")
        {
            StudentAdmissionQualification obj = new StudentAdmissionQualification();
            QualifiationMasterList sub = new QualifiationMasterList();
            ExamForm ob = new ExamForm();
            AcademicSession ad = new AcademicSession();
            BL_PrintApplication PritApp = new BL_PrintApplication();
            var obj1 = ob.GetAppLicationDataForEnrollmentFee();
            Commn_master com = new Commn_master();
            Commn_master datecom = new Commn_master();
            datecom = com.check_EnrollmentSubmit_check(ad.GetAcademiccurrentSession().ID, Convert.ToInt32(CommonSetting.Commonid.EducationtypeBEd));
            ViewBag.check_EnrollmentFeeSubmit_open = datecom.isopendate;
            ViewBag.check_EnrollmentFeeSubmit_Close = datecom.isclosedate;
            ViewBag.enrollStartdateValue = datecom.opendate;
            ViewBag.ViewBagenrollExtenddateValue = datecom.closedate;
            if (ViewBag.check_EnrollmentFeeSubmit_open == true && ViewBag.check_EnrollmentFeeSubmit_Close == true)
            {


            }
            else
            {
                ob.Status = false;
                TempData["msgfees"] = "Dated Closed  !!!";
                return RedirectToAction("EnrollmentFeesSubmit");
            }
            if (obj1.objExamFrom.IsAdmissionFee == false)
            {
                ob.Status = false;
                TempData["msgfees"] = "You cannot apply for registration fee as your admission fee is not submitted yet  !!!";
                return RedirectToAction("EnrollmentFeesSubmit");
            }
            //if (obj1.objExamFrom.migrationcertificate_iseligible != 1)
            //{
            //    ob.Status = false;
            //    TempData["msgfees"] = "Your migration document not verify !! !!!";
            //    return RedirectToAction("EnrollmentFeesSubmit");
            //}

            if (obj1.objfeesubmit.IsRegistrationFee == false)
            {
                ob.Status = false;
                //stlogin.Feessub();
                //stlogin.FeessubStudenttest();
                //TempData["msgfees"] = "Fees Submitted Successfully !!!";
                //return RedirectToAction("FeesSubmit");

                return RedirectToAction("PGGatewayEnrollment");
            }
            else
            {
                ob.Status = false;
                TempData["msgfees"] = "Fees Already Submitted !!!";
                return RedirectToAction("EnrollmentFeesSubmit");
            }
            return View(obj1);
        }
        public JsonResult ADDmigrationcertificate(string id = "")
        {
            StudentAdmissionQualification doc = new StudentAdmissionQualification();
            doc.ID = Convert.ToInt32(Request.Form["ID"] == "" ? "0" : Request.Form["ID"]);
            DataLayer.Login objl = new DataLayer.Login();
            StudentLogin objs = new StudentLogin();
            doc.ApplicationNo = (EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENBApplicationNo")));
            doc.SID = Convert.ToInt32(EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENNBStID")));
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
                            string s3FileName = System.DateTime.Now.ToString("dd_MM_yyyy_HHmmss") + "_migrationcertificate_" + objl.FirstName + @name;

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
                    if (doc.FileURl == "")
                    {
                        StudentAdmissionQualification ob1 = new StudentAdmissionQualification();
                        ob1.Msg = "Error Please select File: ";
                        return Json(ob1, JsonRequestBehavior.AllowGet);

                    }
                    var result = doc.Addmigrationdocument(doc.SID, doc.FileURl, Convert.ToInt32((ClsLanguage.GetCookies("NBSission"))));
                    if (result.Status == true)
                    {
                        result.Msg = "migraion Document Upload Successfully,Please reach college and verify document !!";
                    }
                    else
                    {
                        result.Msg = "Failed to upload document !!";
                    }
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
                catch (Exception ex)
                {

                    CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "migraion : document Image Upload", "DocumentType1" + jsonstring);
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
        public ActionResult PGGatewayEnrollment()
        {
            //return RedirectToAction("Index", "HomeB");
            try
            {
                SbiPayEnrollment sbi = new SbiPayEnrollment();
                FeesSubmit stlogin = new FeesSubmit();
                ExamForm ob = new ExamForm();
                AcademicSession ad = new AcademicSession();
                Commn_master com = new Commn_master();
                decimal Amount1 = 1;
                string MerchantCustomerID1 = "1";
                Commn_master datecom = new Commn_master();
                datecom = com.check_EnrollmentSubmit_check(ad.GetAcademiccurrentSession().ID, Convert.ToInt32(CommonSetting.Commonid.EducationtypeBEd));
                ViewBag.check_EnrollmentFeeSubmit_open = datecom.isopendate;
                ViewBag.check_EnrollmentFeeSubmit_Close = datecom.isclosedate;
                if (ViewBag.check_EnrollmentFeeSubmit_open == true && ViewBag.check_EnrollmentFeeSubmit_Close == true)
                {
                }
                else
                {
                    return RedirectToAction("EnrollmentFeesSubmit");
                }

                var result = stlogin.FeessubEncrytEnrollment(Convert.ToInt32(EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENNBStID"))));
                 if (result.migrationcertificate_iseligible == 1)
                {

                }
                else
                {
                    return RedirectToAction("EnrollmentFeesSubmit");
                }
                if (result != null)
                {
                    Amount1 = Convert.ToDecimal(result.Fees);
                    // Amount1 = 1;
                    //if (result.migrationcertificate_iseligible != 1)
                    //{
                    //    return RedirectToAction("EnrollmentFeesSubmit");
                    //}
                    if (Amount1 <= 0)
                    {
                        return RedirectToAction("EnrollmentFeesSubmit");
                    }
                    var obj = sbi.encriptData(Amount1, EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENBApplicationNo")), "studentBED/ExamBED/PGSucessEnrollment", "studentBED/ExamBED/PGFailedEnrollment");

                    ViewBag.requestparams = obj.requestparams;
                    ViewBag.merchantId = obj.merchantId;
                    ViewBag.EncryptbillingDetails = obj.EncryptbillingDetails;
                    ViewBag.EncryptshippingDetais = obj.EncryptshippingDetais;
                    ViewBag.url = obj.url;
                }
            }
            catch (Exception ex)
            {
                CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "action PGGateway error for Enrollment Fee", ClsLanguage.GetCookies("NBApplicationNo"));

                return View();
            }
            return View();
        }
        public ActionResult ResponseEnrollment()
        {
           // return RedirectToAction("Index", "HomeB");

            try
            {
                ExamForm ob = new ExamForm();
                AcademicSession ad = new AcademicSession();
                var obj1 = ob.GetAppLicationDataForEnrollmentFee();
                return View(obj1);

            }
            catch (Exception ex)
            {
                CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "Response payment gateway get method for Enrollment Fee on Controller error", ClsLanguage.GetCookies("NBApplicationNo"));
                PrintExamForm PritApp = new PrintExamForm();
                return View(PritApp);
            }
        }
        public ActionResult PGSucessEnrollment()
        {
            //return RedirectToAction("Index", "HomeB");
            string paramInfo = "";
            if (Request.Form.Count > 0)
            {
                try
                {
                    SbiPayEnrollment sbi = new SbiPayEnrollment();
                    var result = sbi.pgsucessdecrypt(Convert.ToInt32(EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENNBStID"))));
                    return RedirectToAction("ResponseEnrollment");
                }
                catch (Exception ex)
                {
                    CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "Payment PaymentGateway suucess url hit on Controller error for Enrollment Fees Submit", ClsLanguage.GetCookies("NBApplicationNo"));

                }
            }
            return RedirectToAction("EnrollmentFeesSubmit");
            return View();
        }

        public ActionResult PGFailedEnrollment()
        {
            //return RedirectToAction("Index", "HomeB");
            string paramInfo = "";
            if (Request.Form.Count > 0)
            {
                try
                {
                    SbiPayEnrollment sbi = new SbiPayEnrollment();
                    var result = sbi.pgfaileddecrypt(Convert.ToInt32(EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENNBStID"))));
                    return RedirectToAction("ResponseEnrollment");
                }
                catch (Exception ex)
                {
                    CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "Payment PaymentGateway suucess url hit on Controller error for Enrollment Fees Submit", ClsLanguage.GetCookies("NBApplicationNo"));

                }
            }
            return RedirectToAction("EnrollmentFeesSubmit");
            return View();

        }
        [HttpPost, ValidateInput(false)]
        public ActionResult PrintEnrollmentFeeReceipt()
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
        public ActionResult PrintDownloadenrollmentslipReceipt()
        {
            string ApplicationID = ClsLanguage.GetCookies("NBApplicationNo");
            StudentLogin tblST = new StudentLogin();
            var obj1 = tblST.BasicDetail(ApplicationID);
            Recruitment rec = new Recruitment();
            RecruitmentList reclist = new RecruitmentList();
            reclist = rec.view1customfeesubmittedstudentdetailList(obj1.Id);
            rec = reclist.qlist.ToList().FirstOrDefault();
            if (rec == null)
            {
                return RedirectToAction("EnrollmentFeesSubmit");
            }
            if (rec.enrollmentno == null || rec.enrollmentno == "")
            {
                return RedirectToAction("EnrollmentFeesSubmit");
            }
            StringBuilder builder = new StringBuilder();
            string content = "";
            content += @"

<div class=""container-fluid"" style=""display:block"">
        <div class=""sparkline8-list DocumentUpload"">
            <div style=""display:block"">
                <div id=""divtoPrint"">

                      <div style=""width:640px; margin:auto; padding:0; font-size:14px; color:#333; font-family:Arial, Helvetica, sans-serif; border:5px solid #333; padding:20px ;background-image:url(https://portal.DemoUniversity.com/images/registrationslip_bg.jpg); background-repeat:no-repeat; background-size:cover;"">
                        <table width=""100%"" border=""0"" cellspacing=""0"" cellpadding=""0"">
                            <tr>
                                <td colspan=""3"" align=""center"" valign=""middle""><img id=""logo11"" src=""" + System.Configuration.ConfigurationManager.AppSettings["siteUrl"] + "/images/logotree.png" + @""" width=""200""></td>
                            </tr>
                                 <tr>
                                <td colspan=""3"" align='center' valign='middle' style='font-size:25px;'>Munger University,Bhagalpur ,Bihar</td>
                            </tr>
                            <tr>
                                <td colspan=""3"" align='center' valign='middle' style='font-size:12px;'>(Administrative BlockMunger University,Bhagalpur ,Bihar</td>
                            </tr>
                            <tr>
                                <td colspan=""3"" align=""center"" valign=""middle"">&nbsp;</td>
                            </tr>
                            <tr>
                                <td colspan=""3"" align=""center"" valign=""middle"" style=""font-size:16px; text-transform:uppercase; border-bottom:1px solid #000; padding-bottom:13px""><strong>Registration Slip</strong></td>
                            </tr>
                            <tr>
                                <td colspan=""3"" align=""center"" valign=""middle"">&nbsp;</td>
                            </tr>
                            <tr>
                                <td align=""left"" colspan=""2"" valign=""middle"">Registration No: " + rec.enrollmentno + @" </td>
                                <td width=""182"" align=""left"" valign=""middle"">Session :" + rec.Session + @" </td>
                            </tr>
                            <tr>
                                <td colspan=""3"" align=""center"" valign=""middle"">&nbsp;</td>
                            </tr>
                            <tr>
                                <td align=""left"" colspan=""2"" valign=""middle"">College Roll No : " + rec.RollNo + @" </td>
                                <td width=""182"" align=""left"" valign=""middle"">Course : " + rec.coursecategotyName + @" </td>
                            </tr>
 <tr>
                                <td colspan=""3"" align=""center"" valign=""middle"">&nbsp;</td>
                            </tr>
<tr>
                                <td align=""left"" colspan=""3"" valign=""middle"">
                                    College Name:  <strong>" + rec.CollegeName + @" 
                                  </strong>
                                </td>
                            </tr>
                            <tr>
                                <td colspan=""3"" align=""center"" valign=""middle"">&nbsp;</td>
                            </tr>


                            <tr>
                                <td colspan=""3"" align=""center"" valign=""middle"">&nbsp;</td>
                            </tr>
                            <tr>
                                <td width=""175"" align=""left"" valign=""top"">Name :</td>
                                <td width=""283"" align=""left"" valign=""top"">" + rec.StudentName + @" </td>
                                <td rowspan=""19"" align=""left"" valign=""top"" style=""padding:0 5px; width:150px;"">
                                    <table width=""100%"" border=""0"" cellspacing=""0"" cellpadding=""0"">
                                        <tr>
                                            <td align=""center"" valign=""middle"" style=""border:1px solid #333; height:150px;""><img id=""userImage1""  src=""" + System.Configuration.ConfigurationManager.AppSettings["AmozonbucketPath"] + "/Student/Photoandsign/" + rec.stphoto + @""" width=""160"" style=""""></td>
                                        </tr>
                                        <tr>
                                            <td align=""center"" valign=""middle"">&nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td align=""center"" valign=""middle"" style=""border:1px solid #333; height:50px;""><img id=""userSign1"" width=""160"" src=""" + System.Configuration.ConfigurationManager.AppSettings["AmozonbucketPath"] + "/Student/Photoandsign/" + rec.stsign + @""" style=""max-height:50px""></td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr valign=""top"">
                                <td colspan=""2"" align=""left"">&nbsp;</td>
                            </tr>
                            <tr valign=""top"">
                                <td align=""left"">" + (obj1.Ftitle == 20 ? "Father's Name" : (obj1.Ftitle == 21 ? "  Husband's  Name" : " Father's Name")) + @": </td>
                                <td align=""left"">" + rec.FatherName + @" </td>
                            </tr>
                            <tr valign=""top"">
                                <td colspan=""2"" align=""left"">&nbsp;</td>
                            </tr>
                            <tr valign=""top"">
                                <td align=""left"">Mother's Name : </td>
                                <td align=""left"">" + rec.mothername + @" </td>
                            </tr>
                            <tr valign=""top"">
                                <td colspan=""2"" align=""left"">&nbsp;</td>
                            </tr>
                            <tr valign=""top"">
                                <td align=""left"">Date of Birth :</td>
                                <td align=""left""> " + rec.DOB + @" </td>
                            </tr>
                            <tr valign=""top"">
                                <td colspan=""2"" align=""left"">&nbsp;</td>
                            </tr>
                            <tr valign=""top"">
                                <td align=""left"">Gender :</td>
                                <td align=""left"">" + rec.Gender + @" </td>
                            </tr>
                            <tr valign=""top"">
                                <td colspan=""2"" align=""left"">&nbsp;</td>
                            </tr>
                            <tr valign=""top"">
                                <td align=""left"">Category :</td>
                                <td align=""left"">" + rec.StudentCasteCategoryName + @" </td>
                            </tr>
                            <tr valign=""top"">
                                <td colspan=""2"" align=""left"">&nbsp;</td>
                            </tr>
                            <tr valign=""top"">
                                <td align=""left"">Nationality  :</td>
                                <td align=""left"">" + rec.Nationlity + @" </td>
                            </tr>

                            <tr>
                                <td colspan=""2"" align=""left"" valign=""middle"">&nbsp;</td>
                            </tr>
                            <tr>
                                <td colspan=""3"" align=""left"" valign=""middle"">&nbsp;</td>
                            </tr>
                            <tr>
                                <td colspan=""3"" align=""left"" valign=""middle"">&nbsp;</td>
                            </tr>
                            <tr>
                                <td colspan=""3"" align=""left"" valign=""middle"">&nbsp;</td>
                            </tr>

                            <tr>
                                <td colspan=""3"" align=""left"" valign=""middle"">&nbsp;</td>
                            </tr>

                            <tr>
                                <td colspan=""3"" align=""left"" valign=""middle"">&nbsp;</td>
                            </tr>
                            <tr>
                                <td colspan=""3"" align=""right"" valign=""middle""><img id=""registratrara11"" src=""" + System.Configuration.ConfigurationManager.AppSettings["siteUrl"] + "/images/registrarsign.jpg" + @""" style=""max-height:50px""></td>
                            </tr>
                            <tr>
                                <td colspan=""3"" align=""left"" valign=""middle"">&nbsp;</td>
                            </tr>
                            <tr>
                                <td colspan=""3"" align=""left"" valign=""middle"">
                                    <table width=""100%"" border=""0"" cellspacing=""0"" cellpadding=""0"">
                                        <tr>
                                            <td align=""right"" valign=""top"" width=""25%"" style=""padding:0 25px 0 0px;"">Registrar</td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td colspan=""3"" align=""left"" valign=""middle"">&nbsp;</td>
                            </tr>
                        </table>
                    </div>



                </div>



            </div>

        </div>
    </div>
    
                     ";
            builder.Append(content);
            // DownloadAdmitcard(builder.ToString());
            string InpuData = "<html><head><title></title><meta http-equiv='Content-Type' content='text/html;charset=utf-8' /></head><body><div style=''><center></center></div><br />" + builder.ToString() + "</body></html>";
            //Div Content will be fetched from form data.                                                    
            string PageType = "Portrait";  //here we will recieve Page Type sent from front end.
            string dataname = "RegistrationNoSlip";  // here we declare for filename for download
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
            pdfDoc.Margins = pageMargins;                      //margins added to PDF.                                                 //Why I am adding this to page footer?? So we can get paging in footer section of each PDF page.how its working?? A Javascript code is written inside the DLL which is handling  Div's class i.e. page and topage.
            var pdfBytes = pdfDoc.GeneratePdf(htmlContent);
            HttpContext.Response.ContentType = "application/pdf";
            HttpContext.Response.AddHeader("content-disposition", "attachment;filename=" + dataname.Replace(" ", "") + System.DateTime.Now.ToString() + ".pdf");//Use inline in place of attachment If You wish to open PDF on Browser.
            HttpContext.Response.Cache.SetCacheability(HttpCacheability.NoCache);
            HttpContext.Response.BinaryWrite(pdfBytes);
            HttpContext.Response.Flush();
            HttpContext.Response.End();
            HttpContext.ApplicationInstance.CompleteRequest();
            return View();
        }
        public ActionResult BackExamFeesSubmit(string courseyearidenc="")
        {
            ExamForm ob = new ExamForm();
            AcademicSession ad = new AcademicSession();
            BL_PrintApplication PritApp = new BL_PrintApplication();
            //// manaul change  course year value from  fetch tbl_CourseYear according to
            int courseyearid = Convert.ToInt32(EncriptDecript.Decrypt(courseyearidenc));// couryearid 28 for first year; , kon se semester ke lia open krna h , manaul set semesterid
            Commn_master com = new Commn_master();
            

            Commn_master datecom = new Commn_master();
            datecom = com.check_BAckExamFeeSubmit_check(ad.GetAcademiccurrentSession().ID, Convert.ToInt32(CommonSetting.Commonid.EducationtypeBEd));
            ViewBag.check_ExamFeeSubmit_open = datecom.isopendate;
            ViewBag.check_ExamFeeSubmit_Close = datecom.isclosedate;
            ViewBag.check_admissionopen = ViewBag.check_ExamFeeSubmit_open;


            var obj1 = ob.backGetAppLicationDataForExamFee_BEd(Convert.ToInt32(EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENNBStID"))), courseyearid, System.DateTime.Now.Year);

            if (obj1.objExamFrom == null)
            {
                return RedirectToAction("Index", "HomeB");
            }
            if (obj1.objExamFrom.isfeesubmitregistration == 0)
            {
                return RedirectToAction("Index", "HomeB");
            }
            if (obj1.objExamFrom.RegistrationNo == null || obj1.objExamFrom.RegistrationNo == "")
            {
                return RedirectToAction("Index", "HomeB");
            }
            if (obj1.objExamFrom.RollNo == null || obj1.objExamFrom.RollNo == "")
            {
                return RedirectToAction("Index", "HomeB");
            }

            // only for session student check

            //if (obj1.objExamFrom.sessionid != 39)
            //{
            //    return RedirectToAction("Index", "HomeB");
            //}
            //if (obj1.objExamFrom.Currentyear_courseyarid == 2 || obj1.objExamFrom.Currentyear_courseyarid == 5 || obj1.objExamFrom.Currentyear_courseyarid == 8)
            //{
            //    // allow only for 2nd year student which have back paper
            //    // first check exam fee payment before admission fee submit or not 
            //    //int a = ob.check_examfeebefore_admissionfee(obj1.objExamFrom.coursecategoryid, obj1.objExamFrom.StreamCategoryID, obj1.objExamFrom.Currentyear_courseyarid, obj1.objExamFrom.sessionid, obj1.objExamFrom.collegeid, obj1.objExamFrom.sid);
            //    //if (a <= 0)
            //    //{
            //    //    return RedirectToAction("Index", "HomeB");
            //    //}
            //}
            //else
            //{ return RedirectToAction("Index", "HomeB"); }
            StudentAdmissionQualification obj = new StudentAdmissionQualification();
            QualifiationMasterList sub = new QualifiationMasterList();
            List<EaxmFeesSubmit> feestruckture = new List<EaxmFeesSubmit>();
            List<EaxmFeesSubmit> subjectlist = new List<EaxmFeesSubmit>();
            List<EaxmFeesSubmit> Electivesubjectlist = new List<EaxmFeesSubmit>();
            feestruckture = ob.backFeesDetailsstructure(obj1.objExamFrom.collegeid, obj1.objExamFrom.coursecategoryid, obj1.objExamFrom.sessionid, obj1.objExamFrom.castecategoryid, obj1.objExamFrom.StreamCategoryID, obj1.objExamFrom.courseyearid, 0);
            subjectlist = ob.backFeesDetailSubjectlist_Bed(obj1.objExamFrom.coursecategoryid, obj1.objExamFrom.StreamCategoryID, courseyearid, obj1.objExamFrom.sessionid, obj1.objExamFrom.collegeid, obj1.objExamFrom.sid);
            if (subjectlist.Count == 0)
            {
                // if subject count greater than 0 then is counted as back student else it not consider as back student
                return RedirectToAction("Index", "HomeB");
            }
            var tuple = new Tuple<PrintExamForm, QualifiationMasterList, List<EaxmFeesSubmit>, List<EaxmFeesSubmit>>(obj1, sub, feestruckture, subjectlist);
            return View(tuple);
        }
        [HttpPost]
        public ActionResult BackExamFeesSubmit(int id = 0, string applyadmissionform = "", string Substreamcategoryid = "", string BackandEdit = "", string checkboxid = "", string isappeared = "0",string courseyearidenc = "")
        {
            StudentLogin stu = new StudentLogin();
            string ApplicationID = ClsLanguage.GetCookies("NBApplicationNo");
            Login lo = stu.BasicDetail(ApplicationID);
            ExamForm objexamfrom = new ExamForm();
            Login result = new Login();
            ExamForm ob = new ExamForm();
            AcademicSession ad = new AcademicSession();
            BL_PrintApplication PritApp = new BL_PrintApplication();
            // manaul change  course year value from  fetch tbl_CourseYear according to couryearid 28 for 1st year
            int courseyearid = Convert.ToInt32(EncriptDecript.Decrypt(courseyearidenc)); //28;//// couryearid 28 for first year; , kon se semester ke lia open krna h , manaul set semesterid

            var obj1 = ob.backGetAppLicationDataForExamFee_BEd(Convert.ToInt32(EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENNBStID"))), courseyearid, System.DateTime.Now.Year);
            Commn_master com = new Commn_master();
            Commn_master datecom = new Commn_master();
            datecom = com.check_BAckExamFeeSubmit_check(ad.GetAcademiccurrentSession().ID, Convert.ToInt32(CommonSetting.Commonid.EducationtypeBEd));
            ViewBag.check_ExamFeeSubmit_open = datecom.isopendate;
            ViewBag.check_ExamFeeSubmit_Close = datecom.isclosedate;
            ViewBag.check_admissionopen = ViewBag.check_ExamFeeSubmit_open;

            if (ViewBag.check_ExamFeeSubmit_open == true && ViewBag.check_ExamFeeSubmit_Close == true)
            {

            }
            else
            {
                return RedirectToAction("Index", "HomeB");
            }
            if (obj1.objExamFrom == null)
            {
                return RedirectToAction("Index", "HomeB");
            }
            if (obj1.objExamFrom.isfeesubmitregistration == 0)
            {
                return RedirectToAction("Index", "HomeB");
            }

            // only for session student check

            //if (obj1.objExamFrom.sessionid != 39)
            //{
            //    return RedirectToAction("Index", "HomeB");
            //}
            if (applyadmissionform == "applyadmissionform")
            {
                //if (Substreamcategoryid == "")
                //{
                //    return RedirectToAction("ExamFeesSubmit");
                //}
                if (checkboxid == "")
                {
                    return RedirectToAction("BackExamFeesSubmit", new { @courseyearidenc = courseyearidenc });// return RedirectToAction("BackExamFeesSubmit");
                }
                objexamfrom.student_examform_apply_back(obj1.objExamFrom.sid, obj1.objExamFrom.sessionid, obj1.objExamFrom.coursecategoryid, obj1.objExamFrom.collegeid, obj1.objExamFrom.StreamCategoryID, obj1.objExamFrom.courseyearid, Convert.ToInt32(isappeared), Convert.ToInt32(0), "BEd", "", System.DateTime.Now.Year);
                return RedirectToAction("BackExamFeesSubmit", new { @courseyearidenc = courseyearidenc }); //return RedirectToAction("BackExamFeesSubmit");
            }
            if (obj1.objExamFrom.IsDocVerify == 1)
            {

            }
            else
            {
                return RedirectToAction("BackExamFeesSubmit", new { @courseyearidenc = courseyearidenc });// return RedirectToAction("BackExamFeesSubmit");
            }

            StudentAdmissionQualification obj = new StudentAdmissionQualification();
            QualifiationMasterList sub = new QualifiationMasterList();
            List<EaxmFeesSubmit> feestruckture = new List<EaxmFeesSubmit>();
            List<EaxmFeesSubmit> subjectlist = new List<EaxmFeesSubmit>();
            List<EaxmFeesSubmit> Electivesubjectlist = new List<EaxmFeesSubmit>();
            feestruckture = ob.backFeesDetailsstructure(obj1.objExamFrom.collegeid, obj1.objExamFrom.coursecategoryid, obj1.objExamFrom.sessionid, obj1.objExamFrom.castecategoryid, obj1.objExamFrom.StreamCategoryID, obj1.objExamFrom.courseyearid, 0);
            subjectlist = ob.backFeesDetailSubjectlist_Bed(obj1.objExamFrom.coursecategoryid, obj1.objExamFrom.StreamCategoryID, courseyearid, obj1.objExamFrom.sessionid, obj1.objExamFrom.collegeid, obj1.objExamFrom.sid);
            if (subjectlist.Count == 0)
            {
                return RedirectToAction("Index", "HomeB");
            }
            if (obj1.objExamFrom.IsExamfeesubmit == 0)
            {
                // TempData["msgfees"] = "Please wait till exam fee open date !!!";
                //return RedirectToAction("ExamFeesSubmit");
                return RedirectToAction("backPGGatewayExam",new { @courseyearidenc= courseyearidenc });
            }
            else
            {
                FeesSubmit stlogin1 = new FeesSubmit();
                stlogin1.Status = false;
                TempData["msgfees"] = "Exam Fees Already Submitted !!!";
                return RedirectToAction("BackExamFeesSubmit", new { @courseyearidenc = courseyearidenc });
            }
            var tuple = new Tuple<PrintExamForm, QualifiationMasterList, List<EaxmFeesSubmit>, List<EaxmFeesSubmit>>(obj1, sub, feestruckture, subjectlist);
            return View(tuple);
        }
        public ActionResult BackPGGatewayExam( string courseyearidenc = "")
        {
            //  return RedirectToAction("Index", "Home");
            try
            {
                SbiPayExam sbi = new SbiPayExam();
                ExamForm ob = new ExamForm();
                AcademicSession ad = new AcademicSession();
                Commn_master com = new Commn_master();
                List<EaxmFeesSubmit> feestruckture = new List<EaxmFeesSubmit>();
                List<EaxmFeesSubmit> subjectlist = new List<EaxmFeesSubmit>();
                decimal Amount1 = 1;
                decimal latefee = 0;
                decimal amount_without_latefee = 0;
                string MerchantCustomerID1 = "1";
                // manaul change  course year value from  fetch tbl_CourseYear according to couryearid
                int courseyearid = Convert.ToInt32(EncriptDecript.Decrypt(courseyearidenc));// 28;////  28 for first year; , kon se semester ke lia open krna h , manaul set semesterid

                var obj1 = ob.backGetAppLicationDataForExamFee_BEd(Convert.ToInt32(EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENNBStID"))), courseyearid, System.DateTime.Now.Year);
                feestruckture = ob.backFeesDetailsstructure(obj1.objExamFrom.collegeid, obj1.objExamFrom.coursecategoryid, obj1.objExamFrom.sessionid, obj1.objExamFrom.castecategoryid, obj1.objExamFrom.StreamCategoryID, obj1.objExamFrom.courseyearid, 0);
                // manaul change  course year value from  fetch tbl_CourseYear according to couryearid
                subjectlist = ob.backFeesDetailSubjectlist_Bed(obj1.objExamFrom.coursecategoryid, obj1.objExamFrom.StreamCategoryID, courseyearid, obj1.objExamFrom.sessionid, obj1.objExamFrom.collegeid, obj1.objExamFrom.sid);

                if (subjectlist.Count == 0)
                {
                    return RedirectToAction("Index", "HomeB");
                }
                if (obj1.objExamFrom != null)
                {
                    AcademicSession ac = new AcademicSession();
                    int sessionid = ac.GetAcademiccurrentSession().ID;
                    int educationtype = Convert.ToInt32(CommonSetting.Commonid.EducationtypeBEd);
                    //var datestart = com.Backcheck_ExamFeeSubmit_open(ad.GetAcademiccurrentSession().ID, Convert.ToInt32(CommonSetting.Commonid.EducationtypeBEd));
                    //var dateextend = com.Backcheck_ExamFeeSubmit_close(ad.GetAcademiccurrentSession().ID, Convert.ToInt32(CommonSetting.Commonid.EducationtypeBEd));
                    Commn_master datecom = new Commn_master();
                    datecom = com.check_BAckExamFeeSubmit_check(ad.GetAcademiccurrentSession().ID, Convert.ToInt32(CommonSetting.Commonid.EducationtypeBEd));
                    ViewBag.check_ExamFeeSubmit_open = datecom.isopendate;
                    ViewBag.check_ExamFeeSubmit_Close = datecom.isclosedate;
                    ViewBag.check_admissionopen = ViewBag.check_ExamFeeSubmit_open;
                    if (ViewBag.check_ExamFeeSubmit_open == false)
                    {
                        return RedirectToAction("BackExamFeesSubmit", new { @courseyearidenc = courseyearidenc });
                    }
                    if (ViewBag.check_ExamFeeSubmit_Close == false)
                    {
                        return RedirectToAction("BackExamFeesSubmit", new { @courseyearidenc = courseyearidenc });
                    }

                    if (obj1.objExamFrom.IsDocVerify == 1)
                    {

                    }
                    else
                    {
                        return RedirectToAction("Index", "HomeB");
                    }
                    if (obj1.objExamFrom.IsExamfeesubmit == 1) { return RedirectToAction("BackExamFeesSubmit", new { @courseyearidenc = courseyearidenc }); }
                    Amount1 = Convert.ToDecimal(feestruckture.Sum(x => x.amount));
                    latefee = Convert.ToDecimal(feestruckture.Sum(x => x.late_fee));
                    amount_without_latefee = Amount1;
                    Amount1 = Amount1 + latefee;
                    if (Amount1 <= 0)
                    {
                        TempData["msgerror"] = "Amount Should be greater than 0 !!";
                        return RedirectToAction("BackExamFeesSubmit", "ExamBed", new { @courseyearidenc = courseyearidenc });
                    }
                    //Amount1 = 1;
                    var obj = sbi.encriptDataExambackyear(Amount1, EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENBApplicationNo")), "studentBed/ExamBed/backPGSucessExam", "studentbed/ExamBed/backPGFailedExam", obj1.objExamFrom.YearName, latefee, amount_without_latefee, obj1.objExamFrom.courseyearid);
                    ViewBag.requestparams = obj.requestparams;
                    ViewBag.merchantId = obj.merchantId;
                    ViewBag.EncryptbillingDetails = obj.EncryptbillingDetails;
                    ViewBag.EncryptshippingDetais = obj.EncryptshippingDetais;
                    ViewBag.url = obj.url;
                }
            }
            catch (Exception ex)
            {
                CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "action PGGateway error for Eaxm Fee", ClsLanguage.GetCookies("NBApplicationNo"));

                return View();
            }
            return View();
        }
        public ActionResult BackResponseExam(string courseyearidenc = "")
        {
            // return RedirectToAction("Index", "Home");

            try
            {
                // manaul change  course year value from  fetch tbl_CourseYear according to couryearid
                //int courseyearid = 28;////  28 for first year; , kon se semester ke lia open krna h , manaul set semesterid
                int courseyearid = Convert.ToInt32(EncriptDecript.Decrypt(courseyearidenc));// 28;////  28 for first year; , kon se semester ke lia open krna h , manaul set semesterid

                ExamForm ob = new ExamForm();
                AcademicSession ad = new AcademicSession();
                // List<EaxmFeesSubmit> subjectlist = new List<EaxmFeesSubmit>();
                // manaul change  course year value from  fetch tbl_CourseYear according to couryearid

                var obj1 = ob.backGetAppLicationDataForExamFee_BEd(Convert.ToInt32(EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENNBStID"))), courseyearid, System.DateTime.Now.Year);
                return View(obj1);

            }
            catch (Exception ex)
            {
                CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "Response payment gateway get method for exam Fee on Controller error", ClsLanguage.GetCookies("NBApplicationNo"));
                PrintExamForm PritApp = new PrintExamForm();
                return View(PritApp);
            }
        }
        public ActionResult BackPGSucessExam()
        {
            //return RedirectToAction("Index", "Home");
            string paramInfo = "";


            if (Request.Form.Count > 0)
            {
                try
                {
                    SbiPayExam sbi = new SbiPayExam();
                    var result = sbi.pgsucessdecrypt(Convert.ToInt32(EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENNBStID"))));
                    return RedirectToAction("backResponseExam", new { @courseyearidenc = EncriptDecript.Encrypt(result) });
                }
                catch (Exception ex)
                {
                    CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "Payment PaymentGateway suucess url hit on Controller error for Exam Fees Submit", ClsLanguage.GetCookies("NBApplicationNo"));

                }
            }
            return RedirectToAction("BackExamFeesSubmit");
            return View();
        }
        public ActionResult backPGFailedExam()
        {
            //  return RedirectToAction("Index", "Home");
            string paramInfo = "";

            if (Request.Form.Count > 0)
            {
                try
                {
                    SbiPayExam sbi = new SbiPayExam();
                    var result = sbi.pgfaileddecrypt(Convert.ToInt32(EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENNBStID"))));
                    return RedirectToAction("backResponseExam", new { @courseyearidenc = EncriptDecript.Encrypt(result) });
                }
                catch (Exception ex)
                {
                    CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "Payment PaymentGateway suucess url hit on Controller error for Exam Fees Submit", ClsLanguage.GetCookies("NBApplicationNo"));

                }
            }
            return RedirectToAction("BackExamFeesSubmit", new { @courseyearidenc = "" });
            return View();





        }
    }
}