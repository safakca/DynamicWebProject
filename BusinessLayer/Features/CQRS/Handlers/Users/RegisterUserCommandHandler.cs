using BusinessLayer.Features.CQRS.Commands.Users;
using BusinessLayer.Repositories;
using EntityLayer.Concrete;
using EntityLayer.Enums;
using MediatR;

namespace BusinessLayer.Features.CQRS.Handlers.Users;
public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommandRequest>
{
    private readonly IRepository<AppUser> _repository;

    public RegisterUserCommandHandler(IRepository<AppUser> repository) => _repository = repository;

    public async Task<Unit> Handle(RegisterUserCommandRequest request, CancellationToken cancellationToken)
    {
        await _repository.CreateAsync(new AppUser
        {
            Password = request.Password,
            UserName = request.Username,
            AppRoleId = (int)RoleType.Member,
        });
        return Unit.Value;
    }
}
