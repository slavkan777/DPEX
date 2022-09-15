using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using DoctorPremium.Services.Interfaces;
using DoctorPremium.DAL.Model;
using DoctorPremium.Web.Helpers;
using DoctorPremium.Web.Models;
using DoctorPremium.Web.Resources;
using Microsoft.AspNet.Identity;
using Resources;

namespace DoctorPremium.Web.Controllers
{
	[Authorize(Roles = "Doctor")]
    public class PublicSiteController : BaseController
    {
        private IPublicSiteService _UserPublicSiteService;
        private IUserInfoService _UserInfoService;
        public PublicSiteController(IPublicSiteService userPublicPageService, IUserInfoService userInfoService)
        {
            _UserPublicSiteService = userPublicPageService;
            _UserInfoService = userInfoService;
        }

        [Authorize(Roles = "Doctor")]
        public ActionResult IsPublic()
        {
			throw new NotImplementedException();
        /*    int PublicSiteId = Convert.ToInt32(User.Identity.GetUserId());
            //bool isAutorise = true;
            //UserInfo userInfo = _UserInfoService.GetUserInfo(PublicSiteId);
            PublicSiteModels model = new PublicSiteModels();

            UserPublicPage PublicSiteInfo = _UserPublicSiteService.GetPublicSiteInfo(PublicSiteId);

            if (PublicSiteInfo != null)
            {
                if (PublicSiteInfo.IsPublic == true)
                {
                    return RedirectToAction("Index", new {Userid = PublicSiteId});
                }
                else
                {
                    return RedirectToAction("EditIndex", new {Userid = PublicSiteId});
                }
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }*/
        }

		[Authorize(Roles = "Doctor")]
        public ActionResult EditIndex(int Userid)
        {
			throw new NotImplementedException();
        /*    UserInfo userInfo = _UserInfoService.GetUserInfo(Userid);
            PublicSiteModels model = new PublicSiteModels();
            int PublicSiteId = Convert.ToInt32(User.Identity.GetUserId());
            UserPublicPage PublicSiteInfo = _UserPublicSiteService.GetPublicSiteInfo(PublicSiteId);

            if (PublicSiteInfo == null)
            {
                throw new HttpException(404, "Info not found"); // TODO: need not found page
            }
            else
            {
				model.NameOfDoctor = FullNameHelper.GetFullName(userInfo.LastName, userInfo.FirstName, userInfo.SurName);
                model.FromEntity(PublicSiteInfo);
                model.UserInfoPhoto = userInfo.Photo;
            }
            return View("Index", model);*/
        }

        // GET: PublicSite
		[AllowAnonymous]
        public ActionResult Index(int Userid, bool? isPreview)
        {
			UserPublicPage publicSiteInfo = _UserPublicSiteService.GetPublicSiteInfoIfIsDeleted(Userid);

			if (publicSiteInfo == null || (!publicSiteInfo.IsPublic && !(isPreview == true && User.Identity.IsAuthenticated && Int32.Parse(User.Identity.GetUserId()) == Userid)))
			{
				return View("PublicSiteLost"); //new HttpNotFoundResult(); RedirectToAction("PublicSiteLost", "Error");
				//throw new HttpException(404, "Info not found"); // TODO: need not found page
			}
			else
			{
				UserInfo userInfo = _UserInfoService.GetUserInfo(Userid);
				PublicSiteModels model = new PublicSiteModels();

				//counter ++ 
				publicSiteInfo.VisitCounter += isPreview != true ? 1 : 0;
				model.NameOfDoctor = FullNameHelper.GetFullName(userInfo.LastName, userInfo.FirstName, userInfo.SurName);
				model.FromEntity(publicSiteInfo);
				model.UserInfoPhoto = userInfo.Photo;
				if (isPreview != true)
				{
					_UserPublicSiteService.Save(publicSiteInfo); //save counter
				}
				return View(model);
			}    
        }

        [Authorize(Roles = "Doctor")]
        public ActionResult Settings()
        {
            int UserId = Convert.ToInt32(User.Identity.GetUserId());
            UserInfo userInfo = _UserInfoService.GetUserInfo(UserId);
			UserPublicPage page = _UserPublicSiteService.GetPublicSiteInfoIfIsDeleted(UserId);
            PublicSiteModels model = new PublicSiteModels();

            if (page == null)
            {
                //model.IsPublic = true;
                model.UserId = UserId;
                model.NameOfDoctor = FullNameHelper.GetFullName(userInfo.LastName, userInfo.FirstName, userInfo.SurName);
            }
            else
            {
	            if (page.IsDeleted)
	            {
					string Val = PublicS.SiteIsDeteted;	
					return Content(Val, "text/html");
	            }
                model.FromEntity(page);
            }

            return View(model);
        }

        
        [HttpPost]
		[Authorize(Roles = "Doctor")]
		public async Task<ActionResult> Settings(PublicSiteModels model, CancellationToken cancellationToken)
        {
            if (ModelState.IsValid)
            {
                int UserId = Convert.ToInt32(User.Identity.GetUserId());
                UserPublicPage oldPublicInfo = _UserPublicSiteService.GetPublicSiteInfoIfIsDeleted(UserId);

                if (oldPublicInfo == null)
                {
                    UserPublicPage _PublicInfo = model.ToEntity(model, new UserPublicPage());
                    _UserPublicSiteService.Save(_PublicInfo);
                }
                else
                {
					if (oldPublicInfo.IsDeleted)
					{
						string Val = PublicS.SiteIsDeteted;
						return Content(Val, "text/html");
					}

                    UserPublicPage _PublicInfo = model.ToEntity(model, oldPublicInfo);
                    _UserPublicSiteService.Save(_PublicInfo);
					
					return RedirectToAction("Index", "Schedule");
                }
            }
			return View(model); 
        }

		[NonAction]
		public ActionResult PublicSiteLost()
		{
			return View();
		}
    }
}