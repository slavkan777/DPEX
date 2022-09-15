using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DoctorPremium.DAL.Model;

namespace DoctorPremium.Services.Interfaces
{
	public interface IPatientService
	{
		Patient Save(Patient patient);
        void Delete(Patient patient);
		Patient GetPatientInfo(int patientId, int userId);

		List<Patient> GetListToTable(int userId, int start, int length, string searchString, int sortCol, string dir, out int recordsTotal,
			out int recordsFiltered);
        List<Patient> GetList(int userId);
	}
}