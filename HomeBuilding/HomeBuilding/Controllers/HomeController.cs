using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HomeBuilding.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult SignIn() {
            ViewBag.Message = TempData["shortMessage"]?.ToString();
            return View("SignIn");
        }

        public ActionResult ResetPassword() {
            return View("ResetPassword");
        }

        public ActionResult ChangePassword() { 
            return View("ChangePassword");
        }

        public ActionResult ForgotPassword()
        {
            return View("ForgotPassword");
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}