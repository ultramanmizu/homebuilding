using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HomeBuilding.Controllers
{
    public class ReceiptController : Controller
    {
        // GET: Receipt
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Search()
        {
            if (Session["UserProfile.Id"] == null) { return RedirectToAction("signin", "Home"); }
            return View("Search");
        }

        public ActionResult Print()
        {
            if (Session["UserProfile.Id"] == null) { return RedirectToAction("signin", "Home"); }
            return View("Print");
        }

        public ActionResult Summary()
        {
            if (Session["UserProfile.Id"] == null) { return RedirectToAction("signin", "Home"); }
            return View("Summary");
        }

    }
}