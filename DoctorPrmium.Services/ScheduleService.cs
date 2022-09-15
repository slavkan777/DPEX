using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using DoctorPremium.Services.Interfaces;
using DoctorPremium.DAL.Interfaces;
using DoctorPremium.DAL.Model;

namespace DoctorPremium.Services
{
	public class ScheduleService : IScheduleService
	{
		private IUnitOfWork _unitOfWork;
		private IGenericRepository<ScheduleRecord> _scheduleRepository;

		public ScheduleService(IUnitOfWork unitOfWork,
							IGenericRepository<ScheduleRecord> scheduleRepository)
		{
			this._unitOfWork = unitOfWork;
			this._scheduleRepository = scheduleRepository;
		}

		public ScheduleRecord GetScheduleRecord(int userid, int recordid)
		{
			return _scheduleRepository.FindBy(x => x.UserId == userid && x.ScheduleRecordId == recordid).SingleOrDefault();
		}

		public ScheduleRecord GetScheduleRecordById(int recordid)
		{
			return _scheduleRepository.FindBy(x => x.ScheduleRecordId == recordid).SingleOrDefault();
		}

		public ScheduleRecord Save(ScheduleRecord schedule)
		{
			if (schedule.ScheduleRecordId == 0)
			{
				schedule.CreateDateUtc = DateTime.UtcNow;
				return _scheduleRepository.Add(schedule);
			}
			else
			{
				_scheduleRepository.Update(schedule);
				return null;
			}
		}

        public void Finished(ScheduleRecord schedule)
        {
            schedule.isFinished = true;
            Save(schedule);
        }
        public void Delete(int id)
        {
            if (id != 0)
            {
                _scheduleRepository.Delete(id);
            }
            
        }

		public List<ScheduleRecord> GetListForCalendar(int userid, DateTime from, DateTime to)
		{
			List<ScheduleRecord> records = _scheduleRepository.FindBy(x => x.UserId == userid && (x.RecordDate > from && x.RecordDate <= to)).OrderBy(x=>x.RecordDate).ToList();
			return records;
		}
	}
}
