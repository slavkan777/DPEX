using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DoctorPremium.DAL.Interfaces;
using DoctorPremium.DAL.Model;

namespace DoctorPremium.DAL
{
	public class UnitOfWork : IUnitOfWork
	{
		protected string ConnectionString;
        private DoctorPremiumEntities _context;

		public UnitOfWork(string connectionString)
		{
			this.ConnectionString = connectionString;
		}

		public DoctorPremiumEntities DbContext
		{
			get
			{
				if (_context == null)
				{
					_context = new DoctorPremiumEntities(ConnectionString);
				}
				return _context;
			}
		}

		public int Save()
		{
			return _context.SaveChanges();
		}

		public void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (_context != null)
				{
					_context.Dispose();
					_context = null;
				}
			}
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}
	}
}
