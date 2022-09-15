using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DoctorPremium.DAL.Model;

namespace DoctorPremium.Services.Interfaces
{
    public interface IPublicSiteService
    {
        UserPublicPage Save(UserPublicPage _PublicInfo);
        UserPublicPage GetPublicSiteInfo(int UserInfoId);
	    UserPublicPage GetPublicSiteInfoIfIsDeleted(int UserInfoId);
	    UserPublicPage GetPublicSiteInfoIfIsDeletedPlusCounter(int UserInfoId);
	    List<UserPublicPage> GetActivePublicSites();
    }
}
