using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DoctorPremium.DAL.Interfaces;
using DoctorPremium.DAL.Model;
using DoctorPrmium.Services.Interfaces;

namespace DoctorPrmium.Services
{
    public class HelpDataService : IHelpDataService
    {
        private IGenericRepository<Help> _HelpInfoRepository;
        private IUnitOfWork _unitOfWork;


        public HelpDataService(IUnitOfWork unitOfWork, IGenericRepository<Help> HelpInfoRepository)
		{
			this._unitOfWork = unitOfWork;
            this._HelpInfoRepository = HelpInfoRepository;
		}
        public  IEnumerable<Help> GetHelpInfo(string URL, int LanguageId)
        {
            return _HelpInfoRepository.FindBy(x => (x.URL.Contains(URL) && x.LanguageId == LanguageId && x.IsPublish));
        }

    }
}
