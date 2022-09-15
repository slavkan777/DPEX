using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using DoctorPremium.Services.Interfaces;
using DoctorPremium.DAL.Interfaces;
using DoctorPremium.DAL.Model;
using Microsoft.AspNet.Identity.Owin;

namespace DoctorPremium.Services
{
	public class CityService : ICityService
	{
		private IUnitOfWork _unitOfWork;
		private IGenericRepository<City> _cityRepository;

		public CityService(IUnitOfWork unitOfWork, IGenericRepository<City> cityRepository)
		{
			this._unitOfWork = unitOfWork;
			this._cityRepository = cityRepository;
		}

		public List<City> GetCitiesByCountry(int countryId)
		{
			return _cityRepository.FindBy(x=> x.CountryId == countryId).ToList();
		}
	}
}
