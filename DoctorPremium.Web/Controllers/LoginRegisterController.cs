using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoctorPremium.Web.Controllers
{
    public class LoginRegisterController : BaseController
    {
        // GET: LoginRegister
        public ActionResult Login()
        {
            return View();
        }
    }
}