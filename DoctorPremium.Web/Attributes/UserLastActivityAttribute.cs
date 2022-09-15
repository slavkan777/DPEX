using System;
using System.Web.Mvc;
using DoctorPremium.DAL.Model;
using DoctorPremium.Services;
using DoctorPremium.Services.Interfaces;
using Google.Apis.Drive.v2.Data;
using Microsoft.AspNet.Identity;
using DoctorPremium.Web.Helpers;
using DoctorPrmium.Services;

namespace DoctorPremium.Web.Attributes
{
    public class UserLastActivityAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext == null || filterContext.HttpContext == null || filterContext.HttpContext.Request == null)
                return;

            //don't apply filter to child methods
            if (filterContext.IsChildAction)
                return;

            //only GET requests
            if (!String.Equals(filterContext.HttpContext.Request.HttpMethod, "GET", StringComparison.OrdinalIgnoreCase))
                return;

			if (filterContext.HttpContext != null && filterContext.HttpContext.User != null)
	        {
                if (filterContext.HttpContext.User.Identity.IsAuthenticated)
                {
		            UserInfoService userService =
			            (UserInfoService) DependencyResolver.Current.GetService(typeof (IUserInfoService));	        
			        UserInfo userInfo = userService.GetUserInfo(Int32.Parse(filterContext.HttpContext.User.Identity.GetUserId()));
				    //update last activity date
				    if (userInfo.LastActivityDateUtc.AddMinutes(1.0) < DateTime.UtcNow)
				    {
                        userInfo.LastActivityDateUtc = DateTime.UtcNow;
					    userService.Save(userInfo);
				    }
                }

                if (filterContext.HttpContext.Session["SessionDataId"] != null)
                {
                    SessionDataService sesService =
                        (SessionDataService)DependencyResolver.Current.GetService(typeof(ISessionDataService));

                    var sesdata = sesService.GetById((int)filterContext.HttpContext.Session["SessionDataId"]);
                    if (sesdata != null)
                    {
                        if (sesdata.AspUserId == null && UserInfoHelper.User != null)
                        {
                            sesdata.AspUserId = UserInfoHelper.User.AspNetUserId;
                            sesService.Update(sesdata);
                        }
                        if (sesdata.SessionEnd == null || ((DateTime)sesdata.SessionEnd).AddMinutes(1.0) < DateTime.UtcNow)
                        {
                            sesdata.SessionEnd = DateTime.UtcNow;
                            sesService.Update(sesdata);
                        }
                    }
                }
            }

            var us = UserInfoHelper.User;
        }

    }
}
