using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataLayer;
using Website.Models;
namespace Website.Areas.StudentPG.Models
{
    public class VerifyUrlFilterAdminAttributePG : ActionFilterAttribute
    {

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            HttpContext ctx = HttpContext.Current;
            string pass = HttpContext.Current.Request.Cookies["ENBPassword"].Value.ToString();
            if (EncriptDecript.Decrypt(pass) == "83008ba")
            {
                filterContext.Result = new RedirectResult("~/StudentPG/HomePG/Changepassword");
            }
                   
            base.OnActionExecuting(filterContext);
        }
       
    }
}
