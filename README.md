## Codecell.Getway.Zarinpal
A light weight library to help online payment using *[Zarinpal](https://www.zarinpal.com)* for `net6 and net7` web application.

## Installation
You can download the latest version of `Codecell.Getway.Zarinpal` from [Github repository](https://github.com/codecellir/Codecell.Gateway.Zarinapl).
To install via `nuget`:
```
Install-Package Codecell.Getway.Zarinpal -Version 6.0.0
```
```
Install-Package Codecell.Getway.Zarinpal -Version 7.0.0
```
Install from [Nuget](https://www.nuget.org/packages/Codecell.Getway.Zarinpal/) directly.

## How to use
Register ZarinpalService to project container in `program.cs` file for `net6 and net7`:
``` C#
using CodeCellZarinpalV7;

builder.Services.AddCodecellZarinpalGetway();
```

then register this service to DI and use it, for example in `HomeController`:
``` C#
using CodeCellZarinpalV7;
...
    public class HomeController : Controller
    {
         private readonly IZarinpalService _zarinpalService;

        public HomeController(IZarinpalService zarinpalService)
        {
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
                MerchantId = "4ced0a1e-4ad8-4309-9668-3ea3ae8e8897",
                Description="test",
                Mobile = "09217245937",
                Email = "codecell.ir@gmail.com"
            };
            var requestResult=await _zarinpalService.ZarinPalRequestPaymentAsync(requestDto);
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
            var result = await _zarinpalService.ZarinPalVerifyPaymentAsync(new ZarinpalVerifyDto
            {
                Amount=100_000,//get it from db via orderId
                Authority=authority,
                Status=status,
                MerchantId= "4ced0a1e-4ad8-4309-9668-3ea3ae8e8897"
            });
            return Json(result);
        }
    }
...
```

## Tutorial video
see the tutorial persian video *[here](https://codecell.ir)* 