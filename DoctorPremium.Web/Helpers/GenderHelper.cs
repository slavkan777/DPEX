using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Resources;

namespace DoctorPremium.Web.Helpers
{
	public static class GenderHelper
	{
		public static List<SelectListItem> GetGenderListItems()
		{
			var selectList = new List<SelectListItem>
			{
				new SelectListItem {Text = "", Value = String.Empty},
				new SelectListItem {Text = PatientInfo.Male, Value = "true"},
				new SelectListItem {Text = PatientInfo.Female, Value = "false"}
			};
			return selectList;
		}
	}

	public static class FullNameHelper
	{
		public static string GetFullName(string lastName, string firstName, string surName)
		{
			return String.Format("{0} {1} {2}",lastName, firstName, !String.IsNullOrEmpty(surName) ? surName : "");
		}
	}
}
