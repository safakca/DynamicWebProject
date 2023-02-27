using DtoLayer.Concrete.Articles;
using MediatR;

namespace BusinessLayer.Features.CQRS.Queries.Articles;

public class GetArticleByIdQueryRequest : IRequest<ArticlesDto>
{
    public int Id { get; set; }
    public GetArticleByIdQueryRequest(int id)
    {
        Id = id;
    }
}
