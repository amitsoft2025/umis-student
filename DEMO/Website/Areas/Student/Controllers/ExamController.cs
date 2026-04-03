using DataLayer;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using Website.Areas.Student.Models;
using Website.Models;

namespace Website.Areas.Student.Controllers
{

    //[CookiesExpireFilterAdminAttribute]

    public class ExamController : Controller
    {
        public string easebuzz_action_url = string.Empty;
        public string gen_hash;
        public string txnid = String.Empty;
        public string easebuzz_merchant_key = string.Empty;
        public string salt = string.Empty;
        public string Key = string.Empty;
        public string env = string.Empty;

        Sbiepay dsghd = new Sbiepay();
        Crc32 shds = new Crc32();
        //string jsonstring = JsonConvert.SerializeObject();
        // GET: Student/Exam
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ExamFeesSubmit()
        {

            //return RedirectToAction("ExamFeesSubmit", "Exam");

            ExamForm ob = new ExamForm();
            Commn_master com = new Commn_master();
            Commn_master datecom = new Commn_master();
            StudentLogin stu = new StudentLogin();
            string ApplicationID = ClsLanguage.GetCookies("NBApplicationNo");
            Login lo = stu.BasicDetail(ApplicationID);
            var obj1 = ob.GetAppLicationDataForExamFee(Convert.ToInt32(EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENNBStID"))));


            datecom = com.check_ExamFeeApply(lo.Id,"main", obj1.objExamFrom.Currentyear_courseyarid);

            if (datecom.FormStatus == "1")
            {

                ViewBag.ExamFormApply = "1";
                
            }
            else 
            {
                
            }

            CommonMethod.TraceLogWritetoNotepad("Call ExamfeecSubmit page", "Exam/ExamFeesSubmit5", "Remarks", "Id");
            //return RedirectToAction("Index", "Home");
            
            AcademicSession ad = new AcademicSession();
            BL_PrintApplication PritApp = new BL_PrintApplication();
            CommonMethod.TraceLogWritetoNotepad("obj1.objExamFrom1", "Exam/ExamFeesSubmit6", "obj1.objExamFrom", "Id");
            
  
            datecom = com.check_ExamFeeSubmit_check(ad.GetAcademiccurrentSession().ID, Convert.ToInt32(CommonSetting.Commonid.Educationtype));
            CommonMethod.TraceLogWritetoNotepad("obj1.objExamFrom1", "Exam/ExamFeesSubmit7", "obj1.objExamFrom", "Id");
            ViewBag.check_ExamFeeSubmit_open = datecom.isopendate;
            ViewBag.check_ExamFeeSubmit_Close = datecom.isclosedate;
            ViewBag.check_admissionopen = ViewBag.check_ExamFeeSubmit_open;
            //if (ViewBag.check_ExamFeeSubmit_open == true && ViewBag.check_ExamFeeSubmit_Close == true)
            //{
            //}
            //else
            //{
            //    return RedirectToAction("Index", "Home");
            //}
            CommonMethod.TraceLogWritetoNotepad("obj1.objExamFrom1", "Exam/ExamFeesSubmit8", "obj1.objExamFrom", "Id");
        
            CommonMethod.TraceLogWritetoNotepad("obj1.objExamFrom1", "Exam/ExamFeesSubmit4", "obj1.objExamFrom", "Id");

            //if (obj1.objExamFrom.Currentyear_courseyarid == 3 || obj1.objExamFrom.Currentyear_courseyarid == 6 || obj1.objExamFrom.Currentyear_courseyarid == 9)
            //{

                
            //}
            //else 
            //{
           
            //    return RedirectToAction("Index", "Home");
            //}

            if (obj1.objExamFrom == null)
            {
                return RedirectToAction("Index", "Home");
            }
            if (obj1.objExamFrom.isfeesubmitregistration == 0)
            {
                return RedirectToAction("Index", "Home");
            }
            CommonMethod.TraceLogWritetoNotepad("obj1.objExamFrom2", "Exam/ExamFeesSubmit3", "obj1.objExamFrom", "Id");
            //if (obj1.objExamFrom.RegistrationNo == "")
            //{
            //    return RedirectToAction("Index", "Home");
            //}
            if (obj1.objExamFrom.RollNo == "")
            {
                return RedirectToAction("Index", "Home");
            }
            //if (obj1.objExamFrom.courseyearid != 31)
            //{
            //    return RedirectToAction("Index", "Home");
            //}
            //if (obj1.objExamFrom.sessionid == 40) // only for 2 year student check 
            //{
            //    if (obj1.objExamFrom.Currentyear_courseyarid == 2 || obj1.objExamFrom.Currentyear_courseyarid == 5 || obj1.objExamFrom.Currentyear_courseyarid == 8)
            //    {
            //        // first check exam fee payment before admission fee submit or not 
            //        int a = ob.check_examfeebefore_admissionfee(obj1.objExamFrom.coursecategoryid, obj1.objExamFrom.StreamCategoryID, obj1.objExamFrom.Currentyear_courseyarid, obj1.objExamFrom.sessionid, obj1.objExamFrom.collegeid, obj1.objExamFrom.sid);
            //        if (a <= 0)
            //        {
            //            return RedirectToAction("Index", "Home");
            //        }
            //    }

            //}

            //if (obj1.objExamFrom.sessionid == 41) // only for 2 year student check 
            //{
            //    if (obj1.objExamFrom.Currentyear_courseyarid == 1 || obj1.objExamFrom.Currentyear_courseyarid == 4 || obj1.objExamFrom.Currentyear_courseyarid == 7)
            //    {
            //        // first check exam fee payment before admission fee submit or not 
                  int a = ob.check_examfeebefore_admissionfee(obj1.objExamFrom.coursecategoryid, obj1.objExamFrom.StreamCategoryID, obj1.objExamFrom.Currentyear_courseyarid, obj1.objExamFrom.sessionid, obj1.objExamFrom.collegeid, obj1.objExamFrom.sid);
                  if (a <= 0)
              {
                      return RedirectToAction("Index", "Home");
                  }
            //    }

            //}
            CommonMethod.TraceLogWritetoNotepad("obj1.objExamFrom4", "Exam/ExamFeesSubmit2", "obj1.objExamFrom", "Id");
            StudentAdmissionQualification obj = new StudentAdmissionQualification();
            QualifiationMasterList sub = new QualifiationMasterList();
            //sub = obj.QualificationdetailList(1, 100000);
            //ViewBag.previousqualification = sub;
            List<EaxmFeesSubmit> feestruckture = new List<EaxmFeesSubmit>();
            List<EaxmFeesSubmit> subjectlist = new List<EaxmFeesSubmit>();
            List<EaxmFeesSubmit> Electivesubjectlist = new List<EaxmFeesSubmit>();
            feestruckture = ob.FeesDetailsstructure(obj1.objExamFrom.collegeid, obj1.objExamFrom.coursecategoryid, obj1.objExamFrom.sessionid, obj1.objExamFrom.castecategoryid, obj1.objExamFrom.StreamCategoryID, obj1.objExamFrom.courseyearid, 0);
            //subjectlist = ob.FeesDetailSubjectlist(obj1.objExamFrom.coursecategoryid, obj1.objExamFrom.StreamCategoryID, obj1.objExamFrom.courseyearid);
            var tuple = new Tuple<PrintExamForm, QualifiationMasterList, List<EaxmFeesSubmit>, List<EaxmFeesSubmit>>(obj1, sub, feestruckture, subjectlist);
            CommonMethod.TraceLogWritetoNotepad("obj1.objExamFrom5", "Exam/ExamFeesSubmit1", "obj1.objExamFrom", "Id");
            return View(tuple);

        }

        [HttpPost]
        public ActionResult ExamFeesSubmit(HttpPostedFileBase fileupload, int id = 0, string applyadmissionform = "", string Substreamcategoryid = "", string BackandEdit = "", string checkboxid = "")
        {
            //return RedirectToAction("Index", "Home");

            StudentLogin stu = new StudentLogin();
            string ApplicationID = ClsLanguage.GetCookies("NBApplicationNo");
            Login lo = stu.BasicDetail(ApplicationID);
            ExamForm objexamfrom = new ExamForm();
            Login result = new Login();
            ExamForm ob = new ExamForm();
            AcademicSession ad = new AcademicSession();
            BL_PrintApplication PritApp = new BL_PrintApplication();

            Commn_master datecom = new Commn_master();
            Commn_master com = new Commn_master();

            var obj1 = ob.GetAppLicationDataForExamFee(Convert.ToInt32(EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENNBStID"))));

            datecom = com.check_ExamFeeApply(lo.Id, "main", obj1.objExamFrom.Currentyear_courseyarid);

            if (datecom.FormStatus == "1")
            {

                ViewBag.ExamFormApply = "1";

            }


            else
            {
                objexamfrom.student_examform_apply(obj1.objExamFrom.sid, obj1.objExamFrom.sessionid, obj1.objExamFrom.coursecategoryid, obj1.objExamFrom.collegeid, obj1.objExamFrom.StreamCategoryID, obj1.objExamFrom.courseyearid, Convert.ToInt32(0), Convert.ToInt32(0), "UG", "");
                return RedirectToAction("Index", "Home");
            }




            if (obj1.objExamFrom.courseyearid == 1 || obj1.objExamFrom.courseyearid == 2 || obj1.objExamFrom.courseyearid == 4 || obj1.objExamFrom.courseyearid == 5 || obj1.objExamFrom.courseyearid == 7 || obj1.objExamFrom.courseyearid == 8)
            {
                
            }

            else 
            {
                return RedirectToAction("ExamFeesSubmit", "Exam");

            }



            //ViewBag.check_ExamFeeSubmit_open = com.check_ExamFeeSubmit_open(ad.GetAcademiccurrentSession().ID, Convert.ToInt32(CommonSetting.Commonid.Educationtype));
            //ViewBag.check_ExamFeeSubmit_Close = com.check_ExamFeeSubmit_close(ad.GetAcademiccurrentSession().ID, Convert.ToInt32(CommonSetting.Commonid.Educationtype));

            datecom = com.check_ExamFeeSubmit_check(ad.GetAcademiccurrentSession().ID, Convert.ToInt32(CommonSetting.Commonid.Educationtype));

            ViewBag.check_ExamFeeSubmit_open = datecom.isopendate;
            ViewBag.check_ExamFeeSubmit_Close = datecom.isclosedate;
            ViewBag.check_admissionopen = ViewBag.check_ExamFeeSubmit_open;
            if (ViewBag.check_ExamFeeSubmit_open == true && ViewBag.check_ExamFeeSubmit_Close == true)
            {
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
            if (obj1.objExamFrom == null)
            {
                return RedirectToAction("Index", "Home");
            }
            if (obj1.objExamFrom.isfeesubmitregistration == 0)
            {
                return RedirectToAction("Index", "Home");
            }
            if (obj1.objExamFrom.RegistrationNo == null || obj1.objExamFrom.RegistrationNo == "")
            {
                TempData["msgfees"] = "Registration no does not esist!!!";
                return RedirectToAction("ExamFeesSubmit");
                return RedirectToAction("Index", "Home");
            }
            if (obj1.objExamFrom.RollNo == null || obj1.objExamFrom.RollNo == "")
            {
                TempData["msgfees"] = "Roll no does not esist!!!";
                return RedirectToAction("ExamFeesSubmit");
                return RedirectToAction("Index", "Home");
            }
            if (obj1.Examobjfeesubmit.sessionid != 40)
            {
                // return RedirectToAction("Index", "Home");
            }
            // for double verification from college
            //if (applyadmissionform == "applyadmissionform")
            //{

            //    string fileuploadname = "";

            //    if (checkboxid == "")
            //    {
            //        return RedirectToAction("ExamFeesSubmit");
            //    }
            //    objexamfrom.student_examform_apply(obj1.objExamFrom.sid, obj1.objExamFrom.sessionid, obj1.objExamFrom.coursecategoryid, obj1.objExamFrom.collegeid, obj1.objExamFrom.StreamCategoryID, obj1.objExamFrom.courseyearid, Convert.ToInt32(0), Convert.ToInt32(0), "UG", fileuploadname);
            //    return RedirectToAction("ExamFeesSubmit");
            //}
            //objexamfrom.student_examform_apply(obj1.objExamFrom.sid, obj1.objExamFrom.sessionid, obj1.objExamFrom.coursecategoryid, obj1.objExamFrom.collegeid, obj1.objExamFrom.StreamCategoryID, obj1.objExamFrom.courseyearid, Convert.ToInt32(0), Convert.ToInt32(0), "UG", "");
            objexamfrom.student_examform_apply(obj1.objExamFrom.sid, obj1.objExamFrom.sessionid, obj1.objExamFrom.coursecategoryid, obj1.objExamFrom.collegeid, obj1.objExamFrom.StreamCategoryID, obj1.objExamFrom.courseyearid, Convert.ToInt32(0), Convert.ToInt32(0), "UG", "");
            if (obj1.objExamFrom.IsDocVerify == 1)
            {

            }
            else
            {

                TempData["msgfees"] = "Exam Form verification Pending !!!";
                return RedirectToAction("ExamFeesSubmit");
            }

            //return RedirectToAction("ExamFeesSubmit");
            StudentAdmissionQualification obj = new StudentAdmissionQualification();
            QualifiationMasterList sub = new QualifiationMasterList();
            //sub = obj.QualificationdetailList(1, 100000);
            //ViewBag.previousqualification = sub;
            List<EaxmFeesSubmit> feestruckture = new List<EaxmFeesSubmit>();
            List<EaxmFeesSubmit> subjectlist = new List<EaxmFeesSubmit>();
            List<EaxmFeesSubmit> Electivesubjectlist = new List<EaxmFeesSubmit>();
            feestruckture = ob.FeesDetailsstructure(obj1.objExamFrom.collegeid, obj1.objExamFrom.coursecategoryid, obj1.objExamFrom.sessionid, obj1.objExamFrom.castecategoryid, obj1.objExamFrom.StreamCategoryID, obj1.objExamFrom.courseyearid, 0);
            // subjectlist = ob.FeesDetailSubjectlist(obj1.objExamFrom.coursecategoryid, obj1.objExamFrom.StreamCategoryID, obj1.objExamFrom.courseyearid);

            if (obj1.objExamFrom.IsExamfeesubmit == 0)
            {
                // TempData["msgfees"] = "Please wait till exam fee open date !!!";
                //return RedirectToAction("ExamFeesSubmit");
                //return RedirectToAction("PGGatewayExam"); Sbi Payment Getway
                return RedirectToAction("SelectPaymentGetway");
                /* return RedirectToAction("AirPayPGGatewayExam");*/  //AirPayPGGatewayExam
            }
            else
            {
                FeesSubmit stlogin1 = new FeesSubmit();
                stlogin1.Status = false;
                TempData["msgfees"] = "Exam Fees Already Submitted !!!";
                return RedirectToAction("ExamFeesSubmit", "Exam");
            }
            var tuple = new Tuple<PrintExamForm, QualifiationMasterList, List<EaxmFeesSubmit>, List<EaxmFeesSubmit>>(obj1, sub, feestruckture, subjectlist);
            return View(tuple);
        }


        public ActionResult ExamFeesSubmitPart3()
        {


            StudentLogin tblST = new StudentLogin();
            string ApplicationID = ClsLanguage.GetCookies("NBApplicationNo");

            var objfailpass = tblST.alldetailcheeck(ApplicationID);


            if (objfailpass.CheckFailOrPass.ToString() == "2")
            {

            }

            else 
            {
                TempData["msgfees"] = "Only Part-1 Part-2 Pass Students Allow for Exam fill!!!";
                return RedirectToAction("Index", "Home");

            }


            // return RedirectToAction("Index", "HomeB");
            ExamForm ob = new ExamForm();
            AcademicSession ad = new AcademicSession();
            BL_PrintApplication PritApp = new BL_PrintApplication();
            var obj1 = ob.GetAppLicationDataForExamFee(Convert.ToInt32(EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENNBStID"))));

            int a = ob.check_examfeebefore_admissionfee(obj1.objExamFrom.coursecategoryid, obj1.objExamFrom.StreamCategoryID, obj1.objExamFrom.Currentyear_courseyarid, obj1.objExamFrom.sessionid, obj1.objExamFrom.collegeid, obj1.objExamFrom.sid);

            if (a <= 0)
            {
                TempData["msgfees"] = "Admission Fee Not Submited!!!";
                return RedirectToAction("Index", "Home");
            }

   
            Commn_master com = new Commn_master();

            Commn_master datecom = new Commn_master();
            datecom = com.check_ExamFeeSubmit_check(ad.GetAcademiccurrentSession().ID, Convert.ToInt32(CommonSetting.Commonid.Educationtype));
            ViewBag.check_ExamFeeSubmit_open = datecom.isopendate;
            ViewBag.check_ExamFeeSubmit_Close = datecom.isclosedate;

            //comment by jc
            ViewBag.check_ExamFeeSubmit_open = true;
            ViewBag.check_ExamFeeSubmit_Close = true;



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
                return RedirectToAction("Index", "Home");
            }
            if (obj1.objExamFrom.isfeesubmitregistration == 0)
            {
                return RedirectToAction("Index", "Home");
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
            Electivesubjectlist = ob.ElectiveFeesDetailSubjectlist_bed_C11(obj1.objExamFrom.coursecategoryid, obj1.objExamFrom.StreamCategoryID, obj1.objExamFrom.courseyearid, Convert.ToInt32(CommonMethod.SubSubjectType.Electivesubject));
            Electivesubjectlist_c7b = ob.ElectiveFeesDetailSubjectlist_UG_c7b(obj1.objExamFrom.coursecategoryid, obj1.objExamFrom.StreamCategoryID, obj1.objExamFrom.courseyearid, Convert.ToInt32(CommonMethod.SubSubjectType.Electivesubject));
            Electivesubjectlist_c7a = ob.ElectiveFeesDetailSubjectlist_UG_c7a(obj1.objExamFrom.coursecategoryid, obj1.objExamFrom.StreamCategoryID, obj1.objExamFrom.courseyearid, Convert.ToInt32(CommonMethod.SubSubjectType.Electivesubject));
            ViewBag.Electivesubjectlist = Electivesubjectlist;


            ViewBag.Electivesubjectlist_c7b = Electivesubjectlist_c7b;
            ViewBag.Electivesubjectlist_c7a = Electivesubjectlist_c7a;

           if( Electivesubjectlist_c7b.Count>0)
            {           
                ViewBag.subject8 = "1";
            }
            else
            {
                ViewBag.subject8 = "0";
            }

            if (Electivesubjectlist_c7a.Count > 0)
            {
                ViewBag.subject7 = "1";
            }
            else 
            {
                ViewBag.subject7 = "0";
            }
                  
            var tuple = new Tuple<PrintExamForm, QualifiationMasterList, List<EaxmFeesSubmit>, List<EaxmFeesSubmit>>(obj1, sub, feestruckture, subjectlist);
            return View(tuple);
        }

        [HttpPost]
        public ActionResult ExamFeesSubmitPart3(int id = 0, string applyadmissionform = "", string isappeared = "", string BackandEdit = "", string checkboxid = "", string Substreamcategoryid = "0", string Substreamcategoryid2 = "0")
        {
            StudentLogin tblST = new StudentLogin();
            string ApplicationID = ClsLanguage.GetCookies("NBApplicationNo");

            var objfailpass = tblST.alldetailcheeck(ApplicationID);


            if (objfailpass.CheckFailOrPass.ToString() == "2")
            {

            }

            else
            {
                TempData["msgfees"] = "Only Part-1 Part-2 Pass Students Allow for Exam fill!!!";
                return RedirectToAction("Index", "Home");

            }


            StudentLogin stu = new StudentLogin();
           
            Login lo = stu.BasicDetail(ApplicationID);
            ExamForm objexamfrom = new ExamForm();
            Login result = new Login();
            ExamForm ob = new ExamForm();
            AcademicSession ad = new AcademicSession();
            BL_PrintApplication PritApp = new BL_PrintApplication();
            var obj1 = ob.GetAppLicationDataForExamFee(Convert.ToInt32(EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENNBStID"))));

            Commn_master com = new Commn_master();
            Commn_master datecom = new Commn_master();
            datecom = com.check_ExamFeeSubmit_check(ad.GetAcademiccurrentSession().ID, Convert.ToInt32(CommonSetting.Commonid.Educationtype));
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
                return RedirectToAction("ExamFeesSubmitPart3");
            }
            if (obj1.objExamFrom == null)
            {
                //return RedirectToAction("Index", "HomeB");
                TempData["msgfees"] = "Registration no does not esist!!!";
                return RedirectToAction("ExamFeesSubmitPart3");
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
                return RedirectToAction("ExamFeesSubmitPart3");

            }
            if (obj1.Examobjfeesubmit.sessionid != 40)
            {
                // return RedirectToAction("Index", "HomeB");
            }
            if (applyadmissionform == "applyadmissionform")
            {
                
                if (obj1.objExamFrom.courseyearid == 29)// only for second year student 
                {
                    if (Substreamcategoryid == "")
                    {
                        TempData["msgfees"] = "Please Select Optional Subject!!!";
                        return RedirectToAction("ExamFeesSubmitPart3");
                    }
                    if (Substreamcategoryid2 == "")
                    {
                        TempData["msgfees"] = "Please C-7 (b) Subject!!!";
                        return RedirectToAction("ExamFeesSubmitPart3");
                    }
                }
                if (obj1.objExamFrom.courseyearid == 28)// only for first year student 
                {
                    if (Substreamcategoryid == "")
                    {
                        TempData["msgfees"] = "Please Select C-7 (a) Subject!!!";
                        return RedirectToAction("ExamFeesSubmitPart3");
                    }
                }
                if (checkboxid == "")
                {
                    return RedirectToAction("ExamFeesSubmitPart3");
                }
                objexamfrom.student_examform_apply(obj1.objExamFrom.sid, obj1.objExamFrom.sessionid, obj1.objExamFrom.coursecategoryid, obj1.objExamFrom.collegeid, obj1.objExamFrom.StreamCategoryID, obj1.objExamFrom.courseyearid, 0, Convert.ToInt32(Substreamcategoryid), "Bed", "", Convert.ToInt32(Substreamcategoryid2));
                return RedirectToAction("ExamFeesSubmitPart3");
            }
            if (obj1.objExamFrom.IsDocVerify == 1)
            {
            }
            else
            {
                TempData["msgfees"] = "Exam Form verification Pending !!!";
                return RedirectToAction("ExamFeesSubmitPart3");
            }



            if (obj1.objExamFrom.isfeesubmitregistration == 0)
            {
                //  return RedirectToAction("Index", "HomeB");
                TempData["msgfees"] = "Application no does not paid!!!";
                return RedirectToAction("ExamFeesSubmitPart3");
            }
            if (obj1.objExamFrom.RegistrationNo == null || obj1.objExamFrom.RegistrationNo == "")
            {
                TempData["msgfees"] = "Registration no does not esist!!!";
                return RedirectToAction("ExamFeesSubmitPart3");
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
                //if (ApplicationID == "MU17762673")
                //{
                   // return RedirectToAction("SelectPaymentGetway");
                //}

                //else { }
                //return RedirectToAction("SelectPaymentGetway");

                // TempData["msgfees"] = "Please wait till exam fee open date !!!";
                //return RedirectToAction("ExamFeesSubmit");

            }
            else
            {
                FeesSubmit stlogin1 = new FeesSubmit();
                stlogin1.Status = false;
                TempData["msgfees"] = "Exam Fees Already Submitted !!!";
                return RedirectToAction("ExamFeesSubmitPart3");
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
                    datecom = com.check_ExamFeeSubmit_check(ad.GetAcademiccurrentSession().ID, Convert.ToInt32(CommonSetting.Commonid.Educationtype));
                    if (datecom.isopendate == false)
                    {
                        return RedirectToAction("ExamFeesSubmit", "Exam");
                    }
                    if (datecom.isclosedate == false)
                    {
                        return RedirectToAction("ExamFeesSubmit", "Exam");
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
                        return RedirectToAction("ExamFeesSubmit", "Exam");
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
                    var obj = sbi.encriptDataExamAirPay(Amount1, EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENBApplicationNo")), "student/Exam/AirPayPGSucessExam", "student/Exam/AirPayPGFailedExam", obj1.objExamFrom.YearName, latefee, amount_without_latefee, obj1.objExamFrom.courseyearid, sEmail, sPhone, sFName, sLName, sAddress, sCity, sState, sCountry, sPincode, sAmount, sOrderId);

                    ViewBag.orderid = obj.Oid;
                    ViewBag.buyerEmail = sEmail;
                    ViewBag.buyerPhone = lo.MobileNo;
                    ViewBag.buyerFirstName = lo.FirstName;
                    ViewBag.buyerLastName = lo.LastName;
                    ViewBag.buyerPinCode = lo.CA_PinCode;
                    ViewBag.amount = obj.Eamount;
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




        public ActionResult EaseBuzzPGGatewayExam()
        {
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
                    datecom = com.check_ExamFeeSubmit_check(ad.GetAcademiccurrentSession().ID, Convert.ToInt32(CommonSetting.Commonid.Educationtype));
                    if (datecom.isopendate == false)
                    {
                        return RedirectToAction("ExamFeesSubmit", "Exam");
                    }
                    if (datecom.isclosedate == false)
                    {
                        return RedirectToAction("ExamFeesSubmit", "Exam");
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

                    /* Amount1 = 10; */ // Dommey amount
                    if (Amount1 <= 0)
                    {
                        TempData["msgerror"] = "Amount Should be greater than 0 !!";
                        return RedirectToAction("ExamFeesSubmit", "Exam");
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
                    string sAmount = Amount1.ToString("0.00"); ;
                    string sOrderId = "";
                    EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENBApplicationNo"));//other Amount  me bhejna h
                    //Amount1 = 1;
                    var obj = sbi.encriptDataExamEaseBuzz(Convert.ToDecimal(sAmount), EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENBApplicationNo")), "student/Exam/EaseBuzzPGSucessExam", "student/Exam/EaseBuzzPGSucessExam", obj1.objExamFrom.YearName, latefee, amount_without_latefee, obj1.objExamFrom.courseyearid, sEmail, sPhone, sFName, sLName, sAddress, sCity, sState, sCountry, sPincode, sAmount, sOrderId);

                    ViewBag.txnid = obj.Etxnid;
                    ViewBag.Key = obj.EKey;
                    ViewBag.amount = obj.Eamount;
                    ViewBag.firstname = obj.Efirstname;
                    ViewBag.email = obj.Eemail;
                    ViewBag.phone = obj.Ephone;
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
                            return RedirectToAction("AirPayResponseExam");

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

       

        public ActionResult EaseBuzzPGSucessExam()
        {
            CommonMethod.TraceLogWritetoNotepad("obj1.objExamFrom4", "Exam/AirPayPGSucessExam1", "obj1.objExamFrom", "Id");
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
                Response.Write("Hash value did not matched");
            }
            else
            {
                order_id = Request.Form["txnid"];

                //Response.Write("value matched");
                if (Request.Form["status"] == "success")
                {
                    Response.Write(Request.Form);

                    var respon = Request.Form;
                }
                else
                {
                    Response.Write(Request.Form);
                }
                //Hash value did not matched
            }

            if (Request.Form.Count > 0)
            {
                try
                {
                    //| udf1 Applicationno | udf2 Exam type | udf3 | udf4 | udf5

                    //CommonMethod.TraceLogWritetoNotepad("obj1.objExamFrom4", "Exam/AirPayPGSucessExam2", "obj1.objExamFrom", "Id");

                    string TRANSACTIONSTATUS = Request.Form["status"];
                    //string TRANSACTIONID = Request.Form["txnid"];
                    string MESSAGE = Request.Form["status"];
                    string TRANSACTIONID = Request.Form["bank_ref_num"];
                    string AMOUNT = Request.Form["amount"];
                    //string CUSTOMVAR = Request.Form["txnid"];
                    string ap_SecureHash = Request.Form["easepayid"];
                    string CHMOD = "Web";
                    ////CommonMethod.TraceLogWritetoNotepad("obj1.objExamFrom4", "Exam/AirPayPGSucessExam3", "obj1.objExamFrom", "Id");
                    //if (TRANSACTIONSTATUS == "" || APTRANSACTIONID == "" || TRANSACTIONID == "" || AMOUNT == "" || ap_SecureHash == "")
                    //{
                    //    if (TRANSACTIONID == "") { error = "TRANSACTIONID"; }
                    //    if (APTRANSACTIONID == "") { error = "APTRANSACTIONID"; }
                    //    if (AMOUNT == "") { error = "AMOUNT"; }
                    //    if (TRANSACTIONSTATUS == "") { error = "TRANSACTIONSTATUS"; }
                    //    if (ap_SecureHash == "") { error = "ap_SecureHash"; }
                    //}
                    //CommonMethod.TraceLogWritetoNotepad("obj1.objExamFrom4", "Exam/AirPayPGSucessExam4", "obj1.objExamFrom", "Id");
                    DateTime now = DateTime.Now;
                    string MID = "";
                    string username = "";
                    //comparing Secure Hash with Hash sent by Airpay
                    string banktrxid = Request.Form["bank_ref_num"]; ;
                    string clienttrxid = "";
                    string amount1 = AMOUNT;
                    string feeamount = "0";
                    string gst = "0";
                    string error = Request.Form["error"];
                    string commission = Request.Form["deduction_percentage"];
                    string paymode = Request.Form["card_type"];
                    string banktxndate = Request.Form["addedon"];
                    string Reason = error;
                    string apitxnid = Request.Form["easepayid"]; ;
                    string ApplicationNo = "";
                    string courseyearid = "";
                    string examType = "";
                    string Requestdata = merc_hash_string;
                    string dRequestdata = merc_hash;
                    string PGstatus = MESSAGE;
                    string Sid = "";
                    string Sessionid = "";
                    CommonMethod.TraceLogWritetoNotepad("obj1.objExamFrom4", "Exam/AirPayPGSucessExam5", "obj1.objExamFrom", "Id");
                    //Other_Details = Other_Details1 + ",MainYear," + courseyearid + ",Exampaper-" + year + "," + Order_Number;
                    //var other_detail = CUSTOMVAR.Split(',');
                    ApplicationNo = Request.Form["udf1"];
                    examType = Request.Form["udf2"];
                    courseyearid = Request.Form["udf3"];
                    clienttrxid = Request.Form["udf4"];
                    Sid = Request.Form["udf5"];

                    //if (error == "NA" || error ==null || error == "")
                    //{
                    if (TRANSACTIONSTATUS.ToLower() == "success")
                    {
                        //PGstatus = "SUCCESS";
                        SbiPayExam sbi = new SbiPayExam();
                        //var result = sbi.pgsucessdecrypt(Convert.ToInt32(EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENNBStID"))));
                        var result = sbi.EaseBuzzPaypgsucessdecrypt(Convert.ToInt32(Sid), banktrxid, clienttrxid, amount1, feeamount, gst, commission, paymode, banktxndate, Reason, apitxnid, ApplicationNo, courseyearid, examType, Requestdata, dRequestdata, PGstatus);
                        //var result = sbi.AirPaypgsucessdecrypt(Sid, banktrxid, clienttrxid, amount1, feeamount, gst, commission, paymode, banktxndate, Reason, apitxnid, ApplicationNo, courseyearid, Requestdata, dRequestdata, PGstatus);
                        CommonMethod.TraceLogWritetoNotepad("obj1.objExamFrom4", "Exam/AirPayPGSucessExam8", "obj1.objExamFrom", "Id");
                        return RedirectToAction("EaseBuzzResponseExam");
                        //Literal1.Text = "<form target='_parent' name='frmresponse' name='frmresponse' action='thankyou.aspx' method='POST'> <table width='100%'><tr width='100%'><td align='left' width='50%'>Transaction Id</td><td align='left' width='50%' style='color:black;'>" + TRANSACTIONID + "</td></tr><tr width='100%'><td align='left' width='50%'>Airpay Transaction Id</td><td align='left' width='50%' style='color:black;'>" + APTRANSACTIONID + "</td></tr><tr width='100%'><td align='left' width='50%'>Amount</td><td align='left' width='50%' style='color:black;'>" + AMOUNT + "</td></tr><tr width='100%'><td align='left' width='50%'>Transaction Status Code</td><td align='left' width='50%' style='color:black;'>" + TRANSACTIONSTATUS + "</td></tr><tr width='100%'><td align='left' width='50%'>Message</td><td align='left' width='50%' style='color:black;'>" + MESSAGE + "</td></tr><tr width='100%'><td align='left' width='50%'>Status</td><td align='left' width='50%' style='color:green;'>Success</td></tr></table></form>";
                    }
                    else
                    {
                        CommonMethod.TraceLogWritetoNotepad("obj1.objExamFrom4", "Exam//AirPayPGSucessExam9", "obj1.objExamFrom", "Id");
                        //PGstatus = "FAIL";
                        SbiPayExam sbi = new SbiPayExam();
                        //var result = sbi.pgfaileddecrypt(Convert.ToInt32(EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENNBStID"))));
                        var result = sbi.EaseBuzzPaypgsucessdecrypt(Convert.ToInt32(Sid), banktrxid, clienttrxid, amount1, feeamount, gst, commission, paymode, banktxndate, Reason, apitxnid, ApplicationNo, courseyearid, examType, Requestdata, dRequestdata, PGstatus);
                        //var result = sbi.AirPaypgsucessdecrypt(Sid, banktrxid, clienttrxid, amount1, feeamount, gst, commission, paymode, banktxndate, Reason, apitxnid, ApplicationNo, courseyearid, Requestdata, dRequestdata, PGstatus);
                        CommonMethod.TraceLogWritetoNotepad("obj1.objExamFrom4", "Exam//AirPayPGSucessExam10", "obj1.objExamFrom", "Id");
                        //return RedirectToAction("AirPayResponseExam", new { id = Sid, session= Sessionid });
                        return RedirectToAction("EaseBuzzResponseExam");
                        //Literal1.Text = "<form target='_parent' name='frmresponse' name='frmresponse' action='thankyou.aspx' method='POST'> <table width='100%'><tr width='100%'><td align='left' width='50%'>Transaction Id</td><td align='left' width='50%' style='color:black;'>" + TRANSACTIONID + "</td></tr><tr width='100%'><td align='left' width='50%'>Airpay Transaction Id</td><td align='left' width='50%' style='color:black;'>" + APTRANSACTIONID + "</td></tr><tr width='100%'><td align='left' width='50%'>Amount</td><td align='left' width='50%' style='color:black;'>" + AMOUNT + "</td></tr><tr width='100%'><td align='left' width='50%'>Transaction Status Code</td><td align='left' width='50%' style='color:black;'>" + TRANSACTIONSTATUS + "</td></tr><tr width='100%'><td align='left' width='50%'>Message</td><td align='left' width='50%' style='color:black;'>" + MESSAGE + "</td></tr><tr width='100%'><td align='left' width='50%'>Status</td><td align='left' width='50%' style='color:green;'>Failed</td></tr></table></form>";
                    }
                    //}
                    //    else
                    //{
                    //    PGstatus = TRANSACTIONSTATUS;
                    //    CommonMethod.TraceLogWritetoNotepad("obj1.objExamFrom4", "Exam//AirPayPGSucessExam11", "obj1.objExamFrom", "Id");
                    //    SbiPayExam sbi = new SbiPayExam();
                    //    //var result = sbi.pgfaileddecrypt(Convert.ToInt32(EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENNBStID"))));
                    //    var result = sbi.EaseBuzzPaypgsucessdecrypt(Convert.ToInt32(Sid), banktrxid, clienttrxid, amount1, feeamount, gst, commission, paymode, banktxndate, Reason, apitxnid, ApplicationNo, courseyearid, examType, Requestdata, dRequestdata, PGstatus);
                    //    CommonMethod.TraceLogWritetoNotepad("obj1.objExamFrom4", "Exam//AirPayPGSucessExam12", "obj1.objExamFrom", "Id");
                    //    return RedirectToAction("EaseBuzzResponseExam");
                    //    //Literal1.Text = "<table width='100%'><tr><td align='center'>Variable(s) " + error + " is/are empty.</td></tr></table>";
                    //}

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

        //prepare a postform for redirection to payment gateway
        public ActionResult SafeXPGGatewayExam()
        {

            return RedirectToAction("ExamFeesSubmit", "Exam");

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
                CommonMethod.TraceLogWritetoNotepad("obj1.objExamFrom4", "Exam/SafeXPGGatewayExam1", "safexeaxamcontroller1", "Id");
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
                    datecom = com.check_ExamFeeSubmit_check(ad.GetAcademiccurrentSession().ID, Convert.ToInt32(CommonSetting.Commonid.Educationtype));
                    if (datecom.isopendate == false)
                    {
                        return RedirectToAction("ExamFeesSubmit", "Exam");
                    }
                    if (datecom.isclosedate == false)
                    {
                        return RedirectToAction("ExamFeesSubmit", "Exam");
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

                    /*    Amount1 = 50; */ // Dommey amount
                    if (Amount1 <= 0)
                    {
                        TempData["msgerror"] = "Amount Should be greater than 0 !!";
                        return RedirectToAction("ExamFeesSubmit", "Exam");
                    }
                    CommonMethod.TraceLogWritetoNotepad("obj1.objExamFrom4", "Exam/SafeXPGGatewayExam2", "safexeaxamcontroller2", "Id");
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
                    //string sAmount2 = Amount1.ToString("0.00");
                    string sOrderId = "";
                    EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENBApplicationNo"));//other Amount  me bhejna h
                   var obj = sbi.encriptDataExamSafexPay(Amount1, EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENBApplicationNo")), "student/Exam/SafexPayPGSucessExam", "student/Exam/SafexPayPGSucessExam", obj1.objExamFrom.YearName, latefee, amount_without_latefee, obj1.objExamFrom.courseyearid, sEmail, sPhone, sFName, sLName, sAddress, sCity, sState, sCountry, sPincode, sAmount, sOrderId);
                    //sbi.Safex_me_id = txnmerchantId; // As String
                    //sbi.Safex_merchant_request = enc_request;
                    //sbi.SAfex_hash = enc_hash;

                    ViewBag.url = obj.Safex_POSTURL;
                    ViewBag.me_id = obj.Safex_me_id;
                    ViewBag.merchant_request = obj.Safex_merchant_request;
                    ViewBag.hash = obj.Safex_hash;

                }
            }
            catch (Exception ex)
            {
                CommonMethod.TraceLogWritetoNotepad("obj1.objExamFrom3", ex.ToString(), "exaptioncontroller", "Id");
                CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "action PGGateway error for Eaxm Fee", ClsLanguage.GetCookies("NBApplicationNo"));

                return View();
            }
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



                //CommonMethod.TraceLogWritetoNotepad("obj1.objExamFrom4", "Exam/AirPayPGSucessExam5", "obj1.objExamFrom", "Id");
                //{
                if (status == "Successful")
                {
                    PGstatus = "Success";
                    status = "Success";
                    //CommonMethod.TraceLogWritetoNotepad("obj1.objExamFrom4", "Exam/AirPayPGSucessExam7", "obj1.objExamFrom", "Id");
                    //PGstatus = "SUCCESS";
                    SbiPayExam sbi = new SbiPayExam();
                    //var result = sbi.pgsucessdecrypt(Convert.ToInt32(EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENNBStID"))));
                    var result = sbi.SafexPaypgsucessdecrypt(Convert.ToInt32(Sid), banktrxid, clienttrxid, amount1, feeamount, gst, commission, paymode, banktxndate, Reason, apitxnid, ApplicationNo, courseyearid, examType, Requestdata, dRequestdata, PGstatus);

                    //var result = sbi.AirPaypgsucessdecrypt(Sid, banktrxid, clienttrxid, amount1, feeamount, gst, commission, paymode, banktxndate, Reason, apitxnid, ApplicationNo, courseyearid, Requestdata, dRequestdata, PGstatus);
                    //CommonMethod.TraceLogWritetoNotepad("obj1.objExamFrom4", "Exam/AirPayPGSucessExam8", "obj1.objExamFrom", "Id");
                    return RedirectToAction("SafexPayResponseExam");
                    //Literal1.Text = "<form target='_parent' name='frmresponse' name='frmresponse' action='thankyou.aspx' method='POST'> <table width='100%'><tr width='100%'><td align='left' width='50%'>Transaction Id</td><td align='left' width='50%' style='color:black;'>" + TRANSACTIONID + "</td></tr><tr width='100%'><td align='left' width='50%'>Airpay Transaction Id</td><td align='left' width='50%' style='color:black;'>" + APTRANSACTIONID + "</td></tr><tr width='100%'><td align='left' width='50%'>Amount</td><td align='left' width='50%' style='color:black;'>" + AMOUNT + "</td></tr><tr width='100%'><td align='left' width='50%'>Transaction Status Code</td><td align='left' width='50%' style='color:black;'>" + TRANSACTIONSTATUS + "</td></tr><tr width='100%'><td align='left' width='50%'>Message</td><td align='left' width='50%' style='color:black;'>" + MESSAGE + "</td></tr><tr width='100%'><td align='left' width='50%'>Status</td><td align='left' width='50%' style='color:green;'>Success</td></tr></table></form>";
                }
                else
                {
                    //CommonMethod.TraceLogWritetoNotepad("obj1.objExamFrom4", "Exam//AirPayPGSucessExam9", "obj1.objExamFrom", "Id");
                    //PGstatus = "FAIL";
                    SbiPayExam sbi = new SbiPayExam();
                    //var result = sbi.pgfaileddecrypt(Convert.ToInt32(EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENNBStID"))));
                    var result = sbi.SafexPaypgsucessdecrypt(Convert.ToInt32(Sid), banktrxid, clienttrxid, amount1, feeamount, gst, commission, paymode, banktxndate, Reason, apitxnid, ApplicationNo, courseyearid, examType, Requestdata, dRequestdata, PGstatus);
                    //var result = sbi.AirPaypgsucessdecrypt(Sid, banktrxid, clienttrxid, amount1, feeamount, gst, commission, paymode, banktxndate, Reason, apitxnid, ApplicationNo, courseyearid, Requestdata, dRequestdata, PGstatus);
                    //CommonMethod.TraceLogWritetoNotepad("obj1.objExamFrom4", "Exam//AirPayPGSucessExam10", "obj1.objExamFrom", "Id");
                    //return RedirectToAction("AirPayResponseExam", new { id = Sid, session= Sessionid });
                    return RedirectToAction("SafexPayResponseExam");

                    //Literal1.Text = "<form target='_parent' name='frmresponse' name='frmresponse' action='thankyou.aspx' method='POST'> <table width='100%'><tr width='100%'><td align='left' width='50%'>Transaction Id</td><td align='left' width='50%' style='color:black;'>" + TRANSACTIONID + "</td></tr><tr width='100%'><td align='left' width='50%'>Airpay Transaction Id</td><td align='left' width='50%' style='color:black;'>" + APTRANSACTIONID + "</td></tr><tr width='100%'><td align='left' width='50%'>Amount</td><td align='left' width='50%' style='color:black;'>" + AMOUNT + "</td></tr><tr width='100%'><td align='left' width='50%'>Transaction Status Code</td><td align='left' width='50%' style='color:black;'>" + TRANSACTIONSTATUS + "</td></tr><tr width='100%'><td align='left' width='50%'>Message</td><td align='left' width='50%' style='color:black;'>" + MESSAGE + "</td></tr><tr width='100%'><td align='left' width='50%'>Status</td><td align='left' width='50%' style='color:green;'>Failed</td></tr></table></form>";
                }

                //else
                //{
                //    PGstatus = "ERROR";
                //    CommonMethod.TraceLogWritetoNotepad("obj1.objExamFrom4", "Exam//AirPayPGSucessExam11", "obj1.objExamFrom", "Id");
                //    SbiPayExam sbi = new SbiPayExam();
                //    //var result = sbi.pgfaileddecrypt(Convert.ToInt32(EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENNBStID"))));
                //    var result = sbi.AirPaypgsucessdecrypt(Convert.ToInt32(Sid), banktrxid, clienttrxid, amount1, feeamount, gst, commission, paymode, banktxndate, Reason, apitxnid, ApplicationNo, courseyearid, examType, Requestdata, dRequestdata, PGstatus);
                //    CommonMethod.TraceLogWritetoNotepad("obj1.objExamFrom4", "Exam//AirPayPGSucessExam12", "obj1.objExamFrom", "Id");
                //    return RedirectToAction("AirPayResponseExam");

                //Literal1.Text = "<table width='100%'><tr><td align='center'>Variable(s) " + error + " is/are empty.</td></tr></table>";
                //    }

                //}
                //catch (Exception ex)
                //{
                //    CommonMethod.TraceLogWritetoNotepad("obj1.objExamFrom4", "Exam//AirPayPGSucessExam13", "obj1.objExamFrom", "Id");
                //    CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, " Air Pay Payment PaymentGateway suucess url hit on Controller error for Exam Fees Submit", ClsLanguage.GetCookies("NBApplicationNo"));

                //}
                //}
                //return RedirectToAction("ExamFeesSubmit");
            }
            //    }
            //    catch (Exception ex)
            //    {
            //        //PGstatus = "ERROR";
            //        //CommonMethod.TraceLogWritetoNotepad("obj1.objExamFrom4", "Exam//AirPayPGSucessExam11", "obj1.objExamFrom", "Id");
            //        //SbiPayExam sbi = new SbiPayExam();
            //        ////var result = sbi.pgfaileddecrypt(Convert.ToInt32(EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENNBStID"))));
            //        //var result = sbi.AirPaypgsucessdecrypt(Convert.ToInt32(Sid), banktrxid, clienttrxid, amount1, feeamount, gst, commission, paymode, banktxndate, Reason, apitxnid, ApplicationNo, courseyearid, examType, Requestdata, dRequestdata, PGstatus);
            //        //CommonMethod.TraceLogWritetoNotepad("obj1.objExamFrom4", "Exam//AirPayPGSucessExam12", "obj1.objExamFrom", "Id");
            //        //return RedirectToAction("AirPayResponseExam");
            //        CommonMethod.TraceLogWritetoNotepad("obj1.objExamFrom4", responseapi, ex.ToString(), "Id");
            //        CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, " Air Pay Payment PaymentGateway suucess url hit on Controller error for Exam Fees Submit", ClsLanguage.GetCookies("NBApplicationNo"));


            //}
            return View();
        }

        public ActionResult PGGatewayExam()
        {
            //  return RedirectToAction("Index", "Home");
            try
            {
                CommonMethod.TraceLogWritetoNotepad("obj1.objExamFrom4", "Exam/PGGatewayExam", "obj1.objExamFrom1", "Id");
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
                    datecom = com.check_ExamFeeSubmit_check(ad.GetAcademiccurrentSession().ID, Convert.ToInt32(CommonSetting.Commonid.Educationtype));
                    if (datecom.isopendate == false)
                    {
                        return RedirectToAction("ExamFeesSubmit", "Exam");
                    }
                    if (datecom.isclosedate == false)
                    {
                        return RedirectToAction("ExamFeesSubmit", "Exam");
                    }


                    if (obj1.objExamFrom.IsDocVerify == 1)
                    {

                    }
                    else
                    { return RedirectToAction("Index", "Home"); }
                    if (obj1.objExamFrom.IsExamfeesubmit == 1) { return RedirectToAction("ExamFeesSubmit"); }
                    Amount1 = Convert.ToDecimal(feestruckture.Sum(x => x.amount));
                    latefee = Convert.ToDecimal(feestruckture.Sum(x => x.late_fee));

                    amount_without_latefee = Amount1;
                    Amount1 = Amount1 + latefee;
                    //Amount1 = 1;
                    if (Amount1 <= 0)
                    {
                        TempData["msgerror"] = "Amount Should be greater than 0 !!";
                        return RedirectToAction("ExamFeesSubmit", "Exam");
                    }
                    //Amount1 = 1;
                    var obj = sbi.encriptDataExam(Amount1, EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENBApplicationNo")), "student/Exam/PGSucessExam", "student/Exam/PGFailedExam", obj1.objExamFrom.YearName, latefee, amount_without_latefee, obj1.objExamFrom.courseyearid);
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

        public ActionResult EaseBuzzResponseExam()
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


        public ActionResult AirPayResponseExamBack()
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

        public ActionResult AirPayResponseExamPart3()
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

        public ActionResult SafexPayResponseExam()
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
                CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "Air Pay Response payment gateway get method for exam Fee on Controller error", ClsLanguage.GetCookies("NBApplicationNo"));
                PrintExamForm PritApp = new PrintExamForm();
                return View(PritApp);
            }
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
        public ActionResult AirPayPGFailedExam()
        {
            //  return RedirectToAction("Index", "HomePG");
            string paramInfo = "";

            if (Request.Form.Count > 0)
            {
                try
                {
                    SbiPayExam sbi = new SbiPayExam();
                    var result = sbi.pgfaileddecrypt(Convert.ToInt32(EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENNBStID"))));
                    return RedirectToAction("AirPayResponseExam");
                }
                catch (Exception ex)
                {
                    CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "Air Pay Payment PaymentGateway suucess url hit on Controller error for Exam Fees Submit", ClsLanguage.GetCookies("NBApplicationNo"));

                }
            }
            return RedirectToAction("ExamFeesSubmit");
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

        public JsonResult Subject_bindDanamic(string courseyearidenc = "", string SubjectList = "", string TotalSubject = "", string TotalFees = "")
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

            List<SelectListItem> slist = new List<SelectListItem>();

            foreach (SubjectMaster dr in list)
            {
                slist.Add(new SelectListItem { Text = dr.SubjectName, Value = dr.ID.ToString() });
            }
            ViewBag.Subject = slist;
            return Json(list, JsonRequestBehavior.AllowGet);
        }

       
   public ActionResult BackExamFeesSubmit(string courseyearidenc = "")

        {
            //return RedirectToAction("Index", "Home");
            ExamForm ob = new ExamForm();
            AcademicSession ad = new AcademicSession();
            BL_PrintApplication PritApp = new BL_PrintApplication();
            // manaul change  course year value from  fetch tbl_CourseYear according to couryearid
            int courseyearid = Convert.ToInt32(EncriptDecript.Decrypt(courseyearidenc));
            Commn_master com = new Commn_master();
            ViewBag.check_ExamFeeSubmit_open = com.Backcheck_ExamFeeSubmit_open(ad.GetAcademiccurrentSession().ID, Convert.ToInt32(CommonSetting.Commonid.Educationtype));
            ViewBag.check_ExamFeeSubmit_Close = com.Backcheck_ExamFeeSubmit_close(ad.GetAcademiccurrentSession().ID, Convert.ToInt32(CommonSetting.Commonid.Educationtype));
            ViewBag.check_admissionopen = ViewBag.check_ExamFeeSubmit_open;

            var obj1 = ob.backGetAppLicationDataForExamFeeUG_back(Convert.ToInt32(EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENNBStID"))), courseyearid, System.DateTime.Now.Year);

            if (obj1.objExamFrom == null)
            {
                return RedirectToAction("Index", "Home");
            }
            if (obj1.objExamFrom.isfeesubmitregistration == 0)
            {
                return RedirectToAction("Index", "Home");
            }
            if (obj1.objExamFrom.RegistrationNo == null || obj1.objExamFrom.RegistrationNo == "")
            {
                return RedirectToAction("Index", "Home");
            }
            if (obj1.objExamFrom.RollNo == null || obj1.objExamFrom.RollNo == "")
            {
                return RedirectToAction("Index", "Home");
            }

            // only for session student check

            //if (obj1.objExamFrom.sessionid != 39)
            //{
            //    return RedirectToAction("Index", "Home");
            //}
            //if (obj1.objExamFrom.Currentyear_courseyarid == 2 || obj1.objExamFrom.Currentyear_courseyarid == 5 || obj1.objExamFrom.Currentyear_courseyarid == 8)
            //{
            //    // allow only for 2 year student which have back paper
            //    // first check exam fee payment before admission fee submit or not 
            //    //int a = ob.check_examfeebefore_admissionfee(obj1.objExamFrom.coursecategoryid, obj1.objExamFrom.StreamCategoryID, obj1.objExamFrom.Currentyear_courseyarid, obj1.objExamFrom.sessionid, obj1.objExamFrom.collegeid, obj1.objExamFrom.sid);
            //    //if (a <= 0)
            //    //{
            //    //    return RedirectToAction("Index", "Home");
            //    //}
            //}
            //else
            //{ return RedirectToAction("Index", "Home"); }
            StudentAdmissionQualification obj = new StudentAdmissionQualification();
            QualifiationMasterList sub = new QualifiationMasterList();
            List<EaxmFeesSubmit> feestruckture = new List<EaxmFeesSubmit>();
            List<EaxmFeesSubmit> subjectlist = new List<EaxmFeesSubmit>();
            List<EaxmFeesSubmit> Electivesubjectlist = new List<EaxmFeesSubmit>();

            //if (Convert.ToInt32(EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENNBCourseCategory"))) == 7)
            //{
            //    courseyearid = 16;// couryearid 16 for first semester ma; , kon se semester ke lia open krna h , manaul set semesterid
            //}
            //if (Convert.ToInt32(EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENNBCourseCategory"))) == 9)
            //{
            //    courseyearid = 26;// couryearid 26 for first semester msc; , kon se semester ke lia open krna h , manaul set semesterid
            //}
            //if (Convert.ToInt32(EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENNBCourseCategory"))) == 11)
            //{
            //    courseyearid = 24;// couryearid 24 for first semester mcon; , kon se semester ke lia open krna h , manaul set semesterid
            //}

            Commn_master datecom = new Commn_master();
            datecom = com.check_ExamFeeApply(Convert.ToInt32(EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENNBStID"))), "back", courseyearid);

            if (datecom.FormStatus == "1")
            {
                ViewBag.ExamFormApply = "1";
            }
            else
            {
                ViewBag.ExamFormApply = "0";
            }


            feestruckture = ob.backFeesDetailsstructure(obj1.objExamFrom.collegeid, obj1.objExamFrom.coursecategoryid, obj1.objExamFrom.sessionid, obj1.objExamFrom.castecategoryid, obj1.objExamFrom.StreamCategoryID, courseyearid, 0);
            // manaul change  course year value from  fetch tbl_CourseYear according to couryearid
            subjectlist = ob.backFeesDetailSubjectlist_UG(obj1.objExamFrom.coursecategoryid, obj1.objExamFrom.StreamCategoryID, courseyearid, obj1.objExamFrom.sessionid, obj1.objExamFrom.collegeid, obj1.objExamFrom.sid);
            if (subjectlist.Count == 0)
            {
                // if subject count greater than 0 then is counted as back student else it not consider as back student
                return RedirectToAction("Index", "Home");
            }
            var tuple = new Tuple<PrintExamForm, QualifiationMasterList, List<EaxmFeesSubmit>, List<EaxmFeesSubmit>>(obj1, sub, feestruckture, subjectlist);
            return View(tuple);
        }
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
            // manaul change  course year value from  fetch tbl_CourseYear according to couryearid

            int courseyearid = Convert.ToInt32(EncriptDecript.Decrypt(courseyearidenc));

            var obj1 = ob.backGetAppLicationDataForExamFeeUG_back(Convert.ToInt32(EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENNBStID"))), courseyearid, System.DateTime.Now.Year);
            Commn_master com = new Commn_master();
            ViewBag.check_ExamFeeSubmit_open = com.Backcheck_ExamFeeSubmit_open(ad.GetAcademiccurrentSession().ID, Convert.ToInt32(CommonSetting.Commonid.Educationtype));
            ViewBag.check_ExamFeeSubmit_Close = com.Backcheck_ExamFeeSubmit_close(ad.GetAcademiccurrentSession().ID, Convert.ToInt32(CommonSetting.Commonid.Educationtype));
            ViewBag.check_admissionopen = ViewBag.check_ExamFeeSubmit_open;
            if (ViewBag.check_ExamFeeSubmit_open == true && ViewBag.check_ExamFeeSubmit_Close == true)
            {

            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
            if (obj1.objExamFrom == null)
            {
                return RedirectToAction("Index", "Home");
            }
            if (obj1.objExamFrom.isfeesubmitregistration == 0)
            {
                return RedirectToAction("Index", "Home");
            }

            // only for session student check

            //if (obj1.objExamFrom.sessionid != 39)
            //{
            //    return RedirectToAction("Index", "Home");
            //}
            //if (applyadmissionform == "applyadmissionform")
            //{
            //    //if (Substreamcategoryid == "")
            //    //{
            //    //    return RedirectToAction("ExamFeesSubmit");
            //    //}
            //    if (checkboxid == "")
            //    {
            //        return RedirectToAction("BackExamFeesSubmit");
            //    }
            Commn_master datecom = new Commn_master();

            datecom = com.check_ExamFeeApply(lo.Id, "back", courseyearid);

            if (datecom.FormStatus == "1")
            {
                ViewBag.ExamFormApply = "1";
            }
            else
            {
                objexamfrom.student_examform_apply_back(obj1.objExamFrom.sid, obj1.objExamFrom.sessionid, obj1.objExamFrom.coursecategoryid, obj1.objExamFrom.collegeid, obj1.objExamFrom.StreamCategoryID, courseyearid, Convert.ToInt32(0), Convert.ToInt32(0), "UG", "", System.DateTime.Now.Year);
                return RedirectToAction("BackExamFeesSubmit", new { @courseyearidenc = courseyearidenc });
            }
            //ViewBag.PaymentStatus = com.Backcheck_ExamFeeSubmit_status(ad.GetAcademiccurrentSession().ID, Convert.ToInt32(CommonSetting.Commonid.Educationtype), courseyearid, Convert.ToInt32(EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENNBStID"))));

            //if (ViewBag.PaymentStatus == true)
            //{
            //    return RedirectToAction("BackExamFeesSubmit", new { @courseyearidenc = courseyearidenc });
            //}

            StudentAdmissionQualification obj = new StudentAdmissionQualification();
            QualifiationMasterList sub = new QualifiationMasterList();
            List<EaxmFeesSubmit> feestruckture = new List<EaxmFeesSubmit>();
            List<EaxmFeesSubmit> subjectlist = new List<EaxmFeesSubmit>();
            List<EaxmFeesSubmit> Electivesubjectlist = new List<EaxmFeesSubmit>();
            feestruckture = ob.backFeesDetailsstructure(obj1.objExamFrom.collegeid, obj1.objExamFrom.coursecategoryid, obj1.objExamFrom.sessionid, obj1.objExamFrom.castecategoryid, obj1.objExamFrom.StreamCategoryID, obj1.objExamFrom.courseyearid, 0);
            // manaul change  course year value from  fetch tbl_CourseYear according to couryearid
            subjectlist = ob.backFeesDetailSubjectlist_UG(obj1.objExamFrom.coursecategoryid, obj1.objExamFrom.StreamCategoryID, courseyearid, obj1.objExamFrom.sessionid, obj1.objExamFrom.collegeid, obj1.objExamFrom.sid);
            
            if (subjectlist.Count == 0)
            {
                return RedirectToAction("Index", "Home");
            }


            if (obj1.objExamFrom.IsExamfeesubmit == 0)
            {
                // TempData["msgfees"] = "Please wait till exam fee open date !!!";
                //return RedirectToAction("ExamFeesSubmit");

                //return RedirectToAction("SelectPaymentGetway");
                //return RedirectToAction("SelectPaymentGetway");

                return RedirectToAction("SelectPaymentGetwayBacklog", new { @courseyearidenc = courseyearidenc });
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


        public ActionResult SelectPaymentGetwayBacklog(  string courseyearidenc)
        {
           // return RedirectToAction("SelectPaymentGetwayBacklog", new { @courseyearidenc = courseyearidenc });

            return View();
        }


        public ActionResult AirPayPGGatewayExambacklog()
        {
            try {


                var courseyearidenc = Request.QueryString[0];

                int courseyearid = Convert.ToInt32(EncriptDecript.Decrypt(courseyearidenc));

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

            StudentLogin stu = new StudentLogin();
            string ApplicationID = ClsLanguage.GetCookies("NBApplicationNo");
            Login lo = stu.BasicDetail(ApplicationID);
            // manaul change  course year value from  fetch tbl_CourseYear according to couryearid
            
            var obj1 = ob.backGetAppLicationDataForExamFeeUG_back(Convert.ToInt32(EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENNBStID"))), courseyearid, System.DateTime.Now.Year);
            feestruckture = ob.backFeesDetailsstructure(obj1.objExamFrom.collegeid, obj1.objExamFrom.coursecategoryid, obj1.objExamFrom.sessionid, obj1.objExamFrom.castecategoryid, obj1.objExamFrom.StreamCategoryID, courseyearid, 0);
            // manaul change  course year value from  fetch tbl_CourseYear according to couryearid
            subjectlist = ob.backFeesDetailSubjectlist_UG(obj1.objExamFrom.coursecategoryid, obj1.objExamFrom.StreamCategoryID, courseyearid, obj1.objExamFrom.sessionid, obj1.objExamFrom.collegeid, obj1.objExamFrom.sid);

            //subjectlist = ob.backFeesDetailSubjectlist_UG(obj1.objExamFrom.coursecategoryid, obj1.objExamFrom.StreamCategoryID, obj1.objExamFrom.courseyearid, obj1.objExamFrom.sessionid, obj1.objExamFrom.collegeid, obj1.objExamFrom.sid);
            if (subjectlist.Count == 0)
            {
                return RedirectToAction("Index", "Home");
            }
            if (obj1.objExamFrom != null)
            {
                AcademicSession ac = new AcademicSession();
                int sessionid = ac.GetAcademiccurrentSession().ID;
                int educationtype = Convert.ToInt32(CommonSetting.Commonid.Educationtype);
                var datestart = com.Backcheck_ExamFeeSubmit_open(ad.GetAcademiccurrentSession().ID, Convert.ToInt32(CommonSetting.Commonid.Educationtype));
                if (datestart == false)
                {
                    return RedirectToAction("BackExamFeesSubmit", new { @courseyearidenc = courseyearidenc });
                }
                var dateextend = com.Backcheck_ExamFeeSubmit_close(ad.GetAcademiccurrentSession().ID, Convert.ToInt32(CommonSetting.Commonid.Educationtype));
                if (dateextend == false)
                {
                    return RedirectToAction("BackExamFeesSubmit", new { @courseyearidenc = courseyearidenc });
                }
                //if (obj1.objExamFrom.IsDocVerify == 1)
                //{

                //}
                //else
                //{
                //    return RedirectToAction("Index", "Home");
                //}
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
                    var obj = sbi.encriptDataExamAirPay_back(Amount1, EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENBApplicationNo")), "student/Exam/AirPayPGSucessExamback", "student/Exam/AirPayPGFailedExamback", courseyearid.ToString(), latefee, amount_without_latefee, obj1.objExamFrom.courseyearid, sEmail, sPhone, sFName, sLName, sAddress, sCity, sState, sCountry, sPincode, sAmount, sOrderId);

                    ViewBag.orderid = obj.Oid;
                    ViewBag.buyerEmail = sEmail;
                    ViewBag.buyerPhone = lo.MobileNo;
                    ViewBag.buyerFirstName = lo.FirstName;
                    ViewBag.buyerLastName = lo.LastName;
                    ViewBag.buyerPinCode = lo.CA_PinCode;
                    ViewBag.amount = obj.Eamount;
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


        public ActionResult AirPayPGSucessExamback()
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
                    examType = "Backyear";
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
                            //return RedirectToAction("BackResponseExam");
                            return RedirectToAction("BackResponseExam", new { @courseyearidenc = courseyearid });
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
                            //return RedirectToAction("BackResponseExam");
                            return RedirectToAction("BackResponseExam", new { @courseyearidenc = courseyearid });

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
                        return RedirectToAction("BackResponseExam", new { @courseyearidenc = courseyearid });

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


        //public ActionResult BackPGGatewayExam(string courseyearidenc = "")
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

        //        StudentLogin stu = new StudentLogin();
        //        string ApplicationID = ClsLanguage.GetCookies("NBApplicationNo");
        //        Login lo = stu.BasicDetail(ApplicationID);
        //        // manaul change  course year value from  fetch tbl_CourseYear according to couryearid
        //        int courseyearid = Convert.ToInt32(EncriptDecript.Decrypt(courseyearidenc));
        //        var obj1 = ob.backGetAppLicationDataForExamFeeUG_back(Convert.ToInt32(EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENNBStID"))), courseyearid, System.DateTime.Now.Year);
        //        feestruckture = ob.backFeesDetailsstructure(obj1.objExamFrom.collegeid, obj1.objExamFrom.coursecategoryid, obj1.objExamFrom.sessionid, obj1.objExamFrom.castecategoryid, obj1.objExamFrom.StreamCategoryID, courseyearid, 0);
        //        // manaul change  course year value from  fetch tbl_CourseYear according to couryearid
        //        subjectlist = ob.backFeesDetailSubjectlist_UG(obj1.objExamFrom.coursecategoryid, obj1.objExamFrom.StreamCategoryID, courseyearid, obj1.objExamFrom.sessionid, obj1.objExamFrom.collegeid, obj1.objExamFrom.sid);

        //        //subjectlist = ob.backFeesDetailSubjectlist_UG(obj1.objExamFrom.coursecategoryid, obj1.objExamFrom.StreamCategoryID, obj1.objExamFrom.courseyearid, obj1.objExamFrom.sessionid, obj1.objExamFrom.collegeid, obj1.objExamFrom.sid);
        //        if (subjectlist.Count == 0)
        //        {
        //            return RedirectToAction("Index", "Home");
        //        }
        //        if (obj1.objExamFrom != null)
        //        {
        //            AcademicSession ac = new AcademicSession();
        //            int sessionid = ac.GetAcademiccurrentSession().ID;
        //            int educationtype = Convert.ToInt32(CommonSetting.Commonid.Educationtype);
        //            var datestart = com.Backcheck_ExamFeeSubmit_open(ad.GetAcademiccurrentSession().ID, Convert.ToInt32(CommonSetting.Commonid.Educationtype));
        //            if (datestart == false)
        //            {
        //                return RedirectToAction("BackExamFeesSubmit", new { @courseyearidenc = courseyearidenc });
        //            }
        //            var dateextend = com.Backcheck_ExamFeeSubmit_close(ad.GetAcademiccurrentSession().ID, Convert.ToInt32(CommonSetting.Commonid.Educationtype));
        //            if (dateextend == false)
        //            {
        //                return RedirectToAction("BackExamFeesSubmit", new { @courseyearidenc = courseyearidenc });
        //            }
        //            //if (obj1.objExamFrom.IsDocVerify == 1)
        //            //{

        //            //}
        //            //else
        //            //{
        //            //    return RedirectToAction("Index", "Home");
        //            //}
        //            if (obj1.objExamFrom.IsExamfeesubmit == 1) { return RedirectToAction("BackExamFeesSubmit", new { @courseyearidenc = courseyearidenc }); }
        //            Amount1 = Convert.ToDecimal(feestruckture.Sum(x => x.amount));
        //            latefee = Convert.ToDecimal(feestruckture.Sum(x => x.late_fee));
        //            amount_without_latefee = Amount1;
        //            Amount1 = Amount1 + latefee;
        //            if (Amount1 <= 0)
        //            {
        //                TempData["msgerror"] = "Amount Should be greater than 0 !!";
        //                return RedirectToAction("BackExamFeesSubmit", new { @courseyearidenc = courseyearidenc });
        //            }
        //            //Amount1 = 1;
        //            //var obj = sbi.encriptDataExambackyear(Amount1, EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENBApplicationNo")), "student/Exam/backPGSucessExam", "student/Exam/backPGFailedExam", obj1.objExamFrom.YearName, latefee, amount_without_latefee, courseyearid);
                    
        //            string sEmail = lo.Email;
        //            string sPhone = lo.MobileNo;
        //            string sFName = lo.FirstName;
        //            string sLName = lo.LastName;
        //            string sAddress = lo.CurrentAddress;
        //            string sCity = lo.CA_City.ToString();
        //            string sState = lo.CA_State.ToString();
        //            string sCountry = lo.CA_Country.ToString();
        //            string sPincode = lo.CA_PinCode;
        //            string sAmount = Amount1.ToString("0.00");
        //            string sOrderId = "";

        //            EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENBApplicationNo"));//other Amount  me bhejna h
        //            //Amount1 = 1;
        //            var obj = sbi.encriptDataExamAirPay_back(Amount1, EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENBApplicationNo")), "student/Exam/AirPayPGSucessExamBack", "student/Exam/AirPayPGFailedExamBack", obj1.objExamFrom.YearName, latefee, amount_without_latefee, obj1.objExamFrom.courseyearid, sEmail, sPhone, sFName, sLName, sAddress, sCity, sState, sCountry, sPincode, sAmount, sOrderId);

        //            ViewBag.orderid = obj.Oid;
        //            ViewBag.buyerEmail = sEmail;
        //            ViewBag.buyerPhone = lo.MobileNo;
        //            ViewBag.buyerFirstName = lo.FirstName;
        //            ViewBag.buyerLastName = lo.LastName;
        //            ViewBag.buyerPinCode = lo.CA_PinCode;
        //            ViewBag.amount = obj.Eamount;
        //            ViewBag.chmod = "";
        //            ViewBag.checksum = obj.checksum;
        //            ViewBag.privatekey = obj.privatekey;
        //            ViewBag.mercid = obj.merchantId;
        //            ViewBag.kittype = "inline";
        //            ViewBag.currency = "360";
        //            ViewBag.isocurrency = "INR";
        //            ViewBag.url = obj.url;
        //            ViewBag.success_url = obj.A_Success_url;
        //            ViewBag.customvar = obj.customvar;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "action PGGateway error for Eaxm Fee", ClsLanguage.GetCookies("NBApplicationNo"));

        //        return View();
        //    }
        //    return View();
        //}
        public ActionResult BackResponseExam(string courseyearidenc = "")
        {
            // return RedirectToAction("Index", "Home");

         


            try
            {
                ExamForm ob = new ExamForm();
                AcademicSession ad = new AcademicSession();
                // List<EaxmFeesSubmit> subjectlist = new List<EaxmFeesSubmit>();
                // manaul change  course year value from  fetch tbl_CourseYear according to couryearid
                //int courseyearid =  Convert.ToInt32(EncriptDecript.Decrypt(courseyearidenc));
                var obj1 = ob.backGetAppLicationDataForExamFeeUG(Convert.ToInt32(EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENNBStID"))), Convert.ToInt32( courseyearidenc), System.DateTime.Now.Year);
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
            return RedirectToAction("BackExamFeesSubmit");
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

        [HttpPost, ValidateInput(false)]
        public ActionResult PrintApplicationCallReceiptPart3()
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
        public ActionResult PrintApplicationPart3()
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
        public ActionResult ExaminationAdmitCard(string courseyearid)
        {
            //return RedirectToAction("Index", "Home");
            ExamForm ob = new ExamForm();
            Commn_master com = new Commn_master();
            AcademicSession ad = new AcademicSession();
            if (courseyearid == "")
            {
                courseyearid = "0";
            }
            var result = ob.StudentDetailForAdmitCard(Convert.ToInt32(courseyearid));
            return View(result);
        }
        public ActionResult BackExaminationAdmitCard(string courseyearid)
        {
            //return RedirectToAction("Index", "Home");
            ExamForm ob = new ExamForm();
            Commn_master com = new Commn_master();
            AcademicSession ad = new AcademicSession();
            if (courseyearid == "")
            {
                courseyearid = "0";
            }
            var result = ob.StudentDetailForAdmitCard_backyear(Convert.ToInt32(courseyearid));
            return View(result);
        }

        public ActionResult EnrollmentFeesSubmit()
        {
            // return RedirectToAction("Index", "Home");
            AcademicSession ad = new AcademicSession();
            BL_student_formcomplete bl = new BL_student_formcomplete();
            ExamForm ob = new ExamForm();
            var obj1 = ob.GetAppLicationDataForEnrollmentFee();
            Commn_master com = new Commn_master();
            Commn_master datecom = new Commn_master();
            datecom = com.check_EnrollmentSubmit_check(ad.GetAcademiccurrentSession().ID, Convert.ToInt32(CommonSetting.Commonid.Educationtype));
            ViewBag.check_EnrollmentFeeSubmit_open = datecom.isopendate;
            ViewBag.check_EnrollmentFeeSubmit_Close = datecom.isclosedate;
            ViewBag.enrollStartdateValue = datecom.opendate;
            ViewBag.ViewBagenrollExtenddateValue = datecom.closedate;
            var res1 = bl.CheckAdmission_details(ad.GetAcademiccurrentSession().ID);
            ViewBag.IsDocVerify = res1.IsDocVerify;
            ViewBag.IsAdmisApplied = res1.IsApplied;
            ViewBag.IsAppliedDate = res1.IsAppliedDate;
            ViewBag.IsDocVerifyDate = res1.IsDocVerifyDate;
            ViewBag.isfeesubmitt = res1.isfeesubmitt;
            ViewBag.IsfeesubmitDate = res1.IsfeesubmitDate;
            ViewBag.rejectreason = res1.rejectreason;

            //ViewBag.check_EnrollmentFeeSubmit_open = com.check_EnrollmentFeeSubmit_open(ad.GetAcademiccurrentSession().ID, Convert.ToInt32(CommonSetting.Commonid.Educationtype));

            //ViewBag.check_EnrollmentFeeSubmit_Close = com.check_EnrollmentFeeSubmit_Close(ad.GetAcademiccurrentSession().ID, Convert.ToInt32(CommonSetting.Commonid.Educationtype));

            // ViewBag.check_EnrollmentFeeSubmit_open = com.check_EnrollmentFeeSubmit_open(ad.GetAcademiccurrentSession().ID, Convert.ToInt32(CommonSetting.Commonid.Educationtype));

            //ViewBag.check_EnrollmentFeeSubmit_Close = com.check_EnrollmentFeeSubmit_Close(ad.GetAcademiccurrentSession().ID, Convert.ToInt32(CommonSetting.Commonid.Educationtype));
            // 39 student who paid exam fees but not paid enrollment fees
            //            if ( (Convert.ToInt32(EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENNBStID")))) == 9523
            //|| (Convert.ToInt32(EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENNBStID")))) == 45817
            //|| (Convert.ToInt32(EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENNBStID")))) == 42216
            //|| (Convert.ToInt32(EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENNBStID")))) == 33886
            //|| (Convert.ToInt32(EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENNBStID")))) == 18896
            //|| (Convert.ToInt32(EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENNBStID")))) == 52349
            //|| (Convert.ToInt32(EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENNBStID")))) == 26030
            //|| (Convert.ToInt32(EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENNBStID")))) == 46537
            //|| (Convert.ToInt32(EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENNBStID")))) == 52477
            //|| (Convert.ToInt32(EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENNBStID")))) == 27222
            //|| (Convert.ToInt32(EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENNBStID")))) == 26576
            //|| (Convert.ToInt32(EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENNBStID")))) == 35196
            //|| (Convert.ToInt32(EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENNBStID")))) == 25500
            //|| (Convert.ToInt32(EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENNBStID")))) == 3649
            //|| (Convert.ToInt32(EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENNBStID")))) == 35584
            //|| (Convert.ToInt32(EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENNBStID")))) == 38149
            //|| (Convert.ToInt32(EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENNBStID")))) == 39420
            //|| (Convert.ToInt32(EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENNBStID")))) == 22100
            //|| (Convert.ToInt32(EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENNBStID")))) == 25042
            //|| (Convert.ToInt32(EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENNBStID")))) == 45776
            //|| (Convert.ToInt32(EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENNBStID")))) == 32854
            //|| (Convert.ToInt32(EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENNBStID")))) == 42797
            //|| (Convert.ToInt32(EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENNBStID")))) == 20580
            //|| (Convert.ToInt32(EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENNBStID")))) == 431
            //|| (Convert.ToInt32(EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENNBStID")))) == 13991
            //|| (Convert.ToInt32(EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENNBStID")))) == 33179
            //|| (Convert.ToInt32(EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENNBStID")))) == 32712
            //|| (Convert.ToInt32(EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENNBStID")))) == 43133
            //|| (Convert.ToInt32(EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENNBStID")))) == 36453
            //|| (Convert.ToInt32(EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENNBStID")))) == 10477
            //|| (Convert.ToInt32(EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENNBStID")))) == 6901
            //|| (Convert.ToInt32(EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENNBStID")))) == 52740
            //|| (Convert.ToInt32(EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENNBStID")))) == 35268
            //|| (Convert.ToInt32(EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENNBStID")))) == 42655
            //|| (Convert.ToInt32(EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENNBStID")))) == 10395
            //|| (Convert.ToInt32(EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENNBStID")))) == 25731
            //|| (Convert.ToInt32(EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENNBStID")))) == 14144
            //|| (Convert.ToInt32(EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENNBStID")))) == 29538
            //|| (Convert.ToInt32(EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENNBStID")))) == 4326
            //                )
            //            {
            //                //ViewBag.check_EnrollmentFeeSubmit_open = true;
            //                //ViewBag.check_EnrollmentFeeSubmit_Close = true;
            //            }
            //            else
            //            {
            //                return RedirectToAction("Index", "Home");
            //            }

            //if (obj1.objfeesubmit.Id == 14144 || obj1.objfeesubmit.Id == 18896)
            //{
            //    ViewBag.check_EnrollmentFeeSubmit_open = true;
            //    ViewBag.check_EnrollmentFeeSubmit_Close = true;
            //}
            //else
            //{
            //    if (ViewBag.check_EnrollmentFeeSubmit_open == true && ViewBag.check_EnrollmentFeeSubmit_Close == true)
            //    {
            //        string abc = "pqr";
            //    }
            //    else
            //    {
            //        return RedirectToAction("Index", "Home");
            //    }


            //}
            if (obj1.objExamFrom == null)
            {

                return RedirectToAction("Index", "Home");
            }

            if (obj1.objExamFrom.IsAdmissionFee == false)
            {

                return RedirectToAction("Index", "Home");
            }
            //if (obj1.objfeesubmit.sessionid != 41)
            //{
            //    return RedirectToAction("Index", "Home");
            //}
            //}
            ViewBag.boardtype = CommonMethod.BoradtypePrevious().Where(x => x.boardid == 1 || x.boardid == 2);
            StudentAdmissionQualification obj = new StudentAdmissionQualification();
            QualifiationMasterList sub = new QualifiationMasterList();
            sub = obj.QualificationdetailList(1, 1000);
            List<StudentAdmissionQualification> asa = new List<StudentAdmissionQualification>();
            asa = sub.qlist.Where(x => x.QualicationType != 1).ToList();
            var boardtype = asa.FirstOrDefault().boardtype;
            ViewBag.boardtypename = "";
            if (boardtype == 0)
            {
                ViewBag.showpayment = false;
            }
            else
            {
                ViewBag.boardtypename = CommonMethod.BoradtypePrevious().Where(x => x.boardid == boardtype).FirstOrDefault().boardname;
                ViewBag.showpayment = true;
            }
            return View(obj1);
        }
        [HttpPost]
        public ActionResult EnrollmentFeesSubmit(int id = 0, string saveandnext = "", int boardtype = 0, string BackandEdit = "")
        {
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
            //obj.student_save_boardtype(Convert.ToInt32(EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENNBStID"))), 2);
            // return RedirectToAction("EnrollmentFeesSubmit");

            ExamForm ob = new ExamForm();
            AcademicSession ad = new AcademicSession();

            BL_PrintApplication PritApp = new BL_PrintApplication();
            var obj1 = ob.GetAppLicationDataForEnrollmentFee();
            Commn_master com = new Commn_master();
            //Commn_master datecom = new Commn_master();
            //datecom = com.check_EnrollmentSubmit_check(ad.GetAcademiccurrentSession().ID, Convert.ToInt32(CommonSetting.Commonid.Educationtype));
            //ViewBag.check_EnrollmentFeeSubmit_open = datecom.isopendate;
            //ViewBag.check_EnrollmentFeeSubmit_Close = datecom.isclosedate;
            //ViewBag.enrollStartdateValue = datecom.opendate;
            //ViewBag.ViewBagenrollExtenddateValue = datecom.closedate;
            //ViewBag.check_EnrollmentFeeSubmit_open = com.check_EnrollmentFeeSubmit_open(ad.GetAcademiccurrentSession().ID, Convert.ToInt32(CommonSetting.Commonid.Educationtype));
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
                //return RedirectToAction("PGGatewayEnrollment");
                return RedirectToAction("SelectGetwayRegistrationFee", "Exam");
            }
            else
            {
                ob.Status = false;
                TempData["msgfees"] = "Fees Already Submitted !!!";
                return RedirectToAction("EnrollmentFeesSubmit");
            }
            ViewBag.boardtype = CommonMethod.BoradtypePrevious().Where(x => x.boardid == 1 || x.boardid == 2);
            sub = obj.QualificationdetailList(1, 1000);
            List<StudentAdmissionQualification> asa = new List<StudentAdmissionQualification>();
            asa = sub.qlist.Where(x => x.QualicationType != 1).ToList();
            var boardtype1 = asa.FirstOrDefault().boardtype;
            if (boardtype1 == 0)
            {
                ViewBag.showpayment = false;
            }
            else
            {
                ViewBag.showpayment = false;
            }
            return View(obj1);
        }

        public ActionResult SelectGetwayRegistrationFee()
        {
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
                datecom = com.check_EnrollmentSubmit_check(ad.GetAcademiccurrentSession().ID, Convert.ToInt32(CommonSetting.Commonid.Educationtype));
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
                    //string SucccessUrl = " http://localhost:33166/Student/Exam/PGSucessEnrollmentEaseBuzz?CollegeId=" + "1000";

                    string SucccessUrl = "https://portal.DemoUniversity.com//Student/Exam/PGSucessEnrollmentEaseBuzz?CollegeId=" + "1000";


                    //string SucccessUrl = "PGSucessEnrollmentEaseBuzz?CollegeId=" + "1000";

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
                    var obj = sbi.encriptData(Amount1, EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENBApplicationNo")), "student/Exam/PGSucessEnrollment", "student/Exam/PGFailedEnrollment");

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
                datecom = com.check_EnrollmentSubmit_check(ad.GetAcademiccurrentSession().ID, Convert.ToInt32(CommonSetting.Commonid.Educationtype));
                ViewBag.check_EnrollmentFeeSubmit_open = datecom.isopendate;
                ViewBag.check_EnrollmentFeeSubmit_Close = datecom.isclosedate;
                ViewBag.enrollStartdateValue = datecom.opendate;
                ViewBag.ViewBagenrollExtenddateValue = datecom.closedate;
                //if ((Convert.ToInt32(EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENNBStID")))) == 21768 || (Convert.ToInt32(EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENNBStID")))) == 44545)
                //{
                //    ViewBag.check_EnrollmentFeeSubmit_open = true;
                //    ViewBag.check_EnrollmentFeeSubmit_Close = true;
                //}
                //else
                //{

                if (ViewBag.check_EnrollmentFeeSubmit_open == true && ViewBag.check_EnrollmentFeeSubmit_Close == true)
                {

                }
                else
                {
                    return RedirectToAction("EnrollmentFeesSubmit");
                }
                //}
                var result = stlogin.FeessubEncrytEnrollment(Convert.ToInt32(EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENNBStID"))));
                //if (result.migrationcertificate_iseligible == 1)
                //{

                //}
                //else
                //{
                //    return RedirectToAction("EnrollmentFeesSubmit");
                //}
                if (result != null)
                {
                    Amount1 = Convert.ToDecimal(result.Fees);
                    // Amount1 = 1;
                    var obj = sbi.encriptData(Amount1, EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENBApplicationNo")), "student/Exam/PGSucessEnrollment", "student/Exam/PGFailedEnrollment");

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
                                <td align=""left"" colspan=""3"" valign=""middle"">Subject: <strong> " + rec.StreamCategoryName + @"  </strong></td>
                            </tr>
                            <tr>
                                <td colspan=""3"" align=""center"" valign=""middle"">&nbsp;</td>
                            </tr>
                            <tr>
                                <td align=""left"" colspan=""3"" valign=""middle"">Subsidiary - 1:  <strong> " + rec.Subsidiary1_subject + @" </strong> , Subsidiary- 2:   <strong>" + rec.Subsidiary2_subject + @" </strong> </td>
                            </tr>
                            <tr>
                                <td colspan=""3"" align=""center"" valign=""middle"">&nbsp;</td>
                            </tr>
                            <tr>
                                <td align=""left"" colspan=""3"" valign=""middle"">
                                    Composition:  <strong>" + rec.Compulsory1_subject + @" 
                                    <span> " + ((rec.Compulsory2_subject == "" ? "(100 Marks)" : ("(50 Marks), " + rec.Compulsory2_subject + "(50 Marks)"))) + @" </strong></span>
                                </td>
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
        public ActionResult ExamFeesSubmitPrac()
        {
            //return RedirectToAction("Index", "Home");
            ExamForm ob = new ExamForm();
            AcademicSession ad = new AcademicSession();
            BL_PrintApplication PritApp = new BL_PrintApplication();

            Commn_master com = new Commn_master();
            Commn_master datecom = new Commn_master();
            datecom = com.check_ExamFeeSubmit_checkprac(ad.GetAcademiccurrentSession().ID, Convert.ToInt32(CommonSetting.Commonid.Educationtype));
            ViewBag.check_ExamFeeSubmit_open = datecom.isopendate;
            ViewBag.check_ExamFeeSubmit_Close = datecom.isclosedate;
            ViewBag.check_admissionopen = ViewBag.check_ExamFeeSubmit_open;

            var obj1 = ob.GetAppLicationDataForExamFeeprac(Convert.ToInt32(EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENNBStID"))));

            if (obj1.objExamFrom == null)
            {
                return RedirectToAction("Index", "Home");
            }
            if (obj1.objExamFrom.isfeesubmitregistration == 0)
            {
                return RedirectToAction("Index", "Home");
            }
            //if (obj1.objExamFrom.RegistrationNo == "")
            //{
            //    return RedirectToAction("Index", "Home");
            //}
            if (obj1.objExamFrom.RollNo == "")
            {
                return RedirectToAction("Index", "Home");
            }
            //if (obj1.objExamFrom.courseyearid != 31)
            //{
            //    return RedirectToAction("Index", "Home");
            //}
            if (obj1.objExamFrom.sessionid == 39) // only for 2 year student check 
            {
                //if (obj1.objExamFrom.Currentyear_courseyarid == 2 || obj1.objExamFrom.Currentyear_courseyarid == 5 || obj1.objExamFrom.Currentyear_courseyarid == 8)
                //{
                //    // first check exam fee payment before admission fee submit or not 
                //    //int a = ob.check_examfeebefore_admissionfee(obj1.objExamFrom.coursecategoryid, obj1.objExamFrom.StreamCategoryID, obj1.objExamFrom.Currentyear_courseyarid, obj1.objExamFrom.sessionid, obj1.objExamFrom.collegeid, obj1.objExamFrom.sid);
                //    //if (a <= 0)
                //    //{
                //    //    return RedirectToAction("Index", "Home");
                //    //}
                //}

            }
            StudentAdmissionQualification obj = new StudentAdmissionQualification();
            QualifiationMasterList sub = new QualifiationMasterList();

            List<EaxmFeesSubmit> feestruckture = new List<EaxmFeesSubmit>();
            List<EaxmFeesSubmit> subjectlist = new List<EaxmFeesSubmit>();
            List<EaxmFeesSubmit> Electivesubjectlist = new List<EaxmFeesSubmit>();
            feestruckture = ob.pracFeesDetailsstructure(obj1.objExamFrom.collegeid, obj1.objExamFrom.coursecategoryid, obj1.objExamFrom.sessionid, obj1.objExamFrom.castecategoryid, obj1.objExamFrom.StreamCategoryID, obj1.objExamFrom.courseyearid, 0);
            var tuple = new Tuple<PrintExamForm, QualifiationMasterList, List<EaxmFeesSubmit>, List<EaxmFeesSubmit>>(obj1, sub, feestruckture, subjectlist);
            return View(tuple);
        }

        [HttpPost]
        public ActionResult ExamFeesSubmitPrac(HttpPostedFileBase fileupload, int id = 0, string applyadmissionform = "", string Substreamcategoryid = "", string BackandEdit = "", string checkboxid = "")
        {
            StudentLogin stu = new StudentLogin();
            string ApplicationID = ClsLanguage.GetCookies("NBApplicationNo");
            Login lo = stu.BasicDetail(ApplicationID);
            ExamForm objexamfrom = new ExamForm();
            Login result = new Login();
            ExamForm ob = new ExamForm();
            AcademicSession ad = new AcademicSession();
            BL_PrintApplication PritApp = new BL_PrintApplication();
            var obj1 = ob.GetAppLicationDataForExamFeeprac(Convert.ToInt32(EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENNBStID"))));
            Commn_master com = new Commn_master();
            Commn_master datecom = new Commn_master();
            datecom = com.check_ExamFeeSubmit_checkprac(ad.GetAcademiccurrentSession().ID, Convert.ToInt32(CommonSetting.Commonid.Educationtype));
            ViewBag.check_ExamFeeSubmit_open = datecom.isopendate;
            ViewBag.check_ExamFeeSubmit_Close = datecom.isclosedate;
            ViewBag.check_admissionopen = ViewBag.check_ExamFeeSubmit_open;
            if (ViewBag.check_ExamFeeSubmit_open == true && ViewBag.check_ExamFeeSubmit_Close == true)
            {

            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
            if (obj1.objExamFrom == null)
            {
                return RedirectToAction("Index", "Home");
            }
            if (obj1.objExamFrom.isfeesubmitregistration == 0)
            {
                return RedirectToAction("Index", "Home");
            }
            if (obj1.objExamFrom.RegistrationNo == null || obj1.objExamFrom.RegistrationNo == "")
            {
                TempData["msgfees"] = "Registration no does not esist!!!";
                return RedirectToAction("ExamFeesSubmitPrac");
                return RedirectToAction("Index", "Home");
            }
            if (obj1.objExamFrom.RollNo == null || obj1.objExamFrom.RollNo == "")
            {
                TempData["msgfees"] = "Roll no does not esist!!!";
                return RedirectToAction("ExamFeesSubmitPrac");
                return RedirectToAction("Index", "Home");
            }
            if (obj1.Examobjfeesubmit.sessionid != 40)
            {
                // return RedirectToAction("Index", "Home");
            }
            // for double verification from college
            //if (applyadmissionform == "applyadmissionform")
            //{

            //    string fileuploadname = "";

            //    if (checkboxid == "")
            //    {
            //        return RedirectToAction("ExamFeesSubmit");
            //    }
            //    objexamfrom.student_examform_applyprac(obj1.objExamFrom.sid, obj1.objExamFrom.sessionid, obj1.objExamFrom.coursecategoryid, obj1.objExamFrom.collegeid, obj1.objExamFrom.StreamCategoryID, obj1.objExamFrom.courseyearid, Convert.ToInt32(0), Convert.ToInt32(0), "UG", fileuploadname);
            //    return RedirectToAction("ExamFeesSubmit");
            //}
            objexamfrom.student_examform_applyprac(obj1.objExamFrom.sid, obj1.objExamFrom.sessionid, obj1.objExamFrom.coursecategoryid, obj1.objExamFrom.collegeid, obj1.objExamFrom.StreamCategoryID, obj1.objExamFrom.courseyearid, Convert.ToInt32(0), Convert.ToInt32(0), "UG", "");
            if (obj1.objExamFrom.IsDocVerify == 1)
            {

            }
            else
            {
                return RedirectToAction("ExamFeesSubmitPrac");
            }

            StudentAdmissionQualification obj = new StudentAdmissionQualification();
            QualifiationMasterList sub = new QualifiationMasterList();

            List<EaxmFeesSubmit> feestruckture = new List<EaxmFeesSubmit>();
            List<EaxmFeesSubmit> subjectlist = new List<EaxmFeesSubmit>();
            List<EaxmFeesSubmit> Electivesubjectlist = new List<EaxmFeesSubmit>();
            feestruckture = ob.pracFeesDetailsstructure(obj1.objExamFrom.collegeid, obj1.objExamFrom.coursecategoryid, obj1.objExamFrom.sessionid, obj1.objExamFrom.castecategoryid, obj1.objExamFrom.StreamCategoryID, obj1.objExamFrom.courseyearid, 0);

            if (obj1.objExamFrom.IsExamfeesubmit == 0)
            {
                // TempData["msgfees"] = "Please wait till exam fee open date !!!";
                //return RedirectToAction("ExamFeesSubmit");
                return RedirectToAction("PGGatewayExamPrac");
            }
            else
            {
                FeesSubmit stlogin1 = new FeesSubmit();
                stlogin1.Status = false;
                TempData["msgfees"] = "Exam Fees Already Submitted !!!";
                return RedirectToAction("ExamFeesSubmitPrac", "Exam");
            }
            var tuple = new Tuple<PrintExamForm, QualifiationMasterList, List<EaxmFeesSubmit>, List<EaxmFeesSubmit>>(obj1, sub, feestruckture, subjectlist);
            return View(tuple);
        }
        public ActionResult PGGatewayExamPrac()
        {
            //  return RedirectToAction("Index", "Home");
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
                var obj1 = ob.GetAppLicationDataForExamFeeprac(Convert.ToInt32(EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENNBStID"))));
                feestruckture = ob.pracFeesDetailsstructure(obj1.objExamFrom.collegeid, obj1.objExamFrom.coursecategoryid, obj1.objExamFrom.sessionid, obj1.objExamFrom.castecategoryid, obj1.objExamFrom.StreamCategoryID, obj1.objExamFrom.courseyearid, 0);
                if (obj1.objExamFrom != null)
                {
                    AcademicSession ac = new AcademicSession();
                    int sessionid = ac.GetAcademiccurrentSession().ID;
                    int educationtype = Convert.ToInt32(CommonSetting.Commonid.Educationtype);
                    Commn_master datecom = new Commn_master();
                    datecom = com.check_ExamFeeSubmit_checkprac(ad.GetAcademiccurrentSession().ID, Convert.ToInt32(CommonSetting.Commonid.Educationtype));
                    if (datecom.isopendate == false)
                    {
                        return RedirectToAction("ExamFeesSubmitPrac", "Exam");
                    }
                    if (datecom.isclosedate == false)
                    {
                        return RedirectToAction("ExamFeesSubmitPrac", "Exam");
                    }


                    if (obj1.objExamFrom.IsDocVerify == 1)
                    {

                    }
                    else
                    { return RedirectToAction("Index", "Home"); }
                    if (obj1.objExamFrom.IsExamfeesubmit == 1) { return RedirectToAction("ExamFeesSubmitPrac"); }
                    Amount1 = Convert.ToDecimal(feestruckture.Sum(x => x.amount));
                    latefee = Convert.ToDecimal(feestruckture.Sum(x => x.late_fee));
                    amount_without_latefee = Amount1;
                    Amount1 = Amount1 + latefee;
                    if (Amount1 <= 0)
                    {
                        TempData["msgerror"] = "Amount Should be greater than 0 !!";
                        return RedirectToAction("ExamFeesSubmitPrac", "Exam");
                    }
                    //Amount1 = 1;
                    var obj = sbi.encriptDataExamPrac(Amount1, EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENBApplicationNo")), "student/Exam/PGSucessExamPrac", "student/Exam/PGFailedExamPrac", obj1.objExamFrom.YearName, latefee, amount_without_latefee, obj1.objExamFrom.courseyearid);
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
        public ActionResult ResponseExamPrac()
        {
            // return RedirectToAction("Index", "HomePG");

            try
            {
                ExamForm ob = new ExamForm();
                AcademicSession ad = new AcademicSession();
                // List<EaxmFeesSubmit> subjectlist = new List<EaxmFeesSubmit>();

                var obj1 = ob.GetAppLicationDataForExamFeeprac(Convert.ToInt32(EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENNBStID"))));
                return View(obj1);

            }
            catch (Exception ex)
            {
                CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "Response payment gateway get method for exam Fee on Controller error", ClsLanguage.GetCookies("NBApplicationNo"));
                PrintExamForm PritApp = new PrintExamForm();
                return View(PritApp);
            }
        }
        public ActionResult PGSucessExamPrac()
        {
            //return RedirectToAction("Index", "HomePG");
            string paramInfo = "";


            if (Request.Form.Count > 0)
            {
                try
                {
                    SbiPayExam sbi = new SbiPayExam();
                    var result = sbi.pgsucessdecrypt(Convert.ToInt32(EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENNBStID"))));
                    return RedirectToAction("ResponseExamPrac");
                }
                catch (Exception ex)
                {
                    CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "Payment PaymentGateway suucess url hit on Controller error for Exam Fees Submit", ClsLanguage.GetCookies("NBApplicationNo"));

                }
            }
            return RedirectToAction("ExamFeesSubmitPrac");
            return View();
        }

        public ActionResult PGFailedExamPrac()
        {
            //  return RedirectToAction("Index", "HomePG");
            string paramInfo = "";

            if (Request.Form.Count > 0)
            {
                try
                {
                    SbiPayExam sbi = new SbiPayExam();
                    var result = sbi.pgfaileddecrypt(Convert.ToInt32(EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENNBStID"))));
                    return RedirectToAction("ResponseExamPrac");
                }
                catch (Exception ex)
                {
                    CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "Payment PaymentGateway suucess url hit on Controller error for Exam Fees Submit", ClsLanguage.GetCookies("NBApplicationNo"));

                }
            }
            return RedirectToAction("ExamFeesSubmitPrac");
            return View();

        }
    }
}