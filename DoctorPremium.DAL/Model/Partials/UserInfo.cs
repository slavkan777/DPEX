using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorPremium.DAL.Model
{
	public partial class UserInfo
    {
        public UserInfo()
        {
        }

	    public UserInfo(int aspNetUserId, int timeZoneId, int languageId)
		{
			this.AspNetUserId = aspNetUserId;
		    this.LanguageId = languageId;
		    this.TimeZoneId = timeZoneId;
			this.CreateDateUtc = DateTime.UtcNow;
			this.IsDeleted = false;
			this.IsLocked = false;
			this.LastActivityDateUtc = DateTime.UtcNow;
		}
	}
}
