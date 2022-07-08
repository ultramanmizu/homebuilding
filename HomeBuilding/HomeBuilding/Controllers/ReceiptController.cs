using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;
using System.IO;
using System.Text;
using HomeBuilding.Models;

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
            List<Receipt> receipts = db.Receipts.Where(x=>x.ConstructionContractId == contract.Id).ToList();
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

        [HttpPost]
        public ActionResult Create(FormCollection form) {

            if (form["Number"].ToString() == "") { return RedirectToAction("Search", "Receipt"); }

            Receipt receipt = new Receipt() {
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


            return RedirectToAction("Search", "Receipt");
        }

        //[HttpGet]
        //public ActionResult Print(Guid? id)
        //{
        //    if (Session["UserProfile.Id"] == null) { return RedirectToAction("signin", "Home"); }
        //    if (id == null) { return RedirectToAction("Summary", "Receipt"); }

        //    Receipt receipt = db.Receipts.Find(id);
        //    if (receipt == null) { return RedirectToAction("Summary", "Receipt"); }
        //    //List<ReceiptDetail> details = db.ReceiptDetails.Where(x => x.ReceiptId == receipt.Id && x.IsEnabled == true && x.IsDeleted == false).OrderBy(o => o.LineNo).ToList();
        //    ConstructionContract contract = db.ConstructionContracts.Find(receipt.ConstructionContractId);
        //    //List<ContractDescription> contractDescriptions = db.ContractDescriptions.Where(x => x.ConstructionContractId == contract.Id && x.IsEnabled == true && x.IsDeleted == false).OrderBy(o => o.LineNo).ToList();

        //    List<Receipt> receiptOthers = db.Receipts.Where(x=>x.ConstructionContractId == contract.Id && x.Id != receipt.Id && x.IsEnabled == true && x.IsDeleted == false).OrderBy(o => o.Round).ToList();
        //    decimal withdrawTotal = receipt.Total;
        //    foreach (var receiptOther in receiptOthers) { withdrawTotal = +receiptOther.Total; }

        //    User creator = db.Users.Find(receipt.CreatedById);

        //    ViewBag.Receipt = receipt;
        //    //ViewBag.ReceiptDetails = details;
        //    ViewBag.Contract = contract;
        //    //ViewBag.ContractDescriptions = contractDescriptions;
        //    ViewBag.ReceiptOthers = receiptOthers;
        //    ViewBag.WithdrawTotal = withdrawTotal;
        //    ViewBag.Remain = (decimal)contract.ContractValue - withdrawTotal;
        //    ViewBag.Creator = creator.FullName;

        //    List<TopMenu> menus = new List<TopMenu>();
        //    menus.Add(new TopMenu() { Seq = 1, Title = "สัญญา", Link = Url.Action("Create", "Contract") });
        //    menus.Add(new TopMenu() { Seq = 2, Title = "สรุป", Link = Url.Action("Summary", "Receipt") });
        //    MenuView menu = new MenuView();
        //    menu.TopMenus = menus;

        //    return View("Print", menu);
        //}

        [HttpGet]
        public FileResult ExportExcel(Guid? id)
        {
            string text = "<p>(นาย วารินทร์ เกษร)</p><p>ที่อยู่: 123/456</p><p>เบอร์โทร: 02-5146459</p><p>ใบเสร็จรับเงิน</p>";
            return File(System.Text.Encoding.GetEncoding("TIS-620").GetBytes(text), "application/vnd.ms-excel", "Grid.xls");
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