using EbayClone.API.Resources;
using FluentValidation;

namespace EbayClone.API.Validators
{
    public class SaveItemResourceValidator : AbstractValidator<SaveItemResource>
    {
        public SaveItemResourceValidator()
        {
            RuleFor(i => i.Title)
                .NotEmpty()
                .MaximumLength(50);

            RuleFor(i => i.SellerId)
                .NotEmpty()
                .WithMessage("'Seller Id' cannot be 0.");
        }
    }
}