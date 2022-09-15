using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DoctorPremium.DAL.Model;

namespace DoctorPremium.Services.Interfaces
{
    public interface ISessionDataService
    {
        List<SessionData> GetAll();
        SessionData GetById(int sessionDataId);
        int CreateNewSession(string sessionId, DateTime sessionStart, string ipaddress);
        void UpdateEndSession(int sessionDataId, DateTime sessionEnd, int userId);
        void Update (SessionData sessionData);
        List<SessionData> GetListToTable(string searchString, int start, int length, out int recordsTotal, out int recordsFiltered);
        void GetCounter(ref int daily, ref int weekly, ref int monthly);
    }
}