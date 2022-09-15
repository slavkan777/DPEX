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
	public class TimeZoneService : ITimeZoneService
	{
		private IUnitOfWork _unitOfWork;
        private IGenericRepository<DoctorPremium.DAL.Model.TimeZone> _timeZoneRepository;

        public TimeZoneService(IUnitOfWork unitOfWork, IGenericRepository<DoctorPremium.DAL.Model.TimeZone> timeZoneRepository)
		{
			this._unitOfWork = unitOfWork;
            this._timeZoneRepository = timeZoneRepository;
		}

        public List<DoctorPremium.DAL.Model.TimeZone> GetAllTimeZones()
		{
            return _timeZoneRepository.GetAll().ToList();
		}

		public DoctorPremium.DAL.Model.TimeZone GetTimeZoneBySystemId(string systemId)
		{
			return _timeZoneRepository.FindBy(x => x.SystemId == systemId).FirstOrDefault();
		}
	}
}
