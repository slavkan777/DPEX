using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace DoctorPremium.Web.Helpers
{
	public static class DateTimeHelper
	{
		public static List<SelectListItem> GetDurationTimeListItems(string selectValue)
		{
			string min;
			switch (LanguageHelper.GetCurrentLanguage())
			{
				case "ru":
					min = "мин.";
					break;
				default:
					min = "min.";
					break;
			} 

			var selectList = new List<SelectListItem>
			{
				new SelectListItem {Text = String.Concat("30 ",min), Value = "30"},
				new SelectListItem {Text = String.Concat("60 ",min), Value = "60"},
				new SelectListItem {Text = String.Concat("90 ",min), Value = "90"},
				new SelectListItem {Text = String.Concat("120 ",min), Value = "120"},
				new SelectListItem {Text = String.Concat("150 ",min), Value = "150"},
				new SelectListItem {Text = String.Concat("200 ",min), Value = "200"}
			};
			foreach (var item in selectList)
			{
				if (item.Value == selectValue)
				{
					item.Selected = true;
					break; 
				}
			}
			return selectList;
		}
	}
}
