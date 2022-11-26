using CodecellShare.Dtos;
using CodecellShare.Dtos.Internal;
using CodecellShare.Validators;
using System;
using System.Linq;
using System.Text.Json.Nodes;

namespace CodecellShare.Extension
{
    internal static class CodecellGetwayExtentions
    {
        internal static (bool, string) ValidateZarinpalRequest(this ZarinpalRequestDto requestDto)
        {
            if (requestDto == null)
                throw new ArgumentNullException(nameof(requestDto));

            var validator = new ZarinpalRequestDtoValidator();
            var result = validator.Validate(requestDto);
            if (!result.IsValid)
            {
                return (false, string.Join(",", result.Errors.Select(x => x.ErrorMessage)));
            }
            return (true, string.Empty);
        }
        internal static (bool, string) ValidateZarinpalVerify(this ZarinpalVerifyDto verifyDto)
        {
            if (verifyDto == null)
                throw new ArgumentNullException(nameof(verifyDto));

            var validator = new ZarinpalVerifyDtoValidator();
            var result = validator.Validate(verifyDto);
            if (!result.IsValid)
            {
                return (false, string.Join(",", result.Errors.Select(x => x.ErrorMessage)));
            }
            return (true, string.Empty);
        }

        internal static ZarinpalRequestResponseDto GetRequestResponse(this string responseString)
        {
            var result = new ZarinpalRequestResponseDto();
            dynamic obj = JsonNode.Parse(responseString).AsObject();
            var error = obj["errors"];
            var errorType = ((object)error).GetType();
            if (errorType == typeof(JsonArray))
            {
                var data = obj["data"];
                result.Code = (int)data["code"];
                result.Authority = (string)data["authority"];
                result.Message = (string)data["message"];
                result.Fee = (int)data["fee"];
                result.FeeType = (string)data["fee_type"];
            }
            else
            {
                result.Code = (int)error["code"];
                result.Message = (string)error["message"];
            }
            return result;
        }

        internal static ZarinpalVerifyResponseDto GetVerifyResponse(this string responseString)
        {
            var result = new ZarinpalVerifyResponseDto();
            dynamic obj = JsonNode.Parse(responseString).AsObject();
            var error = obj["errors"];
            var errorType = ((object)error).GetType();
            if (errorType == typeof(JsonArray))
            {
                var data = obj["data"];
                result.Code = (int)data["code"];
                result.Message =(string) data["message"];
                result.Fee = (int)data["fee"];
                result.FeeType = (string)data["fee_type"];
                result.CardHash = (string)data["card_hash"];
                result.CardPan = (string)data["card_pan"];
                result.RefId = (long)data["ref_id"];
            }
            else
            {
                result.Code = (int)error["code"];
                result.Message = (string)error["message"];
            }
            return result;
        }
    }
}
