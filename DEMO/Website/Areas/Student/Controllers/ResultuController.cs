using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataLayer;
using System.Data;
using System.IO;
using System.Drawing;
using Website.Areas.Student.Models;
using Website.Models;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.IO;
using System.Web.UI;
using System.Text;
using NReco.PdfGenerator;
using com.toml.dp.util;
using Website.Areas.Student.Models;
using Newtonsoft.Json;
using System.Security.Cryptography;
using System.Collections.Specialized;
using Website.Areas.Student.Models;
namespace Website.Areas.Student.Controllers
{
    [CookiesExpireFilterAdmin]
    public class ResultuController : Controller
    {
        // GET: Student/Resultu
        public ActionResult Index()
        {
            int collegeid = (Convert.ToInt32(EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENNBcollegeid"))));
            int sid = (Convert.ToInt32(EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENNBStID"))));
            string rollno = ((EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENNBrollno"))));
            int coursecategoryid = Convert.ToInt32(EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENNBCourseCategory")));
            Result model = new Result();
            Result tblResult = new Result();
            model.CollegeList = tblResult.GetCollegeListcoursewise(0, 11);
            model.ExaminationList = examtype();
            model.courseyearlist = UGScheme(coursecategoryid);
            List<Result> objList = new List<Result>();
            List<Result> studentList = new List<Result>();
            model.StudentListNew = new List<Result>();
            ViewBag.sessionname = "";
            ViewBag.CourseYearname = "";
            return View(model);
        }
        [HttpPost]
        public ActionResult Index(Result model)
        {
            int collegeid = (Convert.ToInt32(EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENNBcollegeid"))));
            int sid = (Convert.ToInt32(EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENNBStID"))));
            string rollno = ((EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENNBrollno"))));
            int coursecategoryid = Convert.ToInt32(EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENNBCourseCategory")));
            int streamcategoryid = Convert.ToInt32(EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENNBstreamcategoryid")));
            Result tblResult = new Result();
            Result obj = new Result();
            model.CollegeList = tblResult.GetCollegeListcoursewise(model.CourseCategoryID, 11);
            model.ExaminationList = examtype();
            model.courseyearlist = UGScheme(coursecategoryid);
            List<Result> studentList = new List<Result>();
            int courseyearid = 0;
            model.StudentListNew = model.GetTabulationRegisterUG_studentportal(0, Convert.ToInt32(coursecategoryid), collegeid, model.CourseYearID, streamcategoryid, rollno, model.isback);
            return View(model);

        }
        public List<SelectListItem> PopulateExamination()
        {
            List<SelectListItem> ddlExam = new List<SelectListItem>();
            ddlExam.Add(new SelectListItem { Text = "--Select Examination--", Value = "" });
            ddlExam.Add(new SelectListItem { Text = "Examination 2019", Value = "2019" });
            //ddlExam.Add(new SelectListItem { Text = "Examination 2020", Value = "2020" });
            return ddlExam;
        }
        public List<SelectListItem> examtype()
        {
            List<SelectListItem> ddlExam = new List<SelectListItem>();
            // ddlExam.Add(new SelectListItem { Text = "--Select Type--", Value = "" });
            ddlExam.Add(new SelectListItem { Text = "Main Exam", Value = "0" });
            ddlExam.Add(new SelectListItem { Text = "Back Exam", Value = "1" });
            return ddlExam;
        }
        public List<courseyear> UGScheme(int coursecategoryid)
        {
            Result com = new Result();
            List<courseyear> ddlExam = new List<courseyear>();
            ddlExam.Add(new courseyear { YearName = "Part - 1", ID = "1", CourseID = 1 });
            ddlExam.Add(new courseyear { YearName = "Part - 2", ID = "2", CourseID = 1 });
            ddlExam.Add(new courseyear { YearName = "Part - 3", ID = "3", CourseID = 1 });

            ddlExam.Add(new courseyear { YearName = "Part - 1", ID = "4", CourseID = 2 });
            ddlExam.Add(new courseyear { YearName = "Part - 2", ID = "5", CourseID = 2 });
            ddlExam.Add(new courseyear { YearName = "Part - 3", ID = "6", CourseID = 2 });

            ddlExam.Add(new courseyear { YearName = "Part - 1", ID = "7", CourseID = 3 });
            ddlExam.Add(new courseyear { YearName = "Part - 2", ID = "8", CourseID = 3 });
            ddlExam.Add(new courseyear { YearName = "Part - 3", ID = "9", CourseID = 3 });

            var obj = ddlExam.Where(x => x.CourseID == coursecategoryid).ToList();//com.CourseYear( coursecategoryid);
            return obj;
        }

    }

}