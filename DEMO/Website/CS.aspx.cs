using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using Newtonsoft.Json;
using System.Web.Script.Serialization;
namespace Website
{
    public partial class CS : System.Web.UI.Page
    {
        protected void Translate(object sender, EventArgs e)
        {
            string url = "https://translation.googleapis.com/language/translate/v2?key=YOUR_API_KEY";
            url += "&source=" + ddlSource.SelectedItem.Value;
            url += "&target=" + ddlTarget.SelectedItem.Value;
            url += "&q=" + Server.UrlEncode(txtSource.Text.Trim());
            WebClient client = new WebClient();
            string json = client.DownloadString(url);
            JsonData jsonData = (new JavaScriptSerializer()).Deserialize<JsonData>(json);
            txtTarget.Text = jsonData.Data.Translations[0].TranslatedText;
        }

        public class JsonData
        {
            public Data Data { get; set; }
        }
        public class Data
        {
            public List<Translation> Translations { get; set; }
        }
        public class Translation
        {
            public string TranslatedText { get; set; }
        }

        protected void ddlSource_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}