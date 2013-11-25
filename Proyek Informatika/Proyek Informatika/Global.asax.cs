using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Proyek_Informatika.Utilities;

namespace Proyek_Informatika
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //routes.MapRoute(
            //    "Mahasiswa", // Route name
            //    "Mahasiswa/{controller}/{action}/{id}",// URL with parameters
            //    new { controller = "Home", action = "Index", id = UrlParameter.Optional }, // Parameter defaults
            //    new string[] { "Proyek_Informatika.Controllers.Mahasiswa" }
            //);

            //routes.MapRoute(
            //    "Dosen", // Route name
            //    "Dosen/{controller}/{action}/{id}",// URL with parameters
            //    new { controller = "Home", action = "Index", id = UrlParameter.Optional }, // Parameter defaults
            //    new string[] { "Proyek_Informatika.Controllers.Dosen" }
            //);

            //routes.MapRoute(
            //    "Koordinator", // Route name
            //    "Koordinator/{controller}/{action}/{id}",// URL with parameters
            //    new { controller = "Home", action = "Index", id = UrlParameter.Optional }, // Parameter defaults
            //    new string[] { "Proyek_Informatika.Controllers.Koordinator" }
            //);

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );

        }

        protected void Application_Start()
        {
            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(new MyRazorViewEngine());
            ViewEngines.Engines.Add(new MyWebFormViewEngine());
            
            AreaRegistration.RegisterAllAreas();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
        }
    }
}