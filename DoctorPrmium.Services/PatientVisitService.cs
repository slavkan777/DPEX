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
	public class PatientVisitService : IPatientVisitService
	{
		private IUnitOfWork _unitOfWork;
		private IGenericRepository<PatientVisit> _patientVisitInfoRepository;

		public PatientVisitService(IUnitOfWork unitOfWork,
							IGenericRepository<PatientVisit> patientVisitInfoRepository)
		{
			this._unitOfWork = unitOfWork;
			this._patientVisitInfoRepository = patientVisitInfoRepository;
		}

		public PatientVisit Save(PatientVisit visit)
		{
			if (visit.PatientVisitId == 0)
			{
				return _patientVisitInfoRepository.Add(visit);
			}
			else
			{
				visit.UpdateDateUtc = DateTime.UtcNow;
				_patientVisitInfoRepository.Update(visit);
				return visit;
			}
		}


        public void Delete(PatientVisit patientVisit)
        {
            patientVisit.IsDeleted = true;
            Save(patientVisit);
        }

		public PatientVisit GetPatientVisitInfo(int visitId, int userId)
		{
			return _patientVisitInfoRepository.FindBy(x => (x.PatientVisitId == visitId && !x.IsDeleted && (x.Patient != null && x.Patient.UserId == userId))).SingleOrDefault();
		}

		public List<PatientVisit> GetListToTable(int patientId, int start, int length, string searchString, int sortCol, string dir, out int recordsTotal, out int recordsFiltered)
		{
			IQueryable<PatientVisit> allVisits = _patientVisitInfoRepository.GetAll().Where(x => x.PatientId == patientId && !x.IsDeleted);
			recordsTotal = allVisits.Count();
			IQueryable<PatientVisit> filteredVisits;

			List<PatientVisit> result = new List<PatientVisit>();
			if (string.IsNullOrEmpty(searchString))
			{
				filteredVisits = allVisits;
				recordsFiltered = recordsTotal;
			}
			else
			{
				filteredVisits = allVisits;
				recordsFiltered = filteredVisits.Count();
			}

			switch (sortCol)
			{
				//case 2: filteredPatients = (dir == "asc" ? filteredPatients.OrderBy(m => m.LastName) : filteredPatients.OrderByDescending(m => m.LastName));
				//	break;
				//case 3: filteredPatients = (dir == "asc" ? filteredPatients.OrderBy(m => m.BirthDate) :
				//	filteredPatients.OrderByDescending(m => m.BirthDate));
				//	break;		 // TODO: need visits, dept, vip, last visit;
				default:
					filteredVisits = filteredVisits.OrderBy(m => m.VisitDate).ThenBy(v => v.VisitStartTime);
					break;
			}

			if (recordsTotal != 0)
			{
				result = filteredVisits.Skip(start).Take(length).ToList();
			}
			return result;
		}
	}
}
