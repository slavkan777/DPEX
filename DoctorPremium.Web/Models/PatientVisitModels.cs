using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using DoctorPremium.DAL.Model;
using DoctorPremium.Web.Helpers;
using System.Net;
using DoctorPremium.Services;
using DoctorPremium.Services.Interfaces;
using Google.Apis.Drive.v2.Data;
using Resources;

namespace DoctorPremium.Web.Models
{
    public class PatientVisitEditViewModel
    {
        public PatientVisitEditViewModel()
        {
            DurationItems = DateTimeHelper.GetDurationTimeListItems("60");
        }

        public PatientVisit ToEntity(PatientVisitEditViewModel model, PatientVisit oldPatientVisit, int userid)
        {

            PatientVisit newPatientVisit = oldPatientVisit;
            newPatientVisit.VisitDate = model.VisitDate;
            newPatientVisit.VisitStartTime = model.VisitStartTime;
            newPatientVisit.Duration = model.Duration;
            newPatientVisit.PurposeOfVisit = model.PurposeOfVisit != null ? model.PurposeOfVisit.Trim() : null;
            newPatientVisit.SubjectiveComplaints = model.SubjectiveComplaints != null ? model.SubjectiveComplaints.Trim() : null;
            newPatientVisit.AdditionalResearch = model.AdditionalResearch != null ? model.AdditionalResearch.Trim() : null;
            newPatientVisit.LaboratoryResearch = model.LaboratoryResearch != null ? model.LaboratoryResearch.Trim() : null;
            newPatientVisit.ClinicalDiagnosis = model.ClinicalDiagnosis != null ? model.ClinicalDiagnosis.Trim() : null;
            newPatientVisit.Treatment = model.Treatment != null ? model.Treatment.Trim() : null;
            newPatientVisit.DoctorAssignments = model.DoctorAssignments != null ? model.DoctorAssignments.Trim() : null;
            if ((newPatientVisit.NextVizitRecordId == null || newPatientVisit.NextVizitRecordId == 0) && model.NextVizitRecordId != null)
            {
                newPatientVisit.NextVizitRecordId = model.NextVizitRecordId;
            }

            if (model.CostOfServices != null || model.Paid != null ||
                !String.IsNullOrEmpty(model.Comment) || model.PatientVisitPaymentId != 0)
            {
                PatientVisitPayment payment;
                int count = newPatientVisit.PatientVisitPayments.Count;
                if (count == 0)
                {
                    payment = new PatientVisitPayment();
                    payment.PatientVisitId = model.PatientVisitId;
                    payment.CostOfServices = model.CostOfServices;
                    payment.Paid = model.Paid;
                    payment.Comment = model.Comment;
                    payment.CreateDateUtc = DateTime.UtcNow;
                    newPatientVisit.PatientVisitPayments.Add(payment);
                }
                else
                {
                    payment = count > 0 ? newPatientVisit.PatientVisitPayments.FirstOrDefault(x => x.PatientVisitPaymentId == model.PatientVisitPaymentId) : null;
                    if (payment == null)
                    {
                        payment = new PatientVisitPayment();
                    }
                    payment.CostOfServices = model.CostOfServices;
                    payment.Paid = model.Paid;
                    payment.Comment = model.Comment;
                    payment.UpdateDateUtc = DateTime.UtcNow;
                    newPatientVisit.PatientVisitPayments.ToList()[count - 1] = payment;
                }
            }

            return newPatientVisit;
        }

        public void FromEntity(PatientVisit visit)
        {
            Mapper.CreateMap<PatientVisit, PatientVisitEditViewModel>();
            Mapper.Map(visit, this);
            if (visit.Patient != null)
            {
                this.FullName = FullNameHelper.GetFullName(visit.Patient.LastName, visit.Patient.FirstName, visit.Patient.SurName);
            }
            if (visit.PatientVisitPayments.Count > 0)
            {
                PatientVisitPayment payment = visit.PatientVisitPayments.LastOrDefault();
                this.PatientVisitPaymentId = payment.PatientVisitPaymentId;
                this.CostOfServices = payment.CostOfServices;
                this.Paid = payment.Paid;
                this.Comment = payment.Comment;
            }
        }

        public int PatientVisitId { get; set; }
        [Required]
        public int PatientId { get; set; }
        public int ScheduleRecordID { get; set; }


        //[Display(Name = "Last", ResourceType = typeof(PatientInfo))]
        [Required(ErrorMessageResourceType = typeof(PatientVisitLang), ErrorMessageResourceName = "DataVisitsRequiered")]
        [Display(Name = "DataVisits", ResourceType = typeof(PatientVisitLang))]
        public System.DateTime VisitDate { get; set; }

        [Required(ErrorMessageResourceType = typeof(PatientVisitLang), ErrorMessageResourceName = "DataVisitsRequiered")]
        [Display(Name = "TimeOfVisit", ResourceType = typeof(PatientVisitLang))]
        public System.TimeSpan VisitStartTime { get; set; }

        [Display(Name = "Duration", ResourceType = typeof(PatientVisitLang))]
        public int? Duration { get; set; }

        [StringLength(300)]
        [Display(Name = "Goal", ResourceType = typeof(PatientVisitLang))]
        public string PurposeOfVisit { get; set; }

        [StringLength(500)]
        [Display(Name = "Symptoms", ResourceType = typeof(PatientVisitLang))]
        public string SubjectiveComplaints { get; set; }

        [StringLength(500)]
        [Display(Name = "AdditionalSymptoms", ResourceType = typeof(PatientVisitLang))]
        public string AdditionalResearch { get; set; }

        [StringLength(500)]
        [Display(Name = "DataX", ResourceType = typeof(PatientVisitLang))]
        public string LaboratoryResearch { get; set; }

        [StringLength(500)]
        [Display(Name = "ClinicalDiagnosis", ResourceType = typeof(PatientVisitLang))]
        public string ClinicalDiagnosis { get; set; }

        [StringLength(400)]
        [Display(Name = "Treatment", ResourceType = typeof(PatientVisitLang))]
        public string Treatment { get; set; }

        [StringLength(400)]
        [Display(Name = "DoctorAppointments", ResourceType = typeof(PatientVisitLang))]
        public string DoctorAssignments { get; set; }
        public System.DateTime CreateDateUtc { get; set; }
        public Nullable<System.DateTime> UpdateDateUtc { get; set; }

        [Required]
        public int PatientVisitPaymentId { get; set; }

        [Range(0, 999999999)]
		[Display(Name = "CostOfServices", ResourceType = typeof(PatientVisitLang))]
		[RegularExpression(@"\d+([\,\.]\d{1,2})?", ErrorMessageResourceType = typeof(PatientVisitLang), ErrorMessageResourceName = "InvalidPrice")]
        public decimal? CostOfServices { get; set; }

        [Display(Name = "Paid", ResourceType = typeof(PatientVisitLang))]
        [Range(0, 999999999)]
		[RegularExpression(@"\d+([\,\.]\d{1,2})?", ErrorMessageResourceType = typeof(PatientVisitLang), ErrorMessageResourceName = "InvalidPrice")]
        public decimal? Paid { get; set; }

        [StringLength(200)]
        [Display(Name = "Comment", ResourceType = typeof(PatientVisitLang))]
        public string Comment { get; set; }

        public int? NextVizitRecordId { get; set; }
        [Display(Name = "NextVisitDate", ResourceType = typeof(PatientVisitLang))]
        public System.DateTime? NextVisitDate { get; set; }

        [Display(Name = "NextVisitStartTime", ResourceType = typeof(PatientVisitLang))]
        public System.TimeSpan? NextVisitStartTime { get; set; }

        [StringLength(500)]
        [Display(Name = "CommentNextVisit", ResourceType = typeof(PatientVisitLang))]
        public string CommentNextVisit { get; set; }

        public string FullName { get; set; }

        public List<SelectListItem> DurationItems;
        [Display(Name = "CostOfFeautureServices", ResourceType = typeof(PatientVisitLang))]
        [Range(0, 999999999)]
		[RegularExpression(@"\d+([\,\.]\d{1,2})?", ErrorMessageResourceType = typeof(PatientVisitLang), ErrorMessageResourceName = "InvalidPrice")]
        public decimal? CostOfFeautureServices { get; set; }
    }

    public class PatienVisitListViewModel
    {
        public PatienVisitListViewModel()
        {
        }

        public int PatientVisitId { get; set; }

        public System.DateTime VisitDate { get; set; }

        public System.TimeSpan VisitStartTime { get; set; }

        public string PurposeOfVisit { get; set; }

        public decimal? CostOfServices { get; set; }

        public System.DateTime? NextVisitDate { get; set; }
        public decimal? CostOfFeautureServices { get; set; }
        public decimal? Paid { get; set; }

    }
}