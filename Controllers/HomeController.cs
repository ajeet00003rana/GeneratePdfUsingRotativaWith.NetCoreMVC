using GeneratePdfDotNetCore.Models;

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
    }

    public class PaymentReceiptViewModel
    {
        public string? ReceiptNumber { get; set; }
        public DateTime PaymentDate { get; set; }
        public decimal AmountPaid { get; set; }
        public string? CustomerName { get; set; }
    }
}