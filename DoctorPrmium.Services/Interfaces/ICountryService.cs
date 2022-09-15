using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DoctorPremium.DAL.Model;

namespace DoctorPremium.Services.Interfaces
{
	public interface ICountryService
	{
		List<Country> GetAllCountries();
		int GetCountryIdbyName(string countryName);
	}
}