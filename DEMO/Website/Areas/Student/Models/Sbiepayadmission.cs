using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using com.toml.dp.util;
using System.Text;
using DataLayer;
using System.Web.Configuration;
using System.Data.SqlClient;
using System.Data;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using com.paygate.ag.common.utils;
using System.Xml.Serialization;
using System.IO;

namespace Website.Areas.Student.Models
{
    public class SbiepayAdmission
    {
        public string requestparams { get; set; }
        public string merchantId { get; set; }
        public string EncryptshippingDetais { get; set; }
        public string EncryptbillingDetails { get; set; }
        public string url { get; set; }
        string status { get; set; }



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

        //Liveairpay
        string username = "";
        string password = "";
        string secretKey = "";
        string AirpayMid = "";
        string AirPay_Success_url = "";
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
        //public string salt = "";
        //public string Key = "";
        public string env = "prod";
        //public string EasySuccessUrl = "";
        //public string EasyFailUrl = "";


        //safex LOcal
        public string merchant_id = "";// System.Configuration.ConfigurationSettings.AppSettings["merchant_id"];
        public string ag_id = "Paygate"; //System.Configuration.ConfigurationSettings.AppSettings["ag_id"];
        public string merchant_key = "";// System.Configuration.ConfigurationSettings.AppSettings["merchant_key"];
        public string txn_type = "SALE";// System.Configuration.ConfigurationSettings.AppSettings["txn_type"];
        public string POSTURL = "https://www.avantgardepayments.com/agcore/paymentProcessing";// System.Configuration.ConfigurationSettings.AppSettings["POSTURL"];
        public string SafexPaySuccessUrl = "";// System.Configuration.ConfigurationSettings.AppSettings["SafexPaySuccessUrl"];
        public string SafexPayFailUrl = "";// System.Configuration.ConfigurationSettings.AppSettings["SafexPayFailUrl"];
                                           //Safex End

        string saveurl = "https://www.sbiepay.sbi/secure/AggregatorHostedListener";//live
                                                                                   // string saveurl = "https://test.sbiepay.sbi/secure/AggregatorHostedListener";//local
        string Success_URL = WebConfigurationManager.AppSettings["siteUrl"];
        string Failure_URL = WebConfigurationManager.AppSettings["siteUrl"];
        string Collaborator_Id = "SBIEPAY";
        string Operating_Mode = "DOM";
        string Country = "IN";
        string Currency = "INR";
        string Paymode = "NB";
        string Accesmedium = "ONLINE";
        string TransactionSource = "ONLINE";
        string MerchantCustomerID = "NA";
        int keysize = 128;
        public SbiepayAdmission encriptDataadmission(decimal Amount1, string Other_Details1, string Success_URL1, string Failure_URL1, string cmid11, string ckey11, int collegeid, int courseyearid)
        {
            Success_URL = Success_URL + Success_URL1;
            Failure_URL = Failure_URL + Failure_URL1;
            string Amount = Amount1.ToString();
            string Order_Number = "";
            string Other_Details = Other_Details1;

            DateTime now = DateTime.Now;
            loop:
            Order_Number = CommonMethod.RandomNumber(100000, 999999) + now.ToString("MdHHmmyyssfff");
            //Order_Number = "11342161916181909037";
            string ckey = CommonMethod.MIDcollegewise().Where(x => x.collegeid == collegeid).FirstOrDefault().mkey;
            string cmid = CommonMethod.MIDcollegewise().Where(x => x.collegeid == collegeid).FirstOrDefault().mid;
            StudentLogin stlogin = new StudentLogin();
            AdmissionFeesSubmit fee = new AdmissionFeesSubmit();
            fee.ApplicationNo = Other_Details1;
            fee.clienttrxid = Order_Number;

            fee.mid = cmid;
            fee.Collegeid = collegeid;

            if (courseyearid == Convert.ToInt32(CommonSetting.CourseYearID.BA1st) || courseyearid == Convert.ToInt32(CommonSetting.CourseYearID.bba1sem) || courseyearid == Convert.ToInt32(CommonSetting.CourseYearID.bca1sem) || courseyearid == Convert.ToInt32(CommonSetting.CourseYearID.Bcom1st) || courseyearid == Convert.ToInt32(CommonSetting.CourseYearID.BEDpart1) || courseyearid == Convert.ToInt32(CommonSetting.CourseYearID.Bsc1st) || courseyearid == Convert.ToInt32(CommonSetting.CourseYearID.LLB1sem) || courseyearid == Convert.ToInt32(CommonSetting.CourseYearID.ma1sem) || courseyearid == Convert.ToInt32(CommonSetting.CourseYearID.mcom1sem) || courseyearid == Convert.ToInt32(CommonSetting.CourseYearID.msc1sem) || courseyearid == Convert.ToInt32(CommonSetting.CourseYearID.biotech1sem))
            {
                Other_Details = Other_Details + ",firstyear";
            }
            else
            {
                Other_Details = Other_Details + ",otheryear";
            }
            var obj = fee.FeessubStudentbeforeadmission(fee);
            if (obj.Status == false)
            {
                goto loop;
            }
            //EncryptTrans = Merchant ID|Operating Mode|Merchant Country|Merchant Currency|Posting Amount|Other Details|SuccessURL | Fail URL | Aggregator ID | Merchant Order Number| Merchant Customer ID| Paymode | Access Medium | Transaction Source
            string Requestparameter = cmid + "|" + Operating_Mode + "|" + Country + "|" + Currency + "|" + Amount + "|" + Other_Details + "|" + Success_URL + "|" + Failure_URL + "|" + Collaborator_Id + "|" + Order_Number + "|" + MerchantCustomerID + "|" + Paymode + "|" + Accesmedium + "|" + TransactionSource + "";
            string EncryptedParam = AES128Bit.Encrypt(Requestparameter, ckey, keysize);
            string billingDtls = "";
            string shippingDtls = "";
            SbiepayAdmission sbi = new SbiepayAdmission();
            sbi.url = saveurl;
            sbi.merchantId = cmid;
            sbi.requestparams = EncryptedParam;
            sbi.EncryptbillingDetails = billingDtls;
            sbi.EncryptshippingDetais = shippingDtls;
            CommonMethod.WritetoNotePaymentgateway(EncryptedParam, Requestparameter, HttpContext.Current.Request.Url.AbsolutePath, " Request parameter time ", ClsLanguage.GetCookies("NBApplicationNo"));

            return sbi;
        }

        string privateKey = "";

        public SbiepayAdmission encriptDataadmissionAirPay(decimal Amount1, string Other_Details1, string Success_URL1, string Failure_URL1, string cmid11, string ckey11, int collegeid, int courseyearid)
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
            Success_URL = Success_URL + Success_URL1;
            Failure_URL = Failure_URL + Failure_URL1;
            string Amount = Amount1.ToString();
            string Order_Number = "";
            string Other_Details = Other_Details1;
            DateTime now = DateTime.Now;
            loop:
            Order_Number = CommonMethod.RandomNumber(100000, 999999) + now.ToString("MdHHmmyyssfff");
            //Order_Number = "11342161916181909037";
            string cmid = CommonMethod.MIDcollegewiseAirPay().Where(x => x.collegeid == collegeid).FirstOrDefault().mid;
            string cusername = CommonMethod.MIDcollegewiseAirPay().Where(x => x.collegeid == collegeid).FirstOrDefault().UserName;
            string cpassword = CommonMethod.MIDcollegewiseAirPay().Where(x => x.collegeid == collegeid).FirstOrDefault().Password;
            string ckey = CommonMethod.MIDcollegewiseAirPay().Where(x => x.collegeid == collegeid).FirstOrDefault().mkey;
            StudentLogin stlogin = new StudentLogin();
            AdmissionFeesSubmit fee = new AdmissionFeesSubmit();
            SbiepayAdmission sbi = new SbiepayAdmission();
            fee.ApplicationNo = Other_Details1;
            fee.clienttrxid = Order_Number;
            fee.GetwayType = "AirPay";
            fee.mid = cmid;
            fee.Collegeid = collegeid;
            fee.amount = Amount1;
            if (courseyearid == Convert.ToInt32(CommonSetting.CourseYearID.BA1st) || courseyearid == Convert.ToInt32(CommonSetting.CourseYearID.bba1sem) || courseyearid == Convert.ToInt32(CommonSetting.CourseYearID.bca1sem) || courseyearid == Convert.ToInt32(CommonSetting.CourseYearID.Bcom1st) || courseyearid == Convert.ToInt32(CommonSetting.CourseYearID.BEDpart1) || courseyearid == Convert.ToInt32(CommonSetting.CourseYearID.Bsc1st) || courseyearid == Convert.ToInt32(CommonSetting.CourseYearID.LLB1sem) || courseyearid == Convert.ToInt32(CommonSetting.CourseYearID.ma1sem) || courseyearid == Convert.ToInt32(CommonSetting.CourseYearID.mcom1sem) || courseyearid == Convert.ToInt32(CommonSetting.CourseYearID.msc1sem) || courseyearid == Convert.ToInt32(CommonSetting.CourseYearID.biotech1sem))
            {


                Other_Details = Other_Details + ",firstyear";
            }
            else
            {
                Other_Details = Other_Details + ",otheryear";
            }

            var obj = fee.FeessubStudentbeforeadmission(fee);
            if (obj.Status == false)
            {
                goto loop;
            }
            //EncryptTrans = Merchant ID|Operating Mode|Merchant Country|Merchant Currency|Posting Amount|Other Details|SuccessURL | Fail URL | Aggregator ID | Merchant Order Number| Merchant Customer ID| Paymode | Access Medium | Transaction Source
            int StID = (ClsLanguage.GetCookies("NBStID") != null ? Convert.ToInt32(ClsLanguage.GetCookies("NBStID")) : 0);
            int Sission = (ClsLanguage.GetCookies("NBSission") != null ? Convert.ToInt32(ClsLanguage.GetCookies("NBSission")) : 0);
            Other_Details = Other_Details + "," + 1 + ", Admission," + Order_Number + "," + StID + ","+ Sission+","; 
            username = cusername;
            password = cpassword;
            secretKey = ckey;
            AirpayMid = cmid;
           // Order_Number = CommonMethod.RandomNumber(100000, 999999) + now.ToString("MdHHmmyyssfff");
            // AirPay_Success_url = "http://localhost:33166/StudentPG/homePG/AirPayPGSucessAdmission";//Local
            AirPay_Success_url = "";//Live
            privateKey = secretKey;          
            allParamValue = sEmail + sFName + sLName + sAddress + sCity + sState + sCountry + Amount + Order_Number;
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


        public SbiepayAdmission encriptDataadmissionEaseBuzz(decimal Amount1, string Other_Details1, string Success_URL1, string Failure_URL1, string cmid11, string ckey11, int collegeid, int courseyearid)
        {

            string ckey = CommonMethod.MIDcollegewiseEaseBuzz().Where(x => x.collegeid == collegeid).FirstOrDefault().mkey;
            string ESalt = CommonMethod.MIDcollegewiseEaseBuzz().Where(x => x.collegeid == collegeid).FirstOrDefault().Salt;
            string Mid = CommonMethod.MIDcollegewiseEaseBuzz().Where(x => x.collegeid == collegeid).FirstOrDefault().mid;
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
            string productinfo = "Admission Fees";
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

            SbiepayAdmission sbi = new SbiepayAdmission();
            //return sbi;
            Success_URL = Success_URL1;
            Failure_URL =Failure_URL1;
            string Amount = Amount1.ToString();
            string Order_Number = "";
            string Other_Details = Other_Details1;
            DateTime now = DateTime.Now;
            loop:
            Order_Number = CommonMethod.RandomNumber(100000, 999999) + now.ToString("MdHHmmyyssfff");
            StudentLogin stlogin = new StudentLogin();
            AdmissionFeesSubmit fee = new AdmissionFeesSubmit();
         
            fee.ApplicationNo = Other_Details1;
            fee.clienttrxid = Order_Number;
            fee.GetwayType = "EaseBuzz";
            fee.mid = Mid;
            fee.Collegeid = collegeid;


            string YearType = "";
            //  Order_Number = "";
            var obj = fee.FeessubStudentbeforeadmission(fee);
            if (obj.Status == false)
            {
                goto loop;
            }
            if (courseyearid == Convert.ToInt32(CommonSetting.CourseYearID.BA1st) || courseyearid == Convert.ToInt32(CommonSetting.CourseYearID.bba1sem) || courseyearid == Convert.ToInt32(CommonSetting.CourseYearID.bca1sem) || courseyearid == Convert.ToInt32(CommonSetting.CourseYearID.Bcom1st) || courseyearid == Convert.ToInt32(CommonSetting.CourseYearID.BEDpart1) || courseyearid == Convert.ToInt32(CommonSetting.CourseYearID.Bsc1st) || courseyearid == Convert.ToInt32(CommonSetting.CourseYearID.LLB1sem) || courseyearid == Convert.ToInt32(CommonSetting.CourseYearID.ma1sem) || courseyearid == Convert.ToInt32(CommonSetting.CourseYearID.mcom1sem) || courseyearid == Convert.ToInt32(CommonSetting.CourseYearID.msc1sem) || courseyearid == Convert.ToInt32(CommonSetting.CourseYearID.biotech1sem))
            {
                YearType = "firstyear";
                Other_Details = Other_Details + ",firstyear";
            }
            else
            {
                YearType = "otheryear";
                Other_Details = Other_Details + ",otheryear";
            }

            int StID = (ClsLanguage.GetCookies("NBStID") != null ? Convert.ToInt32(ClsLanguage.GetCookies("NBStID")) : 0);
            int Sission = (ClsLanguage.GetCookies("NBSission") != null ? Convert.ToInt32(ClsLanguage.GetCookies("NBSission")) : 0);
            Other_Details = Other_Details + "," + 1 + ", Admission," + Order_Number + "," + StID + "," + Sission + ",";

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
            data.Add("key", ckey);
            //string AmountForm = Convert.ToDecimal(amount.Trim()).ToString("g29");// eliminating trailing zeros  
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
            sbi.Eeasebuzz_action_url = saveurl;
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




        public SbiepayAdmission encriptDataadmissionSafex(decimal Amount1, string Other_Details1, string Success_URL1, string Failure_URL1, string cmid11, string ckey11, int collegeid, int courseyearid)
        {
            StudentLogin stu = new StudentLogin();
            string ApplicationID = ClsLanguage.GetCookies("NBApplicationNo");
            Login lo = stu.BasicDetail(ApplicationID);
            SbiepayAdmission sbi = new SbiepayAdmission();
            string Amount = Amount1.ToString();
            string Order_Number = "";
            string Other_Details = Other_Details1;
            string Applicationno = Other_Details1;

            DateTime now = DateTime.Now;
            loop:
            Order_Number = CommonMethod.RandomNumber(100000, 999999) + now.ToString("MdHHmmyyssfff");
            StudentLogin stlogin = new StudentLogin();
            AdmissionFeesSubmit fee = new AdmissionFeesSubmit();

            merchant_id = CommonMethod.MIDcollegewiseSafex().Where(x => x.collegeid == collegeid).FirstOrDefault().mid;
            merchant_key = CommonMethod.MIDcollegewiseSafex().Where(x => x.collegeid == collegeid).FirstOrDefault().mkey;

            //AdmissionFeesSubmit fee = new AdmissionFeesSubmit();
            //SbiepayAdmission sbi = new SbiepayAdmission();
            fee.ApplicationNo = Other_Details1;
            fee.clienttrxid = Order_Number;
            fee.GetwayType = "Safex";
            fee.mid = merchant_id;
            fee.Collegeid = collegeid;

            if (courseyearid == Convert.ToInt32(CommonSetting.CourseYearID.BA1st) || courseyearid == Convert.ToInt32(CommonSetting.CourseYearID.bba1sem) || courseyearid == Convert.ToInt32(CommonSetting.CourseYearID.bca1sem) || courseyearid == Convert.ToInt32(CommonSetting.CourseYearID.Bcom1st) || courseyearid == Convert.ToInt32(CommonSetting.CourseYearID.BEDpart1) || courseyearid == Convert.ToInt32(CommonSetting.CourseYearID.Bsc1st) || courseyearid == Convert.ToInt32(CommonSetting.CourseYearID.LLB1sem) || courseyearid == Convert.ToInt32(CommonSetting.CourseYearID.ma1sem) || courseyearid == Convert.ToInt32(CommonSetting.CourseYearID.mcom1sem) || courseyearid == Convert.ToInt32(CommonSetting.CourseYearID.msc1sem) || courseyearid == Convert.ToInt32(CommonSetting.CourseYearID.biotech1sem))
            {


                Other_Details = Other_Details + ",firstyear";
            }
            else
            {
                Other_Details = Other_Details + ",otheryear";
            }

            string YearType = "";
            //  Order_Number = "";
            var obj = fee.FeessubStudentbeforeadmission(fee);
            if (obj.Status == false)
            {
                goto loop;
            }
           
            
            int Sission = (ClsLanguage.GetCookies("NBSission") != null ? Convert.ToInt32(ClsLanguage.GetCookies("NBSission")) : 0);
            int StID = (ClsLanguage.GetCookies("NBStID") != null ? Convert.ToInt32(ClsLanguage.GetCookies("NBStID")) : 0);
            string txnagId = "Paygate";
            string txnmerchantId = merchant_id;
            string txnmerchantKey = merchant_key;
            string txnorderNumber = Order_Number;
            string txnAmount = Amount1.ToString();
            string txnCountry = "IND";
            string txnCountryCurrency = "INR";
            string txnType = txn_type;
            string txnsuccessUrl = Success_URL1+ "?CollegeId=" + collegeid;
            string txnfailureUrl = Success_URL1 + "?CollegeId=" + collegeid;
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
            string udf1 = Applicationno;
            string udf2 = YearType;
            string udf3 = Order_Number;
            string udf4 = StID.ToString();
            string udf5 = Sission.ToString(); 
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
            string enc_request = encrypt(request, merchant_key);
            string hashing = ComputeSha256Hash(Hash);
            string enc_hash = encrypt(hashing, merchant_key);
            sbi.Safex_me_id = txnmerchantId; // As String
            sbi.Safex_merchant_request = enc_request;
            sbi.Safex_hash = enc_hash;
            sbi.Safex_POSTURL = POSTURL;
            return sbi;
        }


        public SbiepayAdmission encriptDataEaseBuzz(decimal Amount1, string Other_Details1, string Success_URL1, string Failure_URL1, string cmid11, string ckey11, int collegeid, int courseyearid)
        {
            string ckey = CommonMethod.MIDcollegewiseEaseBuzz().Where(x => x.collegeid == collegeid).FirstOrDefault().mkey;
            string ESalt = CommonMethod.MIDcollegewiseEaseBuzz().Where(x => x.collegeid == collegeid).FirstOrDefault().Salt;
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
            string productinfo = "Admission Fees";
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

            SbiepayAdmission sbi = new SbiepayAdmission();
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
            fee.amount = Amount1.ToString();
            string YearType = "";
            //  Order_Number = "";
            var obj = fee.FeessubStudentbefore(fee);
            if (obj.Status == false)
            {
                goto loop;
            }
            if (courseyearid == Convert.ToInt32(CommonSetting.CourseYearID.BA1st) || courseyearid == Convert.ToInt32(CommonSetting.CourseYearID.bba1sem) || courseyearid == Convert.ToInt32(CommonSetting.CourseYearID.bca1sem) || courseyearid == Convert.ToInt32(CommonSetting.CourseYearID.Bcom1st) || courseyearid == Convert.ToInt32(CommonSetting.CourseYearID.BEDpart1) || courseyearid == Convert.ToInt32(CommonSetting.CourseYearID.Bsc1st) || courseyearid == Convert.ToInt32(CommonSetting.CourseYearID.LLB1sem) || courseyearid == Convert.ToInt32(CommonSetting.CourseYearID.ma1sem) || courseyearid == Convert.ToInt32(CommonSetting.CourseYearID.mcom1sem) || courseyearid == Convert.ToInt32(CommonSetting.CourseYearID.msc1sem) || courseyearid == Convert.ToInt32(CommonSetting.CourseYearID.biotech1sem))
            {
                YearType = "firstyear";
                Other_Details = Other_Details + ",firstyear";
            }
            else
            {
                YearType = "otheryear";
                Other_Details = Other_Details + ",otheryear";
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
            data.Add("key", ckey);
            //string AmountForm = Convert.ToDecimal(amount.Trim()).ToString("g29");// eliminating trailing zeros
            int StID = (ClsLanguage.GetCookies("NBStID") != null ? Convert.ToInt32(ClsLanguage.GetCookies("NBStID")) : 0);
            int Sission = (ClsLanguage.GetCookies("NBSission") != null ? Convert.ToInt32(ClsLanguage.GetCookies("NBSission")) : 0);
            data.Add("amount", amount);
            data.Add("firstname", firstname.Trim());
            data.Add("email", email.Trim());
            data.Add("phone", phone.Trim());
            data.Add("productinfo", productinfo.Trim());
            data.Add("surl", "");
            data.Add("furl", "");
            data.Add("udf1", Other_Details1.Trim());
            data.Add("udf2", Order_Number.Trim() );
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
            sbi.Eeasebuzz_action_url = saveurl;
            sbi.Esurl = "";
            sbi.Efurl = "";
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


        public string encrypt(string plainText,string pkey)
        {
            string va = string.Empty;
            privateKey = pkey;
            string encryptText = PayGateCryptoUtils.encrypt(plainText, privateKey);
            // string encryptText = encrypt(plainText, privateKey);
            return encryptText;
        }
        public string decrypt(string encryptText, string pkey)
        {
            privateKey = pkey;
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
            public string encrypt(string plainText ,string privateKey)
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




        public AdmissionFeesSubmit Feeeadmission(string ApplicationNo, decimal amount)
        {
            AdmissionFeesSubmit stlogin = new AdmissionFeesSubmit();
            try
            {

                stlogin.ApplicationNo = ApplicationNo;

                stlogin.amount = amount;
                stlogin.PGstatus = "Success";
                var obj = stlogin.FeessubStudentbeforeadmissionFree(stlogin);
                return obj;
            }
            catch (Exception ex)
            {
                CommonMethod.WritetoNotePaymentgateway(ex.Message + "      " + ex.StackTrace, "", HttpContext.Current.Request.Url.AbsolutePath, "pgsucessdecrypt() url hit error on payment PaymentGateway", ClsLanguage.GetCookies("NBApplicationNo"));
                return stlogin;
            }
        }



        //public AdmissionFeesSubmit CheckAirPayVeification(string ApplicationNo, int courseyearId=0)
        //{
        //    AdmissionFeesSubmit stlogin = new AdmissionFeesSubmit();
        //    try
        //    {

        //        stlogin.ApplicationNo = ApplicationNo;
        //        stlogin.PGstatus = "AirPay";
        //        stlogin.coursecategoryid = courseyearId;
        //        var obj = stlogin.GetDoubleVerificationStudent(stlogin);
        //        return obj;
        //    }
        //    catch (Exception ex)
        //    {
        //        CommonMethod.WritetoNotePaymentgateway(ex.Message + "      " + ex.StackTrace, "", HttpContext.Current.Request.Url.AbsolutePath, "pgsucessdecrypt() url hit error on payment PaymentGateway", ClsLanguage.GetCookies("NBApplicationNo"));
        //        return stlogin;
        //    }
        //}

        public string pgsucessdecryptadmission(int sid, string ckey)
        {
            try
            {
                string encData = HttpContext.Current.Request.Form["encData"];
                //CommonMethod.WritetoNotePaymentgateway(encData, "", HttpContext.Current.Request.Url.AbsolutePath, "admission pgsucessdecrypt url hit payment gatway", ClsLanguage.GetCookies("NBApplicationNo"));
                string encDatadecryptdata = AES128Bit.Decrypt(encData, ckey, keysize);
                var aa = encDatadecryptdata.Split('|');
                CommonMethod.WritetoNotePaymentgateway(encData, encDatadecryptdata, HttpContext.Current.Request.Url.AbsolutePath, " admission pgsucessdecrypt url hit payment gatway", ClsLanguage.GetCookies("NBApplicationNo"));
                AdmissionFeesSubmit stlogin = new AdmissionFeesSubmit();
                AdmissionFeesSubmit obj = new AdmissionFeesSubmit();
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
                stlogin.amount = Convert.ToDecimal(aa[3]);
                stlogin.feeamount = "";
                stlogin.gst = gst;
                stlogin.commission = commission;
                stlogin.paymode = aa[5];
                stlogin.banktxndate = aa[10];
                stlogin.Reason = aa[7];
                stlogin.apitxnid = aa[1];
                // stlogin.ApplicationNo = aa[6];
                var other_detail = aa[6].Split(',');
                stlogin.ApplicationNo = other_detail[0];
                
                if (other_detail[1] == "otheryear")
                {

                    obj = stlogin.FeessubStudentadmission_otheryear(stlogin);
                }
                else
                {
                    obj = stlogin.FeessubStudentadmission(stlogin);
                }

                //Sample Response for a ‘Successful’ Transaction:
                //Merchant Order Number|SBIePayRefID/ATRN|Transaction Status|Amount|Currency|Pay Mode|Other    Details | Reason / Message | Bank Code | Bank Reference Number| Transaction Date | Country | CIN | Merchant ID | Total Fee GST| Ref1 | Ref2 | Ref3 | Ref4 | Ref5 | Ref6 | Ref7 | Ref8 | Ref9
                // encData=CH8809800|4430840943731|SUCCESS|100|INR|IMPS|ABC^DEF^ERD|Transaction Successful|NA|G1312423|2018-06-24  16:30:24 | IN | 10000032018062412345 | 1000003 | 10.00 ^ 1.80 |||||||||
                //Sample Response for a ‘Pending’ Transaction:
                //encData=CH8809800|4430840943731|PENDING|100|INR|IMPS|ABC^DEF^ERD|Pending for authorization|NA|G1312423|2018-06-24 16:30:24 | IN | 10000032018062412345 | 1000003 | 10.00 ^ 1.80 ||||||||
                //Sample Response for a ‘Failed’ Transaction:
                //encData = CH8809800 | 4430840943731 | FAIL or FAILURE | 100 | INR | IMPS | ABC ^ DEF ^ ERD | Invalid GatewayPostingAmount | NA | G1312423 | 2018 - 06  - 24 16:30:24 | IN | NA | 1000003 | 10.00 ^ 1.80 |||||||||
                //if (obj.Id == 0)
                //{
                paymentgatewayemailadmission(other_detail[0]);
                //}
                return encDatadecryptdata;
            }
            catch (Exception ex)
            {
                CommonMethod.WritetoNotePaymentgateway(ex.Message + "      " + ex.StackTrace, "", HttpContext.Current.Request.Url.AbsolutePath, "pgsucessdecrypt() url hit error on payment PaymentGateway", ClsLanguage.GetCookies("NBApplicationNo"));
                return "error";
            }
        }


       
        public string AirPaypgsucessdecrypt(int sid, string banktrxid, string clienttrxid, string amount1, string feeamount, string gst, string commission, string paymode, string banktxndate, string Reason, string apitxnid, string ApplicationNo, string courseyearid, string AdmissionType, string encData, string encDatadecryptdata, string PGstatus ,string Sessionid)
        {
            try
            {
           
                AdmissionFeesSubmit stlogin = new AdmissionFeesSubmit();
                AdmissionFeesSubmit obj = new AdmissionFeesSubmit();
                stlogin.Requestdata = encData;
                stlogin.dRequestdata = encDatadecryptdata;
                stlogin.PGstatus = PGstatus;
                gst = gst;
                commission = commission;
          
                stlogin.banktrxid = banktrxid;
                stlogin.clienttrxid = clienttrxid;
                stlogin.amount =  Convert.ToDecimal(amount1);
                stlogin.feeamount = feeamount;
                stlogin.gst = gst;
                stlogin.commission = commission;
                stlogin.paymode = paymode;
                stlogin.banktxndate = banktxndate;
                stlogin.Reason = Reason;
                stlogin.apitxnid = apitxnid;
                //var other_detail = aa[6].Split(',');
                stlogin.ApplicationNo = ApplicationNo;
                if (Sessionid == "")
                {
                    Sessionid = "0";
                }
                stlogin.sessionid = Convert.ToInt32( Sessionid);

                if (AdmissionType == "otheryear")
                {

                    obj = stlogin.FeessubStudentadmission_otheryear_OtherGetway(stlogin);
                }
                else
                {
                    obj = stlogin.FeessubStudentadmissionOtherGetway(stlogin);
                }
                if (obj.Id == 0)
                {
                    paymentgatewayemailadmission(ApplicationNo);
                }
                return PGstatus;
            }
            catch (Exception ex)
            {
                return "error";
            }
        }

        public string pgfaileddecryptadmission(int sid, string ckey)
        {
            try
            {

                string encData = HttpContext.Current.Request.Form["encData"];
                // CommonMethod.WritetoNotePaymentgateway(encData, "", HttpContext.Current.Request.Url.AbsolutePath, "admission pgsucessdecrypt url hit payment gatway", ClsLanguage.GetCookies("NBApplicationNo"));
                string encDatadecryptdata = AES128Bit.Decrypt(encData, ckey, keysize);
                //string encDatadecryptdata = "CH8809800|4430840943731|SUCCESS|100|INR|IMPS|ABC^DEF^ERD|Transaction Successful|NA|G1312423|2018-06-24  16:30:24 | IN | 10000032018062412345 | 1000003 | 10.00 ^ 1.80 ||||||||||";
                var aa = encDatadecryptdata.Split('|');
                CommonMethod.WritetoNotePaymentgateway(encData, encDatadecryptdata, HttpContext.Current.Request.Url.AbsolutePath, "admission url hit payment PaymentGateway", ClsLanguage.GetCookies("NBApplicationNo"));
                AdmissionFeesSubmit stlogin = new AdmissionFeesSubmit();
                AdmissionFeesSubmit obj = new AdmissionFeesSubmit();
                stlogin.Requestdata = encData;
                stlogin.dRequestdata = encDatadecryptdata;
                stlogin.PGstatus = aa[2];// "success";//
                stlogin.banktrxid = aa[9];
                stlogin.clienttrxid = aa[0];
                stlogin.amount = Convert.ToDecimal(aa[3]);
                stlogin.feeamount = "";
                stlogin.gst = "";
                stlogin.commission = "";
                stlogin.paymode = aa[5];
                stlogin.banktxndate = aa[10];
                stlogin.Reason = aa[7];
                stlogin.apitxnid = aa[1];
                //stlogin.ApplicationNo = aa[6];
                //var obj = stlogin.FeessubStudentadmission(stlogin);
                var other_detail = aa[6].Split(',');
                stlogin.ApplicationNo = other_detail[0];
                if (other_detail[1] == "otheryear")
                {

                    obj = stlogin.FeessubStudentadmission_otheryear(stlogin);
                }
                else
                {
                    obj = stlogin.FeessubStudentadmission(stlogin);
                }
                if (obj.Id == 0)
                {
                    paymentgatewayemailadmission(other_detail[0]);
                }
                return encDatadecryptdata;
            }
            catch (Exception ex)
            {
                CommonMethod.WritetoNotePaymentgateway(ex.Message + "    " + ex.StackTrace, "", HttpContext.Current.Request.Url.AbsolutePath, "pgfaileddecrypt() url hit error on payment PaymentGateway", ClsLanguage.GetCookies("NBApplicationNo"));
                return "error";
            }
        }
        public void paymentgatewayemailadmission(string applicationno)
        {
            // return;
            AdmissionFeesSubmit obj = new AdmissionFeesSubmit();
            AdmissionFeesSubmit stlogin = new AdmissionFeesSubmit();
            List<AdmissionFeesSubmit> feestruckture = new List<AdmissionFeesSubmit>();
            //obj = stlogin.FeesDetailsapplicationno(applicationno);
            //feestruckture = stlogin.FeesDetailsstructure(obj.Collegeid, obj.coursecategoryid, obj.sessionid);
            //Email.SendEmailDynamic(obj.email, obj.CollegeName, obj.name,obj.FeeStatus, "Admission Fees Acknowledgement !", "Admission Fees Acknowledgement ", obj.banktxndate, obj.banktrxid, obj.clienttrxid, obj.ApplicationNo, obj.Fees, obj.PaymentType, obj.mobileno,obj.surcharge.ToString(),(Convert.ToDecimal(obj.Fees)+Convert.ToDecimal(obj.surcharge)).ToString());
            //SendEmailDynamic(string email, string collegename, string name, string Status, string mailheading, string subject, string trxdate, string banktrxid, string TransactionId, string ApplicationNo, string Fees, string PaymentType, string mobileno, string surcharge, string totalfee)


        }
        public string Pushresponseradmission(string ckey)
        {
            try
            {
                //string ckey = CommonMethod.MIDcollegewise().Where(x=> x.collegeid==1).FirstOrDefault().mkey;
                string encData = HttpContext.Current.Request.Form["pushRespData"];
                string encDatadecryptdata = AES128Bit.Decrypt(encData, ckey, keysize);
                var aa = encDatadecryptdata.Split('|');
                AdmissionFeesSubmit stlogin = new AdmissionFeesSubmit();
                AdmissionFeesSubmit obj = new AdmissionFeesSubmit();
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
                stlogin.amount = Convert.ToDecimal(aa[3]);
                stlogin.feeamount = "";
                stlogin.gst = gst;
                stlogin.commission = commission;
                stlogin.paymode = aa[5];
                stlogin.banktxndate = aa[10];
                stlogin.Reason = aa[7];
                stlogin.apitxnid = aa[1];
                //stlogin.ApplicationNo = aa[6];
                var other_detail = aa[6].Split(',');
                stlogin.ApplicationNo = other_detail[0];
                if (other_detail[1] == "otheryear")
                {

                    obj = stlogin.FeessubStudentadmissionPushresponse_otheryear(stlogin);
                }
                else
                {
                    obj = stlogin.FeessubStudentadmissionPushresponse(stlogin);
                }

                CommonMethod.WritetoNotePaymentgateway(encData, encDatadecryptdata, HttpContext.Current.Request.Url.AbsolutePath, "admission Paymentgateway Pushresponse  url hit payment gatway");
                if (obj.Id == 0)
                {
                    paymentgatewayemailadmission(other_detail[0]);
                }

                // Message Structure of ‘Push Response’
                //         Merchant Order Number | SBIePayRefID / ATRN | Transaction Status | Amount | Currency | Pay Mode | OtherDetails | Reason / Message | Bank Code | Bank Reference Number| Transaction Date | Country | CIN | Merchant ID | Total Fee GST| Ref1 | Ref2 | Ref3 | Ref4 | Ref5 | Ref6 | Ref7 | Ref8 | Ref9
                return encDatadecryptdata;
            }
            catch (Exception ex)
            {
                CommonMethod.WritetoNotePaymentgateway(ex.Message + "      " + ex.StackTrace, "", HttpContext.Current.Request.Url.AbsolutePath, "admission Paymentgateway Pushresponse url hit error on payment PaymentGateway");
                return "error";
            }
        }

        public string doubleverificationAdmissionAirpay(string data)
        {
            try
            {
                AdmissionFeesSubmit obj = new AdmissionFeesSubmit();
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
                AdmissionFeesSubmit stlogin = new AdmissionFeesSubmit();
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
                    stlogin.amount =Convert.ToDecimal((logProperties.TRANSACTION.AMOUNT));
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
                    if (other_detail[1] == "otheryear")
                    {

                        obj = stlogin.FeessubStudentadmissionDoubleverification_otheryear(stlogin);
                    }
                    else
                    {
                        obj = stlogin.FeessubStudentadmissionDoubleverification(stlogin);
                    }
                    //CommonMethod.WritetoNotePaymentgateway("Double Verification", "Double Verification", HttpContext.Current.Request.Url.AbsolutePath, "admission Paymentgateway Pushresponse  url hit payment gatway");
                    if (obj.Id == 0)
                    {
                        paymentgatewayemailadmission(other_detail[0]);
                    }
                    // Message Structure of ‘Push Response’
                    //         Merchant Order Number | SBIePayRefID / ATRN | Transaction Status | Amount | Currency | Pay Mode | OtherDetails | Reason / Message | Bank Code | Bank Reference Number| Transaction Date | Country | CIN | Merchant ID | Total Fee GST| Ref1 | Ref2 | Ref3 | Ref4 | Ref5 | Ref6 | Ref7 | Ref8 | Ref9
                    return "";
                }
                return "";

            }
            catch (Exception ex)
            {
                CommonMethod.WritetoNotePaymentgateway(ex.Message + " Double Verification Admission     " + ex.StackTrace, "", HttpContext.Current.Request.Url.AbsolutePath, "admission Paymentgateway Pushresponse url hit error on payment PaymentGateway");
                return "error";
            }
        }

        public string doubleverification(string data)
        {
            try
            {
                //string ckey = CommonMethod.MIDcollegewise().Where(x=> x.collegeid==1).FirstOrDefault().mkey;
                //string encData = HttpContext.Current.Request.Form["pushRespData"];
                //string encDatadecryptdata = AES128Bit.Decrypt(encData, ckey, keysize);
                var aa = (data).Split('|');
                AdmissionFeesSubmit stlogin = new AdmissionFeesSubmit();
                AdmissionFeesSubmit obj = new AdmissionFeesSubmit();
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
                stlogin.amount = Convert.ToDecimal(aa[7]);
                stlogin.feeamount = "";
                stlogin.gst = gst;
                stlogin.commission = commission;
                stlogin.paymode = aa[12];
                stlogin.banktxndate = aa[11];
                stlogin.Reason = aa[8];
                stlogin.apitxnid = aa[1];
                //stlogin.ApplicationNo = aa[5];
                //var obj = stlogin.FeessubStudentadmissionDoubleverification(stlogin);
                var other_detail = aa[5].Split(',');
                stlogin.ApplicationNo = other_detail[0];
                if (other_detail[1] == "otheryear")
                {

                    obj = stlogin.FeessubStudentadmissionDoubleverification_otheryear(stlogin);
                }
                else
                {
                    obj = stlogin.FeessubStudentadmissionDoubleverification(stlogin);
                }
                CommonMethod.WritetoNotePaymentgateway("Double Verification", "Double Verification", HttpContext.Current.Request.Url.AbsolutePath, "admission Paymentgateway Pushresponse  url hit payment gatway");
                if (obj.Id == 0)
                {
                    paymentgatewayemailadmission(other_detail[0]);
                }

                // Message Structure of ‘Push Response’
                //         Merchant Order Number | SBIePayRefID / ATRN | Transaction Status | Amount | Currency | Pay Mode | OtherDetails | Reason / Message | Bank Code | Bank Reference Number| Transaction Date | Country | CIN | Merchant ID | Total Fee GST| Ref1 | Ref2 | Ref3 | Ref4 | Ref5 | Ref6 | Ref7 | Ref8 | Ref9
                return "";
            }
            catch (Exception ex)
            {
                CommonMethod.WritetoNotePaymentgateway(ex.Message + "      " + ex.StackTrace, "", HttpContext.Current.Request.Url.AbsolutePath, "admission Paymentgateway Pushresponse url hit error on payment PaymentGateway");
                return "error";
            }
        }
        //public string PushresponserJRSCOLLEGEJAMALPUR()
        //{
        //    try
        //    {

        //        string ckey = CommonMethod.MIDcollegewise().Where(x => x.collegeid == 2).FirstOrDefault().mkey;
        //        string encData = HttpContext.Current.Request.Form["pushRespData"];
        //        string encDatadecryptdata = AES128Bit.Decrypt(encData, ckey, keysize);
        //        var aa = encDatadecryptdata.Split('|');
        //        AdmissionFeesSubmit stlogin = new AdmissionFeesSubmit();
        //        var gstcomm = aa[14].Split('^');
        //        string gst = "";
        //        string commission = "";
        //        if (gstcomm.Length > 1)
        //        {
        //            commission = gstcomm[0];
        //            gst = gstcomm[1];
        //        }
        //        stlogin.Requestdata = encData;
        //        stlogin.dRequestdata = encDatadecryptdata;
        //        stlogin.PGstatus = aa[2];
        //        stlogin.banktrxid = aa[9];
        //        stlogin.clienttrxid = aa[0];
        //        stlogin.amount = Convert.ToDecimal(aa[3]);
        //        stlogin.feeamount = "";
        //        stlogin.gst = gst;
        //        stlogin.commission = commission;
        //        stlogin.paymode = aa[5];
        //        stlogin.banktxndate = aa[10];
        //        stlogin.Reason = aa[7];
        //        stlogin.apitxnid = aa[1];
        //        stlogin.ApplicationNo = aa[6];

        //        var obj = stlogin.FeessubStudentadmissionPushresponse(stlogin);
        //        CommonMethod.WritetoNotePaymentgateway(encData, encDatadecryptdata, HttpContext.Current.Request.Url.AbsolutePath, "admission Paymentgateway Pushresponse  url hit payment gatway");
        //        if (obj.Id == 0)
        //        {
        //            paymentgatewayemailadmission(aa[6]);
        //        }

        //        // Message Structure of ‘Push Response’
        //        //         Merchant Order Number | SBIePayRefID / ATRN | Transaction Status | Amount | Currency | Pay Mode | OtherDetails | Reason / Message | Bank Code | Bank Reference Number| Transaction Date | Country | CIN | Merchant ID | Total Fee GST| Ref1 | Ref2 | Ref3 | Ref4 | Ref5 | Ref6 | Ref7 | Ref8 | Ref9
        //        return encDatadecryptdata;
        //    }
        //    catch (Exception ex)
        //    {
        //        CommonMethod.WritetoNotePaymentgateway(ex.Message + "      " + ex.StackTrace, "", HttpContext.Current.Request.Url.AbsolutePath, "admission Paymentgateway Pushresponse url hit error on payment PaymentGateway");
        //        return "error";
        //    }
        //}
        //public string PushresponserKOSHICOLLEGEKHAGARIA()
        //{
        //    try
        //    {

        //        string ckey = CommonMethod.MIDcollegewise().Where(x => x.collegeid == 3).FirstOrDefault().mkey;
        //        string encData = HttpContext.Current.Request.Form["pushRespData"];
        //        string encDatadecryptdata = AES128Bit.Decrypt(encData, ckey, keysize);
        //        var aa = encDatadecryptdata.Split('|');
        //        AdmissionFeesSubmit stlogin = new AdmissionFeesSubmit();
        //        var gstcomm = aa[14].Split('^');
        //        string gst = "";
        //        string commission = "";
        //        if (gstcomm.Length > 1)
        //        {
        //            commission = gstcomm[0];
        //            gst = gstcomm[1];
        //        }
        //        stlogin.Requestdata = encData;
        //        stlogin.dRequestdata = encDatadecryptdata;
        //        stlogin.PGstatus = aa[2];
        //        stlogin.banktrxid = aa[9];
        //        stlogin.clienttrxid = aa[0];
        //        stlogin.amount = Convert.ToDecimal(aa[3]);
        //        stlogin.feeamount = "";
        //        stlogin.gst = gst;
        //        stlogin.commission = commission;
        //        stlogin.paymode = aa[5];
        //        stlogin.banktxndate = aa[10];
        //        stlogin.Reason = aa[7];
        //        stlogin.apitxnid = aa[1];
        //        stlogin.ApplicationNo = aa[6];

        //        var obj = stlogin.FeessubStudentadmissionPushresponse(stlogin);
        //        CommonMethod.WritetoNotePaymentgateway(encData, encDatadecryptdata, HttpContext.Current.Request.Url.AbsolutePath, "admission Paymentgateway Pushresponse  url hit payment gatway");
        //        if (obj.Id == 0)
        //        {
        //            paymentgatewayemailadmission(aa[6]);
        //        }

        //        // Message Structure of ‘Push Response’
        //        //         Merchant Order Number | SBIePayRefID / ATRN | Transaction Status | Amount | Currency | Pay Mode | OtherDetails | Reason / Message | Bank Code | Bank Reference Number| Transaction Date | Country | CIN | Merchant ID | Total Fee GST| Ref1 | Ref2 | Ref3 | Ref4 | Ref5 | Ref6 | Ref7 | Ref8 | Ref9
        //        return encDatadecryptdata;
        //    }
        //    catch (Exception ex)
        //    {
        //        CommonMethod.WritetoNotePaymentgateway(ex.Message + "      " + ex.StackTrace, "", HttpContext.Current.Request.Url.AbsolutePath, "admission Paymentgateway Pushresponse url hit error on payment PaymentGateway");
        //        return "error";
        //    }
        //}
        //public string PushresponserKKMCOLLEGEJAMUI()
        //{
        //    try
        //    {

        //        string ckey = CommonMethod.MIDcollegewise().Where(x => x.collegeid == 3).FirstOrDefault().mkey;
        //        string encData = HttpContext.Current.Request.Form["pushRespData"];
        //        string encDatadecryptdata = AES128Bit.Decrypt(encData, ckey, keysize);
        //        var aa = encDatadecryptdata.Split('|');
        //        AdmissionFeesSubmit stlogin = new AdmissionFeesSubmit();
        //        var gstcomm = aa[14].Split('^');
        //        string gst = "";
        //        string commission = "";
        //        if (gstcomm.Length > 1)
        //        {
        //            commission = gstcomm[0];
        //            gst = gstcomm[1];
        //        }
        //        stlogin.Requestdata = encData;
        //        stlogin.dRequestdata = encDatadecryptdata;
        //        stlogin.PGstatus = aa[2];
        //        stlogin.banktrxid = aa[9];
        //        stlogin.clienttrxid = aa[0];
        //        stlogin.amount = Convert.ToDecimal(aa[3]);
        //        stlogin.feeamount = "";
        //        stlogin.gst = gst;
        //        stlogin.commission = commission;
        //        stlogin.paymode = aa[5];
        //        stlogin.banktxndate = aa[10];
        //        stlogin.Reason = aa[7];
        //        stlogin.apitxnid = aa[1];
        //        stlogin.ApplicationNo = aa[6];

        //        var obj = stlogin.FeessubStudentadmissionPushresponse(stlogin);
        //        CommonMethod.WritetoNotePaymentgateway(encData, encDatadecryptdata, HttpContext.Current.Request.Url.AbsolutePath, "admission Paymentgateway Pushresponse  url hit payment gatway");
        //        if (obj.Id == 0)
        //        {
        //            paymentgatewayemailadmission(aa[6]);
        //        }

        //        // Message Structure of ‘Push Response’
        //        //         Merchant Order Number | SBIePayRefID / ATRN | Transaction Status | Amount | Currency | Pay Mode | OtherDetails | Reason / Message | Bank Code | Bank Reference Number| Transaction Date | Country | CIN | Merchant ID | Total Fee GST| Ref1 | Ref2 | Ref3 | Ref4 | Ref5 | Ref6 | Ref7 | Ref8 | Ref9
        //        return encDatadecryptdata;
        //    }
        //    catch (Exception ex)
        //    {
        //        CommonMethod.WritetoNotePaymentgateway(ex.Message + "      " + ex.StackTrace, "", HttpContext.Current.Request.Url.AbsolutePath, "admission Paymentgateway Pushresponse url hit error on payment PaymentGateway");
        //        return "error";
        //    }
        //}

    }
}