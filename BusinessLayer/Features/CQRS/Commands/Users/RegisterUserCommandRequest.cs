using MediatR;

namespace BusinessLayer.Features.CQRS.Commands.Users;
public record RegisterUserCommandRequest : IRequest
{
    public string? Username { get; set; }
    public string? Password { get; set; }
    public string? Email { get; set; } 
}
