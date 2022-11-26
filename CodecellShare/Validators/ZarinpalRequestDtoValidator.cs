using CodecellShare.Dtos;
using FluentValidation;

namespace CodecellShare.Validators
{
    internal class ZarinpalRequestDtoValidator : AbstractValidator<ZarinpalRequestDto>
    {
        public ZarinpalRequestDtoValidator()
        {
            ClassLevelCascadeMode = CascadeMode.Stop;

            RuleFor(x => x.MerchantId)
                .NotEmpty()
                .WithMessage("MerchantId is required!");

            RuleFor(x => x.CallbackUrl)
                .NotEmpty()
                .WithMessage("CallbackUrl is required!");

            RuleFor(x => x.Amount)
                .GreaterThanOrEqualTo(1000)
                .WithMessage("The amount must be greater than or equal to 1000 rial");
        }
    }
}
