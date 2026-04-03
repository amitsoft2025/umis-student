using System.Web.Mvc;

namespace Website.Areas.UGCBCS
{
    public class StudentPGAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "UGCBCS";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "UGCBCS_default",
                "UGCBCS/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}