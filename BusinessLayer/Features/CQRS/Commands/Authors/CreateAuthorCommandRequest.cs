using MediatR;

namespace BusinessLayer.Features.CQRS.Commands.Authors;
public class CreateAuthorCommandRequest : IRequest
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public int Age { get; set; }
}
