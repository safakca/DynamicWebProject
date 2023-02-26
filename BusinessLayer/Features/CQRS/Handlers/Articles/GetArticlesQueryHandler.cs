using AutoMapper;
using BusinessLayer.Features.CQRS.Queries.Articles;
using BusinessLayer.Repositories;
using DtoLayer.Concrete.Articles;
using EntityLayer.Concrete;
using MediatR;

namespace BusinessLayer.Features.CQRS.Handlers.Articles;

public class GetArticlesQueryHandler : IRequestHandler<GetArticlesQueryRequest, List<ArticlesDto>>
{
    private readonly IRepository<Article> _repository;
    private readonly IMapper _mapper;

    public GetArticlesQueryHandler(IRepository<Article> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<List<ArticlesDto>> Handle(GetArticlesQueryRequest request, CancellationToken cancellationToken)
    {
        var data = await _repository.GetAllAsync();
        return _mapper.Map<List<ArticlesDto>>(data);
    }
}
