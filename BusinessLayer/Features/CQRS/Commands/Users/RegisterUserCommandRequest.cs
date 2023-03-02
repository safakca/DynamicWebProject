using MediatR;

namespace BusinessLayer.Features.CQRS.Commands.Users;
public class RegisterUserCommandRequest : IRequest
{
    public string? Username { get; set; }
    public string? Password { get; set; }
}
