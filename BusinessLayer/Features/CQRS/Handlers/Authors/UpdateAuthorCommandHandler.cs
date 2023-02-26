using BusinessLayer.Features.CQRS.Commands.Authors;
using BusinessLayer.Repositories;
using EntityLayer.Concrete;
using MediatR;

namespace BusinessLayer.Features.CQRS.Handlers.Authors;

public class UpdateAuthorCommandHandler : IRequestHandler<UpdateAuthorCommandRequest>
{
    private readonly IRepository<Author> _repository;

    public UpdateAuthorCommandHandler(IRepository<Author> repository) =>_repository = repository;

    public async Task<Unit> Handle(UpdateAuthorCommandRequest request, CancellationToken cancellationToken)
    {
        var updated = await _repository.GetAsync(request.Id);
        if (updated != null)
        {
            updated.Name = request.Name;
            updated.Surname = request.Surname;
            updated.Age = request.Age;
            updated.UpdatedDate = DateTime.UtcNow;
        }
        return Unit.Value;
    }
}
