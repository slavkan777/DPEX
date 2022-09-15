using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using DoctorPremium.DAL.Model;
using DoctorPremium.Web.Helpers;
using Newtonsoft.Json;
using Org.BouncyCastle.Asn1.Cms;
using Resources;

namespace DoctorPremium.Web.Models
{
    public class ProfileEditViewModel
    {
        public ProfileEditViewModel()
        {
            genderItems = GenderHelper.GetGenderListItems();
			countryItems = new List<SelectListItem>();
			cityItems = new List<SelectListItem>();
            languageItems = new List<SelectListItem>();
            timeZoneItems = new List<SelectListItem>();
        }

        public UserInfo ToEntity(ProfileEditViewModel model, UserInfo oldUser)
        {
            UserInfo newUser = oldUser;
            newUser.UserInfoId = model.UserInfoId;
            newUser.AspNetUserId = model.AspNetUserId;
            newUser.LastName = model.LastName != null ? model.LastName.Trim() : null;
            newUser.FirstName = model.FirstName != null ? model.FirstName.Trim() : null;
            newUser.Address = model.Address != null ? model.Address.Trim() : null;
            newUser.BirthDate = model.BirthDate;
			newUser.CityId = model.CityId;
			newUser.CountryId = model.CountryId;
            newUser.IsMale = model.IsMale ?? false;
			newUser.CreateDateUtc = DateTime.UtcNow;
            newUser.Phone1 = model.Phone1 != null ? model.Phone1.Trim() : null;
            newUser.Phone2 = model.Phone2 != null ? model.Phone2.Trim() : null;
            newUser.Photo = model.Photo;
            newUser.SurName = model.SurName != null ? model.SurName.Trim() : null;
            newUser.TimeZoneId = model.TimeZoneId;
			newUser.LanguageId = model.LanguageId;
			newUser.UpdateDateUtc = DateTime.UtcNow;
            newUser.WorkTimeFrom = model.WorkTimeFrom;
            newUser.WorkTimeTo = model.WorkTimeTo;
            newUser.Notepad = model.Notepad;

            return newUser;
        }


        public void FromEntity(UserInfo _UserInfo)
        {
            Mapper.CreateMap<UserInfo, ProfileEditViewModel>();
            Mapper.Map(_UserInfo, this);
        }

        public int UserInfoId { get; set; }
        public int AspNetUserId { get; set; }

        //[Required(ErrorMessageResourceType = typeof(PatientVisitLang), ErrorMessageResourceName = "DataVisitsRequiered")]
        //[Display(Name = "DataVisits", ResourceType = typeof(PatientVisitLang))]
        
        [StringLength(64)]
        [Display(Name = "LastName", ResourceType = typeof(UserInfos))] 
        public string LastName { get; set; }

     
        [StringLength(64)]
        [Display(Name = "FirstName", ResourceType = typeof(UserInfos))]  
        public string FirstName { get; set; }

        [StringLength(64)]
        [Display(Name = "SurName", ResourceType = typeof(UserInfos))]  
        public string SurName { get; set; }

        public string Photo { get; set; }

         [Display(Name = "IsMale", ResourceType = typeof(UserInfos))]  
        public bool? IsMale { get; set; }

        [Display(Name = "BirthDate", ResourceType = typeof(UserInfos))]  
        public DateTime? BirthDate { get; set; }

        [StringLength(16)]
        [Display(Name = "Phone1", ResourceType = typeof(UserInfos))]  
        public string Phone1 { get; set; }

        [StringLength(16)]
        [Display(Name = "Phone2", ResourceType = typeof(UserInfos))]  
        public string Phone2 { get; set; }

        [Display(Name = "Country", ResourceType = typeof(UserInfos))]  
		public int? CountryId { get; set; }

        [Display(Name = "CityId", ResourceType = typeof(UserInfos))]  
		public int? CityId { get; set; }

		[Required]
        [Display(Name = "LanguageId", ResourceType = typeof(UserInfos))]
        public int LanguageId { get; set; }

		[Required]
        [Display(Name = "TimeZoneId", ResourceType = typeof(UserInfos))]
        public int TimeZoneId { get; set; }

        [StringLength(200)]
        [Display(Name = "Address", ResourceType = typeof(UserInfos))]  
        public string Address { get; set; }

        [StringLength(200)]
        [Display(Name = "Specialty", ResourceType = typeof(UserInfos))]  
        public string Specialty { get; set; }

        public TimeSpan? WorkTimeFrom { get; set; }
        public TimeSpan? WorkTimeTo { get; set; }

		public DateTime CreateDateUtc { get; set; }

		public DateTime UpdateDateUtc { get; set; }

        public bool IsLocked { get; set; }

        public bool IsDeleted { get; set; }

        [StringLength(1000)]
        public string Notepad { get; set; }

        public List<SelectListItem> genderItems;
		public List<SelectListItem> countryItems;
		public List<SelectListItem> cityItems;
        public List<SelectListItem> languageItems;
        public List<SelectListItem> timeZoneItems;

        public HttpPostedFileBase UserInfoPhoto { get; set; }
    }
}
 