using BusinessLayer.Features.CQRS.Commands.Authors;
using BusinessLayer.Repositories;
using EntityLayer.Concrete;
using MediatR;

namespace BusinessLayer.Features.CQRS.Handlers.Authors;

public class CreateAuthorCommandHandler : IRequestHandler<CreateAuthorCommandRequest>
{
    private readonly IRepository<Author> _repository;

    public CreateAuthorCommandHandler(IRepository<Author> repository) => _repository = repository;
    
    public async Task<Unit> Handle(CreateAuthorCommandRequest request, CancellationToken cancellationToken)
    {
        await _repository.CreateAsync(new Author
        {
            Name = request.Name,
            Surname = request.Surname,
            Age = request.Age
        });
        return Unit.Value;
    }
}
