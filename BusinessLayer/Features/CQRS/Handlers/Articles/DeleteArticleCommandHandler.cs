using BusinessLayer.Features.CQRS.Commands.Articles;
using BusinessLayer.Repositories;
using EntityLayer.Concrete;
using MediatR;

namespace BusinessLayer.Features.CQRS.Handlers.Articles;

public class DeleteArticleCommandHandler : IRequestHandler<DeleteArticleCommandRequest>
{
    private readonly IRepository<Article> _repository;

    public DeleteArticleCommandHandler(IRepository<Article> repository) => _repository = repository;

    public async Task<Unit> Handle(DeleteArticleCommandRequest request, CancellationToken cancellationToken)
    {
        var deleted = await _repository.GetAsync(request.Id);
        if (deleted != null)
        {
            await _repository.RemoveAsync(deleted);
        }
        return Unit.Value;
    }
}
