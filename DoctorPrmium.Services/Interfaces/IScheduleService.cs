using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DoctorPremium.DAL.Model;

namespace DoctorPremium.Services.Interfaces
{
	public interface IScheduleService
	{
        void Delete(int Id);
        void Finished(ScheduleRecord schedule);
		ScheduleRecord Save(ScheduleRecord schedule);
		ScheduleRecord GetScheduleRecord(int userid, int recordid);
		ScheduleRecord GetScheduleRecordById(int recordid);
		List<ScheduleRecord> GetListForCalendar(int userid, DateTime from, DateTime to);
	}
}