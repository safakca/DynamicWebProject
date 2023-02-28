using AutoMapper;
using BusinessLayer.Features.CQRS.Commands.Articles;
using BusinessLayer.Repositories;
using DtoLayer.Concrete.Articles;
using EntityLayer.Concrete;
using MediatR;

namespace BusinessLayer.Features.CQRS.Handlers.Articles;

public class UpdateArticleCommandHandler : IRequestHandler<UpdateArticleCommandRequest, UpdateArticleDto>
{
    private readonly IRepository<Article> _repository;
    private readonly IMapper _mapper;
    public UpdateArticleCommandHandler(IRepository<Article> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<UpdateArticleDto> Handle(UpdateArticleCommandRequest request, CancellationToken cancellationToken)
    {
        var article = await _repository.GetAsync(request.Id);

        article.AuthorId = request.AuthorId;
        article.Title = request.Title;
        article.Description = request.Description;
        article.UpdatedDate = DateTime.UtcNow;

        var updated = await _repository.UpdateAsync(article);
        var mapped = _mapper.Map<UpdateArticleDto>(updated);
        return mapped;
    }
}
