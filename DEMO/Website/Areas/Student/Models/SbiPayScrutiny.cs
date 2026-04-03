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
    public class SbiPayScrutiny

    {
        public string now { get; set; } // As String
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
     
        public string Safex_me_id { get; set; }
        public string Safex_merchant_request { get; set; }
        public string Safex_hash { get; set; }
        public string Safex_POSTURL { get; set; }
        

        //data.Add("me_id", txnmerchantId);
        //data.Add("merchant_request", enc_request);
        //data.Add("hash", enc_hash);
        string username = WebConfigurationManager.AppSettings["username"];
        string password = WebConfigurationManager.AppSettings["password"];
        string secretKey = WebConfigurationManager.AppSettings["secret"];
        string AirpayMid = WebConfigurationManager.AppSettings["mercid"];
        string AirPay_Success_url = WebConfigurationManager.AppSettings["AirPayURL"];
        //Easy Buzz
        public string salt = System.Configuration.ConfigurationSettings.AppSettings["salt"];
        public string Key = System.Configuration.ConfigurationSettings.AppSettings["key"];
        public string env = System.Configuration.ConfigurationSettings.AppSettings["env"];
        public string EasySuccessUrl = System.Configuration.ConfigurationSettings.AppSettings["EasySuccessUrl"];
        public string EasyFailUrl = System.Configuration.ConfigurationSettings.AppSettings["EasyFailUrl"];
        public string merchant_id = System.Configuration.ConfigurationSettings.AppSettings["merchant_id"];
        public string ag_id = System.Configuration.ConfigurationSettings.AppSettings["ag_id"];
        public string merchant_key = System.Configuration.ConfigurationSettings.AppSettings["merchant_key"];
        public string txn_type = System.Configuration.ConfigurationSettings.AppSettings["txn_type"];
        public string POSTURL = System.Configuration.ConfigurationSettings.AppSettings["POSTURL"];
        public string SafexPaySuccessUrl = System.Configuration.ConfigurationSettings.AppSettings["SafexPaySuccessUrl"];
        public string SafexPayFailUrl = System.Configuration.ConfigurationSettings.AppSettings["SafexPayFailUrl"];
        //public string env = "prod";
        //public string senv = "test";
        // Easy Buzz End
        public string requestparams { get; set; }
        public string merchantId { get; set; }
        public string EncryptshippingDetais { get; set; }
        public string EncryptbillingDetails { get; set; }
        public string url { get; set; }
        string status { get; set; }
        public string MID = "1000858";//live mid of Scrutinyment account
                                      //public string MID = "1000003";//local
        string saveurl = "https://www.sbiepay.sbi/secure/AggregatorHostedListener";//live
                                                                                   //string saveurl = "https://test.sbiepay.sbi/secure/AggregatorHostedListener";//local
        string Success_URL = WebConfigurationManager.AppSettings["siteUrl"];
        string Failure_URL = WebConfigurationManager.AppSettings["siteUrl"];
        string Collaborator_Id = "SBIEPAY";
        string Operating_Mode = "DOM";
        string Country = "IN";
        string Currency = "INR";
        string EncodedKey = "WLHmLk/jyu82aBE4mP20jg==";//"JrnO/NYyZoY0/G8Pn1IMcg==";//live  mid of Scrutinyment account
                                                       //string EncodedKey = "fBc5628ybRQf88f/aqDUOQ==";//local
        string Paymode = "NB";
        string Accesmedium = "ONLINE";
        string TransactionSource = "ONLINE";
        string MerchantCustomerID = "NA";
        int keysize = 128;
        public SbiPayScrutiny encriptDataScrutiny(decimal Amount1, string Other_Details1, string Success_URL1, string Failure_URL1, string year, decimal latefee, decimal amount_without_latefee, int courseyearid)
        {

            SbiPayScrutiny sbi = new SbiPayScrutiny();
            //return sbi;
            Success_URL = Success_URL + Success_URL1;
            Failure_URL = Failure_URL + Failure_URL1;
            string Amount = Amount1.ToString();
            string Order_Number = "";
            string Other_Details = Other_Details1;
            DateTime now = DateTime.Now;
            loop:
            Order_Number = CommonMethod.RandomNumber(100000, 999999) + now.ToString("MdHHmmyyssfff");
           ScrutinyFeesSubmit fee = new ScrutinyFeesSubmit();
            fee.ApplicationNo = Other_Details1;
            fee.clienttrxid = Order_Number;
            fee.late_fee = latefee;
            fee.fee_without_late_fee = amount_without_latefee;
            //Other_Details = Other_Details1 + ",Scrutinypaper-" + year;
            Other_Details = Other_Details1 + ",MainYear," + courseyearid + ",Scrutinypaper-" + year;
            fee.mid = MID;
            fee.Other_Details = Other_Details;
            try
            {
                var obj = fee.FeessubScrutinymentbefore(fee);
                if (obj.Status == false)
                {
                    goto loop;
                }
            }
            catch (Exception ex)
            {
                goto loop;
            }
            //EncryptTrans = Merchant ID|Operating Mode|Merchant Country|Merchant Currency|Posting Amount|Other Details|SuccessURL | Fail URL | Aggregator ID | Merchant Order Number| Merchant Customer ID| Paymode | Access Medium | Transaction Source
            string Requestparameter = MID + "|" + Operating_Mode + "|" + Country + "|" + Currency + "|" + Amount + "|" + Other_Details + "|" + Success_URL + "|" + Failure_URL + "|" + Collaborator_Id + "|" + Order_Number + "|" + MerchantCustomerID + "|" + Paymode + "|" + Accesmedium + "|" + TransactionSource + "";
            string EncryptedParam = AES128Bit.Encrypt(Requestparameter, EncodedKey, keysize);
            string billingDtls = "";
            string shippingDtls = "";
            sbi.url = saveurl;
            sbi.merchantId = MID;
            sbi.requestparams = EncryptedParam;
            sbi.EncryptbillingDetails = billingDtls;
            sbi.EncryptshippingDetais = shippingDtls;
            return sbi;
        }

        public SbiPayScrutiny encriptDataScrutinyAirPay(decimal Amount1, string Other_Details1, string Success_URL1, string Failure_URL1, string year, decimal latefee, decimal amount_without_latefee, int courseyearid, string ssEmail, string ssPhone, string ssFName, string ssLName, string ssAddress, string ssCity, string ssState, string ssCountry, string ssPincode, string ssAmount, string ssOrderId)
        {

            string sEmail = ssEmail;
            string sPhone = ssPhone;
            string sFName = ssFName;
            string sLName = ssLName;
            string sAddress = ssAddress;
            string sCity = ssCity;
            string sState = ssState;
            string sCountry = ssCountry;
            string sPincode = ssPincode;
            string sAmount = ssAmount;
            string sOrderId = ssOrderId;

            // server side validation
            //validatepost(sEmail, sPhone, sFName, sLName, sAddress, sCity, sState, sCountry, sPincode, sAmount, sOrderId);
            MID = "256750";

            //string saveurl = "https://www.sbiepay.sbi/secure/AggregatorHostedListener";//live
            string saveurl = "https://intschoolpay.nowpay.co.in/pay/index.php";//local

            SbiPayScrutiny sbi = new SbiPayScrutiny();
            //return sbi;
            Success_URL = Success_URL + Success_URL1;
            Failure_URL = Failure_URL + Failure_URL1;
            string Amount = Amount1.ToString();
            string Order_Number = "";
            string Other_Details = Other_Details1;
            DateTime now = DateTime.Now;
            loop:
            Order_Number = CommonMethod.RandomNumber(100000, 999999) + now.ToString("MdHHmmyyssfff");
           ScrutinyFeesSubmit fee = new ScrutinyFeesSubmit();
            fee.ApplicationNo = Other_Details1;
            fee.clienttrxid = Order_Number;
            fee.late_fee = latefee;
            fee.fee_without_late_fee = amount_without_latefee;
            //Other_Details = Other_Details1 + ",Scrutinypaper-" + year;

            int StID = (ClsLanguage.GetCookies("NBStID") != null ? Convert.ToInt32(ClsLanguage.GetCookies("NBStID")) : 0);
            int Sission =(ClsLanguage.GetCookies("NBSission") != null ? Convert.ToInt32(ClsLanguage.GetCookies("NBSission")) : 0);

            Other_Details = Other_Details1 + ",MainYear," + courseyearid + ",Scrutinypaper-" + year + "," + Order_Number + "," + StID + "," + Order_Number;
            fee.mid = MID;
            fee.Other_Details = Other_Details;
            try
            {
                var obj = fee.FeessubScrutinymentbeforeAirPay(fee);
                if (obj.Status == false)
                {
                    goto loop;
                }
            }
            catch (Exception ex)
            {
                goto loop;
            }

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
            //sbi.customvar = Other_Details;
            //EncryptTrans = Merchant ID|Operating Mode|Merchant Country|Merchant Currency|Posting Amount|Other Details|SuccessURL | Fail URL | Aggregator ID | Merchant Order Number| Merchant Customer ID| Paymode | Access Medium | Transaction Source
            //string Requestparameter = MID + "|" + Operating_Mode + "|" + Country + "|" + Currency + "|" + Amount + "|" + Other_Details + "|" + Success_URL + "|" + Failure_URL + "|" + Collaborator_Id + "|" + Order_Number + "|" + MerchantCustomerID + "|" + Paymode + "|" + Accesmedium + "|" + TransactionSource + "";
            //string EncryptedParam = AES128Bit.Encrypt(Requestparameter, EncodedKey, keysize);
            //string billingDtls = "";
            //string shippingDtls = "";
            //sbi.url = saveurl;
            //sbi.merchantId = MID;
            //sbi.requestparams = EncryptedParam;
            //sbi.EncryptbillingDetails = billingDtls;
            //sbi.EncryptshippingDetais = shippingDtls;
            return sbi;
        }

        public SbiPayScrutiny encriptDataScrutinySafexPay(decimal Amount1, string Other_Details1, string Success_URL1, string Failure_URL1, string year, decimal latefee, decimal amount_without_latefee, int courseyearid, string ssEmail, string ssPhone, string ssFName, string ssLName, string ssAddress, string ssCity, string ssState, string ssCountry, string ssPincode, string ssAmount, string ssOrderId)
        {

            SbiPayScrutiny sbi = new SbiPayScrutiny();
  
            string Order_Number = "";
            string Other_Details = Other_Details1;
            DateTime now = DateTime.Now;
            loop:
            Order_Number = CommonMethod.RandomNumber(100000, 999999) + now.ToString("MdHHmmyyssfff");
           ScrutinyFeesSubmit fee = new ScrutinyFeesSubmit();
            fee.ApplicationNo = Other_Details1;
            fee.clienttrxid = Order_Number;
            fee.late_fee = latefee;
            fee.fee_without_late_fee = amount_without_latefee;
            //Other_Details = Other_Details1 + ",Scrutinypaper-" + year;
            CommonMethod.TraceLogWritetoNotepad("obj1.objScrutinyFrom3", "Scrutiny/sbipayScrutiny2", "sbipayScrutiny2", "Id");
            int StID = (ClsLanguage.GetCookies("NBStID") != null ? Convert.ToInt32(ClsLanguage.GetCookies("NBStID")) : 0);
            int Sission = (ClsLanguage.GetCookies("NBSission") != null ? Convert.ToInt32(ClsLanguage.GetCookies("NBSission")) : 0);
            
            Other_Details = Other_Details1 + ",MainYear," + courseyearid + ",Scrutinypaper-" + year + "," + Order_Number + "," + StID + "," + Order_Number;
            fee.mid = MID;
            fee.Other_Details = Other_Details;
            try
            {
                var obj = fee.FeessubScrutinymentbeforeSafexPay(fee);  /*TransactionSource save in table */
                if (obj.Status == false)
                {
                    CommonMethod.TraceLogWritetoNotepad("obj1.objScrutinyFrom3", "Scrutiny/sbipayScrutiny3", "feesloop", "Id");
                    goto loop;
                }
            }
           
            catch (Exception ex)
            {
                CommonMethod.TraceLogWritetoNotepad("obj1.objScrutinyFrom3", "Scrutiny/sbipayScrutiny1", "exaption loop", "Id");
                goto loop;
            }
            CommonMethod.TraceLogWritetoNotepad("obj1.objScrutinyFrom3", "Scrutiny/sbipayScrutiny1", "beforevariable", "Id");
            //safeXperamitter
            string txnagId = ag_id;
            string txnmerchantId = merchant_id;
            string txnmerchantKey = merchant_key;

            string txnorderNumber = Order_Number;
            string txnAmount = Amount1.ToString(); ;
            string txnCountry = "IND";
            string txnCountryCurrency = "INR";
            string txnType = txn_type;
            string txnsuccessUrl = SafexPaySuccessUrl;
            string txnfailureUrl = SafexPayFailUrl;
            string txnChannel = "WEB";

            string pgId = string.Empty;
            string pgPayMode = string.Empty;
            string pgscheme = string.Empty;
            string pgEmiMonths = string.Empty;

            string ccCardNo = string.Empty;
            string ccExpMonth = string.Empty;
            string ccexpYear = string.Empty;
            string ccCardName = string.Empty;
            string ccCvv2 = string.Empty;

            string custName = ssFName + "" + ssLName;
            string custEmailId = ssEmail;
            string custMobileNo = ssPhone;
            string custUniqueId = string.Empty;
            string custisLoggedIn = string.Empty;

            string billAddress = string.Empty;
            string billCity = string.Empty;
            string billState = string.Empty;
            string billCountry = "IND";
            string billZip = string.Empty;

            string shipAddress = string.Empty;
            string shipCity = string.Empty;
            string shipState = string.Empty;
            string shipCountry = string.Empty;
            string shipZip = string.Empty;
            string shipDays = string.Empty;
            string shipAddressCount = string.Empty;

            string itemCount = string.Empty;
            string itemValue = string.Empty;
            string itemCategory = string.Empty;

            string UPIdetails = string.Empty;
            //Console.WriteLine(UPIdetails);
            string udf1 = Other_Details1;
            string udf2 = "MainYear";
            string udf3 = courseyearid.ToString();
            string udf4 = StID.ToString(); ;
            string udf5 = Order_Number;

            string txn_details = txnagId + "|" + txnmerchantId + "|" + txnorderNumber + "|" + txnAmount + "|" + txnCountry + "|" + txnCountryCurrency + "|" + txnType + "|" + txnsuccessUrl + "|" + txnfailureUrl + "|" + txnChannel;
            string pg_details = pgId + "|" + pgPayMode + "|" + pgscheme + "|" + pgEmiMonths;
            string card_details = ccCardNo + "|" + ccExpMonth + "|" + ccexpYear + "|" + ccCvv2 + "|" + ccCardName;
            string cust_details = custName + "|" + custEmailId + "|" + custMobileNo + "|" + custUniqueId + "|" + custisLoggedIn;
            string bill_details = billAddress + "|" + billCity + "|" + billState + "|" + billCountry + "|" + billZip;
            string ship_details = shipAddress + "|" + shipCity + "|" + shipState + "|" + shipCountry + "|" + shipZip + "|" + shipDays + "|" + shipAddressCount;
            string item_details = itemCount + "|" + itemValue + "|" + itemCategory;
            string upi_details = UPIdetails;
            string other_details = udf1 + "|" + udf2 + "|" + udf3 + "|" + udf4 + "|" + udf5;

            string request = txn_details + "~" + pg_details + "~" + card_details + "~" + cust_details + "~" + bill_details + "~" + ship_details + "~" + item_details + "~" + upi_details + "~" + other_details;

            string Hash = txnmerchantId + "~" + txnorderNumber + "~" + txnAmount + "~" + txnCountry + "~" + txnCountryCurrency;

            MyCryptoClass1 aes = new MyCryptoClass1();
            string enc_request = aes.encrypt(request);
            string hashing = aes.ComputeSha256Hash(Hash);
            string enc_hash = aes.encrypt(hashing);
            CommonMethod.TraceLogWritetoNotepad("obj1.objScrutinyFrom3", "Scrutiny/sbipayScrutiny1", "beforevariableset sbi", "Id");
            sbi.Safex_me_id = txnmerchantId; // As String
            sbi.Safex_merchant_request = enc_request;
            sbi.Safex_hash = enc_hash;
            sbi.Safex_POSTURL = POSTURL;
            return sbi;
        }


        public SbiPayScrutiny encriptDataScrutinyEaseBuzz(decimal Amount1, string Other_Details1, string Success_URL1, string Failure_URL1, string year, decimal latefee, decimal amount_without_latefee, int courseyearid, string ssEmail, string ssPhone, string ssFName, string ssLName, string ssAddress, string ssCity, string ssState, string ssCountry, string ssPincode, string ssAmount, string ssOrderId)
        {

            string[] hashVarsSeq;
            string hash_string = string.Empty;
            string saltvalue = salt;
            string amount = Amount1.ToString();
            string firstname = ssFName+" "+ssLName;
            string email = ssEmail;
            string phone = ssPhone;
            string productinfo = "Scrutiny Form";
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
            //string surl = Surl;
            //string furl = Furl;

            SbiPayScrutiny sbi = new SbiPayScrutiny();
            //return sbi;
            Success_URL = EasySuccessUrl;
            Failure_URL = EasyFailUrl;
            string Amount = Amount1.ToString();
            string Order_Number = "";
            string Other_Details = Other_Details1;
            DateTime now = DateTime.Now;
            loop:
            Order_Number = CommonMethod.RandomNumber(100000, 999999) + now.ToString("MdHHmmyyssfff");
           ScrutinyFeesSubmit fee = new ScrutinyFeesSubmit();
            fee.ApplicationNo = Other_Details1;
            fee.clienttrxid = Order_Number;
            fee.late_fee = latefee;
            fee.fee_without_late_fee = amount_without_latefee;
            //Other_Details = Other_Details1 + ",Scrutinypaper-" + year;

            int StID = (ClsLanguage.GetCookies("NBStID") != null ? Convert.ToInt32(ClsLanguage.GetCookies("NBStID")) : 0);
            int Sission = (ClsLanguage.GetCookies("NBSission") != null ? Convert.ToInt32(ClsLanguage.GetCookies("NBSission")) : 0);

            Other_Details = Other_Details1 + ",MainYear," + courseyearid + ",Scrutinypaper-" + year + "," + Order_Number + "," + StID + "," + Order_Number;
            fee.mid = MID;
            fee.Other_Details = Other_Details;
            try
            {
                var obj = fee.FeessubScrutinymentbeforeEaseBuzz(fee);
                if (obj.Status == false)
                {
                    goto loop;
                }
            }
            catch (Exception ex)
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
            easebuzz_action_url =  paymentUrl   + "/pay/secure";
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
            data.Add("surl", Success_URL.Trim());
            data.Add("furl", Failure_URL.Trim());
            data.Add("udf1", Other_Details1.Trim());
            data.Add("udf2", "MainYear");
            data.Add("udf3", courseyearid);
            data.Add("udf4", Order_Number.Trim());
            data.Add("udf5", StID);
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
            sbi.Efirstname = firstname ;
            sbi.Eemail = email;
            sbi.Ephone = phone;
            sbi.Eproductinfo = productinfo;
            sbi.Eeasebuzz_action_url = saveurl;
            sbi.Esurl = Success_URL;
            sbi.Efurl = Failure_URL;
            sbi.Eudf1 = Other_Details1;
            sbi.Eudf2 = "MainYear";
            sbi.Eudf3 = courseyearid.ToString();
            sbi.Eudf4 = Order_Number;
            sbi.Eudf5 = StID.ToString();
            sbi.Esaltvalue = salt.ToString();
            sbi.Ehash_string = gen_hash.ToString();
            sbi.EstrForm = strForm;
            sbi.Eeasebuzz_action_url = easebuzz_action_url.ToString(); 
            return sbi;
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

        public SbiPayScrutiny encriptDataScrutinybackyear(decimal Amount1, string Other_Details1, string Success_URL1, string Failure_URL1, string year, decimal latefee, decimal amount_without_latefee, int courseyearid)
        {

            SbiPayScrutiny sbi = new SbiPayScrutiny();
            //return sbi;
            Success_URL = Success_URL + Success_URL1;
            Failure_URL = Failure_URL + Failure_URL1;
            string Amount = Amount1.ToString();
            string Order_Number = "";
            string Other_Details = Other_Details1;
            DateTime now = DateTime.Now;
            loop:
            Order_Number = CommonMethod.RandomNumber(100000, 999999) + now.ToString("MdHHmmyyssfff");
           ScrutinyFeesSubmit fee = new ScrutinyFeesSubmit();
            fee.ApplicationNo = Other_Details1;
            fee.clienttrxid = Order_Number;
            fee.late_fee = latefee;
            fee.fee_without_late_fee = amount_without_latefee;
            Other_Details = Other_Details1 + ",Backyear," + courseyearid + ",Scrutinypaper-" + year;
            fee.mid = MID;
            fee.Other_Details = Other_Details;
            fee.courseyearid = courseyearid;
            try
            {
                var obj = fee.FeessubScrutinymentbeforebackyear(fee);
                if (obj.Status == false)
                {
                    goto loop;
                }
            }
            catch (Exception ex)
            {
                goto loop;
            }
            //EncryptTrans = Merchant ID|Operating Mode|Merchant Country|Merchant Currency|Posting Amount|Other Details|SuccessURL | Fail URL | Aggregator ID | Merchant Order Number| Merchant Customer ID| Paymode | Access Medium | Transaction Source
            string Requestparameter = MID + "|" + Operating_Mode + "|" + Country + "|" + Currency + "|" + Amount + "|" + Other_Details + "|" + Success_URL + "|" + Failure_URL + "|" + Collaborator_Id + "|" + Order_Number + "|" + MerchantCustomerID + "|" + Paymode + "|" + Accesmedium + "|" + TransactionSource + "";
            string EncryptedParam = AES128Bit.Encrypt(Requestparameter, EncodedKey, keysize);
            string billingDtls = "";
            string shippingDtls = "";
            sbi.url = saveurl;
            sbi.merchantId = MID;
            sbi.requestparams = EncryptedParam;
            sbi.EncryptbillingDetails = billingDtls;
            sbi.EncryptshippingDetais = shippingDtls;
            return sbi;
        }
        public SbiPayScrutiny encriptDataScrutinyPrac(decimal Amount1, string Other_Details1, string Success_URL1, string Failure_URL1, string year, decimal latefee, decimal amount_without_latefee, int courseyearid)
        {

            SbiPayScrutiny sbi = new SbiPayScrutiny();
            //return sbi;
            Success_URL = Success_URL + Success_URL1;
            Failure_URL = Failure_URL + Failure_URL1;
            string Amount = Amount1.ToString();
            string Order_Number = "";
            string Other_Details = Other_Details1;
            DateTime now = DateTime.Now;
            loop:
            Order_Number = CommonMethod.RandomNumber(100000, 999999) + now.ToString("MdHHmmyyssfff");
           ScrutinyFeesSubmit fee = new ScrutinyFeesSubmit();
            fee.ApplicationNo = Other_Details1;
            fee.clienttrxid = Order_Number;
            fee.late_fee = latefee;
            fee.fee_without_late_fee = amount_without_latefee;
            //Other_Details = Other_Details1 + ",Scrutinypaper-" + year;
            Other_Details = Other_Details1 + ",PracMainYear," + courseyearid + ",Scrutinypaper-" + year;
            fee.mid = MID;
            fee.Other_Details = Other_Details;
            try
            {
                var obj = fee.FeessubScrutinymentbeforePract(fee);
                if (obj.Status == false)
                {
                    goto loop;
                }
            }
            catch (Exception ex)
            {
                goto loop;
            }
            //EncryptTrans = Merchant ID|Operating Mode|Merchant Country|Merchant Currency|Posting Amount|Other Details|SuccessURL | Fail URL | Aggregator ID | Merchant Order Number| Merchant Customer ID| Paymode | Access Medium | Transaction Source
            string Requestparameter = MID + "|" + Operating_Mode + "|" + Country + "|" + Currency + "|" + Amount + "|" + Other_Details + "|" + Success_URL + "|" + Failure_URL + "|" + Collaborator_Id + "|" + Order_Number + "|" + MerchantCustomerID + "|" + Paymode + "|" + Accesmedium + "|" + TransactionSource + "";
            string EncryptedParam = AES128Bit.Encrypt(Requestparameter, EncodedKey, keysize);
            string billingDtls = "";
            string shippingDtls = "";
            sbi.url = saveurl;
            sbi.merchantId = MID;
            sbi.requestparams = EncryptedParam;
            sbi.EncryptbillingDetails = billingDtls;
            sbi.EncryptshippingDetais = shippingDtls;
            return sbi;
        }

        public string pgsucessdecrypt(int sid)
        {
            try
            {
                string encData = HttpContext.Current.Request.Form["encData"];
                CommonMethod.WritetoNotePaymentgateway(encData, "", HttpContext.Current.Request.Url.AbsolutePath, " pgsucessdecrypt url hit payment gatway for Scrutinyment Fee", ClsLanguage.GetCookies("NBApplicationNo"));
                string encDatadecryptdata = AES128Bit.Decrypt(encData, EncodedKey, keysize);
                var aa = encDatadecryptdata.Split('|');
                CommonMethod.WritetoNotePaymentgateway(encData, encDatadecryptdata, HttpContext.Current.Request.Url.AbsolutePath, " pgsucessdecrypt url hit payment gatway for Scrutinyment Fee", ClsLanguage.GetCookies("NBApplicationNo"));
               ScrutinyFeesSubmit stlogin = new ScrutinyFeesSubmit();
               ScrutinyFeesSubmit obj = new ScrutinyFeesSubmit();
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
                stlogin.amount1 = aa[3];
                stlogin.feeamount = "";
                stlogin.gst = gst;
                stlogin.commission = commission;
                stlogin.paymode = aa[5];
                stlogin.banktxndate = aa[10];
                stlogin.Reason = aa[7];
                stlogin.apitxnid = aa[1];
                var other_detail = aa[6].Split(',');
                stlogin.ApplicationNo = other_detail[0];
                if (other_detail[1] == "Backyear")
                {
                    stlogin.courseyearid = Convert.ToInt32(other_detail[2]);
                    obj = stlogin.FeessubScrutinybackyear(stlogin);
                }
                else if (other_detail[1] == "PracMainYear")
                {
                    stlogin.courseyearid = Convert.ToInt32(other_detail[2]);
                    obj = stlogin.FeessubScrutinyprac(stlogin);
                }
                else
                {
                    obj = stlogin.FeessubScrutiny(stlogin);
                }

                //if(obj.Status== true)
                //{
                //    ScrutinymentRequest Scrutiny = new ScrutinymentRequest();
                //    var result = Scrutiny.ApproveScrutinymentRequest(aa[6]);

                //}

                if (obj.Id == 0)
                {
                    paymentgatewayemailScrutinyment(aa[6]);
                }
                return stlogin.courseyearid.ToString();
            }
            catch (Exception ex)
            {
                CommonMethod.WritetoNotePaymentgateway(ex.Message + "      " + ex.StackTrace, "", HttpContext.Current.Request.Url.AbsolutePath, "pgsucessdecrypt() url hit error on payment PaymentGateway  for Scrutinyment Fee", ClsLanguage.GetCookies("NBApplicationNo"));
                return "error";
            }
        }


        public string AirPaypgsucessdecrypt(int sid, string banktrxid, string clienttrxid, string amount1, string feeamount, string gst, string commission, string paymode, string banktxndate, string Reason, string apitxnid, string ApplicationNo, string courseyearid, string ScrutinyType, string encData, string encDatadecryptdata ,string PGstatus)
        {
            try
            {
               ScrutinyFeesSubmit stlogin = new ScrutinyFeesSubmit();
               ScrutinyFeesSubmit obj = new ScrutinyFeesSubmit();
                stlogin.Requestdata = encData;
                stlogin.dRequestdata = encDatadecryptdata;
                stlogin.PGstatus = PGstatus;
                gst = gst;
                commission = commission;
                stlogin.banktrxid = banktrxid;
                stlogin.clienttrxid = clienttrxid;
                stlogin.amount1 = amount1;
                stlogin.feeamount = feeamount;
                stlogin.gst = gst;
                stlogin.commission = commission;
                stlogin.paymode = paymode;
                stlogin.banktxndate = banktxndate;
                stlogin.Reason = Reason;
                stlogin.apitxnid = apitxnid;
                //var other_detail = aa[6].Split(',');
                stlogin.ApplicationNo = ApplicationNo;
                if (ScrutinyType == "Backyear")
                {
                    stlogin.courseyearid = Convert.ToInt32(courseyearid);
                    obj = stlogin.AirPayFeessubScrutinybackyear(stlogin);
                }
                else if (ScrutinyType == "PracMainYear")
                {
                    stlogin.courseyearid = Convert.ToInt32(courseyearid);
                    obj = stlogin.FeessubScrutinyprac(stlogin);
                }
                else
                {
                    obj = stlogin.AirPayFeessubScrutiny(stlogin);
                }

                if (obj.Id == 0)
                {
                    paymentgatewayemailScrutinyment(ApplicationNo);
                }
                return stlogin.courseyearid.ToString();
            }
            catch (Exception ex)
            {
                return "error";
            }
        }



        public string EaseBuzzPaypgsucessdecrypt(int sid, string banktrxid, string clienttrxid, string amount1, string feeamount, string gst, string commission, string paymode, string banktxndate, string Reason, string apitxnid, string ApplicationNo, string courseyearid, string ScrutinyType, string encData, string encDatadecryptdata, string PGstatus)
        {
            try
            {
                //string encData = HttpContext.Current.Request.Form["encData"];
                //CommonMethod.WritetoNotePaymentgateway(encData, "", HttpContext.Current.Request.Url.AbsolutePath, " pgsucessdecrypt url hit payment gatway for Scrutinyment Fee", ClsLanguage.GetCookies("NBApplicationNo"));
                //string encDatadecryptdata = AES128Bit.Decrypt(encData, EncodedKey, keysize);
                //var aa = encDatadecryptdata.Split('|');
                //CommonMethod.WritetoNotePaymentgateway(encData, encDatadecryptdata, HttpContext.Current.Request.Url.AbsolutePath, " pgsucessdecrypt url hit payment gatway for Scrutinyment Fee", ClsLanguage.GetCookies("NBApplicationNo"));
               ScrutinyFeesSubmit stlogin = new ScrutinyFeesSubmit();
               ScrutinyFeesSubmit obj = new ScrutinyFeesSubmit();
                stlogin.Requestdata = encData;
                stlogin.dRequestdata = encDatadecryptdata;
                stlogin.PGstatus = PGstatus;
                //var gstcomm = aa[14].Split('^');
                gst = gst;
                commission = commission;
                //if (gstcomm.Length > 1)
                //{
                //    commission = gstcomm[0];
                //    gst = gstcomm[1];
                //}

                CommonMethod.TraceLogWritetoNotepad("obj1.objScrutinyFrom4", "Scrutiny/sbipay11", "AirPaypgsucessdecrypt", "Id");
                stlogin.banktrxid = banktrxid;
                stlogin.clienttrxid = clienttrxid;
                stlogin.amount1 = amount1;
                stlogin.feeamount = feeamount;
                stlogin.gst = gst;
                stlogin.commission = commission;
                stlogin.paymode = paymode;
                stlogin.banktxndate = banktxndate;
                stlogin.Reason = Reason;
                stlogin.apitxnid = apitxnid;
                //var other_detail = aa[6].Split(',');
                stlogin.ApplicationNo = ApplicationNo;
                if (ScrutinyType == "Backyear")
                {
                    CommonMethod.TraceLogWritetoNotepad("obj1.objScrutinyFrom4", "Scrutiny/sbipay12", "AirPaypgsucessdecrypt", "Id");
                    //CommonMethod.TraceLogWritetoNotepad("obj1.objScrutinyFrom4", "Scrutiny/ScrutinyFeesSubmit12", "AirPaypgsucessdecrypt", "Id");
                    stlogin.courseyearid = Convert.ToInt32(courseyearid);
                    obj = stlogin.AirPayFeessubScrutinybackyear(stlogin);
                }


                else if (ScrutinyType == "PracMainYear")
                {
                    CommonMethod.TraceLogWritetoNotepad("obj1.objScrutinyFrom4", "Scrutiny/sbipay13", "AirPaypgsucessdecrypt", "Id");
                    //CommonMethod.TraceLogWritetoNotepad("obj1.objScrutinyFrom4", "Scrutiny/ScrutinyFeesSubmit12", "AirPaypgsucessdecrypt", "Id");
                    stlogin.courseyearid = Convert.ToInt32(courseyearid);
                    obj = stlogin.FeessubScrutinyprac(stlogin);
                }
                else
                {
                    CommonMethod.TraceLogWritetoNotepad("obj1.objScrutinyFrom4", "Scrutiny/sbipay14", "AirPaypgsucessdecrypt", "Id");
                    obj = stlogin.EaseBuzzFeessubScrutiny(stlogin);
                }

                CommonMethod.TraceLogWritetoNotepad("obj1.objScrutinyFrom4", "Scrutiny//sbipay15", "obj1.objScrutinyFrom", "Id");



                //if(obj.Status== true)
                //{
                //    ScrutinymentRequest Scrutiny = new ScrutinymentRequest();
                //    var result = Scrutiny.ApproveScrutinymentRequest(aa[6]);

                //}

                if (obj.Id == 0)
                {
                    CommonMethod.TraceLogWritetoNotepad("obj1.objScrutinyFrom4", "Scrutiny//sbipay16", "obj1.objScrutinyFrom", "Id");
                    paymentgatewayemailScrutinyment(ApplicationNo);
                }
                return stlogin.courseyearid.ToString();
            }
            catch (Exception ex)
            {
                CommonMethod.TraceLogWritetoNotepad("obj1.objScrutinyFrom4", "Scrutiny//sbipay17", "obj1.objScrutinyFrom", "Id");
                CommonMethod.WritetoNotePaymentgateway(ex.Message + "      " + ex.StackTrace, "", HttpContext.Current.Request.Url.AbsolutePath, "pgsucessdecrypt() url hit error on payment PaymentGateway  for Scrutinyment Fee", ClsLanguage.GetCookies("NBApplicationNo"));
                return "error";
            }
        }


        public string SafexPaypgsucessdecrypt(int sid, string banktrxid, string clienttrxid, string amount1, string feeamount, string gst, string commission, string paymode, string banktxndate, string Reason, string apitxnid, string ApplicationNo, string courseyearid, string ScrutinyType, string encData, string encDatadecryptdata, string PGstatus)
        {
            try
            {
               ScrutinyFeesSubmit stlogin = new ScrutinyFeesSubmit();
               ScrutinyFeesSubmit obj = new ScrutinyFeesSubmit();
                stlogin.Requestdata = encData;
                stlogin.dRequestdata = encDatadecryptdata;
                stlogin.PGstatus = PGstatus;
                gst = gst;
                commission = commission;
                CommonMethod.TraceLogWritetoNotepad("obj1.objScrutinyFrom4", "Scrutiny/sbipay11", "AirPaypgsucessdecrypt", "Id");
                stlogin.banktrxid = banktrxid;
                stlogin.clienttrxid = clienttrxid;
                stlogin.amount1 = amount1;
                stlogin.feeamount = feeamount;
                stlogin.gst = gst;
                stlogin.commission = commission;
                stlogin.paymode = paymode;
                stlogin.banktxndate = banktxndate;
                stlogin.Reason = Reason;
                stlogin.apitxnid = apitxnid;
                //var other_detail = aa[6].Split(',');
                stlogin.ApplicationNo = ApplicationNo;
                if (ScrutinyType == "Backyear")
                {
                    CommonMethod.TraceLogWritetoNotepad("obj1.objScrutinyFrom4", "Scrutiny/sbipay12", "AirPaypgsucessdecrypt", "Id");
                    stlogin.courseyearid = Convert.ToInt32(courseyearid);
                    obj = stlogin.SafexPayFeessubScrutinybackyear(stlogin);
                }
                else if (ScrutinyType == "PracMainYear")
                {
                    CommonMethod.TraceLogWritetoNotepad("obj1.objScrutinyFrom4", "Scrutiny/sbipay13", "AirPaypgsucessdecrypt", "Id");
                    stlogin.courseyearid = Convert.ToInt32(courseyearid);
                    obj = stlogin.FeessubScrutinyprac(stlogin);
                }
                else
                {
                    CommonMethod.TraceLogWritetoNotepad("obj1.objScrutinyFrom4", "Scrutiny/sbipay14", "AirPaypgsucessdecrypt", "Id");
                    obj = stlogin.SafexPayFeessubScrutiny(stlogin);
                }

                CommonMethod.TraceLogWritetoNotepad("obj1.objScrutinyFrom4", "Scrutiny//sbipay15", "obj1.objScrutinyFrom", "Id");
                if (obj.Id == 0)
                {
                    CommonMethod.TraceLogWritetoNotepad("obj1.objScrutinyFrom4", "Scrutiny//sbipay16", "obj1.objScrutinyFrom", "Id");
                    paymentgatewayemailScrutinyment(ApplicationNo);
                }
                return stlogin.courseyearid.ToString();
            }
            catch (Exception ex)
            {
                CommonMethod.TraceLogWritetoNotepad("obj1.objScrutinyFrom4", "Scrutiny//sbipay17", "obj1.objScrutinyFrom", "Id");
                CommonMethod.WritetoNotePaymentgateway(ex.Message + "      " + ex.StackTrace, "", HttpContext.Current.Request.Url.AbsolutePath, "pgsucessdecrypt() url hit error on payment PaymentGateway  for Scrutinyment Fee", ClsLanguage.GetCookies("NBApplicationNo"));
                return "error";
            }
        }



        public string pgfaileddecrypt(int sid)
        {
            try
            {

                string encData = HttpContext.Current.Request.Form["encData"];
                CommonMethod.WritetoNotePaymentgateway(encData, "", HttpContext.Current.Request.Url.AbsolutePath, " pgsucessdecrypt url hit payment gatway for Scrutinyment Fee", ClsLanguage.GetCookies("NBApplicationNo"));

                string encDatadecryptdata = AES128Bit.Decrypt(encData, EncodedKey, keysize);
                //string encDatadecryptdata = "CH8809800|4430840943731|SUCCESS|100|INR|IMPS|ABC^DEF^ERD|Transaction Successful|NA|G1312423|2018-06-24  16:30:24 | IN | 10000032018062412345 | 1000003 | 10.00 ^ 1.80 ||||||||||";

                var aa = encDatadecryptdata.Split('|');
                CommonMethod.WritetoNotePaymentgateway(encData, encDatadecryptdata, HttpContext.Current.Request.Url.AbsolutePath, " url hit payment PaymentGateway gatway for Scrutinyment Fee", ClsLanguage.GetCookies("NBApplicationNo"));
               ScrutinyFeesSubmit stlogin = new ScrutinyFeesSubmit();
               ScrutinyFeesSubmit obj = new ScrutinyFeesSubmit();
                stlogin.Requestdata = encData;
                stlogin.dRequestdata = encDatadecryptdata;
                stlogin.PGstatus = aa[2];// "success";//
                stlogin.banktrxid = aa[9];
                stlogin.clienttrxid = aa[0];
                stlogin.amount1 = aa[3];
                stlogin.feeamount = "";
                stlogin.gst = "";
                stlogin.commission = "";
                stlogin.paymode = aa[5];
                stlogin.banktxndate = aa[10];
                stlogin.Reason = aa[7];
                stlogin.apitxnid = aa[1];
                var other_detail = aa[6].Split(',');
                stlogin.ApplicationNo = other_detail[0];
                if (other_detail[1] == "Backyear")
                {
                    stlogin.courseyearid = Convert.ToInt32(other_detail[2]);
                    obj = stlogin.FeessubScrutinybackyear(stlogin);
                }
                else if (other_detail[1] == "PracMainYear")
                {
                    stlogin.courseyearid = Convert.ToInt32(other_detail[2]);
                    obj = stlogin.FeessubScrutinyprac(stlogin);
                }
                else
                {
                    obj = stlogin.FeessubScrutiny(stlogin);
                }
                if (obj.Id == 0)
                {
                    paymentgatewayemailScrutinyment(aa[6]);
                }
                return stlogin.courseyearid.ToString();
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
               ScrutinyFeesSubmit stlogin = new ScrutinyFeesSubmit();
               ScrutinyFeesSubmit obj = new ScrutinyFeesSubmit();
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
                stlogin.amount1 = aa[3];
                stlogin.feeamount = "";
                stlogin.gst = gst;
                stlogin.commission = commission;
                stlogin.paymode = aa[5];
                stlogin.banktxndate = aa[10];
                stlogin.Reason = aa[7];
                stlogin.apitxnid = aa[1];
                var other_detail = aa[6].Split(',');//application no, Scrutinyment
                stlogin.ApplicationNo = other_detail[0];

                if (other_detail[1] == "Backyear")
                {
                    stlogin.courseyearid = Convert.ToInt32(other_detail[2]);
                    obj = stlogin.FeessubScrutinyPushresponse_backyear(stlogin);
                }
                else if (other_detail[1] == "PracMainYear")
                {
                    stlogin.courseyearid = Convert.ToInt32(other_detail[2]);
                    obj = stlogin.FeessubScrutinyPushresponseprac(stlogin);
                }
                else
                {
                    obj = stlogin.FeessubScrutinyPushresponse(stlogin);
                }
                //obj = stlogin.FeessubScrutinyPushresponse(stlogin);
                if (obj.Status == true)
                {


                }
                CommonMethod.WritetoNotePaymentgateway(encData, encDatadecryptdata, HttpContext.Current.Request.Url.AbsolutePath, " Paymentgateway Pushresponse  url hit payment gatway  for Scrutinyment Fee");
                if (obj.Id == 0)
                {
                    paymentgatewayemailScrutinyment(aa[6]);
                }

                // Message Structure of ‘Push Response’
                //         Merchant Order Number | SBIePayRefID / ATRN | Transaction Status | Amount | Currency | Pay Mode | OtherDetails | Reason / Message | Bank Code | Bank Reference Number| Transaction Date | Country | CIN | Merchant ID | Total Fee GST| Ref1 | Ref2 | Ref3 | Ref4 | Ref5 | Ref6 | Ref7 | Ref8 | Ref9
                return encDatadecryptdata;
            }
            catch (Exception ex)
            {
                CommonMethod.WritetoNotePaymentgateway(ex.Message + "      " + ex.StackTrace, "", HttpContext.Current.Request.Url.AbsolutePath, "Paymentgateway Pushresponse url hit error on payment PaymentGateway for Scrutinyment Fee");
                return "error";
            }
        }
        public void paymentgatewayemailScrutinyment(string applicationno)
        {
            CommonMethod.TraceLogWritetoNotepad("obj1.objScrutinyFrom4", "Scrutiny/AirpayGetResponse21", "obj1.objScrutinyFrom", "Id");
            return;
            ScrutinyForm ob = new ScrutinyForm();
            //var obj1 = ob.GetAppLicationDataForScrutinymentFee();
            // Email.SendEmailForSt_ScrutinyPaymentgateway(obj1.objScrutinyFrom.Email, obj1.objPrintRecipt.status, obj1.objScrutinyFrom.StudentName, obj1.objPrintRecipt.trxdate, obj1.objPrintRecipt.banktrxid, obj1.objPrintRecipt.TransactionId, obj1.objPrintRecipt.ApplicationNo, obj1.objfeesubmit.Fees, obj1.objPrintRecipt.PaymentType, obj1.objScrutinyFrom.MobileNo, obj1.objScrutinyFrom.RegistrationNo);
        }
        public string doubleverificationregistration(string data)
        {
            try
            {
                //string ckey = CommonMethod.MIDcollegewise().Where(x=> x.collegeid==1).FirstOrDefault().mkey;
                //string encData = HttpContext.Current.Request.Form["pushRespData"];
                //string encDatadecryptdata = AES128Bit.Decrypt(encData, ckey, keysize);
                var aa = (data).Split('|');
               ScrutinyFeesSubmit stlogin = new ScrutinyFeesSubmit();
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
                stlogin.amount1 = (aa[7]);
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
               ScrutinyFeesSubmit obj = new ScrutinyFeesSubmit();
                if (other_detail[1] == "Backyear")
                {
                    stlogin.courseyearid = Convert.ToInt32(other_detail[2]);
                    obj = stlogin.FeessubStudentPushresponseDoubleverificationScrutiny_backlog(stlogin);
                }
                else if (other_detail[1] == "PracMainYear")
                {
                    stlogin.courseyearid = Convert.ToInt32(other_detail[2]);
                    obj = stlogin.FeessubStudentPushresponseDoubleverificationScrutinyprac(stlogin);
                }
                else
                {
                    obj = stlogin.FeessubStudentPushresponseDoubleverificationScrutiny(stlogin);
                }

                CommonMethod.WritetoNotePaymentgateway("Double Verification Scrutiny", "Double Verification Scrutiny", HttpContext.Current.Request.Url.AbsolutePath, "admission Paymentgateway Pushresponse  url hit payment gatway");

                // Message Structure of ‘Push Response’
                //         Merchant Order Number | SBIePayRefID / ATRN | Transaction Status | Amount | Currency | Pay Mode | OtherDetails | Reason / Message | Bank Code | Bank Reference Number| Transaction Date | Country | CIN | Merchant ID | Total Fee GST| Ref1 | Ref2 | Ref3 | Ref4 | Ref5 | Ref6 | Ref7 | Ref8 | Ref9
                return "";
            }
            catch (Exception ex)
            {
                CommonMethod.WritetoNotePaymentgateway(ex.Message + " Double Verification Scrutiny     " + ex.StackTrace, "", HttpContext.Current.Request.Url.AbsolutePath, "admission Paymentgateway Pushresponse url hit error on payment PaymentGateway");
                return "error";
            }
        }

        public string doubleverificationregistrationEaseBuzz(string data)
        {
            try
            {

                //data.Add("udf1", application.Trim());
                //data.Add("udf2", "MainYear");
                //data.Add("udf3", courseyearid);
                //data.Add("udf4", Order_Number.Trim());
                //data.Add("udf5", StID);

                Easebuzzresponse dataobj = JsonConvert.DeserializeObject<Easebuzzresponse>(data);

                //string ckey = CommonMethod.MIDcollegewise().Where(x=> x.collegeid==1).FirstOrDefault().mkey;
                //string encData = HttpContext.Current.Request.Form["pushRespData"];
                //string encDatadecryptdata = AES128Bit.Decrypt(encData, ckey, keysize);

                if (dataobj.msg != null && dataobj.status == true)
                {


                    //var aa = (data).Split('|');
                   ScrutinyFeesSubmit stlogin = new ScrutinyFeesSubmit();
                    //var gstcomm = aa[15].Split('^');
                    string gst = "";
                    string commission = "";

                    stlogin.Requestdata = "";
                    stlogin.dRequestdata = "";
                    stlogin.PGstatus = dataobj.msg.status;
                    stlogin.banktrxid = dataobj.msg.bank_ref_num;
                    stlogin.clienttrxid = dataobj.msg.txnid;
                    stlogin.amount1 = (dataobj.msg.amount);
                    stlogin.feeamount = "";
                    stlogin.gst = gst;
                    stlogin.commission = commission;
                    stlogin.paymode = dataobj.msg.mode;
                    stlogin.banktxndate = DateTime.Now.ToString();
                    stlogin.Reason = dataobj.msg.error;
                    stlogin.apitxnid = dataobj.msg.easepayid; ;
                    //var other_detail = aa[5].Split(',');
                    stlogin.ApplicationNo = dataobj.msg.udf1;
                    //stlogin.ApplicationNo = aa[5];
                    string Scrutinytype = dataobj.msg.udf2;
                    string courseyearid = dataobj.msg.udf3;
                   ScrutinyFeesSubmit obj = new ScrutinyFeesSubmit();
                    if (Scrutinytype == "Backyear")
                    {
                        stlogin.courseyearid = Convert.ToInt32(courseyearid);
                        obj = stlogin.FeessubStudentPushresponseDoubleverificationScrutiny_backlog(stlogin);
                    }
                    else if (Scrutinytype == "PracMainYear")
                    {
                        stlogin.courseyearid = Convert.ToInt32(courseyearid);
                        obj = stlogin.FeessubStudentPushresponseDoubleverificationScrutinyprac(stlogin);
                    }
                    else
                    {
                        obj = stlogin.FeessubStudentPushresponseDoubleverificationScrutiny(stlogin);
                    }

                    CommonMethod.WritetoNotePaymentgateway("Double Verification Scrutiny", "Double Verification Scrutiny", HttpContext.Current.Request.Url.AbsolutePath, "admission Paymentgateway Pushresponse  url hit payment gatway");

                    // Message Structure of ‘Push Response’
                    //         Merchant Order Number | SBIePayRefID / ATRN | Transaction Status | Amount | Currency | Pay Mode | OtherDetails | Reason / Message | Bank Code | Bank Reference Number| Transaction Date | Country | CIN | Merchant ID | Total Fee GST| Ref1 | Ref2 | Ref3 | Ref4 | Ref5 | Ref6 | Ref7 | Ref8 | Ref9
                    return "";

                }
                return "";
            }
            catch (Exception ex)
            {
                CommonMethod.WritetoNotePaymentgateway(ex.Message + " Double Verification Scrutiny     " + ex.StackTrace, "", HttpContext.Current.Request.Url.AbsolutePath, "admission Paymentgateway Pushresponse url hit error on payment PaymentGateway");
                return "error";
            }
        }

        public string doubleverificationregistrationSafex(string data)
        {
            try
            {
                //string ckey = CommonMethod.MIDcollegewise().Where(x=> x.collegeid==1).FirstOrDefault().mkey;
                //string encData = HttpContext.Current.Request.Form["pushRespData"];
                //string encDatadecryptdata = AES128Bit.Decrypt(encData, ckey, keysize);

                MyCryptoClass1 aes = new MyCryptoClass1();
                string enc_txn_response = (!String.IsNullOrEmpty(data)) ? data : string.Empty;
                string txn_response = aes.decrypt(enc_txn_response);

                SafexResponse1 dataobj = JsonConvert.DeserializeObject<SafexResponse1>(txn_response);

                if (dataobj.txn_response != null && dataobj.other_details != null && dataobj.pg_details != null && dataobj.fraud_details != null)
                {


                    string status = dataobj.txn_response.status;
                    string pg_ref = dataobj.txn_response.pg_ref;
                    string order_no = dataobj.txn_response.order_no;
                    string amount = dataobj.txn_response.amount;
                    string txn_date = dataobj.txn_response.txn_date;
                    string txn_time = dataobj.txn_response.txn_time;
                    string ag_ref = dataobj.txn_response.ag_ref;
                    string res_message = dataobj.txn_response.res_message;

                    string banktrxid = pg_ref;
                    string clienttrxid = order_no;
                    string amount1 = amount;
                    string feeamount = amount;
                    string gst = "0";
                    string commission = "0";
                    string banktxndate = txn_date + " " + txn_time;
                    string Reason = res_message;
                    string apitxnid = ag_ref;
                    string ApplicationNo = dataobj.other_details.udf_1;
                    string Requestdata = "";
                    string dRequestdata = "";
                    string PGstatus = dataobj.txn_response.status;
                    string Sessionid = "";
                    string Scrutinytype = dataobj.other_details.udf_2;

                    string courseyearid = dataobj.other_details.udf_3;

                   ScrutinyFeesSubmit stlogin = new ScrutinyFeesSubmit();
                    if (status == "Successful")
                    {
                        status = "Success";
                    }


                    stlogin.Requestdata = "";
                    stlogin.dRequestdata = "";
                    stlogin.PGstatus = status;
                    stlogin.banktrxid = banktrxid;
                    stlogin.clienttrxid = clienttrxid;
                    stlogin.amount1 = amount1;
                    stlogin.feeamount = "";
                    stlogin.gst = gst;
                    stlogin.commission = commission;
                    stlogin.paymode = dataobj.pg_details.paymode;
                    stlogin.banktxndate = banktxndate;
                    stlogin.Reason = Reason;
                    stlogin.apitxnid = apitxnid;
                    //var other_detail = aa[5].Split(',');
                    stlogin.ApplicationNo = ApplicationNo;
                    //stlogin.ApplicationNo = aa[5];
                   ScrutinyFeesSubmit obj = new ScrutinyFeesSubmit();
                    if (Scrutinytype == "Backyear")
                    {
                        stlogin.courseyearid = Convert.ToInt32(courseyearid);
                        obj = stlogin.FeessubStudentPushresponseDoubleverificationScrutiny_backlog(stlogin);
                    }
                    else if (Scrutinytype == "PracMainYear")
                    {
                        stlogin.courseyearid = Convert.ToInt32(courseyearid);
                        obj = stlogin.FeessubStudentPushresponseDoubleverificationScrutinyprac(stlogin);
                    }
                    else
                    {
                        obj = stlogin.FeessubStudentPushresponseDoubleverificationScrutiny(stlogin);
                    }

                    CommonMethod.WritetoNotePaymentgateway("Double Verification Scrutiny", "Double Verification Scrutiny", HttpContext.Current.Request.Url.AbsolutePath, "admission Paymentgateway Pushresponse  url hit payment gatway");

                    // Message Structure of ‘Push Response’
                    //         Merchant Order Number | SBIePayRefID / ATRN | Transaction Status | Amount | Currency | Pay Mode | OtherDetails | Reason / Message | Bank Code | Bank Reference Number| Transaction Date | Country | CIN | Merchant ID | Total Fee GST| Ref1 | Ref2 | Ref3 | Ref4 | Ref5 | Ref6 | Ref7 | Ref8 | Ref9
                    return "";

                }
                return "";
            }
            catch (Exception ex)
            {
                CommonMethod.WritetoNotePaymentgateway(ex.Message + " Double Verification Scrutiny     " + ex.StackTrace, "", HttpContext.Current.Request.Url.AbsolutePath, "admission Paymentgateway Pushresponse url hit error on payment PaymentGateway");
                return "error";
            }
        }

        public string doubleverificationregistrationAirPay(string data)
        {
            try
            {
                var serializer = new XmlSerializer(typeof(RESPONSE));

                RESPONSE logProperties;

                using (TextReader reader = new StringReader(data))
                {
                    logProperties = (RESPONSE)serializer.Deserialize(reader);
                }

                if (logProperties != null)
                {

                    var other_detail = logProperties.TRANSACTION.CUSTOMVAR.Split(',');
                    string ApplicationNo = other_detail[0];
                    string Scrutinytype = other_detail[1];
                    string courseyearid = other_detail[2];


                    // var aa = (data).Split('|');
                   ScrutinyFeesSubmit stlogin = new ScrutinyFeesSubmit();
                    // var gstcomm = aa[15].Split('^');
                    string gst = "";
                    string commission = "";
                    //if (gstcomm.Length > 1)
                    //{
                    //    commission = gstcomm[0];
                    //    gst = gstcomm[1];
                    //}
                    stlogin.Requestdata = "";
                    stlogin.dRequestdata = "";
                    stlogin.PGstatus = logProperties.TRANSACTION.TRANSACTIONPAYMENTSTATUS;
                    stlogin.banktrxid = logProperties.TRANSACTION.RRN;
                    stlogin.clienttrxid = logProperties.TRANSACTION.TRANSACTIONID;
                    stlogin.amount1 = (logProperties.TRANSACTION.AMOUNT);
                    stlogin.feeamount = "";
                    stlogin.gst = gst;
                    stlogin.commission = commission;
                    stlogin.paymode = logProperties.TRANSACTION.CHMOD;
                    stlogin.banktxndate = logProperties.TRANSACTION.TRANSACTIONTIME;
                    stlogin.Reason = logProperties.TRANSACTION.MESSAGE;
                    stlogin.apitxnid = logProperties.TRANSACTION.APTRANSACTIONID;
                    //var other_detail = aa[5].Split(',');
                    stlogin.ApplicationNo = ApplicationNo;

                    //stlogin.ApplicationNo = aa[5];
                   ScrutinyFeesSubmit obj = new ScrutinyFeesSubmit();
                    if (Scrutinytype == "Backyear")
                    {
                        stlogin.courseyearid = Convert.ToInt32(courseyearid);
                        obj = stlogin.FeessubStudentPushresponseDoubleverificationScrutiny_backlog(stlogin);
                    }
                    else if (Scrutinytype == "PracMainYear")
                    {
                        stlogin.courseyearid = Convert.ToInt32(courseyearid);
                        obj = stlogin.FeessubStudentPushresponseDoubleverificationScrutinyprac(stlogin);
                    }
                    else
                    {
                        obj = stlogin.FeessubStudentPushresponseDoubleverificationScrutiny(stlogin);
                    }

                    CommonMethod.WritetoNotePaymentgateway("Double Verification Scrutiny", "Double Verification Scrutiny", HttpContext.Current.Request.Url.AbsolutePath, "admission Paymentgateway Pushresponse  url hit payment gatway");

                    // Message Structure of ‘Push Response’
                    //         Merchant Order Number | SBIePayRefID / ATRN | Transaction Status | Amount | Currency | Pay Mode | OtherDetails | Reason / Message | Bank Code | Bank Reference Number| Transaction Date | Country | CIN | Merchant ID | Total Fee GST| Ref1 | Ref2 | Ref3 | Ref4 | Ref5 | Ref6 | Ref7 | Ref8 | Ref9
                    return "";

                }
                return "";
            }
            catch (Exception ex)
            {
                CommonMethod.WritetoNotePaymentgateway(ex.Message + " Double Verification Scrutiny     " + ex.StackTrace, "", HttpContext.Current.Request.Url.AbsolutePath, "admission Paymentgateway Pushresponse url hit error on payment PaymentGateway");
                return "error";
            }
        }

    }

    public class MyCryptoClass1
    {
        string privateKey = System.Configuration.ConfigurationSettings.AppSettings["merchant_key"];// "89diCMlKzp+GWwwBm5aVDv6sD+7wJj9ewrMjC6MsHmc="; // this Is Incription Key
        public string encrypt(string plainText)
        {
            string va = string.Empty;

            string encryptText = PayGateCryptoUtils.encrypt(plainText, privateKey);
            // string encryptText = encrypt(plainText, privateKey);
            return encryptText;
        }
        public string decrypt(string encryptText)
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



    public class EasebuzzresponseMsg1
    {
        public string txnid { get; set; }
        public string firstname { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public string key { get; set; }
        public string mode { get; set; }
        public string unmappedstatus { get; set; }
        public string cardCategory { get; set; }
        public string addedon { get; set; }
        public string payment_source { get; set; }
        public string PG_TYPE { get; set; }
        public string bank_ref_num { get; set; }
        public string bankcode { get; set; }
        public string error { get; set; }
        public string error_Message { get; set; }
        public string name_on_card { get; set; }
        public string upi_va { get; set; }
        public string cardnum { get; set; }
        public string issuing_bank { get; set; }
        public string easepayid { get; set; }
        public string amount { get; set; }
        public string net_amount_debit { get; set; }
        public string cash_back_percentage { get; set; }
        public string deduction_percentage { get; set; }
        public string merchant_logo { get; set; }
        public string surl { get; set; }
        public string furl { get; set; }
        public string productinfo { get; set; }
        public string udf10 { get; set; }
        public string udf9 { get; set; }
        public string udf8 { get; set; }
        public string udf7 { get; set; }
        public string udf6 { get; set; }
        public string udf5 { get; set; }
        public string udf4 { get; set; }
        public string udf3 { get; set; }
        public string udf2 { get; set; }
        public string udf1 { get; set; }
        public string card_type { get; set; }
        public string hash { get; set; }
        public string status { get; set; }
        public string bank_name { get; set; }
    }

    public class Easebuzzresponse1
    {
        public bool status { get; set; }
        public EasebuzzresponseMsg msg { get; set; }
    }

    public class TxnResponseSafex1
    {
        public string ag_id { get; set; }
        public string me_id { get; set; }
        public string order_no { get; set; }
        public string amount { get; set; }
        public string country { get; set; }
        public string currency { get; set; }
        public string txn_date { get; set; }
        public string txn_time { get; set; }
        public string ag_ref { get; set; }
        public string pg_ref { get; set; }
        public string status { get; set; }
        public string res_code { get; set; }
        public string res_message { get; set; }
        public string bank_response { get; set; }
    }

    public class PgDetailsSafex1
    {
        public string pg_id { get; set; }
        public string pg_name { get; set; }
        public string paymode { get; set; }
        public string emi_months { get; set; }
    }

    public class FraudDetailsSafex1
    {
    }

    public class OtherDetailsSafex1
    {
        public string udf_1 { get; set; }
        public string udf_2 { get; set; }
        public string udf_3 { get; set; }
        public string udf_4 { get; set; }
        public string udf_5 { get; set; }
    }
    public class SafexResponse1
    {
        public TxnResponseSafex txn_response { get; set; }
        public PgDetailsSafex pg_details { get; set; }
        public FraudDetailsSafex fraud_details { get; set; }
        public OtherDetailsSafex other_details { get; set; }
        public string card_no { get; set; }
    }

    //this class use for Airpay response in double verification api 
    [XmlRoot(ElementName = "TRANSACTION")]
    public class TRANSACTION1
    {
        [XmlElement(ElementName = "TRANSACTIONSTATUS")]
        public string TRANSACTIONSTATUS { get; set; }
        [XmlElement(ElementName = "MESSAGE")]
        public string MESSAGE { get; set; }
        [XmlElement(ElementName = "APTRANSACTIONID")]
        public string APTRANSACTIONID { get; set; }
        [XmlElement(ElementName = "TRANSACTIONID")]
        public string TRANSACTIONID { get; set; }
        [XmlElement(ElementName = "AMOUNT")]
        public string AMOUNT { get; set; }
        [XmlElement(ElementName = "ap_SecureHash")]
        public string Ap_SecureHash { get; set; }
        [XmlElement(ElementName = "AP_SECUREHASH")]
        public string AP_SECUREHASH { get; set; }
        [XmlElement(ElementName = "CUSTOMVAR")]
        public string CUSTOMVAR { get; set; }
        [XmlElement(ElementName = "CARDCOUNTRY")]
        public string CARDCOUNTRY { get; set; }
        [XmlElement(ElementName = "CHMOD")]
        public string CHMOD { get; set; }
        [XmlElement(ElementName = "CONVERSIONRATE")]
        public string CONVERSIONRATE { get; set; }
        [XmlElement(ElementName = "BANKNAME")]
        public string BANKNAME { get; set; }
        [XmlElement(ElementName = "CARDISSUER")]
        public string CARDISSUER { get; set; }
        [XmlElement(ElementName = "CARDTYPE")]
        public string CARDTYPE { get; set; }
        [XmlElement(ElementName = "CUSTOMER")]
        public string CUSTOMER { get; set; }
        [XmlElement(ElementName = "CUSTOMEREMAIL")]
        public string CUSTOMEREMAIL { get; set; }
        [XmlElement(ElementName = "CUSTOMERPHONE")]
        public string CUSTOMERPHONE { get; set; }
        [XmlElement(ElementName = "CURRENCYCODE")]
        public string CURRENCYCODE { get; set; }
        [XmlElement(ElementName = "RISK")]
        public string RISK { get; set; }
        [XmlElement(ElementName = "TRANSACTIONTYPE")]
        public string TRANSACTIONTYPE { get; set; }
        [XmlElement(ElementName = "TRANSACTIONTIME")]
        public string TRANSACTIONTIME { get; set; }
        [XmlElement(ElementName = "CARD_NUMBER")]
        public string CARD_NUMBER { get; set; }
        [XmlElement(ElementName = "TRANSACTIONPAYMENTSTATUS")]
        public string TRANSACTIONPAYMENTSTATUS { get; set; }
        [XmlElement(ElementName = "MERCHANT_NAME")]
        public string MERCHANT_NAME { get; set; }
        [XmlElement(ElementName = "WALLETBALANCE")]
        public string WALLETBALANCE { get; set; }
        [XmlElement(ElementName = "SURCHARGE")]
        public string SURCHARGE { get; set; }
        [XmlElement(ElementName = "SETTLEMENT_DATE")]
        public string SETTLEMENT_DATE { get; set; }
        [XmlElement(ElementName = "BILLEDAMOUNT")]
        public string BILLEDAMOUNT { get; set; }
        [XmlElement(ElementName = "TRANSACTIONREASON")]
        public string TRANSACTIONREASON { get; set; }
        [XmlElement(ElementName = "RRN")]
        public string RRN { get; set; }
    }


    //this class use for Airpay response in double verification api 
    [XmlRoot(ElementName = "RESPONSE")]
    public class RESPONSE1
    {
        [XmlElement(ElementName = "TRANSACTION")]
        public TRANSACTION TRANSACTION { get; set; }
    }
}