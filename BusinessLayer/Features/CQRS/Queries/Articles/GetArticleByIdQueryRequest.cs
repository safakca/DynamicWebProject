using MediatR;
using DtoLayer.Concrete.Articles;

namespace BusinessLayer.Features.CQRS.Queries.Articles;

public class GetArticleByIdQueryRequest : IRequest<ArticlesDto>
{
    public int Id { get; set; }
    public GetArticleByIdQueryRequest(int id)
    {
        Id = id;
    }
}
