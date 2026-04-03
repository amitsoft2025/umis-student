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
    public class ExamForm
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
        public bool IsExamFee { get; set; }
        public string City { get; set; }
        public string RollNo { get; set; }
        public string ExamCenter { get; set; }
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
        public int IsExamfeesubmit { get; set; }
        public string IsExamfeesubmitdate { get; set; }
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
        public string Examyear { get; set; }
        public string ExamStartDate { get; set; }
        public string subexamcenter { get; set; }
        public int migrationcertificate_iseligible { get; set; }
        public string migrationcertificate { get; set; }
        public string migrationRejectReaseon { get; set; }


        public ExamForm StudentDetail()
        {
            ExamForm Obj = new ExamForm();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                int StID = (ClsLanguage.GetCookies("NBStID") != null ? Convert.ToInt32(ClsLanguage.GetCookies("NBStID")) : 0);
                int Sission = (ClsLanguage.GetCookies("NBSission") != null ? Convert.ToInt32(ClsLanguage.GetCookies("NBSission")) : 0);
                CommonMethod cmn = new CommonMethod();
                string ip = CommonMethod.GetIPAddress();
                Obj = conn.Query<ExamForm>("[sp_student_ExamFrom]", new { @Action = "StudentDetail", @SID = StID, @session = Sission }, commandType: CommandType.StoredProcedure).FirstOrDefault();

                return Obj;
            }
        }
        public ExamForm student_examform_apply(int sid, int Session, int coursecategoryid, int collegeid, int StreamCategoryID, int courseyearid, int isappearedearlierfail, int electivesubjectid, string type = "", string fileupload = "", int electivesubjectid_2 = 0)
        {
            ExamForm Obj = new ExamForm();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {

                CommonMethod cmn = new CommonMethod();
                string ip = CommonMethod.GetIPAddress();
                Obj = conn.Query<ExamForm>("[sp_student_examform_apply]", new
                {
                    @Action = "isapply",
                    @SID = sid,
                    @sessionid = Session,
                    @coursecategoryid = coursecategoryid,
                    @collegeid = collegeid,
                    @StreamCategoryID = StreamCategoryID,
                    @courseyearid = courseyearid,
                    @isappearedearlierfail = isappearedearlierfail,
                    @electivesubjectid = electivesubjectid,
                    @type = type,
                    @fileupload = fileupload
                    ,
                    @electivesubjectid_2 = electivesubjectid_2
                }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return Obj;
            }
        }

        public ExamForm student_examform_applyprac(int sid, int Session, int coursecategoryid, int collegeid, int StreamCategoryID, int courseyearid, int isappearedearlierfail, int electivesubjectid, string type = "", string fileupload = "", int electivesubjectid_2 = 0)
        {
            ExamForm Obj = new ExamForm();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {

                CommonMethod cmn = new CommonMethod();
                string ip = CommonMethod.GetIPAddress();
                Obj = conn.Query<ExamForm>("[sp_student_examform_apply_prac]", new
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


        public ExamForm student_examform_apply_back(int sid, int Session, int coursecategoryid, int collegeid, int StreamCategoryID, int courseyearid, int isappearedearlierfail, int electivesubjectid, string type = "", string fileupload = "", int currentyear = 0)
        {
            ExamForm Obj = new ExamForm();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {

                CommonMethod cmn = new CommonMethod();
                string ip = CommonMethod.GetIPAddress();
                Obj = conn.Query<ExamForm>("[sp_student_examform_apply_back]", new
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
                    @currentyear = currentyear
                }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return Obj;
            }
        }


        public ExamForm student_examform_editfileupload(int sid, int id, string fileupload = "")
        {
            ExamForm Obj = new ExamForm();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {

                CommonMethod cmn = new CommonMethod();
                string ip = CommonMethod.GetIPAddress();
                Obj = conn.Query<ExamForm>("[sp_student_examform_apply]", new
                {
                    @Action = "editfileupload",
                    @SID = sid,
                    @id = id,
                    @fileupload = fileupload
                }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return Obj;
            }
        }
        public ExamForm StudentDetailForAdmitCard(int courseyearid = 0)
        {
            ExamForm Obj = new ExamForm();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                int StID = (ClsLanguage.GetCookies("NBStID") != null ? Convert.ToInt32(ClsLanguage.GetCookies("NBStID")) : 0);
                int Sission = (ClsLanguage.GetCookies("NBSission") != null ? Convert.ToInt32(ClsLanguage.GetCookies("NBSission")) : 0);
                CommonMethod cmn = new CommonMethod();
                string ip = CommonMethod.GetIPAddress();
                Obj = conn.Query<ExamForm>("[sp_student_ExamFrom]", new { @Action = "StudentDetailForAdmitCard", @SID = StID, @session = Sission, @courseyearid = courseyearid }, commandType: CommandType.StoredProcedure).FirstOrDefault();

                return Obj;
            }
        }
        public ExamForm StudentDetailForAdmitCard_backyear(int courseyearid = 0)
        {
            ExamForm Obj = new ExamForm();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                int StID = (ClsLanguage.GetCookies("NBStID") != null ? Convert.ToInt32(ClsLanguage.GetCookies("NBStID")) : 0);
                int Sission = (ClsLanguage.GetCookies("NBSission") != null ? Convert.ToInt32(ClsLanguage.GetCookies("NBSission")) : 0);
                CommonMethod cmn = new CommonMethod();
                string ip = CommonMethod.GetIPAddress();
                Obj = conn.Query<ExamForm>("[sp_student_ExamFrom_back]", new { @Action = "StudentDetailForAdmitCard", @SID = StID, @session = Sission, @currentyear = System.DateTime.Now.Year, @courseyearid = courseyearid }, commandType: CommandType.StoredProcedure).FirstOrDefault();

                return Obj;
            }
        }
        public PrintExamForm GetAppLicationDataForExamFeeprac(int id)
        {
            //  int StID = (ClsLanguage.GetCookies("NBStID") != null ? Convert.ToInt32(ClsLanguage.GetCookies("NBStID")) : 0);
            int Sission = (ClsLanguage.GetCookies("NBSission") != null ? Convert.ToInt32(ClsLanguage.GetCookies("NBSission")) : 0);
            PrintExamForm objdata = new PrintExamForm();
            EaxmFeesSubmit stlogin = new EaxmFeesSubmit();
            BL_PrintRecipt PritFee = new BL_PrintRecipt();
            ExamForm ob = new ExamForm();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var obj = conn.QueryMultiple("sp_student_ExamFromprac", new { @Action = "StudentDetail", @SID = id, @session = Sission }, commandTimeout: 120545, commandType: CommandType.StoredProcedure);
                if (obj != null)
                {
                    objdata.objExamFrom = obj.Read<ExamForm>().FirstOrDefault();
                    objdata.Examobjfeesubmit = stlogin.pracExamFeesDetail();
                    objdata.objPrintRecipt = PritFee.GetPaymentReciptExamFeeprac();
                    if (objdata.objExamFrom != null)
                    {
                        objdata.subjectlist = ob.FeesDetailSubjectlistprac(objdata.objExamFrom.coursecategoryid, objdata.objExamFrom.StreamCategoryID, objdata.objExamFrom.courseyearid);
                    }
                }
                return objdata;
            }
        }

        public PrintExamForm GetAppLicationDataForExamFee(int id)
        {
            //  int StID = (ClsLanguage.GetCookies("NBStID") != null ? Convert.ToInt32(ClsLanguage.GetCookies("NBStID")) : 0);
            int Sission = (ClsLanguage.GetCookies("NBSission") != null ? Convert.ToInt32(ClsLanguage.GetCookies("NBSission")) : 0);
            PrintExamForm objdata = new PrintExamForm();
            EaxmFeesSubmit stlogin = new EaxmFeesSubmit();
            BL_PrintRecipt PritFee = new BL_PrintRecipt();
            ExamForm ob = new ExamForm();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var obj = conn.QueryMultiple("sp_student_ExamFrom", new { @Action = "StudentDetail", @SID = id, @session = Sission }, commandTimeout: 120545, commandType: CommandType.StoredProcedure);
                if (obj != null)
                {
                    objdata.objExamFrom = obj.Read<ExamForm>().FirstOrDefault();
                    objdata.Examobjfeesubmit = stlogin.ExamFeesDetail();
                    objdata.objPrintRecipt = PritFee.GetPaymentReciptExamFee();
                    if (objdata.objExamFrom != null)
                    {
                        CommonMethod.TraceLogWritetoNotepad("obj1.objExamFrom1", "Exam/ExamFeesSubmit51", "obj1.objExamFrom", "Id");
                        objdata.subjectlist = ob.FeesDetailSubjectlist(objdata.objExamFrom.coursecategoryid, objdata.objExamFrom.StreamCategoryID, objdata.objExamFrom.courseyearid);
                    }
                }
                return objdata;
            }
        }

        public PrintExamForm AirPayGetAppLicationDataForExamFee()
        {

            //int id,int sessionid
            //  int StID = (ClsLanguage.GetCookies("NBStID") != null ? Convert.ToInt32(ClsLanguage.GetCookies("NBStID")) : 0);
            int Sission = (ClsLanguage.GetCookies("NBSission") != null ? Convert.ToInt32(ClsLanguage.GetCookies("NBSission")) : 0);
            PrintExamForm objdata = new PrintExamForm();
            EaxmFeesSubmit stlogin = new EaxmFeesSubmit();
            BL_PrintRecipt PritFee = new BL_PrintRecipt();
            ExamForm ob = new ExamForm();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var obj = conn.QueryMultiple("sp_student_ExamFrom", new { @Action = "StudentDetail", @SID = id, @session = Sission }, commandTimeout: 120545, commandType: CommandType.StoredProcedure);
                if (obj != null)
                {
                    objdata.objExamFrom = obj.Read<ExamForm>().FirstOrDefault();
                    objdata.Examobjfeesubmit = stlogin.ExamFeesDetail();
                    objdata.objPrintRecipt = PritFee.GetPaymentReciptExamFee();
                    if (objdata.objExamFrom != null)
                    {
                        objdata.subjectlist = ob.FeesDetailSubjectlist(objdata.objExamFrom.coursecategoryid, objdata.objExamFrom.StreamCategoryID, objdata.objExamFrom.courseyearid);
                    }
                }
                return objdata;
            }
        }

        public PrintExamForm SAfexPayGetAppLicationDataForExamFee()
        {

            //int id,int sessionid
            //  int StID = (ClsLanguage.GetCookies("NBStID") != null ? Convert.ToInt32(ClsLanguage.GetCookies("NBStID")) : 0);
            int Sission = (ClsLanguage.GetCookies("NBSission") != null ? Convert.ToInt32(ClsLanguage.GetCookies("NBSission")) : 0);
            PrintExamForm objdata = new PrintExamForm();
            EaxmFeesSubmit stlogin = new EaxmFeesSubmit();
            BL_PrintRecipt PritFee = new BL_PrintRecipt();
            ExamForm ob = new ExamForm();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var obj = conn.QueryMultiple("sp_student_ExamFrom", new { @Action = "StudentDetail", @SID = id, @session = Sission }, commandTimeout: 120545, commandType: CommandType.StoredProcedure);
                if (obj != null)
                {
                    objdata.objExamFrom = obj.Read<ExamForm>().FirstOrDefault();
                    objdata.Examobjfeesubmit = stlogin.ExamFeesDetail();
                    objdata.objPrintRecipt = PritFee.GetPaymentReciptExamFee();
                    if (objdata.objExamFrom != null)
                    {
                        objdata.subjectlist = ob.FeesDetailSubjectlist(objdata.objExamFrom.coursecategoryid, objdata.objExamFrom.StreamCategoryID, objdata.objExamFrom.courseyearid);
                    }
                }
                return objdata;
            }
        }
        public PrintExamForm backGetAppLicationDataForExamFee(int id, int courseyearid, int currentyear)
        {
            //jitendra check
            //  int StID = (ClsLanguage.GetCookies("NBStID") != null ? Convert.ToInt32(ClsLanguage.GetCookies("NBStID")) : 0);
            int Sission = (ClsLanguage.GetCookies("NBSission") != null ? Convert.ToInt32(ClsLanguage.GetCookies("NBSission")) : 0);
            PrintExamForm objdata = new PrintExamForm();
            EaxmFeesSubmit stlogin = new EaxmFeesSubmit();
            BL_PrintRecipt PritFee = new BL_PrintRecipt();
            ExamForm ob = new ExamForm();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var obj = conn.QueryMultiple("[sp_student_ExamFrom_back]", new { @Action = "StudentDetail", @SID = id, @session = Sission, @courseyearid = courseyearid, @currentyear = currentyear }, commandType: CommandType.StoredProcedure);
                if (obj != null)
                {
                    objdata.objExamFrom = obj.Read<ExamForm>().FirstOrDefault();
                    objdata.Examobjfeesubmit = stlogin.backExamFeesDetail(objdata.objExamFrom.courseyearid);
                    objdata.objPrintRecipt = PritFee.backGetPaymentReciptExamFee(objdata.objExamFrom.courseyearid);
                    if (objdata.objExamFrom != null)
                    {
                        objdata.subjectlist = ob.backFeesDetailSubjectlist_LLB(objdata.objExamFrom.coursecategoryid, objdata.objExamFrom.StreamCategoryID, objdata.objExamFrom.courseyearid, objdata.objExamFrom.sessionid, objdata.objExamFrom.collegeid, objdata.objExamFrom.sid);
                    }
                }
                return objdata;
            }
        }
        public PrintExamForm backGetAppLicationDataForExamFee_LLB(int id, int courseyearid, int currentyear)
        {
            //  int StID = (ClsLanguage.GetCookies("NBStID") != null ? Convert.ToInt32(ClsLanguage.GetCookies("NBStID")) : 0);
            int Sission = (ClsLanguage.GetCookies("NBSission") != null ? Convert.ToInt32(ClsLanguage.GetCookies("NBSission")) : 0);
            PrintExamForm objdata = new PrintExamForm();
            EaxmFeesSubmit stlogin = new EaxmFeesSubmit();
            BL_PrintRecipt PritFee = new BL_PrintRecipt();
            ExamForm ob = new ExamForm();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var obj = conn.QueryMultiple("[sp_student_ExamFrom_back]", new { @Action = "StudentDetail", @SID = id, @session = Sission, @courseyearid = courseyearid, @currentyear = currentyear }, commandType: CommandType.StoredProcedure);
                if (obj != null)
                {
                    objdata.objExamFrom = obj.Read<ExamForm>().FirstOrDefault();
                    objdata.Examobjfeesubmit = stlogin.backExamFeesDetail(objdata.objExamFrom.courseyearid);
                    objdata.objPrintRecipt = PritFee.backGetPaymentReciptExamFee(objdata.objExamFrom.courseyearid);
                    if (objdata.objExamFrom != null)
                    {
                        objdata.subjectlist = ob.backFeesDetailSubjectlist_LLB(objdata.objExamFrom.coursecategoryid, objdata.objExamFrom.StreamCategoryID, objdata.objExamFrom.courseyearid, objdata.objExamFrom.sessionid, objdata.objExamFrom.collegeid, objdata.objExamFrom.sid);
                    }
                }
                return objdata;
            }

        }

        public PrintExamForm backGetAppLicationDataForExamFee_BPharma(int id, int courseyearid, int currentyear)
        {
            //  int StID = (ClsLanguage.GetCookies("NBStID") != null ? Convert.ToInt32(ClsLanguage.GetCookies("NBStID")) : 0);
            int Sission = (ClsLanguage.GetCookies("NBSission") != null ? Convert.ToInt32(ClsLanguage.GetCookies("NBSission")) : 0);
            PrintExamForm objdata = new PrintExamForm();
            EaxmFeesSubmit stlogin = new EaxmFeesSubmit();
            BL_PrintRecipt PritFee = new BL_PrintRecipt();
            ExamForm ob = new ExamForm();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var obj = conn.QueryMultiple("[sp_student_ExamFrom_back]", new { @Action = "StudentDetail", @SID = id, @session = Sission, @courseyearid = courseyearid, @currentyear = currentyear }, commandType: CommandType.StoredProcedure);
                if (obj != null)
                {
                    objdata.objExamFrom = obj.Read<ExamForm>().FirstOrDefault();
                    objdata.Examobjfeesubmit = stlogin.backExamFeesDetail(objdata.objExamFrom.courseyearid);
                    objdata.objPrintRecipt = PritFee.backGetPaymentReciptExamFee(objdata.objExamFrom.courseyearid);
                    if (objdata.objExamFrom != null)
                    {
                        objdata.subjectlist = ob.backFeesDetailSubjectlist_Bed(objdata.objExamFrom.coursecategoryid, objdata.objExamFrom.StreamCategoryID, objdata.objExamFrom.courseyearid, objdata.objExamFrom.sessionid, objdata.objExamFrom.collegeid, objdata.objExamFrom.sid);
                    }
                }
                return objdata;
            }

        }

        public PrintExamForm backGetAppLicationDataForExamFee_BEd(int id, int courseyearid, int currentyear)
        {
            //  int StID = (ClsLanguage.GetCookies("NBStID") != null ? Convert.ToInt32(ClsLanguage.GetCookies("NBStID")) : 0);
            int Sission = (ClsLanguage.GetCookies("NBSission") != null ? Convert.ToInt32(ClsLanguage.GetCookies("NBSission")) : 0);
            PrintExamForm objdata = new PrintExamForm();
            EaxmFeesSubmit stlogin = new EaxmFeesSubmit();
            BL_PrintRecipt PritFee = new BL_PrintRecipt();
            ExamForm ob = new ExamForm();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var obj = conn.QueryMultiple("[sp_student_ExamFrom_back]", new { @Action = "StudentDetail", @SID = id, @session = Sission, @courseyearid = courseyearid, @currentyear = currentyear }, commandType: CommandType.StoredProcedure);
                if (obj != null)
                {
                    objdata.objExamFrom = obj.Read<ExamForm>().FirstOrDefault();
                    objdata.Examobjfeesubmit = stlogin.backExamFeesDetail(objdata.objExamFrom.courseyearid);
                    objdata.objPrintRecipt = PritFee.backGetPaymentReciptExamFee(objdata.objExamFrom.courseyearid);
                    if (objdata.objExamFrom != null)
                    {
                        objdata.subjectlist = ob.backFeesDetailSubjectlist_Bed(objdata.objExamFrom.coursecategoryid, objdata.objExamFrom.StreamCategoryID, objdata.objExamFrom.courseyearid, objdata.objExamFrom.sessionid, objdata.objExamFrom.collegeid, objdata.objExamFrom.sid);
                    }
                }
                return objdata;
            }

        }
        public PrintExamForm backGetAppLicationDataForExamFee_PG(int id, int courseyearid, int currentyear)
        {
            //  int StID = (ClsLanguage.GetCookies("NBStID") != null ? Convert.ToInt32(ClsLanguage.GetCookies("NBStID")) : 0);
            int Sission = (ClsLanguage.GetCookies("NBSission") != null ? Convert.ToInt32(ClsLanguage.GetCookies("NBSission")) : 0);
            PrintExamForm objdata = new PrintExamForm();
            EaxmFeesSubmit stlogin = new EaxmFeesSubmit();
            BL_PrintRecipt PritFee = new BL_PrintRecipt();
            ExamForm ob = new ExamForm();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var obj = conn.QueryMultiple("[sp_student_ExamFrom_back]", new { @Action = "StudentDetail", @SID = id, @session = Sission, @courseyearid = courseyearid, @currentyear = currentyear }, commandType: CommandType.StoredProcedure);
                if (obj != null)
                {
                    objdata.objExamFrom = obj.Read<ExamForm>().FirstOrDefault();
                    objdata.Examobjfeesubmit = stlogin.backExamFeesDetail(objdata.objExamFrom.courseyearid);
                    objdata.objPrintRecipt = PritFee.backGetPaymentReciptExamFee(objdata.objExamFrom.courseyearid);
                    if (objdata.objExamFrom != null)
                    {
                        objdata.subjectlist = ob.backFeesDetailSubjectlist_pg(objdata.objExamFrom.coursecategoryid, objdata.objExamFrom.StreamCategoryID, objdata.objExamFrom.courseyearid, objdata.objExamFrom.sessionid, objdata.objExamFrom.collegeid, objdata.objExamFrom.sid);
                    }
                }
                return objdata;
            }

        }
        public PrintExamForm backGetAppLicationDataForExamFeeUG(int id, int courseyearid, int currentyear)
        {
            int Sission = (ClsLanguage.GetCookies("NBSission") != null ? Convert.ToInt32(ClsLanguage.GetCookies("NBSission")) : 0);
            PrintExamForm objdata = new PrintExamForm();
            EaxmFeesSubmit stlogin = new EaxmFeesSubmit();
            BL_PrintRecipt PritFee = new BL_PrintRecipt();
            ExamForm ob = new ExamForm();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var obj = conn.QueryMultiple("[sp_student_ExamFrom_back]", new { @Action = "StudentDetailUG", @SID = id, @session = Sission, @courseyearid = courseyearid, @currentyear = currentyear }, commandTimeout: 120545, commandType: CommandType.StoredProcedure);
                if (obj != null)
                {
                    objdata.objExamFrom = obj.Read<ExamForm>().FirstOrDefault();
                    objdata.Examobjfeesubmit = stlogin.backExamFeesDetail(courseyearid);
                    objdata.objPrintRecipt = PritFee.backGetPaymentReciptExamFee(courseyearid);
                    var paymentstatus = objdata.objExamFrom.IsExamfeesubmit;
                    if (objdata.objExamFrom != null)
                    {
                        objdata.subjectlist = ob.backFeesDetailSubjectlist_UG(objdata.objExamFrom.coursecategoryid, objdata.objExamFrom.StreamCategoryID, courseyearid, objdata.objExamFrom.sessionid, objdata.objExamFrom.collegeid, objdata.objExamFrom.sid);
                    }
                }
                return objdata;
            }
        }

        public PrintExamForm backGetAppLicationDataForExamFeeUG_back(int id, int courseyearid, int currentyear)
        {
            int Sission = (ClsLanguage.GetCookies("NBSission") != null ? Convert.ToInt32(ClsLanguage.GetCookies("NBSission")) : 0);
            PrintExamForm objdata = new PrintExamForm();
            EaxmFeesSubmit stlogin = new EaxmFeesSubmit();
            BL_PrintRecipt PritFee = new BL_PrintRecipt();
            ExamForm ob = new ExamForm();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var obj = conn.QueryMultiple("[sp_student_ExamFrom_back]", new { @Action = "StudentDetailUG_back", @SID = id, @session = Sission, @courseyearid = courseyearid, @currentyear = currentyear }, commandTimeout: 120545, commandType: CommandType.StoredProcedure);
                if (obj != null)
                {
                    objdata.objExamFrom = obj.Read<ExamForm>().FirstOrDefault();
                    objdata.Examobjfeesubmit = stlogin.backExamFeesDetail(courseyearid);
                    objdata.objPrintRecipt = PritFee.backGetPaymentReciptExamFee(courseyearid);
                    var paymentstatus = objdata.objExamFrom.IsExamfeesubmit;
                    if (objdata.objExamFrom != null)
                    {
                        objdata.subjectlist = ob.backFeesDetailSubjectlist_UG(objdata.objExamFrom.coursecategoryid, objdata.objExamFrom.StreamCategoryID, courseyearid, objdata.objExamFrom.sessionid, objdata.objExamFrom.collegeid, objdata.objExamFrom.sid);
                    }
                }
                return objdata;
            }
        }


        public PrintExamForm GetAppLicationDataForEnrollmentFee(int id = 0)
        {
            int StID = (ClsLanguage.GetCookies("NBStID") != null ? Convert.ToInt32(ClsLanguage.GetCookies("NBStID")) : 0);
            int Sission = (ClsLanguage.GetCookies("NBSission") != null ? Convert.ToInt32(ClsLanguage.GetCookies("NBSission")) : 0);
            PrintExamForm objdata = new PrintExamForm();
            FeesSubmit stlogin = new FeesSubmit();
            BL_PrintRecipt PritFee = new BL_PrintRecipt();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var obj = conn.QueryMultiple("[sp_EnrollmentRequest]", new { @Action = "StudentDetail", @SID = StID, @session = Sission }, commandType: CommandType.StoredProcedure);
                if (obj != null)
                {
                    objdata.objExamFrom = obj.Read<ExamForm>().FirstOrDefault();
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

                obj = conn.Query<Login>("[sp_student_ExamFrom]", new { @Action = "StudentFeesDetail", @SID = StID, @session = Sission }, commandType: CommandType.StoredProcedure).FirstOrDefault();
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
        public List<EaxmFeesSubmit> FeesDetailsstructure(int collegeid, int courseCategoryid, int sessionid, int castecategory, int streamCategoryid, int courseyearid, int isconsession = 0)
        {
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                CommonMethod cmn = new CommonMethod();
                string ip = CommonMethod.GetIPAddress();
                var ObjFees = conn.Query<EaxmFeesSubmit>("[sp_student_ExamFrom]", new { @Action = "StudentExamStuctureFeesDetail", @collegeid = collegeid, @session = sessionid, @courseCategoryid = courseCategoryid, @castecategory = castecategory, @streamCategoryid = streamCategoryid, @courseyearid = courseyearid, @isconsession = isconsession }, commandType: CommandType.StoredProcedure).ToList();
                return ObjFees;
            }
        }
        public List<EaxmFeesSubmit> pracFeesDetailsstructure(int collegeid, int courseCategoryid, int sessionid, int castecategory, int streamCategoryid, int courseyearid, int isconsession = 0)
        {
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                CommonMethod cmn = new CommonMethod();
                string ip = CommonMethod.GetIPAddress();
                var ObjFees = conn.Query<EaxmFeesSubmit>("[sp_student_ExamFromprac]", new { @Action = "StudentExamStuctureFeesDetail", @collegeid = collegeid, @session = sessionid, @courseCategoryid = courseCategoryid, @castecategory = castecategory, @streamCategoryid = streamCategoryid, @courseyearid = courseyearid, @isconsession = isconsession }, commandType: CommandType.StoredProcedure).ToList();
                return ObjFees;
            }
        }
        public List<EaxmFeesSubmit> backFeesDetailsstructure(int collegeid, int courseCategoryid, int sessionid, int castecategory, int streamCategoryid, int courseyearid, int isconsession = 0)
        {
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                CommonMethod cmn = new CommonMethod();
                string ip = CommonMethod.GetIPAddress();
                var ObjFees = conn.Query<EaxmFeesSubmit>("[sp_student_ExamFrom_back]", new { @Action = "StudentExamStuctureFeesDetail", @collegeid = collegeid, @session = sessionid, @courseCategoryid = courseCategoryid, @castecategory = castecategory, @streamCategoryid = streamCategoryid, @courseyearid = courseyearid, @isconsession = isconsession }, commandType: CommandType.StoredProcedure).ToList();
                return ObjFees;
            }
        }
        public List<EaxmFeesSubmit> FeesDetailSubjectlist(int courseCategoryid, int streamCategoryid, int courseyearid)
        {
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                CommonMethod cmn = new CommonMethod();
                string ip = CommonMethod.GetIPAddress();
                var ObjFees = conn.Query<EaxmFeesSubmit>("[sp_student_ExamFrom]", new { @Action = "subjectlist", @courseCategoryid = courseCategoryid, @streamCategoryid = streamCategoryid, @courseyearid = courseyearid }, commandTimeout: 120545, commandType: CommandType.StoredProcedure).ToList();
                return ObjFees;
            }
        }
        public List<EaxmFeesSubmit> FeesDetailSubjectlistprac(int courseCategoryid, int streamCategoryid, int courseyearid)
        {
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                CommonMethod cmn = new CommonMethod();
                string ip = CommonMethod.GetIPAddress();
                var ObjFees = conn.Query<EaxmFeesSubmit>("[sp_student_ExamFromprac]", new { @Action = "subjectlist", @courseCategoryid = courseCategoryid, @streamCategoryid = streamCategoryid, @courseyearid = courseyearid }, commandTimeout: 120545, commandType: CommandType.StoredProcedure).ToList();
                return ObjFees;
            }
        }
        public int check_examfeebefore_admissionfee(int courseCategoryid, int streamCategoryid, int courseyearid, int session, int collegeid, int SID)
        {
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                CommonMethod cmn = new CommonMethod();
                EaxmFeesSubmit obj = new EaxmFeesSubmit();
                string ip = CommonMethod.GetIPAddress();

                var ObjFees = conn.Query<int>("[sp_check_examfeebefore_admissionfee]", new { @Action = "check_examfeebefore_admissionfee", @courseCategoryid = courseCategoryid, @streamCategoryid = streamCategoryid, @courseyearid = courseyearid, @sessionid = session, @collegeid = @collegeid, @SID = SID }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return ObjFees;
            }
        }
        public List<EaxmFeesSubmit> backFeesDetailSubjectlist(int courseCategoryid, int streamCategoryid, int courseyearid, int session, int collegeid, int SID)
        {
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                CommonMethod cmn = new CommonMethod();
                List<EaxmFeesSubmit> obj = new List<EaxmFeesSubmit>();
                string ip = CommonMethod.GetIPAddress();
                if (courseCategoryid == Convert.ToInt32(CommonSetting.coursecategory.bca))
                {
                    var ObjFees = conn.Query<EaxmFeesSubmit>("[sp_student_ExamFrom_back]", new { @Action = "findBCA_backpaper", @courseCategoryid = courseCategoryid, @streamCategoryid = 1124, @courseyearid = courseyearid, @session = session, @collegeid = @collegeid, @SID = SID }, commandType: CommandType.StoredProcedure).ToList();
                    obj = ObjFees;
                }
                return obj;
            }
        }
        public List<EaxmFeesSubmit> backFeesDetailSubjectlist_LLB(int courseCategoryid, int streamCategoryid, int courseyearid, int session, int collegeid, int SID)
        {
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                CommonMethod cmn = new CommonMethod();
                List<EaxmFeesSubmit> obj = new List<EaxmFeesSubmit>();
                string ip = CommonMethod.GetIPAddress();
                if (courseCategoryid == Convert.ToInt32(CommonSetting.coursecategory.LLB))
                {
                    var ObjFees = conn.Query<EaxmFeesSubmit>("[sp_student_ExamFrom_back]", new { @Action = "findLLB_backpaper", @courseCategoryid = courseCategoryid, @streamCategoryid = streamCategoryid, @courseyearid = courseyearid, @session = session, @collegeid = @collegeid, @SID = SID }, commandType: CommandType.StoredProcedure).ToList();
                    obj = ObjFees;
                }
                return obj;
            }
        }

        public List<EaxmFeesSubmit> backFeesDetailSubjectlist_Vocational(int courseCategoryid, int streamCategoryid, int courseyearid, int session, int collegeid, int SID)
        {
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                CommonMethod cmn = new CommonMethod();
                List<EaxmFeesSubmit> obj = new List<EaxmFeesSubmit>();
                string ip = CommonMethod.GetIPAddress();
                if (courseCategoryid == Convert.ToInt32(CommonSetting.coursecategory.bba) || courseCategoryid == Convert.ToInt32(CommonSetting.coursecategory.BioTech) || courseCategoryid == Convert.ToInt32(CommonSetting.coursecategory.bca))
                {
                    var ObjFees = conn.Query<EaxmFeesSubmit>("[sp_student_ExamFrom_back]", new { @Action = "findVocational_backpaper", @courseCategoryid = courseCategoryid, @streamCategoryid = streamCategoryid, @courseyearid = courseyearid, @session = session, @collegeid = @collegeid, @SID = SID }, commandTimeout: 120545, commandType: CommandType.StoredProcedure).ToList();
                    obj = ObjFees;
                }
                return obj;
            }
        }

        public List<EaxmFeesSubmit> backFeesDetailSubjectlist_BPharma(int courseCategoryid, int streamCategoryid, int courseyearid, int session, int collegeid, int SID)
        {
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                CommonMethod cmn = new CommonMethod();
                List<EaxmFeesSubmit> obj = new List<EaxmFeesSubmit>();
                string ip = CommonMethod.GetIPAddress();
                if (courseCategoryid == Convert.ToInt32(CommonSetting.coursecategory.BPharma))
                {
                    var ObjFees = conn.Query<EaxmFeesSubmit>("[sp_student_ExamFrom_back]", new { @Action = "findBEd_backpaper", @courseCategoryid = courseCategoryid, @streamCategoryid = streamCategoryid, @courseyearid = courseyearid, @session = session, @collegeid = @collegeid, @SID = SID }, commandType: CommandType.StoredProcedure).ToList();
                    obj = ObjFees;
                }
                return obj;
            }
        }

        public List<EaxmFeesSubmit> backFeesDetailSubjectlist_Bed(int courseCategoryid, int streamCategoryid, int courseyearid, int session, int collegeid, int SID)
        {
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                CommonMethod cmn = new CommonMethod();
                List<EaxmFeesSubmit> obj = new List<EaxmFeesSubmit>();
                string ip = CommonMethod.GetIPAddress();
                if (courseCategoryid == Convert.ToInt32(CommonSetting.coursecategory.BEd))
                {
                    var ObjFees = conn.Query<EaxmFeesSubmit>("[sp_student_ExamFrom_back]", new { @Action = "findBEd_backpaper", @courseCategoryid = courseCategoryid, @streamCategoryid = streamCategoryid, @courseyearid = courseyearid, @session = session, @collegeid = @collegeid, @SID = SID }, commandType: CommandType.StoredProcedure).ToList();
                    obj = ObjFees;
                }
                return obj;
            }
        }


        public List<EaxmFeesSubmit> backFeesDetailSubjectlist_pg(int courseCategoryid, int streamCategoryid, int courseyearid, int session, int collegeid, int SID)
        {
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                CommonMethod cmn = new CommonMethod();
                List<EaxmFeesSubmit> obj = new List<EaxmFeesSubmit>();
                string ip = CommonMethod.GetIPAddress();

                var ObjFees = conn.Query<EaxmFeesSubmit>("[sp_student_ExamFrom_back]", new { @Action = "findPG_backpaper", @courseCategoryid = courseCategoryid, @streamCategoryid = streamCategoryid, @courseyearid = courseyearid, @session = session, @collegeid = @collegeid, @SID = SID }, commandType: CommandType.StoredProcedure).ToList();
                obj = ObjFees;

                return obj;
            }
        }
        public List<EaxmFeesSubmit> backFeesDetailSubjectlist_UG(int courseCategoryid, int streamCategoryid, int courseyearid, int session, int collegeid, int SID)
        {
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                CommonMethod cmn = new CommonMethod();
                List<EaxmFeesSubmit> obj = new List<EaxmFeesSubmit>();
                string ip = CommonMethod.GetIPAddress();
                if (courseCategoryid == Convert.ToInt32(CommonSetting.coursecategory.ba) || courseCategoryid == Convert.ToInt32(CommonSetting.coursecategory.bsc) || courseCategoryid == Convert.ToInt32(CommonSetting.coursecategory.bcomm))
                {
                    var ObjFees = conn.Query<EaxmFeesSubmit>("[sp_student_ExamFrom_back]", new { @Action = "findUG_backpaper", @courseCategoryid = courseCategoryid, @streamCategoryid = streamCategoryid, @courseyearid = courseyearid, @session = session, @collegeid = @collegeid, @SID = SID }, commandTimeout: 120545, commandType: CommandType.StoredProcedure).ToList();
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
        //            var ObjFees = conn.Query<DummyAdmitCard>("[sp_student_ExamFrom_back]", new { @Action = "findUG_backpaper", @courseCategoryid = courseCategoryid,  @courseyearid = courseyearid,  @SID = SID }, commandTimeout: 120545, commandType: CommandType.StoredProcedure).ToList();
        //            obj = ObjFees;
        //        }
        //        return obj;
        //    }
        //}

        public List<EaxmFeesSubmit> ElectiveFeesDetailSubjectlist(int courseCategoryid, int streamCategoryid, int courseyearid, int SubjectType)
        {
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                CommonMethod cmn = new CommonMethod();
                string ip = CommonMethod.GetIPAddress();
                var ObjFees = conn.Query<EaxmFeesSubmit>("[sp_student_ExamFrom]", new { @Action = "Electivesubjectlist", @courseCategoryid = courseCategoryid, @streamCategoryid = streamCategoryid, @courseyearid = courseyearid, @SubjectType = SubjectType }, commandType: CommandType.StoredProcedure).ToList();
                return ObjFees;
            }
        }
        public List<EaxmFeesSubmit> ElectiveFeesDetailSubjectlist_bed_C11(int courseCategoryid, int streamCategoryid, int courseyearid, int SubjectType)
        {
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                CommonMethod cmn = new CommonMethod();
                string ip = CommonMethod.GetIPAddress();
                var ObjFees = conn.Query<EaxmFeesSubmit>("[sp_student_ExamFrom]", new { @Action = "ElectivesubjectlistBed_c11", @courseCategoryid = courseCategoryid, @streamCategoryid = streamCategoryid, @courseyearid = courseyearid, @SubjectType = SubjectType }, commandType: CommandType.StoredProcedure).ToList();
                return ObjFees;
            }
        }
        public List<EaxmFeesSubmit> ElectiveFeesDetailSubjectlist_BPharma_C11(int courseCategoryid, int streamCategoryid, int courseyearid, int SubjectType)
        {
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                CommonMethod cmn = new CommonMethod();
                string ip = CommonMethod.GetIPAddress();
                var ObjFees = conn.Query<EaxmFeesSubmit>("[sp_student_ExamFrom]", new { @Action = "ElectivesubjectlistBed_c11", @courseCategoryid = courseCategoryid, @streamCategoryid = streamCategoryid, @courseyearid = courseyearid, @SubjectType = SubjectType }, commandType: CommandType.StoredProcedure).ToList();
                return ObjFees;
            }
        }

        public List<EaxmFeesSubmit> ElectiveFeesDetailSubjectlist_bed_c7b(int courseCategoryid, int streamCategoryid, int courseyearid, int SubjectType)
        {
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                CommonMethod cmn = new CommonMethod();
                string ip = CommonMethod.GetIPAddress();
                var ObjFees = conn.Query<EaxmFeesSubmit>("[sp_student_ExamFrom]", new { @Action = "ElectivesubjectlistBed_c7b", @courseCategoryid = courseCategoryid, @streamCategoryid = streamCategoryid, @courseyearid = courseyearid, @SubjectType = SubjectType }, commandType: CommandType.StoredProcedure).ToList();
                return ObjFees;
            }
        }

        public List<EaxmFeesSubmit> ElectiveFeesDetailSubjectlist_BPharma_c7b(int courseCategoryid, int streamCategoryid, int courseyearid, int SubjectType)
        {
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                CommonMethod cmn = new CommonMethod();
                string ip = CommonMethod.GetIPAddress();
                var ObjFees = conn.Query<EaxmFeesSubmit>("[sp_student_ExamFrom]", new { @Action = "ElectivesubjectlistBed_c7b", @courseCategoryid = courseCategoryid, @streamCategoryid = streamCategoryid, @courseyearid = courseyearid, @SubjectType = SubjectType }, commandType: CommandType.StoredProcedure).ToList();
                return ObjFees;
            }
        }



        public List<EaxmFeesSubmit> ElectiveFeesDetailSubjectlist_bed_c7a(int courseCategoryid, int streamCategoryid, int courseyearid, int SubjectType)
        {
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                CommonMethod cmn = new CommonMethod();
                string ip = CommonMethod.GetIPAddress();
                var ObjFees = conn.Query<EaxmFeesSubmit>("[sp_student_ExamFrom]", new { @Action = "ElectivesubjectlistBed_c7a", @courseCategoryid = courseCategoryid, @streamCategoryid = streamCategoryid, @courseyearid = courseyearid, @SubjectType = SubjectType }, commandType: CommandType.StoredProcedure).ToList();
                return ObjFees;
            }
        }

        public List<EaxmFeesSubmit> ElectiveFeesDetailSubjectlist_UG_c7a(int courseCategoryid, int streamCategoryid, int courseyearid, int SubjectType)
        {
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                CommonMethod cmn = new CommonMethod();
                string ip = CommonMethod.GetIPAddress();
                var ObjFees = conn.Query<EaxmFeesSubmit>("[sp_student_ExamFrom]", new { @Action = "Set1UG", @courseCategoryid = courseCategoryid, @streamCategoryid = streamCategoryid, @courseyearid = courseyearid, @SubjectType = SubjectType }, commandType: CommandType.StoredProcedure).ToList();
                return ObjFees;
            }
        }

        public List<EaxmFeesSubmit> ElectiveFeesDetailSubjectlist_UG_c7b(int courseCategoryid, int streamCategoryid, int courseyearid, int SubjectType)
        {
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                CommonMethod cmn = new CommonMethod();
                string ip = CommonMethod.GetIPAddress();
                var ObjFees = conn.Query<EaxmFeesSubmit>("[sp_student_ExamFrom]", new { @Action = "Set2UG", @courseCategoryid = courseCategoryid, @streamCategoryid = streamCategoryid, @courseyearid = courseyearid, @SubjectType = SubjectType }, commandType: CommandType.StoredProcedure).ToList();
                return ObjFees;
            }
        }

        public List<EaxmFeesSubmit> ElectiveFeesDetailSubjectlist_BPharma_c7a(int courseCategoryid, int streamCategoryid, int courseyearid, int SubjectType)
        {
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                CommonMethod cmn = new CommonMethod();
                string ip = CommonMethod.GetIPAddress();
                var ObjFees = conn.Query<EaxmFeesSubmit>("[sp_student_ExamFrom]", new { @Action = "ElectivesubjectlistBpharma_Optional", @courseCategoryid = courseCategoryid, @streamCategoryid = streamCategoryid, @courseyearid = courseyearid, @SubjectType = SubjectType }, commandType: CommandType.StoredProcedure).ToList();
                return ObjFees;
            }
        }
        public ExamForm Feesischeckexamfeesubmit(int sid, int session)
        {
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                CommonMethod cmn = new CommonMethod();
                string ip = CommonMethod.GetIPAddress();
                var ObjFees = conn.Query<ExamForm>("[sp_student_ExamFrom]", new { @Action = "ischeckexamfeesubmit", @SID = sid, @session = session }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return ObjFees;
            }
        }
    }
    public class PrintExamForm
    {
        public EaxmFeesSubmit Examobjfeesubmit { get; set; }
        public FeesSubmit objfeesubmit { get; set; }
        public ExamForm objExamFrom { get; set; }
        public BL_PrintRecipt objPrintRecipt { get; set; }
        public List<EaxmFeesSubmit> subjectlist { get; set; }


    }


    public class DummyAdmitCard
    {
        public string Sid { get; set; }
        public string examformsubmit { get; set; }
        public string mainorback { get; set; }
    }
    public class EaxmFeesSubmit
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
        public bool IsExamFee { get; set; }
        public int Currentyear_courseyarid { get; set; }
        public string HonoursSubject { get; set; }
        public string Subsidiary1Subject { get; set; }
        public string Subsidiary2Subject { get; set; }
        public string Compulsory1Subject { get; set; }
        public string Compulsory2Subject { get; set; }
        public string PaymentGetway { get; set; }
        public string Scrutinylist { get; set; }
        public string ScrutinyAmount { get; set; }
        public string TotalSubject { get; set; }
        public string TotalSubjectList { get; set; }
        public string AdmitCardUpload { get; set; }
        public string DocumentUpload { get; set; }

        public string GetWay { get; set; }
        public EaxmFeesSubmit ExamFeesDetail()
        {
            EaxmFeesSubmit ObjFees = new EaxmFeesSubmit();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                CommonMethod.TraceLogWritetoNotepad("obj1.objExamFrom1", "Exam/ExamFeesSubmit621", "id", "session");
                int StID = (ClsLanguage.GetCookies("NBStID") != null ? Convert.ToInt32(ClsLanguage.GetCookies("NBStID")) : 0);
                int Sission = (ClsLanguage.GetCookies("NBSission") != null ? Convert.ToInt32(ClsLanguage.GetCookies("NBSission")) : 0);
                CommonMethod.TraceLogWritetoNotepad("obj1.objExamFrom1", "Exam/ExamFeesSubmit620", StID.ToString(), Sission.ToString());
                //Directory.CreateDirectory("~/Images");
                CommonMethod.TraceLogWritetoNotepad("obj1.objExamFrom1", "Exam/ExamFeesSubmit623", StID.ToString(), Sission.ToString());
                if (StID == 0)
                {
                    CommonMethod.TraceLogWritetoNotepad("obj1.objExamFrom1", "Exam/ExamFeesSubmit522", StID.ToString(), Sission.ToString());
                    ObjFees.Status = false;
                    ObjFees.Message = "Network Error !!";
                    CommonMethod.TraceLogWritetoNotepad("obj1.objExamFrom1", "Exam/ExamFeesSubmit5221", StID.ToString(), Sission.ToString());
                    return ObjFees;
                }
                CommonMethod.TraceLogWritetoNotepad("obj1.objExamFrom1", "Exam/ExamFeesSubmit5222", StID.ToString(), Sission.ToString());
                CommonMethod cmn = new CommonMethod();
                string ip = CommonMethod.GetIPAddress();
                CommonMethod.TraceLogWritetoNotepad("obj1.objExamFrom1", "Exam/ExamFeesSubmit524", StID.ToString(), Sission.ToString());
                ObjFees = conn.Query<EaxmFeesSubmit>("[sp_student_ExamFrom]", new { @Action = "StudentExamFeesDetail", @Sid = StID, @session = Sission }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                CommonMethod.TraceLogWritetoNotepad("obj1.objExamFrom1", "Exam/ExamFeesSubmit525", StID.ToString(), Sission.ToString());
                if (ObjFees == null)
                {
                    CommonMethod.TraceLogWritetoNotepad("obj1.objExamFrom1", "Exam/ExamFeesSubmit526", StID.ToString(), Sission.ToString());
                    EaxmFeesSubmit objnew = new EaxmFeesSubmit();
                    ObjFees = objnew;
                }
                else
                {
                    CommonMethod.TraceLogWritetoNotepad("obj1.objExamFrom1", "Exam/ExamFeesSubmit627", StID.ToString(), Sission.ToString());
                    if (ObjFees.Id <= 0)
                    {
                        CommonMethod.TraceLogWritetoNotepad("obj1.objExamFrom1", "Exam/ExamFeesSubmit628", StID.ToString(), Sission.ToString());
                        ObjFees.Status = false;

                    }
                }

                return ObjFees;
            }
        }
        public EaxmFeesSubmit pracExamFeesDetail()
        {
            EaxmFeesSubmit ObjFees = new EaxmFeesSubmit();
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
                ObjFees = conn.Query<EaxmFeesSubmit>("[sp_student_ExamFromprac]", new { @Action = "StudentExamFeesDetail", @Sid = StID, @session = Sission }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                if (ObjFees == null)
                {
                    EaxmFeesSubmit objnew = new EaxmFeesSubmit();
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
        public EaxmFeesSubmit backExamFeesDetail(int courseyearid)
        {
            EaxmFeesSubmit ObjFees = new EaxmFeesSubmit();
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
                ObjFees = conn.Query<EaxmFeesSubmit>("[sp_student_ExamFrom_back]", new { @Action = "StudentExamFeesDetail", @Sid = StID, @session = Sission, @currentyear = System.DateTime.Now.Year, @courseyearid = courseyearid }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                if (ObjFees == null)
                {
                    EaxmFeesSubmit objnew = new EaxmFeesSubmit();
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
        public EaxmFeesSubmit FeessubExammentbefore(EaxmFeesSubmit obj123)
        {
            EaxmFeesSubmit ObjFees = new EaxmFeesSubmit();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                int Sission = Convert.ToInt32(ClsLanguage.GetCookies("NBSission"));
                CommonMethod cmn = new CommonMethod();
                ObjFees = conn.Query<EaxmFeesSubmit>("[sp_student_Examfee_before]", new
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

        public EaxmFeesSubmit FeessubExammentbeforeAirPay(EaxmFeesSubmit obj123)
        {
            EaxmFeesSubmit ObjFees = new EaxmFeesSubmit();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                int Sission = Convert.ToInt32(ClsLanguage.GetCookies("NBSission"));
                CommonMethod cmn = new CommonMethod();
                ObjFees = conn.Query<EaxmFeesSubmit>("[sp_student_Examfee_before]", new
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


        public EaxmFeesSubmit AirPayFeessubExammentbeforebackyear(EaxmFeesSubmit obj123)
        {
            EaxmFeesSubmit ObjFees = new EaxmFeesSubmit();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                int Sission = Convert.ToInt32(ClsLanguage.GetCookies("NBSission"));
                CommonMethod cmn = new CommonMethod();
                ObjFees = conn.Query<EaxmFeesSubmit>("[sp_student_Examfee_before_backyear]", new
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
                    @courseyearid = obj123.courseyearid,
                    @GetWay = obj123.GetWay
                }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                ObjFees.Status = ObjFees.Status;
                return ObjFees;
            }
        }


        public EaxmFeesSubmit FeessubExammentbeforeSafexPay(EaxmFeesSubmit obj123)
        {
            EaxmFeesSubmit ObjFees = new EaxmFeesSubmit();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                int Sission = Convert.ToInt32(ClsLanguage.GetCookies("NBSission"));
                CommonMethod cmn = new CommonMethod();
                ObjFees = conn.Query<EaxmFeesSubmit>("[sp_student_Examfee_before]", new
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


        public EaxmFeesSubmit FeessubExammentbeforeEaseBuzz(EaxmFeesSubmit obj123)
        {
            EaxmFeesSubmit ObjFees = new EaxmFeesSubmit();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                int Sission = Convert.ToInt32(ClsLanguage.GetCookies("NBSission"));
                CommonMethod cmn = new CommonMethod();
                ObjFees = conn.Query<EaxmFeesSubmit>("[sp_student_Examfee_before]", new
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
        public EaxmFeesSubmit FeessubExammentbeforePract(EaxmFeesSubmit obj123)
        {
            EaxmFeesSubmit ObjFees = new EaxmFeesSubmit();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                int Sission = Convert.ToInt32(ClsLanguage.GetCookies("NBSission"));
                CommonMethod cmn = new CommonMethod();
                ObjFees = conn.Query<EaxmFeesSubmit>("[sp_student_Examfee_beforeprac]", new
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
        public EaxmFeesSubmit FeessubExammentbeforebackyear(EaxmFeesSubmit obj123)
        {
            EaxmFeesSubmit ObjFees = new EaxmFeesSubmit();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                int Sission = Convert.ToInt32(ClsLanguage.GetCookies("NBSission"));
                CommonMethod cmn = new CommonMethod();
                ObjFees = conn.Query<EaxmFeesSubmit>("[sp_student_Examfee_before_backyear]", new
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

        public EaxmFeesSubmit FeessubExammentbeforebackyearNewGetway(EaxmFeesSubmit obj123)
        {
            EaxmFeesSubmit ObjFees = new EaxmFeesSubmit();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                int Sission = Convert.ToInt32(ClsLanguage.GetCookies("NBSission"));
                CommonMethod cmn = new CommonMethod();
                ObjFees = conn.Query<EaxmFeesSubmit>("[sp_student_Examfee_before_backyear]", new
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
                    @courseyearid = obj123.courseyearid,
                    @GetWay= obj123.PaymentGetway
                }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                ObjFees.Status = ObjFees.Status;
                return ObjFees;
            }
        }
        public EaxmFeesSubmit FeessubExam(EaxmFeesSubmit obj123)
        {
            EaxmFeesSubmit ObjFees = new EaxmFeesSubmit();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                int Sission = Convert.ToInt32(ClsLanguage.GetCookies("NBSission"));
                CommonMethod cmn = new CommonMethod();
                ObjFees = conn.Query<EaxmFeesSubmit>("[sp_student_Examfee]", new
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

        public EaxmFeesSubmit AirPayFeessubExam(EaxmFeesSubmit obj123)
        {
            EaxmFeesSubmit ObjFees = new EaxmFeesSubmit();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                //int Sission = Convert.ToInt32(ClsLanguage.GetCookies("NBSission"));
                CommonMethod cmn = new CommonMethod();
                ObjFees = conn.Query<EaxmFeesSubmit>("[sp_student_Examfee]", new
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

        public EaxmFeesSubmit EaseBuzzFeessubExam(EaxmFeesSubmit obj123)
        {
            EaxmFeesSubmit ObjFees = new EaxmFeesSubmit();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                //int Sission = Convert.ToInt32(ClsLanguage.GetCookies("NBSission"));
                CommonMethod cmn = new CommonMethod();
                ObjFees = conn.Query<EaxmFeesSubmit>("[sp_student_Examfee]", new
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


        public EaxmFeesSubmit SafexPayFeessubExam(EaxmFeesSubmit obj123)
        {
            EaxmFeesSubmit ObjFees = new EaxmFeesSubmit();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                //int Sission = Convert.ToInt32(ClsLanguage.GetCookies("NBSission"));
                CommonMethod cmn = new CommonMethod();
                ObjFees = conn.Query<EaxmFeesSubmit>("[sp_student_Examfee]", new
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

        public EaxmFeesSubmit FeessubExamprac(EaxmFeesSubmit obj123)
        {
            EaxmFeesSubmit ObjFees = new EaxmFeesSubmit();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                int Sission = Convert.ToInt32(ClsLanguage.GetCookies("NBSission"));
                CommonMethod cmn = new CommonMethod();
                ObjFees = conn.Query<EaxmFeesSubmit>("[sp_student_Examfeeprac]", new
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


        public EaxmFeesSubmit FeessubExambackyear(EaxmFeesSubmit obj123)
        {
            EaxmFeesSubmit ObjFees = new EaxmFeesSubmit();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                int Sission = Convert.ToInt32(ClsLanguage.GetCookies("NBSission"));
                CommonMethod cmn = new CommonMethod();
                ObjFees = conn.Query<EaxmFeesSubmit>("[sp_student_Examfee_backyear]", new
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
        public EaxmFeesSubmit AirPayFeessubExambackyear(EaxmFeesSubmit obj123)
        {
            EaxmFeesSubmit ObjFees = new EaxmFeesSubmit();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                //int Sission = Convert.ToInt32(ClsLanguage.GetCookies("NBSission"));
                CommonMethod cmn = new CommonMethod();
                ObjFees = conn.Query<EaxmFeesSubmit>("[sp_student_Examfee_backyear]", new
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

        public EaxmFeesSubmit SafexPayFeessubExambackyear(EaxmFeesSubmit obj123)
        {
            EaxmFeesSubmit ObjFees = new EaxmFeesSubmit();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                //int Sission = Convert.ToInt32(ClsLanguage.GetCookies("NBSission"));
                CommonMethod cmn = new CommonMethod();
                ObjFees = conn.Query<EaxmFeesSubmit>("[sp_student_Examfee_backyear]", new
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
        public EaxmFeesSubmit FeessubExamPushresponse(EaxmFeesSubmit obj123)
        {
            EaxmFeesSubmit ObjFees = new EaxmFeesSubmit();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                ObjFees = conn.Query<EaxmFeesSubmit>("[sp_student_Examfee]", new
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
        public EaxmFeesSubmit FeessubExamPushresponseprac(EaxmFeesSubmit obj123)
        {
            EaxmFeesSubmit ObjFees = new EaxmFeesSubmit();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                ObjFees = conn.Query<EaxmFeesSubmit>("[sp_student_ExamFromprac]", new
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
        public EaxmFeesSubmit FeessubExamPushresponse_backyear(EaxmFeesSubmit obj123)
        {
            EaxmFeesSubmit ObjFees = new EaxmFeesSubmit();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                ObjFees = conn.Query<EaxmFeesSubmit>("[sp_student_Examfee_backyear]", new
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
        public EaxmFeesSubmit FeessubStudentPushresponseDoubleverificationExamprac(EaxmFeesSubmit obj123)
        {
            EaxmFeesSubmit ObjFees = new EaxmFeesSubmit();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                ObjFees = conn.Query<EaxmFeesSubmit>("[sp_student_ExamFromprac]", new
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
        public EaxmFeesSubmit FeessubStudentPushresponseDoubleverificationExam(EaxmFeesSubmit obj123)
        {
            EaxmFeesSubmit ObjFees = new EaxmFeesSubmit();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                ObjFees = conn.Query<EaxmFeesSubmit>("[sp_student_Examfee]", new
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
        public EaxmFeesSubmit FeessubStudentPushresponseDoubleverificationExam_backlog(EaxmFeesSubmit obj123)
        {
            EaxmFeesSubmit ObjFees = new EaxmFeesSubmit();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                ObjFees = conn.Query<EaxmFeesSubmit>("[sp_student_Examfee_backyear]", new
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
