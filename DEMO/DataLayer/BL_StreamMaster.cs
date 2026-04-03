using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Dapper;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DataLayer
{
    public class BL_StreamMaster
    {
        public int Flag { get; set; }
        [Required(ErrorMessage = "Type is Required")]
        public int CommonId { get; set; }
        public int StreamCategoryID { get; set; } // PK
        public string Title { get; set; }
        [Required(ErrorMessage = "Category   is Required")]
        public int CourseCategoryID { get; set; }
        [Required(ErrorMessage = "Stream is Required")]
        public string streamCategory { get; set; }
        public string IPaddress { get; set; }
        public int InsertBy { get; set; }
        public int Marks { get; set; }
        public bool IsCompulsory { get; set; }

        public string CourseCategory { get; set; }
        public string SubjectCode { get; set; }
        public bool Ispractical { get; set; }
        public int TotalSeat { get; set; }

        public bool status { get; set; }
        public string Msg { get; set; }
        public int collegeid { get; set; }
        public int hounors_subjectid { get; set; }
        public string hounors_subjectName { get; set; } // PK
        public string CollegeName { get; set; }
        public int SaveStreamData(BL_StreamMaster obj)
        {
            int Result = 0;
            //@,@
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                Result = conn.Execute("USP_StreamMaster", new
                {
                    @flag = obj.Flag,
                    CourseCategoryID = obj.CourseCategoryID,
                    streamCategory = obj.streamCategory,
                    IsActive = true,
                    IsDelete = 1,
                    IPaddress = obj.IPaddress,
                    InsertBy = obj.InsertBy,
                    IsCompulsory = obj.IsCompulsory,
                    Marks = obj.Marks
                }, commandType: CommandType.StoredProcedure);

            }
            return Result;
        }

        public List<BL_StreamMaster> GetSubjectData(int varflag)
        {
            List<BL_StreamMaster> objdata = new List<BL_StreamMaster>();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                objdata = conn.Query<BL_StreamMaster>("USP_StreamMaster",
                    new { @flag = varflag },
                        commandType: CommandType.StoredProcedure).ToList();

            }
            return objdata;
        }

        public BL_StreamMaster GetCollegeDataBYID(int varflag, int StreamID)
        {
            BL_StreamMaster objdata = new BL_StreamMaster();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                objdata = conn.Query<BL_StreamMaster>("USP_StreamMaster", new { @flag = varflag, @StreamCategoryID = StreamID }, commandType: CommandType.StoredProcedure).FirstOrDefault();

            }
            return objdata;
        }
        public List<BL_StreamMaster> getsubjectbycourse(int varflag, int courseid, int collegeid = 0)
        {

            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var objdata = conn.Query<BL_StreamMaster>("USP_StreamMaster", new { @flag = varflag, @CourseCategoryID = courseid, @college = collegeid }, commandType: CommandType.StoredProcedure).ToList();
                return objdata;
            }

        }
        //public List<BL_StreamMaster> getcollegesubjects(int varflag, int collegeid,int subjectid=0,int subsidiary1=0,int compulsory1=0)
        //{

        //    using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
        //    {
        //        var objdata = conn.Query<BL_StreamMaster>("USP_StreamMaster", new { @flag = varflag, @CollegeID = collegeid, @subjectid = subjectid , @subsidiary1 = subsidiary1, @compulsory1=compulsory1 }, commandType: CommandType.StoredProcedure).ToList();
        //        return objdata;
        //    }

        //}
        public int UpdateSubjectdata(BL_StreamMaster obj)
        {
            int Result = 0;
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                Result = conn.Execute("USP_StreamMaster",
                    new
                    {

                        flag = obj.Flag,
                        CourseCategoryID = obj.CourseCategoryID,
                        streamCategory = obj.streamCategory,
                        IPaddress = obj.IPaddress,
                        InsertBy = obj.InsertBy,
                        StreamCategoryID = obj.StreamCategoryID,
                        IsCompulsory = obj.IsCompulsory,
                        Marks = obj.Marks,
                        Ispractical = obj.Ispractical,
                        SubjectCode = obj.SubjectCode

                    }, commandType: CommandType.StoredProcedure);

            }
            return Result;
        }
        public StreamListType GetSubjectDataByType(int courseid = 0)
        {
            StreamListType objdata = new StreamListType();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var obj = conn.QueryMultiple("[USP_StreamMaster]", new { @flag = 7, @type = "Honours", @type1 = "Subsidiary", @courseid = courseid }, commandType: CommandType.StoredProcedure);
                objdata.honourslist = obj.Read<BL_StreamMaster>().ToList();
                objdata.subsidiarylist = obj.Read<BL_StreamMaster>().ToList();
            }
            return objdata;
        }
        public CollegeCourseAllocation SaveData(CollegeCourseAllocation obj)
        {
            AcademicSession ac = new AcademicSession();
            var IP = CommonMethod.GetIPAddress();
            var app = Convert.ToInt32(ClsLanguage.GetCookies("NUserId"));
            var sessionid = ac.GetAcademiccurrentSession().ID;
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var Result = conn.Query<CollegeCourseAllocation>("USP_StreamMaster", new
                {
                    @flag = 8,
                    @EducationTypeID = obj.EducationTypeID,
                    @CourseCategoryID = obj.CourseCategoryID,
                    @HonoursSubject = obj.HonoursSubject,
                    @SubsidiarySubject = obj.SubsidiarySubject,
                    @IPaddress = IP,
                    @InsertBy = app,
                    @CollegeID = obj.CollegeID,
                    @ID = obj.ID,
                    @sessionid = sessionid

                }, commandType: CommandType.StoredProcedure).FirstOrDefault();

                return Result;
            }
        }

        //-- bharti 01/04/2019
        public SubjectList GetSubjectListing(int pageIndex, int pageSize, string search = "", string searchcode = "", string searchname = "", string practical = "", string compulsory = "")
        {

            SubjectList sub = new SubjectList();
            using (IDbConnection con = new SqlConnection(CommonSetting.constr))
            {
                var result = con.QueryMultiple("USP_StreamMaster", new { @flag = 16, PageIndex = pageIndex, pageSize = pageSize, search = search, searchcode = searchcode, searchname = searchname, practical = practical, compulsory = compulsory }, commandType: CommandType.StoredProcedure);
                sub.qlist = result.Read<BL_StreamMaster>().ToList();
                sub.totalCount = result.Read<String>().FirstOrDefault();
            }
            return sub;
        }
        public BL_StreamMaster getcollegesubjects_seatavailbale(int CollegeID,int subjectid,int sessionin,int coursecategoryid,int AddmissionCategoryid=1)
        {

            BL_StreamMaster sub = new BL_StreamMaster();
            using (IDbConnection con = new SqlConnection(CommonSetting.constr))
            {
                var result = con.Query<BL_StreamMaster>("Get_Seat_available_spot_proc", new {
                    @CollegeID = CollegeID,
                    @subjectid = subjectid,
                    @sessionid = sessionin,
                    @coursecategoryid = coursecategoryid,
                    @AddmissionCategoryid = AddmissionCategoryid
                }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                sub = result;
            }
            return sub;
        }
        public BL_StreamMaster getcollegesubjects_Savefor15minute(string action,int sid,int CasteCategory, int CollegeID, int subjectid, int sessionin, int coursecategoryid, int AddmissionCategoryid = 1)
        {

            BL_StreamMaster sub = new BL_StreamMaster();
            string ip = CommonMethod.GetIPAddress();
            using (IDbConnection con = new SqlConnection(CommonSetting.constr))
            {
                var result = con.Query<BL_StreamMaster>("sp_spot_recruitment", new
                {
                    @action = action,
                    @id =0,
                    @sid = sid,
                    @percenatge =0,
                    @AddmissionCategoryid =1,
                    @coursecategoryid = coursecategoryid,
                    @CasteCategory = CasteCategory,
                    @sessionid = sessionin,
                    @collegeid = CollegeID,
                    @StreamCategoryID = subjectid,
                    @adddate ="",
                    @choicetable_id =0,
                    @manualAdIPAddress= ip

                }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                sub = result;
            }
            return sub;
        }
        public BL_StreamMaster getcollegesubjects_Saveslideup(string action, int sid, int CasteCategory, int CollegeID, int subjectid, int sessionin, int coursecategoryid, int AddmissionCategoryid = 1, decimal percenatge = 0, int choicetable_id = 0, bool ishandicapped=false,int studentyear=0)
        {

            BL_StreamMaster sub = new BL_StreamMaster();
            string ip = CommonMethod.GetIPAddress();
            using (IDbConnection con = new SqlConnection(CommonSetting.constr))
            {
                var result = con.Query<BL_StreamMaster>("sp_slideup_recruitment", new
                {
                    @action = action,
                    @id = 0,
                    @sid = sid,
                    @percenatge = percenatge,
                    @AddmissionCategoryid = 1,
                    @coursecategoryid = coursecategoryid,
                    @CasteCategory = CasteCategory,
                    @sessionid = sessionin,
                    @collegeid = CollegeID,
                    @StreamCategoryID = subjectid,
                    @adddate = "",
                    @choicetable_id = choicetable_id,
                    @manualAdIPAddress = ip,
                    @Subsidiary1  = 0,
                    @Susidiary2  = 0,
                    @Compulsory1  = 0,
                    @Compulsory2  = 0,
                    @ishandicapped  = ishandicapped,
                    @studentyear  = studentyear

                }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                sub = result;
            }
            return sub;
        }
        public BL_StreamMaster getcollegesubjects_seatavailbale_slideup(int CollegeID, int subjectid, int sessionin, int coursecategoryid,int CasteCategory, bool ishandicapped, int AddmissionCategoryid = 1)
        {

            BL_StreamMaster sub = new BL_StreamMaster();
            using (IDbConnection con = new SqlConnection(CommonSetting.constr))
            {
                var result = con.Query<BL_StreamMaster>("sp_slideup_recruitment", new
                {
                    @action= "checkseatavailable",
                    @CollegeID = CollegeID,
                   
                    @sessionid = sessionin,
                    @CasteCategory= CasteCategory,
                    @coursecategoryid = coursecategoryid,
                    @StreamCategoryID = subjectid,
                    @AddmissionCategoryid=1,
                    @ishandicapped= ishandicapped
                }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                sub = result;
            }
            return sub;
        }
        public BL_StreamMaster subsidiarysubjects_Save(string action, int sid, int Subsidiary1 = 0, int Susidiary2 = 0, int Compulsory1 = 0, int Compulsory2 = 0,int sessionin=0)
        {
           
            BL_StreamMaster sub = new BL_StreamMaster();
            string ip = CommonMethod.GetIPAddress();
            using (IDbConnection con = new SqlConnection(CommonSetting.constr))
            {
                var result = con.Query<BL_StreamMaster>("sp_spot_recruitment", new
                {
                    @action = action,
                    @sid = sid,
                    @Subsidiary1= Subsidiary1,
                    @Susidiary2= Susidiary2,
                    @Compulsory1= Compulsory1,
                    @Compulsory2= Compulsory2,
                    @sessionid = sessionin,
                    @manualAdIPAddress = ip

                }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                sub = result;
            }
            return sub;
        }
        public BL_StreamMaster Changesubjects_Choice(string action, int sid, int sessionin = 0)
        {

            BL_StreamMaster sub = new BL_StreamMaster();
            string ip = CommonMethod.GetIPAddress();
            using (IDbConnection con = new SqlConnection(CommonSetting.constr))
            {
                var result = con.Query<BL_StreamMaster>("sp_spot_recruitment", new
                {
                    @action = action,
                    @sid = sid,                  
                    @sessionid = sessionin,
                    @manualAdIPAddress = ip

                }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                sub = result;
            }
            return sub;
        }
        public BL_StreamMaster checkspotadmissionEntry( int sid)
        {

            BL_StreamMaster sub = new BL_StreamMaster();
            string ip = CommonMethod.GetIPAddress();
            using (IDbConnection con = new SqlConnection(CommonSetting.constr))
            {
                var result = con.Query<BL_StreamMaster>("sp_spot_recruitment", new
                {
                    @action = "CheckSpotAdmission",                   
                    @sid = sid,                    

                }, commandTimeout: 500000,commandType: CommandType.StoredProcedure).FirstOrDefault();
                sub = result;
            }
            return sub;
        }
        public BL_StreamMaster checkspotsubsidiarySave(int sid)
        {

            BL_StreamMaster sub = new BL_StreamMaster();
            string ip = CommonMethod.GetIPAddress();
            using (IDbConnection con = new SqlConnection(CommonSetting.constr))
            {
                var result = con.Query<BL_StreamMaster>("sp_spot_recruitment", new
                {
                    @action = "CheckspotsubsidiarySave",
                    @sid = sid,

                }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                sub = result;
            }
            return sub;
        }
        //-- til here 
        public List<BL_StreamMaster> getcollegesubjects(int sid, int varflag, int collegeid, int subjectid = 0, int subsidiary1 = 0, int compulsory1 = 0, int coursecategoryid =0)
        {
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                // With New Rule 
                var sessionid = Convert.ToInt32((ClsLanguage.GetCookies("NBSission")));
                var objdata = conn.Query<BL_StreamMaster>("USP_StreamMaster_choicefill_JC_new",
                    new { @SID = sid, @flag = varflag, @CollegeID = collegeid, @subjectid = subjectid, @subsidiary1 = subsidiary1, @compulsory1 = compulsory1, @sessionid= sessionid }, commandTimeout: 500000, commandType: CommandType.StoredProcedure).ToList();
                //var objdata = conn.Query<BL_StreamMaster>("[USP_StreamMaster]",
                //   new { @SID = sid, @flag = varflag, @CollegeID = collegeid, @subjectid = subjectid, @subsidiary1 = subsidiary1, @compulsory1 = compulsory1 }, commandType: CommandType.StoredProcedure).ToList();
                return objdata;
            }
        }
    }
     
    public class StreamListType
    {
        public List<BL_StreamMaster> honourslist { get; set; }
        public List<BL_StreamMaster> subsidiarylist { get; set; }
    }
    public class SubjectList
    {
        public List<BL_StreamMaster> qlist { get; set; }
        public string totalCount { get; set; }
    }
}
