using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using DoctorPremium.Web.Attributes;
using DoctorPremium.Web.Helpers;
using Microsoft.Ajax.Utilities;

namespace DoctorPremium.Web.Controllers
{
    /// <summary>
    /// Base controller
    /// </summary>
	[UserLastActivity]
    public abstract class BaseController : Controller
    {
		protected override IAsyncResult BeginExecuteCore(AsyncCallback callback, object state)
		{
			/*string cultureName = null;

			// Attempt to read the culture cookie from Request
			HttpCookie cultureCookie = Request.Cookies["lang"];
			if (cultureCookie != null)
				cultureName = cultureCookie.Value;
			else
			{
				cultureName = Request.UserLanguages != null && Request.UserLanguages.Length > 0
					? (Request.UserLanguages[0].Contains("ru") ? "ru" : "en")
					: null;
			}

			if (!LanguageHelper.IsRelevantLanguage(cultureName))
			{
				cultureName = "en";
			}

			// Modify current thread's cultures            
			Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo(cultureName);
			Thread.CurrentThread.CurrentUICulture = Thread.CurrentThread.CurrentCulture;
			LanguageHelper.SetCurrentLanguage(cultureName);

			//set route data "lang"
			if (!(cultureName == LanguageHelper.GetDefaultLanguage()))
			{
				Request.RequestContext.RouteData.Values["lang"] = cultureName;
			}
			else
			{
				Request.RequestContext.RouteData.Values.Remove("lang");
			}*/

			return base.BeginExecuteCore(callback, state);
		}
		
    }
}
