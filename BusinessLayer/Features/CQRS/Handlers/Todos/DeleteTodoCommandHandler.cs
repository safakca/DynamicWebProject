using BusinessLayer.Features.CQRS.Commands.Todos;
using BusinessLayer.Repositories;
using EntityLayer.Concrete;
using MediatR;

namespace BusinessLayer.Features.CQRS.Handlers.Todos;

public class DeleteTodoCommandHandler : IRequestHandler<DeleteTodoCommandRequest>
{
    private readonly IRepository<Todo> _repository;

    public DeleteTodoCommandHandler(IRepository<Todo> repository) => _repository = repository;

    public async Task<Unit> Handle(DeleteTodoCommandRequest request, CancellationToken cancellationToken)
    {
        var deleted = await _repository.GetAsync(request.Id);
        if (deleted != null)
        {
            await _repository.RemoveAsync(deleted);
        }
        return Unit.Value;
    }
}