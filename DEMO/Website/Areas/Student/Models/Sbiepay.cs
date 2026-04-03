using com.paygate.ag.common.utils;
using com.toml.dp.util;
using DataLayer;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Configuration;
using System.Xml.Serialization;

namespace Website.Areas.Student.Models
{
    public class Sbiepay
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


        //public string Safex_me_id { get; set; }
        //public string Safex_merchant_request { get; set; }
        //public string Safex_hash { get; set; }
        //public string Safex_POSTURL { get; set; }

        //Airpay END

        //Airpay
        //Local
        //string username = "5926256";
        //string password = "me65Pf2K";
        //string secretKey = "A3brM5V9wjMWZh29";
        //string AirpayMid = "40594";
        //string AirPay_Success_url = "http://localhost:33166/student/Home/PGSucess_AirPay";
        ////string AirPay_Success_url = "https://portal.DemoUniversity.com/student/Home/PGSucess_AirPay";

        //Liveairpay
        string username = ConfigurationManager.AppSettings["username"]; 
        string password = ConfigurationManager.AppSettings["password"];
        string secretKey = ConfigurationManager.AppSettings["secret"]; 
        string AirpayMid = ConfigurationManager.AppSettings["mercid"]; 
        string AirPay_Success_url = ConfigurationManager.AppSettings["AirPayURL"];

        //AirpayEND

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

        //safex
        public string Safex_me_id { get; set; }
        public string Safex_merchant_request { get; set; }
        public string Safex_hash { get; set; }
        public string Safex_POSTURL { get; set; }

        //End safex
        //EaseBuzz End

        //easeBuzz live
        public string salt = "F6LU2RUJ1C";
        public string Key = "I2DPETI0FE";
        public string env = "prod";
        public string EasySuccessUrl = "https://portal.DemoUniversity.com/student/Home/PGSucess_EaseBuzz";
        public string EasyFailUrl = "https://portal.DemoUniversity.com/student/Home/PGSucess_EaseBuzz";

        //easeBuzz local
        //public string salt = "DAH88E3UWQ";
        //public string Key = "2PBP7IABZ2";
        //public string env = "test";
        //public string EasySuccessUrl = "http://localhost:33166/student/Home/PGSucess_EaseBuzz";
        //public string EasyFailUrl = "http://localhost:33166/student/Home/PGSucess_EaseBuzz";

        //EaseBuzz End

        //safex Live
        //public string merchant_id = "20220329008";// System.Configuration.ConfigurationSettings.AppSettings["merchant_id"];
        //public string ag_id = "Paygate"; //System.Configuration.ConfigurationSettings.AppSettings["ag_id"];
        //public string merchant_key = "lAoVyxKy4VanP5krhgy58c3cZ87ovjMFQKaRp6wFjVw=";// System.Configuration.ConfigurationSettings.AppSettings["merchant_key"];
        //public string txn_type = "SALE";// System.Configuration.ConfigurationSettings.AppSettings["txn_type"];
        //public string POSTURL = "https://pguat.safexpay.com/agcore/paymentProcessing";// System.Configuration.ConfigurationSettings.AppSettings["POSTURL"];
        //public string SafexPaySuccessUrl = "https://portal.DemoUniversity.com/student/Home/PGGateway_Safex";// System.Configuration.ConfigurationSettings.AppSettings["SafexPaySuccessUrl"];
        //public string SafexPayFailUrl = "https://portal.DemoUniversity.com/student/Home/PGGateway_Safex";// System.Configuration.ConfigurationSettings.AppSettings["SafexPayFailUrl"];
        //                                                                                                          //Safex End

        //safex LOcal
        public string merchant_id = "202203290008";// System.Configuration.ConfigurationSettings.AppSettings["merchant_id"];
        public string ag_id = "Paygate"; //System.Configuration.ConfigurationSettings.AppSettings["ag_id"];
        public string merchant_key = "lAoVyxKy4VanP5krhgy58c3cZ87ovjMFQKaRp6wFjVw=";// System.Configuration.ConfigurationSettings.AppSettings["merchant_key"];
        public string txn_type = "SALE";// System.Configuration.ConfigurationSettings.AppSettings["txn_type"];
        public string POSTURL = "https://www.avantgardepayments.com/agcore/paymentProcessing";// System.Configuration.ConfigurationSettings.AppSettings["POSTURL"];
        public string SafexPaySuccessUrl = "https://portal.DemoUniversity.com/student/Home/SafexPayPGSucess";// System.Configuration.ConfigurationSettings.AppSettings["SafexPaySuccessUrl"];
        public string SafexPayFailUrl = "https://portal.DemoUniversity.com/student/Home/SafexPayPGSucess";// System.Configuration.ConfigurationSettings.AppSettings["SafexPayFailUrl"];
                                                                                                                  //Safex End

        //safex Live
        //      <add key = "merchant_id" value="202203290007" />
        //<add key = "ag_id" value="Paygate" />
        //<add key = "merchant_key" value="6Q+73qvBQkd1mvSiWf83ExK5WjSodCFFljSUvcljnM8=" />
        //<add key = "txn_type" value="SALE" />
        //<add key = "POSTURL" value="https://www.avantgardepayments.com/agcore/paymentProcessing" />
        //<add key = "SafexPaySuccessUrl" value="https://portal.DemoUniversity.com/student/Exam/SafexPayPGSucessExam" />
        //<add key = "SafexPayFailUrl" value="https://portal.DemoUniversity.com/student/Exam/SafexPayPGSucessExam" />

        string status { get; set; }
        string MID = "1000694";//live
        //string MID = "1000003";//local
        string saveurl = "https://www.sbiepay.sbi/secure/AggregatorHostedListener";//live
        //string saveurl = "https://test.sbiepay.sbi/secure/AggregatorHostedListener";//local
        string Success_URL = WebConfigurationManager.AppSettings["siteUrl"];
        string Failure_URL = WebConfigurationManager.AppSettings["siteUrl"];
        string Collaborator_Id = "SBIEPAY";
        string Operating_Mode = "DOM";
        string Country = "IN";
        string Currency = "INR";
        string EncodedKey = "Y10jVVOtaH/mVb6NVLedIQ==";//"PF7gGMTXdESdmaeiXvYuIw==";//live
        //string EncodedKey = "fBc5628ybRQf88f/aqDUOQ==";//local
        string Paymode = "NB";
        string Accesmedium = "ONLINE";
        string TransactionSource = "ONLINE";
        string MerchantCustomerID = "NA";
        int keysize = 128;
        public Sbiepay encriptData(decimal Amount1, string Other_Details1 ,string Success_URL1,string Failure_URL1 )
        {
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
          //  Order_Number = "";
            var obj = fee.FeessubStudentbefore(fee);
            if (obj.Status == false)
            {
                goto  loop;
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

        public Sbiepay encriptDataSafex(decimal Amount1, string Other_Details1, string Success_URL1, string Failure_URL1)
        {
            StudentLogin stu = new StudentLogin();
            string ApplicationID = ClsLanguage.GetCookies("NBApplicationNo");
            Login lo = stu.BasicDetail(ApplicationID);
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
            fee.GetWayType = "Safex";
            //  Order_Number = "";
            var obj = fee.FeessubStudentbefore(fee);
            if (obj.Status == false)
            {
                goto loop;
            }

            int StID = (ClsLanguage.GetCookies("NBStID") != null ? Convert.ToInt32(ClsLanguage.GetCookies("NBStID")) : 0);

            string txnagId = ag_id;
            string txnmerchantId = merchant_id;
            string txnmerchantKey = merchant_key;
            string txnorderNumber = Order_Number;
            string txnAmount = Amount1.ToString(); 
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
            string custName = lo.FirstName + "" + lo.LastName;
            string custEmailId = lo.Email;
            string custMobileNo = lo.MobileNo;
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
            string udf2 = "RegistrationFee";
            string udf3 = "";
            string udf4 = StID.ToString();
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
            //MyCryptoClass aes = new MyCryptoClass();
            string enc_request = encrypt(request);
            string hashing = ComputeSha256Hash(Hash);
            string enc_hash = encrypt(hashing);
            sbi.Safex_me_id = txnmerchantId; // As String
            sbi.Safex_merchant_request = enc_request;
            sbi.Safex_hash = enc_hash;
            sbi.Safex_POSTURL = POSTURL;
            return sbi;
        }


        public Sbiepay encriptDataEaseBuzz(decimal Amount1, string Other_Details1, string Success_URL1, string Failure_URL1)
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
            fee.amount = Amount1.ToString(); ;
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



        public Sbiepay encriptDataAirpay(decimal Amount1, string Other_Details1, string Success_URL1, string Failure_URL1)
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

            Other_Details = Other_Details1 + ",Registration," + 1 + ",registration-" + DateTime.Now.Year.ToString() + "," + Order_Number + "," + StID + "," + Order_Number;

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

        public Sbiepay encriptDataSafexPay(decimal Amount1, string Other_Details1, string Success_URL1, string Failure_URL1)
        {
            Sbiepay sbi = new Sbiepay();
           
            
            StudentLogin stu = new StudentLogin();
            string ApplicationID = ClsLanguage.GetCookies("NBApplicationNo");
            Login lo = stu.BasicDetail(ApplicationID);

            string Order_Number = "";
            string Other_Details = Other_Details1;
            DateTime now = DateTime.Now;
            loop:
            Order_Number = CommonMethod.RandomNumber(100000, 999999) + now.ToString("MdHHmmyyssfff");
            FeesSubmit fee = new FeesSubmit();
            fee.ApplicationNo = Other_Details1;

            fee.clienttrxid = Order_Number;
            fee.GetWayType = "SafexPay";
            //Other_Details = Other_Details1 + ",Exampaper-" + year;

            int StID = (ClsLanguage.GetCookies("NBStID") != null ? Convert.ToInt32(ClsLanguage.GetCookies("NBStID")) : 0);
            int Sission = (ClsLanguage.GetCookies("NBSission") != null ? Convert.ToInt32(ClsLanguage.GetCookies("NBSission")) : 0);

            Other_Details = Other_Details1 + ",Registration," + 1 + ",registration-" + DateTime.Now.Year.ToString() + "," + Order_Number + "," + StID + "," + Order_Number;
        
            fee.Other_Details = Other_Details;
            try
            {
                var obj = fee.FeessubStudentbefore(fee);  /*TransactionSource save in table */
                if (obj.Status == false)
                {
                    goto loop;
                }
            }

            catch (Exception ex)
            {
                goto loop;
            }
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

            string custName = lo.FirstName +" " +lo.LastName; 
            string custEmailId = lo.Email;
            string custMobileNo = lo.MobileNo;
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
            string udf2 = "ApplicationYear";
            string udf3 = "";
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

            //MyCryptoClass aes = new MyCryptoClass();
            string enc_request = encrypt(request);
            string hashing = ComputeSha256Hash(Hash);
            string enc_hash = encrypt(hashing); 
            sbi.Safex_me_id = txnmerchantId; // As String
            sbi.Safex_merchant_request = enc_request;
            sbi.Safex_hash = enc_hash;
            sbi.Safex_POSTURL = POSTURL;
            return sbi;
        }

  //      	<add key = "merchant_id" value="202203290007" />
		//<add key = "ag_id" value="Paygate" />
		//<add key = "merchant_key" value="6Q+73qvBQkd1mvSiWf83ExK5WjSodCFFljSUvcljnM8=" />
		//<add key = "txn_type" value="SALE" />
		//<add key = "POSTURL" value="https://www.avantgardepayments.com/agcore/paymentProcessing" />
		//<add key = "SafexPaySuccessUrl" value="https://portal.DemoUniversity.com/student/Exam/SafexPayPGSucessExam" />
		//<add key = "SafexPayFailUrl" value="https://portal.DemoUniversity.com/student/Exam/SafexPayPGSucessExam" />

            string privateKey = "lAoVyxKy4VanP5krhgy58c3cZ87ovjMFQKaRp6wFjVw=";// "89diCMlKzp+GWwwBm5aVDv6sD+7wJj9ewrMjC6MsHmc="; // this Is Incription Key
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


    



        public Sbiepay encriptData_spot(decimal Amount1, string Other_Details1, string Success_URL1, string Failure_URL1)
        {

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
            var obj = fee.FeessubStudentbefore_spot(fee);
            if (obj.Status == false)
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

        public Sbiepay encriptDataadmission(decimal Amount1, string Other_Details1, string Success_URL1, string Failure_URL1,string cmid,string ckey)
        {
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
            var obj = fee.FeessubStudentbefore(fee);
            if (obj.Status == false)
            {
                goto loop;
            }
            //EncryptTrans = Merchant ID|Operating Mode|Merchant Country|Merchant Currency|Posting Amount|Other Details|SuccessURL | Fail URL | Aggregator ID | Merchant Order Number| Merchant Customer ID| Paymode | Access Medium | Transaction Source
            string Requestparameter = cmid + "|" + Operating_Mode + "|" + Country + "|" + Currency + "|" + Amount + "|" + Other_Details + "|" + Success_URL + "|" + Failure_URL + "|" + Collaborator_Id + "|" + Order_Number + "|" + MerchantCustomerID + "|" + Paymode + "|" + Accesmedium + "|" + TransactionSource + "";
            string EncryptedParam = AES128Bit.Encrypt(Requestparameter, ckey, keysize);
            string billingDtls = "";
            string shippingDtls = "";
            Sbiepay sbi = new Sbiepay();
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
                CommonMethod.WritetoNotePaymentgateway(encData, "", HttpContext.Current.Request.Url.AbsolutePath, " pgsucessdecrypt url hit payment gatway", ClsLanguage.GetCookies("NBApplicationNo"));
                string encDatadecryptdata = AES128Bit.Decrypt(encData, EncodedKey, keysize);
                var aa = encDatadecryptdata.Split('|');
                CommonMethod.WritetoNotePaymentgateway(encData, encDatadecryptdata, HttpContext.Current.Request.Url.AbsolutePath, " pgsucessdecrypt url hit payment gatway", ClsLanguage.GetCookies("NBApplicationNo"));
                FeesSubmit stlogin = new FeesSubmit();
                stlogin.Requestdata = encData;
                stlogin.dRequestdata = encDatadecryptdata;
                stlogin.PGstatus = aa[2];
                var gstcomm = aa[14].Split('^');
                string gst = "";
                string commission = "";
                if (gstcomm.Length >1)
                {
                    commission = gstcomm[0];
                    gst= gstcomm[1];
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
                stlogin.ApplicationNo = aa[6];

                var obj = stlogin.FeessubStudent(stlogin);
                //Sample Response for a ‘Successful’ Transaction:
                //Merchant Order Number|SBIePayRefID/ATRN|Transaction Status|Amount|Currency|Pay Mode|Other    Details | Reason / Message | Bank Code | Bank Reference Number| Transaction Date | Country | CIN | Merchant ID | Total Fee GST| Ref1 | Ref2 | Ref3 | Ref4 | Ref5 | Ref6 | Ref7 | Ref8 | Ref9
                // encData=CH8809800|4430840943731|SUCCESS|100|INR|IMPS|ABC^DEF^ERD|Transaction Successful|NA|G1312423|2018-06-24  16:30:24 | IN | 10000032018062412345 | 1000003 | 10.00 ^ 1.80 |||||||||
                //Sample Response for a ‘Pending’ Transaction:
                //encData=CH8809800|4430840943731|PENDING|100|INR|IMPS|ABC^DEF^ERD|Pending for authorization|NA|G1312423|2018-06-24 16:30:24 | IN | 10000032018062412345 | 1000003 | 10.00 ^ 1.80 ||||||||
                //Sample Response for a ‘Failed’ Transaction:
                //encData = CH8809800 | 4430840943731 | FAIL or FAILURE | 100 | INR | IMPS | ABC ^ DEF ^ ERD | Invalid GatewayPostingAmount | NA | G1312423 | 2018 - 06  - 24 16:30:24 | IN | NA | 1000003 | 10.00 ^ 1.80 |||||||||
                if (obj.Id == 0)
                {
                    paymentgatewayemail(aa[6]);
                }
                return encDatadecryptdata;
            }
            catch (Exception ex)
            {
                CommonMethod.WritetoNotePaymentgateway(ex.Message+"      "+ex.StackTrace, "", HttpContext.Current.Request.Url.AbsolutePath, "pgsucessdecrypt() url hit error on payment PaymentGateway", ClsLanguage.GetCookies("NBApplicationNo"));
                return "error";
            }
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


        public string pgsucessdecryptSafexPay(int sid, string banktrxid, string clienttrxid, string amount1, string feeamount, string gst, string commission, string paymode, string banktxndate, string Reason, string apitxnid, string ApplicationNo, string courseyearid, string examType, string encData, string encDatadecryptdata, string PGstatus)
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


        public string pgsucessdecrypt_spot(int sid)
        {
            try
            {

                string encData = HttpContext.Current.Request.Form["encData"];
                CommonMethod.WritetoNotePaymentgateway(encData, "", HttpContext.Current.Request.Url.AbsolutePath, " pgsucessdecrypt url hit payment gatway", ClsLanguage.GetCookies("NBApplicationNo"));
                string encDatadecryptdata = AES128Bit.Decrypt(encData, EncodedKey, keysize);
                var aa = encDatadecryptdata.Split('|');
                CommonMethod.WritetoNotePaymentgateway(encData, encDatadecryptdata, HttpContext.Current.Request.Url.AbsolutePath, " pgsucessdecrypt url hit payment gatway", ClsLanguage.GetCookies("NBApplicationNo"));
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
                stlogin.ApplicationNo = aa[6];
                var obj = stlogin.FeessubStudent_spot(stlogin);
                //Sample Response for a ‘Successful’ Transaction:
                //Merchant Order Number|SBIePayRefID/ATRN|Transaction Status|Amount|Currency|Pay Mode|Other    Details | Reason / Message | Bank Code | Bank Reference Number| Transaction Date | Country | CIN | Merchant ID | Total Fee GST| Ref1 | Ref2 | Ref3 | Ref4 | Ref5 | Ref6 | Ref7 | Ref8 | Ref9
                // encData=CH8809800|4430840943731|SUCCESS|100|INR|IMPS|ABC^DEF^ERD|Transaction Successful|NA|G1312423|2018-06-24  16:30:24 | IN | 10000032018062412345 | 1000003 | 10.00 ^ 1.80 |||||||||
                //Sample Response for a ‘Pending’ Transaction:
                //encData=CH8809800|4430840943731|PENDING|100|INR|IMPS|ABC^DEF^ERD|Pending for authorization|NA|G1312423|2018-06-24 16:30:24 | IN | 10000032018062412345 | 1000003 | 10.00 ^ 1.80 ||||||||
                //Sample Response for a ‘Failed’ Transaction:
                //encData = CH8809800 | 4430840943731 | FAIL or FAILURE | 100 | INR | IMPS | ABC ^ DEF ^ ERD | Invalid GatewayPostingAmount | NA | G1312423 | 2018 - 06  - 24 16:30:24 | IN | NA | 1000003 | 10.00 ^ 1.80 |||||||||
                if (obj.Id == 0)
                {
                    paymentgatewayemail(aa[6]);
                }
                return encDatadecryptdata;
            }
            catch (Exception ex)
            {
                CommonMethod.WritetoNotePaymentgateway(ex.Message + "      " + ex.StackTrace, "", HttpContext.Current.Request.Url.AbsolutePath, "pgsucessdecrypt() url hit error on payment PaymentGateway", ClsLanguage.GetCookies("NBApplicationNo"));
                return "error";
            }
        }
        public string pgfaileddecrypt_spot(int sid)
        {
            try
            {

                string encData = HttpContext.Current.Request.Form["encData"];
                CommonMethod.WritetoNotePaymentgateway(encData, "", HttpContext.Current.Request.Url.AbsolutePath, " pgsucessdecrypt url hit payment gatway", ClsLanguage.GetCookies("NBApplicationNo"));

                string encDatadecryptdata = AES128Bit.Decrypt(encData, EncodedKey, keysize);
                //string encDatadecryptdata = "CH8809800|4430840943731|SUCCESS|100|INR|IMPS|ABC^DEF^ERD|Transaction Successful|NA|G1312423|2018-06-24  16:30:24 | IN | 10000032018062412345 | 1000003 | 10.00 ^ 1.80 ||||||||||";

                var aa = encDatadecryptdata.Split('|');
                CommonMethod.WritetoNotePaymentgateway(encData, encDatadecryptdata, HttpContext.Current.Request.Url.AbsolutePath, " url hit payment PaymentGateway", ClsLanguage.GetCookies("NBApplicationNo"));
                FeesSubmit stlogin = new FeesSubmit();
                stlogin.Requestdata = encData;
                stlogin.dRequestdata = encDatadecryptdata;
                stlogin.PGstatus = aa[2];
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
                stlogin.ApplicationNo = aa[6];
                var obj = stlogin.FeessubStudent_spot(stlogin);
                if (obj.Id == 0)
                {
                    paymentgatewayemail(aa[6]);
                }
                return encDatadecryptdata;
            }
            catch (Exception ex)
            {
                CommonMethod.WritetoNotePaymentgateway(ex.Message + "             " + ex.StackTrace, "", HttpContext.Current.Request.Url.AbsolutePath, "pgfaileddecrypt() url hit error on payment PaymentGateway", ClsLanguage.GetCookies("NBApplicationNo"));
                return "error";
            }
        }


        public string Pushresponsedecrypt()
        {
            try
            {
                string encData = HttpContext.Current.Request.Form["pushRespData"];
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
                stlogin.feeamount ="";
                stlogin.gst = gst;
                stlogin.commission = commission;
                stlogin.paymode = aa[5];
                stlogin.banktxndate = aa[10];
                stlogin.Reason = aa[7];
                stlogin.apitxnid = aa[1];
                stlogin.ApplicationNo = aa[6];
                
                var obj = stlogin.FeessubStudentPushresponse(stlogin);
                CommonMethod.WritetoNotePaymentgateway(encData, encDatadecryptdata, HttpContext.Current.Request.Url.AbsolutePath, " Paymentgateway Pushresponse  url hit payment gatway");
                if(obj.Id==0)
                {
                    paymentgatewayemail(aa[6]);
                }
               
                // Message Structure of ‘Push Response’
                //         Merchant Order Number | SBIePayRefID / ATRN | Transaction Status | Amount | Currency | Pay Mode | OtherDetails | Reason / Message | Bank Code | Bank Reference Number| Transaction Date | Country | CIN | Merchant ID | Total Fee GST| Ref1 | Ref2 | Ref3 | Ref4 | Ref5 | Ref6 | Ref7 | Ref8 | Ref9
                return encDatadecryptdata;
            }
            catch (Exception ex)
            {
                CommonMethod.WritetoNotePaymentgateway(ex.Message + "      " + ex.StackTrace, "", HttpContext.Current.Request.Url.AbsolutePath, "Paymentgateway Pushresponse url hit error on payment PaymentGateway");
                return "error";
            }
        }
        public string Pushresponsedecrypt_spot()
        {
            try
            {
                string encData = HttpContext.Current.Request.Form["pushRespData"];
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
                stlogin.ApplicationNo = aa[6];

                var obj = stlogin.FeessubStudentPushresponse_spot(stlogin);
                CommonMethod.WritetoNotePaymentgateway(encData, encDatadecryptdata, HttpContext.Current.Request.Url.AbsolutePath, " Paymentgateway Pushresponse  url hit payment gatway");
                if (obj.Id == 0)
                {
                    paymentgatewayemail(aa[6]);
                }

                // Message Structure of ‘Push Response’
                //         Merchant Order Number | SBIePayRefID / ATRN | Transaction Status | Amount | Currency | Pay Mode | OtherDetails | Reason / Message | Bank Code | Bank Reference Number| Transaction Date | Country | CIN | Merchant ID | Total Fee GST| Ref1 | Ref2 | Ref3 | Ref4 | Ref5 | Ref6 | Ref7 | Ref8 | Ref9
                return encDatadecryptdata;
            }
            catch (Exception ex)
            {
                CommonMethod.WritetoNotePaymentgateway(ex.Message + "      " + ex.StackTrace, "", HttpContext.Current.Request.Url.AbsolutePath, "Paymentgateway Pushresponse url hit error on payment PaymentGateway");
                return "error";
            }
        }

        public string pgfaileddecrypt(int sid)
        {
            try
            {
              
                string encData = HttpContext.Current.Request.Form["encData"];
                CommonMethod.WritetoNotePaymentgateway(encData, "", HttpContext.Current.Request.Url.AbsolutePath, " pgsucessdecrypt url hit payment gatway", ClsLanguage.GetCookies("NBApplicationNo"));

                 string encDatadecryptdata = AES128Bit.Decrypt(encData, EncodedKey, keysize);
                //string encDatadecryptdata = "CH8809800|4430840943731|SUCCESS|100|INR|IMPS|ABC^DEF^ERD|Transaction Successful|NA|G1312423|2018-06-24  16:30:24 | IN | 10000032018062412345 | 1000003 | 10.00 ^ 1.80 ||||||||||";

                var aa = encDatadecryptdata.Split('|');
                CommonMethod.WritetoNotePaymentgateway(encData, encDatadecryptdata, HttpContext.Current.Request.Url.AbsolutePath, " url hit payment PaymentGateway", ClsLanguage.GetCookies("NBApplicationNo"));
                FeesSubmit stlogin = new FeesSubmit();
                stlogin.Requestdata = encData;
                stlogin.dRequestdata = encDatadecryptdata;
                stlogin.PGstatus = aa[2];
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
                stlogin.ApplicationNo = aa[6];
                var obj = stlogin.FeessubStudent(stlogin);
                if (obj.Id == 0)
                {
                    paymentgatewayemail(aa[6]);
                }
                return encDatadecryptdata;
            }
            catch (Exception ex)
            {
                CommonMethod.WritetoNotePaymentgateway(ex.Message + "             "+ex.StackTrace , "", HttpContext.Current.Request.Url.AbsolutePath, "pgfaileddecrypt() url hit error on payment PaymentGateway", ClsLanguage.GetCookies("NBApplicationNo"));
                return "error";
            }
        }
        public void paymentgatewayemail(string applicationno)
        {
            BL_PrintApplication PritApp = new BL_PrintApplication();
            var app = applicationno;
            Login objl22 = new Login();
            StudentLogin objs = new StudentLogin();
            objl22 = objs.BasicDetail(app);
            var obj1 = PritApp.GetAppLicationDataAdminPush(objl22.Id,objl22.session);
            Email.SendEmailForSt_RegistrationPaymentgateway(obj1.ObjApplication.Email, obj1.objPrintRecipt.status, obj1.ObjApplication.Name, obj1.objPrintRecipt.trxdate, obj1.objPrintRecipt.banktrxid, obj1.objPrintRecipt.TransactionId, obj1.objPrintRecipt.ApplicationNo, obj1.objfeesubmit.Fees, obj1.objPrintRecipt.PaymentType, obj1.ObjApplication.MobileNo);
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
                var obj = stlogin.FeessubStudent_doubleverification(stlogin);
                CommonMethod.WritetoNotePaymentgateway("Double Verification doubleverification", "Double Verification doubleverification", HttpContext.Current.Request.Url.AbsolutePath, "admission Paymentgateway Pushresponse  url hit payment gatway");

                // Message Structure of ‘Push Response’
                //         Merchant Order Number | SBIePayRefID / ATRN | Transaction Status | Amount | Currency | Pay Mode | OtherDetails | Reason / Message | Bank Code | Bank Reference Number| Transaction Date | Country | CIN | Merchant ID | Total Fee GST| Ref1 | Ref2 | Ref3 | Ref4 | Ref5 | Ref6 | Ref7 | Ref8 | Ref9
                return "";
            }
            catch (Exception ex)
            {
                CommonMethod.WritetoNotePaymentgateway(ex.Message + " Double Verification doubleverification     " + ex.StackTrace, "", HttpContext.Current.Request.Url.AbsolutePath, "admission Paymentgateway Pushresponse url hit error on payment PaymentGateway");
                return "error";
            }
        }
        public string doubleverificationregistrationAirpay(string data)
        {
            try
            {

                var serializer = new XmlSerializer(typeof(RESPONSE));

                RESPONSE logProperties;

                using (TextReader reader = new StringReader(data))
                {
                    logProperties = (RESPONSE)serializer.Deserialize(reader);
                }

                //string ckey = CommonMethod.MIDcollegewise().Where(x=> x.collegeid==1).FirstOrDefault().mkey;
                //string encData = HttpContext.Current.Request.Form["pushRespData"];
                //string encDatadecryptdata = AES128Bit.Decrypt(encData, ckey, keysize);
                var aa = (data).Split('|');
                FeesSubmit stlogin = new FeesSubmit();
                //var gstcomm = aa[15].Split('^');
                string gst = "";
                string commission = "";
                //if (gstcomm.Length > 1)
                //{
                //    commission = gstcomm[0];
                //    gst = gstcomm[1];
                //}

                if (logProperties != null)
                {

                    var other_detail = logProperties.TRANSACTION.CUSTOMVAR.Split(',');
                    string ApplicationNo = other_detail[0];
                    string examtype = other_detail[1];
                    string courseyearid = other_detail[2];
                    stlogin.Requestdata = "";
                stlogin.dRequestdata = "";
                stlogin.PGstatus = logProperties.TRANSACTION.TRANSACTIONPAYMENTSTATUS;
                stlogin.banktrxid = logProperties.TRANSACTION.RRN;
                stlogin.clienttrxid = logProperties.TRANSACTION.TRANSACTIONID;
                stlogin.amount = (logProperties.TRANSACTION.AMOUNT);
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
                var obj = stlogin.FeessubStudent_doubleverification(stlogin);
               
                // Message Structure of ‘Push Response’
                //         Merchant Order Number | SBIePayRefID / ATRN | Transaction Status | Amount | Currency | Pay Mode | OtherDetails | Reason / Message | Bank Code | Bank Reference Number| Transaction Date | Country | CIN | Merchant ID | Total Fee GST| Ref1 | Ref2 | Ref3 | Ref4 | Ref5 | Ref6 | Ref7 | Ref8 | Ref9
                return "";
                }
                return "";

                }
            catch (Exception ex)
            {
                CommonMethod.WritetoNotePaymentgateway(ex.Message + " Double Verification doubleverification     " + ex.StackTrace, "", HttpContext.Current.Request.Url.AbsolutePath, "admission Paymentgateway Pushresponse url hit error on payment PaymentGateway");
                return "error";
            }
        }

        //public string doubleverificationregistrationEaseBuzz(string data)
        //{
        //    try
        //    {
        //        var serializer = new XmlSerializer(typeof(RESPONSE));
        //        RESPONSE logProperties;
        //        using (TextReader reader = new StringReader(data))
        //        {
        //            logProperties = (RESPONSE)serializer.Deserialize(reader);
        //        }

        //        var aa = (data).Split('|');
        //        FeesSubmit stlogin = new FeesSubmit();
        //        //var gstcomm = aa[15].Split('^');
        //        string gst = "";
        //        string commission = "";
        //        if (logProperties != null)
        //        {

        //            var other_detail = logProperties.TRANSACTION.CUSTOMVAR.Split(',');
        //            string ApplicationNo = other_detail[0];
        //            string examtype = other_detail[1];
        //            string courseyearid = other_detail[2];
        //            stlogin.Requestdata = "";
        //            stlogin.dRequestdata = "";
        //            stlogin.PGstatus = logProperties.TRANSACTION.TRANSACTIONPAYMENTSTATUS;
        //            stlogin.banktrxid = logProperties.TRANSACTION.RRN;
        //            stlogin.clienttrxid = logProperties.TRANSACTION.TRANSACTIONID;
        //            stlogin.amount = (logProperties.TRANSACTION.AMOUNT);
        //            stlogin.feeamount = "";
        //            stlogin.gst = gst;
        //            stlogin.commission = commission;
        //            stlogin.paymode = logProperties.TRANSACTION.CHMOD;
        //            stlogin.banktxndate = logProperties.TRANSACTION.TRANSACTIONTIME;
        //            stlogin.Reason = logProperties.TRANSACTION.MESSAGE;
        //            stlogin.apitxnid = logProperties.TRANSACTION.APTRANSACTIONID;
        //            stlogin.ApplicationNo = ApplicationNo;
        //            var obj = stlogin.FeessubStudent_doubleverification(stlogin);
        //             return "";
        //        }
        //        return "";

        //    }
        //    catch (Exception ex)
        //    {
        //        CommonMethod.WritetoNotePaymentgateway(ex.Message + " Double Verification doubleverification     " + ex.StackTrace, "", HttpContext.Current.Request.Url.AbsolutePath, "admission Paymentgateway Pushresponse url hit error on payment PaymentGateway");
        //        return "error";
        //    }
        //}

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
                    FeesSubmit stlogin = new FeesSubmit();
                    //var gstcomm = aa[15].Split('^');
                    string gst = "";
                    string commission = "";

                    stlogin.Requestdata = "";
                    stlogin.dRequestdata = "";
                    stlogin.PGstatus = dataobj.msg.status;
                    stlogin.banktrxid = dataobj.msg.bank_ref_num;
                    stlogin.clienttrxid = dataobj.msg.txnid;
                    stlogin.amount = (dataobj.msg.amount);
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
                    string examtype = dataobj.msg.udf2;
                    string courseyearid = dataobj.msg.udf3;

                    var obj = stlogin.FeessubStudent_doubleverification(stlogin);


                    CommonMethod.WritetoNotePaymentgateway("Double Verification Exam", "Double Verification Exam", HttpContext.Current.Request.Url.AbsolutePath, "admission Paymentgateway Pushresponse  url hit payment gatway");

                    // Message Structure of ‘Push Response’
                    //         Merchant Order Number | SBIePayRefID / ATRN | Transaction Status | Amount | Currency | Pay Mode | OtherDetails | Reason / Message | Bank Code | Bank Reference Number| Transaction Date | Country | CIN | Merchant ID | Total Fee GST| Ref1 | Ref2 | Ref3 | Ref4 | Ref5 | Ref6 | Ref7 | Ref8 | Ref9
                    return "";

                }
                return "";
            }
            catch (Exception ex)
            {
                CommonMethod.WritetoNotePaymentgateway(ex.Message + " Double Verification Exam     " + ex.StackTrace, "", HttpContext.Current.Request.Url.AbsolutePath, "admission Paymentgateway Pushresponse url hit error on payment PaymentGateway");
                return "error";
            }
        }


        public string doubleverificationregistrationSafex(string data)
        {
            try
            {

                var serializer = new XmlSerializer(typeof(RESPONSE));

                RESPONSE logProperties;

                using (TextReader reader = new StringReader(data))
                {
                    logProperties = (RESPONSE)serializer.Deserialize(reader);
                }

                //string ckey = CommonMethod.MIDcollegewise().Where(x=> x.collegeid==1).FirstOrDefault().mkey;
                //string encData = HttpContext.Current.Request.Form["pushRespData"];
                //string encDatadecryptdata = AES128Bit.Decrypt(encData, ckey, keysize);
                var aa = (data).Split('|');
                FeesSubmit stlogin = new FeesSubmit();
                //var gstcomm = aa[15].Split('^');
                string gst = "";
                string commission = "";
                //if (gstcomm.Length > 1)
                //{
                //    commission = gstcomm[0];
                //    gst = gstcomm[1];
                //}

                if (logProperties != null)
                {

                    var other_detail = logProperties.TRANSACTION.CUSTOMVAR.Split(',');
                    string ApplicationNo = other_detail[0];
                    string examtype = other_detail[1];
                    string courseyearid = other_detail[2];
                    stlogin.Requestdata = "";
                    stlogin.dRequestdata = "";
                    stlogin.PGstatus = logProperties.TRANSACTION.TRANSACTIONPAYMENTSTATUS;
                    stlogin.banktrxid = logProperties.TRANSACTION.RRN;
                    stlogin.clienttrxid = logProperties.TRANSACTION.TRANSACTIONID;
                    stlogin.amount = (logProperties.TRANSACTION.AMOUNT);
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
                    var obj = stlogin.FeessubStudent_doubleverification(stlogin);
                    
                    // Message Structure of ‘Push Response’
                    //         Merchant Order Number | SBIePayRefID / ATRN | Transaction Status | Amount | Currency | Pay Mode | OtherDetails | Reason / Message | Bank Code | Bank Reference Number| Transaction Date | Country | CIN | Merchant ID | Total Fee GST| Ref1 | Ref2 | Ref3 | Ref4 | Ref5 | Ref6 | Ref7 | Ref8 | Ref9
                    return "";
                }
                return "";

            }
            catch (Exception ex)
            {
                CommonMethod.WritetoNotePaymentgateway(ex.Message + " Double Verification doubleverification     " + ex.StackTrace, "", HttpContext.Current.Request.Url.AbsolutePath, "admission Paymentgateway Pushresponse url hit error on payment PaymentGateway");
                return "error";
            }
        }
        public string doubleverificationregistration_spot(string data)
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
                var obj = stlogin.FeessubStudent_doubleverification_spot(stlogin);
                CommonMethod.WritetoNotePaymentgateway("Double Verification doubleverification", "Double Verification doubleverification", HttpContext.Current.Request.Url.AbsolutePath, "admission Paymentgateway Pushresponse  url hit payment gatway");

                // Message Structure of ‘Push Response’
                //         Merchant Order Number | SBIePayRefID / ATRN | Transaction Status | Amount | Currency | Pay Mode | OtherDetails | Reason / Message | Bank Code | Bank Reference Number| Transaction Date | Country | CIN | Merchant ID | Total Fee GST| Ref1 | Ref2 | Ref3 | Ref4 | Ref5 | Ref6 | Ref7 | Ref8 | Ref9
                return "";
            }
            catch (Exception ex)
            {
                CommonMethod.WritetoNotePaymentgateway(ex.Message + " Double Verification doubleverification     " + ex.StackTrace, "", HttpContext.Current.Request.Url.AbsolutePath, "admission Paymentgateway Pushresponse url hit error on payment PaymentGateway");
                return "error";
            }
        }
    }



    public class Crc32 : HashAlgorithm
    {
        public const UInt32 DefaultPolynomial = 0xedb88320u; 
    public const UInt32 DefaultSeed = 0xffffffffu;

        private static UInt32[] defaultTable;

        private readonly UInt32 seed;
        private readonly UInt32[] table;
        private UInt32 hash;

        public Crc32()
            : this(DefaultPolynomial, DefaultSeed)
        {
        }

        public Crc32(UInt32 polynomial, UInt32 seed)
        {
            table = InitializeTable(polynomial);
            this.seed = hash = seed;
        }

        public override void Initialize()
        {
            hash = seed;
        }

        protected override void HashCore(byte[] buffer, int start, int length)
        {
            hash = CalculateHash(table, hash, buffer, start, length);
        }

        protected override byte[] HashFinal()
        {
            byte[] hashBuffer = UInt32ToBigEndianBytes(~hash);
            HashValue = hashBuffer;
            return hashBuffer;
        }

        public override int HashSize { get { return 32; } }

        public static UInt32 Compute(byte[] buffer)
        {
            return Compute(DefaultSeed, buffer);
        }

        public static UInt32 Compute(UInt32 seed, byte[] buffer)
        {
            return Compute(DefaultPolynomial, seed, buffer);
        }

        public static UInt32 Compute(UInt32 polynomial, UInt32 seed, byte[] buffer)
        {
            return ~CalculateHash(InitializeTable(polynomial), seed, buffer, 0, buffer.Length);
        }

        private static UInt32[] InitializeTable(UInt32 polynomial)
        {
            if (polynomial == DefaultPolynomial && defaultTable != null)
                return defaultTable;

            UInt32[] createTable = new UInt32[256];
            for (int i = 0; i < 256; i++)
            {
                UInt32 entry = (UInt32)i;
                for (int j = 0; j < 8; j++)
                    if ((entry & 1) == 1)
                        entry = (entry >> 1) ^ polynomial;
                    else
                        entry = entry >> 1;
                createTable[i] = entry;
            }

            if (polynomial == DefaultPolynomial)
                defaultTable = createTable;

            return createTable;
        }

        private static UInt32 CalculateHash(UInt32[] table, UInt32 seed, IList<byte> buffer, int start, int size)
        {
            UInt32 crc = seed;
            for (int i = start; i < size - start; i++)
                crc = (crc >> 8) ^ table[buffer[i] ^ crc & 0xff];
            return crc;
        }

        private static byte[] UInt32ToBigEndianBytes(UInt32 uint32)
        {
            byte[] result = BitConverter.GetBytes(uint32);

            if (BitConverter.IsLittleEndian)
                Array.Reverse(result);

            return result;
        }
    }
}