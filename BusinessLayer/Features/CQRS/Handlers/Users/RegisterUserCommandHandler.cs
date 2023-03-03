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
        var exist = await _repository.GetAllAsync();
        if (exist.Any(x => x.UserName == request.Username))
        {
            throw new Exception("Dublicate Username!");
        }

        await _repository.CreateAsync(new AppUser
        {
            Password = request.Password,
            UserName = request.Username,
            Email = request.Email,
            MailCode = new Random().Next(10000, 999999).ToString(),
            AppRoleId = (int)RoleType.Member,
        }) ;
        return Unit.Value;
    }
}


