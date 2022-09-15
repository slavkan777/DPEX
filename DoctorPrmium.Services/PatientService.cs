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
    public class PatientService : IPatientService, IDisposable
    {
        private IUnitOfWork _unitOfWork;
        private IGenericRepository<Patient> _patientInfoRepository;

        public PatientService(IUnitOfWork unitOfWork, IGenericRepository<Patient> patientInfoRepository)
        {
            this._unitOfWork = unitOfWork;
            this._patientInfoRepository = patientInfoRepository;
        }

        public Patient Save(Patient patient)
        {
            if (patient.PatientId == 0)
            {
				//char[] charArray = DateTime.Now.Ticks.ToString().ToCharArray();
				//Array.Reverse(charArray);
				//patient.CardNumber = new string(charArray).Substring(0, 9);
				patient.CreateDateUtc = DateTime.UtcNow;
                Patient p = _patientInfoRepository.Add(patient);
				//int length = p.PatientId.ToString().Length;
				//string s = "000000000";
				//p.CardNumber = String.Concat(s.Substring(0, 10 - length), p.PatientId);
				//_patientInfoRepository.Update(p);
	            return p;
            }
            else
            {
				patient.UpdateDateUtc = DateTime.UtcNow;
                _patientInfoRepository.Update(patient);
	            return null;
            }
        }

        public void Delete(Patient patient)
        {
            patient.IsDeleted = true;
            Save(patient);
        }

        /// <summary>
		/// Get patient info inf by patientid and userid
		/// </summary>
		/// <param name="patientId">id patient</param>
		/// <param name="userId">id user</param>
		/// <returns>Patient from db</returns>
        public Patient GetPatientInfo(int patientId, int userId)
        {
            return _patientInfoRepository.FindBy(x => (x.PatientId == patientId && !x.IsDeleted && x.UserId == userId)).SingleOrDefault();
        }

        public List<Patient> GetListToTable(int userId, int start, int length, string searchString, int sortCol, string dir, out int recordsTotal, out int recordsFiltered)
        {
            IQueryable<Patient> allPatients = _patientInfoRepository.GetAll().Where(x => x.UserId == userId && !x.IsDeleted);
            recordsTotal = allPatients.Count();
            IQueryable<Patient> filteredPatients;

            List<Patient> result = new List<Patient>();
            if (string.IsNullOrEmpty(searchString))
            {
                filteredPatients = allPatients;
                recordsFiltered = recordsTotal;
            }
            else
            {
                string[] subSearch = searchString.Split('$');
                string[] sValues;
                string searchStr;
                List<Patient> searchedPatients = new List<Patient>();
                filteredPatients = allPatients;
                if (subSearch.Any())
                {
                    searchedPatients = filteredPatients.ToList();
                    foreach (var str in subSearch)
                    {

                        if (!String.IsNullOrEmpty(str))
                        {
                            sValues = str.Split('^');
                            if (sValues.Count() > 1)
                            {
                                searchStr = sValues[1];
                                switch (sValues[0])
                                {
                                    case "0":
                                        searchedPatients = searchedPatients.Where(x => x.LastName.Contains(searchStr) || x.FirstName.Contains(searchStr)
                                            || (x.SurName != null && x.SurName.Contains(searchStr)) || (x.HomePhone != null && x.HomePhone.Contains(searchStr)) || (x.MobilePhone != null && x.MobilePhone.Contains(searchStr))).ToList();
                                        break;
                                    case "1":
                                        searchedPatients = searchedPatients.Where(x => x.LastName.Contains(searchStr)).ToList();
                                        break;
                                    case "2":
                                        searchedPatients = searchedPatients.Where(x => x.CardNumber != null && x.CardNumber.Contains(searchStr)).ToList();
                                        break;
                                    case "3":
                                        DateTime date;
                                        DateTime.TryParse(searchStr, out date);
                                        searchedPatients = searchedPatients.Where(x => x.BirthDate == date).ToList();
                                        break;
                                    //case "4":
                                    //filteredPatients = filteredPatients.Where(x => x.CardNumber.Contains(searchStr));
                                    //	break;
                                    case "5":		//TODO: need lastvisit
                                        string[] dates = searchStr.Split('-');
                                        DateTime fromDate = new DateTime();
                                        DateTime.TryParse(dates[0], out fromDate);
                                        if (dates.Count() > 1)
                                        {
                                            DateTime toDate = new DateTime();
                                            DateTime.TryParse(dates[1], out toDate);
                                            searchedPatients = searchedPatients.Where(x => x.PatientVisits.Count > 0 && (x.PatientVisits.OrderBy(o => o.VisitDate).Last().VisitDate >= fromDate.Date && x.PatientVisits.OrderBy(d => d.VisitDate).Last().VisitDate <= toDate)).ToList();
                                        }
                                        else
                                        {
                                            searchedPatients = searchedPatients.Where(x => x.PatientVisits.Count > 0 && x.PatientVisits.OrderBy(m => m.VisitDate).Last().VisitDate == fromDate.Date).ToList();
                                        }
                                        break;
                                    case "6":
                                        searchedPatients = searchedPatients.Where(x => (x.HomePhone != null && x.HomePhone.Contains(searchStr)) || (x.MobilePhone != null && x.MobilePhone.Contains(searchStr))).ToList();
                                        break;
                                    //case "7": //TODO: need lastvisit diagnost
                                    //filteredPatients = filteredPatients.Where(x => x.AlergoStatus.Contains(searchStr) || x.Anamnez.Contains(searchStr) || x.AdditionalInfo.Contains(searchStr));
                                    //	break;
                                    case "8":
                                        searchedPatients = searchedPatients.Where(x => x.Email != null && x.Email.Contains(searchStr)).ToList();
                                        break;
                                    case "9":
                                        if (searchStr == "man" || searchStr == "woman")
                                        {
                                            searchedPatients = searchedPatients.Where(x => x.IsMale == (searchStr == "man")).ToList();
                                        }
                                        break;
                                }
                            }
                        }
                    }
                    filteredPatients = searchedPatients.AsQueryable();
                }
                recordsFiltered = filteredPatients.Count();
            }

            Func<Patient, string> orderingFunction;

            switch (sortCol)
            {
                case 2: filteredPatients = (dir == "asc" ? filteredPatients.OrderBy(m => m.LastName) : filteredPatients.OrderByDescending(m => m.LastName));
                    break;
                case 3: filteredPatients = (dir == "asc" ? filteredPatients.OrderBy(m => m.BirthDate) :
                    filteredPatients.OrderByDescending(m => m.BirthDate));
                    break;
                case 5: filteredPatients = (dir == "asc" ? filteredPatients.OrderBy(m => m.PatientVisits.Count) : filteredPatients.OrderByDescending(m => m.PatientVisits.Count));
                    break;
                //case 6: filteredPatients = (dir == "asc" ? filteredPatients.OrderBy(m => m.PatientVisits.Count) : filteredPatients.OrderByDescending(m => m.PatientVisits.Count));
                //break;// TODO: need dept;
                case 7: filteredPatients = (dir == "asc" ? filteredPatients.OrderBy(m => m.IsVIP) : filteredPatients.OrderByDescending(m => m.IsVIP));
                    break;
                case 8: filteredPatients = (dir == "asc" ? filteredPatients.OrderBy(m => m.PatientVisits.OrderByDescending(x => x.VisitDate).FirstOrDefault().VisitDate) : filteredPatients.OrderByDescending(m => m.PatientVisits.OrderByDescending(x => x.VisitDate).FirstOrDefault().VisitDate));
                    break;
                default:
                    filteredPatients = filteredPatients.OrderBy(m => m.LastName);
                    break;
            }

            if (recordsTotal != 0)
            {
                result = filteredPatients.Skip(start).Take(length).ToList();
            }
            return result;
        }

        public void Dispose()
        {
        	this._unitOfWork.Dispose();
        }



        public List<Patient> GetList(int userId)
        {
            IQueryable<Patient> allPatients = _patientInfoRepository.GetAll().Where(x => x.UserId == userId && !x.IsDeleted);

            List<Patient> result = new List<Patient>();
            result = allPatients.ToList();
            return result;
        }
    }
}
