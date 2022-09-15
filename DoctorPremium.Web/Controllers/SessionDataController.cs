using AutoMapper;
using DoctorPremium.DAL.Model;
using DoctorPremium.Services.Interfaces;
using DoctorPremium.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoctorPremium.Web.Controllers
{
    [Authorize(Users = "testdoc@mail.com")]
    public class SessionDataController : Controller
    {
        private ISessionDataService _sessionDataService;

        public SessionDataController(ISessionDataService sessionDataService)
        {
            this._sessionDataService = sessionDataService;
        }

        // GET: SessionData
        public ActionResult List()
        {
            SessionDataCounter counter;
            int daily = 0;
            int weekly = 0;
            int monthly = 0;

            _sessionDataService.GetCounter(ref daily, ref weekly, ref monthly);

            counter = new SessionDataCounter() { Daily = daily, Weekly = weekly, Monthly = monthly };

            return View(counter);
        }

        [HttpPost]
        public ActionResult ListSessionData(DataTableParamModel param)
        {
            try
            {
                string searchString = HttpContext.Request["search[value]"];
                int recordsTotal = 0;
                int recordsFiltered = 0;
                List<SessionData> data = _sessionDataService.GetListToTable(searchString, param.Start, param.Length, out recordsTotal, out recordsFiltered);

                List<SessionDataModels> items = new List<SessionDataModels>();

                if (data.Count > 0)
                {
                    Mapper.CreateMap<SessionData, SessionDataModels>()
                            .ForMember(d => d.UserName, opt => opt.MapFrom(s => s.AspNetUser.UserName))
                            .ForMember(d => d.Duration, opt => opt.MapFrom(s => s.SessionEnd != null ? (s.SessionEnd - s.SessionStart).Value.Minutes : 0));
                    items = Mapper.Map<List<SessionData>, List<SessionDataModels>>(data);
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

        // GET: SessionData/Details/5
        //public ActionResult Details(int id)
        //{
        //    return View();
        //}

        //// GET: SessionData/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        //// POST: SessionData/Create
        //[HttpPost]
        //public ActionResult Create(FormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add insert logic here

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        //// GET: SessionData/Edit/5
        //public ActionResult Edit(int id)
        //{
        //    return View();
        //}

        //// POST: SessionData/Edit/5
        //[HttpPost]
        //public ActionResult Edit(int id, FormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add update logic here

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        //// GET: SessionData/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        // POST: SessionData/Delete/5
        //[HttpPost]
        //public ActionResult Delete(int id, FormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add delete logic here

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
    }
}
