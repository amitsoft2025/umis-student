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
using Website.Areas.StudentBEd.Models;
//using Website.Areas.StudentV.Models;

namespace Website.Areas.StudentBEd.Controllers
{
    [CookiesExpireFilterBEd]
    public class ResultBedController : Controller
    {
        // GET: StudentBEd/ResultBed
        public ActionResult Index()
        {
            int coursecategoryid = Convert.ToInt32(EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENNBCourseCategory")));
            Result model = new Result();
            Result tblResult = new Result();
            model.CollegeList = tblResult.GetCollegeListcoursewise(0, 40);
            model.ExaminationList = examtype();
            model.courseyearlist = BEDYears(coursecategoryid);
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
            model.courseyearlist = BEDYears(coursecategoryid);
            List<Result> studentList = new List<Result>();
            model.StudentListNew = model.Studentportal_GetTabulationRegisterBED(0, "MainExamCustomSearch", 0, 13, Convert.ToInt32(coursecategoryid), collegeid, model.CourseYearID, 0, rollno, "", model.isback, 0);
            obj = obj.GetBEDSubject(model.CourseYearID);

            if (model.StudentListNew.Count > 0)
            {
                if (model.CourseYearID == 28)// part-1
                {
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

                    ViewBag.C6 = obj.C6;
                    ViewBag.C6_code = obj.C6_code;
                    if (model.StudentListNew.FirstOrDefault().ElectiveSubject1_id > 0)
                    {
                        if (model.StudentListNew.FirstOrDefault().ElectiveSubject1_id == 313)//Pedagogy of a School Subject- Part I - Language
                        {
                            ViewBag.C7 = obj.C7_a_1;
                            ViewBag.C7_code = obj.C7_a_1_code;
                        }
                        else if (model.StudentListNew.FirstOrDefault().ElectiveSubject1_id == 316)//Pedagogy of a School Subject- Part I - Science
                        {
                            ViewBag.C7 = obj.C7_a_2;
                            ViewBag.C7_code = obj.C7_a_2_code;
                        }
                        else if (model.StudentListNew.FirstOrDefault().ElectiveSubject1_id == 321)//Pedagogy of a School Subject- Part I - Social Science
                        {
                            ViewBag.C7 = obj.C7_a_3;
                            ViewBag.C7_code = obj.C7_a_3_code;
                        }

                    }
                    else
                    {
                        ViewBag.C7 = "";// obj.C7_a_1;
                        ViewBag.C7_code = "";// obj.C7_a_1_code;

                    }
                    ViewBag.EPC_I = obj.EPC_I;
                    ViewBag.EPC_I_code = obj.EPC_I_code;

                    ViewBag.EPC_2 = obj.EPC_2;
                    ViewBag.EPC_2_code = obj.EPC_2_code;

                    ViewBag.EPC_3 = obj.EPC_3;
                    ViewBag.EPC_3_code = obj.EPC_3_code;
                }
                else // part -2
                {
                    if (model.StudentListNew.FirstOrDefault().ElectiveSubject1_id > 0)
                    {
                        Result obj123 = new Result();
                        obj123 = model.GetBEDSubject_single(Convert.ToInt32(model.CourseYearID), model.StudentListNew.FirstOrDefault().ElectiveSubject1_id);
                        ViewBag.C1 = obj123.C2;
                        ViewBag.C1_code = obj123.C2_code;

                    }
                    if (282 > 0)
                    {
                        Result obj123 = new Result();
                        obj123 = model.GetBEDSubject_single(Convert.ToInt32(model.CourseYearID), 282);
                        ViewBag.C2 = obj123.C2;
                        ViewBag.C2_code = obj123.C2_code;
                    }
                    if (283 > 0)
                    {
                        Result obj123 = new Result();
                        obj123 = model.GetBEDSubject_single(Convert.ToInt32(model.CourseYearID), 283);
                        ViewBag.C3 = obj123.C2;
                        ViewBag.C3_code = obj123.C2_code;
                    }
                    if (284 > 0)
                    {
                        Result obj123 = new Result();
                        obj123 = model.GetBEDSubject_single(Convert.ToInt32(model.CourseYearID), 284);
                        ViewBag.C4 = obj123.C2;
                        ViewBag.C4_code = obj123.C2_code;
                    }


                    if (model.StudentListNew.FirstOrDefault().ElectiveSubject2_id > 0)
                    {
                        Result obj123 = new Result();
                        obj123 = model.GetBEDSubject_single(Convert.ToInt32(model.CourseYearID), model.StudentListNew.FirstOrDefault().ElectiveSubject2_id);
                        ViewBag.C5 = obj123.C2;
                        ViewBag.C5_code = obj123.C2_code;

                    }

                    if (286 > 0)
                    {
                        Result obj123 = new Result();
                        obj123 = model.GetBEDSubject_single(Convert.ToInt32(model.CourseYearID), 286);

                        ViewBag.EPC_I = obj123.C2;
                        ViewBag.EPC_I_code = obj123.C2_code;

                    }
                    if (287 > 0)
                    {
                        Result obj123 = new Result();
                        obj123 = model.GetBEDSubject_single(Convert.ToInt32(model.CourseYearID), 287);
                        ViewBag.EPC_2 = obj123.C2;
                        ViewBag.EPC_2_code = obj123.C2_code;
                    }

                }

                //ViewBag.C7_a_1 = obj.C7_a_1;
                //ViewBag.C7_a_1_code = obj.C7_a_1_code;

                //ViewBag.C7_a_2 = obj.C7_a_2;
                //ViewBag.C7_a_2_code = obj.C7_a_2_code;

                //ViewBag.C7_a_3 = obj.C7_a_2;
                //ViewBag.C7_a_2_code = obj.C7_a_2_code;

            }

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
        public List<courseyear> BEDYears(int coursecategoryid)
        {
            Result com = new Result();
            List<courseyear> ddlExam = new List<courseyear>();
            ddlExam.Add(new courseyear { YearName = "Part - 1", ID = "28", CourseID = 28});
            ddlExam.Add(new courseyear { YearName = "Part - 2", ID = "29", CourseID = 28 });
         
            var obj = ddlExam.Where(x => x.CourseID == coursecategoryid).ToList();//com.CourseYear( coursecategoryid);
            return obj;
        }
    }
}