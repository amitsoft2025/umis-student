using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;
/// <summary>
/// </summary>
public class SMSFUN
{

   
    static DataTable dtSMS = new DataTable();
    public SMSFUN()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public static void sms_StudentRegistration(int template_id,string mobileno,string name,string applicationid,string password,string projectname) // template id 1 for student Registration
    {
        if (name == null)
        {
            name = "Student";
        }
        if (name == "")
        {
            name = "Student";
        }
        //SMS.Send(mobileno, "Dear " + name + ",Thank you for apply. Please find the user credentail to complete the registration. LoginID: " + applicationid + " Password: " + password + " Regard, " + projectname + "");
        // SMS.Send(mobileno, "Dear Student,Please find the user credential for login.LoginID: " + applicationid + " Password: " + password + " Regard, Munger Univ");
        // tempalet id : '1207161838703761674

        // template name : student id 3	(local--student registration)
        // Content: Dear Student, Please find the user credential for login. LoginID: {#var#} Password: {#var#} Regards, Munger University 

        SMS.SendgetDLT(mobileno, "Dear Student, Please find the user credential for login. LoginID: " + applicationid + " Password: " + password + " Regards, Munger University ", "1207161838703761674");

    }
    public static void sms_StudentPaymentgateway(int template_id, string mobileno, string name, string applicationid, string status,string banktransaid,string tranactionid, string projectname) // template id 2 for student Registration payment gateways status
    {
        // SMS.Send(mobileno, "Dear  " + name + ",Thank You. Your payment transaction was "+ status + " for application no. "+ applicationid + " Ref No. "+ banktransaid + " Transaction No. "+ tranactionid + " Regard, " + projectname + "");
        // SMS.Send(mobileno, "Dear Student, Your payment transaction was "+ status + " for application no. "+ applicationid + " Regard, Munger Univ");
        // tempalet id : '1207161838694970000
        // template name : student id 2	(local--student Payment information)
        // Content: Dear Student, Your payment transaction was {#var#} for application no {#var#} Regards, Munger University 

        SMS.SendgetDLT(mobileno, "Dear Student, Your payment transaction was " + status + " for application no " + applicationid + " Regards, Munger University ", "1207161838694970000");


    }
    public static void sms_PasswordSend(int template_id, string mobileno, string name, string applicationid, string password,string projectname) // template id 3 for student passsword send
    {
        // old SMS.Send(mobileno, "Dear  " + name + ",your Loginid: " + applicationid + " and Password: "+ password + " Regard, Munger Univ");
        // tempalet id : '1207161838683847183
        // template name : student id 1	(local--forget password)
        // Content: Dear {#var#}, your Login id : {#var#} and Password: {#var#} Regards, Munger University 
        SMS.SendgetDLT(mobileno, "Dear " + name + ", your Login id : " + applicationid + " and Password: " + password + " Regards, Munger University ", "1207161838683847183");
    }
    public static void Send_OTPverifymobile(string mobileno, string otp) // template id 4 for student passsword send
    {
        
        // tempalet id : '1207161857492492280
        // template name : one time password2
        // Content: OTP is {#var#}, Verify your Mobile No Regards, Munger University 
        SMS.SendgetDLT(mobileno, "OTP is " + otp + ", Verify your Mobile No Regards, Munger University", "1207161857492492280");


    }

}