using GeneratePdfDotNetCore.Models;
using Microsoft.AspNetCore.Mvc;
using Rotativa.AspNetCore;
using Rotativa.AspNetCore.Options;
using System.Diagnostics;

namespace GeneratePdfDotNetCore.Controllers
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

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        /// <summary>
        /// 1.Install package Rotativa.AspNetCore
        /// 2.Setup in program.cs
        /// 3.Copy files from google to wwwroot/Rotativa 
        /// </summary>
        /// <returns></returns>
        public ActionResult GenerateReceipt()
        {
            var receipt = new PaymentReceiptViewModel
            {
                ReceiptNumber = "RCT123456",
                PaymentDate = DateTime.Now,
                AmountPaid = 100.00m,
                CustomerName = "John Doe"
            };

            return View(receipt);
        }

        public ActionResult DownloadReceipt()
        {
            var receipt = new PaymentReceiptViewModel
            {
                ReceiptNumber = "RCT123456",
                PaymentDate = DateTime.Now,
                AmountPaid = 100.00m,
                CustomerName = "John Doe"
            };

            return new ViewAsPdf("GenerateReceipt", receipt)
            {
                FileName = "PaymentReceipt.pdf",
                PageSize = Size.A4,
                PageOrientation = Orientation.Portrait,
                CustomSwitches = "--no-pdf-compression"
            };
        }

        public ActionResult GenerateInvoice()
        {
            return View("~/Views/Home/DownloadInvoice.cshtml", GetInvoice());
        }

        public ActionResult DownloadInvoice()
        {


            return new ViewAsPdf("DownloadInvoice", GetInvoice())
            {
                FileName = "Invoice.pdf",
                PageSize = Size.A4,
                PageOrientation = Orientation.Portrait,
                CustomSwitches = "--no-pdf-compression"
            };
        }

        private invoiceViewModel GetInvoice()
        {
            var body = new List<TableBody>
            {
                new TableBody {
                    Item="Interview",
                    Description="System Admin Profile",
                    RatePerHours=1000,
                    Amount=2000,
                    Qty=2,
                    Currency="INR"
                },
                new TableBody {
                    Item="Interview",
                    Description=".Net Profile",
                    RatePerHours=1000,
                    Amount=1000,
                    Qty=1,
                    Currency="INR"
                }
            };

            return new invoiceViewModel
            {
                InvoiceNumber = "TSSS240910",
                InvoiceGeneratedOn = DateTime.Now,
                Total = 3000.00m,
                BillToCompany = "6th Sense Advertising",
                BillFrom = "Tech Squadron Software Solutions",
                AddressBillFrom = "Mohali, PB, India",
                WebsiteUrl = "https://techsquadronsolutions.com/",
                Body = body
            };
        }
    }

    public class PaymentReceiptViewModel
    {
        public string? ReceiptNumber { get; set; }
        public DateTime PaymentDate { get; set; }
        public decimal AmountPaid { get; set; }
        public string? CustomerName { get; set; }
    }

    public class invoiceViewModel
    {
        public string? InvoiceNumber { get; set; }
        public string? BillToCompany { get; set; }
        public DateTime InvoiceGeneratedOn { get; set; }
        public string? BillFrom { get; set; }
        public string? AddressBillFrom { get; set; }
        public string? WebsiteUrl { get; set; }
        public decimal Total { get; set; }
        public List<TableBody>? Body { get; set; }
    }

    public class TableBody
    {
        public string? Item { get; set; }
        public string? Description { get; set; }
        public string? Currency { get; set; }
        public decimal RatePerHours { get; set; }
        public decimal Qty { get; set; }
        public decimal Amount { get; set; }
    }
}