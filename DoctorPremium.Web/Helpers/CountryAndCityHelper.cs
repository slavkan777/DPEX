using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using DoctorPremium.DAL.Model;
using DoctorPremium.Services.Interfaces;

namespace DoctorPremium.Web.Helpers
{
	public class CountryAndCityHelper
	{
		private ICountryService _countryService;
		private ICityService _cityService;

		public CountryAndCityHelper(ICountryService countryService, ICityService cityService)
        {
			_countryService = countryService;
			_cityService = cityService;
        }

		public List<SelectListItem> GetCountryDropList()
		{	
            string lang = LanguageHelper.GetCurrentLanguage();
            List<SelectListItem> selectList = _countryService.GetAllCountries().Select(x => new SelectListItem() { Text = lang.ToLower() == "ru" ? x.CountryNameRu : x.CountryNameEn, Value = x.CountryId.ToString() }).ToList();
			return selectList;
		}

		public List<SelectListItem> GetCityDropList(int countryId)
        {
            string lang = LanguageHelper.GetCurrentLanguage();
            List<SelectListItem> selectList = _cityService.GetCitiesByCountry(countryId).Select(x => new SelectListItem() { Text = lang.ToLower() == "ru" ? x.CityNameRu : x.CityNameEn, Value = x.CityId.ToString() }).ToList();
			return selectList;
		}
	}
}
