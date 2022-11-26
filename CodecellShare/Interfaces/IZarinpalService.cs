using CodecellShare.Dtos;
using System.Threading.Tasks;

namespace CodecellShare.Interfaces
{
    public interface IZarinpalService
    {
        Task<ZarinpalRequestResultDto> ZarinPalRequestPaymentAsync(ZarinpalRequestDto requestDto);
        Task<ZarinpalVerifyResultDto> ZarinPalVerifyPaymentAsync(ZarinpalVerifyDto verifyDto);
    }
}
