using DtoLayer.Concrete.Jwt;
using MediatR;

namespace BusinessLayer.Features.CQRS.Queries;
public class CheckUserQueryRequest : IRequest<CheckUserResponseDto>
{
    public string Username { get; set; } = null!;
    public string Password { get; set; } = null!;
}
