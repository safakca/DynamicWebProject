using DtoLayer.Concrete.Authors;
using FluentValidation;

namespace BusinessLayer.Validations;
public class AuthorAddValidator : AbstractValidator<CreateAuthorDto>
{
    public AuthorAddValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");
        RuleFor(x => x.Surname).NotEmpty().WithMessage("Surname is required");
        RuleFor(x => x.Age).NotEmpty().WithMessage("Age is required");
    }
}
