using DtoLayer.Concrete.Articles;
using EntityLayer.Concrete;
using FluentValidation;

namespace BusinessLayer.Validations;
public class ArticleAddValidator : AbstractValidator<CreateArticleDto>
{
    public ArticleAddValidator()
    {
        RuleFor(x => x.AuthorId).NotNull().NotEmpty().WithMessage("Author Id is Required");
        RuleFor(x => x.Title).NotEmpty().WithMessage("Title is Required");
        RuleFor(x => x.Description).NotEmpty().WithMessage("Description is Required");
    }
}
