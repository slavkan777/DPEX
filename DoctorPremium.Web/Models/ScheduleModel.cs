using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using DoctorPremium.DAL.Model;
using DoctorPremium.Web.Helpers;
using System.Net;
using System.Web.Services.Description;
using Microsoft.Ajax.Utilities;
using Resources;

namespace DoctorPremium.Web.Models
{
	public class ScheduleListModel
	{
		public ScheduleListModel()
		{
		}

		public int ScheduleRecordId { get; set; }
		public byte ScheduleRecordTypeId { get; set; }
		public System.DateTime RecordDate { get; set; }
		public TimeSpan? RecordStartTime { get; set; }
		public byte Duration { get; set; }
		public string Description { get; set; }
		public string Title { get; set; }
        [Required]
        public bool? isFinished { get; set; }
        public decimal? CostOfFeautureServices { get; set; }
	}

	public class ScheduleEditModel
	{
		public ScheduleEditModel()
		{
			DurationItems = DateTimeHelper.GetDurationTimeListItems("60");
            PatientsItems = new List<SelectListItem>();
		}

		public ScheduleRecord ToEntity(ScheduleEditModel model, ScheduleRecord oldRecord)
		{
			Mapper.CreateMap<ScheduleEditModel, ScheduleRecord>()
				.ForMember(dto => dto.CreateDateUtc, opt => opt.Ignore());
			return Mapper.Map(this, oldRecord);
		}

		public void FromEntity(ScheduleRecord record)
		{
			Mapper.CreateMap<ScheduleRecord, ScheduleEditModel>();
			Mapper.Map(record, this);
		}

		public int ScheduleRecordId { get; set; }

		public byte ScheduleRecordTypeId { get; set; }

		[DataType(DataType.Date)]
       [Required(ErrorMessageResourceType = typeof(Schedule), ErrorMessageResourceName = "RecordDateRequired")]
       [Display(Name = "RecordDate", ResourceType = typeof(Schedule))]
     	public System.DateTime RecordDate { get; set; }

       [Required(ErrorMessageResourceType = typeof(Schedule), ErrorMessageResourceName = "RecordStartTimeRequired")]
       [Display(Name = "RecordStartTime", ResourceType = typeof(Schedule))]
      	public TimeSpan? RecordStartTime { get; set; }

        //[Required(ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "TitleRequired")]
        [Display(Name = "Description", ResourceType = typeof(Schedule))]
        [StringLength(200)] 
		public string Description { get; set; }

		[StringLength(80)]
        [Required(ErrorMessageResourceType = typeof(Schedule), ErrorMessageResourceName = "TitleRequired")]
        [Display(Name = "Title", ResourceType = typeof(Schedule))]
      	public string Title { get; set; }

		[Range(0, Byte.MaxValue)]
        [Display(Name = "Duration", ResourceType = typeof(Schedule))]
		public byte Duration { get; set; }

		public List<SelectListItem> DurationItems;

        [Display(Name = "CostOfFeautureServices", ResourceType = typeof(Schedule))]
        [Range(0, 999999999)]
		[RegularExpression(@"\d+([\,\.]\d{1,2})?", ErrorMessageResourceType = typeof(PatientVisitLang), ErrorMessageResourceName = "InvalidPrice")]
        public decimal? CostOfFeautureServices { get; set; }

        public List<SelectListItem> PatientsItems;
        public Nullable<int> PatientId { get; set; }
        public Nullable<int> PatientVisitId { get; set; }
     
	}
}