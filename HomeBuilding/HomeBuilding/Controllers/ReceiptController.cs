using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;
using System.IO;
using System.Text;
using HomeBuilding.Models;
using Microsoft.Reporting.WebForms;

namespace HomeBuilding.Controllers
{
    public class ReceiptController : Controller
    {

        private HomeBuildingEntities db = new HomeBuildingEntities();
        // GET: Receipt
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Search()
        {
            if (Session["UserProfile.Id"] == null) { return RedirectToAction("signin", "Home"); }
            List<TopMenu> menus = new List<TopMenu>();
            menus.Add(new TopMenu() { Seq = 1, Title = "สัญญา", Link = Url.Action("Create", "Contract") });
            menus.Add(new TopMenu() { Seq = 2, Title = "สรุป", Link = Url.Action("Summary", "Receipt") });
            MenuView menu = new MenuView();
            menu.TopMenus = menus;

            return View("Search", menu);
        }

        [HttpGet]
        public ActionResult Print(Guid? id)
        {
            if (Session["UserProfile.Id"] == null) { return RedirectToAction("signin", "Home"); }
            if (id == null) { return RedirectToAction("Search", "Receipt"); }

            ConstructionContract contract = db.ConstructionContracts.Find(id);
            if (contract == null) { return RedirectToAction("Search", "Receipt"); }
            List<Receipt> receipts = db.Receipts.Where(x => x.ConstructionContractId == contract.Id).ToList();
            decimal receiptTotal = 0;
            int receiptSeq = 1;
            foreach (var receipt in receipts) { receiptTotal += receipt.Total; receiptSeq++; }

            ViewBag.ReceiptNumber = db.usp_get_document_number("Receipt", string.Concat("R-", DateTime.Now.Year.ToString(), "-"), 6).FirstOrDefault();
            ViewBag.Round = receiptSeq;
            ViewBag.ReceiptDate = DateTime.Now.ToString("dd/MM/yyyy");

            ViewBag.Receipts = receipts;
            ViewBag.Contract = contract;
            ViewBag.ReceiptTotal = receiptTotal;
            ViewBag.Remain = (decimal)contract.ContractValue - receiptTotal;

            List<TopMenu> menus = new List<TopMenu>();
            menus.Add(new TopMenu() { Seq = 1, Title = "สัญญา", Link = Url.Action("Create", "Contract") });
            menus.Add(new TopMenu() { Seq = 2, Title = "สรุป", Link = Url.Action("Summary", "Receipt") });
            MenuView menu = new MenuView();
            menu.TopMenus = menus;

            return View("Print", menu);
        }

        [HttpGet]
        public FileResult DownloadPDF(Guid? id)
        {
            Receipt receipt = db.Receipts.Find(id);
            ConstructionContract contract = db.ConstructionContracts.Find(receipt.ConstructionContractId);
            User creator = db.Users.Find(receipt.CreatedById);

            decimal total = receipt.Total;
            var ReceiptHistory = db.usp_get_receipt_history(id.ToString()).ToList();
            foreach (var item in ReceiptHistory) { total += Convert.ToDecimal(item.Total); }
            decimal remain = Convert.ToDecimal(contract.ContractValue) - total;

            ReportParameter[] param = new ReportParameter[17];
            param[0] = new ReportParameter("ReceiptNumber", receipt.ReceiptNumber);
            param[1] = new ReportParameter("ContractorName", contract.ContractorName);
            param[2] = new ReportParameter("ContractorAddress", contract.ContractorAddress);
            param[3] = new ReportParameter("ContractorTel", contract.ContractorTel);
            param[4] = new ReportParameter("ReceiptDate", receipt.ReceiptDate?.ToString("dd/MM/yyyy"));
            param[5] = new ReportParameter("OwnerName", contract.OwnerName);
            param[6] = new ReportParameter("OwnerAddress", contract.OwnerAddress);
            param[7] = new ReportParameter("OwnerTel", contract.OwnerTel);
            param[8] = new ReportParameter("DescriptionOfWork", contract.DescriptionOfWork);
            param[9] = new ReportParameter("WorkSite", contract.WorkSite);
            param[10] = new ReportParameter("ContractValue", String.Format("{0:n}", contract.ContractValue));
            param[11] = new ReportParameter("ContractNumber", contract.ContractNumber);
            param[12] = new ReportParameter("Round", receipt.Round.ToString());
            param[13] = new ReportParameter("ReceiptValue", String.Format("{0:n}", receipt.Total));
            param[14] = new ReportParameter("Total", String.Format("{0:n}", total));
            param[15] = new ReportParameter("Remain", String.Format("{0:n}", remain));
            param[16] = new ReportParameter("CreatorName", creator.FullName);

            LocalReport report = new LocalReport();
            report.ReportPath = Server.MapPath("~/Reports/Receipt.rdlc");
            report.DataSources.Clear();
            report.DataSources.Add(new ReportDataSource("ReceiptHistory", ReceiptHistory));
            report.SetParameters(param);
            byte[] bytes = report.Render("PDF");

            return File(bytes, "application/pdf", string.Format("{0}.pdf", receipt.ReceiptNumber));
        }

        [HttpPost]
        public ActionResult Create(FormCollection form)
        {
            if (form["Number"].ToString() == "") { return RedirectToAction("Search", "Receipt"); }

            Receipt receipt = new Receipt()
            {
                Id = Guid.NewGuid(),
                ConstructionContractId = new Guid(form["ContractId"]),
                ReceiptDate = DateTime.Now.Date,
                ReceiptNumber = form["Number"].ToString(),
                Round = Convert.ToInt32(form["Round"]),
                Sequence = Convert.ToInt32(form["Round"]),
                Total = Convert.ToDecimal(form["Amount"]),
                CreatedById = new Guid(form["CreatedById"]),
                CreatedDate = DateTime.Now,
                UpdatedById = new Guid(form["CreatedById"]),
                UpdatedDate = DateTime.Now,
                IsEnabled = true
            };
            db.Receipts.Add(receipt);
            db.SaveChanges();

            return DownloadPDF(receipt.Id);

            //return RedirectToAction("Search", "Receipt");
        }

        public ActionResult Summary()
        {
            if (Session["UserProfile.Id"] == null) { return RedirectToAction("signin", "Home"); }

            List<TopMenu> menus = new List<TopMenu>();
            menus.Add(new TopMenu() { Seq = 1, Title = "สัญญา", Link = Url.Action("Create", "Contract") });
            menus.Add(new TopMenu() { Seq = 2, Title = "ค้นหา", Link = Url.Action("Search", "Receipt") });
            MenuView menu = new MenuView();
            menu.TopMenus = menus;

            ViewBag.OwnerName = TempData["OwnerName"]?.ToString();
            ViewBag.ContractDate = TempData["ContractDate"]?.ToString();

            return View("Summary", menu);
        }

    }
}