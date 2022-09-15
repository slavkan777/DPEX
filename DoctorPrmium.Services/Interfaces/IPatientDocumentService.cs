using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DoctorPremium.DAL.Model;

namespace DoctorPremium.Services.Interfaces
{
	public interface IPatientDocumentService
	{
		PatientDocument Save(PatientDocument patientDocument);
		void SaveList(List<PatientDocument> patientDocuments);
		List<PatientDocument> GetListDocumentsForPatient(int patientId);
	}
}