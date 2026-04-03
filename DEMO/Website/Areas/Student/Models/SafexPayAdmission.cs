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



    public class SafexPayAdmission
    {

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


        int keysize = 128;

        string privateKey = "";


        public SafexPayAdmission encriptDataadmissionSafex(decimal Amount1, string Other_Details1, string Success_URL1, string Failure_URL1, string cmid11, string ckey11, int collegeid, int courseyearid)
        {
            StudentLogin stu = new StudentLogin();
            string ApplicationID = ClsLanguage.GetCookies("NBApplicationNo");
            Login lo = stu.BasicDetail(ApplicationID);
            SafexPayAdmission sbi = new SafexPayAdmission();
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
            string txnsuccessUrl = Success_URL1;
            string txnfailureUrl = Success_URL1;
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

        public string encrypt(string plainText, string pkey)
        {
            privateKey = pkey;
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






        public string AirPaypgsucessdecrypt(int sid, string banktrxid, string clienttrxid, string amount1, string feeamount, string gst, string commission, string paymode, string banktxndate, string Reason, string apitxnid, string ApplicationNo, string courseyearid, string AdmissionType, string encData, string encDatadecryptdata, string PGstatus, string Sessionid)
        {
            try
            {
                //EaxmFeesSubmit stlogin = new EaxmFeesSubmit();
                //EaxmFeesSubmit obj = new EaxmFeesSubmit();

                AdmissionFeesSubmit stlogin = new AdmissionFeesSubmit();
                AdmissionFeesSubmit obj = new AdmissionFeesSubmit();
                stlogin.Requestdata = encData;
                stlogin.dRequestdata = encDatadecryptdata;
                stlogin.PGstatus = PGstatus;
                gst = gst;
                commission = commission;
                CommonMethod.TraceLogWritetoNotepad("obj1.objExamFrom4", "Exam/sbipay11", "AirPaypgsucessdecrypt", "Id");
                stlogin.banktrxid = banktrxid;
                stlogin.clienttrxid = clienttrxid;
                stlogin.amount = Convert.ToDecimal(amount1);
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
                stlogin.sessionid = Convert.ToInt32(Sessionid);

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
    }
}