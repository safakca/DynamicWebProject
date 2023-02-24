using MediatR;

namespace BusinessLayer.Features.CQRS.Commands.Articles;

public class CreateArticleCommandRequest : IRequest
{
    public int AuthorId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
}
