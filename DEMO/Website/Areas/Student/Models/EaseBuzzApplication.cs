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
    public class EaseBuzzApplication
    {
        //EaseBuzz
        public string Eeasebuzz_action_url { get; set; }
        public string Egen_hash { get; set; }
        public string Etxnid { get; set; }
        public string easebuzz_merchant_key { get; set; }
        public string ssalt { get; set; }
        public string EKey { get; set; }
        public string Eenv { get; set; }
        public string Ehash_string { get; set; }
        public string Esaltvalue { get; set; }
        public string Eamount { get; set; }
        public string Efirstname { get; set; }
        public string Eemail { get; set; }
        public string Ephone { get; set; }
        public string Eproductinfo { get; set; }
        public string Esurl { get; set; }
        public string Efurl { get; set; }
        public string Eudf1 { get; set; }
        public string Eudf2 { get; set; }
        public string Eudf3 { get; set; }
        public string Eudf4 { get; set; }
        public string Eudf5 { get; set; }
        public string EstrForm { get; set; }

        //easeBuzz live
        public string salt = "F6LU2RUJ1C";
        public string Key = "I2DPETI0FE";
        public string env = "prod";
        public string EasySuccessUrl = "https://portal.mungeruniversity.ac.in/student/Home/PGSucess_EaseBuzz";
        public string EasyFailUrl = "https://portal.mungeruniversity.ac.in/student/Home/PGSucess_EaseBuzz";

        //easeBuzz local
        //public string salt = "DAH88E3UWQ";
        //public string Key = "2PBP7IABZ2";
        //public string env = "test";
        //public string EasySuccessUrl = "http://localhost:33166/student/Home/PGSucess_EaseBuzz";
        //public string EasyFailUrl = "http://localhost:33166/student/Home/PGSucess_EaseBuzz";
        //EaseBuzz End
        public EaseBuzzApplication encriptDataEaseBuzz(decimal Amount1, string Other_Details1, string Success_URL1, string Failure_URL1)
        {

            StudentLogin stu = new StudentLogin();
            string ApplicationID = ClsLanguage.GetCookies("NBApplicationNo");
            Login lo = stu.BasicDetail(ApplicationID);
            string[] hashVarsSeq;
            string hash_string = string.Empty;
            string saltvalue = salt;
            string amount = Amount1.ToString();
            string firstname = lo.FirstName + " " + lo.LastName;
            string email = lo.Email;
            string phone = lo.MobileNo;
            string productinfo = "Registration Fees";
            string easebuzz_action_url = "";
            string surl = "";
            string key = "";
            string furl = "";
            string udf1 = "";
            string udf2 = "";
            string udf3 = "";
            string udf4 = "";
            string udf5 = "";
            string udf6 = "";
            string udf7 = "";
            string udf9 = "";
            string udf10 = "";
            string txnid = "";
            string gen_hash = "";

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
            fee.GetWayType = "EaseBuzz";
            //  Order_Number = "";
            var obj = fee.FeessubStudentbefore(fee);
            if (obj.Status == false)
            {
                goto loop;
            }
            string ShowPaymentMode = "";
            // Generate transaction ID -> make sure this is unique for all transactions
            Random rnd = new Random();
            string strHash = Easebuzz_Generatehash512(rnd.ToString() + DateTime.Now);
            //txnid = strHash.ToString().Substring(0, 20);
            txnid = Order_Number;
            string paymentUrl = getURL();
            // Get configs from web config
            easebuzz_action_url = paymentUrl + "/pay/secure";
            // generate hash table
            System.Collections.Hashtable data = new System.Collections.Hashtable(); // adding values in gash table for data post
            data.Add("txnid", txnid);
            data.Add("key", Key);
            //string AmountForm = Convert.ToDecimal(amount.Trim()).ToString("g29");// eliminating trailing zeros

            data.Add("amount", amount);
            data.Add("firstname", firstname.Trim());
            data.Add("email", email.Trim());
            data.Add("phone", phone.Trim());
            data.Add("productinfo", productinfo.Trim());
            data.Add("surl", EasySuccessUrl.Trim());
            data.Add("furl", EasyFailUrl.Trim());
            data.Add("udf1", Other_Details1.Trim());
            data.Add("udf2", "Registration");
            data.Add("udf3", "");
            data.Add("udf4", Order_Number.Trim());
            data.Add("udf5", "");
            // generate hash
            hashVarsSeq = "key|txnid|amount|productinfo|firstname|email|udf1|udf2|udf3|udf4|udf5|udf6|udf7|udf8|udf9|udf10".Split('|'); // spliting hash sequence from config
            hash_string = "";
            foreach (string hash_var in hashVarsSeq)
            {
                hash_string = hash_string + (data.ContainsKey(hash_var) ? data[hash_var].ToString() : "");
                hash_string = hash_string + '|';
            }
            hash_string += salt;// appending SALT
            gen_hash = Easebuzz_Generatehash512(hash_string).ToLower();        //generating hash
            data.Add("hash", gen_hash);
            data.Add("show_payment_mode", ShowPaymentMode.Trim());
            string strForm = Easebuzz_PreparePOSTForm(easebuzz_action_url, data);
            sbi.Etxnid = txnid;
            sbi.EKey = Key;
            sbi.Eamount = amount;
            sbi.Efirstname = firstname;
            sbi.Eemail = email;
            sbi.Ephone = phone;
            sbi.Eproductinfo = productinfo;
            sbi.Eeasebuzz_action_url = saveurl;
            sbi.Esurl = EasySuccessUrl.ToString();
            sbi.Efurl = EasyFailUrl.ToString();
            sbi.Eudf1 = Other_Details1;
            sbi.Eudf2 = "Registration";
            sbi.Eudf3 = "";
            sbi.Eudf4 = Order_Number;
            sbi.Eudf5 = "";
            sbi.Esaltvalue = salt.ToString();
            sbi.Ehash_string = gen_hash.ToString();
            sbi.EstrForm = strForm;
            sbi.Eeasebuzz_action_url = easebuzz_action_url.ToString();
            return sbi;
        }
        public string pgsucessdecryptEaseBuzz(int sid, string banktrxid, string clienttrxid, string amount1, string feeamount, string gst, string commission, string paymode, string banktxndate, string Reason, string apitxnid, string ApplicationNo, string courseyearid, string examType, string encData, string encDatadecryptdata, string PGstatus)
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
        public string Easebuzz_Generatehash512(string text)
        {
            byte[] message = Encoding.UTF8.GetBytes(text);
            UnicodeEncoding UE = new UnicodeEncoding();
            byte[] hashValue;
            SHA512Managed hashString = new SHA512Managed();
            string hex = "";
            hashValue = hashString.ComputeHash(message);
            foreach (byte x in hashValue)
            {
                hex += String.Format("{0:x2}", x);
            }
            return hex;

        }

        public string Easebuzz_PreparePOSTForm(string url, System.Collections.Hashtable data)
        {
            //Set a name for the form
            string formID = "PostForm";
            //Build the form using the specified data to be posted.
            StringBuilder strForm = new StringBuilder();
            strForm.Append("<form id=\"" + formID + "\" name=\"" +
                           formID + "\" action=\"" + url +
                           "\" method=\"POST\">");
            foreach (System.Collections.DictionaryEntry key in data)
            {
                strForm.Append("<input type=\"hidden\" name=\"" + key.Key +
                               "\" value=\"" + key.Value + "\">");
            }
            strForm.Append("</form>");
            //Build the JavaScript which will do the Posting operation.
            StringBuilder strScript = new StringBuilder();
            strScript.Append("<script language='javascript'>");
            strScript.Append("var v" + formID + " = document." +
                             formID + ";");
            strScript.Append("v" + formID + ".submit();");
            strScript.Append("</script>");
            //Return the form and the script concatenated.
            //(The order is important, Form then JavaScript)
            return strForm.ToString() + strScript.ToString();
        }

        public string getURL()
        {
            if (env == "test")
            {
                string paymentUrl = "https://testpay.easebuzz.in";
                return paymentUrl;
            }
            else
            {
                string paymentUrl = "https://pay.easebuzz.in";
                return paymentUrl;
            }
        }
    }
}