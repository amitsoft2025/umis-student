using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataLayer;
using System.Data;
using System.IO;
using System.Configuration;
using Website.Models;
using System.Threading;
using Newtonsoft.Json;
using Website.Areas.Student.Models;
using System.Security.Cryptography;
using System.Text;
using System.Net;
using System.Collections.Specialized;
using ThirdParty.Json.LitJson;
using System.Web.Script.Serialization;
using System.Net;
using System.Web.Script.Serialization;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using System.Drawing;
using System.Data.SqlClient;
namespace Website.Controllers
{
    public class DOubleController : Controller
    {
        // GET: DOuble
      
        public ActionResult DoubleVerification_CollegeFeesloop()
        {

            try
            {
                doubleverificationgetstudent obj211 = new doubleverificationgetstudent();
                UserLogin objlogin = new UserLogin();
                 var obj = objlogin.GetStudents_ForDoubleVerification(obj211);
                foreach (var item in obj)

                {
                    string mid = item.Mid;
                    string trxid = item.clienttrxid;
                    string sid = item.sid.ToString();
                    string applicationno = item.applicationno;
                    string URI = "https://www.sbiepay.sbi/payagg/orderStatusQuery/getOrderStatusQuery";
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                    var request = (HttpWebRequest)WebRequest.Create(URI);
                    var postData = "queryRequest=|" + mid + "|" + trxid;
                    postData += "&aggregatorId=SBIEPAY";
                    postData += "&merchantId=" + mid + "";
                    var data = Encoding.ASCII.GetBytes(postData);
                    request.Method = "POST";
                    request.ContentType = "application/x-www-form-urlencoded";
                    request.ContentLength = data.Length;
                    using (var stream = request.GetRequestStream())
                    {
                        stream.Write(data, 0, data.Length);
                    }
                    var response = (HttpWebResponse)request.GetResponse();

                 
                    var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
                    if (!string.IsNullOrEmpty(responseString))
                    {
                        string[] result11 = responseString.Split('|');
                        if (!string.IsNullOrEmpty(result11[2].ToLower()))
                        {
                            obj211.applicationno = item.applicationno;
                            obj211.sid = item.sid;
                            obj211.clienttrxid = item.clienttrxid;
                            obj211.paystatus = result11[2].ToLower();
                            int Returnvalue = objlogin.addtempdata(obj211);

                        }
                    }
                   // TempData["responseString"] = responseString;
                    SbiepayAdmission sbi = new SbiepayAdmission();
                    var result = sbi.doubleverification(responseString);
                    System.Threading.Thread.Sleep(10);
                }
            }
            catch (Exception ex)
            { }
            return View();
        }

        public ActionResult DoubleVerification_300loop()
        {
            try
            {

                doubleverificationgetstudent obj211 = new doubleverificationgetstudent();

                Recruitment re = new Recruitment();
                DataTable dt = new DataTable();
                UserLogin objlogin = new UserLogin();
                dt = null;
                var obj= objlogin.GetStudents_ForDoubleVerification300(obj211);
                int a = 0;
                string mid = "";
                string trxid = "";
                foreach (var item in obj)
                {

                    mid = "1000694";
                    trxid = item.clienttrxid;
                   // CommonMethod.WritetoNotepad(null, HttpContext.Request.Url.AbsolutePath, "DoubleVerificationURlRegistration " + "Breakpreetam heelo ");
                    string URI = "https://www.sbiepay.sbi/payagg/orderStatusQuery/getOrderStatusQuery";
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                    var request = (HttpWebRequest)WebRequest.Create(URI);
                    var postData = "queryRequest=|" + mid + "|" + trxid;
                    postData += "&aggregatorId=SBIEPAY";
                    postData += "&merchantId=" + mid + "";
                    var data = Encoding.ASCII.GetBytes(postData);
                    request.Method = "POST";
                    request.ContentType = "application/x-www-form-urlencoded";
                    request.ContentLength = data.Length;
                    using (var stream = request.GetRequestStream())
                    {
                        stream.Write(data, 0, data.Length);
                    }
                    var response = (HttpWebResponse)request.GetResponse();
                    var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
                    Sbiepay sbi = new Sbiepay();

                    if (!string.IsNullOrEmpty(responseString))
                    {
                        string[] result11 = responseString.Split('|');
                        if (!string.IsNullOrEmpty(result11[2].ToLower()))
                        {
                            obj211.applicationno = item.applicationno;
                            obj211.sid = Convert.ToInt32(item.sid);
                            obj211.clienttrxid = item.clienttrxid;
                            obj211.paystatus = result11[2].ToLower();
                            int Returnvalue = objlogin.addtempdata4(obj211);

                        }
                    }
                    a++;
                    // end for save reord not repeat aganin
                    var result = sbi.doubleverificationregistration(responseString);
                    //CommonMethod.WritetoNotepad(null, HttpContext.Request.Url.AbsolutePath, "DoubleVerificationURl " + "Break" + responseString);
                    System.Threading.Thread.Sleep(10);
                }

            }
            catch (SystemException ex)
            {
                //CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "DoubleVerificationURl admission Paymentgateway Pushresponse  url hit on Controller error");
                return View();
            }
            return View();
        }
        public ActionResult DoubleVerification_enroll()
        {
            try
            {

                doubleverificationgetstudent obj211 = new doubleverificationgetstudent();

                Recruitment re = new Recruitment();
                DataTable dt = new DataTable();
                UserLogin objlogin = new UserLogin();
                dt = null;
                var obj = objlogin.GetStudents_ForDoubleVerificationgetenroll(obj211);
                int a = 0;
                string mid = "";
                string trxid = "";
                foreach (var item in obj)
                {
                    SbiPayEnrollment sbi = new SbiPayEnrollment();
                    mid = sbi.MID;
                    trxid = item.clienttrxid;
                    // CommonMethod.WritetoNotepad(null, HttpContext.Request.Url.AbsolutePath, "DoubleVerificationURlRegistration " + "Breakpreetam heelo ");
                    string URI = "https://www.sbiepay.sbi/payagg/orderStatusQuery/getOrderStatusQuery";
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                    var request = (HttpWebRequest)WebRequest.Create(URI);
                    var postData = "queryRequest=|" + mid + "|" + trxid;
                    postData += "&aggregatorId=SBIEPAY";
                    postData += "&merchantId=" + mid + "";
                    var data = Encoding.ASCII.GetBytes(postData);
                    request.Method = "POST";
                    request.ContentType = "application/x-www-form-urlencoded";
                    request.ContentLength = data.Length;
                    using (var stream = request.GetRequestStream())
                    {
                        stream.Write(data, 0, data.Length);
                    }
                    var response = (HttpWebResponse)request.GetResponse();

                    var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
                    // TempData["responseString"] = responseString;
                    // for save reord not repeat aganin
                    //string connetionString = null;
                    //SqlConnection sqlCnn;
                    //SqlCommand sqlCmd;
                    //SqlDataAdapter adapter = new SqlDataAdapter();
                    //DataSet ds = new DataSet();
                    //int i = a;
                    //string sql = null;
                    //connetionString = "Data Source=101.53.153.84;initial catalog=db_DemoUniversity1;user id=mungeruser;password=User%#$#lj45;MultipleActiveResultSets=True";
                    //sqlCnn = new SqlConnection(connetionString);
                    //SqlCommand cmd = new SqlCommand("insert into [tempsms2]  (mobileno,applicationno,transactionid,type) values(@mobileno,@applicationo,@transactionid,@type)", sqlCnn);
                    //cmd.Parameters.AddWithValue("@mobileno", item["sid"].ToString());
                    //cmd.Parameters.AddWithValue("@applicationo", item["clienttrxid"].ToString());
                    //cmd.Parameters.AddWithValue("@transactionid", item["clienttrxid"].ToString());
                    //cmd.Parameters.AddWithValue("@type", 12);
                    //sqlCnn.Open();
                    //cmd.ExecuteNonQuery();
                    //sqlCnn.Close();
                    if (!string.IsNullOrEmpty(responseString))
                    {
                        string[] result11 = responseString.Split('|');
                        if (!string.IsNullOrEmpty(result11[2].ToLower()))
                        {
                            obj211.applicationno = item.applicationno;
                            obj211.sid = Convert.ToInt32(item.sid);
                            obj211.clienttrxid = item.clienttrxid;
                            obj211.paystatus = result11[2].ToLower();
                            int Returnvalue = objlogin.addtempdata5(obj211);

                        }
                    }
                    a++;
                    // end for save reord not repeat aganin
                    var result = sbi.doubleverificationregistration(responseString);
                    //CommonMethod.WritetoNotepad(null, HttpContext.Request.Url.AbsolutePath, "DoubleVerificationURl " + "Break" + responseString);
                    System.Threading.Thread.Sleep(10);
                }

            }
            catch (SystemException ex)
            {
                //CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "DoubleVerificationURl admission Paymentgateway Pushresponse  url hit on Controller error");
                return View();
            }
            return View();
        }
    }
}