using CodecellShare.Dtos;
using FluentValidation;

namespace CodecellShare.Validators
{
    internal class ZarinpalVerifyDtoValidator : AbstractValidator<ZarinpalVerifyDto>
    {
        public ZarinpalVerifyDtoValidator()
        {
            ClassLevelCascadeMode = CascadeMode.Stop;

            RuleFor(x => x.MerchantId)
                .NotEmpty()
                .WithMessage("MerchantId is required!");

            RuleFor(x => x.Status)
                .NotEmpty()
                .WithMessage("status is required!");

            RuleFor(x => x.Authority)
                .NotEmpty()
                .WithMessage("authority is required!");

            RuleFor(x => x.Amount)
                .NotEmpty()
                .WithMessage("amount is required!");
        }
    }
}
