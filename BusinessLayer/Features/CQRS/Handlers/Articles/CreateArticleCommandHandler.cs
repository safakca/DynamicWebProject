using AutoMapper;
using BusinessLayer.Features.CQRS.Commands.Articles;
using BusinessLayer.Repositories;
using DtoLayer.Concrete.Articles;
using EntityLayer.Concrete;
using MediatR;

namespace BusinessLayer.Features.CQRS.Handlers.Articles;

public class CreateArticleCommandHandler : IRequestHandler<CreateArticleCommandRequest, CreateArticleDto>
{
    private readonly IRepository<Article> _repository;
    private readonly IMapper _mapper;

    public CreateArticleCommandHandler(IRepository<Article> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<CreateArticleDto> Handle(CreateArticleCommandRequest request, CancellationToken cancellationToken)
    {
        var article = _mapper.Map<Article>(request);
        article.CreatedDate = DateTime.UtcNow;
        var added = await _repository.CreateAsync(article);
        var mapped = _mapper.Map<CreateArticleDto>(added);

        new Exception("Create is succeeded! ");
        return mapped;
    }

}
