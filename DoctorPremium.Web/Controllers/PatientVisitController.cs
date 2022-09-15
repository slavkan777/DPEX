using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using AutoMapper;
using DoctorPremium.Services.Interfaces;
using DoctorPremium.DAL.Model;
using DoctorPremium.Web.Helpers;
using DoctorPremium.Web.Models;
using log4net;
using Microsoft.AspNet.Identity;

namespace DoctorPremium.Web.Controllers
{
	[Authorize(Roles = "Doctor")]
	public class PatientVisitController : BaseController
    {
   
		private IPatientService _patientService;
		private IPatientVisitService _visitService;
        private IScheduleService _scheduleService;

        public PatientVisitController(IPatientService patientService, IPatientVisitService visitService, IScheduleService scheduleService)
		{
			this._patientService = patientService;
			this._visitService = visitService;
            this._scheduleService = scheduleService;
		}

        public ActionResult Edit(int? id, int? patientId, int? ScheduleRecordID)
        { 
			PatientVisitEditViewModel model = new PatientVisitEditViewModel();
			if (id == null || id == 0)
			{
				if (patientId == null || patientId == 0)
				{
					throw new HttpException(404, "Patient id not found"); // TODO: need not found page
				}
				else
				{
				        Patient patientInfo = _patientService.GetPatientInfo(patientId ?? 0, Int32.Parse(User.Identity.GetUserId()));
				        if (patientInfo == null)
				        {
							throw new HttpException(404, "Patient not found"); // TODO: need not found page
				        }
				        else
				        { 
                            Patient patientInfVal = _patientService.GetPatientInfo((int)patientId, Int32.Parse(User.Identity.GetUserId()));
                            ViewBag.Debt = patientInfVal.Debt;
                            model.FullName = FullNameHelper.GetFullName(patientInfo.LastName, patientInfo.FirstName, patientInfo.SurName);
						    model.PatientId = (int)patientId;
				            model.VisitDate = DateTime.Now; //TODO: Need local client value
                            model.CommentNextVisit += " " + model.FullName + " " + patientInfo.MobilePhone + " " + patientInfo.HomePhone + ". ";
				        }
				}
			}
			else
			{
                PatientVisit visitInfo = _visitService.GetPatientVisitInfo(id ?? 0, Int32.Parse(User.Identity.GetUserId()));
                Patient patientInfo = _patientService.GetPatientInfo(visitInfo.PatientId, Int32.Parse(User.Identity.GetUserId()));
                ViewBag.Debt = patientInfo.Debt;
                if (visitInfo == null || visitInfo.Patient == null || visitInfo.Patient.IsDeleted)
				{
					throw new HttpException(404, "visitInfo not found"); // TODO: need not found page
				}
				else
				{
					model.FromEntity(visitInfo);
				}

			}
			if (model.NextVizitRecordId != null && model.NextVizitRecordId > 0)
			{
				ScheduleRecord record = _scheduleService.GetScheduleRecordById(model.NextVizitRecordId ?? 0);
				if (record != null)
				{
					model.NextVisitDate = record.RecordDate;
					model.CommentNextVisit = record.Description;
					model.NextVisitStartTime = record.RecordStartTime;
					model.CostOfFeautureServices = record.CostOfFeautureServices;
				}
			}

            model.ScheduleRecordID = ScheduleRecordID ?? 0;
			return View(model);
        }

		[HttpPost]
		public ActionResult Edit(PatientVisitEditViewModel model)
		{
			if (ModelState.IsValid)
			{
				if (model.NextVizitRecordId != null && model.NextVizitRecordId > 0)
				{
					ScheduleRecord record = _scheduleService.GetScheduleRecordById(model.NextVizitRecordId ?? 0);
					if (record != null)
					{
						record.RecordDate = model.NextVisitDate ?? DateTime.UtcNow.AddDays(180);
						record.Description = model.CommentNextVisit + " ";
						record.RecordStartTime = model.NextVisitStartTime;
						record.CostOfFeautureServices = model.CostOfFeautureServices;
					    record.PatientId = model.PatientId;
                        //string fio = "";
                        //Patient patientInfo = _patientService.GetPatientInfo(model.PatientId, Int32.Parse(User.Identity.GetUserId()));
                        //if (patientInfo != null)
                        //{
                        //    fio = String.Format("{0} {1}", FullNameHelper.GetFullName(patientInfo.LastName, patientInfo.FirstName, patientInfo.SurName), patientInfo.MobilePhone ?? "");
                        //}
                        //record = new ScheduleRecord() { ScheduleRecordTypeId = 1, Title = !String.IsNullOrEmpty(fio) ? fio : "Визит пациента", Description = !String.IsNullOrEmpty(model.CommentNextVisit) ? model.CommentNextVisit : fio, Duration = 60, RecordStartTime = model.NextVisitStartTime ?? new TimeSpan(0, 9, 0, 0), RecordDate = model.NextVisitDate ?? DateTime.UtcNow.AddDays(180), UserId = Int32.Parse(User.Identity.GetUserId()), CostOfFeautureServices = model.CostOfFeautureServices, CreateDate = DateTime.UtcNow };
                    //    record = new ScheduleRecord() { ScheduleRecordTypeId = 1, Title = !String.IsNullOrEmpty(record.Title) ? record.Title : "Визит пациента", Description = !String.IsNullOrEmpty(model.CommentNextVisit) ? model.CommentNextVisit :  fio, Duration = 60, RecordStartTime = model.NextVisitStartTime ?? new TimeSpan(0, 9, 0, 0), RecordDate = model.NextVisitDate ?? DateTime.UtcNow.AddDays(180), UserId = Int32.Parse(User.Identity.GetUserId()), CostOfFeautureServices = model.CostOfFeautureServices, CreateDate = DateTime.UtcNow };
                        _scheduleService.Save(record);
                        //model.NextVizitRecordId = record.ScheduleRecordId;
					}
				}
				else
				{
					string fio = "";
					Patient patientInfo = _patientService.GetPatientInfo(model.PatientId, Int32.Parse(User.Identity.GetUserId()));
					if (patientInfo != null)
					{
						fio = String.Format("{0} {1}", FullNameHelper.GetFullName(patientInfo.LastName, patientInfo.FirstName, patientInfo.SurName), patientInfo.MobilePhone ?? "");
					}
					ScheduleRecord record = new ScheduleRecord() { ScheduleRecordTypeId = 1, Title = !String.IsNullOrEmpty(fio) ? fio : "Визит пациента", Description = !String.IsNullOrEmpty(model.CommentNextVisit) ? model.CommentNextVisit : fio, Duration = 60, RecordStartTime = model.NextVisitStartTime ?? new TimeSpan(0, 9, 0, 0), RecordDate = model.NextVisitDate ?? DateTime.UtcNow.AddDays(180), UserId = Int32.Parse(User.Identity.GetUserId()), CostOfFeautureServices = model.CostOfFeautureServices, CreateDateUtc = DateTime.UtcNow };
                    record.PatientId = model.PatientId;
                    record = _scheduleService.Save(record);
					model.NextVizitRecordId = record.ScheduleRecordId;
				}

				PatientVisit visit;
				decimal oldCost = 0;
				decimal oldPaid = 0;

				if (model.PatientVisitId == 0)
				{
					visit = model.ToEntity(model, new PatientVisit() { PatientId = model.PatientId }, Int32.Parse(User.Identity.GetUserId()));
					visit.CreateDateUtc = DateTime.UtcNow;
                    PatientVisit newPatientVisit = _visitService.Save(visit); // TODO: Need result
                    model.PatientVisitId = newPatientVisit.PatientVisitId;
                    if (model.ScheduleRecordID > 0)
				    {
                        ScheduleRecord record = _scheduleService.GetScheduleRecord(Int32.Parse(User.Identity.GetUserId()), model.ScheduleRecordID);
                        if (record != null)
                        {
                            record.PatientId = newPatientVisit.PatientId;
                            record.PatientVisitId = newPatientVisit.PatientVisitId;
                            _scheduleService.Save(record);
                        }
				    }
				}
				else
				{
					visit = _visitService.GetPatientVisitInfo(model.PatientVisitId, Int32.Parse(User.Identity.GetUserId()));
					if (visit == null)
					{
						throw new HttpException(404, "visitInfo not found"); // TODO: need not found page
					}
					else
					{
						if (visit.PatientVisitPayments != null && visit.PatientVisitPayments.FirstOrDefault() != null)
						{
							PatientVisitPayment payment = visit.PatientVisitPayments.FirstOrDefault() ?? new PatientVisitPayment();
							oldCost = payment.CostOfServices ?? 0;
							oldPaid = payment.Paid ?? 0;
						}
						
						PatientVisit visitinfo = model.ToEntity(model, visit, Int32.Parse(User.Identity.GetUserId()));
                        _visitService.Save(visitinfo); // TODO: Need result
					}
				}	
				Patient NewPatientInfo = _patientService.GetPatientInfo(model.PatientId, Int32.Parse(User.Identity.GetUserId()));
				if (NewPatientInfo != null)
				{
					NewPatientInfo.Debt += CalculateBalance(model.Paid ?? 0, model.CostOfServices ?? 0, oldPaid, oldCost);
					_patientService.Save(NewPatientInfo);
				}
            
				return RedirectToAction("Review", "Patient", new { id = model.PatientId });
			}
			return View(model);
		}

		[HttpPost]
		public ActionResult ListPatientVisits(DataTableParamModel param, int? patientid)
		{
			if (patientid == null || patientid == 0)
			{
				return Json(new { Result = "ERROR", Message = "Patient not found!" });
			}
			Patient patientInfo = _patientService.GetPatientInfo(patientid ?? 0, Int32.Parse(User.Identity.GetUserId()));
			if (patientInfo == null)
			{
				return Json(new { Result = "ERROR", Message = "Patient not found!" });
			}
			try
			{
				string searchString = HttpContext.Request["search[value]"];
				int recordsTotal = 0;
				int recordsFiltered = 0;

				List<PatientVisit> visits = _visitService.GetListToTable((int)patientid, param.Start,
					param.Length, searchString, param.Order.ToList()[0].Column, param.Order.ToList()[0].Dir, out recordsTotal, out recordsFiltered);

				List<PatienVisitListViewModel> items = new List<PatienVisitListViewModel>();

				if (visits.Count > 0)
				{
					Mapper.CreateMap<PatientVisit, PatienVisitListViewModel>()
                                 .ForMember(d => d.CostOfServices, opt => opt.MapFrom(s => s.PatientVisitPayments.Count > 0 ? s.PatientVisitPayments.ToList()[0].CostOfServices : null))
                                .ForMember(d => d.Paid, opt => opt.MapFrom(s => s.PatientVisitPayments.Count > 0 ? s.PatientVisitPayments.ToList()[0].Paid : null));
					items = Mapper.Map<List<PatientVisit>, List<PatienVisitListViewModel>>(visits);
				}

				return Json(new
				{
					draw = param.Draw,
					recordsTotal = recordsTotal,
					recordsFiltered = recordsFiltered,
					data = items
				},
					JsonRequestBehavior.AllowGet);
			}
			catch (Exception ex)
			{
				return Json(new { Result = "ERROR", Message = ex.Message });
				throw ex;
			}
		}

        [HttpPost]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult DeleteVisitPatient(int id)
        {
            bool success = false;
            if (id != 0)
            {
                PatientVisit patientInfo = _visitService.GetPatientVisitInfo((int)id, Int32.Parse(User.Identity.GetUserId()));
                if (patientInfo == null)
                {
                    throw new HttpException(404, "Patient not found"); // TODO: need not found patient page
                }
                else
                {
                    _visitService.Delete(patientInfo);
                    success = true;
                }
            }
            else
            {
                throw new HttpException(404, "Page not found");

            }
            var v = new { success = success, error = "" };
            return Json(v);
        }

		private decimal CalculateBalance(decimal paid, decimal cost, decimal oldPaid, decimal oldCost)
		{
			return (paid - oldPaid) - (cost - oldCost);
		}
    }
}