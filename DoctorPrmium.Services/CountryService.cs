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
	public class CountryService : ICountryService
	{
		private IUnitOfWork _unitOfWork;
		private IGenericRepository<Country> _countryRepository;

		public CountryService(IUnitOfWork unitOfWork, IGenericRepository<Country> countryRepository)
		{
			this._unitOfWork = unitOfWork;
			this._countryRepository = countryRepository;
		}

        public List<Country> GetAllCountries()
		{
            return _countryRepository.GetAll().ToList();
		}

		public int GetCountryIdbyName(string countryName)
		{
			Country country = _countryRepository.FindBy(c => c.CountryNameEn.ToLower().Equals(countryName.ToLower()) || c.CountryNameRu.ToLower().Equals(countryName.ToLower())).FirstOrDefault();
			return country != null ? country.CountryId : 0;
		}
	}
}
