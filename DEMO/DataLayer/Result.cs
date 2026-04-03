using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Dapper;
using System.Data;
using System.Web.Mvc;

namespace DataLayer
{
    public class Result
    {
        public int ExaminationID { get; set; }
        public int ProductID { get; set; }
        public string ImagePath { get; set; }
        public HttpPostedFileBaseModelBinder[] files { get; set; }
        public int Allow { get; set; }
        public int ID { get; set; }
        public int CollegeID { get; set; }
        public int Session { get; set; }
        public int InsertedBY { get; set; }
        public string IPAddress { get; set; }
        public string CreateDate { get; set; }
        public int ModifyBY { get; set; }
        public bool IsActive { get; set; }
        public string CollegeIDList { get; set; }
        public int isback { get; set; }
        public bool Status { get; set; }
        public string Msg { get; set; }
        public string EncriptedID { get; set; }
        public string EduTypeID { get; set; }
        public int CourseCategoryID { get; set; }
        public string SubjectID { get; set; }
        public int HounorsSubjectID { get; set; }
        public int StreamCategoryID { get; set; }
        public int CourseYearID { get; set; }
        public string YearName { get; set; }
        public int IsHonourse { get; set; }
        public int Paper1 { get; set; }
        public int Paper2 { get; set; }
        public int Paper3 { get; set; }
        public int Paper4 { get; set; }
        public int show { get; set; }
        public int PaperMinMark { get; set; }
        public int PraticalMarks { get; set; }
        public int PraticalMinMarks { get; set; }
        public int PaperTotalMark { get; set; }
        public string HonourseName { get; set; }
        public int Ispractical { get; set; }
        public int IsCompulsory { get; set; }
        public int IsCompulsory100 { get; set; }
        public int PaperTotalPassingMark { get; set; }
        public int totalEnq { get; set; }
        public int EducationType { get; set; }
        public int SessionID { get; set; }
        public string StudentName { get; set; }
        public string MonthName { get; set; }
        public string CurrentYearName { get; set; }
        public string CurrentDate { get; set; }
        public string Title { get; set; }
        public string CourseCategoryName { get; set; }
        public string SessionName { get; set; }
        public string UpdateDate { get; set; }
        public string streamCategoryName { get; set; }
        public string Fullname { get; set; }
        public string IpNumber { get; set; }
        public string AddDate { get; set; }
        public string HonoursP1 { get; set; }
        public string Honoursp2 { get; set; }
        public string Honoursp3 { get; set; }
        public string Honoursp4 { get; set; }
        public string HonoursPer { get; set; }
        public string Subsidiary1P1 { get; set; }
        public string Subsidiary1Per { get; set; }
        public string Subsidiary2P1 { get; set; }
        public string Subsidiary2Per { get; set; }
        public string CompulsoryP1 { get; set; }
        public string CompulsoryP2 { get; set; }
        public string GenStudies { get; set; }
        public string TotalObetand { get; set; }
        public string Name { get; set; }
        public string EnrollmentNo { get; set; }
        public string RollNo { get; set; }
        public string Honours { get; set; }
        public string Subsidiary1 { get; set; }
        public string Subsidiary2 { get; set; }
        public string Compulsory1 { get; set; }
        public string Compulsory2 { get; set; }
        public string TotalPaperObetand { get; set; }
        // public string SID { get; set; }
        // public string Subsidiary1Pass { get; set; }
        // public string HonoursPass { get; set; }
        // public string Subsidiary2Pass { get; set; }
        // public string CompulsoryPass { get; set; }
        // public string SumHonours { get; set; }
        public string SumSubsidiary1 { get; set; }
        public string SumSubsidiary2 { get; set; }
        public string SumCompulsory { get; set; }
        public string TotalHonours { get; set; }
        public string result { get; set; }
        public string HonoursP12nd { get; set; }
        public string Honoursp22nd { get; set; }
        public string HonoursPer2nd { get; set; }
        public string Subsidiary1P12nd { get; set; }
        public string Subsidiary1Per2nd { get; set; }
        public string Subsidiary2P12nd { get; set; }
        public string Subsidiary2Per2nd { get; set; }
        public string CompulsoryP12nd { get; set; }
        public string CompulsoryP22nd { get; set; }
        public string GenStudies2nd { get; set; }
        // public string TotalPaperObetand2nd { get; set; }
        // public string TotalObetand2nd { get; set; }
        // public string SumHonours2nd { get; set; }
        public string SumSubsidiary12nd { get; set; }
        public string SumSubsidiary22nd { get; set; }
        public string SumCompulsory2nd { get; set; }
        public string TotalHonours2nd { get; set; }
        // public string EducationType2nd { get; set; }
        // public string CourseCategoryID2nd { get; set; }
        // public string SessionID2nd { get; set; }
        // public string CourseYearID2nd { get; set; }
        // public string CollegeID2nd { get; set; }
        // public int SID2nd { get; set; }
        // public string Result2nd { get; set; }
        public string HonoursP13rd { get; set; }
        public string Honoursp23rd { get; set; }
        public string Honoursp33rd { get; set; }
        public string Honoursp43rd { get; set; }
        public string HonoursPer3rd { get; set; }
        public string GenStudies3rd { get; set; }
        // public string TotalPaperObetand3rd { get; set; }
        // public string TotalObetand3rd { get; set; }
        public string SumHonours3rd { get; set; }
        public string TotalHonours3rd { get; set; }
        // public string SID3rd { get; set; }
        // public int EducationType3rd { get; set; }
        // public int CourseCategoryID3rd { get; set; }
        // public int SessionID3rd { get; set; }
        // public int CourseYearID3rd { get; set; }
        // public int CollegeID3rd { get; set; }
        // public string Result3rd { get; set; }
        // public string FinalResult { get; set; }
        // public int TotalCompulsory { get; set; }
        public string Honoursp32nd { get; set; }
        public int GrandTotal { get; set; }
        public int HounorsTotalMarks { get; set; }
        public int HounorsPraTotalMarks { get; set; }
        public int Subsidiary1TotalMarks { get; set; }
        public int Subsidiary1PraTotalMarks { get; set; }
        public int Subsidiary2TotalMarks { get; set; }
        public int Subsidiary2PraTotalMarks { get; set; }
        public int Compulsory1TotalMarks { get; set; }
        public int Compulsory2TotalMarks { get; set; }
        // public int ResultFinal { get; set; }
        // public int ResultFinalNew { get; set; }
        public string Remarks { get; set; }
        // public string division { get; set; }
        // public string SerialNo { get; set; }

        public int HounorsSubjectBak { get; set; }
        public int Subsidiary1Bak { get; set; }
        public int Subsidiary2Bak { get; set; }
        public int Compulsory1Bak { get; set; }
        public int Compulsory2Bak { get; set; }
        // public string HonoursPaperName { get; set; }
        // public string Subsidiary1PaperName { get; set; }
        // public string Subsidiary2PaperName { get; set; }
        // public string Composition1PaperName { get; set; }
        // public string Composition2PaperName { get; set; }

        // public List<SelectListItem> ExamNumberList { get; set; }
        // public List<SelectListItem> EduTypeList { get; set; }
        // public List<SelectListItem> CourseCategoryList { get; set; }
        // public List<SelectListItem> SubjectList { get; set; }
        // public List<SelectListItem> CourseYearIDList { get; set; }
        // public List<SelectListItem> CollegeStudentList { get; set; }
        // public List<CollegeExamCenter> StudentList { get; set; }
        public List<Result> StudentListNew { get; set; }
        // public List<CollegeExamCenter> MultipalImage { get; set; }
        // public int PageIndex { get; set; }
        // public int PageIndex1 { get; set; }
        // public int PageSize { get; set; }
        // public int totalCount { get; set; }
        public int Row { get; set; }
        // public string ResultName { get; set; }
        // public string ApplicationNo { get; set; }
        // public bool searchType { get; set; }
        // public string YearValue { get; set; }


        // public int IsBack { get; set; }
        public int paperbak1 { get; set; }
        public int paperbak2 { get; set; }
        public int paperbak3 { get; set; }
        public int paperbak4 { get; set; }
        public int paperbak5 { get; set; }
        public int paperbak6 { get; set; }

      
        public int paper5 { get; set; }
        public int paper6 { get; set; }

        // //LLB TR keys Start
        // //public int Id { get; set; }
        public string Paper_P1_Th_Max { get; set; }
        public string Paper_P1_Internal_Max { get; set; }
        public string Paper_P1_Th_Min { get; set; }
        public string Paper_P1_Internal_Min { get; set; }
        public string Paper_P1_Th_Obtained { get; set; }
        public string Paper_P1_Internal_Obtained { get; set; }
        public string Paper_P2_Th_Max { get; set; }
        public string Paper_P2_Internal_Max { get; set; }
        public string Paper_P2_Th_Min { get; set; }
        public string Paper_P2_Internal_Min { get; set; }
        public string Paper_P2_Th_Obtained { get; set; }
        public string Paper_P2_Internal_Obtained { get; set; }
        public string Paper_P3_Th_Max { get; set; }
        public string Paper_P3_Internal_Max { get; set; }
        public string Paper_P3_Th_Min { get; set; }
        public string Paper_P3_Internal_Min { get; set; }
        public string Paper_P3_Th_Obtained { get; set; }
        public string Paper_P3_Internal_Obtained { get; set; }
        public string Paper_P4_Th_Max { get; set; }
        public string Paper_P4_Internal_Max { get; set; }
        public string Paper_P4_Th_Min { get; set; }
        public string Paper_P4_Internal_Min { get; set; }
        public string Paper_P4_Th_Obtained { get; set; }
        public string Paper_P4_Internal_Obtained { get; set; }
        public string Paper_P5_Th_Max { get; set; }
        public string Paper_P5_Internal_Max { get; set; }
        public string Paper_P5_Th_Min { get; set; }
        public string Paper_P5_Internal_Min { get; set; }
        public string Paper_P5_Th_Obtained { get; set; }
        public string Paper_P5_Internal_Obtained { get; set; }

        public string Paper_P6_Th_Max { get; set; }
        public string Paper_P6_Internal_Max { get; set; }
        public string Paper_P6_Th_Min { get; set; }
        public string Paper_P6_Internal_Min { get; set; }
        public string Paper_P6_Th_Obtained { get; set; }
        public string Paper_P6_Internal_Obtained { get; set; }

        // public int InsertedBy { get; set; }
        public string TotalPaperMarks { get; set; }
        // public string SerialNumber { get; set; }
        public string TotalPaperMinMarks { get; set; }
        public string Collegename { get; set; }
        public string PaperI { get; set; }
        public string PaperII { get; set; }
        public string PaperIII { get; set; }
        public string PaperIV { get; set; }
        public string PaperV { get; set; }
        public string PaperVI { get; set; }
        public string PaperIc { get; set; }
        public string PaperIIc { get; set; }
        public string PaperIIIc { get; set; }
        public string PaperIVc { get; set; }
        public string PaperVc { get; set; }
        public string PaperVIc { get; set; }

        // public string PaperVII { get; set; }
        // public string PaperEPC1 { get; set; }
        // public string PaperEPC2 { get; set; }
        // public string PaperEPC3 { get; set; }

        // BED properties
        public int ElectiveSubject1_id { get; set; }
        public int ElectiveSubject2_id { get; set; }
        public string Paper_C1_Th_Max { get; set; }
        public string Paper_C1_Internal_Max { get; set; }
        public string Paper_C1_Th_Min { get; set; }
        public string Paper_C1_Internal_Min { get; set; }
        public string Paper_C1_Th_Obtained { get; set; }
        public string Paper_C1_Internal_Obtained { get; set; }

        public string Paper_C2_Th_Max { get; set; }
        public string Paper_C2_Internal_Max { get; set; }
        public string Paper_C2_Th_Min { get; set; }
        public string Paper_C2_Internal_Min { get; set; }
        public string Paper_C2_Th_Obtained { get; set; }
        public string Paper_C2_Internal_Obtained { get; set; }


        public string Paper_C3_Th_Max { get; set; }
        public string Paper_C3_Internal_Max { get; set; }
        public string Paper_C3_Th_Min { get; set; }
        public string Paper_C3_Internal_Min { get; set; }
        public string Paper_C3_Th_Obtained { get; set; }
        public string Paper_C3_Internal_Obtained { get; set; }
        public string Paper_C4_Th_Max { get; set; }
        public string Paper_C4_Internal_Max { get; set; }
        public string Paper_C4_Th_Min { get; set; }
        public string Paper_C4_Internal_Min { get; set; }
        public string Paper_C4_Th_Obtained { get; set; }
        public string Paper_C4_Internal_Obtained { get; set; }

        public string Paper_C5_Th_Max { get; set; }
        public string Paper_C5_Internal_Max { get; set; }
        public string Paper_C5_Th_Min { get; set; }
        public string Paper_C5_Internal_Min { get; set; }
        public string Paper_C5_Th_Obtained { get; set; }
        public string Paper_C5_Internal_Obtained { get; set; }

        public string Paper_C6_Th_Max { get; set; }
        public string Paper_C6_Internal_Max { get; set; }
        public string Paper_C6_Th_Min { get; set; }
        public string Paper_C6_Internal_Min { get; set; }
        public string Paper_C6_Th_Obtained { get; set; }
        public string Paper_C6_Internal_Obtained { get; set; }

        public string Paper_C7_Th_Max { get; set; }
        public string Paper_C7_Internal_Max { get; set; }
        public string Paper_C7_Th_Min { get; set; }
        public string Paper_C7_Internal_Min { get; set; }
        public string Paper_C7_Th_Obtained { get; set; }
        public string Paper_C7_Internal_Obtained { get; set; }

        public string Paper_EPC1_Th_Max { get; set; }
        public string Paper_EPC1_Internal_Max { get; set; }
        public string Paper_EPC1_Th_Min { get; set; }
        public string Paper_EPC1_Internal_Min { get; set; }
        public string Paper_EPC1_Th_Obtained { get; set; }
        public string Paper_EPC1_Internal_Obtained { get; set; }

        public string Paper_EPC2_Th_Max { get; set; }
        public string Paper_EPC2_Internal_Max { get; set; }
        public string Paper_EPC2_Th_Min { get; set; }
        public string Paper_EPC2_Internal_Min { get; set; }
        public string Paper_EPC2_Th_Obtained { get; set; }
        public string Paper_EPC2_Internal_Obtained { get; set; }

        public string Paper_EPC3_Th_Max { get; set; }
        public string Paper_EPC3_Internal_Max { get; set; }
        public string Paper_EPC3_Th_Min { get; set; }
        public string Paper_EPC3_Internal_Min { get; set; }
        public string Paper_EPC3_Th_Obtained { get; set; }
        public string Paper_EPC3_Internal_Obtained { get; set; }

        // public string Paper2ndI { get; set; }
        // public string Paper2ndII { get; set; }
        // public string Paper2ndIII { get; set; }
        // public string Paper2ndIV { get; set; }
        // public string Paper2ndV { get; set; }
        // public string Paper2ndVI { get; set; }
        // public string Paper2ndVII { get; set; }

        // public string Paper2nd_C1_Th_Max { get; set; }
        // public string Paper2nd_C1_Internal_Max { get; set; }
        // public string Paper2nd_C1_Th_Min { get; set; }
        // public string Paper2nd_C1_Internal_Min { get; set; }
        // public string Paper2nd_C1_Th_Obtained { get; set; }
        // public string Paper2nd_C1_Internal_Obtained { get; set; }
        // public string Paper2nd_C2_Th_Max { get; set; }
        // public string Paper2nd_C2_Internal_Max { get; set; }




        // public string Paper2nd_C2_Th_Min { get; set; }
        // public string Paper2nd_C2_Internal_Min { get; set; }
        // public string Paper2nd_C2_Th_Obtained { get; set; }
        // public string Paper2nd_C2_Internal_Obtained { get; set; }
        // public string Paper2nd_C3_Th_Max { get; set; }
        // public string Paper2nd_C3_Internal_Max { get; set; }
        // public string Paper2nd_C3_Th_Min { get; set; }
        // public string Paper2nd_C3_Internal_Min { get; set; }

        // public string Paper2nd_C3_Th_Obtained { get; set; }
        // public string Paper2nd_C3_Internal_Obtained { get; set; }
        // public string Paper2nd_C4_Th_Max { get; set; }
        // public string Paper2nd_C4_Internal_Max { get; set; }
        // public string Paper2nd_C4_Th_Min { get; set; }
        // public string Paper2nd_C4_Internal_Min { get; set; }
        // public string Paper2nd_C4_Th_Obtained { get; set; }
        // public string Paper2nd_C4_Internal_Obtained { get; set; }

        // public string Paper2nd_C5_Th_Max { get; set; }
        // public string Paper2nd_C5_Internal_Max { get; set; }
        // public string Paper2nd_C5_Th_Min { get; set; }
        // public string Paper2nd_C5_Internal_Min { get; set; }
        // public string Paper2nd_C5_Th_Obtained { get; set; }
        // public string Paper2nd_C5_Internal_Obtained { get; set; }
        // public string Paper2nd_EPC1_Th_Max { get; set; }
        // public string Paper2nd_EPC1_Internal_Max { get; set; }


        // public string Paper2nd_EPC1_Th_Min { get; set; }
        // public string Paper2nd_EPC1_Internal_Min { get; set; }
        // public string Paper2nd_EPC1_Th_Obtained { get; set; }
        // public string Paper2nd_EPC1_Internal_Obtained { get; set; }
        // public string Paper2nd_EPC2_Th_Max { get; set; }
        // public string Paper2nd_EPC2_Internal_Max { get; set; }
        // public string Paper2nd_EPC2_Th_Min { get; set; }
        // public string Paper2nd_EPC2_Internal_Min { get; set; }



        // public string Paper2nd_EPC2_Th_Obtained { get; set; }
        // public string Paper2nd_EPC2_Internal_Obtained { get; set; }

        // public string Remarks2nd { get; set; }
        // public string TotalPaperMarks2nd { get; set; }
        // public string SerialNumber2nd { get; set; }
        // public string division2nd { get; set; }
        // public string TotalPaperMinMarks2nd { get; set; }




        // public string Paper2ndThMaxMarks { get; set; }
        // public string Paper2ndThMinMarks { get; set; }
        // public string Paper2ndInternalMaxMarks { get; set; }
        // public string Paper2ndInternalMinMarks { get; set; }
        // public string TotalPaper2ndThObtained { get; set; }
        // public string TotalPaperEPCInternalObtained { get; set; }


        // public string Paper1stThMaxMarks { get; set; }
        // public string Paper1stThMinMarks { get; set; }
        // public string Paper1stInternalMaxMarks { get; set; }
        // public string Paper1stInternalMinMarks { get; set; }
        // public string TotalPaper1stThObtained { get; set; }
        // public string TotalPaperEPCInternalObtained1st { get; set; }
        // public string Education { get; set; }
        //// public string Cours
        //public string Education { get; set; }
        //public string CourseCategory { get; set; }
        //public string GPA { get; set; }

        #region BED Paper Names
        public string C1 { get; set; }
        public string C1_code { get; set; }

        public string C2 { get; set; }
        public string C2_code { get; set; }

        public string C3 { get; set; }
        public string C3_code { get; set; }

        public string C4 { get; set; }
        public string C4_code { get; set; }

        public string C5 { get; set; }
        public string C5_code { get; set; }

        public string C6 { get; set; }
        public string C6_code { get; set; }

        public string C7_a_1 { get; set; }
        public string C7_a_1_code { get; set; }

        public string C7_a_2 { get; set; }
        public string C7_a_2_code { get; set; }

        public string C7_a_3 { get; set; }
        public string C7_a_3_code { get; set; }

        public string EPC_I { get; set; }
        public string EPC_I_code { get; set; }

        public string EPC_2 { get; set; }
        public string EPC_2_code { get; set; }

        public string EPC_3 { get; set; }
        public string EPC_3_code { get; set; }

        public int Paper_C1_back { get; set; }
        public int Paper_C2_back { get; set; }
        public int Paper_C3_back { get; set; }
        public int Paper_C4_back { get; set; }
        public int Paper_C5_back { get; set; }
        public int Paper_C6_back { get; set; }
        public int Paper_C7_back { get; set; }
        public int Paper_EPC1_back { get; set; }
        public int Paper_EPC2_back { get; set; }
        public int Paper_EPC3_back { get; set; }



        #endregion


        public string publicationDate { get; set; }
        public string Examyear { get; set; }
        public List<SelectListItem> ExaminationList { get; set; }
        public List<Result> ProgrammeList { get; set; }
        public List<courseyear> courseyearlist { get; set; }
        public List<SelectListItem> CollegeList { get; set; }
        public List<SelectListItem> GetCollegeList()
        {
            var clg = CommonMethod.MIDcollegewise();
            List<SelectListItem> ddlCollege = new List<SelectListItem>();
            ddlCollege.Add(new SelectListItem { Text = "--Select College--", Value = "" });
            foreach (var dr in clg)
            {
                ddlCollege.Add(new SelectListItem { Text = dr.collegename.ToString(), Value = dr.collegeid.ToString() });
            }
            return ddlCollege;
        }
        public List<SelectListItem> GetCollegeListcoursewise(int coursecategory, int educationtypeid)
        {

            List<SelectListItem> items = new List<SelectListItem>();
            try
            {
                List<BL_CollegeMaster> objdata = new List<BL_CollegeMaster>();
                var Types = objdata;
                if (coursecategory > 0)
                {
                    Types = GetCollegeDatabycollegealloted(6, coursecategory);
                }
                else if (educationtypeid > 0)
                {
                    Types = GetCollegeDatabycollegealloted(7, educationtypeid);
                }
                else
                {
                    Types = GetCollegeData(5);
                }


                if (Types != null)
                {
                    foreach (var p in Types)
                    {
                        items.Add(new SelectListItem { Value = p.ID.ToString(), Text = p.CollegeName });
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return items;
        }
        public List<BL_CollegeMaster> GetCollegeData(int varflag)
        {
            List<BL_CollegeMaster> objdata = new List<BL_CollegeMaster>();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                objdata = conn.Query<BL_CollegeMaster>("[Resultdeclare_CollegeMaster]", new { @flag = varflag }, commandType: CommandType.StoredProcedure).ToList();
            }
            return objdata;
        }
        public List<BL_CollegeMaster> GetCollegeDatabycollegealloted(int varflag, int coursecategory)
        {
            List<BL_CollegeMaster> objdata = new List<BL_CollegeMaster>();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                objdata = conn.Query<BL_CollegeMaster>("[Resultdeclare_CollegeMaster]", new { @flag = varflag, @coursecategoryid = coursecategory }, commandType: CommandType.StoredProcedure).ToList();
            }
            return objdata;
        }
        public List<Result> GetTabulationRegisterBCA(int blankview, string action = "", int Session = 0, int Educationtype = 0, int CourseCategoryID = 0, int CollegeID = 0, int yearid = 0, int StreamCategory = 0, string enrollmentno = "", string ApplicationNo = "", int isback = 0, int Examyear = 0)
        {
            List<Result> obj = new List<Result>();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                if (blankview == 1)
                {

                }
                else
                {
                    if (isback == 0)
                    {
                        obj = conn.Query<Result>("[sp_ResultBCA]", new { @Action = "MainExamCustomSearch", @EducationType = Educationtype, @CourseCategoryID = CourseCategoryID, @CollegeID = CollegeID, @SessionID = Session, @CourseYearID = yearid, @StreamCategoryid = StreamCategory, @enrollmentno = enrollmentno, @ApplicationNo = ApplicationNo, @Examyear = Examyear }, commandTimeout: 120545, commandType: CommandType.StoredProcedure).ToList();
                    }
                    else
                    {
                        obj = conn.Query<Result>("[sp_ResultBCA]", new { @Action = "BackExamCustomSearch", @EducationType = Educationtype, @CourseCategoryID = CourseCategoryID, @CollegeID = CollegeID, @SessionID = Session, @CourseYearID = yearid, @StreamCategoryid = StreamCategory, @enrollmentno = enrollmentno, @ApplicationNo = ApplicationNo, @Examyear = Examyear }, commandTimeout: 120545, commandType: CommandType.StoredProcedure).ToList();
                    }
                }
            }
            return obj;
        }
        public List<Result> Studentportal_GetTabulationRegisterBCA(int blankview, string action = "", int Session = 0, int Educationtype = 0, int CourseCategoryID = 0, int CollegeID = 0, int yearid = 0, int StreamCategory = 0, string rollno = "", string ApplicationNo = "", int isback = 0, int Examyear = 0)
        {
            List<Result> obj = new List<Result>();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                if (blankview == 1)
                {

                }
                else
                {
                    if (isback == 0)
                    {
                        string SQL_Insert = " selecT * from vw_student_result_vocational  WHERE  rollno= @rollno        and collegeid=@collegeid      and is_published=1  and CourseYearID = @courseyearid";
                        obj = conn.Query<Result>(SQL_Insert,
                          new
                          {
                              @rollno = rollno,
                              @collegeid = CollegeID,
                              @courseyearid = yearid


                          }, commandType: CommandType.Text).ToList();
                        //obj = conn.Query<Result>("[sp_ResultBCA]", new { @Action = "MainExamCustomSearch", @EducationType = Educationtype, @CourseCategoryID = CourseCategoryID, @CollegeID = CollegeID, @SessionID = Session, @CourseYearID = yearid, @StreamCategoryid = StreamCategory, @enrollmentno = enrollmentno, @ApplicationNo = ApplicationNo, @Examyear = Examyear }, commandTimeout: 120545, commandType: CommandType.StoredProcedure).ToList();
                    }
                    else
                    {
                        string SQL_Insert = " selecT * from vw_student_result_vocational_back  WHERE  rollno= @rollno        and collegeid=@collegeid      and is_published=1  and CourseYearID = @courseyearid";
                        obj = conn.Query<Result>(SQL_Insert,
                          new
                          {
                              @rollno = rollno,
                              @collegeid = CollegeID,
                              @courseyearid = yearid


                          }, commandType: CommandType.Text).ToList();
                        //obj = conn.Query<Result>("[sp_ResultBCA]", new { @Action = "BackExamCustomSearch", @EducationType = Educationtype, @CourseCategoryID = CourseCategoryID, @CollegeID = CollegeID, @SessionID = Session, @CourseYearID = yearid, @StreamCategoryid = StreamCategory, @enrollmentno = rollno, @ApplicationNo = ApplicationNo, @Examyear = Examyear }, commandTimeout: 120545, commandType: CommandType.StoredProcedure).ToList();
                    }
                }
            }
            return obj;
        }

        public List<Result> CourseYear(int id)
        {
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var objdata = conn.Query<Result>("sp_CourseYear", new { @CourseID = id }, commandType: CommandType.StoredProcedure).ToList();
                return objdata;
            }
        }
        public Result GetBCASubjectCode(int CourceYearID = 0)
        {
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                Result obj = new Result();
                obj = conn.Query<Result>("[sp_GetBCASubjectcode]", new { @Courseyearid = CourceYearID }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return obj;
            }
        }
        public Result GetBCASubject(int CourceYearID = 0)
        {
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                Result obj = new Result();
                obj = conn.Query<Result>("[sp_GetBCASubject]", new { @Courseyearid = CourceYearID }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return obj;
            }
        }
        public List<Result> GetTabulationRegisterUG_studentportal(int blankview, int CourseCategoryID = 0, int CollegeID = 0, int yearid = 0, int StreamCategory = 0, string rollno = "", int isback = 0)
        {
            List<Result> obj = new List<Result>();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                if (blankview == 1)
                {

                }
                else
                {
                    if (isback == 0)
                    {                        
                        
                        
                       // obj = conn.Query<Result>("[sp_UG_declareresult]", new { @Action = "MainexamCustomSearch", @EducationType = Educationtype, @CourseCategoryID = CourseCategoryID, @CollegeID = CollegeID, @SessionID = Session, @CourseYearID = yearid, @StreamCategoryid = StreamCategory, @enrollmentno = enrollmentno, @ApplicationNo = ApplicationNo, @Examyear = Examyear }, commandTimeout: 120545, commandType: CommandType.StoredProcedure).ToList();


                        string SQL_Insert = " select *,case when (cs1Ispractical=1) and (@Streemcategory)<>1049 then '75' when cs1Ispractical=1 and @Streemcategory=1049   then '50' else '100' end as HounorsTotalMarks  from vw_studentresult_UG where RollNo=@rollno  and collegeid=@collegeid      and is_published=1  and CourseYearID = @courseyearid";
                        obj = conn.Query<Result>(SQL_Insert,
                          new
                          {
                              @rollno = rollno,
                              @collegeid = CollegeID,
                              @courseyearid = yearid,
                              @Streemcategory = StreamCategory

                          }, commandType: CommandType.Text).ToList();
                        //  obj = conn.Query<Result>("[sp_UG_declareresult]", new { @Action = "MainexamCustomSearch", @EducationType = Educationtype, @CourseCategoryID = CourseCategoryID, @CollegeID = CollegeID, @SessionID = Session, @CourseYearID = yearid, @StreamCategoryid = StreamCategory, @enrollmentno = enrollmentno, @ApplicationNo = ApplicationNo, @Examyear = Examyear }, commandTimeout: 120545, commandType: CommandType.StoredProcedure).ToList();

                    }
                    else
                    {
                        string SQL_Insert = " select *,case when (cs1Ispractical=1) and (@Streemcategory)<>1049 then '75' when cs1Ispractical=1 and @Streemcategory=1049   then '50' else '100' end as HounorsTotalMarks  from vw_studentresult_UG_back where RollNo=@rollno  and collegeid=@collegeid      and is_published=1  and CourseYearID = @courseyearid";
                        obj = conn.Query<Result>(SQL_Insert,
                          new
                          {
                              @rollno = rollno,
                              @collegeid = CollegeID,
                              @courseyearid = yearid,
                              @Streemcategory = StreamCategory

                          }, commandType: CommandType.Text).ToList();
                        // obj = conn.Query<Result>("[sp_UG_declareresult]", new { @Action = "BackExamCustomSearch", @EducationType = Educationtype, @CourseCategoryID = CourseCategoryID, @CollegeID = CollegeID, @SessionID = Session, @CourseYearID = yearid, @StreamCategoryid = StreamCategory, @enrollmentno = enrollmentno, @ApplicationNo = ApplicationNo, @Examyear = Examyear }, commandTimeout: 120545, commandType: CommandType.StoredProcedure).ToList();
                    }
                }
            }
            return obj;
        }


        //19-04-2021
        public List<Result> Studentportal_GetTabulationRegisterBED(int blankview, string action = "", int Session = 0, int Educationtype = 0, int CourseCategoryID = 0, int CollegeID = 0, int yearid = 0, int StreamCategory = 0, string rollno = "", string ApplicationNo = "", int isback = 0, int Examyear = 0)
        {
            List<Result> obj = new List<Result>();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                if (blankview == 1)
                {

                }
                else
                {
                    if (isback == 0)
                    {
                        string SQL_Insert = " selecT * from [vw_student_result_BED]  WHERE  rollno= @rollno        and collegeid=@collegeid      and is_published=1  and CourseYearID = @courseyearid";
                        obj = conn.Query<Result>(SQL_Insert,
                          new
                          {
                              @rollno = rollno,
                              @collegeid = CollegeID,
                              @courseyearid = yearid


                          },commandTimeout:12000000, commandType: CommandType.Text).ToList();
                        //obj = conn.Query<Result>("[sp_ResultBCA]", new { @Action = "MainExamCustomSearch", @EducationType = Educationtype, @CourseCategoryID = CourseCategoryID, @CollegeID = CollegeID, @SessionID = Session, @CourseYearID = yearid, @StreamCategoryid = StreamCategory, @enrollmentno = enrollmentno, @ApplicationNo = ApplicationNo, @Examyear = Examyear }, commandTimeout: 120545, commandType: CommandType.StoredProcedure).ToList();
                    }
                    else
                    {
                        string SQL_Insert = " selecT * from [vw_student_result_BED_back]  WHERE  rollno= @rollno        and collegeid=@collegeid      and is_published=1  and CourseYearID = @courseyearid";
                        obj = conn.Query<Result>(SQL_Insert,
                          new
                          {
                              @rollno = rollno,
                              @collegeid = CollegeID,
                              @courseyearid = yearid


                          }, commandTimeout: 12000000, commandType: CommandType.Text).ToList();
                        //obj = conn.Query<Result>("[sp_ResultBCA]", new { @Action = "BackExamCustomSearch", @EducationType = Educationtype, @CourseCategoryID = CourseCategoryID, @CollegeID = CollegeID, @SessionID = Session, @CourseYearID = yearid, @StreamCategoryid = StreamCategory, @enrollmentno = rollno, @ApplicationNo = ApplicationNo, @Examyear = Examyear }, commandTimeout: 120545, commandType: CommandType.StoredProcedure).ToList();
                    }
                }
            }
            return obj;
        }

        public Result GetBEDSubject(int CourceYearID = 0, int Coursecategoryid = 0)
        {
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                Result obj = new Result();
                obj = conn.Query<Result>("[sp_GetBED_Subject]", new
                {
                    @Courseyearid = CourceYearID

                }, commandTimeout: 12000000, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return obj;
            }
        }
        public Result GetBEDSubject_single(int CourceYearID = 0, int Substreamcategoryid = 0)
        {
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                Result obj = new Result();
                obj = conn.Query<Result>("select SubjectName as C2,SubjectCode as C2_code  from tbl_coursestreamcategory_sub  WITH (NOLOCK) where Courseyearid =@Courseyearid and IsActive=1 and IsDelete=0 and Substreamcategoryid=@Substreamcategoryid", new
                {
                    @Courseyearid = CourceYearID,
                    @Substreamcategoryid = Substreamcategoryid,
                }, commandTimeout: 12000000,commandType: CommandType.Text).FirstOrDefault();
                return obj;
            }
        }
        //20-04-2021
        public List<Result> Studentportal_GetTabulationRegisterLLB(int CollegeID = 0, int yearid = 0, string rollno = "",  int isback = 0)
        {
            List<Result> obj = new List<Result>();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                    if (isback == 0)
                    {
                        string SQL_Insert = " selecT * from [vw_student_result_LLB]  WHERE  rollno= @rollno        and collegeid=@collegeid      and is_published=1  and CourseYearID = @courseyearid";
                        obj = conn.Query<Result>(SQL_Insert,
                          new
                          {
                              @rollno = rollno,
                              @collegeid = CollegeID,
                              @courseyearid = yearid


                          }, commandTimeout: 12000000, commandType: CommandType.Text).ToList();
                        //obj = conn.Query<Result>("[sp_ResultBCA]", new { @Action = "MainExamCustomSearch", @EducationType = Educationtype, @CourseCategoryID = CourseCategoryID, @CollegeID = CollegeID, @SessionID = Session, @CourseYearID = yearid, @StreamCategoryid = StreamCategory, @enrollmentno = enrollmentno, @ApplicationNo = ApplicationNo, @Examyear = Examyear }, commandTimeout: 120545, commandType: CommandType.StoredProcedure).ToList();
                    }
                    else
                    {
                        // back exam numbers
                        string SQL_Insert = " selecT * from [vw_student_result_LLB_back]  WHERE  rollno= @rollno        and collegeid=@collegeid      and is_published=1  and CourseYearID = @courseyearid";
                        obj = conn.Query<Result>(SQL_Insert,
                          new
                          {
                              @rollno = rollno,
                              @collegeid = CollegeID,
                              @courseyearid = yearid


                          }, commandTimeout: 12000000, commandType: CommandType.Text).ToList();
                        //obj = conn.Query<Result>("[sp_ResultBCA]", new { @Action = "BackExamCustomSearch", @EducationType = Educationtype, @CourseCategoryID = CourseCategoryID, @CollegeID = CollegeID, @SessionID = Session, @CourseYearID = yearid, @StreamCategoryid = StreamCategory, @enrollmentno = rollno, @ApplicationNo = ApplicationNo, @Examyear = Examyear }, commandTimeout: 120545, commandType: CommandType.StoredProcedure).ToList();
                    }
                
            }
            return obj;
        }
        public Result GetLLBSubject(int CourceYearID = 0, int Coursecategoryid = 0)
        {
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                Result obj = new Result();
                obj = conn.Query<Result>("[sp_GetLLB_Subject]", new
                {
                    @Courseyearid = CourceYearID

                }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return obj;
            }
        }
        public Result GetPGSubject(int CourceYearID = 0,int streamcategoryid=0,int coursecategoryid=0)
        {
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                Result obj = new Result();
                obj = conn.Query<Result>("[sp_GetPGSubject]", new { @Courseyearid = CourceYearID , @streamcategoryid = streamcategoryid , @coursecategoryid = coursecategoryid }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return obj;
            }
        }
        public List<Result> Studentportal_GetTabulationRegisterPG(int CollegeID = 0, int yearid = 0, string rollno = "", int isback = 0,int coursecategoryid=0)
        {
            List<Result> obj = new List<Result>();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                if (isback == 0)
                {
                    string SQL_Insert = " selecT * from [vw_student_result_PG]  WHERE  rollno= @rollno        and collegeid=@collegeid      and is_published=1  and CourseYearID = @courseyearid";
                    obj = conn.Query<Result>(SQL_Insert,
                      new
                      {
                          @rollno = rollno,
                          @collegeid = CollegeID,
                          @courseyearid = yearid
                      }, commandTimeout: 12000000, commandType: CommandType.Text).ToList();
                }
                else
                {
                    string SQL_Insert = " selecT * from [vw_student_result_PG_back]  WHERE  rollno= @rollno        and collegeid=@collegeid      and is_published=1  and CourseYearID = @courseyearid";
                    obj = conn.Query<Result>(SQL_Insert,
                      new
                      {
                          @rollno = rollno,
                          @collegeid = CollegeID,
                          @courseyearid = yearid
                      }, commandTimeout: 12000000, commandType: CommandType.Text).ToList();
                 }

            }
            return obj;
        }

    }
    public class ResultList
    {
        public List<Result> qlist { get; set; }
        public string totalCount { get; set; }
    }
}
