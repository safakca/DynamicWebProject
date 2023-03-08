using MediatR;

namespace BusinessLayer.Features.CQRS.Commands.Users;
public record RegisterUserCommandRequest : IRequest
{
    public string? Name { get; set; }
    public string? Surname { get; set; }
    public string? ImageURL { get; set; }
    public string? Gender { get; set; } 
    public string? Username { get; set; }
    public string? Password { get; set; }
    public string? Email { get; set; }
}
