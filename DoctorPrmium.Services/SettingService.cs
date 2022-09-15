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
    public class SettingService : ISettingService
    {
        private IGenericRepository<Setting> _SettingRepository;
        private IUnitOfWork _unitOfWork;


        public SettingService(IUnitOfWork unitOfWork, IGenericRepository<Setting> SettingRepository)
		{
			this._unitOfWork = unitOfWork;
            this._SettingRepository = SettingRepository;
		}

        public Setting GetByName(string name)
        {
            return _SettingRepository.FindBy(x => x.SettingName == name).FirstOrDefault();
        }

        public void Save(Setting newsett)
        {
            _SettingRepository.Add(newsett);
        }

        public void Update(Setting sett)
        {
            _SettingRepository.Update(sett);
        }
    }
}
