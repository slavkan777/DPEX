using System;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using DoctorPremium.Services.Interfaces;
using DoctorPremium.Common;
using DoctorPremium.Common.Helpers;
using DoctorPremium.DAL.Model;
using DoctorPremium.Web.Helpers;
using DoctorPremium.Web.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.DataProtection;
using Resources;
using log4net;
using System.IO;

namespace DoctorPremium.Web.Controllers
{

    [Authorize]
    public class AccountController : BaseController
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        private ApplicationRoleManager _roleManager;
        private IMailService _mailService;
        private IUserInfoService _userInfoService;
        private ICountryService _countryService;
        private ICityService _cityService;
        private CountryAndCityHelper _countryAndCityHelper;
        private ILanguageService _languageService;
        private ITimeZoneService _timeZoneService;
		private const string emailPath = "~/Content/Emails";

		public bool valCaptcha; 

        public AccountController()
        {

        }

        public AccountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager,
            IMailService mailService,
            IUserInfoService userInfoService, ICountryService countryService, ICityService cityService,
            ILanguageService languageService, ITimeZoneService timeZoneService)
        {
            UserManager = userManager;
            SignInManager = signInManager;
            this._mailService = mailService;
            this._userInfoService = userInfoService;
            this._countryService = countryService;
            this._cityService = cityService;
            this._languageService = languageService;
            this._timeZoneService = timeZoneService;
        }

        public ApplicationSignInManager SignInManager
        {
            get { return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>(); }
            private set { _signInManager = value; }
        }

        public ApplicationUserManager UserManager
        {
            get { return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>(); }
            private set { _userManager = value; }
        }

        public ApplicationRoleManager RoleManager
        {
            get { return _roleManager ?? HttpContext.GetOwinContext().Get<ApplicationRoleManager>(); }
            private set { _roleManager = value; }
        }

        public CountryAndCityHelper CountryAndCityHelper
        {
            get { return _countryAndCityHelper ?? new CountryAndCityHelper(_countryService, _cityService); }
        }

        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await UserManager.FindByNameAsync(model.Email);
            if (user != null)
            {
                if (!await UserManager.IsEmailConfirmedAsync(user.Id))
                {
                    ViewBag.errorMessage = ErrorMessageHelper.NotConfirmedEmail;
                    return View("Error");
                }
            }
            var result =
                await
                    SignInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe,
                        shouldLockout: false);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToAction("Index", "Schedule");;
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.RequiresVerification:
                    return null;
                //RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = model.RememberMe });
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Invalid login or password.");
                    return View(model);
            }
        }

        //[AllowAnonymous]
        //public async Task<ActionResult> VerifyCode(string provider, string returnUrl, bool rememberMe)
        //{
        //	// Require that the user has already logged in via username/password or external login
        //	if (!await SignInManager.HasBeenVerifiedAsync())
        //	{
        //		return View("Error");
        //	}
        //	return View(new VerifyCodeViewModel { Provider = provider, ReturnUrl = returnUrl, RememberMe = rememberMe });
        //}

        //[HttpPost]
        //[AllowAnonymous]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> VerifyCode(VerifyCodeViewModel model)
        //{
        //	if (!ModelState.IsValid)
        //	{
        //		return View(model);
        //	}
        //	var result = await SignInManager.TwoFactorSignInAsync(model.Provider, model.Code, isPersistent:  model.RememberMe, rememberBrowser: model.RememberBrowser);
        //	switch (result)
        //	{
        //		case SignInStatus.Success:
        //			return RedirectToLocal(model.ReturnUrl);
        //		case SignInStatus.LockedOut:
        //			return View("Lockout");
        //		case SignInStatus.Failure:
        //		default:
        //			ModelState.AddModelError("", "Invalid code.");
        //			return View(model);
        //	}
        //}

        [AllowAnonymous]
        public ActionResult Register()
        {
            RegisterViewModel model = new RegisterViewModel();
            //bool valCaptcha = (bool)Session["captcha"];
            //	string countryName = RegionInfo.CurrentRegion.DisplayName;
            model.LanguageId = LanguageHelper.GetCurrentLanguageId();
            DoctorPremium.DAL.Model.TimeZone tz = TimeZoneHelper.GetTimeZoneBySystemId(_timeZoneService,
                TimeZoneInfo.Local.Id);
            if (tz != null)
            {
                model.TimeZoneId = tz.TimeZoneId;
            }

            //model.CountryId = _countryService.GetCountryIdbyName(countryName);			
            FillDropdowns(model);
            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model, string CaptchaText,  bool checkboxLicanse)
        {
           

            if (CaptchaText != HttpContext.Session["captchastring"] as string)
            {
                ViewBag.Message = (string)Account.CapsButton;

                ModelState.AddModelError("Captcha", (string)Account.CapsButton);
            }

            if (!checkboxLicanse)
            {
                ViewBag.Message = (string)Account.CheckLicense;

                ModelState.AddModelError("checkboxLicanse", (string)Account.CheckLicense);
            }

            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                UserManager.UserValidator = new UserValidator<ApplicationUser, int>(UserManager)
                {
                    AllowOnlyAlphanumericUserNames = false,
                    RequireUniqueEmail = true
                };
                var result = await UserManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    await UserManager.AddToRoleAsync(user.Id, "Doctor");
                    _userInfoService.CreateNewUserInfo(new UserInfo(user.Id, model.TimeZoneId, model.LanguageId));

                    SetTokenProvider();
                    var code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                    var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = HttpUtility.UrlEncode(code) }, protocol: Request.Url.Scheme);


					string mailBody = GetFileContent(Path.Combine(Server.MapPath(emailPath), (LanguageHelper.GetCurrentLanguage() == "ru"? "ConfirmMail.html" : "ConfirmMailEn.html")));
					mailBody = mailBody.Replace("@USER@", user.UserName);
					mailBody = mailBody.Replace("@CONFIRMLINK@", callbackUrl);
					mailBody = mailBody.Replace("@CODE@", code);
					mailBody = mailBody.Replace("@MANUALCONFIRMLINK@", Url.Action("ConfirmEmailManual", "Account", new { uId = user.Id }, protocol: Request.Url.Scheme));

					LogManager.GetLogger(typeof(MvcApplication)).Info(String.Format("Mail register for user {0}. Body: {1}", user.UserName, String.Format("{0} <a href='{1}'>{2}</a>", Account.TopicConfirmBody, callbackUrl, Account.ConfirmLink)));

					await _mailService.SendEmailAsync(model.Email, Account.TopicConfirm, mailBody);

                    return View("ConfirmEmailSent"); //return RedirectToAction("Index", "Home");
                }
                AddErrors(result);
            }
            FillDropdowns(model);
            return View(model);
        }

        [AllowAnonymous]
        public CaptchaImageResult ShowCaptchaImage()
        {
            return new CaptchaImageResult();
        }

        [NonAction]
        private void FillDropdowns(RegisterViewModel model)
        {
            /*model.countryItems.AddRange(CountryAndCityHelper.GetCountryDropList());
            if (model.CountryId != null)
            {
                model.cityItems.AddRange(CountryAndCityHelper.GetCityDropList((int)model.CountryId));
            }*/
            model.languageItems = LanguageHelper.GetLanguageDropList(_languageService);
            model.timeZoneItems = TimeZoneHelper.GetTimeZoneDropList(_timeZoneService);
        }

        [AllowAnonymous]
        public async Task<ActionResult> ConfirmEmail(int userId, string code)
        {
            ViewBag.errorMessage = ErrorMessageHelper.NotValidConfirmedCode;
            if (userId == default(int) || code == null)
            {
                return View("Error");
            }
            SetTokenProvider();
            var result = await UserManager.ConfirmEmailAsync(userId, HttpUtility.UrlDecode(code));

            return View(result.Succeeded ? "ConfirmEmail" : "Error");
        }

		[HttpGet]
		[AllowAnonymous]
		public ActionResult ConfirmEmailManual(int uId)
		{
			if (uId > 0)
			{
				ConfirmEmailManualViewModel model = new ConfirmEmailManualViewModel() { UserId = uId };
				return View(model);
			}
			else
			{
				return View("Error");
			}
		}

		[HttpPost]
		[AllowAnonymous]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> ConfirmEmailManual(ConfirmEmailManualViewModel model)
		{
			if (!ModelState.IsValid)
			{
				return View(model);
			}
			
			if (model.UserId == default(int) || String.IsNullOrEmpty(model.ConfirmCode))
			{
				ViewBag.errorMessage = ErrorMessageHelper.NotValidConfirmedCode;
				return View("Error");
			}
			SetTokenProvider();
			var result = await UserManager.ConfirmEmailAsync(model.UserId, model.ConfirmCode);

			if (result.Succeeded)
			{
				return View("ConfirmEmail");
			}
			else
			{
				LogManager.GetLogger(typeof(MvcApplication)).Error(String.Format("ConfirmEmailManual error: UserID={0} Erors = {1}", model.UserId, result.Errors.ToList()[0]));

				ViewBag.errorMessage = ErrorMessageHelper.NotValidConfirmedCode;
				return View("Error");
			}
		}

		[AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByNameAsync(model.Email);
                if (user == null || !(await UserManager.IsEmailConfirmedAsync(user.Id)))
                {
                    return View("ForgotPasswordConfirmation");
                }
                SetTokenProvider();
                var code = await UserManager.GeneratePasswordResetTokenAsync(user.Id);
                var callbackUrl = Url.Action("ResetPassword", "Account",
                new { UserId = user.Id, code = HttpUtility.UrlEncode(code) }, protocol: Request.Url.Scheme);

				string mailBody = GetFileContent(Path.Combine(Server.MapPath(emailPath), (LanguageHelper.GetCurrentLanguage() == "ru" ? "ForgotPassword.html" : "ForgotPasswordEn.html")));
				mailBody = mailBody.Replace("@USER@", user.UserName);
				mailBody = mailBody.Replace("@RESETLINK@", callbackUrl);

				LogManager.GetLogger(typeof(MvcApplication)).Info(String.Format("Mail ForgotPassword for user {0}. Body: {1}", user.UserName, Account.ResetPasswordBody + " <a href=\"" + callbackUrl + "\">link reset password</a>"));
				await _mailService.SendEmailAsync(user.Email, Account.ResetPassword, mailBody);
                return View("ForgotPasswordConfirmation");
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        [AllowAnonymous]
        public ActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult ResetPassword(string code)
        {
            return code == null ? View("Error") : View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await UserManager.FindByNameAsync(model.Email);
            if (user == null)
            {
                // Don't reveal that the user does not exist
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }
            SetTokenProvider();
            var result = await UserManager.ResetPasswordAsync(user.Id, HttpUtility.UrlDecode(model.Code), model.Password);
            if (result.Succeeded)
            {
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }
            AddErrors(result);
            return View();
        }

        [AllowAnonymous]
        public ActionResult ResetPasswordConfirmation()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            UserInfoHelper.Clear();
            return RedirectToAction("Index", "Home");
        }

        [AllowAnonymous]
        public ActionResult ConfirmEmailSent()
        {
            return View();
        }

        private void SetTokenProvider()
        {
            if (UserManager.UserTokenProvider == null)
            {
                UserManager.UserTokenProvider =
                    new DataProtectorTokenProvider<ApplicationUser, int>(Startup.DataProtectionProvider.Create("DoctorPremium"))
                {
                    TokenLifespan = TimeSpan.FromHours(3)
                };
            }
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult LoginTestAccount()
        {
            String email = "";
            String password = "doc123456";
            switch (LanguageHelper.GetCurrentLanguage())
            {
                case "ru":
                    email = "testdocru@mail.com";
                    break;
                case "en":
                    email = "testdoc@mail.com";
                    break;
                default:
                    email = "testdoc@mail.com";
                    break;
            }
            var result = SignInManager.PasswordSignIn(email, password, false, shouldLockout: false);
            return RedirectToAction("Index", "Schedule");
        }

        #region NotUse

        //[HttpPost]
        //[AllowAnonymous]
        //[ValidateAntiForgeryToken]
        //public ActionResult ExternalLogin(string provider, string returnUrl)
        //{
        //    // Request a redirect to the external login provider
        //    return new ChallengeResult(provider, Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl }));
        //}

        //[AllowAnonymous]
        //public async Task<ActionResult> ConfirmEmailAgain(int userId, string code)
        //{
        //	return View();
        //}

        //[HttpPost]
        //[AllowAnonymous]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> ConfirmEmailAgain(ConfirmEmailAgainViewModel model)
        //{
        //	if (ModelState.IsValid)
        //	{
        //		var user = await UserManager.FindByEmailAsync(model.Email);
        //		if (user == null || (await UserManager.IsEmailConfirmedAsync(user.Id)))
        //		{
        //			return View("ConfirmEmailAgainSent");
        //		}
        //			SetTokenProvider();
        //			var code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
        //			var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = HttpUtility.UrlEncode(code) }, protocol: Request.Url.Scheme);//protocol: Context.Request.Scheme
        //			await _mailService.SendEmailAsync(model.Email, "Confirm your account",
        //				"Please confirm your account by clicking this link: <a href=\"" + callbackUrl + "\">link</a>");
        //			return View("ConfirmEmailAgainSent");
        //	}
        //	return View(model);
        //}

        //[AllowAnonymous]
        //public async Task<ActionResult> SendCode(string returnUrl, bool rememberMe)
        //{
        //	var userId = await SignInManager.GetVerifiedUserIdAsync();
        //	if (userId == default(int))
        //	{
        //		return View("Error");
        //	}
        //	var userFactors = await UserManager.GetValidTwoFactorProvidersAsync(userId);
        //	var factorOptions = userFactors.Select(purpose => new SelectListItem { Text = purpose, Value = purpose }).ToList();
        //	return View(new SendCodeViewModel { Providers = factorOptions, ReturnUrl = returnUrl, RememberMe = rememberMe });
        //}

        //[HttpPost]
        //[AllowAnonymous]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> SendCode(SendCodeViewModel model)
        //{
        //	if (!ModelState.IsValid)
        //	{
        //		return View();
        //	}

        //	// Generate the token and send it
        //	if (!await SignInManager.SendTwoFactorCodeAsync(model.SelectedProvider))
        //	{
        //		return View("Error");
        //	}
        //	return RedirectToAction("VerifyCode", new { Provider = model.SelectedProvider, ReturnUrl = model.ReturnUrl, RememberMe = model.RememberMe });
        //}

        //[AllowAnonymous]
        //public async Task<ActionResult> ExternalLoginCallback(string returnUrl)
        //{
        //    var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync();
        //    if (loginInfo == null)
        //    {
        //        return RedirectToAction("Login");
        //    }

        //    // Sign in the user with this external login provider if the user already has a login
        //    var result = await SignInManager.ExternalSignInAsync(loginInfo, isPersistent: false);
        //    switch (result)
        //    {
        //        case SignInStatus.Success:
        //            return RedirectToLocal(returnUrl);
        //        case SignInStatus.LockedOut:
        //            return View("Lockout");
        //        case SignInStatus.RequiresVerification:
        //            return null;//RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = false });
        //        case SignInStatus.Failure:
        //        default:
        //            // If the user does not have an account, then prompt the user to create an account
        //            ViewBag.ReturnUrl = returnUrl;
        //            ViewBag.LoginProvider = loginInfo.Login.LoginProvider;
        //            return View("ExternalLoginConfirmation", new ExternalLoginConfirmationViewModel { Email = loginInfo.Email });
        //    }
        //}

        //[HttpPost]
        //[AllowAnonymous]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> ExternalLoginConfirmation(ExternalLoginConfirmationViewModel model, string returnUrl)
        //{
        //    if (User.Identity.IsAuthenticated)
        //    {
        //        return RedirectToAction("Index", "Manage");
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        // Get the information about the user from the external login provider
        //        var info = await AuthenticationManager.GetExternalLoginInfoAsync();
        //        if (info == null)
        //        {
        //            return View("ExternalLoginFailure");
        //        }
        //        var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
        //        var result = await UserManager.CreateAsync(user);
        //        if (result.Succeeded)
        //        {
        //            result = await UserManager.AddLoginAsync(user.Id, info.Login);
        //            if (result.Succeeded)
        //            {
        //                await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
        //                return RedirectToLocal(returnUrl);
        //            }
        //        }
        //        AddErrors(result);
        //    }

        //    ViewBag.ReturnUrl = returnUrl;
        //    return View(model);
        //}

        //[AllowAnonymous]
        //public ActionResult ExternalLoginFailure()
        //{
        //    return View();
        //}

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_userManager != null)
                {
                    _userManager.Dispose();
                    _userManager = null;
                }

                if (_signInManager != null)
                {
                    _signInManager.Dispose();
                    _signInManager = null;
                }
            }

            base.Dispose(disposing);
        }
        #endregion

        #region Helpers
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

		private static string GetFileContent(string filePath)
		{
			string content;
			using (TextReader tr = new StreamReader(filePath))
			{
				content = tr.ReadToEnd();
			}

			return content;
		}

		internal class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri)
                : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties { RedirectUri = RedirectUri };
                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }
        #endregion
    }
}