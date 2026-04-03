using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Reflection;
using DataLayer;
namespace Website.Models
{
    public class CookiesExpireFilterCommon : ActionFilterAttribute
    {

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            HttpContext ctx = HttpContext.Current;
            if (HttpContext.Current.Request.Cookies["NBApplicationNo"] == null)
            {
                 filterContext.Result = new RedirectResult("~/Student/Login");
                return;
            }
            else
            {
              
            }
            base.OnActionExecuting(filterContext);
        }
    }
}