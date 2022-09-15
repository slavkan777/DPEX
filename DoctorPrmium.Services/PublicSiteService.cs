using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DoctorPremium.Services.Interfaces;
using DoctorPremium.DAL.Interfaces;
using DoctorPremium.DAL.Model;

namespace DoctorPremium.Services
{
    public  class PublicSiteService : IPublicSiteService
    {
        private IUnitOfWork _unitOfWork;
		private IGenericRepository<UserPublicPage> _userPublicPageRepository;

        public PublicSiteService(IUnitOfWork unitOfPublicPage, IGenericRepository<UserPublicPage> PublicPageRepository)
		{
			this._unitOfWork = unitOfPublicPage;
			this._userPublicPageRepository = PublicPageRepository;
		}

        public UserPublicPage GetPublicSiteInfo(int UserInfoId)
		{
            return _userPublicPageRepository.FindBy(x => x.UserId == UserInfoId).FirstOrDefault();
		}

		public UserPublicPage GetPublicSiteInfoIfIsDeleted(int UserInfoId)
		{
			return _userPublicPageRepository.FindBy(x => x.UserId == UserInfoId && (bool)!x.IsDeleted).FirstOrDefault();
		}

		public UserPublicPage GetPublicSiteInfoIfIsDeletedPlusCounter(int UserInfoId)
		{
			UserPublicPage page = _userPublicPageRepository.FindBy(x => x.UserId == UserInfoId).FirstOrDefault();
			if (page != null)
			{
				page.VisitCounter ++;
				_userPublicPageRepository.Update(page);
			}
			return page;
		}

		public List<UserPublicPage> GetActivePublicSites()
		{
			return _userPublicPageRepository.FindBy(x => !x.IsDeleted && x.IsPublic).ToList();
		}

        public UserPublicPage Save(UserPublicPage _publicPageInfo)
        {
            if (_publicPageInfo.Id == 0)
            {
				_publicPageInfo.CreateDateUtc = DateTime.UtcNow;
                return _userPublicPageRepository.Add(_publicPageInfo);
            }
            else
            {
				_publicPageInfo.UpdateDateUtc = DateTime.UtcNow;
                _userPublicPageRepository.Update(_publicPageInfo);
                return null; 
            }    
        }
    }
}
