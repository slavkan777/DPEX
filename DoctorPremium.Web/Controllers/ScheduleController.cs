using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using DoctorPremium.Services.Interfaces;
using DoctorPremium.DAL.Model;
using DoctorPremium.Web.Helpers;
using DoctorPremium.Web.Models;
using log4net;
using Microsoft.AspNet.Identity;
using Resources;

namespace DoctorPremium.Web.Controllers
{
	[Authorize(Roles = "Doctor")]
	public class ScheduleController : BaseController
    {

        private IScheduleService _scheduleService;
        private IPatientService _patientService;
       

        public ScheduleController(IScheduleService scheduleService, IPatientService patientService)
        {
            this._scheduleService = scheduleService;
            this._patientService = patientService;
        }

        public ActionResult Index()
        {
            ScheduleListModel model = new ScheduleListModel();
            return View(model);
        }

        [HttpGet]
        public JsonResult GetEvents(double start, double end)
        {
            var fromDate = ConvertFromUnixTimestamp(start).Date;
            var toDate = ConvertFromUnixTimestamp(end).Date;

            List<ScheduleRecord> records =
                _scheduleService.GetListForCalendar(Int32.Parse(User.Identity.GetUserId() ?? "0"), fromDate, toDate);
            List<ScheduleListModel> events = new List<ScheduleListModel>();
            if (records.Count > 0)
            {
                Mapper.CreateMap<ScheduleRecord, ScheduleListModel>();
                events = Mapper.Map<List<ScheduleRecord>, List<ScheduleListModel>>(records);
            }
            var eventList = from e in events
                            select new
                            {
                                id = e.ScheduleRecordId,
                                className = e.isFinished == true ? "FinishedgreyEvent" : e.ScheduleRecordTypeId == 1 ? "greenEvent" : "greyEvent",
                                TypeIvent = (e.ScheduleRecordTypeId == 1) ? "  " : "   ",
                                Money = (e.ScheduleRecordTypeId == 1) ? "  " + e.CostOfFeautureServices.ToString() + "  " : "",
                                Topic = (e.ScheduleRecordTypeId == 1) ? "      " : "     ",
                                labelMoney = (e.ScheduleRecordTypeId == 1) ? e.CostOfFeautureServices.ToString() : "",
                                title = e.Title,
                                description = e.Description,
                                start =
                                    (e.RecordDate + (e.RecordStartTime ?? new TimeSpan())).ToString("s",
                                        System.Globalization.CultureInfo.InvariantCulture),
                                //ConvertToUnixTimestamp(e.RecordDate + (e.RecordStartTime ?? new TimeSpan())).ToString(),
                                end =
                                    (e.RecordDate +
                                     (e.RecordStartTime != null
                                         ? e.RecordStartTime.Value.Add(new TimeSpan(0, e.Duration, 0))
                                         : new TimeSpan())).ToString("s", System.Globalization.CultureInfo.InvariantCulture),
                                //ConvertToUnixTimestamp(e.RecordDate + (e.RecordEndTime ?? new TimeSpan())).ToString(), 
                                allDay = false,
								finished = e.isFinished == true ? 1 : 0
                            };

            var rows = eventList.ToArray();
            return Json(rows, JsonRequestBehavior.AllowGet);

        }

        private static DateTime ConvertFromUnixTimestamp(double timestamp)
        {
            var origin = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return origin.AddSeconds(timestamp);
        }

        private static double ConvertToUnixTimestamp(DateTime date)
        {
            var origin = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return (date - origin).TotalSeconds;
        }

        public ActionResult EditEntry(int id, int type, TimeSpan? time, DateTime? day)
        {
            int userId = Int32.Parse(User.Identity.GetUserId());

            ScheduleEditModel model = new ScheduleEditModel();
            if (id == 0)
            {
                if (type == 0)
                {
                    return null;
                }
                model.ScheduleRecordTypeId = (byte)type;
				model.RecordDate = DateTime.Now;			//TODO: Need local client value
                model.Duration = 60;
                model.PatientsItems = _patientService.GetList(userId).Select(m => new SelectListItem
                {
                    Text = m.FirstName + " " + m.LastName + " " + m.SurName + " " + m.BirthDate.ToString("yyyy/dd/MM"),
                    Value = m.PatientId.ToString()

                }).ToList();
                if (time != null)
                {
                    model.RecordStartTime = time;
                    if (day != null)
                    {
                        model.RecordDate = (DateTime)day;
                    }

                }
            }
            else
            {
                ScheduleRecord record = _scheduleService.GetScheduleRecord(Int32.Parse(User.Identity.GetUserId()), id);
                if (record == null)
                {
                    return null;
                }
                else
                {
                    model.FromEntity(record);
                }
            }
             
            return PartialView(model);
            
        }

        [HttpPost]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EditEntry(ScheduleEditModel model) //добавление инфы в расспсиании
        {
            if (Request.IsAjaxRequest())
            {
                bool success = false;
                if (ModelState.IsValid)
                {
                    ScheduleRecord record;
                    if (model.ScheduleRecordId == 0)
                    {
                        record = model.ToEntity(model, new ScheduleRecord());
                        record.UserId = Int32.Parse(User.Identity.GetUserId());
                        _scheduleService.Save(record);
                        success = true;
                    }
                    else
                    {
                        record = _scheduleService.GetScheduleRecord(Int32.Parse(User.Identity.GetUserId()), model.ScheduleRecordId);

                        if (record == null)
                        {
                            success = false;
                        }
                        else
                        {

                            ScheduleRecord editRecord = model.ToEntity(model, record);
                            _scheduleService.Save(editRecord);
                            success = true;
                        }
                    }
                }
                else
                {
                    return PartialView(model);
                }
                var v = new { success = success, error = "" };
                return Json(v);
            }
             
            return null;
        }

	     

	    //public ActionResult SaveMoveCalendar(int id, int type, TimeSpan? time, DateTime? day) //добавление инфы в расспсиании
        [HttpPost]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult SaveMoveCalendar(int id, DateTime? timeStartOne) //добавление инфы в расспсиании
        {
            DateTime timeStart = timeStartOne.GetValueOrDefault();
            if (timeStartOne != null & id > 0)
            {

                TimeSpan dateTime = timeStart.TimeOfDay;
                ScheduleRecord record;
                bool success = false;
                if (Request.IsAjaxRequest())
                {
                    record = _scheduleService.GetScheduleRecord(Int32.Parse(User.Identity.GetUserId()), id);
                    if (record == null)
                    {
                        success = false;
                    }
                    else
                    {

                        record.RecordStartTime = dateTime;
                        record.RecordDate = timeStart.Date;
                        _scheduleService.Save(record);
                        success = true;
                    }


                    var v = new { success = success, error = "" };
                    return Json(v);
                }
            }
            return null;
        }

        public ActionResult ModalFormEntry()
        {
            return PartialView();
        }

        [HttpPost]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult DeleteDataCalendar(int id) //добавление инфы в расспсиании
        {
            
            if (id > 0)
            {
                ScheduleRecord record = _scheduleService.GetScheduleRecord(Int32.Parse(User.Identity.GetUserId()), id);
                bool success = false;
                if (record != null & id > 0)
                {

                    
                        if (record == null)
                        {
                            success = false;
                        }
                        else
                        {
                            _scheduleService.Delete(id);
                            success = true;
                        }



                        var v = new { success = success, error = "" };
                        return Json(v);
                    }
               
            }
            return null;
        }


        [HttpPost]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult FinishDataCalendar(int id) //добавление инфы в расспсиании
        {

            if (id > 0)
            {
                ScheduleRecord record = _scheduleService.GetScheduleRecord(Int32.Parse(User.Identity.GetUserId()), id);
                bool success = false;
                if (record != null & id > 0)
                {


                    if (record == null)
                    {
                        success = false;
                    }
                    else
                    {
                        _scheduleService.Finished(record);
                        success = true;
                    }



                    var v = new { success = success, error = "" };
                    return Json(v);
                }

            }
            return null;
        }

        [HttpPost]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult GetDataPatient(int id) //добавление инфы в расспсиании
        {  
            PatientEditViewModel model = new PatientEditViewModel();
            bool success = false;
            if (id == null || id == 0)
            {
                model.CreateNewDentalCards();
                //model = new PatientEditViewModel();

            }
            else
            {
                Patient patientInfo = _patientService.GetPatientInfo((int)id, Int32.Parse(User.Identity.GetUserId()));
                if (patientInfo == null)
                {
                    throw new HttpException(404, "Patient not found"); // TODO: need not found page
                }
                else
                {
                    model.FromEntity(patientInfo);
                    //success = true;
                }
                //var v = new { success = success, error = "" };
                return Json( new{
                    //v,
                    name = model.FirstName + " " + model.LastName + " " + model.SurName + " ",
                    phone1 = " " + model.HomePhone + " " + model.MobilePhone,
                },
                    JsonRequestBehavior.AllowGet);
            }
            return null;
        }

        public ActionResult VidioHelp()
        {
            switch (LanguageHelper.GetCurrentLanguage())
            {
                case "ru":
                      ViewBag.Lang = "1";
                      return View();

                default:
                    ViewBag.Lang = "2";
                    return View();
            }
            
        }

        public ActionResult BuyPersonalVariant()
        {
            switch (LanguageHelper.GetCurrentLanguage())
            {
                case "ru":
                    ViewBag.Lang = "1";
                    return View();

                default:
                    ViewBag.Lang = "2";
                    return View();
            }

        }


    }
}