using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer;
using System.Data;
using System.Data.SqlClient;
using Dapper;

namespace DataLayer
{
    public class Student_Admission_Choicesubject
    {
        public int ID { get; set; }
        public int SID { get; set; }
        public string collegeidlist { get; set; }
        public string hounors_subjectidlist { get; set; }
        public string Subsidiary1_subjectidlist { get; set; }
        public string Subsidiary2_subjectidlist { get; set; }
        public string Compulsory1_subjectidlist { get; set; }
        public string Compulsory2_subjectidlist { get; set; }

        public int collegeid { get; set; }
        public int hounors_subjectid { get; set; }
        public int Subsidiary1_subjectid { get; set; }

        public int Subsidiary2_subjectid { get; set; }
        public int Compulsory1_subjectid { get; set; }
        public int Compulsory2_subjectid { get; set; }

        public string adddate { get; set; }
        public bool Status { get; set; }
        public string Msg { get; set; }
        public int sessionid { get; set; }
        public Student_Admission_Choicesubject savest_choicesubject(Student_Admission_Choicesubject obj1)
        {
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var obj = conn.Query<Student_Admission_Choicesubject>("[sp_Student_choice_subject]", new
                {
                    @Action = "insert",
                    @SID = obj1.SID,
                    @collegeidlist = obj1.collegeidlist,
                    @hounors_subjectidlist = obj1.hounors_subjectidlist,
                    @Subsidiary1_subjectidlist = obj1.Subsidiary1_subjectidlist,
                    @Subsidiary2_subjectidlist = obj1.Subsidiary2_subjectidlist,
                    @Compulsory1_subjectidlist = obj1.Compulsory1_subjectidlist,
                    @Compulsory2_subjectidlist = obj1.Compulsory2_subjectidlist,
                }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return obj;
            }

        }
        public List<Student_Admission_Choicesubject> viewst_choicesubject(int SID)
        {
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var obj = conn.Query<Student_Admission_Choicesubject>("[sp_Student_choice_subject]", new
                {
                    @Action = "viewbysid",
                    @SID = SID,
                },commandTimeout:50000, commandType: CommandType.StoredProcedure).ToList();
                return obj;
            }

        }
        public List<Student_Admission_Choicesubject> viewst_choicesubject_1(int SID)
        {
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var obj = conn.Query<Student_Admission_Choicesubject>("[sp_Student_choice_subject]", new
                {
                    @Action = "viewbysid1",
                    @SID = SID,
                }, commandType: CommandType.StoredProcedure).ToList();
                return obj;
            }

        }
        public List<Student_Admission_Choicesubject> viewst_choicesubject_spot(int SID)
        {
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var obj = conn.Query<Student_Admission_Choicesubject>("[sp_Student_choice_subject]", new
                {
                    @Action = "viewbysidspot",
                    @SID = SID,
                }, commandType: CommandType.StoredProcedure).ToList();
                return obj;
            }

        }
        public Student_Admission_Choicesubject againfillform(int SID, int sessionid)
        {
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var obj = conn.Query<Student_Admission_Choicesubject>("[sp_Student_choice_subject]", new
                {
                    @Action = "againfillform",
                    @SID = SID,
                    @sessionid = sessionid
                }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return obj;
            }

        }
    }
}
