using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using DoctorPremium.DAL.Model;
using Resources;

namespace DoctorPremium.Web.Models
{
    public class PublicSiteModels
    {
        public PublicSiteModels()
        {

        }

        public UserPublicPage ToEntity(PublicSiteModels model, UserPublicPage oldPublicPage)
        {
            UserPublicPage newPublicInfo = oldPublicPage;
            newPublicInfo.AboutOfDoctor = model.AboutOfDoctor != null ? model.AboutOfDoctor.Trim() : null;
            newPublicInfo.DoctorService = model.DoctorService != null ? model.DoctorService.Trim() : null;
            newPublicInfo.PublicEmailAddress = model.PublicEmailAddress != null ? model.PublicEmailAddress.Trim() : null;
        //    newPublicInfo.IsDeleted = model.IsDeleted;
            newPublicInfo.IsPublic = model.IsPublic;
            newPublicInfo.PublicAddressForPatients = model.PublicAddressForPatients != null ? model.PublicAddressForPatients.Trim() : null;
            newPublicInfo.PublicPhone = model.PublicPhone != null ? model.PublicPhone.Trim() : null;
            newPublicInfo.UserId = model.UserId;
            return newPublicInfo;
        }

        public void FromEntity(UserPublicPage _PublicPage)
        {
            Mapper.CreateMap<UserPublicPage, PublicSiteModels>();
            Mapper.Map(_PublicPage, this);
        }

		public int Id { get; set; }
		
        [Required]
        public int UserId { get; set; }

        //[Required(ErrorMessageResourceType = typeof(PatientInfo), ErrorMessageResourceName = "BirthDate")]
        //[Display(Name = "BirthDate", ResourceType = typeof(PatientInfo))]
    
        [StringLength(200)]
        [Display(Name = "Name", ResourceType = typeof(PublicS))] 
        public string NameOfDoctor { get; set; }

        [StringLength(4000)]
        [Display(Name = "About", ResourceType = typeof(PublicS))] 
        public string AboutOfDoctor { get; set; }

        [StringLength(2147483647)]
        [Display(Name = "DoctorService", ResourceType = typeof(PublicS))] 
        [AllowHtml]
        public string DoctorService { get; set; }

        [StringLength(100)]
        [Display(Name = "Email:")]
        public string PublicEmailAddress { get; set; }

        [StringLength(500)]
        [Display(Name = "PublicAddressForPatients", ResourceType = typeof(PublicS))] 
        public string PublicAddressForPatients { get; set; }

        [StringLength(100)]
        [Display(Name = "PublicPhone", ResourceType = typeof(PublicS))] 
        public string PublicPhone { get; set; }

        [Display(Name = "IsPublic", ResourceType = typeof(PublicS))] 
        public bool IsPublic { get; set; }
        
        
        //public bool IsDeleted { get; set; }

        [Display(Name = "UserInfo", ResourceType = typeof(PublicS))] 
        public virtual UserInfo UserInfo { get; set; }
        public string UserInfoPhoto { get; set; }
    }
}