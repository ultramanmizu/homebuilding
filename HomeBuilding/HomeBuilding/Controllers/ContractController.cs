using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;
using System.Data.Linq.SqlClient;
using System.Globalization;
using HomeBuilding.Models;

using ZetPDF.Pdf;
using ZetPDF.Drawing;
using System.IO;
using Microsoft.Reporting.WebForms;

namespace HomeBuilding.Controllers
{
    public class ContractController : Controller
    {
        private HomeBuildingEntities db = new HomeBuildingEntities();
        // GET: Contract
        public ActionResult Index()
        {
            if (Session["UserProfile.Id"] == null) { return RedirectToAction("signin", "Home"); }
            return View();
        }

        public ActionResult Create()
        {
            if (Session["UserProfile.Id"] == null) { return RedirectToAction("signin", "Home"); }
            string prefix = string.Concat("C-", DateTime.Now.Year.ToString(), "-");
            var docNumber = db.usp_get_document_number("Contract", prefix, 6).FirstOrDefault();
            ViewBag.ContractNumber = docNumber.ToString();
            //ViewBag.MadeAt = "บริษัท เนเจอร์ เอ็ซเทท จำกัด";
            ViewBag.CreateDate = DateTime.Now.ToString("dd/MM/yyyy");

            List<TopMenu> menus = new List<TopMenu>();
            menus.Add(new TopMenu() { Seq = 1, Title = "สรุป", Link = Url.Action("Summary", "Receipt") });
            menus.Add(new TopMenu() { Seq = 2, Title = "ค้นหา", Link = Url.Action("Search", "Receipt") });
            MenuView menu = new MenuView();
            menu.TopMenus = menus;

            return View("Create", menu);
        }

        public HtmlString GetDescriptionForm(int i, int line_no)
        {
            string pathImageDel = Url.Content("~/Content/images/icons/delete_remove_icon.svg");
            //get Material/Labor
            List<Material> materials = db.Materials.Where(x => x.IsEnabled == true && x.IsDeleted == false).OrderBy(o => o.Sequence).ToList();

            string html = "<tr id=\"tr-description-" + i + "\"><td class=\"align-middle\"><div id=\"description-line-" + i + "\">" + line_no + "</div></td><td><div id=\"div-select-material-" + i + "\"><select class=\"form-select\" id=\"select-material-" + i + "\" name=\"material-" + i + "\" onchange=\"javascript:MaterialChange('" + i + "')\"><option value=\"\" selected>กรุณาเลือกข้อมูล</option>";
            foreach (Material material in materials)
            {
                html += "<option value=\"" + material.Id + "\">" + material.Name + "</option>";
            }
            html += "<option value=\"other\">อื่นๆ ระบุ</option></select></div><div id=\"div-txt-material-" + i + "\" style=\"display: none;\"><input type=\"text\" class=\"form-control\" id=\"txt-material-" + i + "\" name=\"material-other-" + i + "\" value=\"\"></div></td>" +
                "<td><input type=\"number\" min=\"0\" class=\"form-control\" id=\"txt-quantity-" + i + "\" name=\"quantity-" + i + "\" value=\"0\" onblur=\"javascript:SumDescription('" + i + "')\"></td>" +
                "<td><div id=\"div-select-unit-" + i + "\"><select class=\"form-select text-start\" id=\"select-unit-" + i + "\" name=\"unit-" + i + "\" onchange=\"javascript:UnitChange('" + i + "')\"><option value=\"\" selected>กรุณาเลือก</option></select></div><div id=\"div-txt-unit-" + i + "\" style=\"display: none;\"><input type=\"text\" class=\"form-control\" id=\"txt-unit-" + i + "\" name=\"unit-other-" + i + "\" value=\"\"></div></td>" +
                "<td><div id=\"div-select-unitprice-" + i + "\"><select class=\"form-select text-start\"id=\"select-unitprice-" + i + "\" name=\"unitprice-" + i + "\" onchange=\"javascript:SumDescription('" + i + "')\"><option value=\"0\" selected>กรุณาเลือก</option></select></div><div id=\"div-txt-unitprice-" + i + "\" style=\"display: none;\"><input type=\"number\" min=\"0\" class=\"form-control\" id=\"txt-unitprice-" + i + "\" name=\"unitprice-other-" + i + "\" value=\"0\" onblur=\"javascript:SumDescription('" + i + "')\"></div></td>" +
                "<td class=\"align-middle text-red fs-16 text-center\"><p class=\"p-0 m-0\" id=\"lable-description-total-" + i + "\">0.00</p><input type=\"hidden\" id=\"description-total-" + i + "\" name=\"description-total-" + i + "\" value=\"0\"></td>" +
                "<td class=\"align-middle w-0 text-center bg-gray\">" +
                    "<a href=\"javascript:RemoveDescription('" + i + "')\"><img src=\"" + pathImageDel + "\" width=\"16\" title=\"ลบ\"/></a> " +
                "</td></tr>";
            return new HtmlString(html);
        }

        public HtmlString GetUnitForm(int i, string materialId)
        {
            string html = "<option value=\"\" selected>กรุณาเลือก</option>";
            if (materialId != "")
            {
                //get Unit
                List<MaterialUnit> units = db.MaterialUnits.Where(x => x.MaterialId == new Guid(materialId) && x.IsEnabled == true && x.IsDeleted == false).OrderBy(o => o.CreatedDate).ToList();
                foreach (MaterialUnit unit in units)
                {
                    html += "<option value=\"" + unit.Id + "\">" + unit.Name + "</option>";
                }
                html += "<option value=\"other\">อื่นๆ ระบุ</option>";
            }
            return new HtmlString(html);
        }

        public HtmlString GetUnitPriceForm(int i, string unitId)
        {
            string html = "<option value=\"0\" selected>กรุณาเลือก</option>";
            if (unitId != "")
            {
                //get Unit Price
                List<UnitPrice> prices = db.UnitPrices.Where(x => x.UnitId == new Guid(unitId) && x.IsEnabled == true && x.IsDeleted == false).OrderBy(o => o.CreatedDate).ToList();
                foreach (UnitPrice price in prices)
                {
                    html += "<option value=\"" + price.Price + "\">" + price.Name + "</option>";
                }
                html += "<option value=\"other\">อื่นๆ ระบุ</option>";
            }
            return new HtmlString(html);
        }

        public HtmlString GetWithdrawForm(int i, int line_no)
        {
            string pathImageDel = Url.Content("~/Content/images/icons/delete_remove_icon.svg");
            //get Options
            List<MasterData> masterDatas = db.MasterDatas.Where(x => x.Key.Contains("withdraw") && x.IsEnabled == true && x.IsDeleted == false).OrderBy(o => o.CreatedDate).ToList();

            string html = "<tr id=\"tr-withdraw-" + i + "\"><td class=\"align-middle\"><div id=\"withdraw-line-" + i + "\">" + line_no + "</div></td><td><div id=\"div-select-withdraw-" + i + "\"><select class=\"form-select\" id=\"select-withdraw-" + i + "\" name=\"withdraw-" + i + "\" onchange=\"javascript:WithdrawChange('" + i + "')\"><option value=\"\" selected>กรุณาเลือกข้อมูล</option>";
            foreach (MasterData option in masterDatas)
            {
                html += "<option value=\"" + option.Value + "\">" + option.Text + "</option>";
            }
            html += "<option value=\"other\">อื่นๆ ระบุ</option></select></div><div id=\"div-txt-withdraw-" + i + "\" style=\"display: none;\"><input type=\"text\" class=\"form-control\" id=\"txt-withdraw-" + i + "\" name=\"withdraw-other-" + i + "\" value=\"\"></div></td>" +
                    "<td class=\"align-middle\"><input type=\"text\" class=\"form-control text-center fs-16\" id=\"txt-withdraw-percent-" + i + "\" name=\"withdraw-percent-" + i + "\" value=\"0\" onblur=\"javascript:SumWithdraw('" + i + "')\"></td>" +
                    "<td class=\"align-middle text-red fs-16 text-center\"><p class=\"p-0 m-0\" id=\"lable-withdraw-total-" + i + "\">0.00</p><input type=\"hidden\" id=\"withdraw-total-" + i + "\" name=\"withdraw-total-" + i + "\" value=\"0\"></td>" +
                    "<td class=\"align-middle w-0 text-center bg-gray\">" +
                        "<a href=\"javascript:RemoveWithdraw('" + i + "')\"><img src=\""+ pathImageDel + "\" width=\"16\" title=\"ลบ\"/></a> " +
                    "</td></tr>";
            return new HtmlString(html);
        }

        [HttpPost]
        public ActionResult PostCreate(FormCollection form)
        {
            //contract header
            ConstructionContract contract = new ConstructionContract()
            {
                Id = Guid.NewGuid(),
                ContractNumber = form["ContractNumber"],
                ContractDate = DateTime.Now.Date,
                MadeAt = form["OwnerAddress"],
                OwnerName = form["OwnerName"],
                OwnerNumber = form["OwnerNumber"],
                OwnerAddress = form["OwnerAddress"],
                OwnerTel = form["OwnerTel"],
                ContractorName = form["ContractorName"],
                ContractorLicenseNumber = form["ContractorLicenseNumber"],
                ContractorAddress = form["ContractorAddress"],
                ContractorTel = form["ContractorTel"],
                DescriptionOfWork = form["DescriptionOfWork"],
                WorkSite = form["WorkSite"],
                ContractValue = Convert.ToDecimal(form["ContractValue"]),
                CreatedById = new Guid(form["CreatedById"]),
                CreatedDate = DateTime.Now,
                UpdatedById = new Guid(form["CreatedById"]),
                UpdatedDate = DateTime.Now,
                IsEnabled = true
            };
            db.ConstructionContracts.Add(contract);
            //contract description
            string[] arrayDescription = form["array_description"].Split(',');
            int line = 1;

            var materialData = db.Materials.Where(x => x.IsEnabled == true && x.IsDeleted == false).OrderByDescending(o => o.Sequence).FirstOrDefault();
            int materiaSeq = materialData.Sequence;
            foreach (string desIndex in arrayDescription)
            {
                Material material;
                if (form["material-" + desIndex] == "other")
                {
                    //create material
                    materiaSeq++;
                    material = new Material() {
                        Id = Guid.NewGuid(),
                        Type = "Labor",
                        Name = form["material-other-" + desIndex].ToString(),
                        Sequence = materiaSeq,
                        CreatedById = new Guid(form["CreatedById"]),
                        CreatedDate = DateTime.Now,
                        UpdatedById = new Guid(form["CreatedById"]),
                        UpdatedDate = DateTime.Now,
                        IsEnabled = true
                    };
                    db.Materials.Add(material);
                }
                else {
                    material = db.Materials.Find(new Guid(form["material-" + desIndex]));
                }

                MaterialUnit unit;
                if (form["unit-" + desIndex] == "other")
                {
                    unit = new MaterialUnit() {
                        Id = Guid.NewGuid(),
                        MaterialId = material.Id,
                        Name = form["unit-other-" + desIndex].ToString(),
                        Sequence = 1,
                        CreatedById = new Guid(form["CreatedById"]),
                        CreatedDate = DateTime.Now,
                        UpdatedById = new Guid(form["CreatedById"]),
                        UpdatedDate = DateTime.Now,
                        IsEnabled = true
                    };
                    db.MaterialUnits.Add(unit);
                }
                else {
                    unit = db.MaterialUnits.Find(new Guid(form["unit-" + desIndex]));
                }

                decimal price;
                if (form["unitprice-" + desIndex] == "other")
                {
                    price = Convert.ToDecimal(form["unitprice-other-" + desIndex]);
                    UnitPrice unitPrice = new UnitPrice() {
                        Id = Guid.NewGuid(),
                        UnitId = unit.Id,
                        Name = String.Format("{0:N}", price),
                        Price = price,
                        StartDate = DateTime.Now,
                        Sequence = 1,
                        CreatedById = new Guid(form["CreatedById"]),
                        CreatedDate = DateTime.Now,
                        UpdatedById = new Guid(form["CreatedById"]),
                        UpdatedDate = DateTime.Now,
                        IsEnabled = true
                    };
                    db.UnitPrices.Add(unitPrice);
                   
                }
                else {
                    price = Convert.ToDecimal(form["unitprice-" + desIndex]);
                }

                ContractDescription description = new ContractDescription()
                {
                    Id = Guid.NewGuid(),
                    ConstructionContractId = contract.Id,
                    LineNo = line,
                    Sequence = line,
                    Detail = material.Name,
                    Quantity = Convert.ToDecimal(form["quantity-" + desIndex]),
                    Unit = unit.Name,
                    UnitPrice = price,
                    Total = Convert.ToDecimal(form["description-total-" + desIndex]),
                    CreatedById = new Guid(form["CreatedById"]),
                    CreatedDate = DateTime.Now,
                    UpdatedById = new Guid(form["CreatedById"]),
                    UpdatedDate = DateTime.Now,
                    IsEnabled = true
                };
                db.ContractDescriptions.Add(description);
                line++;
            }
            //withdram
            if(form["array_withdraw"] != "") {              
                string[] arrayWithdraw = form["array_withdraw"].Split(',');
                line = 1;
                MasterData masterData = db.MasterDatas.Where(x => x.Key == "withdraw" && x.IsEnabled == true && x.IsDeleted == false).OrderByDescending(o => o.Sequence).FirstOrDefault();
                int masterSeq = masterData.Sequence;

                foreach (string wdIndex in arrayWithdraw)
                {
                    string detail;
                    if (form["withdraw-" + wdIndex] == "other")
                    {
                        detail = form["withdraw-other-" + wdIndex];
                        //add to master data
                        masterSeq++;
                        db.MasterDatas.Add(new MasterData()
                        {
                            Id = Guid.NewGuid(),
                            Key = "withdraw",
                            Text = detail,
                            Value = detail,
                            Sequence = masterSeq,
                            CreatedById = new Guid(form["CreatedById"]),
                            CreatedDate = DateTime.Now,
                            UpdatedById = new Guid(form["CreatedById"]),
                            UpdatedDate = DateTime.Now,
                            IsEnabled = true
                        });
                    }
                    else
                    {
                        detail = form["withdraw-" + wdIndex];
                    }
                    Withdraw withdraw = new Withdraw()
                    {
                        Id = Guid.NewGuid(),
                        ConstructionContractId = contract.Id,
                        LineNo = line,
                        Sequence = line,
                        Detail = detail,
                        Percent = Convert.ToDecimal(form["withdraw-percent-" + wdIndex]),
                        Total = Convert.ToDecimal(form["withdraw-total-" + wdIndex]),
                        CreatedById = new Guid(form["CreatedById"]),
                        CreatedDate = DateTime.Now,
                        UpdatedById = new Guid(form["CreatedById"]),
                        UpdatedDate = DateTime.Now,
                        IsEnabled = true
                    };
                    db.Withdraws.Add(withdraw);
                    line++;
                    //receipt.Total += withdraw.Total;
                }
                //db.Receipts.Add(receipt);
            }
            db.SaveChanges();

            TempData["OwnerName"] = contract.OwnerName;
            TempData["ContractDate"] = contract.ContractDate.ToString("dd/MM/yyyy");
            if (form["print"] == "Y") {
                 return DownloadPDF(contract.Id);
            }
            return RedirectToAction("Summary", "Receipt");
        }

        [HttpPost]
        public HtmlString ListContractSearch(FormCollection form)
        {
            string name = form["name"];
            string description = form["description"];
            string worksite = form["worksite"];

            string html = "";
            if (name == "" && description == "" && worksite == "")
            {
                html = "<tr><td colspan=\"7\" class=\"text-center\">ไม่มีรายการ</td></tr>";
            }
            else
            {
                var contracts = (from c in db.ConstructionContracts
                                 where c.ContractorName.Contains(name) &&
                                       c.DescriptionOfWork.Contains(description) &&
                                       c.WorkSite.Contains(worksite) &&
                                       c.IsEnabled == true && c.IsDeleted == false
                                 orderby c.CreatedDate ascending
                                 select new
                                 {
                                     Id = c.Id,
                                     ContractNumber = c.ContractNumber,
                                     ContractDate = c.ContractDate,
                                     OwnerName = c.OwnerName,
                                     ContractorName = c.ContractorName,
                                     DescriptionOfWork = c.DescriptionOfWork,
                                     WorkSite = c.WorkSite
                                 }
                                 ).ToList();

                if (contracts.Count > 0)
                {
                    foreach (var item in contracts)
                    {
                        html += string.Format("<tr><td><input class=\"form-check-input p-0 m-0\" type=\"radio\" name=\"selected\" value=\"{6}\"></td><td>{0}</td><td>{1}</td><td>{2}</td><td>{3}</td><td>{4}</td><td>{5}</td></tr>",
                            item.ContractNumber, item.ContractDate.ToString("dd/MM/yyyy"), item.OwnerName, item.ContractorName, item.DescriptionOfWork, item.WorkSite,item.Id);
                    }
                }
                else
                {
                    html = " <tr><td colspan=\"7\" class=\"text-center\">ไม่มีรายการ</td></tr>";
                }
            }
            return new HtmlString(html);
        }

        [HttpPost]
        public HtmlString ListContractSummarySearch(FormCollection form)
        {
            string name = form["name"];
            string startDate = form["startdate"];
            string endDate = form["enddate"];
            decimal sumTotal = 0;

            string html = "";
            if (name == "" && startDate == "" && endDate == "")
            {
                html = "<tr><td colspan=\"9\" class=\"text-center\">ไม่มีรายการ</td></tr>";
            }
            else
            {
                DateTime stDate = startDate == "" ? DateTime.MinValue : DateTime.ParseExact(startDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                DateTime enDate = endDate == "" ? DateTime.MaxValue : DateTime.ParseExact(endDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);

                var contracts = (from c in db.ConstructionContracts
                                 where  c.OwnerName.Contains(name) &&
                                        c.ContractDate >= stDate && c.ContractDate <= enDate &&
                                        c.IsEnabled == true && c.IsDeleted == false
                                 orderby c.CreatedDate ascending
                                 select new
                                 {
                                     Id = c.Id,
                                     ContractNumber = c.ContractNumber,
                                     ContractDate = c.ContractDate,
                                     OwnerName = c.OwnerName,
                                     ContractorName = c.ContractorName,
                                     DescriptionOfWork = c.DescriptionOfWork,
                                     WorkSite = c.WorkSite
                                 }
                                 ).ToList();
                if (contracts.Count > 0)
                {
                    foreach (var item in contracts)
                    {
                        int line = 1;
                        var receipts = (from r in db.Receipts
                                        where r.ConstructionContractId == item.Id &&
                                                r.IsEnabled == true && r.IsDeleted == false
                                        orderby r.Round ascending
                                        select new
                                        {
                                            Id = r.Id,
                                            Round = r.Round,
                                            ReceiptNumber = r.ReceiptNumber,
                                            Total = r.Total
                                        }
                                        ).ToList();
                        if (receipts.Count > 0)
                        {
                            foreach (var receipt in receipts)
                            {
                                sumTotal += receipt.Total;
                                if (line == 1)
                                {
                                    html += string.Format("<tr><td>{0}</td><td>{1}</td><td>{2}</td><td>{3}</td><td>{4}</td><td>{5}</td><td>{6}</td><td><a href=\"{9}\">{7}</a></td><td>{8}</td></tr>",
                                    item.ContractNumber, item.ContractDate.ToString("dd/MM/yyyy"), item.OwnerName, item.ContractorName, item.DescriptionOfWork, item.WorkSite , receipt.Round, receipt.ReceiptNumber, String.Format("{0:n}", receipt.Total) , String.Format("{0}?id={1}", Url.Action("DownloadPDF", "Receipt"), receipt.Id));
                                }
                                else {
                                    html += string.Format("<tr><td colspan=\"6\"></td><td>{0}</td><td><a href=\"{3}\">{1}</a></td><td>{2}</td></tr>",
                                    receipt.Round, receipt.ReceiptNumber, String.Format("{0:n}", receipt.Total), String.Format("{0}?id={1}", Url.Action("DownloadPDF", "Receipt"), receipt.Id));
                                }
                                line++;
                            }
                        }
                        else {
                            html += string.Format("<tr><td>{0}</td><td>{1}</td><td>{2}</td><td>{3}</td><td>{4}</td><td>{5}</td><td colspan=\"3\"></td></tr>",
                                item.ContractNumber, item.ContractDate.ToString("dd/MM/yyyy"), item.OwnerName, item.ContractorName, item.DescriptionOfWork, item.WorkSite);
                        }  
                    }
                }
                else
                {
                    html = "<tr><td colspan=\"9\" class=\"text-center\">ไม่มีรายการ</td></tr>";
                }   
            }
            html += String.Format("<input type=\"hidden\" name=\"sumtotal\" id=\"sum-total\" value=\"{0}\" />", sumTotal);
            return new HtmlString(html);
        }

        [HttpPost]
        public FileResult DownloadSummaryPDF(FormCollection form)
        {
            string name = form["name"];
            string startDate = form["startdate"] == "" ? "01/01/1753" : form["startdate"];
            string endDate = form["enddate"] == "" ? DateTime.Now.ToString("dd/MM/yyyy") : form["enddate"];
            decimal sumTotal = 0;

            LocalReport report = new LocalReport();
            report.ReportPath = Server.MapPath("~/Reports/Summary.rdlc");

            DateTime sd = DateTime.ParseExact(startDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            DateTime ed = DateTime.ParseExact(endDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            var data = db.usp_get_contract_receipt(name, sd, ed).ToList();

            foreach (var item in data) {
                sumTotal += Convert.ToDecimal(item.Total);
            }

            ReportParameter[] param = new ReportParameter[4];
            param[0] = new ReportParameter("Name", form["name"] == "" ? "-" : form["name"]);
            param[1] = new ReportParameter("StartDate", form["startdate"] == ""?"-": form["startdate"]);
            param[2] = new ReportParameter("EndDate", form["enddate"] == "" ? "-" : form["enddate"]);
            param[3] = new ReportParameter("Total", String.Format("{0:n}", sumTotal));

            report.DataSources.Clear();
            report.DataSources.Add(new ReportDataSource("DataSet", data));
            report.SetParameters(param);

            byte[] bytes = report.Render("PDF");

            return File(bytes, "application/pdf", "Summary.pdf");
        }
        
        [HttpGet]
        public FileResult DownloadPDF(Guid? id)
        {
            ConstructionContract contract = db.ConstructionContracts.Find(id);
            var descript = db.usp_get_contract_description(contract.Id.ToString()).ToList();
            var withdraw = db.usp_get_contract_withdraw(contract.Id.ToString()).ToList();
            User creator = db.Users.Find(contract.CreatedById);
            decimal wdPercent = 0;
            decimal wdTotal = 0;
            foreach(var item in withdraw)
            {
                wdPercent += Convert.ToDecimal(item.Percent);
                wdTotal += Convert.ToDecimal(item.Total);
            }

            LocalReport report = new LocalReport();
            report.ReportPath = Server.MapPath("~/Reports/Contract.rdlc");

            ReportParameter[] param = new ReportParameter[17];
            param[0] = new ReportParameter("ContractNumber", contract.ContractNumber);
            param[1] = new ReportParameter("ContractDate", contract.ContractDate.ToString("dd/MM/yyyy"));
            param[2] = new ReportParameter("MadeAt", contract.MadeAt);
            param[3] = new ReportParameter("OwnerName", contract.OwnerName);
            param[4] = new ReportParameter("OwnerAddress", contract.OwnerAddress);
            param[5] = new ReportParameter("OwnerTel", contract.OwnerTel);
            param[6] = new ReportParameter("ContractorName", contract.ContractorName);
            param[7] = new ReportParameter("ContractorAddress", contract.ContractorAddress);
            param[8] = new ReportParameter("ContractorTel", contract.ContractorTel);
            param[9] = new ReportParameter("DescriptionOfWork", contract.DescriptionOfWork);
            param[10] = new ReportParameter("WorkSite", contract.WorkSite);
            param[11] = new ReportParameter("ContractValue", String.Format("{0:n}", contract.ContractValue));
            param[12] = new ReportParameter("SumPercent", String.Format("{0:n}", wdPercent));
            param[13] = new ReportParameter("SumTotal", String.Format("{0:n}", wdTotal));
            param[14] = new ReportParameter("CreatorName", creator.FullName);
            param[15] = new ReportParameter("OwnerNumber", contract.OwnerNumber);
            param[16] = new ReportParameter("ContractorLicenseNumber", contract.ContractorLicenseNumber);

            report.DataSources.Clear();
            report.DataSources.Add(new ReportDataSource("Description", descript));
            report.DataSources.Add(new ReportDataSource("WithDraw", withdraw));
            report.SetParameters(param);

            byte[] bytes = report.Render("PDF");

            return File(bytes, "application/pdf", string.Format("{0}.pdf",contract.ContractNumber));
        }
    }
} 