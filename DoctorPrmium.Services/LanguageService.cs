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
	public class LanguageService : ILanguageService
	{
		private IUnitOfWork _unitOfWork;
		private IGenericRepository<Language> _languageRepository;

        public LanguageService(IUnitOfWork unitOfWork, IGenericRepository<Language> languageRepository)
		{
			this._unitOfWork = unitOfWork;
            this._languageRepository = languageRepository;
		}

        public List<Language> GetAllLanguages()
		{
            return _languageRepository.GetAll().ToList();
		}
	}
}
