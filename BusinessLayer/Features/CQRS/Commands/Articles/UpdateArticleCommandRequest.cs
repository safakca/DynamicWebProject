using DtoLayer.Concrete.Articles;
using MediatR;

namespace BusinessLayer.Features.CQRS.Commands.Articles;

public class UpdateArticleCommandRequest : IRequest<UpdateArticleDto>
{
    public int Id { get; set; }
    public int AuthorId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
}
