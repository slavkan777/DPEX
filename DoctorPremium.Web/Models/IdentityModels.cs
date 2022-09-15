using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Claims;
using System.Threading.Tasks;
using DoctorPremium.DAL.Model;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace DoctorPremium.Web.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
	public class ApplicationUser : IdentityUser<int, ApplicationUserLogin,
	ApplicationUserRole, ApplicationUserClaim>
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser,int> manager)
        {
			// Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
			var	userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

	public class ApplicationUserLogin : IdentityUserLogin<int>
	{
	}

	public class ApplicationUserClaim : IdentityUserClaim<int>
	{
	}

	public class ApplicationUserRole : IdentityUserRole<int>
	{
	}
	
	public class ApplicationRole : IdentityRole<int, ApplicationUserRole>
	{
		public ApplicationRole() : base() { }
	}

	public class ApplicationUserStore : UserStore<ApplicationUser, ApplicationRole, int,
		ApplicationUserLogin, ApplicationUserRole, ApplicationUserClaim>
	{
		public ApplicationUserStore(ApplicationDbContext context)
			: base(context)
		{
		}
	}

	public class ApplicationRoleStore : RoleStore<ApplicationRole, int, ApplicationUserRole>
	{
		public ApplicationRoleStore(ApplicationDbContext context)
			: base(context)
		{
		}
	} 

	public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, int,
		ApplicationUserLogin, ApplicationUserRole, ApplicationUserClaim> 
    {
        public ApplicationDbContext()
			: base("IdentityConnection")
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}