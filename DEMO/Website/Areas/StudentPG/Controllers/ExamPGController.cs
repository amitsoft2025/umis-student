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
using Website.Areas.StudentPG.Models;
using Website.Areas.Student.Models;
using Website.Models;
using System.IO;
using System.Text;
using System.Security.Cryptography;

namespace Website.Areas.StudentPG.Controllers
{
    //[CookiesExpireFilterPG]

    public class ExamPGController : Controller
    {
        // GET: Student/Exam
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ExamFeesSubmit()
        {
            //return RedirectToAction("Index", "HomePG");
            ExamForm ob = new ExamForm();
            AcademicSession ad = new AcademicSession();
            BL_PrintApplication PritApp = new BL_PrintApplication();
            var obj1 = ob.GetAppLicationDataForExamFee(Convert.ToInt32(EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENNBStID"))));
            Commn_master com = new Commn_master();
            Commn_master datecom = new Commn_master();
            datecom = com.check_ExamFeeSubmit_check(ad.GetAcademiccurrentSession().ID, Convert.ToInt32(CommonSetting.Commonid.EducationtypePG));
            ViewBag.check_ExamFeeSubmit_open = datecom.isopendate;
            ViewBag.check_ExamFeeSubmit_Close = datecom.isclosedate;
            ViewBag.check_admissionopen = ViewBag.check_ExamFeeSubmit_open;
            //if (ViewBag.check_ExamFeeSubmit_open == true && ViewBag.check_ExamFeeSubmit_Close == true)
            //{

            //}
            //else
            //{
            //    return RedirectToAction("Index", "HomePG");
            //}
            ViewBag.studentyear = obj1.objExamFrom.courseyearid;
            if (obj1.objExamFrom == null)
            {
                return RedirectToAction("Index", "HomePG");
            }
            if (obj1.objExamFrom.isfeesubmitregistration == 0)
            {
                return RedirectToAction("Index", "HomePG");
            }
            //if (obj1.objExamFrom.courseyearid == 17 || obj1.objExamFrom.courseyearid == 27 || obj1.objExamFrom.courseyearid == 25)
            //{
            //    // return RedirectToAction("Index", "HomePG");
            //}
            //else
            //{
            //    return RedirectToAction("Index", "HomePG");
            //}
            //if (obj1.objExamFrom.courseyearid == 17 || obj1.objExamFrom.courseyearid == 27 || obj1.objExamFrom.courseyearid == 25)
            //{
            //    // return RedirectToAction("Index", "HomePG");
            //}
            //else
            //{
            //    return RedirectToAction("Index", "HomePG");
            //}
            //if (obj1.objExamFrom.courseyearid != 31)
            //{
            //    return RedirectToAction("Index", "HomePG");
            //}
            //if (obj1.Examobjfeesubmit.sessionid == 40)
            //{
            //     return RedirectToAction("Index", "HomePG");
            //}
            StudentAdmissionQualification obj = new StudentAdmissionQualification();
            QualifiationMasterList sub = new QualifiationMasterList();
            //sub = obj.QualificationdetailList(1, 100000);
            //ViewBag.previousqualification = sub;
            List<EaxmFeesSubmit> feestruckture = new List<EaxmFeesSubmit>();
            List<EaxmFeesSubmit> subjectlist = new List<EaxmFeesSubmit>();
            List<EaxmFeesSubmit> Electivesubjectlist = new List<EaxmFeesSubmit>();
            feestruckture = ob.FeesDetailsstructure(obj1.objExamFrom.collegeid, obj1.objExamFrom.coursecategoryid, obj1.objExamFrom.sessionid, obj1.objExamFrom.castecategoryid, obj1.objExamFrom.StreamCategoryID, obj1.objExamFrom.courseyearid, 0);
            subjectlist = ob.FeesDetailSubjectlist(obj1.objExamFrom.coursecategoryid, obj1.objExamFrom.StreamCategoryID, obj1.objExamFrom.courseyearid);
            Electivesubjectlist = ob.ElectiveFeesDetailSubjectlist(obj1.objExamFrom.coursecategoryid, obj1.objExamFrom.StreamCategoryID, obj1.objExamFrom.courseyearid, Convert.ToInt32(CommonMethod.SubSubjectType.Electivesubject));
            ViewBag.Electivesubjectlist = Electivesubjectlist;
            var tuple = new Tuple<PrintExamForm, QualifiationMasterList, List<EaxmFeesSubmit>, List<EaxmFeesSubmit>>(obj1, sub, feestruckture, subjectlist);
            return View(tuple);
        }

        [HttpPost]
        public ActionResult ExamFeesSubmit(HttpPostedFileBase fileupload, int id = 0, string applyadmissionform = "", string Substreamcategoryid = "", string BackandEdit = "", string checkboxid = "")
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
            datecom = com.check_ExamFeeSubmit_check(ad.GetAcademiccurrentSession().ID, Convert.ToInt32(CommonSetting.Commonid.EducationtypePG));
            ViewBag.check_ExamFeeSubmit_open = datecom.isopendate;
            ViewBag.check_ExamFeeSubmit_Close = datecom.isclosedate;
            ViewBag.check_admissionopen = ViewBag.check_ExamFeeSubmit_open;
            if (ViewBag.check_ExamFeeSubmit_open == true && ViewBag.check_ExamFeeSubmit_Close == true)
            {

            }
            else
            {
                TempData["msgfees"] = "Date Closed!!!";
                return RedirectToAction("Index", "HomePG");
            }
            if (obj1.objExamFrom == null)
            {
                return RedirectToAction("Index", "HomePG");
            }
            if (obj1.objExamFrom.isfeesubmitregistration == 0)
            {
                return RedirectToAction("Index", "HomePG");
            }
            ViewBag.studentyear = obj1.objExamFrom.courseyearid;
            //if (obj1.objExamFrom.courseyearid == 17 || obj1.objExamFrom.courseyearid == 27 || obj1.objExamFrom.courseyearid == 25)
            //{
            //    // only for sem-2
            //    TempData["msgfees"] = "Date Closed!!!";
            //    return RedirectToAction("ExamFeesSubmit", "ExamPG");
            //}
            //else
            //{
            // only for sem-1
            // return RedirectToAction("Index", "HomePG");
            //}
            //if (obj1.objExamFrom.sessionid == 39)
            //{
            //    if (obj1.objExamFrom.courseyearid == 17 || obj1.objExamFrom.courseyearid == 27 || obj1.objExamFrom.courseyearid == 25)
            //    {
            //        TempData["msgfees"] = "Date Closed !!";
            //        return RedirectToAction("ExamFeesSubmit");

            //    }
            //}
            //if (obj1.Examobjfeesubmit.sessionid != 40)
            //{
            //    // return RedirectToAction("Index", "HomePG");
            //}
            if (obj1.objExamFrom.RollNo == null || obj1.objExamFrom.RollNo == "")
            {
                TempData["msgfees"] = "Roll no does not esist!!!";
                return RedirectToAction("ExamFeesSubmit");

            }
            if (applyadmissionform == "applyadmissionform")
            {
                if (obj1.objExamFrom.courseyearid == 41 || obj1.objExamFrom.courseyearid == 39 || obj1.objExamFrom.courseyearid == 37)
                {
                    Substreamcategoryid = "0";
                }
                else
                {
                    if (Substreamcategoryid == "")
                    {
                        return RedirectToAction("ExamFeesSubmit");
                    }
                }
                string fileuploadname = "";
                //if (fileupload != null)
                //{
                //    Stream st1 = fileupload.InputStream;
                //    string name = Path.GetFileName(fileupload.FileName);
                //    try
                //    {
                //        string myBucketName = "mungeruniversity"; //your s3 bucket name goes here  
                //        string s3DirectoryName = "Student/Document";
                //        string s3FileName = System.DateTime.Now.ToString("dd_MM_yyyy_HH:mm:ss") + "_Studentcastecertificate_" + obj1.objExamFrom.RegistrationNo + name;
                //        s3FileName = s3FileName.Replace(" ", "");
                //        fileuploadname = s3FileName;
                //        bool a;
                //        AmazonUploader myUploader = new AmazonUploader();
                //        a = myUploader.sendMyFileToFolder(photo,Server.MapPath("~/App_Data/uploads"),s3FileName);
                //    }
                //    catch (Exception ex)
                //    {
                //        CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "exam form in file upload post Method", ClsLanguage.GetCookies("NBApplicationNo") + "   " );
                //    }
                //}
                if (checkboxid == "")
                {
                    return RedirectToAction("ExamFeesSubmit");
                }
                objexamfrom.student_examform_apply(obj1.objExamFrom.sid, obj1.objExamFrom.sessionid, obj1.objExamFrom.coursecategoryid, obj1.objExamFrom.collegeid, obj1.objExamFrom.StreamCategoryID, obj1.objExamFrom.courseyearid, Convert.ToInt32(0), Convert.ToInt32(Substreamcategoryid), "PG", fileuploadname);
                //return RedirectToAction("ExamFeesSubmit");
            }
            //if (obj1.objExamFrom.IsDocVerify == 1)
            //{

            //}
            //else
            //{
            //    return RedirectToAction("ExamFeesSubmit");
            //}

            StudentAdmissionQualification obj = new StudentAdmissionQualification();
            QualifiationMasterList sub = new QualifiationMasterList();
            //sub = obj.QualificationdetailList(1, 100000);
            //ViewBag.previousqualification = sub;
            List<EaxmFeesSubmit> feestruckture = new List<EaxmFeesSubmit>();
            List<EaxmFeesSubmit> subjectlist = new List<EaxmFeesSubmit>();
            List<EaxmFeesSubmit> Electivesubjectlist = new List<EaxmFeesSubmit>();
            feestruckture = ob.FeesDetailsstructure(obj1.objExamFrom.collegeid, obj1.objExamFrom.coursecategoryid, obj1.objExamFrom.sessionid, obj1.objExamFrom.castecategoryid, obj1.objExamFrom.StreamCategoryID, obj1.objExamFrom.courseyearid, 0);
            subjectlist = ob.FeesDetailSubjectlist(obj1.objExamFrom.coursecategoryid, obj1.objExamFrom.StreamCategoryID, obj1.objExamFrom.courseyearid);
            Electivesubjectlist = ob.ElectiveFeesDetailSubjectlist(obj1.objExamFrom.coursecategoryid, obj1.objExamFrom.StreamCategoryID, obj1.objExamFrom.courseyearid, Convert.ToInt32(CommonMethod.SubSubjectType.Electivesubject));
            ViewBag.Electivesubjectlist = Electivesubjectlist;

            if (obj1.objExamFrom.IsExamfeesubmit == 0)
            {
                if (obj1.objExamFrom.courseyearid == 27 || obj1.objExamFrom.courseyearid == 39 || obj1.objExamFrom.courseyearid == 25 || obj1.objExamFrom.courseyearid == 37 || obj1.objExamFrom.courseyearid == 17 || obj1.objExamFrom.courseyearid == 41)
                {
                    return RedirectToAction("SelectPaymentGetway");
                }
                else
                {
                    TempData["msgfees"] = "Only Sem 3rd and sem 4th exam Form fill";

                }
                // TempData["msgfees"] = "Please wait till exam fee open date !!!";
                //return RedirectToAction("ExamFeesSubmit");
                //return RedirectToAction("PGGatewayExam");
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

        public ActionResult SelectPaymentGetway()
        {
            return View();
        }

        public ActionResult SelectPaymentGetwayBack()
        {
            return View();
        }


        public ActionResult AirPayPGGatewayExam()
        {


            //if (ClsLanguage.GetCookies("NBApplicationNo") == "MU21587151")
            //{
            //}
            //else
            //{
            //    return RedirectToAction("Index", "Home");
            //}

            //  return RedirectToAction("Index", "Home");
            try

            {
                StudentLogin stu = new StudentLogin();
                string ApplicationID = ClsLanguage.GetCookies("NBApplicationNo");
                Login lo = stu.BasicDetail(ApplicationID);
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
                    int educationtype = Convert.ToInt32(CommonSetting.Commonid.Educationtype);
                    //var datestart = com.check_ExamFeeSubmit_open(ad.GetAcademiccurrentSession().ID, Convert.ToInt32(CommonSetting.Commonid.Educationtype));
                    //var dateextend = com.check_ExamFeeSubmit_close(ad.GetAcademiccurrentSession().ID, Convert.ToInt32(CommonSetting.Commonid.Educationtype));
                    Commn_master datecom = new Commn_master();
                    datecom = com.check_ExamFeeSubmit_check(ad.GetAcademiccurrentSession().ID, Convert.ToInt32(CommonSetting.Commonid.EducationtypePG));
                    if (datecom.isopendate == false)
                    {
                        return RedirectToAction("ExamFeesSubmit", "ExamPG");
                    }
                    if (datecom.isclosedate == false)
                    {
                        return RedirectToAction("ExamFeesSubmit", "ExamPG");
                    }
                    if (obj1.objExamFrom.IsDocVerify == 1)
                    {
                    }
                    else
                    { return RedirectToAction("Index", "Home"); }
                    if (obj1.objExamFrom.IsExamfeesubmit == 1) { return RedirectToAction("ExamFeesSubmit"); }
                    Amount1 = Convert.ToDecimal(feestruckture.Sum(x => x.amount));
                    latefee = Convert.ToDecimal(feestruckture.Sum(x => x.late_fee));
                    //Amount1 = 50; //only for testing purpose Amount Set
                    amount_without_latefee = Amount1;
                    Amount1 = Amount1 + latefee;

                    // Dommey amount
                    if (Amount1 <= 0)
                    {
                        TempData["msgerror"] = "Amount Should be greater than 0 !!";
                        return RedirectToAction("ExamFeesSubmit", "ExamPG");
                    }
                    string sEmail = lo.Email;
                    string sPhone = lo.MobileNo;
                    string sFName = lo.FirstName;
                    string sLName = lo.LastName;
                    string sAddress = lo.CurrentAddress;
                    string sCity = lo.CA_City.ToString();
                    string sState = lo.CA_State.ToString();
                    string sCountry = lo.CA_Country.ToString();
                    string sPincode = lo.CA_PinCode;
                    string sAmount = Amount1.ToString("0.00");
                    string sOrderId = "";
                    string Backyear = "MainExam";
                    EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENBApplicationNo"));//other Amount  me bhejna h
                    //Amount1 = 1;
                    var obj = sbi.encriptDataExamAirPay(Amount1, EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENBApplicationNo")), "studentPG/ExamPG/AirPayPGSucessExam", "student/Exam/AirPayPGFailedExam", obj1.objExamFrom.YearName, latefee, amount_without_latefee, obj1.objExamFrom.courseyearid, sEmail, sPhone, sFName, sLName, sAddress, sCity, sState, sCountry, sPincode, sAmount, sOrderId );

                    ViewBag.orderid = obj.Oid;
                    ViewBag.buyerEmail = sEmail;
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
                    ViewBag.url = obj.url;
                    ViewBag.success_url = obj.A_Success_url;
                    ViewBag.customvar = obj.customvar;

                }
            }
            catch (Exception ex)
            {
                CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "action PGGateway error for Eaxm Fee", ClsLanguage.GetCookies("NBApplicationNo"));

                return View();
            }
            return View();
        }


        public ActionResult AirPayPGSucessExam()
        {
            CommonMethod.TraceLogWritetoNotepad("obj1.objExamFrom4", "Exam/AirPayPGSucessExam1", "obj1.objExamFrom", "Id");
            //return RedirectToAction("Index", "HomePG");
            string paramInfo = "";
            if (Request.Form.Count > 0)
            {
                try
                {
                    CommonMethod.TraceLogWritetoNotepad("obj1.objExamFrom4", "Exam/AirPayPGSucessExam2", "obj1.objExamFrom", "Id");
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
                    CommonMethod.TraceLogWritetoNotepad("obj1.objExamFrom4", "Exam/AirPayPGSucessExam3", "obj1.objExamFrom", "Id");
                    if (TRANSACTIONSTATUS == "" || APTRANSACTIONID == "" || TRANSACTIONID == "" || AMOUNT == "" || ap_SecureHash == "")
                    {
                        if (TRANSACTIONID == "") { error = "TRANSACTIONID"; }
                        if (APTRANSACTIONID == "") { error = "APTRANSACTIONID"; }
                        if (AMOUNT == "") { error = "AMOUNT"; }
                        if (TRANSACTIONSTATUS == "") { error = "TRANSACTIONSTATUS"; }

                        if (ap_SecureHash == "") { error = "ap_SecureHash"; }
                    }
                    CommonMethod.TraceLogWritetoNotepad("obj1.objExamFrom4", "Exam/AirPayPGSucessExam4", "obj1.objExamFrom", "Id");
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
                    CommonMethod.TraceLogWritetoNotepad("obj1.objExamFrom4", "Exam/AirPayPGSucessExam5", "obj1.objExamFrom", "Id");
                    //Other_Details = Other_Details1 + ",MainYear," + courseyearid + ",Exampaper-" + year + "," + Order_Number;
                    var other_detail = CUSTOMVAR.Split(',');
                    ApplicationNo = other_detail[0];
                    examType = other_detail[1];
                    courseyearid = other_detail[2];
                    clienttrxid = other_detail[4];
                    Sid = other_detail[5];

                    Sessionid = other_detail[6];
                    CommonMethod.TraceLogWritetoNotepad("obj1.objExamFrom4", "Exam/AirPayPGSucessExam6", "obj1.objExamFrom", "Id");

                    if (error == "")
                    {
                        if (TRANSACTIONSTATUS == "200")
                        {
                            CommonMethod.TraceLogWritetoNotepad("obj1.objExamFrom4", "Exam/AirPayPGSucessExam7", "obj1.objExamFrom", "Id");
                            //PGstatus = "SUCCESS";
                            SbiPayExam sbi = new SbiPayExam();
                            //var result = sbi.pgsucessdecrypt(Convert.ToInt32(EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENNBStID"))));
                            var result = sbi.AirPaypgsucessdecrypt(Convert.ToInt32(Sid), banktrxid, clienttrxid, amount1, feeamount, gst, commission, paymode, banktxndate, Reason, apitxnid, ApplicationNo, courseyearid, examType, Requestdata, dRequestdata, PGstatus);

                            //var result = sbi.AirPaypgsucessdecrypt(Sid, banktrxid, clienttrxid, amount1, feeamount, gst, commission, paymode, banktxndate, Reason, apitxnid, ApplicationNo, courseyearid, Requestdata, dRequestdata, PGstatus);
                            CommonMethod.TraceLogWritetoNotepad("obj1.objExamFrom4", "Exam/AirPayPGSucessExam8", "obj1.objExamFrom", "Id");
                            return RedirectToAction("ResponseExam");
                            //Literal1.Text = "<form target='_parent' name='frmresponse' name='frmresponse' action='thankyou.aspx' method='POST'> <table width='100%'><tr width='100%'><td align='left' width='50%'>Transaction Id</td><td align='left' width='50%' style='color:black;'>" + TRANSACTIONID + "</td></tr><tr width='100%'><td align='left' width='50%'>Airpay Transaction Id</td><td align='left' width='50%' style='color:black;'>" + APTRANSACTIONID + "</td></tr><tr width='100%'><td align='left' width='50%'>Amount</td><td align='left' width='50%' style='color:black;'>" + AMOUNT + "</td></tr><tr width='100%'><td align='left' width='50%'>Transaction Status Code</td><td align='left' width='50%' style='color:black;'>" + TRANSACTIONSTATUS + "</td></tr><tr width='100%'><td align='left' width='50%'>Message</td><td align='left' width='50%' style='color:black;'>" + MESSAGE + "</td></tr><tr width='100%'><td align='left' width='50%'>Status</td><td align='left' width='50%' style='color:green;'>Success</td></tr></table></form>";
                        }
                        else
                        {
                            CommonMethod.TraceLogWritetoNotepad("obj1.objExamFrom4", "Exam//AirPayPGSucessExam9", "obj1.objExamFrom", "Id");
                            //PGstatus = "FAIL";
                            SbiPayExam sbi = new SbiPayExam();
                            //var result = sbi.pgfaileddecrypt(Convert.ToInt32(EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENNBStID"))));
                            var result = sbi.AirPaypgsucessdecrypt(Convert.ToInt32(Sid), banktrxid, clienttrxid, amount1, feeamount, gst, commission, paymode, banktxndate, Reason, apitxnid, ApplicationNo, courseyearid, examType, Requestdata, dRequestdata, PGstatus);
                            //var result = sbi.AirPaypgsucessdecrypt(Sid, banktrxid, clienttrxid, amount1, feeamount, gst, commission, paymode, banktxndate, Reason, apitxnid, ApplicationNo, courseyearid, Requestdata, dRequestdata, PGstatus);
                            CommonMethod.TraceLogWritetoNotepad("obj1.objExamFrom4", "Exam//AirPayPGSucessExam10", "obj1.objExamFrom", "Id");
                            //return RedirectToAction("AirPayResponseExam", new { id = Sid, session= Sessionid });
                            return RedirectToAction("ResponseExam");

                            //Literal1.Text = "<form target='_parent' name='frmresponse' name='frmresponse' action='thankyou.aspx' method='POST'> <table width='100%'><tr width='100%'><td align='left' width='50%'>Transaction Id</td><td align='left' width='50%' style='color:black;'>" + TRANSACTIONID + "</td></tr><tr width='100%'><td align='left' width='50%'>Airpay Transaction Id</td><td align='left' width='50%' style='color:black;'>" + APTRANSACTIONID + "</td></tr><tr width='100%'><td align='left' width='50%'>Amount</td><td align='left' width='50%' style='color:black;'>" + AMOUNT + "</td></tr><tr width='100%'><td align='left' width='50%'>Transaction Status Code</td><td align='left' width='50%' style='color:black;'>" + TRANSACTIONSTATUS + "</td></tr><tr width='100%'><td align='left' width='50%'>Message</td><td align='left' width='50%' style='color:black;'>" + MESSAGE + "</td></tr><tr width='100%'><td align='left' width='50%'>Status</td><td align='left' width='50%' style='color:green;'>Failed</td></tr></table></form>";
                        }
                    }
                    else
                    {
                        PGstatus = "ERROR";
                        CommonMethod.TraceLogWritetoNotepad("obj1.objExamFrom4", "Exam//AirPayPGSucessExam11", "obj1.objExamFrom", "Id");
                        SbiPayExam sbi = new SbiPayExam();
                        //var result = sbi.pgfaileddecrypt(Convert.ToInt32(EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENNBStID"))));
                        var result = sbi.AirPaypgsucessdecrypt(Convert.ToInt32(Sid), banktrxid, clienttrxid, amount1, feeamount, gst, commission, paymode, banktxndate, Reason, apitxnid, ApplicationNo, courseyearid, examType, Requestdata, dRequestdata, PGstatus);
                        CommonMethod.TraceLogWritetoNotepad("obj1.objExamFrom4", "Exam//AirPayPGSucessExam12", "obj1.objExamFrom", "Id");
                        return RedirectToAction("AirPayResponseExam");

                        //Literal1.Text = "<table width='100%'><tr><td align='center'>Variable(s) " + error + " is/are empty.</td></tr></table>";
                    }

                }
                catch (Exception ex)
                {
                    CommonMethod.TraceLogWritetoNotepad("obj1.objExamFrom4", "Exam//AirPayPGSucessExam13", "obj1.objExamFrom", "Id");
                    CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, " Air Pay Payment PaymentGateway suucess url hit on Controller error for Exam Fees Submit", ClsLanguage.GetCookies("NBApplicationNo"));

                }
            }
            return RedirectToAction("ExamFeesSubmit");
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
                Response.Write("Secure Hash mismatch.");
                // return true.ToString();
                // Environment.Exit(0);
            }

            return hash;

        }


        public JsonResult ADDcastecertificate(string id = "")
        {
            StudentAdmissionQualification ob5551 = new StudentAdmissionQualification();
            ob5551.Msg = "Error occurred. Error details: ";
            return Json(ob5551, JsonRequestBehavior.AllowGet);
            StudentAdmissionQualification doc = new StudentAdmissionQualification();
            doc.ID = Convert.ToInt32(Request.Form["ID"] == "" ? "0" : Request.Form["ID"]);
            DataLayer.Login objl = new DataLayer.Login();
            StudentLogin objs = new StudentLogin();
            doc.ApplicationNo = ClsLanguage.GetCookies("NBApplicationNo");
            doc.SID = Convert.ToInt32(ClsLanguage.GetCookies("NBStID"));

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
                            string s3FileName = System.DateTime.Now.ToString("dd_MM_yyyy_HH_mm_ss") + "_Studentcastecertificate_" + objl.FirstName + @name;

                            s3FileName = s3FileName.Replace(" ", "");
                            doc.FileURl = s3FileName;
                            bool a;
                            AmazonUploader myUploader = new AmazonUploader();
                            a = myUploader.sendMyFileToFolder(fileUpload, Server.MapPath("~/App_Data/uploads"),s3FileName);
                        }
                        catch (Exception ex)
                        {
                            CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "exam form in file upload edit document Image Upload", "DocumentType1");
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
                    ExamForm objexamfrom = new ExamForm();
                    var result = objexamfrom.student_examform_editfileupload(doc.SID, doc.ID, doc.FileURl);

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

                    CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "Employee : document Image Upload", "DocumentType1");
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

        public ActionResult PGGatewayExam()
        {
            //  return RedirectToAction("Index", "HomePG");
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
                    int educationtype = Convert.ToInt32(CommonSetting.Commonid.EducationtypePG);
                    Commn_master datecom = new Commn_master();
                    datecom = com.check_ExamFeeSubmit_check(ad.GetAcademiccurrentSession().ID, Convert.ToInt32(CommonSetting.Commonid.EducationtypePG));
                    if (datecom.isopendate == false)
                    {
                        return RedirectToAction("ExamFeesSubmit");
                    }
                    if (datecom.isclosedate == false)
                    {
                        return RedirectToAction("ExamFeesSubmit");
                    }
                    //if (obj1.objExamFrom.IsDocVerify == 1)
                    //{

                    //}
                    //else
                    //{
                    //    return RedirectToAction("ExamFeesSubmit"); 
                    //}
                    if (obj1.objExamFrom.IsExamfeesubmit == 1) { return RedirectToAction("ExamFeesSubmit"); }
                    Amount1 = Convert.ToDecimal(feestruckture.Sum(x => x.amount));
                    latefee = Convert.ToDecimal(feestruckture.Sum(x => x.late_fee));
                    amount_without_latefee = Amount1;
                    Amount1 = Amount1 + latefee;
                    if (Amount1 <= 0)
                    {
                        TempData["msgfees"] = "Amount Should be greater than 0 !!";
                        return RedirectToAction("ExamFeesSubmit");
                    }
                    //Amount1 = 1;
                    var obj = sbi.encriptDataExam(Amount1, EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENBApplicationNo")), "studentPG/ExamPG/PGSucessExam", "studentPG/ExamPG/PGFailedExam", obj1.objExamFrom.YearName, latefee, amount_without_latefee, obj1.objExamFrom.courseyearid);
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
            // return RedirectToAction("Index", "HomePG");

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
            //return RedirectToAction("Index", "HomePG");
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
            //  return RedirectToAction("Index", "HomePG");
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

            ExamForm ob = new ExamForm();
            Commn_master com = new Commn_master();
            AcademicSession ad = new AcademicSession();
            var result = ob.StudentDetailForAdmitCard();
            List<EaxmFeesSubmit> subjectlist = new List<EaxmFeesSubmit>();
            if (result == null)
            {
                return RedirectToAction("Index", "HomePG");
            }
            subjectlist = ob.FeesDetailSubjectlist(result.coursecategoryid, result.StreamCategoryID, 16);
            ViewBag.subjectlist = subjectlist;

            if (result.IsExamfeesubmit != 1)
            {
                return RedirectToAction("Index", "HomePG");
            }
            return View(result);
        }
        public ActionResult MigrationCertificate()
        {
            return RedirectToAction("Index", "HomePG");
            ExamForm ob = new ExamForm();
            Commn_master com = new Commn_master();
            AcademicSession ad = new AcademicSession();
            var result = ob.StudentDetailForAdmitCard();
            return View(result);
        }
        public ActionResult EnrollmentFeesSubmit()
        {
            // return RedirectToAction("Index", "HomePG");
            AcademicSession ad = new AcademicSession();
            ExamForm ob = new ExamForm();
            var obj1 = ob.GetAppLicationDataForEnrollmentFee();
            Commn_master com = new Commn_master();
            Commn_master datecom = new Commn_master();
            datecom = com.check_EnrollmentSubmit_check(ad.GetAcademiccurrentSession().ID, Convert.ToInt32(CommonSetting.Commonid.EducationtypePG));
            ViewBag.check_EnrollmentFeeSubmit_open = datecom.isopendate;
            ViewBag.check_EnrollmentFeeSubmit_Close = datecom.isclosedate;
            ViewBag.enrollStartdateValue = datecom.opendate;
            ViewBag.ViewBagenrollExtenddateValue = datecom.closedate;


            if (obj1.objExamFrom == null)
            {
                return RedirectToAction("Index", "HomePG");
            }
            if (obj1.objExamFrom.IsAdmissionFee == false)
            {

                return RedirectToAction("Index", "HomePG");
            }
            //if (obj1.objfeesubmit.sessionid != 42)
            //{

            //    return RedirectToAction("Index", "HomePG");
            //}
            ViewBag.boardtype = CommonMethod.BoradtypePrevious().Where(x => x.boardid == 1 || x.boardid == 2);
            StudentAdmissionQualification obj = new StudentAdmissionQualification();
            QualifiationMasterList sub = new QualifiationMasterList();
            //sub = obj.QualificationdetailList(1, 1000);
            List<StudentAdmissionQualification> asa = new List<StudentAdmissionQualification>();
            //asa = sub.qlist.Where(x => x.QualicationType != 1).ToList();
            // var boardtype = asa.FirstOrDefault().boardtype;
            ViewBag.boardtypename = "";
            //if (boardtype == 0)
            //{
            //    ViewBag.showpayment = false;
            //}
            //else
            //{
            //    ViewBag.boardtypename = CommonMethod.BoradtypePrevious().Where(x => x.boardid == boardtype).FirstOrDefault().boardname;
            //    ViewBag.showpayment = true;
            //}
            return View(obj1);
        }
        [HttpPost]
        public ActionResult EnrollmentFeesSubmit(int id = 0, string saveandnext = "", int boardtype = 0, string BackandEdit = "")
        {

            //return RedirectToAction("EnrollmentFeesSubmit");

            StudentAdmissionQualification obj = new StudentAdmissionQualification();
            QualifiationMasterList sub = new QualifiationMasterList();
            //if (saveandnext == "saveandnext")
            //{
            //    if (boardtype == 0)
            //    {
            //        return RedirectToAction("EnrollmentFeesSubmit");
            //    }
            //    obj.student_save_boardtype(Convert.ToInt32(EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENNBStID"))), boardtype);
            //    return RedirectToAction("EnrollmentFeesSubmit");
            //}
            //if (BackandEdit == "BackandEdit")
            //{

            //    obj.student_save_boardtype(Convert.ToInt32(EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENNBStID"))), 0);
            //    return RedirectToAction("EnrollmentFeesSubmit");
            //}

            ExamForm ob = new ExamForm();
            AcademicSession ad = new AcademicSession();

            BL_PrintApplication PritApp = new BL_PrintApplication();
            var obj1 = ob.GetAppLicationDataForEnrollmentFee();

            Commn_master com = new Commn_master();
            Commn_master datecom = new Commn_master();
            datecom = com.check_EnrollmentSubmit_check(ad.GetAcademiccurrentSession().ID, Convert.ToInt32(CommonSetting.Commonid.EducationtypePG));
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
            if (obj1.objfeesubmit.IsRegistrationFee == false)
            {
                ob.Status = false;
                //stlogin.Feessub();
                //stlogin.FeessubStudenttest();
                //TempData["msgfees"] = "Fees Submitted Successfully !!!";
                //return RedirectToAction("FeesSubmit");
                //return RedirectToAction("PGGatewayEnrollment");old Sbi getway comment by jc
                return RedirectToAction("SelectGetwayRegistrationFee", "ExamPG");
            }
            else
            {
                ob.Status = false;
                TempData["msgfees"] = "Fees Already Submitted !!!";
                return RedirectToAction("EnrollmentFeesSubmit");
            }
            ViewBag.boardtype = CommonMethod.BoradtypePrevious().Where(x => x.boardid == 1 || x.boardid == 2);
            // sub = obj.QualificationdetailList(1, 1000);
            List<StudentAdmissionQualification> asa = new List<StudentAdmissionQualification>();
            //asa = sub.qlist.Where(x => x.QualicationType != 1).ToList();
            //var boardtype1 = asa.FirstOrDefault().boardtype;
            //if (boardtype1 == 0)
            //{
            //    ViewBag.showpayment = false;
            //}
            //else
            //{
            //    ViewBag.showpayment = false;
            //}
            return View(obj1);
        }


        public ActionResult SelectGetwayRegistrationFee()
        {
            return View();
        }

        public ActionResult RegistrationGatewayAirPay()
        {
            //  return RedirectToAction("Index", "HomePG");
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
                datecom = com.check_EnrollmentSubmit_check(ad.GetAcademiccurrentSession().ID, Convert.ToInt32(CommonSetting.Commonid.EducationtypePG));
                ViewBag.check_EnrollmentFeeSubmit_open = datecom.isopendate;
                ViewBag.check_EnrollmentFeeSubmit_Close = datecom.isclosedate;
                ViewBag.enrollStartdateValue = datecom.opendate;
                ViewBag.ViewBagenrollExtenddateValue = datecom.closedate;
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
                    if (Amount1 <= 0)
                    {
                        return RedirectToAction("EnrollmentFeesSubmit");
                    }
                    var obj = sbi.encriptData(Amount1, EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENBApplicationNo")), "studentPG/ExamPG/PGSucessEnrollment", "studentPG/ExamPG/PGFailedEnrollment");

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

        public ActionResult RegistrationGatewayEaseBuzz()
        {


            //return RedirectToAction("ResponseEnrollment");

            //  return RedirectToAction("Index", "HomePG");
            try
            {
                SbiPayEnrollment sbi = new SbiPayEnrollment();
                FeesSubmit stlogin = new FeesSubmit();
                ExamForm ob = new ExamForm();
                AcademicSession ad = new AcademicSession();
                Commn_master com = new Commn_master();
                decimal Amount1 = 1;
                string MerchantCustomerID1 = "1";

                string ApplicationID = EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENBApplicationNo"));
                StudentLogin tblST = new StudentLogin();
                var obj1 = tblST.BasicDetail(ApplicationID);
                Commn_master datecom = new Commn_master();
                datecom = com.check_EnrollmentSubmit_check(ad.GetAcademiccurrentSession().ID, Convert.ToInt32(CommonSetting.Commonid.EducationtypePG));
                ViewBag.check_EnrollmentFeeSubmit_open = datecom.isopendate;
                ViewBag.check_EnrollmentFeeSubmit_Close = datecom.isclosedate;
                ViewBag.enrollStartdateValue = datecom.opendate;
                ViewBag.ViewBagenrollExtenddateValue = datecom.closedate;
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
                    if (Amount1 <= 0)
                    {
                        return RedirectToAction("EnrollmentFeesSubmit");
                    }
                    //var obj = sbi.encriptData(Amount1, EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENBApplicationNo")), "studentPG/ExamPG/PGSucessEnrollment", "studentPG/ExamPG/PGFailedEnrollment");
                    // string SucccessUrl = " http://localhost:33166/StudentPG/ExamPG/PGSucessEnrollmentEaseBuzz?CollegeId=" + "1000";

                    string SucccessUrl = "https://portal.DemoUniversity.com/StudentPG/ExamPG/PGSucessEnrollmentEaseBuzz?CollegeId=" + "1000";

                    //Amount1 = 10; //Add by jitendra 
                    var obj = sbi.encriptDataEaseBuzz(Amount1, EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENBApplicationNo")), SucccessUrl, SucccessUrl, "", "", 0, obj1.StudentYear);
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
                CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "action PGGateway error for Enrollment Fee", ClsLanguage.GetCookies("NBApplicationNo"));

                return View();
            }
            return View();
        }



        public ActionResult PGGatewayEnrollment()
        {
            //  return RedirectToAction("Index", "HomePG");
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
                datecom = com.check_EnrollmentSubmit_check(ad.GetAcademiccurrentSession().ID, Convert.ToInt32(CommonSetting.Commonid.EducationtypePG));
                ViewBag.check_EnrollmentFeeSubmit_open = datecom.isopendate;
                ViewBag.check_EnrollmentFeeSubmit_Close = datecom.isclosedate;
                ViewBag.enrollStartdateValue = datecom.opendate;
                ViewBag.ViewBagenrollExtenddateValue = datecom.closedate;
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
                    if (Amount1 <= 0)
                    {
                        return RedirectToAction("EnrollmentFeesSubmit");
                    }
                    var obj = sbi.encriptData(Amount1, EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENBApplicationNo")), "studentPG/ExamPG/PGSucessEnrollment", "studentPG/ExamPG/PGFailedEnrollment");

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
            //  return RedirectToAction("Index", "HomePG");

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
            // return RedirectToAction("Index", "HomePG");
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

        public ActionResult PGSucessEnrollmentEaseBuzz(string CollegeId)
        {

            string Salt = CommonMethod.MIDcollegewiseEaseBuzz().Where(x => x.collegeid == Convert.ToInt32(1000)).FirstOrDefault().Salt;

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
                        PGstatus = "success";
                        SbiPayEnrollment sbi = new SbiPayEnrollment();
                        var result = sbi.pgsucessdecryptEaseBuzz(Convert.ToInt32(Sid), banktrxid, clienttrxid, amount1, feeamount, gst, commission, paymode, banktxndate, Reason, apitxnid, ApplicationNo, courseyearid, AdmissionType, Requestdata, dRequestdata, PGstatus, Sessionid);
                        return RedirectToAction("ResponseEnrollment");
                    }
                    else
                    {
                        SbiPayEnrollment sbi = new SbiPayEnrollment();
                        var result = sbi.pgsucessdecryptEaseBuzz(Convert.ToInt32(Sid), banktrxid, clienttrxid, amount1, feeamount, gst, commission, paymode, banktxndate, Reason, apitxnid, ApplicationNo, courseyearid, AdmissionType, Requestdata, dRequestdata, PGstatus, Sessionid);
                        //var result = sbi.AirPaypgsucessdecrypt(Convert.ToInt32(Sid), banktrxid, clienttrxid, amount1, feeamount, gst, commission, paymode, banktxndate, Reason, apitxnid, ApplicationNo, courseyearid, examType, Requestdata, dRequestdata, TRANSACTIONSTATUS);
                        return RedirectToAction("ResponseEnrollment");
                    }
                }
                catch (Exception ex)
                {
                    CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, " Air Pay Payment PaymentGateway suucess url hit on Controller error for Exam Fees Submit", ClsLanguage.GetCookies("NBApplicationNo"));
                }
            }
            return RedirectToAction("EnrollmentFeesSubmit");
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


        public ActionResult PGFailedEnrollment()
        {
            //return RedirectToAction("Index", "HomePG");
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
        public ActionResult BackExamFeesSubmit()
        {
            ExamForm ob = new ExamForm();
            AcademicSession ad = new AcademicSession();
            BL_PrintApplication PritApp = new BL_PrintApplication();
            //// manaul change  course year value from  fetch tbl_CourseYear according to
            int courseyearid = 0;
            if (Convert.ToInt32(EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENNBCourseCategory"))) == 7)
            {
                courseyearid = 17;// couryearid 16 for first semester ma; , kon se semester ke lia open krna h , manaul set semesterid
            }
            if (Convert.ToInt32(EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENNBCourseCategory"))) == 9)
            {
                courseyearid = 27;// couryearid 26 for first semester mcom; , kon se semester ke lia open krna h , manaul set semesterid
            }
            if (Convert.ToInt32(EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENNBCourseCategory"))) == 11)
            {
                courseyearid = 25;// couryearid 24 for first semester msc; , kon se semester ke lia open krna h , manaul set semesterid
            }
            Commn_master com = new Commn_master();


            Commn_master datecom = new Commn_master();
            datecom = com.check_BAckExamFeeSubmit_check(ad.GetAcademiccurrentSession().ID, Convert.ToInt32(CommonSetting.Commonid.EducationtypePG));
            ViewBag.check_ExamFeeSubmit_open = datecom.isopendate;
            ViewBag.check_ExamFeeSubmit_Close = datecom.isclosedate;
            ViewBag.check_admissionopen = ViewBag.check_ExamFeeSubmit_open;


            var obj1 = ob.backGetAppLicationDataForExamFee_PG(Convert.ToInt32(EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENNBStID"))), courseyearid, System.DateTime.Now.Year);

            if (obj1.objExamFrom == null)
            {
                return RedirectToAction("Index", "HomePG");
            }
            if (obj1.objExamFrom.isfeesubmitregistration == 0)
            {
                return RedirectToAction("Index", "HomePG");
            }
            if (obj1.objExamFrom.RegistrationNo == null || obj1.objExamFrom.RegistrationNo == "")
            {
                return RedirectToAction("Index", "HomePG");
            }
            if (obj1.objExamFrom.RollNo == null || obj1.objExamFrom.RollNo == "")
            {
                return RedirectToAction("Index", "HomePG");
            }

            // only for session student check

            //if (obj1.objExamFrom.sessionid != 39)
            //{
            //    return RedirectToAction("Index", "HomePG");
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
            subjectlist = ob.backFeesDetailSubjectlist_pg(obj1.objExamFrom.coursecategoryid, obj1.objExamFrom.StreamCategoryID, courseyearid, obj1.objExamFrom.sessionid, obj1.objExamFrom.collegeid, obj1.objExamFrom.sid);
            if (subjectlist.Count == 0)
            {
                // if subject count greater than 0 then is counted as back student else it not consider as back student
                return RedirectToAction("Index", "HomePG");
            }
            var tuple = new Tuple<PrintExamForm, QualifiationMasterList, List<EaxmFeesSubmit>, List<EaxmFeesSubmit>>(obj1, sub, feestruckture, subjectlist);
            return View(tuple);
        }
        [HttpPost]
        public ActionResult BackExamFeesSubmit(int id = 0, string applyadmissionform = "", string Substreamcategoryid = "", string BackandEdit = "", string checkboxid = "", string isappeared = "0")
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
            int courseyearid = 0;
            if (Convert.ToInt32(EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENNBCourseCategory"))) == 7)
            {
                courseyearid = 17;// couryearid 16 for first semester ma; , kon se semester ke lia open krna h , manaul set semesterid
            }
            if (Convert.ToInt32(EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENNBCourseCategory"))) == 9)
            {
                courseyearid = 27;// couryearid 26 for first semester msc; , kon se semester ke lia open krna h , manaul set semesterid
            }
            if (Convert.ToInt32(EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENNBCourseCategory"))) == 11)
            {
                courseyearid = 25;// couryearid 24 for first semester mcon; , kon se semester ke lia open krna h , manaul set semesterid
            }
            var obj1 = ob.backGetAppLicationDataForExamFee_PG(Convert.ToInt32(EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENNBStID"))), courseyearid, System.DateTime.Now.Year);
            Commn_master com = new Commn_master();
            Commn_master datecom = new Commn_master();
            datecom = com.check_BAckExamFeeSubmit_check(ad.GetAcademiccurrentSession().ID, Convert.ToInt32(CommonSetting.Commonid.EducationtypePG));
            ViewBag.check_ExamFeeSubmit_open = datecom.isopendate;
            ViewBag.check_ExamFeeSubmit_Close = datecom.isclosedate;
            ViewBag.check_admissionopen = ViewBag.check_ExamFeeSubmit_open;

            if (ViewBag.check_ExamFeeSubmit_open == true && ViewBag.check_ExamFeeSubmit_Close == true)
            {

            }
            else
            {
                return RedirectToAction("Index", "HomePG");
            }
            if (obj1.objExamFrom == null)
            {
                return RedirectToAction("Index", "HomePG");
            }
            if (obj1.objExamFrom.isfeesubmitregistration == 0)
            {
                return RedirectToAction("Index", "HomePG");
            }

            // only for session student check

            //if (obj1.objExamFrom.sessionid != 39)
            //{
            //    return RedirectToAction("Index", "HomePG");
            //}
            if (applyadmissionform == "applyadmissionform")
            {
                //if (Substreamcategoryid == "")
                //{
                //    return RedirectToAction("ExamFeesSubmit");
                //}
                if (checkboxid == "")
                {
                    return RedirectToAction("BackExamFeesSubmit");
                }
                objexamfrom.student_examform_apply_back(obj1.objExamFrom.sid, obj1.objExamFrom.sessionid, obj1.objExamFrom.coursecategoryid, obj1.objExamFrom.collegeid, obj1.objExamFrom.StreamCategoryID, obj1.objExamFrom.courseyearid, Convert.ToInt32(isappeared), Convert.ToInt32(0), "PG", "", System.DateTime.Now.Year);
                //return RedirectToAction("BackExamFeesSubmit");
            }
            //if (obj1.objExamFrom.IsDocVerify == 1)
            //{

            //}
            //else
            //{
            //    return RedirectToAction("BackExamFeesSubmit");
            //}

            StudentAdmissionQualification obj = new StudentAdmissionQualification();
            QualifiationMasterList sub = new QualifiationMasterList();
            List<EaxmFeesSubmit> feestruckture = new List<EaxmFeesSubmit>();
            List<EaxmFeesSubmit> subjectlist = new List<EaxmFeesSubmit>();
            List<EaxmFeesSubmit> Electivesubjectlist = new List<EaxmFeesSubmit>();
            feestruckture = ob.backFeesDetailsstructure(obj1.objExamFrom.collegeid, obj1.objExamFrom.coursecategoryid, obj1.objExamFrom.sessionid, obj1.objExamFrom.castecategoryid, obj1.objExamFrom.StreamCategoryID, obj1.objExamFrom.courseyearid, 0);
            subjectlist = ob.backFeesDetailSubjectlist_pg(obj1.objExamFrom.coursecategoryid, obj1.objExamFrom.StreamCategoryID, courseyearid, obj1.objExamFrom.sessionid, obj1.objExamFrom.collegeid, obj1.objExamFrom.sid);
            if (subjectlist.Count == 0)
            {
                return RedirectToAction("Index", "HomePG");
            }
            if (obj1.objExamFrom.IsExamfeesubmit == 0)
            {
                // TempData["msgfees"] = "Please wait till exam fee open date !!!";
                //return RedirectToAction("ExamFeesSubmit");
                return RedirectToAction("BackPGGatewayExamAirPay");
            }
            else
            {
                FeesSubmit stlogin1 = new FeesSubmit();
                stlogin1.Status = false;
                TempData["msgfees"] = "Exam Fees Already Submitted !!!";
                return RedirectToAction("BackExamFeesSubmit");
            }
            var tuple = new Tuple<PrintExamForm, QualifiationMasterList, List<EaxmFeesSubmit>, List<EaxmFeesSubmit>>(obj1, sub, feestruckture, subjectlist);
            return View(tuple);
        }
        public ActionResult BackPGGatewayExam()
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
                int courseyearid = 0;
                if (Convert.ToInt32(EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENNBCourseCategory"))) == 7)
                {
                    courseyearid = 17;// couryearid 16 for first semester ma; , kon se semester ke lia open krna h , manaul set semesterid
                }
                if (Convert.ToInt32(EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENNBCourseCategory"))) == 9)
                {
                    courseyearid = 27;// couryearid 26 for first semester msc; , kon se semester ke lia open krna h , manaul set semesterid
                }
                if (Convert.ToInt32(EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENNBCourseCategory"))) == 11)
                {
                    courseyearid = 25;// couryearid 24 for first semester mcon; , kon se semester ke lia open krna h , manaul set semesterid
                }
                var obj1 = ob.backGetAppLicationDataForExamFee_PG(Convert.ToInt32(EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENNBStID"))), courseyearid, System.DateTime.Now.Year);
                feestruckture = ob.backFeesDetailsstructure(obj1.objExamFrom.collegeid, obj1.objExamFrom.coursecategoryid, obj1.objExamFrom.sessionid, obj1.objExamFrom.castecategoryid, obj1.objExamFrom.StreamCategoryID, obj1.objExamFrom.courseyearid, 0);
                // manaul change  course year value from  fetch tbl_CourseYear according to couryearid
                subjectlist = ob.backFeesDetailSubjectlist_pg(obj1.objExamFrom.coursecategoryid, obj1.objExamFrom.StreamCategoryID, courseyearid, obj1.objExamFrom.sessionid, obj1.objExamFrom.collegeid, obj1.objExamFrom.sid);

                if (subjectlist.Count == 0)
                {
                    return RedirectToAction("Index", "HomePG");
                }
                if (obj1.objExamFrom != null)
                {
                    AcademicSession ac = new AcademicSession();
                    int sessionid = ac.GetAcademiccurrentSession().ID;
                    int educationtype = Convert.ToInt32(CommonSetting.Commonid.EducationtypePG);
                    //var datestart = com.Backcheck_ExamFeeSubmit_open(ad.GetAcademiccurrentSession().ID, Convert.ToInt32(CommonSetting.Commonid.EducationtypePG));
                    //var dateextend = com.Backcheck_ExamFeeSubmit_close(ad.GetAcademiccurrentSession().ID, Convert.ToInt32(CommonSetting.Commonid.EducationtypePG));
                    Commn_master datecom = new Commn_master();
                    datecom = com.check_BAckExamFeeSubmit_check(ad.GetAcademiccurrentSession().ID, Convert.ToInt32(CommonSetting.Commonid.EducationtypePG));
                    ViewBag.check_ExamFeeSubmit_open = datecom.isopendate;
                    ViewBag.check_ExamFeeSubmit_Close = datecom.isclosedate;
                    ViewBag.check_admissionopen = ViewBag.check_ExamFeeSubmit_open;
                    if (ViewBag.check_ExamFeeSubmit_open == false)
                    {
                        return RedirectToAction("BackExamFeesSubmit");
                    }
                    if (ViewBag.check_ExamFeeSubmit_Close == false)
                    {
                        return RedirectToAction("BackExamFeesSubmit");
                    }

                    if (obj1.objExamFrom.IsDocVerify == 1)
                    {

                    }
                    else
                    {
                        return RedirectToAction("Index", "HomePG");
                    }
                    if (obj1.objExamFrom.IsExamfeesubmit == 1) { return RedirectToAction("BackExamFeesSubmit"); }
                    Amount1 = Convert.ToDecimal(feestruckture.Sum(x => x.amount));
                    latefee = Convert.ToDecimal(feestruckture.Sum(x => x.late_fee));
                    amount_without_latefee = Amount1;
                    Amount1 = Amount1 + latefee;
                    if (Amount1 <= 0)
                    {
                        TempData["msgerror"] = "Amount Should be greater than 0 !!";
                        return RedirectToAction("BackExamFeesSubmit", "ExamPG");
                    }
                    //Amount1 = 1;
                    var obj = sbi.encriptDataExambackyear(Amount1, EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENBApplicationNo")), "studentpg/Exampg/backPGSucessExam", "studentpg/Exampg/backPGFailedExam", obj1.objExamFrom.YearName, latefee, amount_without_latefee, obj1.objExamFrom.courseyearid);
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



        public ActionResult BackPGGatewayExamAirPay()
        {
            //  return RedirectToAction("Index", "Home");
            try
            {

                StudentLogin stu = new StudentLogin();
                string ApplicationID = ClsLanguage.GetCookies("NBApplicationNo");
                Login lo = stu.BasicDetail(ApplicationID);
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
                int courseyearid = 0;
                if (Convert.ToInt32(EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENNBCourseCategory"))) == 7)
                {
                    courseyearid = 17;// couryearid 16 for first semester ma; , kon se semester ke lia open krna h , manaul set semesterid
                }
                if (Convert.ToInt32(EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENNBCourseCategory"))) == 9)
                {
                    courseyearid = 27;// couryearid 26 for first semester msc; , kon se semester ke lia open krna h , manaul set semesterid
                }
                if (Convert.ToInt32(EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENNBCourseCategory"))) == 11)
                {
                    courseyearid = 25;// couryearid 24 for first semester mcon; , kon se semester ke lia open krna h , manaul set semesterid
                }
                var obj1 = ob.backGetAppLicationDataForExamFee_PG(Convert.ToInt32(EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENNBStID"))), courseyearid, System.DateTime.Now.Year);
                feestruckture = ob.backFeesDetailsstructure(obj1.objExamFrom.collegeid, obj1.objExamFrom.coursecategoryid, obj1.objExamFrom.sessionid, obj1.objExamFrom.castecategoryid, obj1.objExamFrom.StreamCategoryID, obj1.objExamFrom.courseyearid, 0);
                // manaul change  course year value from  fetch tbl_CourseYear according to couryearid
                subjectlist = ob.backFeesDetailSubjectlist_pg(obj1.objExamFrom.coursecategoryid, obj1.objExamFrom.StreamCategoryID, courseyearid, obj1.objExamFrom.sessionid, obj1.objExamFrom.collegeid, obj1.objExamFrom.sid);

                if (subjectlist.Count == 0)
                {
                    return RedirectToAction("Index", "HomePG");
                }
                if (obj1.objExamFrom != null)
                {
                    AcademicSession ac = new AcademicSession();
                    int sessionid = ac.GetAcademiccurrentSession().ID;
                    int educationtype = Convert.ToInt32(CommonSetting.Commonid.EducationtypePG);
                    //var datestart = com.Backcheck_ExamFeeSubmit_open(ad.GetAcademiccurrentSession().ID, Convert.ToInt32(CommonSetting.Commonid.EducationtypePG));
                    //var dateextend = com.Backcheck_ExamFeeSubmit_close(ad.GetAcademiccurrentSession().ID, Convert.ToInt32(CommonSetting.Commonid.EducationtypePG));
                    Commn_master datecom = new Commn_master();
                    datecom = com.check_BAckExamFeeSubmit_check(ad.GetAcademiccurrentSession().ID, Convert.ToInt32(CommonSetting.Commonid.EducationtypePG));
                    ViewBag.check_ExamFeeSubmit_open = datecom.isopendate;
                    ViewBag.check_ExamFeeSubmit_Close = datecom.isclosedate;
                    ViewBag.check_admissionopen = ViewBag.check_ExamFeeSubmit_open;
                    if (ViewBag.check_ExamFeeSubmit_open == false)
                    {
                        return RedirectToAction("BackExamFeesSubmit");
                    }
                    if (ViewBag.check_ExamFeeSubmit_Close == false)
                    {
                        return RedirectToAction("BackExamFeesSubmit");
                    }

                    if (obj1.objExamFrom.IsDocVerify == 1)
                    {

                    }
                    else
                    {
                        return RedirectToAction("Index", "HomePG");
                    }
                    if (obj1.objExamFrom.IsExamfeesubmit == 1) { return RedirectToAction("BackExamFeesSubmit"); }
                    Amount1 = Convert.ToDecimal(feestruckture.Sum(x => x.amount));
                    latefee = Convert.ToDecimal(feestruckture.Sum(x => x.late_fee));
                    amount_without_latefee = Amount1;
                    Amount1 = Amount1 + latefee;
                    if (Amount1 <= 0)
                    {
                        TempData["msgerror"] = "Amount Should be greater than 0 !!";
                        return RedirectToAction("BackExamFeesSubmit", "ExamPG");
                    }
                    string sEmail = lo.Email;
                    string sPhone = lo.MobileNo;
                    string sFName = lo.FirstName;
                    string sLName = lo.LastName;
                    string sAddress = lo.CurrentAddress;
                    string sCity = lo.CA_City.ToString();
                    string sState = lo.CA_State.ToString();
                    string sCountry = lo.CA_Country.ToString();
                    string sPincode = lo.CA_PinCode;
                    string sAmount = Amount1.ToString("0.00");
                    string sOrderId = "";
                  
                    EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENBApplicationNo"));//other Amount  me bhejna h
                    //Amount1 = 1;
                    var obj = sbi.encriptDataExambackyearAirPay(Amount1, EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENBApplicationNo")), "studentPG/ExamPG/AirPayPGSucessExam", "student/Exam/AirPayPGFailedExam", obj1.objExamFrom.YearName, latefee, amount_without_latefee, obj1.objExamFrom.courseyearid, sEmail, sPhone, sFName, sLName, sAddress, sCity, sState, sCountry, sPincode, sAmount, sOrderId);

                    ViewBag.orderid = obj.Oid;
                    ViewBag.buyerEmail = sEmail;
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
                    ViewBag.url = obj.url;
                    ViewBag.success_url = obj.A_Success_url;
                    ViewBag.customvar = obj.customvar;

                }
            }
            catch (Exception ex)
            {
                CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "action PGGateway error for Eaxm Fee", ClsLanguage.GetCookies("NBApplicationNo"));

                return View();
            }
            return View();
        }


        public ActionResult BackResponseExam()
        {
            // return RedirectToAction("Index", "Home");

            try
            {
                // manaul change  course year value from  fetch tbl_CourseYear according to couryearid
                int courseyearid = 0;
                if (Convert.ToInt32(EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENNBCourseCategory"))) == 7)
                {
                    courseyearid = 40;// couryearid 16 for first semester ma; , kon se semester ke lia open krna h , manaul set semesterid
                }
                if (Convert.ToInt32(EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENNBCourseCategory"))) == 9)
                {
                    courseyearid = 38;// couryearid 26 for first semester msc; , kon se semester ke lia open krna h , manaul set semesterid
                }
                if (Convert.ToInt32(EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENNBCourseCategory"))) == 11)
                {
                    courseyearid = 36;// couryearid 24 for first semester mcon; , kon se semester ke lia open krna h , manaul set semesterid
                }
                ExamForm ob = new ExamForm();
                AcademicSession ad = new AcademicSession();
                // List<EaxmFeesSubmit> subjectlist = new List<EaxmFeesSubmit>();
                // manaul change  course year value from  fetch tbl_CourseYear according to couryearid

                var obj1 = ob.backGetAppLicationDataForExamFee_PG(Convert.ToInt32(EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENNBStID"))), courseyearid, System.DateTime.Now.Year);
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
                    return RedirectToAction("BackResponseExam");
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
                    return RedirectToAction("backResponseExam");
                }
                catch (Exception ex)
                {
                    CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "Payment PaymentGateway suucess url hit on Controller error for Exam Fees Submit", ClsLanguage.GetCookies("NBApplicationNo"));

                }
            }
            return RedirectToAction("BackExamFeesSubmit");
            return View();





        }


        /// add by jitendra 

        public ActionResult BackExamFeesSubmit1()
        {
            ExamForm ob = new ExamForm();
            AcademicSession ad = new AcademicSession();
            BL_PrintApplication PritApp = new BL_PrintApplication();
            //// manaul change  course year value from  fetch tbl_CourseYear according to
            int courseyearid = 0;
            if (Convert.ToInt32(EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENNBCourseCategory"))) == 7)
            {
                courseyearid = 40;// couryearid 16 for first semester ma; , kon se semester ke lia open krna h , manaul set semesterid
            }
            if (Convert.ToInt32(EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENNBCourseCategory"))) == 9)
            {
                courseyearid = 38;// couryearid 26 for first semester mcom; , kon se semester ke lia open krna h , manaul set semesterid
            }
            if (Convert.ToInt32(EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENNBCourseCategory"))) == 11)
            {
                courseyearid = 36;// couryearid 24 for first semester msc; , kon se semester ke lia open krna h , manaul set semesterid
            }
            Commn_master com = new Commn_master();


            Commn_master datecom = new Commn_master();
            datecom = com.check_BAckExamFeeSubmit_check(ad.GetAcademiccurrentSession().ID, Convert.ToInt32(CommonSetting.Commonid.EducationtypePG));
            ViewBag.check_ExamFeeSubmit_open = datecom.isopendate;
            ViewBag.check_ExamFeeSubmit_Close = datecom.isclosedate;
            ViewBag.check_admissionopen = ViewBag.check_ExamFeeSubmit_open;


            var obj1 = ob.backGetAppLicationDataForExamFee_PG(Convert.ToInt32(EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENNBStID"))), courseyearid, System.DateTime.Now.Year);

            if (obj1.objExamFrom == null)
            {
                return RedirectToAction("Index", "HomePG");
            }
            if (obj1.objExamFrom.isfeesubmitregistration == 0)
            {
                return RedirectToAction("Index", "HomePG");
            }
            if (obj1.objExamFrom.RegistrationNo == null || obj1.objExamFrom.RegistrationNo == "")
            {
                return RedirectToAction("Index", "HomePG");
            }
            if (obj1.objExamFrom.RollNo == null || obj1.objExamFrom.RollNo == "")
            {
                return RedirectToAction("Index", "HomePG");
            }

            // only for session student check

            //if (obj1.objExamFrom.sessionid != 39)
            //{
            //    return RedirectToAction("Index", "HomePG");
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
            subjectlist = ob.backFeesDetailSubjectlist_pg(obj1.objExamFrom.coursecategoryid, obj1.objExamFrom.StreamCategoryID, courseyearid, obj1.objExamFrom.sessionid, obj1.objExamFrom.collegeid, obj1.objExamFrom.sid);
            //if (subjectlist.Count == 0)
            //{
            //    // if subject count greater than 0 then is counted as back student else it not consider as back student
            //    return RedirectToAction("Index", "HomePG");
            //}
            var tuple = new Tuple<PrintExamForm, QualifiationMasterList, List<EaxmFeesSubmit>, List<EaxmFeesSubmit>>(obj1, sub, feestruckture, subjectlist);
            return View(tuple);
        }
        [HttpPost]
        public ActionResult BackExamFeesSubmit1(int id = 0, string applyadmissionform = "", string Substreamcategoryid = "", string BackandEdit = "", string checkboxid = "", string isappeared = "0")
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
            int courseyearid = 0;
            if (Convert.ToInt32(EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENNBCourseCategory"))) == 7)
            {
                courseyearid = 40;// couryearid 16 for first semester ma; , kon se semester ke lia open krna h , manaul set semesterid
            }
            if (Convert.ToInt32(EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENNBCourseCategory"))) == 9)
            {
                courseyearid = 38;// couryearid 26 for first semester mcom; , kon se semester ke lia open krna h , manaul set semesterid
            }
            if (Convert.ToInt32(EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENNBCourseCategory"))) == 11)
            {
                courseyearid = 36;// couryearid 24 for first semester msc; , kon se semester ke lia open krna h , manaul set semesterid
            }
            var obj1 = ob.backGetAppLicationDataForExamFee_PG(Convert.ToInt32(EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENNBStID"))), courseyearid, System.DateTime.Now.Year);
            Commn_master com = new Commn_master();
            Commn_master datecom = new Commn_master();
            datecom = com.check_BAckExamFeeSubmit_check(ad.GetAcademiccurrentSession().ID, Convert.ToInt32(CommonSetting.Commonid.EducationtypePG));
            ViewBag.check_ExamFeeSubmit_open = datecom.isopendate;
            ViewBag.check_ExamFeeSubmit_Close = datecom.isclosedate;
            ViewBag.check_admissionopen = ViewBag.check_ExamFeeSubmit_open;

            if (ViewBag.check_ExamFeeSubmit_open == true && ViewBag.check_ExamFeeSubmit_Close == true)
            {

            }
            else
            {
                return RedirectToAction("Index", "HomePG");
            }
            if (obj1.objExamFrom == null)
            {
                return RedirectToAction("Index", "HomePG");
            }
            if (obj1.objExamFrom.isfeesubmitregistration == 0)
            {
                return RedirectToAction("Index", "HomePG");
            }

            // only for session student check

            //if (obj1.objExamFrom.sessionid != 39)
            //{
            //    return RedirectToAction("Index", "HomePG");
            //}
            if (applyadmissionform == "applyadmissionform")
            {
                //if (Substreamcategoryid == "")
                //{
                //    return RedirectToAction("ExamFeesSubmit");
                //}
                if (checkboxid == "")
                {
                    return RedirectToAction("BackExamFeesSubmit");
                }
                objexamfrom.student_examform_apply_back(obj1.objExamFrom.sid, obj1.objExamFrom.sessionid, obj1.objExamFrom.coursecategoryid, obj1.objExamFrom.collegeid, obj1.objExamFrom.StreamCategoryID, obj1.objExamFrom.courseyearid, Convert.ToInt32(isappeared), Convert.ToInt32(0), "PG", "", System.DateTime.Now.Year);
                //return RedirectToAction("BackExamFeesSubmit");
            }
            //if (obj1.objExamFrom.IsDocVerify == 1)
            //{

            //}
            //else
            //{
            //    return RedirectToAction("BackExamFeesSubmit");
            //}

            StudentAdmissionQualification obj = new StudentAdmissionQualification();
            QualifiationMasterList sub = new QualifiationMasterList();
            List<EaxmFeesSubmit> feestruckture = new List<EaxmFeesSubmit>();
            List<EaxmFeesSubmit> subjectlist = new List<EaxmFeesSubmit>();
            List<EaxmFeesSubmit> Electivesubjectlist = new List<EaxmFeesSubmit>();
            feestruckture = ob.backFeesDetailsstructure(obj1.objExamFrom.collegeid, obj1.objExamFrom.coursecategoryid, obj1.objExamFrom.sessionid, obj1.objExamFrom.castecategoryid, obj1.objExamFrom.StreamCategoryID, obj1.objExamFrom.courseyearid, 0);
            subjectlist = ob.backFeesDetailSubjectlist_pg(obj1.objExamFrom.coursecategoryid, obj1.objExamFrom.StreamCategoryID, courseyearid, obj1.objExamFrom.sessionid, obj1.objExamFrom.collegeid, obj1.objExamFrom.sid);
            if (subjectlist.Count == 0)
            {
                return RedirectToAction("Index", "HomePG");
            }
            if (obj1.objExamFrom.IsExamfeesubmit == 0)
            {
                // TempData["msgfees"] = "Please wait till exam fee open date !!!";
                ////return RedirectToAction("ExamFeesSubmit");
                return RedirectToAction("BackPGGatewayExamAirPay");
            }
            else
            {
                FeesSubmit stlogin1 = new FeesSubmit();
                stlogin1.Status = false;
                TempData["msgfees"] = "Exam Fees Already Submitted !!!";
                return RedirectToAction("BackExamFeesSubmit1");
            }
            var tuple = new Tuple<PrintExamForm, QualifiationMasterList, List<EaxmFeesSubmit>, List<EaxmFeesSubmit>>(obj1, sub, feestruckture, subjectlist);
            return View(tuple);
        }
        public ActionResult BackPGGatewayExam1()
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
                int courseyearid = 0;
                if (Convert.ToInt32(EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENNBCourseCategory"))) == 7)
                {
                    courseyearid = 17;// couryearid 16 for first semester ma; , kon se semester ke lia open krna h , manaul set semesterid
                }
                if (Convert.ToInt32(EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENNBCourseCategory"))) == 9)
                {
                    courseyearid = 27;// couryearid 26 for first semester mcom; , kon se semester ke lia open krna h , manaul set semesterid
                }
                if (Convert.ToInt32(EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENNBCourseCategory"))) == 11)
                {
                    courseyearid = 25;// couryearid 24 for first semester msc; , kon se semester ke lia open krna h , manaul set semesterid
                }
                var obj1 = ob.backGetAppLicationDataForExamFee_PG(Convert.ToInt32(EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENNBStID"))), courseyearid, System.DateTime.Now.Year);
                feestruckture = ob.backFeesDetailsstructure(obj1.objExamFrom.collegeid, obj1.objExamFrom.coursecategoryid, obj1.objExamFrom.sessionid, obj1.objExamFrom.castecategoryid, obj1.objExamFrom.StreamCategoryID, obj1.objExamFrom.courseyearid, 0);
                // manaul change  course year value from  fetch tbl_CourseYear according to couryearid
                subjectlist = ob.backFeesDetailSubjectlist_pg(obj1.objExamFrom.coursecategoryid, obj1.objExamFrom.StreamCategoryID, courseyearid, obj1.objExamFrom.sessionid, obj1.objExamFrom.collegeid, obj1.objExamFrom.sid);

                if (subjectlist.Count == 0)
                {
                    return RedirectToAction("Index", "HomePG");
                }
                if (obj1.objExamFrom != null)
                {
                    AcademicSession ac = new AcademicSession();
                    int sessionid = ac.GetAcademiccurrentSession().ID;
                    int educationtype = Convert.ToInt32(CommonSetting.Commonid.EducationtypePG);
                    //var datestart = com.Backcheck_ExamFeeSubmit_open(ad.GetAcademiccurrentSession().ID, Convert.ToInt32(CommonSetting.Commonid.EducationtypePG));
                    //var dateextend = com.Backcheck_ExamFeeSubmit_close(ad.GetAcademiccurrentSession().ID, Convert.ToInt32(CommonSetting.Commonid.EducationtypePG));
                    Commn_master datecom = new Commn_master();
                    datecom = com.check_BAckExamFeeSubmit_check(ad.GetAcademiccurrentSession().ID, Convert.ToInt32(CommonSetting.Commonid.EducationtypePG));
                    ViewBag.check_ExamFeeSubmit_open = datecom.isopendate;
                    ViewBag.check_ExamFeeSubmit_Close = datecom.isclosedate;
                    ViewBag.check_admissionopen = ViewBag.check_ExamFeeSubmit_open;
                    if (ViewBag.check_ExamFeeSubmit_open == false)
                    {
                        return RedirectToAction("BackExamFeesSubmit1");
                    }
                    if (ViewBag.check_ExamFeeSubmit_Close == false)
                    {
                        return RedirectToAction("BackExamFeesSubmit1");
                    }

                    if (obj1.objExamFrom.IsDocVerify == 1)
                    {

                    }
                    else
                    {
                        return RedirectToAction("Index", "HomePG");
                    }
                    if (obj1.objExamFrom.IsExamfeesubmit == 1) { return RedirectToAction("BackExamFeesSubmit1"); }
                    Amount1 = Convert.ToDecimal(feestruckture.Sum(x => x.amount));
                    latefee = Convert.ToDecimal(feestruckture.Sum(x => x.late_fee));
                    amount_without_latefee = Amount1;
                    Amount1 = Amount1 + latefee;
                    if (Amount1 <= 0)
                    {
                        TempData["msgerror"] = "Amount Should be greater than 0 !!";
                        return RedirectToAction("BackExamFeesSubmit1", "ExamPG");
                    }
                    //Amount1 = 1;
                    var obj = sbi.encriptDataExambackyear(Amount1, EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENBApplicationNo")), "studentpg/Exampg/backPGSucessExam1", "studentpg/Exampg/backPGFailedExam1", obj1.objExamFrom.YearName, latefee, amount_without_latefee, obj1.objExamFrom.courseyearid);
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
        public ActionResult BackResponseExam1()
        {
            // return RedirectToAction("Index", "Home");

            try
            {
                // manaul change  course year value from  fetch tbl_CourseYear according to couryearid
                int courseyearid = 0;
                if (Convert.ToInt32(EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENNBCourseCategory"))) == 7)
                {
                    courseyearid = 17;// couryearid 16 for first semester ma; , kon se semester ke lia open krna h , manaul set semesterid
                }
                if (Convert.ToInt32(EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENNBCourseCategory"))) == 9)
                {
                    courseyearid = 27;// couryearid 26 for first semester mcom; , kon se semester ke lia open krna h , manaul set semesterid
                }
                if (Convert.ToInt32(EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENNBCourseCategory"))) == 11)
                {
                    courseyearid = 25;// couryearid 24 for first semester msc; , kon se semester ke lia open krna h , manaul set semesterid
                }
                ExamForm ob = new ExamForm();
                AcademicSession ad = new AcademicSession();
                // List<EaxmFeesSubmit> subjectlist = new List<EaxmFeesSubmit>();
                // manaul change  course year value from  fetch tbl_CourseYear according to couryearid

                var obj1 = ob.backGetAppLicationDataForExamFee_PG(Convert.ToInt32(EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENNBStID"))), courseyearid, System.DateTime.Now.Year);
                return View(obj1);

            }
            catch (Exception ex)
            {
                CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "Response payment gateway get method for exam Fee on Controller error", ClsLanguage.GetCookies("NBApplicationNo"));
                PrintExamForm PritApp = new PrintExamForm();
                return View(PritApp);
            }
        }
        public ActionResult BackPGSucessExam1()
        {
            //return RedirectToAction("Index", "Home");
            string paramInfo = "";


            if (Request.Form.Count > 0)
            {
                try
                {
                    SbiPayExam sbi = new SbiPayExam();
                    var result = sbi.pgsucessdecrypt(Convert.ToInt32(EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENNBStID"))));
                    return RedirectToAction("BackResponseExam1");
                }
                catch (Exception ex)
                {
                    CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "Payment PaymentGateway suucess url hit on Controller error for Exam Fees Submit", ClsLanguage.GetCookies("NBApplicationNo"));

                }
            }
            return RedirectToAction("BackExamFeesSubmit1");
            return View();
        }
        public ActionResult backPGFailedExam1()
        {
            //  return RedirectToAction("Index", "Home");
            string paramInfo = "";

            if (Request.Form.Count > 0)
            {
                try
                {
                    SbiPayExam sbi = new SbiPayExam();
                    var result = sbi.pgfaileddecrypt(Convert.ToInt32(EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENNBStID"))));
                    return RedirectToAction("backResponseExam1");
                }
                catch (Exception ex)
                {
                    CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "Payment PaymentGateway suucess url hit on Controller error for Exam Fees Submit", ClsLanguage.GetCookies("NBApplicationNo"));

                }
            }
            return RedirectToAction("BackExamFeesSubmit1");
            return View();





        }

    }
}