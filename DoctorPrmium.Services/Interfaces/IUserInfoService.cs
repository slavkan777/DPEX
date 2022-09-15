using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DoctorPremium.DAL.Model;

namespace DoctorPremium.Services.Interfaces
{
	public interface IUserInfoService
	{
        UserInfo Save(UserInfo _UserInfo);
        UserInfo GetUserInfo(int AspNetUserId);
        UserInfo GetUserInfoByName(string name);
	    void CreateNewUserInfo(UserInfo user);
	}
}