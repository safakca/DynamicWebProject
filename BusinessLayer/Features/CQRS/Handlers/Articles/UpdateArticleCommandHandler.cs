using BusinessLayer.Features.CQRS.Commands.Articles;
using BusinessLayer.Repositories;
using EntityLayer.Concrete;
using MediatR;

namespace BusinessLayer.Features.CQRS.Handlers.Articles;

public class UpdateArticleCommandHandler : IRequestHandler<UpdateArticleCommandRequest>
{
    private readonly IRepository<Article> _repository;

    public UpdateArticleCommandHandler(IRepository<Article> repository) => _repository = repository;
    
    public async Task<Unit> Handle(UpdateArticleCommandRequest request, CancellationToken cancellationToken)
    {
        var updated = await _repository.GetAsync(request.Id);
        if (updated != null)
        {
            updated.Title = request.Title;
            updated.Description = request.Description;
            updated.UpdatedDate = DateTime.UtcNow;
            await _repository.UpdateAsync(updated);
        }
        return Unit.Value;
    }
}
