using EbayClone.API.Resources;
using FluentValidation;

namespace EbayClone.API.Validators
{
    public class SaveUserResourceValidator : AbstractValidator<SaveUserResource>
    {
        public SaveUserResourceValidator()
        {
            RuleFor(u => u.Name)
                .NotEmpty()
                .MaximumLength(50);
        }
    }
}