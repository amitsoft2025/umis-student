using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Website.Areas.Student.Models
{


    public class AirPayAdmission
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
            Other_Details = Other_Details + "," + 1 + ", Admission," + Order_Number + "," + StID + "," + Sission + ",";
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
                    stlogin.amount = Convert.ToDecimal((logProperties.TRANSACTION.AMOUNT));
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
    }
}