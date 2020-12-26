using EbayClone.API.Resources;
using FluentValidation;

namespace EbayClone.API.Validators
{
    public class SaveUserResourceValidator : AbstractValidator<SaveUserResource>
    {
        public SaveUserResourceValidator()
        {
            RuleFor(u => u.UserName)
				.NotEmpty()
				.MaximumLength(50);
			RuleFor(u => u.FirstName)
				.NotEmpty()
				.MaximumLength(50);
			RuleFor(u => u.LastName)
				.NotEmpty()
				.MaximumLength(50);
			RuleFor(u => u.Email)
				.NotEmpty()
				.MaximumLength(50);
            RuleFor(u => u.Address)
                .NotEmpty()
                .MaximumLength(50);
            RuleFor(u => u.PhoneNumber)
                .MaximumLength(11);
        }
    }
}