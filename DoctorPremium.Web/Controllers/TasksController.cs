using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoctorPremium.Web.Controllers
{
    public class TasksController : BaseController
    {
        // GET: Tasks
        public ActionResult Index()
        {
            return View();
        }
    }
}