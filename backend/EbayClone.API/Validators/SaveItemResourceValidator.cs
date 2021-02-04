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
                .MaximumLength(100);
            RuleFor(i => i.Description)
                .MaximumLength(800);
            RuleFor(i => i.Price)
                .NotNull();
            RuleFor(i => i.Condition)
                .NotEmpty()
                .MaximumLength(50);
            RuleFor(i => i.IsAuction)
                .NotNull();
            RuleFor(i => i.Quantity)
                .NotNull();
            RuleFor(i => i.SellerId)
                .NotNull()
                .WithMessage("'Seller Id' cannot be 0.");
        }
    }
}