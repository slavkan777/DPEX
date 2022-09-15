using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoctorPremium.Web.Controllers
{
    public class ErrorController : BaseController
    {
        // GET: Error
        public ActionResult page404()
        {   
            return View();
        }

        public ActionResult page500()
        {
            return View();
        }
    }
}