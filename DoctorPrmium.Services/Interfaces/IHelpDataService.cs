using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DoctorPremium.DAL.Model;

namespace DoctorPrmium.Services.Interfaces
{
   public interface IHelpDataService
    {
       IEnumerable<Help> GetHelpInfo(string URL, int LanguageId);
    }
}
