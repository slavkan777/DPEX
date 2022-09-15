using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using DoctorPremium.Web.Controllers;
using DoctorPremium.Web.Helpers;
using log4net;
//ds
using MvcSiteMapProvider.Web.Mvc;
using DoctorPremium.Services;
using DoctorPremium.Services.Interfaces;
using System.Runtime.Remoting.Contexts;
using Microsoft.AspNet.Identity;

namespace DoctorPremium.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        //public static readonly ILog log = LogManager.GetLogger(typeof(ScheduleController));
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(typeof(MvcApplication));

        protected void Application_Start()
        {
            Log.Info("Startup application.");
            log4net.Config.XmlConfigurator.Configure(new FileInfo(Server.MapPath("~/Web.config")));
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
			XmlSiteMapController.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            UnityConfig.RegisterComponents();
			
        }

		protected void Application_End()
		{
			Log.Info("Application end.");
		}

        protected void Application_Error(object sender, EventArgs e)
        {
            Exception exception = Server.GetLastError();
			var routeData = new RouteData();
			routeData.Values.Add("controller", "Error");
			//по умолчанию 500 ошибка 
			routeData.Values.Add("action", "page500");
			routeData.Values["statusCode"] = 500;

			if (exception.GetType() == typeof(HttpException))
			{
				Response.StatusCode = ((HttpException)exception).GetHttpCode();
				routeData.Values["statusCode"] = Response.StatusCode;
				//Здесь можно добавить другие коды ошибок
				switch (Response.StatusCode)
				{
					case 404:
						routeData.Values["action"] = "page404";
						Log.Error(String.Format("Page or file not found: {0}.{1} IP: {2}", (Request != null && Request.Url != null ? Request.Url.OriginalString ?? "" : ""), (User.Identity.IsAuthenticated ? String.Format("UserName: {0}.",User.Identity.Name) : ""), Request.UserHostAddress));
						break;
				}
			}
			if ((int)routeData.Values["statusCode"] == 500)
			{
				String logMessage = "App server Error: ";
				var ex = exception.InnerException;
				while (ex != null)
				{
					if (ex.InnerException != null)
					{
						ex = ex.InnerException;
					}
					else
					{
						break;
					}
				}
		//		Log.Error(String.Concat(logMessage, exception.Message));
				Log.Error(String.Format("{0} {1} IP: {2}", String.Concat(logMessage, exception.Message), (User.Identity.IsAuthenticated ? String.Format("UserName: {0}", User.Identity.Name) : ""), Request.UserHostAddress));
				if (ex != null)
				{
					Log.Error(String.Concat("InnerEx:", ex.Message));
				}

				routeData.Values["Error"] = exception.Message;
				//routeData.Values["MoreError"] = ex != null ? ex.Message : "";
				//routeData.Values["Trace"] = exception.StackTrace;
			}
			Response.Clear();
			Response.TrySkipIisCustomErrors = true;
			Context.Response.ContentType = "text/html";
			IController controller = new ErrorController();
			controller.Execute(new RequestContext(new HttpContextWrapper(Context), routeData));
			Server.ClearError();
        }

	    protected void Application_AcquireRequestState(object sender, EventArgs e)
	    {
		    //var routes = RouteTable.Routes;

		    //var httpContext = Request.RequestContext.HttpContext;
		    //if (httpContext == null) return;

		    //var routeData = routes.GetRouteData(httpContext);

		    //if (routeData == null)
		    //	return;


		    string cultureName = "en";
			if (LanguageHelper.IsNullCurrentLanguage())
		    {
			    HttpCookie cultureCookie = Request.Cookies["lang"];
			    if (cultureCookie != null)
			    {
				    cultureName = cultureCookie.Value;

				    if (!LanguageHelper.IsRelevantLanguage(cultureName))
				    {
					    cultureName = "en";
				    }
			    }
			    else
			    {
				    cultureName = Request.UserLanguages != null && Request.UserLanguages.Length > 0
					    ? (Request.UserLanguages[0].Contains("ru") ? "ru" : "en")
					    : "en";
			    }
				LanguageHelper.SetCurrentLanguage(cultureName);
			}
			else
			{
				cultureName = LanguageHelper.GetCurrentLanguage();
			}

			if (Request.RequestContext.RouteData.Values["lang"] as string != cultureName)
			{
				//string returnUrl = Request.RawUrl;
				if (cultureName == "ru")
				{
					Request.RequestContext.RouteData.Values["lang"] = cultureName;
					//returnUrl = string.Concat('/', cultureName, returnUrl);
				}
				else
				{
					if (Request.RequestContext.RouteData.Values["lang"] != null)
					{
						//returnUrl = returnUrl.Replace("/" + Request.RequestContext.RouteData.Values["lang"], "");
						Request.RequestContext.RouteData.Values.Remove("lang");
					}
				}

				//if (!String.IsNullOrEmpty(returnUrl))
				//{
				//	Response.Redirect(returnUrl);
				//}
			}
				
			Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo(cultureName);
			Thread.CurrentThread.CurrentUICulture = Thread.CurrentThread.CurrentCulture;
	    }

	    protected void Session_Start(object sender, EventArgs e)
        {
            if (this.Session != null) 
            {
                var _service = DependencyResolver.Current.GetService<ISessionDataService>();
                string sessionId = this.Session.SessionID;
                string ip2 = HttpContext.Current.Request.UserHostAddress;
                string ipaddress = Context.Request.UserHostAddress;
                int newItemId = _service.CreateNewSession(sessionId, DateTime.UtcNow, ipaddress);
                this.Session.Add("SessionDataId", newItemId);
                Session["service"] = _service;
            }
        }

        protected void Session_End(object sender, EventArgs e)
        {
            //if (this.Session != null && Session["service"] != null)
            //{
            //    var _service = Session["service"] as SessionDataService;
            //    int sessionId = 0;
            //    int userId = 0;
            //    if (User.Identity.IsAuthenticated)
            //    {
            //        userId = Int32.Parse(User.Identity.GetUserId());
            //    }
            //    Int32.TryParse(Session["UserId"].ToString(), out sessionId);
            //    _service.UpdateEndSession(sessionId, DateTime.UtcNow, userId);
            //}
        }

        protected void Application_AuthenticateRequest(Object sender, EventArgs e)
        {
            //if (HttpContext.Current.User != null)
            //{
            //    if (HttpContext.Current.User.Identity.IsAuthenticated && Session["UserId"] == null)
            //    {
            //        Session["UserId"] = HttpContext.Current.User.Identity.GetUserId();
            //    }
            //}
        }
            //protected void OnBeginRequest(object sender, System.EventArgs e)
            //{
            //	if (Request.UserLanguages != null && Request.UserLanguages.Any())
            //	{
            //		var cultureInfo = new CultureInfo(Request.UserLanguages[0]);
            //		Thread.CurrentThread.CurrentUICulture = cultureInfo;
            //	}
            //}

            //protected void Application_BeginRequest(object sender, EventArgs e)
            //{
            //	if (Request.UserLanguages != null && Request.UserLanguages.Any())
            //	{
            //		var cultureInfo = new CultureInfo(Request.UserLanguages[0]);
            //		Thread.CurrentThread.CurrentUICulture = cultureInfo;
            //	}
            //}
        }
}

