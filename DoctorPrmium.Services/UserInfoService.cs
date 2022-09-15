using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using DoctorPremium.Services.Interfaces;
using DoctorPremium.DAL.Interfaces;
using DoctorPremium.DAL.Model;
using Microsoft.AspNet.Identity.Owin;

namespace DoctorPremium.Services
{
	public class UserInfoService : IUserInfoService
	{
		private IUnitOfWork _unitOfWork;
		private IGenericRepository<UserInfo> _userInfoRepository;

		public UserInfoService (IUnitOfWork unitOfWork, IGenericRepository<UserInfo> userInfoRepository)
		{
			this._unitOfWork = unitOfWork;
			this._userInfoRepository = userInfoRepository;
		}

        public UserInfo GetUserInfo(int AspnetUserid)
		{
            return _userInfoRepository.GetAll().FirstOrDefault(x => x.AspNetUserId == AspnetUserid);
		}

        public UserInfo GetUserInfoByName(string name)
        {
            return _userInfoRepository.GetAll().FirstOrDefault(x => x.AspNetUser.UserName == name);
        }

		public void CreateNewUserInfo(UserInfo user)
		{
			_userInfoRepository.Add(user);
		}

        public UserInfo Save(UserInfo _UserInfo)
        {
			_UserInfo.UpdateDateUtc = DateTime.UtcNow;
            _userInfoRepository.Update(_UserInfo);
                return null; 
        }
	}
}
