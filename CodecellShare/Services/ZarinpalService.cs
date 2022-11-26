using CodecellShare.Constant;
using CodecellShare.Dtos;
using CodecellShare.Exceptions;
using CodecellShare.Extension;
using CodecellShare.Interfaces;
using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CodecellShare.Services
{
    public class ZarinpalService : IZarinpalService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ZarinpalService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<ZarinpalRequestResultDto> ZarinPalRequestPaymentAsync(ZarinpalRequestDto requestDto)
        {
            try
            {
                var validationResult = requestDto.ValidateZarinpalRequest();
                if (!validationResult.Item1)
                {
                    return new ZarinpalRequestResultDto
                    {
                        StatusCode = -101,
                        Message = validationResult.Item2
                    };
                }

                using (var client = _httpClientFactory.CreateClient(ClientName.CodecellZarinpalClientName))
                {
                    var requestJson = JsonSerializer.Serialize(requestDto);
                    var content = new StringContent(requestJson, Encoding.UTF8, "application/json");
                    var response = await client.PostAsync("request.json", content);
                    var responseString = await response.Content.ReadAsStringAsync();
                    if (response.IsSuccessStatusCode)
                    {
                        var result = responseString.GetRequestResponse();
                        if (result.Code == 100)
                        {
                            return new ZarinpalRequestResultDto
                            {
                                Fee = result.Fee,
                                FeeType = result.FeeType,
                                Message = result.Message,
                                StatusCode = result.Code,
                                CallBackUrl = $"https://zarinpal.com/pg/StartPay/{result.Authority}"
                            };
                        }
                        else
                        {
                            return new ZarinpalRequestResultDto
                            {
                                Message = result.Message,
                                StatusCode = result.Code,
                            };
                        }

                    }
                    return new ZarinpalRequestResultDto
                    {
                        Message = responseString,
                        StatusCode = -100
                    };
                }
            }
            catch (Exception ex)
            {
                throw new ZarinpalException(ex.Message);
            }
        }

        public async Task<ZarinpalVerifyResultDto> ZarinPalVerifyPaymentAsync(ZarinpalVerifyDto verifyDto)
        {
            try
            {
                var validationResult = verifyDto.ValidateZarinpalVerify();
                if (!validationResult.Item1)
                {
                    return new ZarinpalVerifyResultDto
                    {
                        StatusCode = -101,
                        Message = validationResult.Item2
                    };
                }

                if (!string.IsNullOrEmpty(verifyDto.Status) && string.Equals(verifyDto.Status, "ok", StringComparison.OrdinalIgnoreCase) && !string.IsNullOrEmpty(verifyDto.Authority))
                {
                    using (var client = _httpClientFactory.CreateClient(ClientName.CodecellZarinpalClientName))
                    {
                        var verifyJson = JsonSerializer.Serialize(verifyDto);
                        var content = new StringContent(verifyJson, Encoding.UTF8, "application/json");
                        var response = await client.PostAsync("verify.json", content);
                        var responseString = await response.Content.ReadAsStringAsync();
                        var result = responseString.GetVerifyResponse();
                        if (result.Code == 100 || result.Code == 101)
                        {
                            return new ZarinpalVerifyResultDto
                            {
                                Message = result.Message,
                                StatusCode = result.Code,
                                CardPan = result.CardPan,
                                CardHash = result.CardHash,
                                RefId = result.RefId,
                                TransactionNumber = verifyDto.Authority.TrimStart('A').ToString().TrimStart('0')
                            };
                        }
                        return new ZarinpalVerifyResultDto
                        {
                            Message = result.Message,
                            StatusCode = result.Code
                        };
                    }
                }

                return new ZarinpalVerifyResultDto
                {
                    Message = "transaction failed or cancel",
                    StatusCode = -102
                };
            }
            catch (Exception ex)
            {
                throw new ZarinpalException(ex.Message);
            }
        }
    }
}
