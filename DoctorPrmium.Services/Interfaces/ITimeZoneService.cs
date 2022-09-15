using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DoctorPremium.DAL.Model;

namespace DoctorPremium.Services.Interfaces
{
    public interface ITimeZoneService
	{
        List<DoctorPremium.DAL.Model.TimeZone> GetAllTimeZones();
	    DoctorPremium.DAL.Model.TimeZone GetTimeZoneBySystemId(string systemId);
	}
}