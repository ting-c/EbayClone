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
            RuleFor(i => i.Description)
                .NotEmpty()
                .MaximumLength(200);
            RuleFor(i => i.Price)
                .NotEmpty();
            RuleFor(i => i.Condition)
                .NotEmpty()
                .MaximumLength(50);
            RuleFor(i => i.IsAuction)
                .NotEmpty();
            RuleFor(i => i.SellerId)
                .NotEmpty()
                .WithMessage("'Seller Id' cannot be 0.");
        }
    }
}