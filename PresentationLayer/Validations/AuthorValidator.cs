using FluentValidation;
using PresentationLayer.Models;

namespace PresentationLayer.Validations;
public class AuthorValidator : AbstractValidator<CreateAuthorModel>
{
	public AuthorValidator()
	{
		RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");
		RuleFor(x => x.Surname).NotEmpty().WithMessage("Surname is required");
		RuleFor(x => x.Age).NotEmpty().WithMessage("Age is required");
	}
}
