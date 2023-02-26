using BusinessLayer.Features.CQRS.Commands.Articles;
using BusinessLayer.Repositories;
using EntityLayer.Concrete;
using MediatR;

namespace BusinessLayer.Features.CQRS.Handlers.Articles;

public class CreateArticleCommandHandler : IRequestHandler<CreateArticleCommandRequest>
{
    private readonly IRepository<Article> _repository;

    public CreateArticleCommandHandler(IRepository<Article> repository) => _repository = repository;

    public async Task<Unit> Handle(CreateArticleCommandRequest request, CancellationToken cancellationToken)
    {
        await _repository.CreateAsync(new Article
        {
            Title = request.Title,
            Description = request.Description,
            CreatedDate = DateTime.UtcNow
        }) ;
        return Unit.Value;
    }
}
