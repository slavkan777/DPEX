using System.Web.Mvc;
using System.Web.Routing;
using DoctorPremium.Web.Helpers;
using MvcSiteMapProvider.Web.Mvc;

namespace DoctorPremium.Web
{
	public class RouteConfig
	{
		public static void RegisterRoutes(RouteCollection routes)
		{
			routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
               name: "PublicSite/Index",
               url: "Office{Userid}",
               defaults: new { controller = "PublicSite", action = "Index", Userid = @"\d+" }
           );

			//routes.MapRoute(
			//	name: "PatientVisit/New",
			//	url: "PatientVisit/New/{patientId}",
			//	defaults: new { controller = "PatientVisit", action = "Edit", patientId = UrlParameter.Optional }
			//);

			routes.MapRoute(
				name: "Patient/NewWithLang",
				url: "{lang}/{controller}/New",
				constraints: new { lang = "ru", controller = "Patient|PatientVisit" },
				defaults: new { controller = "Patient", action = "Edit", id = 0 }
			);

			routes.MapRoute(
				name: "Patient/New",
				url: "{controller}/New",
				constraints: new { controller = "Patient|PatientVisit" },
				defaults: new { controller = "Patient", action = "Edit", id = 0 }
			);

            routes.MapRoute(
                name: "DefaultWithLang",
                url: "{lang}/{controller}/{action}/{id}",
				constraints: new { lang = "ru" },		//new { lang = "[a-z]{2}?" },
				defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

           // routes.MapRoute(
           //    name: "Account/ShowCaptchaImage",
           //    url: "{controller}/{action}",
           //    defaults: new { controller = "Account", action = "ShowCaptchaImage" }
           //);


			routes.MapRoute(
				name: "Default",
				url: "{controller}/{action}/{id}",
				defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
			);

			routes.IgnoreRoute("{*robotstxt}", new { robotstxt = @"(.*/)?robots.txt(/.*)?" });
			routes.IgnoreRoute("{*favicon}", new { favicon = @"(.*/)?favicon.ico(/.*)?" });
			routes.IgnoreRoute("{*staticfile}", new { staticfile = @".*\.(css|js|gif|jpg)(/.*)?" });
		}
	}
}
