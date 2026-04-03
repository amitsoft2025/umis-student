using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Website.Models;
using DataLayer;

namespace Website.Controllers
{
    public class ResultController : Controller
    {
        public ActionResult UG()
        {
            CollegeExamCenter model = new CollegeExamCenter();
            CollegeExamCenter tblResult = new CollegeExamCenter();
            model.CollegeList = tblResult.GetCollegeListcoursewise(0,11);
            model.ExaminationList = PopulateExamination();
            model.ProgrammeList = UGScheme();
            List<CollegeExamCenter> objList = new List<CollegeExamCenter>();
            List<CollegeExamCenter> studentList = new List<CollegeExamCenter>();
            //model.StudentListNew = model.GetTabulationRegisterUG(1,"", 0, 11, Convert.ToInt32(0), "", 0);
            ViewBag.sessionname = "";
            ViewBag.CourseYearname = "";
            
            return View(model);
        }
        [HttpPost]
        public ActionResult UG(CollegeExamCenter model)
        {
            CollegeExamCenter tblResult = new CollegeExamCenter();
           
            CollegeExamCenter obj = new CollegeExamCenter();
            model.CollegeList = tblResult.GetCollegeListcoursewise(model.CourseCategoryID,11);
            model.ExaminationList = PopulateExamination();
            model.ProgrammeList = UGScheme();
            List<CollegeExamCenter> studentList = new List<CollegeExamCenter>();
            int courseyearid = 0;
            int coursecategoryid = 0;
            // for ba
            if (model.EducationType == 1)
            {
                courseyearid = 1;
                model.isback = 0;
                coursecategoryid = 1;
            }

            if (model.EducationType == 2)
            {
                courseyearid = 2;
                model.isback = 0;
                coursecategoryid = 1;
            }
            if (model.EducationType == 3)
            {
                courseyearid = 3;
                model.isback = 0;
                coursecategoryid = 1;
            }
            //for bsc
            if (model.EducationType == 4)
            {
                courseyearid = 4;
                model.isback = 0;
                coursecategoryid = 2;
            }
            if (model.EducationType == 5)
            {
                courseyearid = 5;
                model.isback = 0;
                coursecategoryid = 2;
            }
            if (model.EducationType == 6)
            {
                courseyearid = 6;
                model.isback = 0;
                coursecategoryid = 2;
            }
            // for bcom
            if (model.EducationType == 7)
            {
                courseyearid = 7;
                model.isback = 0;
                coursecategoryid = 3;
            }
            if (model.EducationType == 8)
            {
                courseyearid = 8;
                model.isback = 0;
                coursecategoryid = 3;
            }
            if (model.EducationType == 9)
            {
                courseyearid = 9;
                model.isback = 0;
                coursecategoryid = 3;
            }
           // model.StudentListNew = model.GetTabulationRegisterUG(0,"MainExamCustomSearch", 0, 11, Convert.ToInt32(coursecategoryid), model.CollegeID, courseyearid, 0, model.RollNo, "",0, model.ExaminationID);
            return View(model);
        }

       

        public ActionResult Index()
        {
           
            return View();
        }
        
        public ActionResult BCA()
        {
            CollegeExamCenter model = new CollegeExamCenter();
            CollegeExamCenter tblResult = new CollegeExamCenter();
            model.CollegeList = tblResult.GetCollegeListcoursewise(0, 13);
            model.ExaminationList = PopulateExamination();
            model.ProgrammeList = BCASemester();
            List<CollegeExamCenter> objList = new List<CollegeExamCenter>();
            List<CollegeExamCenter> studentList = new List<CollegeExamCenter>();
            model.StudentListNew = new List<CollegeExamCenter>();
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
        public ActionResult BCA(CollegeExamCenter model)
        {
            CollegeExamCenter tblResult = new CollegeExamCenter();
            CollegeExamCenter obj = new CollegeExamCenter();
            model.CollegeList = tblResult.GetCollegeListcoursewise(0, 13);
            model.ExaminationList = PopulateExamination();
            model.ProgrammeList = BCASemester();
            List<CollegeExamCenter> studentList = new List<CollegeExamCenter>();
            int courseyearid = 0;
            model.isback = 0;
            if (model.EducationType == 1)
            {
                courseyearid = 10;
                model.isback = 0;
            }

            if (model.EducationType == 2)
            {
                courseyearid = 10;
                model.isback = 1;
            }
            if (model.EducationType == 3)
            {
                courseyearid = 12;
                model.isback = 0;
            }
            if (model.EducationType == 4)
            {

            }
            model.StudentListNew = model.GetTabulationRegisterBCA(0,"MainExamCustomSearch", 0, 13, Convert.ToInt32(CommonSetting.coursecategory.bca), model.CollegeID, courseyearid, 0, model.RollNo, "", model.isback, model.ExaminationID);
            obj = obj.GetBCASubject(courseyearid);
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

        #region Populate Dropdown                
        public List<SelectListItem> PopulateExamination()
        {
            List<SelectListItem> ddlExam = new List<SelectListItem>();
            ddlExam.Add(new SelectListItem { Text = "--Select Examination--", Value = "" });
            ddlExam.Add(new SelectListItem { Text = "Examination 2019", Value = "2019" });
         //   ddlExam.Add(new SelectListItem { Text = "Examination 2020", Value = "2020" });
            return ddlExam;
        }
        public List<SelectListItem> BCASemester()
        {
            List<SelectListItem> ddlExam = new List<SelectListItem>();
            ddlExam.Add(new SelectListItem { Text = "--Select Course--", Value = "" });
            ddlExam.Add(new SelectListItem { Text = "BCA Semster 1 (Main Exam)", Value = "1" });
            ddlExam.Add(new SelectListItem { Text = "BCA Semster 1 (Back Exam)", Value = "2" });
            ddlExam.Add(new SelectListItem { Text = "BCA Semster 3 (Main Exam)", Value = "3" });

            return ddlExam;
        }
        public List<SelectListItem> UGScheme()
        {
            List<SelectListItem> ddlExam = new List<SelectListItem>();
            ddlExam.Add(new SelectListItem { Text = "--Select Course--", Value = "" });
            ddlExam.Add(new SelectListItem { Text = "B.A. Part 1 (Main Exam)", Value = "1" });
            ddlExam.Add(new SelectListItem { Text = "B.A. Part 2 (Main Exam)", Value = "2" });
            ddlExam.Add(new SelectListItem { Text = "B.A. Part 3 (Main Exam)", Value = "3" });

            ddlExam.Add(new SelectListItem { Text = "B.Sc. Part 1 (Main Exam)", Value = "4" });
            ddlExam.Add(new SelectListItem { Text = "B.Sc. Part 2 (Main Exam)", Value = "5" });
            ddlExam.Add(new SelectListItem { Text = "B.Sc. Part 3 (Main Exam)", Value = "6" });

            ddlExam.Add(new SelectListItem { Text = "B.Com. Part 1 (Main Exam)", Value = "7" });
            ddlExam.Add(new SelectListItem { Text = "B.Com. Part 2 (Main Exam)", Value = "8" });
            ddlExam.Add(new SelectListItem { Text = "B.Com. Part 3 (Main Exam)", Value = "9" });
            return ddlExam;
        }
        #endregion
    }
}