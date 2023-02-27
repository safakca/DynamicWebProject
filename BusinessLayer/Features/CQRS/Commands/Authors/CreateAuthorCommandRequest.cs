using DtoLayer.Concrete.Authors;
using MediatR;

namespace BusinessLayer.Features.CQRS.Commands.Authors;
public record CreateAuthorCommandRequest : IRequest<CreateAuthorDto>
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public int Age { get; set; }
}
