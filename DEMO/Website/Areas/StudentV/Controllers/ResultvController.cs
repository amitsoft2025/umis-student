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
using Website.Areas.StudentV.Models;
namespace Website.Areas.StudentV.Controllers
{
    [CookiesExpireFilterVoc]
    public class ResultvController : Controller
    {
        // GET: StudentV/Resultv
        public ActionResult Index()
        {
            int coursecategoryid =Convert.ToInt32( EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENNBCourseCategory")));
            Result model = new Result();
            Result tblResult = new Result();
            model.CollegeList = tblResult.GetCollegeListcoursewise(0, 13);
            model.ExaminationList = examtype();
            model.courseyearlist = BCASemester(coursecategoryid);
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
            model.CollegeList = tblResult.GetCollegeListcoursewise(0, 13);
            model.ExaminationList = examtype();
            model.courseyearlist = BCASemester(coursecategoryid);
            List<Result> studentList = new List<Result>();
            model.StudentListNew = model.Studentportal_GetTabulationRegisterBCA(0, "MainExamCustomSearch", 0, 13, Convert.ToInt32(coursecategoryid), collegeid, model.CourseYearID, 0, rollno, "", model.isback, 0);
            obj = obj.GetBCASubject(model.CourseYearID);
            ViewBag.PaperI = obj.PaperI;
            ViewBag.PaperII = obj.PaperII;
            ViewBag.PaperIII = obj.PaperIII;
            ViewBag.PaperIV = obj.PaperIV;
            ViewBag.PaperV = obj.PaperV;
            ViewBag.PaperVI = obj.PaperVI;
            ViewBag.PaperIc = obj.PaperIc;
            ViewBag.PaperIIc = obj.PaperIIc;
            ViewBag.PaperIIIc = obj.PaperIIIc;
            ViewBag.PaperIVc = obj.PaperIVc;
            ViewBag.PaperVc = obj.PaperVc;
            ViewBag.PaperVIc = obj.PaperVIc;
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
        public List<courseyear> BCASemester(int coursecategoryid)
        {
            Result com = new Result();
            List<courseyear> ddlExam = new List<courseyear>();
            ddlExam.Add(new courseyear { YearName = "Semester - 1", ID = "10", CourseID = 26  });
            ddlExam.Add(new courseyear { YearName = "Semester - 2", ID = "11", CourseID = 26 });
            ddlExam.Add(new courseyear { YearName = "Semester - 3", ID = "12", CourseID = 26 });
            ddlExam.Add(new courseyear { YearName = "Semester - 4", ID = "13", CourseID = 26 });
            ddlExam.Add(new courseyear { YearName = "Semester - 5", ID = "14", CourseID = 26 });
            ddlExam.Add(new courseyear { YearName = "Semester - 6", ID = "15", CourseID = 26 });

            ddlExam.Add(new courseyear { YearName = "Semester - 1", ID = "18", CourseID = 27 });
            ddlExam.Add(new courseyear { YearName = "Semester - 2", ID = "19", CourseID = 27 });
            ddlExam.Add(new courseyear { YearName = "Semester - 3", ID = "20", CourseID = 27 });
            ddlExam.Add(new courseyear { YearName = "Semester - 4", ID = "21", CourseID = 27 });
            ddlExam.Add(new courseyear { YearName = "Semester - 5", ID = "22", CourseID = 27 });
            ddlExam.Add(new courseyear { YearName = "Semester - 6", ID = "23", CourseID = 27 });

            ddlExam.Add(new courseyear { YearName = "Semester - 1", ID = "42", CourseID = 30 });
            ddlExam.Add(new courseyear { YearName = "Semester - 2", ID = "43", CourseID = 30 });
            ddlExam.Add(new courseyear { YearName = "Semester - 3", ID = "44", CourseID = 30 });
            ddlExam.Add(new courseyear { YearName = "Semester - 4", ID = "45", CourseID = 30 });
            ddlExam.Add(new courseyear { YearName = "Semester - 5", ID = "46", CourseID = 30 });
            ddlExam.Add(new courseyear { YearName = "Semester - 6", ID = "47", CourseID = 30 });
            var obj = ddlExam.Where(x => x.CourseID == coursecategoryid).ToList();//com.CourseYear( coursecategoryid);
            return obj;
        }
       
    }
   
}