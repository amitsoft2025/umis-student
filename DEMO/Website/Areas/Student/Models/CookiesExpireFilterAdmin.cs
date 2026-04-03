using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Reflection;
using DataLayer;
using Website.Models;
namespace Website.Areas.Student.Models
{
    public class CookiesExpireFilterAdminAttribute : ActionFilterAttribute
    {

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            HttpContext ctx = HttpContext.Current;
            if (HttpContext.Current.Request.Cookies["NBApplicationNo"] == null || HttpContext.Current.Request.Cookies["ENBUGApplicationNo"] == null)
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
                //DateTime lastdatecheck = Convert.ToDateTime(EncriptDecript.Decrypt(DataLayer.ClsLanguage.GetCookies("ENNBLastLogin")));
                //lastdatecheck = lastdatecheck.AddHours(1);
                //if (lastdatecheck < DateTime.Now)
                //{
                //    UserLogin.ExpireAllCookies();
                //    filterContext.Result = new RedirectResult("~/Login");
                //    return;
                //}

            }
            base.OnActionExecuting(filterContext);
        }
    }
}