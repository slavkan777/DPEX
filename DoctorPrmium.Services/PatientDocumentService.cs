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
	public class PatientDocumentService : IPatientDocumentService
	{
		private IUnitOfWork _unitOfWork;
		private IGenericRepository<PatientDocument> _patientDocumentRepository;

		public PatientDocumentService(IUnitOfWork unitOfWork,
							IGenericRepository<PatientDocument> patientDocumentRepository)
		{
			this._unitOfWork = unitOfWork;
			this._patientDocumentRepository = patientDocumentRepository;
		}

		public void SaveList (List<PatientDocument> patientDocuments)
		{
			foreach (var document in patientDocuments)
			{
				Save(document);
			}
		}

		public PatientDocument Save(PatientDocument patientDocument)
		{
			return _patientDocumentRepository.Add(patientDocument);
		}
		
		public List<PatientDocument> GetListDocumentsForPatient(int patientId)
		{
			return _patientDocumentRepository.FindBy(x => x.PatientId == patientId).ToList();
		}
	}
}
