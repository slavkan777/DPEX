using System.Data.Entity;
using System.Web;
using System.Web.Mvc;
using DoctorPremium.Services;
using DoctorPremium.Services.Interfaces;
using DoctorPremium.DAL;
using DoctorPremium.DAL.Interfaces;
using DoctorPremium.Web.Models;
using DoctorPrmium.Services;
using DoctorPrmium.Services.Interfaces;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Practices.Unity;
using Unity.Mvc5;

namespace DoctorPremium.Web
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

			RegisterTypes(container);
            
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
			//GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }

		public static void RegisterTypes(IUnityContainer container)
		{
			container.RegisterType<IUnitOfWork, UnitOfWork>(new HierarchicalLifetimeManager(),
				new InjectionConstructor("DefaultConnection"));

			container.RegisterType<UserManager<ApplicationUser, int>>(new HierarchicalLifetimeManager());
			container.RegisterType<IUserStore<ApplicationUser, int>, ApplicationUserStore>(new HierarchicalLifetimeManager());
			container.RegisterType<DbContext, ApplicationDbContext>(new HierarchicalLifetimeManager());
			container.RegisterType<HttpContextBase>(new InjectionFactory(_ => new HttpContextWrapper(HttpContext.Current)));
			container.RegisterType<IOwinContext>(new InjectionFactory(c => c.Resolve<HttpContextBase>().GetOwinContext()));
			container.RegisterType<IAuthenticationManager>(new InjectionFactory(c => c.Resolve<IOwinContext>().Authentication));
            container.RegisterType<IPublicSiteService, PublicSiteService>();
			container.RegisterType(typeof(IGenericRepository<>), typeof(GenericRepository<>));
			container.RegisterType<IPatientService, PatientService>();
			container.RegisterType<IMailService, MailService>();
			container.RegisterType<IUserInfoService, UserInfoService>();
			container.RegisterType<IPatientVisitService, PatientVisitService>();
			container.RegisterType<IScheduleService, ScheduleService>();
            container.RegisterType<IUserInfoService, UserInfoService>();
			container.RegisterType<IPatientDocumentService, PatientDocumentService>();
			container.RegisterType<ICountryService, CountryService>();
			container.RegisterType<ICityService, CityService>();
            container.RegisterType<IRightMoneyDataService, RightMoneyDataService>();
            container.RegisterType<IHelpDataService, HelpDataService>();
            container.RegisterType<ILanguageService, LanguageService>();
            container.RegisterType<ITimeZoneService, TimeZoneService>();
            container.RegisterType<ISessionDataService, SessionDataService>();
            container.RegisterType<ISettingService, SettingService>();
        }

    }
}