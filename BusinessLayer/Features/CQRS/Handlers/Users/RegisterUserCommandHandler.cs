using BusinessLayer.Features.CQRS.Commands.Users;
using EntityLayer.Concrete;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BusinessLayer.Features.CQRS.Handlers.Users;
public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommandRequest>
{
    private readonly UserManager<AppUser> _userManager;

    public RegisterUserCommandHandler(UserManager<AppUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<Unit> Handle(RegisterUserCommandRequest request, CancellationToken cancellationToken)
    {
        var exist = await _userManager.Users.ToListAsync();
        if (exist.Any(x => x.UserName == request.Username))
        {
            throw new Exception("Dublicate Username!");
        }

        await _userManager.CreateAsync(new AppUser
        {
            Name = request.Name,
            Surname = request.Surname,
            UserName = request.Username,
            Email = request.Email,
            MailCode = new Random().Next(10000, 999999).ToString(),
            PasswordHash = request.Password,
            EmailConfirmed = false,
        });
        return Unit.Value;
    }
}


