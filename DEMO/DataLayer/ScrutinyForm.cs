using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class ScrutinyForm
    {
        public string StudentName { get; set; }
        public string StudentNameHindi { get; set; }
        public string CollegeName { get; set; }
        public string SessionName { get; set; }
        public string DOB { get; set; }
        public string MobileNo { get; set; }
        public string CurrentAddress { get; set; }
        public string FatherName { get; set; }
        public string MotherName { get; set; }
        public string sign { get; set; }
        public string photo { get; set; }
        public string streamCategory { get; set; }
        public string CourseCategory { get; set; }
        public string RegistrationNo { get; set; }
        public string YearName { get; set; }
        public string HonoursSubject { get; set; }
        public string Subsidiary1Subject { get; set; }
        public string Subsidiary2Subject { get; set; }
        public string Compulsory1Subject { get; set; }
        public string Compulsory2Subject { get; set; }

        public string HonoursId { get; set; }
        public string Subsidiary1Id { get; set; }
        public string Subsidiary2Id { get; set; }
        public string Compulsory1Id { get; set; }
        public string Compulsory2Id { get; set; }
        public bool IsScrutinyFee { get; set; }
        public string City { get; set; }
        public string RollNo { get; set; }
        public string ScrutinyCenter { get; set; }
        public bool IsAdmissionFee { get; set; }
        public bool Status { get; set; }
        public string Msg { get; set; }
        public string Email { get; set; }
        public int sid { get; set; }
        public int coursecategoryid { get; set; }
        public int sessionid { get; set; }
        public int collegeid { get; set; }
        public int StreamCategoryID { get; set; }
        public int courseyearid { get; set; }
        public string adddate { get; set; }
        public bool isappearedearlierfail { get; set; }
        public bool IsApplied { get; set; }
        public string IsAppliedDate { get; set; }
        public int IsDocVerify { get; set; }
        public string IsDocVerifyDate { get; set; }
        public int IsDocVerifyBy { get; set; }
        public string IsDocVerifyIP { get; set; }
        public int IsScrutinyfeesubmit { get; set; }
        public string IsScrutinyfeesubmitdate { get; set; }
        public string rejectreason { get; set; }
        public string rejectdate { get; set; }
        public string EnrollmentNo { get; set; }
        public string Nationality { get; set; }
        public string Religion { get; set; }
        public int ftitle { get; set; }
        public string castecategoryname { get; set; }
        public string permanentAddress { get; set; }
        public string formno { get; set; }
        public string currentyear { get; set; }
        public int isfeesubmitregistration { get; set; }
        public int castecategoryid { get; set; }
        public int electivesubjectid { get; set; }
        public string electivesubjectname { get; set; }
        public string electivesubjectname_2 { get; set; }
        public int Registrationyear { get; set; }
        public string fileupload { get; set; }
        public bool isconcession_apply { get; set; }
        public int id { get; set; }
        public int Currentyear_courseyarid { get; set; }
        public string Scrutinyyear { get; set; }
        public string ScrutinyStartDate { get; set; }
        public string subScrutinycenter { get; set; }
        public int migrationcertificate_iseligible { get; set; }
        public string migrationcertificate { get; set; }
        public string migrationRejectReaseon { get; set; }


        public ScrutinyForm StudentDetail()
        {
            ScrutinyForm Obj = new ScrutinyForm();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                int StID = (ClsLanguage.GetCookies("NBStID") != null ? Convert.ToInt32(ClsLanguage.GetCookies("NBStID")) : 0);
                int Sission = (ClsLanguage.GetCookies("NBSission") != null ? Convert.ToInt32(ClsLanguage.GetCookies("NBSission")) : 0);
                CommonMethod cmn = new CommonMethod();
                string ip = CommonMethod.GetIPAddress();
                Obj = conn.Query<ScrutinyForm>("[sp_student_ScrutinyFrom]", new { @Action = "StudentDetail", @SID = StID, @session = Sission }, commandType: CommandType.StoredProcedure).FirstOrDefault();

                return Obj;
            }
        }


        public ScrutinyForm student_Scrutinyform_apply(int sid, int Session, int coursecategoryid, int collegeid, int StreamCategoryID, int courseyearid, int isappearedearlierfail, int electivesubjectid, string type = "", string fileupload = "", int currentyear = 0, string AdmitCardPath = "", string DocumentPath = "", string subject1 = "", string subject2 = "", string subject3 = "", string subject4 = "", string Allsubject = "", string TotalSubject = "", int TotalAmount = 0, int id = 0)
        {
            ScrutinyForm Obj = new ScrutinyForm();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {

                CommonMethod cmn = new CommonMethod();
                string ip = CommonMethod.GetIPAddress();
                Obj = conn.Query<ScrutinyForm>("[sp_student_Scrutinyform_apply]", new
                {
                    @Action = "isapply",
                    @SID = sid,
                    @sessionid = Session,
                    @coursecategoryid = @coursecategoryid,
                    @collegeid = collegeid,
                    @StreamCategoryID = StreamCategoryID,
                    @courseyearid = courseyearid,
                    @isappearedearlierfail = isappearedearlierfail,
                    @electivesubjectid = electivesubjectid,
                    @type = type,
                    @fileupload = fileupload,                 
                    @subject1 = subject1,
                    @subject2 = subject2,
                    @subject3 = subject3,
                    @subject4 = subject4,
                    @AllSubject = Allsubject,
                    @AdmitCard = AdmitCardPath,
                    @DocumentUpload = DocumentPath,
                    @TotalSubject = TotalSubject,
                    @TotalAmount = TotalAmount

                }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return Obj;
            }
        }



        public ScrutinyForm student_Scrutinyform_applyprac(int sid, int Session, int coursecategoryid, int collegeid, int StreamCategoryID, int courseyearid, int isappearedearlierfail, int electivesubjectid, string type = "", string fileupload = "", int electivesubjectid_2 = 0)
        {
            ScrutinyForm Obj = new ScrutinyForm();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {

                CommonMethod cmn = new CommonMethod();
                string ip = CommonMethod.GetIPAddress();
                Obj = conn.Query<ScrutinyForm>("[sp_student_Scrutinyform_apply_prac]", new
                {
                    @Action = "isapply",
                    @SID = sid,
                    @sessionid = Session,
                    @coursecategoryid = coursecategoryid,
                    @collegeid = collegeid,
                    @StreamCategoryID = StreamCategoryID,
                    @courseyearid = courseyearid,
                    @isappearedearlierfail = isappearedearlierfail,
                    @electivesubjectid = electivesubjectid
                    ,
                    @type = type,
                    @fileupload = fileupload
                    ,
                    @electivesubjectid_2 = electivesubjectid_2
                }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return Obj;
            }
        }


        public ScrutinyForm student_Scrutinyform_apply_back(int sid, int Session, int coursecategoryid, int collegeid, int StreamCategoryID, int courseyearid, int isappearedearlierfail, int electivesubjectid, string type = "", string fileupload = "", int currentyear = 0, string AdmitCardPath = "", string DocumentPath = "", string subject1 = "", string subject2 = "", string subject3 = "", string subject4 = "", string Allsubject ="", string TotalSubject = "" , int TotalAmount =0 ,int id=0)
        {
            ScrutinyForm Obj = new ScrutinyForm();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {

                CommonMethod cmn = new CommonMethod();
                string ip = CommonMethod.GetIPAddress();
                Obj = conn.Query<ScrutinyForm>("[sp_student_Scrutinyform_apply_back]", new
                {
                    @Action = "isapply",
                    @SID = sid,
                    @sessionid = Session,
                    @coursecategoryid = @coursecategoryid,
                    @collegeid = collegeid,
                    @StreamCategoryID = StreamCategoryID,
                    @courseyearid = courseyearid,
                    @isappearedearlierfail = isappearedearlierfail,
                    @electivesubjectid = electivesubjectid ,
                    @type = type,
                    @fileupload = fileupload,
                    @currentyear = currentyear,
                    @subject1 = subject1,       
                    @subject2 = subject2,
                    @subject3 = subject3,
                    @subject4 = subject4,
                    @AllSubject = Allsubject,
                    @AdmitCard = AdmitCardPath,
                    @DocumentUpload = DocumentPath,
                    @TotalSubject = TotalSubject,
                    @TotalAmount = TotalAmount

                }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return Obj;
            }
        }


        public ScrutinyForm student_Scrutinyform_editfileupload(int sid, int id, string fileupload = "")
        {
            ScrutinyForm Obj = new ScrutinyForm();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {

                CommonMethod cmn = new CommonMethod();
                string ip = CommonMethod.GetIPAddress();
                Obj = conn.Query<ScrutinyForm>("[sp_student_Scrutinyform_apply]", new
                {
                    @Action = "editfileupload",
                    @SID = sid,
                    @id = id,
                    @fileupload = fileupload
                }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return Obj;
            }
        }
        public ScrutinyForm StudentDetailForAdmitCard(int courseyearid = 0)
        {
            ScrutinyForm Obj = new ScrutinyForm();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                int StID = (ClsLanguage.GetCookies("NBStID") != null ? Convert.ToInt32(ClsLanguage.GetCookies("NBStID")) : 0);
                int Sission = (ClsLanguage.GetCookies("NBSission") != null ? Convert.ToInt32(ClsLanguage.GetCookies("NBSission")) : 0);
                CommonMethod cmn = new CommonMethod();
                string ip = CommonMethod.GetIPAddress();
                Obj = conn.Query<ScrutinyForm>("[sp_student_ScrutinyFrom]", new { @Action = "StudentDetailForAdmitCard", @SID = StID, @session = Sission, @courseyearid = courseyearid }, commandType: CommandType.StoredProcedure).FirstOrDefault();

                return Obj;
            }
        }
        public ScrutinyForm StudentDetailForAdmitCard_backyear(int courseyearid = 0)
        {
            ScrutinyForm Obj = new ScrutinyForm();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                int StID = (ClsLanguage.GetCookies("NBStID") != null ? Convert.ToInt32(ClsLanguage.GetCookies("NBStID")) : 0);
                int Sission = (ClsLanguage.GetCookies("NBSission") != null ? Convert.ToInt32(ClsLanguage.GetCookies("NBSission")) : 0);
                CommonMethod cmn = new CommonMethod();
                string ip = CommonMethod.GetIPAddress();
                Obj = conn.Query<ScrutinyForm>("[sp_student_ScrutinyFrom_back]", new { @Action = "StudentDetailForAdmitCard", @SID = StID, @session = Sission, @currentyear = System.DateTime.Now.Year, @courseyearid = courseyearid }, commandType: CommandType.StoredProcedure).FirstOrDefault();

                return Obj;
            }
        }
        public PrintScrutinyForm GetAppLicationDataForScrutinyFeeprac(int id)
        {
            //  int StID = (ClsLanguage.GetCookies("NBStID") != null ? Convert.ToInt32(ClsLanguage.GetCookies("NBStID")) : 0);
            int Sission = (ClsLanguage.GetCookies("NBSission") != null ? Convert.ToInt32(ClsLanguage.GetCookies("NBSission")) : 0);
            PrintScrutinyForm objdata = new PrintScrutinyForm();
            ScrutinyFeesSubmit stlogin = new ScrutinyFeesSubmit();
            BL_PrintRecipt PritFee = new BL_PrintRecipt();
            ScrutinyForm ob = new ScrutinyForm();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var obj = conn.QueryMultiple("sp_student_ScrutinyFromprac", new { @Action = "StudentDetail", @SID = id, @session = Sission }, commandTimeout: 120545, commandType: CommandType.StoredProcedure);
                if (obj != null)
                {
                    objdata.objScrutinyFrom = obj.Read<ScrutinyForm>().FirstOrDefault();
                    objdata.Scrutinyobjfeesubmit = stlogin.pracScrutinyFeesDetail();
                    objdata.objPrintRecipt = PritFee.GetPaymentReciptScrutinyFeeprac();
                    if (objdata.objScrutinyFrom != null)
                    {
                        objdata.subjectlist = ob.FeesDetailSubjectlistprac(objdata.objScrutinyFrom.coursecategoryid, objdata.objScrutinyFrom.StreamCategoryID, objdata.objScrutinyFrom.courseyearid);
                    }
                }
                return objdata;
            }
        }

        public PrintScrutinyForm GetAppLicationDataForScrutinyFee(int id)
        {
            //  int StID = (ClsLanguage.GetCookies("NBStID") != null ? Convert.ToInt32(ClsLanguage.GetCookies("NBStID")) : 0);
            int Sission = (ClsLanguage.GetCookies("NBSission") != null ? Convert.ToInt32(ClsLanguage.GetCookies("NBSission")) : 0);
            PrintScrutinyForm objdata = new PrintScrutinyForm();
            ScrutinyFeesSubmit stlogin = new ScrutinyFeesSubmit();
            BL_PrintRecipt PritFee = new BL_PrintRecipt();
            ScrutinyForm ob = new ScrutinyForm();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var obj = conn.QueryMultiple("sp_student_ScrutinyFrom", new { @Action = "StudentDetail", @SID = id, @session = Sission }, commandTimeout: 120545, commandType: CommandType.StoredProcedure);
                if (obj != null)
                {
                    objdata.objScrutinyFrom = obj.Read<ScrutinyForm>().FirstOrDefault();
                    objdata.Scrutinyobjfeesubmit = stlogin.ScrutinyFeesDetail();
                    objdata.objPrintRecipt = PritFee.GetPaymentReciptScrutinyFee();
                    if (objdata.objScrutinyFrom != null)
                    {
                        CommonMethod.TraceLogWritetoNotepad("obj1.objScrutinyFrom1", "Scrutiny/ScrutinyFeesSubmit51", "obj1.objScrutinyFrom", "Id");
                        objdata.subjectlist = ob.FeesDetailSubjectlist(objdata.objScrutinyFrom.coursecategoryid, objdata.objScrutinyFrom.StreamCategoryID, objdata.objScrutinyFrom.courseyearid);
                    }
                }
                return objdata;
            }
        }

        public PrintScrutinyForm AirPayGetAppLicationDataForScrutinyFee()
        {

            //int id,int sessionid
            //  int StID = (ClsLanguage.GetCookies("NBStID") != null ? Convert.ToInt32(ClsLanguage.GetCookies("NBStID")) : 0);
            int Sission = (ClsLanguage.GetCookies("NBSission") != null ? Convert.ToInt32(ClsLanguage.GetCookies("NBSission")) : 0);
            PrintScrutinyForm objdata = new PrintScrutinyForm();
            ScrutinyFeesSubmit stlogin = new ScrutinyFeesSubmit();
            BL_PrintRecipt PritFee = new BL_PrintRecipt();
            ScrutinyForm ob = new ScrutinyForm();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var obj = conn.QueryMultiple("sp_student_ScrutinyFrom", new { @Action = "StudentDetail", @SID = id, @session = Sission }, commandTimeout: 120545, commandType: CommandType.StoredProcedure);
                if (obj != null)
                {
                    objdata.objScrutinyFrom = obj.Read<ScrutinyForm>().FirstOrDefault();
                    objdata.Scrutinyobjfeesubmit = stlogin.ScrutinyFeesDetail();
                    objdata.objPrintRecipt = PritFee.GetPaymentReciptScrutinyFee();
                    if (objdata.objScrutinyFrom != null)
                    {
                        objdata.subjectlist = ob.FeesDetailSubjectlist(objdata.objScrutinyFrom.coursecategoryid, objdata.objScrutinyFrom.StreamCategoryID, objdata.objScrutinyFrom.courseyearid);
                    }
                }
                return objdata;
            }
        }

        public PrintScrutinyForm SAfexPayGetAppLicationDataForScrutinyFee()
        {

            //int id,int sessionid
            //  int StID = (ClsLanguage.GetCookies("NBStID") != null ? Convert.ToInt32(ClsLanguage.GetCookies("NBStID")) : 0);
            int Sission = (ClsLanguage.GetCookies("NBSission") != null ? Convert.ToInt32(ClsLanguage.GetCookies("NBSission")) : 0);
            PrintScrutinyForm objdata = new PrintScrutinyForm();
            ScrutinyFeesSubmit stlogin = new ScrutinyFeesSubmit();
            BL_PrintRecipt PritFee = new BL_PrintRecipt();
            ScrutinyForm ob = new ScrutinyForm();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var obj = conn.QueryMultiple("sp_student_ScrutinyFrom", new { @Action = "StudentDetail", @SID = id, @session = Sission }, commandTimeout: 120545, commandType: CommandType.StoredProcedure);
                if (obj != null)
                {
                    objdata.objScrutinyFrom = obj.Read<ScrutinyForm>().FirstOrDefault();
                    objdata.Scrutinyobjfeesubmit = stlogin.ScrutinyFeesDetail();
                    objdata.objPrintRecipt = PritFee.GetPaymentReciptScrutinyFee();
                    if (objdata.objScrutinyFrom != null)
                    {
                        objdata.subjectlist = ob.FeesDetailSubjectlist(objdata.objScrutinyFrom.coursecategoryid, objdata.objScrutinyFrom.StreamCategoryID, objdata.objScrutinyFrom.courseyearid);
                    }
                }
                return objdata;
            }
        }
        public PrintScrutinyForm backGetAppLicationDataForScrutinyFee(int id, int courseyearid, int currentyear)
        {
            //jitendra check
            //  int StID = (ClsLanguage.GetCookies("NBStID") != null ? Convert.ToInt32(ClsLanguage.GetCookies("NBStID")) : 0);
            int Sission = (ClsLanguage.GetCookies("NBSission") != null ? Convert.ToInt32(ClsLanguage.GetCookies("NBSission")) : 0);
            PrintScrutinyForm objdata = new PrintScrutinyForm();
            ScrutinyFeesSubmit stlogin = new ScrutinyFeesSubmit();
            BL_PrintRecipt PritFee = new BL_PrintRecipt();
            ScrutinyForm ob = new ScrutinyForm();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var obj = conn.QueryMultiple("[sp_student_ScrutinyFrom_back]", new { @Action = "StudentDetail", @SID = id, @session = Sission, @courseyearid = courseyearid, @currentyear = currentyear }, commandType: CommandType.StoredProcedure);
                if (obj != null)
                {
                    objdata.objScrutinyFrom = obj.Read<ScrutinyForm>().FirstOrDefault();
                    objdata.Scrutinyobjfeesubmit = stlogin.backScrutinyFeesDetail(objdata.objScrutinyFrom.courseyearid);
                    objdata.objPrintRecipt = PritFee.backGetPaymentReciptScrutinyFee(objdata.objScrutinyFrom.courseyearid);
                    if (objdata.objScrutinyFrom != null)
                    {
                        objdata.subjectlist = ob.backFeesDetailSubjectlist_LLB(objdata.objScrutinyFrom.coursecategoryid, objdata.objScrutinyFrom.StreamCategoryID, objdata.objScrutinyFrom.courseyearid, objdata.objScrutinyFrom.sessionid, objdata.objScrutinyFrom.collegeid, objdata.objScrutinyFrom.sid);
                    }
                }
                return objdata;
            }
        }
        public PrintScrutinyForm backGetAppLicationDataForScrutinyFee_LLB(int id, int courseyearid, int currentyear)
        {
            //  int StID = (ClsLanguage.GetCookies("NBStID") != null ? Convert.ToInt32(ClsLanguage.GetCookies("NBStID")) : 0);
            int Sission = (ClsLanguage.GetCookies("NBSission") != null ? Convert.ToInt32(ClsLanguage.GetCookies("NBSission")) : 0);
            PrintScrutinyForm objdata = new PrintScrutinyForm();
            ScrutinyFeesSubmit stlogin = new ScrutinyFeesSubmit();
            BL_PrintRecipt PritFee = new BL_PrintRecipt();
            ScrutinyForm ob = new ScrutinyForm();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var obj = conn.QueryMultiple("[sp_student_ScrutinyFrom_back]", new { @Action = "StudentDetail", @SID = id, @session = Sission, @courseyearid = courseyearid, @currentyear = currentyear }, commandType: CommandType.StoredProcedure);
                if (obj != null)
                {
                    objdata.objScrutinyFrom = obj.Read<ScrutinyForm>().FirstOrDefault();
                    objdata.Scrutinyobjfeesubmit = stlogin.backScrutinyFeesDetail(objdata.objScrutinyFrom.courseyearid);
                    objdata.objPrintRecipt = PritFee.backGetPaymentReciptScrutinyFee(objdata.objScrutinyFrom.courseyearid);
                    if (objdata.objScrutinyFrom != null)
                    {
                        objdata.subjectlist = ob.backFeesDetailSubjectlist_LLB(objdata.objScrutinyFrom.coursecategoryid, objdata.objScrutinyFrom.StreamCategoryID, objdata.objScrutinyFrom.courseyearid, objdata.objScrutinyFrom.sessionid, objdata.objScrutinyFrom.collegeid, objdata.objScrutinyFrom.sid);
                    }
                }
                return objdata;
            }

        }

        public PrintScrutinyForm backGetAppLicationDataForScrutinyFee_BPharma(int id, int courseyearid, int currentyear)
        {
            //  int StID = (ClsLanguage.GetCookies("NBStID") != null ? Convert.ToInt32(ClsLanguage.GetCookies("NBStID")) : 0);
            int Sission = (ClsLanguage.GetCookies("NBSission") != null ? Convert.ToInt32(ClsLanguage.GetCookies("NBSission")) : 0);
            PrintScrutinyForm objdata = new PrintScrutinyForm();
            ScrutinyFeesSubmit stlogin = new ScrutinyFeesSubmit();
            BL_PrintRecipt PritFee = new BL_PrintRecipt();
            ScrutinyForm ob = new ScrutinyForm();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var obj = conn.QueryMultiple("[sp_student_ScrutinyFrom_back]", new { @Action = "StudentDetail", @SID = id, @session = Sission, @courseyearid = courseyearid, @currentyear = currentyear }, commandType: CommandType.StoredProcedure);
                if (obj != null)
                {
                    objdata.objScrutinyFrom = obj.Read<ScrutinyForm>().FirstOrDefault();
                    objdata.Scrutinyobjfeesubmit = stlogin.backScrutinyFeesDetail(objdata.objScrutinyFrom.courseyearid);
                    objdata.objPrintRecipt = PritFee.backGetPaymentReciptScrutinyFee(objdata.objScrutinyFrom.courseyearid);
                    if (objdata.objScrutinyFrom != null)
                    {
                        objdata.subjectlist = ob.backFeesDetailSubjectlist_Bed(objdata.objScrutinyFrom.coursecategoryid, objdata.objScrutinyFrom.StreamCategoryID, objdata.objScrutinyFrom.courseyearid, objdata.objScrutinyFrom.sessionid, objdata.objScrutinyFrom.collegeid, objdata.objScrutinyFrom.sid);
                    }
                }
                return objdata;
            }

        }

        public PrintScrutinyForm backGetAppLicationDataForScrutinyFee_BEd(int id, int courseyearid, int currentyear)
        {
            //  int StID = (ClsLanguage.GetCookies("NBStID") != null ? Convert.ToInt32(ClsLanguage.GetCookies("NBStID")) : 0);
            int Sission = (ClsLanguage.GetCookies("NBSission") != null ? Convert.ToInt32(ClsLanguage.GetCookies("NBSission")) : 0);
            PrintScrutinyForm objdata = new PrintScrutinyForm();
            ScrutinyFeesSubmit stlogin = new ScrutinyFeesSubmit();
            BL_PrintRecipt PritFee = new BL_PrintRecipt();
            ScrutinyForm ob = new ScrutinyForm();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var obj = conn.QueryMultiple("[sp_student_ScrutinyFrom_back]", new { @Action = "StudentDetail", @SID = id, @session = Sission, @courseyearid = courseyearid, @currentyear = currentyear }, commandType: CommandType.StoredProcedure);
                if (obj != null)
                {
                    objdata.objScrutinyFrom = obj.Read<ScrutinyForm>().FirstOrDefault();
                    objdata.Scrutinyobjfeesubmit = stlogin.backScrutinyFeesDetail(objdata.objScrutinyFrom.courseyearid);
                    objdata.objPrintRecipt = PritFee.backGetPaymentReciptScrutinyFee(objdata.objScrutinyFrom.courseyearid);
                    if (objdata.objScrutinyFrom != null)
                    {
                        objdata.subjectlist = ob.backFeesDetailSubjectlist_Bed(objdata.objScrutinyFrom.coursecategoryid, objdata.objScrutinyFrom.StreamCategoryID, objdata.objScrutinyFrom.courseyearid, objdata.objScrutinyFrom.sessionid, objdata.objScrutinyFrom.collegeid, objdata.objScrutinyFrom.sid);
                    }
                }
                return objdata;
            }

        }
        public PrintScrutinyForm backGetAppLicationDataForScrutinyFee_PG(int id, int courseyearid, int currentyear)
        {
            //  int StID = (ClsLanguage.GetCookies("NBStID") != null ? Convert.ToInt32(ClsLanguage.GetCookies("NBStID")) : 0);
            int Sission = (ClsLanguage.GetCookies("NBSission") != null ? Convert.ToInt32(ClsLanguage.GetCookies("NBSission")) : 0);
            PrintScrutinyForm objdata = new PrintScrutinyForm();
            ScrutinyFeesSubmit stlogin = new ScrutinyFeesSubmit();
            BL_PrintRecipt PritFee = new BL_PrintRecipt();
            ScrutinyForm ob = new ScrutinyForm();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var obj = conn.QueryMultiple("[sp_student_ScrutinyFrom_back]", new { @Action = "StudentDetail", @SID = id, @session = Sission, @courseyearid = courseyearid, @currentyear = currentyear }, commandType: CommandType.StoredProcedure);
                if (obj != null)
                {
                    objdata.objScrutinyFrom = obj.Read<ScrutinyForm>().FirstOrDefault();
                    objdata.Scrutinyobjfeesubmit = stlogin.backScrutinyFeesDetail(objdata.objScrutinyFrom.courseyearid);
                    objdata.objPrintRecipt = PritFee.backGetPaymentReciptScrutinyFee(objdata.objScrutinyFrom.courseyearid);
                    if (objdata.objScrutinyFrom != null)
                    {
                        objdata.subjectlist = ob.backFeesDetailSubjectlist_pg(objdata.objScrutinyFrom.coursecategoryid, objdata.objScrutinyFrom.StreamCategoryID, objdata.objScrutinyFrom.courseyearid, objdata.objScrutinyFrom.sessionid, objdata.objScrutinyFrom.collegeid, objdata.objScrutinyFrom.sid);
                    }
                }
                return objdata;
            }

        }
        public PrintScrutinyForm backGetAppLicationDataForScrutinyFeeUG(int id, int courseyearid, int currentyear)
        {
            int Sission = (ClsLanguage.GetCookies("NBSission") != null ? Convert.ToInt32(ClsLanguage.GetCookies("NBSission")) : 0);
            PrintScrutinyForm objdata = new PrintScrutinyForm();
            ScrutinyFeesSubmit stlogin = new ScrutinyFeesSubmit();
            BL_PrintRecipt PritFee = new BL_PrintRecipt();
            ScrutinyForm ob = new ScrutinyForm();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var obj = conn.QueryMultiple("[sp_student_ScrutinyFrom_back]", new { @Action = "StudentDetailUG", @SID = id, @session = Sission, @courseyearid = courseyearid, @currentyear = currentyear }, commandTimeout: 120545, commandType: CommandType.StoredProcedure);
                if (obj != null)
                {
                    objdata.objScrutinyFrom = obj.Read<ScrutinyForm>().FirstOrDefault();
                    objdata.Scrutinyobjfeesubmit = stlogin.backScrutinyFeesDetail(courseyearid);
                    objdata.objPrintRecipt = PritFee.backGetPaymentReciptScrutinyFee(courseyearid);
                    var paymentstatus = objdata.objScrutinyFrom.IsScrutinyfeesubmit;
                    if (objdata.objScrutinyFrom != null)
                    {
                        objdata.subjectlist = ob.backFeesDetailSubjectlist_UG(objdata.objScrutinyFrom.coursecategoryid, objdata.objScrutinyFrom.StreamCategoryID, courseyearid, objdata.objScrutinyFrom.sessionid, objdata.objScrutinyFrom.collegeid, objdata.objScrutinyFrom.sid);
                    }
                }
                return objdata;
            }
        }

        public PrintScrutinyForm backGetAppLicationDataForScrutinyFeeUG_back(int id, int courseyearid, int currentyear)
        {
            int Sission = (ClsLanguage.GetCookies("NBSission") != null ? Convert.ToInt32(ClsLanguage.GetCookies("NBSission")) : 0);
            PrintScrutinyForm objdata = new PrintScrutinyForm();
            ScrutinyFeesSubmit stlogin = new ScrutinyFeesSubmit();
            BL_PrintRecipt PritFee = new BL_PrintRecipt();
            ScrutinyForm ob = new ScrutinyForm();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var obj = conn.QueryMultiple("[sp_student_ScrutinyFrom_back]", new { @Action = "StudentDetailUG_back", @SID = id, @session = Sission, @courseyearid = courseyearid, @currentyear = currentyear }, commandTimeout: 120545, commandType: CommandType.StoredProcedure);
                if (obj != null)
                {
                    objdata.objScrutinyFrom = obj.Read<ScrutinyForm>().FirstOrDefault();
                    objdata.Scrutinyobjfeesubmit = stlogin.backScrutinyFeesDetail(courseyearid);
                    objdata.objPrintRecipt = PritFee.backGetPaymentReciptScrutinyFee(courseyearid);
                    var paymentstatus = objdata.objScrutinyFrom.IsScrutinyfeesubmit;
                    if (objdata.objScrutinyFrom != null)
                    {
                        objdata.subjectlist = ob.backFeesDetailSubjectlist_UG(objdata.objScrutinyFrom.coursecategoryid, objdata.objScrutinyFrom.StreamCategoryID, courseyearid, objdata.objScrutinyFrom.sessionid, objdata.objScrutinyFrom.collegeid, objdata.objScrutinyFrom.sid);
                    }
                }
                return objdata;
            }
        }


        public PrintScrutinyForm GetAppLicationDataForEnrollmentFee(int id = 0)
        {
            int StID = (ClsLanguage.GetCookies("NBStID") != null ? Convert.ToInt32(ClsLanguage.GetCookies("NBStID")) : 0);
            int Sission = (ClsLanguage.GetCookies("NBSission") != null ? Convert.ToInt32(ClsLanguage.GetCookies("NBSission")) : 0);
            PrintScrutinyForm objdata = new PrintScrutinyForm();
            FeesSubmit stlogin = new FeesSubmit();
            BL_PrintRecipt PritFee = new BL_PrintRecipt();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var obj = conn.QueryMultiple("[sp_EnrollmentRequest]", new { @Action = "StudentDetail", @SID = StID, @session = Sission }, commandType: CommandType.StoredProcedure);
                if (obj != null)
                {
                    objdata.objScrutinyFrom = obj.Read<ScrutinyForm>().FirstOrDefault();
                    objdata.objfeesubmit = stlogin.EnrollmentFeesDetail();
                    objdata.objPrintRecipt = PritFee.GetPaymentReciptEnrollmentFee();
                }
                return objdata;
            }
        }
        public Login BasicDetail()
        {
            int StID = (ClsLanguage.GetCookies("NBStID") != null ? Convert.ToInt32(ClsLanguage.GetCookies("NBStID")) : 0);
            int Sission = (ClsLanguage.GetCookies("NBSission") != null ? Convert.ToInt32(ClsLanguage.GetCookies("NBSission")) : 0);
            Login obj = new Login();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                CommonMethod cmn = new CommonMethod();

                obj = conn.Query<Login>("[sp_student_ScrutinyFrom]", new { @Action = "StudentFeesDetail", @SID = StID, @session = Sission }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return obj;
            }
        }
        public FeesSubmit FeessubStudenttest()
        {
            FeesSubmit ObjFees = new FeesSubmit();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                int StID = Convert.ToInt32(ClsLanguage.GetCookies("NBStID"));
                string ApplicationNo = ClsLanguage.GetCookies("NBApplicationNo");
                if (StID == 0)
                {
                    ObjFees.Status = false;
                    ObjFees.Message = "Network Error !!";
                    return ObjFees;
                }
                int Sission = Convert.ToInt32(ClsLanguage.GetCookies("NBSission"));
                CommonMethod cmn = new CommonMethod();
                //ObjFees = conn.Query<FeesSubmit>("[sp_StudentRegistration]", new { @Action = "FeesSubmit", @ApplicID = ApplicationNo.ToString(), @Id = StID }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                //if (ObjFees.Id <= 0)
                //{
                //    ObjFees.Status = false;

                //}
                return ObjFees;
            }
        }
        public List<ScrutinyFeesSubmit> FeesDetailsstructure(int collegeid, int courseCategoryid, int sessionid, int castecategory, int SID, int courseyearid, int isconsession = 0)
        {
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                CommonMethod cmn = new CommonMethod();
                string ip = CommonMethod.GetIPAddress();
                var ObjFees = conn.Query<ScrutinyFeesSubmit>("[sp_student_ScrutinyFrom]", new { @Action = "StudentScrutinyStuctureFeesDetail", @collegeid = collegeid, @session = sessionid, @courseCategoryid = courseCategoryid, @castecategory = castecategory, @SID = SID, @courseyearid = courseyearid, @isconsession = isconsession }, commandType: CommandType.StoredProcedure).ToList();
                return ObjFees;
            }
        }
        public List<ScrutinyFeesSubmit> pracFeesDetailsstructure(int collegeid, int courseCategoryid, int sessionid, int castecategory, int streamCategoryid, int courseyearid, int isconsession = 0)
        {
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                CommonMethod cmn = new CommonMethod();
                string ip = CommonMethod.GetIPAddress();
                var ObjFees = conn.Query<ScrutinyFeesSubmit>("[sp_student_ScrutinyFromprac]", new { @Action = "StudentScrutinyStuctureFeesDetail", @collegeid = collegeid, @session = sessionid, @courseCategoryid = courseCategoryid, @castecategory = castecategory, @streamCategoryid = streamCategoryid, @courseyearid = courseyearid, @isconsession = isconsession }, commandType: CommandType.StoredProcedure).ToList();
                return ObjFees;
            }
        }
        public List<ScrutinyFeesSubmit> backFeesDetailsstructure(int collegeid, int courseCategoryid, int sessionid, int castecategory, int streamCategoryid, int courseyearid, int isconsession = 0)
        {
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                CommonMethod cmn = new CommonMethod();
                string ip = CommonMethod.GetIPAddress();
                var ObjFees = conn.Query<ScrutinyFeesSubmit>("[sp_student_ScrutinyFrom_back]", new { @Action = "StudentScrutinyStuctureFeesDetail", @collegeid = collegeid, @session = sessionid, @courseCategoryid = courseCategoryid, @castecategory = castecategory, @streamCategoryid = streamCategoryid, @courseyearid = courseyearid, @isconsession = isconsession }, commandType: CommandType.StoredProcedure).ToList();
                return ObjFees;
            }
        }
        public List<ScrutinyFeesSubmit> FeesDetailSubjectlist(int courseCategoryid, int streamCategoryid, int courseyearid)
        {
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                CommonMethod cmn = new CommonMethod();
                string ip = CommonMethod.GetIPAddress();
                var ObjFees = conn.Query<ScrutinyFeesSubmit>("[sp_student_ScrutinyFrom]", new { @Action = "subjectlist", @courseCategoryid = courseCategoryid, @streamCategoryid = streamCategoryid, @courseyearid = courseyearid }, commandTimeout: 120545, commandType: CommandType.StoredProcedure).ToList();
                return ObjFees;
            }
        }
        public List<ScrutinyFeesSubmit> FeesDetailSubjectlistprac(int courseCategoryid, int streamCategoryid, int courseyearid)
        {
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                CommonMethod cmn = new CommonMethod();
                string ip = CommonMethod.GetIPAddress();
                var ObjFees = conn.Query<ScrutinyFeesSubmit>("[sp_student_ScrutinyFromprac]", new { @Action = "subjectlist", @courseCategoryid = courseCategoryid, @streamCategoryid = streamCategoryid, @courseyearid = courseyearid }, commandTimeout: 120545, commandType: CommandType.StoredProcedure).ToList();
                return ObjFees;
            }
        }
        public int check_Scrutinyfeebefore_admissionfee(int courseCategoryid, int streamCategoryid, int courseyearid, int session, int collegeid, int SID)
        {
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                CommonMethod cmn = new CommonMethod();
                ScrutinyFeesSubmit obj = new ScrutinyFeesSubmit();
                string ip = CommonMethod.GetIPAddress();

                var ObjFees = conn.Query<int>("[sp_check_Scrutinyfeebefore_admissionfee]", new { @Action = "check_Scrutinyfeebefore_admissionfee", @courseCategoryid = courseCategoryid, @streamCategoryid = streamCategoryid, @courseyearid = courseyearid, @sessionid = session, @collegeid = @collegeid, @SID = SID }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return ObjFees;
            }
        }
        public List<ScrutinyFeesSubmit> backFeesDetailSubjectlist(int courseCategoryid, int streamCategoryid, int courseyearid, int session, int collegeid, int SID)
        {
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                CommonMethod cmn = new CommonMethod();
                List<ScrutinyFeesSubmit> obj = new List<ScrutinyFeesSubmit>();
                string ip = CommonMethod.GetIPAddress();
                if (courseCategoryid == Convert.ToInt32(CommonSetting.coursecategory.bca))
                {
                    var ObjFees = conn.Query<ScrutinyFeesSubmit>("[sp_student_ScrutinyFrom_back]", new { @Action = "findBCA_backpaper", @courseCategoryid = courseCategoryid, @streamCategoryid = 1124, @courseyearid = courseyearid, @session = session, @collegeid = @collegeid, @SID = SID }, commandType: CommandType.StoredProcedure).ToList();
                    obj = ObjFees;
                }
                return obj;
            }
        }
        public List<ScrutinyFeesSubmit> backFeesDetailSubjectlist_LLB(int courseCategoryid, int streamCategoryid, int courseyearid, int session, int collegeid, int SID)
        {
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                CommonMethod cmn = new CommonMethod();
                List<ScrutinyFeesSubmit> obj = new List<ScrutinyFeesSubmit>();
                string ip = CommonMethod.GetIPAddress();
                if (courseCategoryid == Convert.ToInt32(CommonSetting.coursecategory.LLB))
                {
                    var ObjFees = conn.Query<ScrutinyFeesSubmit>("[sp_student_ScrutinyFrom_back]", new { @Action = "findLLB_backpaper", @courseCategoryid = courseCategoryid, @streamCategoryid = streamCategoryid, @courseyearid = courseyearid, @session = session, @collegeid = @collegeid, @SID = SID }, commandType: CommandType.StoredProcedure).ToList();
                    obj = ObjFees;
                }
                return obj;
            }
        }

        public List<ScrutinyFeesSubmit> backFeesDetailSubjectlist_Vocational(int courseCategoryid, int streamCategoryid, int courseyearid, int session, int collegeid, int SID)
        {
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                CommonMethod cmn = new CommonMethod();
                List<ScrutinyFeesSubmit> obj = new List<ScrutinyFeesSubmit>();
                string ip = CommonMethod.GetIPAddress();
                if (courseCategoryid == Convert.ToInt32(CommonSetting.coursecategory.bba) || courseCategoryid == Convert.ToInt32(CommonSetting.coursecategory.BioTech) || courseCategoryid == Convert.ToInt32(CommonSetting.coursecategory.bca))
                {
                    var ObjFees = conn.Query<ScrutinyFeesSubmit>("[sp_student_ScrutinyFrom_back]", new { @Action = "findVocational_backpaper", @courseCategoryid = courseCategoryid, @streamCategoryid = streamCategoryid, @courseyearid = courseyearid, @session = session, @collegeid = @collegeid, @SID = SID }, commandTimeout: 120545, commandType: CommandType.StoredProcedure).ToList();
                    obj = ObjFees;
                }
                return obj;
            }
        }

        public List<ScrutinyFeesSubmit> backFeesDetailSubjectlist_BPharma(int courseCategoryid, int streamCategoryid, int courseyearid, int session, int collegeid, int SID)
        {
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                CommonMethod cmn = new CommonMethod();
                List<ScrutinyFeesSubmit> obj = new List<ScrutinyFeesSubmit>();
                string ip = CommonMethod.GetIPAddress();
                if (courseCategoryid == Convert.ToInt32(CommonSetting.coursecategory.BPharma))
                {
                    var ObjFees = conn.Query<ScrutinyFeesSubmit>("[sp_student_ScrutinyFrom_back]", new { @Action = "findBEd_backpaper", @courseCategoryid = courseCategoryid, @streamCategoryid = streamCategoryid, @courseyearid = courseyearid, @session = session, @collegeid = @collegeid, @SID = SID }, commandType: CommandType.StoredProcedure).ToList();
                    obj = ObjFees;
                }
                return obj;
            }
        }

        public List<ScrutinyFeesSubmit> backFeesDetailSubjectlist_Bed(int courseCategoryid, int streamCategoryid, int courseyearid, int session, int collegeid, int SID)
        {
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                CommonMethod cmn = new CommonMethod();
                List<ScrutinyFeesSubmit> obj = new List<ScrutinyFeesSubmit>();
                string ip = CommonMethod.GetIPAddress();
                if (courseCategoryid == Convert.ToInt32(CommonSetting.coursecategory.BEd))
                {
                    var ObjFees = conn.Query<ScrutinyFeesSubmit>("[sp_student_ScrutinyFrom_back]", new { @Action = "findBEd_backpaper", @courseCategoryid = courseCategoryid, @streamCategoryid = streamCategoryid, @courseyearid = courseyearid, @session = session, @collegeid = @collegeid, @SID = SID }, commandType: CommandType.StoredProcedure).ToList();
                    obj = ObjFees;
                }
                return obj;
            }
        }


        public List<ScrutinyFeesSubmit> backFeesDetailSubjectlist_pg(int courseCategoryid, int streamCategoryid, int courseyearid, int session, int collegeid, int SID)
        {
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                CommonMethod cmn = new CommonMethod();
                List<ScrutinyFeesSubmit> obj = new List<ScrutinyFeesSubmit>();
                string ip = CommonMethod.GetIPAddress();

                var ObjFees = conn.Query<ScrutinyFeesSubmit>("[sp_student_ScrutinyFrom_back]", new { @Action = "findPG_backpaper", @courseCategoryid = courseCategoryid, @streamCategoryid = streamCategoryid, @courseyearid = courseyearid, @session = session, @collegeid = @collegeid, @SID = SID }, commandType: CommandType.StoredProcedure).ToList();
                obj = ObjFees;

                return obj;
            }
        }
        public List<ScrutinyFeesSubmit> backFeesDetailSubjectlist_UG(int courseCategoryid, int streamCategoryid, int courseyearid, int session, int collegeid, int SID)
        {
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                CommonMethod cmn = new CommonMethod();
                List<ScrutinyFeesSubmit> obj = new List<ScrutinyFeesSubmit>();
                string ip = CommonMethod.GetIPAddress();
                if (courseCategoryid == Convert.ToInt32(CommonSetting.coursecategory.ba) || courseCategoryid == Convert.ToInt32(CommonSetting.coursecategory.bsc) || courseCategoryid == Convert.ToInt32(CommonSetting.coursecategory.bcomm))
                {
                    var ObjFees = conn.Query<ScrutinyFeesSubmit>("[sp_student_ScrutinyFrom_back]", new { @Action = "findUG_backpaper", @courseCategoryid = courseCategoryid, @streamCategoryid = streamCategoryid, @courseyearid = courseyearid, @session = session, @collegeid = @collegeid, @SID = SID }, commandTimeout: 120545, commandType: CommandType.StoredProcedure).ToList();
                    obj = ObjFees;
                }
                return obj;
            }
        }



        public DataTable DummyAdmitCardFormStatus_UG(int courseCategoryid = 0, int SID = 0, string TypeOfAdmitCard = "")
        {

            string connetionString = null;
            SqlConnection sqlCnn;
            SqlCommand sqlCmd;
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataSet ds = new DataSet();
            int i = 0;
            string sql = null;
            connetionString = CommonSetting.constr;
            sqlCnn = new SqlConnection(connetionString);
            sqlCnn.Open();
            sqlCmd = new SqlCommand("sp_student_AdmitCardStatus_jc", sqlCnn);
            sqlCmd.Parameters.Add(new SqlParameter("@Action", TypeOfAdmitCard));
            sqlCmd.Parameters.Add(new SqlParameter("@Sid", SID));
            sqlCmd.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand = sqlCmd;
            adapter.Fill(ds);
            adapter.Dispose();
            sqlCmd.Dispose();
            sqlCnn.Close();
            return ds.Tables[0];

        }

        //public List<DummyAdmitCard> DummyAdmitCardFormStatus_UG( int SID, int courseCategoryid , int isback )
        //{
        //    using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
        //    {
        //        CommonMethod cmn = new CommonMethod();
        //        List<DummyAdmitCard> obj = new List<DummyAdmitCard>();
        //        string ip = CommonMethod.GetIPAddress();
        //        if (courseCategoryid == Convert.ToInt32(CommonSetting.coursecategory.ba) || courseCategoryid == Convert.ToInt32(CommonSetting.coursecategory.bsc) || courseCategoryid == Convert.ToInt32(CommonSetting.coursecategory.bcomm))
        //        {
        //            var ObjFees = conn.Query<DummyAdmitCard>("[sp_student_ScrutinyFrom_back]", new { @Action = "findUG_backpaper", @courseCategoryid = courseCategoryid,  @courseyearid = courseyearid,  @SID = SID }, commandTimeout: 120545, commandType: CommandType.StoredProcedure).ToList();
        //            obj = ObjFees;
        //        }
        //        return obj;
        //    }
        //}

        public List<ScrutinyFeesSubmit> ElectiveFeesDetailSubjectlist(int courseCategoryid, int streamCategoryid, int courseyearid, int SubjectType)
        {
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                CommonMethod cmn = new CommonMethod();
                string ip = CommonMethod.GetIPAddress();
                var ObjFees = conn.Query<ScrutinyFeesSubmit>("[sp_student_ScrutinyFrom]", new { @Action = "Electivesubjectlist", @courseCategoryid = courseCategoryid, @streamCategoryid = streamCategoryid, @courseyearid = courseyearid, @SubjectType = SubjectType }, commandType: CommandType.StoredProcedure).ToList();
                return ObjFees;
            }
        }
        public List<ScrutinyFeesSubmit> ElectiveFeesDetailSubjectlist_bed_C11(int courseCategoryid, int streamCategoryid, int courseyearid, int SubjectType)
        {
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                CommonMethod cmn = new CommonMethod();
                string ip = CommonMethod.GetIPAddress();
                var ObjFees = conn.Query<ScrutinyFeesSubmit>("[sp_student_ScrutinyFrom]", new { @Action = "ElectivesubjectlistBed_c11", @courseCategoryid = courseCategoryid, @streamCategoryid = streamCategoryid, @courseyearid = courseyearid, @SubjectType = SubjectType }, commandType: CommandType.StoredProcedure).ToList();
                return ObjFees;
            }
        }
        public List<ScrutinyFeesSubmit> ElectiveFeesDetailSubjectlist_BPharma_C11(int courseCategoryid, int streamCategoryid, int courseyearid, int SubjectType)
        {
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                CommonMethod cmn = new CommonMethod();
                string ip = CommonMethod.GetIPAddress();
                var ObjFees = conn.Query<ScrutinyFeesSubmit>("[sp_student_ScrutinyFrom]", new { @Action = "ElectivesubjectlistBed_c11", @courseCategoryid = courseCategoryid, @streamCategoryid = streamCategoryid, @courseyearid = courseyearid, @SubjectType = SubjectType }, commandType: CommandType.StoredProcedure).ToList();
                return ObjFees;
            }
        }

        public List<ScrutinyFeesSubmit> ElectiveFeesDetailSubjectlist_bed_c7b(int courseCategoryid, int streamCategoryid, int courseyearid, int SubjectType)
        {
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                CommonMethod cmn = new CommonMethod();
                string ip = CommonMethod.GetIPAddress();
                var ObjFees = conn.Query<ScrutinyFeesSubmit>("[sp_student_ScrutinyFrom]", new { @Action = "ElectivesubjectlistBed_c7b", @courseCategoryid = courseCategoryid, @streamCategoryid = streamCategoryid, @courseyearid = courseyearid, @SubjectType = SubjectType }, commandType: CommandType.StoredProcedure).ToList();
                return ObjFees;
            }
        }

        public List<ScrutinyFeesSubmit> ElectiveFeesDetailSubjectlist_BPharma_c7b(int courseCategoryid, int streamCategoryid, int courseyearid, int SubjectType)
        {
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                CommonMethod cmn = new CommonMethod();
                string ip = CommonMethod.GetIPAddress();
                var ObjFees = conn.Query<ScrutinyFeesSubmit>("[sp_student_ScrutinyFrom]", new { @Action = "ElectivesubjectlistBed_c7b", @courseCategoryid = courseCategoryid, @streamCategoryid = streamCategoryid, @courseyearid = courseyearid, @SubjectType = SubjectType }, commandType: CommandType.StoredProcedure).ToList();
                return ObjFees;
            }
        }



        public List<ScrutinyFeesSubmit> ElectiveFeesDetailSubjectlist_bed_c7a(int courseCategoryid, int streamCategoryid, int courseyearid, int SubjectType)
        {
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                CommonMethod cmn = new CommonMethod();
                string ip = CommonMethod.GetIPAddress();
                var ObjFees = conn.Query<ScrutinyFeesSubmit>("[sp_student_ScrutinyFrom]", new { @Action = "ElectivesubjectlistBed_c7a", @courseCategoryid = courseCategoryid, @streamCategoryid = streamCategoryid, @courseyearid = courseyearid, @SubjectType = SubjectType }, commandType: CommandType.StoredProcedure).ToList();
                return ObjFees;
            }
        }

        public List<ScrutinyFeesSubmit> ElectiveFeesDetailSubjectlist_BPharma_c7a(int courseCategoryid, int streamCategoryid, int courseyearid, int SubjectType)
        {
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                CommonMethod cmn = new CommonMethod();
                string ip = CommonMethod.GetIPAddress();
                var ObjFees = conn.Query<ScrutinyFeesSubmit>("[sp_student_ScrutinyFrom]", new { @Action = "ElectivesubjectlistBed_c7a", @courseCategoryid = courseCategoryid, @streamCategoryid = streamCategoryid, @courseyearid = courseyearid, @SubjectType = SubjectType }, commandType: CommandType.StoredProcedure).ToList();
                return ObjFees;
            }
        }
        public ScrutinyForm FeesischeckScrutinyfeesubmit(int sid, int session)
        {
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                CommonMethod cmn = new CommonMethod();
                string ip = CommonMethod.GetIPAddress();
                var ObjFees = conn.Query<ScrutinyForm>("[sp_student_ScrutinyFrom]", new { @Action = "ischeckScrutinyfeesubmit", @SID = sid, @session = session }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return ObjFees;
            }
        }
    }
    public class PrintScrutinyForm
    {
        public ScrutinyFeesSubmit Scrutinyobjfeesubmit { get; set; }
        public FeesSubmit objfeesubmit { get; set; }
        public ScrutinyForm objScrutinyFrom { get; set; }
        public BL_PrintRecipt objPrintRecipt { get; set; }
        public List<ScrutinyFeesSubmit> subjectlist { get; set; }


    }



    public class ScrutinyFeesSubmit
    {
        public int Id { get; set; }
        public string ApplicationNo { get; set; }
        public string Title { get; set; }
        public string Session { get; set; }
        public string Message { get; set; }
        public string CastCategort { get; set; }
        public string Fees { get; set; }
        public string Course { get; set; }
        public string IsFeeSubmit { get; set; }
        public string FeeStatus { get; set; }
        public string FeeStatus1 { get; set; }
        public bool Status { get; set; }
        public string CollegeName { get; set; }
        public string Stream { get; set; }
        public string PaymentType { get; set; }
        public string PaymentHolderName { get; set; }
        public string CardNumber { get; set; }
        public string Expires { get; set; }
        public string NuthNum { get; set; }
        public string mid { get; set; }
        public string mkey { get; set; }
        public string headname { get; set; }
        public decimal amount { get; set; }
        public string amount1 { get; set; }
        public int Collegeid { get; set; }
        public int coursecategoryid { get; set; }
        public int sessionid { get; set; }
        public string Requestdata { get; set; }
        public string dRequestdata { get; set; }
        public string requesttime { get; set; }
        public string PGstatus { get; set; }
        public string banktrxid { get; set; }
        public string clienttrxid { get; set; }
        public string feeamount { get; set; }
        public string gst { get; set; }
        public string commission { get; set; }
        public string paymode { get; set; }
        public string banktxndate { get; set; }
        public string Reason { get; set; }
        public string apitxnid { get; set; }
        public decimal surcharge { get; set; }
        public string email { get; set; }
        public string mobileno { get; set; }
        public string name { get; set; }
        public int CastCategory { get; set; }
        public int streamcategoryid { get; set; }
        public string yearname { get; set; }
        public bool IsAdmissionFee2 { get; set; }
        public string FeeStatus2 { get; set; }
        public int studentyear { get; set; }
        public int incomecertificate_iseligible { get; set; }
        public decimal late_fee { get; set; }
        public decimal fee_without_late_fee { get; set; }
        public string Other_Details { get; set; }
        public string SubjectName { get; set; }
        public string SubjectCode { get; set; }
        public int Substreamcategoryid { get; set; }
        public int courseyearid { get; set; }
        public string SessionName { get; set; }
        public bool IsScrutinyFee { get; set; }
        public int Currentyear_courseyarid { get; set; }
        public string HonoursSubject { get; set; }
        public string Subsidiary1Subject { get; set; }
        public string Subsidiary2Subject { get; set; }
        public string Compulsory1Subject { get; set; }
        public string Compulsory2Subject { get; set; }

        //Ad By Jc new
        public string Honoursid { get; set; }
        public string Subsidiary1id { get; set; }
        public string Subsidiary2id { get; set; }
        public string Compulsory1id { get; set; }
        public string Compulsory2id { get; set; }
        //End By Jc new
        public string PaymentGetway { get; set; }
        public string Scrutinylist { get; set; }
        public string ScrutinyAmount { get; set; }
        public string TotalSubject { get; set; }
        public string TotalSubjectList { get; set; }
        public string AdmitCardUpload { get; set; }
        public string DocumentUpload { get; set; }
        public ScrutinyFeesSubmit ScrutinyFeesDetail()
        {
            ScrutinyFeesSubmit ObjFees = new ScrutinyFeesSubmit();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                CommonMethod.TraceLogWritetoNotepad("obj1.objScrutinyFrom1", "Scrutiny/ScrutinyFeesSubmit621", "id", "session");
                int StID = (ClsLanguage.GetCookies("NBStID") != null ? Convert.ToInt32(ClsLanguage.GetCookies("NBStID")) : 0);
                int Sission = (ClsLanguage.GetCookies("NBSission") != null ? Convert.ToInt32(ClsLanguage.GetCookies("NBSission")) : 0);
                CommonMethod.TraceLogWritetoNotepad("obj1.objScrutinyFrom1", "Scrutiny/ScrutinyFeesSubmit620", StID.ToString(), Sission.ToString());
                //Directory.CreateDirectory("~/Images");
                CommonMethod.TraceLogWritetoNotepad("obj1.objScrutinyFrom1", "Scrutiny/ScrutinyFeesSubmit623", StID.ToString(), Sission.ToString());
                if (StID == 0)
                {
                    CommonMethod.TraceLogWritetoNotepad("obj1.objScrutinyFrom1", "Scrutiny/ScrutinyFeesSubmit522", StID.ToString(), Sission.ToString());
                    ObjFees.Status = false;
                    ObjFees.Message = "Network Error !!";
                    CommonMethod.TraceLogWritetoNotepad("obj1.objScrutinyFrom1", "Scrutiny/ScrutinyFeesSubmit5221", StID.ToString(), Sission.ToString());
                    return ObjFees;
                }
                CommonMethod.TraceLogWritetoNotepad("obj1.objScrutinyFrom1", "Scrutiny/ScrutinyFeesSubmit5222", StID.ToString(), Sission.ToString());
                CommonMethod cmn = new CommonMethod();
                string ip = CommonMethod.GetIPAddress();
                CommonMethod.TraceLogWritetoNotepad("obj1.objScrutinyFrom1", "Scrutiny/ScrutinyFeesSubmit524", StID.ToString(), Sission.ToString());
                ObjFees = conn.Query<ScrutinyFeesSubmit>("[sp_student_ScrutinyFrom]", new { @Action = "StudentScrutinyFeesDetail", @Sid = StID, @session = Sission }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                CommonMethod.TraceLogWritetoNotepad("obj1.objScrutinyFrom1", "Scrutiny/ScrutinyFeesSubmit525", StID.ToString(), Sission.ToString());
                if (ObjFees == null)
                {
                    CommonMethod.TraceLogWritetoNotepad("obj1.objScrutinyFrom1", "Scrutiny/ScrutinyFeesSubmit526", StID.ToString(), Sission.ToString());
                    ScrutinyFeesSubmit objnew = new ScrutinyFeesSubmit();
                    ObjFees = objnew;
                }
                else
                {
                    CommonMethod.TraceLogWritetoNotepad("obj1.objScrutinyFrom1", "Scrutiny/ScrutinyFeesSubmit627", StID.ToString(), Sission.ToString());
                    if (ObjFees.Id <= 0)
                    {
                        CommonMethod.TraceLogWritetoNotepad("obj1.objScrutinyFrom1", "Scrutiny/ScrutinyFeesSubmit628", StID.ToString(), Sission.ToString());
                        ObjFees.Status = false;

                    }
                }

                return ObjFees;
            }
        }
        public ScrutinyFeesSubmit pracScrutinyFeesDetail()
        {
            ScrutinyFeesSubmit ObjFees = new ScrutinyFeesSubmit();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {

                int StID = (ClsLanguage.GetCookies("NBStID") != null ? Convert.ToInt32(ClsLanguage.GetCookies("NBStID")) : 0);
                int Sission = (ClsLanguage.GetCookies("NBSission") != null ? Convert.ToInt32(ClsLanguage.GetCookies("NBSission")) : 0);

                if (StID == 0)
                {
                    ObjFees.Status = false;
                    ObjFees.Message = "Network Error !!";
                    return ObjFees;
                }
                CommonMethod cmn = new CommonMethod();
                string ip = CommonMethod.GetIPAddress();
                ObjFees = conn.Query<ScrutinyFeesSubmit>("[sp_student_ScrutinyFromprac]", new { @Action = "StudentScrutinyFeesDetail", @Sid = StID, @session = Sission }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                if (ObjFees == null)
                {
                    ScrutinyFeesSubmit objnew = new ScrutinyFeesSubmit();
                    ObjFees = objnew;
                }
                else
                {
                    if (ObjFees.Id <= 0)
                    {
                        ObjFees.Status = false;
                    }
                }

                return ObjFees;
            }
        }
        public ScrutinyFeesSubmit backScrutinyFeesDetail(int courseyearid)
        {
            ScrutinyFeesSubmit ObjFees = new ScrutinyFeesSubmit();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {

                int StID = (ClsLanguage.GetCookies("NBStID") != null ? Convert.ToInt32(ClsLanguage.GetCookies("NBStID")) : 0);
                int Sission = (ClsLanguage.GetCookies("NBSission") != null ? Convert.ToInt32(ClsLanguage.GetCookies("NBSission")) : 0);

                if (StID == 0)
                {
                    ObjFees.Status = false;
                    ObjFees.Message = "Network Error !!";
                    return ObjFees;
                }
                CommonMethod cmn = new CommonMethod();
                string ip = CommonMethod.GetIPAddress();
                ObjFees = conn.Query<ScrutinyFeesSubmit>("[sp_student_ScrutinyFrom_back]", new { @Action = "StudentScrutinyFeesDetail", @Sid = StID, @session = Sission, @currentyear = System.DateTime.Now.Year, @courseyearid = courseyearid }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                if (ObjFees == null)
                {
                    ScrutinyFeesSubmit objnew = new ScrutinyFeesSubmit();
                    ObjFees = objnew;
                }
                else
                {
                    if (ObjFees.Id <= 0)
                    {
                        ObjFees.Status = false;

                    }
                }

                return ObjFees;
            }
        }
        public ScrutinyFeesSubmit FeessubScrutinymentbefore(ScrutinyFeesSubmit obj123)
        {
            ScrutinyFeesSubmit ObjFees = new ScrutinyFeesSubmit();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                int Sission = Convert.ToInt32(ClsLanguage.GetCookies("NBSission"));
                CommonMethod cmn = new CommonMethod();
                ObjFees = conn.Query<ScrutinyFeesSubmit>("[sp_student_Scrutinyfee_before]", new
                {
                    @applicationno = obj123.ApplicationNo,
                    @Requestdata = obj123.Requestdata,
                    @dRequestdata = obj123.dRequestdata,
                    @status = obj123.PGstatus,
                    @banktrxid = obj123.banktrxid,
                    @clienttrxid = obj123.clienttrxid,
                    @amount = obj123.amount,
                    @feeamount = (obj123.fee_without_late_fee == 0 ? 0 : obj123.fee_without_late_fee),
                    @gst = (obj123.gst == "" ? "0" : obj123.gst),
                    @commission = (obj123.commission == "" ? "0" : obj123.commission),
                    @paymode = obj123.paymode,
                    @Reason = obj123.Reason,
                    @banktxndate = obj123.banktxndate,
                    @apitxnid = obj123.apitxnid,
                    @responsevia = "browser",
                    @Other_Details = obj123.Other_Details,
                    @feewithout_late_fee = obj123.fee_without_late_fee,
                    @late_fee = obj123.late_fee,
                    @mid = obj123.mid
                }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                ObjFees.Status = ObjFees.Status;
                return ObjFees;
            }
        }

        public ScrutinyFeesSubmit FeessubScrutinymentbeforeAirPay(ScrutinyFeesSubmit obj123)
        {
            ScrutinyFeesSubmit ObjFees = new ScrutinyFeesSubmit();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                int Sission = Convert.ToInt32(ClsLanguage.GetCookies("NBSission"));
                CommonMethod cmn = new CommonMethod();
                ObjFees = conn.Query<ScrutinyFeesSubmit>("[sp_student_Scrutinyfee_before]", new
                {
                    @applicationno = obj123.ApplicationNo,
                    @Requestdata = obj123.Requestdata,
                    @dRequestdata = obj123.dRequestdata,
                    @status = obj123.PGstatus,
                    @banktrxid = obj123.banktrxid,
                    @clienttrxid = obj123.clienttrxid,
                    @amount = obj123.amount,
                    @feeamount = (obj123.fee_without_late_fee == 0 ? 0 : obj123.fee_without_late_fee),
                    @gst = (obj123.gst == "" ? "0" : obj123.gst),
                    @commission = (obj123.commission == "" ? "0" : obj123.commission),
                    @paymode = obj123.paymode,
                    @Reason = obj123.Reason,
                    @banktxndate = obj123.banktxndate,
                    @apitxnid = obj123.apitxnid,
                    @responsevia = "browser",
                    @Other_Details = obj123.Other_Details,
                    @feewithout_late_fee = obj123.fee_without_late_fee,
                    @late_fee = obj123.late_fee,
                    @mid = obj123.mid,
                    @PaymentGetway = "AirPay"
                }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                ObjFees.Status = ObjFees.Status;
                return ObjFees;
            }
        }

        public ScrutinyFeesSubmit FeessubScrutinymentbeforeSafexPay(ScrutinyFeesSubmit obj123)
        {
            ScrutinyFeesSubmit ObjFees = new ScrutinyFeesSubmit();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                int Sission = Convert.ToInt32(ClsLanguage.GetCookies("NBSission"));
                CommonMethod cmn = new CommonMethod();
                ObjFees = conn.Query<ScrutinyFeesSubmit>("[sp_student_Scrutinyfee_before]", new
                {
                    @applicationno = obj123.ApplicationNo,
                    @Requestdata = obj123.Requestdata,
                    @dRequestdata = obj123.dRequestdata,
                    @status = obj123.PGstatus,
                    @banktrxid = obj123.banktrxid,
                    @clienttrxid = obj123.clienttrxid,
                    @amount = obj123.amount,
                    @feeamount = (obj123.fee_without_late_fee == 0 ? 0 : obj123.fee_without_late_fee),
                    @gst = (obj123.gst == "" ? "0" : obj123.gst),
                    @commission = (obj123.commission == "" ? "0" : obj123.commission),
                    @paymode = obj123.paymode,
                    @Reason = obj123.Reason,
                    @banktxndate = obj123.banktxndate,
                    @apitxnid = obj123.apitxnid,
                    @responsevia = "browser",
                    @Other_Details = obj123.Other_Details,
                    @feewithout_late_fee = obj123.fee_without_late_fee,
                    @late_fee = obj123.late_fee,
                    @mid = obj123.mid,
                    @PaymentGetway = "SafexPay"
                }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                ObjFees.Status = ObjFees.Status;
                return ObjFees;
            }
        }


        public ScrutinyFeesSubmit FeessubScrutinymentbeforeEaseBuzz(ScrutinyFeesSubmit obj123)
        {
            ScrutinyFeesSubmit ObjFees = new ScrutinyFeesSubmit();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                int Sission = Convert.ToInt32(ClsLanguage.GetCookies("NBSission"));
                CommonMethod cmn = new CommonMethod();
                ObjFees = conn.Query<ScrutinyFeesSubmit>("[sp_student_Scrutinyfee_before]", new
                {
                    @applicationno = obj123.ApplicationNo,
                    @Requestdata = obj123.Requestdata,
                    @dRequestdata = obj123.dRequestdata,
                    @status = obj123.PGstatus,
                    @banktrxid = obj123.banktrxid,
                    @clienttrxid = obj123.clienttrxid,
                    @amount = obj123.amount,
                    @feeamount = (obj123.fee_without_late_fee == 0 ? 0 : obj123.fee_without_late_fee),
                    @gst = (obj123.gst == "" ? "0" : obj123.gst),
                    @commission = (obj123.commission == "" ? "0" : obj123.commission),
                    @paymode = obj123.paymode,
                    @Reason = obj123.Reason,
                    @banktxndate = obj123.banktxndate,
                    @apitxnid = obj123.apitxnid,
                    @responsevia = "browser",
                    @Other_Details = obj123.Other_Details,
                    @feewithout_late_fee = obj123.fee_without_late_fee,
                    @late_fee = obj123.late_fee,
                    @mid = obj123.mid,
                    @PaymentGetway = "EaseBuzz"
                }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                ObjFees.Status = ObjFees.Status;
                return ObjFees;
            }
        }
        public ScrutinyFeesSubmit FeessubScrutinymentbeforePract(ScrutinyFeesSubmit obj123)
        {
            ScrutinyFeesSubmit ObjFees = new ScrutinyFeesSubmit();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                int Sission = Convert.ToInt32(ClsLanguage.GetCookies("NBSission"));
                CommonMethod cmn = new CommonMethod();
                ObjFees = conn.Query<ScrutinyFeesSubmit>("[sp_student_Scrutinyfee_beforeprac]", new
                {
                    @applicationno = obj123.ApplicationNo,
                    @Requestdata = obj123.Requestdata,
                    @dRequestdata = obj123.dRequestdata,
                    @status = obj123.PGstatus,
                    @banktrxid = obj123.banktrxid,
                    @clienttrxid = obj123.clienttrxid,
                    @amount = obj123.amount,
                    @feeamount = (obj123.fee_without_late_fee == 0 ? 0 : obj123.fee_without_late_fee),
                    @gst = (obj123.gst == "" ? "0" : obj123.gst),
                    @commission = (obj123.commission == "" ? "0" : obj123.commission),
                    @paymode = obj123.paymode,
                    @Reason = obj123.Reason,
                    @banktxndate = obj123.banktxndate,
                    @apitxnid = obj123.apitxnid,
                    @responsevia = "browser",
                    @Other_Details = obj123.Other_Details,
                    @feewithout_late_fee = obj123.fee_without_late_fee,
                    @late_fee = obj123.late_fee,
                    @mid = obj123.mid
                }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                ObjFees.Status = ObjFees.Status;
                return ObjFees;
            }
        }
        public ScrutinyFeesSubmit FeessubScrutinymentbeforebackyear(ScrutinyFeesSubmit obj123)
        {
            ScrutinyFeesSubmit ObjFees = new ScrutinyFeesSubmit();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                int Sission = Convert.ToInt32(ClsLanguage.GetCookies("NBSission"));
                CommonMethod cmn = new CommonMethod();
                ObjFees = conn.Query<ScrutinyFeesSubmit>("[sp_student_Scrutinyfee_before_backyear]", new
                {
                    @applicationno = obj123.ApplicationNo,
                    @Requestdata = obj123.Requestdata,
                    @dRequestdata = obj123.dRequestdata,
                    @status = obj123.PGstatus,
                    @banktrxid = obj123.banktrxid,
                    @clienttrxid = obj123.clienttrxid,
                    @amount = obj123.amount,
                    @feeamount = (obj123.fee_without_late_fee == 0 ? 0 : obj123.fee_without_late_fee),
                    @gst = (obj123.gst == "" ? "0" : obj123.gst),
                    @commission = (obj123.commission == "" ? "0" : obj123.commission),
                    @paymode = obj123.paymode,
                    @Reason = obj123.Reason,
                    @banktxndate = obj123.banktxndate,
                    @apitxnid = obj123.apitxnid,
                    @responsevia = "browser",
                    @Other_Details = obj123.Other_Details,
                    @feewithout_late_fee = obj123.fee_without_late_fee,
                    @late_fee = obj123.late_fee,
                    @mid = obj123.mid,
                    @currentyear = System.DateTime.Now.Year,
                    @courseyearid = obj123.courseyearid
                }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                ObjFees.Status = ObjFees.Status;
                return ObjFees;
            }
        }
        public ScrutinyFeesSubmit FeessubScrutiny(ScrutinyFeesSubmit obj123)
        {
            ScrutinyFeesSubmit ObjFees = new ScrutinyFeesSubmit();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                int Sission = Convert.ToInt32(ClsLanguage.GetCookies("NBSission"));
                CommonMethod cmn = new CommonMethod();
                ObjFees = conn.Query<ScrutinyFeesSubmit>("[sp_student_Scrutinyfee]", new
                {
                    @applicationno = obj123.ApplicationNo,
                    @Requestdata = obj123.Requestdata,
                    @dRequestdata = obj123.dRequestdata,
                    @status = obj123.PGstatus,
                    @banktrxid = obj123.banktrxid,
                    @clienttrxid = obj123.clienttrxid,
                    @amount = obj123.amount1,
                    @feeamount = (obj123.feeamount == "" ? "0" : obj123.feeamount),
                    @gst = (obj123.gst == "" ? "0" : obj123.gst),
                    @commission = (obj123.commission == "" ? "0" : obj123.commission),
                    @paymode = obj123.paymode,
                    @Reason = obj123.Reason,
                    @banktxndate = obj123.banktxndate,
                    @apitxnid = obj123.apitxnid,
                    @responsevia = "browser"

                }, commandTimeout: 120545, commandType: CommandType.StoredProcedure).FirstOrDefault();
                if (ObjFees.Id <= 0)
                {
                    ObjFees.Status = false;

                }
                return ObjFees;
            }
        }

        public ScrutinyFeesSubmit AirPayFeessubScrutiny(ScrutinyFeesSubmit obj123)
        {
            ScrutinyFeesSubmit ObjFees = new ScrutinyFeesSubmit();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                //int Sission = Convert.ToInt32(ClsLanguage.GetCookies("NBSission"));
                CommonMethod cmn = new CommonMethod();
                ObjFees = conn.Query<ScrutinyFeesSubmit>("[sp_student_Scrutinyfee]", new
                {
                    @applicationno = obj123.ApplicationNo,
                    @Requestdata = obj123.Requestdata,
                    @dRequestdata = obj123.dRequestdata,
                    @status = obj123.PGstatus,
                    @banktrxid = obj123.banktrxid,
                    @clienttrxid = obj123.clienttrxid,
                    @amount = obj123.amount1,
                    @feeamount = (obj123.feeamount == "" ? "0" : obj123.feeamount),
                    @gst = (obj123.gst == "" ? "0" : obj123.gst),
                    @commission = (obj123.commission == "" ? "0" : obj123.commission),
                    @paymode = obj123.paymode,
                    @Reason = obj123.Reason,
                    @banktxndate = obj123.banktxndate,
                    @apitxnid = obj123.apitxnid,
                    @responsevia = "browser"

                }, commandTimeout: 120545, commandType: CommandType.StoredProcedure).FirstOrDefault();
                if (ObjFees.Id <= 0)
                {
                    ObjFees.Status = false;

                }
                return ObjFees;
            }
        }

        public ScrutinyFeesSubmit EaseBuzzFeessubScrutiny(ScrutinyFeesSubmit obj123)
        {
            ScrutinyFeesSubmit ObjFees = new ScrutinyFeesSubmit();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                //int Sission = Convert.ToInt32(ClsLanguage.GetCookies("NBSission"));
                CommonMethod cmn = new CommonMethod();
                ObjFees = conn.Query<ScrutinyFeesSubmit>("[sp_student_Scrutinyfee]", new
                {
                    @applicationno = obj123.ApplicationNo,
                    @Requestdata = obj123.Requestdata,
                    @dRequestdata = obj123.dRequestdata,
                    @status = obj123.PGstatus,
                    @banktrxid = obj123.banktrxid,
                    @clienttrxid = obj123.clienttrxid,
                    @amount = obj123.amount1,
                    @feeamount = (obj123.feeamount == "" ? "0" : obj123.feeamount),
                    @gst = (obj123.gst == "" ? "0" : obj123.gst),
                    @commission = (obj123.commission == "" ? "0" : obj123.commission),
                    @paymode = obj123.paymode,
                    @Reason = obj123.Reason,
                    @banktxndate = obj123.banktxndate,
                    @apitxnid = obj123.apitxnid,
                    @responsevia = "browser"

                }, commandTimeout: 120545, commandType: CommandType.StoredProcedure).FirstOrDefault();
                if (ObjFees.Id <= 0)
                {
                    ObjFees.Status = false;

                }
                return ObjFees;
            }
        }


        public ScrutinyFeesSubmit SafexPayFeessubScrutiny(ScrutinyFeesSubmit obj123)
        {
            ScrutinyFeesSubmit ObjFees = new ScrutinyFeesSubmit();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                //int Sission = Convert.ToInt32(ClsLanguage.GetCookies("NBSission"));
                CommonMethod cmn = new CommonMethod();
                ObjFees = conn.Query<ScrutinyFeesSubmit>("[sp_student_Scrutinyfee]", new
                {
                    @applicationno = obj123.ApplicationNo,
                    @Requestdata = obj123.Requestdata,
                    @dRequestdata = obj123.dRequestdata,
                    @status = obj123.PGstatus,
                    @banktrxid = obj123.banktrxid,
                    @clienttrxid = obj123.clienttrxid,
                    @amount = obj123.amount1,
                    @feeamount = (obj123.feeamount == "" ? "0" : obj123.feeamount),
                    @gst = (obj123.gst == "" ? "0" : obj123.gst),
                    @commission = (obj123.commission == "" ? "0" : obj123.commission),
                    @paymode = obj123.paymode,
                    @Reason = obj123.Reason,
                    @banktxndate = obj123.banktxndate,
                    @apitxnid = obj123.apitxnid,
                    @responsevia = "browser"

                }, commandTimeout: 120545, commandType: CommandType.StoredProcedure).FirstOrDefault();
                if (ObjFees.Id <= 0)
                {
                    ObjFees.Status = false;

                }
                return ObjFees;
            }
        }

        public ScrutinyFeesSubmit FeessubScrutinyprac(ScrutinyFeesSubmit obj123)
        {
            ScrutinyFeesSubmit ObjFees = new ScrutinyFeesSubmit();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                int Sission = Convert.ToInt32(ClsLanguage.GetCookies("NBSission"));
                CommonMethod cmn = new CommonMethod();
                ObjFees = conn.Query<ScrutinyFeesSubmit>("[sp_student_Scrutinyfeeprac]", new
                {
                    @applicationno = obj123.ApplicationNo,
                    @Requestdata = obj123.Requestdata,
                    @dRequestdata = obj123.dRequestdata,
                    @status = obj123.PGstatus,
                    @banktrxid = obj123.banktrxid,
                    @clienttrxid = obj123.clienttrxid,
                    @amount = obj123.amount1,
                    @feeamount = (obj123.feeamount == "" ? "0" : obj123.feeamount),
                    @gst = (obj123.gst == "" ? "0" : obj123.gst),
                    @commission = (obj123.commission == "" ? "0" : obj123.commission),
                    @paymode = obj123.paymode,
                    @Reason = obj123.Reason,
                    @banktxndate = obj123.banktxndate,
                    @apitxnid = obj123.apitxnid,
                    @responsevia = "browser"

                }, commandTimeout: 120545, commandType: CommandType.StoredProcedure).FirstOrDefault();
                if (ObjFees.Id <= 0)
                {
                    ObjFees.Status = false;

                }
                return ObjFees;
            }
        }


        public ScrutinyFeesSubmit FeessubScrutinybackyear(ScrutinyFeesSubmit obj123)
        {
            ScrutinyFeesSubmit ObjFees = new ScrutinyFeesSubmit();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                int Sission = Convert.ToInt32(ClsLanguage.GetCookies("NBSission"));
                CommonMethod cmn = new CommonMethod();
                ObjFees = conn.Query<ScrutinyFeesSubmit>("[sp_student_Scrutinyfee_backyear]", new
                {
                    @applicationno = obj123.ApplicationNo,
                    @Requestdata = obj123.Requestdata,
                    @dRequestdata = obj123.dRequestdata,
                    @status = obj123.PGstatus,
                    @banktrxid = obj123.banktrxid,
                    @clienttrxid = obj123.clienttrxid,
                    @amount = obj123.amount1,
                    @feeamount = (obj123.feeamount == "" ? "0" : obj123.feeamount),
                    @gst = (obj123.gst == "" ? "0" : obj123.gst),
                    @commission = (obj123.commission == "" ? "0" : obj123.commission),
                    @paymode = obj123.paymode,
                    @Reason = obj123.Reason,
                    @banktxndate = obj123.banktxndate,
                    @apitxnid = obj123.apitxnid,
                    @responsevia = "browser",
                    @currentyear = System.DateTime.Now.Year,
                    @courseyearid = obj123.courseyearid
                }, commandTimeout: 120545, commandType: CommandType.StoredProcedure).FirstOrDefault();
                if (ObjFees.Id <= 0)
                {
                    ObjFees.Status = false;

                }
                return ObjFees;
            }
        }
        public ScrutinyFeesSubmit AirPayFeessubScrutinybackyear(ScrutinyFeesSubmit obj123)
        {
            ScrutinyFeesSubmit ObjFees = new ScrutinyFeesSubmit();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                //int Sission = Convert.ToInt32(ClsLanguage.GetCookies("NBSission"));
                CommonMethod cmn = new CommonMethod();
                ObjFees = conn.Query<ScrutinyFeesSubmit>("[sp_student_Scrutinyfee_backyear]", new
                {
                    @applicationno = obj123.ApplicationNo,
                    @Requestdata = obj123.Requestdata,
                    @dRequestdata = obj123.dRequestdata,
                    @status = obj123.PGstatus,
                    @banktrxid = obj123.banktrxid,
                    @clienttrxid = obj123.clienttrxid,
                    @amount = obj123.amount1,
                    @feeamount = (obj123.feeamount == "" ? "0" : obj123.feeamount),
                    @gst = (obj123.gst == "" ? "0" : obj123.gst),
                    @commission = (obj123.commission == "" ? "0" : obj123.commission),
                    @paymode = obj123.paymode,
                    @Reason = obj123.Reason,
                    @banktxndate = obj123.banktxndate,
                    @apitxnid = obj123.apitxnid,
                    @responsevia = "browser",
                    @currentyear = System.DateTime.Now.Year,
                    @courseyearid = obj123.courseyearid
                }, commandTimeout: 120545, commandType: CommandType.StoredProcedure).FirstOrDefault();
                if (ObjFees.Id <= 0)
                {
                    ObjFees.Status = false;

                }
                return ObjFees;
            }
        }

        public ScrutinyFeesSubmit SafexPayFeessubScrutinybackyear(ScrutinyFeesSubmit obj123)
        {
            ScrutinyFeesSubmit ObjFees = new ScrutinyFeesSubmit();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                //int Sission = Convert.ToInt32(ClsLanguage.GetCookies("NBSission"));
                CommonMethod cmn = new CommonMethod();
                ObjFees = conn.Query<ScrutinyFeesSubmit>("[sp_student_Scrutinyfee_backyear]", new
                {
                    @applicationno = obj123.ApplicationNo,
                    @Requestdata = obj123.Requestdata,
                    @dRequestdata = obj123.dRequestdata,
                    @status = obj123.PGstatus,
                    @banktrxid = obj123.banktrxid,
                    @clienttrxid = obj123.clienttrxid,
                    @amount = obj123.amount1,
                    @feeamount = (obj123.feeamount == "" ? "0" : obj123.feeamount),
                    @gst = (obj123.gst == "" ? "0" : obj123.gst),
                    @commission = (obj123.commission == "" ? "0" : obj123.commission),
                    @paymode = obj123.paymode,
                    @Reason = obj123.Reason,
                    @banktxndate = obj123.banktxndate,
                    @apitxnid = obj123.apitxnid,
                    @responsevia = "browser",
                    @currentyear = System.DateTime.Now.Year,
                    @courseyearid = obj123.courseyearid
                }, commandTimeout: 120545, commandType: CommandType.StoredProcedure).FirstOrDefault();
                if (ObjFees.Id <= 0)
                {
                    ObjFees.Status = false;

                }
                return ObjFees;
            }
        }
        public ScrutinyFeesSubmit FeessubScrutinyPushresponse(ScrutinyFeesSubmit obj123)
        {
            ScrutinyFeesSubmit ObjFees = new ScrutinyFeesSubmit();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                ObjFees = conn.Query<ScrutinyFeesSubmit>("[sp_student_Scrutinyfee]", new
                {
                    @applicationno = obj123.ApplicationNo,
                    @Requestdata = obj123.Requestdata,
                    @dRequestdata = obj123.dRequestdata,
                    @status = obj123.PGstatus,
                    @banktrxid = obj123.banktrxid,
                    @clienttrxid = obj123.clienttrxid,
                    @amount = obj123.amount1,
                    @feeamount = (obj123.feeamount == "" ? "0" : obj123.feeamount),
                    @gst = (obj123.gst == "" ? "0" : obj123.gst),
                    @commission = (obj123.commission == "" ? "0" : obj123.commission),
                    @paymode = obj123.paymode,
                    @Reason = obj123.Reason,
                    @banktxndate = obj123.banktxndate,
                    @apitxnid = obj123.apitxnid,
                    @responsevia = "Pushresponse"

                }, commandTimeout: 120545, commandType: CommandType.StoredProcedure).FirstOrDefault();
                if (ObjFees.Id <= 0)
                {
                    ObjFees.Status = false;

                }
                return ObjFees;
            }
        }
        public ScrutinyFeesSubmit FeessubScrutinyPushresponseprac(ScrutinyFeesSubmit obj123)
        {
            ScrutinyFeesSubmit ObjFees = new ScrutinyFeesSubmit();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                ObjFees = conn.Query<ScrutinyFeesSubmit>("[sp_student_ScrutinyFromprac]", new
                {
                    @applicationno = obj123.ApplicationNo,
                    @Requestdata = obj123.Requestdata,
                    @dRequestdata = obj123.dRequestdata,
                    @status = obj123.PGstatus,
                    @banktrxid = obj123.banktrxid,
                    @clienttrxid = obj123.clienttrxid,
                    @amount = obj123.amount1,
                    @feeamount = (obj123.feeamount == "" ? "0" : obj123.feeamount),
                    @gst = (obj123.gst == "" ? "0" : obj123.gst),
                    @commission = (obj123.commission == "" ? "0" : obj123.commission),
                    @paymode = obj123.paymode,
                    @Reason = obj123.Reason,
                    @banktxndate = obj123.banktxndate,
                    @apitxnid = obj123.apitxnid,
                    @responsevia = "Pushresponse"

                }, commandTimeout: 120545, commandType: CommandType.StoredProcedure).FirstOrDefault();
                if (ObjFees.Id <= 0)
                {
                    ObjFees.Status = false;

                }
                return ObjFees;
            }
        }
        public ScrutinyFeesSubmit FeessubScrutinyPushresponse_backyear(ScrutinyFeesSubmit obj123)
        {
            ScrutinyFeesSubmit ObjFees = new ScrutinyFeesSubmit();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                ObjFees = conn.Query<ScrutinyFeesSubmit>("[sp_student_Scrutinyfee_backyear]", new
                {
                    @applicationno = obj123.ApplicationNo,
                    @Requestdata = obj123.Requestdata,
                    @dRequestdata = obj123.dRequestdata,
                    @status = obj123.PGstatus,
                    @banktrxid = obj123.banktrxid,
                    @clienttrxid = obj123.clienttrxid,
                    @amount = obj123.amount1,
                    @feeamount = (obj123.feeamount == "" ? "0" : obj123.feeamount),
                    @gst = (obj123.gst == "" ? "0" : obj123.gst),
                    @commission = (obj123.commission == "" ? "0" : obj123.commission),
                    @paymode = obj123.paymode,
                    @Reason = obj123.Reason,
                    @banktxndate = obj123.banktxndate,
                    @apitxnid = obj123.apitxnid,
                    @responsevia = "Pushresponse",
                    @currentyear = System.DateTime.Now.Year
                  ,
                    @courseyearid = obj123.courseyearid
                }, commandTimeout: 120545, commandType: CommandType.StoredProcedure).FirstOrDefault();
                if (ObjFees.Id <= 0)
                {
                    ObjFees.Status = false;

                }
                return ObjFees;
            }
        }
        public ScrutinyFeesSubmit FeessubStudentPushresponseDoubleverificationScrutinyprac(ScrutinyFeesSubmit obj123)
        {
            ScrutinyFeesSubmit ObjFees = new ScrutinyFeesSubmit();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                ObjFees = conn.Query<ScrutinyFeesSubmit>("[sp_student_ScrutinyFromprac]", new
                {
                    @applicationno = obj123.ApplicationNo,
                    @Requestdata = obj123.Requestdata,
                    @dRequestdata = obj123.dRequestdata,
                    @status = obj123.PGstatus,
                    @banktrxid = obj123.banktrxid,
                    @clienttrxid = obj123.clienttrxid,
                    @amount = obj123.amount1,
                    @feeamount = (obj123.feeamount == "" ? "0" : obj123.feeamount),
                    @gst = (obj123.gst == "" ? "0" : obj123.gst),
                    @commission = (obj123.commission == "" ? "0" : obj123.commission),
                    @paymode = obj123.paymode,
                    @Reason = obj123.Reason,
                    @banktxndate = obj123.banktxndate,
                    @apitxnid = obj123.apitxnid,
                    @responsevia = "DoubleVerification"

                }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                if (ObjFees.Id <= 0)
                {
                    ObjFees.Status = false;

                }
                return ObjFees;
            }
        }
        public ScrutinyFeesSubmit FeessubStudentPushresponseDoubleverificationScrutiny(ScrutinyFeesSubmit obj123)
        {
            ScrutinyFeesSubmit ObjFees = new ScrutinyFeesSubmit();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                ObjFees = conn.Query<ScrutinyFeesSubmit>("[sp_student_Scrutinyfee]", new
                {
                    @applicationno = obj123.ApplicationNo,
                    @Requestdata = obj123.Requestdata,
                    @dRequestdata = obj123.dRequestdata,
                    @status = obj123.PGstatus,
                    @banktrxid = obj123.banktrxid,
                    @clienttrxid = obj123.clienttrxid,
                    @amount = obj123.amount1,
                    @feeamount = (obj123.feeamount == "" ? "0" : obj123.feeamount),
                    @gst = (obj123.gst == "" ? "0" : obj123.gst),
                    @commission = (obj123.commission == "" ? "0" : obj123.commission),
                    @paymode = obj123.paymode,
                    @Reason = obj123.Reason,
                    @banktxndate = obj123.banktxndate,
                    @apitxnid = obj123.apitxnid,
                    @responsevia = "DoubleVerification"

                }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                if (ObjFees.Id <= 0)
                {
                    ObjFees.Status = false;

                }
                return ObjFees;
            }
        }
        public ScrutinyFeesSubmit FeessubStudentPushresponseDoubleverificationScrutiny_backlog(ScrutinyFeesSubmit obj123)
        {
            ScrutinyFeesSubmit ObjFees = new ScrutinyFeesSubmit();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                ObjFees = conn.Query<ScrutinyFeesSubmit>("[sp_student_Scrutinyfee_backyear]", new
                {
                    //@applicationno = obj123.ApplicationNo,
                    //@Requestdata = obj123.Requestdata,
                    //@dRequestdata = obj123.dRequestdata,
                    //@status = obj123.PGstatus,
                    //@banktrxid = obj123.banktrxid,
                    //@clienttrxid = obj123.clienttrxid,
                    //@amount = obj123.amount1,
                    //@feeamount = (obj123.feeamount == "" ? "0" : obj123.feeamount),
                    //@gst = (obj123.gst == "" ? "0" : obj123.gst),
                    //@commission = (obj123.commission == "" ? "0" : obj123.commission),
                    //@paymode = obj123.paymode,
                    //@Reason = obj123.Reason,
                    //@banktxndate = obj123.banktxndate,
                    //@apitxnid = obj123.apitxnid,
                    //@responsevia = "DoubleVerification"

                    @applicationno = obj123.ApplicationNo,
                    @Requestdata = obj123.Requestdata,
                    @dRequestdata = obj123.dRequestdata,
                    @status = obj123.PGstatus,
                    @banktrxid = obj123.banktrxid,
                    @clienttrxid = obj123.clienttrxid,
                    @amount = obj123.amount1,
                    @feeamount = (obj123.feeamount == "" ? "0" : obj123.feeamount),
                    @gst = (obj123.gst == "" ? "0" : obj123.gst),
                    @commission = (obj123.commission == "" ? "0" : obj123.commission),
                    @paymode = obj123.paymode,
                    @Reason = obj123.Reason,
                    @banktxndate = obj123.banktxndate,
                    @apitxnid = obj123.apitxnid,
                    @responsevia = "DoubleVerification",
                    @currentyear = System.DateTime.Now.Year,
                    @courseyearid = obj123.courseyearid
                }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                if (ObjFees.Id <= 0)
                {
                    ObjFees.Status = false;

                }
                return ObjFees;
            }
        }
    }

}
