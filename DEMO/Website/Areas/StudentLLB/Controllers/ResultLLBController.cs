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
using System.Configuration;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web.UI;
using NReco.PdfGenerator;
using com.toml.dp.util;
using Newtonsoft.Json;
using System.Security.Cryptography;
using System.Collections.Specialized;
using Website.Areas.StudentLLB.Models;
//using Website.Areas.StudentV.Models;

namespace Website.Areas.StudentLLB.Controllers
{
    [CookiesExpireFilterLLB]
    public class ResultLLBController : Controller
    {
        // GET: StudentLLB/ResultLLB
        public ActionResult Index()
        {
            int coursecategoryid = Convert.ToInt32(EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENNBCourseCategory")));
            Result model = new Result();
            Result tblResult = new Result();
            model.CollegeList = tblResult.GetCollegeListcoursewise(0, 41);
            model.ExaminationList = examtype();
            model.courseyearlist = LLBSemesters(coursecategoryid);
            List<Result> objList = new List<Result>();
            List<Result> studentList = new List<Result>();

            model.StudentListNew = new List<Result>();

            ViewBag.PaperI = "";
            ViewBag.PaperII = "";
            ViewBag.PaperIII = "";
            ViewBag.PaperIV = "";
            ViewBag.PaperV = "";
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
            if (rollno == null || rollno == "")
            {
                return View(model);
            }
            Result tblResult = new Result();
            Result obj = new Result();
            model.CollegeList = tblResult.GetCollegeListcoursewise(0, 40);
            model.ExaminationList = examtype();
            model.courseyearlist = LLBSemesters(coursecategoryid);
            List<Result> studentList = new List<Result>();
            model.StudentListNew = model.Studentportal_GetTabulationRegisterLLB(collegeid, model.CourseYearID,rollno, model.isback);
            obj = obj.GetLLBSubject(model.CourseYearID);

            ViewBag.C1 = obj.C1;
            ViewBag.C1_code = obj.C1_code;

            ViewBag.C2 = obj.C2;
            ViewBag.C2_code = obj.C2_code;

            ViewBag.C3 = obj.C3;
            ViewBag.C3_code = obj.C3_code;

            ViewBag.C4 = obj.C4;
            ViewBag.C4_code = obj.C4_code;

            ViewBag.C5 = obj.C5;
            ViewBag.C5_code = obj.C5_code;
            
            ViewBag.CourseYearname = "";
            if (model.StudentListNew.Count() > 0)
            {
                ViewBag.Collegename = model.StudentListNew.FirstOrDefault().Collegename;
                ViewBag.Coursename = model.StudentListNew.FirstOrDefault().CourseCategoryName;
                ViewBag.CourseYearname = model.StudentListNew.FirstOrDefault().YearName;
                ViewBag.sessionname = model.StudentListNew.FirstOrDefault().SessionName;
            }

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
            ddlExam.Add(new SelectListItem { Text = "--Select Type--", Value = "" });
            ddlExam.Add(new SelectListItem { Text = "Main Exam", Value = "0" });
            ddlExam.Add(new SelectListItem { Text = "Back Exam", Value = "1" });
            return ddlExam;
        }
        public List<courseyear> LLBSemesters(int coursecategoryid)
        {
            Result com = new Result();
            List<courseyear> ddlExam = new List<courseyear>();
            ddlExam.Add(new courseyear { YearName = "Semester - 1", ID = "30", CourseID = 29 });
            ddlExam.Add(new courseyear { YearName = "Semester - 2", ID = "31", CourseID = 29 });
            ddlExam.Add(new courseyear { YearName = "Semester - 3", ID = "32", CourseID = 29 });
            ddlExam.Add(new courseyear { YearName = "Semester - 4", ID = "33", CourseID = 29 });
            ddlExam.Add(new courseyear { YearName = "Semester - 5", ID = "34", CourseID = 29 });
            ddlExam.Add(new courseyear { YearName = "Semester - 6", ID = "35", CourseID = 29 });


            var obj = ddlExam.Where(x => x.CourseID == coursecategoryid).ToList();//com.CourseYear( coursecategoryid);
            return obj;
        }
    }
}