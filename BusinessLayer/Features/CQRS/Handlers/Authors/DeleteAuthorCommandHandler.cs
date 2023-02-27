using BusinessLayer.Features.CQRS.Commands.Authors;
using BusinessLayer.Repositories;
using EntityLayer.Concrete;
using MediatR;

namespace BusinessLayer.Features.CQRS.Handlers.Authors;

public class DeleteAuthorCommandHandler : IRequestHandler<DeleteAuthorCommandRequest>
{
    private readonly IRepository<Author> _repository;

    public DeleteAuthorCommandHandler(IRepository<Author> repository) => _repository = repository;

    public async Task<Unit> Handle(DeleteAuthorCommandRequest request, CancellationToken cancellationToken)
    {
        var deleted = await _repository.GetAsync(request.Id);
        if (deleted != null)
        {
            await _repository.RemoveAsync(deleted);
        }
        return Unit.Value;
    }
}