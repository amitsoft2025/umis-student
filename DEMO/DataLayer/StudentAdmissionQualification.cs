
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace DataLayer
{
    public class StudentAdmissionQualification
    {

        public int ID { get; set; }
        public int QualicationType { get; set; }
        public string Board_UniversityName { get; set; }
        public decimal Percentage { get; set; }
        public string PassingYear { get; set; }
        public DateTime Createdate { get; set; }
        public string IPaddress { get; set; }
        public string ApplicationNo { get; set; }
        public int session { get; set; }
        public string FileURl { get; set; }
        public string Msg { get; set; }
        public string QualificationTypeName { get; set; }
        public bool Status { get; set; }
        public string file { get; set; }
        public string hfile { get; set; }
        public string RollNo { get; set; }
        public int SID { get; set; }
        public int ScopeIdentity { get; set; }
        public string EncriptedID { get; set; }
        public int TotalMarksQ { get; set; }
        public int obtainedMarks { get; set; }
        public string sublist { get; set; }
        public string subper { get; set; }
        public string TotalMarks { get; set; }
        public string MarksObtain { get; set; }
        public string SubID { get; set; }
        public int InsertedBy { get; set; }
        public int boardtype { get; set; }
        public decimal HonoursPercentage { get; set; }
        public string paperTotalMarks { get; set; }
        public string paperMarksObtain { get; set; }
        public string totaobtaindmasks { get; set; }
        public string totalpapermasks { get; set; }
        public string marksType { get; set; }
        


        public StudentAdmissionQualification SaveQualificationDetails(StudentAdmissionQualification ob)
        {
            var IP = CommonMethod.GetIPAddress();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var obj = conn.Query<StudentAdmissionQualification>("[sp_StudentQualification]", new
                {
                    @Action = "Insert",
                    @ID = ob.ID,
                    @QualicationType = ob.QualicationType,
                    @PassingYear = CommonSetting.RemoveSpecialChars(ob.PassingYear),
                    @Percentage = ob.Percentage,
                    @Board_UniversityName = CommonSetting.RemoveSpecialChars(ob.Board_UniversityName),
                    @IPaddress = IP,
                    @ApplicationNo = ob.ApplicationNo,
                    @session = ob.session,
                    @DocumentURl = ob.FileURl,
                    @RollNo = CommonSetting.RemoveSpecialChars(ob.RollNo),
                    @SID = ob.SID,
                    @totalpapermasks = ob.paperTotalMarks,
                    @totaobtaindmasks = ob.paperMarksObtain,
                    @boardtype=ob.boardtype,
                    @marksType= ob.marksType
                }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return obj;
            }

        }
        public StudentAdmissionQualification GetQualifiationByID(int id = 0)
        {
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var obj = conn.Query<StudentAdmissionQualification>("sp_StudentQualification", new { Action = "viewbyid", ID = id }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return obj;
            }

        }
        public StudentAdmissionQualification DeleteQualifiationByID(int id = 0)
        {
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var obj = conn.Query<StudentAdmissionQualification>("sp_StudentQualification", new { Action = "delete", ID = id }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return obj;
            }

        }
        public StudentAdmissionQualification DeleteQualifiationByIDwithoutchoice(int id = 0)
        {
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var obj = conn.Query<StudentAdmissionQualification>("sp_StudentQualification", new { Action = "deletewithoutchoice", ID = id }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return obj;
            }

        }
        public StudentAdmissionQualification Addincomedocument(int id = 0,string documenturl ="")
        {
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var obj = conn.Query<StudentAdmissionQualification>("[Sp_IncomeCertification_Verify]", new { Action = "uploaddocument", @SID = id , @documenturl = documenturl }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return obj;
            }

        }
        public StudentAdmissionQualification Addmigrationdocument(int id = 0, string documenturl = "",int SessionID=0)
        {
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var obj = conn.Query<StudentAdmissionQualification>("[Sp_mirationCertification_Verify]", new { Action = "uploaddocument", @SID = id, @documenturl = documenturl, @SessionID= SessionID }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return obj;
            }

        }
        public StudentAdmissionQualification DeleteQualifiationByID_old(int id = 0)
        {
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var obj = conn.Query<StudentAdmissionQualification>("sp_StudentQualification_old", new { Action = "delete", ID = id }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return obj;
            }

        }
        public StudentAdmissionQualification student_save_boardtype(int sid = 0,int boardype=0)
        {
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var obj = conn.Query<StudentAdmissionQualification>("sp_student_saveboardtype", new { @action = "save", @sid = sid, @boardtype= boardype }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return obj;
            }

        }
        public List<QualifiationMaster> getqualificationst(int educationid,string action= "getqualificationst")
        {
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var obj = conn.Query<QualifiationMaster>("sp_qulification_condition", new { @action = action , @educationid = educationid }, commandType: CommandType.StoredProcedure).ToList();

                //  var obj = conn.Query<QualifiationMaster>(" selecT * From tbl_Student_Admission_Qualifiation_Master where educationtype like '%," + educationid + ",%' and Isactive=1").ToList();
                return obj;
            }

        }
        public List<QualifiationMaster> GetQualifiationMaster(int edu = 0, int pre = 0, string edit = "", int sid = 0)
        {

            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                if (edit == "")
                {
                    var obj = conn.Query<QualifiationMaster>("sp_qulification_condition", new { @action = "GetQualifiationMaster", @educationid = edu, @pre = pre, @sid = sid }, commandType: CommandType.StoredProcedure).ToList();
                    return obj;
                }
                else
                {

                    var obj = conn.Query<QualifiationMaster>("sp_qulification_condition", new { @action = "GetQualifiationMasteredit", @educationid = edu, @pre = pre }, commandType: CommandType.StoredProcedure).ToList();
                    return obj;
                }

                //if (edu == 11)
                //{
                //    var obj = conn.Query<QualifiationMaster>("Select * from tbl_Student_Admission_Qualifiation_Master where Isactive=1 and Id In(1) or Id='" + pre + "'").ToList();
                //    return obj;
                //}
                //if (edu == 12)
                //{
                //    var obj = conn.Query<QualifiationMaster>("Select * from tbl_Student_Admission_Qualifiation_Master where Isactive=1 and Id In(1,2,3,4) or Id='" + pre + "'").ToList();
                //    return obj;
                //}
                //else
                //{
                //    var obj = conn.Query<QualifiationMaster>("Select * from tbl_Student_Admission_Qualifiation_Master where Isactive=1").ToList();
                //    return obj;
                //}

                //if (edu == 11)
                //{
                //    var obj = conn.Query<QualifiationMaster>("Select * from tbl_Student_Admission_Qualifiation_Master where Isactive=1 and Id In(1) or Id='" + pre + "'").ToList();
                //    return obj;
                //}
                if (edu == 11)
                {
                    var obj = conn.Query<QualifiationMaster>("Select * from tbl_Student_Admission_Qualifiation_Master where Isactive=1 and Id In(1) or Id='" + pre + "'").ToList();
                    return obj;
                }
                if (edu == 12)
                {
                    var obj = conn.Query<QualifiationMaster>("Select * from tbl_Student_Admission_Qualifiation_Master where Isactive=1 and Id In(1,2,3,4) or Id='" + pre + "'").ToList();
                    return obj;
                }
                else
                {
                    var obj = conn.Query<QualifiationMaster>("Select * from tbl_Student_Admission_Qualifiation_Master where Isactive=1").ToList();
                    return obj;
                }
            }


        }
        public List<QualifiationMaster> GetQualifiationMasterBYyear(int edu = 0)
        {

            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var obj = conn.Query<QualifiationMaster>("sp_qulification_condition", new { @action = "GetQualifiationMasterBYyear", @educationid = edu }, commandType: CommandType.StoredProcedure).ToList();
                return obj;
                //var obj = conn.Query<QualifiationMaster>("Select * from tbl_Student_Admission_Qualifiation_Master where Isactive=1 and Id='"+edu+"'").ToList();
                //    return obj;

            }


        }
        public QualifiationMasterList QualificationdetailList(int pageIndex1 = 1, int pageSize1 = 25)
        {
            var app = ClsLanguage.GetCookies("NBApplicationNo");
            QualifiationMasterList list = new QualifiationMasterList();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var obj = conn.QueryMultiple("[sp_StudentQualification]", new { Action = "view", PageIndex = pageIndex1, pageSize = pageSize1, @ApplicationNo = app }, commandType: CommandType.StoredProcedure);
                list.qlist = obj.Read<StudentAdmissionQualification>().ToList();
                list.totalCount = obj.Read<string>().FirstOrDefault();
            }
            return list;
        }
        //byApplication
        public List<StudentAdmissionQualification> GetQualifiationByApplication(string Application = "")
        {
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var obj = conn.Query<StudentAdmissionQualification>("sp_StudentQualification", new { Action = "byApplication", @ApplicationNo = Application }, commandTimeout: 500000,commandType: CommandType.StoredProcedure).ToList();
                return obj;
            }

        }
        public StudentAdmissionQualification GetQualifiationByApplicationAndQA(string Application = "")
        {
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var obj = conn.Query<StudentAdmissionQualification>("sp_StudentQualification", new { Action = "byApplicationQA", @ApplicationNo = Application }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return obj;
            }

        }
        //QualifiationMaster
        public List<QualifiationMaster> GetQualifiation()
        {
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                //var obj = conn.Query<QualifiationMaster>("select * from tbl_Student_Admission_Qualifiation_Master").ToList();
                var obj = conn.Query<QualifiationMaster>("[sp_Common_QueryMethod]", new
                {
                    @Action = "Qualification",
                }, commandType: CommandType.StoredProcedure).ToList();
                return obj;
            }

        }
        public StudentAdmissionQualification SaveQualificationDetailsPG(StudentAdmissionQualification ob)
        {
            var IP = CommonMethod.GetIPAddress();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var obj = conn.Query<StudentAdmissionQualification>("[sp_StudentQualification_pg]", new
                {
                    @Action = "Insert",
                    @ID = ob.ID,
                    @QualicationType = ob.QualicationType,
                    @PassingYear = ob.PassingYear,
                    @Percentage = ob.Percentage,
                    @Board_UniversityName = ob.Board_UniversityName,
                    @IPaddress = IP,
                    @ApplicationNo = ob.ApplicationNo,
                    @session = ob.session,
                    @TotalMarksQ = ob.TotalMarksQ,
                    @obtainedMarks = ob.obtainedMarks,
                    @DocumentURl = ob.FileURl,
                    @RollNo = ob.RollNo,
                    @SID = ob.SID,
                    @MarksObtain = ob.MarksObtain,
                    @sublist = ob.sublist,
                    @subper = ob.subper,
                    @TotalMarks = ob.TotalMarks,
                    @InsertedBy = ob.InsertedBy,
                    @SubID = ob.SubID
                }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return obj;
            }

        }
        public StudentAdmissionQualification Checkpassingyear(string year = "", int sid = 0, string id = "")
        {
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var obj = conn.Query<StudentAdmissionQualification>("[sp_StudentQualification_pg]", new { Action = "checkyear", @SID = sid, @PassingYear = year, @id = id }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return obj;
            }

        }
        public List<QualifiationMaster> GetQualifiationMasterOldStudent()
        {
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var obj = conn.Query<QualifiationMaster>("sp_AddStudentDataUG", new { @action = "GetQualifiationMaster" }, commandType: CommandType.StoredProcedure).ToList();
                return obj;
            }
        }
        public List<QualifiationMaster> GetQualifiationMasterOldStudentPG()
        {
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var obj = conn.Query<QualifiationMaster>("sp_AddStudentDataPG", new { @action = "GetQualifiationMaster" }, commandType: CommandType.StoredProcedure).ToList();
                return obj;
            }
        }
        public List<QualifiationMaster> GetQualifiationMasterOldStudentBpharma()
        {
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var obj = conn.Query<QualifiationMaster>("sp_AddStudentDataPG", new { @action = "GetQualifiationMasterBpharma" }, commandType: CommandType.StoredProcedure).ToList();
                return obj;
            }
        }
        public StudentAdmissionQualification SaveQualificationDetailsForOldStudent(StudentAdmissionQualification ob)
        {
            var IP = CommonMethod.GetIPAddress();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var obj = conn.Query<StudentAdmissionQualification>("[sp_AddStudentDataUG]", new
                {
                    @Action = "SaveQualification",
                    @ID = ob.ID,
                    @QualicationType = ob.QualicationType,
                    @PassingYear = ob.PassingYear,
                    @Percentage = ob.Percentage,
                    @Board_UniversityName = ob.Board_UniversityName,
                    @IPaddress = IP,
                    @ApplicationNo = ob.ApplicationNo,
                    @session = ob.session,
                    @DocumentURl = ob.FileURl,
                    @RollNo = ob.RollNo,
                    @SID = ob.SID,
                    @totalpapermasks = ob.paperTotalMarks,
                    @totaobtaindmasks = ob.paperMarksObtain,
                    @boardtype = ob.boardtype
                }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return obj;
            }

        }
        public StudentAdmissionQualification SaveQualificationDetailsForOldStudentPG(StudentAdmissionQualification ob)
        {
            var IP = CommonMethod.GetIPAddress();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var obj = conn.Query<StudentAdmissionQualification>("[sp_AddStudentDataPG]", new
                {
                    @Action = "SaveQualification",
                    @ID = ob.ID,
                    @QualicationType = ob.QualicationType,
                    @PassingYear = ob.PassingYear,
                    @Percentage = ob.Percentage,
                    @Board_UniversityName = ob.Board_UniversityName,
                    @IPaddress = IP,
                    @ApplicationNo = ob.ApplicationNo,
                    @session = ob.session,
                    @DocumentURl = ob.FileURl,
                    @RollNo = ob.RollNo,
                    @SID = ob.SID,
                    @HonoursPercentage=ob.HonoursPercentage
                }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return obj;
            }

        }

    }
    public class QualifiationMaster
    {
        public int ID { get; set; }
        public string QualificationType { get; set; }
        public bool Isactive { get; set; }
    }
    public class QualifiationMasterList
    {
        public List<StudentAdmissionQualification> qlist { get; set; }
        public string totalCount { get; set; }
    }
    public class StudentPreviousQualification
    {
        public int ID { get; set; }
        public int QualificationMasterID { get; set; }
        public int SubjectID { get; set; }
        public decimal SubjectPercentage { get; set; }
        public bool Status { get; set; }
        public string Msg { get; set; }
        public string SubjectName { get; set; }
        public int TotalMarks { get; set; }
        public int MarksObtain { get; set; }
        public int paperTotalMarks { get; set; }
        public int paperMarksObtain { get; set; }
        //Subject
        public List<SubjectMaster> GetLLSubject()
        {
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var obj = conn.Query<SubjectMaster>("sp_StudentQualification", new { Action = "SubjectLL" }, commandType: CommandType.StoredProcedure).ToList();
                return obj;
            }

        }
        public List<SubjectMaster> GetNBSubject()
        {
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var obj = conn.Query<SubjectMaster>("sp_StudentQualification", new { Action = "SubjectNB" }, commandType: CommandType.StoredProcedure).ToList();
                return obj;
            }

        }
        public List<SubjectMaster> GetNRBSubject()
        {
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var obj = conn.Query<SubjectMaster>("sp_StudentQualification", new { Action = "SubjectNRB" }, commandType: CommandType.StoredProcedure).ToList();
                return obj;
            }

        }
        public List<SubjectMaster> GetSubject(int id = 0, string board = "")
        {
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                if (board == "1")
                {
                    var obj = conn.Query<SubjectMaster>("sp_StudentQualification", new { Action = "Subject", @sub = id }, commandType: CommandType.StoredProcedure).ToList();
                    return obj;
                }
                else
                {
                    var obj = conn.Query<SubjectMaster>("sp_StudentQualification", new { Action = "Subjectotherboard", @sub = id }, commandType: CommandType.StoredProcedure).ToList();
                    return obj;
                }
            }
        }



        public List<SubjectMaster> GetSubjectBYID(int id = 0, string res = "", string board = "")
        {
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                if (board == "1")
                {
                    var obj = conn.Query<SubjectMaster>("sp_StudentQualification", new { Action = "SubjectByID", @sub = id, @ApplicationNo = res }, commandType: CommandType.StoredProcedure).ToList();
                    return obj;
                }
                else
                {
                    var obj = conn.Query<SubjectMaster>("sp_StudentQualification", new { Action = "SubjectByIDotherboard", @sub = id, @ApplicationNo = res }, commandType: CommandType.StoredProcedure).ToList();
                    return obj;
                }
            }

        }
        public bool Insert(List<StudentPreviousQualification> obj)
        {

            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                for (int i = 0; i < obj.Count; i++)
                {
                    var objgold = conn.Query<StudentPreviousQualification>("[sp_StudentQualification]", new
                    {
                        Action = "insertSub",
                        ID = obj[i].ID,
                        QualificationMasterID = obj[i].QualificationMasterID,
                        SubjectID = obj[i].SubjectID,
                        SubjectPercentage = obj[i].SubjectPercentage,
                        TotalMarks = obj[i].TotalMarks,
                        MarksObtain = obj[i].MarksObtain,


                    }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                    if (objgold != null)
                    {
                        obj[i].Msg = objgold.Msg;
                        if (objgold.Status == true)
                        {
                            obj[i].Status = true;

                        }


                    }
                    else
                    {
                        obj[i].Msg = objgold.Msg;
                        return false;
                    }
                }
                return true;

            }
        }
        public bool Insertsingle(string id, int QualificationMasterID, string SubjectID, string SubjectPercentage, string TotalMarks, string MarksObtain)
        {

            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var objgold = conn.Query<StudentPreviousQualification>("[sp_StudentQualification]", new
                {
                    Action = "insertSub",
                    ID = id,
                    QualificationMasterID = QualificationMasterID,
                    SubjectID = SubjectID,
                    SubjectPercentage = SubjectPercentage,
                    TotalMarks = TotalMarks,
                    MarksObtain = MarksObtain,
                }, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
            return true;
        }

        public List<StudentPreviousQualification> GetSubjectPercentageData(string id = "")
        {
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var obj = conn.Query<StudentPreviousQualification>("sp_StudentQualification", new { Action = "SubjectPer", @ApplicationNo = id }, commandTimeout:50000,commandType: CommandType.StoredProcedure).ToList();
                return obj;
            }

        }

        public StudentPreviousQualification GetSubjecisdiffrentstream_percentage(string id = "", int QualificationMasterID = 0)
        {
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var obj = conn.Query<StudentPreviousQualification>("sp_StudentQualification", new { Action = "isdiffrentstream_percentage", @SID = id, @QualificationMasterID = QualificationMasterID }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return obj;
            }

        }
        public StudentPreviousQualification PG_honours_percentage_check(string id = "", int QualificationMasterID = 0)
        {
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var obj = conn.Query<StudentPreviousQualification>("sp_StudentQualification", new { Action = "PG_honours_percentage_check", @SID = id, @QualificationMasterID = QualificationMasterID }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return obj;
            }

        }
        public StudentPreviousQualification GetSubjecissamestream_percentage(string id = "", int QualificationMasterID = 0,int coursecategoryid =0)
        {
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                if (coursecategoryid == 3)
                {
                    var obj = conn.Query<StudentPreviousQualification>("sp_StudentQualification", new { Action = "issamestream_percentagecommrse", @SID = id, @QualificationMasterID = QualificationMasterID }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                    return obj;
                }
                else
                {
                    var obj = conn.Query<StudentPreviousQualification>("sp_StudentQualification", new { Action = "issamestream_percentage", @SID = id, @QualificationMasterID = QualificationMasterID }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                    return obj;
                }
            }

        }
        public StudentPreviousQualification ischeck_failedorpass(string id = "", int QualificationMasterID = 0)
        {
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var obj = conn.Query<StudentPreviousQualification>("sp_StudentQualification", new { Action = "ischeck_failedorpass", @SID = id, @QualificationMasterID = QualificationMasterID },commandTimeout:50000, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return obj;
            }

        }
        public StudentPreviousQualification ischeck_bcain_math(string id = "", int QualificationMasterID = 0)
        {
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var obj = conn.Query<StudentPreviousQualification>("sp_StudentQualification", new { Action = "ischeck_bcain_math", @SID = id, @QualificationMasterID = QualificationMasterID }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return obj;
            }

        }
        public StudentPreviousQualification ischeck_biotechin_biology(string id = "", int QualificationMasterID = 0)
        {
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var obj = conn.Query<StudentPreviousQualification>("sp_StudentQualification", new { Action = "ischeck_biotechin_biology", @SID = id, @QualificationMasterID = QualificationMasterID }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return obj;
            }

        }
        public string getqualify_percentage(int coursecategoryid = 0, bool issame_stream = false)
        {
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var obj = conn.Query<StudentPreviousQualification>("sp_StudentQualification", new { Action = "getqualify_percentage", @id = coursecategoryid, @issame_stream = issame_stream },commandTimeout:500000, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return obj.SubjectPercentage.ToString();
            }

        }

    }
    public class SubjectMaster
    {
        public int ID { get; set; }
        public string SubjectName { get; set; }
        public bool IsActive { get; set; }
    }
}

