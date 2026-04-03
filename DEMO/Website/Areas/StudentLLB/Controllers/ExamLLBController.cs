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
using Website.Areas.StudentLLB.Models;
using Website.Areas.Student.Models;
using Website.Models;
using Newtonsoft;
using System.Text;
using System.Web.UI;
using NReco.PdfGenerator;
using com.toml.dp.util;
using Newtonsoft.Json;
using System.Data;
using System.IO;
namespace Website.Areas.StudentLLB.Controllers
{
    //[CookiesExpireFilterLLB]

    public class ExamLLBController : Controller
    {
        // GET: Student/Exam
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ExaminationFrom()
        {
            return RedirectToAction("Index", "HomeL");
            ExamForm ob = new ExamForm();
            Commn_master com = new Commn_master();
            AcademicSession ad = new AcademicSession();
            ViewBag.check_admissionopen = com.check_ExamFeeSubmit_open(ad.GetAcademiccurrentSession().ID, Convert.ToInt32(CommonSetting.Commonid.EducationtypeLLB));
            //int sid = ((ClsLanguage.GetCookies("NBStID") != null ? Convert.ToInt32(ClsLanguage.GetCookies("NBStID")) : 0));
            var result = ob.StudentDetail();
            return View(result);
        }
        public ActionResult ExamFeesSubmit()
        {
             //return RedirectToAction("Index", "HomeL");
            ExamForm ob = new ExamForm();
            AcademicSession ad = new AcademicSession();
            BL_PrintApplication PritApp = new BL_PrintApplication();
            var obj1 = ob.GetAppLicationDataForExamFee(Convert.ToInt32(EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENNBStID"))));
            Commn_master com = new Commn_master();
            Commn_master datecom = new Commn_master();
            datecom = com.check_ExamFeeSubmit_check(ad.GetAcademiccurrentSession().ID, Convert.ToInt32(CommonSetting.Commonid.EducationtypeLLB));
            ViewBag.check_ExamFeeSubmit_open = datecom.isopendate;
            ViewBag.check_ExamFeeSubmit_Close = datecom.isclosedate;
            if (datecom.isopendate == true && datecom.isclosedate == true)
            {
                ViewBag.check_admissionopen = true;
            }
            //if (ViewBag.check_ExamFeeSubmit_open == true && ViewBag.check_ExamFeeSubmit_Close == true)
            //{

            //}
            //else
            //{
            //    return RedirectToAction("Index", "HomeL");
            //}
            if (obj1.objExamFrom == null)
            {
                return RedirectToAction("Index", "HomeL");
            }
            if (obj1.objExamFrom.isfeesubmitregistration == 0)
            {
                return RedirectToAction("Index", "HomeL");
            }
            //if (obj1.objExamFrom.courseyearid != 31)
            //{
            //    return RedirectToAction("Index", "HomeL");
            //}
            //if (obj1.Examobjfeesubmit.sessionid == 40)
            //{
            //    return RedirectToAction("Index", "HomeL");
            //}
            StudentAdmissionQualification obj = new StudentAdmissionQualification();
            QualifiationMasterList sub = new QualifiationMasterList();
           // sub = obj.QualificationdetailList(1, 100000);
            ViewBag.previousqualification = sub;
            List<EaxmFeesSubmit> feestruckture = new List<EaxmFeesSubmit>();
            List<EaxmFeesSubmit> subjectlist = new List<EaxmFeesSubmit>();
            feestruckture = ob.FeesDetailsstructure(obj1.objExamFrom.collegeid, obj1.objExamFrom.coursecategoryid, obj1.objExamFrom.sessionid, obj1.objExamFrom.castecategoryid, obj1.objExamFrom.StreamCategoryID, obj1.objExamFrom.courseyearid, 0);
            subjectlist = ob.FeesDetailSubjectlist(obj1.objExamFrom.coursecategoryid, obj1.objExamFrom.StreamCategoryID, obj1.objExamFrom.courseyearid);

            var tuple = new Tuple<PrintExamForm, QualifiationMasterList, List<EaxmFeesSubmit>, List<EaxmFeesSubmit>>(obj1, sub,feestruckture,subjectlist);
            return View(tuple);
        }

        [HttpPost]
        public ActionResult ExamFeesSubmit(int id = 0 ,string applyadmissionform = "", string isappeared = "", string BackandEdit = "",string checkboxid="")
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
            ViewBag.check_ExamFeeSubmit_open = com.check_ExamFeeSubmit_open(ad.GetAcademiccurrentSession().ID, Convert.ToInt32(CommonSetting.Commonid.EducationtypeLLB));
            ViewBag.check_ExamFeeSubmit_Close = com.check_ExamFeeSubmit_close(ad.GetAcademiccurrentSession().ID, Convert.ToInt32(CommonSetting.Commonid.EducationtypeLLB));
            ViewBag.check_admissionopen = ViewBag.check_ExamFeeSubmit_open;
            if (ViewBag.check_ExamFeeSubmit_open == true && ViewBag.check_ExamFeeSubmit_Close == true)
            {

            }
            else
            {
                return RedirectToAction("Index", "HomeL");
            }
            if (obj1.objExamFrom == null)
            {
                return RedirectToAction("Index", "HomeL");
            }
            if (obj1.objExamFrom.isfeesubmitregistration == 0)
            {
                return RedirectToAction("Index", "HomeL");
            }

            //if (obj1.Examobjfeesubmit.sessionid != 40)
            //{
            //    // return RedirectToAction("Index", "HomeL");
            //}
            // for first verify from college thne fee submit
            if (applyadmissionform == "applyadmissionform")
            {
                if (isappeared == "")
                {
                    return RedirectToAction("ExamFeesSubmit");
                }
                if (checkboxid == "")
                {
                    return RedirectToAction("ExamFeesSubmit");
                }
                objexamfrom.student_examform_apply(obj1.objExamFrom.sid, obj1.objExamFrom.sessionid, obj1.objExamFrom.coursecategoryid, obj1.objExamFrom.collegeid, obj1.objExamFrom.StreamCategoryID, obj1.objExamFrom.courseyearid, Convert.ToInt32(isappeared), 0);
                return RedirectToAction("ExamFeesSubmit");
            }
            // for direct fess payment
            //if (obj1.objExamFrom.IsDocVerify == 1)
            //{

            //}
            //else
            //{
            //    objexamfrom.student_examform_apply(obj1.objExamFrom.sid, obj1.objExamFrom.sessionid, obj1.objExamFrom.coursecategoryid, obj1.objExamFrom.collegeid, obj1.objExamFrom.StreamCategoryID, obj1.objExamFrom.courseyearid, Convert.ToInt32(isappeared), Convert.ToInt32(0), "LLB", "");


            //    //return RedirectToAction("ExamFeesSubmit");
            //}

            StudentAdmissionQualification obj = new StudentAdmissionQualification();
            QualifiationMasterList sub = new QualifiationMasterList();
            sub = obj.QualificationdetailList(1, 100000);
            ViewBag.previousqualification = sub;
            List<EaxmFeesSubmit> feestruckture = new List<EaxmFeesSubmit>();
            List<EaxmFeesSubmit> subjectlist = new List<EaxmFeesSubmit>();
            feestruckture = ob.FeesDetailsstructure(obj1.objExamFrom.collegeid, obj1.objExamFrom.coursecategoryid, obj1.objExamFrom.sessionid, obj1.objExamFrom.castecategoryid, obj1.objExamFrom.StreamCategoryID, obj1.objExamFrom.courseyearid, 0);
            subjectlist = ob.FeesDetailSubjectlist( obj1.objExamFrom.coursecategoryid, obj1.objExamFrom.StreamCategoryID, obj1.objExamFrom.courseyearid);


            if (obj1.objExamFrom.IsExamfeesubmit == 0)
            {
               // TempData["msgfees"] = "Please wait till exam fee open date !!!";
                //return RedirectToAction("ExamFeesSubmit");
                 //return RedirectToAction("PGGatewayExam");
                return RedirectToAction("SelectPaymentGetway");
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
                    datecom = com.check_ExamFeeSubmit_check(ad.GetAcademiccurrentSession().ID, Convert.ToInt32(CommonSetting.Commonid.EducationtypeLLB));
                    if (datecom.isopendate == false)
                    {
                        return RedirectToAction("ExamFeesSubmit", "ExamLLB");
                    }
                    if (datecom.isclosedate == false)
                    {
                        return RedirectToAction("ExamFeesSubmit", "ExamLLB");
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
                        return RedirectToAction("ExamFeesSubmit", "ExamLLB");
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
                
                    var obj = sbi.encriptDataExamAirPay(Amount1, EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENBApplicationNo")), "studentLLB/ExamLLB/AirPayPGSucessExam", "studentLLB/ExamLLB/AirPayPGSucessExam", obj1.objExamFrom.YearName, latefee, amount_without_latefee, obj1.objExamFrom.courseyearid, sEmail, sPhone, sFName, sLName, sAddress, sCity, sState, sCountry, sPincode, sAmount, sOrderId);

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
                            PGstatus = "SUCCESS";
                            SbiPayExam sbi = new SbiPayExam();
                            //var result = sbi.pgsucessdecrypt(Convert.ToInt32(EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENNBStID"))));
                            var result = sbi.AirPaypgsucessdecrypt(Convert.ToInt32(Sid), banktrxid, clienttrxid, amount1, feeamount, gst, commission, paymode, banktxndate, Reason, apitxnid, ApplicationNo, courseyearid, examType, Requestdata, dRequestdata, PGstatus);

                            //var result = sbi.AirPaypgsucessdecrypt(Sid, banktrxid, clienttrxid, amount1, feeamount, gst, commission, paymode, banktxndate, Reason, apitxnid, ApplicationNo, courseyearid, Requestdata, dRequestdata, PGstatus);
                            CommonMethod.TraceLogWritetoNotepad("obj1.objExamFrom4", "Exam/AirPayPGSucessExam8", "obj1.objExamFrom", "Id");
                            return RedirectToAction("AirPayResponseExam");
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
                        return RedirectToAction("ResponseExam");

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
        public ActionResult AirPayResponseExam()
        {
            // return RedirectToAction("Index", "HomePG");

            try
            {
                CommonMethod.TraceLogWritetoNotepad("obj1.objExamFrom4", "Exam/AirPayResponseExam1", "obj1.objExamFrom", "Id");

                ExamForm ob = new ExamForm();
                AcademicSession ad = new AcademicSession();
                // List<EaxmFeesSubmit> subjectlist = new List<EaxmFeesSubmit>();

                var obj1 = ob.GetAppLicationDataForExamFee(Convert.ToInt32(EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENNBStID"))));
                CommonMethod.TraceLogWritetoNotepad("obj1.objExamFrom4", "Exam/AirPayResponseExam2", "obj1.objExamFrom", "SID" + obj1);
                return View(obj1);

            }
            catch (Exception ex)
            {
                CommonMethod.TraceLogWritetoNotepad("obj1.objExamFrom4", "Exam/AirPayResponseExam3", "obj1.objExamFrom", "ex" + ex);
                CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "Air Pay Response payment gateway get method for exam Fee on Controller error", ClsLanguage.GetCookies("NBApplicationNo"));
                PrintExamForm PritApp = new PrintExamForm();
                return View(PritApp);
            }
        }





        public ActionResult PGGatewayExam()
        {
            //  return RedirectToAction("Index", "HomeL");
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
                    int educationtype = Convert.ToInt32(CommonSetting.Commonid.EducationtypeLLB);
                    var datestart = com.check_ExamFeeSubmit_open(ad.GetAcademiccurrentSession().ID, Convert.ToInt32(CommonSetting.Commonid.EducationtypeLLB));
                    if (datestart == false)
                    {
                        return RedirectToAction("ExamFeesSubmit");
                    }
                    var dateextend = com.check_ExamFeeSubmit_close(ad.GetAcademiccurrentSession().ID, Convert.ToInt32(CommonSetting.Commonid.EducationtypeLLB));
                    if (dateextend == false)
                    {
                        return RedirectToAction("ExamFeesSubmit");
                    }
                    if (obj1.objExamFrom.IsDocVerify == 1)
                    {

                    }
                    else
                    { return RedirectToAction("ExamFeesSubmit", "examLLB"); }
                    if (obj1.objExamFrom.IsExamfeesubmit == 1) { return RedirectToAction("ExamFeesSubmit"); }
                    Amount1 = Convert.ToDecimal(feestruckture.Sum(x => x.amount));
                    latefee = Convert.ToDecimal(feestruckture.Sum(x => x.late_fee));
                    amount_without_latefee = Amount1;
                    Amount1 = Amount1 + latefee;
                    if (Amount1 <= 0)
                    {
                        TempData["msgerror"] = "Amount Should be greater than 0 !!";
                        return RedirectToAction("ExamFeesSubmit", "examLLB");
                    }
                    //Amount1 = 1;
                    var obj = sbi.encriptDataExam(Amount1, EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENBApplicationNo")), "studentLLB/ExamLLb/PGSucessExam", "studentLLB/ExamLLb/PGFailedExam", obj1.objExamFrom.YearName, latefee, amount_without_latefee, obj1.objExamFrom.courseyearid);
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
           // return RedirectToAction("Index", "HomeL");

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
            //return RedirectToAction("Index", "HomeL");
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
          //  return RedirectToAction("Index", "HomeL");
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
            ViewBag.subjectlist = ob.FeesDetailSubjectlist(result.coursecategoryid, result.StreamCategoryID, result.courseyearid);

            if (result == null)
            {
                return RedirectToAction("Index", "HomeL");
            }
            if (result.IsExamfeesubmit != 1)
            {
                return RedirectToAction("Index", "HomeL");
            }
            return View(result);
        }
        public ActionResult MigrationCertificate()
        {
            return RedirectToAction("Index", "HomeL");
            ExamForm ob = new ExamForm();
            Commn_master com = new Commn_master();
            AcademicSession ad = new AcademicSession();
            var result = ob.StudentDetailForAdmitCard();
            return View(result);
        }
        public ActionResult EnrollmentFeesSubmit()
        {
            //return RedirectToAction("Index", "HomeL");
            AcademicSession ad = new AcademicSession();
            ExamForm ob = new ExamForm();
            Commn_master com = new Commn_master();
            Commn_master datecom = new Commn_master();
            datecom = com.check_EnrollmentSubmit_check(ad.GetAcademiccurrentSession().ID, Convert.ToInt32(CommonSetting.Commonid.EducationtypeLLB));
            ViewBag.check_EnrollmentFeeSubmit_open = datecom.isopendate;
            ViewBag.check_EnrollmentFeeSubmit_Close = datecom.isclosedate;
            ViewBag.enrollStartdateValue = datecom.opendate;
            ViewBag.ViewBagenrollExtenddateValue = datecom.closedate;
            //if (ViewBag.check_EnrollmentFeeSubmit_open == true && ViewBag.check_EnrollmentFeeSubmit_Close == true)
            //{


            //}
            //else
            //{
            //    return RedirectToAction("Index", "HomeL");
            //}
            var obj1 = ob.GetAppLicationDataForEnrollmentFee();
            
            if (obj1.objExamFrom == null)
            {

                return RedirectToAction("Index", "HomeL");
            }
            if (obj1.objExamFrom.IsAdmissionFee == false)
            {

                return RedirectToAction("Index", "HomeL");
            }
            //if (obj1.objfeesubmit.sessionid != 40)
            //{

            //    return RedirectToAction("Index", "HomeL");
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
            datecom = com.check_EnrollmentSubmit_check(ad.GetAcademiccurrentSession().ID, Convert.ToInt32(CommonSetting.Commonid.EducationtypeLLB));
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
            if (obj1.objExamFrom.migrationcertificate_iseligible != 1)
            {
                ob.Status = false;
                TempData["msgfees"] = "Your migration document not verify !! !!!";
                return RedirectToAction("EnrollmentFeesSubmit");
            }

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
                            string s3FileName = System.DateTime.Now.ToString("dd_MM_yyyy_HH_mm_ss") + "_migrationcertificate_" + objl.FirstName + @name;

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
                datecom = com.check_EnrollmentSubmit_check(ad.GetAcademiccurrentSession().ID, Convert.ToInt32(CommonSetting.Commonid.EducationtypeLLB));
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
                if (result != null)
                {
                    Amount1 = Convert.ToDecimal(result.Fees);
                    // Amount1 = 1;
                    if (result.migrationcertificate_iseligible != 1)
                    {
                        return RedirectToAction("EnrollmentFeesSubmit");
                    }
                    if (Amount1 <= 0)
                    {
                        return RedirectToAction("EnrollmentFeesSubmit");
                    }
                    var obj = sbi.encriptData(Amount1, EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENBApplicationNo")), "studentLLB/ExamLLb/PGSucessEnrollment", "studentLLB/ExamLLb/PGFailedEnrollment");

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
            //return RedirectToAction("Index", "HomeL");

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
            //return RedirectToAction("Index", "HomeL");
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
            return RedirectToAction("ResponseEnrollment");
            return View();
        }

        public ActionResult PGFailedEnrollment()
        {
            //return RedirectToAction("Index", "HomeL");
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
            return RedirectToAction("ResponseEnrollment");
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

        public ActionResult BackExamFeesSubmit_new()
        {
            ExamForm ob = new ExamForm();
            AcademicSession ad = new AcademicSession();
            BL_PrintApplication PritApp = new BL_PrintApplication();
            //// manaul change  course year value from  fetch tbl_CourseYear according to
            int courseyearid = 31;// couryearid 30 for second year; , kon se semester ke lia open krna h , manaul set semesterid
            Commn_master com = new Commn_master();
            ViewBag.check_ExamFeeSubmit_open = com.Backcheck_ExamFeeSubmit_open(ad.GetAcademiccurrentSession().ID, Convert.ToInt32(CommonSetting.Commonid.EducationtypeLLB));
            ViewBag.check_ExamFeeSubmit_Close = com.Backcheck_ExamFeeSubmit_close(ad.GetAcademiccurrentSession().ID, Convert.ToInt32(CommonSetting.Commonid.EducationtypeLLB));
            ViewBag.check_admissionopen = ViewBag.check_ExamFeeSubmit_open;

            var obj1 = ob.backGetAppLicationDataForExamFee_LLB(Convert.ToInt32(EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENNBStID"))), courseyearid, System.DateTime.Now.Year);

            if (obj1.objExamFrom == null)
            {
                return RedirectToAction("Index", "HomeL");
            }
            if (obj1.objExamFrom.isfeesubmitregistration == 0)
            {
                return RedirectToAction("Index", "HomeL");
            }
            if (obj1.objExamFrom.RegistrationNo == null || obj1.objExamFrom.RegistrationNo == "")
            {
                return RedirectToAction("Index", "HomeL");
            }
            if (obj1.objExamFrom.RollNo == null || obj1.objExamFrom.RollNo == "")
            {
                return RedirectToAction("Index", "HomeL");
            }

            // only for session student check

            if (obj1.objExamFrom.sessionid != 39)
            {
                return RedirectToAction("Index", "HomeL");
            }
            //if (obj1.objExamFrom.Currentyear_courseyarid == 2 || obj1.objExamFrom.Currentyear_courseyarid == 5 || obj1.objExamFrom.Currentyear_courseyarid == 8)
            //{
            //    // allow only for 2nd year student which have back paper
            //    // first check exam fee payment before admission fee submit or not 
            //    //int a = ob.check_examfeebefore_admissionfee(obj1.objExamFrom.coursecategoryid, obj1.objExamFrom.StreamCategoryID, obj1.objExamFrom.Currentyear_courseyarid, obj1.objExamFrom.sessionid, obj1.objExamFrom.collegeid, obj1.objExamFrom.sid);
            //    //if (a <= 0)
            //    //{
            //    //    return RedirectToAction("Index", "HomeL");
            //    //}
            //}
            //else
            //{ return RedirectToAction("Index", "HomeL"); }
            StudentAdmissionQualification obj = new StudentAdmissionQualification();
            QualifiationMasterList sub = new QualifiationMasterList();
            List<EaxmFeesSubmit> feestruckture = new List<EaxmFeesSubmit>();
            List<EaxmFeesSubmit> subjectlist = new List<EaxmFeesSubmit>();
            List<EaxmFeesSubmit> Electivesubjectlist = new List<EaxmFeesSubmit>();
            feestruckture = ob.backFeesDetailsstructure(obj1.objExamFrom.collegeid, obj1.objExamFrom.coursecategoryid, obj1.objExamFrom.sessionid, obj1.objExamFrom.castecategoryid, obj1.objExamFrom.StreamCategoryID, obj1.objExamFrom.courseyearid, 0);
            subjectlist = ob.backFeesDetailSubjectlist_LLB(obj1.objExamFrom.coursecategoryid, obj1.objExamFrom.StreamCategoryID, courseyearid, obj1.objExamFrom.sessionid, obj1.objExamFrom.collegeid, obj1.objExamFrom.sid);
            if (subjectlist.Count == 0)
            {
                // if subject count greater than 0 then is counted as back student else it not consider as back student
                return RedirectToAction("Index", "HomeL");
            }
            var tuple = new Tuple<PrintExamForm, QualifiationMasterList, List<EaxmFeesSubmit>, List<EaxmFeesSubmit>>(obj1, sub, feestruckture, subjectlist);
            return View(tuple);
        }
        public ActionResult BackExamFeesSubmit(string courseyearidenc = "")
        {
            //new start
            //return RedirectToAction("Index", "Homev");
            ExamForm ob = new ExamForm();
            AcademicSession ad = new AcademicSession();
            BL_PrintApplication PritApp = new BL_PrintApplication();
            // manaul change courseyear id       10;
            int courseyearid = 0;
            //if (courseyearidenc == null || courseyearidenc == "")
            //{
            //    return RedirectToAction("Index", "Homev");
            //}
            courseyearid = Convert.ToInt32(EncriptDecript.Decrypt(courseyearidenc));// courseyearid = 11;// couryearid 11 for 2 semester bca
            var obj1 = ob.backGetAppLicationDataForExamFee_LLB(Convert.ToInt32(EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENNBStID"))), courseyearid, System.DateTime.Now.Year);
            Commn_master com = new Commn_master();
            Commn_master datecom = new Commn_master();
            datecom = com.check_BAckExamFeeSubmit_check(ad.GetAcademiccurrentSession().ID, Convert.ToInt32(CommonSetting.Commonid.EducationtypeVoc));
            ViewBag.check_ExamFeeSubmit_open = datecom.isopendate;
            ViewBag.check_ExamFeeSubmit_Close = true;
            ViewBag.check_admissionopen = ViewBag.check_ExamFeeSubmit_open;
            if (obj1.objExamFrom == null)
            {
                return RedirectToAction("Index", "HomeL");
            }
            if (obj1.objExamFrom.isfeesubmitregistration == 0)
            {
                return RedirectToAction("Index", "HomeL");
            }
            if (obj1.objExamFrom.RegistrationNo == null || obj1.objExamFrom.RegistrationNo == "")
            {
                return RedirectToAction("Index", "HomeL");
            }

            // only for session student check
            //if (obj1.objExamFrom.sessionid != 39)
            //{
            //    return RedirectToAction("Index", "Homev");
            //}
            StudentAdmissionQualification obj = new StudentAdmissionQualification();
            QualifiationMasterList sub = new QualifiationMasterList();
            List<EaxmFeesSubmit> feestruckture = new List<EaxmFeesSubmit>();
            List<EaxmFeesSubmit> subjectlist = new List<EaxmFeesSubmit>();
            List<EaxmFeesSubmit> Electivesubjectlist = new List<EaxmFeesSubmit>();
            feestruckture = ob.backFeesDetailsstructure(obj1.objExamFrom.collegeid, obj1.objExamFrom.coursecategoryid, obj1.objExamFrom.sessionid, obj1.objExamFrom.castecategoryid, obj1.objExamFrom.StreamCategoryID, obj1.objExamFrom.courseyearid, 0);

            subjectlist = ob.backFeesDetailSubjectlist_LLB(obj1.objExamFrom.coursecategoryid, obj1.objExamFrom.StreamCategoryID, obj1.objExamFrom.courseyearid, obj1.objExamFrom.sessionid, obj1.objExamFrom.collegeid, obj1.objExamFrom.sid);
            if (subjectlist.Count == 0)
            {
                return RedirectToAction("Index", "HomeL");
            }
            var tuple = new Tuple<PrintExamForm, QualifiationMasterList, List<EaxmFeesSubmit>, List<EaxmFeesSubmit>>(obj1, sub, feestruckture, subjectlist);
            return View(tuple);
            // New End 




            //ExamForm ob = new ExamForm();
            //AcademicSession ad = new AcademicSession();
            //BL_PrintApplication PritApp = new BL_PrintApplication();
            ////// manaul change  course year value from  fetch tbl_CourseYear according to
            //int courseyearid = 31;// couryearid 30 for second year; , kon se semester ke lia open krna h , manaul set semesterid
            //Commn_master com = new Commn_master();
            //ViewBag.check_ExamFeeSubmit_open = com.Backcheck_ExamFeeSubmit_open(ad.GetAcademiccurrentSession().ID, Convert.ToInt32(CommonSetting.Commonid.EducationtypeLLB));
            //ViewBag.check_ExamFeeSubmit_Close = com.Backcheck_ExamFeeSubmit_close(ad.GetAcademiccurrentSession().ID, Convert.ToInt32(CommonSetting.Commonid.EducationtypeLLB));
            //ViewBag.check_admissionopen = ViewBag.check_ExamFeeSubmit_open;
            
            //var obj1 = ob.backGetAppLicationDataForExamFee_LLB(Convert.ToInt32(EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENNBStID"))), courseyearid, System.DateTime.Now.Year);

            //if (obj1.objExamFrom == null)
            //{
            //    return RedirectToAction("Index", "HomeL");
            //}
            //if (obj1.objExamFrom.isfeesubmitregistration == 0)
            //{
            //    return RedirectToAction("Index", "HomeL");
            //}
            //if (obj1.objExamFrom.RegistrationNo == null || obj1.objExamFrom.RegistrationNo == "")
            //{
            //    return RedirectToAction("Index", "HomeL");
            //}
            //if (obj1.objExamFrom.RollNo == null || obj1.objExamFrom.RollNo == "")
            //{
            //    return RedirectToAction("Index", "HomeL");
            //}

            //// only for session student check

            ////if (obj1.objExamFrom.sessionid != 39)
            ////{
            ////    return RedirectToAction("Index", "HomeL");
            ////}
            ////if (obj1.objExamFrom.Currentyear_courseyarid == 2 || obj1.objExamFrom.Currentyear_courseyarid == 5 || obj1.objExamFrom.Currentyear_courseyarid == 8)
            ////{
            ////    // allow only for 2nd year student which have back paper
            ////    // first check exam fee payment before admission fee submit or not 
            ////    //int a = ob.check_examfeebefore_admissionfee(obj1.objExamFrom.coursecategoryid, obj1.objExamFrom.StreamCategoryID, obj1.objExamFrom.Currentyear_courseyarid, obj1.objExamFrom.sessionid, obj1.objExamFrom.collegeid, obj1.objExamFrom.sid);
            ////    //if (a <= 0)
            ////    //{
            ////    //    return RedirectToAction("Index", "HomeL");
            ////    //}
            ////}
            ////else
            ////{ return RedirectToAction("Index", "HomeL"); }
            //StudentAdmissionQualification obj = new StudentAdmissionQualification();
            //QualifiationMasterList sub = new QualifiationMasterList();
            //List<EaxmFeesSubmit> feestruckture = new List<EaxmFeesSubmit>();
            //List<EaxmFeesSubmit> subjectlist = new List<EaxmFeesSubmit>();
            //List<EaxmFeesSubmit> Electivesubjectlist = new List<EaxmFeesSubmit>();
            //feestruckture = ob.backFeesDetailsstructure(obj1.objExamFrom.collegeid, obj1.objExamFrom.coursecategoryid, obj1.objExamFrom.sessionid, obj1.objExamFrom.castecategoryid, obj1.objExamFrom.StreamCategoryID, obj1.objExamFrom.courseyearid, 0);
            //subjectlist = ob.backFeesDetailSubjectlist_LLB(obj1.objExamFrom.coursecategoryid, obj1.objExamFrom.StreamCategoryID, courseyearid, obj1.objExamFrom.sessionid, obj1.objExamFrom.collegeid, obj1.objExamFrom.sid);
            //if (subjectlist.Count == 0)
            //{
            //    // if subject count greater than 0 then is counted as back student else it not consider as back student
            //    return RedirectToAction("Index", "HomeL");
            //}
            //var tuple = new Tuple<PrintExamForm, QualifiationMasterList, List<EaxmFeesSubmit>, List<EaxmFeesSubmit>>(obj1, sub, feestruckture, subjectlist);
            //return View(tuple);
        }
        //[HttpPost]
        //public ActionResult BackExamFeesSubmit(int id = 0, string applyadmissionform = "", string Substreamcategoryid = "", string BackandEdit = "", string checkboxid = "",string isappeared="")
        //{
        //    StudentLogin stu = new StudentLogin();
        //    string ApplicationID = ClsLanguage.GetCookies("NBApplicationNo");
        //    Login lo = stu.BasicDetail(ApplicationID);
        //    ExamForm objexamfrom = new ExamForm();
        //    Login result = new Login();
        //    ExamForm ob = new ExamForm();
        //    AcademicSession ad = new AcademicSession();
        //    BL_PrintApplication PritApp = new BL_PrintApplication();
        //    // manaul change  course year value from  fetch tbl_CourseYear according to couryearid 30 for 1st year
        //    int courseyearid = 32;//// couryearid 30 for first year; , kon se semester ke lia open krna h , manaul set semesterid

        //    var obj1 = ob.backGetAppLicationDataForExamFee_LLB(Convert.ToInt32(EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENNBStID"))), courseyearid, System.DateTime.Now.Year);
        //    Commn_master com = new Commn_master();
        //    ViewBag.check_ExamFeeSubmit_open = com.Backcheck_ExamFeeSubmit_open(ad.GetAcademiccurrentSession().ID, Convert.ToInt32(CommonSetting.Commonid.EducationtypeLLB));
        //    ViewBag.check_ExamFeeSubmit_Close = com.Backcheck_ExamFeeSubmit_close(ad.GetAcademiccurrentSession().ID, Convert.ToInt32(CommonSetting.Commonid.EducationtypeLLB));
        //    ViewBag.check_admissionopen = ViewBag.check_ExamFeeSubmit_open;
        //    if (ViewBag.check_ExamFeeSubmit_open == true && ViewBag.check_ExamFeeSubmit_Close == true)
        //    {

        //    }
        //    else
        //    {
        //        return RedirectToAction("Index", "HomeL");
        //    }
        //    if (obj1.objExamFrom == null)
        //    {
        //        return RedirectToAction("Index", "HomeL");
        //    }
        //    if (obj1.objExamFrom.isfeesubmitregistration == 0)
        //    {
        //        return RedirectToAction("Index", "HomeL");
        //    }

        //    // only for session student check

        //    if (obj1.objExamFrom.sessionid != 39)
        //    {
        //        return RedirectToAction("Index", "HomeL");
        //    }
        //    if (applyadmissionform == "applyadmissionform")
        //    {
        //        //if (Substreamcategoryid == "")
        //        //{
        //        //    return RedirectToAction("ExamFeesSubmit");
        //        //}
        //        if (checkboxid == "")
        //        {
        //            return RedirectToAction("BackExamFeesSubmit_new");
        //        }
        //        objexamfrom.student_examform_apply_back(obj1.objExamFrom.sid, obj1.objExamFrom.sessionid, obj1.objExamFrom.coursecategoryid, obj1.objExamFrom.collegeid, obj1.objExamFrom.StreamCategoryID, obj1.objExamFrom.courseyearid, Convert.ToInt32(isappeared), Convert.ToInt32(0), "LLB", "", System.DateTime.Now.Year);
        //        return RedirectToAction("BackExamFeesSubmit_new");
        //        Session["courseyearid"] = "";
        //    }
        //    if (obj1.objExamFrom.IsDocVerify == 1)
        //    {

        //    }
        //    else
        //    {
        //        return RedirectToAction("BackExamFeesSubmit_new");
        //    }

        //    StudentAdmissionQualification obj = new StudentAdmissionQualification();
        //    QualifiationMasterList sub = new QualifiationMasterList();
        //    List<EaxmFeesSubmit> feestruckture = new List<EaxmFeesSubmit>();
        //    List<EaxmFeesSubmit> subjectlist = new List<EaxmFeesSubmit>();
        //    List<EaxmFeesSubmit> Electivesubjectlist = new List<EaxmFeesSubmit>();
        //    feestruckture = ob.backFeesDetailsstructure(obj1.objExamFrom.collegeid, obj1.objExamFrom.coursecategoryid, obj1.objExamFrom.sessionid, obj1.objExamFrom.castecategoryid, obj1.objExamFrom.StreamCategoryID, obj1.objExamFrom.courseyearid, 0);
        //    subjectlist = ob.backFeesDetailSubjectlist_LLB(obj1.objExamFrom.coursecategoryid, obj1.objExamFrom.StreamCategoryID, courseyearid, obj1.objExamFrom.sessionid, obj1.objExamFrom.collegeid, obj1.objExamFrom.sid);
        //    if (subjectlist.Count == 0)
        //    {
        //        return RedirectToAction("Index", "HomeL");
        //    }
        //    if (obj1.objExamFrom.IsExamfeesubmit == 0)
        //    {
        //        // TempData["msgfees"] = "Please wait till exam fee open date !!!";
        //        //return RedirectToAction("ExamFeesSubmit");
        //        return RedirectToAction("backPGGatewayExam");
        //    }
        //    else
        //    {
        //        FeesSubmit stlogin1 = new FeesSubmit();
        //        stlogin1.Status = false;
        //        TempData["msgfees"] = "Exam Fees Already Submitted !!!";
        //        return RedirectToAction("BackExamFeesSubmit_new");
        //    }
        //    var tuple = new Tuple<PrintExamForm, QualifiationMasterList, List<EaxmFeesSubmit>, List<EaxmFeesSubmit>>(obj1, sub, feestruckture, subjectlist);
        //    return View(tuple);
        //}


        [HttpPost]
        public ActionResult BackExamFeesSubmit(int id = 0, string applyadmissionform = "", string Substreamcategoryid = "", string BackandEdit = "", string checkboxid = "", string courseyearidenc = "")
        {
            StudentLogin stu = new StudentLogin();
            string ApplicationID = ClsLanguage.GetCookies("NBApplicationNo");
            Login lo = stu.BasicDetail(ApplicationID);
            ExamForm objexamfrom = new ExamForm();
            Login result = new Login();
            ExamForm ob = new ExamForm();
            AcademicSession ad = new AcademicSession();
            BL_PrintApplication PritApp = new BL_PrintApplication();
            StudentAdmissionQualification obj = new StudentAdmissionQualification();
            QualifiationMasterList sub = new QualifiationMasterList();
            List<EaxmFeesSubmit> feestruckture = new List<EaxmFeesSubmit>();
            List<EaxmFeesSubmit> subjectlist = new List<EaxmFeesSubmit>();
            List<EaxmFeesSubmit> Electivesubjectlist = new List<EaxmFeesSubmit>();
            PrintExamForm obj1 = new PrintExamForm();
            // manaul change courseyear id
            int courseyearid = 0;
            try
            {
                //CommonMethod.WritetoNotepad(null, HttpContext.Request.Url.AbsolutePath, " bca exam 2021 6" + courseyearidenc, ClsLanguage.GetCookies("NBApplicationNo"));


                courseyearid = Convert.ToInt32(EncriptDecript.Decrypt(courseyearidenc));// couryearid 11 for 2 semester bca

                obj1 = ob.backGetAppLicationDataForExamFee(Convert.ToInt32(EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENNBStID"))), courseyearid, System.DateTime.Now.Year);
                Commn_master com = new Commn_master();
                //CommonMethod.WritetoNotepad(null, HttpContext.Request.Url.AbsolutePath, " bca exam 2021 5" + courseyearidenc, ClsLanguage.GetCookies("NBApplicationNo"));


                //ViewBag.check_ExamFeeSubmit_open = com.Backcheck_ExamFeeSubmit_open(ad.GetAcademiccurrentSession().ID, Convert.ToInt32(CommonSetting.Commonid.EducationtypeVoc));
                //ViewBag.check_ExamFeeSubmit_Close = com.Backcheck_ExamFeeSubmit_close(ad.GetAcademiccurrentSession().ID, Convert.ToInt32(CommonSetting.Commonid.EducationtypeVoc));
                // ViewBag.check_admissionopen = ViewBag.check_ExamFeeSubmit_open;
                Commn_master datecom = new Commn_master();
                datecom = com.check_BAckExamFeeSubmit_check(ad.GetAcademiccurrentSession().ID, Convert.ToInt32(CommonSetting.Commonid.EducationtypeVoc));
                ViewBag.check_ExamFeeSubmit_open = datecom.isopendate;
                ViewBag.check_ExamFeeSubmit_Close = true;
                ViewBag.check_admissionopen = ViewBag.check_ExamFeeSubmit_open;

                //if (ViewBag.check_ExamFeeSubmit_open == true && ViewBag.check_ExamFeeSubmit_Close == true)
                //{

                //}
                //else
                //{
                //    return RedirectToAction("Index", "HomePG");
                //}
                if (obj1.objExamFrom == null)
                {
                    return RedirectToAction("Index", "HomeL");
                }
                if (obj1.objExamFrom.isfeesubmitregistration == 0)
                {
                    return RedirectToAction("Index", "HomeL");
                }

                // only for session student check

                //if (obj1.objExamFrom.sessionid != 39)
                //{
                //    return RedirectToAction("Index", "Homev");
                //}
                //  CommonMethod.WritetoNotepad(null, HttpContext.Request.Url.AbsolutePath, " bca exam 2021 2" + courseyearidenc, ClsLanguage.GetCookies("NBApplicationNo"));

                if (applyadmissionform == "applyadmissionform")
                {
                    //if (Substreamcategoryid == "")
                    //{
                    //    return RedirectToAction("ExamFeesSubmit");
                    //}
                    if (checkboxid == "")
                    {
                        return RedirectToAction("BackExamFeesSubmit", new { @courseyearidenc = courseyearidenc });
                    }
                    objexamfrom.student_examform_apply_back(obj1.objExamFrom.sid, obj1.objExamFrom.sessionid, obj1.objExamFrom.coursecategoryid, obj1.objExamFrom.collegeid, obj1.objExamFrom.StreamCategoryID, obj1.objExamFrom.courseyearid, Convert.ToInt32(0), Convert.ToInt32(0), "BCA", "", System.DateTime.Now.Year);
                    return RedirectToAction("BackExamFeesSubmit", new { @courseyearidenc = courseyearidenc });
                }
                if (obj1.objExamFrom.IsDocVerify == 1)
                {

                }
                else
                {
                    return RedirectToAction("BackExamFeesSubmit", new { @courseyearidenc = courseyearidenc });
                }



                feestruckture = ob.backFeesDetailsstructure(obj1.objExamFrom.collegeid, obj1.objExamFrom.coursecategoryid, obj1.objExamFrom.sessionid, obj1.objExamFrom.castecategoryid, obj1.objExamFrom.StreamCategoryID, obj1.objExamFrom.courseyearid, 0);
                subjectlist = ob.backFeesDetailSubjectlist_LLB(obj1.objExamFrom.coursecategoryid, obj1.objExamFrom.StreamCategoryID, obj1.objExamFrom.courseyearid, obj1.objExamFrom.sessionid, obj1.objExamFrom.collegeid, obj1.objExamFrom.sid);
                if (subjectlist.Count == 0)
                {
                    return RedirectToAction("Index", "HomeL");
                }
                // CommonMethod.WritetoNotepad(null, HttpContext.Request.Url.AbsolutePath, " bca exam 2021 3" + courseyearidenc, ClsLanguage.GetCookies("NBApplicationNo"));

                if (obj1.objExamFrom.IsExamfeesubmit == 0)
                {
                    // TempData["msgfees"] = "Please wait till exam fee open date !!!";
                    //return RedirectToAction("ExamFeesSubmit");
                    return RedirectToAction("backPGGatewayExam", new { @courseyearidenc = courseyearidenc });
                    //return RedirectToAction("backPGGatewayExam");
                }
                else
                {
                    FeesSubmit stlogin1 = new FeesSubmit();
                    stlogin1.Status = false;
                    TempData["msgfees"] = "Exam Fees Already Submitted !!!";
                    return RedirectToAction("BackExamFeesSubmit", new { @courseyearidenc = courseyearidenc });
                }
            }
            catch (Exception ex)
            {
                CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, " LLB exam 2021" + courseyearidenc, ClsLanguage.GetCookies("NBApplicationNo"));


                return RedirectToAction("BackExamFeesSubmit", new { @courseyearidenc = courseyearidenc });
            }
            var tuple = new Tuple<PrintExamForm, QualifiationMasterList, List<EaxmFeesSubmit>, List<EaxmFeesSubmit>>(obj1, sub, feestruckture, subjectlist);
            return View(tuple);
        }


        public ActionResult BackPGGatewayExam(string courseyearidenc = "")
        {
            //  return RedirectToAction("Index", "HomePG");
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
                // manual add courseyearid
                int courseyearid = 0;

                courseyearid = Convert.ToInt32(EncriptDecript.Decrypt(courseyearidenc));//// couryearid 16 for first semester ma; , kon se semester ke lia open krna h , manaul set semesterid

                var obj1 = ob.backGetAppLicationDataForExamFee(Convert.ToInt32(EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENNBStID"))), courseyearid, System.DateTime.Now.Year);
                feestruckture = ob.backFeesDetailsstructure(obj1.objExamFrom.collegeid, obj1.objExamFrom.coursecategoryid, obj1.objExamFrom.sessionid, obj1.objExamFrom.castecategoryid, obj1.objExamFrom.StreamCategoryID, obj1.objExamFrom.courseyearid, 0);
                subjectlist = ob.backFeesDetailSubjectlist_LLB(obj1.objExamFrom.coursecategoryid, obj1.objExamFrom.StreamCategoryID, obj1.objExamFrom.courseyearid, obj1.objExamFrom.sessionid, obj1.objExamFrom.collegeid, obj1.objExamFrom.sid);
                if (subjectlist.Count == 0)
                {
                    return RedirectToAction("Index", "HomeL");
                }
                if (obj1.objExamFrom != null)
                {
                    AcademicSession ac = new AcademicSession();
                    int sessionid = ac.GetAcademiccurrentSession().ID;
                    int educationtype = Convert.ToInt32(CommonSetting.Commonid.EducationtypeVoc);
                    Commn_master datecom = new Commn_master();
                    datecom = com.check_BAckExamFeeSubmit_check(ad.GetAcademiccurrentSession().ID, Convert.ToInt32(CommonSetting.Commonid.EducationtypeVoc));
                    ViewBag.check_ExamFeeSubmit_open = datecom.isopendate;
                    ViewBag.check_ExamFeeSubmit_Close = datecom.isclosedate;
                    ViewBag.check_admissionopen = ViewBag.check_ExamFeeSubmit_open;
                    if (obj1.objExamFrom.IsDocVerify == 1)
                    {

                    }
                    else
                    {
                        return RedirectToAction("Index", "HomeL");
                    }
                    if (obj1.objExamFrom.IsExamfeesubmit == 1) { return RedirectToAction("BackExamFeesSubmit", new { @courseyearidenc = courseyearidenc }); }
                    Amount1 = Convert.ToDecimal(feestruckture.Sum(x => x.amount));
                    latefee = Convert.ToDecimal(feestruckture.Sum(x => x.late_fee));
                    amount_without_latefee = Amount1;
                    Amount1 = Amount1 + latefee;
                    if (Amount1 <= 0)
                    {
                        TempData["msgerror"] = "Amount Should be greater than 0 !!";
                        return RedirectToAction("BackExamFeesSubmit", new { @courseyearidenc = courseyearidenc });
                    }
                    //Amount1 = 1;
                    var obj = sbi.encriptDataExambackyear(Amount1, EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENBApplicationNo")), "studentL/Examv/backPGSucessExam", "studentL/Examv/backPGFailedExam", obj1.objExamFrom.YearName, latefee, amount_without_latefee, obj1.objExamFrom.courseyearid);
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
            // return RedirectToAction("Index", "HomePG");

            try
            {
                ExamForm ob = new ExamForm();
                AcademicSession ad = new AcademicSession();
                // List<EaxmFeesSubmit> subjectlist = new List<EaxmFeesSubmit>();
                // manual add courseyearid 11
                int courseyearid = 0;
                courseyearid = Convert.ToInt32(EncriptDecript.Decrypt(courseyearidenc)); ;
                var obj1 = ob.backGetAppLicationDataForExamFee(Convert.ToInt32(EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENNBStID"))), courseyearid, System.DateTime.Now.Year);
                return View(obj1);

            }
            catch (Exception ex)
            {
                CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "Response payment gateway get method for exam Fee on Controller error", ClsLanguage.GetCookies("NBApplicationNo"));
                PrintExamForm PritApp = new PrintExamForm();
                return View(PritApp);
            }
        }
        //public ActionResult BackPGGatewayExam()
        //{
        //    //  return RedirectToAction("Index", "Home");
        //    try
        //    {
        //        SbiPayExam sbi = new SbiPayExam();
        //        ExamForm ob = new ExamForm();
        //        AcademicSession ad = new AcademicSession();
        //        Commn_master com = new Commn_master();
        //        List<EaxmFeesSubmit> feestruckture = new List<EaxmFeesSubmit>();
        //        List<EaxmFeesSubmit> subjectlist = new List<EaxmFeesSubmit>();
        //        decimal Amount1 = 1;
        //        decimal latefee = 0;
        //        decimal amount_without_latefee = 0;
        //        string MerchantCustomerID1 = "1";
        //        // manaul change  course year value from  fetch tbl_CourseYear according to couryearid
        //        int courseyearid = 31;////  30 for first year; , kon se semester ke lia open krna h , manaul set semesterid

        //        var obj1 = ob.backGetAppLicationDataForExamFee_LLB(Convert.ToInt32(EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENNBStID"))), courseyearid, System.DateTime.Now.Year);
        //        feestruckture = ob.backFeesDetailsstructure(obj1.objExamFrom.collegeid, obj1.objExamFrom.coursecategoryid, obj1.objExamFrom.sessionid, obj1.objExamFrom.castecategoryid, obj1.objExamFrom.StreamCategoryID, obj1.objExamFrom.courseyearid, 0);
        //        // manaul change  course year value from  fetch tbl_CourseYear according to couryearid
        //        subjectlist = ob.backFeesDetailSubjectlist_LLB(obj1.objExamFrom.coursecategoryid, obj1.objExamFrom.StreamCategoryID, courseyearid, obj1.objExamFrom.sessionid, obj1.objExamFrom.collegeid, obj1.objExamFrom.sid);

        //         if (subjectlist.Count == 0)
        //        {
        //            return RedirectToAction("Index", "HomeL");
        //        }
        //        if (obj1.objExamFrom != null)
        //        {
        //            AcademicSession ac = new AcademicSession();
        //            int sessionid = ac.GetAcademiccurrentSession().ID;
        //            int educationtype = Convert.ToInt32(CommonSetting.Commonid.EducationtypeLLB);
        //            var datestart = com.Backcheck_ExamFeeSubmit_open(ad.GetAcademiccurrentSession().ID, Convert.ToInt32(CommonSetting.Commonid.EducationtypeLLB));
        //            if (datestart == false)
        //            {
        //                return RedirectToAction("BackExamFeesSubmit");
        //            }
        //            var dateextend = com.Backcheck_ExamFeeSubmit_close(ad.GetAcademiccurrentSession().ID, Convert.ToInt32(CommonSetting.Commonid.EducationtypeLLB));
        //            if (dateextend == false)
        //            {
        //                return RedirectToAction("BackExamFeesSubmit");
        //            }
        //            if (obj1.objExamFrom.IsDocVerify == 1)
        //            {

        //            }
        //            else
        //            {
        //                return RedirectToAction("Index", "HomeL");
        //            }
        //            if (obj1.objExamFrom.IsExamfeesubmit == 1) { return RedirectToAction("BackExamFeesSubmit"); }
        //            Amount1 = Convert.ToDecimal(feestruckture.Sum(x => x.amount));
        //            latefee = Convert.ToDecimal(feestruckture.Sum(x => x.late_fee));
        //            amount_without_latefee = Amount1;
        //            Amount1 = Amount1 + latefee;
        //            if (Amount1 <= 0)
        //            {
        //                TempData["msgerror"] = "Amount Should be greater than 0 !!";
        //                return RedirectToAction("BackExamFeesSubmit", "ExamLLB");
        //            }
        //            //Amount1 = 1;
        //            var obj = sbi.encriptDataExambackyear(Amount1, EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENBApplicationNo")), "studentLLB/ExamLLB/backPGSucessExam", "studentLLB/ExamLLB/backPGFailedExam", obj1.objExamFrom.YearName, latefee, amount_without_latefee, obj1.objExamFrom.courseyearid);
        //            ViewBag.requestparams = obj.requestparams;
        //            ViewBag.merchantId = obj.merchantId;
        //            ViewBag.EncryptbillingDetails = obj.EncryptbillingDetails;
        //            ViewBag.EncryptshippingDetais = obj.EncryptshippingDetais;
        //            ViewBag.url = obj.url;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "action PGGateway error for Eaxm Fee", ClsLanguage.GetCookies("NBApplicationNo"));

        //        return View();
        //    }
        //    return View();
        //}
        //public ActionResult BackResponseExam()
        //{
        //    // return RedirectToAction("Index", "Home");

        //    try
        //    {
        //        // manaul change  course year value from  fetch tbl_CourseYear according to couryearid
        //        int courseyearid = 31;////  30 for first year; , kon se semester ke lia open krna h , manaul set semesterid

        //        ExamForm ob = new ExamForm();
        //        AcademicSession ad = new AcademicSession();
        //        // List<EaxmFeesSubmit> subjectlist = new List<EaxmFeesSubmit>();
        //        // manaul change  course year value from  fetch tbl_CourseYear according to couryearid

        //        var obj1 = ob.backGetAppLicationDataForExamFee_LLB(Convert.ToInt32(EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENNBStID"))), courseyearid, System.DateTime.Now.Year);
        //        return View(obj1);

        //    }
        //    catch (Exception ex)
        //    {
        //        CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "Response payment gateway get method for exam Fee on Controller error", ClsLanguage.GetCookies("NBApplicationNo"));
        //        PrintExamForm PritApp = new PrintExamForm();
        //        return View(PritApp);
        //    }
        //}
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
            if (rec.enrollmentno == null || rec.enrollmentno=="")
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
                                <td align=""left"">" + (obj1.Ftitle == 20 ? "Father's Name" : (obj1.Ftitle == 21 ? "  Husband's  Name" : " Father's Name") ) +@": </td>
                                <td align=""left"">"+rec.FatherName + @" </td>
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
                                <td align=""left"">" + rec.Gender+ @" </td>
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
                                <td colspan=""3"" align=""right"" valign=""middle""><img id=""registratrara11"" src="""+ System.Configuration.ConfigurationManager.AppSettings["siteUrl"] + "/images/registrarsign.jpg"+@""" style=""max-height:50px""></td>
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
    }
}