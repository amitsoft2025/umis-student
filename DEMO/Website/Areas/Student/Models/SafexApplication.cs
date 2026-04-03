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
    public class SafexApplication
    {

        //safex
        public string Safex_me_id { get; set; }
        public string Safex_merchant_request { get; set; }
        public string Safex_hash { get; set; }
        public string Safex_POSTURL { get; set; }

        //safex LOcal
        public string merchant_id = "202104230001";// System.Configuration.ConfigurationSettings.AppSettings["merchant_id"];
        public string ag_id = "Paygate"; //System.Configuration.ConfigurationSettings.AppSettings["ag_id"];
        public string merchant_key = "89diCMlKzp+GWwwBm5aVDv6sD+7wJj9ewrMjC6MsHmc=";// System.Configuration.ConfigurationSettings.AppSettings["merchant_key"];
        public string txn_type = "SALE";// System.Configuration.ConfigurationSettings.AppSettings["txn_type"];
        public string POSTURL = "https://pguat.safexpay.com/agcore/paymentProcessing";// System.Configuration.ConfigurationSettings.AppSettings["POSTURL"];
        public string SafexPaySuccessUrl = "https://portal.mungeruniversity.ac.in/student/Home/PGGateway_Safex";// System.Configuration.ConfigurationSettings.AppSettings["SafexPaySuccessUrl"];
        public string SafexPayFailUrl = "https://portal.mungeruniversity.ac.in/student/Home/PGGateway_Safex";// System.Configuration.ConfigurationSettings.AppSettings["SafexPayFailUrl"];
                                                                                                             //Safex End

        //safex Live
        //      <add key = "merchant_id" value="202203290007" />
        //<add key = "ag_id" value="Paygate" />
        //<add key = "merchant_key" value="6Q+73qvBQkd1mvSiWf83ExK5WjSodCFFljSUvcljnM8=" />
        //<add key = "txn_type" value="SALE" />
        //<add key = "POSTURL" value="https://www.avantgardepayments.com/agcore/paymentProcessing" />
        //<add key = "SafexPaySuccessUrl" value="https://portal.mungeruniversity.ac.in/student/Exam/SafexPayPGSucessExam" />
        //<add key = "SafexPayFailUrl" value="https://portal.mungeruniversity.ac.in/student/Exam/SafexPayPGSucessExam" />

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
            string udf4 = "";
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
            MyCryptoClass aes = new MyCryptoClass();
            string enc_request = aes.encrypt(request);
            string hashing = aes.ComputeSha256Hash(Hash);
            string enc_hash = aes.encrypt(hashing);
            sbi.Safex_me_id = txnmerchantId; // As String
            sbi.Safex_merchant_request = enc_request;
            sbi.Safex_hash = enc_hash;
            sbi.Safex_POSTURL = POSTURL;
            return sbi;
        }
    }
    public class MyCryptoClass
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

}