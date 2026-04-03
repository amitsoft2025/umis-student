using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataLayer;
using System.Web.Security;
using Website.Models;
using Newtonsoft.Json;
using System.IO;
using System.Configuration;
using System.Net;
using System.Configuration;
using System.Security.Cryptography;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Website.Areas.Student.Controllers
{
    /// <summary>LoginInvalid ApplicationID and password
    /// 
    /// </summary>
    /// 
    [RouteArea("MUStudentPortal")]
    [RoutePrefix("")]
    public class LoginController : Controller
    {
        public static string ReCaptcha_Key = "";
        public static string ReCaptcha_Secret = "";

        [Route("custom-login")]
        public ActionResult Index()
        {
            if (Request.Url.AbsolutePath.ToLower().Contains("/student/login"))
            {
                return HttpNotFound(); // ❌ पूरी तरह block
            }

            if (Session["User"] != null)
            {
                return RedirectToAction("Index", "Home");
            }

            return View("~/Areas/Student/Views/Login/Index.cshtml");
        }

        [Route("Login")]
        public ActionResult OldLogin()
        {
            return Redirect("/custom-login"); // custom-login pe bhej dega
        }

        //public ActionResult Index()

        //{
        //   // UserLogin.ExpireAllCookies();
        //    //https://portal.DemoUniversity.com
        //    ReCaptcha_Key = "6Lf49wAVAAAAAKX37Xugd-CbsxJpTYq9SrDPoWCG";
        //    ReCaptcha_Secret = "6Lf49wAVAAAAAIMXEFeEgAs7DT5pPC9jMs0UXqdY";
        //    //https://admission.DemoUniversity.com/
        //    //ReCaptcha_Key = "6LfvaaEUAAAAAC8cH4bXqPNzwVUcpbGa_kwMHqn_";
        //    //ReCaptcha_Secret = "6LfvaaEUAAAAADzynFTZyLNS_pd92yo_cAWI5fTW";

        //    //local
        //    //ReCaptcha_Key = "6LdUbKEUAAAAADaK0GwZsRaCMV7ipucy4GaHtlAD";
        //    //ReCaptcha_Secret = "6LdUbKEUAAAAAJ0L_0VWfP75AuHS6rqyJ9X3xKEu";
        //    // UserLogin.ExpireAllCookies();


        //    if (HttpContext.Request.Cookies["ENBUGApplicationNo"] != null )
        //    {
        //        return RedirectToAction("Index", "Home");
        //    }
        //    if (HttpContext.Request.Cookies["ENBPGApplicationNo"] != null)
        //    {

        //        return RedirectToAction("Index", "HomePG", new { area = "StudentPG" });
        //    }
        //    if (HttpContext.Request.Cookies["ENBVCApplicationNo"] != null)
        //    {

        //        return RedirectToAction("Index", "HomeV", new { area = "StudentV" });
        //    }
        //    if (HttpContext.Request.Cookies["ENBBEdApplicationNo"] != null)
        //    {

        //        return RedirectToAction("Index", "HomeB", new { area = "StudentBEd" });
        //    }
        //    if (HttpContext.Request.Cookies["ENBLLBApplicationNo"] != null)
        //    {

        //        return RedirectToAction("Index", "HomeL", new { area = "StudentLLB" });
        //    }
        //    if (HttpContext.Request.Cookies["ENBLLBApplicationNo"] != null)
        //    {

        //        return RedirectToAction("Index", "HomeBPharma", new { area = "StudentBPharma" });
        //    }

        //    if (HttpContext.Request.Cookies["ENBUGCBCSApplicationNo"] != null)
        //    {

        //        return RedirectToAction("Index", "HomeUGCBCS", new { area = "UGCBCS" });
        //    }

        //    return View();
        //}
        public ActionResult Devlogin()
        {
            return View();
        }
        //public JsonResult ResetPass
        public string VerifyCaptcha(string response)
        {
            string url = "https://www.google.com/recaptcha/api/siteverify?secret=" + ReCaptcha_Secret + "&response=" + response;
            return (new WebClient()).DownloadString(url);
        }
        [HttpPost]
        [Route("do-login")]
        public ActionResult Login(Login objlogin)
        {
            DateTime baseDate = new DateTime();
            baseDate = System.DateTime.Now;

            CommonMethod.WritetoNotepadtest("Page login block Start", "start time " + System.DateTime.Now.ToString("yyyy.MM.dd HH:mm:ss:ffff"));
          
            StudentLogin objStudent = new StudentLogin();
            string jsonstring = JsonConvert.SerializeObject(objlogin);
            try
            {
              
                MD5CryptoServiceProvider md5Hasher = new MD5CryptoServiceProvider();
                Byte[] hashedBytes;
                UTF8Encoding encoder = new UTF8Encoding();
                hashedBytes = md5Hasher.ComputeHash(encoder.GetBytes(objlogin.Password));
                objlogin.hashedBytes = hashedBytes;
                var obj = objStudent.Login(objlogin);
                if (obj.status)
                {
                    UserLogin.ExpireAllCookies();
                    int loginRedriect = 1;
                    if (obj.Message!=null)
                    {
                        TempData["errorMsg"] = obj.Message;
                        return View(obj);
                    }
                    TempData["StudentName"] = obj.FirstName +" "+ obj.LastName ;
                    DateTime expiryDate = DateTime.Now.AddHours(1);
                    expiryDate = DateTime.Now.AddHours(1);


                    FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(2, obj.ApplicationNo.ToString(), DateTime.Now, expiryDate, true, obj.session.ToString() + "$#$" + obj.ApplicationNo + "$#$"  + obj.FatherName + "$#$" + obj.LastName + "$#$" + obj.Id + "$#$" + obj.FirstName + "$#$" + obj.stphoto + "$#$" + obj.CourseCategory + "$#$" + obj.CourseType+ "$#$" + obj.CollegeID+ "$#$" + (obj.rollno == null ? "" : obj.rollno));
                    string encryptedTicket = FormsAuthentication.Encrypt(ticket);
                    //create a new authentication cookie - and set its expiration date
                    HttpCookie authenticationCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);

                    //--- new code
                    string[] userdata = ticket.UserData.Split(new[] { "$#$" }, StringSplitOptions.None);
                    ClsLanguage.SetCookies(userdata[1], "NBApplicationNo");
                    ClsLanguage.SetCookies(userdata[2]+""+ userdata[3], "NBName");
                    ClsLanguage.SetCookies(obj.FirstName + " " + (obj.MiddleName == null ? "" : obj.MiddleName) + " " + (obj.LastName == null ? "" : obj.LastName), "NBUserName");
                    ClsLanguage.SetCookies(userdata[0] , "NBSission");
                    ClsLanguage.SetCookies(userdata[6] , "NBphoto");
                    ClsLanguage.SetCookies(userdata[4], "NBStID");
                    ClsLanguage.SetCookies(EncriptDecript.Encrypt(userdata[4]), "ENNBStID");
                    ClsLanguage.SetCookies(EncriptDecript.Encrypt(userdata[1]), "ENBApplicationNo");
                    ClsLanguage.SetCookies(EncriptDecript.Encrypt(obj.Password), "ENBPassword");
                    ClsLanguage.SetCookies((obj.islogout.ToString()), "islogout");
                    ClsLanguage.SetCookies(EncriptDecript.Encrypt(userdata[0]), "ENBSission");
                    ClsLanguage.SetCookies(EncriptDecript.Encrypt(userdata[7]), "ENNBCourseCategory");
                    ClsLanguage.SetCookies(EncriptDecript.Encrypt(userdata[8]), "ENNBstreamCategory");
                    ClsLanguage.SetCookies(EncriptDecript.Encrypt(userdata[9]), "ENNBcollegeid");
                    ClsLanguage.SetCookies(EncriptDecript.Encrypt(userdata[10]), "ENNBrollno");
                    ClsLanguage.SetCookies(obj.courseyear, "courseyear");
                    string branchname = obj.FirstName + "" + obj.LastName;
                    if (obj.EducationType == Convert.ToInt32(CommonSetting.Commonid.Educationtype))
                    {
                        ClsLanguage.SetCookies(EncriptDecript.Encrypt(userdata[1]), "ENBUGApplicationNo");
                        loginRedriect = 1;
                    }
                    else if (obj.EducationType == Convert.ToInt32(CommonSetting.Commonid.EducationtypePG))
                    {
                        ClsLanguage.SetCookies(EncriptDecript.Encrypt(userdata[1]), "ENBPGApplicationNo");
                        loginRedriect = 3;
                    }
                    else if (obj.EducationType == Convert.ToInt32(CommonSetting.Commonid.EducationtypeBEd))
                    {
                        ClsLanguage.SetCookies(EncriptDecript.Encrypt(userdata[1]), "ENBBEdApplicationNo");
                        loginRedriect = 4;
                    }
                    else if (obj.EducationType == Convert.ToInt32(CommonSetting.Commonid.EducationtypeLLB))
                    {
                        ClsLanguage.SetCookies(EncriptDecript.Encrypt(userdata[1]), "ENBLLBApplicationNo");
                        loginRedriect = 5;
                    }
                    else if (obj.EducationType == Convert.ToInt32(CommonSetting.Commonid.EducationtypeVoc))
                    {
                        if (obj.CourseCategory == Convert.ToInt32(CommonSetting.coursecategory.bca) || obj.CourseCategory == Convert.ToInt32(CommonSetting.coursecategory.bba) || obj.CourseCategory == Convert.ToInt32(CommonSetting.coursecategory.BioTech))
                        {
                            ClsLanguage.SetCookies(EncriptDecript.Encrypt(userdata[1]), "ENBVCApplicationNo");
                            loginRedriect = 2;
                        }
                    }

                    else if (obj.EducationType == Convert.ToInt32(CommonSetting.Commonid.EducationtypeBPharma))
                    {
                        ClsLanguage.SetCookies(EncriptDecript.Encrypt(userdata[1]), "ENBBPharmaApplicationNo");
                        loginRedriect = 6;
                    }

                    else if (obj.EducationType == Convert.ToInt32(CommonSetting.Commonid.EducationtypeUGCBCS))
                    {
                        ClsLanguage.SetCookies(EncriptDecript.Encrypt(userdata[1]), "ENBUGCBCSApplicationNo");
                        loginRedriect = 7;
                    }

                    //--
                    if (obj.rememberMe)
                    {
                        HttpCookie remCookie = new HttpCookie("rcpc");
                        remCookie.Expires = DateTime.Now.AddHours(1);
                        Response.Cookies.Add(remCookie);
                        Response.Cookies["rcpc"]["li"] = EncriptDecript.Encrypt(obj.ApplicationNo);
                        Response.Cookies["rcpc"]["p"] = EncriptDecript.Encrypt(obj.Password);                      
                        Response.Cookies["rcpc"]["r"] = obj.rememberMe.ToString();
                        authenticationCookie.Expires = ticket.Expiration;
                    }
                    else
                    {
                        authenticationCookie.Expires = DateTime.Now.AddHours(1);
                        if (Request.Cookies["rcpc"] != null)
                        {
                            Response.Cookies["rcpc"].Expires = DateTime.Now.AddDays(-101);
                        }
                    }
                    Response.Cookies.Add(authenticationCookie);
                    //Student/Login/
                    Login st112 = new Login();
                    st112.status = obj.status;
                    st112.Message = obj.Message; 
                    st112.loginRedriect = loginRedriect;
                    TimeSpan diff = DateTime.Now - baseDate;
                    CommonMethod.WritetoNotepadtest("Page login block Start", "end time " + System.DateTime.Now.ToString("yyyy.MM.dd HH:mm:ss:ffff"));

                    CommonMethod.WritetoNotepadtest("Page load end login  ", "end time final Millisecond:--------" + diff.TotalMilliseconds.ToString());

                    return Json(st112, JsonRequestBehavior.AllowGet);
                }
                else
                {
                   
                    
                    return Json(obj, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "Student Login page : ", "Registration" + jsonstring);

            }
            return View();
        }

        [Route("custom-Logout")]
        public ActionResult Logout()
        {
            Session.Clear();
            Session.Abandon();
            return Redirect("/custom-login");
        }


        //[HttpPost]
        //public ActionResult DevLogin(Login objlogin)
        //{
        //    StudentLogin objStudent = new StudentLogin();
        //    UserLogin.ExpireAllCookies();
        //    string jsonstring = JsonConvert.SerializeObject(objlogin);
        //    try
        //    {
             
               
        //        var obj = objStudent.DevLogin(objlogin);
        //        if (obj.status)
        //        {
        //            if (obj.Message != null)
        //            {
        //                TempData["errorMsg"] = obj.Message;
        //                return View(obj);
        //            }
        //            int loginRedriect = 1;
        //            TempData["StudentName"] = obj.FirstName + "" + obj.LastName;
        //            DateTime expiryDate = DateTime.Now.AddHours(1);
        //            expiryDate = DateTime.Now.AddHours(1);
        //            FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(2, obj.ApplicationNo.ToString(), DateTime.Now, expiryDate, true, obj.session.ToString() + "$#$" + obj.ApplicationNo + "$#$" + obj.FatherName + "$#$" + obj.LastName + "$#$" + obj.Id + "$#$" + obj.FirstName + "$#$" + obj.stphoto + "$#$" + obj.CourseCategory + "$#$" + obj.CourseType);
        //            string encryptedTicket = FormsAuthentication.Encrypt(ticket);
                    
        //            HttpCookie authenticationCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
        //            //--- new code
        //            string[] userdata = ticket.UserData.Split(new[] { "$#$" }, StringSplitOptions.None);
        //            ClsLanguage.SetCookies(userdata[1], "NBApplicationNo");
        //            ClsLanguage.SetCookies(userdata[2] + "" + userdata[3], "NBName");
                  
        //            ClsLanguage.SetCookies(obj.FirstName + " " + (obj.MiddleName == null ? "" : obj.MiddleName) + " " + (obj.LastName == null ? "" : obj.LastName), "NBUserName");
        //            ClsLanguage.SetCookies(userdata[0], "NBSission");
        //            ClsLanguage.SetCookies(userdata[6], "NBphoto");
        //            ClsLanguage.SetCookies(userdata[4], "NBStID");
        //            ClsLanguage.SetCookies(EncriptDecript.Encrypt(userdata[4]), "ENNBStID");
        //            ClsLanguage.SetCookies(EncriptDecript.Encrypt(userdata[1]), "ENBApplicationNo");
        //            ClsLanguage.SetCookies(EncriptDecript.Encrypt(userdata[1]), "ENB123");
        //            ClsLanguage.SetCookies(obj.courseyear, "courseyear");
        //            ClsLanguage.SetCookies((obj.islogout.ToString()), "islogout");
        //            ClsLanguage.SetCookies(EncriptDecript.Encrypt(userdata[0]), "ENBSission");
        //            string branchname = obj.FirstName + "" + obj.LastName;
        //            ClsLanguage.SetCookies(EncriptDecript.Encrypt(obj.Password), "ENBPassword");
        //            ClsLanguage.SetCookies(EncriptDecript.Encrypt(userdata[7]), "ENNBCourseCategory");
        //            ClsLanguage.SetCookies(EncriptDecript.Encrypt(userdata[8]), "ENNBstreamCategory");
        //            if (obj.EducationType == Convert.ToInt32(CommonSetting.Commonid.Educationtype))
        //            {
        //                ClsLanguage.SetCookies(EncriptDecript.Encrypt(userdata[1]), "ENBUGApplicationNo");
        //                loginRedriect = 1;
        //            }
        //            else if (obj.EducationType == Convert.ToInt32(CommonSetting.Commonid.EducationtypePG))
        //            {
        //                ClsLanguage.SetCookies(EncriptDecript.Encrypt(userdata[1]), "ENBPGApplicationNo");
        //                loginRedriect = 3;
        //            }
        //            else if (obj.EducationType == Convert.ToInt32(CommonSetting.Commonid.EducationtypeBEd))
        //            {
        //                ClsLanguage.SetCookies(EncriptDecript.Encrypt(userdata[1]), "ENBBEdApplicationNo");
        //                loginRedriect = 4;
        //            }
        //            else if (obj.EducationType == Convert.ToInt32(CommonSetting.Commonid.EducationtypeLLB))
        //            {
        //                ClsLanguage.SetCookies(EncriptDecript.Encrypt(userdata[1]), "ENBLLBApplicationNo");
        //                loginRedriect = 5;
        //            }
        //            else if (obj.EducationType == Convert.ToInt32(CommonSetting.Commonid.EducationtypeVoc))
        //            {
        //                if (obj.CourseCategory == Convert.ToInt32(CommonSetting.coursecategory.bca) || obj.CourseCategory == Convert.ToInt32(CommonSetting.coursecategory.bba) || obj.CourseCategory == Convert.ToInt32(CommonSetting.coursecategory.BioTech))
        //                {
        //                    ClsLanguage.SetCookies(EncriptDecript.Encrypt(userdata[1]), "ENBVCApplicationNo");
        //                    loginRedriect = 2;
        //                }
        //            }

        //            else if (obj.EducationType == Convert.ToInt32(CommonSetting.Commonid.EducationtypeBPharma))
        //            {
        //                ClsLanguage.SetCookies(EncriptDecript.Encrypt(userdata[1]), "ENBBPharmaApplicationNo");
        //                loginRedriect = 6;
        //            }

        //            else if (obj.EducationType == Convert.ToInt32(CommonSetting.Commonid.EducationtypeUGCBCS))
        //            {
        //                ClsLanguage.SetCookies(EncriptDecript.Encrypt(userdata[1]), "ENBUGCBCSApplicationNo");
        //                loginRedriect = 7;
        //            }
        //            //--
        //            if (obj.rememberMe)
        //            {
        //                HttpCookie remCookie = new HttpCookie("rcpc");
        //                remCookie.Expires = DateTime.Now.AddHours(1);
        //                Response.Cookies.Add(remCookie);
        //                Response.Cookies["rcpc"]["li"] = EncriptDecript.Encrypt(obj.ApplicationNo);
        //                Response.Cookies["rcpc"]["p"] = EncriptDecript.Encrypt(obj.Password);
        //                Response.Cookies["rcpcs"]["r"] = obj.rememberMe.ToString();
        //                authenticationCookie.Expires = ticket.Expiration;
        //            }
        //            else
        //            {
        //                authenticationCookie.Expires = DateTime.Now.AddHours(1);
        //                if (Request.Cookies["devrcpc"] != null)
        //                {
        //                    Response.Cookies["devrcpc"].Expires = DateTime.Now.AddDays(-101);
        //                }
        //            }
        //            Response.Cookies.Add(authenticationCookie);
        //            Login st112 = new Login();
        //            st112.status = obj.status;
        //            st112.Message = obj.Message;
        //            st112.loginRedriect = loginRedriect;
        //            return Json(st112, JsonRequestBehavior.AllowGet);
        //        }
        //        else
        //        {

        //            //TempData["errorMsg"] = "Invalid LoginId & Password";
        //            return Json(obj, JsonRequestBehavior.AllowGet);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "Student dev Login page : ", "Registration" + jsonstring);


        //    }
        //    return View();
        //}
        //public ActionResult Logout()
        //{
        //    UserLogin.ExpireAllCookies();
        //    return RedirectToAction("Index", "Login");
        //}


        //  Admin  Forgot Password  start
        //public ActionResult ForgotPassword(string id)
        //{
        //    string AdminID = EncriptDecript.Decrypt(id);
        //    BL_ForgotPass obj = new BL_ForgotPass();

        //    obj.AdminID = Convert.ToInt32(AdminID);
        //    ViewBag.ID = Convert.ToInt32(AdminID);
        //    return View();
        //}
        //public JsonResult CaptchaImage()
        //{
        //    string[] strArray = new string[36];
        //   strArray = new string[] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z", "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" };
        //   // strArray = new string[] {   "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z", "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" };

        //    Random autoRand = new Random();
        //    string strCaptcha = string.Empty;
        //    string strCaptcha1 = string.Empty;
        //    for (int i = 0; i < 6; i++)
        //    {
        //        int j = Convert.ToInt32(autoRand.Next(0, 62));
        //        strCaptcha += strArray[j].ToString();
        //        strCaptcha1 += strArray[j].ToString()+" ";
        //    }
        //    Session["CAPTCHA"] = strCaptcha;
            
        //    ImageConverter converter = new ImageConverter();
        //    // Response.BinaryWrite((byte[])converter.ConvertTo(CaptchaGeneration(strCaptcha), typeof(byte[])));
        //    var data = (Convert.ToBase64String((byte[])converter.ConvertTo(CaptchaGeneration(strCaptcha1), typeof(byte[]))));
        //    return Json(data, JsonRequestBehavior.AllowGet);
        //}
        //public Bitmap CaptchaGeneration(string captchatxt)
        //{
        //    Bitmap bmp = new Bitmap(250, 100);
        //    using (Graphics graphics = Graphics.FromImage(bmp))
        //    {
        //        Font font = new Font("Tahoma", 14,FontStyle.Bold);
        //        Random objRandom = new Random();
        //        graphics.DrawLine(Pens.Black, objRandom.Next(0, 50), objRandom.Next(10, 30), objRandom.Next(0, 200), objRandom.Next(0, 50));
        //        graphics.DrawRectangle(Pens.Blue, objRandom.Next(0, 20), objRandom.Next(0, 20), objRandom.Next(50, 80), objRandom.Next(0, 20));
        //        graphics.DrawLine(Pens.Blue, objRandom.Next(0, 20), objRandom.Next(10, 50), objRandom.Next(100, 200), objRandom.Next(0, 80));
        //        // graphics.FillRectangle(new SolidBrush(Color.Gray), 0, 0, bmp.Width, bmp.Height);
        //        // graphics.DrawString(captchatxt, font, new SolidBrush(Color.Gold), 25, 10);
        //        Brush objBrush =        default(Brush);
        //        //create background style  
        //        HatchStyle[] aHatchStyles = new HatchStyle[]
        //        {
        //            HatchStyle.BackwardDiagonal, HatchStyle.Cross, HatchStyle.DashedDownwardDiagonal, HatchStyle.DashedHorizontal, HatchStyle.DashedUpwardDiagonal, HatchStyle.DashedVertical,
        //            HatchStyle.DiagonalBrick, HatchStyle.DiagonalCross, HatchStyle.Divot, HatchStyle.DottedDiamond, HatchStyle.DottedGrid, HatchStyle.ForwardDiagonal, HatchStyle.Horizontal,
        //            HatchStyle.HorizontalBrick, HatchStyle.LargeCheckerBoard, HatchStyle.LargeConfetti, HatchStyle.LargeGrid, HatchStyle.LightDownwardDiagonal, HatchStyle.LightHorizontal
        //        };
        //        RectangleF oRectangleF = new RectangleF(0, 0, 300, 300);
        //        objBrush = new HatchBrush(aHatchStyles[objRandom.Next(aHatchStyles.Length - 3)], Color.FromArgb((objRandom.Next(100, 255)), (objRandom.Next(100, 255)), (objRandom.Next(100, 255))), Color.White);
        //        graphics.FillRectangle(objBrush, oRectangleF);
        //        //Generate the image for captcha  
        //        Font objFont = new Font("Tahoma", 20, FontStyle.Bold);//"Courier New"
        //        //Draw the image for captcha  
        //        graphics.DrawString(captchatxt, objFont, Brushes.Black, 20, 30);


        //        graphics.Flush();
        //        font.Dispose();
        //        graphics.Dispose();
        //    }
        //    return bmp;
        //}
    }

}