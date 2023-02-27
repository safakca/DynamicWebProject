using DtoLayer.Concrete.Articles;
using MediatR;

namespace BusinessLayer.Features.CQRS.Commands.Articles;

public record CreateArticleCommandRequest : IRequest<CreateArticleDto>
{
    public int AuthorId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
}
