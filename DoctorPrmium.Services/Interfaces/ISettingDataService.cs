using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DoctorPremium.DAL.Model;

namespace DoctorPrmium.Services.Interfaces
{
   public interface ISettingService
    {
        Setting GetByName(string str);
        void Update(Setting sett);
        void Save(Setting newsett);
    }
}
