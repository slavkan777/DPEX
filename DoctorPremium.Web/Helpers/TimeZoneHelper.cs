using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.ModelBinding;
using System.Web.Mvc;
using DoctorPremium.Common.Enums;
using DoctorPremium.Services.Interfaces;

namespace DoctorPremium.Web.Helpers
{
	public static class TimeZoneHelper
	{
        public static List<SelectListItem> GetTimeZoneDropList(ITimeZoneService timeZoneService)
        {	
            string lang = LanguageHelper.GetCurrentLanguage();
            List<SelectListItem> selectList = timeZoneService.GetAllTimeZones().Select(x => new SelectListItem() { Text = lang.ToLower() == "ru" ? x.TimeZoneNameRu : x.TimeZoneNameEn, Value = x.TimeZoneId.ToString() }).ToList();
            return selectList;
        }

		public static DoctorPremium.DAL.Model.TimeZone GetTimeZoneBySystemId(ITimeZoneService timeZoneService, string systemId)
		{	
			return timeZoneService.GetTimeZoneBySystemId(systemId);
		}
	}
}