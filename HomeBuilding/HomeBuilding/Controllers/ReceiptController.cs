using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;
using System.IO;
using System.Text;

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
            return View("Search");
        }

        [HttpGet]
        public ActionResult Print(Guid? id)
        {
            if (Session["UserProfile.Id"] == null) { return RedirectToAction("signin", "Home"); }
            if (id == null) { return RedirectToAction("Summary", "Receipt"); }

            Receipt receipt = db.Receipts.Find(id);
            if (receipt == null) { return RedirectToAction("Summary", "Receipt"); }
            //List<ReceiptDetail> details = db.ReceiptDetails.Where(x => x.ReceiptId == receipt.Id && x.IsEnabled == true && x.IsDeleted == false).OrderBy(o => o.LineNo).ToList();
            ConstructionContract contract = db.ConstructionContracts.Find(receipt.ConstructionContractId);
            //List<ContractDescription> contractDescriptions = db.ContractDescriptions.Where(x => x.ConstructionContractId == contract.Id && x.IsEnabled == true && x.IsDeleted == false).OrderBy(o => o.LineNo).ToList();

            List<Receipt> receiptOthers = db.Receipts.Where(x=>x.ConstructionContractId == contract.Id && x.Id != receipt.Id && x.IsEnabled == true && x.IsDeleted == false).OrderBy(o => o.Round).ToList();
            decimal withdrawTotal = receipt.Total;
            foreach (var receiptOther in receiptOthers) { withdrawTotal = +receiptOther.Total; }

            User creator = db.Users.Find(receipt.CreatedById);

            ViewBag.Receipt = receipt;
            //ViewBag.ReceiptDetails = details;
            ViewBag.Contract = contract;
            //ViewBag.ContractDescriptions = contractDescriptions;
            ViewBag.ReceiptOthers = receiptOthers;
            ViewBag.WithdrawTotal = withdrawTotal;
            ViewBag.Remain = (decimal)contract.ContractValue - withdrawTotal;
            ViewBag.Creator = creator.FullName;

            return View("Print");
        }

        [HttpGet]
        public FileResult ExportExcel(Guid? id)
        {
            string text = "<p>(นาย วารินทร์ เกษร)</p><p>ที่อยู่: 123/456</p><p>เบอร์โทร: 02-5146459</p><p>ใบเสร็จรับเงิน</p>";
            return File(System.Text.Encoding.GetEncoding("TIS-620").GetBytes(text), "application/vnd.ms-excel", "Grid.xls");
        }

        public ActionResult Summary()
        {
            if (Session["UserProfile.Id"] == null) { return RedirectToAction("signin", "Home"); }

            ViewBag.OwnerName = TempData["OwnerName"]?.ToString();
            ViewBag.ContractDate = TempData["ContractDate"]?.ToString();
            return View("Summary");
        }

    }
}