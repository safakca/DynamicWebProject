using AutoMapper;
using BusinessLayer.Features.CQRS.Queries.Articles;
using BusinessLayer.Repositories;
using DtoLayer.Concrete.Articles;
using EntityLayer.Concrete;
using MediatR;

namespace BusinessLayer.Features.CQRS.Handlers.Articles;

public class GetArticleByIdQueryHandler : IRequestHandler<GetArticleByIdQueryRequest, ArticlesDto>
{
    private readonly IRepository<Article> _repository;
    private readonly IMapper _mapper;

    public GetArticleByIdQueryHandler(IRepository<Article> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<ArticlesDto> Handle(GetArticleByIdQueryRequest request, CancellationToken cancellationToken)
    {
        var data = await _repository.GetByFilterAsync(x => x.Id == request.Id);
        return _mapper.Map<ArticlesDto>(data);
    }
}
