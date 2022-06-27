using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;

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
            ViewBag.MadeAt = "บริษัท เนเจอร์ เอ็ซเทท จำกัด";
            ViewBag.CreateDate = DateTime.Now.ToString("dd/MM/yyyy");

            return View();
        }
        public HtmlString GetDescriptionForm(int i , int line_no) {
            //get Material/Labor
            List<Material> materials = db.Materials.Where(x => x.IsEnabled == true && x.IsDeleted == false).OrderBy(o => o.Sequence).ToList();

            string html = "<tr id=\"tr-description-" + i + "\"><td class=\"align-middle\"><div id=\"description-line-" + i + "\">" + line_no + "</div></td><td><select class=\"form-select\" id=\"select-material-"+i+ "\" name=\"material-"+i+ "\" onchange=\"javascript:MaterialChange('"+i+"')\"><option value=\"\" selected>กรุณาเลือกข้อมูล</option>";
            foreach (Material material in materials) {
                html += "<option value=\""+ material.Id + "\">"+ material.Name + "</option>";
            }
            html += "</select></td>" +
                "<td><input type=\"number\" class=\"form-control\" id=\"txt-quantity-" + i + "\" name=\"quantity-" + i + "\" value=\"0\" onblur=\"javascript:SumDescription('" + i + "')\"></td>" +
                "<td><select class=\"form-select text-start\" id=\"select-unit-" + i + "\" name=\"unit-" + i + "\" onchange=\"javascript:UnitChange('" + i + "')\"><option value=\"\" selected>กรุณาเลือก</option></select></td>" +
                "<td><select class=\"form-select text-start\"id=\"select-unitprice-" + i + "\" name=\"unitprice-" + i + "\" onchange=\"javascript:SumDescription('" + i + "')\"><option value=\"0\" selected>กรุณาเลือก</option></select></td>" +
                "<td class=\"align-middle text-red fs-16 text-center\"><p class=\"p-0 m-0\" id=\"lable-description-total-" + i + "\">0.00</p><input type=\"hidden\" id=\"description-total-" + i + "\" value=\"0\"></td>" +
                "<td class=\"align-middle w-0 text-center bg-gray\">" +
                    "<a href=\"javascript:RemoveDescription('"+i+"')\"><img src=\"../../Content/images/icons/delete_remove_icon.svg\" width=\"16\" title=\"ลบ\"/></a> "+
                "</td></tr>";
            return new HtmlString(html);
        }

        public HtmlString GetUnitForm(int i, string materialId)
        {
            string html = "<option value=\"\" selected>กรุณาเลือก</option>";
            if (materialId != "")
            {
                //get Unit
                List<MaterialUnit> units = db.MaterialUnits.Where(x => x.MaterialId == new Guid(materialId) &&  x.IsEnabled == true && x.IsDeleted == false).OrderBy(o => o.Sequence).ToList();
                foreach (MaterialUnit unit in units)
                {
                    html += "<option value=\"" + unit.Id + "\">" + unit.Name + "</option>";
                }
            }
            return new HtmlString(html);
        }

        public HtmlString GetUnitPriceForm(int i, string unitId)
        {
            string html = "<option value=\"0\" selected>กรุณาเลือก</option>";
            if (unitId != "")
            {
                //get Unit Price
                List<UnitPrice> prices = db.UnitPrices.Where(x => x.UnitId == new Guid(unitId) && x.IsEnabled == true && x.IsDeleted == false).OrderBy(o => o.Sequence).ToList();
                foreach (UnitPrice price in prices)
                {
                    html += "<option value=\"" + price.Price + "\">" + price.Name + "</option>";
                }
            }
            return new HtmlString(html);
        }

        public HtmlString GetWithdrawForm(int i, int line_no)
        {
            //get Options
            List<MasterData> masterDatas = db.MasterDatas.Where(x => x.Key.Contains("withdraw") && x.IsEnabled == true && x.IsDeleted == false).OrderBy(o => o.Sequence).ToList();

            string html = "<tr id=\"tr-withdraw-" + i + "\"><td class=\"align-middle\"><div id=\"withdraw-line-" + i + "\">" + line_no + "</div></td><td><select class=\"form-select\" id=\"select-withdraw-" + i + "\" name=\"withdraw-" + i + "\"><option value=\"\" selected>กรุณาเลือกข้อมูล</option>";
            foreach (MasterData option in masterDatas)
            {
                html += "<option value=\"" + option.Value + "\">" + option.Text + "</option>";
            }
            html += "</select></td>" +
                    "<td class=\"align-middle\"><input type=\"text\" class=\"form-control text-center fs-16\" id=\"txt-withdraw-percent-" + i + "\" name=\"withdraw-percent-" + i + "\" value=\"0\" onblur=\"javascript:SumWithdraw('" + i + "')\"></td>" +
                    "<td class=\"align-middle text-red fs-16 text-center\"><p class=\"p-0 m-0\" id=\"lable-withdraw-total-" + i + "\">0.00</p><input type=\"hidden\" id=\"withdraw-total-" + i + "\" value=\"0\"></td>" +
                    "<td class=\"align-middle w-0 text-center bg-gray\">" +
                        "<a href=\"javascript:RemoveWithdraw('" + i + "')\"><img src=\"../../Content/images/icons/delete_remove_icon.svg\" width=\"16\" title=\"ลบ\"/></a> " +
                    "</td></tr>";
            return new HtmlString(html);
        }
    }
}