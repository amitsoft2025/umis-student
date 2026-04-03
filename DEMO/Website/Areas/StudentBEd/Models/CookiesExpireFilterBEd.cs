using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Reflection;
using DataLayer;
namespace Website.Areas.StudentBEd.Models
{
    public class CookiesExpireFilterBEd : ActionFilterAttribute
    {

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            HttpContext ctx = HttpContext.Current;
            if (HttpContext.Current.Request.Cookies["ENBBEdApplicationNo"] == null )
            {
                 filterContext.Result = new RedirectResult("~/Student/Login");
                return;
            }
            else
            {
                if (HttpContext.Current.Request.Cookies["islogout"] == null)
                {
                    UserLogin.ExpireAllCookies();
                    filterContext.Result = new RedirectResult("~/Student/Login");
                    return;
                }
                else
                {
                    var islogout = (DataLayer.ClsLanguage.GetCookies("islogout"));
                    if (islogout == "False")
                    {
                        UserLogin.ExpireAllCookies();
                        filterContext.Result = new RedirectResult("~/Student/Login");
                        return;
                    }


                }
            }
            base.OnActionExecuting(filterContext);
        }
    }
}