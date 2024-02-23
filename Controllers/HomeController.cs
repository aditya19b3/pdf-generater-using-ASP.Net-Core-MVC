using Microsoft.AspNetCore.Mvc;
using pdf_generater.Models;
using SelectPdf;
using System.Diagnostics;
using System.IO;

namespace pdf_generater.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult GeneratePdf(string html)
        {
            html = html.Replace("StrTag", "<").Replace("EndTag", ">");

            var cssPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "css", "site.css");
            var cssContent = System.IO.File.ReadAllText(cssPath);

            
            html = $@"
                <html>
                    <head>
                     <style>{cssContent}</style>
                    </head>
                    <body>
                        {html}
                    </body>
                </html>";

            HtmlToPdf oHtmlToPdf = new HtmlToPdf();

            PdfDocument oPdfDocument = oHtmlToPdf.ConvertHtmlString(html);
            byte[] pdf = oPdfDocument.Save();
            oPdfDocument.Close();

            return File(
                pdf,
                "application/pdf",
                "StudentList.pdf"
            );
        }

    }
}
