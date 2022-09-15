using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DoctorPremium.DAL.Model;

namespace DoctorPremium.DAL.Interfaces
{
	public interface IUnitOfWork : IDisposable
	{
        DoctorPremiumEntities DbContext { get; }
		int Save();
	}
}
