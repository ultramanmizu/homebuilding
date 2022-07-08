using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
//using ZetPDF.Drawing;
//using ZetPDF.Pdf;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using System.Diagnostics;


namespace HomeBuilding.Controllers
{
    public class HomeController : Controller
    {
        
        public ActionResult Index()
        {
            //System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

            //PdfDocument document = new PdfDocument();

            //PdfPage page = document.AddPage();

            //XGraphics gfx = XGraphics.FromPdfPage(page);

            //XFont font = new XFont("Tahoma", 16, XFontStyle.Regular);

            //gfx.DrawString("บรรทัดที่ 1 :", font, XBrushes.Black, new XRect(0, 0, page.Width, page.Height), XStringFormats.TopLeft);
            //gfx.DrawString("บรรทัดที่ 2 :", font, XBrushes.Black, new XRect(0, 0, page.Width, page.Height + 10), XStringFormats.TopLeft);

            //gfx.DrawString("บรรทัดที่ 3 :", font, XBrushes.Black, new XRect(0, 0, page.Width, page.Height + 20), XStringFormats.TopLeft);


            //PdfDocument document = new PdfDocument();

            //// Create new page
            //PdfPage page = document.AddPage();
            //page.Size = ZetPDF.PageSize.A4;

            //// Get an XGraphics object for drawing
            //XGraphics gfx = XGraphics.FromPdfPage(page);

            //XPdfFontOptions options = new XPdfFontOptions(PdfFontEncoding.Unicode, PdfFontEmbedding.Always);

            //// Create a font
            //XFont font = new XFont("Tahoma", 20, XFontStyle.BoldItalic);
            ////XFont font = new XFont("Times New Roman", 20, XFontStyle.Regular);
            //// Draw the text
            //gfx.DrawString("ภาษาไทย / English", font, XBrushes.Black,
            //  new XRect(0, 0, page.Width, page.Height),
            //  XStringFormats.Center);

            // Send PDF to browser
            //MemoryStream stream = new MemoryStream();
            //document.Save(stream, false);
            //Response.Clear();
            //Response.ContentType = "application/pdf";
            //Response.AddHeader("content-length", stream.Length.ToString());
            //Response.BinaryWrite(stream.ToArray());
            //Response.Flush();
            //stream.Close();
            //Response.End();
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