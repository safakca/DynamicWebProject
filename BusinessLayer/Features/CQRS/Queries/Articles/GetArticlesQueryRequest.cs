using DtoLayer.Concrete.Articles;
using MediatR;

namespace BusinessLayer.Features.CQRS.Queries.Articles;

public class GetArticlesQueryRequest : IRequest<List<ArticlesDto>> { }
