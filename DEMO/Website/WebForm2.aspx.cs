using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Threading;
using Newtonsoft.Json;
using Website.Areas.Student.Models;
using System.Security.Cryptography;
using System.Text;
using System.Net;
using System.Collections.Specialized;
using ThirdParty.Json.LitJson;
using System.Web.Script.Serialization;
using System.Net;
using System.Web.Script.Serialization;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using System.Drawing;
using System.Data.SqlClient;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataLayer;
using System.Data;
using System.IO;
using System.Configuration;
using Website.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using com.toml.dp.util;
using System.Text;
using DataLayer;
using System.Web.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace Website
{
    public partial class WebForm2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string encData = HttpContext.Current.Request.Form["encData"];
            //CommonMethod.WritetoNotePaymentgateway(encData, "", HttpContext.Current.Request.Url.AbsolutePath, "admission pgsucessdecrypt url hit payment gatway", ClsLanguage.GetCookies("NBApplicationNo"));
            string encDatadecryptdata = AES128Bit.Decrypt(encData, "Zsg/zaGnfdaarsnvO3sF3g==", 128);
            var aa = encDatadecryptdata.Split('|');
             AdmissionFeesSubmit stlogin = new AdmissionFeesSubmit();
            AdmissionFeesSubmit obj = new AdmissionFeesSubmit();
            for (int i = 0; i <aa.Length; i++)
            {
                Response.Write("<br>");
                Response.Write(aa[i]);
                Response.Write("<br>");
            }

        }
    }
}