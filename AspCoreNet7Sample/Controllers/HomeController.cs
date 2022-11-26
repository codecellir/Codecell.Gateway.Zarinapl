using AspCoreNet7Sample.Models;
using CodecellShare.Dtos;
using CodecellShare.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AspCoreNet7Sample.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IZarinpalService _zarinpalService;
        public HomeController(ILogger<HomeController> logger, IZarinpalService zarinpalService)
        {
            _logger = logger;
            _zarinpalService = zarinpalService;
        }

        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> ZarinpalPayment()
        {
            long orderId = 278;
            //get order detail from db with orderId
            string callbackUrl = $"{Request.Scheme}://{Request.Host}/codecell-pay/{orderId}";
            var requestDto = new ZarinpalRequestDto
            {
                Amount = 100_000,////get it from db via orderId
                CallbackUrl = callbackUrl,
                MerchantId = "4ced0a1e-4ad8-4309-9668-3ea3ae8e8897",//use this merchant id for test
                Description = "test",
                Mobile = "09217245937",
                Email = "codecell.ir@gmail.com"
            };
            var requestResult = await _zarinpalService.ZarinPalRequestPaymentAsync(requestDto);
            if (requestResult.IsSuccess)
            {
                return Redirect(requestResult.CallBackUrl);
            }
            return Json(requestResult);
        }

        [HttpGet("codecell-pay/{orderId}")]
        public async Task<IActionResult> CodeCellPayment(long orderId)
        {
            //get order detail from db with orderId
            string status = HttpContext.Request.Query["Status"].ToString();
            string authority = HttpContext.Request.Query["Authority"].ToString();
            status = "OK";//change this to OK for test of result success
            authority = "A00000000000000000000000000000000002";//use this authority for test success result
            var result = await _zarinpalService.ZarinPalVerifyPaymentAsync(new ZarinpalVerifyDto
            {
                //Amount=100_000,//get it from db via orderId
                Amount = 1000,//use this amount for test success result
                Authority = authority,
                Status = status,
                MerchantId = "4ced0a1e-4ad8-4309-9668-3ea3ae8e8897"
            });
            return Json(result);
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
    }
}