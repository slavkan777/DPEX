using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using DoctorPremium.Services.Interfaces;
using DoctorPremium.DAL.Model;
using DoctorPremium.Web.Helpers;
using DoctorPremium.Web.Models;
using log4net;
using Microsoft.AspNet.Identity;
using DoctorPrmium.Services.Interfaces;

namespace DoctorPremium.Web.Controllers
{
    [Authorize(Roles = "Doctor")]
    public class UserInfoController : BaseController
    {
        private IUserInfoService _UserInfoService;
        private ICountryService _countryService;
        private ICityService _cityService;
        private CountryAndCityHelper _countryAndCityHelper;
        private ILanguageService _languageService;
        private ITimeZoneService _timeZoneService;
        private ISettingService _settingService;

        public CountryAndCityHelper CountryAndCityHelper
        {
            get
            {
                return _countryAndCityHelper ?? new CountryAndCityHelper(_countryService, _cityService);
            }
        }

        public UserInfoController(IUserInfoService userInfoService, ICountryService countryService, ICityService cityService, ILanguageService languageService, ITimeZoneService timeZoneService, ISettingService settingService)
        {
            this._UserInfoService = userInfoService;
            this._countryService = countryService;
            this._cityService = cityService;
            this._languageService = languageService;
            this._timeZoneService = timeZoneService;
            this._settingService = settingService;
        }

        public ActionResult Edit()
        {
            ProfileEditViewModel model = new ProfileEditViewModel();
            int UserId = Convert.ToInt32(User.Identity.GetUserId());
            UserInfo userInfo = _UserInfoService.GetUserInfo(UserId);
            if (userInfo == null)
            {
                throw new HttpException(404, "User info not found"); // TODO: need not found page
            }
            else
            {
                model.FromEntity(userInfo);
            }

            FillDropdowns(model);
            return View(model);

        }

        [HttpPost]
        public async Task<ActionResult> Edit(ProfileEditViewModel model, CancellationToken cancellationToken)
        {
            if (ModelState.IsValid)
            {
                int UserId = Convert.ToInt32(User.Identity.GetUserId());
                UserInfo oldUserInfo = _UserInfoService.GetUserInfo(UserId);
                if (oldUserInfo == null)
                {
                    throw new HttpException(404, "Patient not found"); // TODO: need not found patient page
                }
                else
                {
                    UserInfo _UserInfo = model.ToEntity(model, oldUserInfo);
                    String newPhoto = SaveUserPhoto(model.UserInfoPhoto, model.UserInfoId, model.Photo);
                    if (!String.IsNullOrEmpty(newPhoto))
                    {
                        _UserInfo.Photo = newPhoto;
                    }
                    UserInfoHelper.Clear();
                    _UserInfoService.Save(_UserInfo);
                }
                return RedirectToAction("Index", "Schedule");
            }
            FillDropdowns(model);
            return View(model);
        }

        [ChildActionOnly]
        public ActionResult Notepad()
        {
            ProfileEditViewModel model = new ProfileEditViewModel();
            int UserId = Convert.ToInt32(User.Identity.GetUserId());
            UserInfo userInfo = _UserInfoService.GetUserInfo(UserId);
            if (userInfo == null)
            {
                throw new HttpException(404, "Patient not found"); // TODO: need not found page
            }
            else
            {
                model.FromEntity(userInfo);
            }

            FillDropdowns(model);
            ViewData["Name"] = model.Notepad;
            //return PartialView(model);
            return PartialView();
        }

        [ChildActionOnly]
        public ActionResult Warning()
        {

            //ProfileEditViewModel model = new ProfileEditViewModel();
            int UserId = Convert.ToInt32(User.Identity.GetUserId());
            UserInfo userInfo = _UserInfoService.GetUserInfo(UserId);
            var vallLabel = Session["LabelWarning"];
            if (userInfo.AspNetUser.Email == "testdocru@mail.com" ||
                userInfo.AspNetUser.Email == "testdoc@mail.com")
            {
                if (vallLabel == null)
                {
                    return PartialView();
                }
               
                 
            }
            //ViewData["Login"] = userInfo.AspNetUser.AspNetUserLogins;
            //return PartialView(model);
            return null;
        }


        public ActionResult LabelSession(bool LabVal)
        {
            bool success = false;
            Session["LabelWarning"] = LabVal;
            success = true;
            var v = new { success = success, error = "" };
            return Json(v);
        }

        public ActionResult Paypal()
        {
            var sett = _settingService.GetByName("PayPalClick");
            if (sett != null)
            {
                int count = 0;
                Int32.TryParse(sett.SettingValue, out count);
                count++;
                sett.SettingValue = count.ToString();
                _settingService.Update(sett);
            }
            else
            {
                Setting newsett = new Setting() { SettingName = "PayPalClick", SettingValue= "1" };
                _settingService.Save(newsett);
            }
            return Redirect("https://www.paypal.com/cgi-bin/webscr?cmd=_s-xclick&hosted_button_id=3R8PUH8H2SPYJ&source=url");
        }

        [HttpPost]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Notepad(string textareaNotePadVal) //добавление инфы в расспсиании
        {
            if (textareaNotePadVal != null)
            {

                int UserId = Convert.ToInt32(User.Identity.GetUserId());
                bool success = false;
                UserInfo oldUserInfo = _UserInfoService.GetUserInfo(UserId);
                if (oldUserInfo == null)
                {
                    throw new HttpException(404, "Patient not found"); // TODO: need not found patient page
                }
                else
                {
                    oldUserInfo.Notepad = textareaNotePadVal;
                    _UserInfoService.Save(oldUserInfo);
                    success = true;
                }
                var v = new { success = success, error = "" };
                return Json(v);
            }
            else
            {
                return null;
            }
        }

        private string SaveUserPhoto(HttpPostedFileBase photo, int UserInfoId, string oldFileName)
        {
            try
            {
                if (photo != null && photo.ContentLength > 0 && photo.ContentType.Contains("image"))
                {
                    String fileName = ImageNameHelper.GetFileNameUserInfoPhoto(UserInfoId, photo.FileName);
                    var path = Path.Combine(Server.MapPath("~/Photos/"), System.IO.Path.GetFileName(fileName));
	                if (!String.IsNullOrEmpty(oldFileName))
	                {
		                DeletePatientPhoto(Path.Combine(Server.MapPath("~/Photos/"), System.IO.Path.GetFileName(oldFileName)));
	                }
	                photo.SaveAs(path);
                    return fileName;
                }
            }
            catch (Exception ex)
            {
                LogManager.GetLogger(typeof(MvcApplication)).Error("error file saving");
				throw ex;
			}

            return null;
        }

        private void DeletePatientPhoto(String path)
        {
            if (!String.IsNullOrEmpty(path) && System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }
        }

        //[HttpPost]
            //public ActionResult WriteReview()
            //{
            //    return null;// View();
            //}

        [HttpPost]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult GetCityList(int countryid)
        {
            bool success = false;
            List<SelectListItem> list = new List<SelectListItem>();
            if (countryid != 0)
            {
                list = CountryAndCityHelper.GetCityDropList(countryid);
                success = true;
            }
            var v = new { success = success, error = "", value = list };
            return Json(v);
        }

        [NonAction]
        private void FillDropdowns(ProfileEditViewModel model)
        {
            model.countryItems.AddRange(CountryAndCityHelper.GetCountryDropList());
            if (model.CountryId != null)
            {
                model.cityItems.AddRange(CountryAndCityHelper.GetCityDropList((int)model.CountryId));
            }
            model.languageItems = LanguageHelper.GetLanguageDropList(_languageService);
            model.timeZoneItems = TimeZoneHelper.GetTimeZoneDropList(_timeZoneService);
        }
    }
}