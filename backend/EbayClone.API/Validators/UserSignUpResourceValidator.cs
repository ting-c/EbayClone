using EbayClone.API.Resources;
using FluentValidation;

namespace EbayClone.API.Validators
{
    public class UserSignUpResourceValidator : AbstractValidator<UserSignUpResource>
    {
        public UserSignUpResourceValidator()
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
		}
    }
}