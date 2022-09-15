using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using DoctorPremium.DAL.Model;
using DoctorPremium.Services.Interfaces;
using DoctorPremium.DAL.Interfaces;

namespace DoctorPremium.Services
{
    public class SessionDataService : ISessionDataService
    {
        private IUnitOfWork _unitOfWork;
        private readonly IGenericRepository<SessionData> _sessionDataRepository;

        public SessionDataService(IUnitOfWork unitOfWork, IGenericRepository<SessionData> sessionDataRepository)
        {
            this._unitOfWork = unitOfWork;
            this._sessionDataRepository = sessionDataRepository;
        }

        public int CreateNewSession(string sessionId, DateTime sessionStart, string ipaddress)
        {
            SessionData data = new SessionData() { SessionId = sessionId, SessionStart = sessionStart, IPAddress = ipaddress };
            _sessionDataRepository.Add(data);
            return data.Id;
        }

        public List<SessionData> GetAll()
        {
            throw new NotImplementedException();
        }

        public SessionData GetById(int sessionDataId)
        {
            return _sessionDataRepository.GetById(sessionDataId);
        }

        public void GetCounter(ref int daily, ref int weekly, ref int monthly)
        {
            DateTime today = DateTime.UtcNow.Date;
            DateTime weekDate = today.AddDays(-7);
            DateTime monthDate = today.AddDays(-30);
            //_entitySet
            var entities = _sessionDataRepository.GetAll();
            daily = entities.Count(x => x.SessionStart >= today);
            weekly = entities.Count(x => x.SessionStart >= weekDate);
            monthly = entities.Count(x => x.SessionStart >= monthDate);
        }

        public List<SessionData> GetListToTable(string searchString, int start, int length, out int recordsTotal, out int recordsFiltered)
        {
            IQueryable<SessionData> allSessionData = _sessionDataRepository.GetAll();
            recordsTotal = allSessionData.Count();
            IQueryable<SessionData> filteredSessionData;

            List<SessionData> result = new List<SessionData>();
            if (string.IsNullOrEmpty(searchString))
            {
                filteredSessionData = allSessionData;
                recordsFiltered = recordsTotal;
            }
            else
            {
                DateTime searchDate;
                if (DateTime.TryParse(searchString, out searchDate))
                {
                    DateTime toDate = searchDate.AddDays(1);
                    filteredSessionData = allSessionData.Where(x => x.SessionStart >= searchDate && x.SessionStart < toDate);
                }
                else
                {
                    filteredSessionData = allSessionData.Where(x => x.AspNetUser.UserName.Contains(searchString) || x.SessionStart.ToString().Contains(searchString)
                     || x.SessionId.Contains(searchString) || x.IPAddress.Contains(searchString));
                }
                recordsFiltered = filteredSessionData.Count();
            }

            if (recordsTotal != 0)
            {
                result = filteredSessionData.OrderByDescending(c => c.SessionStart).Skip(start).Take(length).ToList();
            }
            return result;
        }

        public void Update(SessionData sessionData)
        {
            SessionData item = _sessionDataRepository.GetById(sessionData.Id);
            if (item.AspUserId == null)
            {
                item.AspUserId = sessionData.AspUserId;
            }
            item.SessionEnd = sessionData.SessionEnd;
            _sessionDataRepository.Update(item);
        }

        public void UpdateEndSession(int sessionDataId, DateTime sessionEnd, int userId)
        {
            throw new NotImplementedException();
        }
    }
}                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                              