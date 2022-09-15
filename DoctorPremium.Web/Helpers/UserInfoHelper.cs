using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DoctorPremium.DAL.Model;
using DoctorPremium.Services;
using DoctorPremium.Services.Interfaces;

namespace DoctorPremium.Web.Helpers
{
    public static class UserInfoHelper
    {
        public static UserInfo User { 
            get { 
                if (HttpContext.Current.Session["UserInfo"] == null && HttpContext.Current.User.Identity.IsAuthenticated)
	            {
                    UserInfoService userService =
                        (UserInfoService)DependencyResolver.Current.GetService(typeof(IUserInfoService));
                    HttpContext.Current.Session["UserInfo"] = userService.GetUserInfoByName(HttpContext.Current.User.Identity.Name);
	            }
                return HttpContext.Current.Session["UserInfo"] as UserInfo;
            } 
        }

        public static void Clear()
        {
            HttpContext.Current.Session["UserInfo"] = null;
        }
    }
}