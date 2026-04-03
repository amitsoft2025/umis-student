using com.paygate.ag.common.utils;
using com.toml.dp.util;
using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Configuration;

namespace Website.Areas.Student.Models
{
    public class SbiPayEnrollment
    {

        //Add Ease Buzz
        string privateKey = "0";

        public string env = "prod";

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


        //

        public string requestparams { get; set; }
        public string merchantId { get; set; }
        public string EncryptshippingDetais { get; set; }
        public string EncryptbillingDetails { get; set; }
        public string url { get; set; }
        string status { get; set; }
        public string MID = "1000852";//live mid of enrollment account
         //public string MID = "1000003";//local
        string saveurl = "https://www.sbiepay.sbi/secure/AggregatorHostedListener";//live
        //string saveurl = "https://test.sbiepay.sbi/secure/AggregatorHostedListener";//local
        string Success_URL = WebConfigurationManager.AppSettings["siteUrl"];
        string Failure_URL = WebConfigurationManager.AppSettings["siteUrl"];
        string Collaborator_Id = "SBIEPAY";
        string Operating_Mode = "DOM";
        string Country = "IN";
        string Currency = "INR";
        string EncodedKey = "FAzW7HG/MwuYgFwuYyOKaw==";//"ruHcfGgaFa763ONfQ4qphg==";//live  mid of enrollment account
        // string EncodedKey = "fBc5628ybRQf88f/aqDUOQ==";//local
        string Paymode = "NB";
        string Accesmedium = "ONLINE";
        string TransactionSource = "ONLINE";
        string MerchantCustomerID = "NA";
        int keysize = 128;

        public SbiPayEnrollment encriptData(decimal Amount1, string Other_Details1, string Success_URL1, string Failure_URL1)
        {

            SbiPayEnrollment sbi = new SbiPayEnrollment();
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
            //  Order_Number = "";
            Other_Details = Other_Details1 + ",Enrollment";
            fee.Other_Details = Other_Details;
            var obj = fee.FeessubEnrollmentbefore(fee);
            if (obj.Status == false)
            {
                goto loop;
            }


            //EncryptTrans = Merchant ID|Operating Mode|Merchant Country|Merchant Currency|Posting Amount|Other Details|SuccessURL | Fail URL | Aggregator ID | Merchant Order Number| Merchant Customer ID| Paymode | Access Medium | Transaction Source
            string Requestparameter = MID + "|" + Operating_Mode + "|" + Country + "|" + Currency + "|" + Amount + "|" + Other_Details + "|" + Success_URL + "|" + Failure_URL + "|" + Collaborator_Id + "|" + Order_Number + "|" + MerchantCustomerID + "|" + Paymode + "|" + Accesmedium + "|" + TransactionSource + "";
            string EncryptedParam = AES128Bit.Encrypt(Requestparameter, EncodedKey, keysize);
            string billingDtls = "";
            string shippingDtls = "";
            //Sbiepay sbi = new Sbiepay();
            sbi.url = saveurl;
            sbi.merchantId = MID;
            sbi.requestparams = EncryptedParam;
            sbi.EncryptbillingDetails = billingDtls;
            sbi.EncryptshippingDetais = shippingDtls;

            return sbi;
        }


        public SbiPayEnrollment encriptDataSafexPay( decimal Amount1, string Other_Details1, string Success_URL1, string Failure_URL1, string cmid11, string ckey11, int collegeid, int courseyearid)
        {

        
            StudentLogin stu = new StudentLogin();
            string ApplicationID = ClsLanguage.GetCookies("NBApplicationNo");
            Login lo = stu.BasicDetail(ApplicationID);
            SbiPayEnrollment sbi = new SbiPayEnrollment();
            string Amount = Amount1.ToString();
            string Order_Number = "";
            string Other_Details = Other_Details1;
            string Applicationno = Other_Details1;

            DateTime now = DateTime.Now;
           loop:
            Order_Number = CommonMethod.RandomNumber(100000, 999999) + now.ToString("MdHHmmyyssfff");
            StudentLogin stlogin = new StudentLogin();
            FeesSubmit fee = new FeesSubmit();

            //iske bad se pending h 

            //merchant_id = CommonMethod.MIDcollegewiseSafex().Where(x => x.collegeid == collegeid).FirstOrDefault().mid;
           // merchant_key = CommonMethod.MIDcollegewiseSafex().Where(x => x.collegeid == collegeid).FirstOrDefault().mkey;





            var obj = fee.FeessubEnrollmentbefore(fee);
            if (obj.Status == false)
            {
                goto loop;
            }


            //EncryptTrans = Merchant ID|Operating Mode|Merchant Country|Merchant Currency|Posting Amount|Other Details|SuccessURL | Fail URL | Aggregator ID | Merchant Order Number| Merchant Customer ID| Paymode | Access Medium | Transaction Source
            string Requestparameter = MID + "|" + Operating_Mode + "|" + Country + "|" + Currency + "|" + Amount + "|" + Other_Details + "|" + Success_URL + "|" + Failure_URL + "|" + Collaborator_Id + "|" + Order_Number + "|" + MerchantCustomerID + "|" + Paymode + "|" + Accesmedium + "|" + TransactionSource + "";
            string EncryptedParam = AES128Bit.Encrypt(Requestparameter, EncodedKey, keysize);
            string billingDtls = "";
            string shippingDtls = "";
            //Sbiepay sbi = new Sbiepay();
            sbi.url = saveurl;
            sbi.merchantId = MID;
            sbi.requestparams = EncryptedParam;
            sbi.EncryptbillingDetails = billingDtls;
            sbi.EncryptshippingDetais = shippingDtls;

            return sbi;
        }



        public SbiPayEnrollment encriptDataEaseBuzz(decimal Amount1, string Other_Details1, string Success_URL1, string Failure_URL1, string cmid11, string ckey11, int collegeid, int courseyearid)
        {

            string ckey = CommonMethod.MIDcollegewiseEaseBuzz().Where(x => x.collegeid == 1000).FirstOrDefault().mkey;
            string ESalt = CommonMethod.MIDcollegewiseEaseBuzz().Where(x => x.collegeid == 1000).FirstOrDefault().Salt;
            string Mid = CommonMethod.MIDcollegewiseEaseBuzz().Where(x => x.collegeid == 1000).FirstOrDefault().mid;

            privateKey = ckey;


            StudentLogin stu = new StudentLogin();
            string ApplicationID = ClsLanguage.GetCookies("NBApplicationNo");
            Login lo = stu.BasicDetail(ApplicationID);
            string[] hashVarsSeq;
            string hash_string = string.Empty;
            string saltvalue = ESalt;
            string amount = Amount1.ToString();
            string firstname = lo.FirstName + " " + lo.LastName;
            string email = lo.Email;
            string phone = lo.MobileNo;
            string productinfo = "Enrollment Fees";
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

            SbiPayEnrollment sbi = new SbiPayEnrollment();
            //return sbi;
            Success_URL =  Success_URL1;
            Failure_URL =  Failure_URL1;
            string Amount = Amount1.ToString();
            string Order_Number = "";
            string Other_Details = Other_Details1;
            DateTime now = DateTime.Now;
        loop:
            Order_Number = CommonMethod.RandomNumber(100000, 999999) + now.ToString("MdHHmmyyssfff");
            StudentLogin stlogin = new StudentLogin();
          
           
            //  Order_Number = "";
        
            FeesSubmit fee = new FeesSubmit();
            fee.ApplicationNo = Other_Details1;
            fee.clienttrxid = Order_Number;
            fee.GetWayType = "EaseBuzz";
            fee.amount = Amount1.ToString();
            string YearType = "";
            //  Order_Number = "";
            Other_Details = Other_Details1 + ",Enrollment";
            fee.Other_Details = Other_Details;
            fee.MID = Mid;
            var obj = fee.FeessubEnrollmentbefore(fee);

            if (obj.Status == false)
            {
                goto loop;
            }
            if (courseyearid == Convert.ToInt32(CommonSetting.CourseYearID.BA1st) || courseyearid == Convert.ToInt32(CommonSetting.CourseYearID.bba1sem) || courseyearid == Convert.ToInt32(CommonSetting.CourseYearID.bca1sem) || courseyearid == Convert.ToInt32(CommonSetting.CourseYearID.Bcom1st) || courseyearid == Convert.ToInt32(CommonSetting.CourseYearID.BEDpart1) || courseyearid == Convert.ToInt32(CommonSetting.CourseYearID.Bsc1st) || courseyearid == Convert.ToInt32(CommonSetting.CourseYearID.LLB1sem) || courseyearid == Convert.ToInt32(CommonSetting.CourseYearID.ma1sem) || courseyearid == Convert.ToInt32(CommonSetting.CourseYearID.mcom1sem) || courseyearid == Convert.ToInt32(CommonSetting.CourseYearID.msc1sem) || courseyearid == Convert.ToInt32(CommonSetting.CourseYearID.biotech1sem)|| courseyearid == Convert.ToInt32(CommonSetting.CourseYearID.bpharma1sem))
            {
                YearType = "firstyear";
                Other_Details = Other_Details + ",Enrollment";
            }
            else
            {
                YearType = "otheryear";
                Other_Details = Other_Details + ",Enrollment";
            }
            string ShowPaymentMode = "";
            // Generate transaction ID -> make sure this is unique for all transactions
            Random rnd = new Random();
            string strHash = Easebuzz_Generatehash512(rnd.ToString() + DateTime.Now);
            //txnid = strHash.ToString().Substring(0, 20);
            txnid = Order_Number;
            string paymentUrl = "https://pay.easebuzz.in";
            // Get configs from web config
            easebuzz_action_url = paymentUrl + "/pay/secure";
            // generate hash table
            System.Collections.Hashtable data = new System.Collections.Hashtable(); // adding values in gash table for data post
            data.Add("txnid", txnid);
            data.Add("key", ckey);
            //string AmountForm = Convert.ToDecimal(amount.Trim()).ToString("g29");// eliminating trailing zeros
            int StID = (ClsLanguage.GetCookies("NBStID") != null ? Convert.ToInt32(ClsLanguage.GetCookies("NBStID")) : 0);
            int Sission = (ClsLanguage.GetCookies("NBSission") != null ? Convert.ToInt32(ClsLanguage.GetCookies("NBSission")) : 0);
            data.Add("amount", amount);
            data.Add("firstname", firstname.Trim());
            data.Add("email", email.Trim());
            data.Add("phone", phone.Trim());
            data.Add("productinfo", productinfo.Trim());
            data.Add("surl", Success_URL1);
            data.Add("furl", Success_URL1);
            data.Add("udf1", Other_Details1.Trim());
            data.Add("udf2", Order_Number.Trim());
            data.Add("udf3", YearType);
            data.Add("udf4", StID);
            data.Add("udf5", Sission);

            // generate hash
            hashVarsSeq = "key|txnid|amount|productinfo|firstname|email|udf1|udf2|udf3|udf4|udf5|udf6|udf7|udf8|udf9|udf10".Split('|'); // spliting hash sequence from config
            hash_string = "";
            foreach (string hash_var in hashVarsSeq)
            {
                hash_string = hash_string + (data.ContainsKey(hash_var) ? data[hash_var].ToString() : "");
                hash_string = hash_string + '|';
            }
            hash_string += ESalt;// appending SALT
            gen_hash = Easebuzz_Generatehash512(hash_string).ToLower();        //generating hash
            data.Add("hash", gen_hash);
            data.Add("show_payment_mode", ShowPaymentMode.Trim());
            string strForm = Easebuzz_PreparePOSTForm(easebuzz_action_url, data);
            sbi.Etxnid = txnid;
            sbi.EKey = ckey;
            sbi.Eamount = amount;
            sbi.Efirstname = firstname;
            sbi.Eemail = email;
            sbi.Ephone = phone;
            sbi.Eproductinfo = productinfo;
            sbi.Esurl = Success_URL1;
            sbi.Efurl = Success_URL1;
            sbi.Eudf1 = Other_Details1;
            sbi.Eudf2 = Order_Number.Trim();
            sbi.Eudf3 = YearType;
            sbi.Eudf4 = StID.ToString();
            sbi.Eudf5 = Sission.ToString();
            sbi.Esaltvalue = ESalt.ToString();
            sbi.Ehash_string = gen_hash.ToString();
            sbi.EstrForm = strForm;
            sbi.Eeasebuzz_action_url = easebuzz_action_url.ToString();
            return sbi;
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


        public string encrypt(string plainText,string key)
        {
            string va = string.Empty;
            privateKey = key;


            string encryptText = PayGateCryptoUtils.encrypt(plainText, privateKey);
            // string encryptText = encrypt(plainText, privateKey);
            return encryptText;
        }
        public string decrypt(string encryptText, string key)
        {
              privateKey= key;
            string va = string.Empty;
            //string decryptText = PayGateCryptoUtils.decrypt(encryptText, privateKey);
            string decryptText = PayGateCryptoUtils.decrypt(encryptText, privateKey);
            return decryptText;
        }

        public string ComputeSha256Hash(string rawData)
        {
            // Create a SHA256   
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // ComputeHash - returns byte array  
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

                // Convert byte array to a string   
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }


        public class MyCryptoClass
        {
            // string privateKey = System.Configuration.ConfigurationSettings.AppSettings["merchant_key"];// "89diCMlKzp+GWwwBm5aVDv6sD+7wJj9ewrMjC6MsHmc="; // this Is Incription Key
            public string encrypt(string plainText, string privateKey)
            {
                string va = string.Empty;

                string encryptText = PayGateCryptoUtils.encrypt(plainText, privateKey);
                // string encryptText = encrypt(plainText, privateKey);
                return encryptText;
            }
            public string decrypt(string encryptText, string privateKey)
            {
                string va = string.Empty;
                //string decryptText = PayGateCryptoUtils.decrypt(encryptText, privateKey);
                string decryptText = PayGateCryptoUtils.decrypt(encryptText, privateKey);
                return decryptText;
            }

            public string ComputeSha256Hash(string rawData)
            {
                // Create a SHA256   
                using (SHA256 sha256Hash = SHA256.Create())
                {
                    // ComputeHash - returns byte array  
                    byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

                    // Convert byte array to a string   
                    StringBuilder builder = new StringBuilder();
                    for (int i = 0; i < bytes.Length; i++)
                    {
                        builder.Append(bytes[i].ToString("x2"));
                    }
                    return builder.ToString();
                }
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






        public string pgsucessdecrypt(int sid)
        {
            try
            {

                string encData = HttpContext.Current.Request.Form["encData"];
                CommonMethod.WritetoNotePaymentgateway(encData, "", HttpContext.Current.Request.Url.AbsolutePath, " pgsucessdecrypt url hit payment gatway for Enrollment Fee", ClsLanguage.GetCookies("NBApplicationNo"));
                string encDatadecryptdata = AES128Bit.Decrypt(encData, EncodedKey, keysize);
                var aa = encDatadecryptdata.Split('|');
                CommonMethod.WritetoNotePaymentgateway(encData, encDatadecryptdata, HttpContext.Current.Request.Url.AbsolutePath, " pgsucessdecrypt url hit payment gatway for Enrollment Fee", ClsLanguage.GetCookies("NBApplicationNo"));
                FeesSubmit stlogin = new FeesSubmit();
                stlogin.Requestdata = encData;
                stlogin.dRequestdata = encDatadecryptdata;
                stlogin.PGstatus = aa[2];
                var gstcomm = aa[14].Split('^');
                string gst = "";
                string commission = "";
                if (gstcomm.Length > 1)
                {
                    commission = gstcomm[0];
                    gst = gstcomm[1];
                }
                stlogin.banktrxid = aa[9];
                stlogin.clienttrxid = aa[0];
                stlogin.amount = aa[3];
                stlogin.feeamount = "";
                stlogin.gst = gst;
                stlogin.commission = commission;
                stlogin.paymode = aa[5];
                stlogin.banktxndate = aa[10];
                stlogin.Reason = aa[7];
                stlogin.apitxnid = aa[1];
                var other_detail = aa[6].Split(',');
                stlogin.ApplicationNo = other_detail[0];

                var obj = stlogin.FeessubEnrollment(stlogin);

                //if(obj.Status== true)
                //{
                //    EnrollmentRequest enroll = new EnrollmentRequest();
                //    var result = enroll.ApproveEnrollmentRequest(aa[6]);

                //}

                if (obj.Id == 0)
                {
                    paymentgatewayemailEnrollment(aa[6]);
                }
                return encDatadecryptdata;
            }
            catch (Exception ex)
            {
                CommonMethod.WritetoNotePaymentgateway(ex.Message + "      " + ex.StackTrace, "", HttpContext.Current.Request.Url.AbsolutePath, "pgsucessdecrypt() url hit error on payment PaymentGateway  for Enrollment Fee", ClsLanguage.GetCookies("NBApplicationNo"));
                return "error";
            }
        }


        public string pgsucessdecryptEaseBuzz(int sid, string banktrxid, string clienttrxid, string amount1, string feeamount, string gst, string commission, string paymode, string banktxndate, string Reason, string apitxnid, string ApplicationNo, string courseyearid, string AdmissionType, string encData, string encDatadecryptdata, string PGstatus, string Sessionid)
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

                var obj = stlogin.FeessubEnrollment(stlogin);
                //if(obj.Status== true)
                //{
                //    EnrollmentRequest enroll = new EnrollmentRequest();
                //    var result = enroll.ApproveEnrollmentRequest(aa[6]);

                //}

                if (obj.Id == 0)
                {
                    paymentgatewayemailEnrollment(ApplicationNo);
                }
                return encDatadecryptdata;
            }
            catch (Exception ex)
            {
                CommonMethod.WritetoNotePaymentgateway(ex.Message + "      " + ex.StackTrace, "", HttpContext.Current.Request.Url.AbsolutePath, "pgsucessdecrypt() url hit error on payment PaymentGateway  for Enrollment Fee", ClsLanguage.GetCookies("NBApplicationNo"));
                return "error";
            }
        }


        public string pgfaileddecrypt(int sid)
        {
            try
            {

                string encData = HttpContext.Current.Request.Form["encData"];
                CommonMethod.WritetoNotePaymentgateway(encData, "", HttpContext.Current.Request.Url.AbsolutePath, " pgsucessdecrypt url hit payment gatway for Enrollment Fee", ClsLanguage.GetCookies("NBApplicationNo"));

                string encDatadecryptdata = AES128Bit.Decrypt(encData, EncodedKey, keysize);
                //string encDatadecryptdata = "CH8809800|4430840943731|SUCCESS|100|INR|IMPS|ABC^DEF^ERD|Transaction Successful|NA|G1312423|2018-06-24  16:30:24 | IN | 10000032018062412345 | 1000003 | 10.00 ^ 1.80 ||||||||||";

                var aa = encDatadecryptdata.Split('|');
                CommonMethod.WritetoNotePaymentgateway(encData, encDatadecryptdata, HttpContext.Current.Request.Url.AbsolutePath, " url hit payment PaymentGateway gatway for Enrollment Fee", ClsLanguage.GetCookies("NBApplicationNo"));
                FeesSubmit stlogin = new FeesSubmit();
                stlogin.Requestdata = encData;
                stlogin.dRequestdata = encDatadecryptdata;
                stlogin.PGstatus =aa[2];// "success";//
                stlogin.banktrxid = aa[9];
                stlogin.clienttrxid = aa[0];
                stlogin.amount = aa[3];
                stlogin.feeamount = "";
                stlogin.gst = "";
                stlogin.commission = "";
                stlogin.paymode = aa[5];
                stlogin.banktxndate = aa[10];
                stlogin.Reason = aa[7];
                stlogin.apitxnid = aa[1];
                var other_detail = aa[6].Split(',');
                stlogin.ApplicationNo = other_detail[0];

                var obj = stlogin.FeessubEnrollment(stlogin);
                if (obj.Id == 0)
                {
                    paymentgatewayemailEnrollment(aa[6]);
                }
                return encDatadecryptdata;
            }
            catch (Exception ex)
            {
                CommonMethod.WritetoNotePaymentgateway(ex.Message + "             " + ex.StackTrace, "", HttpContext.Current.Request.Url.AbsolutePath, "pgfaileddecrypt() url hit error on payment PaymentGateway", ClsLanguage.GetCookies("NBApplicationNo"));
                return "error";
            }
        }
        public string Pushresponsedecrypt(string pushRespData)
        {
            try
            {
                string encData = pushRespData;
                string encDatadecryptdata = AES128Bit.Decrypt(encData, EncodedKey, keysize);
                var aa = encDatadecryptdata.Split('|');
                FeesSubmit stlogin = new FeesSubmit();
                var gstcomm = aa[14].Split('^');
                string gst = "";
                string commission = "";
                if (gstcomm.Length > 1)
                {
                    commission = gstcomm[0];
                    gst = gstcomm[1];
                }
                stlogin.Requestdata = encData;
                stlogin.dRequestdata = encDatadecryptdata;
                stlogin.PGstatus = aa[2];
                stlogin.banktrxid = aa[9];
                stlogin.clienttrxid = aa[0];
                stlogin.amount = aa[3];
                stlogin.feeamount = "";
                stlogin.gst = gst;
                stlogin.commission = commission;
                stlogin.paymode = aa[5];
                stlogin.banktxndate = aa[10];
                stlogin.Reason = aa[7];
                stlogin.apitxnid = aa[1];
                var other_detail = aa[6].Split(',');//application no, enrollment
                stlogin.ApplicationNo = other_detail[0];
                var obj = stlogin.FeessubEnrollmentPushresponse(stlogin);
                if (obj.Status == true)
                {
                    EnrollmentRequest enroll = new EnrollmentRequest();
                    var result = enroll.ApproveEnrollmentRequest(aa[6]);

                }
                CommonMethod.WritetoNotePaymentgateway(encData, encDatadecryptdata, HttpContext.Current.Request.Url.AbsolutePath, " Paymentgateway Pushresponse  url hit payment gatway  for Enrollment Fee");
                if (obj.Id == 0)
                {
                    paymentgatewayemailEnrollment(aa[6]);
                }

                // Message Structure of ‘Push Response’
                //         Merchant Order Number | SBIePayRefID / ATRN | Transaction Status | Amount | Currency | Pay Mode | OtherDetails | Reason / Message | Bank Code | Bank Reference Number| Transaction Date | Country | CIN | Merchant ID | Total Fee GST| Ref1 | Ref2 | Ref3 | Ref4 | Ref5 | Ref6 | Ref7 | Ref8 | Ref9
                return encDatadecryptdata;
            }
            catch (Exception ex)
            {
                CommonMethod.WritetoNotePaymentgateway(ex.Message + "      " + ex.StackTrace, "", HttpContext.Current.Request.Url.AbsolutePath, "Paymentgateway Pushresponse url hit error on payment PaymentGateway for Enrollment Fee");
                return "error";
            }
        }
        public void paymentgatewayemailEnrollment(string applicationno)
        {
            return;
            ExamForm ob = new ExamForm();
            var obj1 = ob.GetAppLicationDataForEnrollmentFee();
            Email.SendEmailForSt_EnrollmentPaymentgateway(obj1.objExamFrom.Email, obj1.objPrintRecipt.status, obj1.objExamFrom.StudentName, obj1.objPrintRecipt.trxdate, obj1.objPrintRecipt.banktrxid, obj1.objPrintRecipt.TransactionId, obj1.objPrintRecipt.ApplicationNo, obj1.objfeesubmit.Fees, obj1.objPrintRecipt.PaymentType, obj1.objExamFrom.MobileNo, obj1.objExamFrom.RegistrationNo);
        }
        public string doubleverificationregistration(string data)
        {
            try
            {
                //string ckey = CommonMethod.MIDcollegewise().Where(x=> x.collegeid==1).FirstOrDefault().mkey;
                //string encData = HttpContext.Current.Request.Form["pushRespData"];
                //string encDatadecryptdata = AES128Bit.Decrypt(encData, ckey, keysize);
                var aa = (data).Split('|');
                FeesSubmit stlogin = new FeesSubmit();
                var gstcomm = aa[15].Split('^');
                string gst = "";
                string commission = "";
                if (gstcomm.Length > 1)
                {
                    commission = gstcomm[0];
                    gst = gstcomm[1];
                }
                stlogin.Requestdata = "";
                stlogin.dRequestdata = "";
                stlogin.PGstatus = aa[2];
                stlogin.banktrxid = aa[10];
                stlogin.clienttrxid = aa[6];
                stlogin.amount = (aa[7]);
                stlogin.feeamount = "";
                stlogin.gst = gst;
                stlogin.commission = commission;
                stlogin.paymode = aa[12];
                stlogin.banktxndate = aa[11];
                stlogin.Reason = aa[8];
                stlogin.apitxnid = aa[1];
                var other_detail = aa[5].Split(',');
                stlogin.ApplicationNo = other_detail[0];
                //stlogin.ApplicationNo = aa[5];
                var obj = stlogin.FeessubStudentPushresponseDoubleverificationEnroll(stlogin);
                //CommonMethod.WritetoNotePaymentgateway("Double Verification Enroll", "Double Verification enroll", HttpContext.Current.Request.Url.AbsolutePath, "admission Paymentgateway Pushresponse  url hit payment gatway");

                // Message Structure of ‘Push Response’
                //         Merchant Order Number | SBIePayRefID / ATRN | Transaction Status | Amount | Currency | Pay Mode | OtherDetails | Reason / Message | Bank Code | Bank Reference Number| Transaction Date | Country | CIN | Merchant ID | Total Fee GST| Ref1 | Ref2 | Ref3 | Ref4 | Ref5 | Ref6 | Ref7 | Ref8 | Ref9
                return "";
            }
            catch (Exception ex)
            {
                CommonMethod.WritetoNotePaymentgateway(ex.Message + " Double Verification Enroll     " + ex.StackTrace, "", HttpContext.Current.Request.Url.AbsolutePath, "admission Paymentgateway Pushresponse url hit error on payment PaymentGateway");
                return "error";
            }
        }
    }
}