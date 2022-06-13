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
            return View("Search");
        }

        public ActionResult Print()
        {
            return View("Print");
        }

        public ActionResult Summary()
        {
            return View("Summary");
        }

    }
}