using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using com.toml.dp.util;
using System.Text;
using DataLayer;
using System.Web.Configuration;
using System.Security.Cryptography;

using com.toml.dp.util;
using DataLayer;
using System;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.IO;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Configuration;
using com.paygate.ag.common.utils;
using System.Collections.Specialized;
using Website.Models;
using Newtonsoft.Json;
using System.Xml.Serialization;

namespace Website.Areas.Student.Models
{
    public class AirPayApplication
    {
        public string requestparams { get; set; }
        public string merchantId { get; set; }
        public string EncryptshippingDetais { get; set; }
        public string EncryptbillingDetails { get; set; }
        public string url { get; set; }
        public string now { get; set; }
        //Airpay
        public string allParamValue1 { get; set; }
        public string sTemp { get; set; }
        public string str256Key { get; set; }
        public string allParamValue12 { get; set; }
        public string checksum1 { get; set; }
        public string checksum { get; set; }
        public string privatekey { get; set; }
        public string customvar { get; set; }
        public string Oid { get; set; }
        public string A_Success_url { get; set; }
        public string A_Mid { get; set; }
        public string allParamValue { get; set; }
        public string GetType { get; set; }
        public string Success_url { get; set; }
        //Airpay END

        //Airpay
        //Local
        //string username = "5926256";
        //string password = "me65Pf2K";
        //string secretKey = "A3brM5V9wjMWZh29";
        //string AirpayMid = "40594";
        //string AirPay_Success_url = "http://localhost:33166/student/Home/PGSucess_AirPay";
        ////string AirPay_Success_url = "https://portal.mungeruniversity.ac.in/student/Home/PGSucess_AirPay";

        //Liveairpay
        string username = "2609081";
        string password = "EJ3Qhr7E";
        string secretKey = "5CKDVhJGqpZ5b2gz";
        string AirpayMid = "256538";
        string AirPay_Success_url = "https://portal.mungeruniversity.ac.in/student/Home/PGSucess_AirPay";

        //AirpayEND
        public AirPayApplication encriptDataAirpay(decimal Amount1, string Other_Details1, string Success_URL1, string Failure_URL1)
        {

            StudentLogin stu = new StudentLogin();
            string ApplicationID = ClsLanguage.GetCookies("NBApplicationNo");
            Login lo = stu.BasicDetail(ApplicationID);
            string sEmail = lo.Email;
            string sPhone = lo.MobileNo;
            string sFName = lo.FirstName;
            string sLName = lo.LastName;
            string sAddress = lo.CurrentAddress;
            string sCity = lo.CA_City.ToString();
            string sState = lo.CA_State.ToString();
            string sCountry = lo.CA_Country.ToString();
            string sPincode = lo.CA_PinCode;
            string sAmount = Amount1.ToString("0.00");
            string sOrderId = "";
            Sbiepay sbi = new Sbiepay();
            //return sbi;
            Success_URL = Success_URL + Success_URL1;
            Failure_URL = Failure_URL + Failure_URL1;
            string Amount = Amount1.ToString();
            string Order_Number = "";
            string Other_Details = Other_Details1;
            DateTime now = DateTime.Now;
            loop:
            Order_Number = CommonMethod.RandomNumber(100000, 999999) + now.ToString("MdHHmmyyssfff");
            StudentLogin stlogin = new StudentLogin();
            FeesSubmit fee = new FeesSubmit();
            fee.ApplicationNo = Other_Details1;
            fee.clienttrxid = Order_Number;
            fee.GetWayType = "AirPay";
            int StID = (ClsLanguage.GetCookies("NBStID") != null ? Convert.ToInt32(ClsLanguage.GetCookies("NBStID")) : 0);
            int Sission = (ClsLanguage.GetCookies("NBSission") != null ? Convert.ToInt32(ClsLanguage.GetCookies("NBSission")) : 0);

            Other_Details = Other_Details1 + ",Registration," + 1 + ",registration-" + "2022" + "," + Order_Number + "," + StID + "," + Order_Number;

            //  Order_Number = "";
            var obj = fee.FeessubStudentbefore(fee);
            if (obj.Status == false)
            {
                goto loop;
            }
            //EncryptTrans = Merchant ID|Operating Mode|Merchant Country|Merchant Currency|Posting Amount|Other Details|SuccessURL | Fail URL | Aggregator ID | Merchant Order Number| Merchant Customer ID| Paymode | Access Medium | Transaction Source
            allParamValue = sEmail + sFName + sLName + sAddress + sCity + sState + sCountry + sAmount + sOrderId;
            DateTime now1 = DateTime.Today; // As DateTime
            sbi.now = now1.ToString("yyyy-MM-dd"); // As String
            sbi.allParamValue1 = allParamValue + now;
            sTemp = sbi.sTemp = secretKey + "@" + username + ":|:" + password;
            sbi.str256Key = EncryptSHA256Managed(sTemp);
            sbi.allParamValue12 = sbi.allParamValue1 + sbi.str256Key;
            sbi.checksum1 = MD5Hash(sbi.allParamValue12);
            sbi.checksum = sbi.checksum1;
            sbi.privatekey = sbi.str256Key;
            sbi.url = saveurl;
            sbi.merchantId = AirpayMid;
            sbi.GetType = "inline";
            sbi.Oid = Order_Number;
            sbi.A_Mid = AirpayMid;
            sbi.A_Success_url = AirPay_Success_url;
            sbi.customvar = Other_Details;
            return sbi;
        }


        public string pgsucessdecryptAirpay(int sid, string banktrxid, string clienttrxid, string amount1, string feeamount, string gst, string commission, string paymode, string banktxndate, string Reason, string apitxnid, string ApplicationNo, string courseyearid, string examType, string encData, string encDatadecryptdata, string PGstatus)
        {
            try
            {
                FeesSubmit stlogin = new FeesSubmit();
                //EaxmFeesSubmit obj = new EaxmFeesSubmit();
                stlogin.Requestdata = encData;
                stlogin.dRequestdata = encDatadecryptdata;
                stlogin.PGstatus = PGstatus;
                gst = gst;
                commission = commission;
                CommonMethod.TraceLogWritetoNotepad("obj1.objExamFrom4", "Exam/sbipay11", "AirPaypgsucessdecrypt", "Id");
                stlogin.banktrxid = banktrxid;
                stlogin.clienttrxid = clienttrxid;
                stlogin.amount = amount1;
                stlogin.feeamount = feeamount;
                stlogin.gst = gst;
                stlogin.commission = commission;
                stlogin.paymode = paymode;
                stlogin.banktxndate = banktxndate;
                stlogin.Reason = Reason;
                stlogin.apitxnid = apitxnid;
                //var other_detail = aa[6].Split(',');
                stlogin.ApplicationNo = ApplicationNo;

                var obj = stlogin.FeessubStudent(stlogin);
                if (obj.Id == 0)
                {
                    paymentgatewayemail(ApplicationNo);
                }
                return encDatadecryptdata;
            }
            catch (Exception ex)
            {
                CommonMethod.WritetoNotePaymentgateway(ex.Message + "      " + ex.StackTrace, "", HttpContext.Current.Request.Url.AbsolutePath, "pgsucessdecrypt() url hit error on payment PaymentGateway", ClsLanguage.GetCookies("NBApplicationNo"));
                return "error";
            }
        }

        public string EncryptSHA256Managed(String ClearString)
        {

            byte[] bytClearString = System.Text.ASCIIEncoding.UTF8.GetBytes(ClearString);

            HashAlgorithm sha = new SHA256Managed();

            byte[] hash = sha.ComputeHash(bytClearString);

            StringBuilder hexString = new StringBuilder(hash.Length);
            for (int i = 0; i < hash.Length; i++)
            {
                hexString.Append(hash[i].ToString("x2"));
            }
            return hexString.ToString();

        }

        public static string MD5Hash(string text)
        {
            byte[] bytClearString = System.Text.ASCIIEncoding.UTF8.GetBytes(text);

            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] hash = md5.ComputeHash(bytClearString);


            byte[] result = md5.Hash;

            StringBuilder strBuilder = new StringBuilder(hash.Length);
            for (int i = 0; i < hash.Length; i++)
            {

                strBuilder.Append(hash[i].ToString("x2"));
            }

            return strBuilder.ToString();
        }
    }


}