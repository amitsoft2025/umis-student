using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.Configuration;

namespace DataLayer
{
    public static class CommonSetting
    {
        public enum UserType
        {
            Admin = 1,
            SubAdmin = 2,
            Accountant = 3,
            Cashier = 4,
            Librarian = 5

        }
        public enum Commonid
        {
            Educationtype = 11,
            EducationtypePG = 12,
            EducationtypeVoc = 13,
            EducationtypeBEd = 40,
            EducationtypeLLB = 41,
            EducationtypeBPharma = 42,
            EducationtypeUGCBCS = 44,
            Femalegender = 9,
            General = 4,
            SC = 5,
            ST = 6,
            BC1 = 7,
            BC2 = 22,
            WBC = 23,
            Sports = 35,
            NCCCandidate = 36,
            Exserviceman = 37,
            UniversityStaff = 38,
            RegularAdmissionType = 1,
            Mr = 16,
            Mrs = 17,
            Miss = 18
        }
        public enum Religion
        {
            OtherReligion = 28
        }
        public enum Streamtype
        {
            Art12 = 1,
            Science12 = 2,
            Comm12 = 3,
            ba = 10,
            bsc = 11,
            bcomm = 12,
            LLBba = 13,
            LLBbsc = 14,
            LLBbcomm = 15,
            BPharamabcomm = 31,
            UGCBCSbcomm = 36,
        }
        public enum commQualification
        {
            Ten = 1,
            Art12 = 2,
            Science12 = 3,
            Comm12 = 4,
            diploma = 11,
            ArtUG = 5,
            ScienceUG = 7,
            CommUG = 9,
            others = 12,
                  PCM = 13,
            PCB = 14,
         
        }
        public enum coursecategory
        {
            ba = 1,
            bsc = 2,
            bcomm = 3,
            bca = 26,
            bba = 27,
            Ma = 7,
            Msc = 11,
            Mcomm = 7,
            BEd = 28,
            LLB = 29,
            BioTech = 30,
            BPharma = 31,
            CBCSBASocialScience = 33,
            CBCSBSc = 34,
            CBCSBCom = 35,
            CBCSBAHumanities = 36,
        }
        public enum CourseYearID
        {
            BA1st = 1,
            BA2nd = 2,
            BA3rd = 3,
            Bsc1st = 4,
            Bsc2nd = 5,
            Bsc3rd = 6,
            Bcom1st = 7,
            Bcom2nd = 8,
            Bcom3rd = 9,
            CHEMISTRYID = 1049,
            bca1sem = 10,
            bca2sem = 11,
            bca3sem = 12,
            bca4sem = 13,
            bca5sem = 14,
            bca6sem = 15,
            ma1sem = 16,
            ma2sem = 17,
            ma3sem = 40,
            ma4sem = 41,
            bba1sem = 18,
            bba2sem = 19,
            bba3sem = 20,
            bba1st = 18,
            bba2nd = 19,
            bba3rd = 20,
            bba4sem = 21,
            bba5sem = 22,
            bba6sem = 23,
            BEDpart1 = 28,
            BEDpart2 = 29,
            LLB1sem = 30,
            LLB2sem = 31,
            LLB3sem = 32,
            LLB4sem = 33,
            LLB5sem = 34,
            LLB6sem = 35,
            msc1sem = 24,
            msc2sem = 25,
            msc3sem = 36,
            msc4sem = 37,
            mcom1sem = 26,
            mcom2sem = 27,
            mcom3sem = 38,
            mcom4sem = 39,
            biotech1sem = 42,
            biotech2sem = 43,
            biotech3sem = 44,
            biotech4sem = 45,
            biotech5sem = 46,
            biotech6sem = 47,
            biotech1st = 42,
            biotech2nd = 43,
            biotech3rd = 44,

            bpharma1sem = 48,
            bpharma2sem = 49,
            bpharma3sem = 50,
            bpharma4sem = 51,
            bpharmasem =  52,
            bpharma6sem = 53,
            bpharma7sem = 54,
            bpharma8sem = 55,

        }
        public enum Honours
        {
            HONOURS = 1,
            SUBSIDIARY = 2,
            COMPULSORY = 3
        }
        public enum Coursestreamcategoryid
        {
            BCAstreamid = 1124,
            BBAstreamid = 1125,
            Biotechstreamid = 1144
        }

        public static string ProjectName = WebConfigurationManager.AppSettings["ProjectName"];
        public static string Usernmae = ClsLanguage.GetCookies("NUsername");
        public static string LoginID = ClsLanguage.GetCookies("NLoginID");
        //public static string Email = "deepbr22@gmail.com";
        //public static string EmailPassword = "G00gl3@123@#$";
        //public static string EmailIp = "";
        //public static string EmailHost = "smtp.gmail.com";
        //public static int EmailPort = 587;
        public static string ProjectNamecapital = "MUNGER UNIVERSITY";
        public static string Email = "admission-no-reply@Demouniversiry.ac.in";
        public static string EmailPassword = "Munger@123";
        public static string EmailIp = "";
        public static string EmailHost = "smtp.sendgrid.net";
        public static int EmailPort = 587;


        public static string Emailbgimgurl = WebConfigurationManager.AppSettings["siteUrl"] + "emailtemplate/bg.png";
        public static string Emaillogo = WebConfigurationManager.AppSettings["EmailLogopath"] + "";
        public static string Emailfblogo = WebConfigurationManager.AppSettings["siteUrl"] + "emailtemplate/fb.png";
        public static string Emailtelogo = WebConfigurationManager.AppSettings["siteUrl"] + "emailtemplate/twitter.png";
        public static string Emailgooglelogo = WebConfigurationManager.AppSettings["siteUrl"] + "emailtemplate/google.png";
        public static string EmailSt_loginurl = WebConfigurationManager.AppSettings["EmailSt_loginurl"];
        //public static string Patient { get { return ""; } }
        //public static string Doctor { get { return ""; } }
        // public static string SiteUrl { get { return WebConfigurationManager.AppSettings["siteUrl"]; } }
        //public static string ProfilePhoto { get { return SiteUrl + "/uploads/profile/"; } }
        //public static string CountryFlag { get { return SiteUrl + "/uploads/flag/"; } }
        //public static string SpecialityPhoto { get { return SiteUrl + "/uploads/speciality/"; } }
        //public static string StaticPage { get { return SiteUrl + "/uploads/staticpage/"; } }
        //public static double HoursDiff { get { return 5.5; } }
        public static string constr = ConfigurationManager.ConnectionStrings["dbCon"].ConnectionString;
        public static string PhotonConstr = ConfigurationManager.ConnectionStrings["dbCon"].ConnectionString;
        public static string CreatePassword(int length)
        {
            const string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            StringBuilder res = new StringBuilder();
            Random rnd = new Random();
            while (0 < length--)
            {
                res.Append(valid[rnd.Next(valid.Length)]);
            }
            return res.ToString();
        }
        public static string RemoveSpecialChars(string str)
        {
            // Create  a string array and add the special characters you want to remove
            //You can include / exclude more special characters based on your needs
            string[] chars = new string[] { ",", "/", "!", "@", "#", "$", "%", "^", "&", "*", "'", "\"", ";", "_", "(", ")", ":", "|", "[", "]", "<", ">", "?", "!", "#", "$", "%", "&", "'", "(", ")", "*", "+", "-", "/", ":", ";", "<", "=", ">", "?", "@", "[", "\",", "]", "^", "_", "`", "{", "|", "}", "~" };
            //Iterate the number of times based on the String array length.
            if (str == null)
            {
                return str;
            }
            for (int i = 0; i < chars.Length; i++)
            {

                if (str.Contains(chars[i]))
                {
                    str = str.Replace(chars[i], "");
                }
            }

            return str;
        }
        public static string RemoveSpecialCharsemail(string str)
        {
            // Create  a string array and add the special characters you want to remove
            //You can include / exclude more special characters based on your needs
            string[] chars = new string[] { ",", "/", "!", "#", "$", "%", "^", "&", "*", "'", "\"", ";", "(", ")", ":", "|", "[", "]", "<", ">", "?", "!", "#", "$", "%", "&", "'", "(", ")", "*", "+", "-", "/", ":", ";", "<", "=", ">", "?", "[", "\",", "]", "^", "`", "{", "|", "}", "~" };   //Iterate the number of times based on the String array length.
            if (str == null)
            {
                return str;
            }
            for (int i = 0; i < chars.Length; i++)
            {

                if (str.Contains(chars[i]))
                {
                    str = str.Replace(chars[i], "");
                }
            }

            return str;
        }
        public static string RemoveSpecialCharsaddress(string str)
        {
            // Create  a string array and add the special characters you want to remove
            //You can include / exclude more special characters based on your needs
            string[] chars = new string[] { "!", "@", "#", "$", "%", "^", "&", "*", "'", "\"", ";", "_", "(", ")", ":", "|", "[", "]", "<", ">", "?", "!", "#", "$", "%", "&", "'", "(", ")", "*", "+", ":", ";", "<", "=", ">", "?", "@", "[", "\",", "]", "^", "_", "`", "{", "|", "}", "~" };    //Iterate the number of times based on the String array length.
            if (str == null)
            {
                return str;
            }
            for (int i = 0; i < chars.Length; i++)
            {

                if (str.Contains(chars[i]))
                {
                    str = str.Replace(chars[i], "");
                }
            }

            return str;
        }
        public static string Removenumber(string str)
        {
            // Create  a string array and add the special characters you want to remove
            //You can include / exclude more special characters based on your needs
            string[] chars = new string[] { "0", "2", "3", "4", "5", "6", "7", "8", "9", "1" };
            if (str == null)
            {
                return str;
            }
            for (int i = 0; i < chars.Length; i++)
            {

                if (str.Contains(chars[i]))
                {
                    str = str.Replace(chars[i], "0");
                }
            }

            return str;
        }
        public static bool SendMail(string TO, string Sub, string msg, string[] CC = null)
        {
            bool status = false;
            try
            {
                if (WebConfigurationManager.AppSettings["EnableEmail"] == "true")
                {

                    string StrBody = "";
                    MailMessage mail = new MailMessage();
                    mail.Priority = MailPriority.High;
                    mail.To.Add(TO);
                    if (CC != null)
                    {
                        foreach (var strEmail in CC)
                        {
                            mail.CC.Add(strEmail);
                        }
                    }
                    mail.From = new MailAddress(Email, ProjectName);
                    mail.Subject = Sub;
                    mail.IsBodyHtml = true;
                    StrBody = msg;
                    mail.Body = StrBody;
                    SmtpClient smtp = new SmtpClient(EmailIp);
                    NetworkCredential Credentials = new NetworkCredential(Email, EmailPassword);
                    smtp.Credentials = Credentials;
                    smtp.EnableSsl = true;
                    smtp.Host = EmailHost;
                    smtp.Port = EmailPort;
                    smtp.Send(mail);
                    status = true;
                }
            }
            catch (Exception ex)
            {
                //CommonMethod.PrintLog(ex,"", Sub);
                CommonMethod.WritetoNotepad(ex, "", "Email Send", Sub);

                status = false;

            }
            return status;

        }

    }
    public class courseyear
    {
        public string ID { get; set; }
        public string YearName { get; set; }
        public int CourseID { get; set; }
    }
}
