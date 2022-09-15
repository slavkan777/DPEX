using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoctorPremium.Web.Controllers
{
	[Authorize]
    public class DeliveryController : BaseController
    {
        // GET: Delivery
        public ActionResult Index()
        {
            return View();
        }
    }
}