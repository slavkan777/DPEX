using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DoctorPremium.DAL.Interfaces;
using DoctorPremium.DAL.Model;
using DoctorPrmium.Services.Interfaces;

namespace DoctorPrmium.Services
{
    public class RightMoneyDataService : IRightMoneyDataService
    {
        private IUnitOfWork _unitOfWork;
        private IGenericRepository<RightMoneyData_Result> _rightManyData_ResultRepository;

        public RightMoneyDataService(IUnitOfWork unitOfWork, IGenericRepository<RightMoneyData_Result> rightManyData_ResultRepository)
        {
            this._unitOfWork = unitOfWork;
            this._rightManyData_ResultRepository = rightManyData_ResultRepository;
        }

        public RightMoneyData_Result GetMoneyInfo(int DataUserIdForMany)
        {
            return _rightManyData_ResultRepository.ExecWithStoreProcedure("RightMoneyData  @DataUserIdForMany",
             new SqlParameter("DataUserIdForMany", SqlDbType.Int) { Value = DataUserIdForMany }).FirstOrDefault();
        }

    }
}
