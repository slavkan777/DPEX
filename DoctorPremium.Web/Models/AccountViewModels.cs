using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.AccessControl;
using System.Web.Mvc;
using Resources;

namespace DoctorPremium.Web.Models
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
		[Display(Name = "Email", ResourceType = typeof(Account))]
        public string Email { get; set; }
    }

    public class ExternalLoginListViewModel
    {
        public string ReturnUrl { get; set; }
    }

    public class SendCodeViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
        public string ReturnUrl { get; set; }
        public bool RememberMe { get; set; }
    }

    public class VerifyCodeViewModel
    {
        [Required]
        public string Provider { get; set; }

        [Required]
        [Display(Name = "Code")]
        public string Code { get; set; }
        public string ReturnUrl { get; set; }

        [Display(Name = "Remember this browser?")]
        public bool RememberBrowser { get; set; }

        public bool RememberMe { get; set; }
    }

    public class ForgotViewModel
    {
        [Required]
		[Display(Name = "Email", ResourceType = typeof(Account))]
        public string Email { get; set; }
    }

    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Email", ResourceType = typeof(Account))]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password", ResourceType = typeof(Account))]
        public string Password { get; set; }

		[Display(Name = "RememberMe", ResourceType = typeof(Account))]
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
    {
		public RegisterViewModel()
		{
			//countryItems = new List<SelectListItem>();
			//cityItems = new List<SelectListItem>();
			languageItems = new List<SelectListItem>();
			timeZoneItems = new List<SelectListItem>();
		}

        [Required]
        [EmailAddress]
		[StringLength(80, ErrorMessageResourceType=typeof(Account),ErrorMessageResourceName ="MustBeLeast", MinimumLength = 6)]
		[Display(Name = "Email", ResourceType = typeof(Account))]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessageResourceType=typeof(Account),ErrorMessageResourceName ="MustBeLeast", MinimumLength = 6)]
        [DataType(DataType.Password)]
		[Display(Name = "Password", ResourceType = typeof(Account))]
        public string Password { get; set; }

        [DataType(DataType.Password)]
		[Display(Name = "ConfirmPassword", ResourceType = typeof(Account))]
        [System.ComponentModel.DataAnnotations.Compare("Password")]
        public string ConfirmPassword { get; set; }

		[Required]
		[Display(Name = "CountryId", ResourceType = typeof(PatientInfo))]
        public int CountryId { get; set; }

		[Required]
        [Display(Name = "CityId", ResourceType = typeof(PatientInfo))]
		public int CityId { get; set; }

		[Required]
		[Display(Name = "LanguageId", ResourceType = typeof(UserInfos))]
		public int LanguageId { get; set; }

		[Required]
		[Display(Name = "TimeZoneId", ResourceType = typeof(UserInfos))]
		public int TimeZoneId { get; set; }

		//public List<SelectListItem> countryItems;
		//public List<SelectListItem> cityItems;
		public List<SelectListItem> languageItems;
		public List<SelectListItem> timeZoneItems;
    }

    public class ResetPasswordViewModel
    {
        [Required]
        [EmailAddress]
		[Display(Name = "Email", ResourceType = typeof(Account))]
        public string Email { get; set; }

        [Required]
		[StringLength(100, ErrorMessageResourceType = typeof(Account), ErrorMessageResourceName = "MustBeLeast", MinimumLength = 6)]
        [DataType(DataType.Password)]
		[Display(Name = "Password", ResourceType = typeof(Account))]
        public string Password { get; set; }

        [DataType(DataType.Password)]
		[Display(Name = "ConfirmPassword", ResourceType = typeof(Account))]
        [System.ComponentModel.DataAnnotations.Compare("Password")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
		[Display(Name = "Email", ResourceType = typeof(Account))]
        public string Email { get; set; }
    }

	public class ConfirmEmailAgainViewModel
	{
		[Required]
		[EmailAddress]
		[Display(Name = "Email", ResourceType = typeof(Account))]
		public string Email { get; set; }
	}

	public class ConfirmEmailManualViewModel
	{
		[Required]
		public int UserId { get; set; }

		[Required]
		[Display(Name = "ConfirmCode", ResourceType = typeof(Account))]
		public string ConfirmCode { get; set; }
	}
}
