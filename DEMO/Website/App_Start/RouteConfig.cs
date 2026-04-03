using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Website
{
    public class RouteConfig
    {


        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.RouteExistingFiles = false;

            routes.MapRoute(
                name: "RootDefault",
                url: "",
                defaults: new { area = "MUStudentPortal", controller = "Login", action = "Index" },
                namespaces: new[] { "Website.Areas.MUStudentPortal.Controllers" }
            );

            routes.MapRoute(
                name: "CustomLogin",
                url: "custom-login",
                defaults: new { area = "MUStudentPortal", controller = "Login", action = "Index" },
                namespaces: new[] { "Website.Areas.MUStudentPortal.Controllers" }
            );
            routes.MapRoute(
             name: "CustomLogout",
             url: "custom-Logout",
             defaults: new { area = "MUStudentPortal", controller = "Login", action = "Logout" },
             namespaces: new[] { "Website.Areas.MUStudentPortal.Controllers" }
         );
            routes.MapRoute(
                name: "Dashboard",
                url: "dashboard",
                defaults: new { area = "MUStudentPortalDashBoard", controller = "HomeUGCBCS", action = "Index" },
                namespaces: new[] { "Website.Areas.MUStudentPortalDashBoard.Controllers" }
            );

            routes.MapRoute(
              name: "updatePassword",
              url: "update-Password",
              defaults: new { area = "MUStudentPortalDashBoard", controller = "HomeUGCBCS", action = "updatePassword" },
              namespaces: new[] { "Website.Areas.MUStudentPortalDashBoard.Controllers" }
          );

            routes.MapRoute(
          name: "stream",
          url: "stream",
          defaults: new { area = "MUStudentPortalDashBoard", controller = "HomeUGCBCS", action = "getstream" },
          namespaces: new[] { "Website.Areas.MUStudentPortalDashBoard.Controllers" }
      );


            routes.MapRoute(
              name: "state",
              url: "state",
              defaults: new { area = "MUStudentPortalDashBoard", controller = "HomeUGCBCS", action = "State_Bind" },
              namespaces: new[] { "Website.Areas.MUStudentPortalDashBoard.Controllers" }
          );

            routes.MapRoute(
                name: "ChangePassword",
                url: "change-password",
                defaults: new { area = "MUStudentPortalDashBoard", controller = "HomeUGCBCS", action = "Changepassword" },
                namespaces: new[] { "Website.Areas.MUStudentPortalDashBoard.Controllers" }
            );

            routes.MapRoute(
                name: "Admission",
                url: "admission",
                defaults: new { area = "MUStudentPortalDashBoard", controller = "HomeUGCBCS", action = "AdmissionFeeSubmit" },
                namespaces: new[] { "Website.Areas.MUStudentPortalDashBoard.Controllers" }
            );

            routes.MapRoute(
                name: "Profile",
                url: "profile",
                defaults: new { area = "MUStudentPortalDashBoard", controller = "HomeUGCBCS", action = "BasicDetail" },
                namespaces: new[] { "Website.Areas.MUStudentPortalDashBoard.Controllers" }
            );
            routes.MapRoute(
             name: "updateprofile",
             url: "update-profile",
             defaults: new { area = "MUStudentPortalDashBoard", controller = "HomeUGCBCS", action = "BasicDetail" },
             namespaces: new[] { "Website.Areas.MUStudentPortalDashBoard.Controllers" }
         );

            routes.MapRoute(
                name: "PaymentGateway",
                url: "admission/payment-gateway",
                defaults: new { area = "MUStudentPortalDashBoard", controller = "HomeUGCBCS", action = "SelectGetwayAdmissionFee" },
                namespaces: new[] { "Website.Areas.MUStudentPortalDashBoard.Controllers" }
            );

            routes.MapRoute(
                name: "CatchAll",
                url: "{*url}",
                defaults: new { controller = "Home", action = "NotFound" },
                namespaces: new[] { "Website.Controllers" }
            );
        }

    }
}
