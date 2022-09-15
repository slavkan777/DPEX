using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DoctorPremium.DAL.Model;

namespace DoctorPremium.Services.Interfaces
{
	public interface IPatientVisitService
	{
		PatientVisit Save(PatientVisit patient);
		PatientVisit GetPatientVisitInfo(int patientId, int userId);
        void Delete(PatientVisit patientVisit);
		List<PatientVisit> GetListToTable(int patientId, int start, int length, string searchString, int sortCol,
			string dir, out int recordsTotal, out int recordsFiltered);
	}
}