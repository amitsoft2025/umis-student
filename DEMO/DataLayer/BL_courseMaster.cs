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
    public class BL_courseMaster
    {
        #region Properties
        public int Flag { get; set; }
        [Required(ErrorMessage = "Type is Required")]
        public int CommonId { get; set; }
        public string Title { get; set; }
        [Required(ErrorMessage ="Course Name is Required")]
        public string CourseCategory { get; set; }
        public int CourseCategoryID { get; set; }
       
        #endregion


        public List<SelectListItem> GetEducationType()
        {
            //BL_Expert objExpert = new BL_Expert();
            List<SelectListItem> items = new List<SelectListItem>();
            try
            {
                var Types = GetEduType(1);
                if (Types != null)
                {
                    foreach (var p in Types)
                    {
                        items.Add(new SelectListItem { Value = p.CommonId.ToString(), Text = p.Title.ToString() });
                    }
                }
            }
            catch (Exception ex)
            {
                
            }
            return items;
        }

        public List<EducationType> GetEduType(int varflag)
        {
            List<EducationType> countryList = new List<EducationType>();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                countryList = conn.Query<EducationType>("USP_CourseMaster", new { @flag = varflag }, commandType: CommandType.StoredProcedure).ToList();
               
            }
            return countryList;
        }

        public int SaveEducationtype(BL_courseMaster obj)
        {
            int Result = 0;
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                Result = conn.Execute("USP_CourseMaster", new { @flag = obj.Flag, EducationTypeID=obj.CommonId, CourseCategory=obj.CourseCategory }, commandType: CommandType.StoredProcedure);

            }
            return Result;
        }

        public int updatedata(BL_courseMaster obj)
        {
            int Result = 0;
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                Result = conn.Execute("USP_CourseMaster", new { @flag = obj.Flag, EducationTypeID = obj.CommonId, CourseCategory = obj.CourseCategory, @CourseCategoryID=obj.CourseCategoryID }, commandType: CommandType.StoredProcedure);

            }
            return Result;
        }

        public List<BL_courseMaster> GetCourseCategoryData(int varflag)
        {
            List<BL_courseMaster> objdata = new List<BL_courseMaster>();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                objdata = conn.Query<BL_courseMaster>("USP_CourseMaster", new { @flag = varflag }, commandType: CommandType.StoredProcedure).ToList();

            }
            return objdata;
        }

        public BL_courseMaster GetCourseCategoryDataBYID(int varflag,int CourseID)
        {
            BL_courseMaster objdata = new BL_courseMaster();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                objdata = conn.Query<BL_courseMaster>("USP_CourseMaster", new { @flag = varflag , @CourseCategoryID = CourseID }, commandType: CommandType.StoredProcedure).FirstOrDefault();

            }
            return objdata;
        }
    }

    public class EducationType
    {
        public int CommonId { get; set; }
        public String Title { get; set; }
    }


}
