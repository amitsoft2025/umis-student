using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Web.Configuration;
using System.Threading;
using System.Web.Mail;
using System.Net.Mail;

namespace DataLayer
{
    public class Email
    {
        public static void SendEmailForSt_signup(string email, string password, string name, string applicaionno, string mobileno)
        {
            try
            {
                string msgBody = "";
                string body = string.Empty;
                // string path = WebConfigurationManager.AppSettings["siteUrl"];
                string filePath = System.Web.HttpContext.Current.Server.MapPath("~/");
                filePath = filePath + "emailtemplate/signup.html";
                using (StreamReader reader = new StreamReader(filePath))
                {
                    body = reader.ReadToEnd();
                }
                string projectname = DataLayer.CommonSetting.ProjectName.ToString();
                body = body.Replace("@name@", name); //replacing the required things  
                body = body.Replace("@loginid@", applicaionno);
                body = body.Replace("@password@", password);
                body = body.Replace("@year@", DateTime.Now.Year.ToString());
                body = body.Replace("@bgurl@", DataLayer.CommonSetting.Emailbgimgurl);
                body = body.Replace("@logourl@", DataLayer.CommonSetting.Emaillogo);
                body = body.Replace("@fburl@", DataLayer.CommonSetting.Emailfblogo);
                body = body.Replace("@twittterurl@", DataLayer.CommonSetting.Emailtelogo);
                body = body.Replace("@gooleourl@", DataLayer.CommonSetting.Emailgooglelogo);
                body = body.Replace("@@projectname@", DataLayer.CommonSetting.ProjectName);
                body = body.Replace("@loginurl@", DataLayer.CommonSetting.EmailSt_loginurl);
                //DataLayer.CommonSetting.SendMail(email, "Student Registration", body);
                //  sendmailthread(body, "Student Registration", email);
                // SMSFUN.sms_StudentRegistration(1, mobileno, name, applicaionno, password)
                if (WebConfigurationManager.AppSettings["EnableSMS"].ToString() != "true")
                    return;
                SMSFUN.sms_StudentRegistration(1, mobileno, name, applicaionno, password, projectname);
                //SMSFUN.sms_StudentRegistration(1, mobileno, name, applicaionno, password, projectname);
                //Thread backgroundThread2 = new Thread(() => ;
                //backgroundThread2.IsBackground = true;
                //backgroundThread2.Start();
                if (WebConfigurationManager.AppSettings["EnableEmail"].ToString() != "true")
                    return;
                //sendmailthread(body, "Student Registration", email);
                Thread backgroundThread = new Thread(() => sendmailthread(body, "Student Registration", email));
                backgroundThread.IsBackground = true;
                backgroundThread.Start();

            }
            catch { }
        }
        public static void sendmailthread(string body, string subject, string email)
        {
            try
            {

                using (System.Net.Mail.MailMessage mailMessage = new System.Net.Mail.MailMessage())
                {
                    mailMessage.From = new MailAddress("helpdesk@Demouniversiry.ac.in", "no-reply");
                    mailMessage.Subject = subject;
                    mailMessage.Body = body.Replace("\n", "").Replace("\r", "").Replace("\r\n", "");
                    mailMessage.IsBodyHtml = true;
                    mailMessage.To.Add(new MailAddress(email));
                    SmtpClient smtp = new SmtpClient();
                    smtp.Host = "smtp.gmail.com";
                    smtp.EnableSsl = Convert.ToBoolean(true);
                    System.Net.NetworkCredential NetworkCred = new System.Net.NetworkCredential();
                    NetworkCred.UserName = "helpdesk@Demouniversiry.ac.in";
                    NetworkCred.Password = "Munger@123";
                    smtp.UseDefaultCredentials = true;
                    smtp.Credentials = NetworkCred;
                    smtp.Port = 587;
                    smtp.Send(mailMessage);

                }
                return;
                mailbysendgrid mailsendgrid = new mailbysendgrid();
                mailsendgrid.EmailTemplateFileName = body;
                mailsendgrid.EmailTemplateSubject = subject;
                mailsendgrid.Emailsend = email;
                mailsendgrid.mailsend();

            }
            catch { }
        }


        public static void SendEmailForSt_RegistrationPaymentgateway(string email, string status, string name, string trxdate, string banktrxid, string TransactionId, string ApplicationNo, string Fees, string PaymentType, string mobileno)
        {
            try
            {
                string msgBody = "";
                string body = string.Empty;
                // string path = WebConfigurationManager.AppSettings["siteUrl"];
                string filePath = System.Web.HttpContext.Current.Server.MapPath("~/");

                filePath = filePath + "emailtemplate/Paymentgateway.html";
                using (StreamReader reader = new StreamReader(filePath))
                {
                    body = reader.ReadToEnd();
                }
                body = body.Replace("@name@", name); //replacing the required things  
                body = body.Replace("@Model.objPrintRecipt.status@", status);
                body = body.Replace("@Model.objPrintRecipt.trxdate@", trxdate);
                body = body.Replace("@Model.objPrintRecipt.banktrxid@", banktrxid);
                body = body.Replace("@Model.objPrintRecipt.TransactionId@", TransactionId);
                body = body.Replace("@Model.objfeesubmit.ApplicationNo@ ", ApplicationNo);
                body = body.Replace("@Model.objfeesubmit.Fees@", Fees);
                body = body.Replace("@Model.objPrintRecipt.PaymentType@", PaymentType);
                body = body.Replace("@year@", DateTime.Now.Year.ToString());
                body = body.Replace("@bgurl@", DataLayer.CommonSetting.Emailbgimgurl);
                body = body.Replace("@logourl@", DataLayer.CommonSetting.Emaillogo);
                body = body.Replace("@fburl@", DataLayer.CommonSetting.Emailfblogo);
                body = body.Replace("@twittterurl@", DataLayer.CommonSetting.Emailtelogo);
                body = body.Replace("@gooleourl@", DataLayer.CommonSetting.Emailgooglelogo);
                body = body.Replace("@@projectname@", DataLayer.CommonSetting.ProjectName);
                body = body.Replace("@loginurl@", DataLayer.CommonSetting.EmailSt_loginurl);
                string projectname = DataLayer.CommonSetting.ProjectName.ToString();
                //Thread backgroundThread = new Thread(() => DataLayer.CommonSetting.SendMail(email, WebConfigurationManager.AppSettings["ProjectName"] + "  Fees Acknowledgement", body));
                //backgroundThread.IsBackground = true;
                //backgroundThread.Start();
                //mailbysendgrid mailsendgrid = new mailbysendgrid();
                //mailsendgrid.EmailTemplateFileName = body;
                //mailsendgrid.EmailTemplateSubject = WebConfigurationManager.AppSettings["ProjectName"] + "  Fees Acknowledgement";
                //mailsendgrid.Emailsend = email;
                //mailsendgrid.mailsend();
                if (WebConfigurationManager.AppSettings["EnableSMS"].ToString() != "true")
                    return;
                SMSFUN.sms_StudentPaymentgateway(2, mobileno, name, ApplicationNo, status, banktrxid, TransactionId, projectname);
                if (WebConfigurationManager.AppSettings["EnableEmail"].ToString() != "true")
                    return;
                Thread backgroundThread3 = new Thread(() => sendmailthread(body, WebConfigurationManager.AppSettings["ProjectName"] + " Fees Acknowledgement", email));
                backgroundThread3.IsBackground = true;
                backgroundThread3.Start();



            }
            catch { }
        }
        public static void SendEmailForCollege_signup(string email, string password, string name, string collegeCode, string college, string UserID)
        {
            try
            {
                string msgBody = "";
                string body = string.Empty;
                string filePath = System.Web.HttpContext.Current.Server.MapPath("~/");
                filePath = filePath + "emailtemplate/register_confirmation.html";
                using (StreamReader reader = new StreamReader(filePath))
                {
                    body = reader.ReadToEnd();
                }
                body = body.Replace("@name@", name); //replacing the required things  
                body = body.Replace("@loginid@", UserID);
                body = body.Replace("@password@", password);
                body = body.Replace("@college@", college);
                body = body.Replace("@year@", DateTime.Now.Year.ToString());
                body = body.Replace("@bgurl@", DataLayer.CommonSetting.Emailbgimgurl);
                body = body.Replace("@logourl@", DataLayer.CommonSetting.Emaillogo);
                body = body.Replace("@fburl@", DataLayer.CommonSetting.Emailfblogo);
                body = body.Replace("@twittterurl@", DataLayer.CommonSetting.Emailtelogo);
                body = body.Replace("@gooleourl@", DataLayer.CommonSetting.Emailgooglelogo);
                body = body.Replace("@@projectname@", DataLayer.CommonSetting.ProjectName);
                //DataLayer.CommonSetting.SendMail(email, "College Registration", body);
                mailbysendgrid mailsendgrid = new mailbysendgrid();
                mailsendgrid.EmailTemplateFileName = body;
                mailsendgrid.EmailTemplateSubject = WebConfigurationManager.AppSettings["ProjectName"] + " College Registration";
                mailsendgrid.Emailsend = email;
                mailsendgrid.mailsend();

            }
            catch { }
        }
        public static void SendEmailForResetPassword(string email, string name, string Link)
        {
            try
            {
                string msgBody = "";
                string body = string.Empty;
                string filePath = System.Web.HttpContext.Current.Server.MapPath("~/");
                filePath = filePath + "emailtemplate/ResetPassword.html";
                using (StreamReader reader = new StreamReader(filePath))
                {
                    body = reader.ReadToEnd();
                }
                body = body.Replace("@name@", name); //replacing the required things  
                body = body.Replace("@link@", Link);
                body = body.Replace("@year@", DateTime.Now.Year.ToString());
                body = body.Replace("@bgurl@", DataLayer.CommonSetting.Emailbgimgurl);
                body = body.Replace("@logourl@", DataLayer.CommonSetting.Emaillogo);
                body = body.Replace("@fburl@", DataLayer.CommonSetting.Emailfblogo);
                body = body.Replace("@twittterurl@", DataLayer.CommonSetting.Emailtelogo);
                body = body.Replace("@gooleourl@", DataLayer.CommonSetting.Emailgooglelogo);
                body = body.Replace("@@projectname@", DataLayer.CommonSetting.ProjectName);
                //DataLayer.CommonSetting.SendMail(email, "Reset Password", body);
                mailbysendgrid mailsendgrid = new mailbysendgrid();
                mailsendgrid.EmailTemplateFileName = body;
                mailsendgrid.EmailTemplateSubject = WebConfigurationManager.AppSettings["ProjectName"] + " Reset Password";
                mailsendgrid.Emailsend = email;
                mailsendgrid.mailsend();

            }
            catch { }
        }
        public static void SendEmailDynamic(string email, string collegename, string name, string Status, string mailheading, string subject, string trxdate, string banktrxid, string TransactionId, string ApplicationNo, string Fees, string PaymentType, string mobileno, string surcharge, string totalfee)
        {
            try
            {
                string msgBody = "";
                string body = string.Empty;
                string filePath = System.Web.HttpContext.Current.Server.MapPath("~/");
                filePath = filePath + "emailtemplate/EmailTamplate.html";
                using (StreamReader reader = new StreamReader(filePath))
                {
                    body = reader.ReadToEnd();
                }
                msgBody = "<table width='100%' border='0' cellspacing='0'><tr><td width='50px' align='left' valign='top' style='font-size:16px;'></td><td align='left' valign='top' style='font-size:16px;'></td></tr><tr><td width='50px' align='left' valign='top' style='font-size:16px;'></td><td align='left' valign='top' style='font-size:16px;'></td></tr><tr><td colspan = '2' align = 'left' valign = 'top' style = 'font-size:14px; color:#006DF0;' > ";
                msgBody += mailheading + "</td></tr><tr><td align = 'left' colspan = '2' valign = 'top' height = '15px' ></td></tr>";
                msgBody += "<tr><td width='50px' align='left' valign='top' style='font-size:16px;'></td><td align='left' valign='top' style='font-size:16px;'></td></tr><tr><td width='50px' align='left' valign='top' style='font-size:16px;'></td><td align='left' valign='top' style='font-size:16px;'></td></tr>";
                msgBody += "<tr><td width = '50px' align = 'left' valign = 'top' style = 'font-size:16px;' ><strong > Dear : </strong ></td><td align = 'left' valign = 'top' style = 'font-size:16px;'><strong> " + name + " </strong ></td ></tr > ";
                msgBody += "<tr ><td align = 'left' colspan = '2' valign = 'top' height = '2px' ></td></tr><tr><table width='100%' border='0' cellspacing='0'>";

                msgBody += "</td></tr>";

                msgBody += @"<tr>
                            <td align='left' valign='top' height='10px'></td>
                        </tr>
                        <tr>
                            <td align='left' valign='top' style='font-size:14px;'>
                                <p style='margin:0; padding:0;'>Your Payment Transaction details are below-</p>
                            </td>
                        </tr>
                        <tr>
                            <td align='left'  valign='top' height='10px'></td>
                        </tr>
                        <tr>
                            <td align='left' valign='top' style='font-size:14px;'>
                                <p style='margin:0; padding:0;'><ul>";
                msgBody += "<li><span>Transaction Status</span> : &nbsp;&nbsp; " + Status + "</li>";
                msgBody += "<li ><span > Transaction Date </span > : &nbsp; &nbsp;" + trxdate + "</li > ";
                msgBody += "<li ><span > Bank Reference Number</span >  : &nbsp; &nbsp; " + banktrxid + "</li > ";
                msgBody += "<li ><span > Transaction Id </span > : &nbsp; &nbsp;" + TransactionId + "</li > ";
                msgBody += "<li ><span > Application Number </span >  : &nbsp; &nbsp;" + ApplicationNo + "</li > ";
                msgBody += "<li ><span > Fees Amount </span > : &nbsp; &nbsp; " + Fees + "</li > ";
                msgBody += "<li ><span > Processingfees (including GST)</span > : &nbsp; &nbsp; " + surcharge + "</li > ";
                msgBody += "<li ><span > Total Amount </span > : &nbsp; &nbsp; " + totalfee + "</li > ";
                msgBody += "<li ><span > Payment Mode </span > : &nbsp; &nbsp; " + PaymentType + "</li > ";
                msgBody += "<li ><span > College Name </span > : &nbsp; &nbsp; " + collegename + "</li > ";

                msgBody += "</ul>   </p>    </td>    </tr>";
                msgBody += "</table>";
                body = body.Replace("@msgBody@", msgBody); //replacing the required things  

                body = body.Replace("@year@", DateTime.Now.Year.ToString());
                body = body.Replace("@bgurl@", DataLayer.CommonSetting.Emailbgimgurl);
                body = body.Replace("@logourl@", DataLayer.CommonSetting.Emaillogo);
                body = body.Replace("@fburl@", DataLayer.CommonSetting.Emailfblogo);
                body = body.Replace("@twittterurl@", DataLayer.CommonSetting.Emailtelogo);
                body = body.Replace("@gooleourl@", DataLayer.CommonSetting.Emailgooglelogo);
                body = body.Replace("@@projectname@", DataLayer.CommonSetting.ProjectName);
                string projectname = DataLayer.CommonSetting.ProjectName.ToString();
                if (WebConfigurationManager.AppSettings["EnableEmail"].ToString() != "true")
                    return;
                Thread backgroundThread6 = new Thread(() => sendmailthread(body.Replace("\n", "").Replace("\r", ""), subject, email));
                backgroundThread6.IsBackground = true;
                backgroundThread6.Start();

                //if (WebConfigurationManager.AppSettings["EnableSMS"].ToString() != "true")
                //    return;
                //Thread backgroundThread7= new Thread(() => SMSFUN.sms_StudentPaymentgateway(2, mobileno, name, ApplicationNo, Status, banktrxid, TransactionId, projectname));
                //backgroundThread7.IsBackground = true;
                //backgroundThread7.Start();

            }
            catch { }

        }
        public static void SendEmailForSt_EnrollmentPaymentgateway(string email, string status, string name, string trxdate, string banktrxid, string TransactionId, string ApplicationNo, string Fees, string PaymentType, string mobileno, string enrollmentNo)
        {
            try
            {
                string msgBody = "";
                string body = string.Empty;
                // string path = WebConfigurationManager.AppSettings["siteUrl"];
                string filePath = System.Web.HttpContext.Current.Server.MapPath("~/");

                filePath = filePath + "emailtemplate/EnrollmentPayment.html";
                using (StreamReader reader = new StreamReader(filePath))
                {
                    body = reader.ReadToEnd();
                }
                body = body.Replace("@name@", name); //replacing the required things  
                body = body.Replace("@Model.objPrintRecipt.status@", status);
                body = body.Replace("@Model.objPrintRecipt.trxdate@", trxdate);
                body = body.Replace("@Model.objPrintRecipt.banktrxid@", banktrxid);
                body = body.Replace("@Model.objPrintRecipt.TransactionId@", TransactionId);
                body = body.Replace("@Model.objfeesubmit.ApplicationNo@ ", ApplicationNo);
                body = body.Replace("@Model.objfeesubmit.Fees@", Fees);
                body = body.Replace("@Model.objPrintRecipt.PaymentType@", PaymentType);
                body = body.Replace("@Model.objExamForm.RegistrationNo@", enrollmentNo);
                body = body.Replace("@year@", DateTime.Now.Year.ToString());
                body = body.Replace("@bgurl@", DataLayer.CommonSetting.Emailbgimgurl);
                body = body.Replace("@logourl@", DataLayer.CommonSetting.Emaillogo);
                body = body.Replace("@fburl@", DataLayer.CommonSetting.Emailfblogo);
                body = body.Replace("@twittterurl@", DataLayer.CommonSetting.Emailtelogo);
                body = body.Replace("@gooleourl@", DataLayer.CommonSetting.Emailgooglelogo);
                body = body.Replace("@@projectname@", DataLayer.CommonSetting.ProjectName);
                body = body.Replace("@loginurl@", DataLayer.CommonSetting.EmailSt_loginurl);
                string projectname = DataLayer.CommonSetting.ProjectName.ToString();
                //Thread backgroundThread = new Thread(() => DataLayer.CommonSetting.SendMail(email, WebConfigurationManager.AppSettings["ProjectName"] + "  Fees Acknowledgement", body));
                //backgroundThread.IsBackground = true;
                //backgroundThread.Start();
                //mailbysendgrid mailsendgrid = new mailbysendgrid();
                //mailsendgrid.EmailTemplateFileName = body;
                //mailsendgrid.EmailTemplateSubject = WebConfigurationManager.AppSettings["ProjectName"] + "  Fees Acknowledgement";
                //mailsendgrid.Emailsend = email;
                //mailsendgrid.mailsend();
                if (WebConfigurationManager.AppSettings["EnableEmail"].ToString() != "true")
                    return;
                Thread backgroundThread3 = new Thread(() => sendmailthread(body, WebConfigurationManager.AppSettings["ProjectName"] + " Fees Acknowledgement", email));
                backgroundThread3.IsBackground = true;
                backgroundThread3.Start();

                if (WebConfigurationManager.AppSettings["EnableSMS"].ToString() != "true")
                    return;
                Thread backgroundThread4 = new Thread(() => SMSFUN.sms_StudentPaymentgateway(2, mobileno, name, ApplicationNo, status, banktrxid, TransactionId, projectname));
                backgroundThread4.IsBackground = true;
                backgroundThread4.Start();

            }
            catch { }
        }
        public static void SendEmailForSt_ExamPaymentgateway(string email, string status, string name, string trxdate, string banktrxid, string TransactionId, string ApplicationNo, string Fees, string PaymentType, string mobileno, string enrollmentNo)
        {
            try
            {
                string msgBody = "";
                string body = string.Empty;
                // string path = WebConfigurationManager.AppSettings["siteUrl"];
                string filePath = System.Web.HttpContext.Current.Server.MapPath("~/");

                filePath = filePath + "emailtemplate/EnrollmentPayment.html";
                using (StreamReader reader = new StreamReader(filePath))
                {
                    body = reader.ReadToEnd();
                }
                body = body.Replace("@name@", name); //replacing the required things  
                body = body.Replace("@Model.objPrintRecipt.status@", status);
                body = body.Replace("@Model.objPrintRecipt.trxdate@", trxdate);
                body = body.Replace("@Model.objPrintRecipt.banktrxid@", banktrxid);
                body = body.Replace("@Model.objPrintRecipt.TransactionId@", TransactionId);
                body = body.Replace("@Model.objfeesubmit.ApplicationNo@ ", ApplicationNo);
                body = body.Replace("@Model.objfeesubmit.Fees@", Fees);
                body = body.Replace("@Model.objPrintRecipt.PaymentType@", PaymentType);
                body = body.Replace("@Model.objExamForm.RegistrationNo@", enrollmentNo);
                body = body.Replace("@year@", DateTime.Now.Year.ToString());
                body = body.Replace("@bgurl@", DataLayer.CommonSetting.Emailbgimgurl);
                body = body.Replace("@logourl@", DataLayer.CommonSetting.Emaillogo);
                body = body.Replace("@fburl@", DataLayer.CommonSetting.Emailfblogo);
                body = body.Replace("@twittterurl@", DataLayer.CommonSetting.Emailtelogo);
                body = body.Replace("@gooleourl@", DataLayer.CommonSetting.Emailgooglelogo);
                body = body.Replace("@@projectname@", DataLayer.CommonSetting.ProjectName);
                body = body.Replace("@loginurl@", DataLayer.CommonSetting.EmailSt_loginurl);
                string projectname = DataLayer.CommonSetting.ProjectName.ToString();
                //Thread backgroundThread = new Thread(() => DataLayer.CommonSetting.SendMail(email, WebConfigurationManager.AppSettings["ProjectName"] + "  Fees Acknowledgement", body));
                //backgroundThread.IsBackground = true;
                //backgroundThread.Start();
                //mailbysendgrid mailsendgrid = new mailbysendgrid();
                //mailsendgrid.EmailTemplateFileName = body;
                //mailsendgrid.EmailTemplateSubject = WebConfigurationManager.AppSettings["ProjectName"] + "  Fees Acknowledgement";
                //mailsendgrid.Emailsend = email;
                //mailsendgrid.mailsend();
                if (WebConfigurationManager.AppSettings["EnableEmail"].ToString() != "true")
                    return;
                Thread backgroundThread3 = new Thread(() => sendmailthread(body, WebConfigurationManager.AppSettings["ProjectName"] + " Fees Acknowledgement", email));
                backgroundThread3.IsBackground = true;
                backgroundThread3.Start();

                if (WebConfigurationManager.AppSettings["EnableSMS"].ToString() != "true")
                    return;
                Thread backgroundThread4 = new Thread(() => SMSFUN.sms_StudentPaymentgateway(2, mobileno, name, ApplicationNo, status, banktrxid, TransactionId, projectname));
                backgroundThread4.IsBackground = true;
                backgroundThread4.Start();



            }
            catch { }
        }

    }
}
